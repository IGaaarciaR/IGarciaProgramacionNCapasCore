using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using System.Runtime.CompilerServices;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public IActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();

            return View(result);
        }

        [HttpPost]
        public IActionResult CargaMasiva(ML.Usuario usuario)
        {
            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"]; //leer archivo con nombre del archivo que recibe en la vista
            usuario.Status = true;


            if(HttpContext.Session.GetString("PathArchivo") == null) //creamos una Session
            {
                if(excelCargaMasiva.Length > 0) //revisar la longitud del archivo, que no venga nula
                {
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);     //obtener nombre del archivo
                    string folderPath = _configuration["PathFolder:value"]; //donde se guarda path de app settings
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName.ToLower());     //extension del archivo extraido
                    string extensionModulo = _configuration["TipoExcel"];    //extension especificada en app settings

                    if (extensionArchivo == extensionModulo) //validacion de las extensiones - que sean iguales/compatibles
                    {
                        //crear copia del archivo -- Donde esta guardado + nombre del archivo sin extension + concatena la fecha y hora + agrega extension .xlsx
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                //crear la copia del archivo
                                excelCargaMasiva.CopyTo(stream);
                            }

                            string ConnectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result resultUsuarios = BL.Usuario.ConvertirExceltoDataTable(ConnectionString);

                            if(resultUsuarios.Correct) //Si existe informacion en el archivo Excel y se leyo correctamente todos los campos
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);

                                if(resultValidacion.Objects.Count == 0) //se leyo correctamente los datos del excel y no existen errores
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath); //obtener la sesion creada anteriormente
                                }
                                else
                                {
                                    ViewBag.Message = "No se puede completar el registro: Existen campos vacios en su archivo";
                                }

                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "No se pueden leer los registros";
                            }

                        }
                        else
                        {
                            ViewBag.Message = "El archivo no existe en el directorio";
                        }   
                    
                    }
                    else
                    {
                        ViewBag.Message = "La extension de su archivo no es compatible con: " + extensionModulo;
                    }

                }
                else
                {
                    ViewBag.Message = "No existe informacion en su archivo Excel";
                }

            }

            else //si ya existe una sesion entonces agregar el archivo cargado
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExceltoDataTable(connectionString);

                if(resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach(ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem); //agregar el usuario

                        if (!resultAdd.Correct) //si resultan errores al agregar el usuario
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + "   Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if(resultErrores.Objects.Count > 0) //Si existen errores entonces: escribir en un txt los errores
                    {
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"Files\logErrores.txt");
                        using(StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach(string line in resultErrores.Objects)
                            {
                                writer.WriteLine(line);
                            }
                        }

                        ViewBag.Message = "Los Usuarios NO han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message =  "Los Usuarios han sido registrados correctamente";
                    }
                }
            }
            return View("Modal");
        }
    }
}

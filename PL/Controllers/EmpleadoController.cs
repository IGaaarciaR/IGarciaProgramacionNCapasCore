using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();
            ML.Empleado empleado = new ML.Empleado();

            if (result.Correct)
            {    //lista vacia     //lista de aseguradoras
                empleado.Empleados = result.Objects; // estoy pasando de result.objects a aseguradora.Aseguradoras

                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
            // return View();
        }


        //Formulario
        //Vista para actualizar
        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();

            ML.Result resultEmpresas = BL.Empresa.GetAll();//lista de empresas
            
            if (NumeroEmpleado == null)
            {
                empleado.Action = "Add";
                //DropDownList de empresas
                empleado.Empresa.Empresas = resultEmpresas.Objects; 
                return View(empleado); 
            }
            else 
            {
                
                empleado.NumeroEmpleado = NumeroEmpleado; //Insertar el IdAseguradora Leido (nulo o not null) al obj aseguradora
                ML.Result result = BL.Empleado.GetById(empleado); 

                if (result.Correct) 
                {
                    empleado = ((ML.Empleado)result.Object); //Unboxing 
                    empleado.Action = "Update";
                    empleado.Empresa.Empresas = resultEmpresas.Objects; //guardar list empresas en empleado.empresas

                    return View(empleado);
                }
                else
                {
                    //error
                    return View(empleado);
                }
            }
        }


        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {

            IFormFile image = Request.Form.Files["IFImage"];
            if (ModelState.IsValid)
            {
                //valido si traigo imagen
                if (image != null)
                {
                    //llamar al metodo que convierte a bytes la imagen
                    byte[] ImagenBytes = ConvertToBytes(image);
                    //convierto a base 64 la imagen y la guardo en mi objeto usuario
                    empleado.Foto = Convert.ToBase64String(ImagenBytes);
                }

                //agregar usuario
                ML.Result result = new ML.Result();
                if (empleado.Action == "Add") 
                {
                    
                    result = BL.Empleado.Add(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Empleado agregado correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar el empleado: " + result.ErrorMessage;
                    }
                }
                else
                {
                    if(empleado.Action == "Update")
                    {
                        result = BL.Empleado.Update(empleado); //Actualiza el usuario dependiento del valor de su ID

                        if (result.Correct)
                        {
                            ViewBag.Message = "Empleado actualizado correctamente";
                        }
                        else
                        {
                            ViewBag.Message = "Ocurrio un error al actualizar el empleado: " + result.ErrorMessage;
                        }
                    }
                    
                }
                return View("Modal");
            }
            else
            {
                ML.Result resultEmpresas = BL.Empresa.GetAll();

                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = resultEmpresas.Objects;


                return View(empleado);
            }


        }

        public ActionResult Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            if (empleado.NumeroEmpleado == null)
            {

                return View();

            }
            else //Delete
            {
                result = BL.Empleado.Delete(empleado);

                if (result.Correct)
                {
                    ViewBag.Message = "Empleado eliminado correctamente"; //sin dependencias
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar el empleado: " + result.ErrorMessage; //sin dependencias
                }
            }
            return View("Modal");
        }

        //Imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }









}

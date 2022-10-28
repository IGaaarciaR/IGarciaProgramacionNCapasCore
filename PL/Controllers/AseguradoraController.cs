using Microsoft.AspNetCore.Mvc;
using ML;
using System.Net.Http;
using System.Net.Http.Json;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AseguradoraController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //// GETALL: Aseguradora
        //[HttpGet]
        //public ActionResult GetAll()
        //{
        //    ML.Result result = BL.Aseguradora.GetAll();
        //    ML.Aseguradora aseguradora = new ML.Aseguradora();

        //    if (result.Correct)
        //    {    //lista vacia     //lista de aseguradoras
        //        aseguradora.Aseguradoras = result.Objects; // estoy pasando de result.objects a aseguradora.Aseguradoras

        //        return View(aseguradora);
        //    }
        //    else
        //    {
        //        return View(aseguradora);
        //    }
        //    // return View();
        //}

        
        [HttpGet]
        //SERVICIOS WEB API:  GETALL
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result resultAseguradoras = new ML.Result();

            resultAseguradoras.Objects = new List<Object>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("aseguradora/GetAll");
                responseTask.Wait();
                //aqui entra al servicio
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Aseguradora resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(resultItem.ToString());
                        resultAseguradoras.Objects.Add(resultItemList);
                    }
                }
                aseguradora.Aseguradoras = resultAseguradoras.Objects;

            }

            return View(aseguradora);

        }


        //FORM: ASEGURADORA.. GETBYID 
        //Vista para actualizar
        //[HttpGet]
        //public ActionResult Form(int? IdAseguradora)
        //{
        //    ML.Aseguradora aseguradora = new ML.Aseguradora();
        //    aseguradora.Usuario = new ML.Usuario();//instancia de Llave foranea

        //    ML.Result resultUsuarios = BL.Usuario.GetAll(aseguradora.Usuario);//lista de usuarios
        //    //revisar modificacion

        //    if (IdAseguradora == null)
        //    {
        //        //Vista del Metodo agregar para desplegar la DropDownList de Usuarios
        //        aseguradora.Usuario.Usuarios = resultUsuarios.Objects; //llenar la lista de usuarios 
        //        return View(aseguradora); //enviar objeto aseguradora para agregar 
        //    }
        //    else //IdAseguradora no nulo, sig que actualiza su aseguradora
        //    {
        //        aseguradora.IdAseguradora = IdAseguradora.Value; //Insertar el IdAseguradora Leido (nulo o not null) al obj aseguradora
        //        ML.Result result = BL.Aseguradora.GetById(aseguradora); //Llamar getbyid para traer la info

        //        if (result.Correct) //Si GetById trae datos correctos
        //        {
        //            aseguradora = ((ML.Aseguradora)result.Object); //Unboxing insertar datos en aseguradora

        //            aseguradora.Usuario.Usuarios = resultUsuarios.Objects; //guardar list usuarios en aseguradora.usuarios

        //            return View(aseguradora);
        //        }
        //        else
        //        {
        //            //error
        //            return View();
        //        }
        //    }
        //}

        //WEB API: FORM - ASEGURADORA


        [HttpGet]
        //SERVICIOS WEB API: GETBYID
        public ActionResult Form(int? IdAseguradora)
        {
            ML.Result result = new ML.Result();

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.Usuario = new ML.Usuario();//instancia de Llave foranea

            ML.Result resultUsuarios = BL.Usuario.GetAll(aseguradora.Usuario);//lista de usuarios
            //revisar modificacion

            if (IdAseguradora == null)
            {
                //Vista del Metodo agregar para desplegar la DropDownList de Usuarios
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects; //llenar la lista de usuarios 
                return View(aseguradora); //enviar objeto aseguradora para agregar 
            }
            else //IdAseguradora no nulo, sig que actualiza su aseguradora
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    aseguradora.IdAseguradora = IdAseguradora.Value;
                    
                    var responseTask = client.GetAsync("aseguradora/GetById" + IdAseguradora);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;

                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        ML.Aseguradora resultItemList = new ML.Aseguradora();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Aseguradora>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;

                        aseguradora = (ML.Aseguradora)result.Object; //Unboxing insertar datos en aseguradora

                        aseguradora.Usuario.Usuarios = resultUsuarios.Objects; //guardar list usuarios en aseguradora.usuarios

                        result.Correct = true;

                        return View(aseguradora);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla de aseguradora";
                    }
                }

            }
            return View(aseguradora);
        }


        //SERVICIOS WEB API: ADD Y UPDATE
        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {   //Agregar un nuevo usuario
            //ML.Result result = new ML.Result(); //Instancia de Result donde vamos a guardar datos

            //Validar que el ID Aseguradora este vacio para agregar uno nuevo
            if (aseguradora.IdAseguradora == 0)
            {                           //Llamar metodo ADD aseguradora
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("aseguradora/Add", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Aseguradora agregada correctamente";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar la Aseguradora: ";
                    }
                }

            }
            else //Si existe IdAseguradora entonces se actualiza el dato
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Aseguradora>("aseguradora/Update", aseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Aseguradora actualizada correctamente";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar la Aseguradora: ";
                    }
                }
            }
            return View("Modal");
        }



        // FORM: ADD Y UPDATE
        //[HttpPost]
        //public ActionResult Form(ML.Aseguradora aseguradora)
        //{   //Agregar un nuevo usuario
        //    ML.Result result = new ML.Result(); //Instancia de Result donde vamos a guardar datos

        //    //Validar que el ID Aseguradora este vacio para agregar uno nuevo
        //    if (aseguradora.IdAseguradora == 0)
        //    {                           //Llamar metodo ADD aseguradora
        //        result = BL.Aseguradora.Add(aseguradora); //guardar en result los datos de aseguradora
        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Aseguradora agregada correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Ocurrio un error al agregar la Aseguradora " + result.ErrorMessage;
        //        }

        //    }
        //    else //Si existe IdAseguradora entonces se actualiza el dato
        //    {
        //        result = BL.Aseguradora.Update(aseguradora);

        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Aseguradora actualizada correctamente";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Ocurrio un error al actualizar la Aseguradora: " + result.ErrorMessage;
        //        }
        //    }
        //    return View("Modal");
        //}

        ////DELETE: ASEGURADORA
        //public ActionResult Delete(ML.Aseguradora aseguradora)
        //{
        //    ML.Result result = new ML.Result();

        //    if (aseguradora.IdAseguradora == 0)
        //    {

        //        return View();

        //    }
        //    else //Delete
        //    {
        //        result = BL.Aseguradora.Delete(aseguradora);

        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Aseguradora eliminada correctamente"; //sin dependencias
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Ocurrio un error al eliminar la Aseguradora: La aseguradora tiene un usuario asignado" + result.ErrorMessage; //sin dependencias
        //        }
        //    }
        //    return View("Modal");
        //}

        [HttpGet]
        //SERVICIOS WEB API: DELETE
        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            ML.Result resultListAseguradoras = new ML.Result();

            int IdAseguradora = aseguradora.IdAseguradora;

            if (aseguradora.IdAseguradora == 0)
            {

                return View(); //agregar

            }
            else
            {

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    //var postTask = client.GetAsync("usuario/Delete" + '{'+ IdUsuario+'}');
                    var postTask = client.GetAsync("aseguradora/Delete" + IdAseguradora);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        resultListAseguradoras = BL.Aseguradora.GetAll();
                        //return RedirectToAction("GetAll", resultListProduct);
                        if (resultListAseguradoras.Correct)
                        {
                            ViewBag.Message = "Aseguradora eliminada correctamente"; //sin dependencias
                        }
                        else
                        {
                            ViewBag.Message = "No se puede eliminar esta Aseguradora: ";
                        }
                        return View("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "NOT FOUND ";
                    }
                    return View("Modal");
                }

            }

        }

    }
}


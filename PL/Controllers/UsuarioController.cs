using Microsoft.AspNetCore.Mvc;
using ML;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net.Http;
using System.Net.Http.Json;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //GETALL: USUARIO 
        //VISTA DE TODOS LOS USUARIOS + DIRECCION
        //[HttpGet]
        //public ActionResult GetAll()
        //{

        //    ML.Usuario usuario = new ML.Usuario();

        //    ML.Result result = BL.Usuario.GetAll(usuario);

        //    if (result.Correct)
        //    {    //lista vacia     //lista de alumnos
        //        usuario.Usuarios = result.Objects; // estoy pasando de result.objects a alumno.Alumnos

        //        return View(usuario);
        //    }
        //    else
        //    {
        //        return View(usuario);
        //    }
        //}

        //Consumir servicios de web api

        [HttpGet]
        //SERVICIOS WEB API: GETALL 
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultUsuarios = new ML.Result();

            resultUsuarios.Objects = new List<Object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("usuario/GetAll");
                responseTask.Wait();
                //aqui entra al servicio
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuarios.Objects.Add(resultItemList);
                    }
                }
                usuario.Usuarios = resultUsuarios.Objects;

            }

            return View(usuario);
        }


        [HttpPost]
        //SERVICIOS WEB API: BUSQUEDA ABIERTA
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result resultUsuarios = new ML.Result();

            if (usuario.Nombre == null || usuario.ApellidoPaterno == null || usuario.ApellidoMaterno == null)
            {
                if (usuario.Nombre == null)
                {
                    usuario.Nombre = " ";
                }
                if (usuario.ApellidoPaterno == null)
                {
                    usuario.ApellidoPaterno = " ";
                }
                if (usuario.ApellidoMaterno == null)
                {
                    usuario.ApellidoMaterno = " ";
                }
            }

            resultUsuarios.Objects = new List<Object>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    var responseTask = client.GetAsync("usuario/GetAll" + usuario.Nombre + "," + usuario.ApellidoPaterno + "," + usuario.ApellidoMaterno);
                    responseTask.Wait();
                    //aqui entra al servicio
                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        foreach (var resultItem in readTask.Result.Objects)
                        {
                            ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                            resultUsuarios.Objects.Add(resultItemList);
                        }
                    }
                    usuario.Usuarios = resultUsuarios.Objects;

                }
                

            return View(usuario);
        }


        //BUSQUEDA ABIERTA: USUARIO + GETBYID
        //[HttpPost]
        ////Buscador
        //public ActionResult GetAll(ML.Usuario usuario)
        //{
        //    ML.Result result = BL.Usuario.GetAll(usuario);

        //    if (result.Correct)
        //    {    //lista vacia     //lista de alumnos
        //        usuario.Usuarios = result.Objects; // estoy pasando de result.objects a alumno.Alumnos

        //        return View(usuario);
        //    }
        //    else
        //    {
        //        return View(usuario);
        //    }
        //}

        //FORM: USUARIO GETBYID - ACTUALIZAR(UPDATE)
        //[HttpGet]
        ////getbyid
        //public ActionResult Form(int? IdUsuario)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    usuario.Rol = new ML.Rol();//instancia de FK

        //    ML.Result resultRoles = BL.Rol.GetAll();//metodo que devuelve una lista de roles

        //    ML.Result resultPaises = BL.Pais.GetAll(); //devuelve la lista de paises



        //    usuario.Direccion = new ML.Direccion(); //instancia direccion
        //    usuario.Direccion.Colonia = new ML.Colonia(); // direccion a colonia
        //    usuario.Direccion.Colonia.Municipio = new ML.Municipio();   //de colonia a municipio
        //    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();   //de municipio a estado
        //    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();   // de estado a pais 


        //    if (IdUsuario == null)
        //    {
        //        //Agregar
        //        usuario.Rol.Roles = resultRoles.Objects; //llenar la lista de roles 

        //        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;


        //        return View(usuario); //enviar objeto usuario para agregar 
        //    }
        //    else// actualizar
        //    {
        //        usuario.IdUsuario = IdUsuario.Value;

        //        ML.Result result = BL.Usuario.GetById(usuario);


        //        if (result.Correct)
        //        {
        //            usuario = ((ML.Usuario)result.Object); //Unboxing
        //            usuario.Rol.Roles = resultRoles.Objects;


        //            ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);
        //            ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
        //            ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio.Value);

        //            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
        //            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
        //            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
        //            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


        //            return View(usuario);
        //        }
        //        else
        //        {
        //            //error
        //            return View();
        //        }
        //    }
        //}

        [HttpGet]
        //SERVICIOS WEB API: GETBYID
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();

            usuario.Rol = new ML.Rol();//instancia de FK

            ML.Result resultRoles = BL.Rol.GetAll();//metodo que devuelve una lista de roles

            ML.Result resultPaises = BL.Pais.GetAll(); //devuelve la lista de paises



            usuario.Direccion = new ML.Direccion(); //instancia direccion
            usuario.Direccion.Colonia = new ML.Colonia(); // direccion a colonia
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();   //de colonia a municipio
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();   //de municipio a estado
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();   // de estado a pais 

            if (IdUsuario == null)
            {
                //Agregar
                usuario.Rol.Roles = resultRoles.Objects; //llenar la lista de roles 

                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;


                return View(usuario); //enviar objeto usuario para agregar 
            }
            else
            {
                try
                {

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(_configuration["WebAPI"]);

                        var responseTask = client.GetAsync("usuario/GetById" + IdUsuario);
                        responseTask.Wait();
                        var resultAPI = responseTask.Result;

                        if (resultAPI.IsSuccessStatusCode)
                        {
                            var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();

                            ML.Usuario resultItemList = new ML.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            result.Object = resultItemList;

                            usuario = ((ML.Usuario)result.Object); //Unboxing
                            usuario.Rol.Roles = resultRoles.Objects;


                            ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);
                            ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
                            ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio.Value);

                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


                            result.Correct = true;

                            return View(usuario);
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en la tabla de usuario";
                        }

                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.ErrorMessage = ex.Message;

                }
            }
            
            //return result;

            return View(usuario);
        }

        
        [HttpPost]
        //SERVICIOS WEB API: ADD Y UPDATE
        public ActionResult Form(ML.Usuario usuario)
        {
            //Agregar imagen
            IFormFile image = Request.Form.Files["IFImage"];
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto usuario
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            //ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0) //id=0 por lo tanto se agg nuevo usuario
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("usuario/Add", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Usuario agregado correctamente";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar al usuario: ";
                    }
                }
               
            }
            else
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("usuario/Update", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Usuario actualizado correctamente";
                        //return RedirectToAction("GetAll");
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al agregar al usuario: " ;
                    }
                }
            }
            return View("Modal");
        }

        //FORM: USUARIO ADD Y UPDATE + DELETE
        //[HttpPost]
        //public ActionResult Form(ML.Usuario usuario)
        //{

        //    //Agregar imagen
        //    IFormFile image = Request.Form.Files["IFImage"];
        //    if (ModelState.IsValid)
        //    {
        //        //valido si traigo imagen
        //        if (image != null)
        //        {
        //            //llamar al metodo que convierte a bytes la imagen
        //            byte[] ImagenBytes = ConvertToBytes(image);
        //            //convierto a base 64 la imagen y la guardo en mi objeto usuario
        //            usuario.Imagen = Convert.ToBase64String(ImagenBytes);
        //        }
        //        //else
        //        //{
        //        //    usuario.Imagen = null;
        //        //}

        //        //agregar usuario
        //        ML.Result result = new ML.Result();
        //        if (usuario.IdUsuario == 0) //id=0 por lo tanto se agg nuevo usuario
        //        {
        //            result = BL.Usuario.Add(usuario);
        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "Usuario agregado correctamente";
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Ocurrio un error al agregar al usuario: " + result.ErrorMessage;
        //            }
        //        }
        //        else
        //        {

        //            result = BL.Usuario.Update(usuario); //Actualiza el usuario dependiento del valor de su ID

        //            if (result.Correct)
        //            {
        //                ViewBag.Message = "Usuario actualizado correctamente";
        //            }
        //            else
        //            {
        //                ViewBag.Message = "Ocurrio un error al actualizar el usuario: " + result.ErrorMessage;
        //            }
        //        }
        //        return View("Modal");
        //    }
        //    else
        //    {
        //        //ML.Result result = new ML.Result();
        //        //usuario = ((ML.Usuario)result.Object); //Unboxing

        //        ML.Result resultRoles = BL.Rol.GetAll();

        //        usuario.Rol = new ML.Rol();
        //        usuario.Rol.Roles = resultRoles.Objects;


        //        ML.Result resultPaises = BL.Pais.GetAll(); //devuelve la lista de paises

        //        ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais.Value);
        //        ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado.Value);
        //        ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio.Value);


        //        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
        //        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
        //        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
        //        usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

        //        return View(usuario);
        //    }


        //}


        //public ActionResult Delete(ML.Usuario usuario)
        //{
        //    ML.Result result = new ML.Result();

        //    if (usuario.IdUsuario == 0)
        //    {

        //        return View(); //agregar

        //    }
        //    else //Delete
        //    {
        //        //resultDireccion = BL.Usuario.DeleteEF(Direccion)
        //        result = BL.Usuario.Delete(usuario);

        //        if (result.Correct)
        //        {
        //            ViewBag.Message = "Usuario eliminado correctamente"; //sin dependencias
        //        }
        //        else
        //        {
        //            ViewBag.Message = "No se puede eliminar este usuario: " + result.ErrorMessage; //tiene dependencias de llaves foraneas 
        //        }
        //    }
        //    return View("Modal");
        //}


        [HttpGet]
        //SERVICIOS WEB API: DELETE
        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result resultListUsuarios = new ML.Result();
            int IdUsuario = usuario.IdUsuario;

            if (usuario.IdUsuario == 0)
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
                    var postTask = client.GetAsync("usuario/Delete" + IdUsuario );
                    postTask.Wait(); 

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        resultListUsuarios = BL.Usuario.GetAll(usuario);
                        //return RedirectToAction("GetAll", resultListProduct);
                        if (resultListUsuarios.Correct)
                        {
                            ViewBag.Message = "Usuario eliminado correctamente"; //sin dependencias
                        }
                        else
                        {
                            ViewBag.Message = "No se puede eliminar este usuario: ";
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


        //dropdownlist cascada de Pais a Estado
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.EstadoGetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }



        //Imagen
        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        [HttpGet]
        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;

            ML.Result result = BL.Usuario.GetById(usuario);

            if (result.Correct)
            {
                
                usuario = ((ML.Usuario)result.Object);

                usuario.Status = (usuario.Status) ? false : true;

                result = BL.Usuario.Update(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Estatus actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el Estatus" + result.ErrorMessage;
                }
            }
            else
            {
                ViewBag.Message = "Ocurrio un erro al actualizar el Estatus" + result.ErrorMessage;
            }
            return View("Modal");
        }

    }
        
}

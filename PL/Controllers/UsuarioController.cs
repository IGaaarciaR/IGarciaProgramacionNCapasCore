using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {

        //VISTA DE TODOS LOS USUARIOS + DIRECCION
        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {    //lista vacia     //lista de alumnos
                usuario.Usuarios = result.Objects; // estoy pasando de result.objects a alumno.Alumnos

                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }
        //Buscador
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {    //lista vacia     //lista de alumnos
                usuario.Usuarios = result.Objects; // estoy pasando de result.objects a alumno.Alumnos

                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
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
            else// actualizar
            {
                usuario.IdUsuario = IdUsuario.Value;

                ML.Result result = BL.Usuario.GetById(usuario);


                if (result.Correct)
                {
                    usuario = ((ML.Usuario)result.Object); //Unboxing
                    usuario.Rol.Roles = resultRoles.Objects;


                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;


                    return View(usuario);
                }
                else
                {
                    //error
                    return View();
                }
            }
        }


        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //Agregar imagen
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto usuario
                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }

            //agregar usuario
            ML.Result result = new ML.Result();
            if (usuario.IdUsuario == 0) //Agrega un nuevo usuario 
            {
                result = BL.Usuario.Add(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Usuario agregado correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar al usuario: " + result.ErrorMessage;
                }
            }
            else
            {

                result = BL.Usuario.Update(usuario); //Actualiza el usuario dependiento del valor de su ID

                if (result.Correct)
                {
                    ViewBag.Message = "Usuario actualizado correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el usuario: " + result.ErrorMessage;
                }
            }
            return View("Modal");
        }

        public ActionResult Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            if (usuario.IdUsuario == 0)
            {

                return View(); //agregar

            }
            else //Delete
            {
                //resultDireccion = BL.Usuario.DeleteEF(Direccion)
                result = BL.Usuario.Delete(usuario);

                if (result.Correct)
                {
                    ViewBag.Message = "Usuario eliminado correctamente"; //sin dependencias
                }
                else
                {
                    ViewBag.Message = "No se puede eliminar este usuario: " + result.ErrorMessage; //tiene dependencias de llaves foraneas 
                }
            }
            return View("Modal");
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

    }
}

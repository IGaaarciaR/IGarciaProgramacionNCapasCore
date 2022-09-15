using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        // GET: Aseguradora
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            if (result.Correct)
            {    //lista vacia     //lista de aseguradoras
                aseguradora.Aseguradoras = result.Objects; // estoy pasando de result.objects a aseguradora.Aseguradoras

                return View(aseguradora);
            }
            else
            {
                return View(aseguradora);
            }
            // return View();
        }


        //Formulario
        //Vista para actualizar
        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
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
                aseguradora.IdAseguradora = IdAseguradora.Value; //Insertar el IdAseguradora Leido (nulo o not null) al obj aseguradora
                ML.Result result = BL.Aseguradora.GetById(aseguradora); //Llamar getbyid para traer la info

                if (result.Correct) //Si GetById trae datos correctos
                {
                    aseguradora = ((ML.Aseguradora)result.Object); //Unboxing insertar datos en aseguradora

                    aseguradora.Usuario.Usuarios = resultUsuarios.Objects; //guardar list usuarios en aseguradora.usuarios

                    return View(aseguradora);
                }
                else
                {
                    //error
                    return View();
                }
            }
        }


        //Agregar y actualizar
        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {   //Agregar un nuevo usuario
            ML.Result result = new ML.Result(); //Instancia de Result donde vamos a guardar datos

            //Validar que el ID Aseguradora este vacio para agregar uno nuevo
            if (aseguradora.IdAseguradora == 0)
            {                           //Llamar metodo ADD aseguradora
                result = BL.Aseguradora.Add(aseguradora); //guardar en result los datos de aseguradora
                if (result.Correct)
                {
                    ViewBag.Message = "Aseguradora agregada correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar la Aseguradora " + result.ErrorMessage;
                }

            }
            else //Si existe IdAseguradora entonces se actualiza el dato
            {
                result = BL.Aseguradora.Update(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Aseguradora actualizada correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar la Aseguradora: " + result.ErrorMessage;
                }
            }
            return View("Modal");
        }


        public ActionResult Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            if (aseguradora.IdAseguradora == 0)
            {

                return View();

            }
            else //Delete
            {
                result = BL.Aseguradora.Delete(aseguradora);

                if (result.Correct)
                {
                    ViewBag.Message = "Aseguradora eliminada correctamente"; //sin dependencias
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar la Aseguradora: La aseguradora tiene un usuario asignado" + result.ErrorMessage; //sin dependencias
                }
            }
            return View("Modal");
        }

    }
}


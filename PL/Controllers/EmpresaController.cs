using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {

        // GET: Empresa
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Empresa.GetAll();
            ML.Empresa empresa = new ML.Empresa();

            if (result.Correct)
            { 
                empresa.Empresas = result.Objects; 

                return View(empresa);
            }
            else
            {
                return View(empresa);
            }
        }


        //Formulario
        //Vista para actualizar
        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();

           
            if (IdEmpresa == null)
            {
                return View(empresa); 
            }
            else 
            {
                empresa.IdEmpresa = IdEmpresa.Value; 
                ML.Result result = BL.Empresa.GetById(empresa); 

                if (result.Correct) 
                {
                    empresa = ((ML.Empresa)result.Object); //Unboxing

                   
                    return View(empresa);
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
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en mi objeto usuario
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }

            //Agregar nueva empresa
            ML.Result result = new ML.Result();

            if (empresa.IdEmpresa == 0)
            {                          
                result = BL.Empresa.Add(empresa); 
                if (result.Correct)
                {
                    ViewBag.Message = "Empresa agregada correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar la Empresa: " + result.ErrorMessage;
                }

            }
            else //Si existe IdAseguradora entonces se actualiza el dato
            {
                result = BL.Empresa.Update(empresa);

                if (result.Correct)
                {
                    ViewBag.Message = "Empresa actualizada correctamente";
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar la Empresa: " + result.ErrorMessage;
                }
            }
            return View("Modal");
        }


        public ActionResult Delete(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            if (empresa.IdEmpresa == 0)
            {

                return View();

            }
            else //Delete
            {
                result = BL.Empresa.Delete(empresa);

                if (result.Correct)
                {
                    ViewBag.Message = "Empresa eliminada correctamente"; //sin dependencias
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al eliminar la Empresa: " + result.ErrorMessage; //sin dependencias
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

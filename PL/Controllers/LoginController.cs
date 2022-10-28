using BL;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public LoginController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //public IActionResult Login()
        //{

        //    return View();
        //}
        [HttpGet]
        public IActionResult Login()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("login/inicioSesion");
                responseTask.Wait();
                //aqui entra al servicio
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();
                }
            }
            return View();
        }


        //POST: GETBYUSERNAME
        //[HttpPost]
        //public ActionResult Login(string username, string password)
        //{
        //    ML.Result result = new ML.Result();
        //    ML.Usuario usuario = new ML.Usuario();

        //    result = BL.Usuario.GetByUserName(username);

        //    if (result.Correct)
        //    {
        //        usuario = (ML.Usuario)result.Object; //unboxing

        //        if(usuario.Password == password &&usuario.UserName == username)
        //        {
        //            return RedirectToAction("Index", "Home");   
        //            //return  View(); //mandar llamar una vista 
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Contraseña incorrecta ";
        //        }
        //    }
        //    else
        //    {
        //        ViewBag.Message = "Usuario incorrecto ";
        //    }

        //    return View("Modal");
        //}
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);
                usuario.UserName = username;
                usuario.Password = password;

                //var responseTask = client.GetAsync("login/Login" + usuario.UserName + "%2" + usuario.Password + "?"+ "username="+username + "&"+ "password=" +password);
                //var responseTask = client.GetAsync("http://localhost:5098/api/login/LoginBety00%2Cbety1234GF.?username=Bety00&password=bety1234GF.");
                var responseTask = client.GetAsync("login/Login/"+ username + ","+ password);


                responseTask.Wait();
                var resultAPI = responseTask.Result;

                //aqui entra al servicio

                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    ML.Usuario resultItemList = new ML.Usuario();
                    resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                    result.Object = resultItemList;

                    usuario = (ML.Usuario)result.Object; //unboxing
                    result.Correct = true;
                    if (result.Correct)
                    {
                        if (usuario.Password == password && usuario.UserName == username)
                        {
                            return RedirectToAction("Index", "Home"); //GetAll, usuario
                            //mandar llamar una vista  - home
                        }
                        else
                        {
                            ViewBag.Message = "Contraseña incorrecta ";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Usuario incorrecto ";
                    }

                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No existen registros en la tabla de usuario";
                    ViewBag.Message = "Algo salio mal  ";
                }
                return View("Modal");
            }
        }



    }
}

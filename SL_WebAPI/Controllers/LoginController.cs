using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/<LoginController>
        [Route("inicioSesion")]
        [HttpGet]
        public IActionResult Login()
        {
                return Ok();
        }


        // POST:: api/<LoginController>
        [Route("Login/{username},{password}")]
        [HttpGet]
        public IActionResult Login(string username, string password)
        {
            ML.Result result = new ML.Result();
            ML.Usuario usuario = new ML.Usuario();

            result = BL.Usuario.GetByUserName(username);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }


    }
}

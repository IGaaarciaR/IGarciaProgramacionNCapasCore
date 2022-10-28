using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }


        [Route("GetById{IdUsuario}")]
        [HttpGet]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.IdUsuario = IdUsuario;

            ML.Result result = BL.Usuario.GetById(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("GetAll{Nombre},{ApellidoPaterno},{ApellidoMaterno}")]
        [HttpGet]
        public IActionResult GetAll(string? Nombre, string? ApellidoPaterno, string? ApellidoMaterno)
        {
            ML.Usuario usuario = new ML.Usuario();
            
                usuario.Nombre = Nombre;
                usuario.ApellidoPaterno = ApellidoPaterno;
                usuario.ApellidoMaterno = ApellidoMaterno;
            

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("Update")]
        [HttpPost]
        public IActionResult Update([FromBody] ML.Usuario usuario)
        {

            ML.Result result = BL.Usuario.Update(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("Update{IdUsuario}")]
        [HttpPut]
        public IActionResult Update(int IdUsuario, [FromBody] ML.Usuario usuario)
        {
            //ML.Usuario usuario = new ML.Usuario();

            usuario.IdUsuario = IdUsuario;
            if(usuario.IdUsuario != null)
            {
                
                ML.Result result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {

                    return Ok(result);
                }
                else
                {
                    return NotFound();

                }
            }
            else
            {
                return NotFound();

            }

        }


        // DELETE api/<UsuarioController>/5
        [Route("Delete{IdUsuario}")]
       [HttpGet]
        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.IdUsuario = IdUsuario;

            ML.Result result = BL.Usuario.Delete(usuario);

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

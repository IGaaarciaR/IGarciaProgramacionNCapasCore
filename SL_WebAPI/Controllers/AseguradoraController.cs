using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL_WebAPI.Controllers
{
    [Route("api/aseguradora")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        // GET: api/AseguradoraController>
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("GetById{IdAseguradora}")]
        [HttpGet]
        public IActionResult GetById(int IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            aseguradora.IdAseguradora = IdAseguradora;

            ML.Result result = BL.Aseguradora.GetById(aseguradora);

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
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {

            ML.Result result = BL.Aseguradora.Add(aseguradora);

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
        public IActionResult Update([FromBody] ML.Aseguradora aseguradora)
        {

            ML.Result result = BL.Aseguradora.Update(aseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();

            }
        }

        [Route("Delete{IdAseguradora}")]
        [HttpGet]
        public IActionResult Delete(int IdAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            aseguradora.IdAseguradora = IdAseguradora;

            ML.Result result = BL.Aseguradora.Delete(aseguradora);

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

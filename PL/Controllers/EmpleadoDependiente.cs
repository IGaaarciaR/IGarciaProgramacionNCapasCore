using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependiente : Controller
    {
        // GET: Empleado Dependiente

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = BL.Dependiente.GetAllDependiente();
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

        //GetById
        [HttpGet]
        public ActionResult GetByIdEmpleado(string NumeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.Dependiente = new ML.Dependiente();


            //ML.Result resultDependientes = BL.Dependiente.DependienteGetByIdEmpleado(NumeroEmpleado);

            if (NumeroEmpleado != null)
            {
                //empleado.Action = "Add";
                //DropDownList de empresas
            //empleado.Dependiente.Dependientes = resultDependientes.Objects;
                //empleado.Empresa.Empresas = resultEmpresas.Objects;
                return View(empleado);
            }

            return View(empleado);
        }
    }
}

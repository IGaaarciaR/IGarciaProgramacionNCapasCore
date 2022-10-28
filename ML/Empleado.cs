using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public string NumeroEmpleado { get; set; }
        public string RFC { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string FechaNacimiento { get; set; }
        public string NSS { get; set; } = null!;
        public string FechaIngreso { get; set; }
        public string Foto { get; set; }
        public List<object> Empleados { get; set; }
        public ML.Empresa? Empresa { get; set; } //Referencia a una tabla -- empresa
        public int? IdEmpresa { get; set; }
        public string Action { get; set; }

        public ML.Dependiente Dependiente { get; set; } 
    }
}

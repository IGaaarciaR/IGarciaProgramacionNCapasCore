using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Dependiente
    {
        public int IdDependiente { get; set; }
        public string NumeroEmpleado { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; } = null!;
        public string Genero { get; set; } = null!;
        public string? Telefono { get; set; }
        public string Rfc { get; set; } = null!;
        public int? IdDependienteTipo { get; set; }

        public virtual DependienteTipo? IdDependienteTipoNavigation { get; set; }
        public virtual Empleado NumeroEmpleadoNavigation { get; set; } = null!;

        public string NombreCompletoEmpleado { get; set; }

    }
}

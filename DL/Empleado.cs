using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public Empleado()
        {
            Dependientes = new HashSet<Dependiente>();
        }

        public string NumeroEmpleado { get; set; } = null!;
        public string? Rfc { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Nss { get; set; } = null!;
        public DateTime FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public int? IdEmpresa { get; set; }

        public virtual Empresa? IdEmpresaNavigation { get; set; }
        public virtual ICollection<Dependiente> Dependientes { get; set; }
        public string NombreEmpresa { get; set; }


        //Referencias Dependiente
        public string NombreCompletoEmpleado { get; set; } 
        public int DIDdependiente { get; set; } 
        public string DNumEmpleado { get; set; } 
        public string DNombre { get; set; } = null!;    
        public string DApellidoPaterno { get; set; }  
        public string DApellidoMaterno { get; set; }   
        public DateTime DFechaNac { get; set; } 
        public string DGenero { get; set; }    
        public string DTelefono { get; set; }   
        public string DRFC { get; set; }
        public string DEstadoCivil { get; set; }  
        public int DIdDependienteTipo { get; set; }


    }
}

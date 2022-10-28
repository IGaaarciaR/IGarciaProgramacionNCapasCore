using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Usuario
    {
        public Usuario()
        {
            Aseguradoras = new HashSet<Aseguradora>();
            Direccions = new HashSet<Direccion>();
        }

        public int IdUsuario { get; set; }
        public string UserName { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string ApellidoMaterno { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? Celular { get; set; }
        public string? Curp { get; set; }
        public byte? IdRol { get; set; }
        public string? Imagen { get; set; }
        public bool? Status { get; set; }

        public virtual Rol? IdRolNavigation { get; set; }
        public virtual ICollection<Aseguradora> Aseguradoras { get; set; }
        public virtual ICollection<Direccion> Direccions { get; set; }


        public string NombreCompleto { get; set; }

        //referencias Rol
        public string Rol { get; set; }


        //Referencias Direccion
        public int IdDireccion { get; set; }
        public string Calle { get; set; }
        public string NumeroInterior { get; set; }
        public string NumeroExterior { get; set; }


        //Referencias Colonia
        public int IdColonia { get; set; }
        public string ColoniaNombre { get; set; }
        public string CodigoPostal { get; set; }


        //Referencias Municipio
        public int IdMunicipio { get; set; }
        public string MunicipioNombre { get; set; }

        //Referencias Estado
        public int IdEstado { get; set; }
        public string EstadoNombre { get; set; }

        //Referencias Pais
        public int IdPais { get; set; }
        public string PaisNombre { get; set; }


    }
}

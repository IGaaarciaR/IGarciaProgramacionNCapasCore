using System;
using System.Collections.Generic;
using System.Globalization;

namespace DL
{
    public partial class Aseguradora
    {
        public int IdAseguradora { get; set; }
        public string Nombre { get; set; } = null!;
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public int? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }

        public string NombreUsuario { get; set; }
        public string ApellidoPUsuario { get; set; }
        public string ApellidoMUsuario { get; set; }

        public string NombreCompleto
        {
            get
            {
                return String.Format("{0} {1}, {2}", ApellidoPUsuario, ApellidoMUsuario, NombreUsuario);
            }
        }
    }
}

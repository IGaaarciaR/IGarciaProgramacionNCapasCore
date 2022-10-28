using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int? IdDireccion { get; set; }

        //[Required(ErrorMessage = "Ingresa la Calle")]
        //[RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Ingrese solo letras y numeros")]
        //[StringLength(50)]
        public string? Calle { get; set; }

        //[Required(ErrorMessage = "Ingresa el numero interior")]
        //[RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Ingrese solo numeros y/o Letras")]
        //[StringLength(7)]
        //[DisplayName("Numero Interior")]
        public string? NumeroInterior { get; set; }

        //[Required(ErrorMessage = "Ingresa el numero exterior")]
        //[RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "Ingrese solo numeros y/o Letras")]
        //[StringLength(7)]
        //[DisplayName("Numero Exterior")]
        public string? NumeroExterior { get; set; }


        public ML.Colonia Colonia { get; set; } //acceder a colonia
        public List<object>? Direcciones { get; set; }
    }
}

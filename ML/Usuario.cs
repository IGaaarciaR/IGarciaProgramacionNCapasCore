using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ML
{

    public class Usuario
    {
        //Propiedades con modificador de acceso 
        public int IdUsuario { get; set; }



        //[Required(ErrorMessage = "Se requiere un Nombre de usuario")]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Minimo 5 caracteres para el nombre de usuario")]
        [DisplayName("UserName / Usuario")]
        public string? UserName { get; set; }



        //[Required(ErrorMessage = "Se requiere el Nombre")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Ingrese solo letras")]
        //[StringLength(50)]
        public string? Nombre { get; set; }




        //[Required(ErrorMessage = "Se requiere el Apellido Paterno")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Ingrese solo letras")]
        [DisplayName("Apellido Paterno")]
        //[StringLength(50)]
        public string? ApellidoPaterno { get; set; }


        //[Required(ErrorMessage = "Se requiere el Apellido Materno")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Ingrese solo letras")]
        //[StringLength(50)]
        [DisplayName("Apellido Materno")]
        public string? ApellidoMaterno { get; set; }


        //[StringLength(254)]
        //[Required(ErrorMessage = "Se requiere un Email")]
        //[RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Ingrese un email correcto")]
        public string? Email { get; set; }



        //[Required(ErrorMessage = "Se requiere una contraseña")]
        //[StringLength(16, MinimumLength = 8)]
        public string? Password { get; set; }



        //[Required(ErrorMessage = "Se requiere ingresar la Fecha de Nacimiento")]

        [DisplayName("Fecha de Nacimiento")]
        public string? FechaNacimiento { get; set; }




        //[Required(ErrorMessage = "Selecciona una opcion")]
        [DisplayName("Sexo")]
        public string? Sexo { get; set; }




        //[Required(ErrorMessage = "Ingresa el Telefono")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese un telefono correcto")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese solo numeros")]
        public string? Telefono { get; set; }




        //[Required(ErrorMessage = "Ingresa el Celular")]
        //[RegularExpression(@"^[0-9]+$", ErrorMessage = "Ingrese solo numeros")]
        //[StringLength(10, MinimumLength = 10, ErrorMessage = "Ingrese un celular correcto")]
        public string? Celular { get; set; }



        //[Required(ErrorMessage = "Ingresa la CURP")]
        //[StringLength(18, MinimumLength = 18)]
        //[RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Ingrese solo letras y numeros")]
        public string? CURP { get; set; }


        public string? Imagen { get; set; }
        public List<object>? Usuarios { get; set; }
        public ML.Rol? Rol { get; set; } //Referencia a una tabla -- Es una propiedad de navegacion
        public ML.Direccion? Direccion { get; set; } //llave foranea 

        [DisplayName("Nombre Completo")]
        public string? NombreCompleto { get; set; }


        [DisplayName("Estatus")]
        public bool Status { get; set; }

    }
}
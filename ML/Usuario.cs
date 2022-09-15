namespace ML
{
    public class Usuario
    {
        //Propiedades con modificador de acceso 
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string CURP { get; set; }

        public string Imagen { get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; } //Referencia a una tabla -- Es una propiedad de navegacion
        public ML.Direccion Direccion { get; set; } //llave foranea 

        public string NombreCompleto { get; set; }

    }
}
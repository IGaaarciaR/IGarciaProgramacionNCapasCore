// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using ML;
using System.Diagnostics.Metrics;

ReadFile();


static void ReadFile()
{
    string file = @"C:\Users\digis\OneDrive\Documents\Irma Garcia Ramirez\CargaMasivaUsuario.txt"; //ruta
    
    if (File.Exists(file))
    {
        //ML.Result result = new ML.Result();

        ML.Result resultErrores = new ML.Result();
        resultErrores.Objects = new List<object>();


        StreamReader Text = new StreamReader(file);
        string Line;
        Line = Text.ReadLine();

        while ((Line = Text.ReadLine()) != null)
        {
            string[] lines = Line.Split('|');

            ML.Usuario usuario = new ML.Usuario();

            usuario.UserName = lines[0];
            usuario.Nombre = lines[1];
            usuario.ApellidoPaterno = lines[2];
            usuario.ApellidoMaterno = lines[3]; 
            usuario.Email = lines[4];
            usuario.Password = lines[5];
            usuario.FechaNacimiento = lines[6];
            usuario.Sexo = lines[7];
            usuario.Telefono = lines[8];
            usuario.Celular = lines[9];
            usuario.CURP = lines[10];

            usuario.Rol = new ML.Rol();
            usuario.Rol.IdRol = byte.Parse(lines[11]);

            usuario.Imagen = null;
            usuario.Status = bool.Parse(lines[12]);

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Calle = lines[13];
            usuario.Direccion.NumeroInterior = lines[14];   
            usuario.Direccion.NumeroExterior = lines[15];

            usuario.Direccion.Colonia = new ML.Colonia();   
            usuario.Direccion.Colonia.IdColonia = int.Parse(lines[16]);

            ML.Result result = BL.Usuario.Add(usuario);

            if (!result.Correct) //diferente de correctto !
            {
                resultErrores.Objects.Add(
                    "No se inserto el User name " + usuario.UserName +
                    "No se inserto el Nombre " + usuario.Nombre +
                    "No se inserto el Apellido Paterno " + usuario.ApellidoPaterno +
                    "No se inserto el Apellido Materno " + usuario.ApellidoMaterno +
                    "No se inserto el Email  " + usuario.Email +
                    "No se inserto el Password  " + usuario.Password +
                    "No se inserto el Fecha de Nacimiento  " + usuario.FechaNacimiento +
                    "No se inserto el Sexo " + usuario.Sexo +
                    "No se inserto el Telefono " + usuario.Telefono +
                    "No se inserto el Celular " + usuario.Celular +
                    "No se inserto el CURP " + usuario.CURP +
                    "No se inserto el ID Rol " + usuario.Rol.IdRol +
                    "No se inserto la Imagen " + usuario.Imagen +
                    "No se inserto el Estatus " + usuario.Status +
                    "No se inserto la Calle " + usuario.Direccion.Calle +
                    "No se inserto el Numero Interior " + usuario.Direccion.NumeroInterior +
                    "No se inserto el Numero Exterior " + usuario.Direccion.NumeroExterior +
                    "No se inserto el ID de COlonia " + usuario.Direccion.Colonia.IdColonia);
            }
            else
            {
                Console.WriteLine("Se guardaron los registros exitosamente");
            }
            if (resultErrores.Objects != null)
            {
                

            }
            TextWriter tw = new StreamWriter(@"C:\Users\digis\OneDrive\Documents\Irma Garcia Ramirez\ErroresCargaMasivaUsuario.txt");
            foreach (string Error in resultErrores.Objects)
            {
                tw.WriteLine(Error); //lista de erroes
                //Console.WriteLine("Error");
            }
            tw.Close();
        }
    }
}


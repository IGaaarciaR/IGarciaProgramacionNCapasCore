using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByUserName(string username)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.UserName = username;


            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{usuario.UserName}'").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;

                        //info de rol
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        //usuario.Rol.Nombre = query.Rol.ToString();

                        //info de Imagen 
                        usuario.Imagen = query.Imagen.ToString();

                        //Info de Status
                        usuario.Status = query.Status.Value;

                        //Info de Direccion
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle.ToString();
                        usuario.Direccion.NumeroInterior = query.NumeroInterior.ToString();
                        usuario.Direccion.NumeroExterior = query.NumeroExterior.ToString();

                        //Info de Colonia 
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.ColoniaNombre.ToString();
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal.ToString();


                        //Info de Municipio
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.MunicipioNombre.ToString();

                        //Info de Estado
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.EstadoNombre.ToString();

                        //Info de Pais
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.PaisNombre.ToString();

                        usuario.NombreCompleto = query.NombreCompleto.ToString();

                        result.Object = usuario; //boxing

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }
        

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    usuario.Status = true;
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', {usuario.Status}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");
                    //ExecuteSql-- consulta que no devuelva informacion-- add, delete, update (modelo general base de datos)
                    //FromSql -- consultas GetAll y GetByID (llama a la entidad)

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', {usuario.Status}, '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

                    if (query > 0)
                    {
                        //usuario.Status = (usuario.Status) ? false : true;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    //FromSql -- consultas GetAll y GetByID
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString();
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;


                            //Info de Rol
                            usuario.Rol = new ML.Rol();

                            usuario.Rol.IdRol = (byte)obj.IdRol;
                            usuario.Rol.Nombre = obj.Rol.ToString();

                            //info de Imagen 
                            usuario.Imagen = obj.Imagen;

                            //Info de Status
                            usuario.Status = obj.Status.Value;

                            //Info de Direccion
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle.ToString();
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior.ToString();
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior.ToString();

                            //Info de Colonia 
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.ColoniaNombre.ToString();
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal.ToString();


                            //Info de Municipio
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre.ToString();

                            //Info de Estado
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre.ToString();

                            //Info de Pais
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre.ToString();

                            usuario.NombreCompleto = obj.NombreCompleto.ToString();

                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result GetById(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {usuario.IdUsuario}").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;

                        //info de rol
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;
                        //usuario.Rol.Nombre = query.Rol.ToString();

                        //info de Imagen 
                        usuario.Imagen = query.Imagen.ToString();

                        //Info de Status
                        usuario.Status = query.Status.Value;

                        //Info de Direccion
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle.ToString();
                        usuario.Direccion.NumeroInterior = query.NumeroInterior.ToString();
                        usuario.Direccion.NumeroExterior = query.NumeroExterior.ToString();

                        //Info de Colonia 
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;
                        usuario.Direccion.Colonia.Nombre = query.ColoniaNombre.ToString();
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal.ToString();


                        //Info de Municipio
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = query.MunicipioNombre.ToString();

                        //Info de Estado
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.EstadoNombre.ToString();

                        //Info de Pais
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = query.PaisNombre.ToString();

                        usuario.NombreCompleto = query.NombreCompleto.ToString();

                        result.Object = usuario; //boxing

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();

                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();

                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString(); 
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                //rol
                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = (byte)int.Parse(row[11].ToString());

                                usuario.Imagen = null;

                                usuario.Status = true;

                                //Info de Direccion
                                usuario.Direccion = new ML.Direccion();

                                usuario.Direccion.Calle = row[14].ToString();
                                usuario.Direccion.NumeroInterior = row[15].ToString();
                                usuario.Direccion.NumeroExterior = row[16].ToString();

                                //Info de Colonia 
                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[17].ToString());

                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;
                        }

                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el archivo Excel";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result ValidarExcel(List<object> Object) 
        {
            ML.Result result = new ML.Result(); 

            try
            {
                result.Objects = new List<object>();
                int i = 1; //contador

                foreach(ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++; //Incrementa el Id Resgistro de errores

                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresa el User Name";
                    }
                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresa el Nombre";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresa el Apellido Paterno";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresa el Apellido Materno";
                    }
                    
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingresa el Email ": usuario.Email;
                    
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresa el Password";
                    }
                    if (usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresa la Fecha de Nacimiento";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresa el Sexo";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresa el Telefono";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresa el Celular";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresa la CURP";
                    }
                    if (usuario.Rol.IdRol == null)
                    {
                        error.Mensaje += "Ingresa el ID de Rol";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresa la Calle";
                    }
                    if (usuario.Direccion.NumeroInterior == "")
                    {
                        error.Mensaje += "Ingresa el Numero Interior";
                    }if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresa el Numero Exterior";
                    }
                    if (usuario.Direccion.Colonia.IdColonia == null)
                    {
                        error.Mensaje += "Ingresa el ID de  la Colonia";
                    }

                    if(error.Mensaje != null) //si la lista de errores tiene elementos
                    {
                        result.Objects.Add(error); //agrega los erroes
                    }
                }

                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

    }
}

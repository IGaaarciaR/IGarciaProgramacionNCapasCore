using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");
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
            catch (Exception)
            {
                throw;
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
                    var query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.UserName}', '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}");

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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
            catch (Exception)
            {
                throw;
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
                        usuario.Imagen = query.Imagen;

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
            catch (Exception)
            {
                throw;
            }

            return result;
        }

    }
}
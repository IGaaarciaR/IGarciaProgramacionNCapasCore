using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', '{aseguradora.FechaCreacion}', '{aseguradora.FechaModificacion}', {aseguradora.Usuario.IdUsuario}");
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

        public static ML.Result Update(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraUpdate {aseguradora.IdAseguradora},'{aseguradora.Nombre}', '{aseguradora.FechaModificacion}', { aseguradora.Usuario.IdUsuario}");
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

        public static ML.Result Delete(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {aseguradora.IdAseguradora}");
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var usuarios = context.Aseguradoras.FromSqlRaw("AseguradoraGetAll").ToList();
                    result.Objects = new List<object>();
                    

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.Nombre = obj.NombreUsuario.ToString();
                            aseguradora.Usuario.ApellidoPaterno = obj.ApellidoPUsuario.ToString();
                            aseguradora.Usuario.ApellidoMaterno = obj.ApellidoMUsuario.ToString();

                            //aseguradora.Usuario.Nombre + ' ' + aseguradora.Usuario.ApellidoPaterno + ' ' + aseguradora.Usuario.ApellidoMaterno = obj.NombreCompleto;
                            //aseguradora.Usuario.Nombre + aseguradora.Usuario.ApellidoPaterno = obj.NombreCompleto;

                            //= obj.NombreCompleto;
                            //aseguradora.Usuario.ApellidoPaterno =obj


                            result.Objects.Add(aseguradora);

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

        public static ML.Result GetById(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {aseguradora.IdAseguradora}").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        aseguradora = new ML.Aseguradora();

                        aseguradora.IdAseguradora = query.IdAseguradora;
                        aseguradora.Nombre = query.Nombre;
                        aseguradora.FechaCreacion = query.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = query.FechaModificacion.ToString();

                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = query.IdUsuario.Value;
                        aseguradora.Usuario.Nombre = query.NombreUsuario.ToString();
                        aseguradora.Usuario.ApellidoPaterno = query.ApellidoPUsuario.ToString();
                        aseguradora.Usuario.ApellidoMaterno = query.ApellidoMUsuario.ToString();
                        result.Object = aseguradora; //boxing

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

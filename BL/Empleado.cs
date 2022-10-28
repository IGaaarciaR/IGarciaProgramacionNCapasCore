using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {
        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}','{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}");
                  

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

        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}','{empleado.RFC}', '{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}");


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

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete '{empleado.NumeroEmpleado}'");
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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    //FromSql -- consultas GetAll y GetByID
                    var empleados = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();
                    result.Objects = new List<object>();

                    if (empleados != null)
                    {
                        foreach (var obj in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.RFC = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.ToString();
                            empleado.NSS = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.ToString();
                            empleado.Foto = obj.Foto;


                            //Info de Empresa
                            empleado.Empresa = new ML.Empresa();

                            empleado.Empresa.IdEmpresa = (int)obj.IdEmpresa;
                            empleado.Empresa.Nombre = obj.NombreEmpresa.ToString();


                            //Info de Dependiente
                            //empleado.Dependiente.IdDependiente = int.Parse(obj.DIDdependiente);

                            //empleado.Dependiente.NumeroEmpleado = obj.DNumEmpleado;
                            //empleado.Dependiente.Nombre = obj.DNombre;
                            //empleado.Dependiente.ApellidoPaterno = obj.DApellidoPaterno;
                            //empleado.Dependiente.ApellidoMaterno = obj.DApellidoMaterno;

                            //empleado.Dependiente.FechaNacimiento = obj.DFechaNac.ToString();
                            //empleado.Dependiente.EstadoCivil = obj.DEstadoCivil;
                            //empleado.Dependiente.Genero = obj.DGenero;
                            //empleado.Dependiente.Telefono = obj.DTelefono;
                            //empleado.Dependiente.RFC = obj.DRFC;

                            //empleado.Dependiente.DependienteTipo = new ML.DependienteTipo();
                            //empleado.Dependiente.DependienteTipo.IdDependienteTipo = int.Parse(obj.DIDdependiente);


                            result.Objects.Add(empleado);
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

        public static ML.Result GetById(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Empleados.FromSqlRaw($"EmpleadoGetById '{empleado.NumeroEmpleado}'").AsEnumerable().SingleOrDefault();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        empleado = new ML.Empleado();
                        empleado.NumeroEmpleado = query.NumeroEmpleado;
                        empleado.RFC = query.Rfc;
                        empleado.Nombre = query.Nombre;
                        empleado.ApellidoPaterno = query.ApellidoPaterno;
                        empleado.ApellidoMaterno = query.ApellidoMaterno;
                        empleado.Email = query.Email;
                        empleado.Telefono = query.Telefono;
                        empleado.FechaNacimiento = query.FechaNacimiento.ToString("dd-MM-yyyy");
                        empleado.NSS = query.Nss;
                        empleado.FechaIngreso = query.FechaIngreso.ToString("dd-MM-yyyy");
                        empleado.Foto = query.Foto;


                        //Info de Empresa
                        empleado.Empresa = new ML.Empresa();

                        empleado.Empresa.IdEmpresa = (int)query.IdEmpresa;
                        empleado.Empresa.Nombre = query.NombreEmpresa.ToString();

                        //usuario.NombreCompleto = obj.NombreCompleto.ToString();


                        result.Object = empleado; //boxing

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

        
    }
}

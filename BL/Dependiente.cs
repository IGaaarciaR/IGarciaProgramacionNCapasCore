using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
       // DependienteGetByIdEmpleado

        //public static ML.Result DependienteGetByIdEmpleado(string NumeroEmpleado)
        //{

        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
        //        {
        //            var query = context.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado '{NumeroEmpleado}'").ToList();

        //            if (query != null)
        //            {
        //                result.Objects = new List<object>();

        //                foreach (var item in query)
        //                {
        //                    ML.Empleado empleado = new ML.Empleado();
        //                    //empleado
        //                    empleado.NumeroEmpleado = item.NumeroEmpleado;
        //                    empleado.Nombre = item.NombreCompletoEmpleado;
        //                    empleado.ApellidoPaterno = item.ApellidoPaterno;
        //                    empleado.ApellidoMaterno = item.ApellidoMaterno;
        //                    empleado.RFC = item.Rfc;
        //                    empleado.Email = item.Email;
        //                    empleado.Telefono = item.Telefono;
        //                    empleado.FechaNacimiento = item.FechaNacimiento.ToString("dd-MM-yyyy");
        //                    empleado.NSS = item.Nss;
        //                    empleado.FechaIngreso = item.FechaIngreso.ToString("dd-MM-yyyy");
        //                    empleado.Foto = item.Foto;

        //                    empleado.Empresa = new ML.Empresa();

        //                    empleado.Empresa.IdEmpresa = (int)item.IdEmpresa;
        //                    empleado.Empresa.Nombre = item.NombreEmpresa.ToString();

        //                    //empleado dependiente

        //                    empleado.Dependiente = new ML.Dependiente();

        //                    empleado.Dependiente.IdDependiente = (int)item.DIDdependiente;
        //                    empleado.Dependiente.NumeroEmpleado = item.DNumEmpleado;
        //                    empleado.Dependiente.Nombre = item.DNombre;
        //                    empleado.Dependiente.ApellidoPaterno = item.DApellidoPaterno;
        //                    empleado.Dependiente.ApellidoMaterno = item.DApellidoMaterno;

        //                    empleado.Dependiente.FechaNacimiento = item.DFechaNac.ToString();
        //                    empleado.Dependiente.EstadoCivil = item.DEstadoCivil;
        //                    empleado.Dependiente.Genero = item.DGenero;
        //                    empleado.Dependiente.Telefono = item.DTelefono;
        //                    empleado.Dependiente.RFC = item.DRFC;

        //                    empleado.Dependiente.DependienteTipo = new ML.DependienteTipo();
        //                    empleado.Dependiente.DependienteTipo.IdDependienteTipo = (int)item.DIDdependiente;

        //                    result.Objects.Add(empleado);
        //                }
        //                result.Correct = true;
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;
        //    }
        //    return result;
        //}


        //EmpleadoDependienteGetAll
        public static ML.Result GetAllDependiente()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    //FromSql -- consultas GetAll y GetByID
                    var empleados = context.Empleados.FromSqlRaw($"EmpleadoDependienteGetAll").ToList();
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
    }
}

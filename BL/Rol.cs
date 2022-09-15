using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var roles = context.Rols.FromSqlRaw ("RolGetAll").ToList();
                    result.Objects = new List<object>();

                    if (roles != null)
                    {
                        foreach (var obj in roles)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = obj.IdRol;
                            rol.Nombre = obj.Nombre;


                            result.Objects.Add(rol);
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
    }
}

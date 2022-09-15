using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var paises = context.Pais.FromSqlRaw("PaisGetAll").ToList();
                    //FromSql -- consultas GetAll y GetByID

                    if (paises != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in paises)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = item.IdPais;
                            pais.Nombre = item.Nombre;

                            result.Objects.Add(pais);

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

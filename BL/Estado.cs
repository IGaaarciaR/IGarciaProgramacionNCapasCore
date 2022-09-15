using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.IGarciaProgramacionNCapasContext context = new DL.IGarciaProgramacionNCapasContext())
                {
                    var query = context.Estados.FromSqlRaw($"EstadoGetByIdPais {IdPais}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var item in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = item.IdEstado;
                            estado.Nombre = item.Nombre.ToString();

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = item.IdPais.Value;

                            result.Objects.Add(estado);
                        }
                        result.Correct = true;
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

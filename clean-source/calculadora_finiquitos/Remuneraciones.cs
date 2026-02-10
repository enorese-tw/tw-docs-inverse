using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Remuneraciones
    {
        public string ficha { get; set; }

        private static string strSql;
        private static DataSet ds;

        public List<Remuneracion[]> liquidaciones(string connectionString)
        {
            try
            {
                strSql = string.Format("select ficha, mes from TWEST.softland.sw_variablepersona where ficha = '{0}' group by ficha, mes order by mes desc", ficha);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<Remuneracion[]> rem = new List<Remuneracion[]>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                //Remuneracion[] rem = new Remuneracion[30] ;
                                Remuneracion remu1 = new Remuneracion();
                                int Mes = int.Parse(dr["mes"].ToString());


                                //remu[1]= new VariableRemuneracion[1];
                                return rem;
                            }
                            return null;
                        }
                        return null;
                    }
                    return null;

                }
                return null;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Remuneraciones.Listar", ex.Message);
                return null;
            }

        }
        /*  public <Remuneracion> Listar(string connectionString)
          {
              try
              {
                  strSql = string.Format("select ficha, mes from TWEST.softland.sw_variablepersona where ficha = '{0}' group by ficha, mes order by mes desc", ficha);
                  ds = Interface.ExecuteDataSet(connectionString, strSql);
                  List<Remuneracion> result = new List<Remuneracion>();
                  if (ds != null)
                  {
                      if (ds.Tables.Count > 0)
                      {
                          if (ds.Tables[0].Rows.Count > 0)
                          {
                              foreach (DataRow dr in ds.Tables[0].Rows)
                              {
                                  Remuneracion remu = new Remuneracion();
                                  int Mes = int.Parse(dr["mes"].ToString());
                                   Remuneracion.Listar("juan", ficha, Mes);
                                   result.Add(Remuneracion.Listar("JuN", ficha, Mes));
                              }
                              return result;
                          }
                          else
                          {
                              return null;
                          }
                      }
                      else
                      {
                          return null;
                      }
                  }
                  else
                  {
                      return null;
                  }
              }
              catch (Exception ex)
              {
                  Utilidades.LogError(connectionString, "Remuneraciones.Listar", ex.Message);
                  return null;
              }
          }*/

    }
}
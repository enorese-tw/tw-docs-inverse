using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Causales
    {
        private  static string strSql;
        private string strSqlDelete;
        private static DataSet ds;

        public static List<Causal> cargarCausales(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT * FROM FN_CAUSAL ORDER BY ID ASC; ");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<Causal> result = new List<Causal>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Causal d = new Causal();
                                d.NUMERO = dr["NUMERO"].ToString();
                                d.CAUSALCONCATENADA = dr["NUMERO"].ToString() + "   |   " + dr["DESCRIPCION"].ToString();
                                result.Add(d);
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
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }

        public static List<Causal> cargarCausalDocumento(string connectionString, string causalDocumento)
        {
            try
            {
                strSql = string.Format("SELECT NUMERO, DESCRIPCION FROM FN_CAUSALDOCUMENTO WHERE NUMERO LIKE '%" + causalDocumento + "'");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<Causal> result = new List<Causal>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Causal d = new Causal();
                                d.NUMERO = dr["NUMERO"].ToString();
                                d.DESCRIPCION = dr["DESCRIPCION"].ToString();
                                result.Add(d);
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
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }
    }
}
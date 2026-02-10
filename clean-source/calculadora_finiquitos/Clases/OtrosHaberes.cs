using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class OtrosHaberes
    {
        private static string strSql;
        private static DataSet ds;

        public static List<Otroshaber> Listar(string connectionString)
        {
            try
            {
                List<Otroshaber> result = new List<Otroshaber>();
                strSql = "SELECT * FROM FNOtrosHaberes";
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Otroshaber OtroHaber = new Otroshaber();
                    OtroHaber.Id = int.Parse(dr["Id"].ToString());
                    OtroHaber.Obtener(connectionString);
                    result.Add(OtroHaber);
                }
                return result;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "OtrosHaberes.Listar", ex.Message);
                return null;
            }
        }
        public static List<Otroshaber> Listar(string connectionString, int IDdes)
        {
            try
            {
                List<Otroshaber> result = new List<Otroshaber>();
                strSql = string.Format("SELECT * FROM FNOtrosHaberes", IDdes);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Otroshaber OtroHaber = new Otroshaber();
                    OtroHaber.Id = int.Parse(dr["Id"].ToString());
                    OtroHaber.Obtener(connectionString);
                    result.Add(OtroHaber);
                }
                return result;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "OtrosHaberes.Listar", ex.Message);
                return null;
            }
        }
    }
}
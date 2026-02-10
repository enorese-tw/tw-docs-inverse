using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Descuentos
    {
        private static string strSql;
        private static DataSet ds;

        public static List<Descuento> Listar(string connectionString)
        {
            try
            {
                List<Descuento> result = new List<Descuento>();
                strSql = "SELECT * FROM FNDESCUENTO";
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Descuento descuento = new Descuento();
                    descuento.Id = int.Parse(dr["Id"].ToString());
                    descuento.Obtener(connectionString);
                    result.Add(descuento);
                }
                return result;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Descuento.Listar", ex.Message);
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Otroshaber
    {
        public int Id { set; get; }
        public string Descripcion { set; get; }
        public int Estado { set; get; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public void Obtener(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT * FROM FNOtrosHaberes where Id = {0}", Id);
                ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            Descripcion = dr["Descripcion"].ToString();
                            Estado = int.Parse(dr["Estado"].ToString());
                        }
                        else
                        {
                            Descripcion = string.Format("No se encontró el Otro Haber con Id {0}", Descripcion);
                        }
                    }
                    else
                    {
                        Descripcion = string.Format("No se encontró el Otro Haber con Id {0}", Descripcion);
                    }
                }
                else
                {
                    Descripcion = string.Format("No se encontró el Otro Haber con Id {0}", Descripcion);
                }
            }
            catch (Exception ex)
            {
                Clases.Utilidades.LogError(connectionString, "Otro Haber.Obtener", ex.Message);
                Descripcion = string.Format("Error: {0}", ex.Message);
            }
        }
        public void ObtenerId(string connectionString)
        {
            string strSql = string.Format("Select MAX(Id) as Id From FNOtrosHaberes");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr["Id"] != System.DBNull.Value)
                        {
                            Id = int.Parse(dr["Id"].ToString()) + 1;
                        }
                        else { Id = 1; }

                    }
                }
            }
        }
    }
}
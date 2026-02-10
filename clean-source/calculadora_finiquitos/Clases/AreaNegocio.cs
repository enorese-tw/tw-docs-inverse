using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class AreaNegocio
    {
        public string ficha { get; set; }
        public string codanrg { get; set; }
        public string areanegocio { get; set; }
        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public int Obtener(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT     softland.sw_areanegper.ficha, softland.sw_areanegper.codArn, softland.cwtaren.DesArn" +
                    " FROM softland.cwtaren INNER JOIN softland.sw_areanegper ON softland.cwtaren.CodArn = softland.sw_areanegper.codArn" +
                    " WHERE (softland.sw_areanegper.ficha = '{0}') ORDER BY softland.sw_areanegper.vigHasta DESC", ficha);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            codanrg = dr["codArn"].ToString();
                            areanegocio = dr["DesArn"].ToString();
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
                return -1;
            }
        }
    }
}
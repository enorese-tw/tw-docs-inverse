using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace FiniquitosV2.Clases
{
    public class FNDocumento
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int IDDESVINCULACION { get; set; }
        public int Estado { get; set; }
        private string strSql;
        private DataSet ds;

        public void Obtener(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT * FROM FN_Documentos WHERE ID = {0}", ID);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            Descripcion = dr["Descripcion"].ToString();


                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Cliente.Obtener", ex.Message);
                //TW_RazonSocialCliente = string.Format("Error: {0}", ex.Message);
            }
        }
    }
}
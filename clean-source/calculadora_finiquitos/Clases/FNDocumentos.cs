using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class FNDocumentos
    {
        private static string strSql;
        private static DataSet ds;

        public static List<FNDocumento> Listar(string connectionString)
        {
            try
            {
                List<FNDocumento> result = new List<FNDocumento>();
                strSql = "SELECT * FROM FN_Documentos order by ID ASC";
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    FNDocumento Documento = new FNDocumento();
                    Documento.ID = int.Parse(dr["ID"].ToString());
                    Documento.Obtener(connectionString);
                    result.Add(Documento);
                }
                return result;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "EstadosClientes.Listar", ex.Message);
                return null;
            }
        }
    }
}
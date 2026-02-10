using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace FiniquitosV2.Clases
{
    public class ValorUF
    {
        public string UF { get; set; }
    
        private string strSql;
        private string strSqlDelete;
        private DataSet ds;
       

        public void UFULTIMA(string connectionString, string MES, string AÑO)
        {
            try
            {
                strSql = string.Format("SELECT VALORUF FROM HISTORIALUF WHERE MES LIKE '"+MES+"' AND AÑO LIKE '"+AÑO+"';");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        UF = dr["VALORUF"].ToString();
                        
                    }
                   
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
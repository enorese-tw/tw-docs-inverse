using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Usuarios
    {
        private static string strSql;
        private static DataSet ds;
        public string CadenaConexion() {
            return "Data Source=192.168.0.10;Initial Catalog=Finiquitos2018QA;User ID=ADMINTW;Password=Satw.261119";
        }


        public DataTable Listarkam()
        {
         
             try
            {
                SqlConnection con = new SqlConnection(CadenaConexion());
                con.Open();
                string query = "SELECT * FROM FN_USUARIOS WHERE IDTIPO LIKE 2 ";
                DataTable dt = new DataTable();
                 SqlDataAdapter da = new SqlDataAdapter(query,con);
                 da.Fill(dt);
                 con.Close();
                 return dt;
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
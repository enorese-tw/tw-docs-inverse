using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace FiniquitosV2.Clases
{
    public class FNTotalHaber
    {
        public int ID { get; set; }
        public int IDDesvinculacion { get; set; }
        public int FeriadoProporcional { get; set; }
        public int OtrosHaberes { get; set; }
        public int TotalDescuento { get; set; }
        public int TotalHaberes { get; set; }

        private string strSql;
        private DataSet ds;

        public void ObtenerId(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FNTotalHaber");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr["ID"] != System.DBNull.Value)
                        {
                            ID = int.Parse(dr["ID"].ToString()) + 1;
                        }
                        else { ID = 1; }

                    }
                }
            }
        }

        public void Grabar(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand();
            try
            {

                strSql = string.Format("INSERT INTO FNTotalhaber (ID,IDDesvinculacion,FeriadoProporcional,OtrosHaberes,TotalDescuento,TotalHaberes) VALUES ({0},{1},{2},{3},{4},{5})",
                        ID,
                        IDDesvinculacion,
                        FeriadoProporcional,
                        OtrosHaberes,
                        TotalDescuento,
                        TotalHaberes);

                Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strSql);
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "FNTotalHaber.Grabar", ex.Message);
                //Descripcion = string.Format("Error: {0}", ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
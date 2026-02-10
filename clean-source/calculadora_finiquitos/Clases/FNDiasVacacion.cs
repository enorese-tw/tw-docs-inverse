using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class FNDiasVacacion
    {
        public int ID { get; set; }
        public int IDDesvinculacion { get; set; }
        public int TotalDiasFiniquito { get; set; }
        public string DiasHabiles { get; set; }
        public int DiasVacTomadas { get; set; }
        public string Saldodiashabiles { get; set; }
        public string DiasVacacionesFactor { get; set; }
        public string FechaDesde { get; set; }
        public int DiasInhabiles { get; set; }
        public string TotalDiasFeriado { get; set; }
        public string zonaExt { get; set; }
        public string factorCalculo { get; set; }

        private string strSql;
        private DataSet ds;

        public void ObtenerId(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FNDiasVacaciones");
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
                var fechadesde = FechaDesde.Split(' ')[0].Replace('/', '-');
                var fechaAContar = fechadesde.Split('-');

                strSql = string.Format("INSERT INTO FNDiasVacaciones (ID,IDDesvinculacion,TotalDiasFiniquito,DiasHabiles,DiasVacTomadas,Saldodiashabiles,DiasVacacionesFactor,FechaDesde,DiasInhabiles,TotalDiasFeriado, IndiceFactor) VALUES ({0},{1},{2},'{3}',{4},'{5}','{6}',CAST('{7}' AS DATETIME),{8},'{9}', '{10}')",
                    ID,
                    IDDesvinculacion,
                    TotalDiasFiniquito,
                    DiasHabiles,
                    DiasVacTomadas,
                    Saldodiashabiles,
                    DiasVacacionesFactor,
                    fechaAContar[2] + "-" + fechaAContar[1] + "-" + fechaAContar[0],
                    DiasInhabiles,
                    TotalDiasFeriado,
                    factorCalculo);
                Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strSql);
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "FNDiasVacaciones.Grabar", ex.Message);
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
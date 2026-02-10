using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Vacaciones
    {
        public string ficha { get; set; }
        public string fechaDesvinculacion { get; set; }

        private string strSql;
        private string strSqlDelete;
        public int DiasVacaciones { get; set; }
        private DataSet ds;

        public int ObtenerVacaciones(string connectionString)
        {
            try
            {

                //string[] fechaHoy = (DateTime.Now.ToString().Split(' ')[0]).Split('/');

                strSql = string.Format("SELECT SUM(NDiasAp) as DiasVacaciones FROM [softland].[sw_vacsolic] where estado <> 'N' AND Ficha = '{0}' and CAST(FaHasta AS DATETIME) <= CAST('{1}' AS DATETIME)", ficha, fechaDesvinculacion);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr["DiasVacaciones"] != System.DBNull.Value)
                            {
                                DiasVacaciones = int.Parse(dr["DiasVacaciones"].ToString());
                                return DiasVacaciones;
                            }
                            else
                            { return 0; }
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
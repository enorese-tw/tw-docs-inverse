using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


namespace FiniquitosV2.Clases
{
    public class Licencia
    {

        public string ficha { get; set; }
        public string Fechaconsulta { get; set; }
        public bool tienelicencia { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;
        
        public bool ObtenerLicenia(string connectionString)
        {
            try
            {
                int mes = 0, anno = 0;
                //strSql = string.Format("SELECT *  FROM softland.sw_lmlicencia where Ficha = '{0}' and ((DATEPART(MONTH, FechaIniRep) = {1} and DATEPART(YEAR,FechaIniRep) = {2}) or (DATEPART(MONTH, FechaTermino) = {3} and DATEPART(YEAR,FechaTermino) = {4}))", ficha, mes, anno, mes, anno);
                strSql = string.Format("SELECT * FROM softland.sw_lmlicencia with (nolock) where Ficha = '{0}' AND DATEFROMPARTS('{1}', '{2}', '{3}') between format(fechainirep, 'yyyy-MM-01')  and EOMONTH(fechatermino) ", ficha, Fechaconsulta.Split('-')[0], Fechaconsulta.Split('-')[1], Fechaconsulta.Split('-')[2]);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            return true;
                        }
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
                return false;
            }
        }
    }
}
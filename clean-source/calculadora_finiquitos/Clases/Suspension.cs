using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class Suspension
    {
        public string ficha { get; set; }
        public bool tienelicencia { get; set; }
        public string fecha { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public bool ObtenerSuspension(string connectionString, string empresa)
        {
            try
            {
                int mes = 0, anno = 0;
                //strSql = string.Format("SELECT *  FROM softland.sw_lmlicencia where Ficha = '{0}' and ((DATEPART(MONTH, FechaIniRep) = {1} and DATEPART(YEAR,FechaIniRep) = {2}) or (DATEPART(MONTH, FechaTermino) = {3} and DATEPART(YEAR,FechaTermino) = {4}))", ficha, mes, anno, mes, anno);
                switch (empresa)
                {
                    case "TEAMRRHH":
                        strSql = string.Format(@"SELECT valor FROM sOFTLAND.sW_VARIABLEPER SWV WITH (NOLOCK) INNER JOIN SOFTLAND.SW_VSNPRETORNAFECHAMESEXISTENTES SWVFE WITH (NOLOCK) ON swv.mes = SWVFE.indicemes WHERE CODVARIABLE = 'P206' and ficha = '{0}' and cast(fechames as date) = '{1}'", ficha, fecha);
                        break;
                    case "TEAMWORK":
                        strSql = string.Format(@"SELECT valor FROM sOFTLAND.sW_VARIABLEPER SWV WITH (NOLOCK) INNER JOIN SOFTLAND.SW_VSNPRETORNAFECHAMESEXISTENTES SWVFE WITH (NOLOCK) ON swv.mes = SWVFE.indicemes WHERE CODVARIABLE = 'P206' and ficha = '{0}' and cast(fechames as date) = '{1}'", ficha, fecha);
                        break;
                }
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (Convert.ToInt32(dr["valor"]) != 0)
                            {
                                return true;
                            }
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
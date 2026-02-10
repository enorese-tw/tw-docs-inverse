using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Gratificacion
    {
        public string ficha { get; set; }
        public string variable { get; set; }
        public string valorvariable { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public void Obtener(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT     TOP (1) PERCENT softland.sw_variablepersona.ficha, softland.sw_variablepersona.codVariable, softland.sw_variable.descripcion, " +
                      " softland.sw_variablepersona.valor, softland.sw_variablepersona.mes FROM softland.sw_variablepersona INNER JOIN" +
                      " softland.sw_variable ON softland.sw_variablepersona.codVariable = softland.sw_variable.codVariable" +
                      " WHERE     (softland.sw_variablepersona.ficha = '{0}') AND (softland.sw_variablepersona.codVariable = '{1}')" +
                      " ORDER BY softland.sw_variablepersona.mes DESC", ficha, variable);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            valorvariable = dr["valor"].ToString();

                        }
                        else
                        {
                            variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
                        }
                    }
                    else
                    {
                        variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
                    }
                }
                else
                {
                    variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TW_Gratificacion.Obtener", ex.Message);
                variable = string.Format("Error: {0}", ex.Message);
            }
        }
    }
}
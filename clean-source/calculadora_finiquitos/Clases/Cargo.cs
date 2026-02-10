using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Cargo
    {

        public string ficha { get; set; }
        public string codigoMod { get; set; }
        public string glosaMOd { get; set; }
        public string codigoCargo { get; set; }
        public string glosaCargo { get; set; }
        public string ID { get; set; }
        public string codCargomod { get; set; }
        private string strSql;
        private string strSqlDelete;
      
        private DataSet ds;    
        public void Obtener(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT softland.sw_codineper.ficha, softland.sw_codineper.codigo, softland.sw_codine.glosa, softland.sw_cargoper.carCod, softland.cwtcarg.CarNom " +
                                        " FROM softland.sw_codineper INNER JOIN softland.sw_codine ON softland.sw_codineper.codigo = softland.sw_codine.CodIne INNER JOIN softland.sw_cargoper ON softland.sw_codineper.ficha = softland.sw_cargoper.ficha INNER JOIN " +
                                        " softland.cwtcarg ON softland.sw_cargoper.carCod = softland.cwtcarg.CarCod WHERE     (softland.sw_codineper.ficha = '{0}')", ficha);


                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            codigoMod = dr["codigo"].ToString();
                            glosaMOd = dr["glosa"].ToString();
                            codigoCargo = dr["carCod"].ToString();
                            glosaCargo = dr["CarNom"].ToString();

                        }
                        else
                        {
                            glosaMOd = string.Format("No se encontró el Cargo Mod con Id {0}", codigoMod);
                        }
                    }
                    else
                    {
                        glosaMOd = string.Format("No se encontró el Cargo Mod con Id {0}", codigoMod);
                    }
                }
                else
                {
                    glosaMOd = string.Format("No se encontró el Cargo Mod con Id {0}", codigoMod);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TW_Usuario.Obtener", ex.Message);
                glosaMOd = string.Format("Error: {0}", ex.Message);
            }
        }
    }
}
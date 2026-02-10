using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class VariableRemuneracion
    {
        public string ficha { get; set; }
        public string descripcion { get; set; }
        public string valor { get; set; }
        public DateTime fechames { get; set; }
        public string codVariable { get; set; }
        public int mes { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;

        public void Obtener(string connectionString)
        {
            try
            {

                strSql = string.Format("SELECT     TOP (100) PERCENT softland.sw_variablepersona.ficha, softland.sw_variable.descripcion, CASE WHEN CHARINDEX('.', CAST(softland.sw_variablepersona.valor AS VARCHAR(MAX))) = 0 THEN softland.sw_variablepersona.valor ELSE SUBSTRING(CAST(softland.sw_variablepersona.valor AS VARCHAR(MAX)), 1, CHARINDEX('.', CAST(softland.sw_variablepersona.valor AS VARCHAR(MAX))) - 1) END 'valor'," +
                                       " softland.sw_vsnpRetornaFechaMesExistentes.FechaMes, softland.sw_variablepersona.codVariable, softland.sw_variablepersona.mes" +
                                       " FROM softland.sw_variable INNER JOIN softland.sw_variablepersona ON softland.sw_variable.codVariable = softland.sw_variablepersona.codVariable INNER JOIN" +
                                       " softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                                       " WHERE     (softland.sw_variablepersona.ficha = '{0}') AND (softland.sw_variablepersona.codVariable = '{1}') AND (softland.sw_variablepersona.mes = {2})" +
                                       " ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC", ficha, codVariable, mes);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            descripcion = dr["descripcion"].ToString();
                            //string valorbruto = dr["valor"].ToString();
                            //string coma = valorbruto.Replace(".", ",");
                            //double valorlimpio1 = double.Parse(coma.ToString());
                            valor = dr["valor"].ToString();
                            fechames = DateTime.Parse(dr["fechames"].ToString());
                            codVariable = dr["codVariable"].ToString();
                            mes = int.Parse(dr["mes"].ToString());
                        }
                        else
                        {
                            //descripcion = string.Format(" ", descripcion);
                        }
                    }
                    else
                    {
                        //descripcion = string.Format("", descripcion);
                    }
                }
                else
                {
                    //valor = string.Format("No se encontró el Finiquito con Id {0}", descripcion);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                descripcion = string.Format("Error: {0}", ex.Message);
            }
        }
        public DateTime FechaRemuneracion(string connectionString, string ficha, int mes)
        {
            string strSql = string.Format("SELECT softland.sw_variablepersona.mes, softland.sw_variablepersona.ficha, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                " FROM softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                " WHERE     (softland.sw_variablepersona.ficha = '{0}') and (mes = {1}) GROUP BY softland.sw_variablepersona.ficha, softland.sw_variablepersona.mes, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC", ficha, mes);
            ds = Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        return DateTime.Parse(dr["FechaMes"].ToString());
                    }
                }
            }
            return DateTime.Parse("01-01-0001");
        }

    }
}
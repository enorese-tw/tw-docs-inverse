using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class COUTSoftland
    {
        public DataTable GetRutTrabajadorSolicitudBajaOUT(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_OUT");
            DataTable dt = new DataTable();
            string query = "SELECT DISTINCT p.rut, p.nombres as nombres FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetCargoOUT(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_OUT");
            DataTable dt = new DataTable();
            string query = "SELECT softland.sw_codineper.ficha, softland.sw_codineper.codigo, softland.sw_codine.glosa, softland.sw_cargoper.carCod, softland.cwtcarg.CarNom " +
                           " FROM softland.sw_codineper INNER JOIN softland.sw_codine ON softland.sw_codineper.codigo = softland.sw_codine.CodIne INNER JOIN softland.sw_cargoper ON softland.sw_codineper.ficha = softland.sw_cargoper.ficha INNER JOIN " +
                           " softland.cwtcarg ON softland.sw_cargoper.carCod = softland.cwtcarg.CarCod WHERE (softland.sw_codineper.ficha = '{0}')";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetCentroCostosOUT(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_OUT");
            DataTable dt = new DataTable();
            string query = "SELECT p.ficha as ficha, a.codarn as codarn, c.desarn as desarn, cs.DescCC as DescCC FROM softland.sw_personal p inner join softland.sw_areanegper a on p.ficha=a.ficha inner join softland.cwtaren c on a.codArn = c.codArn inner join softland.sw_ccostoper ct on ct.ficha = p.ficha inner join softland.cwtccos cs on cs.CodiCC=ct.CodiCC where p.ficha = '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetRetencionJudicialOUT(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_OUT");
            DataTable dt = new DataTable();
            string query = "SELECT TOP(1) 'Finqiuito contiene retención judicial' 'RETENCION', valor FROM softland.sw_variablepersona where codVariable like 'D046' AND FICHA LIKE '{0}' ORDER BY MES DESC";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetJornadasParttimeOUT(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_OUT");
            DataTable dt = new DataTable();
            string query = "SELECT TOP(4) CASE WHEN codVariable = 'H001' THEN 'SUELDO BASE' " +
                           "WHEN codVariable = 'P034' THEN 'PARTTIME' ELSE 'OTRA VARIABLE' END 'CODVARIABLE', " +
                           "valor 'VALOR' " +
                           "FROM softland.sw_variablepersona " +
                           "WHERE ficha LIKE '{0}' AND codVariable IN ('P034', 'H001') ORDER BY mes DESC";

            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }
    }
}
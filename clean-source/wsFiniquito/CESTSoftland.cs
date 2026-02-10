using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class CESTSoftland
    {
        public DataTable GetCentroCostos(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT p.ficha as ficha, a.codarn as codarn, c.desarn as desarn, cs.DescCC as DescCC FROM softland.sw_personal p inner join softland.sw_areanegper a on p.ficha=a.ficha inner join softland.cwtaren c on a.codArn = c.codArn inner join softland.sw_ccostoper ct on ct.ficha = p.ficha inner join softland.cwtccos cs on cs.CodiCC=ct.CodiCC where p.ficha = '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetContratoActivoBaja(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT p.ficha as ficha, p.rut, p.nombres as nombres, p.fechaIngreso as fechaIngreso,e.descripcion as codEstudios, p.FecTermContrato as FecTermContrato FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '{0}' ORDER BY fectermcontrato DESC;";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetListarFiniquitados(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT p.ficha, p.rut FROM softland.sw_Personal p inner join softland.sw_estudiosup e on e.codEstudios like p.codEstudios where e.descripcion LIKE '%BAJA%' and rut LIKE '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetListarContratosTerminados(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT ficha, rut, nombres, fechaIngreso, FecTermContrato, DATEDIFF(day, fechaIngreso, FecTermContrato) as Dias FROM softland.sw_personal WHERE (ficha = '{0}') AND (FecTermContrato < '{1}');";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetCausalFicha(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT TWEST.softland.sw_variablepersona.ficha, TWEST.softland.sw_variablepersona.codVariable, TWEST.softland.sw_variablepersona.mes, TWEST.softland.sw_variablepersona.valor, TWEST.softland.sw_variablepersona.flag, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes, softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                           " FROM TWEST.softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON TWEST.softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes WHERE (TWEST.softland.sw_variablepersona.ficha = '{0}') AND (TWEST.softland.sw_variablepersona.codVariable = 'P062') AND (softland.sw_vsnpRetornaFechaMesExistentes.FechaMes = '{1}')";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetCargo(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT softland.sw_codineper.ficha, softland.sw_codineper.codigo, softland.sw_codine.glosa, softland.sw_cargoper.carCod, softland.cwtcarg.CarNom " +
                           " FROM softland.sw_codineper INNER JOIN softland.sw_codine ON softland.sw_codineper.codigo = softland.sw_codine.CodIne INNER JOIN softland.sw_cargoper ON softland.sw_codineper.ficha = softland.sw_cargoper.ficha INNER JOIN " +
                           " softland.cwtcarg ON softland.sw_cargoper.carCod = softland.cwtcarg.CarCod WHERE (softland.sw_codineper.ficha = '{0}')";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetAreaNegocio(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT softland.sw_areanegper.ficha, softland.sw_areanegper.codArn, softland.cwtaren.DesArn" +
                           " FROM softland.cwtaren INNER JOIN softland.sw_areanegper ON softland.cwtaren.CodArn = softland.sw_areanegper.codArn" +
                           " WHERE (softland.sw_areanegper.ficha = '{0}') ORDER BY softland.sw_areanegper.vigHasta DESC";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetUltimaLiquidacion(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT TOP (5) softland.sw_variablepersona.mes, softland.sw_variablepersona.ficha, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                     " FROM softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                    " WHERE (softland.sw_variablepersona.ficha = '{0}') GROUP BY softland.sw_variablepersona.ficha, softland.sw_variablepersona.mes, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                    " ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetRutTrabajadorSolicitudBaja(string[] parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT DISTINCT p.rut, p.nombres as nombres FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetJornadasParttimeEST(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT TOP(4) CASE WHEN codVariable = 'P119' THEN 'DIAS JORNADA' " +
                           "WHEN codVariable = 'H001' THEN 'SUELDO BASE' " +
				           "WHEN codVariable = 'P030' THEN 'PARTTIME' " +
				           "WHEN codVariable = 'P120' THEN 'HORAS JORNADA' ELSE 'OTRA VARIABLE' END 'CODIGOVARIABLE', " +
				           "valor 'VALOR' " +
                           "FROM softland.sw_variablepersona SWV " +
                           "INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes VRFME " +
                           "ON VRFME.IndiceMes = SWV.mes " +
                           "WHERE SWV.ficha LIKE '{0}' AND SWV.codVariable IN ('P030', 'P119', 'P120', 'H001') ORDER BY VRFME.FechaMes DESC, SWV.codvariable ASC";

            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetTrabajadorPartTimeEST(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT codVariable, valor 'VALOR' FROM softland.sw_variablepersona " +
                                   "WHERE ficha LIKE '{0}' AND codVariable IN ('P030')";
            dt = helper.ExecuteQuery(query, parametros);
            
            return dt;
        }

        public DataTable GetCreditoCCAFEST(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_EST");
            DataTable dt = new DataTable();
            string query = "SELECT 'Contiene credito CCFA' 'CREDITO', valor FROM softland.sw_variablepersona where codVariable like 'D023' and ficha like '{0}' ORDER BY MES DESC";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace FiniquitosV2.Clases
{
    public class Contrato
    {
        public string ficha { get; set; }
        public string rut { get; set; }
        public string nombres { get; set; }
        public DateTime fechaIngreso { get; set; }
        public DateTime FecTermContrato { get; set; }
        public int Dias { get; set; }
        public double MesesParaVacaciones { get; set; }
        public double TotalDiaHabiles { get; set; }
        public int totalDiasVacaciones { get; set; }
        public string Causal { get; set; }
        public bool estatus { get; set; }
        public string estudios { get; set; }
        public string hoy {get; set;}
        public string centrocosto { get; set; }
        public string Seguimiento { get; set; }
        public string FechaSeguimiento { get; set; }
        public string PartTime { get; set; }
        public string Existe { get; set; }

        public string areaNeg { get; set; }

        private string strSql;
        private string strSqlDelete;
        private DataSet ds;
        
        private static string strSql1;
        private static DataSet ds1;

        public void ContratoActivo(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT p.ficha as ficha, p.rut, p.nombres as nombres, p.fechaIngreso as fechaIngreso,e.descripcion as codEstudios , p.FecTermContrato as FecTermContrato  FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '" + rut + "' and p.FecTermContrato >= '" + hoy + "';");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            nombres = dr["nombres"].ToString();
                            fechaIngreso = DateTime.Parse(string.Format("{0:d}", dr["fechaIngreso"].ToString()));
                            estudios = dr["codEstudios"].ToString();
                            //fechaIngreso = DateTime.Parse(dr["fechaIngreso"].ToString());
                            if ((dr["FecTermContrato"] != null) && (dr["FecTermContrato"].ToString() != ""))
                            {
                                FecTermContrato = DateTime.Parse(string.Format("{0:d}", dr["FecTermContrato"].ToString()));
                            }
                            else
                            {
                                FecTermContrato = DateTime.Parse("01-01-9999");

                            }
                     
                    }
                    else
                    {
                        nombres = string.Format("No existe contrato Activo", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No existe contrato Activo", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }
        }

        public void centrodecosto(string connectionString, string ficha)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT p.ficha as ficha, a.codarn as codarn, c.desarn as desarn, cs.DescCC as DescCC FROM softland.sw_personal p inner join softland.sw_areanegper a on p.ficha=a.ficha inner join softland.cwtaren c on a.codArn=c.codArn inner join softland.sw_ccostoper ct on ct.ficha=p.ficha inner join  softland.cwtccos cs on cs.CodiCC=ct.CodiCC where p.ficha = '" + ficha + "' ");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        centrocosto = dr["DescCC"].ToString();

                    }
                    else
                    {
                        nombres = string.Format("No existe contrato Activo", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No existe contrato Activo", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }
        }

        public int ConsultarRutExistenteRecepcion(string connectionString)
        {
            int retorno = 0;
            strSql = string.Format("SELECT COUNT(*) 'EXISTE' FROM softland.sw_personal WHERE rut LIKE '" + rut + "';");
            ds = Interface.ExecuteDataSet(connectionString, strSql);
            if(ds != null){
                if(ds.Tables.Count > 0){
                    DataRow row = ds.Tables[0].Rows[0];
                    if(Convert.ToInt32(row["EXISTE"]) > 0){
                        retorno = 1;
                    }
                }
            }

            return retorno;
        }

        public void RutTrabajadorSolicitudBaja(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT DISTINCT p.rut, p.nombres as nombres FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '" + rut + "' ORDER BY fectermcontrato DESC;");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        ficha = dr["ficha"].ToString();
                        nombres = dr["nombres"].ToString();
                    }
                    else
                    {
                        nombres = string.Format("No existe contrato Activo", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No existe contrato Activo", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }
        }

        public void validarPersonaExistente(string connectionString)
        {
            try
            {
                strSql = "SELECT CASE WHEN COUNT(*) > 0 THEN 'S' ELSE 'N' END 'EXISTE' FROM softland.sw_personal WHERE RUT LIKE '"+ rut  + "'";
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        Existe = dr["EXISTE"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ContratoActivobaja(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                if (ficha != null)
                {
                    strSql = string.Format("SELECT p.ficha as ficha, p.rut, p.nombres as nombres, p.fechaContratoV as fechaIngreso, e.descripcion as codEstudios , p.FecTermContrato as FecTermContrato  FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like'" + rut + "' AND p.ficha like '" + ficha + "' ORDER BY fectermcontrato DESC;");
                }
                else 
                {
                    strSql = string.Format("SELECT p.ficha as ficha, p.rut, p.nombres as nombres, p.fechaContratoV as fechaIngreso, e.descripcion as codEstudios , p.FecTermContrato as FecTermContrato  FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like'" + rut + "' ORDER BY fectermcontrato DESC;");
                }
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                       
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            nombres = dr["nombres"].ToString();
                            fechaIngreso = DateTime.Parse(string.Format("{0:d}", dr["fechaIngreso"].ToString()));
                            estudios = dr["codEstudios"].ToString();
                            //fechaIngreso = DateTime.Parse(dr["fechaIngreso"].ToString());
                            if ((dr["FecTermContrato"] != null) && (dr["FecTermContrato"].ToString() != ""))
                            {
                                FecTermContrato = DateTime.Parse(string.Format("{0:d}", dr["FecTermContrato"].ToString()));
                            }
                            else
                            {
                                FecTermContrato = DateTime.Parse("01-01-9999");

                            }
                     
                    }
                    else
                    {
                        nombres = string.Format("No existe contrato Activo", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No existe contrato Activo", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }
        }

        public void ContratosTerminados(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT ficha, rut, nombres, fechaContratoV, FecTermContrato, DATEDIFF(day,fechaIngreso,FecTermContrato ) as Dias FROM softland.sw_personal WHERE (ficha = '{0}') AND (FecTermContrato <= DATEADD(DAY, -1, DATEADD(MONTH, 1, CAST(FORMAT(CAST(GETDATE() AS DATE), 'yyyy-MM-01') AS DATE))))", ficha, FECHAFINIQUITO);
                //strSql = string.Format("SELECT ficha, rut, nombres, fechaContratoV, FecTermContrato, DATEDIFF(day,fechaIngreso,FecTermContrato ) as Dias FROM softland.sw_personal WHERE (ficha = '{0}') AND (FecTermContrato < CAST(GETDATE() AS DATE))", ficha, FECHAFINIQUITO);
                //strSql = string.Format("SELECT ficha, rut, nombres, fechaIngreso, FecTermContrato, DATEDIFF(day,fechaIngreso,FecTermContrato ) as Dias FROM softland.sw_personal WHERE (ficha = '{0}') AND (FecTermContrato < '{1}')", ficha, FECHAFINIQUITO);
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTWEST, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            nombres = dr["nombres"].ToString();
                            fechaIngreso = DateTime.Parse(string.Format("{0:d}", dr["fechaContratoV"].ToString()));
                            FecTermContrato = DateTime.Parse(string.Format("{0:d}", dr["FecTermContrato"].ToString()));
                            estatus = true;
                            Dias = int.Parse(dr["Dias"].ToString()) + 1;
                            MesesParaVacaciones = double.Parse(Dias.ToString()) / 30;
                            TotalDiaHabiles = MesesParaVacaciones * 1.25;
                            // totalDiasVacaciones = Utilidades.diashabiles(FecTermContrato, TotalDiaHabiles);
                            Causal = CausalFicha(connectionString, ficha, FecTermContrato);
                            Seguimiento = ContratoSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "1");
                            FechaSeguimiento = ContratoFechaSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "1");

                            PartTime = ContratoPartTime(Properties.Settings.Default.connectionStringTWEST, dr["ficha"].ToString(), 1);
                        }
                        else
                        {
                            nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                        }
                    }
                    else
                    {
                        nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }

        }

        public void ContratosTerminadosOUT(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT ficha, rut, nombres, fechaContratoV, FecTermContrato, DATEDIFF(day,fechaIngreso,FecTermContrato ) as Dias FROM softland.sw_personal WHERE (ficha = '{0}' )", ficha, FECHAFINIQUITO);
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTEAMRRHH, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            nombres = dr["nombres"].ToString();
                            fechaIngreso = DateTime.Parse(string.Format("{0:d}", dr["fechaContratoV"].ToString()));
                            string TERMINO = dr["FecTermContrato"].ToString();
                            if (TERMINO != "")
                            {
                                FecTermContrato = DateTime.Parse(string.Format("{0:d}", dr["FecTermContrato"].ToString()));
                                Dias = int.Parse(dr["Dias"].ToString()) + 1;
                                MesesParaVacaciones = double.Parse(Dias.ToString()) / 30;
                                TotalDiaHabiles = MesesParaVacaciones * 1.25;
                                estatus = true;

                                // totalDiasVacaciones = Utilidades.diashabiles(FecTermContrato, TotalDiaHabiles);
                                Causal = CausalFicha(connectionString, ficha, FecTermContrato);
                                Seguimiento = ContratoSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "2");
                                FechaSeguimiento = ContratoFechaSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "2");

                                PartTime = ContratoPartTime(Properties.Settings.Default.connectionStringTWEST, dr["ficha"].ToString(), 2);
                            }
                            else
                            {
                                FecTermContrato = DateTime.Parse("0001/01/01 00:00:00");
                            }

                        }
                        else
                        {
                            nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                        }
                    }
                    else
                    {
                        nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }

        }

        public void ContratosTerminadosCONSULTORA(string connectionString)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql = string.Format("SELECT ficha, rut, nombres, fechaContratoV, FecTermContrato, DATEDIFF(day,fechaIngreso,FecTermContrato ) as Dias FROM softland.sw_personal WHERE (ficha = '{0}' )", ficha, FECHAFINIQUITO);
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTEAMWORK, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            ficha = dr["ficha"].ToString();
                            nombres = dr["nombres"].ToString();
                            fechaIngreso = DateTime.Parse(string.Format("{0:d}", dr["fechaContratoV"].ToString()));
                            string TERMINO = dr["FecTermContrato"].ToString();
                            if (TERMINO != "")
                            {
                                FecTermContrato = DateTime.Parse(string.Format("{0:d}", dr["FecTermContrato"].ToString()));

                                Dias = int.Parse(dr["Dias"].ToString()) + 1;
                                MesesParaVacaciones = double.Parse(Dias.ToString()) / 30;
                                TotalDiaHabiles = MesesParaVacaciones * 1.25;
                                estatus = true;

                                // totalDiasVacaciones = Utilidades.diashabiles(FecTermContrato, TotalDiaHabiles);
                                Causal = CausalFicha(connectionString, ficha, FecTermContrato);
                                Seguimiento = ContratoSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "3");
                                FechaSeguimiento = ContratoFechaSeguimiento(Properties.Settings.Default.connectionString, dr["ficha"].ToString(), "3");
                            }
                            else
                            {
                                FecTermContrato = DateTime.Parse("0001/01/01 00:00:00");
                            }

                        }
                        else
                        {
                            nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                        }
                    }
                    else
                    {
                        nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                    }
                }
                else
                {
                    nombres = string.Format("No se encontró el Finiquito con Id {0}", nombres);
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                nombres = string.Format("Error: {0}", ex.Message);
            }

        }

        private string ContratoSeguimiento(string connectionString, string ficha, string empresa) 
        { 
            strSql = string.Format("SELECT CASE WHEN FNC.Estado <> '' THEN [TW_OPERACIONES].[dbo].[FNConvierteEstado](FNC.Estado) ELSE '' END 'Seguimiento' FROM [TW_OPERACIONES].[dbo].[FN_Contratos] FNC WITH (NOLOCK) INNER JOIN [TW_OPERACIONES].[dbo].[FN_Finiquito] FNF WITH (NOLOCK) ON FNF.IdFiniquito = FNC.IdFiniquito WHERE FNC.Ficha = '{0}' AND FNC.Estado <> 'ANU' AND FNF.Empresa = CASE WHEN {1} = 1 THEN 'TWEST' WHEN {1} = 2 THEN 'TWRRHH' ELSE 'TWC' END ", ficha, empresa);
            ds = Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        Seguimiento = dr["Seguimiento"].ToString();
                    }
                }
            }
            return Seguimiento;
        }

        private string ContratoFechaSeguimiento(string connectionString, string ficha, string empresa)
        {
            strSql = string.Format("SELECT CASE WHEN FNC.FechaIngreso IS NOT NULL THEN FORMAT(FNC.FechaIngreso, 'dd-MM-yyyy HH:mm') + ' Horas.' ELSE '' END 'FechaSeguimiento' FROM [TW_OPERACIONES].[dbo].[FN_Contratos] FNC WITH (NOLOCK) INNER JOIN [TW_OPERACIONES].[dbo].[FN_Finiquito] FNF WITH (NOLOCK) ON FNF.IdFiniquito = FNC.IdFiniquito WHERE FNC.Ficha = '{0}' AND FNC.Estado <> 'ANU' AND FNF.Empresa = CASE WHEN {1} = 1 THEN 'TWEST' WHEN {1} = 2 THEN 'TWRRHH' ELSE 'TWC' END ", ficha, empresa);
            ds = Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        FechaSeguimiento = dr["FechaSeguimiento"].ToString();
                    }
                }
            }
            return FechaSeguimiento;
        }

        private string ContratoPartTime(string connectionString, string ficha, int empresa)
        {
            switch(empresa){
                case 1:
                    strSql = string.Format("SELECT CASE WHEN VALOR = '1' THEN 'PT' ELSE 'FULL' END 'partTime' FROM softland.sw_variablepersona WHERE ficha LIKE '{0}' AND codVariable IN ('P030')", ficha);
                    break;
                case 2:
                    strSql = string.Format("SELECT CASE WHEN VALOR = '1' THEN 'PT' ELSE 'FULL' END 'partTime' FROM softland.sw_variablepersona WHERE ficha LIKE '{0}' AND codVariable IN ('P034')", ficha);
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
                        PartTime = dr["partTime"].ToString();
                    }
                    else 
                    {
                        PartTime = "FULL";
                    }
                }
            }
            return PartTime;
        }

        private string CausalFicha(string connectionString, string ficha, DateTime FECHACONSULTA)
        {
            DateTime mes = FECHACONSULTA.AddMonths(-1);
            DateTime fechaconsulta = DateTime.Parse("01-" + FECHACONSULTA.Month + "-" + FECHACONSULTA.Year);
            //strSql = string.Format("SELECT TWEST.softland.sw_variablepersona.ficha, TWEST.softland.sw_variablepersona.codVariable, TWEST.softland.sw_variablepersona.mes, TWEST.softland.sw_variablepersona.valor, TWEST.softland.sw_variablepersona.flag, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes, softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
            //" FROM TWEST.softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON TWEST.softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes WHERE (TWEST.softland.sw_variablepersona.ficha = '{0}') AND (TWEST.softland.sw_variablepersona.codVariable = 'P062') AND (softland.sw_vsnpRetornaFechaMesExistentes.FechaMes = '{1}')", ficha, fechaconsulta);
            //strSql = string.Format("select SWV.VALOR From [Twest].[Softland].[Sw_variableper] SWV WITH (NOLOCK) INNER JOIN [twEST].[sOFTLAND].[sW_VSNPRETORNAFECHAMESEXISTENTES] SWVFE WITH (NOLOCK) ON SWVFE.INDICEMES = SWV.MES where SWV.codvariable = 'P062' and SWV.ficha = '{0}' AND SWV.VALOR != '' AND SWVFE.FECHAMES = (SELECT MAX(SWVFE2.FECHAMES) FROM [Twest].[Softland].[Sw_variableper] SWV2 WITH (NOLOCK) INNER JOIN [twEST].[sOFTLAND].[sW_VSNPRETORNAFECHAMESEXISTENTES] SWVFE2 WITH (NOLOCK) ON SWVFE2.INDICEMES = SWV2.MES where SWV2.codvariable = 'P062' and SWV2.ficha = '{0}' AND SWV2.VALOR != '')", ficha);
            strSql = string.Format("SELECT VALOR FROM [Twest].[Softland].[sw_variablepersona_nuevo] WHERE  ficha = '{0}' and  codvariable = 'P062' group by ficha, VALOR", ficha);
            ds = Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        Causal = dr["valor"].ToString();
                    }
                }
            }
            return Causal;
        }

        public static List<Contrato> ListarFiniquitados(string connectionString, string rut)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                //strSql1 = string.Format("SELECT ficha, rut FROM softland.sw_Personal where rut LIKE '{0}' and  FecTermContrato < '{1}' order by fechaIngreso Desc", rut, FECHAFINIQUITO);
                strSql1 = string.Format("SELECT p.ficha, p.rut FROM softland.sw_Personal p inner join softland.sw_estudiosup e on e.codEstudios like p.codEstudios where e.descripcion LIKE '%BAJA%' and rut LIKE '{0}' ORDER BY fechaingreso DESC", rut);
                //strSql1 = string.Format("SELECT p.ficha, p.rut FROM softland.sw_Personal p inner join softland.sw_estudiosup e on e.codEstudios like p.codEstudios where rut LIKE '{0}' ORDER BY fechaingreso DESC", rut);
                ds1 = Interface.ExecuteDataSet(connectionString, strSql1);
                List<Contrato> result = new List<Contrato>();
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                                anegocio.ficha = dr["ficha"].ToString();
                                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070","TWEST"));
                                Contrato contrato = new Contrato();
                                contrato.areaNeg = anegocio.codanrg;
                                contrato.ficha = dr["ficha"].ToString();
                                contrato.rut = dr["rut"].ToString();
                                contrato.ContratosTerminados(connectionString);
                                result.Add(contrato);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajado.Listar", ex.Message);
                return null;
            }
        }

        /**
         *  METODO DE BUSQUEDA PARA SOLICITUD DE BAJA
         */

        public static List<Contrato> SolicitudDeBaja(string connectionString, string rut, string empresa)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                //strSql1 = string.Format("SELECT ficha, rut FROM softland.sw_Personal where rut LIKE '{0}' and  FecTermContrato < '{1}' order by fechaIngreso Desc", rut, FECHAFINIQUITO);
                strSql1 = string.Format("SELECT p.ficha, p.rut FROM softland.sw_Personal p inner join softland.sw_estudiosup e on e.codEstudios like p.codEstudios where e.descripcion NOT LIKE '%BAJA%' and rut LIKE '{0}' ORDER BY fechaingreso DESC", rut);
                ds1 = Interface.ExecuteDataSet(connectionString, strSql1);
                List<Contrato> result = new List<Contrato>();
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                                anegocio.ficha = dr["ficha"].ToString();
                                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa));
                                Contrato contrato = new Contrato();
                                contrato.areaNeg = anegocio.codanrg;
                                contrato.ficha = dr["ficha"].ToString();
                                contrato.rut = dr["rut"].ToString();
                                contrato.ContratosTerminados(connectionString);
                                result.Add(contrato);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajado.Listar", ex.Message);
                return null;
            }
        }

        public static List<Contrato> ListarFiniquitadosOUT(string connectionString, string rut)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql1 = string.Format("SELECT ficha, rut FROM softland.sw_Personal where rut LIKE '{0}' order by fechaIngreso Desc", rut);
                ds1 = Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTEAMRRHH, strSql1);
                List<Contrato> result = new List<Contrato>();
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                                anegocio.ficha = dr["ficha"].ToString();
                                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", "TEAMRRHH"));
                                Contrato contrato = new Contrato();
                                contrato.areaNeg = anegocio.codanrg;
                                contrato.ficha = dr["ficha"].ToString();
                                contrato.rut = dr["rut"].ToString();
                                contrato.ContratosTerminadosOUT(connectionString);
                                result.Add(contrato);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajado.Listar", ex.Message);
                return null;
            }
        }

        public static List<Contrato> ListarFiniquitadosCONSULTORA(string connectionString, string rut)
        {
            try
            {
                DateTime FECHAFINIQUITO;
                FECHAFINIQUITO = DateTime.Today;
                strSql1 = string.Format("SELECT ficha, rut FROM softland.sw_Personal where rut LIKE '{0}' order by fechaIngreso Desc", rut);
                ds1 = Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTEAMWORK, strSql1);
                List<Contrato> result = new List<Contrato>();
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                                anegocio.ficha = dr["ficha"].ToString();
                                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", "TEAMWORK"));
                                Contrato contrato = new Contrato();
                                contrato.areaNeg = anegocio.codanrg;
                                contrato.ficha = dr["ficha"].ToString();
                                contrato.rut = dr["rut"].ToString();
                                contrato.ContratosTerminadosCONSULTORA(connectionString);
                                result.Add(contrato);
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajado.Listar", ex.Message);
                return null;
            }
        }

        public string YearOtroEmpleador(string connectionString, string ficha)
        {
            try
            {
                string AnoOtroEm = string.Empty;
                strSql1 = string.Format("SELECT ISNULL(AnoOtraEm, 0) 'AnoOtraEm' FROM softland.sw_personal WHERE ficha = '{0}'", ficha);
                ds1 = Interface.ExecuteDataSet(connectionString, strSql1);
                List<Contrato> result = new List<Contrato>();
                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                AnoOtroEm = dr["AnoOtraEm"].ToString();
                            }

                            return AnoOtroEm;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajado.Listar", ex.Message);
                return null;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class CFiniquitos
    {

        /** METODOS PL/SQL INERNOS FINIQUITOS EST */

        #region "METODOS EST"

        #region "GET"

        /** METODOS GET FINIQUITOS EST */

        public DataTable GetCargaVariable()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "select * from FNVARIABLEREMUNERACION";
            dt = helper.ExecuteQuery(query);

            return dt;
        }

        public DataTable GetCausales()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            //string query = "SELECT ID, NUMERO, DESCRIPCION FROM FN_CAUSAL ORDER BY ID ASC;";
            string query = "SELECT ID, NUMERO, DESCRIPCION, CAST(NUMERO AS VARCHAR(20)) + ' | ' + CAST(DESCRIPCION AS VARCHAR(100)) AS UNIFICADO FROM FN_CAUSAL ORDER BY ID ASC;";
            dt = helper.ExecuteQuery(query);

            return dt;
        }

        public DataTable GetListarDocumentos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "SELECT * FROM FN_Documentos order by ID ASC;";
            dt = helper.ExecuteQuery(query);

            return dt;
        }

        public DataTable GetUltimaUF(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "";

            if (parametros.Length > 2)
            {
                query = "SELECT VALORUF FROM [dbo].[HISTORIALUF] WITH (NOLOCK) WHERE CAST(CAST(AÑO AS VARCHAR(MAX)) + '-' + CAST(MES AS VARCHAR(MAX))  + '-01' AS DATE) = (SELECT MAX(CAST(CAST(AÑO AS VARCHAR(MAX)) + '-' + CAST(MES AS VARCHAR(MAX))  + '-01' AS DATE)) FROM [dbo].[HISTORIALUF] WITH (NOLOCK))";

                dt = helper.ExecuteQuery(query);
                return dt;
            }

            query = "SELECT VALORUF FROM [dbo].[HISTORIALUF] WITH (NOLOCK) WHERE CAST(CAST(AÑO AS VARCHAR(MAX)) + '-' + CAST(MES AS VARCHAR(MAX))  + '-01' AS DATE) = DATEADD(MONTH, -1, CAST('{1}-{0}-01' AS DATE))";

            /** MES, AÑO */
            dt = helper.ExecuteQuery(query, parametros);
            return dt;
        }

        public DataTable GetOtrosHaberes()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "SELECT * FROM FNOtrosHaberes;";
            dt = helper.ExecuteQuery(query);

            return dt;
        }

        public DataTable GetListarDescuentos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "SELECT * FROM FNDESCUENTO";
            dt = helper.ExecuteQuery(query);

            return dt;
        }

        public DataTable GetObtenerENCFiniquito(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            string query = "SELECT F.ID AS ID, F.FECHAHORASOLICITUD AS FECHAHORASOLICITUD,F.FECHAAVISODESVINCULACION AS FECHAAVISODESVINCULACION, F.USUARIOSOLICITUD AS USUARIOSOLICITUD, F.RUTTRABAJADOR AS RUTTRABAJADOR, F.FICHATRABAJADOR AS FICHATRABAJADOR, F.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABJADOR, E.DESCRIPCION AS ESTADOSOLICITUD, F.FECHAESTADO AS FECHAESTADO, F.IDCAUSAL  AS IDCAUSAL, M.NOMBRE AS IDEMPRESA  FROM FN_ENCDESVINCULACION F INNER JOIN FN_ESTADOS E ON E.ID LIKE F.ESTADOSOLICITUD INNER JOIN FN_EMPRESAS M ON M.ID LIKE F.IDEMPRESA WHERE FICHATRABAJADOR Like '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetObtenerExcepcionAFC(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();

            string query = "SELECT CASE WHEN COUNT(1) > 0 THEN 1 ELSE 0 END 'HasException' FROM [dbo].[ExcepcionAfc] WITH (NOLOCK) WHERE Cliente = '{0}' AND Division = '{1}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }

        public DataTable GetValidaContratosEnCalculo(List<Parametro> parametros) 
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_VALIDA_CONTRATOS_EN_CALCULO", parametros);

            return dt;
        }

        public DataTable GetListarContratosCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_CONTRATOS_CALCULO", parametros);

            return dt;
        }

        public DataTable GetUltimoEncDesvinculacion()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_ULTIMO_ENC_DESVINCULACION");

            return dt;
        }

        public DataTable GetFiniquitosConfirmados(List<Parametro> parametros) 
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_FINIQUITOS_CONFIRMADOS", parametros);

            return dt;
        }

        public DataTable GetFiniquitosRecientes(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_FINIQUITOS_RECIENTES", parametros);

            return dt;
        }

        public DataTable GetFiniquitosSolicitudesBaja(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_FINIQUITOS_SOLICITUDES_BAJA", parametros);

            return dt;
        }

        public DataTable GetFiniquitosSolicitudBaja(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_FINIQUITO_SOLICITUD_BAJA", parametros);

            return dt;
        }

        public DataTable GetFactorCalculo125(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_FACTOR_CALCULO", parametros);

            return dt;
        }

        /** VISUALIZACION DE CALCULO FINIQUITO */

        public DataTable GetVisualizarDatosTrabajadorCalculo(List<Parametro> parametros) 
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_DATOSTRABAJADOR", parametros);

            return dt;
        }

        public DataTable GetVisualizarPeriodosCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_PERIODOS", parametros);

            return dt;
        }

        public DataTable GetValidarConfirmadoFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VALIDAR_CONFIRMADO_FINIQUITO", parametros);

            return dt;
        }

        public DataTable GetVisualizarContratosCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_CONTRATOS", parametros);

            return dt;
        }

        public DataTable GetVisualizarDocumentosCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_DOCUMENTOS", parametros);

            return dt;
        }

        public DataTable GetVisualizarTotalDiasCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_TOTALDIAS", parametros);

            return dt;
        }

        public DataTable GetVisualizarDescuentosFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_DESCUENTOS_FINIQUITO", parametros);

            return dt;
        }

        public DataTable GetVisualizarOtrosHaberesFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_OTROSHABERES_FINIQUITO", parametros);

            return dt;
        }

        public DataTable GetVisualizarDiasVacacionesCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_DIASVACACIONES", parametros);

            return dt;
        }

        public DataTable GetVisualizarTotalesFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_TOTALESFINIQUITO", parametros);

            return dt;
        }

        /** ----------------- */
        public DataTable GetVisualizarHabaeresMensualFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_HABERESMENSUAL", parametros);

            return dt;
        }

        public DataTable GetVisualizarConceptosAdicionalesFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_CONCEPTOSADICIONALES", parametros);

            return dt;
        }

        public DataTable GetVisualizarBonosAdicionalesFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_BONOSADICIONALES", parametros);

            return dt;
        }

        public DataTable GetVisualizarValorUfFiniquitoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_CL_VISUALIZAR_VALORUF", parametros);

            return dt;
        }

        public DataTable GetBuscarKamEmpresa(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_BUSCARKAMEMPRESA", parametros);

            return dt;
        }

        public DataTable GetObtenerSolB4J(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_OBTENERSOLB4J", parametros);

            return dt;
        }

        public DataTable GetGratificacion(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_FN_Gratificacion", parametros);

            return dt;
        }

        /** END VISUALIZACION DE CALCULO FINIQUITO */

        /** SOLICITUD DE BAJA */



        /** END SOLICITUD DE BAJA */

        #endregion

        #region "SET"

        /** METODOS SET FINIQUITOS EST */

        public DataTable SetInsertaContratoCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_FN_INSERTAR_CONTRATOS_CALCULO", parametros);

            return dt;
        }

        public DataTable SetInsertaDiasVacaciones(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_FN_INSERTAR_DIAS_VACACIONES", parametros);

            return dt;
        }

        public DataTable SetInsertaTotalHaber(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_FN_INSERTAR_TOTAL_HABERES", parametros);

            return dt;
        }

        /** VISUALIZACION DE CALCULO FINIQUITO */

        public DataTable SetConfirmarRetiroCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_CL_CONFIRMAR_RETIRO", parametros);

            return dt;
        }

        /** END VISUALIZACION DE CALCULO FINIQUITO */


        public DataTable SetCrearModificarEncDesvinculacion(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_INSERTAR_MODIFICAR", parametros);

            return dt;
        }


        #endregion

        #endregion

        #region "METODOS OUT"

        #region "GET"

        

        #endregion

        #region "SET"

        public DataTable SetContratosOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_CONTRATOS_OUT", parametros);

            return dt;
        }

        public DataTable SetConceptosAdicionalesOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_CONCEPTOSADICIONALES_OUT", parametros);

            return dt;
        }

        public DataTable SetDatosVariablesOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_DATOSVARIABLES_OUT", parametros);

            return dt;
        }

        public DataTable SetControlDocumentosOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_CONTROLDOCUMENTOS_OUT", parametros);

            return dt;
        }

        public DataTable SetBonosAdicionalesOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_BONOSADICIONALES_OUT", parametros);

            return dt;
        }

        public DataTable SetHaberesYDescuentosOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_HABERESYDESCUENTOS_OUT", parametros);

            return dt;
        }

        public DataTable SetDiasVacacionesOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_DIASVACACIONES_OUT", parametros);

            return dt;
        }

        public DataTable SetHaberesMensualesOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_HABEREMENSUAL_OUT", parametros);

            return dt;
        }

        public DataTable SetTotalPagosOUT(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_TOTALPAGOS_OUT", parametros);

            return dt;
        }
        
        #endregion

        #endregion

        #region "METODOS CONSULTORA"

        #region "GET"



        #endregion

        #region "SET"



        #endregion

        #endregion

        #region "METODOS GENERICOS"

        #region "GET"


        public DataTable GetCalculoPartimeEspecial(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_CALCULO_PARTIME_ESPECIAL", parametros);

            return dt;
        }

        public DataTable GetReporteriaFiniquitos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_REPORTERIA_FINIQUITOS", parametros);

            return dt;
        }

        public DataTable GetAlertasInformativas(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_ALERTASINFORMATIVAS", parametros);

            return dt;
        }

        public DataTable GetObtenerProgresivos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_OBTENERPROGRESIVO", parametros);

            return dt;
        }

        public DataTable GetObtenerExcepcionPTAreaNeg(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_EXCEPCIONPTAREANEG", parametros);

            return dt;
        }

        public DataTable GetObtenerExcepcionCalculoIAS(List<Parametro> parametros)
        { 
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_EXCEPCIONCALCULOIAS", parametros);

            return dt;
        }

        public DataTable GetObtenerFechaIngreso(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_FECHAINGRESO", parametros);

            return dt;
        }

        public DataTable GetValidaRepeticionCalculo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_VALIDAREPTCALCULO", parametros);

            return dt;
        }

        #endregion

        #region "SET"

        public DataTable SetAnularFolioFiniquito(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_ANULAR_FOLIO", parametros);

            return dt;
        }

        public DataTable SetFiniquitoPagado(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_PG_FINIQUITO_PAGADO", parametros);

            return dt;
        }

        public DataTable SetAgregarFactorContrario(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_AGREGARFACTORCONTRARIO", parametros);

            return dt;
        }

        #endregion

        #endregion

        /** METODOS PL/SQL INERNOS FINIQUITOS EST */

        #region "METODOS OUT"


        #region "GET"

        /** METODOS GET FINIQUITOS OUT */



        #endregion


        #region "SET"

        /** METODOS SET FINIQUITOS OUT */


        #endregion

        #endregion
    }
}
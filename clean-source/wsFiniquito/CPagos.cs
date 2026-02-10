using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class CPagos
    {
        public DataTable GetContratosCalculoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_CONSULTAR_CONTRATOS_CALCULOS", parametros);

            return dt;
        }

        public DataTable GetEmpresasPagos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_EMPRESAS");

            return dt;
        }

        public DataTable GetBancosPagos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_BANCOS");

            return dt;
        }

        public DataTable GetTiposPagoPagos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_TIPO_PAGO");

            return dt;
        }

        public DataTable GetTotalesPagosEST(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_TOTALES_CALCULADOS_EST", parametros);

            return dt;
        }

        public DataTable GetValidaDocumentoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_VALIDA_DOCUMENTO_CHEQUE", parametros);

            return dt;
        }

        public DataTable GetObtenerUltimoCorrelativoDisponiblePagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_OBTENER_PRIMER_CORRELDISP", parametros);

            return dt;
        }

        public DataTable SetInsertarRegistroPagoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_INSERTAR_REGISTRO_PAGO", parametros);

            return dt;
        }

        public DataTable GetValidarRegistroPago(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_VALIDAR_PAGO_REALIZADO", parametros);

            return dt;
        }

        public DataTable GetObtenerPagoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_OBTENER_PAGOS", parametros);

            return dt;
        }

        public DataTable GetDataChequePagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_DATA_CHEQUE", parametros);

            return dt;
        }

        public DataTable SetChequePagado(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_PG_CHEQUE_PAGADO", parametros);

            return dt;
        }

        public DataTable GetObtenerProveedores(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_P_OBTENER_PROVEEDOR", parametros);

            return dt;
        }

        public DataTable SetRegistrarPagoProveedores(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_P_SET_REGISTRAR_PAGO_PROVEEDORES", parametros);

            return dt;
        }

        public DataTable GetObtenerCorrelativoDisponibleProveedores(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_P_OBTENER_CORRELATIVO_DISPONIBLE", parametros);

            return dt;
        }

        public DataTable GetObtenerClientesPagos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_OBTENER_CLIENTES");

            return dt;
        }

        public DataTable GetObtenerTrabajadoresPagos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_OBTENER_TRABAJADORES");

            return dt;
        }

        public DataTable GetObtenerMontoChequePagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PG_OBTENER_MONTO_CHEQUE", parametros);

            return dt;
        }

        public DataTable SetChequeDisponiblePagoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_PG_CHEQUE_DISPONIBLE_PAGO", parametros);

            return dt;
        }

        public DataTable SetAnularPagoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_PG_ANULAR_PAGO", parametros);

            return dt;
        }

        public DataTable SetNotariadoPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_PG_NOTARIADO", parametros);

            return dt;
        }

        public DataTable SetRevertirConfirmacionPagos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_REVERTIR_CONFIRMACION", parametros);

            return dt;
        }

        

        /** PROCESO TEMPORAL CHEQUE PROVEEDORES */

        public DataTable SetTMPCargaChequeProveedorProveedores(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CARGA_CHEQUE_PROVEEDOR", parametros);

            return dt;
        }

        public DataTable GetTMPValidateProcessInitProveedores()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_VALIDATE_PROCESS_INIT");

            return dt;
        }
         
        public DataTable SetTMPInitProcessChequeProveedorProveedores()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_INIT_PROCESS_CHEQUE_PROVEEDOR");

            return dt;
        }

        public DataTable SetTMPCloseProcessChequeProveedor()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CLOSE_PROCESS_CHEQUE_PROVEEDOR");

            return dt;
        }

        public DataTable GetTMPChequesInProcessProveedor()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CHEQUES_IN_PROCESS_PROVEEDOR");

            return dt;
        }

        /** END PROCESO TEMPORAL CHEQUE PROVEEDORES */
        
        /** PROCESO TEMPORAL CHEQUE FINIQUITOS */

        public DataTable SetTMPCargaChequeFiniquitos(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CARGA_CHEQUE_FINIQUITO", parametros);

            return dt;
        }

        public DataTable GetTMPValidateProcessInitFiniquitos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_VALIDATE_PROCESS_INIT_FINIQUITO");

            return dt;
        }

        public DataTable SetTMPInitProcessChequeFiniquitos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_INIT_PROCESS_CHEQUE_FINIQUITO");

            return dt;
        }

        public DataTable SetTMPCloseProcessChequeFiniquitos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CLOSE_PROCESS_CHEQUE_FINIQUITO");

            return dt;
        }

        public DataTable GetTMPChequesInProcessFiniquitos()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_TMP_CHEQUES_IN_PROCESS_FINIQUITO");

            return dt;
        }

        /** END PROCESO TEMPORAL CHEQUE FINIQUITOS */

        /** PROVEEDORES MANTENCION */

        public DataTable GetObtenerProveedoresProveedor()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PV_OBTENER_PROVEEDORES");

            return dt;
        }

        public DataTable GetVisualizarTiposProveedores()
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PV_VISUALIZAR_TIPOSPROVEEDORES");

            return dt;
        }

        public DataTable SetInsertarProveedor(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_PV_INSERTAR_PROVEEDOR", parametros);

            return dt;
        }

        /** END PROVEEDORES MANTENCION */
    }
}
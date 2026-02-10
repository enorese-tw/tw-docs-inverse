using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ServicioFiniquitos
{
    [ServiceContract]
    public interface IServicioFiniquitos
    {

        /** OPERACIONES DE CONTRATO */
        #region "OPERACIONES DE CONTRATO INTERNOS"

        #region "GET"

        /** METODOS GET PROVEEDORES MANTENCION */
        #region "PROVEEDORES MANTENCION"

        [OperationContract()]
        ServicioFiniquito GetObtenerProveedoresProveedor();

        #endregion

        /** METODOS GET SOLICITUD DE BAJA */
        #region "SOLICITUD DE B4J"

        [OperationContract()]
        ServicioFiniquito GetObtenerSolicitudesB4J(string[] Parametros, string[] valores);

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - EST */
        #region "EST"

        [OperationContract()]
        ServicioFiniquito GetCargaVariable();

        [OperationContract()]
        ServicioFiniquito GetCausales();

        [OperationContract()]
        ServicioFiniquito GetListarDocumentos();

        [OperationContract()]
        ServicioFiniquito GetUltimaUF(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetOtrosHaberes();

        [OperationContract()]
        ServicioFiniquito GetListarDescuentos();

        [OperationContract()]
        ServicioFiniquito GetObtenerENCFiniquito(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetContratosCalculoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetEmpresasPagos();

        [OperationContract()]
        ServicioFiniquito GetBancosPagos();

        [OperationContract()]
        ServicioFiniquito GetTipoPagoPagos();

        [OperationContract()]
        ServicioFiniquito GetTotalesPagosEST(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetValidaDocumentoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetValidaContratosEnCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetListarContratosCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetUltimoEncDesvinculacion();

        [OperationContract()]
        ServicioFiniquito GetFiniquitosConfirmados(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetFiniquitosRecientes(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetFiniquitosSolicitudesBaja(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetFiniquitosSolicitudBaja(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetFactorCalculo125(string[] Parametros, string[] valores);

        /** OPERACIONES DE CONTRATO PAGOS DE FINIQUITOS */

        [OperationContract()]
        ServicioFiniquito GetObtenerUltimoCorrelativoDisponiblePagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetValidarRegistroPago(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerPagoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetDataChequePagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerProveedores(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerCorrelativoDisponibleProveedores(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerClientesPagos();

        [OperationContract()]
        ServicioFiniquito GetObtenerTrabajadoresPagos();

        [OperationContract()]
        ServicioFiniquito GetObtenerMontoChequePagos(string[] Parametros, string[] valores);


        /** OPERACIONES DE CONTRATOS TIPO GET PARA CALCULO DE FINIQUITO */
        
        [OperationContract()]
        ServicioFiniquito GetVisualizarDatosTrabajadorCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarPeriodosCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetValidarConfirmadoFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarContratosCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarDocumentosCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarTotalDiasCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarDescuentosFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarOtrosHaberesFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarDiasVacacionesCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarTotalesFiniquitoCalculo(string[] Parametros, string[] valores);


        [OperationContract()]
        ServicioFiniquito GetVisualizarHabaeresMensualFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarConceptosAdicionalesFiniquitoCalculo(string[] Parametros, string[] valores);
        
        [OperationContract()]
        ServicioFiniquito GetVisualizarBonosAdicionalesFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarValorUfFiniquitoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetBuscarKamEmpresa(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerSolB4J(string[] Parametros, string[] valores);

        /** END OPERACIONES DE CONTRATOS TIPO GET PARA CALCULO DE FINIQUITO*/

        /** OPERACIONES DE CONTRATO TIPO GET PARA PROVEEDORES */

        [OperationContract()]
        ServicioFiniquito GetTMPValidateProcessInitProveedores();

        [OperationContract()]
        ServicioFiniquito GetTMPChequesInProcessProveedor();

        [OperationContract()]
        ServicioFiniquito GetTMPValidateProcessInitFiniquitos();

        [OperationContract()]
        ServicioFiniquito GetTMPChequesInProcessFiniquitos();

        /** END OPERACIONES DE CONTRATO TIPO GET PARA PROVEEDORES */

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - OUT */
        #region "OUT"

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - GENERICOS */
        #region "GENERICOS"
        
        [OperationContract()]
        ServicioFiniquito GetToken(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetCalculoPartimeEspecial(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetVisualizarTiposProveedores();

        [OperationContract()]
        ServicioFiniquito GetReporteriaFiniquitos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetPlantillasCorreo(string[] Parametros, string[] valores);
        
        [OperationContract()]
        ServicioFiniquito GetAlertasInformativas(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerProgresivos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerExcepcionPTAreaNeg(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerExcepcionCalculoIAS(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerFechaIngreso(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetValidaRepeticionCalculo(string[] Parametros, string[] valores);
        
        [OperationContract()]
        ServicioFiniquito GetGratificacion(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito GetObtenerExcepcionAFC(string[] Parametros);

        #endregion

        #endregion

        #region "SET"

        /** METODOS SET SOLICITUD DE BAJA */
        #region "SOLICITUD DE B4J"

        [OperationContract()]
        ServicioFiniquito SetCrearSolicitudB4J(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetAdministrarProcesosSolB4J(string[] Parametros, string[] valores);

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - EST */
        #region "EST"

        [OperationContract()]
        ServicioFiniquito SetInsertaContratoCalculo(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetInsertaDiasVacaciones(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetInsertaTotalHaber(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetInsertarRegistroPagoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetChequePagado(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetChequeDisponiblePagoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetAnularPagoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetRegistrarPagoProveedores(string[] Parametros, string[] valores);
        
        [OperationContract()]
        ServicioFiniquito SetTMPCargaChequeProveedorProveedores(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetTMPInitProcessChequeProveedorProveedores();

        [OperationContract()]
        ServicioFiniquito SetTMPCloseProcessChequeProveedor();

        [OperationContract()]
        ServicioFiniquito SetTMPCargaChequeFiniquitos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetTMPInitProcessChequeFiniquitos();

        [OperationContract()]
        ServicioFiniquito SetTMPCloseProcessChequeFiniquitos();

        /** OPERACIONES DE CONTRATOS TIPO SET PARA CALCULO DE FINIQUITO*/

        [OperationContract()]
        ServicioFiniquito SetConfirmarRetiroCalculo(string[] Parametros, string[] valores);

        /** END OPERACIONES DE CONTRATOS TIPO SET PARA CALCULO DE FINIQUITO*/

        /** OPERACIONES DE CONTRATO TIPO SET PARA SOLICITUD DE BAJA */

        [OperationContract()]
        ServicioFiniquito SetCrearModificarEncDesvinculacion(string[] Parametros, string[] valores);

        /** END OPERACIONES DE CONTRATO TIPO SET PARA SOLICITUD DE BAJA */

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - OUT */
        #region "OUT"

        [OperationContract()]
        ServicioFiniquito SetContratosOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetConceptosAdicionalesOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetDatosVariablesOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetControlDocumentosOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetBonosAdicionalesOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetHaberesYDescuentosOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetDiasVacacionesOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetHaberesMensualesOUT(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetTotalPagosOUT(string[] Parametros, string[] valores);

        #endregion
        
        /** METODOS SET PARA BASE DE DATOS INTERNA - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - GENERICOS */
        #region "GENERICOS"
        
        [OperationContract()]
        ServicioFiniquito SetAnularFolioFiniquito(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetFiniquitoPagado(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetNotariadoPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetRevertirConfirmacionPagos(string[] Parametros, string[] valores);

        [OperationContract()]
        ServicioFiniquito SetInsertarProveedor(string[] Parametros, string[] valores);
        
        [OperationContract()]
        ServicioFiniquito SetAgregarFactorContrario(string[] Parametros, string[] valores);

        #endregion

        #endregion

        #endregion

        #region "OPERACIONES DE CONTRATO SOFTLAND"

        #region "GET"

        /** METODOS GET PARA BASE DE DATOS SOFTLAND - EST */
        #region "EST"

        [OperationContract()]
        ServicioFiniquito GetCreditoCCAFEST(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetJornadasParttimeEST(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetTrabajadorPartTimeEST(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetCentroCostos(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetContratoActivoBaja(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetListarFiniquitados(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetListarContratosTerminados(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetCausalFicha(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetCargo(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetAreaNegocio(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetUltimaLiquidacion(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetRutTrabajadorSolicitudBaja(string[] Parametros);

        #endregion

        /** METODOS GET PARA BASE DE DATOS SOFTLAND - OUT */
        #region "OUT"

        [OperationContract()]
        ServicioFiniquito GetJornadasParttimeOUT(string[] Parametros);
        
        [OperationContract()]
        ServicioFiniquito GetRutTrabajadorSolicitudBajaOUT(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetCargoOUT(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetCentroCostosOUT(string[] Parametros);

        [OperationContract()]
        ServicioFiniquito GetRetencionJudicialOUT(string[] Parametros);
        
        #endregion

        /** METODOS GET PARA BASE DE DATOS SOFTLAND - CONSULTORA */
        #region "CONSULTORA"

        [OperationContract()]
        ServicioFiniquito GetRutTrabajadorSolicitudBajaCONSULTORA(string[] Parametros);
        
        #endregion

        #endregion

        #region "SET"

        /** METODOS SET PARA BASE DE DATOS SOFTLAND - EST */
        #region "EST"

        #endregion

        /** METODOS SET PARA BASE DE DATOS SOFTLAND - OUT */
        #region "OUT"


        #endregion

        /** METODOS SET PARA BASE DE DATOS SOFTLAND - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        #endregion

        #endregion

    }

    [DataContract()]
    public class ServicioFiniquito
    {
        public ServicioFiniquito()
        {
            this.Table = new DataSet("Data");
        }

        [DataMember()]
        public DataSet Table
        {
            get { return m_Table; }
            set { m_Table = value; }
        }

        private DataSet m_Table;

    }
}

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
    public class wsServicioFiniquitos : IServicioFiniquitos
    {

        private List<Parametro> ArrayToListParametros(string[] Parametros, string[] valores)
        {
            List<Parametro> Lista = new List<Parametro>();

            for (int i = 0; i < Parametros.Length; i++)
            {
                Lista.Add(new Parametro(Parametros[i], valores[i]));
            }

            return Lista;
        }

        /** METODOS DE SERVICIO */
        #region "METODOS DE SERVICIO INTERNOS"

        #region "GET"

        /** METODOS GET PROVEEDORES MANTENCION */
    
        public ServicioFiniquito GetObtenerProveedoresProveedor()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerProveedoresProveedor());
            IService.Table = ds;

            return IService;
        }

        #region "SOLICTUD DE BAJA"

        public ServicioFiniquito GetObtenerSolicitudesB4J(string[] Parametros, string[] valores)
        {
            CSolB4J solB4J = new CSolB4J();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(solB4J.GetObtenerSolicitudesB4J(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - EST */
        #region "EST"

        public ServicioFiniquito GetCargaVariable()
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetCargaVariable());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCausales()
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetCausales());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetListarDocumentos()
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetListarDocumentos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetUltimaUF(string[] Parametros)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetUltimaUF(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetOtrosHaberes()
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetOtrosHaberes());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetListarDescuentos()
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetListarDescuentos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerENCFiniquito(string[] Parametros)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerENCFiniquito(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerExcepcionAFC(string[] Parametros)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerExcepcionAFC(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetFiniquitosConfirmados(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetFiniquitosConfirmados(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetFiniquitosRecientes(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetFiniquitosRecientes(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetFiniquitosSolicitudesBaja(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetFiniquitosSolicitudesBaja(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetFiniquitosSolicitudBaja(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetFiniquitosSolicitudBaja(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetFactorCalculo125(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetFactorCalculo125(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        /** METODOS DE PAGOS DE FINIQUITOS */

        public ServicioFiniquito GetContratosCalculoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetContratosCalculoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetEmpresasPagos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetEmpresasPagos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetBancosPagos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetBancosPagos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetTipoPagoPagos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetTiposPagoPagos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetTotalesPagosEST(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetTotalesPagosEST(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetValidaDocumentoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetValidaDocumentoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetValidaContratosEnCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetValidaContratosEnCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetListarContratosCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetListarContratosCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetUltimoEncDesvinculacion()
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetUltimoEncDesvinculacion());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerUltimoCorrelativoDisponiblePagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerUltimoCorrelativoDisponiblePagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetValidarRegistroPago(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetValidarRegistroPago(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerPagoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerPagoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetDataChequePagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetDataChequePagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerProveedores(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerProveedores(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerCorrelativoDisponibleProveedores(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerCorrelativoDisponibleProveedores(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }
        
        public ServicioFiniquito GetObtenerClientesPagos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerClientesPagos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerTrabajadoresPagos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerTrabajadoresPagos());
            IService.Table = ds;
            
            return IService;
        }

        public ServicioFiniquito GetObtenerMontoChequePagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetObtenerMontoChequePagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        /** METODOS DE CALCULO DE FINIQUITO */

        public ServicioFiniquito GetVisualizarDatosTrabajadorCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarDatosTrabajadorCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarPeriodosCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarPeriodosCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetValidarConfirmadoFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetValidarConfirmadoFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarContratosCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarContratosCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarDocumentosCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarDocumentosCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarTotalDiasCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarTotalDiasCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarDescuentosFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarDescuentosFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarOtrosHaberesFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarOtrosHaberesFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarDiasVacacionesCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarDiasVacacionesCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarTotalesFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarTotalesFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }


        public ServicioFiniquito GetVisualizarHabaeresMensualFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarHabaeresMensualFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarConceptosAdicionalesFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarConceptosAdicionalesFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarBonosAdicionalesFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarBonosAdicionalesFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarValorUfFiniquitoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetVisualizarValorUfFiniquitoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetBuscarKamEmpresa(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetBuscarKamEmpresa(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerSolB4J(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetObtenerSolB4J(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        /** END METODOS DE CALCULO DE FINIQUITO */

        /** METODOS DE PROVEEDORES */

        public ServicioFiniquito GetTMPValidateProcessInitProveedores()
        {
            CPagos pago = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pago.GetTMPValidateProcessInitProveedores());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetTMPChequesInProcessProveedor()
        {
            CPagos pago = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pago.GetTMPChequesInProcessProveedor());
            IService.Table = ds;

            return IService;
        }

        /** END METODOS DE PROVEEDORES */

        /** METODOS FINIQUITOS CONFIRMADOS */

        public ServicioFiniquito GetTMPValidateProcessInitFiniquitos()
        {
            CPagos pago = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pago.GetTMPValidateProcessInitFiniquitos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetTMPChequesInProcessFiniquitos()
        {
            CPagos pago = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pago.GetTMPChequesInProcessFiniquitos());
            IService.Table = ds;

            return IService;
        }

        /** END METODOS FINIQUITOS CONFIRMADOS */

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - OUT */
        #region "OUT"

        #endregion

        /** METODOS GET PARA BASE DE DATOS INTERNA - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        #region "GENERICOS"
        public ServicioFiniquito GetToken(string[] Parametros, string[] valores)
        {
            CSolB4J finiquitos = new CSolB4J();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetToken(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }
        public ServicioFiniquito GetCalculoPartimeEspecial(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetCalculoPartimeEspecial(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetVisualizarTiposProveedores()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.GetVisualizarTiposProveedores());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetReporteriaFiniquitos(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetReporteriaFiniquitos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetAlertasInformativas(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.GetAlertasInformativas(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetPlantillasCorreo(string[] Parametros, string[] valores)
        {
            CSolB4J solB4J = new CSolB4J();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(solB4J.GetPlantillasCorreo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerProgresivos(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerProgresivos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerExcepcionPTAreaNeg(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerExcepcionPTAreaNeg(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerExcepcionCalculoIAS(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerExcepcionCalculoIAS(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetObtenerFechaIngreso(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetObtenerFechaIngreso(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetValidaRepeticionCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetValidaRepeticionCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetGratificacion(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquitos = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquitos.GetGratificacion(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        #endregion

        #endregion

        #region "SET"

        /** METODOS GET PROVEEDORES MANTENCION */
        #region "SOLICITUD DE BAJA"

        public ServicioFiniquito SetCrearSolicitudB4J(string[] Parametros, string[] valores)
        {
            CSolB4J solB4J = new CSolB4J();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(solB4J.SetCrearSolicitudB4J(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetAdministrarProcesosSolB4J(string[] Parametros, string[] valores)
        {
            CSolB4J solB4J = new CSolB4J();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(solB4J.SetAdministrarProcesosSolB4J(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - EST */
        #region "EST"

        public ServicioFiniquito SetInsertaContratoCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetInsertaContratoCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetInsertaDiasVacaciones(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetInsertaDiasVacaciones(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetInsertaTotalHaber(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetInsertaTotalHaber(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetInsertarRegistroPagoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetInsertarRegistroPagoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetChequePagado(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetChequePagado(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetChequeDisponiblePagoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetChequeDisponiblePagoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetAnularPagoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetAnularPagoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetNotariadoPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetNotariadoPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetRevertirConfirmacionPagos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetRevertirConfirmacionPagos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }








        public ServicioFiniquito SetRegistrarPagoProveedores(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetRegistrarPagoProveedores(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPCargaChequeProveedorProveedores(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPCargaChequeProveedorProveedores(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPInitProcessChequeProveedorProveedores()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPInitProcessChequeProveedorProveedores());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPCloseProcessChequeProveedor()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPCloseProcessChequeProveedor());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPCargaChequeFiniquitos(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPCargaChequeFiniquitos(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPInitProcessChequeFiniquitos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPInitProcessChequeFiniquitos());
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTMPCloseProcessChequeFiniquitos()
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetTMPCloseProcessChequeFiniquitos());
            IService.Table = ds;

            return IService;
        }

        /** METODOS DE CALCULO DE FINIQUITO */

        public ServicioFiniquito SetConfirmarRetiroCalculo(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetConfirmarRetiroCalculo(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        /** END METODOS DE CALCULO DE FINIQUITO */

        /** METODOS SOLICITUD DE BAJA */
        
        public ServicioFiniquito SetCrearModificarEncDesvinculacion(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetCrearModificarEncDesvinculacion(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        /** END METODOS SOLICITUD DE BAJA */

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - OUT */
        #region "OUT"

        public ServicioFiniquito SetContratosOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetContratosOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetConceptosAdicionalesOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetConceptosAdicionalesOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetDatosVariablesOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetDatosVariablesOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetControlDocumentosOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetControlDocumentosOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetBonosAdicionalesOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetBonosAdicionalesOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetHaberesYDescuentosOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetHaberesYDescuentosOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetDiasVacacionesOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetDiasVacacionesOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetHaberesMensualesOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetHaberesMensualesOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetTotalPagosOUT(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetTotalPagosOUT(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        /** METODOS SET PARA BASE DE DATOS INTERNA - GENERICOS */
        #region "GENERICOS"

        public ServicioFiniquito SetAnularFolioFiniquito(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetAnularFolioFiniquito(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetFiniquitoPagado(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetFiniquitoPagado(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetInsertarProveedor(string[] Parametros, string[] valores)
        {
            CPagos pagos = new CPagos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(pagos.SetInsertarProveedor(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito SetAgregarFactorContrario(string[] Parametros, string[] valores)
        {
            CFiniquitos finiquito = new CFiniquitos();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(finiquito.SetAgregarFactorContrario(ArrayToListParametros(Parametros, valores)));
            IService.Table = ds;

            return IService;
        }

        #endregion

        #endregion

        #endregion

        #region "METODOS DE SERVICIO SOFTLAND"

        #region "GET"

        /** METODOS GET PARA SOFTLAND - EST */
        #region "EST"

        public ServicioFiniquito GetCreditoCCAFEST(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetCreditoCCAFEST(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetJornadasParttimeEST(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetJornadasParttimeEST(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetTrabajadorPartTimeEST(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();
            
            ds.Tables.Add(est.GetTrabajadorPartTimeEST(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCentroCostos(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetCentroCostos(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetContratoActivoBaja(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetContratoActivoBaja(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetListarFiniquitados(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetListarFiniquitados(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetListarContratosTerminados(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetListarContratosTerminados(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCausalFicha(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetCausalFicha(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCargo(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetCargo(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetAreaNegocio(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetAreaNegocio(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetUltimaLiquidacion(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetUltimaLiquidacion(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetRutTrabajadorSolicitudBaja(string[] Parametros)
        {
            CESTSoftland est = new CESTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(est.GetRutTrabajadorSolicitudBaja(Parametros));
            IService.Table = ds;

            return IService;
        }

        #endregion

        /** METODOS GET PARA SOFTLAND - OUT */
        #region "OUT"

        public ServicioFiniquito GetJornadasParttimeOUT(string[] Parametros)
        {
            COUTSoftland outt = new COUTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(outt.GetJornadasParttimeOUT(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetRetencionJudicialOUT(string[] Parametros)
        {
            COUTSoftland outt = new COUTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(outt.GetRetencionJudicialOUT(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetRutTrabajadorSolicitudBajaOUT(string[] Parametros)
        {
            COUTSoftland outt = new COUTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(outt.GetRutTrabajadorSolicitudBajaOUT(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCargoOUT(string[] Parametros)
        {
            COUTSoftland outt = new COUTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(outt.GetCargoOUT(Parametros));
            IService.Table = ds;

            return IService;
        }

        public ServicioFiniquito GetCentroCostosOUT(string[] Parametros)
        {
            COUTSoftland outt = new COUTSoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(outt.GetCentroCostosOUT(Parametros));
            IService.Table = ds;

            return IService;
        }

        #endregion

        /** METODOS GET PARA SOFTLAND - CONSULTORA */
        #region "CONSULTORA"

        public ServicioFiniquito GetRutTrabajadorSolicitudBajaCONSULTORA(string[] Parametros)
        {
            CCONSULTORASoftland consultora = new CCONSULTORASoftland();
            ServicioFiniquito IService = new ServicioFiniquito();
            DataSet ds = new DataSet();

            ds.Tables.Add(consultora.GetRutTrabajadorSolicitudBajaCONSULTORA(Parametros));
            IService.Table = ds;

            return IService;
        }

        #endregion

        #endregion

        #region "SET"

        /** METODOS SET PARA SOFTLAND - EST */
        #region "EST"

        #endregion

        /** METODOS SET PARA SOFTLAND - OUT */
        #region "OUT"

        #endregion

        /** METODOS SET PARA SOFTLAND - CONSULTORA */
        #region "CONSULTORA"

        #endregion

        #endregion

        #endregion

    }
}

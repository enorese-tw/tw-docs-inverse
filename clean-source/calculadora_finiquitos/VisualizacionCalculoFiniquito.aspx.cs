using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using FiniquitosV2.Clases;

namespace FiniquitosV2
{
    public partial class VisualizacionCalculoFiniquito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                if (Session["Tipo"].ToString() == "2")
                {
                    Response.Redirect("RenunciasRecibidas");
                    linkInicio.Visible = false;

                    /** RESPONSIVE */
                    linkInicioResponsive.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "3")
                {
                    Response.Redirect("RecepcionDocumentos");
                }
                else if (Session["Tipo"].ToString() == "1")
                {

                }
                else if (Session["Tipo"].ToString() == "4")
                {
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;

                    /** RESPONSIVE */
                    linkBasePagosResponsive.Visible = false;
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;

                    confirmacionFiniquitoRealizado.Visible = false;
                    confirmacionFiniquito.Visible = false;
                }
                else if(Session["Tipo"].ToString() == "6")
                {
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;

                    /** RESPONSIVE */
                    linkBasePagosResponsive.Visible = false;
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;

                    confirmacionFiniquitoRealizado.Visible = true;
                    confirmacionFiniquito.Visible = true;
                }
                else if (Session["Tipo"].ToString() == "7")
                {
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkCalculoBaja.Visible = false;

                    /** RESPONSIVE */
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                    linkCalculoBajaResponsive.Visible = false;
                }
            }
            else
            {
                Response.Redirect("Login");
            }

        }
        
        protected void downloadExcelDocumentoFiniquito(object sender, EventArgs e)
        {

        }

        protected void downloadPDFDocumentoFiniquito(object sender, EventArgs e)
        {

        }
        
        [WebMethod()]
        public static string GetObtenerPagosPorFiltroPagosService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarDatosTrabajadorCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetValidarConfirmadoFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetValidarConfirmadoFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarContratosCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarContratosCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarDocumentosCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarDocumentosCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarPeriodosCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarPeriodosCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarTotalDiasCalculoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarTotalDiasCalculoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarDescuentosFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarDescuentosFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarOtrosHaberesFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarOtrosHaberesFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarDiasVacacionesCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarDiasVacacionesCalculoService, Formatting.Indented);
        }
        
        [WebMethod()]
        public static string GetVisualizarTotalesFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarTotalesFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarHabaeresMensualFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarHabaeresMensualFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarBonosAdicionalesFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarBonosAdicionalesFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarConceptosAdicionalesFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarConceptosAdicionalesFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarValorUfFiniquitoCalculoService(string idDesvinculacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetVisualizarValorUfFiniquitoCalculoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string SetConfirmarRetiroCalculoService(string idDesvinculacion, string medioPago, string monto, string observacion)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            web.MEDIOPAGO = medioPago;
            web.MONTO = monto;
            web.OBSERVACION = observacion;
            return JsonConvert.SerializeObject(web.SetConfirmarRetiroCalculoService, Formatting.Indented);
        }
        
        [WebMethod()]
        public static string GetCentroCostosService(string ficha)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FICHA = ficha;
            return JsonConvert.SerializeObject(web.GetCentroCostosService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetCentroCostosOUTService(string ficha)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FICHA = ficha;
            return JsonConvert.SerializeObject(web.GetCentroCostosOUTService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetCargoService(string ficha)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FICHA = ficha;
            return JsonConvert.SerializeObject(web.GetCargoService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetCargoOUTService(string ficha)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FICHA = ficha;
            return JsonConvert.SerializeObject(web.GetCargoOUTService, Formatting.Indented);
        }
    }
}
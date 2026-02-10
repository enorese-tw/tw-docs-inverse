using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamwork.Infraestructura.Collections.Finiquitos;
using Teamwork.Infraestructura.Collections.Dasboard;
using Teamwork.Extensions.Excel;
using OfficeOpenXml;

namespace AplicacionOperaciones.Controllers
{
    public class FiniquitosController : Controller
    {
        public ActionResult Index()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult GenerateExcel()
        {
            if (AplicationActive())
            {
                byte[] bytes = null;

                switch (Request.QueryString["excel"])
                {
                    case "FiniquitosProcesoMasivo":
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            bytes = XLSX_ReporteFiniquitoProcesoMasivo.__xlsx(
                                Request.QueryString["excel"],
                                Request.QueryString["data"].Replace(" ", "+")
                            );

                            if (bytes != null)
                            {
                                excel.Workbook.Properties.Title = "Attempts";
                                Response.ClearContent();
                                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Confirmacion Proceso Masivo Finiquitos.xlsx"));
                                Response.ContentType = "application/excel";
                                Response.BinaryWrite(bytes);
                                Response.End();
                                Response.Flush();
                            }
                        }
                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }

        #region "Metodos API"

        public string FiniquitosConsultaFiniquitos(string pagination = "", string filter = "", string dataFilter = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConsultaFiniquitos(Session["CodificarAuth"].ToString(), pagination, filter, dataFilter), Formatting.Indented);
        }
         
        public string FiniquitosPaginationFiniquitos(string pagination = "", string filter = "", string dataFilter = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosPaginationFiniquitos(Session["CodificarAuth"].ToString(), pagination, filter, dataFilter), Formatting.Indented);
        }

        public string FiniquitosConsultaComplementos(string pagination = "", string filter = "", string dataFilter = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConsultaComplementos(Session["CodificarAuth"].ToString(), pagination, filter, dataFilter), Formatting.Indented);
        }

        public string FiniquitosHistorialFiniquitos(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosHistorialFiniquitos(codigo), Formatting.Indented);
        }

        public string FiniquitosValidarFiniquito(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosValidarFiniquito(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosAnularFiniquito(string codigo = "", string obseracion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosAnularFiniquito(Session["CodificarAuth"].ToString(), codigo, obseracion), Formatting.Indented);
        }

        public string FiniquitosGestionEnvioRegiones(string codigo = "", string observacion = "", string envio = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionEnvioRegiones(Session["CodificarAuth"].ToString(), codigo, observacion, envio), Formatting.Indented);
        }

        public string FiniquitosGestionEnvioSantiagoNotaria(string codigo = "", string obseracion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionEnvioSantiagoNotaria(Session["CodificarAuth"].ToString(), codigo, obseracion), Formatting.Indented);
        }

        public string FiniquitosGestionEnvioSantiagoParaFirma(string codigo = "", string obseracion = "", string rolCoordinador = "", string coordinador = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionEnvioSantiagoParaFirma(Session["CodificarAuth"].ToString(), codigo, obseracion, rolCoordinador, coordinador), Formatting.Indented);
        }

        public string FiniquitosGestionRecepcionRegiones(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionRecepcionRegiones(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosGestionRecepcionSantiagoNotaria(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionRecepcionSantiagoNotaria(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosGestionRecepcionSantiagoParaFirma(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionRecepcionSantiagoParaFirma(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosConsultaDatosTEF(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConsultaDatosTEF(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosSolicitudTEF(string codigo = "", string observacion = "", string banco = "", string numeroCta = "", string gastoAdm = "0")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosSolicitudTEF(Session["CodificarAuth"].ToString(), codigo, observacion, banco, numeroCta, gastoAdm), Formatting.Indented);
        }

        public string FiniquitosSolicitudValeVista(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosSolicitudValeVista(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosSolicitudCheque(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosSolicitudCheque(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosBancos()
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosBancos(), Formatting.Indented);
        }

        public string FiniquitosGestionEnvioLegalizacion(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionEnvioLegalizacion(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosGestionRecepcionLegalizacion(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosGestionRecepcionLegalizacion(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosConsultaSolicitudPago(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConsultaSolicitudPago(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosConfirmarProcesoPago(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConfirmarProcesoPago(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosPagarFiniquito(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosPagarFiniquito(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }
        
        public string FiniquitosListarLiquidacionesSueldoYear(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosListarLiquidacionesSueldoYear(codigo), Formatting.Indented);
        }

        public string FiniquitosListarLiquidacionesSueldoMes(string codigo = "", string filter = "", string dataFilter = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosListarLiquidacionesSueldoMes(codigo, filter, dataFilter), Formatting.Indented);
        }

        public string FiniquitosComplementoCrear(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoCrear(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosComplementoListarHaberes(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoListarHaberes(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosComplementoListarDescuento(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoListarDescuento(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosComplementoAgregarHaber(string codigo = "", string monto = "", string descripcion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoAgregarHaber(Session["CodificarAuth"].ToString(), codigo, monto, descripcion), Formatting.Indented);
        }

        public string FiniquitosComplementoAgregarDescuento(string codigo = "", string monto = "", string descripcion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoAgregarDescuento(Session["CodificarAuth"].ToString(), codigo, monto, descripcion), Formatting.Indented);
        }

        public string FiniquitosComplementoEliminarHaber(string codigo = "", string variable = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoEliminarHaber(Session["CodificarAuth"].ToString(), codigo, variable), Formatting.Indented);
        }

        public string FiniquitosComplementoEliminarDescuento(string codigo = "", string variable = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoEliminarDescuento(Session["CodificarAuth"].ToString(), codigo, variable), Formatting.Indented);
        }

        public string FiniquitosComplementoDejarActivoCreado(string codigo = "", string fecha = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosComplementoDejarActivoCreado(Session["CodificarAuth"].ToString(), codigo, fecha), Formatting.Indented);
        }

        public string FiniquitosActualizarMontoAdministrativo(string codigo = "", string observacion = "", string gastoAdm = "0")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosActualizarMontoAdministrativo(Session["CodificarAuth"].ToString(), codigo, observacion, gastoAdm), Formatting.Indented);
        }

        public string FiniquitosRevertirValidacion(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirValidacion(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        public string FiniquitosRevertirGestionEnvio(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirGestionEnvio(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }
        
        public string FiniquitosRevertirLegalizacion(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirLegalizacion(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        public string FiniquitosRevertirSolicitudPago(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirSolicitudPago(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        public string FiniquitosRevertirConfirmacion(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirConfirmacion(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        public string FiniquitosRevertirEmisionPago(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirEmisionPago(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        public string FiniquitosConfigOpcionesMovimientoMasivos(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConfigOpcionesMovimientoMasivos(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosActualizarComentariosAnotaciones(string codigo = "", string html = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosActualizarComentariosAnotaciones(codigo, html), Formatting.Indented);
        }

        public string FiniquitosConsultaComentariosAnotaciones(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosConsultaComentariosAnotaciones(codigo), Formatting.Indented);
        }

        public string FiniquitosTerminarFiniquito(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosTerminarFiniquito(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosFirmarFiniquito(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosFirmarFiniquito(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosReprocesarDocumentosFiniquito(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosReprocesarDocumentosFiniquito(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosValidarFinanzas(string codigo = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosValidarFinanzas(Session["CodificarAuth"].ToString(), codigo), Formatting.Indented);
        }

        public string FiniquitosRevertirValidacionFinanzas(string codigo = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionFiniquitos.__FiniquitosRevertirValidacionFinanzas(Session["CodificarAuth"].ToString(), codigo, observacion), Formatting.Indented);
        }

        #endregion

        #region "Permisos y Redireccionamientos ante cierre y expiración de sesión"

        public bool AplicationActive()
        {
            bool active = false;

            if (Session["NombreUsuario"] != null && Session["Cargo"] != null && Session["Area"] != null && Session["ApplicationActive"] != null && Session["Profile"] != null && Session["base64Username"] != null && Session["base64Password"] != null &&
                Session["CodificarAuth"] != null && Session["PreController"] != null && Session["Username"] != null)
            {
                active = true;
            }

            return active;
        }

        public string ModuleControlRetornoPath()
        {
            string urlServer = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                urlServer = "http://181.190.6.196/AplicacionOperaciones";
            }
            else
            {
                urlServer = "http://localhost:46903";
            }

            return urlServer;
        }

        #endregion
    }
}
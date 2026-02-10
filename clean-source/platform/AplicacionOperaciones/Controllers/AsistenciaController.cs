using AplicacionOperaciones.Collections;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Teamwork.Extensions.Excel;
using Teamwork.Model.Asistencia;
using Teamwork.Model.Operaciones;
using Teamwork.WebApi.Auth;

namespace AplicacionOperaciones.Controllers
{
    public class AsistenciaController : Controller
    {
        ServicioAuth.ServicioAuthClient servicioAuth = new ServicioAuth.ServicioAuthClient();
        ServicioOperaciones.ServicioOperacionesClient servicioOperaciones = new ServicioOperaciones.ServicioOperacionesClient();
        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";
        string schema = "SolicitudAsistencia";
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI.";

        public ActionResult Index()
        {
            if (AplicationActive())
            {
                Session["ApplicationReporteExcelAsistencia"] = false;
                Session["ApplicationReporteExcelAsistenciaSuccess"] = false;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Asistencia()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Calendario()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Reporte()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Periodo()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Bonos()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AdministracionBonos()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult SolicitudAsistencia()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {
                    string plantillaMasiva = Request.QueryString["ref"] == null ? "NDU=" : Request.QueryString["ref"].ToString();

                    if (plantillaMasiva != null && plantillaMasiva != "")
                    {
                        string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1].Split('?')[0];

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View(cargaMasivas);
        }

        public ActionResult SolicitudHorasExtras()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {

                    string plantillaMasiva = Request.QueryString["ref"] == null ? "NDc=" : Request.QueryString["ref"].ToString();

                    if (plantillaMasiva != null && plantillaMasiva != "")
                    {
                        string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        public ActionResult SolicitudAsistenciaRetail()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {
                    string plantillaMasiva = Request.QueryString["ref"] == null ? "NDk=" : Request.QueryString["ref"].ToString();

                    if (plantillaMasiva != null && plantillaMasiva != "")
                    {
                        string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1].Split('?')[0];

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        public ActionResult SolicitudAsistenciaRelojControl()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {
                    string plantillaMasiva = Request.QueryString["ref"] == null ? "" : Request.QueryString["ref"].ToString();

                    if (plantillaMasiva != null && plantillaMasiva != "")
                    {
                        string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1].Split('?')[0];

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }
        
        public ActionResult SolicitudBonos()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {
                    string plantillaMasiva = Request.QueryString["ref"] == null ? "NTU=" : Request.QueryString["ref"].ToString();

                    if (plantillaMasiva != null && plantillaMasiva != "")
                    {
                        string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1].Split('?')[0];

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        public ActionResult CargasMasivas()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult ArchivoAsistencia()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult DownloadArchivo()
        {
            if (AplicationActive())
            {
                try
                {
                    string nombre = DecodeBase64(Request.QueryString["ref1"]);
                    string nombreReal = DecodeBase64(Request.QueryString["ref2"]);
                    string ruta = DecodeBase64(Request.QueryString["ref3"]);

                    if (nombre != "" && nombreReal != "" && ruta != "")
                    {
                        if (System.IO.File.Exists(Path.Combine(ruta, nombre)))
                        {
                            MemoryStream ms = new MemoryStream();
                            var fileByte = System.IO.File.ReadAllBytes(Path.Combine(ruta, nombre));

                            ms.Write(fileByte, 0, fileByte.Length);
                            ms.Position = 0;

                            Response.AddHeader("content-disposition", "attachment;filename=" + nombreReal);
                            Response.Buffer = true;
                            Response.Clear();
                            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
                            Response.OutputStream.Flush();
                            Response.End();
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult JornadaLaboral()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult HorasExtras()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }


        public JsonResult RenderizaCargaMasiva(string cargaMasiva, string plantillaMasiva = "")
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            try
            {
                string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(cargaMasiva, plantillaMasiva, objects[0].Token.ToString(), ModuleControlRetornoPath());

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                // captura de cualquier excepcion del sistema
                // errores 500
                // errores de sesion expirada
            }

            return Json(new { CargaMasivas = cargaMasivas, PlantillaMasiva = plantillaMasiva });
        }

        public JsonResult ObtenerAsistencia(string empresa, string ficha, string fecha, string areaNegocio = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Asistencia> asistencia = CollectionsAsistencia.__AsistenciaObtener(empresa, ficha, fecha, objects[0].Token.ToString(), "");

            List<Licencia> licencia = ObtenerLicencias(empresa, ficha, fecha, objects[0].Token.ToString());

            List<Vacacion> vacacion = ObtenerVacacion(empresa, ficha, fecha, objects[0].Token.ToString());

            List<BajasConfirmadas> bajasConfirmadas = ObtenerBajaConfirmada(empresa, ficha, fecha, areaNegocio, objects[0].Token.ToString());

            List<TipoAsistencia> tipoAsistencia = ObtenerTipoAsistencia(objects[0].Token.ToString());

            List<HorasExtras> horasExtras = ObtenerHorasExtras(empresa, ficha, fecha, areaNegocio, objects[0].Token.ToString());

            return Json(new { Asistencia = asistencia, Licencia = licencia, Vacacion = vacacion, TipoAsistencia = tipoAsistencia, HorasExtras = horasExtras, BajasConfirmadas = bajasConfirmadas });
        }

        public JsonResult InsertaAsistencia(string empresa, string ficha, string asistencias)
        {
            string observacion = "";
            List<Asistencia> asistencia = new List<Asistencia>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            asistencia = CollectionsAsistencia.__AsistenciaInsertar(empresa, ficha, asistencias, observacion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Asistencia = asistencia });
        }

        public JsonResult ActualizaAsistencia(string empresa, string ficha, string fecha, string tipo)
        {
            string observacion = "";
            List<Asistencia> asistencia = new List<Asistencia>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            asistencia.Add(CollectionsAsistencia.__AsistenciaActualizar(empresa, ficha, fecha, tipo, observacion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), ""));

            return Json(new { Asistencia = asistencia });
        }

        public JsonResult EliminaAsistencia(string empresa, string ficha, string fecha, string tipo)
        {
            string observacion = "";
            List<Asistencia> asistencia = new List<Asistencia>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            asistencia.Add(CollectionsAsistencia.__AsistenciaEliminar(empresa, ficha, fecha, tipo, observacion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), ""));

            return Json(new { Asistencia = asistencia });
        }

        public JsonResult GetDataPersonal(string empresa, string ficha, string fecha)
        {
            List<Personal> personal = new List<Personal>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            personal.Add(CollectionsAsistencia.__PersonalDataObtener(empresa, ficha, fecha, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), ""));

            return Json(new { Personal = personal });
        }

        public JsonResult GetTipoAsistencia()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            List<TipoAsistencia> tipoAsistencia = CollectionsAsistencia.__TipoAsistenciaObtener(objects[0].Token.ToString(), "");

            return Json(new { TipoAsistencia = tipoAsistencia });
        }

        public JsonResult GetLegend()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            List<Leyenda> leyenda = CollectionsAsistencia.__LeyendaObtener(objects[0].Token.ToString(), "");

            return Json(new { Leyenda = leyenda });
        }

        public JsonResult GetAsistenciaReport(string empresa, string periodo)
        {
            string code = string.Empty;
            string message = string.Empty;

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());


            return Json(new { Code = code, Message = message });
        }

        public JsonResult ObtenerPlantillaCargaMasiva(string plantilla, string request)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<ProcesoCargaMasiva> cargaMasiva = CollectionsCargaMasiva.__PlantillaListar(request, plantilla, objects[0].Token.ToString(), ModuleControlRetornoPath());

            return Json(new { CargaMasiva = cargaMasiva });
        }
        
        private List<Licencia> ObtenerLicencias(string empresa, string ficha, string fecha, string token)
        {
            return CollectionsAsistencia.__LicenciaObtener(empresa, ficha, fecha, token, "");
        }

        private List<Vacacion> ObtenerVacacion(string empresa, string ficha, string fecha, string token)
        {
            return CollectionsAsistencia.__VacacionObtener(empresa, ficha, fecha, token, "");
        }

        private List<BajasConfirmadas> ObtenerBajaConfirmada(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return CollectionsAsistencia.__ObtenerBajaConfirmada(empresa, ficha, fecha, areaNegocio, token, "");
        }

        private List<TipoAsistencia> ObtenerTipoAsistencia(string token)
        {
            return CollectionsAsistencia.__TipoAsistenciaObtener(token, "");
        }

        private List<Leyenda> ObtenerLeyenda(string token)
        {
            return CollectionsAsistencia.__LeyendaObtener(token, "");
        }
        
        #region "Otros metodos"
        

        private string DecodeBase64(string base64Data)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64Data);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        public ActionResult GenerateExcel()
        {
            try
            {
                if ((bool)Session["ApplicationReporteExcelAsistencia"])
                {
                    byte[] bytes = null;
                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    string excel = Request.QueryString["excel"] ?? "";
                    string empresa = Request.QueryString["empresa"] ?? "";
                    string cliente = Request.QueryString["areanegocio"] ?? "";
                    string periodo = Request.QueryString["periodo"] ?? "";

                    switch (excel)
                    {
                        //ASISTENCIA
                        case "YXNpc3RlbmNpYQ==":
                            List<ReporteAsistencia> reporte = CollectionsAsistencia.__AsistenciaReporteV2(empresa, "", periodo, cliente, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

                            List<BajasConfirmadas> bajasConfirmadas = ObtenerBajaConfirmada(empresa, "", periodo, cliente, objects[0].Token.ToString());

                            List<HorasExtras> horasExtras = CollectionsAsistencia.__AsistenciaReporteHorasExtras(empresa, "", periodo, cliente, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

                            List<ReporteAsistencia> columnas = ObtenerReporteHorasColumnas(empresa, "", periodo, cliente, objects[0].Token.ToString());

                            List<Leyenda> leyenda = ObtenerLeyenda(objects[0].Token.ToString());

                            List<JornadaLaboral> jornadaLaboral = ObtenerJornadasLaborales(empresa, "", Session["NombreUsuario"].ToString(), objects[0].Token.ToString());

                            List<FichaBonos> fichaBonos = ObtenerFichaBonos(empresa, cliente, "", periodo, objects[0].Token.ToString());

                            bytes = XLSX_ReporteAsistencia.ReporteAsistencia(reporte, bajasConfirmadas, horasExtras, columnas, leyenda, jornadaLaboral, fichaBonos, periodo);


                            if (bytes != null)
                            {
                                ViewBag.ReferenciaSuccessExcel = true;
                                Session["ApplicationReporteExcelAsistenciaSuccess"] = true;

                                Response.ClearContent();
                                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Asistecia " + Request.QueryString["empresa"] + periodo.Replace("-", "") + ".xlsx"));
                                Response.ContentType = "application/excel";
                                Response.BinaryWrite(bytes);
                                Response.Flush();
                                Response.End();
                            }
                            else
                            {
                                ViewBag.ReferenciaMessageReporte = "<img src='../../../../Resources/cancel.png' style='width: 50px; border-radius: 50%;' class='danger dspl-inline-block' /> Ups! Lo sentimos, no se ha podido obtener el reporte solicitado!";
                                ViewBag.ReferenciaMessageReporteImp = "Puede cerrar esta pagina o recargarla para volver a solicitar el reporte!";
                                ViewBag.ReferenciaSuccessExcel = false;
                                Session["ApplicationReporteExcelAsistenciaSuccess"] = false;
                            }

                            ViewBag.ReferenciaReporteExcel = false;
                            Session["ApplicationReporteExcelAsistencia"] = false;
                            break;
                    }
                }
                else
                {
                    if ((bool)Session["ApplicationReporteExcelAsistenciaSuccess"])
                    {
                        ViewBag.ReferenciaMessageReporte = "<img src='../../../../Resources/check.png' style='width: 50px; border-radius: 50%;' class='success dspl-inline-block' /> Se ha descargado exitosamente el reporte solicitado!";
                        ViewBag.ReferenciaReporteExcel = false;
                        Session["ApplicationReporteExcelAsistencia"] = false;
                        ViewBag.ReferenciaSuccessExcel = false;
                        Session["ApplicationReporteExcelAsistenciaSuccess"] = false;
                    }
                    else
                    {
                        ViewBag.ReferenciaMessageReporte = "Estamos preparando el archivo con el reporte solicitado, espere un momento...";
                        ViewBag.ReferenciaMessageReporteImp = "Importante! al obtener el reporte solicitado podrá cerrar esta pagina, de lo contrario el sistema indicara que no es posible obtener el reporte.";
                        ViewBag.ReferenciaReporteExcel = true;

                        Session["ApplicationReporteExcelAsistencia"] = true;
                        ViewBag.ReferenciaSuccessExcel = false;
                        Session["ApplicationReporteExcelAsistenciaSuccess"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaMessageReporte = "<img src='../../../../Resources/cancel.png' style='width: 50px; border-radius: 50%;' class='danger dspl-inline-block' /> Ups! Lo sentimos, no se ha podido obtener el reporte solicitado!";
                ViewBag.ReferenciaMessageReporteImp = "Puede cerrar esta pagina o recargarla para volver a solicitar el reporte!";
                ViewBag.ReferenciaSuccessExcel = true;
                Session["ApplicationReporteExcelAsistenciaSuccess"] = false;

                ViewBag.ReferenciaReporteExcel = false;
                //Session["ApplicationReporteExcelAsistencia"] = false;
            }

            return View();
        }

        public ActionResult ViewPartialLoaderTransaccion()
        {
            return PartialView("_LoaderTransaccion");
        }

        public ActionResult ViewPartialLoaderErrorGeneric(string message)
        {
            ViewBag.HTMLLoaderError = message;

            return PartialView("_ErrorGeneric");
        }

        #region ASISTENCIA V2
        public JsonResult InsertaAsistenciaV2(string empresa, string ficha, string asistencias)
        {
            string observacion = "";
            List<Asistencia> asistencia = new List<Asistencia>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            asistencia = CollectionsAsistencia.__AsistenciaInsertarV2(empresa, ficha, asistencias, observacion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Asistencia = asistencia });
        }

        public JsonResult GetAreaNegocio(string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<AreaNegocio> areanegocio = CollectionsAsistencia.__AreaNegocioObtener(empresa, Session["NombreUsuario"].ToString(), Session["Profile"].ToString(), objects[0].Token.ToString(), "");


            return Json(new { AreaNegocio = areanegocio });
        }

        public JsonResult ObtenerAsistenciaV2(string empresa, string ficha, string fecha, string areaNegocio, string pagination, string typePagination)
        {
            List<Asistencia> asistencia = new List<Asistencia>();
            List<Licencia> licencia = new List<Licencia>();
            List<Vacacion> vacacion = new List<Vacacion>();
            List<BajasConfirmadas> bajasConfirmadas = new List<BajasConfirmadas>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Personal> personal = ObtenerPersonalDataV2(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, objects[0].Token.ToString());

            if (personal.Count > 0)
            {
                asistencia = CollectionsAsistencia.__AsistenciaObtenerV2(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, objects[0].Token.ToString(), "");

                licencia = ObtenerLicenciasV2(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, objects[0].Token.ToString());

                vacacion = ObtenerVacacionV2(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, objects[0].Token.ToString());

                bajasConfirmadas = ObtenerBajaConfirmada(empresa, ficha, fecha, areaNegocio, objects[0].Token.ToString());
            }

            return Json(new { Personal = personal, Asistencia = asistencia, Licencia = licencia, Vacacion = vacacion, BajasConfirmadas = bajasConfirmadas });
        }
        #endregion

        #region PERIODOS
        public JsonResult ObtenerCierrePeriodo(string empresa, string fecha, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<CierrePeriodo> cierrePeriodo = CollectionsAsistencia.__AsistenciaObtenerCierrePeriodo(empresa, fecha, areaNegocio, objects[0].Token.ToString(), "");

            return Json(new { CierrePeriodo = cierrePeriodo, Profile = Session["Profile"].ToString() });
        }

        public JsonResult InsertarCierrePeriodo(string empresa, string fecha, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<CierrePeriodo> cierrePeriodo = CollectionsAsistencia.__AsistenciaInsertarCierrePeriodo(empresa, fecha, areaNegocio, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { CierrePeriodo = cierrePeriodo });
        }

        public JsonResult ObtenerCierrePeriodos(string fechaDesde, string fechaHasta, string empresa, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<CierrePeriodo> cierrePeriodo = CollectionsAsistencia.__AsistenciaObtenerPeriodos(fechaDesde, fechaHasta, empresa, areaNegocio, objects[0].Token.ToString(), "");

            return Json(new { CierrePeriodo = cierrePeriodo, Profile = "Administrador"/*Session["Profile"].ToString()*/ });
        }

        public JsonResult InsertarExcepcionPeriodo(string empresa, string fecha, string areaNegocio, string excepcion)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<CierrePeriodo> cierrePeriodo = CollectionsAsistencia.__AsistenciaInsertarExcepcionPeriodo(empresa, fecha, areaNegocio, excepcion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { CierrePeriodo = cierrePeriodo });
        }
        #endregion

        #region ARCHIVO ASISTENCIA
        public JsonResult ObtenerArchivoAsistencia(string empresa, string fecha, string areaNegocio, string ficha)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<ArchivoAsistencia> archivoAsistencia = CollectionsAsistencia.__AsistenciaObtenerArchivosAsistencia(empresa, ficha, areaNegocio, fecha, objects[0].Token.ToString(), "");

            return Json(new { ArchivoAsistencia = archivoAsistencia });
        }

        public JsonResult ObtenerNombreArchivo()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            List<ArchivoAsistencia> archivoAsistencia = CollectionsAsistencia.__AsistenciaObtenerNombreArchivo(objects[0].Token.ToString(), "");

            return Json(new { ArchivoAsistencia = archivoAsistencia });
        }

        public JsonResult ObtenerRutaFichero()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            List<RutaFicheros> rutaFicheros = CollectionsAsistencia.__AsistenciaObtenerRutaFichero(objects[0].Token.ToString(), "");

            return Json(new { RutaFicheros = rutaFicheros });
        }

        public JsonResult ObtenerArchivosAsistencia(string empresa, string ficha, string areaNegocio, string fecha)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<ArchivoAsistencia> archivoAsistencia = CollectionsAsistencia.__AsistenciaObtenerArchivosAsistencia(empresa, ficha, areaNegocio, fecha, objects[0].Token.ToString(), "");

            return Json(new { ArchivoAsistencia = archivoAsistencia });
        }

        public JsonResult InsertaArchivoAsistencia(HttpPostedFileBase fileData, string rutaFichero, string empresa, string areaNegocio, string fecha, string ficha, string codigoRuta, string nombreArchivo)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();
            Personal personal = new Personal();
            string error = "";

            try
            {
                dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                byte[] data = null;
                string extension = Path.GetExtension(fileData.FileName);
                string nombreRealArchivo = Path.GetFileNameWithoutExtension(fileData.FileName);

                if (ficha != "")
                {
                    personal = ObtenerPersonalData(empresa, ficha, fecha, Session["NombreUsuario"].ToString(), objects[0].Token.ToString());

                    if (personal.Code != "200")
                        error = personal.Message;
                }

                if (error == "")
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileData.InputStream))
                    {
                        data = binaryReader.ReadBytes(fileData.ContentLength);
                    }

                    System.IO.File.WriteAllBytes(Path.Combine(rutaFichero, nombreArchivo + extension), data);

                    if (System.IO.File.Exists(Path.Combine(rutaFichero, nombreArchivo + extension)))
                    {
                        archivoAsistencia = InsertarArchivoAsistencia(empresa, ficha, areaNegocio, fecha, codigoRuta, nombreArchivo, nombreRealArchivo, extension, objects[0].Token.ToString());

                        if (archivoAsistencia.Count > 0)
                        {
                            if (archivoAsistencia[0].Code != "200")
                                System.IO.File.Delete(Path.Combine(rutaFichero, nombreArchivo + extension));
                        }
                        else
                        {
                            System.IO.File.Delete(Path.Combine(rutaFichero, nombreArchivo + extension));
                        }
                    }
                    else
                    {
                        error = "Hubo un inconveniente en la carga del archivo " + nombreRealArchivo + extension + "!!";
                    }
                }
            }
            catch (Exception ex)
            {
                error = errorSistema;
            }

            return Json(new { ArchivoAsistencia = archivoAsistencia, Error = error });
        }

        public JsonResult EliminaArchivoAsistencia(string rutaFichero, string codigoRuta, string nombreArchivo, string nombreReal, string codigoArchivo)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();
            string error = "";

            try
            {
                dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                if (System.IO.File.Exists(Path.Combine(DecodeBase64(rutaFichero), DecodeBase64(nombreArchivo))))
                {
                    archivoAsistencia = CollectionsAsistencia.__AsistenciaEliminarArchivoAsistencia(DecodeBase64(codigoRuta), DecodeBase64(codigoArchivo), DecodeBase64(nombreReal), objects[0].Token.ToString(), "");

                    if (archivoAsistencia.Count > 0)
                    {
                        error = archivoAsistencia[0].Message;

                        if (archivoAsistencia[0].Code == "200")
                        {
                            System.IO.File.Delete(Path.Combine(DecodeBase64(rutaFichero), DecodeBase64(nombreArchivo)));
                        }
                    }
                    else
                    {
                        error = "Hubo un inconveniente en la eliminacion del archivo " + DecodeBase64(nombreReal) + "!!!";
                    }
                }
                else
                {
                    error = "Hubo un inconveniente en la eliminacion del archivo " + DecodeBase64(nombreReal) + "!!";
                }
            }
            catch (Exception ex)
            {

            }

            return Json(new { ArchivoAsistencia = archivoAsistencia, Error = error });
        }

        private List<ArchivoAsistencia> InsertarArchivoAsistencia(string empresa, string ficha, string areaNegocio, string fecha, string codigoRuta, string nombreArchivo, string nombreRealArchivo, string extension, string token)
        {
            return CollectionsAsistencia.__AsistenciaInsertarArchivosAsistencia(empresa, ficha, areaNegocio, fecha, Session["NombreUsuario"].ToString(), codigoRuta, nombreArchivo, nombreRealArchivo.Replace("'", ""), extension, token, "");
        }
        #endregion

        #region JORNADA LABORAL
        public JsonResult ObtenerJornadaLaboral(string empresa, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<JornadaLaboral> jornadaLaboral = CollectionsAsistencia.__AsistenciaObtenerJornadaLaboral(empresa, areaNegocio, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { JornadaLaboral = jornadaLaboral });
        }

        public JsonResult ActualizarJornadaLaboral(string codigoJornada, string vigente, string nombreJornada, string descripcionJornada, string horasSemanales, string porcentaje, string action)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<JornadaLaboral> jornadaLaboral = CollectionsAsistencia.__AsistenciaActualizarJornadaLaboral(codigoJornada, vigente, nombreJornada, descripcionJornada, horasSemanales.Replace(",", "."), porcentaje, action, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { JornadaLaboral = jornadaLaboral });
        }

        public JsonResult IngresarJornadaLaboral(string empresa, string areaNegocio, string nombreJornada, string descripcionJornada, string horasSemanales, string porcentaje)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<JornadaLaboral> jornadaLaboral = CollectionsAsistencia.__AsistenciaIngresarJornadaLaboral(empresa, areaNegocio, nombreJornada, descripcionJornada, horasSemanales.Replace(",", "."), porcentaje, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { JornadaLaboral = jornadaLaboral });
        }

        public JsonResult ObtenerFichaJornadaLaboral(string empresa, string areaNegocio, string ficha)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<FichaJornadaLaboral> fichaJornadaLaboral = CollectionsAsistencia.__AsistenciaObtenerFichaJornadaLaboral(empresa, areaNegocio, ficha, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { FichaJornadaLaboral = fichaJornadaLaboral });
        }

        public JsonResult IngresarFichaJornadaLaboral(string empresa, string areaNegocio, string ficha, string codigoJornada)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<FichaJornadaLaboral> fichaJornadaLaboral = CollectionsAsistencia.__AsistenciaIngresarFichaJornadaLaboral(empresa, areaNegocio, ficha, codigoJornada, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { FichaJornadaLaboral = fichaJornadaLaboral });
        }
        #endregion

        #region HORAS EXTRAS
        public JsonResult IngresarHoraExtra(string empresa, string ficha, string fecha, string areaNegocio, string codigoJornada, string horaExtra, string action)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<HorasExtras> horasExtras = CollectionsAsistencia.__AsistenciaIngresarHoraExtra(empresa, ficha, fecha, areaNegocio, codigoJornada, horaExtra, action, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { HorasExtras = horasExtras });
        }

        private List<HorasExtras> ObtenerHorasExtras(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return CollectionsAsistencia.__HorasExtrasObtener(empresa, ficha, fecha, areaNegocio, token, "");
        }
        #endregion

        #region REPORTES
        private List<ReporteAsistencia> ObtenerReporteHorasColumnas(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return CollectionsAsistencia.__AsistenciaReporteHorasColumnas(empresa, ficha, fecha, areaNegocio, token, "");
        }
        #endregion

        #region BONOS
        public JsonResult ObtenerBonos(string empresa, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaObtenerBonos(empresa, areaNegocio, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult IngresarBonos(string empresa, string areaNegocio, string bono, string descripcion)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaIngresarBonos(empresa, areaNegocio, bono, descripcion, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult ActualizarBono(string codigo, string vigente, string bono, string descripcion, string action)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaActualizarBonos(codigo, vigente, bono, descripcion, action, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult EliminarBono(string codigo)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaEliminarBonos(codigo, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult ObtenerBonoCliente(string empresa, string areaNegocio)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaObtenerBonoCliente(empresa, areaNegocio, objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult IngresarBonoCliente(string empresa, string areaNegocio, string codigo)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaIngresarBonoCliente(empresa, areaNegocio, codigo, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult ActualizarBonoCliente(string empresa, string areaNegocio, string codigo, string vigente, string action)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaActualizarBonoCliente(empresa, areaNegocio, codigo, vigente, action, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }

        public JsonResult EliminarBonoCliente(string empresa, string areaNegocio, string codigo)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Bonos> bonos = CollectionsAsistencia.__AsistenciaEliminarBonoCliente(empresa, areaNegocio, codigo, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { Bonos = bonos });
        }


        public JsonResult ObtenerFichaBonos(string empresa, string areaNegocio, string ficha, string fecha, string pagination, string action)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            List<Personal> personal = new List<Personal>();
            List<FichaBonos> fichaBonos = new List<FichaBonos>();
            List<Bonos> bonos = new List<Bonos>();

            switch (action)
            {
                case "Ficha":
                    personal.Add(ObtenerPersonalData(empresa, ficha, fecha, Session["NombreUsuario"].ToString(), objects[0].Token.ToString()));
                    break;
                case "AreaNegocio":
                    personal = ObtenerPersonalDataV2(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, objects[0].Token.ToString());
                    break;
            }

            if (personal.Count > 0)
            {
                bonos = CollectionsAsistencia.__AsistenciaObtenerBonos(empresa, areaNegocio, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

                fichaBonos = CollectionsAsistencia.__AsistenciaObtenerFichaBonos(empresa, areaNegocio, ficha, fecha, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");
            }

            return Json(new { Personal = personal, Bonos = bonos, FichaBonos = fichaBonos });
        }

        public JsonResult IngresarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<FichaBonos> fichaBonos = CollectionsAsistencia.__AsistenciaIngresarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, valor, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { FichaBonos = fichaBonos });
        }

        public JsonResult ActualizarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<FichaBonos> fichaBonos = CollectionsAsistencia.__AsistenciaActualizarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, valor, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { FichaBonos = fichaBonos });
        }

        public JsonResult EliminarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<FichaBonos> fichaBonos = CollectionsAsistencia.__AsistenciaEliminarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, Session["NombreUsuario"].ToString(), objects[0].Token.ToString(), "");

            return Json(new { FichaBonos = fichaBonos });
        }
        #endregion

        public JsonResult Pagination(string empresa, string ficha, string fecha, string areaNegocio, string pagination, string typePagination)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<Pagination> paginacion = CollectionsAsistencia.__AsistenciaPagination(empresa, ficha, fecha, areaNegocio, Session["NombreUsuario"].ToString(), pagination, typePagination, objects[0].Token.ToString(), "");

            return Json(new { Pagination = paginacion });
        }

        private Personal ObtenerPersonalData(string empresa, string ficha, string fecha, string usuario, string token)
        {
            return CollectionsAsistencia.__PersonalDataObtener(empresa, ficha, fecha, usuario, token, "");
        }

        private List<Personal> ObtenerPersonalDataV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return CollectionsAsistencia.__PersonalDataObtenerV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token, "");
        }

        private List<Licencia> ObtenerLicenciasV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return CollectionsAsistencia.__LicenciaObtenerV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token, "");
        }

        private List<Vacacion> ObtenerVacacionV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return CollectionsAsistencia.__VacacionObtenerV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token, "");
        }

        private List<JornadaLaboral> ObtenerJornadasLaborales(string empresa, string areaNegocio, string usuario, string token)
        {
            return CollectionsAsistencia.__AsistenciaObtenerJornadaLaboral(empresa, areaNegocio, usuario, token, "");
        }

        private List<Bonos> ObtenerBonos(string empresa, string areaNegocio, string usuario, string token)
        {
            return CollectionsAsistencia.__AsistenciaObtenerBonos(empresa, areaNegocio, usuario, token, "");
        }

        private List<Bonos> ObtenerBonoCliente(string empresa, string areaNegocio, string token)
        {
            return CollectionsAsistencia.__AsistenciaObtenerBonoCliente(empresa, areaNegocio, token, "");
        }

        private List<FichaBonos> ObtenerFichaBonos(string empresa, string areaNegocio, string ficha, string fecha,  string token)
        {
            return CollectionsAsistencia.__AsistenciaObtenerFichaBonos(empresa, areaNegocio, ficha, fecha, Session["NombreUsuario"].ToString(), token, "");
        }

        #region "Metodos Carga Masiva"
        [System.Web.Http.AllowAnonymous]
        [ValidateInput(false)]
        [System.Web.Http.HttpPost]
        public string PlantillaSubir(string data, string codigoTransaction = "", string templateColumn = "", string plantillaMasiva = "")
        {
            dynamic request = null;

            try
            {
                dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
                string usuarioCreador = Session["base64Username"].ToString();

                request = CollectionsCargaMasiva.__PlantillaSubir(data, codigoTransaction, usuarioCreador, templateColumn, plantillaMasiva, objects[0].Token.ToString());

            }
            catch (Exception ex)
            {
                request = null;
            }

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.AllowAnonymous]
        [ValidateInput(false)]
        [System.Web.Http.HttpPost]
        public string PlantillaSubirDinamica(string data, string codigoTransaction = "", string templateColumn = "", string plantillaMasiva = "")
        {
            dynamic request = null;

            try
            {
                dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
                string usuarioCreador = Session["base64Username"].ToString();

                request = CollectionsCargaMasiva.__PlantillaSubirDinamica(data, codigoTransaction, usuarioCreador, templateColumn, plantillaMasiva, objects[0].Token.ToString());
            }
            catch (Exception)
            {
                request = null;
            }

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaAsistencia(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaAsistencia(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearAsistencia(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearAsistencia(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaHorasExtras(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaHorasExtras(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearHorasExtras(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearHorasExtras(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }


        #region DOWNLOAD PLANTILLAS
        public ActionResult DownloadFileCargaMasiva()
        {
            List<Personal> personal = new List<Personal>();
            List<Leyenda> leyenda = new List<Leyenda>();
            List<JornadaLaboral> jornadaLaboral = new List<JornadaLaboral>();
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic errores = null;

            string template = Request.QueryString["template"] ?? "";
            string empresa = Request.QueryString["empresa"] ?? "";
            string cliente = Request.QueryString["areanegocio"] ?? "";
            string fecha = Request.QueryString["fecha"] ?? "";

            string nombreArchivo = "";

            List<ResponseCarga> objectsDownload = CollectionsCargaMasiva.__PlantillaDownload(template, objects[0].Token.ToString());

            if (Request.QueryString["codigotransaccion"] != null)
            {
                // aqui obtencion de datos erroneos
                errores = JsonConvert.DeserializeObject(CollectionsCargaMasiva.__PlantillaReportError(Request.QueryString["codigotransaccion"], Request.QueryString["template"], objects[0].Token.ToString()));
            }

            switch (template)
            {
                case "NDY="://ASISTENCIA
                    nombreArchivo = "Asistencia";
                    leyenda = ObtenerLeyenda(objects[0].Token.ToString());

                    break;
                case "NTA="://ASISTENCIA RETAIL
                    nombreArchivo = "Asistencia";
                    leyenda = ObtenerLeyenda(objects[0].Token.ToString());
                    break;
                case "NDg="://HORAS EXTRAS
                    nombreArchivo = "Horas Extras";
                    jornadaLaboral = ObtenerJornadasLaborales(empresa, "", Session["NombreUsuario"].ToString(), objects[0].Token.ToString());
                    break;
                case "NTY="://BONOS
                    nombreArchivo = "Bonos";
                    bonos = ObtenerBonos(empresa, cliente, Session["NombreUsuario"].ToString(), objects[0].Token.ToString());
                    break;
            }

            personal = CollectionsAsistencia.__PersonalDataObtenerV2(empresa, "", fecha, cliente, Session["NombreUsuario"].ToString(), "", objects[0].Token.ToString(), "");

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Plantilla Masiva Para Ingreso de " + nombreArchivo + ".xlsx"));
            Response.ContentType = "application/excel";
            Response.BinaryWrite(XLSX_ReporteAsistencia.DownloadCargaMasiva(objectsDownload, personal, leyenda, jornadaLaboral, bonos, errores, Request.QueryString["codigotransaccion"], empresa, fecha));
            Response.Flush();
            Response.End();

            return View();
        }

        public ActionResult DownloadFileCargaMasivaDinamico()
        {
            List<Personal> personal = new List<Personal>();
            List<Leyenda> leyenda = new List<Leyenda>();
            List<JornadaLaboral> jornadaLaboral = new List<JornadaLaboral>();
            List<Bonos> bonos = new List<Bonos>();
            List<Bonos> bonoCliente = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic errores = null;

            string template = Request.QueryString["template"] ?? "";
            string empresa = Request.QueryString["empresa"] ?? "";
            string cliente = Request.QueryString["areanegocio"] ?? "";
            string fecha = Request.QueryString["fecha"] ?? "";

            List<ResponseCarga> objectsDownload = CollectionsCargaMasiva.__PlantillaDownloadDinamica(template, cliente, fecha, empresa, objects[0].Token.ToString());

            if (Request.QueryString["codigotransaccion"] != null)
            {
                // aqui obtencion de datos erroneos
                errores = JsonConvert.DeserializeObject(CollectionsCargaMasiva.__PlantillaReportError(Request.QueryString["codigotransaccion"], template, objects[0].Token.ToString()));
            }

            personal = CollectionsAsistencia.__PersonalDataObtenerV2(empresa, "", fecha, cliente, Session["NombreUsuario"].ToString(), "", objects[0].Token.ToString(), "");
            leyenda = ObtenerLeyenda(objects[0].Token.ToString());

            switch (template)
            {
                case "NTA=":
                    bonos = ObtenerBonoCliente(empresa, cliente, objects[0].Token.ToString());
                    break;

                default:
                    jornadaLaboral = ObtenerJornadasLaborales(empresa, "", Session["NombreUsuario"].ToString(), objects[0].Token.ToString());
                    break;

            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Plantilla Masiva Para Ingreso de Asistencia" + ".xlsx"));
            Response.ContentType = "application/excel";
            Response.BinaryWrite(XLSX_ReporteAsistencia.DownloadCargaMasivaRetail(objectsDownload, personal, leyenda, jornadaLaboral, bonos, errores, Request.QueryString["codigotransaccion"], empresa, fecha));
            Response.Flush();
            Response.End();
            #endregion

            return View();
        }

        public ActionResult DownloadFileCargaMasivaRelojControl()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic errores = null;

            string template = Request.QueryString["template"] ?? "";

            List<ResponseCarga> objectsDownload = CollectionsCargaMasiva.__PlantillaDownload(template, objects[0].Token.ToString());

            if (Request.QueryString["codigotransaccion"] != null)
            {
                errores = JsonConvert.DeserializeObject(CollectionsCargaMasiva.__PlantillaReportError(Request.QueryString["codigotransaccion"], template, objects[0].Token.ToString()));
            }

            Response.ClearContent();
            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Plantilla Masiva Para Ingreso de Asistencia Reloj Control" + ".xlsx"));
            Response.ContentType = "application/excel";
            Response.BinaryWrite(XLSX_ReporteAsistencia.DownloadCargaMasivaRelojControl(objectsDownload, errores, Request.QueryString["codigotransaccion"], template));
            Response.Flush();
            Response.End();
            return View();
        }


        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaAsistenciaRetail(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaAsistenciaRetail(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearAsistenciaRetail(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearAsistenciaRetail(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaAsistenciaRelojControlGeoVictoria(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaAsistenciaRelojControlGeoVictoria(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearAsistenciaRelojControlGeoVictoria(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearAsistenciaRelojControlGeoVictoria(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaAsistenciaRelojControl(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaAsistenciaRelojControl(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearAsistenciaRelojControl(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearAsistenciaRelojControl(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }


        [System.Web.Http.HttpPost]
        public string PlantillaValidarCargaBono(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaBono(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [System.Web.Http.HttpPost]
        public string PlantillaCrearBono(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearBono(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        private string ConvertLetterExcel(string columna)
        {
            char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            int vuelta = ((Convert.ToInt32(columna)) / 26);
            int index = (Convert.ToInt32(columna)) % 26;

            return Convert.ToInt32(columna) > 25
                ? alfabeto[vuelta - 1].ToString() + alfabeto[index - 1].ToString()
                : alfabeto[index - 1].ToString();
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

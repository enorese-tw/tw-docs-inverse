using AplicacionOperaciones.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using br.com.arcnet.barcodegenerator;
using Rotativa.Options;
using OfficeOpenXml;
using Teamwork.Model.Bajas;
using Newtonsoft.Json;
using Teamwork.WebApi.Auth;
using Teamwork.WebApi.Operaciones;
using Teamwork.Extensions.Excel;

namespace AplicacionOperaciones.Controllers
{
    public class BajasController : Controller
    {
        ServicioAuth.ServicioAuthClient servicioAuth = new ServicioAuth.ServicioAuthClient();
        ServicioCorreo.ServicioCorreoClient servicioCorreo = new ServicioCorreo.ServicioCorreoClient();
        ServicioOperaciones.ServicioOperacionesClient servicioOperaciones = new ServicioOperaciones.ServicioOperacionesClient();

        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";
        string sistema = "";
        string ambiente = "";
        string section = "";
        string schema = "";
        string codAction = "";
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.";
        string errorSistemaCorreo = "Ha ocurrido un problema en el envio por correo electrónico de la solicitud de contratos";
        string fromDebugQA = "AplicacionOperaciones";
        
        public ActionResult Index()
        {
            if (AplicationActive())
            {
                try
                {
                    #region "PERMISOS Y ACCESOS"

                    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                    {
                        Session["ApplicationActive"] = null;
                    }

                    string sistema = fromDebugQA;
                    
                    /** APLICACION DE HEADERS */

                    string[] parametrosHeadersAccess =
                    {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CODIFICARAUTH",
                    "@SISTEMA",
                    "@TRABAJADOR"
                };

                    string[] valoresHeadersAccess =
                    {
                    tokenAuth,
                    agenteAplication,
                    Session["CodificarAuth"].ToString(),
                    sistema,
                    Session["NombreUsuario"].ToString()
                };

                    /** END APLICACION DE HEADERS */

                    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
                    List<Accesos> accesos = new List<Accesos>();

                    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                    {
                        Accesos acceso = new Accesos();

                        acceso.Code = rowsPermisos["Code"].ToString();
                        acceso.Message = rowsPermisos["Message"].ToString();
                        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
                        acceso.PreController = rowsPermisos["PreController"].ToString();
                        acceso.Controller = rowsPermisos["Controllers"].ToString();
                        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                        accesos.Add(acceso);
                    }

                    Session["RenderizadoPermisos"] = accesos;


                    #endregion

                    #region "URL PATH APLICATIVO"

                    string prefixApp = "";
                    string urlServer = "";
                    string controller = "";

                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                    {
                        urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                        prefixApp = "/" + Request.Url.AbsoluteUri.Split('/')[3];
                        controller = Request.Url.AbsoluteUri.Split('/')[4];
                    }
                    else if (Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                    {
                        urlServer = "181.190.6.196";
                    }
                    else
                    {
                        urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                        controller = Request.Url.AbsoluteUri.Split('/')[3];
                    }

                    #endregion

                    List<HeaderSolicitud> headerSolicitudB = new List<HeaderSolicitud>();
                    List<Prioridades> prioridades = new List<Prioridades>();
                    List<Pagination> paginatorsB = new List<Pagination>();
                    List<Solicitud> solicitudBajas = new List<Solicitud>();

                    #region "HEADER BAJAS"

                    string[] paramHeaderSolR =
                    {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR",
                    "@CODIGOTRANSACCION",
                    "@TIPOSOLICITUD",
                    "@PAGINATION",
                    "@TYPEFILTER",
                    "@DATAFILTER"
                };

                    string[] valHeaderSolR =
                    {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),
                    "",
                    "U29saWNpdHVkQmFqYQ==",
                    "MS01",
                    "",
                    ""
                };

                    DataSet dataHeaderSolicitudR = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolR, valHeaderSolR).Table;

                    foreach (DataRow item in dataHeaderSolicitudR.Tables[0].Rows)
                    {
                        HeaderSolicitud header = new HeaderSolicitud();

                        header.PostTitulo = item["PostTitulo"].ToString();
                        header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                        header.HtmlPagination = item["HtmlPagination"].ToString();
                        header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                        header.TipoSolicitud = item["TipoSolicitud"].ToString();

                        header.Creado = item["Creado"].ToString();
                        header.Estado = item["Estado"].ToString();
                        header.Glyphicon = item["Glyphicon"].ToString();
                        header.GlyphiconColor = item["GlyphiconColor"].ToString();
                        header.EjecutivoKam = item["EjecutivoKam"].ToString();
                        header.EjecutivoCreador = item["EjecutivoCreador"].ToString();
                        header.Cliente = item["Cliente"].ToString();

                        header.NombreProceso = item["NombreProceso"].ToString();
                        header.ResultSearch = item["ResultSearch"].ToString();

                        headerSolicitudB.Add(header);
                    }

                    ViewBag.RenderizadoHeaderSolicitudBajas = headerSolicitudB;
                    ViewBag.RenderizaSolicitud = false;

                    #endregion

                    #region "PRIORIDADES ESTADOS"

                    string[] paramPrioridades =
                    {
                    "@AUTHENTICATE",
                    "@AGENTAPP" ,
                    "@PRIORIDADESADO",
                    "@DATA"
                };

                    string[] valPrioridades =
                    {
                    tokenAuth,
                    agenteAplication,
                    "UHJpb3JpZGFk",
                    "Bajas"
                };

                    DataSet dataPrioridades = servicioOperaciones.GetPrioridadesEstado(paramPrioridades, valPrioridades).Table;

                    foreach (DataRow rows in dataPrioridades.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Prioridades prioridad = new Prioridades();

                            prioridad.Code = rows["Code"].ToString();
                            prioridad.Message = rows["Message"].ToString();
                            prioridad.Nombre = rows["Nombre"].ToString();
                            prioridad.Glyphicon = rows["Glyphicon"].ToString();
                            prioridad.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            prioridad.BorderColor = rows["BorderColor"].ToString();
                            prioridad.ColorFont = rows["ColorFont"].ToString();

                            prioridades.Add(prioridad);
                        }
                    }

                    ViewBag.RenderizadoPrioridadesBajas = prioridades;

                    #endregion

                    ViewBag.RenderizadoSolicitudBajas = ModuleSolicitudBajas("MS01", "", "");

                    ViewBag.RenderizadoPaginations = ModulePagination("SolicitudBaja", "MS01", "S", "", "");
                    ViewBag.RenderizadoHasBaja = "S";

                    ViewBag.TypeView = "";

                    ViewBag.CodeCorrect = true;
                }
                catch (Exception ex)
                {
                    List<Error> errores = new List<Error>();
                    ViewBag.CodeCorrect = false;
                    Error error = new Error();

                    error.Code = "500.*";
                    error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                    errores.Add(error);

                    ViewBag.Error = errores;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }
        
        public ActionResult Historial()
        {
            if (AplicationActive())
            {
                #region "PERMISOS Y ACCESOS"

                if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                {
                    Session["ApplicationActive"] = null;
                }

                sistema = fromDebugQA;
                schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                /** APLICACION DE HEADERS */

                string[] parametrosHeadersAccess =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CODIFICARAUTH",
                    "@SISTEMA",
                    "@TRABAJADOR"
                };

                string[] valoresHeadersAccess =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["CodificarAuth"].ToString(),
                    sistema,
                    Session["NombreUsuario"].ToString()
                };

                /** END APLICACION DE HEADERS */

                DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
                List<Accesos> accesos = new List<Accesos>();

                foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                {
                    Accesos acceso = new Accesos();

                    acceso.Code = rowsPermisos["Code"].ToString();
                    acceso.Message = rowsPermisos["Message"].ToString();
                    acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
                    acceso.PreController = rowsPermisos["PreController"].ToString();
                    acceso.Controller = rowsPermisos["Controllers"].ToString();
                    acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                    acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                    accesos.Add(acceso);
                }

                Session["RenderizadoPermisos"] = accesos;

                #endregion

                List<Models.Historial> historiales = new List<Models.Historial>();
                string data = string.Empty;
                string tipoEvento = string.Empty;
                string tipoHistorial = string.Empty;

                data = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                tipoEvento = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2].Split(new string[] { "!!" }, StringSplitOptions.RemoveEmptyEntries)[0];
                tipoHistorial = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2].Split(new string[] { "!!" }, StringSplitOptions.RemoveEmptyEntries)[1];

                string[] paramHistorial =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TIPOEVENTO",
                    "@TIPOHISTORIAL",
                    "@DATA"
                };

                string[] valHistorial =
                {
                    tokenAuth,
                    agenteAplication,
                    tipoEvento,
                    tipoHistorial,
                    data
                };

                DataSet dataHistorial = servicioOperaciones.GetHistorial(paramHistorial, valHistorial).Table;

                foreach (DataRow rows in dataHistorial.Tables[0].Rows)
                {
                    Models.Historial historial = new Models.Historial();

                    if (rows["Code"].ToString() == "200")
                    {
                        historial.Code = rows["Code"].ToString();
                        historial.Message = rows["Message"].ToString();
                        historial.Estado = rows["Estado"].ToString();
                        historial.Fecha = rows["Fecha"].ToString();
                        historial.Usuario = rows["Usuario"].ToString();
                        historial.Comentario = rows["Comentario"].ToString();
                    }
                    else
                    {
                        historial.Code = rows["Code"].ToString();
                        historial.Code = rows["Code"].ToString();
                    }

                    historiales.Add(historial);
                }

                ViewBag.RenderizadoHistorial = historiales;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Pdf()
        {
            if (AplicationActive())
            {
                #region "PERMISOS Y ACCESOS"

                if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                {
                    Session["ApplicationActive"] = null;
                }

                sistema = fromDebugQA;
                schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];

                #endregion

                string[] paramPdf = new string[4];
                string[] valPdf = new string[4];
                DataSet dataPdf;

                paramPdf[0] = "@AUTHENTICATE";
                paramPdf[1] = "@AGENTAPP";
                paramPdf[2] = "@PDF";
                paramPdf[3] = "@DATA";

                valPdf[0] = tokenAuth;
                valPdf[1] = agenteAplication;
                valPdf[2] = section;
                valPdf[3] = schema;

                dataPdf = servicioOperaciones.GetPdf(paramPdf, valPdf).Table;

                foreach (DataRow rows in dataPdf.Tables[0].Rows)
                {
                    ViewBag.Barcode = rows["Barcode"].ToString();
                    ViewBag.TitlePdf = rows["Title"].ToString();

                    switch (section)
                    {
                        default:
                            using (MemoryStream ms = new MemoryStream())
                            {
                                var codigo = rows["Barcode"].ToString();

                                var barcode2 = new Barcode(codigo, TypeBarcode.Code39);
                                var bar128 = barcode2.Encode(TypeBarcode.Code39, codigo, 300, 100);
                                bar128.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                ViewBag.BarcodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                            }
                            break;
                    }


                    ViewBag.RenderizaHtmlPdf = rows["Html"].ToString();
                }

                ViewBag.Pdf = section;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }
        
        public ActionResult GeneratePdf()
        {
            string pdf = string.Empty;
            string data = string.Empty;

            pdf = Request.QueryString["pdf"].ToString();
            data = Request.QueryString["data"].ToString();
            
            return new ActionAsPdf("Pdf/" + pdf + "/" + data)
            {
                FileName = "SIS_DOC_FIN_" + 
                DateTime.Now.Day + "" + 
                DateTime.Now.Month + "" + 
                DateTime.Now.Year + "" + 
                DateTime.Now.Hour + "" + 
                DateTime.Now.Minute + "" + 
                DateTime.Now.Second + "" + 
                DateTime.Now.Millisecond +
                ".pdf"
            };
        }

        public ActionResult ViewPdf()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() +"/Auth");
            }

            string[] paramPdf = new string[4];
            string[] valPdf = new string[4];
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            DataSet dataPdf;
            byte[] pdfbyte = null;
            switch (Request.QueryString["pdf"].ToString())
            {
                case "TGlxdWlkYWNpb25TdWVsZG8=":

                    paramMantFin[0] = "@ACCION";
                    paramMantFin[1] = "@USUARIOCREADOR";
                    paramMantFin[2] = "@CODIGOFINIQUITO";
                    paramMantFin[3] = "@METODOPAGO";
                    paramMantFin[4] = "@CODIGOPAGO";
                    paramMantFin[5] = "@CODIGOPROCESO";
                    paramMantFin[6] = "@DESTINATARIO";
                    paramMantFin[7] = "@BANCO";
                    paramMantFin[8] = "@TIPOCUENTA";
                    paramMantFin[9] = "@FECHA";
                    paramMantFin[10] = "@NUMEROCUENTA";
                    paramMantFin[11] = "@RUT";
                    paramMantFin[12] = "@OBSERVACIONES";
                    paramMantFin[13] = "@MONTO";
                    paramMantFin[14] = "@VIGENTE";
                    paramMantFin[15] = "@SECUENCIA";
                    paramMantFin[16] = "@SERIECHQ";
                    paramMantFin[17] = "@EMPRESAORIGEN";
                    paramMantFin[18] = "@PAGINATION";
                    paramMantFin[19] = "@TYPEFILTER";
                    paramMantFin[20] = "@DATAFILTER";
                    
                    valMantFin[0] = "FILEDATALIQSUELDO";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = Request.QueryString["code"].ToString();
                    valMantFin[3] = "";
                    valMantFin[4] = "";
                    valMantFin[5] = "";
                    valMantFin[6] = "";
                    valMantFin[7] = "";
                    valMantFin[8] = "";
                    valMantFin[9] = "";
                    valMantFin[10] = "";
                    valMantFin[11] = "";
                    valMantFin[12] = "";
                    valMantFin[13] = "";
                    valMantFin[14] = "";
                    valMantFin[15] = Request.QueryString["type"].ToString();
                    valMantFin[16] = "";
                    valMantFin[17] = "";
                    valMantFin[18] = "";
                    valMantFin[19] = "";
                    valMantFin[20] = Request.QueryString["data"].ToString();

                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                    foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                    {
                        var generator = new NReco.PdfGenerator.HtmlToPdfConverter();
                        pdfbyte = Convert.FromBase64String(rows["ArchivoBase64"].ToString());
                    }

                    break;
                default:
                    paramPdf[0] = "@AUTHENTICATE";
                    paramPdf[1] = "@AGENTAPP";
                    paramPdf[2] = "@PDF";
                    paramPdf[3] = "@DATA";

                    valPdf[0] = tokenAuth;
                    valPdf[1] = agenteAplication;
                    valPdf[2] = Request.QueryString["pdf"].ToString();
                    valPdf[3] = Request.QueryString["data"].ToString();
            
                    dataPdf = servicioOperaciones.GetPdf(paramPdf, valPdf).Table;

                    foreach (DataRow rows in dataPdf.Tables[0].Rows)
                    {
                        MemoryStream ms = new System.IO.MemoryStream();
                        byte[] byteStream = ms.ToArray();

                        ViewBag.Barcode = rows["Barcode"].ToString();
                        ViewBag.TitlePdf = rows["Title"].ToString();

                        using (MemoryStream msBase64 = new MemoryStream())
                        {
                            var codigo = rows["Barcode"].ToString();

                            var barcode2 = new Barcode(codigo, TypeBarcode.Code39);
                            var bar128 = barcode2.Encode(TypeBarcode.Code39, codigo, 300, 100);
                            bar128.Save(msBase64, System.Drawing.Imaging.ImageFormat.Png);

                            ViewBag.Base64Imagen = "data:image/png;base64," + Convert.ToBase64String(msBase64.ToArray());
                        }

                        var generator = new NReco.PdfGenerator.HtmlToPdfConverter();
                        pdfbyte = generator.GeneratePdf(rows["Html"].ToString().Replace("{{Base64Imagen}}", ViewBag.Base64Imagen));
                    }
                    break;
            }
                    
            return new FileContentResult(pdfbyte, "application/pdf");
            
        }

        public ActionResult Finiquitos()
        {
            if(AplicationActive())
            {
                try
                {
                    string domain = string.Empty;
                    string prefixDomain = string.Empty;
                    string domainReal = string.Empty;
                    string section = string.Empty;
                    schema = fromDebugQA;

                    #region "PERMISOS Y ACCESOS"

                    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                    {
                        Session["ApplicationActive"] = null;
                    }

                    string sistema = fromDebugQA;

                    /** APLICACION DE HEADERS */

                    string[] parametrosHeadersAccess =
                    {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CODIFICARAUTH",
                    "@SISTEMA",
                    "@TRABAJADOR"
                };

                    string[] valoresHeadersAccess =
                    {
                    tokenAuth,
                    agenteAplication,
                    Session["CodificarAuth"].ToString(),
                    sistema,
                    Session["NombreUsuario"].ToString()
                };

                    /** END APLICACION DE HEADERS */

                    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
                    List<Accesos> accesos = new List<Accesos>();

                    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                    {
                        Accesos acceso = new Accesos();

                        acceso.Code = rowsPermisos["Code"].ToString();
                        acceso.Message = rowsPermisos["Message"].ToString();
                        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
                        acceso.PreController = rowsPermisos["PreController"].ToString();
                        acceso.Controller = rowsPermisos["Controllers"].ToString();
                        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                        accesos.Add(acceso);
                    }

                    Session["RenderizadoPermisos"] = accesos;


                    #endregion

                    #region "CONTROL DE RETORNO"

                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                    {
                        if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                        {
                            domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                        }
                        else
                        {
                            domainReal = "181.190.6.196";
                        }

                        domain = "http://" + domainReal + "/";
                        prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
                    }
                    else
                    {
                        domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                        prefixDomain = "";
                    }

                    #endregion

                    #region "DASHBOARD"

                    if (ModulePerfilAuditorAdminFiniquito())
                    {
                        ViewBag.RenderizadoDashboard = ModuleDashboardPendCalcVer();
                        ViewBag.RenderizadoFiniquitos = null;
                    }
                    else
                    {
                        switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2])
                        {
                            case "Finiquitos":
                                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                                {
                                    case "Complementos":
                                        ViewBag.SectionEnabled = "Complementos";

                                        ViewBag.RenderizaComplementos = ModuleComplementos("MS01", "", "", "");
                                        ViewBag.RenderizadoPaginations = ModulePagination("Complementos", "MS01", "S", "", "");
                                        ViewBag.ReferenciaPaginationIndex = "MS01";
                                        ViewBag.HasSectionIndv = "All";
                                        ViewBag.RenderizadoDashboard = null;
                                        ViewBag.OnlyFiniquito = "N";
                                        break;
                                    default:
                                        ViewBag.SectionEnabled = "Finiquito";
                                        ViewBag.HasSectionIndv = "Finiquito";
                                        ViewBag.RenderizadoDashboard = null;
                                        ViewBag.RenderizadoFiniquitos = ModuleFiniquitos("MS01", schema, "", "");
                                        ViewBag.RenderizaComplementos = ModuleComplementos("MS01", schema, "CodigoFiniquito", "");
                                        ViewBag.OnlyFiniquito = "S";
                                        break;
                                }

                                break;
                            case "Complementos":

                                ViewBag.SectionEnabled = "Complementos";
                                ViewBag.HasSectionIndv = "Complemento";
                                ViewBag.RenderizadoDashboard = null;
                                ViewBag.RenderizaComplementos = ModuleComplementos("MS01", "", "CodigoComplemento", schema);
                                ViewBag.OnlyFiniquito = "S";

                                break;
                            case "Bajas":

                                ViewBag.SectionEnabled = "Finiquito";
                                ViewBag.EnlacePagar = ModuleControlRetornoPath() + "/Recepcion/Pagar";
                                ViewBag.EnlaceNotariar = ModuleControlRetornoPath() + "/Recepcion/Notariar";

                                ViewBag.RenderizadoFiniquitos = ModuleFiniquitos("MS01", schema, "", "");
                                ViewBag.RenderizadoPaginations = ModulePagination("Finiquitos", "MS01", "S", "", "");
                                ViewBag.ReferenciaPaginationIndex = "MS01";
                                ViewBag.HasSectionIndv = "All";
                                ViewBag.RenderizadoDashboard = null;

                                break;
                        }
                    }

                    #endregion

                    ViewBag.HTMLLoaderError = "";

                    ViewBag.CodeCorrect = true;
                }
                catch (Exception ex)
                {
                    List<Error> errores = new List<Error>();
                    ViewBag.CodeCorrect = false;
                    Error error = new Error();

                    error.Code = "500.*";
                    error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                    errores.Add(error);

                    ViewBag.Error = errores;
                }

            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Simulacion()
        {
            if (AplicationActive())
            {
                try
                {
                    string domain = string.Empty;
                    string prefixDomain = string.Empty;
                    string domainReal = string.Empty;

                    if (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1] != "Simulacion")
                    {
                        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                    }
                    else
                    {
                        schema = "";
                    }

                    #region "PERMISOS Y ACCESOS"

                    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                    {
                        Session["ApplicationActive"] = null;
                    }

                    string  sistema = fromDebugQA;

                    /** APLICACION DE HEADERS */

                    string[] parametrosHeadersAccess =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIFICARAUTH",
                        "@SISTEMA",
                        "@TRABAJADOR"
                    };

                    string[] valoresHeadersAccess =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["CodificarAuth"].ToString(),
                        sistema,
                        Session["NombreUsuario"].ToString()
                    };

                    /** END APLICACION DE HEADERS */

                    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
                    List<Accesos> accesos = new List<Accesos>();

                    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                    {
                        Accesos acceso = new Accesos();

                        acceso.Code = rowsPermisos["Code"].ToString();
                        acceso.Message = rowsPermisos["Message"].ToString();
                        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
                        acceso.PreController = rowsPermisos["PreController"].ToString();
                        acceso.Controller = rowsPermisos["Controllers"].ToString();
                        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                        accesos.Add(acceso);
                    }

                    Session["RenderizadoPermisos"] = accesos;


                    #endregion

                    #region "CONTROL DE RETORNO"

                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                    {
                        if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                        {
                            domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                        }
                        else
                        {
                            domainReal = "181.190.6.196";
                        }

                        domain = "http://" + domainReal + "/";
                        prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
                    }
                    else
                    {
                        domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                        prefixDomain = "";
                    }

                    #endregion

                    #region "DASHBOARD"

                    if (ModulePerfilAuditorAdminFiniquito())
                    {
                        ViewBag.RenderizadoDashboard = ModuleDashboardPendCalcVer();
                        ViewBag.RenderizadoFiniquitos = null;
                    }
                    else
                    {
                        ViewBag.RenderizadoDashboard = null;
                        ViewBag.RenderizadoFiniquitos = ModuleSimulacion("MS01", schema, "", "");


                        ViewBag.EnlacePagar = ModuleControlRetornoPath() + "/Recepcion/Pagar";
                        ViewBag.EnlaceNotariar = ModuleControlRetornoPath() + "/Recepcion/Notariar";

                        ViewBag.ReferenciaDashboardFiniquitosEstados = ModuleDashboardFiniquitos();
                    }

                    #endregion

                    #region "PAGINATOR DE SIMULACION"

                    if (schema == "")
                    {
                        ViewBag.RenderizadoPaginations = ModulePagination("Simulacion", "MS01", "S", "", "");
                        ViewBag.ReferenciaPaginationIndex = "MS01";
                        ViewBag.HasFiniquitoIndividual = false;
                    }
                    else
                    {
                        ViewBag.HasFiniquitoIndividual = true;
                    }

                    #endregion

                    ViewBag.ReferenciaRedireccionamiento = "";
                    ViewBag.HTMLLoaderError = "";

                    ViewBag.CodeCorrect = true;
                }
                catch (Exception ex)
                {
                    List<Error> errores = new List<Error>();
                    ViewBag.CodeCorrect = false;
                    Error error = new Error();

                    error.Code = "500.*";
                    error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                    errores.Add(error);

                    ViewBag.Error = errores;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Notariar()
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
                List<bool> sessionesAction = new List<bool>();

                if ((bool)Session["ApplicationReporteExcelFiniquitos"])
                {
                    string tipoDocumento = string.Empty;
                    string fechaInicio = string.Empty;
                    string fechaTermino = string.Empty;
                    string cliente = string.Empty;
                    string estado = string.Empty;
                    string empresa = string.Empty;
                    byte[] bytes = null;

                    if (Request.QueryString["doc"] != null)
                    {
                        tipoDocumento = (Request.QueryString["doc"] != "ALL") ? Request.QueryString["doc"] : "";
                    }

                    if (Request.QueryString["fechainicio"] != null)
                    {
                        fechaInicio = (Request.QueryString["fechainicio"] != "ALL") ? Request.QueryString["fechainicio"] : "";
                    }

                    if (Request.QueryString["fechatermino"] != null)
                    {
                        fechaTermino = (Request.QueryString["fechatermino"] != "ALL") ? Request.QueryString["fechatermino"] : "";
                    }

                    if (Request.QueryString["cliente"] != null)
                    {
                        cliente = (Request.QueryString["cliente"] != "ALL") ? Request.QueryString["cliente"] : "";
                    }

                    if (Request.QueryString["estado"] != null)
                    {
                        estado = (Request.QueryString["estado"] != "ALL") ? Request.QueryString["estado"] : "";
                    }

                    if (Request.QueryString["empresa"] != null)
                    {
                        empresa = (Request.QueryString["empresa"] != "ALL") ? Request.QueryString["empresa"] : "";
                    }

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        bytes = XLSX_ReporteFiniquitos.__xlsx(
                            Session["base64Username"].ToString(),
                            tipoDocumento,
                            fechaInicio,
                            fechaTermino,
                            cliente,
                            estado,
                            empresa,
                            servicioOperaciones
                        );

                        if (bytes != null)
                        {
                            ViewBag.ReferenciaSuccessExcel = true;
                            Session["ApplicationReporteExcelFiniquitosSuccess"] = true;

                            excel.Workbook.Properties.Title = "Attempts";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Finiquitos.xlsx"));
                            Response.ContentType = "application/excel";
                            Response.BinaryWrite(bytes);
                            Response.End();
                            Response.Flush();
                        }
                        else
                        {
                            ViewBag.ReferenciaMessageReporte = "<img src='../../../../Resources/cancel.png' style='width: 50px; border-radius: 50%;' class='danger dspl-inline-block' /> Ups! Lo sentimos, no se ha podido obtener el reporte solicitado!";
                            ViewBag.ReferenciaMessageReporteImp = "Puede cerrar esta pagina o recargarla para volver a solicitar el reporte!";
                            ViewBag.ReferenciaSuccessExcel = false;
                            Session["ApplicationReporteExcelFiniquitosSuccess"] = false;
                        }

                        ViewBag.ReferenciaReporteExcel = false;
                        Session["ApplicationReporteExcelFiniquitos"] = false;
                    }
                }
                else
                {
                    if ((bool)Session["ApplicationReporteExcelFiniquitosSuccess"])
                    {
                        ViewBag.ReferenciaMessageReporte = "<img src='../../../../Resources/check.png' style='width: 50px; border-radius: 50%;' class='success dspl-inline-block' /> Se ha descargado exitosamente el reporte solicitado!";
                        ViewBag.ReferenciaReporteExcel = false;
                        Session["ApplicationReporteExcelFiniquitos"] = false;
                        ViewBag.ReferenciaSuccessExcel = false;
                        Session["ApplicationReporteExcelFiniquitosSuccess"] = false;
                    }
                    else
                    {
                        ViewBag.ReferenciaMessageReporte = "Estamos preparando el archivo con el reporte solicitado, espere un momento...";
                        ViewBag.ReferenciaMessageReporteImp = "Importante! al obtener el reporte solicitado podrá cerrar esta pagina, de lo contrario el sistema indicara que no es posible obtener el reporte.";
                        ViewBag.ReferenciaReporteExcel = true;

                        Session["ApplicationReporteExcelFiniquitos"] = true;
                        ViewBag.ReferenciaSuccessExcel = false;
                        Session["ApplicationReporteExcelFiniquitosSuccess"] = false;
                    }
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
               
            return View();
        }

        public ActionResult Reporte()
        {
            if (AplicationActive())
            {
                try
                {
                    #region "PERMISOS Y ACCESOS"

                    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                    {
                        Session["ApplicationActive"] = null;
                    }

                    string sistema = "";

                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                    {
                        sistema = Request.Url.AbsoluteUri.Split('/')[3];
                    }
                    else
                    {
                        sistema = fromDebugQA;
                    }

                    /** APLICACION DE HEADERS */

                    string[] parametrosHeadersAccess =
                    {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CODIFICARAUTH",
                    "@SISTEMA",
                    "@TRABAJADOR"
                };

                    string[] valoresHeadersAccess =
                    {
                    tokenAuth,
                    agenteAplication,
                    Session["CodificarAuth"].ToString(),
                    sistema,
                    Session["NombreUsuario"].ToString()
                };

                    /** END APLICACION DE HEADERS */

                    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
                    List<Accesos> accesos = new List<Accesos>();

                    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                    {
                        Accesos acceso = new Accesos();

                        acceso.Code = rowsPermisos["Code"].ToString();
                        acceso.Message = rowsPermisos["Message"].ToString();
                        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
                        acceso.PreController = rowsPermisos["PreController"].ToString();
                        acceso.Controller = rowsPermisos["Controllers"].ToString();
                        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                        accesos.Add(acceso);
                    }

                    Session["RenderizadoPermisos"] = accesos;


                    #endregion
                }
                catch (Exception ex)
                {
                    List<Error> errores = new List<Error>();
                    ViewBag.CodeCorrect = false;
                    Error error = new Error();

                    error.Code = "500.*";
                    error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                    errores.Add(error);

                    ViewBag.Error = errores;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }

        #region "HTTPPost"
        
        [HttpPost]
        public ActionResult AgregarMontosManualesCartaBaja(string codigoSolicitud, string enlaceCartaBaja, string afc = "", string mesaviso = "N")
        {
            string view = string.Empty;

            try
            {
                if (afc == "")
                {
                    afc = "0";
                }

                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "CARTABAJAADDMM";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoSolicitud;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = afc;
                valMantFin[14] = mesaviso;
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Bajas/_CartaBaja";
                            ViewBag.ReferenciaViewCartaBaja = enlaceCartaBaja;
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }
            
            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConsultarCartaBajaMontosManuales(string codigoSolicitud, string enlaceCartaBaja)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";
                
                valMantFin[0] = "CONSMMADDCARTABAJA";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoSolicitud;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            switch (rows["HasHabilitadoMM"].ToString())
                            {
                                case "S":
                                    ViewBag.ReferenciaCodigoSolicitud = codigoSolicitud;
                                    ViewBag.ReferenciaViewCartaBaja = enlaceCartaBaja;
                                    if (rows["YearIAS"].ToString() != "0")
                                    {
                                        ViewBag.ReferenciaYearIAS = rows["YearIAS"].ToString();
                                    }
                                    else
                                    {
                                        ViewBag.ReferenciaYearIAS = "";
                                    }
                                    ViewBag.ReferenciaDescripcionCausal = rows["CausalDescripcion"].ToString();
                                    ViewBag.ReferenciaHasHabilitadoMM = rows["HasHabilitadoMM"].ToString();
                                    view = "Bajas/_ModalFMontosManualCartaBaja";
                                    break;
                                default:
                                    view = "Bajas/_CartaBaja";
                                    ViewBag.ReferenciaViewCartaBaja = enlaceCartaBaja;
                                    ViewBag.ReferenciaDescripcionCausal = rows["CausalDescripcion"].ToString();
                                    ViewBag.ReferenciaHasHabilitadoMM = rows["HasHabilitadoMM"].ToString();
                                    break;
                            }
                            break;
                        case "300":
                            view = "Bajas/_CartaBaja";
                            ViewBag.ReferenciaViewCartaBaja = enlaceCartaBaja;
                            ViewBag.ReferenciaCodigoSolicitud = codigoSolicitud;
                            if (rows["YearIAS"].ToString() != "0")
                            {
                                ViewBag.ReferenciaYearIAS = rows["YearIAS"].ToString();
                            }
                            else
                            {
                                ViewBag.ReferenciaYearIAS = "";
                            }
                            ViewBag.ReferenciaDescripcionCausal = rows["CausalDescripcion"].ToString();
                            ViewBag.ReferenciaHasHabilitadoMM = rows["HasHabilitadoMM"].ToString();
                            break;
                    }
                }
                
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult Finiquitos(string pagination, string hasBaja = "S", string typeFilter = "", string filterDatos = "", string filterEmpresa = "",
                                       string filterCausal = "", string filterEstado = "", string filterDatoFechaIni = "", string filterDatoFechaEnd = "")
        {
            try
            {

                string domain = string.Empty;
                string prefixDomain = string.Empty;
                string domainReal = string.Empty;
                List<Models.Finiquitos.Finiquitos> finiquitos = new List<Models.Finiquitos.Finiquitos>();
                string filter = string.Empty;

                #region "CONTROL DE RETORNO"

                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                {
                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                    {
                        domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                    }
                    else
                    {
                        domainReal = "181.190.6.196";
                    }

                    domain = "http://" + domainReal + "/";
                    prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
                }
                else
                {
                    domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                    prefixDomain = "";
                }

                #endregion
                

                if (filterDatos != "")
                {
                    filter = filterDatos;
                }

                if (filterEmpresa != "")
                {
                    filter = filterEmpresa;
                }

                if (filterCausal != "")
                {
                    filter = filterCausal;
                }

                if (filterEstado != "")
                {
                    filter = filterEstado;
                }

                if (filterDatoFechaIni != "" && filterDatoFechaEnd != "")
                {
                    filter = filterDatoFechaIni + "@" + filterDatoFechaEnd;
                }

                ViewBag.RenderizadoFiniquitos = ModuleFiniquitos(pagination, "", typeFilter, filter);
                ViewBag.RenderizadoPaginations = ModulePagination("Finiquitos", pagination, hasBaja, typeFilter, filter);
                ViewBag.ReferenciaPaginationIndex = pagination;
                ViewBag.SectionEnabled = "Finiquito";

                ViewBag.HasSectionIndv = "All";
                ViewBag.HTMLLoaderError = "";
                ViewBag.CodeCorrect = true;
                

            }
            catch (Exception ex)
            {
                List<Error> errores = new List<Error>();
                ViewBag.CodeCorrect = false;
                Error error = new Error();

                error.Code = "500.*";
                error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                errores.Add(error);

                ViewBag.Error = errores;
            }

            return PartialView("Bajas/_Finiquitos");
        }

        [HttpPost]
        public ActionResult SolicitudBajas(string pagination, string hasBaja = "S", string typeFilter = "", string filterDatos = "", string filterEmpresa = "", 
                                           string filterCausal = "", string filterEstado = "", string filterDatoFechaIni = "", string filterDatoFechaEnd = "")
        {


            #region "URL PATH APLICATIVO"

            string prefixApp = "";
            string urlServer = "";
            string controller = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                prefixApp = "/" + Request.Url.AbsoluteUri.Split('/')[3];
                controller = Request.Url.AbsoluteUri.Split('/')[4];
            }
            else if (Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
            {
                urlServer = "181.190.6.196";
            }
            else
            {
                urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                controller = Request.Url.AbsoluteUri.Split('/')[3];
            }

            #endregion
            
            string filter = string.Empty;
            List<Solicitud> solicitudBajas = new List<Solicitud>();

            if (filterDatos != "")
            {
                filter = filterDatos;
            }

            if (filterEmpresa != "")
            {
                filter = filterEmpresa;
            }

            if (filterCausal != "")
            {
                filter = filterCausal;
            }

            if (filterEstado != "")
            {
                filter = filterEstado;
            }

            if (filterDatoFechaIni != "" && filterDatoFechaEnd != "")
            {
                filter = filterDatoFechaIni + "@" + filterDatoFechaEnd;
            }
            
            ViewBag.RenderizadoSolicitudBajas = ModuleSolicitudBajas(pagination, typeFilter, filter);
            ViewBag.RenderizadoPaginations = ModulePagination("SolicitudBaja", pagination, hasBaja, typeFilter, filter);

            ViewBag.CodeCorrect = true;
            ViewBag.RenderizadoHasBaja = hasBaja;
            ViewBag.TypeView = "";
            
            return PartialView("Bajas/_SolicitudBaja");
        }

        [HttpPost]
        public ActionResult Complementos(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            string view = string.Empty;

            ViewBag.RenderizaComplementos = ModuleComplementos(pagination, "", typeFilter, dataFilter);
            ViewBag.RenderizadoPaginations = ModulePagination("Complementos", pagination, "S", dataFilter, typeFilter);
            ViewBag.HasSectionIndv = "All";
            ViewBag.OnlyFiniquito = "N";
            view = "Bajas/_Complementos";

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AnularSolicitudBaja(string codigoSolicitud, string razonAnulacionBaja)
        {
            string[] paramCambioEstado = new string[5];
            string[] valCambioEstado = new string[5];

            string[] paramCorreo = new string[5];
            string[] valCorreo = new string[5];

            string vista = string.Empty;

            paramCambioEstado[0] = "@AUTHENTICATE";
            paramCambioEstado[1] = "@AGENTAPP";
            paramCambioEstado[2] = "@TRABAJADOR";
            paramCambioEstado[3] = "@ESTADO";
            paramCambioEstado[4] = "@DATA";

            valCambioEstado[0] = tokenAuth;
            valCambioEstado[1] = agenteAplication;
            valCambioEstado[2] = Session["NombreUsuario"].ToString();
            valCambioEstado[3] = "ANU@!@" + razonAnulacionBaja;
            valCambioEstado[4] = codigoSolicitud;

            DataSet dataCambioEstado = servicioOperaciones.SetCambiarEstadoSolicitud(paramCambioEstado, valCambioEstado).Table;

            foreach (DataRow rows in dataCambioEstado.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    ViewBag.HTMLLoaderError = rows["Message"].ToString();
                    ViewBag.TypeView = "_CompletoGeneric";
                }
                else
                {
                    ViewBag.HTMLLoaderError = rows["Message"].ToString();
                    ViewBag.TypeView = "_ErrorGeneric";
                }
            }

            if (ViewBag.TypeView == "_CompletoGeneric") 
            {
                paramCorreo[0] = "@AUTHENTICATE";
                paramCorreo[1] = "@AGENTAPP";
                paramCorreo[2] = "@PLANTILLACORREO";
                paramCorreo[3] = "@CODIGOTRANSACCION";
                paramCorreo[4] = "@PATHFILE";

                valCorreo[0] = tokenAuth;
                valCorreo[1] = agenteAplication;
                valCorreo[2] = "Q29ycmVvQW51bGFjaW9uQmFqYUNvbmZpcm1hZGE=";
                valCorreo[3] = codigoSolicitud;
                valCorreo[4] = "";

                DataSet dataCorreo = servicioOperaciones.GetPlantillasCorreos(paramCorreo, valCorreo).Table;

                foreach (DataRow rows in dataCorreo.Tables[0].Rows) {


                    string html = rows["Html"].ToString();

                    servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                        rows["Clave"].ToString(),
                                                                        rows["Para"].ToString(),
                                                                        html.Replace("{{motivo-anulacion-baja}}", razonAnulacionBaja).ToString(),
                                                                        rows["Asunto"].ToString(),
                                                                        rows["CC"].ToString(),
                                                                        rows["CCO"].ToString(),
                                                                        "",
                                                                        rows["Importancia"].ToString(),
                                                                        null,
                                                                        null);

                    //servicioCorreo.correoTeamworkEstandarCCO(rows["De"].ToString(),
                    //                                         rows["Clave"].ToString(),
                    //                                         rows["Para"].ToString(),
                    //                                         html.Replace("{{motivo-anulacion-baja}}", razonAnulacionBaja).ToString(),
                    //                                         rows["Asunto"].ToString(),
                    //                                         rows["CC"].ToString(),
                    //                                         rows["CCO"].ToString(),
                    //                                         "",
                    //                                         rows["Importancia"].ToString(), "N");
                }
            }

            #region "URL PATH APLICATIVO"

            string prefixApp = "";
            string urlServer = "";
            string controller = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                prefixApp = "/" + Request.Url.AbsoluteUri.Split('/')[3];
                controller = Request.Url.AbsoluteUri.Split('/')[4];
            }
            else if (Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
            {
                urlServer = "181.190.6.196";
            }
            else
            {
                urlServer = Request.Url.AbsoluteUri.Split('/')[2];
                controller = Request.Url.AbsoluteUri.Split('/')[3];
            }

            #endregion

            List<Solicitud> solicitudBajas = new List<Solicitud>();

            string[] headerParamSolictudC =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@PAGINATION",
                "@HASBAJA",
                "@TYPEFILTER",
                "@DATAFILTER"
            };

            string[] headerValoresSolicitudC =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                "MS01",
                "S",
                "",
                ""
            };

            DataSet dataSolicitudesC = servicioOperaciones.GetBajasObtenerBajasConfirmadas(headerParamSolictudC, headerValoresSolicitudC).Table;

            foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Solicitud solicitud = new Solicitud();

                    solicitud.Code = rows["Code"].ToString();
                    solicitud.Message = rows["Message"].ToString();
                    solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                    solicitud.NombreProceso = rows["NombreProceso"].ToString();
                    solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();
                    solicitud.Creado = rows["Creado"].ToString();
                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
                    solicitud.Comentarios = rows["Comentarios"].ToString();
                    solicitud.CodificarCod = rows["CodificarCod"].ToString();
                    solicitud.Prioridad = rows["Prioridad"].ToString();
                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    solicitud.BorderColor = rows["BorderColor"].ToString();
                    solicitud.ColorFont = rows["ColorFont"].ToString();
                    solicitud.IntegrateSettlement = Request.Url.AbsoluteUri.Split('/')[0] + "//" + urlServer + prefixApp + "/Auth/TokenAuth?events=integratesettlement&authorizate=" + rows["CodigoSolicitud"].ToString() + "&ref=" + rows["IntegrateSettlement"].ToString();
                    solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                    solicitud.OptCalcular = rows["OptCalcular"].ToString();
                    solicitud.OptAnularKam = rows["OptAnularKam"].ToString();

                    solicitudBajas.Add(solicitud);
                }
            }

            ViewBag.RenderizadoSolicitudBajas = solicitudBajas;
            ViewBag.CodeCorrect = true;
            ViewBag.RenderizadoHasBaja = "S";

            #region "PAGINATOR DE BAJAS"

            string[] paramPaginator = new string[8];
            string[] valPaginator = new string[8];
            DataSet dataPaginator;
            List<Models.Teamwork.Pagination> paginations = new List<Models.Teamwork.Pagination>();

            paramPaginator[0] = "@AUTHENTICATE";
            paramPaginator[1] = "@AGENTAPP";
            paramPaginator[2] = "@TRABAJADOR";
            paramPaginator[3] = "@TYPEPAGINATION";
            paramPaginator[4] = "@PAGINATION";
            paramPaginator[5] = "@HASBAJA";
            paramPaginator[6] = "@TYPEFILTER";
            paramPaginator[7] = "@DATAFILTER";

            valPaginator[0] = tokenAuth;
            valPaginator[1] = agenteAplication;
            valPaginator[2] = Session["NombreUsuario"].ToString();
            valPaginator[3] = "SolicitudBaja";
            valPaginator[4] = "MS01";
            valPaginator[5] = "S";
            valPaginator[6] = "";
            valPaginator[7] = "";

            dataPaginator = servicioOperaciones.GetPaginations(paramPaginator, valPaginator).Table;

            foreach (DataRow rows in dataPaginator.Tables[0].Rows)
            {
                Models.Teamwork.Pagination paginationHttp = new Models.Teamwork.Pagination();

                paginationHttp.NumeroPagina = rows["NumeroPagina"].ToString();
                paginationHttp.Rango = rows["Rango"].ToString();
                paginationHttp.Class = rows["Class"].ToString();
                paginationHttp.Properties = rows["Properties"].ToString();
                paginationHttp.TypeFilter = rows["TypeFilter"].ToString();
                paginationHttp.Filter = rows["Filter"].ToString();
                paginationHttp.HasBaja = rows["HasBaja"].ToString();

                paginations.Add(paginationHttp);
            }

            ViewBag.RenderizadoPaginations = paginations;

            #endregion

            return PartialView("Bajas/_SolicitudBaja");
        }

        [HttpPost]
        public ActionResult AnularFiniquitoMalIngresado(string codigoSolicitud)
        {
            string view = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;
            string domainReal = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                {
                    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                }
                else
                {
                    domainReal = "181.190.6.196";
                }

                domain = "http://" + domainReal + "/";
                prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                prefixDomain = "";
            }

            #endregion

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";


            valMantFin[0] = "ANUFGI";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoSolicitud;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                ViewBag.MessagePlataforma = rows["Message"].ToString();

                ViewBag.RenderizadoSolicitudBajas = ModuleSolicitudBajas("MS01", "", "");
                ViewBag.RenderizadoPaginations = ModulePagination("SolicitudBaja", "MS01", "S", "", "");

                view = "Bajas/_SolicitudBaja";
                ViewBag.TypeView = "";

                ViewBag.CodeCorrect = true;
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AnularFiniquito(string codigoFiniquito, string observacion = "", string paginationIndex = "MS01", string onlyFiniquito = "N")
        {
            string view = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;
            string domainReal = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                {
                    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                }
                else
                {
                    domainReal = "181.190.6.196";
                }

                domain = "http://" + domainReal + "/";
                prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                prefixDomain = "";
            }

            #endregion

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";


            valMantFin[0] = "ANUF";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = observacion;
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoSuccess";
                        ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                    default:
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoError";
                        ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                }
            }

            //ViewBag.RenderizadoFiniquitos = ModuleFiniquitos("MS01", "", "", "");
            //ViewBag.RenderizadoPaginations = ModulePagination("Finiquitos", "MS01", "S", "", "");

            //ViewBag.HasFiniquitoIndividual = false;
            //ViewBag.ReferenciaPaginationIndex = "MS01";

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ModalLecturaCodigoBarra()
        {
            return PartialView("Teamwork/_CodigosBarra");
        }
        
        [HttpPost]
        public ActionResult DatosSolicitudTransferencia(string codigoFiniquito = "", string paginationIndex = "MS01", string enlaceViewCaratula = "", string onlyFiniquito = "N",
                                                        string origen = "FNQ", string codigoComplemento = "")
        {
            List<Models.Finiquitos.SolicitudTransferencia> solicitudTransferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            paramMantFin[25] = "@CODIGOHABER";
            paramMantFin[26] = "@CODIGODESCUENTO";
            paramMantFin[27] = "@ORIGEN";
            
            valMantFin[0] = "CONSDATTRANSF";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = "";
            valMantFin[25] = "";
            valMantFin[26] = "";
            valMantFin[27] = origen;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                Models.Finiquitos.SolicitudTransferencia solicitudTransferencia = new Models.Finiquitos.SolicitudTransferencia();

                solicitudTransferencia.Nombres = rows["Nombres"].ToString();
                solicitudTransferencia.Banco = rows["Banco"].ToString();
                solicitudTransferencia.Fecha = rows["Fecha"].ToString();
                solicitudTransferencia.TipoDeposito = rows["TipoDeposito"].ToString();
                solicitudTransferencia.Cuenta = rows["Cuenta"].ToString();
                solicitudTransferencia.Rut = rows["Rut"].ToString();
                solicitudTransferencia.TotalFiniquito = rows["TotalFiniquito"].ToString();

                solicitudTransferencias.Add(solicitudTransferencia);
            }

            ViewBag.RenderizadoDatosSolicitudTransferencia = solicitudTransferencias;
            ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
            ViewBag.ReferenciaPaginationIndex = paginationIndex;
            ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
            ViewBag.OrigenTEF = origen;
            ViewBag.CodigoComplemento = codigoComplemento;

            ViewBag.OnlyFiniquito = onlyFiniquito;

            return PartialView("Bajas/_SolicitudTransferencia");
        }

        [HttpPost]
        public ActionResult SolicitarTransferencia(string observacion, string paginationIndex, string gastosAdm, string location, string onlyFiniquito = "N",
                                                   string codigoFiniquito = "", string origen = "FNQ", string codigoComplemento = "", string banco = "", string numeroCta = "")
        {
            string view = string.Empty;

            try
            {
                List<Models.Finiquitos.SolicitudTransferencia> solicitudTransferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";
                paramMantFin[21] = "@GASTOADM";
                paramMantFin[22] = "@LOCATION";
                paramMantFin[23] = "@CODIGOCOMPLEMENTO";
                paramMantFin[24] = "@CONCEPTO";
                paramMantFin[25] = "@CODIGOHABER";
                paramMantFin[26] = "@CODIGODESCUENTO";
                paramMantFin[27] = "@ORIGEN";

                valMantFin[0] = "SOLTRANSF";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = banco;
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = numeroCta;
                valMantFin[11] = "";
                valMantFin[12] = observacion;
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";
                valMantFin[21] = gastosAdm;
                valMantFin[22] = location;
                valMantFin[23] = codigoComplemento;
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                    }
                }
            }
            catch (Exception EX)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirSolicitudTransferencia(string codigoPago, string paginationIndex, string origen = "FNQ")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";
                paramMantFin[21] = "@GASTOADM";
                paramMantFin[22] = "@LOCATION";
                paramMantFin[23] = "@CODIGOCOMPLEMENTO";
                paramMantFin[24] = "@CONCEPTO";
                paramMantFin[25] = "@CODIGOHABER";
                paramMantFin[26] = "@CODIGODESCUENTO";
                paramMantFin[27] = "@ORIGEN";

                valMantFin[0] = "REVSOLTEF";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = codigoPago;
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = "";
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult TipoVerificacionConfirmacionFiniquito(string codigoFiniquito, string paginationIndex, string enlaceViewCaratula, string onlyFiniquitos = "N")
        {
            string view = string.Empty;

            try
            {
                List<Models.Finiquitos.SolicitudTransferencia> transferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
                List<Models.Finiquitos.Cheque> cheques = new List<Models.Finiquitos.Cheque>();
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;
                DataSet dataTef;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "STAGECONFPAG";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            switch (rows["Pago"].ToString())
                            {
                                case "TEF":
                                    view = "Bajas/_PaymentVerifyTypeTEF";
                                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                                    ViewBag.ReferenciaPaginationIndex = paginationIndex;

                                    valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                    valMantFin[1] = Session["base64Username"].ToString();
                                    valMantFin[2] = codigoFiniquito;
                                    valMantFin[3] = rows["Pago"].ToString();
                                    valMantFin[4] = "";
                                    valMantFin[5] = "";
                                    valMantFin[6] = "";
                                    valMantFin[7] = "";
                                    valMantFin[8] = "";
                                    valMantFin[9] = "";
                                    valMantFin[10] = "";
                                    valMantFin[11] = "";
                                    valMantFin[12] = "";
                                    valMantFin[13] = "";
                                    valMantFin[14] = "";
                                    valMantFin[15] = "";
                                    valMantFin[16] = "";
                                    valMantFin[17] = "";
                                    valMantFin[18] = "";
                                    valMantFin[19] = "";
                                    valMantFin[20] = "";

                                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                    foreach (DataRow rowsTef in dataMantencion.Tables[0].Rows)
                                    {
                                        Models.Finiquitos.SolicitudTransferencia transferencia = new Models.Finiquitos.SolicitudTransferencia();

                                        transferencia.Nombres = rowsTef["Nombres"].ToString();
                                        transferencia.Banco = rowsTef["Banco"].ToString();
                                        transferencia.Fecha = rowsTef["Fecha"].ToString();
                                        transferencia.Cuenta = rowsTef["Cuenta"].ToString();
                                        transferencia.Rut = rowsTef["Rut"].ToString();
                                        transferencia.TotalFiniquito = rowsTef["MontoFiniquito"].ToString();
                                        transferencia.GastoAdministrativo = rowsTef["MontoAdm"].ToString();
                                        transferencia.MontoTotal = rowsTef["MontoTotal"].ToString();
                                        transferencia.Location = rowsTef["Location"].ToString();
                                        transferencia.Observacion = rowsTef["Observacion"].ToString();
                                        transferencia.CodigoPago = rowsTef["CodigoPago"].ToString();

                                        transferencias.Add(transferencia);
                                    }

                                    ViewBag.RenderizadoTransferenciaDatos = transferencias;
                                    ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                                    ViewBag.OnlyFiniquito = onlyFiniquitos;

                                    break;
                                case "CHQ":
                                    view = "Bajas/_PaymentVerifyTypeCHQ";
                                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                                    ViewBag.ReferenciaPaginationIndex = paginationIndex;

                                    valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                    valMantFin[1] = Session["base64Username"].ToString();
                                    valMantFin[2] = codigoFiniquito;
                                    valMantFin[3] = rows["Pago"].ToString();
                                    valMantFin[4] = "";
                                    valMantFin[5] = "";
                                    valMantFin[6] = "";
                                    valMantFin[7] = "";
                                    valMantFin[8] = "";
                                    valMantFin[9] = "";
                                    valMantFin[10] = "";
                                    valMantFin[11] = "";
                                    valMantFin[12] = "";
                                    valMantFin[13] = "";
                                    valMantFin[14] = "";
                                    valMantFin[15] = "";
                                    valMantFin[16] = "";
                                    valMantFin[17] = "";
                                    valMantFin[18] = "";
                                    valMantFin[19] = "";
                                    valMantFin[20] = "";

                                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                    foreach (DataRow rowsChq in dataMantencion.Tables[0].Rows)
                                    {
                                        Models.Finiquitos.Cheque cheque = new Models.Finiquitos.Cheque();

                                        cheque.CodigoPago = rowsChq["CodigoPago"].ToString();
                                        cheque.Nombres = rowsChq["Nombres"].ToString();
                                        cheque.Rut = rowsChq["Rut"].ToString();
                                        cheque.MontoTotal = rowsChq["TotalFiniquito"].ToString();

                                        cheques.Add(cheque);
                                    }

                                    ViewBag.RenderizadoChequesDatos = cheques;
                                    ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                                    ViewBag.OnlyFiniquito = onlyFiniquitos;

                                    break;
                            }
                            break;
                        default:
                            view = "Bajas/_PaymentConfirmType";
                            ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                            ViewBag.OnlyFiniquito = onlyFiniquitos;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }
        
        [HttpPost]
        public ActionResult VerificacionPagoFiniquito(string codigoFiniquito, string paginationIndex, string codigoPago, string tipoPago, string onlyFiniquito)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "VERCALFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                    }
                }

            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConsultarTipoConfirmacion(string codigoFiniquito = "", string paginationIndex = "MS01", string enlaceDownloadCaratula = "", string enlaceViewCaratula = "", 
                                                      string origen = "FNQ", string codigoComplemento = "")
        {
            string view = string.Empty;

            try
            {
                List<Models.Finiquitos.SolicitudTransferencia> transferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
                List<Models.Finiquitos.Cheque> cheques = new List<Models.Finiquitos.Cheque>();
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";
                paramMantFin[21] = "@GASTOADM";
                paramMantFin[22] = "@LOCATION";
                paramMantFin[23] = "@CODIGOCOMPLEMENTO";
                paramMantFin[24] = "@CONCEPTO";
                paramMantFin[25] = "@CODIGOHABER";
                paramMantFin[26] = "@CODIGODESCUENTO";
                paramMantFin[27] = "@ORIGEN";

                valMantFin[0] = "CONSPAGALMACENADO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = codigoComplemento;
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            switch (rows["Pago"].ToString())
                            {
                                case "TEF":
                                    view = "Bajas/_PaymentConfirmTypeTEF";
                                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                                    ViewBag.ReferenciaPaginationIndex = paginationIndex;

                                    valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                    valMantFin[1] = Session["base64Username"].ToString();
                                    valMantFin[2] = codigoFiniquito;
                                    valMantFin[3] = rows["Pago"].ToString();
                                    valMantFin[4] = "";
                                    valMantFin[5] = "";
                                    valMantFin[6] = "";
                                    valMantFin[7] = "";
                                    valMantFin[8] = "";
                                    valMantFin[9] = "";
                                    valMantFin[10] = "";
                                    valMantFin[11] = "";
                                    valMantFin[12] = "";
                                    valMantFin[13] = "";
                                    valMantFin[14] = "";
                                    valMantFin[15] = "";
                                    valMantFin[16] = "";
                                    valMantFin[17] = "";
                                    valMantFin[18] = "";
                                    valMantFin[19] = "";
                                    valMantFin[20] = "";
                                    valMantFin[21] = "";
                                    valMantFin[22] = "";
                                    valMantFin[23] = codigoComplemento;
                                    valMantFin[24] = "";
                                    valMantFin[25] = "";
                                    valMantFin[26] = "";
                                    valMantFin[27] = origen;

                                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                    foreach (DataRow rowsTef in dataMantencion.Tables[0].Rows)
                                    {
                                        Models.Finiquitos.SolicitudTransferencia transferencia = new Models.Finiquitos.SolicitudTransferencia();

                                        transferencia.Nombres = rowsTef["Nombres"].ToString();
                                        transferencia.Banco = rowsTef["Banco"].ToString();
                                        transferencia.Fecha = rowsTef["Fecha"].ToString();
                                        transferencia.Cuenta = rowsTef["Cuenta"].ToString();
                                        transferencia.Rut = rowsTef["Rut"].ToString();
                                        transferencia.TotalFiniquito = rowsTef["MontoFiniquito"].ToString();
                                        transferencia.GastoAdministrativo = rowsTef["MontoAdm"].ToString();
                                        transferencia.MontoTotal = rowsTef["MontoTotal"].ToString();
                                        transferencia.Location = rowsTef["Location"].ToString();
                                        transferencia.Observacion = rowsTef["Observacion"].ToString();
                                        transferencia.CodigoPago = rowsTef["CodigoPago"].ToString();

                                        transferencias.Add(transferencia);
                                    }

                                    ViewBag.RenderizadoTransferenciaDatos = transferencias;
                                    ViewBag.ReferenciaDownloadCaratula = enlaceDownloadCaratula;
                                    ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                                    ViewBag.CodigoComplemento = codigoComplemento;
                                    ViewBag.Origen = origen;

                                    break;
                                case "CHQ":
                                    view = "Bajas/_PaymentConfirmTypeCHQ";
                                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                                    ViewBag.ReferenciaPaginationIndex = paginationIndex;

                                    valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                    valMantFin[1] = Session["base64Username"].ToString();
                                    valMantFin[2] = codigoFiniquito;
                                    valMantFin[3] = rows["Pago"].ToString();
                                    valMantFin[4] = "";
                                    valMantFin[5] = "";
                                    valMantFin[6] = "";
                                    valMantFin[7] = "";
                                    valMantFin[8] = "";
                                    valMantFin[9] = "";
                                    valMantFin[10] = "";
                                    valMantFin[11] = "";
                                    valMantFin[12] = "";
                                    valMantFin[13] = "";
                                    valMantFin[14] = "";
                                    valMantFin[15] = "";
                                    valMantFin[16] = "";
                                    valMantFin[17] = "";
                                    valMantFin[18] = "";
                                    valMantFin[19] = "";
                                    valMantFin[20] = "";
                                    valMantFin[21] = "";
                                    valMantFin[22] = "";
                                    valMantFin[23] = codigoComplemento;
                                    valMantFin[24] = "";
                                    valMantFin[25] = "";
                                    valMantFin[26] = "";
                                    valMantFin[27] = origen;

                                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                    foreach (DataRow rowsChq in dataMantencion.Tables[0].Rows)
                                    {
                                        Models.Finiquitos.Cheque cheque = new Models.Finiquitos.Cheque();

                                        cheque.CodigoPago = rowsChq["CodigoPago"].ToString();
                                        cheque.Nombres = rowsChq["Nombres"].ToString();
                                        cheque.Rut = rowsChq["Rut"].ToString();
                                        cheque.MontoTotal = rowsChq["TotalFiniquito"].ToString();

                                        cheques.Add(cheque);
                                    }
                                    
                                    ViewBag.RenderizadoChequesDatos = cheques;
                                    ViewBag.ReferenciaDownloadCaratula = enlaceDownloadCaratula;
                                    ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                                    ViewBag.CodigoComplemento = codigoComplemento;
                                    ViewBag.Origen = origen;

                                    break;
                            }
                            break;
                        default:
                            view = "Bajas/_PaymentConfirmType";
                            ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            ViewBag.ReferenciaDownloadCaratula = enlaceDownloadCaratula;
                            ViewBag.ReferenciaViewCaratula = enlaceViewCaratula;
                            ViewBag.CodigoComplemento = codigoComplemento;
                            ViewBag.Origen = origen;
                            break;
                    }
                }

            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConfirmacionPagoFiniquito(string codigoPago, string codigoFiniquito = "", string tipoPago = "", string paginationIndex = "MS01", string glosaPago = "", string montoAsociado = "", 
                                                      string montoActual = "", string origen = "FNQ", string codigoComplemento = "")
        {
            string view = string.Empty;

            try
            {
                string monto = string.Empty;

                int montoA = 0;
                int montoN = 0;

                if (montoActual.Replace(".", "").Replace("$ ", "") != "")
                {
                    montoA = Convert.ToInt32(montoActual.Replace(".", "").Replace("$ ", ""));
                }
                else
                {
                    montoA = 0;
                }

                if (montoAsociado.Replace(".", "").Replace("$ ", "") != "")
                {
                    montoN = Convert.ToInt32(montoAsociado.Replace(".", "").Replace("$ ", ""));
                }
                else
                {
                    montoN = 0;
                }

                if (montoA != montoN && montoN > 0)
                {
                    monto = montoN.ToString();
                }
                else
                {
                    monto = montoA.ToString();
                }

                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";
                paramMantFin[21] = "@GASTOADM";
                paramMantFin[22] = "@LOCATION";
                paramMantFin[23] = "@CODIGOCOMPLEMENTO";
                paramMantFin[24] = "@CONCEPTO";
                paramMantFin[25] = "@CODIGOHABER";
                paramMantFin[26] = "@CODIGODESCUENTO";
                paramMantFin[27] = "@ORIGEN";

                valMantFin[0] = "CONFCALFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = glosaPago;
                valMantFin[13] = monto;
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = codigoComplemento;
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirVerificacionPago(string codigoPago, string tipoPago, string codigoFiniquito, string paginationIndex)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "REVVERPAG";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirConfirmacionPago(string codigoPago, string tipoPago, string codigoFiniquito, string paginationIndex, string observacion = "", string onlyFiniquito = "N")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "LIBCONFCALFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = observacion;
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();

                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReaderBarcodeInit()
        {
            string view = string.Empty;

            try
            {
                view = "Bajas/_ReaderBarcodeInit";
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReaderBarcodeStage(string readerBarcode)
        {
            string view = string.Empty;

            try
            {
                List<Models.Finiquitos.SolicitudTransferencia> solicitudTransferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
                List<Models.Finiquitos.SolicitudTransferencia> transferencias = new List<Models.Finiquitos.SolicitudTransferencia>();
                List<Models.Finiquitos.Cheque> cheques = new List<Models.Finiquitos.Cheque>();
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "BARCODEREADER";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = readerBarcode;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["NextStage"].ToString())
                    {
                        case "VER":
                            DataSet dataTef;
                            
                            valMantFin[0] = "STAGECONFPAG";
                            valMantFin[1] = Session["base64Username"].ToString();
                            valMantFin[2] = rows["CodigoFiniquito"].ToString();
                            valMantFin[3] = "";
                            valMantFin[4] = "";
                            valMantFin[5] = "";
                            valMantFin[6] = "";
                            valMantFin[7] = "";
                            valMantFin[8] = "";
                            valMantFin[9] = "";
                            valMantFin[10] = "";
                            valMantFin[11] = "";
                            valMantFin[12] = "";
                            valMantFin[13] = "";
                            valMantFin[14] = "";
                            valMantFin[15] = "";
                            valMantFin[16] = "";
                            valMantFin[17] = "";
                            valMantFin[18] = "";
                            valMantFin[19] = "";
                            valMantFin[20] = "";

                            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                            foreach (DataRow rowsVal in dataMantencion.Tables[0].Rows)
                            {
                                switch (rowsVal["Code"].ToString())
                                {
                                    case "200":
                                        ViewBag.ReferenciaPaginationIndex = "MS01";
                                        ViewBag.ReferenciaCodigoFiniquito = rows["CodigoFiniquito"].ToString();
                                        view = "Bajas/_PaymentTypeSelected";
                                        break;
                                    default:
                                        ViewBag.ReferenciaPaginationIndex = "MS01";
                                        ViewBag.ReferenciaCodigoFiniquito = rows["CodigoFiniquito"].ToString();
                                        view = "Bajas/_PaymentTefType";

                                        valMantFin[0] = "TEFALMACENADA";
                                        valMantFin[1] = Session["base64Username"].ToString();
                                        valMantFin[2] = rows["CodigoFiniquito"].ToString();
                                        valMantFin[3] = "";
                                        valMantFin[4] = "";
                                        valMantFin[5] = "";
                                        valMantFin[6] = "";
                                        valMantFin[7] = "";
                                        valMantFin[8] = "";
                                        valMantFin[9] = "";
                                        valMantFin[10] = "";
                                        valMantFin[11] = "";
                                        valMantFin[12] = "";
                                        valMantFin[13] = "";
                                        valMantFin[14] = "";
                                        valMantFin[15] = "";
                                        valMantFin[16] = "";
                                        valMantFin[17] = "";
                                        valMantFin[18] = "";
                                        valMantFin[19] = "";
                                        valMantFin[20] = "";

                                        dataTef = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                        foreach (DataRow rowsTef in dataTef.Tables[0].Rows)
                                        {
                                            Models.Finiquitos.SolicitudTransferencia solicitudTransferencia = new Models.Finiquitos.SolicitudTransferencia();

                                            solicitudTransferencia.Nombres = rowsTef["Nombres"].ToString();
                                            solicitudTransferencia.Banco = rowsTef["Banco"].ToString();
                                            solicitudTransferencia.Fecha = rowsTef["Fecha"].ToString();
                                            solicitudTransferencia.Cuenta = rowsTef["Cuenta"].ToString();
                                            solicitudTransferencia.Rut = rowsTef["Rut"].ToString();
                                            solicitudTransferencia.TotalFiniquito = rowsTef["TotalFiniquito"].ToString();
                                            solicitudTransferencia.Observacion = rowsTef["Observacion"].ToString();
                                            solicitudTransferencia.CodigoPago = rowsTef["CodigoPago"].ToString();

                                            solicitudTransferencias.Add(solicitudTransferencia);
                                        }

                                        ViewBag.RenderizadoDatosSolicitudTransferencia = solicitudTransferencias;


                                        break;
                                }
                            }
                            break;
                        case "CONF":

                            valMantFin[0] = "CONSPAGALMACENADO";
                            valMantFin[1] = Session["base64Username"].ToString();
                            valMantFin[2] = rows["CodigoFiniquito"].ToString();
                            valMantFin[3] = "";
                            valMantFin[4] = "";
                            valMantFin[5] = "";
                            valMantFin[6] = "";
                            valMantFin[7] = "";
                            valMantFin[8] = "";
                            valMantFin[9] = "";
                            valMantFin[10] = "";
                            valMantFin[11] = "";
                            valMantFin[12] = "";
                            valMantFin[13] = "";
                            valMantFin[14] = "";
                            valMantFin[15] = "";
                            valMantFin[16] = "";
                            valMantFin[17] = "";
                            valMantFin[18] = "";
                            valMantFin[19] = "";
                            valMantFin[20] = "";

                            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                            foreach (DataRow rowsConf in dataMantencion.Tables[0].Rows)
                            {
                                switch (rowsConf["Code"].ToString())
                                {
                                    case "200":
                                        switch (rowsConf["Pago"].ToString())
                                        {
                                            case "TEF":
                                                view = "Bajas/_PaymentConfirmTypeTEF";
                                                ViewBag.ReferenciaCodigoFiniquito = rows["CodigoFiniquito"].ToString();
                                                ViewBag.ReferenciaPaginationIndex = "MS01";

                                                valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                                valMantFin[1] = Session["base64Username"].ToString();
                                                valMantFin[2] = rows["CodigoFiniquito"].ToString();
                                                valMantFin[3] = rowsConf["Pago"].ToString();
                                                valMantFin[4] = "";
                                                valMantFin[5] = "";
                                                valMantFin[6] = "";
                                                valMantFin[7] = "";
                                                valMantFin[8] = "";
                                                valMantFin[9] = "";
                                                valMantFin[10] = "";
                                                valMantFin[11] = "";
                                                valMantFin[12] = "";
                                                valMantFin[13] = "";
                                                valMantFin[14] = "";
                                                valMantFin[15] = "";
                                                valMantFin[16] = "";
                                                valMantFin[17] = "";
                                                valMantFin[18] = "";
                                                valMantFin[19] = "";
                                                valMantFin[20] = "";

                                                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                                foreach (DataRow rowsTef in dataMantencion.Tables[0].Rows)
                                                {
                                                    Models.Finiquitos.SolicitudTransferencia transferencia = new Models.Finiquitos.SolicitudTransferencia();

                                                    transferencia.Nombres = rowsTef["Nombres"].ToString();
                                                    transferencia.Banco = rowsTef["Banco"].ToString();
                                                    transferencia.Fecha = rowsTef["Fecha"].ToString();
                                                    transferencia.Cuenta = rowsTef["Cuenta"].ToString();
                                                    transferencia.Rut = rowsTef["Rut"].ToString();
                                                    transferencia.TotalFiniquito = rowsTef["MontoFiniquito"].ToString();
                                                    transferencia.GastoAdministrativo = rowsTef["MontoAdm"].ToString();
                                                    transferencia.MontoTotal = rowsTef["MontoTotal"].ToString();
                                                    transferencia.Location = rowsTef["Location"].ToString();
                                                    transferencia.Observacion = rowsTef["Observacion"].ToString();
                                                    transferencia.CodigoPago = rowsTef["CodigoPago"].ToString();

                                                    transferencias.Add(transferencia);
                                                }

                                                ViewBag.RenderizadoTransferenciaDatos = transferencias;
                                                ViewBag.ReferenciaViewCaratula = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString();

                                                break;
                                            case "CHQ":
                                                view = "Bajas/_PaymentConfirmTypeCHQ";
                                                ViewBag.ReferenciaCodigoFiniquito = rows["CodigoFiniquito"].ToString();
                                                ViewBag.ReferenciaPaginationIndex = "MS01";

                                                valMantFin[0] = "CONSPAGDATOSALMACENADO";
                                                valMantFin[1] = Session["base64Username"].ToString();
                                                valMantFin[2] = rows["CodigoFiniquito"].ToString();
                                                valMantFin[3] = rowsConf["Pago"].ToString();
                                                valMantFin[4] = "";
                                                valMantFin[5] = "";
                                                valMantFin[6] = "";
                                                valMantFin[7] = "";
                                                valMantFin[8] = "";
                                                valMantFin[9] = "";
                                                valMantFin[10] = "";
                                                valMantFin[11] = "";
                                                valMantFin[12] = "";
                                                valMantFin[13] = "";
                                                valMantFin[14] = "";
                                                valMantFin[15] = "";
                                                valMantFin[16] = "";
                                                valMantFin[17] = "";
                                                valMantFin[18] = "";
                                                valMantFin[19] = "";
                                                valMantFin[20] = "";

                                                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                                                foreach (DataRow rowsChq in dataMantencion.Tables[0].Rows)
                                                {
                                                    Models.Finiquitos.Cheque cheque = new Models.Finiquitos.Cheque();

                                                    cheque.CodigoPago = rowsChq["CodigoPago"].ToString();
                                                    cheque.Nombres = rowsChq["Nombres"].ToString();
                                                    cheque.Rut = rowsChq["Rut"].ToString();
                                                    cheque.MontoTotal = rowsChq["TotalFiniquito"].ToString();

                                                    cheques.Add(cheque);
                                                }

                                                ViewBag.RenderizadoChequesDatos = cheques;
                                                ViewBag.ReferenciaViewCaratula =
                                                ViewBag.ReferenciaViewCaratula = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString();

                                                break;
                                        }
                                        break;
                                    default:
                                        view = "Bajas/_PaymentConfirmType";
                                        ViewBag.ReferenciaCodigoFiniquito = rows["CodigoFiniquito"].ToString();
                                        ViewBag.ReferenciaPaginationIndex = "MS01";
                                        ViewBag.ReferenciaViewCaratula = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString(); 
                                        break;
                                }
                            }
                            break;
                        default:

                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult EnviarProcesoPago(string codigoFiniquito, string paginationIndex, string onlyFiniquito = "N")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "SENDPPAGO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);

                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            
                            
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirEnviarProcesoPago(string codigoFiniquito, string paginationIndex, string onlyFiniquito = "N", string observacion = "")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "REVSENDPPAGO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = observacion;
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AnularPago(string codigoFiniquito, string paginationIndex, string observacion = "", string onlyFiniquito = "N")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "ANUP";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = observacion;
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoSuccess";
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            
                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                        default:
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            view = "Bajas/_InfoError";
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                            ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                ViewBag.ReferenciaPaginationIndex = paginationIndex;
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult MoverFiniquito(string codigoFiniquito, string onlyFiniquito = "S")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet dataMantencion;

                paramMantFin[0] = "@ACCION";
                paramMantFin[1] = "@USUARIOCREADOR";
                paramMantFin[2] = "@CODIGOFINIQUITO";
                paramMantFin[3] = "@METODOPAGO";
                paramMantFin[4] = "@CODIGOPAGO";
                paramMantFin[5] = "@CODIGOPROCESO";
                paramMantFin[6] = "@DESTINATARIO";
                paramMantFin[7] = "@BANCO";
                paramMantFin[8] = "@TIPOCUENTA";
                paramMantFin[9] = "@FECHA";
                paramMantFin[10] = "@NUMEROCUENTA";
                paramMantFin[11] = "@RUT";
                paramMantFin[12] = "@OBSERVACIONES";
                paramMantFin[13] = "@MONTO";
                paramMantFin[14] = "@VIGENTE";
                paramMantFin[15] = "@SECUENCIA";
                paramMantFin[16] = "@SERIECHQ";
                paramMantFin[17] = "@EMPRESAORIGEN";
                paramMantFin[18] = "@PAGINATION";
                paramMantFin[19] = "@TYPEFILTER";
                paramMantFin[20] = "@DATAFILTER";

                valMantFin[0] = "MOVSIMFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = codigoFiniquito;
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaRedireccionamiento = ModuleControlRetornoPath() + "/" + rows["redir"].ToString();
                            ViewBag.RenderizadoPaginations = ModulePagination("Simulacion", "MS01", "S", "", "");
                            ViewBag.RenderizadoFiniquitos = ModuleSimulacion("MS01", "Simulacion", "", "");
                            ViewBag.HasFiniquitoIndividual = false;
                            ViewBag.CodeCorrect = true;
                            ViewBag.HTMLLoaderError = "";
                            view = "Bajas/_Simulacion";
                            break;
                        default:
                            
                            break;
                    }
                }
            }
            catch (Exception)
            {
                ViewBag.ReferenciaHtmlError = "Ha ocurrido un problema inesperado al momento de intentar solicitar la transferencia, intentelo nuevamente o levante un ticket por mesa de ayuda si el problema persiste.";
                
                view = "Bajas/_InfoError";
            }

            return PartialView(view);
        }
        
        [HttpPost]
        public ActionResult ListadoSimulaciones(string pagination, string codigoFiniquito, string typeFilter = "", string dataFilter = "")
        {
            ViewBag.RenderizadoDashboard = null;
            ViewBag.HasFiniquitoIndividual = false;
            ViewBag.RenderizadoFiniquitos = ModuleSimulacion(pagination, "", typeFilter, dataFilter);
            ViewBag.RenderizadoPaginations = ModulePagination("Simulacion", pagination, "S", typeFilter, dataFilter);
            ViewBag.ReferenciaRedireccionamiento = "";
            ViewBag.HTMLLoaderError = "";

            return PartialView("Bajas/_Simulacion");
        }

        [HttpPost]
        public ActionResult ConfirmacionEvento(string option, string pagination = "MS01", string codigoFiniquito = "", string typeFilter = "", string dataFilter = "", 
                                               string onlyFiniquito = "N", string gastosAdm = "", string observacion = "", string montofiniquito = "", string location = "",
                                               string tipoPago = "", string codigoPago = "", string secuencia = "", string codigoComplemento = "", string origen = "FNQ", string banco = "",
                                               string numeroCta = "")
        {
            List<LiquidacionSueldo> liquidacionSueldos = new List<LiquidacionSueldo>();
            List<LiquidacionSueldoFile> liquidacionSueldosFile = new List<LiquidacionSueldoFile>();
            string view = string.Empty;
            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet dataMantencion;
            DataSet dataLiquidacionSueldosFile;
            
            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            paramMantFin[25] = "@CODIGOHABER";
            paramMantFin[26] = "@CODIGODESCUENTO";
            paramMantFin[27] = "@ORIGEN";


            switch (option)
            {
                case "SolicitudTransferencia":

                    valMantFin[0] = "CIFRAMONTOS";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = codigoFiniquito;
                    valMantFin[3] = "";
                    valMantFin[4] = "";
                    valMantFin[5] = "";
                    valMantFin[6] = "";
                    valMantFin[7] = "";
                    valMantFin[8] = "";
                    valMantFin[9] = "";
                    valMantFin[10] = "";
                    valMantFin[11] = "";
                    valMantFin[12] = "";
                    valMantFin[13] = montofiniquito;
                    valMantFin[14] = "";
                    valMantFin[15] = "";
                    valMantFin[16] = "";
                    valMantFin[17] = "";
                    valMantFin[18] = "";
                    valMantFin[19] = "";
                    valMantFin[20] = "";
                    valMantFin[21] = gastosAdm;
                    valMantFin[22] = "";
                    valMantFin[23] = codigoComplemento;
                    valMantFin[24] = "";
                    valMantFin[25] = "";
                    valMantFin[26] = "";
                    valMantFin[27] = origen;


                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                    foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                    {
                        ViewBag.MontoTotalFiniquito = rows["MontoFiniquito"].ToString();
                        ViewBag.MontoGastoAdministrativo = rows["MontoGastosAdm"].ToString();
                        ViewBag.MontoTotalPago = rows["MontoTotalPago"].ToString();
                    }

                    ViewBag.MontoGastoAdm = gastosAdm;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    ViewBag.ObservacionesSolicitud = observacion;
                    ViewBag.LocationTEF = location;
                    ViewBag.OrigenTEF = origen;
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.NumeroCuenta = numeroCta;
                    ViewBag.Banco = banco;

                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "VerificacionPagoFiniquito":
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.TipoPago = tipoPago;
                    ViewBag.TotalFiniquito = montofiniquito;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "LiquidacionSueldos":
                    valMantFin[0] = "MESESUNIQUELIQSUELDO";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = codigoFiniquito;
                    valMantFin[3] = "";
                    valMantFin[4] = "";
                    valMantFin[5] = "";
                    valMantFin[6] = "";
                    valMantFin[7] = "";
                    valMantFin[8] = "";
                    valMantFin[9] = "";
                    valMantFin[10] = "";
                    valMantFin[11] = "";
                    valMantFin[12] = "";
                    valMantFin[13] = "";
                    valMantFin[14] = "";
                    valMantFin[15] = secuencia;
                    valMantFin[16] = "";
                    valMantFin[17] = "";
                    valMantFin[18] = "";
                    valMantFin[19] = "";
                    valMantFin[20] = "";
                    valMantFin[21] = "";

                    dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                    foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                    {
                        LiquidacionSueldo liquidacionSueldo = new LiquidacionSueldo();
                        liquidacionSueldosFile = new List<LiquidacionSueldoFile>();

                        liquidacionSueldo.Years = rows["Years"].ToString();
                        valMantFin[0] = "LIQUIDACIONESDESUELDO";
                        valMantFin[1] = Session["base64Username"].ToString();
                        valMantFin[2] = codigoFiniquito;
                        valMantFin[3] = "";
                        valMantFin[4] = "";
                        valMantFin[5] = "";
                        valMantFin[6] = "";
                        valMantFin[7] = "";
                        valMantFin[8] = "";
                        valMantFin[9] = "";
                        valMantFin[10] = "";
                        valMantFin[11] = "";
                        valMantFin[12] = "";
                        valMantFin[13] = "";
                        valMantFin[14] = "";
                        valMantFin[15] = secuencia;
                        valMantFin[16] = "";
                        valMantFin[17] = "";
                        valMantFin[18] = "";
                        valMantFin[19] = "";
                        valMantFin[20] = rows["Years"].ToString();
                        valMantFin[21] = "";

                        dataLiquidacionSueldosFile = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                        foreach (DataRow rowsLQS in dataLiquidacionSueldosFile.Tables[0].Rows)
                        {
                            LiquidacionSueldoFile liquidacionSueldoFile = new LiquidacionSueldoFile();

                            liquidacionSueldoFile.FechaMes = rowsLQS["FechaMes"].ToString();
                            liquidacionSueldoFile.ViewLiquidacionSueldo = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=TGlxdWlkYWNpb25TdWVsZG8=&data=" + rowsLQS["IdArchivo"].ToString() + "&code=" + codigoFiniquito + "&type=" + secuencia;

                            liquidacionSueldosFile.Add(liquidacionSueldoFile);
                        }

                        liquidacionSueldo.LiquidacionesSueldo = liquidacionSueldosFile;

                        liquidacionSueldos.Add(liquidacionSueldo);
                    }

                    ViewBag.RenderizadoLiquidacionesSueldo = liquidacionSueldos;
                    ViewBag.OptionConfirmacionEvent = option;

                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "RevertirConfirmacionPago":
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.TipoPago = tipoPago;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "AnularFiniquito":
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "PagarFiniquito":
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.TipoPago = tipoPago;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "NotariarFiniquito":
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.TipoPago = tipoPago;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "CrearComplemento":
                    ViewBag.ReferenciaCodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "AnularComplemento":
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                case "AnularPago":
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    ViewBag.OptionConfirmacionEvent = option;
                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
                default:

                    ViewBag.OnlyFiniquito = onlyFiniquito;
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    ViewBag.ReferenciaPaginationIndex = pagination;
                    ViewBag.OptionConfirmacionEvent = option;
                    view = "Bajas/_ConfirmacionEvento";
                    break;
            }

            return PartialView(view);
        }
        
        [HttpPost]
        public ActionResult PagarFiniquito(string tipoPago, string codigoPago, string codigoFiniquito, string paginationIndex = "MS01", string onlyFiniquito = "S")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";

            switch (tipoPago)
            {
                case "TEF":
                    valMantFin[0] = "PAGTEF";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = "";
                    valMantFin[3] = "";
                    valMantFin[4] = codigoPago;
                    valMantFin[5] = "";
                    valMantFin[6] = "";
                    valMantFin[7] = "";
                    valMantFin[8] = "";
                    valMantFin[9] = "";
                    valMantFin[10] = "";
                    valMantFin[11] = "";
                    valMantFin[12] = "";
                    valMantFin[13] = "";
                    valMantFin[14] = "";
                    valMantFin[15] = "";
                    valMantFin[16] = "";
                    valMantFin[17] = "";
                    valMantFin[18] = "";
                    valMantFin[19] = "";
                    valMantFin[20] = "";
                    break;
                case "CHQ":
                    valMantFin[0] = "PAGFIN";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = codigoFiniquito;
                    valMantFin[3] = "";
                    valMantFin[4] = "";
                    valMantFin[5] = "";
                    valMantFin[6] = "";
                    valMantFin[7] = "";
                    valMantFin[8] = "";
                    valMantFin[9] = "";
                    valMantFin[10] = "";
                    valMantFin[11] = "";
                    valMantFin[12] = "";
                    valMantFin[13] = "";
                    valMantFin[14] = "";
                    valMantFin[15] = "";
                    valMantFin[16] = "";
                    valMantFin[17] = "";
                    valMantFin[18] = "";
                    valMantFin[19] = "";
                    valMantFin[20] = "";
                    break;
            }
            

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoSuccess";
                        ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                    default:
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoError";
                        ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult CrearComplemento(string codigoFiniquito)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";

            valMantFin[0] = "CMPADD";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                view = "Bajas/_AgregarHaberComplemento";
                ViewBag.CodigoComplemento = rows["CodigoComplemento"].ToString();
                ViewBag.CodigoFiniquito = codigoFiniquito;
                
                ViewBag.RenderizadoHaberesComplemento = ModuleHaberesComplementos(rows["CodigoComplemento"].ToString());
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AgregarHaberComplemento(string codigoFiniquito, string codigoComplemento, string concepto, string monto)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[25];
            string[] valMantFin = new string[25];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            
            valMantFin[0] = "ADDHABERCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = monto;
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = concepto;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                view = "Bajas/_ListaHaberesComplemento";
                ViewBag.CodigoComplemento = codigoComplemento;
                ViewBag.CodigoFiniquito = codigoFiniquito;

                ViewBag.RenderizadoHaberesComplemento = ModuleHaberesComplementos(codigoComplemento);
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult EliminarHaberComplemento(string codigoHaber, string codigoComplemento, string codigoFiniquito)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[26];
            string[] valMantFin = new string[26];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            paramMantFin[25] = "@CODIGOHABER";

            valMantFin[0] = "DELHABERCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = "";
            valMantFin[25] = codigoHaber;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                view = "Bajas/_ListaHaberesComplemento";
                ViewBag.CodigoComplemento = codigoComplemento;
                ViewBag.CodigoFiniquito = codigoFiniquito;

                ViewBag.RenderizadoHaberesComplemento = ModuleHaberesComplementos(codigoComplemento);
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AgregarDescuentoComplemento(string codigoFiniquito, string codigoComplemento, string concepto, string monto)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[25];
            string[] valMantFin = new string[25];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";

            valMantFin[0] = "ADDDESCCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = monto;
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = concepto;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                view = "Bajas/_ListaDescuentosComplemento";
                ViewBag.CodigoComplemento = codigoComplemento;
                ViewBag.CodigoFiniquito = codigoFiniquito;

                ViewBag.RenderizadoDescuentosComplemento = ModuleDescuentoComplementos(codigoComplemento);
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult EliminarDescuentoComplemento(string codigoDescuento, string codigoComplemento, string codigoFiniquito)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[27];
            string[] valMantFin = new string[27];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            paramMantFin[25] = "@CODIGOHABER";
            paramMantFin[26] = "@CODIGODESCUENTO";

            valMantFin[0] = "DELDESCCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = "";
            valMantFin[25] = "";
            valMantFin[26] = codigoDescuento;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                view = "Bajas/_ListaDescuentosComplemento";
                ViewBag.CodigoComplemento = codigoComplemento;
                ViewBag.CodigoFiniquito = codigoFiniquito;

                ViewBag.RenderizadoDescuentosComplemento = ModuleDescuentoComplementos(codigoComplemento);
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ActivarDocumentoComplemento(string codigoComplemento, string codigoFiniquito, string fecha = "")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[24];
            string[] valMantFin = new string[24];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";

            valMantFin[0] = "ACTIVECMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = fecha;
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        view = "Bajas/_EventoCompletadoCreacionDoc";
                        ViewBag.OptionCreacionDocumento = "200";
                        ViewBag.RedireccionamientoDocumento = ModuleControlRetornoPath() + "/Bajas/Finiquitos/" + codigoFiniquito + "/Complementos/" + codigoComplemento;
                        break;
                    case "404":
                        view = "Bajas/_EventoCompletadoCreacionDoc";
                        ViewBag.OptionCreacionDocumento = "404";
                        ViewBag.MessageEvento = rows["Message"].ToString();
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ContinuarEtapa(string option, string codigoComplemento, string codigoFiniquito, string fecha = "")
        {
            string view = string.Empty;

            switch (option)
            {
                case "AgregarDescuentos":
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    ViewBag.RenderizadoDescuentosComplemento = ModuleDescuentoComplementos(codigoComplemento);
                    view = "Bajas/_AgregarDescuentoComplemento";
                    break;
                case "AgregarHaberes":
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    ViewBag.RenderizadoHaberesComplemento = ModuleHaberesComplementos(codigoComplemento);
                    view = "Bajas/_AgregarHaberComplemento";
                    break;
                case "AgregarFechaDocumento":
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    view = "Bajas/_AgregarFechaDocComplemento";
                    break;
                case "ConfirmarCreacionDocumento":
                    ViewBag.CodigoComplemento = codigoComplemento;
                    ViewBag.CodigoFiniquito = codigoFiniquito;
                    ViewBag.FechaDocumento = fecha;
                    view = "Bajas/_ConfCreacionDocumentoComplemento";
                    break;
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AnularComplemento(string codigoComplemento, string observacion, string paginationIndex = "MS01", string onlyFiniquito = "N")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[24];
            string[] valMantFin = new string[24];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";

            valMantFin[0] = "ANUC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = observacion;
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoSuccess";
                        ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                    default:
                        ViewBag.ReferenciaPaginationIndex = paginationIndex;
                        view = "Bajas/_InfoError";
                        ViewBag.ReferenciaHtmlError = rows["Message"].ToString();

                        ViewBag.ReferenciaTypeUpdate = ModuleValidOnlyFiniquito(onlyFiniquito);
                        break;
                }
            }

            return PartialView(view);
        }

        #endregion
        
        #region "Metodos de API"

        public string Clientes()
        {
            return JsonConvert.SerializeObject(ModuleClientes("", "", "", "", Session["base64Username"].ToString()), Formatting.Indented);
        }

        #endregion

        #region "Modularización de Codigo"

        private List<Teamwork.Model.Operaciones.Clientes> ModuleClientes(string empresa, string pagination, string typeFilter, string dataFilter, string usuarioCreador)
        {
            List<Teamwork.Model.Operaciones.Clientes> clientes = new List<Teamwork.Model.Operaciones.Clientes>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsClientes = JsonConvert.DeserializeObject(Cargo.__Clientes(empresa, pagination, typeFilter, dataFilter, usuarioCreador, objects[0].Token.ToString()));

            for (var i = 0; i < objectsClientes.Count; i++)
            {
                Teamwork.Model.Operaciones.Clientes cliente = new Teamwork.Model.Operaciones.Clientes();

                cliente.Codigo = objectsClientes[i].Codigo.ToString();
                cliente.Value = objectsClientes[i].Value.ToString();

                clientes.Add(cliente);
            }

            return clientes;
        }

        #endregion
        
        #region "MODULARIZACION"

        private List<DescuentoComplemento> ModuleDescuentoComplementos(string codigoComplemento)
        {
            List<DescuentoComplemento> descuentoComplementos = new List<DescuentoComplemento>();

            string[] paramMantFin = new string[25];
            string[] valMantFin = new string[25];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";

            valMantFin[0] = "CONSDESCCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                DescuentoComplemento descuento = new DescuentoComplemento();

                descuento.Nombre = rows["Nombre"].ToString();
                descuento.Valor = rows["Valor"].ToString();
                descuento.Comentarios = rows["Comentarios"].ToString();
                descuento.CreadoPor = rows["CreadoPor"].ToString();
                descuento.Creado = rows["Creado"].ToString();
                descuento.UltimaActualizacion = rows["UltimaActualizacion"].ToString();
                descuento.Border = rows["Border"].ToString();
                descuento.GlyphiconColor = rows["GlyphiconColor"].ToString();
                descuento.Glyphicon = rows["Glyphicon"].ToString();
                descuento.CodigoDescuento = rows["CodigoDescuento"].ToString();

                descuentoComplementos.Add(descuento);
            }

            return descuentoComplementos;
        }

        private List<HaberesComplemento> ModuleHaberesComplementos(string codigoComplemento)
        {
            List<HaberesComplemento> haberesComplementos = new List<HaberesComplemento>();

            string[] paramMantFin = new string[25];
            string[] valMantFin = new string[25];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";

            valMantFin[0] = "CONSHABERCMP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = codigoComplemento;
            valMantFin[24] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                HaberesComplemento haberes = new HaberesComplemento();

                haberes.Nombre = rows["Nombre"].ToString();
                haberes.Valor = rows["Valor"].ToString();
                haberes.Comentarios = rows["Comentarios"].ToString();
                haberes.CreadoPor = rows["CreadoPor"].ToString();
                haberes.Creado = rows["Creado"].ToString();
                haberes.UltimaActualizacion = rows["UltimaActualizacion"].ToString();
                haberes.Border = rows["Border"].ToString();
                haberes.GlyphiconColor = rows["GlyphiconColor"].ToString();
                haberes.Glyphicon = rows["Glyphicon"].ToString();
                haberes.CodigoHaber = rows["CodigoHaber"].ToString();

                haberesComplementos.Add(haberes);
            }

            return haberesComplementos;
        }

        private List<Solicitud> ModuleSolicitudBajas(string pagination, string typeFilter = "", string dataFilter = "")
        {
            List<Solicitud> solicitudBajas = new List<Solicitud>();
            string[] paramSolicitudBaja = new string[7];
            string[] valSolicitudBaja = new string[7];

            paramSolicitudBaja[0] = "@AUTHENTICATE";
            paramSolicitudBaja[1] = "@AGENTAPP";
            paramSolicitudBaja[2] = "@TRABAJADOR";
            paramSolicitudBaja[3] = "@PAGINATION";
            paramSolicitudBaja[4] = "@HASBAJA";
            paramSolicitudBaja[5] = "@TYPEFILTER";
            paramSolicitudBaja[6] = "@DATAFILTER";

            valSolicitudBaja[0] = tokenAuth;
            valSolicitudBaja[1] = agenteAplication;
            valSolicitudBaja[2] = Session["NombreUsuario"].ToString();
            valSolicitudBaja[3] = pagination;
            valSolicitudBaja[4] = "S";
            valSolicitudBaja[5] = typeFilter;
            valSolicitudBaja[6] = dataFilter;
            

            DataSet dataSolicitudesC = servicioOperaciones.GetBajasObtenerBajasConfirmadas(paramSolicitudBaja, valSolicitudBaja).Table;

            foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Solicitud solicitud = new Solicitud();

                    solicitud.Code = rows["Code"].ToString();
                    solicitud.Message = rows["Message"].ToString();
                    solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                    solicitud.NombreProceso = rows["NombreProceso"].ToString();

                    solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();
                    solicitud.Creado = rows["Creado"].ToString();
                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
                    solicitud.Comentarios = rows["Comentarios"].ToString();
                    solicitud.CodificarCod = rows["CodificarCod"].ToString();

                    solicitud.Prioridad = rows["Prioridad"].ToString();
                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    solicitud.BorderColor = rows["BorderColor"].ToString();
                    solicitud.ColorFont = rows["ColorFont"].ToString();

                    solicitud.IntegrateSimulate = ModuleControlRetornoPath() + "/Auth/TokenAuth?events=simulate&authorizate=" + rows["CodigoSolicitud"].ToString() + "&ref=" + rows["IntegrateSettlement"].ToString();
                    solicitud.IntegrateSettlement = ModuleControlRetornoPath() + "/Auth/TokenAuth?events=integratesettlement&authorizate=" + rows["CodigoSolicitud"].ToString() + "&ref=" + rows["IntegrateSettlement"].ToString();
                    solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                    solicitud.OptCalcular = rows["OptCalcular"].ToString();
                    solicitud.OptAnularKam = rows["OptAnularKam"].ToString();
                    solicitud.OptViewCartaBaja = rows["OptViewCartaBaja"].ToString();
                    solicitud.OptSimulacion = rows["OptSimulacion"].ToString();
                    solicitud.OptLiquidacionSueldo = rows["OptLiquidacionSueldo"].ToString();
                    solicitud.OptRevertirCalculo = rows["OptRevertirCalculo"].ToString();

                    solicitud.EnlaceViewCartaBaja = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FydGFCYWph&data=" + rows["CodigoSolicitud"].ToString();

                    solicitudBajas.Add(solicitud);
                }
            }

            return solicitudBajas;
        }

        private List<Models.Finiquitos.Dashboard> ModuleDashboardFiniquitos()
        {
            List<Models.Finiquitos.Dashboard> dashboards = new List<Models.Finiquitos.Dashboard>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";

            valMantFin[0] = "DASHBOARDFINIQUITOSESTADOS";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                Models.Finiquitos.Dashboard dashboard = new Models.Finiquitos.Dashboard();

                dashboard.Estado = rows["Estado"].ToString();
                dashboard.Descripcion = rows["Descripcion"].ToString();
                dashboard.Cantidad = rows["Cantidad"].ToString();
                dashboard.Color = rows["Color"].ToString();

                dashboards.Add(dashboard);
            }

            return dashboards;
        }

        private bool ModulePerfilAuditorAdminFiniquito()
        {
            bool permitidoDashboard = false;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";

            valMantFin[0] = "HASDASHBOARDPENDPROCPAGO";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                switch (rows["HasDashboardPendProcPago"].ToString())
                {
                    case "S":
                        permitidoDashboard = true;
                        switch (rows["PositionActiveDashboard"].ToString())
                        {
                            case "Izq":
                                ViewBag.PositionShowDashboardIzq = "warning";
                                ViewBag.PositionShowDashboardDer = "anulado";
                                break;
                            case "Der":
                                ViewBag.PositionShowDashboardIzq = "anulado";
                                ViewBag.PositionShowDashboardDer = "warning";
                                break;
                        }
                        
                        break;
                }
            }

            return permitidoDashboard;
        }

        private List<Models.Finiquitos.Dashboard> ModuleDashboardPendCalcVer()
        {
            List<Models.Finiquitos.Dashboard> dashboards = new List<Models.Finiquitos.Dashboard>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet dataMantencion;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";

            valMantFin[0] = "DASHBOARDPENDPROCPAGO";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                Models.Finiquitos.Dashboard dashboard = new Models.Finiquitos.Dashboard();

                dashboard.PendientesVerificacion = rows["PendVerCalc"].ToString();
                dashboard.PendientesConfirmacion = rows["PendConfCalc"].ToString();
                dashboard.PendientesConfirmacionCmp = rows["PendConfCmp"].ToString();

                dashboards.Add(dashboard);
            }

            return dashboards;
        }

        private List<Models.Finiquitos.Finiquitos> ModuleFiniquitos(string pagination, string codigoFiniquito, string typeFilter = "", string dataFilter = "")
        {
            #region "FINIQUITOS CALCULADOS"

            List<Models.Finiquitos.Finiquitos> finiquitos = new List<Models.Finiquitos.Finiquitos>();

            string[] paramfiniquitos = new string[8];
            string[] valFiniquitos = new string[8];

            paramfiniquitos[0] = "@AUTHENTICATE";
            paramfiniquitos[1] = "@AGENTAPP";
            paramfiniquitos[2] = "@TRABAJADOR";
            paramfiniquitos[3] = "@TYPEVIEW";
            paramfiniquitos[4] = "@PAGINATION";
            paramfiniquitos[5] = "@CODIGOFINIQUITO";
            paramfiniquitos[6] = "@TYPEFILTER";
            paramfiniquitos[7] = "@DATAFILTER";

            valFiniquitos[0] = tokenAuth;
            valFiniquitos[1] = agenteAplication;
            valFiniquitos[2] = Session["NombreUsuario"].ToString();
            valFiniquitos[3] = "Finiquitos";
            valFiniquitos[4] = pagination;
            valFiniquitos[5] = codigoFiniquito;
            valFiniquitos[6] = typeFilter;
            valFiniquitos[7] = dataFilter;

            DataSet dataFiniquitos = servicioOperaciones.GetObtenerFiniquitos(paramfiniquitos, valFiniquitos).Table;

            foreach (DataRow rows in dataFiniquitos.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Models.Finiquitos.Finiquitos finiquito = new Models.Finiquitos.Finiquitos();

                    finiquito.Code = rows["Code"].ToString();
                    finiquito.Message = rows["Message"].ToString();
                    finiquito.NombreSolicitud = rows["NombreSolicitud"].ToString();
                    finiquito.CodigoFiniquito = rows["CodigoFiniquito"].ToString();
                    finiquito.Folio = rows["Folio"].ToString();
                    finiquito.Creado = rows["Creado"].ToString();
                    finiquito.CreadoPor = rows["CreadoPor"].ToString();
                    finiquito.CargoMod = rows["CargoMod"].ToString();
                    finiquito.Cargo = rows["Cargo"].ToString();
                    finiquito.Causal = rows["Causal"].ToString();
                    finiquito.TotalFiniquito = rows["TotalFiniquito"].ToString();
                    finiquito.Comentarios = rows["Comentarios"].ToString();
                    finiquito.TipoPago = rows["Pago"].ToString();
                    finiquito.CodigoPago = rows["SeriePago"].ToString();

                    finiquito.DetalleFiniquito = ModuleControlRetornoPath() + "/Bajas/Finiquitos/" + rows["CodigoFiniquito"].ToString();

                    finiquito.ViewCaratula = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString();
                    finiquito.ViewDocumento = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=RG9jRmluaXF1aXRv&data=" + rows["CodigoFiniquito"].ToString();
                    finiquito.ViewCarta = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FydGFCYWph&data=" + rows["CodigoFiniquito"].ToString();

                    finiquito.DownloadCaratula = ModuleControlRetornoPath() + "/Bajas/GeneratePdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString();
                    finiquito.DownloadDocumento = ModuleControlRetornoPath() + "/Bajas/GeneratePdf?pdf=RG9jRmluaXF1aXRv&data=" + rows["CodigoFiniquito"].ToString();

                    finiquito.Border = rows["Border"].ToString();
                    finiquito.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    finiquito.Glyphicon = rows["Glyphicon"].ToString();
                    finiquito.Estado = rows["EstadoGlosa"].ToString();

                    finiquito.OptAnular = rows["OptAnular"].ToString();
                    finiquito.OptViewCaratula = rows["OptViewCaratula"].ToString();
                    finiquito.OptDownloadCaratula = rows["OptDownloadCaratula"].ToString();
                    finiquito.OptViewDocumento = rows["OptViewDocumento"].ToString();
                    finiquito.OptDownloadDocumento = rows["OptDownloadDocumento"].ToString();
                    finiquito.ConfirmarPago = rows["ConfirmarPago"].ToString();
                    finiquito.OptAnularPago = rows["OptAnularPago"].ToString();
                    finiquito.OptRevertirConfirmacion = rows["OptRevertirConfirmacion"].ToString();
                    finiquito.OptPagar = rows["OptPagar"].ToString();
                    finiquito.OptNotariado = rows["OptNotariado"].ToString();
                    finiquito.OptSugTransf = rows["OptSugTransf"].ToString();
                    finiquito.OptRevSugTef = rows["OptRevSugTef"].ToString();
                    finiquito.OptVerificarPago = rows["OptVerificarPago"].ToString();
                    finiquito.OptRevVerificarPago = rows["OptRevVerificarPago"].ToString();
                    finiquito.OptEnviarProcesoPago = rows["OptEnviarProcesoPago"].ToString();
                    finiquito.OptRevEnvioProcesoPago =  rows["OptRevEnvioProcesoPago"].ToString();
                    finiquito.OptLiquidacionSueldo = rows["OptLiquidacionSueldo"].ToString();
                    finiquito.OptViewCarta = rows["OptViewCarta"].ToString();

                    if (schema != "")
                    {
                        finiquito.OnlyFiniquito = "S";
                    }
                    else
                    {
                        finiquito.OnlyFiniquito = "N";
                    }
                    

                    finiquitos.Add(finiquito);
                }
            }

            #endregion

            return finiquitos;
        }

        private List<Models.Teamwork.Pagination> ModulePagination(string typePagination, string paginationIndex, string hasBaja, string typeFilter = "", string dataFilter = "")
        {
            string[] paramPaginator = new string[8];
            string[] valPaginator = new string[8];
            DataSet dataPaginator;
            List<Models.Teamwork.Pagination> paginations = new List<Models.Teamwork.Pagination>();

            paramPaginator[0] = "@AUTHENTICATE";
            paramPaginator[1] = "@AGENTAPP";
            paramPaginator[2] = "@TRABAJADOR";
            paramPaginator[3] = "@TYPEPAGINATION";
            paramPaginator[4] = "@PAGINATION";
            paramPaginator[5] = "@HASBAJA";
            paramPaginator[6] = "@TYPEFILTER";
            paramPaginator[7] = "@DATAFILTER";

            valPaginator[0] = tokenAuth;
            valPaginator[1] = agenteAplication;
            valPaginator[2] = Session["NombreUsuario"].ToString();
            valPaginator[3] = typePagination;
            valPaginator[4] = paginationIndex;
            valPaginator[5] = hasBaja;
            valPaginator[6] = typeFilter;
            valPaginator[7] = dataFilter;

            dataPaginator = servicioOperaciones.GetPaginations(paramPaginator, valPaginator).Table;

            foreach (DataRow rows in dataPaginator.Tables[0].Rows)
            {
                Models.Teamwork.Pagination pagination = new Models.Teamwork.Pagination();

                pagination.NumeroPagina = rows["NumeroPagina"].ToString();
                pagination.Rango = rows["Rango"].ToString();
                pagination.Class = rows["Class"].ToString();
                pagination.Properties = rows["Properties"].ToString();
                pagination.TypeFilter = rows["TypeFilter"].ToString();
                pagination.Filter = rows["Filter"].ToString();
                pagination.HasBaja = rows["HasBaja"].ToString();

                paginations.Add(pagination);
            }

            return paginations;
        }
        
        private List<Models.Finiquitos.Finiquitos> ModuleSimulacion(string pagination, string codigoFiniquito, string typeFilter = "", string dataFilter = "")
        {
            #region "FINIQUITOS CALCULADOS"

            List<Models.Finiquitos.Finiquitos> finiquitos = new List<Models.Finiquitos.Finiquitos>();

            string[] paramfiniquitos = new string[8];
            string[] valFiniquitos = new string[8];

            paramfiniquitos[0] = "@AUTHENTICATE";
            paramfiniquitos[1] = "@AGENTAPP";
            paramfiniquitos[2] = "@TRABAJADOR";
            paramfiniquitos[3] = "@TYPEVIEW";
            paramfiniquitos[4] = "@PAGINATION";
            paramfiniquitos[5] = "@CODIGOFINIQUITO";
            paramfiniquitos[6] = "@TYPEFILTER";
            paramfiniquitos[7] = "@DATAFILTER";

            valFiniquitos[0] = tokenAuth;
            valFiniquitos[1] = agenteAplication;
            valFiniquitos[2] = Session["NombreUsuario"].ToString();
            valFiniquitos[3] = "Simulacion";
            valFiniquitos[4] = pagination;
            valFiniquitos[5] = codigoFiniquito;
            valFiniquitos[6] = typeFilter;
            valFiniquitos[7] = dataFilter;

            DataSet dataFiniquitos = servicioOperaciones.GetObtenerFiniquitos(paramfiniquitos, valFiniquitos).Table;

            foreach (DataRow rows in dataFiniquitos.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Models.Finiquitos.Finiquitos finiquito = new Models.Finiquitos.Finiquitos();

                    finiquito.Code = rows["Code"].ToString();
                    finiquito.Message = rows["Message"].ToString();
                    finiquito.NombreSolicitud = rows["NombreSolicitud"].ToString();
                    finiquito.CodigoFiniquito = rows["CodigoFiniquito"].ToString();
                    finiquito.Folio = rows["Folio"].ToString();
                    finiquito.Creado = rows["Creado"].ToString();
                    finiquito.CreadoPor = rows["CreadoPor"].ToString();
                    finiquito.CargoMod = rows["CargoMod"].ToString();
                    finiquito.Cargo = rows["Cargo"].ToString();
                    finiquito.Causal = rows["Causal"].ToString();
                    finiquito.TotalFiniquito = rows["TotalFiniquito"].ToString();
                    finiquito.Comentarios = rows["Comentarios"].ToString();

                    finiquito.DetalleFiniquito = ModuleControlRetornoPath() + "/Bajas/Simulacion/" + rows["CodigoFiniquito"].ToString();

                    finiquito.ViewCarta = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FydGFCYWph&data=" + rows["CodigoFiniquito"].ToString();
                    finiquito.ViewCaratula = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["CodigoFiniquito"].ToString();
                    finiquito.ViewDocumento = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=RG9jRmluaXF1aXRv&data=" + rows["CodigoFiniquito"].ToString();

                    finiquito.Border = rows["Border"].ToString();
                    finiquito.GlyphiconColor = rows["GlyphiconColor"].ToString();

                    finiquito.OptAnular = rows["OptAnular"].ToString();
                    finiquito.OptMoverFiniquito = rows["OptAnular"].ToString();
                    finiquito.OptViewCarta = rows["OptAnular"].ToString();

                    finiquito.OptViewCaratula = rows["OptViewCaratula"].ToString();
                    finiquito.OptViewDocumento = rows["OptViewDocumento"].ToString();
                    finiquito.OptLiquidacionSueldo = rows["OptLiquidacionSueldo"].ToString();

                    if (schema != "")
                    {
                        finiquito.OnlyFiniquito = "S";
                    }
                    else
                    {
                        finiquito.OnlyFiniquito = "N";
                    }


                    finiquitos.Add(finiquito);
                }
            }

            #endregion

            return finiquitos;
        }

        private List<Complementos> ModuleComplementos(string pagination, string codigoFiniquito, string typeFilter = "", string dataFilter = "")
        {
            List<Complementos> complementos = new List<Complementos>();

            string[] paramfiniquitos = new string[8];
            string[] valFiniquitos = new string[8];

            paramfiniquitos[0] = "@AUTHENTICATE";
            paramfiniquitos[1] = "@AGENTAPP";
            paramfiniquitos[2] = "@TRABAJADOR";
            paramfiniquitos[3] = "@TYPEVIEW";
            paramfiniquitos[4] = "@PAGINATION";
            paramfiniquitos[5] = "@CODIGOFINIQUITO";
            paramfiniquitos[6] = "@TYPEFILTER";
            paramfiniquitos[7] = "@DATAFILTER";

            valFiniquitos[0] = tokenAuth;
            valFiniquitos[1] = agenteAplication;
            valFiniquitos[2] = Session["NombreUsuario"].ToString();
            valFiniquitos[3] = "Complementos";
            valFiniquitos[4] = pagination;
            valFiniquitos[5] = codigoFiniquito;
            valFiniquitos[6] = typeFilter;
            valFiniquitos[7] = dataFilter;

            DataSet dataFiniquitos = servicioOperaciones.GetObtenerFiniquitos(paramfiniquitos, valFiniquitos).Table;

            foreach (DataRow rows in dataFiniquitos.Tables[0].Rows)
            {
                Complementos complemento = new Complementos();

                complemento.Border = rows["Border"].ToString();
                complemento.GlyphiconColor = rows["GlyphiconColor"].ToString();
                complemento.DetalleComplemento = ModuleControlRetornoPath() + "/Bajas/Finiquitos/" + rows["CodigoFiniquito"].ToString() + "/Complementos/" + rows["CodigoComplemento"].ToString();
                complemento.Creado = rows["Creado"].ToString();
                complemento.CreadoPor = rows["CreadoPor"].ToString();
                complemento.TotalComplemento = rows["TotalComplemento"].ToString();
                complemento.Comentarios = rows["Comentarios"].ToString();
                complemento.NombreSolicitud = rows["NombreSolicitud"].ToString();
                complemento.Solicitud = rows["Solicitud"].ToString();
                complemento.NombreSolicitudCorto = rows["NombreSolicitudCorto"].ToString();
                complemento.Folio = rows["Folio"].ToString();
                complemento.EnlaceDocumento = ModuleControlRetornoPath() + "/Bajas/ViewPdf?pdf=Q29tcGxlbWVudG9GaW5pcXVpdG8=&data=" + rows["CodigoComplemento"].ToString();
                complemento.CodigoComplemento = rows["CodigoComplemento"].ToString();
                complemento.CodigoPago = rows["CodigoPago"].ToString();
                complemento.Pago = rows["Pago"].ToString();


                complemento.OptAnular = rows["OptAnular"].ToString();
                complemento.OptViewDocumento = rows["OptViewDocumento"].ToString();
                complemento.ConfirmarPago = rows["ConfirmarPago"].ToString();
                complemento.OptAnularPago = rows["OptAnularPago"].ToString();
                complemento.OptRevertirConfirmacion = rows["OptRevertirConfirmacion"].ToString();
                complemento.OptPagar = rows["OptPagar"].ToString();
                complemento.OptNotariado = rows["OptNotariado"].ToString();
                complemento.OptEnviarProcesoPago = rows["OptEnviarProcesoPago"].ToString();
                complemento.OptRevEnvioProcesoPago = rows["OptRevEnvioProcesoPago"].ToString();
                complemento.OptEnviarProcesoPagoTEF = rows["OptEnviarProcesoPagoTEF"].ToString();
                complemento.OptLiquidacionSueldo = rows["OptLiquidacionSueldo"].ToString();
                complemento.OptVolverFiniquito = rows["OptVolverFiniquito"].ToString();
                complemento.EnlaceVolverFiniquito = ModuleControlRetornoPath() + "/Bajas/Finiquitos/" + rows["CodigoFiniquito"].ToString();

                complementos.Add(complemento);
            }

            return complementos;
        }
        
        private string ModuleControlRetornoServer()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                {
                    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                }
                else
                {
                    domainReal = "181.190.6.196";
                }

                domain = "http://" + domainReal;
                prefixDomain = Request.Url.AbsoluteUri.Split('/')[3];
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                prefixDomain = "";
            }

            #endregion

            return domain;

        }
        
        private string ModuleValidOnlyFiniquito(string onlyFiniquito)
        {
            string element = string.Empty;

            switch (onlyFiniquito)
            {
                case "S":
                    element = "Only";
                    break;
                default:
                    element = "All";
                    break;
            }

            return element;
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
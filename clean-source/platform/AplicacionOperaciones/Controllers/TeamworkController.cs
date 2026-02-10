using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplicacionOperaciones.Models.Teamwork;

namespace AplicacionOperaciones.Controllers
{
    public class TeamworkController : Controller
    {

        ServicioAuth.ServicioAuthClient servicioAuth = new ServicioAuth.ServicioAuthClient();
        ServicioOperaciones.ServicioOperacionesClient servicioOperaciones = new ServicioOperaciones.ServicioOperacionesClient();
        ServicioAuth.ServicioAuthClient svcAuth = new ServicioAuth.ServicioAuthClient();

        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";
        string sistema = "";
        string ambiente = "";
        string section = "";
        string schema = "";
        string codAction = "";
        string domain = string.Empty;
        string prefixDomain = string.Empty;
        string domainReal = string.Empty;
        string errorSistema = "Ha ocurrido algo inesperado en la plataforma, favor comunicarse con el area de TI, para ser solucionado el problema. Favor refrescar la pagina para evitar problemas de encolamiento de información.";
        string errorSistemaCorreo = "Ha ocurrido un problema en el envio por correo electrónico de la solicitud de contratos";
        string fromDebugQA = "AplicacionOperaciones";

        // GET: Teamwork
        public ActionResult Index()
        {
            if (AplicationActive())
            {
                #region "PERMISOS Y ACCESOS"

                if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                {
                    Session["ApplicationActive"] = null;
                }

                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                {
                    sistema = Request.Url.AbsoluteUri.Split('/')[3];
                    schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                }
                else
                {
                    sistema = fromDebugQA;
                    schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

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
                List<Models.Accesos> accesos = new List<Models.Accesos>();

                foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
                {
                    Models.Accesos acceso = new Models.Accesos();

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

                string[] paramBarcode = new string[3];
                string[] valBarcode = new string[3];
                DataSet data;
                List<Barcode> barcodes = new List<Barcode>();

                paramBarcode[0] = "@AUTHENTICATE";
                paramBarcode[1] = "@AGENTAPP";
                paramBarcode[2] = "@TRABAJADOR";

                valBarcode[0] = tokenAuth;
                valBarcode[1] = agenteAplication;
                valBarcode[2] = Session["base64Username"].ToString();

                //data = servicioAuth.GetBarcodes(paramBarcode, valBarcode).Table;

                //foreach (DataRow rows in data.Tables[0].Rows)
                //{
                //    Barcode barcode = new Barcode();

                //    barcode.Glosa = rows["Glosa"].ToString();
                //    barcode.Glosa2 = rows["Glosa2"].ToString();
                //    barcode.Glyphicon = rows["Glyphicon"].ToString();
                //    barcode.GlyphiconColor = rows["GlyphiconColor"].ToString();
                //    barcode.Nuevo = rows["Nuevo"].ToString();
                //    barcode.Referencia = domain + prefixDomain + "/Teamwork/Barcode/" + rows["Referencia"].ToString();

                //    barcodes.Add(barcode);
                //}

                //ViewBag.RenderizadoBarcodes = barcodes;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }

        public ActionResult Barcode()
        {
            if (AplicationActive())
            {
                ViewBag.RenderizdoTypeReaderBarcode = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                Session["RenderizdoTypeReaderBarcode"] = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "QXV0b3JpemFjaW9uUGFnbw==":
                        ViewBag.StageBarcodes = "InitAutorizacionFiniquito";
                        break;
                    case "VmFsaWRhY2lvblBhZ28=":
                        ViewBag.StageBarcodes = "InitValidacionFiniquito";
                        break;
                    default:
                        ViewBag.StageBarcodes = "InitNotSectionReaderBarcode";
                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }

        public ActionResult Informa()
        {
            if (AplicationActive())
            {
                ViewBag.ApplicationDomainTeamwork = ModuleControlRetornoPath() + "/Teamwork";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Noticias()
        {
            if (AplicationActive())
            {
                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "New":
                        ViewBag.EventNoticias = "New";
                        break;
                    case "Edit":
                        ViewBag.EventNoticias = "Edit";
                        break;
                    case "All":
                        ViewBag.EventNoticias = "All";
                        break;
                    case "Noticias":
                        ViewBag.EventNoticias = "Management";
                        break;
                    default:
                        ViewBag.EventNoticias = "View";
                        break;
                }

                ViewBag.ApplicationDomainTeamwork = ModuleControlRetornoPath() + "/Teamwork";

            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        #region  "METODOS DE LECTURA"

        [HttpPost]
        public ActionResult EventReadBarcode(string barcode = "", string actionInit = "N")
        {
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
            
            string[] paramMantFin = new string[14];
            string[] valMantFin = new string[14];
            DataSet data;
            string view = string.Empty;

            ViewBag.RenderizdoTypeReaderBarcode = Session["RenderizdoTypeReaderBarcode"].ToString();

            if (actionInit == "S")
            {
                switch (ViewBag.RenderizdoTypeReaderBarcode)
                {
                    case "QXV0b3JpemFjaW9uUGFnbw==":
                        ViewBag.StageBarcodes = "InitAutorizacionFiniquito";
                        break;
                    case "VmFsaWRhY2lvblBhZ28=":
                        ViewBag.StageBarcodes = "InitValidacionFiniquito";
                        break;
                    default:
                        ViewBag.StageBarcodes = "InitNotSectionReaderBarcode";
                        break;
                }
            }
            else
            {
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

                valMantFin[0] = "CFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = barcode;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "404":
                            ViewBag.StageAutorizacionPagoFiniquito = "NoLecturaCodigoBarra";

                            break;
                        case "401":
                            ViewBag.MetodoPagoReferencia = rows["MetodoPago"].ToString();
                            ViewBag.MontoReferencia = rows["Monto"].ToString();

                            ViewBag.StageBarcodes = "BloqueoPago";
                            break;
                        default:
                            ViewBag.FolioReferencia = rows["FolioReferencia"].ToString();
                            ViewBag.ReturnStageAutorizacionPagoFiniquito = rows["Code"].ToString();
                            ViewBag.CaratulaFiniquitoPreview = domain + prefixDomain + "/Bajas/ViewPdf?pdf=Q2FyYXR1bGFGaW5pcXVpdG8=&data=" + rows["FolioReferencia"].ToString();


                            ViewBag.MetodoPagoReferencia = rows["MetodoPago"].ToString();
                            ViewBag.MontoReferencia = rows["Monto"].ToString();

                            ViewBag.StageBarcodes = "ConsultarFiniquito";
                            break;
                    }

                }

            }

            switch (ViewBag.RenderizdoTypeReaderBarcode)
            {
                case "QXV0b3JpemFjaW9uUGFnbw==":
                    view = "_AutorizacionFiniquito";
                    break;
                case "VmFsaWRhY2lvblBhZ28=":
                    view = "_ValidacionFiniquito";
                    break;
            }

            return PartialView("Teamwork/" + view);
        }

        [HttpPost]
        public ActionResult EventConfirmarPago(string codigoFiniquito, string codigoPago, string destinatario = "", string banco = "", string tipoCuenta = "",
                                               string fecha = "", string numeroCuenta = "", string rut = "", string observaciones = "", string montoFiniquito = "")
        {

            string domain = string.Empty;
            string prefixDomain = string.Empty;
            string domainReal = string.Empty;
            string[] paramMantFin = new string[14];
            string[] valMantFin = new string[14];
            DataSet data;
            string view = string.Empty;

            ViewBag.RenderizdoTypeReaderBarcode = Session["RenderizdoTypeReaderBarcode"].ToString();

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

            valMantFin[0] = "REGP";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoFiniquito;
            valMantFin[3] = codigoPago;
            valMantFin[4] = "";
            valMantFin[5] = "";
            valMantFin[6] = destinatario;
            valMantFin[7] = banco;
            valMantFin[8] = tipoCuenta;
            valMantFin[9] = fecha;
            valMantFin[10] = numeroCuenta;
            valMantFin[11] = rut;
            valMantFin[12] = observaciones;
            valMantFin[13] = montoFiniquito;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "101":
                        ViewBag.FolioReferencia = rows["FolioReferencia"].ToString();
                        ViewBag.ReturnStageAutorizacionPagoFiniquito = rows["Code"].ToString();
                        ViewBag.ReferenciaCodigoPago = codigoPago;

                        ViewBag.ReferenciaNombreDestinatario = rows["NombreDestinatario"].ToString();
                        ViewBag.ReferenciaRut = rows["Rut"].ToString();
                        ViewBag.ReferenciaNumeroCuenta = rows["NumeroCuenta"].ToString();
                        ViewBag.ReferenciaTipoCuenta = rows["TipoCuenta"].ToString();
                        ViewBag.ReferenciaBanco = rows["Banco"].ToString();
                        ViewBag.ReferenciaMontoFiniquito = rows["MontoFiniquito"].ToString();

                        ViewBag.ReferenciaGlosaPago = rows["GlosaPago"].ToString();

                        ViewBag.StageAutorizacionPagoFiniquito = "DatosTransaccion";
                        break;
                    default:
                        ViewBag.StageBarcodes = "ConfirmadoLecturaNueva";
                        break;
                }
            }

            switch (ViewBag.RenderizdoTypeReaderBarcode)
            {
                case "QXV0b3JpemFjaW9uUGFnbw==":
                    view = "_AutorizacionFiniquito";
                    break;
                case "VmFsaWRhY2lvblBhZ28=":
                    view = "_ValidacionFiniquito";
                    break;
            }

            return PartialView("Teamwork/" + view);
        }

        [HttpPost]
        public ActionResult EnviarCorreo(string html, string titulo)
        {
            string[] paramCorreo = new string[3];
            string[] valCorreo = new string[3];
            DataSet data;

            paramCorreo[0] = "@HTML";
            paramCorreo[1] = "@TITULO";
            paramCorreo[2] = "@TRABAJADOR";

            valCorreo[0] = html.Replace(@"'", @"""");
            valCorreo[1] = titulo;
            valCorreo[2] = Session["NombreUsuario"].ToString();

            data = svcAuth.SetEnviaCorreoTeamworkInforma(paramCorreo, valCorreo).Table;
            
            return PartialView("Teamwork/_EnviaCorreoTeamworkInforma");
        }

        [HttpPost]
        public ActionResult AgregarNoticiaTeamwork(string ilustracion, string vencimiento, string programable, string fechaInicio, string fechaTermino, string enlace, string enlaceTag,
                                                   string noticia, string subtitulo, string body)
        {
            string view = string.Empty;
            string[] paramManNotice = new string[14];
            string[] valManNotice = new string[14];
            DataSet data;
            string expiracion = string.Empty;
            string programar = string.Empty;

            try
            {

                if (Convert.ToBoolean(vencimiento))
                {
                    expiracion = "S";
                }
                else
                {
                    expiracion = "N";
                }

                if (Convert.ToBoolean(programable))
                {
                    programar = "S";
                }
                else
                {
                    programar = "N";
                }

                paramManNotice[0] = "@ACCION";
                paramManNotice[1] = "@NOTICIA";
                paramManNotice[2] = "@BODY";
                paramManNotice[3] = "@SUBTITULO";
                paramManNotice[4] = "@ENLACE";
                paramManNotice[5] = "@ENLACETAG";
                paramManNotice[6] = "@ILUSTRACION";
                paramManNotice[7] = "@PROGRAMABLE";
                paramManNotice[8] = "@FECHAINICIO";
                paramManNotice[9] = "@FECHATERMINO";
                paramManNotice[10] = "@TYPEPAGINATION";
                paramManNotice[11] = "@PAGINATION";
                paramManNotice[12] = "@FILTER";
                paramManNotice[13] = "@DATEFILTER";

                valManNotice[0] = "ADDNOTICE";
                valManNotice[1] = noticia;
                valManNotice[2] = body.Replace(@"'", @"""");
                valManNotice[3] = subtitulo.Replace(@"'", @"""");
                valManNotice[4] = enlace;
                valManNotice[5] = enlaceTag;
                valManNotice[6] = ilustracion.Replace(@"'", @"""");
                valManNotice[7] = programar;
                valManNotice[8] = fechaInicio;
                valManNotice[9] = fechaTermino;
                valManNotice[10] = "";
                valManNotice[11] = "";
                valManNotice[12] = "";
                valManNotice[13] = "";

                data = svcAuth.CrudMantenedorNoticiasTeamwork(paramManNotice, valManNotice).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Teamwork/Noticias/_SuccessNoticiaPublicada";
                            ViewBag.MessageNoticiaPublicada = rows["Message"].ToString();
                            break;
                    }
                }

            }
            catch (Exception)
            {

            }

            return PartialView(view);
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
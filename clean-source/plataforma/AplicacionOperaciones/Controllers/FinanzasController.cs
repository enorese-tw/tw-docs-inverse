using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Rotativa;
using System.IO;

using OfficeOpenXml;
using br.com.arcnet.barcodegenerator;
using System.Net.Sockets;
using System.Net;
using Teamwork.Model.Operaciones;
using Newtonsoft.Json;
using Teamwork.WebApi.Operaciones;
using Teamwork.WebApi.Auth;
using Teamwork.Model.Finanzas;
using Teamwork.Model.Operaciones;
using Teamwork.Extensions.Excel;
using Teamwork.WebApi;
using Teamwork.Infraestructura.Collections.Persona;
using AplicacionOperaciones.ServicioCorreo;
using Teamwork.Model.Teamwork;

namespace AplicacionOperaciones.Controllers
{
    public class FinanzasController : Controller
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
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();

        }

        public ActionResult AdministrarGastos()
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

                ViewBag.RenderizadoAplicacion = ModuleControlRetornoPath();

                #endregion

                #region "GASTOS"

                ModulePaginationGastos("MOSGAST", "MS01", "", "", "Gastos");

                #endregion
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();

        }

        public ActionResult AdministrarPeriodo()
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

                string[] paramRenderAnexos =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR"
                };

                string[] valRenderAnexos =
                {
                    tokenAuth,
                    agenteAplication,
                    ""
                };

                DataSet dsGastos = servicioOperaciones.GetObtenerPeriodo(paramRenderAnexos, valRenderAnexos).Table;

                List<Models.Periodo> lsPeriodo = new List<Models.Periodo>();

                foreach (DataRow dr in dsGastos.Tables[0].Rows)
                {
                    Models.Periodo periodo = new Models.Periodo();

                    periodo.Code = dr["Code"].ToString();
                    periodo.Message = dr["Message"].ToString();
                    periodo.Id = dr["Id"].ToString();
                    periodo.Empresa = dr["Empresa"].ToString();
                    periodo.PeriodoVigente = dr["PeriodoVigente"].ToString();
                    periodo.Vigente = dr["Vigente"].ToString();

                    lsPeriodo.Add(periodo);
                }

                ViewBag.RenderizaPeriodo = lsPeriodo;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();

        }

        public ActionResult CargaMasivaKam()
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

                List<Models.CargaMasiva> cargasMasivas = new List<Models.CargaMasiva>();

                string[] paramRenderAnexos =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CARGAMASIVA"
                };

                string[] valRenderAnexos =
                {
                    tokenAuth,
                    agenteAplication,
                    "CargaMasivaKam"
                };

                DataSet dataRenderCargaMasiva = servicioOperaciones.GetObtenerRenderizadoCargaMasiva(paramRenderAnexos, valRenderAnexos).Table;

                foreach (DataRow rows in dataRenderCargaMasiva.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.CargaMasiva cargaMasiva = new Models.CargaMasiva();

                        cargaMasiva.Code = rows["Code"].ToString();
                        cargaMasiva.Message = rows["Message"].ToString();
                        cargaMasiva.Glyphicon = rows["Glyphicon"].ToString();
                        cargaMasiva.GlyphiconColor = rows["GlyphiconColor"].ToString();
                        cargaMasiva.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
                        cargaMasiva.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
                        cargaMasiva.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
                        cargaMasiva.UploadRenderizadoColor = rows["UploadRenderizadoColor"].ToString();
                        cargaMasiva.UploadNombrePlantilla = rows["UploadNombrePlantilla"].ToString();
                        cargaMasiva.UploadNombreHojaCargaMasiva = rows["UploadNombreHojaCargaMasiva"].ToString();
                        cargaMasiva.UploadCifrado = rows["UploadCifrado"].ToString();
                        cargaMasiva.UploadColumnas = rows["UploadColumnas"].ToString();
                        cargaMasiva.UploadNodoPadre = rows["UploadNodoPadre"].ToString();
                        cargaMasiva.UploadNodoHijo = rows["UploadNodoHijo"].ToString();
                        cargaMasiva.UploadRenderizadoGlyphicon = rows["UploadRenderizadoGlyphicon"].ToString();
                        cargaMasiva.UploadRenderizadoOneTexto = rows["UploadRenderizadoOneTexto"].ToString();
                        cargaMasiva.UploadRenderizadoSecTexto = rows["UploadRenderizadoSecTexto"].ToString();
                        cargaMasiva.UploadRenderizadoMensajeImpt = rows["UploadRenderizadoMensajeImpt"].ToString();
                        cargaMasiva.RenderizadoTituloDownload = rows["RenderizadoTituloDownload"].ToString();
                        cargaMasiva.DownloadCifrado = rows["DownloadCifrado"].ToString();
                        cargaMasiva.DownloadRenderizadoColor = rows["DownloadRenderizadoColor"].ToString();
                        cargaMasiva.DownloadRenderizadoGlyphicon = rows["DownloadRenderizadoGlyphicon"].ToString();
                        cargaMasiva.DownloadRenderizadoOneTexto = rows["DownloadRenderizadoOneTexto"].ToString();
                        cargaMasiva.DownloadRenderizadoSecTexto = rows["DownloadRenderizadoSecTexto"].ToString();
                        cargaMasiva.DownloadRenderizadoMensajeImpt = rows["DownloadRenderizadoMensajeImpt"].ToString();

                        cargasMasivas.Add(cargaMasiva);
                    }
                }

                ViewBag.RenderizadoCargaMasiva = cargasMasivas;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult CargaMasivaFinanzas()
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

                List<Models.CargaMasiva> cargasMasivas = new List<Models.CargaMasiva>();

                string[] parametrosHeadersCuentas =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR"
                };

                string[] valoresHeadersCuentas =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),

                };

                string[] paramRenderAnexos =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@CARGAMASIVA"
                };

                string[] valRenderAnexos =
                {
                    tokenAuth,
                    agenteAplication,
                    "CargaMasivaFinanzas"
                 };


                DataSet dataRenderCargaMasiva = servicioOperaciones.GetObtenerRenderizadoCargaMasiva(paramRenderAnexos, valRenderAnexos).Table;
                DataSet dsTipoReembolso = servicioOperaciones.GetObtenerTipoReembolso(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsCliente = servicioOperaciones.GetObtenerClientes(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsEmpresa = servicioAuth.GetObtenerEmpresas(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                List<Models.TipoReembolso> lstTipoReembolso = new List<Models.TipoReembolso>();
                List<Models.Cliente> lsCliente = new List<Models.Cliente>();
                List<Models.Empresa> lsEmpresa = new List<Models.Empresa>();

                foreach (DataRow dr in dsTipoReembolso.Tables[0].Rows)
                {
                    Models.TipoReembolso tipoReembolso = new Models.TipoReembolso();

                    tipoReembolso.Code = dr["Code"].ToString();
                    tipoReembolso.Message = dr["Message"].ToString();
                    tipoReembolso.Id = dr["IdTipoReembolso"].ToString();
                    tipoReembolso.Nombre = dr["Nombre"].ToString();
                    tipoReembolso.Codigo = dr["Codigo"].ToString();
                    tipoReembolso.Descripcion = dr["Descripcion"].ToString();
                    tipoReembolso.Vigente = dr["Vigente"].ToString();

                    lstTipoReembolso.Add(tipoReembolso);
                }

                foreach (DataRow dr in dsEmpresa.Tables[0].Rows)
                {
                    Models.Empresa empresa = new Models.Empresa();

                    empresa.Code = dr["Code"].ToString();
                    empresa.Message = dr["Message"].ToString();
                    empresa.Id = dr["Id"].ToString();
                    empresa.Nombre = dr["Nombre"].ToString();
                    empresa.Descripcion = dr["Descripcion"].ToString();
                    empresa.Vigente = dr["Vigente"].ToString();

                    lsEmpresa.Add(empresa);
                }

                foreach (DataRow dr in dsCliente.Tables[0].Rows)
                {
                    Models.Cliente cliente = new Models.Cliente();

                    cliente.Code = dr["Code"].ToString();
                    cliente.Message = dr["Message"].ToString();
                    cliente.Id = dr["Id"].ToString();
                    cliente.Codigo = dr["Codigo"].ToString();
                    cliente.Nombre = dr["Nombre"].ToString();
                    cliente.Rut = dr["Rut"].ToString();
                    cliente.Empresa = dr["Empresa"].ToString();
                    cliente.Vigente = dr["Vigente"].ToString();

                    lsCliente.Add(cliente);
                }

                foreach (DataRow rows in dataRenderCargaMasiva.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.CargaMasiva cargaMasiva = new Models.CargaMasiva();

                        cargaMasiva.Code = rows["Code"].ToString();
                        cargaMasiva.Message = rows["Message"].ToString();
                        cargaMasiva.Glyphicon = rows["Glyphicon"].ToString();
                        cargaMasiva.GlyphiconColor = rows["GlyphiconColor"].ToString();
                        cargaMasiva.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
                        cargaMasiva.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
                        cargaMasiva.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
                        cargaMasiva.UploadRenderizadoColor = rows["UploadRenderizadoColor"].ToString();
                        cargaMasiva.UploadNombrePlantilla = rows["UploadNombrePlantilla"].ToString();
                        cargaMasiva.UploadNombreHojaCargaMasiva = rows["UploadNombreHojaCargaMasiva"].ToString();
                        cargaMasiva.UploadCifrado = rows["UploadCifrado"].ToString();
                        cargaMasiva.UploadColumnas = rows["UploadColumnas"].ToString();
                        cargaMasiva.UploadNodoPadre = rows["UploadNodoPadre"].ToString();
                        cargaMasiva.UploadNodoHijo = rows["UploadNodoHijo"].ToString();
                        cargaMasiva.UploadRenderizadoGlyphicon = rows["UploadRenderizadoGlyphicon"].ToString();
                        cargaMasiva.UploadRenderizadoOneTexto = rows["UploadRenderizadoOneTexto"].ToString();
                        cargaMasiva.UploadRenderizadoSecTexto = rows["UploadRenderizadoSecTexto"].ToString();
                        cargaMasiva.UploadRenderizadoMensajeImpt = rows["UploadRenderizadoMensajeImpt"].ToString();
                        cargaMasiva.RenderizadoTituloDownload = rows["RenderizadoTituloDownload"].ToString();
                        cargaMasiva.DownloadCifrado = rows["DownloadCifrado"].ToString();
                        cargaMasiva.DownloadRenderizadoColor = rows["DownloadRenderizadoColor"].ToString();
                        cargaMasiva.DownloadRenderizadoGlyphicon = rows["DownloadRenderizadoGlyphicon"].ToString();
                        cargaMasiva.DownloadRenderizadoOneTexto = rows["DownloadRenderizadoOneTexto"].ToString();
                        cargaMasiva.DownloadRenderizadoSecTexto = rows["DownloadRenderizadoSecTexto"].ToString();
                        cargaMasiva.DownloadRenderizadoMensajeImpt = rows["DownloadRenderizadoMensajeImpt"].ToString();

                        cargasMasivas.Add(cargaMasiva);
                    }
                }


                ViewBag.RenderizadoReembolso = lstTipoReembolso;
                ViewBag.RenderizadoEmpresa = lsEmpresa;
                ViewBag.RenderizadoCliente = lsCliente;
                ViewBag.RenderizadoCargaMasiva = cargasMasivas;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AgregarGastos()
        {
            if (AplicationActive())
            {
                ViewBag.CodeCorrect = true;

                string[] parametrosHeadersCuentas =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR"
                };

                string[] valoresHeadersCuentas =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),

                };

                //Dataset
                DataSet dsConceptoGasto = servicioOperaciones.GetObtenerConceptoGastos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsEstado = servicioOperaciones.GetObtenerEstadoGasto(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsTipoDocumento = servicioOperaciones.GetObtenerTiposDocumentos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsBanco = servicioOperaciones.GetObtenerBancos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsTipoReembolso = servicioOperaciones.GetObtenerTipoReembolso(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsGasto = servicioOperaciones.GetObtenerGastos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsTipoCuenta = servicioOperaciones.GetObtenerTiposCuentas(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsEmpresa = servicioAuth.GetObtenerEmpresas(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsCliente = servicioOperaciones.GetObtenerClientes(parametrosHeadersCuentas, valoresHeadersCuentas).Table;

                //Object List
                List<Models.ConceptoGasto> lstConceptoGasto = new List<Models.ConceptoGasto>();
                List<Models.Estado> lstEstado = new List<Models.Estado>();
                List<Models.TipoDocumento> lstTipoDocumento = new List<Models.TipoDocumento>();
                List<Models.Banco> lstBanco = new List<Models.Banco>();
                List<Models.TipoReembolso> lstTipoReembolso = new List<Models.TipoReembolso>();
                List<Models.Gasto> lstGasto = new List<Models.Gasto>();
                List<Models.TipoCuenta> lstTipoCuenta = new List<Models.TipoCuenta>();
                List<Models.Empresa> lsEmpresa = new List<Models.Empresa>();
                List<Models.Cliente> lsCliente = new List<Models.Cliente>();

                //Object each

                foreach (DataRow dr in dsConceptoGasto.Tables[0].Rows)
                {
                    Models.ConceptoGasto conceptoGasto = new Models.ConceptoGasto();

                    conceptoGasto.Code = dr["Code"].ToString();
                    conceptoGasto.Message = dr["Message"].ToString();
                    conceptoGasto.Id = dr["IdConceptoGasto"].ToString();
                    conceptoGasto.Nombre = dr["ConceptoGasto"].ToString();
                    conceptoGasto.Descripcion = dr["Descripcion"].ToString();
                    conceptoGasto.Vigente = dr["Vigente"].ToString();

                    lstConceptoGasto.Add(conceptoGasto);
                }

                foreach (DataRow dr in dsEstado.Tables[0].Rows)
                {
                    Models.Estado estado = new Models.Estado();

                    estado.Code = dr["Code"].ToString();
                    estado.Message = dr["Message"].ToString();
                    estado.Codigo = dr["Codigo"].ToString();
                    estado.Descripcion = dr["Descripcion"].ToString();
                    estado.Vigente = dr["Vigente"].ToString();

                    lstEstado.Add(estado);
                }

                foreach (DataRow dr in dsTipoDocumento.Tables[0].Rows)
                {
                    Models.TipoDocumento tipoDocumento = new Models.TipoDocumento();

                    tipoDocumento.Code = dr["Code"].ToString();
                    tipoDocumento.Message = dr["Message"].ToString();
                    tipoDocumento.Id = dr["IdTipoDocumento"].ToString();
                    tipoDocumento.IdSII = dr["IdSII"].ToString();
                    tipoDocumento.Nombre = dr["TipoDocumento"].ToString();
                    tipoDocumento.Descripcion = dr["Descripcion"].ToString();
                    tipoDocumento.Vigente = dr["Vigente"].ToString();

                    lstTipoDocumento.Add(tipoDocumento);
                }

                foreach (DataRow dr in dsBanco.Tables[0].Rows)
                {
                    Models.Banco banco = new Models.Banco();

                    banco.Code = dr["Code"].ToString();
                    banco.Message = dr["Message"].ToString();
                    banco.Id = dr["Id"].ToString();
                    banco.Nombre = dr["Nombre"].ToString();
                    banco.Vigente = dr["Vigente"].ToString();

                    lstBanco.Add(banco);
                }

                foreach (DataRow dr in dsTipoReembolso.Tables[0].Rows)
                {
                    Models.TipoReembolso tipoReembolso = new Models.TipoReembolso();

                    tipoReembolso.Code = dr["Code"].ToString();
                    tipoReembolso.Message = dr["Message"].ToString();
                    tipoReembolso.Id = dr["IdTipoReembolso"].ToString();
                    tipoReembolso.Nombre = dr["Nombre"].ToString();
                    tipoReembolso.Descripcion = dr["Descripcion"].ToString();
                    tipoReembolso.Vigente = dr["Vigente"].ToString();

                    lstTipoReembolso.Add(tipoReembolso);
                }

                foreach (DataRow dr in dsTipoCuenta.Tables[0].Rows)
                {
                    Models.TipoCuenta tipoCuenta = new Models.TipoCuenta();

                    tipoCuenta.Code = dr["Code"].ToString();
                    tipoCuenta.Message = dr["Message"].ToString();
                    tipoCuenta.Id = dr["Id"].ToString();
                    tipoCuenta.Nombre = dr["Nombre"].ToString();
                    tipoCuenta.Vigente = dr["Vigente"].ToString();

                    lstTipoCuenta.Add(tipoCuenta);
                }

                foreach (DataRow dr in dsEmpresa.Tables[0].Rows)
                {
                    Models.Empresa empresa = new Models.Empresa();

                    empresa.Code = dr["Code"].ToString();
                    empresa.Message = dr["Message"].ToString();
                    empresa.Id = dr["Id"].ToString();
                    empresa.Nombre = dr["Nombre"].ToString();
                    empresa.Descripcion = dr["Descripcion"].ToString();
                    empresa.Vigente = dr["Vigente"].ToString();

                    lsEmpresa.Add(empresa);
                }

                foreach (DataRow dr in dsCliente.Tables[0].Rows)
                {
                    Models.Cliente cliente = new Models.Cliente();

                    cliente.Code = dr["Code"].ToString();
                    cliente.Message = dr["Message"].ToString();
                    cliente.Id = dr["Id"].ToString();
                    cliente.Codigo = dr["Codigo"].ToString();
                    cliente.Nombre = dr["Nombre"].ToString();
                    cliente.Rut = dr["Rut"].ToString();
                    cliente.Empresa = dr["Empresa"].ToString();
                    cliente.Vigente = dr["Vigente"].ToString();

                    lsCliente.Add(cliente);
                }

                ViewBag.RenderizadoConceptoGasto = lstConceptoGasto;
                ViewBag.RenderizadoEstado = lstEstado;
                ViewBag.RenderizadoTipoDocumento = lstTipoDocumento;
                ViewBag.RenderizadoBanco = lstBanco;
                ViewBag.RenderizadoReembolso = lstTipoReembolso;
                ViewBag.RenderizadoGasto = lstGasto;
                ViewBag.RenderizadoTipoCuenta = lstTipoCuenta;
                ViewBag.RenderizadoEmpresa = lsEmpresa;
                ViewBag.RenderizadoCliente = lsCliente;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AgregarGasto()
        {
            if (AplicationActive())
            {
                string[] parametrosHeadersCuentas =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR"
                };

                string[] valoresHeadersCuentas =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),

                };

                DataSet dsEmpresa = servicioAuth.GetObtenerEmpresas(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsTipoReembolso = servicioOperaciones.GetObtenerTipoReembolso(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsTipoDocumento = servicioOperaciones.GetObtenerTiposDocumentos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet dsProveedores = servicioOperaciones.GetObtenerProveedores(parametrosHeadersCuentas, valoresHeadersCuentas).Table;

                List<Models.Empresa> lsEmpresa = new List<Models.Empresa>();
                List<Models.TipoReembolso> lstTipoReembolso = new List<Models.TipoReembolso>();
                List<Models.TipoDocumento> lstTipoDocumentos = new List<Models.TipoDocumento>();
                List<Models.Proveedor> lstProveedor = new List<Models.Proveedor>();

                foreach (DataRow dr in dsProveedores.Tables[0].Rows)
                {
                    Models.Proveedor proveedor = new Models.Proveedor();

                    proveedor.Code = dr["Code"].ToString();
                    proveedor.Message = dr["Message"].ToString();
                    proveedor.Nombre = dr["Nombre"].ToString();
                    proveedor.Rut = dr["Rut"].ToString();
                    proveedor.Id = dr["Id"].ToString();
                    proveedor.Vigente = dr["Vigente"].ToString();

                    lstProveedor.Add(proveedor);
                }

                foreach (DataRow dr in dsEmpresa.Tables[0].Rows)
                {
                    Models.Empresa empresa = new Models.Empresa();

                    empresa.Code = dr["Code"].ToString();
                    empresa.Message = dr["Message"].ToString();
                    empresa.Codigo = dr["Codigo"].ToString();
                    empresa.Id = dr["Id"].ToString();
                    empresa.Nombre = dr["Nombre"].ToString();
                    empresa.Descripcion = dr["Descripcion"].ToString();
                    empresa.Vigente = dr["Vigente"].ToString();

                    lsEmpresa.Add(empresa);
                }

                foreach (DataRow dr in dsTipoReembolso.Tables[0].Rows)
                {
                    Models.TipoReembolso tipoReembolso = new Models.TipoReembolso();

                    tipoReembolso.Code = dr["Code"].ToString();
                    tipoReembolso.Message = dr["Message"].ToString();
                    tipoReembolso.Codigo = dr["Codigo"].ToString();
                    tipoReembolso.Id = dr["IdTipoReembolso"].ToString();
                    tipoReembolso.Nombre = dr["Nombre"].ToString();
                    tipoReembolso.Descripcion = dr["Descripcion"].ToString();
                    tipoReembolso.Vigente = dr["Vigente"].ToString();

                    lstTipoReembolso.Add(tipoReembolso);
                }

                foreach (DataRow dr in dsTipoDocumento.Tables[0].Rows)
                {
                    Models.TipoDocumento tipoDocumento = new Models.TipoDocumento();

                    tipoDocumento.Code = dr["Code"].ToString();
                    tipoDocumento.Message = dr["Message"].ToString();
                    tipoDocumento.Id = dr["IdTipoDocumento"].ToString();
                    tipoDocumento.IdSII = dr["IdSII"].ToString();
                    tipoDocumento.Nombre = dr["TipoDocumento"].ToString();
                    tipoDocumento.Descripcion = dr["Descripcion"].ToString();
                    tipoDocumento.Vigente = dr["Vigente"].ToString();

                    lstTipoDocumentos.Add(tipoDocumento);
                }

                ViewBag.RenderizadoProveedor = lstProveedor;
                ViewBag.RenderizadoReembolso = lstTipoReembolso;
                ViewBag.RenderizadoEmpresa = lsEmpresa;
                ViewBag.RenderizadoTipoDocumento = lstTipoDocumentos;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AdministrarPeriodos()
        {
            if (AplicationActive())
            {
                string domain = string.Empty;
                string prefixDomain = string.Empty;
                string domainReal = string.Empty;

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

                string[] paramPeriodo = new string[9];
                string[] valPeriodo = new string[9];

                List<Models.Periodo> periodos = new List<Models.Periodo>();

                paramPeriodo[0] = "@AUTHENTICATE";
                paramPeriodo[1] = "@AGENTAPP";
                paramPeriodo[2] = "@ACCION";
                paramPeriodo[3] = "@EMPRESA";
                paramPeriodo[4] = "@PERIODO";
                paramPeriodo[5] = "@VIGENTE";
                paramPeriodo[6] = "@FECHACIERRE";
                paramPeriodo[7] = "@FECHAAPERTURA";
                paramPeriodo[8] = "@CONFIRMACION";


                valPeriodo[0] = tokenAuth;
                valPeriodo[1] = agenteAplication;
                valPeriodo[2] = "S";
                valPeriodo[3] = "";
                valPeriodo[4] = "";
                valPeriodo[5] = "";
                valPeriodo[6] = "";
                valPeriodo[7] = "";
                valPeriodo[8] = "";


                DataSet dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

                foreach (DataRow item in dataPeriodo.Tables[0].Rows)
                {
                    Models.Periodo periodo = new Models.Periodo();

                    periodo.Empresa = item["Nombre"].ToString();
                    periodo.PeriodoVigente = item["Periodos"].ToString();
                    periodo.EmpresaCodificado = item["EmpresaCodificado"].ToString();

                    periodos.Add(periodo);
                }

                ViewBag.ListadoPeriodo = periodos;

                ViewBag.ConfirmacionPeriodoAbierto = "N";
                ViewBag.ViewMessageApplication = "";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AdministrarProveedor()
        {
            if (AplicationActive())
            {
                string domain = string.Empty;
                string prefixDomain = string.Empty;
                string domainReal = string.Empty;

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

                if (Session["MantencionProveedores"] == null)
                {
                    Session["PaginationProveedores"] = "MS01";
                }
                else
                {
                    if (!(bool)Session["MantencionProveedores"])
                    {
                        Session["PaginationProveedores"] = "MS01";
                    }
                }

                Session["MantencionProveedores"] = false;

                string[] paramProveedores = new string[8];
                string[] valProveedores = new string[8];
                List<Models.Proveedor> proveedores = new List<Models.Proveedor>();

                paramProveedores[0] = "@AUTHENTICATE";
                paramProveedores[1] = "@AGENTAPP";
                paramProveedores[2] = "@ACCION";
                paramProveedores[3] = "@NOMBRE";
                paramProveedores[4] = "@RUT";
                paramProveedores[5] = "@PAGINATION";
                paramProveedores[6] = "@TYPEFILTER";
                paramProveedores[7] = "@DATAFILTER";

                valProveedores[0] = tokenAuth;
                valProveedores[1] = agenteAplication;
                valProveedores[2] = "S";
                valProveedores[3] = "";
                valProveedores[4] = "";
                valProveedores[5] = Session["PaginationProveedores"].ToString();
                valProveedores[6] = "";
                valProveedores[7] = "";

                DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

                foreach (DataRow item in dataProveedores.Tables[0].Rows)
                {
                    Models.Proveedor proveedor = new Models.Proveedor();

                    proveedor.Rut = item["Rut"].ToString();
                    proveedor.Nombre = item["Nombre"].ToString();
                    proveedor.Codificar = domain + prefixDomain + "/Finanzas/Mantencion/RWRpdA==/" + item["Codificar"].ToString();
                    proveedor.CodificarD = domain + prefixDomain + "/Finanzas/Mantencion/RGVsZXRl/" + item["Codificar"].ToString();

                    proveedores.Add(proveedor);
                }

                ViewBag.ListadoProveedores = proveedores;

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
                valPaginator[3] = "Proveedores";
                valPaginator[4] = Session["PaginationProveedores"].ToString();
                valPaginator[5] = "";
                valPaginator[6] = "";
                valPaginator[7] = "";

                dataPaginator = servicioOperaciones.GetPaginations(paramPaginator, valPaginator).Table;

                foreach (DataRow rows in dataPaginator.Tables[0].Rows)
                {
                    Models.Teamwork.Pagination pagination = new Models.Teamwork.Pagination();

                    pagination.NumeroPagina = rows["NumeroPagina"].ToString();
                    pagination.Rango = rows["Rango"].ToString();
                    pagination.Class = rows["Class"].ToString();
                    pagination.Properties = rows["Properties"].ToString();

                    paginations.Add(pagination);
                }

                ViewBag.RenderizadoPaginations = paginations;

                #endregion
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Mantencion()
        {
            if (AplicationActive())
            {
                try
                {

                    string[] paramProveedores = new string[8];
                    string[] valProveedores = new string[8];

                    #region "PERMISOS Y ACCESOS"

                    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                    {
                        Session["ApplicationActive"] = null;
                    }

                    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
                    {
                        sistema = Request.Url.AbsoluteUri.Split('/')[3];
                        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                    }
                    else
                    {
                        sistema = fromDebugQA;
                        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
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

                    ViewBag.EventoMantencion = section;
                    Session["MantencionProveedores"] = true;

                    paramProveedores[0] = "@AUTHENTICATE";
                    paramProveedores[1] = "@AGENTAPP";
                    paramProveedores[2] = "@ACCION";
                    paramProveedores[3] = "@NOMBRE";
                    paramProveedores[4] = "@RUT";
                    paramProveedores[5] = "@PAGINATION";
                    paramProveedores[6] = "@TYPEFILTER";
                    paramProveedores[7] = "@DATAFILTER";

                    valProveedores[0] = tokenAuth;
                    valProveedores[1] = agenteAplication;
                    valProveedores[2] = "SI";
                    valProveedores[3] = "";
                    valProveedores[4] = schema;
                    valProveedores[5] = "";
                    valProveedores[6] = "";
                    valProveedores[7] = "";


                    DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

                    foreach (DataRow rows in dataProveedores.Tables[0].Rows)
                    {
                        ViewBag.NombreProveedor = rows["Nombre"].ToString();
                    }

                    ViewBag.Codificar = schema;




                    ViewBag.CodeCorrect = true;
                }
                catch (Exception ex)
                {
                    List<Models.Error> errores = new List<Models.Error>();
                    ViewBag.CodeCorrect = false;
                    Models.Error error = new Models.Error();

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

        public ActionResult Pagos()
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

                ViewBag.StageProcesoPago = "InitProcesoPago";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult GeneratePdf()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            string pdf = string.Empty;
            string data = string.Empty;

            pdf = Request.QueryString["pdf"].ToString();
            data = Request.QueryString["data"].ToString();

            return new ActionAsPdf("Pdf/" + pdf + "/" + data)
            {
                FileName = "Comprobante de Despacho " +
                DateTime.Now.Day + "" +
                DateTime.Now.Month + "" +
                DateTime.Now.Year + "" +
                DateTime.Now.Hour + "" +
                DateTime.Now.Minute + "" +
                DateTime.Now.Second + "" +
                DateTime.Now.Millisecond +
                ".pdf",
                PageMargins = new Rotativa.Options.Margins(5, 1, 5, 1),
                PageOrientation = Rotativa.Options.Orientation.Portrait,
                PageSize = Rotativa.Options.Size.Letter
            };
        }

        public ActionResult Pdf()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            #region "No utilizable"
            //List<Models.Finanzas.PlantillaCheque> cheques = new List<Models.Finanzas.PlantillaCheque>();
            //string[] paramMantFin = new string[21];
            //string[] valMantFin = new string[21];
            //DataSet data;

            //paramMantFin[0] = "@ACCION";
            //paramMantFin[1] = "@USUARIOCREADOR";
            //paramMantFin[2] = "@CODIGOFINIQUITO";
            //paramMantFin[3] = "@METODOPAGO";
            //paramMantFin[4] = "@CODIGOPAGO";
            //paramMantFin[5] = "@CODIGOPROCESO";
            //paramMantFin[6] = "@DESTINATARIO";
            //paramMantFin[7] = "@BANCO";
            //paramMantFin[8] = "@TIPOCUENTA";
            //paramMantFin[9] = "@FECHA";
            //paramMantFin[10] = "@NUMEROCUENTA";
            //paramMantFin[11] = "@RUT";
            //paramMantFin[12] = "@OBSERVACIONES";
            //paramMantFin[13] = "@MONTO";
            //paramMantFin[14] = "@VIGENTE";
            //paramMantFin[15] = "@SECUENCIA";
            //paramMantFin[16] = "@SERIECHQ";
            //paramMantFin[17] = "@EMPRESAORIGEN";
            //paramMantFin[18] = "@PAGINATION";
            //paramMantFin[19] = "@TYPEFILTER";
            //paramMantFin[20] = "@DATAFILTER";

            //switch (section)
            //{
            //    case "Cheque":
            //        valMantFin[0] = "PLTEMICHQ";
            //        valMantFin[1] = "";
            //        valMantFin[2] = "";
            //        valMantFin[3] = "";
            //        valMantFin[4] = "";
            //        valMantFin[5] = schema;
            //        valMantFin[6] = "";
            //        valMantFin[7] = "";
            //        valMantFin[8] = "";
            //        valMantFin[9] = "";
            //        valMantFin[10] = "";
            //        valMantFin[11] = "";
            //        valMantFin[12] = "";
            //        valMantFin[13] = "";
            //        valMantFin[14] = "";
            //        valMantFin[15] = "";
            //        valMantFin[16] = "";
            //        valMantFin[17] = "";
            //        valMantFin[18] = "";
            //        valMantFin[19] = "";
            //        valMantFin[20] = "";

            //        data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            //        foreach (DataRow rows in data.Tables[0].Rows)
            //        {
            //            Models.Finanzas.PlantillaCheque cheque = new Models.Finanzas.PlantillaCheque();

            //            cheque.Empresa = rows["Empresa"].ToString();
            //            cheque.Comprobante = rows["Comprobante"].ToString();

            //            using (MemoryStream ms = new MemoryStream())
            //            {
            //                var barcode2 = new Barcode(rows["Folio"].ToString(), TypeBarcode.Code39);
            //                var bar128 = barcode2.Encode(TypeBarcode.Code39, rows["Folio"].ToString(), 300, 100);
            //                bar128.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //                cheque.Base64Barcode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            //            }

            //            cheque.Monto = rows["Monto"].ToString();
            //            cheque.Ciudad = rows["Ciudad"].ToString();
            //            cheque.Dia = rows["Dia"].ToString();
            //            cheque.Mes = rows["Mes"].ToString();
            //            cheque.Year = rows["Year"].ToString();
            //            cheque.Beneficiario = rows["Beneficiario"].ToString();
            //            cheque.CifraFirst = rows["CifraFirst"].ToString();
            //            cheque.CifraSecond = rows["CifraSecond"].ToString();
            //            cheque.Protegido = rows["Protegido"].ToString();

            //            cheques.Add(cheque);
            //        }

            //        ViewBag.RenderizadoPlantillaCheques = cheques;

            //        break;
            //    case "ComprobanteDespacho":
            //        ViewBag.RenderizadoComprobanteDespacho = ModuleDatosDespacho(schema);

            //        foreach (Models.Finanzas.DatosDespacho item in ViewBag.RenderizadoComprobanteDespacho)
            //        {
            //            using (MemoryStream ms = new MemoryStream())
            //            {
            //                var barcode2 = new Barcode(item.CodigoDespacho, TypeBarcode.Code39);
            //                var bar128 = barcode2.Encode(TypeBarcode.Code39, item.CodigoDespacho, 1000, 100);
            //                bar128.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //                ViewBag.Base64Barcode = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            //            }
            //        }

            //        break;
            //}

            //ViewBag.Pdf = section;
            //return View();
            #endregion

            string pdf = string.Empty;
            string data = string.Empty;
            byte[] pdfbyte = null;

            pdf = Request.QueryString["pdf"].ToString();
            data = Request.QueryString["data"].ToString();


            switch (pdf)
            {
                case "EstructuraRenta":
                    var generator = new NReco.PdfGenerator.HtmlToPdfConverter();

                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    dynamic objectsPDF = JsonConvert.DeserializeObject(Teamwork.WebApi.Teanwork.PDF.__RemuneracionesEstructuraCargoMod(data, Session["base64Username"].ToString(), objects[0].Token.ToString()));

                    for (var i = 0; i < objectsPDF.Count; i++)
                    {
                        pdfbyte = generator.GeneratePdf(objectsPDF[i].Html.ToString());
                    }

                    break;
            }

            //dynamic html = "<div style='width: 21.7cm; height: 28cm; page-break-before: always;'> <div style='width: 21.7cm; display: inline-block; margin-top: 0.6cm; border: 1px solid rgba(0, 0, 0, 0); height: 21cm;'> <div style=' margin-top: 7cm; float: right;'> <h1 style='font-family: Arial; font-size: 15px;'>GENERACION DE CHEQUE PARA PAGO FINIQUITO </h1> <p style='font-family: Arial; font-size: 11px;'> PARA: SEBASTIAN SALAS <br/> POR EL MONTO: $ 23.993 <br/> EL DIA: 01-10-2021 Horas. <br/><br/> </p></div></div><div style='height: 7cm; float: left; width: 21.7cm;'> <div style='width: 5.6cm; display: inline-block; height: 7cm; vertical-align: top;'> <p style='font-family: Arial; font-size: 0.2cm; margin-top: 2cm; width: 6cm; margin-left: 0.5cm;'> PARA: SEBASTIAN SALAS <br/> POR EL MONTO: $ 23.993 <br/> EL DIA: 01-10-2021 Horas. <br/><br/> </p></div><div style='width: 16cm; display: inline-block; height: 7cm; vertical-align: top;'> <p style='font-family: Arial; font-size: 0.3cm; height: 0.7cm; border: 1px solid rgba(0, 0, 0, 0); margin-left: 10.1cm; margin-top: 0.3cm; width: 5.7cm;'> <span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm;'>2</span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'>3</span>.<span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'>9</span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'>9</span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'>3</span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'>=</span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'></span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'></span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'></span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'></span><span style='width: 0.5cm; display: inline-block; text-align: center; padding-top: 0.1cm; padding-bottom: 0.1cm; margin-left: -0.06cm;'></span> </p><p style='font-family: Arial; width: 6.8cm; margin-left: 9cm; margin-top: 0.2cm; border: 1px solid rgba(0, 0, 0, 0); height: 0.7cm;'> <span style='width: 2cm; display: inline-block; font-size: 0.3cm; text-align: center;'>Stgo</span> <span style='width: 1cm; display: inline-block; margin-left: 0.2cm; font-size: 0.3cm;'> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>1</span> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>9</span> </span> <span style='width: 1cm; display: inline-block; margin-left: 0.2cm; font-size: 0.3cm;'> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>0</span> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>1</span> </span> <span style='width: 1.9cm; margin-left: 0.2cm; display: inline-block; font-size: 0.3cm;'> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>2</span> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>0</span> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>2</span> <span style='width: 0.4cm; display: inline-block; text-align: center; padding-top: 0.2cm; padding-bottom: 0.1cm;'>2</span> </span> </p><p style='font-family: Arial; font-size: 0.3cm; border: 1px solid rgba(0, 0, 0, 0); margin-left: 2.4cm; margin-top: 0cm; width: 12.9cm; height: 0.4cm;'> <span style='display: inline-block; width: 1.4cm; height: 0.2cm; background-color: rgb(0, 0, 0); margin-top: 0.2cm; position: absolute; margin-left: -1.5cm;'></span> SEBASTIAN SALAS </p><p style='font-family: Arial; font-size: 0.3cm; border: 1px solid rgba(0, 0, 0, 0); margin-left: 2.4cm; margin-top: -0.1cm; width: 12.9cm; height: 0.4cm;'> VEINTITRES MIL NOVECIENTOS NOVENTA Y TRES=====</p><p style='font-family: Arial; font-size: 0.3cm; border: 1px solid rgba(0, 0, 0, 0); margin-left: 1.2cm; margin-top: -0.1cm; width: 12.9cm; height: 0.4cm;'> </p><p style='margin-top: 1cm; margin-left: 3.5cm; width: 9.6cm; font-size: 0.6cm; border: 1px solid rgba(0, 0, 0, 0); font-weight: bold; font-family: Arial; text-align: left;'> $ 2 3 . 9 9 3=</p></div></div></div>";

            // var generator = new NReco.PdfGenerator.HtmlToPdfConverter();
            //pdfbyte = generator.GeneratePdf(html);

            return new FileContentResult(pdfbyte, "application/pdf");
        }

        public ActionResult ViewPdf()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            string pdf = string.Empty;
            string data = string.Empty;
            byte[] pdfbyte = null;

            pdf = Request.QueryString["pdf"].ToString();
            data = Request.QueryString["data"].ToString();

            //ActionAsPdf pdfAction = null;

            switch (pdf)
            {
                case "Cheque":
                    //pdfAction = new ActionAsPdf("Pdf/" + pdf + "/" + data)
                    //{
                    //    PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                    //    PageOrientation = Rotativa.Options.Orientation.Portrait,
                    //    PageSize = Rotativa.Options.Size.Letter,
                    //    CustomSwitches = "--zoom 1.2"
                    //};
                    //break;
                case "ComprobanteDespacho":
                    //pdfAction = new ActionAsPdf("Pdf/" + pdf + "/" + data)
                    //{
                    //    PageMargins = new Rotativa.Options.Margins(5, 1, 5, 1),
                    //    PageOrientation = Rotativa.Options.Orientation.Portrait,
                    //    PageSize = Rotativa.Options.Size.Letter
                    //};
                    //break;
                case "EstructuraRenta":
                    var generator = new NReco.PdfGenerator.HtmlToPdfConverter();

                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    dynamic objectsPDF = JsonConvert.DeserializeObject(Teamwork.WebApi.Teanwork.PDF.__RemuneracionesEstructuraCargoMod(data, Session["base64Username"].ToString(), objects[0].Token.ToString()));

                    for (var i = 0; i < objectsPDF.Count; i++)
                    {
                        pdfbyte = generator.GeneratePdf(objectsPDF[i].Html.ToString());
                    }

                    break;
            }

            return new FileContentResult(pdfbyte, "application/pdf");
        }
        
        public ActionResult Despacho()
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

                ViewBag.ReferenciaInformacionDespacho = "";
                ViewBag.ReferenciaHtmlMessage = "";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }
        
        public ActionResult GenerateExcel()
        {
            if (AplicationActive())
            {
                string domain = string.Empty;
                string prefixDomain = string.Empty;
                string domainReal = string.Empty;
                string[] columnas = null;
                string[] nameColumnas = null;
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                string[] paramGastos = new string[11];
                string[] valGastos = new string[11];
                string nombreWorksheet = string.Empty;
                DataSet data;

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

                switch (Request.QueryString["excel"])
                {
                    case "UmVwb3J0ZVBhZ28=":
                        #region "PROCESO PAGO"

                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            columnas = new string[15];
                            nameColumnas = new string[15];

                            switch (Request.QueryString["type"])
                            {
                                case "TEF":
                                    nombreWorksheet = "TEF Emitidas ";
                                    break;
                                case "CHQ":
                                    nombreWorksheet = "Cheques Emitidos ";
                                    break;
                            }

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

                            valMantFin[0] = "XLSXPAGOS";
                            valMantFin[1] = Session["base64Username"].ToString();
                            valMantFin[2] = "";
                            valMantFin[3] = "";
                            valMantFin[4] = "";
                            valMantFin[5] = Request.QueryString["Proceso"];
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

                            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                            foreach (DataRow rows in data.Tables[0].Rows)
                            {
                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == nombreWorksheet + rows["EmpresaTw"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add(nombreWorksheet + rows["EmpresaTw"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets[nombreWorksheet + rows["EmpresaTw"].ToString()];

                                    columnas[0] = "A";
                                    columnas[1] = "B";
                                    columnas[2] = "C";
                                    columnas[3] = "D";
                                    columnas[4] = "E";
                                    columnas[5] = "F";
                                    columnas[6] = "G";
                                    columnas[7] = "H";
                                    columnas[8] = "I";
                                    columnas[9] = "J";
                                    columnas[10] = "K";
                                    columnas[11] = "L";
                                    columnas[12] = "M";
                                    columnas[13] = "N";
                                    columnas[14] = "O";

                                    nameColumnas[0] = "Folio";
                                    nameColumnas[1] = "Area";
                                    nameColumnas[2] = "Nombre";
                                    nameColumnas[3] = "Vacaciones";
                                    nameColumnas[4] = "Mes de Aviso";
                                    nameColumnas[5] = "IAS";
                                    nameColumnas[6] = "Rem. Pendiente";
                                    nameColumnas[7] = "Indemnización Voluntaria";
                                    nameColumnas[8] = "Indemnización Lucro Cesante";
                                    nameColumnas[9] = "Monto Finiquito";
                                    nameColumnas[10] = "Pagado";
                                    nameColumnas[11] = "Cheque";
                                    nameColumnas[12] = "Empresa";
                                    nameColumnas[13] = "GASTOS";
                                    nameColumnas[14] = "Ficha";

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets[nombreWorksheet + rows["EmpresaTw"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["Folio"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["AreaNegocio"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["Beneficiario"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["FeriadoProporcional"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["MesAviso"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["IAS"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["RemPendiente"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["IndemnizacionVoluntaria"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["IndemnizacionLucroCesante"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["MontoFiniquito"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["TotalPago"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["Nomenclatura"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["RolPrivado"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["GastoAdministrativo"].ToString();
                                worksheet.Cells["O" + contador.ToString()].Value = rows["Ficha"].ToString();

                                worksheet.Cells[columnas[0] + ":" + columnas[columnas.Length - 1]].AutoFitColumns();
                            }

                            excel.Workbook.Properties.Title = "Attempts";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", nombreWorksheet + " proceso de pago de finiquito.xlsx"));
                            Response.ContentType = "application/excel";
                            Response.BinaryWrite(excel.GetAsByteArray());
                            Response.Flush();
                            Response.End();
                        }
                        #endregion
                        break;
                    case "ReporteGastos":
                        #region "REPORTE GASTOS"
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            columnas = new string[13];
                            nameColumnas = new string[13];

                            paramGastos[0] = "@ACCION";
                            paramGastos[1] = "@PAGINATION";
                            paramGastos[2] = "@TYPEFILTER";
                            paramGastos[3] = "@DATAFILTER";
                            paramGastos[4] = "@NUMERODOCUMENTO";
                            paramGastos[5] = "@TOKEN";
                            paramGastos[6] = "@CONFIRMACION";
                            paramGastos[7] = "@IDPROVEEDOR";
                            paramGastos[8] = "@IDTIPODOC";
                            paramGastos[9] = "@IDEMPRESA";
                            paramGastos[10] = "@DESCRIPCION";

                            valGastos[0] = "REPORT";
                            valGastos[1] = "";
                            valGastos[2] = "";
                            valGastos[3] = Request.QueryString["fechaIni"] + "@" + Request.QueryString["fechaTerm"];
                            valGastos[4] = "";
                            valGastos[5] = "";
                            valGastos[6] = "";
                            valGastos[7] = "";
                            valGastos[8] = "";
                            valGastos[9] = Request.QueryString["empresa"];
                            valGastos[10] = "";


                            data = servicioOperaciones.CrudMantencionGastos(paramGastos, valGastos).Table;

                            if (data.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow rows in data.Tables[0].Rows)
                                {
                                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Gastos " + rows["TipoDocumento"].ToString()))
                                    {
                                        excel.Workbook.Worksheets.Add("Gastos " + rows["TipoDocumento"].ToString());
                                        var worksheetConfig = excel.Workbook.Worksheets["Gastos " + rows["TipoDocumento"].ToString()];

                                        columnas[0] = "A";
                                        columnas[1] = "B";
                                        columnas[2] = "C";
                                        columnas[3] = "D";
                                        columnas[4] = "E";
                                        columnas[5] = "F";
                                        columnas[6] = "G";
                                        columnas[7] = "H";
                                        columnas[8] = "I";
                                        columnas[9] = "J";
                                        columnas[10] = "K";
                                        columnas[11] = "L";
                                        columnas[12] = "M";


                                        nameColumnas[0] = "Codigo";
                                        nameColumnas[1] = "Descripcion";
                                        nameColumnas[2] = "Periodo";
                                        nameColumnas[3] = "Fecha Rendicion";
                                        nameColumnas[4] = "Monto Neto";
                                        nameColumnas[5] = "Tipo Documento";
                                        nameColumnas[6] = "Numero Documento";
                                        nameColumnas[7] = "Nombre Cliente";
                                        nameColumnas[8] = "Area Negocio";
                                        nameColumnas[9] = "Empresa";
                                        nameColumnas[10] = "Nombre Proveedor";
                                        nameColumnas[11] = "Rut Proveedor";
                                        nameColumnas[12] = "Tipo Gasto";




                                        for (var i = 0; i < columnas.Length; i++)
                                        {
                                            worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                            worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                        }

                                    }

                                    var worksheet = excel.Workbook.Worksheets["Gastos " + rows["TipoDocumento"].ToString()];
                                    int contador = worksheet.Dimension.End.Row + 1;


                                    worksheet.Cells["A" + contador.ToString()].Value = rows["Codigo"].ToString();
                                    worksheet.Cells["B" + contador.ToString()].Value = rows["Descripcion"].ToString();
                                    worksheet.Cells["C" + contador.ToString()].Value = rows["Periodo"].ToString();
                                    worksheet.Cells["D" + contador.ToString()].Value = rows["FRendicion"].ToString();
                                    worksheet.Cells["E" + contador.ToString()].Value = rows["MontoNeto"].ToString();
                                    worksheet.Cells["F" + contador.ToString()].Value = rows["TipoDocumento"].ToString();
                                    worksheet.Cells["G" + contador.ToString()].Value = rows["NumeroDocumento"].ToString();
                                    worksheet.Cells["H" + contador.ToString()].Value = rows["NombreCli"].ToString();
                                    worksheet.Cells["I" + contador.ToString()].Value = rows["CodeCli"].ToString();
                                    worksheet.Cells["J" + contador.ToString()].Value = rows["CodeEmpresa"].ToString();
                                    worksheet.Cells["K" + contador.ToString()].Value = rows["Proveedor"].ToString();
                                    worksheet.Cells["L" + contador.ToString()].Value = rows["RutProveedor"].ToString();
                                    worksheet.Cells["M" + contador.ToString()].Value = rows["TipoRembolso"].ToString();


                                    worksheet.Cells.AutoFitColumns();




                                }



                                excel.Workbook.Properties.Title = "Attempts";
                                Response.ClearContent();
                                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Gastos.xlsx"));
                                Response.ContentType = "application/excel";
                                Response.BinaryWrite(excel.GetAsByteArray());
                                Response.Flush();
                                Response.End();
                            }
                            else
                            {
                                Response.Redirect(ModuleControlRetornoPath() + "/Finanzas/AdministrarGastos");
                            }






                        }

                        #endregion
                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AdministrarChequera()
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

                ViewBag.RenderizadoIndicesCheques = ModuleIndices();
                ViewBag.RenderizadoCuentasOrigen = ModuleCuentasOrigen("MS01", "", "");
                ViewBag.RenderizadoPaginations = ModulePagination("CuentasOrigen", "MS01", "", "");
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Procesos()
        {
            if (AplicationActive())
            {
                ViewBag.ReturnProcesos = ModuleControlRetornoPath() + "/Finanzas/Procesos";
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "Procesos":
                        //ViewBag.RenderizadoSolicitudesTEF = ModuleSolicitudesTEF();
                        ViewBag.RenderizadoProcesosPago = ModuleProcesosPago();
                        ViewBag.RenderizadoPaginationProcesosPago = ModulePagination("ProcesosPago", "MS01");
                        ViewBag.RenderizadoView = "All";
                        break;
                    case "PagoMasivo":
                        valMantFin[0] = "VERINITPGMASIVO";
                        valMantFin[1] = Session["base64Username"].ToString();
                        valMantFin[2] = "";
                        valMantFin[3] = "";
                        valMantFin[4] = "";
                        valMantFin[5] = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
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

                        data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                        foreach (DataRow rows in data.Tables[0].Rows)
                        {
                            switch (rows["Code"].ToString())
                            {
                                case "200":
                                    ViewBag.RenderizadoView = "SuccessProcesoMasivoPago";
                                    ViewBag.CodigoProceso = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                                    ViewBag.RenderizadoPagosMasivosInc = ModulePagosMasivoTEFInc(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizadoPagosMasivosExc = ModulePagosMasivoTEFExc(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    break;
                                case "404":
                                    ViewBag.RenderizadoView = "ErrorProcesoMasivoPago";
                                    break;
                            }
                        }
                        ViewBag.RenderizadoProcesosPago = ModuleProcesosPago("MS01", "UHJvY2Vzbw==", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        break;
                    default:
                        ViewBag.RenderizadoProcesosPago = ModuleProcesosPago("MS01", "UHJvY2Vzbw==", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                        ViewBag.RenderizadoSolicitudesTEF = ModuleSolicitudesTEF(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                        ViewBag.CodigoProceso = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                        ViewBag.RenderizadoView = "Detalle";
                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult CargoMod()
        {
            if (AplicationActive())
            {
                ViewBag.OptionCargoMod = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/RQ==/Create";
                ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/Uw==/Create";

                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "CargoMod":
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.GlosaOrigenDashboard = "Solicitudes en Validación";
                        ViewBag.TagSelected = "CargoMod";
                        break;
                    case "Create":
                        ViewBag.EventoCreacion = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                        break;
                    case "Edit":
                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
                        dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ValidaStageActual(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2], objects[0].Token.ToString()));

                        for (var i = 0; i < objectsCM.Count; i++)
                        {
                            switch (objectsCM[i].Code.ToString())
                            {
                                case "200":
                                    ViewBag.OptionCargoMod = "Edit";
                                    ViewBag.OptionStageCargoMod = "OK";
                                    ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
                                    ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                                    ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
                                    ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                                    ViewBag.HtmlMessageResult = "";

                                    break;
                                case "201":
                                    ViewBag.OptionCargoMod = "Edit";
                                    ViewBag.OptionStageCargoMod = "Send";
                                    ViewBag.HtmlMessageResult = objectsCM[i].Message.ToString();
                                    break;
                            }
                        }

                        break;
                    case "Detail":
                        ViewBag.OptionCargoMod = "Detail";
                        ViewBag.OptionStageCargoMod = "OK";
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
                        ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                        ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
                        break;
                    default:

                        switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2])
                        {
                            case "Estructura":
                                ViewBag.OptionCargoMod = "Estructura";
                                ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                                ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                                ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                                ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]);
                                break;
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

        public ActionResult Data()
        {
            if (AplicationActive())
            {
                ViewBag.RenderizadoValoresDiarios = ModuleValoresDiarios();
                ViewBag.RenderizadoClientes = ModuleClientes("", "", "", "", Session["base64Username"].ToString());
                ViewBag.RenderizadoPaginations = ModulePagination("ValorDiario", "MS01");

                ViewBag.OptionEnabledData = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", "P");
                ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", "G");
                ViewBag.Empresa = "TWEST";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult SeguroCovid()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        #region "HTTPOST UPDATE GASTOS"

        [HttpPost]
        public JsonResult MantenedorGastosX(string acciones, string IdNumDoc, string idproveedor, string IdEmpresa, 
                                            string IdTipoDoc,string token, string descripcion)
        {
            string[] paramGastos = new string[11];
            string[] valGastos = new string[11];
            List<Models.Gasto> gastos = new List<Models.Gasto>();
            DataSet data;
            string code = string.Empty;
            string message = string.Empty;
            string mensaje = string.Empty;
            string[] paramError = new string[13];
            string[] valoresError = new string[13];

            paramGastos[0] = "@ACCION";
            paramGastos[1] = "@PAGINATION";
            paramGastos[2] = "@TYPEFILTER";
            paramGastos[3] = "@DATAFILTER";
            paramGastos[4] = "@NUMERODOCUMENTO";
            paramGastos[5] = "@TOKEN";
            paramGastos[6] = "@CONFIRMACION";
            paramGastos[7] = "@IDPROVEEDOR";
            paramGastos[8] = "@IDTIPODOC";
            paramGastos[9] = "@IDEMPRESA";
            paramGastos[10] = "@DESCRIPCION";

            
            paramError[0] = "@AUTHENTICATE";
            paramError[1] = "@AGENTAPP";
            paramError[2] = "@SISTEMA";
            paramError[3] = "@AMBIENTE";
            paramError[4] = "@MODULO";
            paramError[5] = "@TIPOERROR";
            paramError[6] = "@EXCEPCION";
            paramError[7] = "@ERRORLINE";
            paramError[8] = "@ERRORMESSAGE";
            paramError[9] = "@ERRORNUMBER";
            paramError[10] = "@ERRORPROCEDURE";
            paramError[11] = "@ERRORSEVERITY";
            paramError[12] = "@ERRORSTATE";



            try
            {
                

                valGastos[0] = acciones;
                valGastos[1] = "";
                valGastos[2] = "";
                valGastos[3] = "";
                valGastos[4] = IdNumDoc;
                valGastos[5] = token;
                valGastos[6] = "0";
                valGastos[7] = idproveedor;
                valGastos[8] = IdTipoDoc;
                valGastos[9] = IdEmpresa;
                valGastos[10] = descripcion;

                data = servicioOperaciones.CrudMantencionGastos(paramGastos, valGastos).Table;


                mensaje = mensaje + "<div style='height: 100px;overflow-y: scroll;width: 50%;margin: auto;'><table border='1' cellspacing='0' cellpadding='5' style='margin: auto; width: 100%;'><thead><tr><td>Código</td></tr></thead><tbody>";

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    mensaje = mensaje + "<tr><td>" + rows["Codigo"].ToString() + "</td></tr>";
                }

                mensaje = mensaje + "</tbody></table></div>";


            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;



                valoresError[0] = tokenAuth;
                valoresError[1] = agenteAplication;
                valoresError[2] = sistema;
                valoresError[3] = ambiente;
                valoresError[4] = "HttPost | JsonResult | MantenedorGastos";
                valoresError[5] = "Aplicacion";
                valoresError[6] = ex.Message;
                valoresError[7] = "";
                valoresError[8] = "";
                valoresError[9] = "";
                valoresError[10] = "";
                valoresError[11] = "";
                valoresError[12] = "";

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }
            }

            return Json(mensaje);
        }
        #endregion

        #region "MODULOS DE SECUENCIA DE CODIGO"

        private void ModulePaginationGastos(string action, string paginationIndex, string typeFilter = "", string dataFilter = "",
                                            string typePagination  = ""   )

        {
            #region "GASTOS"
            //Antes de modificar por alonso son 4 parameros 09-01-2019
            string[] paramGastos = new string[11];
            string[] valGastos = new string[11];
            List<Models.Gasto> gastos = new List<Models.Gasto>();
            DataSet data;

            paramGastos[0] = "@ACCION";
            paramGastos[1] = "@PAGINATION";
            paramGastos[2] = "@TYPEFILTER";
            paramGastos[3] = "@DATAFILTER";
            paramGastos[4] = "@NUMERODOCUMENTO";
            paramGastos[5] = "@TOKEN";
            paramGastos[6] = "@CONFIRMACION";
            paramGastos[7] = "@IDPROVEEDOR";
            paramGastos[8] = "@IDTIPODOC";
            paramGastos[9] = "@IDEMPRESA";
            paramGastos[10] = "@DESCRIPCION";


            valGastos[0] = action;
            valGastos[1] = paginationIndex;
            valGastos[2] = typeFilter;
            valGastos[3] = dataFilter;
            valGastos[4] = "";
            valGastos[5] = "0";
            valGastos[6] = "0";
            valGastos[7] = "";
            valGastos[8] = "";
            valGastos[9] = "";
            valGastos[10] = "";


            data = servicioOperaciones.CrudMantencionGastos(paramGastos, valGastos).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Gasto gasto = new Models.Gasto();

                gasto.Periodo = rows["Periodo"].ToString();
                gasto.Codigo = rows["Codigo"].ToString();
                gasto.TipoDocumento = rows["TipoDocumento"].ToString();
                gasto.Proveedor = rows["Proveedor"].ToString();
                gasto.MontoNeto = rows["MontoNeto"].ToString();
                gasto.NumeroDocumento = rows["NumeroDocumento"].ToString();
                gasto.OptModificar = rows["OpcionModificar"].ToString();
                gasto.OptBorrar = rows["OpcionBorrar"].ToString();
                gasto.IdProveedor = rows["IdProveedor"].ToString();
                gasto.IdTipoDocumento = rows["IdTipoDocumento"].ToString();
                gasto.IdEmpresa = rows["IdEmpresa"].ToString();
                gasto.Token = rows["Token"].ToString();
                gasto.Descripcion = rows["Descripcion"].ToString();
                gasto.RutProveedor = rows["RutProveedor"].ToString();
                




                gastos.Add(gasto);
            }

            ViewBag.RenderizadoGasto = gastos;

            #endregion

            #region "PAGINATION"
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
            valPaginator[5] = "";
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

            ViewBag.RenderizadoPaginations = paginations;

            #endregion
        }

        #endregion

        #region "HTTPPOST AdministradorGastos"

        [HttpPost]
        public ActionResult Gastos(string pagination, string typeFilter = "", string dataFilter = "")
        {
            try
            {
                ViewBag.ApplicationProblem = false;
                ModulePaginationGastos("MOSGAST", pagination, typeFilter, dataFilter, "Gastos");
            }
            catch (Exception)
            {
                ViewBag.ApplicationProblem = true;
            }

            return PartialView("Finanzas/_Gastos");
        }

        #endregion
        
        #region HTTPPost

        [HttpPost]
        public JsonResult CargaGasto(string idCliente, string idEmpresa, string idConcepto, string idTipoDocumento, string numeroDocumento, string rutProveedor, string montoNeto,
                                     string fechaRendicion, string comentario, string idTipoBanco, string idTipoCuenta, string numeroCuenta)
        {

            string code = string.Empty;
            string message = string.Empty;
            string totalProcesado = string.Empty;

            #region "DATOS PARA CONTROL DE ERRORES"
            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                sistema = Request.Url.AbsoluteUri.Split('/')[3];
                if (!Request.Url.AbsoluteUri.Split('/')[3].Contains("QA"))
                {
                    ambiente = "Release";
                }
                else
                {
                    ambiente = "Quality";
                }
            }
            else
            {
                sistema = fromDebugQA;
                ambiente = "Debug";
            }
            #endregion

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] parametrosCargaGasto =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@IDUSUARIO",
                        "@IDCLIENTE",
                        "@IDEMPRESA",
                        "@IDCONCEPTO",
                        "@IDTIPODOCUMENTO",
                        "@NUMERODOCUMENTO",
                        "@RUTPROVEEDOR",
                        "@MONTOBRUTO",
                        "@FECHARENDICION",
                        "@DESCRIPCION",
                        "@IDBANCO",
                        "@IDTIPOCUENTA",
                        "@NUMEROCUENTA"
                    };


                    string[] valoresCargaGasto =
                   {
                        tokenAuth,
                        agenteAplication,
                        "",
                        "50",
                        idCliente,
                        idEmpresa,
                        idConcepto,
                        idTipoDocumento,
                        numeroDocumento,
                        rutProveedor,
                        montoNeto,
                        fechaRendicion,
                        comentario,
                        idTipoBanco,
                        idTipoCuenta,
                        numeroCuenta
                    };

                    DataSet dataGasto = servicioOperaciones.SetGasto(parametrosCargaGasto, valoresCargaGasto).Table;

                    foreach (DataRow rowsProceso in dataGasto.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() != "500")
                        {
                            if (rowsProceso["Code"].ToString() == "200")
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();

                            }
                            else
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();

                            }
                        }
                        else
                        {
                            code = "500";
                            message = errorSistema;

                            string[] paramError =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@SISTEMA",
                                "@AMBIENTE",
                                "@MODULO",
                                "@TIPOERROR",
                                "@EXCEPCION",
                                "@ERRORLINE",
                                "@ERRORMESSAGE",
                                "@ERRORNUMBER",
                                "@ERRORPROCEDURE",
                                "@ERRORSEVERITY",
                                "@ERRORSTATE"
                            };

                            string[] valoresError =
                            {
                                tokenAuth,
                                agenteAplication,
                                sistema,
                                ambiente,
                                "HttPost | JsonResult | CargaGasto",
                                "Procedimiento Almacenado",
                                "",
                                rowsProceso["ErrorLine"].ToString(),
                                rowsProceso["ErrorMessage"].ToString(),
                                rowsProceso["ErrorNumber"].ToString(),
                                rowsProceso["ErrorProcedure"].ToString(),
                                rowsProceso["ErrorSeverity"].ToString(),
                                rowsProceso["ErrorState"].ToString()
                            };


                            DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                            foreach (DataRow rowsError in dataError.Tables[0].Rows)
                            {
                                servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                                         rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                                         rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                            }

                        }
                    }
                }
                else
                {
                    code = "600";
                }

            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;

                string[] paramError =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@SISTEMA",
                    "@AMBIENTE",
                    "@MODULO",
                    "@TIPOERROR",
                    "@EXCEPCION",
                    "@ERRORLINE",
                    "@ERRORMESSAGE",
                    "@ERRORNUMBER",
                    "@ERRORPROCEDURE",
                    "@ERRORSEVERITY",
                    "@ERRORSTATE"
                };

                string[] valoresError =
                {
                    tokenAuth,
                    agenteAplication,
                    sistema,
                    ambiente,
                    "HttPost | JsonResult | CargaGasto",
                    "Aplicacion",
                    ex.Message,
                    "", "", "", "", "", ""
                };

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }

            }

            return Json(new { Code = code, Message = message, TotalProcesado = totalProcesado });
        }

        [HttpPost]
        public JsonResult CargaMasiva(string data, string codigoTransaccion, string plantilla, string lengthProcesamiento, string templateColumn)
        {

            string code = string.Empty;
            string message = string.Empty;
            string totalProcesado = string.Empty;

            #region "DATOS PARA CONTROL DE ERRORES"
            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                sistema = Request.Url.AbsoluteUri.Split('/')[3];
                if (!Request.Url.AbsoluteUri.Split('/')[3].Contains("QA"))
                {
                    ambiente = "Release";
                }
                else
                {
                    ambiente = "Quality";
                }
            }
            else
            {
                sistema = fromDebugQA;
                ambiente = "Debug";
            }
            #endregion

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] parametrosCargaMasiva =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@DATA",
                        "@CODIGOTRANSACCION",
                        "@EJECUTIVOPROCESO",
                        "@PLANTILLAMASIVA",
                        "@LENGTHPROCESAMIENTO",
                        "@TEMPLATECOLUMN"
                    };

                    string[] valoresCargaMasiva =
                    {
                        tokenAuth,
                        agenteAplication,
                        data.Replace(@"'", @"""").Replace("&", "@@@@"),
                        codigoTransaccion,
                        Session["NombreUsuario"].ToString(),
                        plantilla,
                        lengthProcesamiento,
                        templateColumn
                    };

                    DataSet dataProceso = servicioOperaciones.SetProcesoMasivo(parametrosCargaMasiva, valoresCargaMasiva).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() != "500")
                        {
                            if (rowsProceso["Code"].ToString() == "200")
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();
                                totalProcesado = rowsProceso["TotalProcesado"].ToString();
                            }
                            else
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();
                                totalProcesado = "";
                            }
                        }
                        else
                        {
                            code = "500";
                            message = errorSistema;

                            string[] paramError =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@SISTEMA",
                                "@AMBIENTE",
                                "@MODULO",
                                "@TIPOERROR",
                                "@EXCEPCION",
                                "@ERRORLINE",
                                "@ERRORMESSAGE",
                                "@ERRORNUMBER",
                                "@ERRORPROCEDURE",
                                "@ERRORSEVERITY",
                                "@ERRORSTATE"
                            };

                            string[] valoresError =
                            {
                                tokenAuth,
                                agenteAplication,
                                sistema,
                                ambiente,
                                "HttPost | JsonResult | CargaMasiva",
                                "Procedimiento Almacenado",
                                "",
                                rowsProceso["ErrorLine"].ToString(),
                                rowsProceso["ErrorMessage"].ToString(),
                                rowsProceso["ErrorNumber"].ToString(),
                                rowsProceso["ErrorProcedure"].ToString(),
                                rowsProceso["ErrorSeverity"].ToString(),
                                rowsProceso["ErrorState"].ToString()
                            };

                            DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                            foreach (DataRow rowsError in dataError.Tables[0].Rows)
                            {
                                servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                                         rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                                         rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                            }

                        }
                    }
                }
                else
                {
                    code = "600";
                }

            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;

                string[] paramError =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@SISTEMA",
                    "@AMBIENTE",
                    "@MODULO",
                    "@TIPOERROR",
                    "@EXCEPCION",
                    "@ERRORLINE",
                    "@ERRORMESSAGE",
                    "@ERRORNUMBER",
                    "@ERRORPROCEDURE",
                    "@ERRORSEVERITY",
                    "@ERRORSTATE"
                };

                string[] valoresError =
                {
                    tokenAuth,
                    agenteAplication,
                    sistema,
                    ambiente,
                    "HttPost | JsonResult | CargaMasiva",
                    "Aplicacion",
                    ex.Message,
                    "", "", "", "", "", ""
                };

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }

            }

            return Json(new { Code = code, Message = message, TotalProcesado = totalProcesado });
        }

        [HttpPost]
        public JsonResult ValidacionCargaMasiva(string codigoTransaccion, string plantilla, string procesamiento)
        {

            string code = string.Empty;
            string message = string.Empty;
            string totalProcesado = string.Empty;
            string totalErrores = string.Empty;
            string messageErrores = string.Empty;
            string contieneErrores = string.Empty;
            string rutProcesado = string.Empty;
            string sigueProceso = string.Empty;

            #region "DATOS PARA CONTROL DE ERRORES"
            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                sistema = Request.Url.AbsoluteUri.Split('/')[3];
                if (!Request.Url.AbsoluteUri.Split('/')[3].Contains("QA"))
                {
                    ambiente = "Release";
                }
                else
                {
                    ambiente = "Quality";
                }
            }
            else
            {
                sistema = fromDebugQA;
                ambiente = "Debug";
            }
            #endregion

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {

                    string[] parametrosCargaMasiva =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO",
                        "@PLANTILLAMASIVA"
                    };

                    string[] valoresCargaMasiva =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento,
                        plantilla
                    };

                    DataSet dataProceso = servicioOperaciones.SetValidacionProcesoMasivo(parametrosCargaMasiva, valoresCargaMasiva).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() != "500")
                        {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
                            totalErrores = rowsProceso["TotalErrores"].ToString();
                            messageErrores = rowsProceso["MessageErrores"].ToString();
                            contieneErrores = rowsProceso["ContieneErrores"].ToString();
                            rutProcesado = rowsProceso["RutProcesado"].ToString();
                            sigueProceso = rowsProceso["SigueProceso"].ToString();
                        }
                        else
                        {
                            code = "500";
                            message = errorSistema;

                            string[] paramError =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@SISTEMA",
                                "@AMBIENTE",
                                "@MODULO",
                                "@TIPOERROR",
                                "@EXCEPCION",
                                "@ERRORLINE",
                                "@ERRORMESSAGE",
                                "@ERRORNUMBER",
                                "@ERRORPROCEDURE",
                                "@ERRORSEVERITY",
                                "@ERRORSTATE"
                            };

                            string[] valoresError =
                            {
                                tokenAuth,
                                agenteAplication,
                                sistema,
                                ambiente,
                                "HttPost | JsonResult | ValidacionCargaMasiva",
                                "Procedimiento Almacenado",
                                "",
                                rowsProceso["ErrorLine"].ToString(),
                                rowsProceso["ErrorMessage"].ToString(),
                                rowsProceso["ErrorNumber"].ToString(),
                                rowsProceso["ErrorProcedure"].ToString(),
                                rowsProceso["ErrorSeverity"].ToString(),
                                rowsProceso["ErrorState"].ToString()
                            };

                            DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                            foreach (DataRow rowsError in dataError.Tables[0].Rows)
                            {
                                servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                                         rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                                         rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                            }
                        }
                    }
                }
                else
                {
                    code = "600";
                }
            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;

                string[] paramError =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@SISTEMA",
                    "@AMBIENTE",
                    "@MODULO",
                    "@TIPOERROR",
                    "@EXCEPCION",
                    "@ERRORLINE",
                    "@ERRORMESSAGE",
                    "@ERRORNUMBER",
                    "@ERRORPROCEDURE",
                    "@ERRORSEVERITY",
                    "@ERRORSTATE"
                };

                string[] valoresError =
                {
                    tokenAuth,
                    agenteAplication,
                    sistema,
                    ambiente,
                    "HttPost | JsonResult | ValidacionCargaMasiva",
                    "Aplicacion",
                    ex.Message,
                    "", "", "", "", "", ""
                };

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }
            }

            return Json(new
            {
                Code = code,
                Message = message,
                TotalProcesado = totalProcesado,
                TotalErrores = totalErrores,
                MessageErrores = messageErrores,
                ContieneErrores = contieneErrores,
                RutProcesado = rutProcesado,
                SigueProceso = sigueProceso
            });
        }

        [HttpPost]
        public ActionResult ViewPartialLoaderProcesoMasivo(string message)
        {
            ViewBag.HTMLLoader = message;

            return PartialView("_LoaderProcesoMasivo");
        }

        [HttpPost]
        public ActionResult ViewPartialLoaderErrorGeneric(string message)
        {
            ViewBag.HTMLLoaderError = message;

            return PartialView("_ErrorGeneric");
        }

        [HttpPost]
        public JsonResult ViewDDLClientes()
        {
            //string empresa
            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString()
            };

            DataSet dsClienteNombre = servicioOperaciones.GetObtenerClientesNombre(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Cliente> lsClientesNombre = new List<Models.Cliente>();

            foreach (DataRow dr in dsClienteNombre.Tables[0].Rows)
            {
                Models.Cliente clienteNombre = new Models.Cliente();

                clienteNombre.Code = dr["Code"].ToString();
                clienteNombre.Message = dr["Message"].ToString();
                clienteNombre.Codigo = dr["Codigo"].ToString();
                clienteNombre.Descripcion_Cliente = dr["Descripcion_Cliente"].ToString();

                lsClientesNombre.Add(clienteNombre);
            }

            return Json(new { Code = "200", Clientes = lsClientesNombre });
        }

        [HttpPost]
        public JsonResult ViewDDLClientesDistintos(string empresa)
        {
            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR"

            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString()
            };

            DataSet dsClienteNombre = servicioOperaciones.GetObtenerClientesDistintos(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Cliente> lsClientesNombre = new List<Models.Cliente>();

            foreach (DataRow dr in dsClienteNombre.Tables[0].Rows)
            {
                Models.Cliente clienteNombre = new Models.Cliente();

                clienteNombre.Code = dr["Code"].ToString();
                clienteNombre.Message = dr["Message"].ToString();
                clienteNombre.Codigo = dr["Codigo"].ToString();
                clienteNombre.Descripcion_Cliente = dr["Descripcion_Cliente"].ToString();

                lsClientesNombre.Add(clienteNombre);
            }

            return Json(new { Code = "200", Clientes = lsClientesNombre });
        }

        [HttpPost]
        public JsonResult ViewDDLClientesDistintosNombre(string cliente)
        {
            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@CLIENTE"

            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                cliente
            };

            DataSet dsClienteNombre = servicioOperaciones.GetObtenerClientesDistintosNombre(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Cliente> lsClientesNombre = new List<Models.Cliente>();

            foreach (DataRow dr in dsClienteNombre.Tables[0].Rows)
            {
                Models.Cliente clienteNombre = new Models.Cliente();

                clienteNombre.Code = dr["Code"].ToString();
                clienteNombre.Message = dr["Message"].ToString();
                clienteNombre.Codigo = dr["Codigo"].ToString();
                clienteNombre.Descripcion_Cliente = dr["Descripcion_Cliente"].ToString();

                lsClientesNombre.Add(clienteNombre);
            }

            return Json(new { Code = "200", Clientes = lsClientesNombre });
        }

        [HttpPost]
        public JsonResult ViewDDLProveedores()
        {
            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                ""
            };

            DataSet dsProveedores = servicioOperaciones.GetObtenerProveedores(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Proveedor> lsProveedores = new List<Models.Proveedor>();

            foreach (DataRow dr in dsProveedores.Tables[0].Rows)
            {
                Models.Proveedor proveedor = new Models.Proveedor();

                proveedor.Code = dr["Code"].ToString();
                proveedor.Message = dr["Message"].ToString();
                proveedor.Id = dr["Id"].ToString();
                proveedor.Nombre = dr["Nombre"].ToString();
                proveedor.Rut = dr["Rut"].ToString();
                proveedor.Vigente = dr["Vigente"].ToString();

                lsProveedores.Add(proveedor);
            }

            return Json(new { Code = "200", Proveedores = lsProveedores });
        }

        [HttpPost]
        public JsonResult CargarGastoFinanzas(string empresa, string cliente, string periodo, string monto, string reembolso,
                                              string comentario, string nFactura, string proveedor, string etiqueta, string token,string tipoDocumento,
                                              string afecto, string montoNetoDocumento)
        {
            string code = string.Empty;
            string message = string.Empty;
            string codigo = string.Empty;
            string totalProcesado = string.Empty;
            
            #region "DATOS PARA CONTROL DE ERRORES"
            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                sistema = Request.Url.AbsoluteUri.Split('/')[3];
                if (!Request.Url.AbsoluteUri.Split('/')[3].Contains("QA"))
                {
                    ambiente = "Release";
                }
                else
                {
                    ambiente = "Quality";
                }
            }
            else
            {
                sistema = fromDebugQA;
                ambiente = "Debug";
            }
            #endregion

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] parametrosCargaGasto =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@IDUSUARIO",
                        "@EMPRESA",
                        "@CLIENTE",
                        "@PERIODO",
                        "@MONTONETO",
                        "@REEMBOLSO",
                        "@COMENTARIO",
                        "@NFACTURA",
                        "@PROVEEDOR",
                        "@ETIQUETA",
                        "@TOKEN",
                        "@TIPODOCUMENTO",
                        "@AFECTO",
                        "@MONTONETODOCUMENTO"
                    };

                    //manejo tipo documento nulo 21-11-2019
                    if (tipoDocumento.Equals("null"))
                    {
                        tipoDocumento = "nulo";
                    }

                    string[] valoresCargaGasto =
                    {
                         tokenAuth,
                         agenteAplication,
                         "",
                         Session["CodificarAuth"].ToString(),
                         empresa,
                         cliente,
                         periodo,
                         monto,
                         reembolso,
                         comentario,
                         nFactura,
                         proveedor,
                         etiqueta,
                         token,
                         tipoDocumento,
                         afecto,
                         montoNetoDocumento
                     };

                    DataSet dataGasto = servicioOperaciones.SetGasto(parametrosCargaGasto, valoresCargaGasto).Table;

                    foreach (DataRow rowsProceso in dataGasto.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() != "500")
                        {
                            if (rowsProceso["Code"].ToString() == "200")
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();
                                codigo = rowsProceso["Codigo"].ToString();
                            }
                            else if (rowsProceso["Code"].ToString() == "206")
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();
                                codigo = rowsProceso["Codigo"].ToString();
                            }
                            else
                            {
                                code = rowsProceso["Code"].ToString();
                                message = rowsProceso["Message"].ToString();
                            }
                        }
                        else
                        {
                            code = "500";
                            message = errorSistema;

                            string[] paramError =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@SISTEMA",
                                "@AMBIENTE",
                                "@MODULO",
                                "@TIPOERROR",
                                "@EXCEPCION",
                                "@ERRORLINE",
                                "@ERRORMESSAGE",
                                "@ERRORNUMBER",
                                "@ERRORPROCEDURE",
                                "@ERRORSEVERITY",
                                "@ERRORSTATE"
                            };

                            string[] valoresError =
                            {
                                tokenAuth,
                                agenteAplication,
                                sistema,
                                ambiente,
                                "HttPost | JsonResult | CargaGastoFinanzas",
                                "Procedimiento Almacenado",
                                "",
                                rowsProceso["ErrorLine"].ToString(),
                                rowsProceso["ErrorMessage"].ToString(),
                                rowsProceso["ErrorNumber"].ToString(),
                                rowsProceso["ErrorProcedure"].ToString(),
                                rowsProceso["ErrorSeverity"].ToString(),
                                rowsProceso["ErrorState"].ToString()
                            };


                            DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                            foreach (DataRow rowsError in dataError.Tables[0].Rows)
                            {
                                servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                                         rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                                         rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                            }

                        }
                    }
                }
                else
                {
                    code = "600";
                }

            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;

                string[] paramError =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@SISTEMA",
                    "@AMBIENTE",
                    "@MODULO",
                    "@TIPOERROR",
                    "@EXCEPCION",
                    "@ERRORLINE",
                    "@ERRORMESSAGE",
                    "@ERRORNUMBER",
                    "@ERRORPROCEDURE",
                    "@ERRORSEVERITY",
                    "@ERRORSTATE"
                };

                string[] valoresError =
                {
                    tokenAuth,
                    agenteAplication,
                    sistema,
                    ambiente,
                    "HttPost | JsonResult | CargaGasto",
                    "Aplicacion",
                    ex.Message,
                    "", "", "", "", "", ""
                };

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }

            }

            return Json(new { Code = code, Message = message, TotalProcesado = totalProcesado, Codigo = codigo });
        }

        [HttpPost]
        public JsonResult ObtenerProveedorRut(string rutproveedor)
        {
            string Codigo = "404";

            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@RUT"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                rutproveedor
            };

            DataSet dsProveedor = servicioOperaciones.GetObtenerProveedoresRut(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Proveedor> lsProveedor = new List<Models.Proveedor>();

            foreach (DataRow dr in dsProveedor.Tables[0].Rows)
            {
                Models.Proveedor proveedor = new Models.Proveedor();

                if (dr["Code"].ToString() == "404")
                {
                    proveedor.Code = dr["Code"].ToString();
                    proveedor.Message = dr["Message"].ToString();
                    Codigo = "404";
                }
                else
                {
                    proveedor.Code = dr["Code"].ToString();
                    proveedor.Message = dr["Message"].ToString();
                    proveedor.Id = dr["Id"].ToString();
                    proveedor.Nombre = dr["Nombre"].ToString();
                    proveedor.Rut = dr["Rut"].ToString();
                    proveedor.Vigente = dr["Vigente"].ToString();
                    Codigo = "200";
                }

                lsProveedor.Add(proveedor);
            }

            return Json(new { Code = Codigo, Proveedores = lsProveedor });
        }

        [HttpPost]
        public JsonResult ObtenerPeriodoVigente(string empresa)
        {

            string code = "404";

            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@EMPRESA"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                empresa
            };

            DataSet dsPeriodo = servicioOperaciones.GetObtenerPeriodoVigente(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Periodo> lsPeriodo = new List<Models.Periodo>();

            foreach (DataRow dr in dsPeriodo.Tables[0].Rows)
            {
                Models.Periodo periodo = new Models.Periodo();

                if (dr["Code"].ToString() == "200")
                {
                    code = "200";
                    periodo.Code = dr["Code"].ToString();
                    periodo.Message = dr["Message"].ToString();
                    periodo.Id = dr["Id"].ToString();
                    periodo.Empresa = dr["Empresa"].ToString();
                    periodo.PeriodoVigente = dr["Periodo"].ToString();
                    periodo.FechaApertura = dr["FechaApertura"].ToString();
                    periodo.FechaCierre = dr["FechaCierre"].ToString();
                    periodo.Vigente = dr["Vigente"].ToString();
                }
                else
                {
                    code = "404";
                    periodo.Code = dr["Code"].ToString();
                    periodo.Message = dr["Message"].ToString();
                }

                lsPeriodo.Add(periodo);
            }

            return Json(new { Code = code, Periodo = lsPeriodo });
        }

        [HttpPost]
        public JsonResult ObtenerClienteByNombre(string nombre, string empresa)
        {

            string codigo = "404";

            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@NOMBRE"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                nombre
               
            };

            DataSet dsCliente = servicioOperaciones.GetObtenerClienteByNombre(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Cliente> lsCliente = new List<Models.Cliente>();

            foreach (DataRow dr in dsCliente.Tables[0].Rows)
            {
                Models.Cliente cliente = new Models.Cliente();

                if (dr["Code"].ToString() == "404")
                {
                    cliente.Code = dr["Code"].ToString();
                    cliente.Message = dr["Message"].ToString();

                    codigo = "404";
                } else {
                    cliente.Code = dr["Code"].ToString();
                    cliente.Message = dr["Message"].ToString();
                    cliente.Codigo = dr["Codigo"].ToString();
                    cliente.Descripcion_Cliente = dr["Descripcion_Cliente"].ToString();
                 

                    codigo = "200";
                }

                lsCliente.Add(cliente);
            }

            return Json(new { Code = codigo, Cliente = lsCliente });
        }

        [HttpPost]
        public JsonResult ObtenerPeriodoVigentes(string empresa)
        {

            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@EMPRESA"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                empresa
            };

            DataSet dsPeriodo = servicioOperaciones.GetObtenerPeriodoVigente(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.Periodo> lsPeriodo = new List<Models.Periodo>();

            foreach (DataRow dr in dsPeriodo.Tables[0].Rows)
            {
                Models.Periodo periodo = new Models.Periodo();

                periodo.Code = dr["Code"].ToString();
                periodo.Message = dr["Message"].ToString();
                periodo.Id = dr["Id"].ToString();
                periodo.Empresa = dr["Empresa"].ToString();
                periodo.PeriodoVigente = dr["Periodo"].ToString();
                periodo.Vigente = dr["Vigente"].ToString();

                lsPeriodo.Add(periodo);
            }

            return Json(new { Code = "200", Periodo = lsPeriodo });
        }

        [HttpPost]
        public JsonResult ObtenerTipoReembolsoDDL(string nombre, string empresa)
        {

            string codigo = "404";

            string[] parametrosHeadersCuentas =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR"
            };

            string[] valoresHeadersCuentas =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString()
            };

            DataSet dsTiporeembolso = servicioOperaciones.GetObtenerTipoReembolso(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
            List<Models.TipoReembolso> lstTipoReembolso = new List<Models.TipoReembolso>();

            foreach (DataRow dr in dsTiporeembolso.Tables[0].Rows)
            {
                Models.TipoReembolso tipoReembolso = new Models.TipoReembolso();

                if (dr["Code"].ToString() == "404")
                {
                    tipoReembolso.Code = dr["Code"].ToString();
                    tipoReembolso.Message = dr["Message"].ToString();

                    codigo = "404";
                }
                else
                {
                    tipoReembolso.Code = dr["Code"].ToString();
                    tipoReembolso.Message = dr["Message"].ToString();
                    tipoReembolso.Id = dr["IdTipoReembolso"].ToString();
                    tipoReembolso.Nombre = dr["Nombre"].ToString();
                    tipoReembolso.Codigo = dr["Codigo"].ToString();
                    tipoReembolso.Descripcion = dr["Descripcion"].ToString();
                    tipoReembolso.Descripcion = dr["Vigente"].ToString();

                    codigo = "200";
                }

                lstTipoReembolso.Add(tipoReembolso);
            }

            return Json(new { Code = codigo, TipoReembolso = lstTipoReembolso });
        }

        [HttpPost]
        public JsonResult GetExisteDocumento(string rutProveedor, string numeroDocumento, string tipoDocumento)
        {

            string existe = string.Empty;

            string[] paramExisteDoc =
            {
                "@RUTPROVEEDOR",
                "@NUMERODOCUMENTO",
                "@TIPODOCUMENTO"
            };

            string[] valExisteDoc =
            {
                rutProveedor,
                numeroDocumento,
                tipoDocumento
            };

            DataSet data = servicioOperaciones.GetExisteDocumento(paramExisteDoc, valExisteDoc).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                existe = rows["Cantidad"].ToString();
            }

            return Json(new { Cantidad = existe });
        }

        #endregion

        #region "HTTPOST CERRAR PERIODO"
        [HttpPost]
        public ActionResult ActualizarPeriodo(string nombreEmpresa)
        {
            string mensaje = string.Empty;

            string[] paramPeriodo = new string[9];
            string[] valPeriodo = new string[9];

            paramPeriodo[0] = "@AUTHENTICATE";
            paramPeriodo[1] = "@AGENTAPP";
            paramPeriodo[2] = "@ACCION";
            paramPeriodo[3] = "@EMPRESA";
            paramPeriodo[4] = "@PERIODO";
            paramPeriodo[5] = "@VIGENTE";
            paramPeriodo[6] = "@FECHACIERRE";
            paramPeriodo[7] = "@FECHAAPERTURA";
            paramPeriodo[8] = "@CONFIRMACION";
           

            valPeriodo[0] = tokenAuth;
            valPeriodo[1] = agenteAplication;
            valPeriodo[2] = "U";
            valPeriodo[3] = nombreEmpresa;
            valPeriodo[4] = "";
            valPeriodo[5] = "N";
            valPeriodo[6] = "";
            valPeriodo[7] = "";
            valPeriodo[8] = "";
           

            DataSet dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

            foreach (DataRow rows in dataPeriodo.Tables[0].Rows)
            {
                
                switch (rows["Code"].ToString())
                {
                    case "200":

                        ViewBag.HTMLLoaderError = rows["Message"].ToString();
                        ViewBag.ViewMessageApplication = "_CompletoGeneric";



                        break;

                }
                
            }

            List<Models.Periodo> periodos = new List<Models.Periodo>();

            valPeriodo[0] = tokenAuth;
            valPeriodo[1] = agenteAplication;
            valPeriodo[2] = "S";
            valPeriodo[3] = "";
            valPeriodo[4] = "";
            valPeriodo[5] = "";
            valPeriodo[6] = "";
            valPeriodo[7] = "";
            valPeriodo[8] = "";

            dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

            foreach (DataRow item in dataPeriodo.Tables[0].Rows)
            {
                Models.Periodo periodo = new Models.Periodo();

                periodo.Empresa = item["Nombre"].ToString();
                periodo.PeriodoVigente = item["Periodos"].ToString();
                periodo.EmpresaCodificado = item["EmpresaCodificado"].ToString();

                periodos.Add(periodo);
            }

            ViewBag.ListadoPeriodo = periodos;


            return PartialView("Periodo/_ListadoPeriodos");
        }
        #endregion

        #region "HTTPOST ABRIR PERIODO"
        [HttpPost]
        public ActionResult AbrirPeriodo(string nombreEmpresa, string periodoparam, string confirmacion = "N")
        {

            /**
             *  LA CONFIRMCIÓN SE UTILIZARA PARA INDICARLE AL USUARIO QUE ESTA APUNTO DE CERRAR EL PERIODO ABIERTO Y ABRIR OTRO. DESDE AQUI MISMO LO HAREMOS.
             *  por ende requerimos algo tan simple como el procedimiento y ver lo que devuelve al momento de encontrar algo abierto
             *  ESO SIGNIFICA QUE POR DEFECTO EL CONFIMACION VA ENTRAR COMO "N", a no ser que el dato venga, es un dato opcional.
             */
            string vistaRetorno = string.Empty;
            string updateTargetId = string.Empty;
            string onSuccess = string.Empty;
            string[] listaObjetivos = new string[4];
            listaObjetivos[0] = "tablaPeriodos";
            listaObjetivos[1] = "modalCerrarPeriodo";
            listaObjetivos[2] = "modalBodyConfirmacion";
            listaObjetivos[3] = "";
            ViewBag.ArrayOpciones = listaObjetivos;

            string[] paramPeriodo = new string[9];
            string[] valPeriodo = new string[9];
            DataSet dataPeriodo;

            paramPeriodo[0] = "@AUTHENTICATE";
            paramPeriodo[1] = "@AGENTAPP";
            paramPeriodo[2] = "@ACCION";
            paramPeriodo[3] = "@EMPRESA";
            paramPeriodo[4] = "@PERIODO";
            paramPeriodo[5] = "@VIGENTE";
            paramPeriodo[6] = "@FECHACIERRE";
            paramPeriodo[7] = "@FECHAAPERTURA";
            paramPeriodo[8] = "@CONFIRMACION";

            valPeriodo[0] = tokenAuth;
            valPeriodo[1] = agenteAplication;
            valPeriodo[2] = "I";
            valPeriodo[3] = nombreEmpresa;
            valPeriodo[4] = periodoparam;
            valPeriodo[5] = "S";
            valPeriodo[6] = "";
            valPeriodo[7] = "";
            valPeriodo[8] = confirmacion;

            dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

            foreach (DataRow rows in dataPeriodo.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.HTMLLoaderError = rows["Message"].ToString();
                        ViewBag.ConfirmacionPeriodoAbierto = "N";
                        ViewBag.ViewMessageApplication = "_CompletoGeneric";
                        vistaRetorno = "Periodo/_ListadoPeriodos";
                        updateTargetId = listaObjetivos[0];
                        onSuccess = listaObjetivos[1];

                        /** actualizacion de vista */

                        List<Models.Periodo> periodos = new List<Models.Periodo>();

                        valPeriodo[0] = tokenAuth;
                        valPeriodo[1] = agenteAplication;
                        valPeriodo[2] = "S";
                        valPeriodo[3] = "";
                        valPeriodo[4] = "";
                        valPeriodo[5] = "";
                        valPeriodo[6] = "";
                        valPeriodo[7] = "";
                        valPeriodo[8] = "";

                        dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

                        foreach (DataRow item in dataPeriodo.Tables[0].Rows)
                        {
                            Models.Periodo periodo = new Models.Periodo();

                            periodo.Empresa = item["Nombre"].ToString();
                            periodo.PeriodoVigente = item["Periodos"].ToString();
                            periodo.EmpresaCodificado = item["EmpresaCodificado"].ToString();

                            periodos.Add(periodo);
                        }

                        ViewBag.ListadoPeriodo = periodos;
                        ViewBag.ErrorCodigo300 = "";

                        break;
                    case "300":
                        ViewBag.ErrorCodigo300 = "S";
                        ViewBag.HTMLLoaderError = rows["Message"].ToString();
                        ViewBag.ConfirmacionPeriodoAbierto = "N";
                        vistaRetorno = "Periodo/_ConfirmacionAbrirPeriodo";
                        break;
                    case "302":
                        ViewBag.HTMLLoaderError = rows["Message"].ToString();
                        ViewBag.ConfirmacionPeriodoAbierto = "S";
                        ViewBag.ViewMessageApplication = "";
                        updateTargetId = listaObjetivos[0];
                        onSuccess = listaObjetivos[1];
                        ViewBag.ErrorCodigo300 = "N";

                        vistaRetorno = "Periodo/_ConfirmacionAbrirPeriodo";
                        break;
                }
            }

            Models.Modal modal = new Models.Modal();
            modal.UpdateTargetId = updateTargetId;
            modal.OnSuccess = onSuccess;

            ViewBag.PeriodoConfirmadoAbrir = periodoparam;
            ViewBag.NombreEmpresaCodificado = nombreEmpresa;

            return PartialView(vistaRetorno, modal);
        }
        #endregion

        #region "HTTPOST Select Periodo"

        [HttpPost]
        public ActionResult ModalAgregarPeriodo(string nombreEmpresa, string nombreEmpresaCodificado, string periodoVigente)
        {
            Models.Modal modal = new Models.Modal();

            try
            {

                ViewBag.ConfirmacionPeriodoAbierto = "N";
                ViewBag.NombreEmpresa = nombreEmpresa;
                ViewBag.NombreEmpresaCodificado = nombreEmpresaCodificado;
                ViewBag.PeriodoVigente = periodoVigente;
                ViewBag.ViewMessageApplication = "";
                ViewBag.ErrorCodigo300 = "";

                string[] paramPeriodo = new string[9];
                string[] valPeriodo = new string[9];
                DataSet dataPeriodo;
                

                paramPeriodo[0] = "@AUTHENTICATE";
                paramPeriodo[1] = "@AGENTAPP";
                paramPeriodo[2] = "@ACCION";
                paramPeriodo[3] = "@EMPRESA";
                paramPeriodo[4] = "@PERIODO";
                paramPeriodo[5] = "@VIGENTE";
                paramPeriodo[6] = "@FECHACIERRE";
                paramPeriodo[7] = "@FECHAAPERTURA";
                paramPeriodo[8] = "@CONFIRMACION";

                valPeriodo[0] = tokenAuth;
                valPeriodo[1] = agenteAplication;
                valPeriodo[2] = "SE";
                valPeriodo[3] = nombreEmpresaCodificado;
                valPeriodo[4] = "";
                valPeriodo[5] = "";
                valPeriodo[6] = "";
                valPeriodo[7] = "";
                valPeriodo[8] = "";

                dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

                foreach (DataRow item in dataPeriodo.Tables[0].Rows)
                {
                    modal.UpdateTargetId = item["UpdateTargetId"].ToString();
                    modal.OnSuccess = item["OnSuccess"].ToString();
                }

                
            }
            catch (Exception ex)
            {
                List<Models.Error> errores = new List<Models.Error>();
                ViewBag.CodeCorrect = false;
                Models.Error error = new Models.Error();

                error.Code = "500.*";
                error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

                errores.Add(error);

                ViewBag.Error = errores;
            }

            return PartialView("Periodo/_ConfirmacionAbrirPeriodo", modal);

        }

        [HttpPost]
        public ActionResult Periodos()
        {
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

            
            ///
            string[] paramPeriodo = new string[9];
            string[] valPeriodo = new string[9];

            List<Models.Periodo> periodos = new List<Models.Periodo>();

            paramPeriodo[0] =   "@AUTHENTICATE";
            paramPeriodo[1] =   "@AGENTAPP";
            paramPeriodo[2] =   "@ACCION";
            paramPeriodo[3] =   "@EMPRESA";
            paramPeriodo[4] =   "@PERIODO";
            paramPeriodo[5] =   "@VIGENTE";
            paramPeriodo[6] =   "@FECHACIERRE";
            paramPeriodo[7] =   "@FECHAAPERTURA";
            paramPeriodo[8] =   "@CONFIRMACION";
           

            valPeriodo[0] = tokenAuth;
            valPeriodo[1] = agenteAplication;
            valPeriodo[2] = "S";
            valPeriodo[3] = "";
            valPeriodo[4] = "";
            valPeriodo[5] = "";
            valPeriodo[6] = "";
            valPeriodo[7] = "";
            valPeriodo[8] = "";
          
            DataSet dataPeriodo = servicioOperaciones.SetMantencionPeriodo(paramPeriodo, valPeriodo).Table;

            foreach (DataRow item in dataPeriodo.Tables[0].Rows)
            {
                Models.Periodo periodo = new Models.Periodo();

                
                periodo.Empresa = item["Empresa"].ToString();
                periodo.PeriodoVigente = item["Periodo"].ToString();
              
             


                periodos.Add(periodo);
            }

            ViewBag.ListadoPeriodo = periodos;

            

            return PartialView("Periodo/_ListadoPeriodos");
        }
        #endregion

        #region "HTTPOST INSERT PROVEEDORES"

        [HttpPost]
        public JsonResult MantenedorProveedores(string rut, string nombre)
        {
            string code = string.Empty;
            string message = string.Empty;
            string[] paramProveedores = new string[8];
            string[] valProveedores = new string[8];
            string mensaje = string.Empty;
            string[] paramError = new string[13];
            string[] valoresError = new string[13];

            paramError[0] =  "@AUTHENTICATE";
            paramError[1] =  "@AGENTAPP";
            paramError[2] =  "@SISTEMA";
            paramError[3] =  "@AMBIENTE";
            paramError[4] =  "@MODULO";
            paramError[5] =  "@TIPOERROR";
            paramError[6] =  "@EXCEPCION";
            paramError[7] =  "@ERRORLINE";
            paramError[8] =  "@ERRORMESSAGE";
            paramError[9] =  "@ERRORNUMBER";
            paramError[10] = "@ERRORPROCEDURE";
            paramError[11] = "@ERRORSEVERITY";
            paramError[12] = "@ERRORSTATE";

           
            try
            {
                paramProveedores[0] = "@AUTHENTICATE";
                paramProveedores[1] = "@AGENTAPP";
                paramProveedores[2] = "@ACCION";
                paramProveedores[3] = "@NOMBRE";
                paramProveedores[4] = "@RUT";
                paramProveedores[5] = "@PAGINATION";
                paramProveedores[6] = "@TYPEFILTER";
                paramProveedores[7] = "@DATAFILTER";

                valProveedores[0] = tokenAuth;
                valProveedores[1] = agenteAplication;
                valProveedores[2] = "I";
                valProveedores[3] = nombre;
                valProveedores[4] = rut;
                valProveedores[5] = "";
                valProveedores[6] = "";
                valProveedores[7] = "";

                DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

                foreach (DataRow rows in dataProveedores.Tables[0].Rows)
                {
                    switch(rows["Code"].ToString())
                    {
                        case "200":
                            mensaje = rows["Message"].ToString();
                            break;
                        default:
                            mensaje = rows["Message"].ToString();
                            break;
                    }
                       
                  
                }


            }
            catch (Exception ex)
            {
                code = "500";
                message = errorSistema;

                

                valoresError[0] =   tokenAuth;
                valoresError[1] =   agenteAplication;
                valoresError[2] =   sistema;
                valoresError[3] =   ambiente;
                valoresError[4] =   "HttPost | JsonResult | MantenedorProveedores";
                valoresError[5] =   "Aplicacion";
                valoresError[6] =   ex.Message;
                valoresError[7] =   "";
                valoresError[8] =   "";
                valoresError[9] =   "";
                valoresError[10] =  "";
                valoresError[11] =  "";
                valoresError[12] =  "";

                DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                foreach (DataRow rowsError in dataError.Tables[0].Rows)
                {
                    servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                                                             rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                             rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                }
            }

            return Json(mensaje);
        }
        #endregion

        #region "NUEVO METODOS DE FINANZAS"
        ////Actualizar Proveedores
        [HttpPost]
        public JsonResult ActualizarProveedores(string rutModal, string nombreModal)
        {
            string mensaje = string.Empty;

            string[] paramProveedores = new string[8];
            string[] valProveedores = new string[8];

            paramProveedores[0] = "@AUTHENTICATE";
            paramProveedores[1] = "@AGENTAPP";
            paramProveedores[2] = "@ACCION";
            paramProveedores[3] = "@NOMBRE";
            paramProveedores[4] = "@RUT";
            paramProveedores[5] = "@PAGINATION";
            paramProveedores[6] = "@TYPEFILTER";
            paramProveedores[7] = "@DATAFILTER";

            valProveedores[0] = tokenAuth;
            valProveedores[1] = agenteAplication;
            valProveedores[2] = "U";
            valProveedores[3] = nombreModal;
            valProveedores[4] = rutModal;
            valProveedores[5] = "";
            valProveedores[6] = "";
            valProveedores[7] = "";

            DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

            foreach (DataRow rows in dataProveedores.Tables[0].Rows)
            {
                mensaje = rows["Message"].ToString();
            }

            return Json(mensaje);
        }

        ////EliminarProveedores
        [HttpPost]
        public JsonResult EliminarProveedores(string rutModal)
        {
            string mensaje = string.Empty;

            string[] paramProveedores = new string[8];
            string[] valProveedores = new string[8];

            paramProveedores[0] = "@AUTHENTICATE";
            paramProveedores[1] = "@AGENTAPP";
            paramProveedores[2] = "@ACCION";
            paramProveedores[3] = "@NOMBRE";
            paramProveedores[4] = "@RUT";
            paramProveedores[5] = "@PAGINATION";
            paramProveedores[6] = "@TYPEFILTER";
            paramProveedores[7] = "@DATAFILTER";

            valProveedores[0] = tokenAuth;
            valProveedores[1] = agenteAplication;
            valProveedores[2] = "D";
            valProveedores[3] = "";
            valProveedores[4] = rutModal;
            valProveedores[5] = "";
            valProveedores[6] = "";
            valProveedores[7] = "";


            DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

            foreach (DataRow rows in dataProveedores.Tables[0].Rows)
            {
                mensaje = rows["Message"].ToString();
            }

            return Json(mensaje);
        }

        [HttpPost]
        public ActionResult Proveedores(string pagination, string typeFilter = "", string filterDatos = "")
        {
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

            Session["PaginationProveedores"] = pagination;

            string[] paramProveedores = new string[8];
            string[] valProveedores = new string[8];
            List<Models.Proveedor> proveedores = new List<Models.Proveedor>();

            paramProveedores[0] = "@AUTHENTICATE";
            paramProveedores[1] = "@AGENTAPP";
            paramProveedores[2] = "@ACCION";
            paramProveedores[3] = "@NOMBRE";
            paramProveedores[4] = "@RUT";
            paramProveedores[5] = "@PAGINATION";
            paramProveedores[6] = "@TYPEFILTER";
            paramProveedores[7] = "@DATAFILTER";

            valProveedores[0] = tokenAuth;
            valProveedores[1] = agenteAplication;
            valProveedores[2] = "S";
            valProveedores[3] = "";
            valProveedores[4] = "";
            valProveedores[5] = pagination;
            valProveedores[6] = typeFilter;
            valProveedores[7] = filterDatos;

            DataSet dataProveedores = servicioOperaciones.SetMantencionProveedores(paramProveedores, valProveedores).Table;

            foreach (DataRow item in dataProveedores.Tables[0].Rows)
            {
                Models.Proveedor proveedor = new Models.Proveedor();

                proveedor.Rut = item["Rut"].ToString();
                proveedor.Nombre = item["Nombre"].ToString();
                proveedor.Codificar = domain + prefixDomain + "/Finanzas/Mantencion/RWRpdA==/" + item["Codificar"].ToString();
                proveedor.CodificarD = domain + prefixDomain + "/Finanzas/Mantencion/RGVsZXRl/" + item["Codificar"].ToString();

                proveedores.Add(proveedor);
            }

            ViewBag.ListadoProveedores = proveedores;

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
            valPaginator[3] = "Proveedores";
            valPaginator[4] = pagination;
            valPaginator[5] = "";
            valPaginator[6] = typeFilter;
            valPaginator[7] = filterDatos;

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

            return PartialView("Proveedor/_ListadoProveedores");
        }

        #endregion

        #region "NUEVO MANTENDOR DE GASTOS"

        public ActionResult IngresarGastos()
        {
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

           

            string[] paramGastos = new string[8];
            string[] valGastos = new string[8];

            List<Models.Gasto> gastos = new List<Models.Gasto>();

            paramGastos[0] = "@AUTHENTICATE";
            paramGastos[1] = "@AGENTAPP";
            paramGastos[2] = "@ACCION";
            paramGastos[3] = "@NOMBRE_EMP";
            paramGastos[4] = "@FILTER";
         

            valGastos[0] = tokenAuth;
            valGastos[1] = agenteAplication;
            valGastos[2] = "SEMP";
            valGastos[3] = "";
            valGastos[4] = "";
      

            

            

            
            return View();
        }


        #endregion
        
        #region "PROCESO DE PAGO FINIQUITO"

        [HttpPost]
        public ActionResult AgregarSucursalDestino(string codigoProceso, string sucursal, string impresora = "")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "ADDSUCBOXDESPACHO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
                valMantFin[6] = sucursal;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Finanzas/_EndBoxDespacho";
                            ViewBag.ReferenciaPdfComprobante = ModuleControlRetornoPath() + "/Pdf/GeneratePdf?pdf=ComprobanteDespacho&data=" + codigoProceso;

                            if (impresora != "")
                            {
                                var memory = new ActionAsPdf("Pdf/ComprobanteDespacho/" + codigoProceso)
                                {
                                    PageMargins = new Rotativa.Options.Margins(5, 1, 5, 1),
                                    PageOrientation = Rotativa.Options.Orientation.Portrait,
                                    PageSize = Rotativa.Options.Size.Letter
                                };

                                byte[] bytes = memory.BuildFile(ControllerContext);

                                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                clientSocket.NoDelay = true;

                                IPAddress ip = IPAddress.Parse(impresora);
                                IPEndPoint ipep = new IPEndPoint(ip, 9100);
                                clientSocket.Connect(ipep);
                                if (clientSocket.Connected)
                                {
                                    var response = clientSocket.Send(bytes);
                                    clientSocket.Close();
                                }
                            }

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
        public ActionResult SeleccionarSucursalDespacho(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                view = "Finanzas/_SeleccionarSucursalDespacho";
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                ViewBag.RenderizadoSucursales = ModuleSucursal();
                ViewBag.RenderizadoImpresoras = ModuleImpresora();
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AnularBoxDespacho(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "REMOVEBOXDESPACHO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Finanzas/_CrearBoxDespacho";
                            ViewBag.ReferenciaInformacionDespacho = rows["Message"].ToString();
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
        public ActionResult CrearBoxDespacho()
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "CREATEBOXDESPACHO";
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
                valMantFin[12] = Request.ServerVariables["REMOTE_ADDR"];
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    view = "Finanzas/_AgregarContenedorDespacho";
                    ViewBag.RenderizadoDatosDespacho = ModuleDatosDespacho(rows["CodigoBox"].ToString());
                    ViewBag.RenderizadoDespachos = ModuleDespacho(rows["CodigoBox"].ToString());
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoBox"].ToString();
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ContinuarConBoxDespacho(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "VALBOXDESPACHO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Finanzas/_AgregarContenedorDespacho";
                            ViewBag.RenderizadoDatosDespacho = ModuleDatosDespacho(codigoProceso);
                            ViewBag.RenderizadoDespachos = ModuleDespacho(codigoProceso);
                            ViewBag.ReferenciaCodigoProceso = codigoProceso;
                            break;
                        case "404":
                            view = "Finanzas/_CrearBoxDespacho";
                            ViewBag.ReferenciaInformacionDespacho = rows["Message"].ToString();
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
        public ActionResult AgregarContenidoContenedorDespacho(string codigoProceso, string barcode)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "ADDCONTENTBOXDESPACHO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = barcode;
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Finanzas/_AgregarContenedorDespacho";
                            ViewBag.RenderizadoDatosDespacho = ModuleDatosDespacho(codigoProceso);
                            ViewBag.RenderizadoDespachos = ModuleDespacho(codigoProceso);
                            ViewBag.ReferenciaCodigoProceso = codigoProceso;
                            ViewBag.ReferenciaHtmlMessage = rows["Message"].ToString();
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
        public ActionResult RemoveDocumentoBoxDespacho(string codigoProceso, string codigoPago)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "REMOVEDOCBOXDESPACHO";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = codigoPago;
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Finanzas/_AgregarContenedorDespacho";
                            ViewBag.RenderizadoDatosDespacho = ModuleDatosDespacho(codigoProceso);
                            ViewBag.RenderizadoDespachos = ModuleDespacho(codigoProceso);
                            ViewBag.ReferenciaCodigoProceso = codigoProceso;
                            ViewBag.ReferenciaHtmlMessage = rows["Message"].ToString();
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
        public ActionResult IndicarEmpresaOrigenCheque(string codigoProceso)
        {
            string view = string.Empty;

            try
            {

                if (ModuleChequesEnProceso(codigoProceso).Count > 0)
                {
                    view = "Finanzas/_IndicarEmpresaOrigenCheque";
                    ViewBag.ReferenciaCodigoProceso = codigoProceso;
                }
                else
                {
                    view = "Finanzas/_NotIndicarEmpresaOrigenCheque";
                    ViewBag.ReferenciaCodigoProceso = codigoProceso;
                }
                
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult EmitirProcesoCHQ(string codigoProceso, string empresa)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "EMICHQ";
                valMantFin[1] = "";
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
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
                valMantFin[17] = empresa;
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch(rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.EmbedPdf = ModuleControlRetornoPath() + "/Finanzas/ViewPdf?pdf=Cheque&data=" + codigoProceso;
                            view = "Finanzas/_EmisionCheque";
                            break;
                    }
                }
                
            }
            catch (Exception ex)
            {
                
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult AgregarChequeraDigital(string banco, string empresaOrigen, string serie, string correlativo)
        {
            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "ADDCHQ";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = banco;
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = "";
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = correlativo;
                valMantFin[16] = serie;
                valMantFin[17] = empresaOrigen;
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["code"].ToString())
                    {
                        case "200":
                            ViewBag.RenderizadoIndicesCheques = ModuleIndices();
                            break;
                    }
                }

                
            }
            catch (Exception)
            {

            }

            return PartialView("Finanzas/_IndicesCheques");
        }

        [HttpPost]
        public ActionResult ChangeEstadoCtaOrigen(string banco, string cuenta, string estado = "N", string paginationIndex = "MS01")
        {
            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "DISABLEDCTAORIGEN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = banco;
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = cuenta;
                valMantFin[11] = "";
                valMantFin[12] = "";
                valMantFin[13] = "";
                valMantFin[14] = estado;
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["code"].ToString())
                    {
                        case "200":
                            ViewBag.RenderizadoCuentasOrigen = ModuleCuentasOrigen(paginationIndex, "", "");
                            ViewBag.RenderizadoPaginations = ModulePagination("CuentasOrigen", paginationIndex, "", "");
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }

            return PartialView("Finanzas/_CuentasOrigen");
        }

        [HttpPost]
        public ActionResult RenderizadoCuentasOrigen(string pagination, string typeFilter = "", string dataFilter = "")
        {
            try
            {
                ViewBag.RenderizadoCuentasOrigen = ModuleCuentasOrigen(pagination, typeFilter, dataFilter);
                ViewBag.RenderizadoPaginations = ModulePagination("CuentasOrigen", pagination, typeFilter, dataFilter);
            }
            catch (Exception)
            {

            }

            return PartialView("Finanzas/_CuentasOrigen");
        }

        [HttpPost]
        public ActionResult modalCuentasBancarias()
        {
            return PartialView("Finanzas/_CuentasBancariasAgregar");
        }

        [HttpPost]
        public ActionResult AgregarCuentaBancaria(string banco, string numeroCuenta, string rutCuenta, string glosa, string paginationIndex = "MS01")
        {
            string view = string.Empty;

            try
            {

                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "ADDCTACTE";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = "";
                valMantFin[6] = "";
                valMantFin[7] = banco;
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = numeroCuenta;
                valMantFin[11] = rutCuenta;
                valMantFin[12] = glosa;
                valMantFin[13] = "";
                valMantFin[14] = "";
                valMantFin[15] = "";
                valMantFin[16] = "";
                valMantFin[17] = "";
                valMantFin[18] = paginationIndex;
                valMantFin[19] = "";
                valMantFin[20] = "";

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.ReferenciaHtmlSuccess = rows["Message"].ToString();
                            view = "Finanzas/_InfoSuccessCtaBank";
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
                            break;
                        default:
                            ViewBag.ReferenciaHtmlError = rows["Message"].ToString();
                            view = "Finanzas/_InfoErrorCtaBank";
                            ViewBag.ReferenciaPaginationIndex = paginationIndex;
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
        public ActionResult IniciarProceso(string codigoPago, string glosaPago, string typeTEF = "normal")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            List<Models.Finanzas.Pagos> pagos = new List<Models.Finanzas.Pagos>();
            DataSet data;

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

            valMantFin[0] = "VERPROCREATE";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = codigoPago;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Type"].ToString())
                {
                    case "Old":
                        switch (rows["Pago"].ToString())
                        {
                            case "TEF":
                                ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas();
                                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                                ViewBag.ReferenciaMetodoPago = codigoPago;
                                ViewBag.TypeFilterTefConfirmadas = "";
                                ViewBag.DataFilterTefConfirmadas = "";
                                ViewBag.OrderByTefConfirmadasRut = "DESC";
                                ViewBag.OrderByTefConfirmadasNombres = "DESC";
                                ViewBag.TypeTEF = "normal";

                                view = "Finanzas/_ProcesoTEFPago";
                                break;
                            case "TEF_DT":
                                ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas(typeTEF: "dt");
                                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                                ViewBag.ReferenciaMetodoPago = codigoPago;
                                ViewBag.TypeFilterTefConfirmadas = "";
                                ViewBag.DataFilterTefConfirmadas = "";
                                ViewBag.OrderByTefConfirmadasRut = "DESC";
                                ViewBag.OrderByTefConfirmadasNombres = "DESC";
                                ViewBag.TypeTEF = "dt";

                                view = "Finanzas/_ProcesoTEFPago";
                                break;
                            case "CHQ":
                                ViewBag.RenderizadoCheques = ModuleChequesConfirmados();
                                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                                ViewBag.ReferenciaMetodoPago = codigoPago;

                                view = "Finanzas/_ProcesoCHQPago";
                                break;
                            case "MCHQ":
                                ViewBag.RenderizadoCheques = ModuleChequesConfirmados();
                                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                                ViewBag.ReferenciaMetodoPago = codigoPago;

                                view = "Finanzas/_ProcesoCHQPago";
                                break;

                            case "VV":
                                ViewBag.RenderizadoValeVista = ModuleValeVistaConfirmadas();
                                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                                ViewBag.ReferenciaMetodoPago = codigoPago;

                                view = "Finanzas/_ProcesoVVPago";
                                break;
                        }
                        break;
                    case "New":
                        ViewBag.ReferenciaMetodoPago = codigoPago;
                        ViewBag.ReferenciaGlosaPago = glosaPago;
                        view = "Finanzas/_ConfirmarCreacionProceso";
                        break;
                }
            }
            
            return PartialView(view);
        }
        
        [HttpPost]
        public ActionResult CrearProceso(string codigoPago)
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "CREATEPROC";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = codigoPago;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (codigoPago)
                    {
                        case "CHQ":
                            ViewBag.RenderizadoCheques = ModuleChequesConfirmados();
                            ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                            ViewBag.ReferenciaMetodoPago = codigoPago;

                            view = "Finanzas/_ProcesoCHQPago";
                            break;
                        case "TEF":
                            ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas();
                            ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                            ViewBag.ReferenciaMetodoPago = codigoPago;
                            ViewBag.OrderByTefConfirmadasRut = "DESC";
                            ViewBag.OrderByTefConfirmadasNombres = "DESC";

                            view = "Finanzas/_ProcesoTEFPago";
                            break;
                        case "VV":
                            ViewBag.RenderizadoValeVista = ModuleValeVistaConfirmadas();
                            ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                            ViewBag.ReferenciaMetodoPago = codigoPago;
                            ViewBag.OrderByTefConfirmadasRut = "DESC";
                            ViewBag.OrderByTefConfirmadasNombres = "DESC";

                            view = "Finanzas/_ProcesoVVPago";
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
        public ActionResult IncorporarTransferenciaPago(string codigoProceso, string codigoPago, string tipoPago, string origen = "FNQ", string typeTEF = "normal")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet data;

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

            valMantFin[0] = "ADDTEFPROCC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = tipoPago;
            valMantFin[4] = codigoPago;
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas(typeTEF: typeTEF);
                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                ViewBag.ReferenciaMetodoPago = codigoPago;
                ViewBag.TypeTEF = typeTEF;

                view = "Finanzas/_TEFConfirmadas";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult IncorporarChequePago(string codigoProceso, string codigoPago, string tipoPago, string origen = "FNQ")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet data;

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

            valMantFin[0] = "ADDCHQPROCC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = tipoPago;
            valMantFin[4] = codigoPago;
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                ViewBag.RenderizadoCheques = ModuleChequesConfirmados();
                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                ViewBag.ReferenciaMetodoPago = codigoPago;

                view = "Finanzas/_CHQConfirmadas";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult IncorporarValeVistaPago(string codigoProceso, string codigoPago, string tipoPago, string origen = "FNQ")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet data;

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

            valMantFin[0] = "ADDVVPROCC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = tipoPago;
            valMantFin[4] = codigoPago;
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                ViewBag.RenderizadoValeVista = ModuleValeVistaConfirmadas();
                ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                ViewBag.ReferenciaMetodoPago = codigoPago;

                view = "Finanzas/_VVConfirmadas";
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirIncorporacionTransferenciaPago(string codigoPago, string tipoPago, string codigoProceso, string origen = "FNQ", string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet data;

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


                valMantFin[0] = "REMOVTEFPROCC";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
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
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = "";
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso);
                    ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                    ViewBag.ReferenciaMetodoPago = codigoPago;
                    ViewBag.TypeTEF = typeTEF;

                    view = "Finanzas/_TEFPendientesEmision";
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirIncorporacionValeVistaPago(string codigoPago, string tipoPago, string codigoProceso, string origen = "FNQ")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet data;

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


                valMantFin[0] = "REMOVVVPROCC";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
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
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = "";
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    ViewBag.RenderizadoValeVista = ModuleValeVistaEnProceso(codigoProceso);
                    ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoVV(codigoProceso);
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                    ViewBag.ReferenciaMetodoPago = codigoPago;

                    view = "Finanzas/_VVPendientesEmision";
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult RevertirIncorporacionChequePago(string codigoPago, string tipoPago, string codigoProceso, string origen = "FNQ")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet data;

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

                valMantFin[0] = "REMOVCHQPROCC";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
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
                valMantFin[21] = "";
                valMantFin[22] = "";
                valMantFin[23] = "";
                valMantFin[24] = "";
                valMantFin[25] = "";
                valMantFin[26] = "";
                valMantFin[27] = origen;

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    ViewBag.RenderizadoCheques = ModuleChequesEnProceso(codigoProceso);
                    ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoCHQ(codigoProceso);
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                    ViewBag.ReferenciaMetodoPago = codigoPago;

                    view = "Finanzas/_CHQPendientesEmision";
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConfirmacionProcesoTEF(string codigoProceso, string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                
                ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso);
                ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                ViewBag.TypeTEF = typeTEF;

                view = "Finanzas/_PendienteEmisionProcesoTEF";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConfirmacionProcesoVV(string codigoProceso)
        {
            string view = string.Empty;

            try
            {

                ViewBag.RenderizadoValeVista = ModuleValeVistaEnProceso(codigoProceso);
                ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;

                view = "Finanzas/_PendienteEmisionProcesoVV";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConfirmacionProcesoCHQ(string codigoProceso)
        {
            string view = string.Empty;

            try
            {

                ViewBag.RenderizadoCheques = ModuleChequesEnProceso(codigoProceso);
                ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoCHQ(codigoProceso);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;

                view = "Finanzas/_PendienteEmisionProcesoCHQ";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }
        
        [HttpPost]
        public ActionResult IndicarCuentaOrigenTEF(string codigoProceso, string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                ViewBag.RenderizadoCuentasOrigenDDL = ModuleCuentasOrigenDDL();
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                ViewBag.TypeTEF = typeTEF;

                view = "Finanzas/_SeleccionarCuentaOrigenTEF";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost] 
        public ActionResult ReplicaArchivoTEF(string codigoProceso)
        {
            string empresa = string.Empty;
            string fecha = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            string[] columns = new string[13];
            string[] nameColumns = new string[13];
            string[] paramCorreo = new string[5];
            string[] valCorreo = new string[5];
            DataSet data;
            DataSet dataCorreo;

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

            valMantFin[0] = "EXCELTRANSFERENCIAS";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;
            
            paramCorreo[0] = "@AUTHENTICATE";
            paramCorreo[1] = "@AGENTAPP";
            paramCorreo[2] = "@PLANTILLACORREO";
            paramCorreo[3] = "@CODIGOTRANSACCION";
            paramCorreo[4] = "@PATHFILE";

            valCorreo[0] = tokenAuth;
            valCorreo[1] = agenteAplication;
            valCorreo[2] = "CorreoComprobanteTEF";
            valCorreo[3] = codigoProceso;
            valCorreo[4] = "-";

            dataCorreo = servicioOperaciones.GetPlantillasCorreos(paramCorreo, valCorreo).Table;

            foreach (DataRow rowsCorreo in dataCorreo.Tables[0].Rows)
            {
                Files files = new Files();
                files = XLSX_ReporteTransferencias.xlsx(data.Tables[0].Rows);
                Attachament[] attachaments = new Attachament[files.ListFile.Count];

                for (int i = 0; i < files.ListFile.Count; i++)
                {
                    Attachament attachament = new Attachament();
                    attachament.File = files.ListFile[i];
                    attachament.Name = string.Format("Archivo de transferencias {0} {1}{2}.xlsx", files.Empresa, files.Fecha, i > 0 ? " " + i.ToString() : "");
                    attachament.ApplicationType = "application/excel";

                    attachaments[i] = attachament;
                }

                if (servicioCorreo.correoTeamworkCCOAttachaments(rowsCorreo["De"].ToString(),
                                                                rowsCorreo["Clave"].ToString(),
                                                                rowsCorreo["Para"].ToString(),
                                                                rowsCorreo["Html"].ToString(),
                                                                rowsCorreo["Asunto"].ToString(),
                                                                rowsCorreo["CC"].ToString(),
                                                                rowsCorreo["CCO"].ToString(),
                                                                rowsCorreo["Importancia"].ToString(),
                                                                attachaments))
                {
                    //view = "Finanzas/_EmisionSolicitudTEF";
                }
            }

            return PartialView("");
        }

        [HttpPost]
        public ActionResult SeleccionarCuentaBancariaOrigen(string codigoProceso, string numeroCuenta)
        {
            string view = string.Empty;

            try
            {
                string empresa = string.Empty;
                string fecha = string.Empty;
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                string[] columns = new string[13];
                string[] nameColumns = new string[13];
                string[] paramCorreo = new string[5];
                string[] valCorreo = new string[5];
                DataSet data;
                DataSet dataCorreo;

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

                valMantFin[0] = "ADDCTAORIGENP";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
                valMantFin[6] = "";
                valMantFin[7] = "";
                valMantFin[8] = "";
                valMantFin[9] = "";
                valMantFin[10] = numeroCuenta;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    valMantFin[0] = "EXCELTRANSFERENCIAS";
                    valMantFin[1] = Session["base64Username"].ToString();
                    valMantFin[2] = "";
                    valMantFin[3] = "";
                    valMantFin[4] = "";
                    valMantFin[5] = codigoProceso;
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

                    data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                    paramCorreo[0] = "@AUTHENTICATE";
                    paramCorreo[1] = "@AGENTAPP";
                    paramCorreo[2] = "@PLANTILLACORREO";
                    paramCorreo[3] = "@CODIGOTRANSACCION";
                    paramCorreo[4] = "@PATHFILE";

                    valCorreo[0] = tokenAuth;
                    valCorreo[1] = agenteAplication;
                    valCorreo[2] = "CorreoComprobanteTEF";
                    valCorreo[3] = codigoProceso;
                    valCorreo[4] = "-";

                    dataCorreo = servicioOperaciones.GetPlantillasCorreos(paramCorreo, valCorreo).Table;

                    foreach (DataRow rowsCorreo in dataCorreo.Tables[0].Rows)
                    {
                        Files files = new Files();
                        files = XLSX_ReporteTransferencias.xlsx(data.Tables[0].Rows);
                        Attachament[] attachaments = new Attachament[files.ListFile.Count];

                        for (int i = 0; i < files.ListFile.Count; i++)
                        {
                            Attachament attachament = new Attachament();
                            attachament.File = files.ListFile[i];
                            attachament.Name = string.Format("Archivo de transferencias {0} {1}{2}.xlsx", files.Empresa, files.Fecha, i > 0 ? " " + i.ToString() : "");
                            attachament.ApplicationType = "application/excel";

                            attachaments[i] = attachament;
                        }

                        if (servicioCorreo.correoTeamworkCCOAttachaments(rowsCorreo["De"].ToString(),
                                                                rowsCorreo["Clave"].ToString(),
                                                                rowsCorreo["Para"].ToString(),
                                                                rowsCorreo["Html"].ToString(),
                                                                rowsCorreo["Asunto"].ToString(),
                                                                rowsCorreo["CC"].ToString(),
                                                                rowsCorreo["CCO"].ToString(),
                                                                rowsCorreo["Importancia"].ToString(),
                                                                attachaments))
                        {
                            view = "Finanzas/_EmisionSolicitudTEF";
                        }
                    }

                }
            }
            catch (Exception)
            {

            }
            
            return PartialView(view);
        }

        [HttpPost]
        public ActionResult EmitirValeVista(string codigoProceso)
        {

            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                string[] columns = new string[13];
                string[] nameColumns = new string[13];
                string[] paramCorreo = new string[5];
                string[] valCorreo = new string[5];
                DataSet data;
                DataSet dataCorreo;

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

                valMantFin[0] = "EmitirValeVista";
                valMantFin[1] = Session["CodificarAuth"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = "";
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            excel.Workbook.Properties.Title = "Attempts";

                            paramCorreo[0] = "@AUTHENTICATE";
                            paramCorreo[1] = "@AGENTAPP";
                            paramCorreo[2] = "@PLANTILLACORREO";
                            paramCorreo[3] = "@CODIGOTRANSACCION";
                            paramCorreo[4] = "@PATHFILE";

                            valCorreo[0] = tokenAuth;
                            valCorreo[1] = agenteAplication;
                            valCorreo[2] = "CorreoComprobanteVV";
                            valCorreo[3] = codigoProceso;
                            valCorreo[4] = "Archivo de Vale Vista.xlsx";

                            dataCorreo = servicioOperaciones.GetPlantillasCorreos(paramCorreo, valCorreo).Table;

                            foreach (DataRow rowsCorreo in dataCorreo.Tables[0].Rows)
                            {
                                if (servicioCorreo.correoTeamworkCCOAttachament(rowsCorreo["De"].ToString(),
                                                                                rowsCorreo["Clave"].ToString(),
                                                                                rowsCorreo["Para"].ToString(),
                                                                                rowsCorreo["Html"].ToString(),
                                                                                rowsCorreo["Asunto"].ToString(),
                                                                                rowsCorreo["CC"].ToString(),
                                                                                rowsCorreo["CCO"].ToString(),
                                                                                rowsCorreo["Attachement"].ToString(),
                                                                                rowsCorreo["Importancia"].ToString(),
                                                                                XLSX_ReporteValeVista.__xlsx(Session["CodificarAuth"].ToString(), codigoProceso, servicioOperaciones),
                                                                                "application/excel"))
                                {
                                    view = "Finanzas/_EmisionSolicitudVV";
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReturnStageIncorporacionTransferencias(string codigoProceso, string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas(typeTEF: typeTEF);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                ViewBag.TypeTEF = typeTEF;

                view = "Finanzas/_ProcesoTEFPago";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReturnStageIncorporacionValeVista(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                ViewBag.RenderizadoValeVista = ModuleValeVistaConfirmadas();
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                view = "Finanzas/_ProcesoVVPago";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReturnStageIncorporacionCheques(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                ViewBag.RenderizadoCheques = ModuleChequesConfirmados();
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                view = "Finanzas/_ProcesoCHQPago";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReturnStageInicial()
        {
            return PartialView("Finanzas/_ProcesoPago");
        }

        [HttpPost]
        public ActionResult MarcarNoTEFNotariado(string codigoPago, string tipoPago, string codigoProceso, string origen = "FNQ", string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet data;

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

                valMantFin[0] = "NONOTTEFADD";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso);
                    ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                    ViewBag.ReferenciaMetodoPago = codigoPago;
                    ViewBag.ReferenciaCodigoProceso = codigoProceso;
                    ViewBag.TypeTEF = typeTEF;

                    view = "Finanzas/_TEFPendientesEmision";
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult MarcarTEFNotariado(string codigoPago, string tipoPago, string codigoProceso, string origen = "FNQ", string typeTEF = "normal")
        {
            string view = string.Empty;

            try
            {
                string[] paramMantFin = new string[28];
                string[] valMantFin = new string[28];
                DataSet data;

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


                valMantFin[0] = "NOTTEFADD";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = tipoPago;
                valMantFin[4] = codigoPago;
                valMantFin[5] = codigoProceso;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso);
                    ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                    ViewBag.ReferenciaCodigoProceso = rows["CodigoProceso"].ToString();
                    ViewBag.ReferenciaMetodoPago = codigoPago;
                    ViewBag.ReferenciaCodigoProceso = codigoProceso;
                    ViewBag.TypeTEF = typeTEF;

                    view = "Finanzas/_TEFPendientesEmision";
                }
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult PagarProceso(string codigoTEF, string codigoProceso)
        {
            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "PAGTEF";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = "";
                valMantFin[3] = "";
                valMantFin[4] = codigoTEF;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.RenderizadoSolicitudesTEF = ModuleSolicitudesTEF(codigoProceso);
                            break;
                    }
                    
                }
            }
            catch (Exception)
            {

            }

            return PartialView("Finanzas/_PagosPendientes");
        }

        [HttpPost]
        public ActionResult NotariarTef(string finiquito, string codigoProceso)
        {
            try
            {
                string[] paramMantFin = new string[21];
                string[] valMantFin = new string[21];
                DataSet data;

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

                valMantFin[0] = "NOTFIN";
                valMantFin[1] = Session["base64Username"].ToString();
                valMantFin[2] = finiquito;
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

                data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            ViewBag.RenderizadoSolicitudesTEF = ModuleSolicitudesTEF(codigoProceso);
                            break;
                    }
                }
            }
            catch (Exception)
            {

            }

            return PartialView("Finanzas/_PagosPendientes");
        }

        [HttpPost]
        public ActionResult ActualizarTEFConfirmadas(string codigoProceso, string tipoPago, string typeFilter = "", string dataFilter = "", string orderByRut = "", string orderByNombres = "", string typeTEF = "normal")
        {
            string orderBy = typeFilter == "Rut" 
                                ? orderByRut 
                                : typeFilter == "Nombres" 
                                    ? orderByNombres 
                                    : "";

            ViewBag.RenderizadoTransferencias = ModuleTransferenciasConfirmadas(typeFilter, dataFilter, orderBy, typeTEF);

            ViewBag.OrderByTefConfirmadasRut = typeFilter == "Rut"
                                                 ? orderByRut == "DESC" 
                                                      ? "ASC" 
                                                      : "DESC" 
                                                 : orderByRut; 
            ViewBag.OrderByTefConfirmadasNombres = typeFilter == "Nombres"
                                                     ? orderByNombres == "DESC"
                                                          ? "ASC"
                                                          : "DESC"
                                                     : orderByNombres;

            ViewBag.ReferenciaCodigoProceso = codigoProceso;
            ViewBag.ReferenciaMetodoPago = tipoPago;
            ViewBag.TypeTEF = typeTEF;

            return PartialView("Finanzas/_TEFConfirmadas");
        }

        [HttpPost]
        public ActionResult ActualizarVVConfirmadas(string codigoProceso, string tipoPago, string typeFilter = "", string dataFilter = "", string orderByRut = "", string orderByNombres = "")
        {
            string orderBy = typeFilter == "Rut"
                                ? orderByRut
                                : typeFilter == "Nombres"
                                    ? orderByNombres
                                    : "";

            ViewBag.RenderizadoValeVista = ModuleValeVistaConfirmadas(typeFilter, dataFilter, orderBy);

            ViewBag.OrderByTefConfirmadasRut = typeFilter == "Rut"
                                                 ? orderByRut == "DESC"
                                                      ? "ASC"
                                                      : "DESC"
                                                 : orderByRut;
            ViewBag.OrderByTefConfirmadasNombres = typeFilter == "Nombres"
                                                     ? orderByNombres == "DESC"
                                                          ? "ASC"
                                                          : "DESC"
                                                     : orderByNombres;

            ViewBag.ReferenciaCodigoProceso = codigoProceso;
            ViewBag.ReferenciaMetodoPago = tipoPago;

            return PartialView("Finanzas/_VVConfirmadas");
        }

        [HttpPost]
        public ActionResult ActualizarChequesConfirmadas()
        {
            ViewBag.RenderizadoCheques = ModuleChequesConfirmados();

            return PartialView("Finanzas/_CHQConfirmadas");
        }

        [HttpPost]
        public ActionResult ActualizarTEFIncorporadasConfirmadas(string codigoProceso, string typeFilter = "", string dataFilter = "", string typeTEF = "normal")
        {
            ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso, typeFilter, dataFilter);
            ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
            ViewBag.ReferenciaCodigoProceso = codigoProceso;
            ViewBag.TypeTEF = typeTEF;

            return PartialView("Finanzas/_TEFPendientesEmision");
        }

        [HttpPost]
        public ActionResult ActualizarVVIncorporadasConfirmadas(string codigoProceso, string typeFilter = "", string dataFilter = "")
        {
            ViewBag.RenderizadoValeVista = ModuleValeVistaEnProceso(codigoProceso, typeFilter, dataFilter);
            ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoVV(codigoProceso);
            ViewBag.ReferenciaCodigoProceso = codigoProceso;

            return PartialView("Finanzas/_VVPendientesEmision");
        }

        [HttpPost]
        public ActionResult ActualizarCHQIncorporadasConfirmadas(string codigoProceso)
        {
            ViewBag.RenderizadoCheques = ModuleChequesEnProceso(codigoProceso);
            ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoCHQ(codigoProceso);
            ViewBag.ReferenciaCodigoProceso = codigoProceso;

            return PartialView("Finanzas/_CHQPendientesEmision");
        }

        [HttpPost]
        public ActionResult ReturnStageConfirmacionTransferencias(string codigoProceso)
        {
            string view = string.Empty;

            try
            {

                ViewBag.RenderizadoTransferencias = ModuleTransferenciasEnProceso(codigoProceso);
                ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoTEF(codigoProceso);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                view = "Finanzas/_TEFPendientesEmision";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ReturnStageConfirmacionCheques(string codigoProceso)
        {
            string view = string.Empty;

            try
            {
                ViewBag.RenderizadoCheques = ModuleChequesEnProceso(codigoProceso);
                ViewBag.RenderizadoDatosProceso = ModuleDatosProcesoCHQ(codigoProceso);
                ViewBag.ReferenciaCodigoProceso = codigoProceso;
                view = "Finanzas/_PendienteEmisionProcesoCHQ";
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult IniciarProcesoMasivoPago(string codigoProceso, string codigoPago)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "INITPAUTCPG";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.RedireccionarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + codigoProceso + "/PagoMasivo";
                        ViewBag.OptionConfirmacionEvent = "InitProcesoLoading";
                        view = "Finanzas/_ConfirmacionEvento";
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult IncluirPagoProcesoMasivo(string codigoProceso, string codigoPago)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "ADDPGAUTCPG";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = codigoPago;
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.RedireccionarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + codigoProceso + "/PagoMasivo";
                        ViewBag.OptionConfirmacionEvent = "Confirmacion";
                        ViewBag.Message = rows["Message"].ToString();
                        ViewBag.OptionConfirmacionEvent = "EventoConfirmadoRedir";
                        view = "Finanzas/_ConfirmacionEvento";
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ExcluirPagoProcesoMasivo(string codigoProceso, string codigoPago)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "DELPGPAUTCPG";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = codigoPago;
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        ViewBag.RedireccionarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + codigoProceso + "/PagoMasivo";
                        ViewBag.OptionConfirmacionEvent = "EventoConfirmadoRedir";
                        ViewBag.Message = rows["Message"].ToString();
                        view = "Finanzas/_ConfirmacionEvento";
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ConfirmarEvento(string option, string codigoProceso, string codigoPago = "")
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            switch (option)
            {
                case "IniciarProcesoMasivoPago":
                    view = "Finanzas/_ConfirmacionEvento";
                    ViewBag.CodigoProceso = codigoProceso;
                    ViewBag.OptionConfirmacionEvent = option;
                    break;
                case "IncluirPagoProceso":
                    view = "Finanzas/_ConfirmacionEvento";
                    ViewBag.CodigoProceso = codigoProceso;
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.OptionConfirmacionEvent = option;
                    break;
                case "ExcluirPagoProceso":
                    view = "Finanzas/_ConfirmacionEvento";
                    ViewBag.CodigoProceso = codigoProceso;
                    ViewBag.CodigoPago = codigoPago;
                    ViewBag.OptionConfirmacionEvent = option;
                    break;
                case "PagarMasivo":
                    view = "Finanzas/_ConfirmacionEvento";
                    ViewBag.CodigoProceso = codigoProceso;
                    ViewBag.OptionConfirmacionEvent = option;
                    break;
                case "TotalesFiniquito":
                    view = "Finanzas/_ConfirmacionEvento";
                    ViewBag.RenderizadoMontosTotales = ModuleTotalesFiniquitos(codigoPago);
                    ViewBag.OptionConfirmacionEvent = option;
                    break;
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult PagarMasivamente(string codigoProceso)
        {
            string view = string.Empty;
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "PAGARMASIVO";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                switch (rows["Code"].ToString())
                {
                    case "200":
                        switch (rows["Redirect"].ToString())
                        {
                            case "Owner":
                                ViewBag.RedireccionarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + codigoProceso;
                                break;
                            case "None":
                                ViewBag.RedireccionarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos";
                                break;
                        }
                        
                        ViewBag.OptionConfirmacionEvent = "EventoConfirmadoRedir";
                        ViewBag.Message = rows["Message"].ToString();
                        view = "Finanzas/_ConfirmacionEvento";
                        break;
                }
            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult ProcesosPagos(string pagination)
        {

            ViewBag.RenderizadoProcesosPago = ModuleProcesosPago(pagination);
            ViewBag.RenderizadoPaginationProcesosPago = ModulePagination("ProcesosPago", pagination);

            return PartialView("Finanzas/_ProcesosPagosPendientes");
        }

        #endregion

        #region "Modal de confirmacion"

        public ActionResult ModalConfirmacionRechazoCargoMod(string estado, string codigoCargoMod, string origen)
        {
            ViewBag.Estado = estado;
            ViewBag.CodigoCargoMod = codigoCargoMod;
            ViewBag.Origen = origen;

            return PartialView("Finanzas/CargoMod/_ModalConfirmacion");
        }

        public ActionResult ModalHistorialCargoMod(string codigoCargoMod)
        {
            List<HistorialCargoMod> historial = new List<HistorialCargoMod>();


            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic historia = JsonConvert.DeserializeObject(Cargo.__HistorialCargoMod(codigoCargoMod, objects[0].Token.ToString()));

            for (var i = 0; i < historia.Count; i++)
            {
                HistorialCargoMod item = new HistorialCargoMod();

                item.Usuario = historia[i].Usuario.ToString();
                item.Descripcion = historia[i].Descripcion.ToString();
                item.Fecha = historia[i].Fecha.ToString();
                item.Estado = historia[i].Estado.ToString();
                
                historial.Add(item);
            }

            ViewBag.Historial = historial;

            return PartialView("Finanzas/CargoMod/_ModalHistorialCargoMod");
        }

        #endregion

        #region "Metodos Http API"

        public ActionResult CrearSolicitud(string empresa, string evento)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CrearSolicitud(empresa, Session["base64Username"].ToString(), evento, objects[0].Token.ToString()));

            ViewBag.RenderizadoSolicitudCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", objectsCM[0].Codine.ToString());
            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.CodigoSolicitud = objectsCM[i].Codine.ToString();
            }

            ViewBag.Redireccionamiento = "";
            ViewBag.EmpresaMandante = empresa;
            ViewBag.TipoEvento = evento;

            switch (evento)
            {
                case "Uw==":
                    ViewBag.TypeAmbiente = "Ambiente a crear : Cotización";
                    break;
                case "RQ==":
                    ViewBag.TypeAmbiente = "Ambiente a crear : Solicitud de cargo mod";
                    break;
            }

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.RenderizadoClientes = ModuleClientes(empresa, "", "", "", Session["base64Username"].ToString());
            }

            return PartialView("Finanzas/_DDLSeleccioneCliente");
        }

        public ActionResult DeshacerSolicitud(string codigoSolicitud, string origen)
        {
            string view = string.Empty;
            string controller = "";

            switch (Session["Profile"].ToString())
            {
                case "KAM":
                    controller = "Operaciones";
                    break;
                case "ADMINISTRADOR":
                    controller = "Operaciones";
                    break;
                case "FINANZAS":
                    controller = "Finanzas";
                    break;
                case "ANALISTA DE REMUNERACIONES":
                    controller = "Remuneraciones";
                    break;
            }

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__DeshacerSolicitud(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                switch (origen)
                {
                    case "Edit":
                        view = "Finanzas/CargoMod/_Edit";
                        break;
                    case "Detail":
                        view = "Finanzas/CargoMod/_Detail";
                        break;
                    case "Solicitud":
                        ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/RQ==/Create";
                        ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/Uw==/Create";
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.HtmlMessageResult = objectsCM[i].Message.ToString();
                        view = "Finanzas/CargoMod/_ItemSolicitudes";
                        break;
                }
            }
            
            return PartialView(view);
        }

        public ActionResult ActualizarCliente(string cliente, string codigoSolicitud, string tipoEvento)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaCliente(Session["base64Username"].ToString(), cliente, codigoSolicitud, objects[0].Token.ToString()));
            
            ViewBag.Redireccionamiento = ModuleControlRetornoPath() + "/Finanzas/CargoMod/" + codigoSolicitud + "/Edit";
            ViewBag.TipoEvento = tipoEvento;

            return PartialView("Finanzas/_DDLSeleccioneCliente");
        }

        public ActionResult AutocompleteClientes(string empresa, string dataFilter = "")
        {
            ViewBag.RenderizadoClientes = ModuleClientes("", "", "Codigo", dataFilter, Session["base64Username"].ToString());

            return PartialView("Teamwork/_ClientesElementAutocomplete");
        }
        
        // NO UTILIZABLE
        public ActionResult CrearValorDiario(string empresa, string cargoMod, string cliente, string valorDiario)
        {
            string view = string.Empty;

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsValorDario = JsonConvert.DeserializeObject(ValorDiario.__ValorDiarioCrear(empresa, cliente, cargoMod, valorDiario, objects[0].Token.ToString()));

            ViewBag.RenderizadoValoresDiarios = ModuleValoresDiarios();
            ViewBag.RenderizadoPaginations = ModulePagination("ValorDiario", "MS01");

            for (var i = 0; i < objectsValorDario.Count; i++)
            {
                switch (objectsValorDario[i].Code)
                {
                    case "200":
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        view = "Finanzas/_ListaValoresDiarios";
                        break;
                    default:
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        view = "Finanzas/_ListaValoresDiarios";
                        break;
                }
            }


            return PartialView(view);
        }
        
        // NO UTILIZABLE
        public ActionResult ActualizarValorDiario(string empresa, string cargoMod, string cliente, string valorDiario)
        {
            string view = string.Empty;

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsValorDario = JsonConvert.DeserializeObject(ValorDiario.__ValorDiarioActualizar(empresa, cliente, cargoMod, valorDiario, objects[0].Token.ToString()));

            ViewBag.RenderizadoValoresDiarios = ModuleValoresDiarios();

            for (var i = 0; i < objectsValorDario.Count; i++)
            {
                switch (objectsValorDario[i].Code)
                {
                    case "200":
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        ViewBag.ResultEmpresa = empresa;
                        ViewBag.ResultCargoMod = cargoMod;
                        ViewBag.ResultEmpresa = cliente;
                        ViewBag.ResultValorDiario = valorDiario;
                        break;
                    default:
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        ViewBag.ResultEmpresa = "";
                        ViewBag.ResultCargoMod = "";
                        ViewBag.ResultEmpresa = "";
                        ViewBag.ResultValorDiario = "";
                        break;
                }
            }


            return PartialView(view);
        }

        // NO UTILIZABLE
        public ActionResult EliminarValorDiario(string empresa, string cargoMod)
        {
            string view = string.Empty;

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsValorDario = JsonConvert.DeserializeObject(ValorDiario.__ValorDiarioEliminar(empresa, cargoMod, objects[0].Token.ToString()));

            ViewBag.RenderizadoValoresDiarios = ModuleValoresDiarios();
            ViewBag.RenderizadoPaginations = ModulePagination("ValorDiario", "MS01");

            for (var i = 0; i < objectsValorDario.Count; i++)
            {
                switch (objectsValorDario[i].Code)
                {
                    case "200":
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        view = "Finanzas/_ListaValoresDiarios";
                        break;
                    default:
                        ViewBag.ResultInformationTransaction = objectsValorDario[i].Message;
                        ViewBag.ResultCodeTransaction = objectsValorDario[i].Code;
                        view = "Finanzas/_ListaValoresDiarios";
                        break;
                }
            }

            return PartialView(view);
        }
        
        public ActionResult ConfirmEventVD(string option = "", string empresa = "", string cargoMod = "")
        {
            string view = string.Empty;

            switch (option)
            {
                case "EliminarValorDiario":
                    ViewBag.OptionActionValorDiario = option;
                    ViewBag.Empresa = empresa;
                    ViewBag.CargoMod = cargoMod;
                    view = "Finanzas/_EliminarValorDiario";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ListarValoresDiarios(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {

            ViewBag.ResultInformationTransaction = "";
            ViewBag.RenderizadoValoresDiarios = ModuleValoresDiarios(pagination, typeFilter, dataFilter);
            ViewBag.RenderizadoPaginations = ModulePagination("ValorDiario", pagination, typeFilter, dataFilter);

            return PartialView("Finanzas/_ItemValoresDiarios");
        }

        public ActionResult ActualizaNombreCargo(string nombreCargo, string codigoSolicitud, string type)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaNombreCargo(Session["base64Username"].ToString(), nombreCargo, codigoSolicitud, type, objects[0].Token.ToString()));
            
            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoSolicitud);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoSolicitud);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoSolicitud);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoSolicitud);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoSolicitud);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoSolicitud);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ActualizaSueldo(string sueldo, string codigoSolicitud, string typeSueldo)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaSueldo(Session["base64Username"].ToString(), sueldo, codigoSolicitud, typeSueldo, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.HtmlMessageResult = (objectsCM[i].Message.ToString() != "") ? objectsCM[i].Message.ToString() : "";
            }

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoSolicitud);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoSolicitud);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoSolicitud);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoSolicitud);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoSolicitud);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoSolicitud);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ActualizaGratificacion(string gratificacion, string typeGratif, string gratifPactada, string codigoSolicitud, string observGratiConv = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaGratificacion(Session["base64Username"].ToString(),
                                                                                             gratificacion != null ? gratificacion : "",
                                                                                             typeGratif != null ? typeGratif : gratifPactada,
                                                                                             observGratiConv,
                                                                                             codigoSolicitud,
                                                                                             objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoSolicitud);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoSolicitud);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoSolicitud);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoSolicitud);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoSolicitud);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoSolicitud);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult AutocompleteBonos(string dataFilter = "", string cliente = "")
        {
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Nombre", dataFilter, cliente);

            return PartialView("Teamwork/_BonosElementAutocomplete");
        }

        public ActionResult AutocompleteANI(string dataFilter = "")
        {
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "Nombre", dataFilter);

            return PartialView("Teamwork/_ANIElementAutocomplete");
        }

        public ActionResult AgregarBono(string codigoBono, string codigoCargoMod, string monto, string condiciones = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Bonos.__AgregarBono(Session["base64Username"].ToString(), codigoBono, codigoCargoMod, monto, condiciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }
        
        public ActionResult EliminarBono(string codigoBono, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Bonos.__EliminarBono(Session["base64Username"].ToString(), codigoBono, codigoCargoMod, objects[0].Token.ToString()));
            
            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ActualizarBono(string codigoBono, string codigoCargoMod, string monto, string condiciones = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Bonos.__ActualizarBono(Session["base64Username"].ToString(), codigoBono, codigoCargoMod, monto, condiciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }
        
        public ActionResult AgregarANI(string codigoANI, string codigoCargoMod, string monto, string deft = null, string observaciones = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(ANIs.__AgregarANI(Session["base64Username"].ToString(), codigoANI, codigoCargoMod, monto, deft != null ? deft : "N", observaciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult EliminarANI(string codigoANI, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(ANIs.__EliminarANI(Session["base64Username"].ToString(), codigoANI, codigoCargoMod, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ActualizarANI(string codigoANI, string codigoCargoMod, string monto, string deft = null, string observaciones = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(ANIs.__ActualizarANI(Session["base64Username"].ToString(), codigoANI, codigoCargoMod, monto, deft != null ? deft : "N", observaciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }
        
        public ActionResult ActualizarAFP(string afp, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizarAFP(codigoCargoMod, afp, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult CambiarInputSueldo(string sueldo, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarInputSueldo(codigoCargoMod, sueldo, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult CambiarCalculoTypeSueldo(string sueldo, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarCalculoTypeSueldo(codigoCargoMod, sueldo, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult CambiarTypeJornada(string typeJornada, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarTypeJornada(codigoCargoMod, typeJornada, objects[0].Token.ToString()));
            
            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult CambioEstadoSolicitud(string estado, string codigoCargoMod, string origen, string observaciones = "")
        {
            string view = string.Empty;
            string controller = "";

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambioEstadoSolicitud(Session["base64Username"].ToString(), codigoCargoMod, estado, observaciones, objects[0].Token.ToString()));
            
            switch (Session["Profile"].ToString())
            {
                case "KAM":
                    controller = "Operaciones";
                    break;
                case "ADMINISTRADOR":
                    controller = "Operaciones";
                    break;
                case "FINANZAS":
                    controller = "Finanzas";
                    break;
                case "ANALISTA DE REMUNERACIONES":
                    controller = "Remuneraciones";
                    break;
            }

            for (var i = 0; i < objectsCM.Count; i++)
            {
                if (objectsCM[i].Codine != null)
                {
                    if (objectsCM[i].Codine.ToString() != codigoCargoMod)
                    {
                        ViewBag.HttpRedirectChangeCodigoCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/" + objectsCM[i].CodineCodify.ToString() + "/Detail";
                        
                        /** INIT [Se genero codine para softland => migración de dato mediante API] */

                        dynamic objectsCMSoftlandCrearCargo = JsonConvert.DeserializeObject(CallAPISoftland.__SoftlandCargoModCrear(objectsCM[i].Codine.ToString(), objectsCM[i].Glosa.ToString(), objectsCM[i].Empresa.ToString(), objects[0].Token.ToString()));

                        /** End [Se genero codine para softland => migración de dato mediante API] */
                    }
                }
                
                switch (origen)
                {
                    case "Edit":

                        switch (objectsCM[i].Code.ToString())
                        {
                            case "200":
                                ViewBag.OptionCargoMod = "SuccessSolicCargo";
                                ViewBag.OptionStageCargoMod = "Send";
                                ViewBag.HtmlMessageResult = objectsCM[i].Message.ToString();
                                break;
                            case "300":
                                ViewBag.ErrorSolicitudCargoMod = objectsCM[i].Message.ToString();
                                ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
                                ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
                                ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
                                ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
                                ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
                                ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                                ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
                                ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
                                ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);
                                break;
                        }
                        view = "Finanzas/CargoMod/_Edit";
                        break;
                    case "Detail":
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);
                        ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
                        ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
                        ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
                        ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
                        ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
                        ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                        ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
                        ViewBag.HtmlMessageResult = objectsCM[i].MessageSimple.ToString();
                        view = "Finanzas/CargoMod/_Detail";
                        break;
                    case "Solicitud":
                        ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/RQ==/Create";
                        ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/Uw==/Create";
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.HtmlMessageResult = objectsCM[i].MessageSimple.ToString();
                        view = "Finanzas/CargoMod/_ItemSolicitudes";
                        break;
                }
            }

            return PartialView(view);
        }

        public ActionResult ActualizarJornadaPTSueldo(string typeJornadaCalculo, string valorDiario, string horasSemanales, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCMTJ = JsonConvert.DeserializeObject(Cargo.__CambiarTypeJornada(codigoCargoMod, typeJornadaCalculo, objects[0].Token.ToString()));
            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaValorDiaPT(Session["base64Username"].ToString(), codigoCargoMod, valorDiario, horasSemanales, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }
        
        public ActionResult ActualizaObservaciones(string observaciones, string codigoCargoMod, string type)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaObservaciones(Session["base64Username"].ToString(), codigoCargoMod, observaciones, type, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult EliminarObservaciones(string observaciones, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__EliminarObservaciones(Session["base64Username"].ToString(), codigoCargoMod, observaciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ActualizaHorario(string horarios, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaHorario(Session["base64Username"].ToString(), codigoCargoMod, horarios, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(codigoCargoMod);
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", codigoCargoMod);

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult ProvMargenCrear(string glosaNewConcepto, string type)
        {
            string view = string.Empty;
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenCrear(Session["base64Username"].ToString(), glosaNewConcepto, type, objects[0].Token.ToString()));
            
            switch (type)
            {
                case "P":
                    ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaProvisionesGeneral";
                    break;
                case "G":
                    ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaGastosGeneral";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ProvMargenDesactivar(string codigoConcepto, string type)
        {
            string view = string.Empty;
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenDesactivar(Session["base64Username"].ToString(), codigoConcepto, objects[0].Token.ToString()));
            
            switch (type)
            {
                case "P":
                    ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaProvisionesGeneral";
                    break;
                case "G":
                    ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaGastosGeneral";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ProvMargenEliminar(string codigoConcepto, string type)
        {
            string view = string.Empty;
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenEliminar(Session["base64Username"].ToString(), codigoConcepto, objects[0].Token.ToString()));

            switch (type)
            {
                case "P":
                    ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaProvisionesGeneral";
                    break;
                case "G":
                    ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaGastosGeneral";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ProvMargenActivar(string codigoConcepto, string type)
        {
            string view = string.Empty;
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActivar(Session["base64Username"].ToString(), codigoConcepto, objects[0].Token.ToString()));
            
            switch (type)
            {
                case "P":
                    ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaProvisionesGeneral";
                    break;
                case "G":
                    ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaGastosGeneral";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ProvMargenActualizar(string codigoConcepto, string glosaNewConcepto, string type)
        {
            string view = string.Empty;
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActualizar(Session["base64Username"].ToString(), codigoConcepto, glosaNewConcepto, objects[0].Token.ToString()));

            switch (type)
            {
                case "P":
                    ViewBag.RenderizadoProvisionesGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaProvisionesGeneral";
                    break;
                case "G":
                    ViewBag.RenderizadoGastosGeneral = ModuleProvisionMargen("", "", "", type);
                    view = "Finanzas/CargoMod/_ListaGastosGeneral";
                    break;
            }

            return PartialView(view);
        }

        public ActionResult ProvMargenAsignaciones(string cliente, string empresa)
        {
            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenCrearAsig(string cliente, string monto, string codigoConcepto, string typeInput, string type, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenCrearAsig(Session["base64Username"].ToString(), codigoConcepto, cliente, type, monto, typeInput, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenEliminarAsig(string cliente, string codigoConcepto, string type, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenEliminarAsig(Session["base64Username"].ToString(), codigoConcepto, cliente, type, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenActualizarAsig(string cliente, string monto, string codigoConcepto, string typeInput, string type, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActualizarAsig(Session["base64Username"].ToString(), codigoConcepto, cliente, type, monto, typeInput, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenActualizarTypeCalculo(string cliente, string codigoConcepto, string typeInput, string type, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActualizarTypeCalculo(Session["base64Username"].ToString(), codigoConcepto, cliente, type, typeInput, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult RenderizadoClientesProvMargen(string empresa, string pagination = "MS01", string typeFilter = "", string dataFilter = "", string evento = "")
        {
            string view = string.Empty;
            ViewBag.RenderizadoClientesProvMargen = ModuleClientes(empresa, pagination, typeFilter, dataFilter, Session["base64Username"].ToString());
            ViewBag.RenderizadoPaginationProvMargen = ModulePaginationClientes(empresa, pagination, typeFilter, dataFilter);

            view = (evento == "") ? "Finanzas/CargoMod/_ListaClientesProvMargenItem" : "Finanzas/CargoMod/_ListaClientesProvMargen";

            return PartialView(view);
        }

        public ActionResult ProvMargenCrearAsignConstCalculo(string cliente, string codigoConcepto, string constante, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenCrearAsignConstCalculo(Session["base64Username"].ToString(), constante, codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenEliminarAsignConstCalculo(string cliente, string codigoConcepto, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenEliminarAsignConstCalculo(Session["base64Username"].ToString(), codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenActualizarAsignConstCalculo(string cliente, string codigoConcepto, string constante, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActualizarAsignConstCalculo(Session["base64Username"].ToString(), constante, codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }
        
        public ActionResult ProvMargenCrearAsignConstTope(string cliente, string codigoConcepto, string constante, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenCrearAsignConstTope(Session["base64Username"].ToString(), constante, codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenEliminarAsignConstTope(string cliente, string codigoConcepto, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenEliminarAsignConstTope(Session["base64Username"].ToString(), codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }

        public ActionResult ProvMargenActualizarAsignConstTope(string cliente, string codigoConcepto, string constante, string empresa)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenActualizarAsignConstTope(Session["base64Username"].ToString(), constante, codigoConcepto, cliente, empresa, objects[0].Token.ToString()));

            ViewBag.RenderizadoProvisionesAsignadas = ModuleProvMargenAsignadas(cliente, "P", empresa);
            ViewBag.RenderizadoMargenesAsignadas = ModuleProvMargenAsignadas(cliente, "G", empresa);
            ViewBag.RenderizadoMargenAsignadas = ModuleProvMargenAsignadas(cliente, "M", empresa);
            ViewBag.RenderizadoConstCalculoProvMargen = ModuleConstCalculoProvMargen();
            ViewBag.RenderizadoConstTopeProvMargen = ModuleConstTopeProvMargen();
            ViewBag.Cliente = cliente;
            ViewBag.Empresa = empresa;

            return PartialView("Finanzas/CargoMod/_ListaAsignacionesProvMargen");
        }
        
        public ActionResult CambiarProvMargGastoExcInc(string codigoVar, string excInc, string codigoCargoMod)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            
            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarProvMargGastoExcInc(Session["base64Username"].ToString(), codigoCargoMod, excInc, codigoVar, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            return PartialView("Finanzas/CargoMod/_Edit");
        }

        public ActionResult SolicitudesCargoMod(string pagination, string typeFilter, string dataFilter, string glosaOrigenDashboard, string tagsSelected = "CargoMod")
        {
            string controller = "";
            ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), pagination, typeFilter, dataFilter);

            switch(Session["Profile"].ToString())
            {
                case "KAM":
                    controller = "Operaciones";
                    break;
                case "ADMINISTRADOR":
                    controller = "Operaciones";
                    break;
                case "FINANZAS":
                    controller = "Finanzas";
                    break;
            }

            ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/RQ==/Create";
            ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/Uw==/Create";
            ViewBag.GlosaOrigenDashboard = glosaOrigenDashboard;
            ViewBag.EstadoSelectedDashboard = dataFilter;
            ViewBag.TagSelected = tagsSelected;

            return PartialView("Finanzas/CargoMod/_ItemSolicitudes");
        }

        public ActionResult SolicitudesCargoModFiltro(string pagination, string typeFilter, string dataFilter)
        {
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), pagination, typeFilter, dataFilter);
            
            return PartialView("Finanzas/CargoMod/_CargoModSolicitud");
        }

        public ActionResult TagsActionSolicitudes(string tagsSelected)
        {
            string controller = "";
            ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
            ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
            ViewBag.TagSelected = tagsSelected;

            switch (Session["Profile"].ToString())
            {
                case "KAM":
                    controller = "Operaciones";
                    break;
                case "ADMINISTRADOR":
                    controller = "Operaciones";
                    break;
                case "Finanzas":
                    controller = "Finanzas";
                    break;
            }

            ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/RQ==/Create";
            ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/Uw==/Create";

            return PartialView("Finanzas/CargoMod/_ItemSolicitudes");
        }

        #endregion

        #region "Metodos Http API Personas"

        public string PersonaConsultarCliente(string filter = "", string dataFilter = "")
        {
            return JsonConvert.SerializeObject(CollectionPersona.__PersonaConsultarCliente(filter, dataFilter), Formatting.Indented);
        }

        public string PersonaCrearCliente(string cliente)
        {
            return JsonConvert.SerializeObject(CollectionPersona.__PersonaCrearCliente(cliente), Formatting.Indented);
        }

        public string PersonaEliminarCliente(string cliente)
        {
            return JsonConvert.SerializeObject(CollectionPersona.__PersonaEliminarCliente(cliente), Formatting.Indented);
        }

        #endregion

        #region "Modularización de API"

        private List<ListObservaciones> ModuleListObservaciones(string codigoSolicitud)
        {
            List<ListObservaciones> listObservaciones = new List<ListObservaciones>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsLI = JsonConvert.DeserializeObject(Cargo.__ListObservaciones(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsLI.Count; i++)
            {
                ListObservaciones lista = new ListObservaciones();

                lista.CodigoItem = objectsLI[i].CodigoItem.ToString();
                lista.Descripcion = objectsLI[i].Descripcion.ToString();

                dynamic objectsValCadObs = JsonConvert.DeserializeObject(Cargo.__ValidaCadenaObservaciones(Session["base64Username"].ToString(), codigoSolicitud, objectsLI[i].Descripcion.ToString(), objects[0].Token.ToString()));

                for (var j = 0; j < objectsValCadObs.Count; j++)
                {
                    if (objectsValCadObs[j].Code.ToString() == "200")
                    {
                        lista.Checked = "checked";
                    }
                }

                listObservaciones.Add(lista);
            }

            return listObservaciones;
        }

        private List<ConstCalculoProvMargen> ModuleConstCalculoProvMargen()
        {
            List<ConstCalculoProvMargen> constantes = new List<ConstCalculoProvMargen>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenListaConstCalcProv(objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                constante.Codigo = objectsCM[i].Referencia.ToString();
                constante.Descripcion = objectsCM[i].Descripcion.ToString();
                
                constantes.Add(constante);
            }

            return constantes;
        }

        private List<ConstCalculoProvMargen> ModuleConstTopeProvMargen()
        {
            List<ConstCalculoProvMargen> constantes = new List<ConstCalculoProvMargen>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenListaConstTopeProv(objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                constante.Codigo = objectsCM[i].ReferenciaTope.ToString();
                constante.Descripcion = objectsCM[i].Descripcion.ToString();

                constantes.Add(constante);
            }

            return constantes;
        }

        private List<ProvisionMargen> ModuleProvMargenAsignadas(string cliente, string type, string empresa)
        {
            List<ProvisionMargen> provisiones = new List<ProvisionMargen>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenLista("", "", "", type, empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ProvisionMargen provision = new ProvisionMargen();

                provision.CodigoVariable = objectsCM[i].CodigoVariable.ToString();
                provision.Descripcion = objectsCM[i].Descripcion.ToString();
                
                dynamic objectsVal = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenValidateAsignacion(objectsCM[i].CodigoVariable.ToString(), cliente, type, empresa, objects[0].Token.ToString()));

                for (var j = 0; j < objectsVal.Count; j++)
                {
                    provision.Monto = objectsVal[j].Valor.ToString();
                    provision.Message = objectsVal[j].Message.ToString();
                    provision.TypeInput = objectsVal[j].TypeCalculo.ToString();
                }

                dynamic objectsOpt = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, type, empresa, objects[0].Token.ToString()));

                for (var h = 0; h < objectsOpt.Count; h++)
                {
                    provision.OptEditar = objectsOpt[h].OptActualizar.ToString();
                    provision.OptCrear = objectsOpt[h].OptCrear.ToString();
                    provision.OptEliminar = objectsOpt[h].OptEliminar.ToString();
                }

                dynamic objectsConst = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenValidateAsignacion(objectsCM[i].CodigoVariable.ToString(), cliente, "CONST", empresa, objects[0].Token.ToString()));

                for (var x = 0; x < objectsConst.Count; x++)
                {
                    ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                    constante.Codigo = objectsConst[x].Valor.ToString();
                    constante.Message = objectsConst[x].Message.ToString();

                    provision.Constante = constante;
                }

                dynamic objectsConstTope = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenValidateAsignacion(objectsCM[i].CodigoVariable.ToString(), cliente, "TOPE", empresa, objects[0].Token.ToString()));

                for (var x = 0; x < objectsConst.Count; x++)
                {
                    ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                    constante.Codigo = objectsConstTope[x].Valor.ToString();
                    constante.Message = objectsConstTope[x].Message.ToString();

                    provision.ConstanteTope = constante;
                }

                dynamic objectsOptConst = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, "CONST", empresa, objects[0].Token.ToString()));

                for (var z = 0; z < objectsOptConst.Count; z++)
                {
                    provision.OptActualizarAsignConstante = objectsOptConst[z].OptActualizar.ToString();
                    provision.OptCrearAsignConstante = objectsOptConst[z].OptCrear.ToString();
                    provision.OptEliminarAsignConstante = objectsOptConst[z].OptEliminar.ToString();
                }

                dynamic objectsOptConstTope = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, "TOPE", empresa, objects[0].Token.ToString()));

                for (var z = 0; z < objectsOptConst.Count; z++)
                {
                    provision.OptActualizarAsignConstanteTope = objectsOptConstTope[z].OptActualizar.ToString();
                    provision.OptCrearAsignConstanteTope = objectsOptConstTope[z].OptCrear.ToString();
                    provision.OptEliminarAsignConstanteTope = objectsOptConstTope[z].OptEliminar.ToString();
                }

                if (type != "M")
                {
                    if (objectsCM[i].OptActivar.ToString() == "N")
                    {
                        provisiones.Add(provision);
                    }
                }
                else
                {
                    provisiones.Add(provision);
                }
            }
            
            return provisiones;
        }

        private List<ProvisionMargen> ModuleProvisionMargen(string pagination = "", string typeFilter = "", string dataFiler = "", string type = "", string empresa = "")
        {
            List<ProvisionMargen> provisiones = new List<ProvisionMargen>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenLista(pagination, typeFilter, dataFiler, type, empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ProvisionMargen provision = new ProvisionMargen();

                provision.CodigoVariable = objectsCM[i].CodigoVariable.ToString();
                provision.Descripcion = objectsCM[i].Descripcion.ToString();
                provision.OptEliminar = objectsCM[i].OptEliminar.ToString();
                provision.OptDesactivar = objectsCM[i].OptDesactivar.ToString();
                provision.OptEditar = objectsCM[i].OptEditar.ToString();
                provision.OptActivar = objectsCM[i].OptActivar.ToString();


                provisiones.Add(provision);
            }

            return provisiones;
        }

        private List<AFP> ModuleAFP(string empresa)
        {
            List<AFP> afps = new List<AFP>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsAFP = JsonConvert.DeserializeObject(Cargo.__AFP(empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsAFP.Count; i++)
            {
                AFP afp = new AFP();

                afp.Nombre = objectsAFP[i].Nombre.ToString();

                afps.Add(afp);
            }

            return afps;
        }

        private List<ANI> ModuleANI(string usuarioCreador, string pagination, string typeFilter = "", string dataFilter = "")
        {
            List<ANI> anis = new List<ANI>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsANI = JsonConvert.DeserializeObject(ANIs.__ANIS(usuarioCreador, pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsANI.Count; i++)
            {
                ANI ani = new ANI();

                ani.Concepto = objectsANI[i].CodigoANI.ToString();
                ani.Valor = objectsANI[i].Nombre.ToString();

                anis.Add(ani);
            }

            return anis;
        }

        private List<Bono> ModuleBonos(string usuarioCreador, string pagination, string typeFilter = "", string dataFilter = "", string cliente = "")
        {
            List<Bono> bonos = new List<Bono>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsBonos = JsonConvert.DeserializeObject(Bonos.__Bonos(usuarioCreador, pagination, typeFilter, dataFilter, cliente, objects[0].Token.ToString()));

            for (var i = 0; i < objectsBonos.Count; i++)
            {
                Bono bono = new Bono();

                bono.Codigo = objectsBonos[i].CodigoBono.ToString();
                bono.Nombre = objectsBonos[i].Nombre.ToString();
                
                dynamic objectsAsign = JsonConvert.DeserializeObject(Bonos.__ValidateAsignBonos(objectsBonos[i].CodigoBono.ToString(), cliente, objects[0].Token.ToString()));

                for (var j = 0; j < objectsAsign.Count; j++)
                {
                    switch (objectsAsign[j].Code.ToString())
                    {
                        case "200":
                            bono.Asignado = "success color-fx3";
                            break;
                        default:
                            bono.Asignado = "";
                            break;
                    }
                    
                }

                bonos.Add(bono);
            }

            return bonos;
        }

        private List<ValoresDiarios> ModuleValoresDiarios(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<ValoresDiarios> valoresDiarios = new List<ValoresDiarios>();
                
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsValorDario = JsonConvert.DeserializeObject(ValorDiario.__ValorDiarioListar(pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsValorDario.Count; i++)
            {
                ValoresDiarios valor = new ValoresDiarios();

                valor.Cliente = objectsValorDario[i].Cliente.ToString();
                valor.Empresa = objectsValorDario[i].Empresa.ToString();
                valor.CargoMod = objectsValorDario[i].CargoMod.ToString();
                valor.ValorDiario = objectsValorDario[i].Valor.ToString();

                valoresDiarios.Add(valor);
            }

            return valoresDiarios;
        }

        private List<Provision> ModuleEstructuraMargenProvision(string codigoSolicitud = "")
        {
            List<Provision> estructuras = new List<Provision>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraMargenProvision(Session["base64Username"].ToString(), codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                Provision provision = new Provision();

                provision.Concepto = objectsEstructura[i].Concepto.ToString();
                provision.Percentage = objectsEstructura[i].Porcentaje.ToString();
                provision.MontoCLP = objectsEstructura[i].MontoCLP.ToString();
                provision.CodigoVariable = objectsEstructura[i].CodigoVariable.ToString();
                provision.OptWithExcluir = objectsEstructura[i].OptWithExcluir.ToString();
                provision.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructuras.Add(provision);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraHeader(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraHeader(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++) 
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.Cliente = objectsEstructura[i].Cliente.ToString();
                estructura.NombreCargo = objectsEstructura[i].NombreCargo.ToString();
                estructura.NombreCargoMod = objectsEstructura[i].NombreCargoMod.ToString();
                estructura.TípoContrato = objectsEstructura[i].TipoContrato.ToString();
                estructura.TopeUFImponible = objectsEstructura[i].UFTopeImponible.ToString();
                estructura.TopeCLPImponible = objectsEstructura[i].CLPTopeImponible.ToString();
                estructura.TopeUFImponibleAFC = objectsEstructura[i].UFTopeImponibleAFC.ToString();
                estructura.TopeCLPImponibleAFC = objectsEstructura[i].CLPTopeImponibleAFC.ToString();
                estructura.EnlaceEstructura = ModuleControlRetornoPath() + "/Finanzas/ViewPdf?pdf=EstructuraRentaTemp&data=" + objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.TypeSueldoInputActual = objectsEstructura[i].TypeSueldoInputActual.ToString();
                estructura.TypeSueldoInputChange = objectsEstructura[i].TypeSueldoInputChange.ToString();
                estructura.TypeCalculoSueldo = objectsEstructura[i].TypeCalculoSueldo.ToString();
                estructura.MessageCargoMod = objectsEstructura[i].MessageCargoMod.ToString();

                estructura.MessageTypeJornada = objectsEstructura[i].MessageTypeJornada.ToString();
                estructura.MessageTypeCalculo = objectsEstructura[i].MessageTypeCalculo.ToString();
                estructura.TypeJornada = objectsEstructura[i].TypeJornada.ToString();
                estructura.MaxLengthCargoMod = objectsEstructura[i].MaxLengthCargoMod.ToString();
                estructura.RestLengthCargoMod = objectsEstructura[i].RestLengthCargoMod.ToString();
                
                estructura.NumeroHorasSemanalesPT = objectsEstructura[i].NumeroHorasSemanalesPT.ToString();
                estructura.TypeCalculoPT = objectsEstructura[i].TypeCalculoPT.ToString();
                estructura.ValorSueldoPT = objectsEstructura[i].ValorSueldoPT.ToString();
                estructura.Horarios = objectsEstructura[i].Horarios.ToString();
                estructura.Observaciones = objectsEstructura[i].Observaciones.ToString();
                estructura.Observaciones2 = objectsEstructura[i].Observaciones2.ToString();
                estructura.Origen = objectsEstructura[i].Origen.ToString();
                estructura.NumeroDias = objectsEstructura[i].NumeroDias.ToString();

                estructura.RazonSocial = objectsEstructura[i].RazonSocial.ToString();

                ViewBag.TypeSueldoInputActual = objectsEstructura[i].TypeSueldoInputActual.ToString();
                ViewBag.TypeJornadaActual = objectsEstructura[i].TypeJornada.ToString();

                Session["ApplicationCliente"] = objectsEstructura[i].Cliente.ToString();
                Session["ApplicationEmpresa"] = objectsEstructura[i].Empresa.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraHaberes(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraHaberes(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.SueldoBase = objectsEstructura[i].SueldoBase.ToString();
                estructura.SueldoLiquido = objectsEstructura[i].SueldoLiquido.ToString();
                estructura.Gratificacion = objectsEstructura[i].Gratificacion.ToString();
                estructura.BaseImponible = objectsEstructura[i].BaseImponible.ToString();
                estructura.BaseImponibleAFC = objectsEstructura[i].BaseImponibleAFC.ToString();
                estructura.TotalImponible = objectsEstructura[i].TotalImponible.ToString();
                estructura.TotalTributable = objectsEstructura[i].TotalTributable.ToString();
                estructura.TotalHaberes = objectsEstructura[i].TotalHaberes.ToString();
                estructura.Bonos = ModuleBonosCargoMod(objectsEstructura[i].CodigoCargoMod.ToString());
                estructura.AsignacionesNOImponibles = ModuleANIsCargoMod(objectsEstructura[i].CodigoCargoMod.ToString());
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.GratificacionPactada = objectsEstructura[i].GratificacionPactada.ToString();
                estructura.GratifCC = objectsEstructura[i].GratifCC.ToString();
                estructura.MessageGratif = objectsEstructura[i].MessageGratif.ToString();
                estructura.MessageSueldoBase = objectsEstructura[i].MessageSueldoBase.ToString();
                estructura.MessageSugGratif = objectsEstructura[i].MessageSugGratif.ToString();
                estructura.GratifSugerida = objectsEstructura[i].GratifSugerida.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraDescuentos(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraDescuentos(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.PorcAFP = objectsEstructura[i].PorcAFP.ToString();
                estructura.AFP = objectsEstructura[i].AFP.ToString();
                estructura.CLPAFP = objectsEstructura[i].CLPAFP.ToString();
                estructura.PorcFondoPensiones = objectsEstructura[i].PorcFondoPensiones.ToString();
                estructura.PorcSegInvalidez = objectsEstructura[i].PorcSegInvalidez.ToString();
                estructura.PorcSalud = objectsEstructura[i].PorcSalud.ToString();
                estructura.CLPSalud = objectsEstructura[i].CLPSalud.ToString();
                estructura.CLPImpuestoUnico = objectsEstructura[i].CLPImpuestoUnico.ToString();
                estructura.PorcSeguroDesempleo = objectsEstructura[i].PorcSeguroDesempleo.ToString();
                estructura.CLPSeguroDesempleo = objectsEstructura[i].CLPSeguroDesempleo.ToString();
                estructura.TotalDescuentos = objectsEstructura[i].TotalDescuentos.ToString();
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<ANIsCargoMod> ModuleANIsCargoMod(string codigoCagoMod)
        {
            List<ANIsCargoMod> ANIList = new List<ANIsCargoMod>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsANI = JsonConvert.DeserializeObject(ANIs.__ANIsCargoMod(codigoCagoMod, objects[0].Token.ToString()));

            for (var i = 0; i < objectsANI.Count; i++)
            {
                ANIsCargoMod ANI = new ANIsCargoMod();

                ANI.Nombre = objectsANI[i].Nombre.ToString();
                ANI.Valor = objectsANI[i].Valor.ToString();
                ANI.Comentarios = objectsANI[i].Comentarios.ToString();
                ANI.CreadoPor = objectsANI[i].CreadoPor.ToString();
                ANI.Creado = objectsANI[i].Creado.ToString();
                ANI.UltimaActualizacion = objectsANI[i].UltimaActualizacion.ToString();
                ANI.Border = objectsANI[i].Border.ToString();
                ANI.GlyphiconColor = objectsANI[i].GlyphiconColor.ToString();
                ANI.Glyphicon = objectsANI[i].Glyphicon.ToString();
                ANI.CodigoCargoMod = objectsANI[i].CodigoCargoMod.ToString();
                ANI.CodigoANI = objectsANI[i].CodigoANI.ToString();
                ANI.MessageANI = objectsANI[i].MessageANI.ToString();
                ANI.Deft = objectsANI[i].Deft.ToString();
                ANI.Observaciones = objectsANI[i].Observaciones.ToString();

                ANIList.Add(ANI);
            }

            return ANIList;
        }

        private List<BonosCargoMod> ModuleBonosCargoMod(string codigoCagoMod)
        {
            List<BonosCargoMod> bonos = new List<BonosCargoMod>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsBonos = JsonConvert.DeserializeObject(Bonos.__BonosCargoMod(codigoCagoMod, objects[0].Token.ToString()));

            for (var i = 0; i < objectsBonos.Count; i++)
            {
                BonosCargoMod bono = new BonosCargoMod();

                bono.Nombre = objectsBonos[i].Nombre.ToString();
                bono.Valor = objectsBonos[i].Valor.ToString();
                bono.Comentarios = objectsBonos[i].Comentarios.ToString();
                bono.CreadoPor = objectsBonos[i].CreadoPor.ToString();
                bono.Creado = objectsBonos[i].Creado.ToString();
                bono.UltimaActualizacion = objectsBonos[i].UltimaActualizacion.ToString();
                bono.Border = objectsBonos[i].Border.ToString();
                bono.GlyphiconColor = objectsBonos[i].GlyphiconColor.ToString();
                bono.Glyphicon = objectsBonos[i].Glyphicon.ToString();
                bono.CodigoCargoMod = objectsBonos[i].CodigoCargoMod.ToString();
                bono.CodigoBono = objectsBonos[i].CodigoBono.ToString();
                bono.MessageAsignado = objectsBonos[i].MessageAsignado.ToString();
                bono.Condiciones = objectsBonos[i].Condiciones.ToString();

                bonos.Add(bono);
            }

            return bonos;
        }

        private List<Dashboard> ModuleDashboard(string usuarioCreador)
        {
            List<Dashboard> dashboards = new List<Dashboard>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsDashboard = JsonConvert.DeserializeObject(Cargo.__Dashboard(usuarioCreador, objects[0].Token.ToString()));

            for (var i = 0; i < objectsDashboard.Count; i++)
            {
                Dashboard dashboard = new Dashboard();

                dashboard.Simulaciones = objectsDashboard[i].Simulaciones.ToString();
                dashboard.ValidacionCotizaciones = objectsDashboard[i].ValidacionCotizaciones.ToString();
                dashboard.CotizacionesAprobadas = objectsDashboard[i].CotizacionesAprobadas.ToString();
                dashboard.CotizacionesRechazadas = objectsDashboard[i].CotizacionesRechazadas.ToString();

                dashboard.Creaciones = objectsDashboard[i].Creaciones.ToString();
                dashboard.RechazadosKam = objectsDashboard[i].RechazadosKam.ToString();
                dashboard.RechazadosRem = objectsDashboard[i].RechazadosRem.ToString();
                dashboard.PendienteFinanzas = objectsDashboard[i].PendientesFinanzas.ToString();
                dashboard.PendienteRemuneraciones = objectsDashboard[i].PendientesRemuneraciones.ToString();
                dashboard.PendientesFirmaDigital = objectsDashboard[i].PendientesFirmaDigital.ToString();
                dashboard.Terminados = objectsDashboard[i].Terminados.ToString();
                dashboard.Profile = objectsDashboard[i].Profile.ToString();

                dashboards.Add(dashboard);
            }

            return dashboards;
        }

        private List<Clientes> ModuleClientes(string empresa, string pagination, string typeFilter, string dataFilter, string usuarioCreador)
        {
            List<Clientes> clientes = new List<Clientes>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsClientes = JsonConvert.DeserializeObject(Cargo.__Clientes(empresa, pagination, typeFilter, dataFilter, usuarioCreador, objects[0].Token.ToString()));

            for (var i = 0; i < objectsClientes.Count; i++)
            {
                Clientes cliente = new Clientes();

                cliente.Codigo = objectsClientes[i].Codigo.ToString();
                cliente.Empresa = objectsClientes[i].Empresa.ToString();
                cliente.Value = objectsClientes[i].Value.ToString();

                clientes.Add(cliente);
            }

            return clientes;
        }

        private List<Models.Teamwork.Pagination> ModulePaginationClientes(string empresa, string pagination, string typeFilter, string dataFilter)
        {
            ViewBag.Empresa = empresa;

            List<Models.Teamwork.Pagination> paginations = new List<Models.Teamwork.Pagination>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsPgt = JsonConvert.DeserializeObject(Cargo.__PaginationClientes(empresa, pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            //for (var i = 0; i < objectsPgt.Count; i++)
            //{
            //    Models.Teamwork.Pagination paginationIndex = new Models.Teamwork.Pagination();

            //    paginationIndex.NumeroPagina = objectsPgt[i].NumeroPagina.ToString();
            //    paginationIndex.Rango = objectsPgt[i].Rango.ToString();
            //    paginationIndex.Class = objectsPgt[i].Class.ToString();
            //    paginationIndex.Properties = objectsPgt[i].Properties.ToString();
            //    paginationIndex.TypeFilter = objectsPgt[i].TypeFilter.ToString();
            //    paginationIndex.Filter = objectsPgt[i].Filter.ToString();

            //    if (objectsPgt[i].NumeroPagina.ToString() == "Siguiente &rsaquo;" && objectsPgt[i].Properties.ToString() == "")
            //    {
            //        paginations.Add(paginationIndex);
            //    } 
                
            //}

            return paginations;
        }

        private List<SolicitudCargo> ModuleSolicitudesCargoMod(string usuarioCreador, string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<SolicitudCargo> solicitudesCargos = new List<SolicitudCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__Solicitudes(Session["base64Username"].ToString(), pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                SolicitudCargo solicitudCargos = new SolicitudCargo();

                string controller = string.Empty;

                switch (objectsCM[i].Profile.ToString())
                {
                    case "FINANZAS":
                        controller = "Finanzas";
                        break;
                    case "KAM":
                        controller = "Operaciones";
                        break;
                    case "ANALISTA DE REMUNERACIONES":
                        controller = "Remuneraciones";
                        break;
                }

                solicitudCargos.CodigoCargoMod = objectsCM[i].CodigoCargoMod.ToString();
                solicitudCargos.Empresa = objectsCM[i].Empresa.ToString();
                solicitudCargos.NombreSolicitud = objectsCM[i].NombreSolicitud.ToString();
                solicitudCargos.Creado = objectsCM[i].Creado.ToString();
                solicitudCargos.CreadoPor = objectsCM[i].CreadoPor.ToString();
                solicitudCargos.Cliente = objectsCM[i].Cliente.ToString();
                solicitudCargos.Comentarios = objectsCM[i].Comentarios.ToString();
                solicitudCargos.Border = objectsCM[i].Border.ToString();
                solicitudCargos.Glyphicon = objectsCM[i].Glyphicon.ToString();
                solicitudCargos.GlyphiconColor = objectsCM[i].GlyphiconColor.ToString();
                solicitudCargos.Estado = objectsCM[i].Estado.ToString();
                solicitudCargos.UltimaActualizacion = objectsCM[i].UltimaActualizacion.ToString();
                solicitudCargos.NombreCargo = objectsCM[i].NombreCargo.ToString();
                solicitudCargos.CodigoSolicitud = objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.EnlaceDetalleSolicitud = ModuleControlRetornoPath() + "/Finanzas/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Detail";

                switch (objectsCM[i].Profile.ToString())
                {
                    case "FINANZAS":
                        solicitudCargos.EnlaceEditar = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Edit";
                        break;
                    case "KAM":
                        solicitudCargos.EnlaceEditar = ModuleControlRetornoPath() + "/" + controller + "/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Wizards/" + objectsCM[i].Wizards.ToString();
                        break;
                }
                solicitudCargos.EnlaceVolver = ModuleControlRetornoPath() + "/" + controller + "/CargoMod";
                solicitudCargos.EnlaceEstructura = ModuleControlRetornoPath() + "/Finanzas/Pdf?pdf=EstructuraRenta&data=" + objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.OptDesechar = objectsCM[i].OptDesechar.ToString();
                solicitudCargos.OptPdf = objectsCM[i].OptPdf.ToString();
                solicitudCargos.OptEditar = objectsCM[i].OptEditar.ToString();
                solicitudCargos.OptSolicitar = objectsCM[i].OptSolicitar.ToString();
                solicitudCargos.OptRechazar = objectsCM[i].OptRechazar.ToString();
                solicitudCargos.OptPublicar = objectsCM[i].OptPublicar.ToString();
                solicitudCargos.OptRechazarRem = objectsCM[i].OptRechazarRem.ToString();
                solicitudCargos.OptHistorial = objectsCM[i].OptHistorial.ToString();

                solicitudCargos.OptDescargarReporteFirmaDigital = objectsCM[i].OptDescargarReporteFirmaDigital.ToString();
                solicitudCargos.EnlaceDescargaReporteFirmaDigital = ModuleControlRetornoPath() + "/Remuneraciones/GenerateExcel?excel=ReporteFirmaDigital&namefile=Reporte__Firma__Digital&codigoCargoMod=" + objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.SectHaber = objectsCM[i].SectHaber.ToString();
                solicitudCargos.SectDesc = objectsCM[i].SectDesc.ToString();
                solicitudCargos.SectPorvMargen = objectsCM[i].SectPorvMargen.ToString();
                
                solicitudCargos.SectPorvMargen = objectsCM[i].SectPorvMargen.ToString();

                solicitudCargos.GlosaCodigo = objectsCM[i].GlosaCodigo.ToString();
                solicitudCargos.Profile = objectsCM[i].Profile.ToString();

                solicitudesCargos.Add(solicitudCargos);
            }

            return solicitudesCargos;
        }

        #endregion

        #region "Modulos"

        private List<Teamwork.Model.Finanzas.MontosTotales> ModuleTotalesFiniquitos(string codigoPago)
        {
            List<Teamwork.Model.Finanzas.MontosTotales> montosTotales = new List<Teamwork.Model.Finanzas.MontosTotales>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "DETAILMONTOSTOTALES";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Teamwork.Model.Finanzas.MontosTotales montoTotal = new Teamwork.Model.Finanzas.MontosTotales();

                montoTotal.FeriadoProporcional = rows["FeriadoProporcional"].ToString();
                montoTotal.RemuneracionPendiente = rows["RemuneracionPendiente"].ToString();
                montoTotal.Haberes = rows["Haberes"].ToString();
                montoTotal.Bono = rows["Bono"].ToString();
                montoTotal.IndemnizacionVoluntaria = rows["IndemnizacionVoluntaria"].ToString();
                montoTotal.MesAviso = rows["MesAviso"].ToString();
                montoTotal.IAS = rows["IAS"].ToString();
                montoTotal.SubTotal = rows["SubTotal"].ToString();
                montoTotal.Descuentos = rows["Descuentos"].ToString();
                montoTotal.TotalFiniquito = rows["TotalFiniquito"].ToString();
                montoTotal.GastoAdministrativo = rows["GastoAdministrativo"].ToString();
                montoTotal.TotalPago = rows["TotalPago"].ToString();
                montoTotal.Comentarios = rows["Comentarios"].ToString();

                montosTotales.Add(montoTotal);
            }

            return montosTotales;
        }

        private List<Teamwork.Model.Finanzas.ProcesosPago> ModuleProcesosPago(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<Teamwork.Model.Finanzas.ProcesosPago> procesosPagos = new List<Teamwork.Model.Finanzas.ProcesosPago>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "PROCESOSPAGOSFZ";
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
            valMantFin[18] = pagination;
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Teamwork.Model.Finanzas.ProcesosPago procesosPago = new Teamwork.Model.Finanzas.ProcesosPago();

                procesosPago.CodigoProceso = rows["CodigoProceso"].ToString();
                procesosPago.NombreProceso = rows["NombreProceso"].ToString();
                procesosPago.Creado = rows["Creado"].ToString();
                procesosPago.CreadoPor = rows["CreadoPor"].ToString();
                procesosPago.FechaCierreDate = rows["FechaCierreDate"].ToString();
                procesosPago.FechaAperturaDate = rows["FechaAperturaDate"].ToString();
                procesosPago.TotalProceso = rows["TotalProceso"].ToString();
                procesosPago.CantidadPagos = rows["CantidadPagos"].ToString();
                procesosPago.ViewDetalleProceso = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + rows["CodigoProceso"].ToString();
                procesosPago.DownloadReportePago = ModuleControlRetornoPath() + "/Finanzas/GenerateExcel?excel=UmVwb3J0ZVBhZ28=&proceso=" + rows["CodigoProceso"].ToString() + "&type=TEF";
                procesosPago.EnlaceContinuarProcesoMasivo = ModuleControlRetornoPath() + "/Finanzas/Procesos/" + rows["CodigoProceso"].ToString() + "/PagoMasivo";
                procesosPago.Border = rows["Border"].ToString();
                procesosPago.GlyphiconColor = rows["GlyphiconColor"].ToString();
                procesosPago.OptReportePagos = rows["OptReportePagos"].ToString();
                procesosPago.OptIniciarProcesoMasivoPago = rows["OptIniciarProcesoMasivoPago"].ToString();
                procesosPago.OptContinarProcesoMasivoPago = rows["OptContinarProcesoMasivoPago"].ToString();
                procesosPago.OptPagarMasivo = rows["OptPagarMasivo"].ToString();

                procesosPagos.Add(procesosPago);
            }

            return procesosPagos;
        }

        private List<Models.Finanzas.SolicTEF> ModuleSolicitudesTEF(string codigoProceso)
        {
            List<Models.Finanzas.SolicTEF> solicitudes = new List<Models.Finanzas.SolicTEF>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "SOLPAGOSTEF";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.SolicTEF solicitud = new Models.Finanzas.SolicTEF();

                solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                solicitud.Creado = rows["Creado"].ToString();
                solicitud.Rut = rows["Rut"].ToString();
                solicitud.Nombre = rows["Nombre"].ToString();
                solicitud.CuentaDestino = rows["CuentaDestino"].ToString();
                solicitud.CuentaOrigen = rows["CuentaOrigen"].ToString();
                solicitud.MontoTotal = rows["MontoTotal"].ToString();
                solicitud.Finiquito = rows["Finiquito"].ToString();
                solicitud.Color = rows["Color"].ToString();
                solicitud.CodigoTEF = rows["CodigoTEF"].ToString();
                solicitud.BorderColor = rows["BorderColor"].ToString();
                solicitud.OptPagar = rows["OptPagar"].ToString();
                solicitud.OptNotariar = rows["OptNotariar"].ToString();

                solicitudes.Add(solicitud);
            }

            return solicitudes;
        }

        private List<Models.Finanzas.SolicTEF> ModulePagosMasivoTEFInc(string codigoProceso)
        {
            List<Models.Finanzas.SolicTEF> solicitudes = new List<Models.Finanzas.SolicTEF>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "VIEWPAGOSMASIVOSINC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.SolicTEF solicitud = new Models.Finanzas.SolicTEF();

                solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                solicitud.Creado = rows["Creado"].ToString();
                solicitud.Rut = rows["Rut"].ToString();
                solicitud.Nombre = rows["Nombre"].ToString();
                solicitud.CuentaDestino = rows["CuentaDestino"].ToString();
                solicitud.CuentaOrigen = rows["CuentaOrigen"].ToString();
                solicitud.MontoTotal = rows["MontoTotal"].ToString();
                solicitud.Finiquito = rows["Finiquito"].ToString();
                solicitud.Color = rows["Color"].ToString();
                solicitud.CodigoTEF = rows["CodigoTEF"].ToString();
                solicitud.BorderColor = rows["BorderColor"].ToString();
                solicitud.OptExcluir = rows["OptExcluir"].ToString();

                solicitudes.Add(solicitud);
            }

            return solicitudes;
        }

        private List<Models.Finanzas.SolicTEF> ModulePagosMasivoTEFExc(string codigoProceso)
        {
            List<Models.Finanzas.SolicTEF> solicitudes = new List<Models.Finanzas.SolicTEF>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "VIEWPAGOSMASIVOSEXC";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.SolicTEF solicitud = new Models.Finanzas.SolicTEF();

                solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                solicitud.Creado = rows["Creado"].ToString();
                solicitud.Rut = rows["Rut"].ToString();
                solicitud.Nombre = rows["Nombre"].ToString();
                solicitud.CuentaDestino = rows["CuentaDestino"].ToString();
                solicitud.CuentaOrigen = rows["CuentaOrigen"].ToString();
                solicitud.MontoTotal = rows["MontoTotal"].ToString();
                solicitud.Finiquito = rows["Finiquito"].ToString();
                solicitud.Color = rows["Color"].ToString();
                solicitud.CodigoTEF = rows["CodigoTEF"].ToString();
                solicitud.BorderColor = rows["BorderColor"].ToString();
                solicitud.OptIncluir = rows["OptIncluir"].ToString();

                solicitudes.Add(solicitud);
            }

            return solicitudes;
        }

        private List<Models.Teamwork.Impresoras> ModuleImpresora()
        {
            List<Models.Teamwork.Impresoras> impresoras = new List<Models.Teamwork.Impresoras>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSIMP";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Teamwork.Impresoras impresora = new Models.Teamwork.Impresoras();

                impresora.IpAddress = rows["IpAddress"].ToString();
                impresora.Nombre = rows["Nombre"].ToString();

                impresoras.Add(impresora);
            }

            return impresoras;
        }

        private List<Models.Finanzas.Sucursal> ModuleSucursal()
        {
            List<Models.Finanzas.Sucursal> sucursales = new List<Models.Finanzas.Sucursal>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSSUCURSAL";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.Sucursal sucursal = new Models.Finanzas.Sucursal();

                sucursal.Nombre = rows["Sucursal"].ToString();
                sucursal.Direccion = rows["Direccion"].ToString();

                sucursales.Add(sucursal);
            }

            return sucursales;
        }

        private List<Models.Finanzas.DatosDespacho> ModuleDatosDespacho(string codigoProceso)
        {
            List<Models.Finanzas.DatosDespacho> despachos = new List<Models.Finanzas.DatosDespacho>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSBOXDESPACHO";
            valMantFin[1] = "";
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.DatosDespacho despacho = new Models.Finanzas.DatosDespacho();

                despacho.CodigoDespacho = rows["CodigoDespacho"].ToString();
                despacho.FechaApertura = rows["FechaApertura"].ToString();
                despacho.HoraApertura = rows["HoraApertura"].ToString();
                despacho.FechaCierre = rows["FechaDespacho"].ToString();
                despacho.HoraCierre = rows["HoraDespacho"].ToString();
                despacho.Cantidad = rows["Cantidad"].ToString();
                despacho.Sucursal = rows["SucursalDestino"].ToString();
                despacho.Direccion = rows["DireccionDestino"].ToString();

                despachos.Add(despacho);
            }

            return despachos;
        }

        private List<Models.Finanzas.Despacho> ModuleDespacho(string codigoProceso)
        {
            List<Models.Finanzas.Despacho> despachos = new List<Models.Finanzas.Despacho>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSDESPBOXDESPACHO";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.Despacho despacho = new Models.Finanzas.Despacho();

                despacho.CodigoDespacho = rows["CodigoDespacho"].ToString();
                despacho.CodigoDocumento = rows["CodigoDocumento"].ToString();
                despacho.TipoDocumento = rows["TipoDocumento"].ToString();
                despacho.Contenido = rows["Contenido"].ToString();
                
                despacho.Beneficiario = rows["Beneficiario"].ToString();
                despacho.Monto = rows["Monto"].ToString();
                despacho.Folio = rows["Folio"].ToString();


                despachos.Add(despacho);
            }

            return despachos;
        }

        private List<Models.Finanzas.Cheques> ModuleChequesEnProceso(string codigoProceso)
        {
            List<Models.Finanzas.Cheques> cheques = new List<Models.Finanzas.Cheques>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONFIRMACIONPROCESOCHQ";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.Cheques cheque = new Models.Finanzas.Cheques();

                cheque.Documento = rows["Documento"].ToString();
                cheque.Folio = rows["Folio"].ToString();
                cheque.Rut = rows["Rut"].ToString();
                cheque.Nombres = rows["Nombre"].ToString();
                cheque.Monto = rows["Monto"].ToString();
                cheque.CodigoPago = rows["CodigoCHQ"].ToString();
                cheque.Proceso = rows["Proceso"].ToString();
                cheque.Origen = rows["Origen"].ToString();

                cheques.Add(cheque);
            }

            return cheques;
        }

        private List<Models.Finanzas.Cheques> ModuleChequesConfirmados()
        {
            List<Models.Finanzas.Cheques> cheques = new List<Models.Finanzas.Cheques>();

            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CHQCONF";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.Cheques cheque = new Models.Finanzas.Cheques();

                cheque.Documento = rows["Documento"].ToString();
                cheque.Folio = rows["Folio"].ToString();
                cheque.Rut = rows["Rut"].ToString();
                cheque.Nombres = rows["Nombres"].ToString();
                cheque.Empresa = rows["Empresa"].ToString();
                cheque.Monto = rows["Monto"].ToString();
                cheque.CodigoPago = rows["CodigoPago"].ToString();
                cheque.Origen = rows["Origen"].ToString();

                cheques.Add(cheque);
            }

            return cheques;
        }

        private List<Models.Finanzas.DatosProceso> ModuleDatosProcesoTEF(string codigoProceso)
        {
            List<Models.Finanzas.DatosProceso> datosProcesos = new List<Models.Finanzas.DatosProceso>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSDATPROCESS";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.DatosProceso proceso = new Models.Finanzas.DatosProceso();

                proceso.Concepto = "TRANSFERENCIAS";
                proceso.CodigoProceso = rows["Proceso"].ToString();
                proceso.MontoTotal = rows["MontoTotal"].ToString();
                proceso.Cifra = rows["Cifra"].ToString();
                proceso.Cantidad = rows["Cantidad"].ToString();

                datosProcesos.Add(proceso);
            }

            return datosProcesos;
        }

        private List<Models.Finanzas.DatosProceso> ModuleDatosProcesoVV(string codigoProceso)
        {
            List<Models.Finanzas.DatosProceso> datosProcesos = new List<Models.Finanzas.DatosProceso>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSDATPROCESS";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.DatosProceso proceso = new Models.Finanzas.DatosProceso();

                proceso.Concepto = "VALEVISTA";
                proceso.CodigoProceso = rows["Proceso"].ToString();
                proceso.MontoTotal = rows["MontoTotal"].ToString();
                proceso.Cifra = rows["Cifra"].ToString();
                proceso.Cantidad = rows["Cantidad"].ToString();

                datosProcesos.Add(proceso);
            }

            return datosProcesos;
        }

        private List<Models.Finanzas.DatosProceso> ModuleDatosProcesoCHQ(string codigoProceso)
        {
            List<Models.Finanzas.DatosProceso> datosProcesos = new List<Models.Finanzas.DatosProceso>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSDATPROCESS";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.DatosProceso proceso = new Models.Finanzas.DatosProceso();

                proceso.Concepto = "CHEQUES";
                proceso.CodigoProceso = rows["Proceso"].ToString();
                proceso.MontoTotal = rows["MontoTotal"].ToString();
                proceso.Cifra = rows["Cifra"].ToString();
                proceso.Cantidad = rows["Cantidad"].ToString();

                datosProcesos.Add(proceso);
            }

            return datosProcesos;
        }

        private List<Models.Finanzas.Transferencia> ModuleTransferenciasEnProceso(string codigoProceso, string typeFilter = "", string dataFilter = "")
        {
            List<Models.Finanzas.Transferencia> transferencias = new List<Models.Finanzas.Transferencia>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONFIRMACIONPROCESOTEF";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.Transferencia transferencia = new Models.Finanzas.Transferencia();

                transferencia.Folio = rows["Folio"].ToString();
                transferencia.Rut = rows["Rut"].ToString();
                transferencia.Nombre = rows["Nombre"].ToString();
                transferencia.TotalFiniquito = rows["TotalFiniquito"].ToString();
                transferencia.GastoAdministrativo = rows["GastoAdministrativo"].ToString();
                transferencia.TotalPago = rows["TotalPago"].ToString();
                transferencia.CodigoTEF = rows["CodigoTEF"].ToString();
                transferencia.Proceso = rows["Proceso"].ToString();
                transferencia.Notariado = rows["Notariado"].ToString();
                transferencia.ColorNotariado = rows["ColorNotariado"].ToString();
                transferencia.OptRevertirIncorporacionTefPago = rows["OptRevertirIncorporacionTefPago"].ToString();
                transferencia.OptNotariado = rows["OptNotariado"].ToString();
                transferencia.OptNoNotariado = rows["OptNoNotariado"].ToString();
                transferencia.Origen = rows["Origen"].ToString();
                transferencia.Documento = rows["Documento"].ToString();
                transferencia.Cuenta = rows["Cuenta"].ToString();
                transferencia.Banco = rows["Banco"].ToString();

                transferencias.Add(transferencia);
            }

            return transferencias;
        }

        private List<Models.Finanzas.ValeVista> ModuleValeVistaEnProceso(string codigoProceso, string typeFilter = "", string dataFilter = "")
        {
            List<Models.Finanzas.ValeVista> vales = new List<Models.Finanzas.ValeVista>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONFIRMACIONPROCESOVV";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = codigoProceso;
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
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.ValeVista valeVista = new Models.Finanzas.ValeVista();

                valeVista.Folio = rows["Folio"].ToString();
                valeVista.Rut = rows["Rut"].ToString();
                valeVista.Nombre = rows["Nombre"].ToString();
                valeVista.TotalFiniquito = rows["TotalFiniquito"].ToString();
                valeVista.GastoAdministrativo = rows["GastoAdministrativo"].ToString();
                valeVista.TotalPago = rows["TotalPago"].ToString();
                valeVista.CodigoVV = rows["CodigoVV"].ToString();
                valeVista.Proceso = rows["Proceso"].ToString();
                valeVista.Notariado = rows["Notariado"].ToString();
                valeVista.ColorNotariado = rows["ColorNotariado"].ToString();
                valeVista.OptRevertirIncorporacionTefPago = rows["OptRevertirIncorporacionTefPago"].ToString();
                valeVista.OptNotariado = rows["OptNotariado"].ToString();
                valeVista.OptNoNotariado = rows["OptNoNotariado"].ToString();
                valeVista.Origen = rows["Origen"].ToString();
                valeVista.Documento = rows["Documento"].ToString();
                valeVista.Cuenta = rows["Cuenta"].ToString();
                valeVista.Banco = rows["Banco"].ToString();

                vales.Add(valeVista);
            }

            return vales;
        }

        public List<Models.Finiquitos.SolicitudTransferencia> ModuleTransferenciasConfirmadas(string typeFilter = "", string dataFilter = "", string orderBy = "", string typeTEF = "normal")
        {
            List<Models.Finiquitos.SolicitudTransferencia> transferencias = new List<Models.Finiquitos.SolicitudTransferencia>();

            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet data;

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
            paramMantFin[26] = "@ORIGEN";
            paramMantFin[27] = "@ORDERBY";

            valMantFin[0] = "TEFCONF";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = typeTEF;
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
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = "";
            valMantFin[24] = "";
            valMantFin[25] = "";
            valMantFin[26] = "";
            valMantFin[27] = orderBy; 

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finiquitos.SolicitudTransferencia transferencia = new Models.Finiquitos.SolicitudTransferencia();

                transferencia.Banco = rows["Banco"].ToString(); 
                transferencia.Nombres = rows["Nombres"].ToString();
                transferencia.Rut = rows["Rut"].ToString();
                transferencia.TotalFiniquito = rows["TotalFiniquito"].ToString();
                transferencia.GastoAdministrativo = rows["GastoAdministrativo"].ToString();
                transferencia.TotalPago = rows["TotalPago"].ToString();
                transferencia.Observacion = rows["Observacion"].ToString();
                transferencia.Cuenta = rows["Cuenta"].ToString();
                transferencia.CodigoPago = rows["CodigoPago"].ToString();
                transferencia.Documento = rows["Documento"].ToString();
                transferencia.Origen = rows["Origen"].ToString();

                transferencias.Add(transferencia);
            }

            return transferencias;
        }

        public List<Models.Finiquitos.ValeVista> ModuleValeVistaConfirmadas(string typeFilter = "", string dataFilter = "", string orderBy = "")
        {
            List<Models.Finiquitos.ValeVista> vales = new List<Models.Finiquitos.ValeVista>();

            string[] paramMantFin = new string[28];
            string[] valMantFin = new string[28];
            DataSet data;

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
            paramMantFin[26] = "@ORIGEN";
            paramMantFin[27] = "@ORDERBY";

            valMantFin[0] = "VVCONF";
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
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = "";
            valMantFin[24] = "";
            valMantFin[25] = "";
            valMantFin[26] = "";
            valMantFin[27] = orderBy;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finiquitos.ValeVista valeVista = new Models.Finiquitos.ValeVista();
                
                valeVista.Nombres = rows["Nombres"].ToString();
                valeVista.Rut = rows["Rut"].ToString();
                valeVista.TotalFiniquito = rows["TotalFiniquito"].ToString();
                valeVista.TotalPago = rows["TotalPago"].ToString();
                valeVista.Observacion = rows["Observacion"].ToString();
                valeVista.CodigoPago = rows["CodigoPago"].ToString();
                valeVista.Documento = rows["Documento"].ToString();
                valeVista.Origen = rows["Origen"].ToString();

                vales.Add(valeVista);
            }

            return vales;
        }

        private List<Models.Finanzas.IndiceCheque> ModuleIndices()
        {
            List<Models.Finanzas.IndiceCheque> indices = new List<Models.Finanzas.IndiceCheque>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSCHEQUE";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.IndiceCheque indice = new Models.Finanzas.IndiceCheque();

                indice.EmpresaOrigen = rows["EmpresaOrigen"].ToString();
                indice.Disponibles = rows["Disponibles"].ToString();
                indice.Emitidos = rows["Emitidos"].ToString();
                indice.Totales = rows["Totales"].ToString();

                indices.Add(indice);
            }

            return indices;
        }
        
        private List<Models.Finanzas.CuentasOrigen> ModuleCuentasOrigen(string paginationIndex, string typeFilter = "", string dataFilter = "")
        {
            List<Models.Finanzas.CuentasOrigen> cuentasOrigens = new List<Models.Finanzas.CuentasOrigen>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSCTAORIGEN";
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
            valMantFin[18] = paginationIndex;
            valMantFin[19] = typeFilter;
            valMantFin[20] = dataFilter;

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.CuentasOrigen cuenta = new Models.Finanzas.CuentasOrigen();

                cuenta.Banco = rows["Banco"].ToString();
                cuenta.NumeroCuenta = rows["NumeroCuenta"].ToString();
                cuenta.GlosaCuenta = rows["GlosaCuenta"].ToString();
                cuenta.RutCuenta = rows["RutCuenta"].ToString();
                cuenta.FechaCreacion = rows["FechaCreacion"].ToString();
                cuenta.Vigente = rows["Vigente"].ToString();

                cuentasOrigens.Add(cuenta);
            }

            return cuentasOrigens;
        }

        private List<Models.Finanzas.CuentasOrigen> ModuleCuentasOrigenDDL()
        {
            List<Models.Finanzas.CuentasOrigen> cuentasOrigens = new List<Models.Finanzas.CuentasOrigen>();
            string[] paramMantFin = new string[21];
            string[] valMantFin = new string[21];
            DataSet data;

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

            valMantFin[0] = "CONSCTAORIGENDDL";
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

            data = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in data.Tables[0].Rows)
            {
                Models.Finanzas.CuentasOrigen cuenta = new Models.Finanzas.CuentasOrigen();

                cuenta.Banco = rows["Banco"].ToString();
                cuenta.NumeroCuenta = rows["NumeroCuenta"].ToString();
                cuenta.GlosaCuenta = rows["GlosaCuenta"].ToString();
                cuenta.RutCuenta = rows["RutCuenta"].ToString();

                cuentasOrigens.Add(cuenta);
            }

            return cuentasOrigens;
        }

        private List<Models.Teamwork.Pagination> ModulePagination(string typePagination, string paginationIndex, string typeFilter = "", string dataFilter = "")
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
            valPaginator[5] = "";
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
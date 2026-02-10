using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamwork.Model.Operaciones;
using Teamwork.Model.Teamwork;
using Teamwork.Extensions.Excel;
using Teamwork.WebApi.Auth;
using Teamwork.WebApi.Operaciones;
using Newtonsoft.Json;
using Teamwork.Model.Finanzas;
using Teamwork.WebApi;
using AplicacionOperaciones.Collections;

namespace AplicacionOperaciones.Controllers
{
    public class OperacionesController : Controller
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

                    switch (Session["Profile"].ToString())
                    {
                        case "Coordinador Procesos":
                            ViewBag.ProfileActivo = Session["Profile"].ToString();
                            ViewBag.ReferenciaReporteExcel = ModuleControlRetornoPath() + "/Bajas/GenerateExcel?report=ReporteFiniquitos&filter=*_*";
                            Session["ApplicationReporteExcelFiniquitos"] = false;
                            Session["ApplicationReporteExcelFiniquitosSuccess"] = false;
                            ViewBag.RenderizadoDashboardFiniquitos = ModuleDashbordFiniquitos();
                            ViewBag.CreadorReportes = ModuleControlRetornoPath() + "/Bajas/Reporte";
                            break;
                        default:
                            ViewBag.ProfileActivo = Session["Profile"].ToString();
                            ViewBag.CreadorReportes = ModuleControlRetornoPath() + "/Bajas/Reporte";
                            Session["ApplicationReporteExcelFiniquitos"] = false;
                            Session["ApplicationReporteExcelFiniquitosSuccess"] = false;
                            //List<Models.Proceso> procesosC = new List<Models.Proceso>();
                            //List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
                            //List<Models.Proceso> procesosR = new List<Models.Proceso>();
                            //List<Models.Pagination> paginatorsR = new List<Models.Pagination>();
                            //List<Models.Proceso> procesosA = new List<Models.Proceso>();
                            //List<Models.Pagination> paginatorsA = new List<Models.Pagination>();

                            //List<Models.HeaderProcesos> headersProcesosC = new List<Models.HeaderProcesos>();
                            //List<Models.HeaderProcesos> headersProcesosR = new List<Models.HeaderProcesos>();
                            //List<Models.HeaderProcesos> headersProcesosA = new List<Models.HeaderProcesos>();

                            #region "BLOQUE DE CODIGO COMENTADO"
                            //    #region "PROCESOS CONTRATOS"

                            ///** HEADER DE PROCESOS */

                            //string[] headerParamHeaderProcesosC =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER"
                            //    };

                            //    string[] headerValoresHeaderProcesosC =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkQ29udHJhdG8=",
                            //        "MS01",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataHeaderProcesosC = servicioOperaciones.GetObtenerHeaderProcesos(headerParamHeaderProcesosC, headerValoresHeaderProcesosC).Table;

                            //    foreach (DataRow item in dataHeaderProcesosC.Tables[0].Rows)
                            //    {
                            //        Models.HeaderProcesos header = new Models.HeaderProcesos();

                            //        header.PostTitulo = item["PostTitulo"].ToString();
                            //        header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                            //        header.HtmlPagination = item["HtmlPagination"].ToString();
                            //        header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                            //        header.TipoSolicitud = item["TipoSolicitud"].ToString();
                            //        header.ResultSearch = item["ResultSearch"].ToString();

                            //        headersProcesosC.Add(header);
                            //    }

                            //    ViewBag.RenderizadoHeaderProcesosContratos = headersProcesosC;

                            //    /** PROCESOS SOLICITUD CONTRATO */

                            //    string[] headerParamProcesosC =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] headerValoresProcesosC =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkQ29udHJhdG8=",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataProcesosC = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesosC, headerValoresProcesosC).Table;

                            //    foreach (DataRow rows in dataProcesosC.Tables[0].Rows)
                            //    {
                            //        if (rows["Code"].ToString() == "200")
                            //        {
                            //            Models.Proceso proceso = new Models.Proceso();

                            //            proceso.Code = rows["Code"].ToString();
                            //            proceso.Message = rows["Message"].ToString();

                            //            proceso.TipoEvento = rows["TipoEvento"].ToString();

                            //            proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                            //            proceso.NombreProceso = rows["NombreProceso"].ToString();
                            //            proceso.Creado = rows["Creado"].ToString();
                            //            proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                            //            proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                            //            proceso.Comentarios = rows["Comentarios"].ToString();
                            //            proceso.CodificarCod = rows["CodificarCod"].ToString();

                            //            proceso.Prioridad = rows["Prioridad"].ToString();
                            //            proceso.Glyphicon = rows["Glyphicon"].ToString();
                            //            proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            //            proceso.BorderColor = rows["BorderColor"].ToString();

                            //            proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            //            proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                            //            proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                            //            proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                            //            proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            //            proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            //            proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();

                            //            proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                            //            procesosC.Add(proceso);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoProcesosContratos = procesosC;

                            //    /** PAGINATOR */

                            //    string[] paramPaginatorProcesosC =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] valPaginatorProcesosC =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkQ29udHJhdG8=",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataPaginadoC = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosC, valPaginatorProcesosC).Table;

                            //    foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
                            //    {
                            //        if (dataRowPaginadoC["Code"].ToString() == "200")
                            //        {
                            //            Models.Pagination paginado = new Models.Pagination();

                            //            paginado.Code = dataRowPaginadoC["Code"].ToString();
                            //            paginado.Message = dataRowPaginadoC["Message"].ToString();
                            //            paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
                            //            paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
                            //            paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
                            //            paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
                            //            paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
                            //            paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
                            //            paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
                            //            paginado.Active = dataRowPaginadoC["Activo"].ToString();

                            //            paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
                            //            paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
                            //            paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

                            //            paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();

                            //            paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

                            //            paginatorsC.Add(paginado);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoPagProcesosContratos = paginatorsC;

                            //    /** END PROCESO SOLICITUD CONTRATO */

                            //    #endregion

                            //    #region "PROCESOS RENOVACIONES"

                            //    /** HEADER DE PROCESOS */

                            //    string[] headerParamHeaderProcesosR =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER"
                            //    };

                            //    string[] headerValoresHeaderProcesosR =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                            //        "MS01",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataHeaderProcesosR = servicioOperaciones.GetObtenerHeaderProcesos(headerParamHeaderProcesosR, headerValoresHeaderProcesosR).Table;

                            //    foreach (DataRow item in dataHeaderProcesosR.Tables[0].Rows)
                            //    {
                            //        Models.HeaderProcesos header = new Models.HeaderProcesos();

                            //        header.PostTitulo = item["PostTitulo"].ToString();
                            //        header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                            //        header.HtmlPagination = item["HtmlPagination"].ToString();
                            //        header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                            //        header.TipoSolicitud = item["TipoSolicitud"].ToString();
                            //        header.ResultSearch = item["ResultSearch"].ToString();

                            //        headersProcesosR.Add(header);
                            //    }

                            //    ViewBag.RenderizadoHeaderProcesosRenovaciones = headersProcesosR;


                            //    /** PROCESOS SOLICITUD RENOVACION */

                            //    string[] headerParamProcesos =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] headerValoresProcesos =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataProcesosR = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesos, headerValoresProcesos).Table;

                            //    foreach (DataRow rows in dataProcesosR.Tables[0].Rows)
                            //    {
                            //        if (rows["Code"].ToString() == "200")
                            //        {
                            //            Models.Proceso proceso = new Models.Proceso();

                            //            proceso.Code = rows["Code"].ToString();
                            //            proceso.Message = rows["Message"].ToString();

                            //            proceso.TipoEvento = rows["TipoEvento"].ToString();

                            //            proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                            //            proceso.NombreProceso = rows["NombreProceso"].ToString();
                            //            proceso.Creado = rows["Creado"].ToString();
                            //            proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                            //            proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                            //            proceso.Comentarios = rows["Comentarios"].ToString();
                            //            proceso.CodificarCod = rows["CodificarCod"].ToString();

                            //            proceso.Prioridad = rows["Prioridad"].ToString();
                            //            proceso.Glyphicon = rows["Glyphicon"].ToString();
                            //            proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            //            proceso.BorderColor = rows["BorderColor"].ToString();

                            //            proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            //            proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                            //            proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                            //            proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                            //            proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            //            proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            //            proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();

                            //            proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                            //            procesosR.Add(proceso);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoProcesosRenovaciones = procesosR;

                            //    /** PAGINATOR */

                            //    string[] paramPaginatorProcesosR =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] valPaginatorProcesosR =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataPaginadoR = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosR, valPaginatorProcesosR).Table;

                            //    foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
                            //    {
                            //        if (dataRowPaginadoR["Code"].ToString() == "200")
                            //        {
                            //            Models.Pagination paginado = new Models.Pagination();

                            //            paginado.Code = dataRowPaginadoR["Code"].ToString();
                            //            paginado.Message = dataRowPaginadoR["Message"].ToString();
                            //            paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
                            //            paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
                            //            paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
                            //            paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
                            //            paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
                            //            paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
                            //            paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
                            //            paginado.Active = dataRowPaginadoR["Activo"].ToString();

                            //            paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
                            //            paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
                            //            paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

                            //            paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();

                            //            paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

                            //            paginatorsR.Add(paginado);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoPagProcesosRenovaciones = paginatorsR;

                            //    /** END PROCESO SOLICITUD RENOVACION */

                            //    #endregion

                            //    #region "PROCESOS ANEXOS"

                            //    /** PROCESOS SOLICITUD ANEXOS */

                            //    string[] headerParamProcesosAnexos =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] headerValoresProcesosAnexos =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkQW5leG9z",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataProcesosA = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesosAnexos, headerValoresProcesosAnexos).Table;

                            //    foreach (DataRow rows in dataProcesosA.Tables[0].Rows)
                            //    {
                            //        if (rows["Code"].ToString() == "200")
                            //        {
                            //            Models.Proceso proceso = new Models.Proceso();

                            //            proceso.Code = rows["Code"].ToString();
                            //            proceso.Message = rows["Message"].ToString();

                            //            proceso.TipoEvento = rows["TipoEvento"].ToString();

                            //            proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                            //            proceso.NombreProceso = rows["NombreProceso"].ToString();
                            //            proceso.Creado = rows["Creado"].ToString();
                            //            proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                            //            proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                            //            proceso.Comentarios = rows["Comentarios"].ToString();
                            //            proceso.CodificarCod = rows["CodificarCod"].ToString();

                            //            proceso.Prioridad = rows["Prioridad"].ToString();
                            //            proceso.Glyphicon = rows["Glyphicon"].ToString();
                            //            proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            //            proceso.BorderColor = rows["BorderColor"].ToString();

                            //            proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            //            proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                            //            proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                            //            proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                            //            proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            //            proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            //            proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();

                            //            proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                            //            procesosA.Add(proceso);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoProcesosAnexos = procesosA;

                            //    /** PAGINATOR */

                            //    string[] paramPaginatorProcesosA =
                            //    {
                            //        "@AUTHENTICATE",
                            //        "@AGENTAPP",
                            //        "@TRABAJADOR",
                            //        "@TIPOSOLICITUD",
                            //        "@PAGINATION",
                            //        "@TYPEFILTER",
                            //        "@DATAFILTER",
                            //        "@TYPEDASHBOARD",
                            //        "@DAYDASHBOARD",
                            //        "@MONTHDASHBOARD",
                            //        "@YEARDASHBOARD"
                            //    };

                            //    string[] valPaginatorProcesosA =
                            //    {
                            //        tokenAuth,
                            //        agenteAplication,
                            //        Session["NombreUsuario"].ToString(),
                            //        "U29saWNpdHVkQW5leG9z",
                            //        "MS01",
                            //        "",
                            //        "",
                            //        "Completo",
                            //        "",
                            //        "",
                            //        ""
                            //    };

                            //    DataSet dataPaginadoA = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosA, valPaginatorProcesosA).Table;

                            //    foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
                            //    {
                            //        if (dataRowPaginadoR["Code"].ToString() == "200")
                            //        {
                            //            Models.Pagination paginado = new Models.Pagination();

                            //            paginado.Code = dataRowPaginadoR["Code"].ToString();
                            //            paginado.Message = dataRowPaginadoR["Message"].ToString();
                            //            paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
                            //            paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
                            //            paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
                            //            paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
                            //            paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
                            //            paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
                            //            paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
                            //            paginado.Active = dataRowPaginadoR["Activo"].ToString();

                            //            paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
                            //            paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
                            //            paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

                            //            paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();

                            //            paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

                            //            paginatorsA.Add(paginado);
                            //        }
                            //    }

                            //    ViewBag.RenderizadoPagProcesosAnexos = paginatorsA;

                            ///** END PROCESO SOLICITUD RENOVACION */

                            //#endregion
                            #endregion

                            #region "MOTIVOS DE ANULACION RENDERIZADO"

                            List<Models.MotivosAnulacion> motivos = new List<Models.MotivosAnulacion>();

                            string[] paramMotivosAnulacion =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP"
                            };

                            string[] valMotivosAnulacion =
                            {
                                tokenAuth,
                                agenteAplication
                            };

                            DataSet dataMotivosAnulacion = servicioOperaciones.GetObtenerMotivosAnulacion(paramMotivosAnulacion, valMotivosAnulacion).Table;

                            foreach (DataRow rows in dataMotivosAnulacion.Tables[0].Rows)
                            {
                                if (rows["Code"].ToString() == "200")
                                {
                                    Models.MotivosAnulacion motivo = new Models.MotivosAnulacion();

                                    motivo.Code = rows["Code"].ToString();
                                    motivo.Message = rows["Message"].ToString();
                                    motivo.Descripcion = rows["Descripcion"].ToString();

                                    motivos.Add(motivo);
                                }
                            }

                            ViewBag.RenderizadoMotivosAnulacion = motivos;

                            #endregion

                            #region "INDICE ESTADISTICO"

                            string dashboard = string.Empty;
                            string dashboardTipo = string.Empty;

                            List<Models.EstadisticaTotal> estadisticas = new List<Models.EstadisticaTotal>();
                            List<Models.EstadisticaXContratos> estadisticasXContrato = new List<Models.EstadisticaXContratos>();
                            List<Models.EstadisticaKam> estadisticasKam = new List<Models.EstadisticaKam>();
                            List<Models.EstadisticaKamEmpresa> estadisticasKamEmpresa = new List<Models.EstadisticaKamEmpresa>();

                            string[] paramIndiceEstadistico =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@TRABAJADOR",
                                "@TIPOINDICE",
                                "@TIPODASHBOARD",
                                "@DAYDASHBOARD",
                                "@MONTHDASHBOARD",
                                "@YEARDASHBOARD"
                            };

                            string[] valIndiceEstadistico =
                            {
                                tokenAuth,
                                agenteAplication,
                                Session["NombreUsuario"].ToString(),
                                "VG90YWxlcw==",
                                "Pendiente",
                                "",
                                "",
                                ""
                            };

                            DataSet dataIndice = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadistico, valIndiceEstadistico).Table;

                            foreach (DataRow rows in dataIndice.Tables[0].Rows)
                            {
                                if (rows["Code"].ToString() == "200")
                                {
                                    switch (rows["Dashboard"].ToString())
                                    {
                                        case "Kam":
                                            switch (rows["TipoDashboard"].ToString())
                                            {
                                                case "Total":
                                                    Models.EstadisticaKam estadisticaKam = new Models.EstadisticaKam();

                                                    dashboard = rows["Dashboard"].ToString();

                                                    estadisticaKam.Code = rows["Code"].ToString();
                                                    estadisticaKam.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                                    estadisticaKam.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                                    estadisticaKam.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                                    estadisticaKam.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                                    estadisticaKam.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                                    estadisticaKam.TotalProcesos = rows["TotalProcesos"].ToString();
                                                    estadisticaKam.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                                    estadisticaKam.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                                    estadisticaKam.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                                    estadisticaKam.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                                    estadisticasKam.Add(estadisticaKam);
                                                    break;
                                                case "Empresa":
                                                    Models.EstadisticaKamEmpresa estadisticaKamEmpresa = new Models.EstadisticaKamEmpresa();

                                                    dashboard = rows["Dashboard"].ToString();
                                                    estadisticaKamEmpresa.Code = rows["Code"].ToString();
                                                    estadisticaKamEmpresa.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                                    estadisticaKamEmpresa.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                                    estadisticaKamEmpresa.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                                    estadisticaKamEmpresa.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.TotalProcesos = rows["TotalProcesos"].ToString();
                                                    estadisticaKamEmpresa.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                                    estadisticaKamEmpresa.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                                    estadisticaKamEmpresa.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.Concepto = rows["Concepto"].ToString();
                                                    estadisticaKamEmpresa.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                                    /** TERMINADAS */
                                                    estadisticaKamEmpresa.EmpresaPCSSolContratos = rows["EmpresaPCSSolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolRenovaciones = rows["EmpresaPCSSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolContratosPercentage = rows["EmpresaPCSSolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolRenovacionesPercentage = rows["EmpresaPCSSolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphiconColor = rows["EmpresaPCSSolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphicon = rows["EmpresaPCSSolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolBarColor = rows["EmpresaPCSSolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaPCSSolConcepto = rows["EmpresaPCSSolConcepto"].ToString();
                                                    /** CRITICAS */
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratos = rows["EmpresaCRITICOSolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovaciones = rows["EmpresaCRITICOSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratosPercentage = rows["EmpresaCRITICOSolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovacionesPercentage = rows["EmpresaCRITICOSolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphiconColor = rows["EmpresaCRITICOSolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphicon = rows["EmpresaCRITICOSolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolBarColor = rows["EmpresaCRITICOSolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaCRITICOSolConcepto = rows["EmpresaCRITICOSolConcepto"].ToString();
                                                    /** URGENTES */
                                                    estadisticaKamEmpresa.EmpresaURGENTESolContratos = rows["EmpresaURGENTESolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovaciones = rows["EmpresaURGENTESolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolContratosPercentage = rows["EmpresaURGENTESolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovacionesPercentage = rows["EmpresaURGENTESolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphiconColor = rows["EmpresaURGENTESolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphicon = rows["EmpresaURGENTESolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolBarColor = rows["EmpresaURGENTESolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaURGENTESolConcepto = rows["EmpresaURGENTESolConcepto"].ToString();
                                                    /** NORMALES */
                                                    estadisticaKamEmpresa.EmpresaNORMALSolContratos = rows["EmpresaNORMALSolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovaciones = rows["EmpresaNORMALSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolContratosPercentage = rows["EmpresaNORMALSolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovacionesPercentage = rows["EmpresaNORMALSolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphiconColor = rows["EmpresaNORMALSolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphicon = rows["EmpresaNORMALSolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolBarColor = rows["EmpresaNORMALSolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaNORMALSolConcepto = rows["EmpresaNORMALSolConcepto"].ToString();
                                                    /** SIN PRIORIDAD */
                                                    estadisticaKamEmpresa.EmpresaSPSolContratos = rows["EmpresaSPSolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolRenovaciones = rows["EmpresaSPSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolContratosPercentage = rows["EmpresaSPSolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolRenovacionesPercentage = rows["EmpresaSPSolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolGlyphiconColor = rows["EmpresaSPSolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolGlyphicon = rows["EmpresaSPSolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolBarColor = rows["EmpresaSPSolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaSPSolConcepto = rows["EmpresaSPSolConcepto"].ToString();
                                                    /** ANULADAS */
                                                    estadisticaKamEmpresa.EmpresaANULADASolContratos = rows["EmpresaANULADASolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolRenovaciones = rows["EmpresaANULADASolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolContratosPercentage = rows["EmpresaANULADASolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolRenovacionesPercentage = rows["EmpresaANULADASolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphiconColor = rows["EmpresaANULADASolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphicon = rows["EmpresaANULADASolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolBarColor = rows["EmpresaANULADASolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaANULADASolConcepto = rows["EmpresaANULADASolConcepto"].ToString();
                                                    /** ERROR */
                                                    estadisticaKamEmpresa.EmpresaERRORSolContratos = rows["EmpresaERRORSolContratos"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolRenovaciones = rows["EmpresaERRORSolRenovaciones"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolContratosPercentage = rows["EmpresaERRORSolContratosPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolRenovacionesPercentage = rows["EmpresaERRORSolRenovacionesPercentage"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphiconColor = rows["EmpresaERRORSolGlyphiconColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphicon = rows["EmpresaERRORSolGlyphicon"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolBarColor = rows["EmpresaERRORSolBarColor"].ToString();
                                                    estadisticaKamEmpresa.EmpresaERRORSolConcepto = rows["EmpresaERRORSolConcepto"].ToString();

                                                    estadisticasKamEmpresa.Add(estadisticaKamEmpresa);
                                                    break;
                                            }
                                            break;
                                        case "Contratos":
                                            Models.EstadisticaTotal estadistica = new Models.EstadisticaTotal();

                                            dashboard = rows["Dashboard"].ToString();

                                            estadistica.Code = rows["Code"].ToString();
                                            estadistica.Message = rows["Message"].ToString();
                                            estadistica.Concepto = rows["Concepto"].ToString();
                                            estadistica.TipoProceso = rows["TipoProceso"].ToString();
                                            estadistica.Total = rows["Total"].ToString();
                                            estadistica.Twest = rows["Twest"].ToString();
                                            estadistica.Twrrhh = rows["Twrrhh"].ToString();
                                            estadistica.Twc = rows["Twc"].ToString();
                                            estadistica.Analista = rows["Analista"].ToString();

                                            estadisticas.Add(estadistica);
                                            break;
                                    }
                                }
                            }

                            switch (dashboard)
                            {
                                case "Kam":
                                    ViewBag.RenderizadoIndiceEstadisticoTotal = estadisticasKam;
                                    ViewBag.RenderizadoIndiceEstadisticoTotalEmpresa = estadisticasKamEmpresa;
                                    ViewBag.RenderizaTipoDashboard = dashboard;
                                    ViewBag.RenderizaOptionFiltrosDashboard = "403";
                                    break;
                                case "Contratos":
                                    ViewBag.RenderizadoIndiceEstadisticoTotal = estadisticas;
                                    ViewBag.RenderizaTipoDashboard = dashboard;
                                    ViewBag.RenderizaOptionFiltrosDashboard = "200";
                                    break;
                            }

                            string[] paramIndiceEstadisticoXContrato =
                            {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@TRABAJADOR",
                                "@TIPOINDICE",
                                "@TIPODASHBOARD",
                                "@DAYDASHBOARD",
                                "@MONTHDASHBOARD",
                                "@YEARDASHBOARD"
                            };

                            string[] valIndiceEstadisticoXContrato =
                            {
                                tokenAuth,
                                agenteAplication,
                                Session["NombreUsuario"].ToString(),
                                "WEFuYWxpc3RhQ29udHJhdG9z",
                                "Total",
                                "",
                                "",
                                ""
                            };

                            DataSet dataIndiceXContrato = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadisticoXContrato, valIndiceEstadisticoXContrato).Table;

                            foreach (DataRow rows in dataIndiceXContrato.Tables[0].Rows)
                            {
                                if (rows["Code"].ToString() == "200")
                                {
                                    ViewBag.RenderizaOptionEstadisticaXContrato = rows["Code"].ToString();

                                    Models.EstadisticaXContratos estadistica = new Models.EstadisticaXContratos();

                                    estadistica.Code = rows["Code"].ToString();
                                    estadistica.Message = rows["Message"].ToString();
                                    estadistica.Analista = rows["Analista"].ToString();
                                    estadistica.ConceptoC = rows["ConceptoC"].ToString();
                                    estadistica.CSolicitado = rows["CSolicitado"].ToString();
                                    estadistica.CTerminado = rows["CTerminado"].ToString();
                                    estadistica.CTotal = rows["CTotal"].ToString();
                                    estadistica.CTwest = rows["CTwest"].ToString();
                                    estadistica.CTwrrhh = rows["CTwrrhh"].ToString();
                                    estadistica.CTwc = rows["CTwc"].ToString();
                                    estadistica.ConceptoR = rows["ConceptoR"].ToString();
                                    estadistica.RSolicitado = rows["RSolicitado"].ToString();
                                    estadistica.RTerminado = rows["RTerminado"].ToString();
                                    estadistica.RTotal = rows["RTotal"].ToString();
                                    estadistica.RTwest = rows["RTwest"].ToString();
                                    estadistica.RTwrrhh = rows["RTwrrhh"].ToString();
                                    estadistica.RTwc = rows["RTwc"].ToString();

                                    estadisticasXContrato.Add(estadistica);
                                }
                                else
                                {
                                    ViewBag.RenderizaOptionEstadisticaXContrato = rows["Code"].ToString();
                                }
                            }

                            ViewBag.RenderizadoIndiceEstadisticoXContrato = estadisticasXContrato;

                            #endregion
                            break;
                    }

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

        public ActionResult SolicitudContrato()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {

                    string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, "", objects[0].Token.ToString(), ModuleControlRetornoPath());

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    // captura de cualquier excepcion del sistema
                    // errores 500
                    // errores de sesion expirada
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        public ActionResult SolicitudRenovacion()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {

                    string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, "", objects[0].Token.ToString(), ModuleControlRetornoPath());

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    // captura de cualquier excepcion del sistema
                    // errores 500
                    // errores de sesion expirada
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        #region "Carga Masiva productiva actual"
        //public ActionResult SolicitudContrato()
        //{
        //    #region "PERMISOS Y ACCESOS"

        //    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
        //    {
        //        Session["ApplicationActive"] = null;
        //    }

        //    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
        //    {
        //        sistema = Request.Url.AbsoluteUri.Split('/')[3];
        //        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
        //    }
        //    else
        //    {
        //        sistema = fromDebugQA;
        //        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
        //    }

        //    /** APLICACION DE HEADERS */

        //    string[] parametrosHeadersAccess =
        //    {
        //        "@AUTHENTICATE",
        //        "@AGENTAPP",
        //        "@CODIFICARAUTH",
        //        "@SISTEMA",
        //        "@TRABAJADOR"
        //    };

        //    string[] valoresHeadersAccess =
        //    {
        //        tokenAuth,
        //        agenteAplication,
        //        Session["CodificarAuth"].ToString(),
        //        sistema,
        //        Session["NombreUsuario"].ToString()
        //    };

        //    /** END APLICACION DE HEADERS */

        //    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
        //    List<Models.Accesos> accesos = new List<Models.Accesos>();

        //    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
        //    {
        //        Models.Accesos acceso = new Models.Accesos();

        //        acceso.Code = rowsPermisos["Code"].ToString();
        //        acceso.Message = rowsPermisos["Message"].ToString();
        //        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
        //        acceso.PreController = rowsPermisos["PreController"].ToString();
        //        acceso.Controller = rowsPermisos["Controllers"].ToString();
        //        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
        //        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

        //        accesos.Add(acceso);
        //    }

        //    Session["RenderizadoPermisos"] = accesos;

        //    #endregion

        //    List<Models.CargaMasiva> cargasMasivas = new List<Models.CargaMasiva>();

        //    string[] paramRenderAnexos =
        //    {
        //        "@AUTHENTICATE",
        //        "@AGENTAPP",
        //        "@CARGAMASIVA"
        //    };

        //    string[] valRenderAnexos =
        //    {
        //        tokenAuth,
        //        agenteAplication,
        //        schema
        //    };

        //    DataSet dataRenderCargaMasiva = servicioOperaciones.GetObtenerRenderizadoCargaMasiva(paramRenderAnexos, valRenderAnexos).Table;

        //    foreach (DataRow rows in dataRenderCargaMasiva.Tables[0].Rows)
        //    {
        //        if (rows["Code"].ToString() == "200")
        //        {
        //            Models.CargaMasiva cargaMasiva = new Models.CargaMasiva();

        //            cargaMasiva.Code = rows["Code"].ToString();
        //            cargaMasiva.Message = rows["Message"].ToString();
        //            cargaMasiva.Glyphicon = rows["Glyphicon"].ToString();
        //            cargaMasiva.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //            cargaMasiva.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
        //            cargaMasiva.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
        //            cargaMasiva.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
        //            cargaMasiva.UploadRenderizadoColor = rows["UploadRenderizadoColor"].ToString();
        //            cargaMasiva.UploadNombrePlantilla = rows["UploadNombrePlantilla"].ToString();
        //            cargaMasiva.UploadNombreHojaCargaMasiva = rows["UploadNombreHojaCargaMasiva"].ToString();
        //            cargaMasiva.UploadCifrado = rows["UploadCifrado"].ToString();
        //            cargaMasiva.UploadColumnas = rows["UploadColumnas"].ToString();
        //            cargaMasiva.UploadNodoPadre = rows["UploadNodoPadre"].ToString();
        //            cargaMasiva.UploadNodoHijo = rows["UploadNodoHijo"].ToString();
        //            cargaMasiva.UploadRenderizadoGlyphicon = rows["UploadRenderizadoGlyphicon"].ToString();
        //            cargaMasiva.UploadRenderizadoOneTexto = rows["UploadRenderizadoOneTexto"].ToString();
        //            cargaMasiva.UploadRenderizadoSecTexto = rows["UploadRenderizadoSecTexto"].ToString();
        //            cargaMasiva.UploadRenderizadoMensajeImpt = rows["UploadRenderizadoMensajeImpt"].ToString();
        //            cargaMasiva.RenderizadoTituloDownload = rows["RenderizadoTituloDownload"].ToString();
        //            cargaMasiva.DownloadCifrado = rows["DownloadCifrado"].ToString();
        //            cargaMasiva.DownloadRenderizadoColor = rows["DownloadRenderizadoColor"].ToString();
        //            cargaMasiva.DownloadRenderizadoGlyphicon = rows["DownloadRenderizadoGlyphicon"].ToString();
        //            cargaMasiva.DownloadRenderizadoOneTexto = rows["DownloadRenderizadoOneTexto"].ToString();
        //            cargaMasiva.DownloadRenderizadoSecTexto = rows["DownloadRenderizadoSecTexto"].ToString();
        //            cargaMasiva.DownloadRenderizadoMensajeImpt = rows["DownloadRenderizadoMensajeImpt"].ToString();

        //            cargasMasivas.Add(cargaMasiva);
        //        }
        //    }

        //    ViewBag.RenderizadoCargaMasiva = cargasMasivas;

        //    ViewBag.CodeCorrect = true;

        //    return View();
        //}

        //public ActionResult SolicitudRenovacion()
        //{
        //    #region "PERMISOS Y ACCESOS"

        //    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
        //    {
        //        Session["ApplicationActive"] = null;
        //    }

        //    if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
        //    {
        //        sistema = Request.Url.AbsoluteUri.Split('/')[3];
        //        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
        //    }
        //    else
        //    {
        //        sistema = fromDebugQA;
        //        schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
        //    }

        //    /** APLICACION DE HEADERS */

        //    string[] parametrosHeadersAccess =
        //    {
        //        "@AUTHENTICATE",
        //        "@AGENTAPP",
        //        "@CODIFICARAUTH",
        //        "@SISTEMA",
        //        "@TRABAJADOR"
        //    };

        //    string[] valoresHeadersAccess =
        //    {
        //        tokenAuth,
        //        agenteAplication,
        //        Session["CodificarAuth"].ToString(),
        //        sistema,
        //        Session["NombreUsuario"].ToString()
        //    };

        //    /** END APLICACION DE HEADERS */

        //    DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
        //    List<Models.Accesos> accesos = new List<Models.Accesos>();

        //    foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
        //    {
        //        Models.Accesos acceso = new Models.Accesos();

        //        acceso.Code = rowsPermisos["Code"].ToString();
        //        acceso.Message = rowsPermisos["Message"].ToString();
        //        acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
        //        acceso.PreController = rowsPermisos["PreController"].ToString();
        //        acceso.Controller = rowsPermisos["Controllers"].ToString();
        //        acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
        //        acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

        //        accesos.Add(acceso);
        //    }

        //    Session["RenderizadoPermisos"] = accesos;

        //    #endregion

        //    List<Models.CargaMasiva> cargasMasivas = new List<Models.CargaMasiva>();

        //    string[] paramRenderAnexos =
        //    {
        //        "@AUTHENTICATE",
        //        "@AGENTAPP",
        //        "@CARGAMASIVA"
        //    };

        //    string[] valRenderAnexos =
        //    {
        //        tokenAuth,
        //        agenteAplication,
        //        schema
        //    };

        //    DataSet dataRenderCargaMasiva = servicioOperaciones.GetObtenerRenderizadoCargaMasiva(paramRenderAnexos, valRenderAnexos).Table;

        //    foreach (DataRow rows in dataRenderCargaMasiva.Tables[0].Rows)
        //    {
        //        if (rows["Code"].ToString() == "200")
        //        {
        //            Models.CargaMasiva cargaMasiva = new Models.CargaMasiva();

        //            cargaMasiva.Code = rows["Code"].ToString();
        //            cargaMasiva.Message = rows["Message"].ToString();
        //            cargaMasiva.Glyphicon = rows["Glyphicon"].ToString();
        //            cargaMasiva.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //            cargaMasiva.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
        //            cargaMasiva.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
        //            cargaMasiva.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
        //            cargaMasiva.UploadRenderizadoColor = rows["UploadRenderizadoColor"].ToString();
        //            cargaMasiva.UploadNombrePlantilla = rows["UploadNombrePlantilla"].ToString();
        //            cargaMasiva.UploadNombreHojaCargaMasiva = rows["UploadNombreHojaCargaMasiva"].ToString();
        //            cargaMasiva.UploadCifrado = rows["UploadCifrado"].ToString();
        //            cargaMasiva.UploadColumnas = rows["UploadColumnas"].ToString();
        //            cargaMasiva.UploadNodoPadre = rows["UploadNodoPadre"].ToString();
        //            cargaMasiva.UploadNodoHijo = rows["UploadNodoHijo"].ToString();
        //            cargaMasiva.UploadRenderizadoGlyphicon = rows["UploadRenderizadoGlyphicon"].ToString();
        //            cargaMasiva.UploadRenderizadoOneTexto = rows["UploadRenderizadoOneTexto"].ToString();
        //            cargaMasiva.UploadRenderizadoSecTexto = rows["UploadRenderizadoSecTexto"].ToString();
        //            cargaMasiva.UploadRenderizadoMensajeImpt = rows["UploadRenderizadoMensajeImpt"].ToString();
        //            cargaMasiva.RenderizadoTituloDownload = rows["RenderizadoTituloDownload"].ToString();
        //            cargaMasiva.DownloadCifrado = rows["DownloadCifrado"].ToString();
        //            cargaMasiva.DownloadRenderizadoColor = rows["DownloadRenderizadoColor"].ToString();
        //            cargaMasiva.DownloadRenderizadoGlyphicon = rows["DownloadRenderizadoGlyphicon"].ToString();
        //            cargaMasiva.DownloadRenderizadoOneTexto = rows["DownloadRenderizadoOneTexto"].ToString();
        //            cargaMasiva.DownloadRenderizadoSecTexto = rows["DownloadRenderizadoSecTexto"].ToString();
        //            cargaMasiva.DownloadRenderizadoMensajeImpt = rows["DownloadRenderizadoMensajeImpt"].ToString();

        //            cargasMasivas.Add(cargaMasiva);
        //        }
        //    }

        //    ViewBag.RenderizadoCargaMasiva = cargasMasivas;

        //    ViewBag.CodeCorrect = true;

        //    return View();
        //}

        #endregion

        public ActionResult Anexos()
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

                List<Models.Anexos> anexos = new List<Models.Anexos>();
                List<Models.CargaMasiva> cargasMasivas = new List<Models.CargaMasiva>();

                switch (schema)
                {
                    case "Anexos":
                        schema = "Anexos";
                        ViewBag.RenderizaIndexCargas = true;
                        break;
                    default:
                        ViewBag.RenderizaIndexCargas = false;
                        break;
                }

                if (schema == "Anexos")
                {
                    string[] paramRenderAnexos =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@FILTER",
                        "@TIPODOCUMENTO"
                    };

                    string[] valRenderAnexos =
                    {
                        tokenAuth,
                        agenteAplication,
                        "",
                        schema
                    };

                    DataSet dataRenderAnexos = servicioOperaciones.GetObtenerRenderizadoDocAnexos(paramRenderAnexos, valRenderAnexos).Table;

                    foreach (DataRow rows in dataRenderAnexos.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.Anexos anexo = new Models.Anexos();

                            anexo.Code = rows["Code"].ToString();
                            anexo.Message = rows["Message"].ToString();
                            anexo.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
                            anexo.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
                            anexo.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
                            anexo.Glyphicon = rows["Glyphicon"].ToString();
                            anexo.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            anexo.BorderColor = rows["BorderColor"].ToString();
                            anexo.ColorFont = rows["ColorFont"].ToString();
                            anexo.ButtonClass = rows["ButtonClass"].ToString();
                            anexo.RenderizadoButtonOne = rows["RenderizadoButtonOne"].ToString();
                            anexo.RenderizadoButtonTwo = rows["RenderizadoButtonTwo"].ToString();
                            anexo.Path = rows["Path"].ToString();

                            anexos.Add(anexo);
                        }
                    }

                    ViewBag.RenderizaIndexDocumentosAnexos = anexos;
                }

                if (schema != "Anexos")
                {
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
                        schema
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
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult AdministrarCuentas()
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

                DataSet dataCuenta = servicioOperaciones.GetObtenerCuentasSinAsignar(parametrosHeadersCuentas, valoresHeadersCuentas).Table;
                DataSet kamDS = servicioOperaciones.GetObtenerKam(parametrosHeadersCuentas, valoresHeadersCuentas).Table;

                List<Models.Cuentas> cuentasTWEST = new List<Models.Cuentas>();
                List<Models.Cuentas> cuentasTWRRHH = new List<Models.Cuentas>();
                List<Models.Cuentas> cuentasTWC = new List<Models.Cuentas>();

                List<Models.Kam> kamList = new List<Models.Kam>();


                foreach (DataRow rowsCuentas in dataCuenta.Tables[0].Rows)
                {
                    Models.Cuentas cuenta = new Models.Cuentas();

                    cuenta.Code = rowsCuentas["Code"].ToString();
                    cuenta.Message = rowsCuentas["Message"].ToString();
                    cuenta.CodificarCod = rowsCuentas["CodificarCod"].ToString();
                    cuenta.Codigo = rowsCuentas["Codigo"].ToString();
                    cuenta.Empresa = rowsCuentas["Empresa"].ToString();
                    cuenta.ColorEvent = rowsCuentas["ColorEvent"].ToString();

                    switch (rowsCuentas["Empresa"].ToString())
                    {
                        case "TWEST":
                            cuentasTWEST.Add(cuenta);
                            break;
                        case "TWRRHH":
                            cuentasTWRRHH.Add(cuenta);
                            break;
                        case "TWC":
                            cuentasTWC.Add(cuenta);
                            break;
                    }

                }

                foreach (DataRow row in kamDS.Tables[0].Rows)
                {
                    List<Models.Cliente> clienteList = new List<Models.Cliente>();
                    Models.Kam kam = new Models.Kam();

                    kam.Code = row["Code"].ToString();
                    kam.Message = row["Message"].ToString();
                    kam.Nombre = row["Nombre"].ToString();
                    kam.Apellido = row["Apellido"].ToString();
                    kam.Id = row["Id"].ToString();

                    string[] parametrosHeadersCuentasRequest =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@KAMID"
                    };

                    string[] valoresHeadersCuentasRequest =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        kam.Id
                    };

                    DataSet clientesKamDS = servicioOperaciones.GetObtenerClientesKam(parametrosHeadersCuentasRequest, valoresHeadersCuentasRequest).Table;
                    //clienteList.Clear();

                    foreach (DataRow rowCliente in clientesKamDS.Tables[0].Rows)
                    {

                        if (rowCliente["Code"].ToString() == "200")
                        {

                            Models.Cliente cliente = new Models.Cliente();

                            cliente.Code = rowCliente["Code"].ToString();
                            cliente.Message = rowCliente["Message"].ToString();
                            cliente.Id = rowCliente["Id"].ToString();
                            cliente.Codigo = rowCliente["Codigo"].ToString();
                            cliente.Nombre = rowCliente["Nombre"].ToString();
                            cliente.Kam = rowCliente["Kam"].ToString();

                            clienteList.Add(cliente);

                        }
                    }

                    kam.clientes = clienteList;

                    kamList.Add(kam);
                }

                ViewBag.RenderizadoCuentasTWEST = cuentasTWEST;
                ViewBag.RenderizadoCuentasTWRRHH = cuentasTWRRHH;
                ViewBag.RenderizadoCuentasTWC = cuentasTWC;

                ViewBag.RenderizadoKams = kamList;
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        #region "Actualmente en produccion"

        public ActionResult Procesos()
        {
            try
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

                    if (schema != "Contratos" && schema != "Renovaciones" && schema != "Procesos")
                    {
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                    }
                }
                else
                {
                    sistema = fromDebugQA;
                    schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    if (schema != "Contratos" && schema != "Renovaciones" && schema != "Procesos")
                    {
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                    }
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

                List<Models.Proceso> procesosC = new List<Models.Proceso>();
                List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
                List<Models.Proceso> procesosR = new List<Models.Proceso>();
                List<Models.Pagination> paginatorsR = new List<Models.Pagination>();
                List<Models.Solicitud> solicitudR = new List<Models.Solicitud>();
                List<Models.Solicitud> solicitudC = new List<Models.Solicitud>();

                List<Models.Prioridades> prioridades = new List<Models.Prioridades>();

                List<Models.HeaderProcesos> headerProcesosC = new List<Models.HeaderProcesos>();
                List<Models.HeaderProcesos> headerProcesosR = new List<Models.HeaderProcesos>();

                List<Models.HeaderSolicitud> headerSolicitudC = new List<Models.HeaderSolicitud>();
                List<Models.HeaderSolicitud> headerSolicitudR = new List<Models.HeaderSolicitud>();

                ViewBag.RenderizaContratos = false;
                ViewBag.RenderizaRenovaciones = false;
                ViewBag.RenderizaSolicitud = false;
                ViewBag.RenderizaContratoSolicitud = false;
                ViewBag.RenderizaRenovacionSolicitud = false;

                switch (schema)
                {
                    case "Procesos":
                        schema = "Procesos";
                        section = "";
                        break;
                    case "Renovaciones":
                        schema = "Renovaciones";
                        section = "";
                        break;
                    case "Contratos":
                        schema = "Contratos";
                        section = "";
                        break;
                    default:
                        ViewBag.RenderizaSolicitud = true;
                        break;
                }


                #region "HEADER PROCESOS RENDERIZADOS"

                #region "HEADER PROCESOS CONTRATOS"

                string[] paramHeaderProcC =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR",
                    "@TIPOSOLICITUD",
                    "@PAGINATION",
                    "@TYPEFILTER",
                    "@DATAFILTER"
                };

                string[] valHeaderProcC =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),
                    "U29saWNpdHVkQ29udHJhdG8=",
                    "MS01",
                    "",
                    ""
                };

                DataSet dataHeaderProcesosC = servicioOperaciones.GetObtenerHeaderProcesos(paramHeaderProcC, valHeaderProcC).Table;

                foreach (DataRow item in dataHeaderProcesosC.Tables[0].Rows)
                {
                    Models.HeaderProcesos header = new Models.HeaderProcesos();

                    header.PostTitulo = item["PostTitulo"].ToString();
                    header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                    header.HtmlPagination = item["HtmlPagination"].ToString();
                    header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                    header.TipoSolicitud = item["TipoSolicitud"].ToString();
                    header.ResultSearch = item["ResultSearch"].ToString();

                    headerProcesosC.Add(header);
                }

                ViewBag.RenderizadoHeaderProcesosContratos = headerProcesosC;

                #endregion

                #region "HEADER PROCESOS RENOVACIONES"

                string[] paramHeaderProcR =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR",
                    "@TIPOSOLICITUD",
                    "@PAGINATION",
                    "@TYPEFILTER",
                    "@DATAFILTER"
                };

                string[] valHeaderProcR =
                {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),
                    "U29saWNpdHVkUmVub3ZhY2lvbg==",
                    "MS01",
                    "",
                    ""
                };

                DataSet dataHeaderProcesosR = servicioOperaciones.GetObtenerHeaderProcesos(paramHeaderProcR, valHeaderProcR).Table;

                foreach (DataRow item in dataHeaderProcesosR.Tables[0].Rows)
                {
                    Models.HeaderProcesos header = new Models.HeaderProcesos();

                    header.PostTitulo = item["PostTitulo"].ToString();
                    header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                    header.HtmlPagination = item["HtmlPagination"].ToString();
                    header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                    header.TipoSolicitud = item["TipoSolicitud"].ToString();
                    header.ResultSearch = item["ResultSearch"].ToString();

                    headerProcesosR.Add(header);
                }

                ViewBag.RenderizadoHeaderProcesosRenovaciones = headerProcesosR;

                #endregion

                #endregion

                #region "HEADER SOLICITUDES RENDERIZADO"

                #region "HEADER SOLICITUDES CONTRATOS"

                if (section == "Contratos" && schema != "Procesos" && schema != "")
                {
                    string[] paramHeaderSolC =
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

                    string[] valHeaderSolC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataHeaderSolicitudC = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolC, valHeaderSolC).Table;

                    foreach (DataRow item in dataHeaderSolicitudC.Tables[0].Rows)
                    {
                        Models.HeaderSolicitud header = new Models.HeaderSolicitud();

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

                        headerSolicitudC.Add(header);
                    }

                    ViewBag.RenderizadoHeaderSolicitudContratos = headerSolicitudC;
                }

                #endregion

                #region "HEADER SOLICITUDES RENOVACIONES"

                if (section == "Renovaciones" && schema != "Procesos" && schema != "")
                {
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
                        schema,
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataHeaderSolicitudR = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolR, valHeaderSolR).Table;

                    foreach (DataRow item in dataHeaderSolicitudR.Tables[0].Rows)
                    {
                        Models.HeaderSolicitud header = new Models.HeaderSolicitud();

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

                        headerSolicitudR.Add(header);
                    }

                    ViewBag.RenderizadoHeaderSolicitudRenovaciones = headerSolicitudR;
                }

                #endregion

                #endregion

                #region "PROCESO RENDERIZADO"

                if (section == "" && schema == "Contratos" || schema == "Procesos")
                {
                    /** PROCESOS SOLICITUD CONTRATOS */

                    string[] headerParamProcesosC =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@TIPOSOLICITUD",
                        "@PAGINATION",
                        "@TYPEFILTER",
                        "@DATAFILTER",
                        "@TYPEDASHBOARD",
                        "@DAYDASHBOARD",
                        "@MONTHDASHBOARD",
                        "@YEARDASHBOARD",
                        "@ANALISTA",
                        "@TIPOESTADO",
                        "@ESTADO"
                    };

                    string[] headerValoresProcesosC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        "",
                        "Completo",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""
                    };

                    DataSet dataProcesosC = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesosC, headerValoresProcesosC).Table;

                    foreach (DataRow rows in dataProcesosC.Tables[0].Rows)
                    {
                        Models.Proceso proceso = new Models.Proceso();

                        if (rows["Code"].ToString() == "200")
                        {
                            proceso.Code = rows["Code"].ToString();
                            proceso.Message = rows["Message"].ToString();

                            proceso.TipoEvento = rows["TipoEvento"].ToString();

                            proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                            proceso.NombreProceso = rows["NombreProceso"].ToString();
                            proceso.Creado = rows["Creado"].ToString();
                            proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                            proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                            proceso.Comentarios = rows["Comentarios"].ToString();
                            proceso.CodificarCod = rows["CodificarCod"].ToString();

                            proceso.Prioridad = rows["Prioridad"].ToString();
                            proceso.Glyphicon = rows["Glyphicon"].ToString();
                            proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            proceso.BorderColor = rows["BorderColor"].ToString();

                            proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                            proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                            proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                            proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();

                            proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                            proceso.OptRevertirTermino = rows["OptRevertirTermino"].ToString();
                        }
                        else
                        {
                            proceso.Code = rows["Code"].ToString();
                            proceso.Message = rows["Message"].ToString();
                        }

                        procesosC.Add(proceso);
                    }

                    ViewBag.RenderizadoProcesosContratos = procesosC;

                    /** PAGINATOR */

                    string[] paramPaginatorProcesosC =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@TIPOSOLICITUD",
                        "@PAGINATION",
                        "@TYPEFILTER",
                        "@DATAFILTER",
                        "@TYPEDASHBOARD",
                        "@DAYDASHBOARD",
                        "@MONTHDASHBOARD",
                        "@YEARDASHBOARD",
                        "@ANALISTA",
                        "@TIPOESTADO",
                        "@ESTADO"
                    };

                    string[] valPaginatorProcesosC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        "",
                        "Completo",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""
                    };

                    DataSet dataPaginadoC = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosC, valPaginatorProcesosC).Table;

                    foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
                    {
                        if (dataRowPaginadoC["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoC["Code"].ToString();
                            paginado.Message = dataRowPaginadoC["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoC["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();
                            paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

                            paginatorsC.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagProcesosContratos = paginatorsC;
                    ViewBag.RenderizaContratos = true;

                    /** END PROCESO SOLICITUD CONTRATOS */
                }

                if (section == "" && schema == "Renovaciones" || schema == "Procesos")
                {

                    /** PROCESOS SOLICITUD RENOVACION */

                    string[] headerParamProcesos =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@TIPOSOLICITUD",
                        "@PAGINATION",
                        "@TYPEFILTER",
                        "@DATAFILTER",
                        "@TYPEDASHBOARD",
                        "@DAYDASHBOARD",
                        "@MONTHDASHBOARD",
                        "@YEARDASHBOARD",
                        "@ANALISTA",
                        "@TIPOESTADO",
                        "@ESTADO"
                    };

                    string[] headerValoresProcesos =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        "",
                        "Completo",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""
                    };

                    DataSet dataProcesos = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesos, headerValoresProcesos).Table;

                    foreach (DataRow rows in dataProcesos.Tables[0].Rows)
                    {
                        Models.Proceso proceso = new Models.Proceso();

                        if (rows["Code"].ToString() == "200")
                        {
                            proceso.Code = rows["Code"].ToString();
                            proceso.Message = rows["Message"].ToString();

                            proceso.TipoEvento = rows["TipoEvento"].ToString();

                            proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                            proceso.NombreProceso = rows["NombreProceso"].ToString();
                            proceso.Creado = rows["Creado"].ToString();
                            proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                            proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                            proceso.Comentarios = rows["Comentarios"].ToString();
                            proceso.CodificarCod = rows["CodificarCod"].ToString();

                            proceso.Prioridad = rows["Prioridad"].ToString();
                            proceso.Glyphicon = rows["Glyphicon"].ToString();
                            proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            proceso.BorderColor = rows["BorderColor"].ToString();

                            proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                            proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                            proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                            proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();

                            proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                            proceso.OptRevertirTermino = rows["OptRevertirTermino"].ToString();
                        }
                        else
                        {
                            proceso.Code = rows["Code"].ToString();
                            proceso.Message = rows["ErrorMessage"].ToString();
                        }

                        procesosR.Add(proceso);
                    }

                    ViewBag.RenderizadoProcesosRenovaciones = procesosR;
                    ViewBag.RenderizaRenovaciones = true;

                    /** PAGINATOR */

                    string[] paramPaginatorProcesosR =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@TIPOSOLICITUD",
                        "@PAGINATION",
                        "@TYPEFILTER",
                        "@DATAFILTER",
                        "@TYPEDASHBOARD",
                        "@DAYDASHBOARD",
                        "@MONTHDASHBOARD",
                        "@YEARDASHBOARD",
                        "@ANALISTA",
                        "@TIPOESTADO",
                        "@ESTADO"
                    };

                    string[] valPaginatorProcesosR =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        "",
                        "Completo",
                        "",
                        "",
                        "",
                        "",
                        "",
                        ""
                    };

                    DataSet dataPaginadoR = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosR, valPaginatorProcesosR).Table;

                    foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
                    {
                        if (dataRowPaginadoR["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoR["Code"].ToString();
                            paginado.Message = dataRowPaginadoR["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoR["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();
                            paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

                            paginatorsR.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagProcesosRenovaciones = paginatorsR;

                    /** END PROCESO SOLICITUD RENOVACION */
                }

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
                    "ContratoRenovacion"
                };

                DataSet dataPrioridades = servicioOperaciones.GetPrioridadesEstado(paramPrioridades, valPrioridades).Table;

                foreach (DataRow rows in dataPrioridades.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.Prioridades prioridad = new Models.Prioridades();

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

                ViewBag.RenderizadoPrioridades = prioridades;

                #endregion

                #region "SOLICITUD RENDERIZADO"

                if (section == "Contratos" && schema != "Procesos" && schema != "")
                {

                    /** PROCESOS SOLICITUD CONTRATO */

                    string[] headerParamSolictudC =
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

                    string[] headerValoresSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataSolicitudesC = servicioOperaciones.GetObtenerSolicitudes(headerParamSolictudC, headerValoresSolicitudC).Table;

                    foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.Solicitud solicitud = new Models.Solicitud();

                            solicitud.Code = rows["Code"].ToString();
                            solicitud.Message = rows["Message"].ToString();
                            solicitud.TipoEvento = rows["TipoEvento"].ToString();
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

                            solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                            solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                            solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                            solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                            solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                            solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                            solicitudC.Add(solicitud);
                        }
                    }

                    ViewBag.RenderizadoSolicitudContratos = solicitudC;

                    /** PAGINATOR */

                    string[] paramPaginatorSolicitudC =
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

                    string[] valPaginatorSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataPaginadoC = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudC, valPaginatorSolicitudC).Table;

                    foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
                    {
                        if (dataRowPaginadoC["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoC["Code"].ToString();
                            paginado.Message = dataRowPaginadoC["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoC["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();

                            paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

                            paginatorsC.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagSolicitudesContratos = paginatorsC;

                    ViewBag.RenderizaContratoSolicitud = true;

                    /** END PROCESO SOLICITUD CONTRATO */

                }

                if (section == "Renovaciones" && schema != "Procesos" && schema != "")
                {
                    /** END PROCESO SOLICITUD RENOVACION */

                    string[] headerParamSolicitudR =
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

                    string[] headerValoresSolicitudR =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataSolicitudesR = servicioOperaciones.GetObtenerSolicitudes(headerParamSolicitudR, headerValoresSolicitudR).Table;

                    foreach (DataRow rows in dataSolicitudesR.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.Solicitud solicitud = new Models.Solicitud();

                            solicitud.Code = rows["Code"].ToString();
                            solicitud.Message = rows["Message"].ToString();
                            solicitud.TipoEvento = rows["TipoEvento"].ToString();
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

                            solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                            solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                            solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                            solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                            solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                            solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                            solicitudR.Add(solicitud);
                        }
                    }

                    ViewBag.RenderizadoSolicitudRenovaciones = solicitudR;

                    /** PAGINATOR */

                    string[] paramPaginatorSolicitudR =
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

                    string[] valPaginatorSolicitudR =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataPaginadoR = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudR, valPaginatorSolicitudR).Table;

                    foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
                    {
                        if (dataRowPaginadoR["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoR["Code"].ToString();
                            paginado.Message = dataRowPaginadoR["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoR["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();
                            paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

                            paginatorsR.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagSolicitudesRenovaciones = paginatorsR;

                    ViewBag.RenderizaRenovacionSolicitud = true;

                    /** END PROCESO SOLICITUD RENOVACION */

                }

                #endregion

                #region "MOTIVOS DE ANULACION RENDERIZADO"

                List<Models.MotivosAnulacion> motivos = new List<Models.MotivosAnulacion>();

                string[] paramMotivosAnulacion =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP"
                };

                string[] valMotivosAnulacion =
                {
                    tokenAuth,
                    agenteAplication
                };

                DataSet dataMotivosAnulacion = servicioOperaciones.GetObtenerMotivosAnulacion(paramMotivosAnulacion, valMotivosAnulacion).Table;

                foreach (DataRow rows in dataMotivosAnulacion.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.MotivosAnulacion motivo = new Models.MotivosAnulacion();

                        motivo.Code = rows["Code"].ToString();
                        motivo.Message = rows["Message"].ToString();
                        motivo.Descripcion = rows["Descripcion"].ToString();

                        motivos.Add(motivo);
                    }
                }

                ViewBag.RenderizadoMotivosAnulacion = motivos;

                #endregion

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

            return View();
        }

        public ActionResult Solicitudes()
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
                    schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    if (schema != "Contrato" && schema != "Renovacion" && schema != "Solicitudes")
                    {
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                    }
                }
                else
                {
                    sistema = fromDebugQA;
                    schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    if (schema != "Contrato" && schema != "Renovacion" && schema != "Solicitudes")
                    {
                        section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                    }
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

                List<Models.Solicitud> solicitudR = new List<Models.Solicitud>();
                List<Models.Pagination> paginatorsR = new List<Models.Pagination>();
                List<Models.Solicitud> solicitudC = new List<Models.Solicitud>();
                List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
                List<Models.SolicitudContrato> solicitudContratos = new List<Models.SolicitudContrato>();
                List<Models.SolicitudRenovacion> solicitudRenovaciones = new List<Models.SolicitudRenovacion>();
                List<Models.Prioridades> prioridades = new List<Models.Prioridades>();

                List<Models.HeaderSolicitud> headerSolicitudC = new List<Models.HeaderSolicitud>();
                List<Models.HeaderSolicitud> headerSolicitudR = new List<Models.HeaderSolicitud>();

                ViewBag.RenderizaContratos = false;
                ViewBag.RenderizaRenovaciones = false;
                ViewBag.RenderizaSolicitudData = false;
                ViewBag.RenderizaContratoSolicitud = false;
                ViewBag.RenderizaRenovacionSolicitud = false;
                ViewBag.RenderizaSolicitud = false;

                switch (schema)
                {
                    case "Procesos":
                        schema = "Solicitudes";
                        section = "";
                        break;
                    case "Renovacion":
                        schema = "Renovacion";
                        section = "";
                        break;
                    case "Contrato":
                        schema = "Contrato";
                        section = "";
                        break;
                }

                #region "HEADER SOLICITUDES RENDERIZADO"

                #region "HEADER SOLICITUDES CONTRATOS"

                if (section == "" && schema != "Contrato" || schema != "Solicitudes")
                {
                    string[] paramHeaderSolC =
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

                    string[] valHeaderSolC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "",
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataHeaderSolicitudC = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolC, valHeaderSolC).Table;

                    foreach (DataRow item in dataHeaderSolicitudC.Tables[0].Rows)
                    {
                        Models.HeaderSolicitud header = new Models.HeaderSolicitud();

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

                        headerSolicitudC.Add(header);
                    }

                    ViewBag.RenderizadoHeaderSolicitudContratos = headerSolicitudC;
                }

                #endregion

                #region "HEADER SOLICITUDES RENOVACIONES"

                if (section == "" && schema != "Renovacion" || schema != "Solicitudes")
                {
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
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataHeaderSolicitudR = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolR, valHeaderSolR).Table;

                    foreach (DataRow item in dataHeaderSolicitudR.Tables[0].Rows)
                    {
                        Models.HeaderSolicitud header = new Models.HeaderSolicitud();

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

                        headerSolicitudR.Add(header);
                    }

                    ViewBag.RenderizadoHeaderSolicitudRenovaciones = headerSolicitudR;
                }

                #endregion

                #endregion

                #region "RENDERIZADO SOLICITUDES"

                if (section == "" && schema == "Contrato" || schema == "Solicitudes")
                {
                    /** PROCESOS SOLICITUD CONTRATOS */

                    string[] headerParamSolictudC =
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

                    string[] headerValoresSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "",
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataSolicitudesC = servicioOperaciones.GetObtenerSolicitudes(headerParamSolictudC, headerValoresSolicitudC).Table;

                    foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.Solicitud solicitud = new Models.Solicitud();

                            solicitud.TipoEvento = rows["TipoEvento"].ToString();
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

                            solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                            solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                            solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                            solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                            solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                            solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                            solicitudC.Add(solicitud);
                        }
                    }

                    ViewBag.RenderizadoSolicitudContratos = solicitudC;

                    /** PAGINATOR */
                    string[] paramPaginator = new string[8];
                    string[] valPaginator = new string[8];
                    DataSet dataPaginator;
                    List<Models.Teamwork.Pagination> paginations = new List<Models.Teamwork.Pagination>();

                    string[] paramPaginatorSolicitudC =
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

                    string[] valPaginatorSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "",
                        "U29saWNpdHVkQ29udHJhdG8=",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataPaginadoC = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudC, valPaginatorSolicitudC).Table;

                    foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
                    {
                        if (dataRowPaginadoC["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoC["Code"].ToString();
                            paginado.Message = dataRowPaginadoC["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoC["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();
                            paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

                            paginatorsC.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagSolicitudesContratos = paginatorsC;

                    /** END PROCESO SOLICITUD CONTRATOS */
                }

                if (section == "" && schema == "Renovacion" || schema == "Solicitudes")
                {
                    /** PROCESOS SOLICITUD RENOVACION */

                    string[] headerParamSolicitudR =
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

                    string[] headerValoresSolicitudR =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "",
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataSolicitudes = servicioOperaciones.GetObtenerSolicitudes(headerParamSolicitudR, headerValoresSolicitudR).Table;

                    foreach (DataRow rows in dataSolicitudes.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.Solicitud solicitud = new Models.Solicitud();

                            solicitud.TipoEvento = rows["TipoEvento"].ToString();
                            solicitud.Code = rows["Code"].ToString();
                            solicitud.Message = rows["Message"].ToString();
                            solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                            solicitud.NombreProceso = rows["NombreProceso"].ToString();
                            solicitud.Creado = rows["Creado"].ToString();
                            solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
                            solicitud.Comentarios = rows["Comentarios"].ToString();
                            solicitud.CodificarCod = rows["CodificarCod"].ToString();

                            solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();
                            solicitud.Prioridad = rows["Prioridad"].ToString();
                            solicitud.Glyphicon = rows["Glyphicon"].ToString();
                            solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            solicitud.BorderColor = rows["BorderColor"].ToString();
                            solicitud.ColorFont = rows["ColorFont"].ToString();

                            solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                            solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                            solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                            solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                            solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                            solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                            solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                            solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                            solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                            solicitudR.Add(solicitud);
                        }
                    }

                    ViewBag.RenderizadoSolicitudRenovaciones = solicitudR;

                    /** PAGINATOR */

                    string[] paramPaginatorSolicitudR =
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

                    string[] valPaginatorSolicitudR =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        "",
                        "U29saWNpdHVkUmVub3ZhY2lvbg==",
                        "MS01",
                        "",
                        ""
                    };

                    DataSet dataPaginadoR = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudR, valPaginatorSolicitudR).Table;

                    foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
                    {
                        if (dataRowPaginadoR["Code"].ToString() == "200")
                        {
                            Models.Pagination paginado = new Models.Pagination();

                            paginado.Code = dataRowPaginadoR["Code"].ToString();
                            paginado.Message = dataRowPaginadoR["Message"].ToString();
                            paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
                            paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
                            paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
                            paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
                            paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
                            paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
                            paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
                            paginado.Active = dataRowPaginadoR["Activo"].ToString();

                            paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
                            paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
                            paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

                            paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();
                            paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

                            paginatorsR.Add(paginado);
                        }
                    }

                    ViewBag.RenderizadoPagSolicitudesRenovaciones = paginatorsR;

                    /** END PROCESO SOLICITUD RENOVACION */
                }

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
                    "ContratoRenovacion"
                };

                DataSet dataPrioridades = servicioOperaciones.GetPrioridadesEstado(paramPrioridades, valPrioridades).Table;

                foreach (DataRow rows in dataPrioridades.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.Prioridades prioridad = new Models.Prioridades();

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

                ViewBag.RenderizadoPrioridades = prioridades;

                #endregion

                #region "RENDERIZA DATOS SOLICITUD"

                if (section == "Contrato" && schema != "Solicitud" && schema != "")
                {
                    ViewBag.RenderizaDatosSolicitudType = "Contratos";

                    string[] headerParamSolictudC =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@CODIGOSOLICITUD",
                        "@TIPOSOLICITUD"
                    };

                    string[] headerValoresSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkQ29udHJhdG8="
                    };

                    DataSet dataSolicitudContrato = servicioOperaciones.GetObtenerDatosSolicitud(headerParamSolictudC, headerValoresSolicitudC).Table;

                    foreach (DataRow rows in dataSolicitudContrato.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.SolicitudContrato solicitudContrato = new Models.SolicitudContrato();

                            solicitudContrato.Code = rows["Code"].ToString();
                            solicitudContrato.Message = rows["Message"].ToString();
                            solicitudContrato.NombreSolicitud = rows["NombreSolicitud"].ToString();
                            solicitudContrato.Comentarios = rows["Comentarios"].ToString();
                            solicitudContrato.Glyphicon = rows["Glyphicon"].ToString();
                            solicitudContrato.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            solicitudContrato.ColorFont = rows["ColorFont"].ToString();
                            solicitudContrato.NombreProceso = rows["NombreProceso"].ToString();
                            solicitudContrato.Creado = rows["Creado"].ToString();
                            solicitudContrato.EjecutivoCarga = rows["EjecutivoCarga"].ToString();
                            solicitudContrato.BusinessCenter = rows["BusinessCenter"].ToString();
                            solicitudContrato.AuthStatement = rows["AuthStatement"].ToString();
                            solicitudContrato.FastTrack = rows["FastTrack"].ToString();
                            solicitudContrato.Rut = rows["Rut"].ToString();
                            solicitudContrato.Nombre = rows["Nombre"].ToString();
                            solicitudContrato.NativeSurname = rows["NativeSurname"].ToString();
                            solicitudContrato.Surname = rows["Surname"].ToString();
                            solicitudContrato.Email = rows["Email"].ToString();
                            solicitudContrato.Birthdate = rows["Birthdate"].ToString();
                            solicitudContrato.CivilState = rows["CivilState"].ToString();
                            solicitudContrato.Gender = rows["Gender"].ToString();
                            solicitudContrato.Address = rows["Address"].ToString();
                            solicitudContrato.Comuna = rows["Comuna"].ToString();
                            solicitudContrato.Ciudad = rows["Ciudad"].ToString();
                            solicitudContrato.Country = rows["Country"].ToString();
                            solicitudContrato.Phone = rows["Phone"].ToString();
                            solicitudContrato.Salud = rows["Salud"].ToString();
                            solicitudContrato.MontoIsapre = rows["MontoIsapre"].ToString();
                            solicitudContrato.Afp = rows["Afp"].ToString();
                            solicitudContrato.PorcentajeAfp = rows["PorcentajeAfp"].ToString();
                            solicitudContrato.PorcentajeSeguroMasComisionAfp = rows["PorcentajeSeguroMasComisionAfp"].ToString();
                            solicitudContrato.Bank = rows["Bank"].ToString();
                            solicitudContrato.BankAccount = rows["BankAccount"].ToString();
                            solicitudContrato.AccountType = rows["AccountType"].ToString();
                            solicitudContrato.Profesion = rows["Profesion"].ToString();
                            solicitudContrato.StartDate = rows["StartDate"].ToString();
                            solicitudContrato.CodCaja = rows["CodCaja"].ToString();
                            solicitudContrato.CodEstudios = rows["CodEstudios"].ToString();
                            solicitudContrato.CodCentroCosto = rows["CodCentroCosto"].ToString();
                            solicitudContrato.CodCargo = rows["CodCargo"].ToString();
                            solicitudContrato.NHijos = rows["NHijos"].ToString();
                            solicitudContrato.UltimoEmpleador = rows["UltimoEmpleador"].ToString();
                            solicitudContrato.TallaPantalon = rows["TallaPantalon"].ToString();
                            solicitudContrato.TallaPolera = rows["TallaPolera"].ToString();
                            solicitudContrato.TallaZapatos = rows["TallaZapatos"].ToString();
                            solicitudContrato.Observaciones = rows["Observaciones"].ToString();
                            solicitudContrato.Categoria = rows["Categoria"].ToString();
                            solicitudContrato.TipoContrato = rows["TipoContrato"].ToString();
                            solicitudContrato.TipoMov = rows["TipoMov"].ToString();
                            solicitudContrato.Causal = rows["Causal"].ToString();
                            solicitudContrato.ObsCausal = rows["ObsCausal"].ToString();
                            solicitudContrato.FinalDate = rows["FinalDate"].ToString();
                            solicitudContrato.DatePrimPlazoInicio = rows["DatePrimPlazoInicio"].ToString();
                            solicitudContrato.DatePrimPlazoTermino = rows["DatePrimPlazoTermino"].ToString();
                            solicitudContrato.DateSegPlazoInicio = rows["DateSegPlazoInicio"].ToString();
                            solicitudContrato.DateSegPlazoTermino = rows["DateSegPlazoTermino"].ToString();
                            solicitudContrato.ObsMantencion = rows["ObsMantencion"].ToString();
                            solicitudContrato.EjecutivoName = rows["EjecutivoName"].ToString();
                            solicitudContrato.IdCliente = rows["IdCliente"].ToString();
                            solicitudContrato.HorasTrab = rows["HorasTrab"].ToString();
                            solicitudContrato.HorariosTrab = rows["HorariosTrab"].ToString();
                            solicitudContrato.Visa = rows["Visa"].ToString();
                            solicitudContrato.VencVisa = rows["VencVisa"].ToString();
                            solicitudContrato.Colacion = rows["Colacion"].ToString();
                            solicitudContrato.CargoReal = rows["CargoReal"].ToString();
                            solicitudContrato.Reemplazo = rows["Reemplazo"].ToString();
                            solicitudContrato.DescripcionFunciones = rows["DescripcionFunciones"].ToString();
                            solicitudContrato.RenovacionAutomatica = rows["RenovacionAutomatica"].ToString();
                            solicitudContrato.Pasaporte = rows["Pasaporte"].ToString();
                            solicitudContrato.Empresa = rows["Empresa"].ToString();

                            solicitudContratos.Add(solicitudContrato);

                        }
                        else
                        {

                        }
                    }

                    ViewBag.RenderizadoSolicitudContrato = solicitudContratos;


                    ViewBag.RenderizaSolicitudData = true;
                }

                if (section == "Renovacion" && schema != "Solicitud" && schema != "")
                {
                    ViewBag.RenderizaDatosSolicitudType = "Renovacion";

                    string[] headerParamSolictudC =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@CODIGOSOLICITUD",
                        "@TIPOSOLICITUD"
                    };

                    string[] headerValoresSolicitudC =
                    {
                        tokenAuth,
                        agenteAplication,
                        Session["NombreUsuario"].ToString(),
                        schema,
                        "U29saWNpdHVkUmVub3ZhY2lvbg=="
                    };

                    DataSet dataSolicitudContrato = servicioOperaciones.GetObtenerDatosSolicitud(headerParamSolictudC, headerValoresSolicitudC).Table;

                    foreach (DataRow rows in dataSolicitudContrato.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            Models.SolicitudRenovacion solicitudRenovacion = new Models.SolicitudRenovacion();

                            solicitudRenovacion.Code = rows["Code"].ToString();
                            solicitudRenovacion.Message = rows["Message"].ToString();
                            solicitudRenovacion.NombreSolicitud = rows["NombreSolicitud"].ToString();
                            solicitudRenovacion.Comentarios = rows["Comentarios"].ToString();
                            solicitudRenovacion.Glyphicon = rows["Glyphicon"].ToString();
                            solicitudRenovacion.GlyphiconColor = rows["GlyphiconColor"].ToString();
                            solicitudRenovacion.ColorFont = rows["ColorFont"].ToString();
                            solicitudRenovacion.NombreProceso = rows["NombreProceso"].ToString();
                            solicitudRenovacion.Creado = rows["Creado"].ToString();
                            solicitudRenovacion.EjecutivoCarga = rows["EjecutivoCarga"].ToString();


                            solicitudRenovacion.Ficha = rows["Ficha"].ToString();
                            solicitudRenovacion.Rut = rows["Rut"].ToString();
                            solicitudRenovacion.NombreCompleto = rows["NombreCompleto"].ToString();
                            solicitudRenovacion.CargoMod = rows["CargoMod"].ToString();
                            solicitudRenovacion.FechaInicio = rows["FechaInicio"].ToString();
                            solicitudRenovacion.FechaTermino = rows["FechaTermino"].ToString();
                            solicitudRenovacion.Causal = rows["Causal"].ToString();
                            solicitudRenovacion.FechaInicioRenov = rows["FechaInicioRenov"].ToString();
                            solicitudRenovacion.FechaTerminoRenov = rows["FechaTerminoRenov"].ToString();
                            solicitudRenovacion.Empresa = rows["Empresa"].ToString();


                            solicitudRenovaciones.Add(solicitudRenovacion);

                        }
                    }

                    ViewBag.RenderizadoSolicitudRenovacion = solicitudRenovaciones;

                    ViewBag.RenderizaSolicitudData = true;
                }

                #endregion

                #region "MOTIVOS DE ANULACION RENDERIZADO"

                List<Models.MotivosAnulacion> motivos = new List<Models.MotivosAnulacion>();

                string[] paramMotivosAnulacion =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP"
                };

                string[] valMotivosAnulacion =
                {
                    tokenAuth,
                    agenteAplication
                };

                DataSet dataMotivosAnulacion = servicioOperaciones.GetObtenerMotivosAnulacion(paramMotivosAnulacion, valMotivosAnulacion).Table;

                foreach (DataRow rows in dataMotivosAnulacion.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Models.MotivosAnulacion motivo = new Models.MotivosAnulacion();

                        motivo.Code = rows["Code"].ToString();
                        motivo.Message = rows["Message"].ToString();
                        motivo.Descripcion = rows["Descripcion"].ToString();

                        motivos.Add(motivo);
                    }
                }

                ViewBag.RenderizadoMotivosAnulacion = motivos;

                #endregion

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

            return View();
        }

        #endregion

        #region "Nueva seccion de procesos y solicitudes"

        //public ActionResult Procesos()
        //{
        //    try
        //    {

        //        #region "PERMISOS Y ACCESOS"

        //        if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
        //        {
        //            Session["ApplicationActive"] = null;
        //        }

        //        if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
        //        {
        //            sistema = Request.Url.AbsoluteUri.Split('/')[3];
        //            schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

        //            if (schema != "Contratos" && schema != "Renovaciones" && schema != "Procesos")
        //            {
        //                section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
        //            }
        //        }
        //        else
        //        {
        //            sistema = fromDebugQA;
        //            schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

        //            if (schema != "Contratos" && schema != "Renovaciones" && schema != "Procesos")
        //            {
        //                section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
        //            }
        //        }

        //        /** APLICACION DE HEADERS */

        //        string[] parametrosHeadersAccess =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP",
        //            "@CODIFICARAUTH",
        //            "@SISTEMA",
        //            "@TRABAJADOR"
        //        };

        //        string[] valoresHeadersAccess =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            Session["CodificarAuth"].ToString(),
        //            sistema,
        //            Session["NombreUsuario"].ToString()
        //        };

        //        /** END APLICACION DE HEADERS */

        //        DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
        //        List<Models.Accesos> accesos = new List<Models.Accesos>();

        //        foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
        //        {
        //            Models.Accesos acceso = new Models.Accesos();

        //            acceso.Code = rowsPermisos["Code"].ToString();
        //            acceso.Message = rowsPermisos["Message"].ToString();
        //            acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
        //            acceso.PreController = rowsPermisos["PreController"].ToString();
        //            acceso.Controller = rowsPermisos["Controllers"].ToString();
        //            acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
        //            acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

        //            accesos.Add(acceso);
        //        }

        //        Session["RenderizadoPermisos"] = accesos;

        //        #endregion

        //        List<Models.Proceso> procesosC = new List<Models.Proceso>();
        //        List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
        //        List<Models.Proceso> procesosR = new List<Models.Proceso>();
        //        List<Models.Pagination> paginatorsR = new List<Models.Pagination>();
        //        List<Models.Solicitud> solicitudR = new List<Models.Solicitud>();
        //        List<Models.Solicitud> solicitudC = new List<Models.Solicitud>();

        //        List<Models.Prioridades> prioridades = new List<Models.Prioridades>();

        //        List<Models.HeaderProcesos> headerProcesosC = new List<Models.HeaderProcesos>();
        //        List<Models.HeaderProcesos> headerProcesosR = new List<Models.HeaderProcesos>();

        //        List<Models.HeaderSolicitud> headerSolicitudC = new List<Models.HeaderSolicitud>();
        //        List<Models.HeaderSolicitud> headerSolicitudR = new List<Models.HeaderSolicitud>();

        //        ViewBag.RenderizaContratos = false;
        //        ViewBag.RenderizaRenovaciones = false;
        //        ViewBag.RenderizaSolicitud = false;
        //        ViewBag.RenderizaContratoSolicitud = false;
        //        ViewBag.RenderizaRenovacionSolicitud = false;

        //        switch (schema)
        //        {
        //            case "Procesos":
        //                schema = "Procesos";
        //                section = "";
        //                break;
        //            case "Renovaciones":
        //                schema = "Renovaciones";
        //                section = "";
        //                break;
        //            case "Contratos":
        //                schema = "Contratos";
        //                section = "";
        //                break;
        //            default:
        //                ViewBag.RenderizaSolicitud = true;
        //                break;
        //        }


        //        #region "HEADER PROCESOS RENDERIZADOS"

        //        #region "HEADER PROCESOS CONTRATOS"

        //        string[] paramHeaderProcC =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP",
        //            "@TRABAJADOR",
        //            "@TIPOSOLICITUD",
        //            "@PAGINATION",
        //            "@TYPEFILTER",
        //            "@DATAFILTER"
        //        };

        //        string[] valHeaderProcC =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            Session["NombreUsuario"].ToString(),
        //            "U29saWNpdHVkQ29udHJhdG8=",
        //            "MS01",
        //            "",
        //            ""
        //        };

        //        DataSet dataHeaderProcesosC = servicioOperaciones.GetObtenerHeaderProcesos(paramHeaderProcC, valHeaderProcC).Table;

        //        foreach (DataRow item in dataHeaderProcesosC.Tables[0].Rows)
        //        {
        //            Models.HeaderProcesos header = new Models.HeaderProcesos();

        //            header.PostTitulo = item["PostTitulo"].ToString();
        //            header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //            header.HtmlPagination = item["HtmlPagination"].ToString();
        //            header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //            header.TipoSolicitud = item["TipoSolicitud"].ToString();
        //            header.ResultSearch = item["ResultSearch"].ToString();

        //            headerProcesosC.Add(header);
        //        }

        //        ViewBag.RenderizadoHeaderProcesosContratos = headerProcesosC;

        //        #endregion

        //        #region "HEADER PROCESOS RENOVACIONES"

        //        string[] paramHeaderProcR =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP",
        //            "@TRABAJADOR",
        //            "@TIPOSOLICITUD",
        //            "@PAGINATION",
        //            "@TYPEFILTER",
        //            "@DATAFILTER"
        //        };

        //        string[] valHeaderProcR =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            Session["NombreUsuario"].ToString(),
        //            "U29saWNpdHVkUmVub3ZhY2lvbg==",
        //            "MS01",
        //            "",
        //            ""
        //        };

        //        DataSet dataHeaderProcesosR = servicioOperaciones.GetObtenerHeaderProcesos(paramHeaderProcR, valHeaderProcR).Table;

        //        foreach (DataRow item in dataHeaderProcesosR.Tables[0].Rows)
        //        {
        //            Models.HeaderProcesos header = new Models.HeaderProcesos();

        //            header.PostTitulo = item["PostTitulo"].ToString();
        //            header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //            header.HtmlPagination = item["HtmlPagination"].ToString();
        //            header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //            header.TipoSolicitud = item["TipoSolicitud"].ToString();
        //            header.ResultSearch = item["ResultSearch"].ToString();

        //            headerProcesosR.Add(header);
        //        }

        //        ViewBag.RenderizadoHeaderProcesosRenovaciones = headerProcesosR;

        //        #endregion

        //        #endregion

        //        #region "HEADER SOLICITUDES RENDERIZADO"

        //        #region "HEADER SOLICITUDES CONTRATOS"

        //        if (section == "Contratos" && schema != "Procesos" && schema != "")
        //        {
        //            string[] paramHeaderSolC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valHeaderSolC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkQ29udHJhdG8=",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataHeaderSolicitudC = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolC, valHeaderSolC).Table;

        //            foreach (DataRow item in dataHeaderSolicitudC.Tables[0].Rows)
        //            {
        //                Models.HeaderSolicitud header = new Models.HeaderSolicitud();

        //                header.PostTitulo = item["PostTitulo"].ToString();
        //                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //                header.HtmlPagination = item["HtmlPagination"].ToString();
        //                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //                header.TipoSolicitud = item["TipoSolicitud"].ToString();

        //                header.Creado = item["Creado"].ToString();
        //                header.Estado = item["Estado"].ToString();
        //                header.Glyphicon = item["Glyphicon"].ToString();
        //                header.GlyphiconColor = item["GlyphiconColor"].ToString();
        //                header.EjecutivoKam = item["EjecutivoKam"].ToString();
        //                header.EjecutivoCreador = item["EjecutivoCreador"].ToString();
        //                header.Cliente = item["Cliente"].ToString();

        //                header.NombreProceso = item["NombreProceso"].ToString();
        //                header.ResultSearch = item["ResultSearch"].ToString();

        //                headerSolicitudC.Add(header);
        //            }

        //            ViewBag.RenderizadoHeaderSolicitudContratos = headerSolicitudC;
        //        }

        //        #endregion

        //        #region "HEADER SOLICITUDES RENOVACIONES"

        //        if (section == "Renovaciones" && schema != "Procesos" && schema != "")
        //        {
        //            string[] paramHeaderSolR =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valHeaderSolR =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkUmVub3ZhY2lvbg==",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataHeaderSolicitudR = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolR, valHeaderSolR).Table;

        //            foreach (DataRow item in dataHeaderSolicitudR.Tables[0].Rows)
        //            {
        //                Models.HeaderSolicitud header = new Models.HeaderSolicitud();

        //                header.PostTitulo = item["PostTitulo"].ToString();
        //                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //                header.HtmlPagination = item["HtmlPagination"].ToString();
        //                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //                header.TipoSolicitud = item["TipoSolicitud"].ToString();

        //                header.Creado = item["Creado"].ToString();
        //                header.Estado = item["Estado"].ToString();
        //                header.Glyphicon = item["Glyphicon"].ToString();
        //                header.GlyphiconColor = item["GlyphiconColor"].ToString();
        //                header.EjecutivoKam = item["EjecutivoKam"].ToString();
        //                header.EjecutivoCreador = item["EjecutivoCreador"].ToString();
        //                header.Cliente = item["Cliente"].ToString();

        //                header.NombreProceso = item["NombreProceso"].ToString();
        //                header.ResultSearch = item["ResultSearch"].ToString();

        //                headerSolicitudR.Add(header);
        //            }

        //            ViewBag.RenderizadoHeaderSolicitudRenovaciones = headerSolicitudR;
        //        }

        //        #endregion

        //        #endregion

        //        #region "PROCESO RENDERIZADO"

        //        if (section == "" && schema == "Contratos" || schema == "Procesos")
        //        {
        //            /** PROCESOS SOLICITUD CONTRATOS */

        //            ViewBag.RenderizadoProcesosContratos = ModuleProcesos("", "U29saWNpdHVkQ29udHJhdG8=");

        //            /** PAGINATOR */

        //            ViewBag.RenderizadoPaginationsProcesosContratos = ModulePagination("ProcesosContratos", "MS01", "N");
        //            ViewBag.RenderizaContratos = true;

        //            /** END PROCESO SOLICITUD CONTRATOS */
        //        }

        //        if (section == "" && schema == "Renovaciones" || schema == "Procesos")
        //        {

        //            ViewBag.RenderizadoProcesosRenovaciones = ModuleProcesos("", "U29saWNpdHVkUmVub3ZhY2lvbg==");
        //            /** PAGINATOR */

        //            ViewBag.RenderizadoPaginationsProcesosRenovaciones = ModulePagination("ProcesosRenovaciones", "MS01", "N");
        //            ViewBag.RenderizaRenovaciones = true;
        //            /** PROCESOS SOLICITUD RENOVACION */



        //            /** END PROCESO SOLICITUD RENOVACION */
        //        }

        //        #endregion

        //        #region "PRIORIDADES ESTADOS"

        //        string[] paramPrioridades =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP" ,
        //            "@PRIORIDADESADO",
        //            "@DATA"
        //        };

        //        string[] valPrioridades =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            "UHJpb3JpZGFk",
        //            "ContratoRenovacion"
        //        };

        //        DataSet dataPrioridades = servicioOperaciones.GetPrioridadesEstado(paramPrioridades, valPrioridades).Table;

        //        foreach (DataRow rows in dataPrioridades.Tables[0].Rows)
        //        {
        //            if (rows["Code"].ToString() == "200")
        //            {
        //                Models.Prioridades prioridad = new Models.Prioridades();

        //                prioridad.Code = rows["Code"].ToString();
        //                prioridad.Message = rows["Message"].ToString();
        //                prioridad.Nombre = rows["Nombre"].ToString();
        //                prioridad.Glyphicon = rows["Glyphicon"].ToString();
        //                prioridad.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                prioridad.BorderColor = rows["BorderColor"].ToString();
        //                prioridad.ColorFont = rows["ColorFont"].ToString();

        //                prioridades.Add(prioridad);
        //            }
        //        }

        //        ViewBag.RenderizadoPrioridades = prioridades;

        //        #endregion

        //        #region "SOLICITUD RENDERIZADO"

        //        if (section == "Contratos" && schema != "Procesos" && schema != "")
        //        {

        //            /** PROCESOS SOLICITUD CONTRATO */

        //            string[] headerParamSolictudC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] headerValoresSolicitudC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkQ29udHJhdG8=",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataSolicitudesC = servicioOperaciones.GetObtenerSolicitudes(headerParamSolictudC, headerValoresSolicitudC).Table;

        //            foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
        //            {
        //                if (rows["Code"].ToString() == "200")
        //                {
        //                    Models.Solicitud solicitud = new Models.Solicitud();

        //                    solicitud.Code = rows["Code"].ToString();
        //                    solicitud.Message = rows["Message"].ToString();
        //                    solicitud.TipoEvento = rows["TipoEvento"].ToString();
        //                    solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
        //                    solicitud.NombreProceso = rows["NombreProceso"].ToString();
        //                    solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();
        //                    solicitud.Creado = rows["Creado"].ToString();
        //                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
        //                    solicitud.Comentarios = rows["Comentarios"].ToString();
        //                    solicitud.CodificarCod = rows["CodificarCod"].ToString();

        //                    solicitud.Prioridad = rows["Prioridad"].ToString();
        //                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
        //                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                    solicitud.BorderColor = rows["BorderColor"].ToString();
        //                    solicitud.ColorFont = rows["ColorFont"].ToString();

        //                    solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

        //                    solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
        //                    solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
        //                    solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
        //                    solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
        //                    solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
        //                    solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
        //                    solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

        //                    solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

        //                    solicitudC.Add(solicitud);
        //                }
        //            }

        //            ViewBag.RenderizadoSolicitudContratos = solicitudC;

        //            /** PAGINATOR */

        //            string[] paramPaginatorSolicitudC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valPaginatorSolicitudC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkQ29udHJhdG8=",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataPaginadoC = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudC, valPaginatorSolicitudC).Table;

        //            foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
        //            {
        //                if (dataRowPaginadoC["Code"].ToString() == "200")
        //                {
        //                    Models.Pagination paginado = new Models.Pagination();

        //                    paginado.Code = dataRowPaginadoC["Code"].ToString();
        //                    paginado.Message = dataRowPaginadoC["Message"].ToString();
        //                    paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
        //                    paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
        //                    paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
        //                    paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
        //                    paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
        //                    paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
        //                    paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
        //                    paginado.Active = dataRowPaginadoC["Activo"].ToString();

        //                    paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
        //                    paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
        //                    paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

        //                    paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();

        //                    paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

        //                    paginatorsC.Add(paginado);
        //                }
        //            }

        //            ViewBag.RenderizadoPagSolicitudesContratos = paginatorsC;

        //            ViewBag.RenderizaContratoSolicitud = true;

        //            /** END PROCESO SOLICITUD CONTRATO */

        //        }

        //        if (section == "Renovaciones" && schema != "Procesos" && schema != "")
        //        {
        //            /** END PROCESO SOLICITUD RENOVACION */

        //            string[] headerParamSolicitudR =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] headerValoresSolicitudR =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkUmVub3ZhY2lvbg==",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataSolicitudesR = servicioOperaciones.GetObtenerSolicitudes(headerParamSolicitudR, headerValoresSolicitudR).Table;

        //            foreach (DataRow rows in dataSolicitudesR.Tables[0].Rows)
        //            {
        //                if (rows["Code"].ToString() == "200")
        //                {
        //                    Models.Solicitud solicitud = new Models.Solicitud();

        //                    solicitud.Code = rows["Code"].ToString();
        //                    solicitud.Message = rows["Message"].ToString();
        //                    solicitud.TipoEvento = rows["TipoEvento"].ToString();
        //                    solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
        //                    solicitud.NombreProceso = rows["NombreProceso"].ToString();

        //                    solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();
        //                    solicitud.Creado = rows["Creado"].ToString();
        //                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
        //                    solicitud.Comentarios = rows["Comentarios"].ToString();
        //                    solicitud.CodificarCod = rows["CodificarCod"].ToString();

        //                    solicitud.Prioridad = rows["Prioridad"].ToString();
        //                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
        //                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                    solicitud.BorderColor = rows["BorderColor"].ToString();
        //                    solicitud.ColorFont = rows["ColorFont"].ToString();

        //                    solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

        //                    solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
        //                    solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
        //                    solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
        //                    solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
        //                    solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
        //                    solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
        //                    solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

        //                    solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

        //                    solicitudR.Add(solicitud);
        //                }
        //            }

        //            ViewBag.RenderizadoSolicitudRenovaciones = solicitudR;

        //            /** PAGINATOR */

        //            string[] paramPaginatorSolicitudR =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valPaginatorSolicitudR =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkUmVub3ZhY2lvbg==",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataPaginadoR = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitudR, valPaginatorSolicitudR).Table;

        //            foreach (DataRow dataRowPaginadoR in dataPaginadoR.Tables[0].Rows)
        //            {
        //                if (dataRowPaginadoR["Code"].ToString() == "200")
        //                {
        //                    Models.Pagination paginado = new Models.Pagination();

        //                    paginado.Code = dataRowPaginadoR["Code"].ToString();
        //                    paginado.Message = dataRowPaginadoR["Message"].ToString();
        //                    paginado.FirstItem = dataRowPaginadoR["FirstItem"].ToString();
        //                    paginado.PreviousItem = dataRowPaginadoR["PreviousItem"].ToString();
        //                    paginado.Codificar = dataRowPaginadoR["Codificar"].ToString();
        //                    paginado.NumeroPagina = dataRowPaginadoR["NumeroPagina"].ToString();
        //                    paginado.NextItem = dataRowPaginadoR["NextItem"].ToString();
        //                    paginado.LastItem = dataRowPaginadoR["LastItem"].ToString();
        //                    paginado.TotalItems = dataRowPaginadoR["TotalItems"].ToString();
        //                    paginado.Active = dataRowPaginadoR["Activo"].ToString();

        //                    paginado.TipoSolicitud = dataRowPaginadoR["TipoSolicitud"].ToString();
        //                    paginado.HtmlRenderizado = dataRowPaginadoR["HtmlRenderizado"].ToString();
        //                    paginado.HtmlPagination = dataRowPaginadoR["HtmlPagination"].ToString();

        //                    paginado.HtmlSearchElement = dataRowPaginadoR["HtmlSearchElement"].ToString();
        //                    paginado.HtmlEventAction = dataRowPaginadoR["HtmlEventAction"].ToString();

        //                    paginatorsR.Add(paginado);
        //                }
        //            }

        //            ViewBag.RenderizadoPagSolicitudesRenovaciones = paginatorsR;

        //            ViewBag.RenderizaRenovacionSolicitud = true;

        //            /** END PROCESO SOLICITUD RENOVACION */

        //        }

        //        #endregion

        //        #region "MOTIVOS DE ANULACION RENDERIZADO"

        //        List<Models.MotivosAnulacion> motivos = new List<Models.MotivosAnulacion>();

        //        string[] paramMotivosAnulacion =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP"
        //        };

        //        string[] valMotivosAnulacion =
        //        {
        //            tokenAuth,
        //            agenteAplication
        //        };

        //        DataSet dataMotivosAnulacion = servicioOperaciones.GetObtenerMotivosAnulacion(paramMotivosAnulacion, valMotivosAnulacion).Table;

        //        foreach (DataRow rows in dataMotivosAnulacion.Tables[0].Rows)
        //        {
        //            if (rows["Code"].ToString() == "200")
        //            {
        //                Models.MotivosAnulacion motivo = new Models.MotivosAnulacion();

        //                motivo.Code = rows["Code"].ToString();
        //                motivo.Message = rows["Message"].ToString();
        //                motivo.Descripcion = rows["Descripcion"].ToString();

        //                motivos.Add(motivo);
        //            }
        //        }

        //        ViewBag.RenderizadoMotivosAnulacion = motivos;

        //        #endregion

        //        ViewBag.CodeCorrect = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        List<Models.Error> errores = new List<Models.Error>();
        //        ViewBag.CodeCorrect = false;
        //        Models.Error error = new Models.Error();

        //        error.Code = "500.*";
        //        error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

        //        errores.Add(error);

        //        ViewBag.Error = errores;
        //    }

        //    return View();
        //}

        //public ActionResult Solicitudes()
        //{
        //    try
        //    {

        //        #region "PERMISOS Y ACCESOS"

        //        if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
        //        {
        //            Session["ApplicationActive"] = null;
        //        }

        //        string sistema = "";

        //        if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
        //        {
        //            sistema = Request.Url.AbsoluteUri.Split('/')[3];
        //            schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

        //            if (schema != "Contrato" && schema != "Renovacion" && schema != "Solicitudes")
        //            {
        //                section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
        //            }
        //        }
        //        else
        //        {
        //            sistema = fromDebugQA;
        //            schema = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

        //            if (schema != "Contrato" && schema != "Renovacion" && schema != "Solicitudes")
        //            {
        //                section = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
        //            }
        //        }

        //        /** APLICACION DE HEADERS */

        //        string[] parametrosHeadersAccess =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP",
        //            "@CODIFICARAUTH",
        //            "@SISTEMA",
        //            "@TRABAJADOR"
        //        };

        //        string[] valoresHeadersAccess =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            Session["CodificarAuth"].ToString(),
        //            sistema,
        //            Session["NombreUsuario"].ToString()
        //        };

        //        /** END APLICACION DE HEADERS */

        //        DataSet dataPermisos = servicioAuth.GetPemisionSectionsAccess(parametrosHeadersAccess, valoresHeadersAccess).Table;
        //        List<Models.Accesos> accesos = new List<Models.Accesos>();

        //        foreach (DataRow rowsPermisos in dataPermisos.Tables[0].Rows)
        //        {
        //            Models.Accesos acceso = new Models.Accesos();

        //            acceso.Code = rowsPermisos["Code"].ToString();
        //            acceso.Message = rowsPermisos["Message"].ToString();
        //            acceso.CodeAction = rowsPermisos["CodeAction"].ToString();
        //            acceso.PreController = rowsPermisos["PreController"].ToString();
        //            acceso.Controller = rowsPermisos["Controllers"].ToString();
        //            acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
        //            acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

        //            accesos.Add(acceso);
        //        }

        //        Session["RenderizadoPermisos"] = accesos;

        //        #endregion

        //        ViewBag.HTMLLoaderError = "";

        //        List<Models.Solicitud> solicitudR = new List<Models.Solicitud>();
        //        List<Models.Pagination> paginatorsR = new List<Models.Pagination>();
        //        List<Models.Solicitud> solicitudC = new List<Models.Solicitud>();
        //        List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
        //        List<Models.SolicitudContrato> solicitudContratos = new List<Models.SolicitudContrato>();
        //        List<Models.SolicitudRenovacion> solicitudRenovaciones = new List<Models.SolicitudRenovacion>();
        //        List<Models.Prioridades> prioridades = new List<Models.Prioridades>();

        //        List<Models.HeaderSolicitud> headerSolicitudC = new List<Models.HeaderSolicitud>();
        //        List<Models.HeaderSolicitud> headerSolicitudR = new List<Models.HeaderSolicitud>();

        //        ViewBag.RenderizaContratos = false;
        //        ViewBag.RenderizaRenovaciones = false;
        //        ViewBag.RenderizaSolicitudData = false;
        //        ViewBag.RenderizaContratoSolicitud = false;
        //        ViewBag.RenderizaRenovacionSolicitud = false;
        //        ViewBag.RenderizaSolicitud = false;

        //        switch (schema)
        //        {
        //            case "Procesos":
        //                schema = "Solicitudes";
        //                section = "";
        //                break;
        //            case "Renovacion":
        //                schema = "Renovacion";
        //                section = "";
        //                break;
        //            case "Contrato":
        //                schema = "Contrato";
        //                section = "";
        //                break;
        //        }

        //        #region "HEADER SOLICITUDES RENDERIZADO"

        //        #region "HEADER SOLICITUDES CONTRATOS"

        //        if (section == "" && schema != "Contrato" || schema != "Solicitudes")
        //        {
        //            string[] paramHeaderSolC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valHeaderSolC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                "",
        //                "U29saWNpdHVkQ29udHJhdG8=",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataHeaderSolicitudC = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolC, valHeaderSolC).Table;

        //            foreach (DataRow item in dataHeaderSolicitudC.Tables[0].Rows)
        //            {
        //                Models.HeaderSolicitud header = new Models.HeaderSolicitud();

        //                header.PostTitulo = item["PostTitulo"].ToString();
        //                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //                header.HtmlPagination = item["HtmlPagination"].ToString();
        //                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //                header.TipoSolicitud = item["TipoSolicitud"].ToString();

        //                header.Creado = item["Creado"].ToString();
        //                header.Estado = item["Estado"].ToString();
        //                header.Glyphicon = item["Glyphicon"].ToString();
        //                header.GlyphiconColor = item["GlyphiconColor"].ToString();
        //                header.EjecutivoKam = item["EjecutivoKam"].ToString();
        //                header.EjecutivoCreador = item["EjecutivoCreador"].ToString();
        //                header.Cliente = item["Cliente"].ToString();

        //                header.NombreProceso = item["NombreProceso"].ToString();
        //                header.ResultSearch = item["ResultSearch"].ToString();

        //                headerSolicitudC.Add(header);
        //            }

        //            ViewBag.RenderizadoHeaderSolicitudContratos = headerSolicitudC;
        //        }

        //        #endregion

        //        #region "HEADER SOLICITUDES RENOVACIONES"

        //        if (section == "" && schema != "Renovacion" || schema != "Solicitudes")
        //        {
        //            string[] paramHeaderSolR =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOTRANSACCION",
        //                "@TIPOSOLICITUD",
        //                "@PAGINATION",
        //                "@TYPEFILTER",
        //                "@DATAFILTER"
        //            };

        //            string[] valHeaderSolR =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                "",
        //                "U29saWNpdHVkUmVub3ZhY2lvbg==",
        //                "MS01",
        //                "",
        //                ""
        //            };

        //            DataSet dataHeaderSolicitudR = servicioOperaciones.GetObtenerHeaderSolicitudes(paramHeaderSolR, valHeaderSolR).Table;

        //            foreach (DataRow item in dataHeaderSolicitudR.Tables[0].Rows)
        //            {
        //                Models.HeaderSolicitud header = new Models.HeaderSolicitud();

        //                header.PostTitulo = item["PostTitulo"].ToString();
        //                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
        //                header.HtmlPagination = item["HtmlPagination"].ToString();
        //                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
        //                header.TipoSolicitud = item["TipoSolicitud"].ToString();

        //                header.Creado = item["Creado"].ToString();
        //                header.Estado = item["Estado"].ToString();
        //                header.Glyphicon = item["Glyphicon"].ToString();
        //                header.GlyphiconColor = item["GlyphiconColor"].ToString();
        //                header.EjecutivoKam = item["EjecutivoKam"].ToString();
        //                header.EjecutivoCreador = item["EjecutivoCreador"].ToString();
        //                header.Cliente = item["Cliente"].ToString();

        //                header.NombreProceso = item["NombreProceso"].ToString();
        //                header.ResultSearch = item["ResultSearch"].ToString();

        //                headerSolicitudR.Add(header);
        //            }

        //            ViewBag.RenderizadoHeaderSolicitudRenovaciones = headerSolicitudR;
        //        }

        //        #endregion

        //        #endregion

        //        #region "RENDERIZADO SOLICITUDES"

        //        if (section == "" && schema == "Contrato" || schema == "Solicitudes")
        //        {
        //            /** PROCESOS SOLICITUD CONTRATOS */

        //            ViewBag.RenderizadoSolicitudContratos = ModuleSolicitud("", "U29saWNpdHVkQ29udHJhdG8=");

        //            /** PAGINATOR */

        //            ViewBag.RenderizadoPaginationsSolicitudContratos = ModulePagination("SolicitudContratos", "MS01", "N", "", "");

        //        }

        //        if (section == "" && schema == "Renovacion" || schema == "Solicitudes")
        //        {
        //            /** PROCESOS SOLICITUD CONTRATOS */

        //            ViewBag.RenderizadoSolicitudRenovaciones = ModuleSolicitud("", "U29saWNpdHVkUmVub3ZhY2lvbg==");

        //            /** PAGINATOR */

        //            ViewBag.RenderizadoPaginationsSolicitudRenovaciones = ModulePagination("SolicitudRenovaciones", "MS01", "N", "", "");

        //        }

        //        #endregion

        //        #region "PRIORIDADES ESTADOS"

        //        string[] paramPrioridades =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP" ,
        //            "@PRIORIDADESADO",
        //            "@DATA"
        //        };

        //        string[] valPrioridades =
        //        {
        //            tokenAuth,
        //            agenteAplication,
        //            "UHJpb3JpZGFk",
        //            "ContratoRenovacion"
        //        };

        //        DataSet dataPrioridades = servicioOperaciones.GetPrioridadesEstado(paramPrioridades, valPrioridades).Table;

        //        foreach (DataRow rows in dataPrioridades.Tables[0].Rows)
        //        {
        //            if (rows["Code"].ToString() == "200")
        //            {
        //                Models.Prioridades prioridad = new Models.Prioridades();

        //                prioridad.Code = rows["Code"].ToString();
        //                prioridad.Message = rows["Message"].ToString();
        //                prioridad.Nombre = rows["Nombre"].ToString();
        //                prioridad.Glyphicon = rows["Glyphicon"].ToString();
        //                prioridad.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                prioridad.BorderColor = rows["BorderColor"].ToString();
        //                prioridad.ColorFont = rows["ColorFont"].ToString();

        //                prioridades.Add(prioridad);
        //            }
        //        }

        //        ViewBag.RenderizadoPrioridades = prioridades;

        //        #endregion

        //        #region "RENDERIZA DATOS SOLICITUD"

        //        if (section == "Contrato" && schema != "Solicitud" && schema != "")
        //        {
        //            ViewBag.RenderizaDatosSolicitudType = "Contratos";

        //            string[] headerParamSolictudC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOSOLICITUD",
        //                "@TIPOSOLICITUD"
        //            };

        //            string[] headerValoresSolicitudC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkQ29udHJhdG8="
        //            };

        //            DataSet dataSolicitudContrato = servicioOperaciones.GetObtenerDatosSolicitud(headerParamSolictudC, headerValoresSolicitudC).Table;

        //            foreach (DataRow rows in dataSolicitudContrato.Tables[0].Rows)
        //            {
        //                if (rows["Code"].ToString() == "200")
        //                {
        //                    Models.SolicitudContrato solicitudContrato = new Models.SolicitudContrato();

        //                    solicitudContrato.Code = rows["Code"].ToString();
        //                    solicitudContrato.Message = rows["Message"].ToString();
        //                    solicitudContrato.NombreSolicitud = rows["NombreSolicitud"].ToString();
        //                    solicitudContrato.Comentarios = rows["Comentarios"].ToString();
        //                    solicitudContrato.Glyphicon = rows["Glyphicon"].ToString();
        //                    solicitudContrato.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                    solicitudContrato.ColorFont = rows["ColorFont"].ToString();
        //                    solicitudContrato.NombreProceso = rows["NombreProceso"].ToString();
        //                    solicitudContrato.Creado = rows["Creado"].ToString();
        //                    solicitudContrato.EjecutivoCarga = rows["EjecutivoCarga"].ToString();
        //                    solicitudContrato.BusinessCenter = rows["BusinessCenter"].ToString();
        //                    solicitudContrato.AuthStatement = rows["AuthStatement"].ToString();
        //                    solicitudContrato.FastTrack = rows["FastTrack"].ToString();
        //                    solicitudContrato.Rut = rows["Rut"].ToString();
        //                    solicitudContrato.Nombre = rows["Nombre"].ToString();
        //                    solicitudContrato.NativeSurname = rows["NativeSurname"].ToString();
        //                    solicitudContrato.Surname = rows["Surname"].ToString();
        //                    solicitudContrato.Email = rows["Email"].ToString();
        //                    solicitudContrato.Birthdate = rows["Birthdate"].ToString();
        //                    solicitudContrato.CivilState = rows["CivilState"].ToString();
        //                    solicitudContrato.Gender = rows["Gender"].ToString();
        //                    solicitudContrato.Address = rows["Address"].ToString();
        //                    solicitudContrato.Comuna = rows["Comuna"].ToString();
        //                    solicitudContrato.Ciudad = rows["Ciudad"].ToString();
        //                    solicitudContrato.Country = rows["Country"].ToString();
        //                    solicitudContrato.Phone = rows["Phone"].ToString();
        //                    solicitudContrato.Salud = rows["Salud"].ToString();
        //                    solicitudContrato.MontoIsapre = rows["MontoIsapre"].ToString();
        //                    solicitudContrato.Afp = rows["Afp"].ToString();
        //                    solicitudContrato.PorcentajeAfp = rows["PorcentajeAfp"].ToString();
        //                    solicitudContrato.PorcentajeSeguroMasComisionAfp = rows["PorcentajeSeguroMasComisionAfp"].ToString();
        //                    solicitudContrato.Bank = rows["Bank"].ToString();
        //                    solicitudContrato.BankAccount = rows["BankAccount"].ToString();
        //                    solicitudContrato.AccountType = rows["AccountType"].ToString();
        //                    solicitudContrato.Profesion = rows["Profesion"].ToString();
        //                    solicitudContrato.StartDate = rows["StartDate"].ToString();
        //                    solicitudContrato.CodCaja = rows["CodCaja"].ToString();
        //                    solicitudContrato.CodEstudios = rows["CodEstudios"].ToString();
        //                    solicitudContrato.CodCentroCosto = rows["CodCentroCosto"].ToString();
        //                    solicitudContrato.CodCargo = rows["CodCargo"].ToString();
        //                    solicitudContrato.NHijos = rows["NHijos"].ToString();
        //                    solicitudContrato.UltimoEmpleador = rows["UltimoEmpleador"].ToString();
        //                    solicitudContrato.TallaPantalon = rows["TallaPantalon"].ToString();
        //                    solicitudContrato.TallaPolera = rows["TallaPolera"].ToString();
        //                    solicitudContrato.TallaZapatos = rows["TallaZapatos"].ToString();
        //                    solicitudContrato.Observaciones = rows["Observaciones"].ToString();
        //                    solicitudContrato.Categoria = rows["Categoria"].ToString();
        //                    solicitudContrato.TipoContrato = rows["TipoContrato"].ToString();
        //                    solicitudContrato.TipoMov = rows["TipoMov"].ToString();
        //                    solicitudContrato.Causal = rows["Causal"].ToString();
        //                    solicitudContrato.ObsCausal = rows["ObsCausal"].ToString();
        //                    solicitudContrato.FinalDate = rows["FinalDate"].ToString();
        //                    solicitudContrato.DatePrimPlazoInicio = rows["DatePrimPlazoInicio"].ToString();
        //                    solicitudContrato.DatePrimPlazoTermino = rows["DatePrimPlazoTermino"].ToString();
        //                    solicitudContrato.DateSegPlazoInicio = rows["DateSegPlazoInicio"].ToString();
        //                    solicitudContrato.DateSegPlazoTermino = rows["DateSegPlazoTermino"].ToString();
        //                    solicitudContrato.ObsMantencion = rows["ObsMantencion"].ToString();
        //                    solicitudContrato.EjecutivoName = rows["EjecutivoName"].ToString();
        //                    solicitudContrato.IdCliente = rows["IdCliente"].ToString();
        //                    solicitudContrato.HorasTrab = rows["HorasTrab"].ToString();
        //                    solicitudContrato.HorariosTrab = rows["HorariosTrab"].ToString();
        //                    solicitudContrato.Visa = rows["Visa"].ToString();
        //                    solicitudContrato.VencVisa = rows["VencVisa"].ToString();
        //                    solicitudContrato.Colacion = rows["Colacion"].ToString();
        //                    solicitudContrato.CargoReal = rows["CargoReal"].ToString();
        //                    solicitudContrato.Reemplazo = rows["Reemplazo"].ToString();
        //                    solicitudContrato.DescripcionFunciones = rows["DescripcionFunciones"].ToString();
        //                    solicitudContrato.RenovacionAutomatica = rows["RenovacionAutomatica"].ToString();
        //                    solicitudContrato.Pasaporte = rows["Pasaporte"].ToString();
        //                    solicitudContrato.Empresa = rows["Empresa"].ToString();

        //                    solicitudContratos.Add(solicitudContrato);

        //                }
        //                else
        //                {

        //                }
        //            }

        //            ViewBag.RenderizadoSolicitudContrato = solicitudContratos;


        //            ViewBag.RenderizaSolicitudData = true;
        //        }

        //        if (section == "Renovacion" && schema != "Solicitud" && schema != "")
        //        {
        //            ViewBag.RenderizaDatosSolicitudType = "Renovacion";

        //            string[] headerParamSolictudC =
        //            {
        //                "@AUTHENTICATE",
        //                "@AGENTAPP",
        //                "@TRABAJADOR",
        //                "@CODIGOSOLICITUD",
        //                "@TIPOSOLICITUD"
        //            };

        //            string[] headerValoresSolicitudC =
        //            {
        //                tokenAuth,
        //                agenteAplication,
        //                Session["NombreUsuario"].ToString(),
        //                schema,
        //                "U29saWNpdHVkUmVub3ZhY2lvbg=="
        //            };

        //            DataSet dataSolicitudContrato = servicioOperaciones.GetObtenerDatosSolicitud(headerParamSolictudC, headerValoresSolicitudC).Table;

        //            foreach (DataRow rows in dataSolicitudContrato.Tables[0].Rows)
        //            {
        //                if (rows["Code"].ToString() == "200")
        //                {
        //                    Models.SolicitudRenovacion solicitudRenovacion = new Models.SolicitudRenovacion();

        //                    solicitudRenovacion.Code = rows["Code"].ToString();
        //                    solicitudRenovacion.Message = rows["Message"].ToString();
        //                    solicitudRenovacion.NombreSolicitud = rows["NombreSolicitud"].ToString();
        //                    solicitudRenovacion.Comentarios = rows["Comentarios"].ToString();
        //                    solicitudRenovacion.Glyphicon = rows["Glyphicon"].ToString();
        //                    solicitudRenovacion.GlyphiconColor = rows["GlyphiconColor"].ToString();
        //                    solicitudRenovacion.ColorFont = rows["ColorFont"].ToString();
        //                    solicitudRenovacion.NombreProceso = rows["NombreProceso"].ToString();
        //                    solicitudRenovacion.Creado = rows["Creado"].ToString();
        //                    solicitudRenovacion.EjecutivoCarga = rows["EjecutivoCarga"].ToString();


        //                    solicitudRenovacion.Ficha = rows["Ficha"].ToString();
        //                    solicitudRenovacion.Rut = rows["Rut"].ToString();
        //                    solicitudRenovacion.NombreCompleto = rows["NombreCompleto"].ToString();
        //                    solicitudRenovacion.CargoMod = rows["CargoMod"].ToString();
        //                    solicitudRenovacion.FechaInicio = rows["FechaInicio"].ToString();
        //                    solicitudRenovacion.FechaTermino = rows["FechaTermino"].ToString();
        //                    solicitudRenovacion.Causal = rows["Causal"].ToString();
        //                    solicitudRenovacion.FechaInicioRenov = rows["FechaInicioRenov"].ToString();
        //                    solicitudRenovacion.FechaTerminoRenov = rows["FechaTerminoRenov"].ToString();
        //                    solicitudRenovacion.Empresa = rows["Empresa"].ToString();


        //                    solicitudRenovaciones.Add(solicitudRenovacion);

        //                }
        //            }

        //            ViewBag.RenderizadoSolicitudRenovacion = solicitudRenovaciones;

        //            ViewBag.RenderizaSolicitudData = true;
        //        }

        //        #endregion

        //        #region "MOTIVOS DE ANULACION RENDERIZADO"

        //        List<Models.MotivosAnulacion> motivos = new List<Models.MotivosAnulacion>();

        //        string[] paramMotivosAnulacion =
        //        {
        //            "@AUTHENTICATE",
        //            "@AGENTAPP"
        //        };

        //        string[] valMotivosAnulacion =
        //        {
        //            tokenAuth,
        //            agenteAplication
        //        };

        //        DataSet dataMotivosAnulacion = servicioOperaciones.GetObtenerMotivosAnulacion(paramMotivosAnulacion, valMotivosAnulacion).Table;

        //        foreach (DataRow rows in dataMotivosAnulacion.Tables[0].Rows)
        //        {
        //            if (rows["Code"].ToString() == "200")
        //            {
        //                Models.MotivosAnulacion motivo = new Models.MotivosAnulacion();

        //                motivo.Code = rows["Code"].ToString();
        //                motivo.Message = rows["Message"].ToString();
        //                motivo.Descripcion = rows["Descripcion"].ToString();

        //                motivos.Add(motivo);
        //            }
        //        }

        //        ViewBag.RenderizadoMotivosAnulacion = motivos;

        //        #endregion

        //        ViewBag.CodeCorrect = true;

        //    }
        //    catch (Exception ex)
        //    {
        //        List<Models.Error> errores = new List<Models.Error>();
        //        ViewBag.CodeCorrect = false;
        //        Models.Error error = new Models.Error();

        //        error.Code = "500.*";
        //        error.Message = "Favor comunicarse con el área TI de Teamwork para solucionar el problema.";

        //        errores.Add(error);

        //        ViewBag.Error = errores;
        //    }

        //    return View();
        //}

        #endregion

        public ActionResult GenerateExcel()
        {
            if (AplicationActive())
            {
                string code = string.Empty;
                string message = string.Empty;
                string domain = string.Empty;
                string prefixDomain = string.Empty;
                string domainReal = string.Empty;

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
                    case "Q29udHJhdG8=":
                        #region "SOLICITUD DE CONTRATO"
                        try
                        {
                            if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                            {

                                string[] parametrosRender =
                                {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@CODIGOTRANSACCION"
                            };

                                string[] valoresRender =
                                {
                                tokenAuth,
                                agenteAplication,
                                Request.QueryString["codigotransaccion"]
                            };

                                DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudContrato(parametrosRender, valoresRender).Table;

                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    Response.ClearContent();
                                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Solicitud Contrato " + Request.QueryString["codigotransaccion"] + ".xlsx"));
                                    Response.ContentType = "application/excel";
                                    Response.BinaryWrite(XLSX_ReporteSolicitudContratos.xslx(dataProceso));
                                    Response.Flush();
                                    Response.End();
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
                            "HttPost | JsonResult | CreaSolicitudContrato",
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
                        #endregion
                        break;
                    case "UmVub3ZhY2lvbg==":
                        #region "SOLICITUD DE RENOVACION"
                        try
                        {
                            if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                            {

                                string[] parametrosRender =
                                {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@CODIGOTRANSACCION"
                            };

                                string[] valoresRender =
                                {
                                tokenAuth,
                                agenteAplication,
                                Request.QueryString["codigotransaccion"]
                            };

                                DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudRenovaciones(parametrosRender, valoresRender).Table;

                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    foreach (DataRow rows in dataProceso.Tables[0].Rows)
                                    {
                                        if (rows["Code"].ToString() == "200")
                                        {

                                            code = "200";

                                            if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                                            {
                                                excel.Workbook.Worksheets.Add("Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                                                var worksheetConfig = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];

                                                string[] columnas =
                                                {
                                                "A",
                                                "B",
                                                "C",
                                                "D",
                                                "E",
                                                "F",
                                                "G",
                                                "H",
                                                "I",
                                                "J",
                                                "K",
                                                "L",
                                                "M",
                                                "N"
                                            };

                                                string[] nameColumnas =
                                                {
                                                "Ficha",
                                                "Rut",
                                                "Nombre Completo",
                                                "Codigo BPO",
                                                "Cargo Mod",
                                                "Fecha Inicio Contrato",
                                                "Fecha Termino Contrato",
                                                "Causal",
                                                "Fecha Inicio Renovacion",
                                                "Fecha Termino Renovacion",
                                                "Tope Legal",
                                                "Area Negocio",
                                                "Empresa (Razon Social)",
                                                "Cliente Firmante"
                                            };

                                                for (var i = 0; i < columnas.Length; i++)
                                                {
                                                    worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                                    worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                                }

                                            }

                                            var worksheet = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                                            int contador = worksheet.Dimension.End.Row + 1;

                                            worksheet.Cells["A" + contador.ToString()].Value = rows["Ficha"].ToString();
                                            worksheet.Cells["B" + contador.ToString()].Value = rows["Rut"].ToString();
                                            worksheet.Cells["C" + contador.ToString()].Value = rows["NombreCompleto"].ToString();
                                            worksheet.Cells["D" + contador.ToString()].Value = rows["CargoBPO"].ToString();
                                            worksheet.Cells["E" + contador.ToString()].Value = rows["CargoMod"].ToString();
                                            worksheet.Cells["F" + contador.ToString()].Value = rows["FechaInicioContrato"].ToString();
                                            worksheet.Cells["G" + contador.ToString()].Value = rows["FechaTerminoContrato"].ToString();
                                            worksheet.Cells["H" + contador.ToString()].Value = rows["Causal"].ToString();
                                            worksheet.Cells["I" + contador.ToString()].Value = rows["FechaInicioRenov"].ToString();
                                            worksheet.Cells["J" + contador.ToString()].Value = rows["FechaTerminoRenov"].ToString();
                                            worksheet.Cells["K" + contador.ToString()].Value = rows["TopeLegal"].ToString();
                                            worksheet.Cells["L" + contador.ToString()].Value = rows["Codigo"].ToString();
                                            worksheet.Cells["M" + contador.ToString()].Value = rows["Empresa"].ToString();
                                            worksheet.Cells["N" + contador.ToString()].Value = rows["ClienteFirmante"].ToString();
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
                                            "HttPost | JsonResult | CreaSolicitudRenovacion",
                                            "Procedimiento Almacenado",
                                            "",
                                            rows["ErrorLine"].ToString(),
                                            rows["ErrorMessage"].ToString(),
                                            rows["ErrorNumber"].ToString(),
                                            rows["ErrorProcedure"].ToString(),
                                            rows["ErrorSeverity"].ToString(),
                                            rows["ErrorState"].ToString()
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

                                    if (code == "200")
                                    {

                                        excel.Workbook.Properties.Title = "Attempts";
                                        Response.ClearContent();
                                        Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Solicitud Renovacion " + Request.QueryString["codigotransaccion"] + ".xlsx"));
                                        Response.ContentType = "application/excel";
                                        Response.BinaryWrite(excel.GetAsByteArray());
                                        Response.Flush();
                                        Response.End();
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
                            "HttPost | JsonResult | CreaSolicitudRenovacion",
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
                        #endregion
                        break;
                    case "YnBvZmlsdHJv":
                        #region "SOLICITUD DE BAJA REPORTE"
                        try
                        {

                            if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                            {
                                string path = "";
                                string nombreArchivo = "";
                                string[] parametrosRender =
                                {
                                "@AUTHENTICATE",
                                "@AGENTAPP",
                                "@CODIGOTRANSACCION",
                                "@FUENTE",
                                "@FECHAINICIO",
                                "@FECHATERMINO"
                            };

                                string[] valoresRender =
                                {
                                tokenAuth,
                                agenteAplication,
                                "",
                                Request.QueryString["fuente"],
                                Request.QueryString["fechainicio"],
                                Request.QueryString["fechatermino"],
                            };


                                DataSet dataProceso = servicioOperaciones.GetArchivoBajasConfirmadas(parametrosRender, valoresRender).Table;

                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                using (ExcelPackage excel = new ExcelPackage())
                                {
                                    foreach (DataRow rows in dataProceso.Tables[0].Rows)
                                    {
                                        code = rows["Code"].ToString();

                                        if (code == "200")
                                        {
                                            if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Sheet1"))
                                            {
                                                excel.Workbook.Worksheets.Add("Sheet1");
                                                var worksheetConfig = excel.Workbook.Worksheets["Sheet1"];

                                                string[] columnas =
                                                {
                                                "A",
                                                "B",
                                                "C",
                                                "D",
                                                "E",
                                                "F",
                                                "G",
                                                "H",
                                                "I",
                                                "J",
                                                "K",
                                                "L",
                                                "M",
                                                "N",
                                                "O",
                                                "P"
                                            };

                                                string[] nameColumnas =
                                                {
                                                "FECHA1-hoy",
                                                "Trabajador",
                                                "FECHA1-termino",
                                                "Causal",
                                                "Hechos",
                                                "FECHA1-inicio",
                                                "Mes-Pago-Cotizaciones",
                                                "Representante-EST",
                                                "area-negocio",
                                                "PERFIL-PROYECTO",
                                                "FECHA1-nopresenta",
                                                "EsBajaAnticipada",
                                                "ContratoDeUnMes",
                                                "ParrafoEspecialArea",
                                                "AnalistaAcargo",
                                                "TipoBaja"
                                            };

                                                for (var i = 0; i < columnas.Length; i++)
                                                {
                                                    worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                                    worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                                }

                                            }

                                            var worksheet = excel.Workbook.Worksheets["Sheet1"];
                                            int contador = worksheet.Dimension.End.Row + 1;

                                            worksheet.Cells["A" + contador.ToString()].Value = rows["FechaSolicitud"].ToString();
                                            worksheet.Cells["B" + contador.ToString()].Value = rows["Rut"].ToString();
                                            worksheet.Cells["C" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                                            worksheet.Cells["D" + contador.ToString()].Value = rows["Causal"].ToString();
                                            worksheet.Cells["E" + contador.ToString()].Value = rows["CausalDescripcion"].ToString();
                                            worksheet.Cells["F" + contador.ToString()].Value = rows["FechaInicio"].ToString();
                                            worksheet.Cells["G" + contador.ToString()].Value = rows["MesPago"].ToString();
                                            worksheet.Cells["H" + contador.ToString()].Value = rows["RepresentanteLegal"].ToString();
                                            worksheet.Cells["I" + contador.ToString()].Value = rows["AreaNegocio"].ToString();
                                            worksheet.Cells["J" + contador.ToString()].Value = rows["PerfilProyecto"].ToString();
                                            worksheet.Cells["K" + contador.ToString()].Value = rows["Fecha1Nopresenta"].ToString();
                                            worksheet.Cells["L" + contador.ToString()].Value = rows["BajaAnticipada"].ToString();
                                            worksheet.Cells["M" + contador.ToString()].Value = rows["ContratoUnMes"].ToString();
                                            worksheet.Cells["N" + contador.ToString()].Value = rows["ParrafoEspecial"].ToString();
                                            worksheet.Cells["O" + contador.ToString()].Value = rows["Correo"].ToString();
                                            worksheet.Cells["P" + contador.ToString()].Value = rows["TipoBaja"].ToString();
                                            worksheet.Cells.AutoFitColumns();
                                        }
                                        else if (code == "500")
                                        {
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
                                            "HttPost | JsonResult | CreaSolicitudBaja",
                                            "Procedimiento Almacenado",
                                            "",
                                            rows["ErrorLine"].ToString(),
                                            rows["ErrorMessage"].ToString(),
                                            rows["ErrorNumber"].ToString(),
                                            rows["ErrorProcedure"].ToString(),
                                            rows["ErrorSeverity"].ToString(),
                                            rows["ErrorState"].ToString()
                                        };

                                            DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                                            foreach (DataRow rowsError in dataError.Tables[0].Rows)
                                            {
                                                servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), "ealcota@team-work.cl",
                                                                                         rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                                                                                         rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                                            }
                                        }
                                    }

                                    switch (code)
                                    {
                                        case "200":
                                            excel.Workbook.Properties.Title = "Attempts";
                                            Response.ClearContent();
                                            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", Request.QueryString["tipo"] + Request.QueryString["codigotransaccion"] + ".xlsx"));
                                            Response.ContentType = "application/excel";
                                            Response.BinaryWrite(excel.GetAsByteArray());
                                            Response.Flush();
                                            Response.End();
                                            break;
                                        default:
                                            Response.Redirect(domain + prefixDomain + "/Operaciones/SolicitudBajas");
                                            break;
                                    }

                                }
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
                            "HttPost | JsonResult | CreaArchivoBPO",
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
                        #endregion
                        break;
                    case "ReporteCargaFirmaDigital":
                        #region "REPORTE CARGA FIRMA DIGITAL"
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        using (ExcelPackage excel = new ExcelPackage())
                        {
                            string filename = string.Empty;

                            foreach (SolicitudCargo item in ModuleSolicitudesCargoMod(Request.QueryString["codigoCargoMod"]))
                            {
                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Cargo Mod - Estructura"))
                                {
                                    excel.Workbook.Worksheets.Add("Cargo Mod - Estructura");
                                    var worksheetConfig = excel.Workbook.Worksheets["Cargo Mod - Estructura"];
                                }

                                var worksheet = excel.Workbook.Worksheets["Cargo Mod - Estructura"];


                                foreach (EstructuraCargo itemHeader in ModuleEstructuraHeader(Request.QueryString["codigoCargoMod"]))
                                {

                                    filename = itemHeader.NombreCargoMod + " - " + item.Empresa;

                                    worksheet.Cells["A1"].Value = "Nombre Cargo";
                                    worksheet.Cells["B1"].Value = itemHeader.NombreCargo;

                                    worksheet.Cells["A2"].Value = "Cliente";
                                    worksheet.Cells["B2"].Value = itemHeader.Cliente;

                                    worksheet.Cells["A3"].Value = "Codigo";
                                    worksheet.Cells["B3"].Value = item.CodigoCargoMod.Replace("-" + item.Empresa, "");

                                    worksheet.Cells["A4"].Value = "Nombre Cargo Mod";
                                    worksheet.Cells["B4"].Value = itemHeader.NombreCargoMod;

                                    foreach (EstructuraCargo itemHaberes in ModuleEstructuraHaberes(Request.QueryString["codigoCargoMod"]))
                                    {
                                        worksheet.Cells["A5"].Value = "Sueldo Liquido";
                                        worksheet.Cells["B5"].Value = itemHaberes.SueldoLiquido.Replace("$ ", "");
                                        worksheet.Cells["C5"].Value = itemHaberes.SueldoLiquidoCifra;

                                        worksheet.Cells["A6"].Value = "Sueldo Base";
                                        worksheet.Cells["B6"].Value = itemHaberes.SueldoBase.Replace("$ ", "");
                                        worksheet.Cells["C6"].Value = itemHaberes.SueldoBaseCifra;

                                        worksheet.Cells["A7"].Value = "Gratificación";
                                        worksheet.Cells["B7"].Value = itemHaberes.Gratificacion.Replace("$ ", "");
                                        worksheet.Cells["C7"].Value = itemHaberes.GratificacionCifra;

                                    }

                                    worksheet.Cells["A8"].Value = "Horas Semanales";
                                    worksheet.Cells["B8"].Value = itemHeader.NumeroHorasSemanalesPT;

                                    worksheet.Cells["A9"].Value = "Horarios";
                                    worksheet.Cells["B9"].Value = itemHeader.Horarios;

                                    foreach (EstructuraCargo itemHaberes in ModuleEstructuraHaberes(Request.QueryString["codigoCargoMod"]))
                                    {
                                        worksheet.Cells["A11"].Value = "Total Imponible";
                                        worksheet.Cells["B11"].Value = itemHaberes.TotalImponible.Replace("$ ", "");
                                        worksheet.Cells["C11"].Value = itemHaberes.TotalImponibleCifra;

                                        worksheet.Cells["A12"].Value = "Total Haberes";
                                        worksheet.Cells["B12"].Value = itemHaberes.TotalHaberes.Replace("$ ", "");
                                        worksheet.Cells["C12"].Value = itemHaberes.TotalHaberesCifra;

                                    }

                                    foreach (Provision itemProv in ModuleEstructuraMargenProvision(Request.QueryString["codigoCargoMod"]))
                                    {
                                        if (itemProv.Concepto.ToUpper() == "COSTO A FACTURAR")
                                        {
                                            worksheet.Cells["A10"].Value = "Costo usuario";
                                            worksheet.Cells["B10"].Value = itemProv.MontoCLP.Replace("$ ", "");
                                            worksheet.Cells["C10"].Value = itemProv.MontoCLPCifra;
                                        }
                                    }
                                }

                                worksheet.Cells.AutoFitColumns();
                            }

                            foreach (BonosCargoMod item in ModuleBonosCargoMod(Request.QueryString["codigoCargoMod"]))
                            {
                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Cargo Mod - Bonos"))
                                {
                                    excel.Workbook.Worksheets.Add("Cargo Mod - Bonos");
                                    var worksheetConfig = excel.Workbook.Worksheets["Cargo Mod - Bonos"];
                                }

                                var worksheet = excel.Workbook.Worksheets["Cargo Mod - Bonos"];
                                int contador = ((worksheet.Dimension != null)
                                                   ? worksheet.Dimension.End.Row
                                                   : 0) + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = item.Nombre;
                                worksheet.Cells["B" + contador.ToString()].Value = item.Valor.Replace("$ ", "");
                                worksheet.Cells["C" + contador.ToString()].Value = item.ValorCifra;
                                worksheet.Cells.AutoFitColumns();
                            }

                            foreach (ANIsCargoMod item in ModuleANIsCargoMod(Request.QueryString["codigoCargoMod"]))
                            {
                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Cargo Mod - Asign. No Imp."))
                                {
                                    excel.Workbook.Worksheets.Add("Cargo Mod - Asign. No Imp.");
                                    var worksheetConfig = excel.Workbook.Worksheets["Cargo Mod - Asign. No Imp."];
                                }

                                var worksheet = excel.Workbook.Worksheets["Cargo Mod - Asign. No Imp."];
                                int contador = ((worksheet.Dimension != null)
                                                   ? worksheet.Dimension.End.Row
                                                   : 0) + 1;


                                worksheet.Cells["A" + contador.ToString()].Value = item.Nombre;
                                worksheet.Cells["B" + contador.ToString()].Value = item.Valor.Replace("$ ", "");
                                worksheet.Cells["C" + contador.ToString()].Value = item.ValorCifra;
                                worksheet.Cells.AutoFitColumns();

                            }

                            excel.Workbook.Properties.Title = "Attempts";
                            Response.ClearContent();
                            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Creación Firma Digital - " + filename + ".xlsx"));
                            Response.ContentType = "application/excel";
                            Response.BinaryWrite(excel.GetAsByteArray());
                            Response.Flush();
                            Response.End();
                        }

                        #endregion
                        break;
                    case "ReporteRenovaciones":
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Renovaciones.xlsx"));
                        Response.ContentType = "application/excel";
                        Response.BinaryWrite(XLSX_ReporteRenovaciones.__xlsx(Request.QueryString["cliente"].ToString(), Request.QueryString["fechainicio"].ToString(), Request.QueryString["fechatermino"].ToString(), Request.QueryString["empresa"].ToString()));
                        Response.Flush();
                        Response.End();
                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult SolicitudBajas()
        {
            List<ProcesoCargaMasiva> cargaMasivas = new List<ProcesoCargaMasiva>();

            if (AplicationActive())
            {
                try
                {
                    string request = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                    dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                    cargaMasivas = CollectionsCargaMasiva.__PlantillaListar(request, "", objects[0].Token.ToString(), ModuleControlRetornoPath());

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    // captura de cualquier excepcion del sistema
                    // errores 500
                    // errores de sesion expirada
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View(cargaMasivas);
        }

        public ActionResult Reporte()
        {
            if (AplicationActive())
            {
                string dato = Request.QueryString["reporte"];
                string type = Request.QueryString["type"];
                string code = string.Empty;

                string[] paramReporte =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@REPORTE",
                    "@TRABAJADOR"
                };

                string[] valReporte =
                {
                    tokenAuth,
                    agenteAplication,
                    dato,
                    Session["NombreUsuario"].ToString()
                };

                DataSet dataReporte = servicioOperaciones.GetReporte(paramReporte, valReporte).Table;

                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    foreach (DataRow rows in dataReporte.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            code = rows["Code"].ToString();
                            if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == rows["TipoDocumento"].ToString() + " Pendientes"))
                            {
                                excel.Workbook.Worksheets.Add(rows["TipoDocumento"].ToString() + " Pendientes");
                                var worksheetConfig = excel.Workbook.Worksheets[rows["TipoDocumento"].ToString() + " Pendientes"];

                                string[] columnas =
                                {
                                "A",
                                "B",
                                "C",
                                "D",
                                "E",
                                "F",
                                "G",
                                "H",
                                "I",
                                "J",
                                "K",
                                "L",
                                "M",
                                "N",
                                "O",
                                "P",
                                "Q"
                            };

                                string[] nameColumnas =
                                {
                                "Rut",
                                "Nombre",
                                "Apellido Paterno",
                                "Apellido Materno",
                                "Cod Cliente",
                                "Cliente",
                                "Empresa",
                                "Ficha",
                                "Fecha de Ingreso",
                                "Fecha Alta KAM",
                                "Dias Solicitud Alta",
                                "Dias Transacurridos",
                                "Estado",
                                "Cargado en Softland",
                                "Analista Contratos",
                                "Comentarios Proceso",
                                "Fecha Término Proceso"
                            };

                                for (var i = 0; i < columnas.Length; i++)
                                {
                                    worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                    worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                }

                            }

                            var worksheet = excel.Workbook.Worksheets[rows["TipoDocumento"].ToString() + " Pendientes"];
                            int contador = worksheet.Dimension.End.Row + 1;

                            worksheet.Cells["A" + contador.ToString()].Value = rows["Rut"].ToString();
                            worksheet.Cells["B" + contador.ToString()].Value = rows["Nombre"].ToString();
                            worksheet.Cells["C" + contador.ToString()].Value = rows["ApellidoPaterno"].ToString();
                            worksheet.Cells["D" + contador.ToString()].Value = rows["ApellidoMaterno"].ToString();
                            worksheet.Cells["E" + contador.ToString()].Value = rows["CodCliente"].ToString();
                            worksheet.Cells["F" + contador.ToString()].Value = rows["Cliente"].ToString();
                            worksheet.Cells["G" + contador.ToString()].Value = rows["Empresa"].ToString();
                            worksheet.Cells["H" + contador.ToString()].Value = rows["Ficha"].ToString();
                            worksheet.Cells["I" + contador.ToString()].Value = rows["FechaIngreso"].ToString();
                            worksheet.Cells["J" + contador.ToString()].Value = rows["FechaAlta"].ToString();
                            worksheet.Cells["K" + contador.ToString()].Value = rows["DiasSolicitud"].ToString();
                            worksheet.Cells["L" + contador.ToString()].Value = rows["DiasTranscurridos"].ToString();
                            worksheet.Cells["M" + contador.ToString()].Value = rows["Estado"].ToString();
                            worksheet.Cells["N" + contador.ToString()].Value = rows["CargadoSoftland"].ToString();
                            worksheet.Cells["O" + contador.ToString()].Value = rows["Analista"].ToString();
                            worksheet.Cells["P" + contador.ToString()].Value = rows["ComentariosProceso"].ToString();
                            worksheet.Cells["Q" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                        }
                        else
                        {
                            code = rows["Code"].ToString();
                        }

                    }

                    if (code == "200")
                    {
                        excel.Workbook.Properties.Title = "Attempts";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte de Contratos y Renovaciones " + type + ".xlsx"));
                        Response.ContentType = "application/excel";
                        Response.BinaryWrite(excel.GetAsByteArray());
                        Response.Flush();
                        Response.End();
                    }
                    else
                    {
                        Response.Redirect("/Operaciones");
                    }

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

        public ActionResult CargoMod()
        {
            if (AplicationActive())
            {
                #region "PERMISOS Y ACCESOS"

                if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
                {
                    Session["ApplicationActive"] = null;
                    return View();
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

                ViewBag.OptionCargoMod = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/Operaciones/CargoMod/RQ==/Create";
                ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/Operaciones/CargoMod/Uw==/Create";

                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "CargoMod":
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.GlosaOrigenDashboard = "Creaciones de Solicitudes";
                        ViewBag.TagSelected = "CargoMod";
                        break;
                    case "Create":
                        ViewBag.EventoCreacion = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2];
                        break;
                    case "Detail":
                        ViewBag.OptionCargoMod = "Detail";
                        ViewBag.OptionStageCargoMod = "OK";
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                        break;
                    default:

                        switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2])
                        {
                            case "Wizards":
                                ViewBag.OptionCargoMod = "Wizards";
                                ViewBag.Wizards = "Operaciones/CargoMod/Wizards/_" + Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
                                ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 3]);
                                ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 3]);
                                ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 3]);
                                ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
                                ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                                ViewBag.RenderizadoListObservaciones = ModuleListObservaciones(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 3]);

                                ViewBag.WizardsNext = Convert.ToInt32(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]) + 1;
                                ViewBag.WizardsPrev = Convert.ToInt32(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1]) - 1;
                                ViewBag.WizardsActual = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];
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

        public ActionResult Pdf()
        {
            if (!AplicationActive())
            {

            }

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

            return new FileContentResult(pdfbyte, "application/pdf");
        }

        #region "Metodos Carga Masiva"

        [AllowAnonymous]
        [ValidateInput(false)]
        [HttpPost]
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

        [HttpPost]
        public string PlantillaValidarCargaContrato(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaContrato(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaValidarCargaRenovacion(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaRenovacion(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaValidarCargaBajas(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaValidarCargaBajas(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaActualizaCreaFicha(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaActualizaCreaFicha(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaCrearContrato(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearContrato(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaCrearRenovacion(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearRenovacion(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaCrearBaja(string codigoTransaction = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();

            dynamic request = CollectionsCargaMasiva.__PlantillaCrearBaja(codigoTransaction, objects[0].Token.ToString());

            return JsonConvert.SerializeObject(request, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaEmitirSolicitudContrato(string codigoTransaction = "", string plantillaMasiva = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();
            string code = string.Empty;
            string nombreArchivo = string.Empty;
            string response = string.Empty;

            dynamic request = CollectionsCargaMasiva.__PlantillaEmitirSolicitudContrato(codigoTransaction, plantillaMasiva, objects[0].Token.ToString());
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                for (dynamic i = 0; i < request.Count; i++)
                {
                    if (request[i].Code.ToString() == "200")
                    {
                        code = "200";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Contratos " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()))
                        {
                            excel.Workbook.Worksheets.Add("Contratos " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString());
                            var worksheetConfig = excel.Workbook.Worksheets["Contratos " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()];

                            string[] columnas =
                            {
                                "A",
                                "B",
                                "C",
                                "D",
                                "E",
                                "F",
                                "G",
                                "H",
                                "I",
                                "J",
                                "K",
                                "L",
                                "M",
                                "N",
                                "O",
                                "P",
                                "Q",
                                "R",
                                "S",
                                "T",
                                "U",
                                "V",
                                "W",
                                "X",
                                "Y",
                                "Z",
                                "AA",
                                "AB",
                                "AC",
                                "AD",
                                "AE",
                                "AF",
                                "AG",
                                "AH",
                                "AI",
                                "AJ",
                                "AK",
                                "AL",
                                "AM",
                                "AN",
                                "AO"
                            };

                            string[] nameColumnas =
                            {
                                "Fecha Transacción",
                                "Ficha",
                                "Area Negocio",
                                "Rut Empresa",
                                "Rut Trabajador",
                                "Nombre Trabajador",
                                "Codigo BPO",
                                "Cargo Mod",
                                "Cargo",
                                "Sucursal",
                                "Ejecutivo",
                                "Fecha de Inicio",
                                "Fecha de Termino",
                                "Causal",
                                "Reemplazo",
                                "TipoContrato",
                                "CC",
                                "Dirección",
                                "Comuna",
                                "Ciudad",
                                "Región",
                                "Horario",
                                "Horas Trabajadas",
                                "Colación",
                                "Nacionalidad",
                                "Visa",
                                "Vencimiento Visa",
                                "Cliente Firmante",
                                "División (Solo FNC)",
                                "Descripcion de funciones",
                                "Fecha Inicio Primer Plazo",
                                "Fecha Término Primer Plazo",
                                "Fecha Inicio Segundo Plazo",
                                "Fecha Término Segundo Plazo",
                                "Renovacion Automática",
                                "Rut Prevencionista",
                                "Numero Registro Prevencionista",
                                "Rut Prevencionista Cliente",
                                "Empresa Mandante",
                                "Tarifa Diaria",
                                "Rut Representante Teamwork"
                            };

                            for (dynamic j = 0; j < columnas.Length; j++)
                            {
                                worksheetConfig.Cells[columnas[j] + "1"].Value = nameColumnas[j];
                                worksheetConfig.Cells[columnas[j] + "1"].Style.Font.Bold = true;
                            }

                        }

                        var worksheet = excel.Workbook.Worksheets["Contratos " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()];
                        int contador = worksheet.Dimension.End.Row + 1;

                        worksheet.Cells["A" + contador.ToString()].Value = request[i].FechaTransaccion.ToString();
                        worksheet.Cells["B" + contador.ToString()].Value = request[i].Ficha.ToString();
                        worksheet.Cells["C" + contador.ToString()].Value = request[i].Codigo.ToString();
                        worksheet.Cells["D" + contador.ToString()].Value = request[i].RutEmpresa.ToString();
                        worksheet.Cells["E" + contador.ToString()].Value = request[i].Rut.ToString();
                        worksheet.Cells["F" + contador.ToString()].Value = request[i].Nombre.ToString();
                        worksheet.Cells["G" + contador.ToString()].Value = request[i].CargoBPO.ToString();
                        worksheet.Cells["H" + contador.ToString()].Value = request[i].CargoMod.ToString();
                        worksheet.Cells["I" + contador.ToString()].Value = request[i].Cargo.ToString();
                        worksheet.Cells["J" + contador.ToString()].Value = request[i].Sucursal.ToString();
                        worksheet.Cells["K" + contador.ToString()].Value = request[i].Ejecutivo.ToString();
                        worksheet.Cells["L" + contador.ToString()].Value = request[i].FechaInicio.ToString();
                        worksheet.Cells["M" + contador.ToString()].Value = request[i].FechaTermino.ToString();
                        worksheet.Cells["N" + contador.ToString()].Value = request[i].Causal.ToString();
                        worksheet.Cells["O" + contador.ToString()].Value = request[i].Reemplazo.ToString();
                        worksheet.Cells["P" + contador.ToString()].Value = request[i].TipoContrato.ToString();
                        worksheet.Cells["Q" + contador.ToString()].Value = request[i].CC.ToString();
                        worksheet.Cells["R" + contador.ToString()].Value = request[i].Direccion.ToString();
                        worksheet.Cells["S" + contador.ToString()].Value = request[i].Comuna.ToString();
                        worksheet.Cells["T" + contador.ToString()].Value = request[i].Ciudad.ToString();
                        worksheet.Cells["U" + contador.ToString()].Value = request[i].Region.ToString();
                        worksheet.Cells["V" + contador.ToString()].Value = request[i].Horarios.ToString();
                        worksheet.Cells["W" + contador.ToString()].Value = request[i].HorasTrab.ToString();
                        worksheet.Cells["X" + contador.ToString()].Value = request[i].Colacion.ToString();
                        worksheet.Cells["Y" + contador.ToString()].Value = request[i].Nacionalidad.ToString();
                        worksheet.Cells["Z" + contador.ToString()].Value = request[i].Visa.ToString();
                        worksheet.Cells["AA" + contador.ToString()].Value = request[i].VencVisa.ToString();
                        worksheet.Cells["AB" + contador.ToString()].Value = request[i].Firmante.ToString();
                        worksheet.Cells["AC" + contador.ToString()].Value = request[i].Division.ToString();
                        worksheet.Cells["AD" + contador.ToString()].Value = request[i].DescripcionFunciones.ToString();
                        worksheet.Cells["AE" + contador.ToString()].Value = request[i].FechaInicioPPlazo.ToString();
                        worksheet.Cells["AF" + contador.ToString()].Value = request[i].FechaTerminoPPlazo.ToString();
                        worksheet.Cells["AG" + contador.ToString()].Value = request[i].FechaInicioSPlazo.ToString();
                        worksheet.Cells["AH" + contador.ToString()].Value = request[i].FechaTerminoSPlazo.ToString();
                        worksheet.Cells["AI" + contador.ToString()].Value = request[i].RenovacionAutomatica.ToString();
                        worksheet.Cells["AJ" + contador.ToString()].Value = request[i].RutPrevencionista.ToString();
                        worksheet.Cells["AK" + contador.ToString()].Value = request[i].NumeroRegistro.ToString();
                        worksheet.Cells["AL" + contador.ToString()].Value = request[i].RutPrevencionistaCliente.ToString();
                        worksheet.Cells["AM" + contador.ToString()].Value = request[i].EmpresaMandante.ToString();
                        worksheet.Cells["AN" + contador.ToString()].Value = request[i].TarifaDiaria.ToString();
                        worksheet.Cells["AO" + contador.ToString()].Value = request[i].RutRepresentanteTeamwork.ToString();
                    }
                }

                if (code == "200")
                {
                    nombreArchivo = "Solicitud Contrato " + codigoTransaction + ".xlsx";

                    byte[] data = excel.GetAsByteArray();

                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvQ29udHJhdG9Tb2xpY2l0dWQ=",
                        "Q29ycmVvU29saWNpdHVkVm91Y2hlcg=="
                    };

                    for (dynamic h = 0; h < plantillasCorreo.Length; h++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[h],
                            codigoTransaction,
                            ""
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                            rows["Clave"].ToString(),
                                                                            rows["Para"].ToString(),
                                                                            rows["Html"].ToString(),
                                                                            rows["Asunto"].ToString(),
                                                                            rows["CC"].ToString(),
                                                                            rows["CCO"].ToString(),
                                                                            nombreArchivo,
                                                                            rows["Importancia"].ToString(),
                                                                            data,
                                                                            "application/vnd.ms-excel"))
                            {
                                response = "{ code: '200' }";
                            }
                            else
                            {
                                response = "{ code: '500' }";
                            }
                        }
                    }



                }
            }

            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaEmitirSolicitudRenovacion(string codigoTransaction = "", string plantillaMasiva = "")
        {

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();
            string code = string.Empty;
            string nombreArchivo = string.Empty;
            string response = string.Empty;

            dynamic request = CollectionsCargaMasiva.__PlantillaEmitirSolicitudRenovacion(codigoTransaction, plantillaMasiva, objects[0].Token.ToString());

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                for (dynamic i = 0; i < request.Count; i++)
                {
                    if (request[i].Code.ToString() == "200")
                    {

                        code = "200";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Renovaciones " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()))
                        {
                            excel.Workbook.Worksheets.Add("Renovaciones " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString());
                            var worksheetConfig = excel.Workbook.Worksheets["Renovaciones " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()];

                            string[] columnas =
                            {
                                "A",
                                "B",
                                "C",
                                "D",
                                "E",
                                "F",
                                "G",
                                "H",
                                "I",
                                "J",
                                "K",
                                "L",
                                "M",
                                "N",
                                "O",
                                "P",
                                "Q"
                            };

                            string[] nameColumnas =
                            {
                                "Fecha Transaccion",
                                "Ficha",
                                "Rut",
                                "Nombre Completo",
                                "Codigo BPO",
                                "Cargo Mod",
                                "Fecha Inicio Contrato",
                                "Fecha Termino Contrato",
                                "Causal",
                                "Fecha Inicio Renovacion",
                                "Fecha Termino Renovacion",
                                "Tope Legal",
                                "Area Negocio",
                                "Empresa (Razon Social)",
                                "Cliente Firmante",
                                "Tarifa Diaria",
                                "Rut Representante Teamwork"
                            };

                            for (var j = 0; j < columnas.Length; j++)
                            {
                                worksheetConfig.Cells[columnas[j] + "1"].Value = nameColumnas[j];
                                worksheetConfig.Cells[columnas[j] + "1"].Style.Font.Bold = true;
                            }

                        }

                        var worksheet = excel.Workbook.Worksheets["Renovaciones " + request[i].Codigo.ToString() + " " + request[i].Empresa.ToString()];
                        int contador = worksheet.Dimension.End.Row + 1;

                        worksheet.Cells["A" + contador.ToString()].Value = request[i].FechaTransaccion.ToString();
                        worksheet.Cells["B" + contador.ToString()].Value = request[i].Ficha.ToString();
                        worksheet.Cells["C" + contador.ToString()].Value = request[i].Rut.ToString();
                        worksheet.Cells["D" + contador.ToString()].Value = request[i].NombreCompleto.ToString();
                        worksheet.Cells["E" + contador.ToString()].Value = request[i].CargoBPO.ToString();
                        worksheet.Cells["F" + contador.ToString()].Value = request[i].CargoMod.ToString();
                        worksheet.Cells["G" + contador.ToString()].Value = request[i].FechaInicioContrato.ToString();
                        worksheet.Cells["H" + contador.ToString()].Value = request[i].FechaTerminoContrato.ToString();
                        worksheet.Cells["I" + contador.ToString()].Value = request[i].Causal.ToString();
                        worksheet.Cells["J" + contador.ToString()].Value = request[i].FechaInicioRenov.ToString();
                        worksheet.Cells["K" + contador.ToString()].Value = request[i].FechaTerminoRenov.ToString();
                        worksheet.Cells["L" + contador.ToString()].Value = request[i].TopeLegal.ToString();
                        worksheet.Cells["M" + contador.ToString()].Value = request[i].Codigo.ToString();
                        worksheet.Cells["N" + contador.ToString()].Value = request[i].Empresa.ToString();
                        worksheet.Cells["O" + contador.ToString()].Value = request[i].ClienteFirmante.ToString();
                        worksheet.Cells["P" + contador.ToString()].Value = request[i].TarifaDiaria.ToString();
                        worksheet.Cells["Q" + contador.ToString()].Value = request[i].RutRepresentanteTeamwork.ToString();
                    }

                }

                if (code == "200")
                {

                    nombreArchivo = "Solicitud Renovacion " + codigoTransaction + ".xlsx";

                    byte[] data = excel.GetAsByteArray();

                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvUmVub3ZhY2lvblZvdWNoZXI=",
                        "Q29ycmVvUmVub3ZhY2lvblNvbGljaXR1ZA=="
                    };

                    for (dynamic h = 0; h < plantillasCorreo.Length; h++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[h],
                            codigoTransaction,
                            ""
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                            rows["Clave"].ToString(),
                                                                            rows["Para"].ToString(),
                                                                            rows["Html"].ToString(),
                                                                            rows["Asunto"].ToString(),
                                                                            rows["CC"].ToString(),
                                                                            rows["CCO"].ToString(),
                                                                            nombreArchivo,
                                                                            rows["Importancia"].ToString(),
                                                                            data,
                                                                            "application/vnd.ms-excel"))
                            {
                                response = "{ code: '200' }";
                            }
                            else
                            {
                                response = "{ code: '500' }";
                            }
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }

        [HttpPost]
        public string PlantillaEmitirSolicitudBajas(string codigoTransaction = "", string plantillaMasiva = "")
        {

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            string usuarioCreador = Session["base64Username"].ToString();
            string code = string.Empty;
            string nombreArchivo = string.Empty;
            string response = string.Empty;
            
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                
                nombreArchivo = "Solicitud Bajas " + codigoTransaction + ".xlsx";

                byte[] data = XLSX_ReporteSolicitudes.__xlsx(codigoTransaction, plantillaMasiva);

                string[] plantillasCorreo =
                {
                    "Q29ycmVvQmFqYXNDb25maXJtYWRhcw=="
                };

                for (dynamic h = 0; h < plantillasCorreo.Length; h++)
                {
                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@PLANTILLACORREO",
                        "@CODIGOTRANSACCION",
                        "@PATHFILE"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        plantillasCorreo[h],
                        codigoTransaction,
                        ""
                    };

                    DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                    foreach (DataRow rows in dataProceso.Tables[0].Rows)
                    {
                        if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                        rows["Clave"].ToString(),
                                                                        rows["Para"].ToString(),
                                                                        rows["Html"].ToString(),
                                                                        rows["Asunto"].ToString(),
                                                                        rows["CC"].ToString(),
                                                                        rows["CCO"].ToString(),
                                                                        nombreArchivo,
                                                                        rows["Importancia"].ToString(),
                                                                        data,
                                                                        "application/vnd.ms-excel"))
                        {
                            response = "{ code: '200' }";
                        }
                        else
                        {
                            response = "{ code: '500' }";
                        }
                    }
                }
            }

            return JsonConvert.SerializeObject(response, Formatting.Indented);
        }

        #endregion

        #region "DESCARGA DE ARCHIVOS"

        public ActionResult DownloadFileCargaMasiva()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            List<ResponseCarga> objectsDownload = CollectionsCargaMasiva.__PlantillaDownload(Request.QueryString["template"], objects[0].Token.ToString());
            dynamic errores = null;

            if (Request.QueryString["codigotransaccion"] != null)
            {
                // aqui obtencion de datos erroneos
                errores = JsonConvert.DeserializeObject(CollectionsCargaMasiva.__PlantillaReportError(Request.QueryString["codigotransaccion"], Request.QueryString["template"], objects[0].Token.ToString()));
                
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                foreach (ResponseCarga item in objectsDownload)
                {
                    excel.Workbook.Worksheets.Add(item.NombreHojaCargaMasiva);
                    var worksheet = excel.Workbook.Worksheets[item.NombreHojaCargaMasiva];

                    foreach (string columna in item.Columnas.Split(':'))
                    {
                        worksheet.Cells[columna.Split('@')[1] + "1"].Value = columna.Split('@')[0];
                    }

                    if (Request.QueryString["codigotransaccion"] != null)
                    {
                        for (dynamic i = 1; i <= errores.Count; i++)
                        {
                            for (dynamic j = 0; j < item.Columnas.Split(':').Length; j++)
                            {
                                worksheet.Cells[item.Columnas.Split(':')[j].Split('@')[1] + "" + (i + 1)].Value = (errores[(i - 1)][item.GetValueError.Split('@')[j].Replace(@"""", "")]).ToString();
                            }
                        }

                        worksheet.Cells[1, item.Columnas.Split(':').Length + 1].Value = "Observaciones a Corregir";

                        for (dynamic h = 1; h <= errores.Count; h++)
                        {
                            worksheet.Cells[(h + 1), item.Columnas.Split(':').Length + 1].Value = (errores[(h - 1)]["ObsProcesamiento"]).ToString();
                        }
                    }

                    excel.Workbook.Properties.Title = "Attempts";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", item.NombrePlantilla + ".xlsx"));
                    Response.ContentType = "application/excel";
                    Response.BinaryWrite(excel.GetAsByteArray());
                    Response.Flush();
                    Response.End();
                }
            }
            
            return View();
        }

        //public ActionResult DownloadFileCargaMasiva()
        //{

        //    if (Session["CodificarAuth"] == null || Session["NombreUsuario"].ToString() == null || Session["RenderizadoPermisos"] == null)
        //    {
        //        Session["ApplicationActive"] = null;
        //    }

        //    string[] parametrosDownload =
        //    {
        //        "@AUTHENTICATE",
        //        "@AGENTAPP",
        //        "@PLANTILLA"
        //    };

        //    string[] valoresDownload =
        //    {
        //        tokenAuth,
        //        agenteAplication,
        //        Request.QueryString["template"]
        //    };

        //    DataSet dataDownload = servicioAuth.GetDownloadPlantillaCargaMasiva(parametrosDownload, valoresDownload).Table;

        //    foreach (DataRow rowsDownload in dataDownload.Tables[0].Rows)
        //    {
        //        using (ExcelPackage excel = new ExcelPackage())
        //        {
        //            excel.Workbook.Worksheets.Add(rowsDownload["NombreHojaCargaMasiva"].ToString());
        //            var worksheet = excel.Workbook.Worksheets[rowsDownload["NombreHojaCargaMasiva"].ToString()];

        //            foreach (string columna in rowsDownload["Columnas"].ToString().Split(':'))
        //            {
        //                worksheet.Cells[columna.Split('@')[1] + "1"].Value = columna.Split('@')[0];
        //            }



        //            switch (Request.QueryString["template"])
        //            {
        //                case "Mw==":
        //                    string[] parametrossHoja =
        //                    {
        //                        "@AUTHENTICATE",
        //                        "@AGENTAPP"
        //                    };

        //                    string[] valoresHoja =
        //                    {
        //                        tokenAuth,
        //                        agenteAplication
        //                    };

        //                    DataSet dataSheet = servicioOperaciones.GetObtenerHojasCargaMasiva(parametrossHoja, valoresHoja).Table;

        //                    foreach (DataRow rows in dataSheet.Tables[0].Rows)
        //                    {
        //                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == rows["Hoja"].ToString()))
        //                        {
        //                            excel.Workbook.Worksheets.Add(rows["Hoja"].ToString());
        //                            var worksheetConfig = excel.Workbook.Worksheets[rows["Hoja"].ToString()];

        //                            string[] columnas =
        //                            {
        //                                "A",
        //                                "B",
        //                                "C",
        //                                "D"
        //                            };

        //                            string[] nameColumnas =
        //                            {
        //                                "Codigo",
        //                                "Area Negocio",
        //                                "Rut",
        //                                "Nombre"
        //                            };

        //                            for (var i = 0; i < columnas.Length; i++)
        //                            {
        //                                worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
        //                                worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
        //                            }

        //                        }

        //                        var worksheetCli = excel.Workbook.Worksheets[rows["Hoja"].ToString()];
        //                        int contador = worksheetCli.Dimension.End.Row + 1;

        //                        worksheetCli.Cells["A" + contador.ToString()].Value = rows["Codigo"].ToString();
        //                        worksheetCli.Cells["B" + contador.ToString()].Value = rows["CodigoCli"].ToString();
        //                        worksheetCli.Cells["C" + contador.ToString()].Value = rows["Rut"].ToString();
        //                        worksheetCli.Cells["D" + contador.ToString()].Value = rows["Nombre"].ToString();

        //                        worksheetCli.Cells["A:D"].AutoFitColumns();
        //                    }
        //                    break;
        //                case "Mzg=":

        //                    break;
        //            }

        //            excel.Workbook.Properties.Title = "Attempts";
        //            Response.ClearContent();
        //            Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", rowsDownload["NombrePlantilla"].ToString() + ".xlsx"));
        //            Response.ContentType = "application/excel";
        //            Response.BinaryWrite(excel.GetAsByteArray());
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }

        //    return View();
        //}

        #endregion

        #region "METODOS HTTPOS"

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
                if(Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
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
                        (data.Replace("&", "@@@@").Replace(@"""", "@_@_@")).Replace(@"'", @""""),
                        codigoTransaccion,
                        Session["NombreUsuario"].ToString(),
                        plantilla,
                        lengthProcesamiento,
                        templateColumn
                    };

                    DataSet dataProceso = servicioOperaciones.SetProcesoMasivo(parametrosCargaMasiva, valoresCargaMasiva).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() != "500") {
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
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null) {

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

            return Json(new { Code = code, Message = message, TotalProcesado = totalProcesado, TotalErrores = totalErrores, MessageErrores = messageErrores,
                              ContieneErrores = contieneErrores, RutProcesado = rutProcesado, SigueProceso = sigueProceso });
        }

        [HttpPost]
        public JsonResult CreaOActualizaFichaPersonal(string codigoTransaccion, string procesamiento)
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

                    string[] parametrosFichaPersonal =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO"
                    };

                    string[] valoresFichaPersonal =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento
                    };

                    DataSet dataProceso = servicioOperaciones.SetCreaOActualizaFichaPersonal(parametrosFichaPersonal, valoresFichaPersonal).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200") {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
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
                                "HttPost | JsonResult | CreaOActualizaFichaPersonal",
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
                    "HttPost | JsonResult | CreaOActualizaFichaPersonal",
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
        public JsonResult CreaContratoDeTrabajo(string codigoTransaccion, string procesamiento)
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

                    string[] parametrosFichaPersonal =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO"
                    };

                    string[] valoresFichaPersonal =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento
                    };

                    DataSet dataProceso = servicioOperaciones.SetCreaContratoDeTrabajo(parametrosFichaPersonal, valoresFichaPersonal).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200") {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
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
                                "HttPost | JsonResult | CreaContratoDeTrabajo",
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
                    "HttPost | JsonResult | CreaContratoDeTrabajo",
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
        public JsonResult CreaRenovacion(string codigoTransaccion, string procesamiento)
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

                    string[] parametrosFichaPersonal =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO"
                    };

                    string[] valoresFichaPersonal =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento
                    };

                    DataSet dataProceso = servicioOperaciones.SetCreaRenovacion(parametrosFichaPersonal, valoresFichaPersonal).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200") {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
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
                                "HttPost | JsonResult | CreaRenovacion",
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
                    "HttPost | JsonResult | CreaRenovacion",
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
        public JsonResult CreaAnexo(string codigoTransaccion, string procesamiento, string template)
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

                    string[] parametrosFichaPersonal =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO",
                        "@TEMPLATE"
                    };

                    string[] valoresFichaPersonal =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento,
                        template
                    };

                    DataSet dataAnexo = servicioOperaciones.SetCreaAnexo(parametrosFichaPersonal, valoresFichaPersonal).Table;

                    foreach (DataRow rowsProceso in dataAnexo.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200")
                        {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
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
                                "HttPost | JsonResult | CreaAnexo",
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
                    "HttPost | JsonResult | CreaAnexo",
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
        public JsonResult CrearBaja(string codigoTransaccion, string procesamiento, string plantilla)
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

                    string[] parametrosFichaPersonal =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@RUTPROCESADO"
                    };

                    string[] valoresFichaPersonal =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        procesamiento
                    };

                    DataSet dataProceso = servicioOperaciones.SetCrearBaja(parametrosFichaPersonal, valoresFichaPersonal).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200")
                        {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
                            totalProcesado = rowsProceso["TotalProcesado"].ToString();
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
                                "HttPost | JsonResult | CrearBaja",
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
                    "HttPost | JsonResult | CrearBaja",
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
        public JsonResult LoaderCrearSolicitud(string codigoTransaccion, string plantilla)
        {
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@PLANTILLA"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        plantilla
                    };

                    DataSet dataProceso = servicioOperaciones.GetRenderLoaderCreacionSolicitud(parametrosRender, valoresRender).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200")
                        {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
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
                                "HttPost | JsonResult | LoaderCrearSolicitud",
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
                    "HttPost | JsonResult | LoaderCrearSolicitud",
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

            return Json(new { Code = code, Message = message });
        }

        [HttpPost]
        public JsonResult CreaSolicitudRenovacion(string codigoTransaccion)
        {
            string path = string.Empty;
            string nombreArchivo = string.Empty;
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion
                    };

                    DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudRenovaciones(parametrosRender, valoresRender).Table;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200") {

                                code = "200";

                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add("Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];

                                    string[] columnas =
                                    {
                                        "A",
                                        "B",
                                        "C",
                                        "D",
                                        "E",
                                        "F",
                                        "G",
                                        "H",
                                        "I",
                                        "J",
                                        "K",
                                        "L",
                                        "M",
                                        "N"
                                    };

                                    string[] nameColumnas =
                                    {
                                        "Ficha",
                                        "Rut",
                                        "Nombre Completo",
                                        "Codigo BPO",
                                        "Cargo Mod",
                                        "Fecha Inicio Contrato",
                                        "Fecha Termino Contrato",
                                        "Causal",
                                        "Fecha Inicio Renovacion",
                                        "Fecha Termino Renovacion",
                                        "Tope Legal",
                                        "Area Negocio",
                                        "Empresa (Razon Social)",
                                        "Cliente Firmante"
                                    };

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["Ficha"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["Rut"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["NombreCompleto"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["CargoBPO"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["CargoMod"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["FechaInicioContrato"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["FechaTerminoContrato"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["Causal"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["FechaInicioRenov"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["FechaTerminoRenov"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["TopeLegal"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["Codigo"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["Empresa"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["ClienteFirmante"].ToString();
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
                                    "HttPost | JsonResult | CreaSolicitudRenovacion",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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

                        if (code == "200") {

                            nombreArchivo = "Solicitud Renovacion " + codigoTransaccion + ".xlsx";

                            byte[] data = excel.GetAsByteArray();

                            Session["ApplicationExcelSolicitud"] = data;
                            Session["ApplicationExcelSolicitudName"] = nombreArchivo;
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
                    "HttPost | JsonResult | CreaSolicitudRenovacion",
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
                
            return Json(new { Code = code, Message = message, PathFile = nombreArchivo });
        }

        [HttpPost]
        public JsonResult CreaSolicitudBaja(string codigoTransaccion)
        {
            string path = string.Empty;
            string nombreArchivo = string.Empty;
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@FUENTE",
                        "@FECHAINICIO",
                        "@FECHATERMINO"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        "NORMAL",
                        "",
                        ""
                    };

                    DataSet dataProceso = servicioOperaciones.GetArchivoBajasConfirmadas(parametrosRender, valoresRender).Table;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {

                                code = "200";

                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Bajas " + rows["Empresa"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add("Bajas " + rows["Empresa"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets["Bajas "  + rows["Empresa"].ToString()];

                                    string[] columnas =
                                    {
                                        "A",
                                        "B",
                                        "C",
                                        "D",
                                        "E",
                                        "F",
                                        "G",
                                        "H",
                                        "I",
                                        "J",
                                        "K",
                                        "L",
                                        "M",
                                        "N",
                                        "O"
                                    };

                                    string[] nameColumnas =
                                    {
                                        "Ficha",
                                        "Rut",
                                        "Nombre Completo",
                                        "Direccion Trabajador",
                                        "Area Negocio",
                                        "Cargo",
                                        "CargoMOD",
                                        "Causal",
                                        "Descripcion Causal",
                                        "Fecha Inicio Contrato",
                                        "Fecha Termino Contrato",
                                        "Mes Termino Contrato",
                                        "Empresa (Razon Social)",
                                        "Fecha Solicitud",
                                        "Observacion"
                                    };

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets["Bajas " + rows["Empresa"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["Ficha"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["Rut"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["NombreCompleto"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["DireccionTrabajador"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["AreaNegocio"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["Cargo"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["CargoMOD"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["Causal"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["CausalDescripcion"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["FechaInicio"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["MesYear"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["Empresa"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["FechaSolicitud"].ToString();
                                worksheet.Cells["O" + contador.ToString()].Value = rows["Observacion"].ToString();
                                worksheet.Cells.AutoFitColumns();
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
                                    "HttPost | JsonResult | CreaSolicitudBaja",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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

                        if (code == "200")
                        {

                            nombreArchivo = "Solicitud Bajas " + codigoTransaccion + ".xlsx";

                            byte[] data = excel.GetAsByteArray();

                            Session["ApplicationExcelSolicitud"] = data;
                            Session["ApplicationExcelSolicitudName"] = nombreArchivo;
                        }
                    }

                   // string[] parametrosRender1 =
                   //{
                   //     "@AUTHENTICATE",
                   //     "@AGENTAPP",
                   //     "@CODIGOTRANSACCION",
                   //     "@FUENTE"
                   // };

                   // string[] valoresRender1 =
                   // {
                   //     tokenAuth,
                   //     agenteAplication,
                   //     codigoTransaccion,
                   //     "BPO"
                   // };

                   // DataSet dataProceso1 = servicioOperaciones.GetArchivoBajasConfirmadas(parametrosRender1, valoresRender1).Table;

                   // using (ExcelPackage excel1 = new ExcelPackage())
                   // {
                   //     foreach (DataRow rows in dataProceso1.Tables[0].Rows)
                   //     {
                   //         if (rows["Code"].ToString() == "200")
                   //         {
                   //             code = "200";
                   //             //if (rows["Empresa"].ToString() == "EST")
                   //             //{
                   //                 if (!excel1.Workbook.Worksheets.Any(sheet => sheet.Name == "Sheet1"))
                   //                 {
                   //                     excel1.Workbook.Worksheets.Add("Sheet1");
                   //                     var worksheetConfig = excel1.Workbook.Worksheets["Sheet1"];

                   //                     string[] columnas =
                   //                     {
                   //                         "A",
                   //                         "B",
                   //                         "C",
                   //                         "D",
                   //                         "E",
                   //                         "F",
                   //                         "G",
                   //                         "H",
                   //                         "I",
                   //                         "J",
                   //                         "K",
                   //                         "L",
                   //                         "M",
                   //                         "N",
                   //                         "O"
                   //                     };

                   //                     string[] nameColumnas =
                   //                     {
                   //                         "FECHA1-hoy",
                   //                         "Trabajador",
                   //                         "area-negocio",
                   //                         "FECHA1-inicio",
                   //                         "FECHA1-termino",
                   //                         "Causal",
                   //                         "Hechos",
                   //                         "PERFIL-PROYECTO",
                   //                         "Mes-Pago-Cotizaciones",
                   //                         "Representante-EST",
                   //                         "FECHA1-nopresenta",
                   //                         "EsBajaAnticipada",
                   //                         "ContratoDeUnMes",
                   //                         "ParrafoEspecialArea",
                   //                         "AnalistaAcargo"
                   //                     };

                   //                     for (var i = 0; i < columnas.Length; i++)
                   //                     {
                   //                         worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                   //                         worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                   //                     }

                   //                 }

                   //                 var worksheet = excel1.Workbook.Worksheets["Sheet1"];
                   //                 int contador = worksheet.Dimension.End.Row + 1;

                   //                 worksheet.Cells["A" + contador.ToString()].Value = rows["FechaSolicitud"].ToString();
                   //                 worksheet.Cells["B" + contador.ToString()].Value = rows["Rut"].ToString();
                   //                 worksheet.Cells["C" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                   //                 worksheet.Cells["D" + contador.ToString()].Value = rows["Causal"].ToString();
                   //                 worksheet.Cells["E" + contador.ToString()].Value = rows["CausalDescripcion"].ToString();
                   //                 worksheet.Cells["F" + contador.ToString()].Value = rows["FechaInicio"].ToString();
                   //                 worksheet.Cells["G" + contador.ToString()].Value = rows["MesPago"].ToString();
                   //                 worksheet.Cells["H" + contador.ToString()].Value = rows["RepresentanteLegal"].ToString();
                   //                 worksheet.Cells["I" + contador.ToString()].Value = rows["AreaNegocio"].ToString();
                   //                 worksheet.Cells["J" + contador.ToString()].Value = rows["PerfilProyecto"].ToString();
                   //                 worksheet.Cells["K" + contador.ToString()].Value = rows["Fecha1Nopresenta"].ToString();
                   //                 worksheet.Cells["L" + contador.ToString()].Value = rows["BajaAnticipada"].ToString();
                   //                 worksheet.Cells["M" + contador.ToString()].Value = rows["ContratoUnMes"].ToString();
                   //                 worksheet.Cells["N" + contador.ToString()].Value = rows["ParrafoEspecial"].ToString();
                   //                 worksheet.Cells["O" + contador.ToString()].Value = rows["Analista"].ToString();
                   //                 worksheet.Cells.AutoFitColumns();

                                
                   //         }
                   //         else
                   //         {
                   //             code = "500";
                   //             message = errorSistema;

                   //             string[] paramError =
                   //             {
                   //                 "@AUTHENTICATE",
                   //                 "@AGENTAPP",
                   //                 "@SISTEMA",
                   //                 "@AMBIENTE",
                   //                 "@MODULO",
                   //                 "@TIPOERROR",
                   //                 "@EXCEPCION",
                   //                 "@ERRORLINE",
                   //                 "@ERRORMESSAGE",
                   //                 "@ERRORNUMBER",
                   //                 "@ERRORPROCEDURE",
                   //                 "@ERRORSEVERITY",
                   //                 "@ERRORSTATE"
                   //             };

                   //             string[] valoresError =
                   //             {
                   //                 tokenAuth,
                   //                 agenteAplication,
                   //                 sistema,
                   //                 ambiente,
                   //                 "HttPost | JsonResult | CreaSolicitudBaja",
                   //                 "Procedimiento Almacenado",
                   //                 "",
                   //                 rows["ErrorLine"].ToString(),
                   //                 rows["ErrorMessage"].ToString(),
                   //                 rows["ErrorNumber"].ToString(),
                   //                 rows["ErrorProcedure"].ToString(),
                   //                 rows["ErrorSeverity"].ToString(),
                   //                 rows["ErrorState"].ToString()
                   //             };

                   //             DataSet dataError = servicioAuth.SetControlErroresSistemas(paramError, valoresError).Table;

                   //             foreach (DataRow rowsError in dataError.Tables[0].Rows)
                   //             {
                   //                 servicioCorreo.correoTeamworkEstandarCCO(rowsError["De"].ToString(), rowsError["Clave"].ToString(), rowsError["Para"].ToString(),
                   //                                                          rowsError["Html"].ToString(), rowsError["Asunto"].ToString(), rowsError["CC"].ToString(),
                   //                                                          rowsError["CCO"].ToString(), "", rowsError["Importancia"].ToString(), "N");
                   //             }
                   //         }
                   //     }
                   // }
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
                    "HttPost | JsonResult | CreaSolicitudBaja",
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

            return Json(new { Code = code, Message = message, PathFile = nombreArchivo });
        }

        [HttpPost]
        public JsonResult CreaSolicitudContrato(string codigoTransaccion)
        {
            string path = string.Empty;
            string nombreArchivo = string.Empty;
            string code = string.Empty;
            string message = string.Empty;

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
                    /** seteo de solicitud creacion */
                    Session["ApplicationExcelSolicitud"] = null;
                    Session["ApplicationExcelSolicitudName"] = "";

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion
                    };

                    DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudContrato(parametrosRender, valoresRender).Table;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                code = "200";

                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add("Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];

                                    string[] columnas =
                                    {
                                        "A",
                                        "B",
                                        "C",
                                        "D",
                                        "E",
                                        "F",
                                        "G",
                                        "H",
                                        "I",
                                        "J",
                                        "K",
                                        "L",
                                        "M",
                                        "N",
                                        "O",
                                        "P",
                                        "Q",
                                        "R",
                                        "S",
                                        "T",
                                        "U",
                                        "V",
                                        "W",
                                        "X",
                                        "Y",
                                        "Z",
                                        "AA",
                                        "AB",
                                        "AC",
                                        "AD",
                                        "AE",
                                        "AF",
                                        "AG",
                                        "AH",
                                        "AI"
                                    };

                                    string[] nameColumnas =
                                    {
                                        "Fecha Transacción",
                                        "Ficha",
                                        "Area Negocio",
                                        "Rut Empresa",
                                        "Rut Trabajador",
                                        "Nombre Trabajador",
                                        "Codigo BPO",
                                        "Cargo Mod",
                                        "Cargo",
                                        "Sucursal",
                                        "Ejecutivo",
                                        "Fecha de Inicio",
                                        "Fecha de Termino",
                                        "Causal",
                                        "Reemplazo",
                                        "TipoContrato",
                                        "CC",
                                        "Dirección",
                                        "Comuna",
                                        "Ciudad",
                                        "Región",
                                        "Horario",
                                        "Horas Trabajadas",
                                        "Colación",
                                        "Nacionalidad",
                                        "Visa",
                                        "Vencimiento Visa",
                                        "Cliente Firmante",
                                        "División (Solo FNC)",
                                        "Descripcion de funciones",
                                        "Fecha Inicio Primer Plazo",
                                        "Fecha Término Primer Plazo",
                                        "Fecha Inicio Segundo Plazo",
                                        "Fecha Término Segundo Plazo",
                                        "Renovacion Automática"
                                    };

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["FechaTransaccion"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["Ficha"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["Codigo"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["RutEmpresa"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["Rut"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["Nombre"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["CargoBPO"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["CargoMod"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["Cargo"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["Sucursal"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["Ejecutivo"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["FechaInicio"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["Causal"].ToString();
                                worksheet.Cells["O" + contador.ToString()].Value = rows["Reemplazo"].ToString();
                                worksheet.Cells["P" + contador.ToString()].Value = rows["TipoContrato"].ToString();
                                worksheet.Cells["Q" + contador.ToString()].Value = rows["CC"].ToString();
                                worksheet.Cells["R" + contador.ToString()].Value = rows["Direccion"].ToString();
                                worksheet.Cells["S" + contador.ToString()].Value = rows["Comuna"].ToString();
                                worksheet.Cells["T" + contador.ToString()].Value = rows["Ciudad"].ToString();
                                worksheet.Cells["U" + contador.ToString()].Value = rows["Region"].ToString();
                                worksheet.Cells["V" + contador.ToString()].Value = rows["Horario"].ToString();
                                worksheet.Cells["W" + contador.ToString()].Value = rows["HorasTrab"].ToString();
                                worksheet.Cells["X" + contador.ToString()].Value = rows["Colacion"].ToString();
                                worksheet.Cells["Y" + contador.ToString()].Value = rows["Nacionalidad"].ToString();
                                worksheet.Cells["Z" + contador.ToString()].Value = rows["Visa"].ToString();
                                worksheet.Cells["AA" + contador.ToString()].Value = rows["VencVisa"].ToString();
                                worksheet.Cells["AB" + contador.ToString()].Value = rows["Firmante"].ToString();
                                worksheet.Cells["AC" + contador.ToString()].Value = rows["Division"].ToString();
                                worksheet.Cells["AD" + contador.ToString()].Value = rows["DescripcionFunciones"].ToString();
                                worksheet.Cells["AE" + contador.ToString()].Value = rows["FechaInicioPPlazo"].ToString();
                                worksheet.Cells["AF" + contador.ToString()].Value = rows["FechaTerminoPPlazo"].ToString();
                                worksheet.Cells["AG" + contador.ToString()].Value = rows["FechaInicioSPlazo"].ToString();
                                worksheet.Cells["AH" + contador.ToString()].Value = rows["FechaTerminoSPlazo"].ToString();
                                worksheet.Cells["AI" + contador.ToString()].Value = rows["RenovacionAutomatica"].ToString();

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
                                    "HttPost | JsonResult | CreaSolicitudContrato",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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

                        if (code == "200")
                        {
                            nombreArchivo = "Solicitud Contrato " + codigoTransaccion + ".xlsx";

                            byte[] data = excel.GetAsByteArray();

                            Session["ApplicationExcelSolicitud"] = data;
                            Session["ApplicationExcelSolicitudName"] = nombreArchivo;

                            //string ruta = @"\\SATW\FichaAlta\";
                            //string ruta = @"\\FILESERVER\Ficha Alta\";
                            //string nombreCarpeta = "tmp" + Session["NombreUsuario"].ToString().ToLower() + @"\";
                            //string rutaCarpeta = ruta + nombreCarpeta;
                            //if (!Directory.Exists(rutaCarpeta))
                            //{
                            //    Directory.CreateDirectory(ruta + nombreCarpeta);
                            //}

                            //path = ruta + nombreCarpeta + nombreArchivo;

                            //System.IO.File.WriteAllBytes(path, data);
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
                    "HttPost | JsonResult | CreaSolicitudContrato",
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

            return Json(new { Code = code, Message = message, PathFile = nombreArchivo });
        }

        [HttpPost]
        public JsonResult LoaderEnviarSolicitud(string codigoTransaccion, string plantilla)
        {
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@PLANTILLA"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        plantilla
                    };

                    DataSet dataProceso = servicioOperaciones.GetRenderLoaderEnvioSolicitud(parametrosRender, valoresRender).Table;

                    foreach (DataRow rowsProceso in dataProceso.Tables[0].Rows)
                    {
                        if (rowsProceso["Code"].ToString() == "200")
                        {
                            code = rowsProceso["Code"].ToString();
                            message = rowsProceso["Message"].ToString();
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
                                "HttPost | JsonResult | LoaderEnviarSolicitud",
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
                    "HttPost | JsonResult | LoaderEnviarSolicitud",
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

            return Json(new { Code = code, Message = message });
        }

        [HttpPost]
        public JsonResult EnviaSolicitudRenovacion(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvUmVub3ZhY2lvblZvdWNoZXI=",
                        "Q29ycmVvUmVub3ZhY2lvblNvbGljaXR1ZA=="
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200") {

                                if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                                rows["Clave"].ToString(),
                                                                                rows["Para"].ToString(),
                                                                                rows["Html"].ToString(),
                                                                                rows["Asunto"].ToString(),
                                                                                rows["CC"].ToString(),
                                                                                rows["CCO"].ToString(),
                                                                                rows["Attachement"].ToString(),
                                                                                rows["Importancia"].ToString(),
                                                                                (byte[])Session["ApplicationExcelSolicitud"],
                                                                                "application/vnd.ms-excel"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudRenovacion | Correo No Enviado | Para => " + 
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudRenovacion",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudRenovacion",
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

            return Json(new { Code = code, Message = message });
        }
        
        [HttpPost]
        public JsonResult EnviaSolicitudContrato(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvQ29udHJhdG9Tb2xpY2l0dWQ=",
                        "Q29ycmVvU29saWNpdHVkVm91Y2hlcg=="
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                code = "200";
                                if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                                rows["Clave"].ToString(),
                                                                                rows["Para"].ToString(),
                                                                                rows["Html"].ToString(),
                                                                                rows["Asunto"].ToString(),
                                                                                rows["CC"].ToString(),
                                                                                rows["CCO"].ToString(),
                                                                                rows["Attachement"].ToString(),
                                                                                rows["Importancia"].ToString(),
                                                                                (byte[])Session["ApplicationExcelSolicitud"],
                                                                                "application/vnd.ms-excel"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudContrato",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudContrato",
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

            return Json(new { Code = code });
        }

        [HttpPost]
        public JsonResult EnviaSolicitudBajas(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvQmFqYXNDb25maXJtYWRhcw=="
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                                rows["Clave"].ToString(),
                                                                                rows["Para"].ToString(),
                                                                                rows["Html"].ToString(),
                                                                                rows["Asunto"].ToString(),
                                                                                rows["CC"].ToString(),
                                                                                rows["CCO"].ToString(),
                                                                                rows["Attachement"].ToString(),
                                                                                rows["Importancia"].ToString(),
                                                                                (byte[])Session["ApplicationExcelSolicitud"],
                                                                                "application/vnd.ms-excel"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudContrato",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudContrato",
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

            return Json(new { Code = code });
        }

        [HttpPost]
        public JsonResult AnularProceso(string codigoTransaccion, string tipoProceso, string motivoAnulacion)
        {
            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] paramAnulacion =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TIPOPROCESO",
                        "@CODIGOTRANSACCION",
                        "@USUARIO",
                        "@MOTIVOANULACION"
                    };

                    string[] valAnulacion =
                    {
                        tokenAuth,
                        agenteAplication,
                        tipoProceso,
                        codigoTransaccion,
                        Session["NombreUsuario"].ToString(),
                        motivoAnulacion
                    };
                    
                    DataSet dataAnulacion = servicioOperaciones.SetAnularProceso(paramAnulacion, valAnulacion).Table;

                    foreach (DataRow rowsAnulacion in dataAnulacion.Tables[0].Rows)
                    {
                        if (rowsAnulacion["Code"].ToString() == "200")
                        {
                            code = rowsAnulacion["Code"].ToString();
                            message = rowsAnulacion["Message"].ToString();
                            path = rowsAnulacion["Path"].ToString();
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
                                "HttPost | JsonResult | AnularProceso",
                                "Procedimiento Almacenado",
                                "",
                                rowsAnulacion["ErrorLine"].ToString(),
                                rowsAnulacion["ErrorMessage"].ToString(),
                                rowsAnulacion["ErrorNumber"].ToString(),
                                rowsAnulacion["ErrorProcedure"].ToString(),
                                rowsAnulacion["ErrorSeverity"].ToString(),
                                rowsAnulacion["ErrorState"].ToString()
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
                    "HttPost | JsonResult | AnularProceso",
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

            return Json(new { Code = code, Message = message, Path = path });
        }

        [HttpPost]
        public JsonResult TerminarProceso(string codigoTransaccion, string tipoProceso)
        {
            string code = string.Empty;
            string message = string.Empty;
            string proceso = string.Empty;
            string glyphicon = string.Empty;
            string glyphiconColor = string.Empty;
            string estado = string.Empty;
            string border = string.Empty;

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] paramTerminado =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TIPOPROCESO",
                        "@CODIGOTRANSACCION",
                        "@TRABAJADOR"
                    };

                    string[] valTerminado =
                    {
                        tokenAuth,
                        agenteAplication,
                        tipoProceso,
                        codigoTransaccion,
                        Session["NombreUsuario"].ToString()
                    };

                    DataSet dataTerminado = servicioOperaciones.SetTerminarProceso(paramTerminado, valTerminado).Table;

                    foreach (DataRow rowsTerminado in dataTerminado.Tables[0].Rows)
                    {
                        if (rowsTerminado["Code"].ToString() == "200")
                        {
                            code = rowsTerminado["Code"].ToString();
                            message = rowsTerminado["Message"].ToString();
                            proceso = rowsTerminado["Proceso"].ToString();

                            glyphicon = rowsTerminado["Glyphicon"].ToString();
                            glyphiconColor = rowsTerminado["GlyphiconColor"].ToString();
                            estado = rowsTerminado["Estado"].ToString();
                            border = rowsTerminado["Border"].ToString();
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
                                "HttPost | JsonResult | TerminarProceso",
                                "Procedimiento Almacenado",
                                "",
                                rowsTerminado["ErrorLine"].ToString(),
                                rowsTerminado["ErrorMessage"].ToString(),
                                rowsTerminado["ErrorNumber"].ToString(),
                                rowsTerminado["ErrorProcedure"].ToString(),
                                rowsTerminado["ErrorSeverity"].ToString(),
                                rowsTerminado["ErrorState"].ToString()
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
                    "HttPost | JsonResult | TerminarProceso",
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

            return Json(new { Code = code, Message = message, Proceso = proceso, Glyphicon = glyphicon, GlyphiconColor = glyphiconColor, Estado = estado, Border = border });
        }

        [HttpPost]
        public JsonResult RevertirTerminoProceso(string codigoTransaccion, string tipoProceso, string proceso)
        {
            string code = string.Empty;
            string message = string.Empty;
            string glyphicon = string.Empty;
            string glyphiconColor = string.Empty;
            string estado = string.Empty;
            string border = string.Empty;

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] paramTerminado = 
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@TYPEFILTER",
                        "@DATAFILTER"
                    };

                    string[] valTerminado = 
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        tipoProceso,
                        "UHJvY2Vzbw"
                    };

                    DataSet dataLiberaTermino = servicioOperaciones.SetLiberaTerminoSolicitud(paramTerminado, valTerminado).Table;

                    foreach (DataRow rowsTerminado in dataLiberaTermino.Tables[0].Rows)
                    {
                        if (rowsTerminado["Code"].ToString() == "200" || rowsTerminado["Code"].ToString() == "400")
                        {
                            code = rowsTerminado["Code"].ToString();
                            message = rowsTerminado["Message"].ToString();
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
                                "HttPost | JsonResult | TerminarProceso",
                                "Procedimiento Almacenado",
                                "",
                                rowsTerminado["ErrorLine"].ToString(),
                                rowsTerminado["ErrorMessage"].ToString(),
                                rowsTerminado["ErrorNumber"].ToString(),
                                rowsTerminado["ErrorProcedure"].ToString(),
                                rowsTerminado["ErrorSeverity"].ToString(),
                                rowsTerminado["ErrorState"].ToString()
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
                    "HttPost | JsonResult | TerminarProceso",
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

            return Json(new { Code = code, Message = message, Proceso = proceso, Glyphicon = glyphicon, GlyphiconColor = glyphiconColor, Estado = estado, Border = border });
        }

        public JsonResult RevertirTerminoSolicitud(string codigoTransaccion, string tipoProceso, string proceso)
        {
            string code = string.Empty;
            string message = string.Empty;
            string glyphicon = string.Empty;
            string glyphiconColor = string.Empty;
            string estado = string.Empty;
            string border = string.Empty;

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] paramTerminado =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION",
                        "@TYPEFILTER",
                        "@DATAFILTER"
                    };

                    string[] valTerminado =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion,
                        tipoProceso,
                        "U29saWNpdHVk"
                    };

                    DataSet dataLiberaTermino = servicioOperaciones.SetLiberaTerminoSolicitud(paramTerminado, valTerminado).Table;

                    foreach (DataRow rowsTerminado in dataLiberaTermino.Tables[0].Rows)
                    {
                        if (rowsTerminado["Code"].ToString() == "200" || rowsTerminado["Code"].ToString() == "400")
                        {
                            code = rowsTerminado["Code"].ToString();
                            message = rowsTerminado["Message"].ToString();
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
                                "HttPost | JsonResult | TerminarProceso",
                                "Procedimiento Almacenado",
                                "",
                                rowsTerminado["ErrorLine"].ToString(),
                                rowsTerminado["ErrorMessage"].ToString(),
                                rowsTerminado["ErrorNumber"].ToString(),
                                rowsTerminado["ErrorProcedure"].ToString(),
                                rowsTerminado["ErrorSeverity"].ToString(),
                                rowsTerminado["ErrorState"].ToString()
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
                    "HttPost | JsonResult | TerminarProceso",
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

            return Json(new { Code = code, Message = message, Proceso = proceso, Glyphicon = glyphicon, GlyphiconColor = glyphiconColor, Estado = estado, Border = border });
        }

        [HttpPost]
        public JsonResult EnviaInformacionProcesoTerminado(string codigoTransaccion, string pathFile, string proceso)
        {
            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        proceso
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                
                                if (servicioCorreo.correoTeamworkEstandarCCO(rows["De"].ToString(),
                                                                             rows["Clave"].ToString(),
                                                                             rows["Para"].ToString(),
                                                                             rows["Html"].ToString(),
                                                                             rows["Asunto"].ToString(),
                                                                             rows["CC"].ToString(),
                                                                             rows["CCO"].ToString(),
                                                                             path,
                                                                             rows["Importancia"].ToString(),
                                                                             "N"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaInformacionProcesoTerminado | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaInformacionProcesoTerminado",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaInformacionProcesoTerminado",
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

            return Json(new { Code = code });
        }

        public JsonResult EnviaInformacionProcesoReversionTermino(string codigoTransaccion, string pathFile, string proceso, string tipoProceso, string tipo)
        {
            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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

                    string plantilla;
                    if (tipoProceso == "Q29udHJhdG8=")
                    {
                        if (tipo == "UHJvY2Vzbw==")
                            plantilla = "Q29ycmVvUmV2ZXJ0aXJUZXJtaW5vUHJvY2Vzb0NvbnQ=";
                        else
                            plantilla = "Q29ycmVvUmV2ZXJ0aXJUZXJtaW5vUHJvY2Vzb1Jlbm92";
                    }
                    else
                    {
                        if (tipo == "UHJvY2Vzbw==")
                            plantilla = "Q29ycmVvUmV2ZXJ0aXJUZXJtaW5vU29saWNpdHVkQ29udA==";
                        else
                            plantilla = "Q29ycmVvUmV2ZXJ0aXJUZXJtaW5vU29saWNpdHVkUmVub3Y=";
                    }


                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@PLANTILLACORREO",
                        "@CODIGOTRANSACCION",
                        "@PATHFILE"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        plantilla,
                        codigoTransaccion,
                        pathFile
                    };

                    DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                    foreach (DataRow rows in dataProceso.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {

                            if (servicioCorreo.correoTeamworkEstandarCCO(rows["De"].ToString(),
                                                                            rows["Clave"].ToString(),
                                                                            rows["Para"].ToString(),
                                                                            rows["Html"].ToString(),
                                                                            rows["Asunto"].ToString(),
                                                                            rows["CC"].ToString(),
                                                                            rows["CCO"].ToString(),
                                                                            path,
                                                                            rows["Importancia"].ToString(),
                                                                            "N"))
                            {
                                code = "200";
                            }
                            else
                            {
                                code = "700";
                                message = errorSistemaCorreo;

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
                                    "HttPost | JsonResult | EnviaInformacionProcesoTerminado | Correo No Enviado | Para => " +
                                    rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                    " | De => " + rows["De"].ToString(),
                                    "Correo Electronico",
                                    "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                "HttPost | JsonResult | EnviaInformacionProcesoTerminado",
                                "Procedimiento Almacenado",
                                "",
                                rows["ErrorLine"].ToString(),
                                rows["ErrorMessage"].ToString(),
                                rows["ErrorNumber"].ToString(),
                                rows["ErrorProcedure"].ToString(),
                                rows["ErrorSeverity"].ToString(),
                                rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaInformacionProcesoTerminado",
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

            return Json(new { Code = code });
        }

        [HttpPost]
        public JsonResult AnularSolicitud(string solicitud, string tipoSolicitud, string motivoAnulacion)
        {
            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

            try
            {
                if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
                {
                    string[] paramAnulacion =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TIPOSOLICITUD",
                        "@SOLICITUD",
                        "@USUARIO",
                        "@MOTIVOANULACION"
                    };

                    string[] valAnulacion =
                    {
                        tokenAuth,
                        agenteAplication,
                        tipoSolicitud,
                        solicitud,
                        Session["NombreUsuario"].ToString(),
                        motivoAnulacion
                    };

                    DataSet dataAnulacion = servicioOperaciones.SetAnularSolicitud(paramAnulacion, valAnulacion).Table;

                    foreach (DataRow rowsAnulacion in dataAnulacion.Tables[0].Rows)
                    {
                        if (rowsAnulacion["Code"].ToString() == "200")
                        {
                            code = rowsAnulacion["Code"].ToString();
                            message = rowsAnulacion["Message"].ToString();
                            path = rowsAnulacion["Path"].ToString();
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
                                "HttPost | JsonResult | AnularSolicitud",
                                "Procedimiento Almacenado",
                                "",
                                rowsAnulacion["ErrorLine"].ToString(),
                                rowsAnulacion["ErrorMessage"].ToString(),
                                rowsAnulacion["ErrorNumber"].ToString(),
                                rowsAnulacion["ErrorProcedure"].ToString(),
                                rowsAnulacion["ErrorSeverity"].ToString(),
                                rowsAnulacion["ErrorState"].ToString()
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
                    "HttPost | JsonResult | AnularSolicitud",
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

            return Json(new { Code = code, Message = message, Path = path });
        }

        [HttpPost]
        public JsonResult CreaSolicitudContratoAnulada(string codigoTransaccion)
        {
            string path = string.Empty;
            string nombreArchivo = string.Empty;
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion
                    };

                    DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudContratoAnulada(parametrosRender, valoresRender).Table;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                code = "200";

                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add("Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];

                                    string[] columnas =
                                    {
                                        "A",
                                        "B",
                                        "C",
                                        "D",
                                        "E",
                                        "F",
                                        "G",
                                        "H",
                                        "I",
                                        "J",
                                        "K",
                                        "L",
                                        "M",
                                        "N",
                                        "O",
                                        "P",
                                        "Q",
                                        "R",
                                        "S",
                                        "T",
                                        "U",
                                        "V",
                                        "W",
                                        "X",
                                        "Y",
                                        "Z",
                                        "AA",
                                        "AB",
                                        "AC",
                                        "AD",
                                        "AF",
                                        "AG",
                                        "AH",
                                        "AI",
                                        "AJ"
                                    };

                                    string[] nameColumnas =
                                    {
                                        "Observación Anulación",
                                        "Fecha Transacción",
                                        "Ficha",
                                        "Area Negocio",
                                        "Rut Empresa",
                                        "Rut Trabajador",
                                        "Nombre Trabajador",
                                        "Codigo BPO",
                                        "Cargo Mod",
                                        "Cargo",
                                        "Sucursal",
                                        "Ejecutivo",
                                        "Fecha de Inicio",
                                        "Fecha de Termino",
                                        "Causal",
                                        "Reemplazo",
                                        "TipoContrato",
                                        "CC",
                                        "Dirección",
                                        "Comuna",
                                        "Ciudad",
                                        "Región",
                                        "Horario",
                                        "Horas Trabajadas",
                                        "Colación",
                                        "Nacionalidad",
                                        "Visa",
                                        "Vencimiento Visa",
                                        "Cliente Firmante",
                                        "División (Solo FNC)",
                                        "Descripcion de funciones",
                                        "Fecha Inicio Primer Plazo",
                                        "Fecha Término Primer Plazo",
                                        "Fecha Inicio Segundo Plazo",
                                        "Fecha Término Segundo Plazo",
                                        "Renovacion Automática"
                                    };

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["MotivoAnulacion"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["FechaTransaccion"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["Ficha"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["Codigo"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["RutEmpresa"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["Rut"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["Nombre"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["CargoBPO"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["CargoMod"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["Cargo"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["Sucursal"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["Ejecutivo"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["FechaInicio"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["FechaTermino"].ToString();
                                worksheet.Cells["O" + contador.ToString()].Value = rows["Causal"].ToString();
                                worksheet.Cells["P" + contador.ToString()].Value = rows["Reemplazo"].ToString();
                                worksheet.Cells["Q" + contador.ToString()].Value = rows["TipoContrato"].ToString();
                                worksheet.Cells["R" + contador.ToString()].Value = rows["CC"].ToString();
                                worksheet.Cells["S" + contador.ToString()].Value = rows["Direccion"].ToString();
                                worksheet.Cells["T" + contador.ToString()].Value = rows["Comuna"].ToString();
                                worksheet.Cells["U" + contador.ToString()].Value = rows["Ciudad"].ToString();
                                worksheet.Cells["V" + contador.ToString()].Value = rows["Region"].ToString();
                                worksheet.Cells["W" + contador.ToString()].Value = rows["Horario"].ToString();
                                worksheet.Cells["X" + contador.ToString()].Value = rows["HorasTrab"].ToString();
                                worksheet.Cells["Y" + contador.ToString()].Value = rows["Colacion"].ToString();
                                worksheet.Cells["Z" + contador.ToString()].Value = rows["Nacionalidad"].ToString();
                                worksheet.Cells["AA" + contador.ToString()].Value = rows["Visa"].ToString();
                                worksheet.Cells["AB" + contador.ToString()].Value = rows["VencVisa"].ToString();
                                worksheet.Cells["AC" + contador.ToString()].Value = rows["Firmante"].ToString();
                                worksheet.Cells["AD" + contador.ToString()].Value = rows["Division"].ToString();
                                worksheet.Cells["AE" + contador.ToString()].Value = rows["DescripcionFunciones"].ToString();
                                worksheet.Cells["AF" + contador.ToString()].Value = rows["FechaInicioPPlazo"].ToString();
                                worksheet.Cells["AG" + contador.ToString()].Value = rows["FechaTerminoPPlazo"].ToString();
                                worksheet.Cells["AH" + contador.ToString()].Value = rows["FechaInicioSPlazo"].ToString();
                                worksheet.Cells["AI" + contador.ToString()].Value = rows["FechaTerminoSPlazo"].ToString();
                                worksheet.Cells["AJ" + contador.ToString()].Value = rows["RenovacionAutomatica"].ToString();

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
                                    "HttPost | JsonResult | CreaSolicitudContratoAnulada",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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

                        if (code == "200")
                        {
                            nombreArchivo = "Solicitud Contrato Anulada " + codigoTransaccion + ".xlsx";

                            byte[] data = excel.GetAsByteArray();

                            Session["ApplicationExcelSolicitud"] = data;
                            Session["ApplicationExcelSolicitudName"] = nombreArchivo;
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
                    "HttPost | JsonResult | CreaSolicitudContrato",
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

            return Json(new { Code = code, Message = message, PathFile = nombreArchivo });
        }

        [HttpPost]
        public JsonResult EnviaSolicitudContratoAnulado(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvU29saWNpdHVkQW51bGFkb1ZvdWNoZXI=",
                        "Q29ycmVvQ29udHJhdG9Tb2xpY2l0dWRBbnVsYWRv"
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                                rows["Clave"].ToString(),
                                                                                rows["Para"].ToString(),
                                                                                rows["Html"].ToString(),
                                                                                rows["Asunto"].ToString(),
                                                                                rows["CC"].ToString(),
                                                                                rows["CCO"].ToString(),
                                                                                rows["Attachement"].ToString(),
                                                                                rows["Importancia"].ToString(),
                                                                                (byte[])Session["ApplicationExcelSolicitud"],
                                                                                "application/vnd.ms-excel"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudContrato",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudContrato",
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

            return Json(new { Code = code });
        }

        [HttpPost]
        public JsonResult CreaSolicitudRenovacionAnulada(string codigoTransaccion)
        {
            string path = string.Empty;
            string nombreArchivo = string.Empty;
            string code = string.Empty;
            string message = string.Empty;

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

                    string[] parametrosRender =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@CODIGOTRANSACCION"
                    };

                    string[] valoresRender =
                    {
                        tokenAuth,
                        agenteAplication,
                        codigoTransaccion
                    };

                    DataSet dataProceso = servicioOperaciones.GetObtenerSolicitudRenovacionAnulada(parametrosRender, valoresRender).Table;

                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    using (ExcelPackage excel = new ExcelPackage())
                    {
                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {

                                code = "200";

                                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                                {
                                    excel.Workbook.Worksheets.Add("Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                                    var worksheetConfig = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];

                                    string[] columnas =
                                    {
                                        "A",
                                        "B",
                                        "C",
                                        "D",
                                        "E",
                                        "F",
                                        "G",
                                        "H",
                                        "I",
                                        "J",
                                        "K",
                                        "L",
                                        "M",
                                        "N"
                                    };

                                    string[] nameColumnas =
                                    {
                                        "Motivo Anulación",
                                        "Ficha",
                                        "Rut",
                                        "Nombre Completo",
                                        "Codigo BPO",
                                        "Cargo Mod",
                                        "Fecha Inicio Contrato",
                                        "Fecha Termino Contrato",
                                        "Causal",
                                        "Fecha Inicio Renovacion",
                                        "Fecha Termino Renovacion",
                                        "Tope Legal",
                                        "Area Negocio",
                                        "Empresa (Razon Social)"
                                    };

                                    for (var i = 0; i < columnas.Length; i++)
                                    {
                                        worksheetConfig.Cells[columnas[i] + "1"].Value = nameColumnas[i];
                                        worksheetConfig.Cells[columnas[i] + "1"].Style.Font.Bold = true;
                                    }

                                }

                                var worksheet = excel.Workbook.Worksheets["Renovaciones " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                                int contador = worksheet.Dimension.End.Row + 1;

                                worksheet.Cells["A" + contador.ToString()].Value = rows["MotivoAnulacion"].ToString();
                                worksheet.Cells["B" + contador.ToString()].Value = rows["Ficha"].ToString();
                                worksheet.Cells["C" + contador.ToString()].Value = rows["Rut"].ToString();
                                worksheet.Cells["D" + contador.ToString()].Value = rows["NombreCompleto"].ToString();
                                worksheet.Cells["E" + contador.ToString()].Value = rows["CargoBPO"].ToString();
                                worksheet.Cells["F" + contador.ToString()].Value = rows["CargoMod"].ToString();
                                worksheet.Cells["G" + contador.ToString()].Value = rows["FechaInicioContrato"].ToString();
                                worksheet.Cells["H" + contador.ToString()].Value = rows["FechaTerminoContrato"].ToString();
                                worksheet.Cells["I" + contador.ToString()].Value = rows["Causal"].ToString();
                                worksheet.Cells["J" + contador.ToString()].Value = rows["FechaInicioRenov"].ToString();
                                worksheet.Cells["K" + contador.ToString()].Value = rows["FechaTerminoRenov"].ToString();
                                worksheet.Cells["L" + contador.ToString()].Value = rows["TopeLegal"].ToString();
                                worksheet.Cells["M" + contador.ToString()].Value = rows["Codigo"].ToString();
                                worksheet.Cells["N" + contador.ToString()].Value = rows["Empresa"].ToString();
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
                                    "HttPost | JsonResult | CreaSolicitudRenovacion",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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

                        if (code == "200")
                        {

                            nombreArchivo = "Solicitud Renovacion Anulada " + codigoTransaccion + ".xlsx";
                                                       

                            byte[] data = excel.GetAsByteArray();

                            Session["ApplicationExcelSolicitud"] = data;
                            Session["ApplicationExcelSolicitudName"] = nombreArchivo;
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
                    "HttPost | JsonResult | CreaSolicitudRenovacion",
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

            return Json(new { Code = code, Message = message, PathFile = nombreArchivo });
        }

        [HttpPost]
        public JsonResult EnviaSolicitudRenovacionAnulado(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvUmVub3ZhY2lvbkFudWxhZG9Wb3VjaGVy",
                        "Q29ycmVvUmVub3ZhY2lvblNvbGljaXR1ZEFudWxhZG8="
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {

                                if (servicioCorreo.correoTeamworkCCOAttachament(rows["De"].ToString(),
                                                                                rows["Clave"].ToString(),
                                                                                rows["Para"].ToString(),
                                                                                rows["Html"].ToString(),
                                                                                rows["Asunto"].ToString(),
                                                                                rows["CC"].ToString(),
                                                                                rows["CCO"].ToString(),
                                                                                rows["Attachement"].ToString(),
                                                                                rows["Importancia"].ToString(),
                                                                                (byte[])Session["ApplicationExcelSolicitud"],
                                                                                "application/vnd.ms-excel"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudContrato",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudContrato",
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

            return Json(new { Code = code });
        }

        [HttpPost]
        public JsonResult EnviaSolicitudContratoAnuladoInd(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvQ29udHJhdG9Tb2xpY2l0dWRBbnVsYWRvSW5k",
                        "Q29ycmVvU29saWNpdHVkQW51bGFkb1ZvdWNoZXJJbmQ="
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                path = "";
                                //if (rows["Attachement"].ToString() != "")
                                //{
                                //    path = @"\\SATW\FichaAlta\" + "tmp" + Session["NombreUsuario"].ToString().ToLower() + @"\Anulaciones\" + rows["Attachement"].ToString();
                                //}
                                //else
                                //{
                                //    path = "";
                                //}

                                if (servicioCorreo.correoTeamworkEstandarCCO(rows["De"].ToString(),
                                                                             rows["Clave"].ToString(),
                                                                             rows["Para"].ToString(),
                                                                             rows["Html"].ToString(),
                                                                             rows["Asunto"].ToString(),
                                                                             rows["CC"].ToString(),
                                                                             rows["CCO"].ToString(),
                                                                             path,
                                                                             rows["Importancia"].ToString(),
                                                                             "N"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudContratoAnuladoInd",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudContratoAnuladoInd",
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

            return Json(new { Code = code });
        }
        
        [HttpPost]
        public JsonResult EnviaSolicitudRenovacionAnuladoInd(string codigoTransaccion, string pathFile)
        {

            string code = string.Empty;
            string message = string.Empty;
            string path = string.Empty;

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
                    string[] plantillasCorreo =
                    {
                        "Q29ycmVvUmVub3ZhY2lvblNvbGljaXR1ZEFudWxhZG9JbmQ=",
                        "Q29ycmVvUmVub3ZhY2lvbkFudWxhZG9Wb3VjaGVySW5k"
                    };

                    for (var i = 0; i < plantillasCorreo.Length; i++)
                    {
                        string[] parametrosRender =
                        {
                            "@AUTHENTICATE",
                            "@AGENTAPP",
                            "@PLANTILLACORREO",
                            "@CODIGOTRANSACCION",
                            "@PATHFILE"
                        };

                        string[] valoresRender =
                        {
                            tokenAuth,
                            agenteAplication,
                            plantillasCorreo[i],
                            codigoTransaccion,
                            pathFile
                        };

                        DataSet dataProceso = servicioOperaciones.GetPlantillasCorreos(parametrosRender, valoresRender).Table;

                        foreach (DataRow rows in dataProceso.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                path = "";

                                if (servicioCorreo.correoTeamworkEstandarCCO(rows["De"].ToString(),
                                                                             rows["Clave"].ToString(),
                                                                             rows["Para"].ToString(),
                                                                             rows["Html"].ToString(),
                                                                             rows["Asunto"].ToString(),
                                                                             rows["CC"].ToString(),
                                                                             rows["CCO"].ToString(),
                                                                             path,
                                                                             rows["Importancia"].ToString(),
                                                                             "N"))
                                {
                                    code = "200";
                                }
                                else
                                {
                                    code = "700";
                                    message = errorSistemaCorreo;

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
                                        "HttPost | JsonResult | EnviaSolicitudContrato | Correo No Enviado | Para => " +
                                        rows["Para"].ToString() + " | CC => " + rows["CC"].ToString() + " | CCO => " + rows["CCO"].ToString() +
                                        " | De => " + rows["De"].ToString(),
                                        "Correo Electronico",
                                        "No se ha enviado el correo electronico, debido a un problema en los destinatarios o el administrador de correos de teamwork.",
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
                                    "HttPost | JsonResult | EnviaSolicitudRenovacionAnuladoInd",
                                    "Procedimiento Almacenado",
                                    "",
                                    rows["ErrorLine"].ToString(),
                                    rows["ErrorMessage"].ToString(),
                                    rows["ErrorNumber"].ToString(),
                                    rows["ErrorProcedure"].ToString(),
                                    rows["ErrorSeverity"].ToString(),
                                    rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | EnviaSolicitudRenovacionAnuladoInd",
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

            return Json(new { Code = code });
        }
        
        [HttpPost]
        public JsonResult RenderizaProcesos(string pagination, string tipoSolicitud, 
                                            string typeFilter, string dataFilter, string typeDashboard, 
                                            string day, string month, string year, string analista, string tipoProceso, string estado)
        {
            string code = string.Empty;
            List<Models.Proceso> procesosC = new List<Models.Proceso>();
            List<Models.Pagination> paginatorsC = new List<Models.Pagination>();
            List<Models.HeaderProcesos> headersProcesosC = new List<Models.HeaderProcesos>();

            /** HEADERS */

            string[] headerParamHeaderProcesosC =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOSOLICITUD",
                "@PAGINATION",
                "@TYPEFILTER",
                "@DATAFILTER"
            };

            string[] headerValoresHeaderProcesosC =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                tipoSolicitud,
                "MS01",
                "",
                ""
            };

            DataSet dataHeaderProcesosC = servicioOperaciones.GetObtenerHeaderProcesos(headerParamHeaderProcesosC, headerValoresHeaderProcesosC).Table;

            foreach (DataRow item in dataHeaderProcesosC.Tables[0].Rows)
            {
                Models.HeaderProcesos header = new Models.HeaderProcesos();

                header.PostTitulo = item["PostTitulo"].ToString();
                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                header.HtmlPagination = item["HtmlPagination"].ToString();
                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                header.TipoSolicitud = item["TipoSolicitud"].ToString();
                header.ResultSearch = item["ResultSearch"].ToString();

                headersProcesosC.Add(header);
            }

            /** PROCESOS */
            
            string[] headerParamProcesosC =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOSOLICITUD",
                "@PAGINATION",
                "@TYPEFILTER",
                "@DATAFILTER",
                "@TYPEDASHBOARD",
                "@DAYDASHBOARD",
                "@MONTHDASHBOARD",
                "@YEARDASHBOARD",
                "@ANALISTA",
                "@TIPOESTADO",
                "@ESTADO"
            };

            string[] headerValoresProcesosC =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                tipoSolicitud,
                pagination,
                typeFilter,
                dataFilter,
                typeDashboard,
                day,
                month,
                year,
                analista,
                tipoProceso,
                estado
            };

            DataSet dataProcesosC = servicioOperaciones.GetObtenerProcesosSolicitudes(headerParamProcesosC, headerValoresProcesosC).Table;

            foreach (DataRow rows in dataProcesosC.Tables[0].Rows)
            {
                Models.Proceso proceso = new Models.Proceso();

                if (rows["Code"].ToString() == "200")
                {
                    code = rows["Code"].ToString();
                    proceso.Code = rows["Code"].ToString();
                    proceso.Message = rows["Message"].ToString();

                    proceso.TipoEvento = rows["TipoEvento"].ToString();

                    proceso.CodigoTransaccion = rows["CodigoTransaccion"].ToString();
                    proceso.NombreProceso = rows["NombreProceso"].ToString();
                    proceso.Creado = rows["Creado"].ToString();
                    proceso.EjecutivoCreador = rows["EjecutivoCreador"].ToString();
                    proceso.TotalSolicitudes = rows["Solicitudes"].ToString();
                    proceso.Comentarios = rows["Comentarios"].ToString();
                    proceso.CodificarCod = rows["CodificarCod"].ToString();

                    proceso.Prioridad = rows["Prioridad"].ToString();
                    proceso.Glyphicon = rows["Glyphicon"].ToString();
                    proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    proceso.BorderColor = rows["BorderColor"].ToString();

                    proceso.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                    proceso.OptAsignarProceso = rows["OptAsignarProceso"].ToString();
                    proceso.OptDescargarSolicitudContrato = rows["OptDescargarSolicitudContrato"].ToString();
                    proceso.OptHistorialProceso = rows["OptHistorialProceso"].ToString();
                    proceso.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                    proceso.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                    proceso.OptAnularProceso = rows["OptAnularProceso"].ToString();
                    proceso.OptTerminarProceso = rows["OptTerminarProceso"].ToString();

                    proceso.OptRevertirTermino = rows["OptRevertirTermino"].ToString();
                }
                else
                {
                    if(rows["Code"].ToString() == "500")
                    {
                        code = rows["Code"].ToString();
                        proceso.Message = rows["ErrorMessage"].ToString();
                    }
                    else
                    {
                        code = rows["Code"].ToString();
                        proceso.Code = rows["Code"].ToString();
                        proceso.Message = rows["Message"].ToString();
                        proceso.Glyphicon = rows["Glyphicon"].ToString();
                    }
                }

                procesosC.Add(proceso);
            }
            
            /** PAGINATOR */

            string[] paramPaginatorProcesosC =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOSOLICITUD",
                "@PAGINATION",
                "@TYPEFILTER",
                "@DATAFILTER",
                "@TYPEDASHBOARD",
                "@DAYDASHBOARD",
                "@MONTHDASHBOARD",
                "@YEARDASHBOARD",
                "@ANALISTA",
                "@TIPOESTADO",
                "@ESTADO"
            };

            string[] valPaginatorProcesosC =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                tipoSolicitud,
                pagination,
                typeFilter,
                dataFilter,
                typeDashboard,
                day,
                month,
                year,
                analista,
                tipoProceso,
                estado
            };

            DataSet dataPaginadoC = servicioOperaciones.GetPaginatorProcesos(paramPaginatorProcesosC, valPaginatorProcesosC).Table;

            foreach (DataRow dataRowPaginadoC in dataPaginadoC.Tables[0].Rows)
            {
                if (dataRowPaginadoC["Code"].ToString() == "200")
                {
                    Models.Pagination paginado = new Models.Pagination();

                    paginado.Code = dataRowPaginadoC["Code"].ToString();
                    paginado.Message = dataRowPaginadoC["Message"].ToString();
                    paginado.FirstItem = dataRowPaginadoC["FirstItem"].ToString();
                    paginado.PreviousItem = dataRowPaginadoC["PreviousItem"].ToString();
                    paginado.Codificar = dataRowPaginadoC["Codificar"].ToString();
                    paginado.NumeroPagina = dataRowPaginadoC["NumeroPagina"].ToString();
                    paginado.NextItem = dataRowPaginadoC["NextItem"].ToString();
                    paginado.LastItem = dataRowPaginadoC["LastItem"].ToString();
                    paginado.TotalItems = dataRowPaginadoC["TotalItems"].ToString();
                    paginado.Active = dataRowPaginadoC["Activo"].ToString();

                    paginado.TipoSolicitud = dataRowPaginadoC["TipoSolicitud"].ToString();
                    paginado.HtmlRenderizado = dataRowPaginadoC["HtmlRenderizado"].ToString();
                    paginado.HtmlPagination = dataRowPaginadoC["HtmlPagination"].ToString();

                    paginado.HtmlSearchElement = dataRowPaginadoC["HtmlSearchElement"].ToString();
                    paginado.HtmlEventAction = dataRowPaginadoC["HtmlEventAction"].ToString();

                    paginatorsC.Add(paginado);
                }
            }
            
            return Json(new { Code = code, Procesos = procesosC, Pagination = paginatorsC, Headers = headersProcesosC });
        }

        [HttpPost]
        public JsonResult RenderizaSolicitudes(string pagination, string tipoSolicitud, string codigoTransaccion, string typeFilter, string dataFilter)
        {
            List<Models.Solicitud> solicitudes = new List<Models.Solicitud>();
            List<Models.Pagination> paginators = new List<Models.Pagination>();

            string[] headerParamSolictud =
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

            string[] headerValoresSolicitud =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                codigoTransaccion,
                tipoSolicitud,
                pagination,
                typeFilter,
                dataFilter
            };

            DataSet dataSolicitudes = servicioOperaciones.GetObtenerSolicitudes(headerParamSolictud, headerValoresSolicitud).Table;

            foreach (DataRow rows in dataSolicitudes.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Models.Solicitud solicitud = new Models.Solicitud();

                    solicitud.TipoEvento = rows["TipoEvento"].ToString();
                    solicitud.Code = rows["Code"].ToString();
                    solicitud.Message = rows["Message"].ToString();
                    solicitud.NombreSolicitud = rows["NombreSolicitud"].ToString();
                    solicitud.NombreProceso = rows["NombreProceso"].ToString();
                    solicitud.Creado = rows["Creado"].ToString();
                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
                    solicitud.Comentarios = rows["Comentarios"].ToString();
                    solicitud.CodificarCod = rows["CodificarCod"].ToString();

                    solicitud.CodigoSolicitud = rows["CodigoSolicitud"].ToString();

                    solicitud.Prioridad = rows["Prioridad"].ToString();
                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    solicitud.BorderColor = rows["BorderColor"].ToString();
                    solicitud.ColorFont = rows["ColorFont"].ToString();

                    solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                    solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                    solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                    solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                    solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                    solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                    solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                    solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                    solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                    solicitudes.Add(solicitud);
                }
            }

            /** PAGINATOR */

            string[] paramPaginatorSolicitud =
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

            string[] valPaginatorSolicitud =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                codigoTransaccion,
                tipoSolicitud,
                pagination,
                typeFilter,
                dataFilter
            };

            DataSet dataPaginado = servicioOperaciones.GetPaginatorSolicitudes(paramPaginatorSolicitud, valPaginatorSolicitud).Table;

            foreach (DataRow dataRowPaginado in dataPaginado.Tables[0].Rows)
            {
                if (dataRowPaginado["Code"].ToString() == "200")
                {
                    Models.Pagination paginado = new Models.Pagination();

                    paginado.Code = dataRowPaginado["Code"].ToString();
                    paginado.Message = dataRowPaginado["Message"].ToString();
                    paginado.FirstItem = dataRowPaginado["FirstItem"].ToString();
                    paginado.PreviousItem = dataRowPaginado["PreviousItem"].ToString();
                    paginado.Codificar = dataRowPaginado["Codificar"].ToString();
                    paginado.NumeroPagina = dataRowPaginado["NumeroPagina"].ToString();
                    paginado.NextItem = dataRowPaginado["NextItem"].ToString();
                    paginado.LastItem = dataRowPaginado["LastItem"].ToString();
                    paginado.TotalItems = dataRowPaginado["TotalItems"].ToString();
                    paginado.Active = dataRowPaginado["Activo"].ToString();

                    paginado.TipoSolicitud = dataRowPaginado["TipoSolicitud"].ToString();
                    paginado.HtmlRenderizado = dataRowPaginado["HtmlRenderizado"].ToString();
                    paginado.HtmlPagination = dataRowPaginado["HtmlPagination"].ToString();

                    paginado.HtmlSearchElement = dataRowPaginado["HtmlSearchElement"].ToString();
                    paginado.HtmlEventAction = dataRowPaginado["HtmlEventAction"].ToString();

                    paginators.Add(paginado);
                }
            }

            return Json(new { Code = "200", Message = "OK", Solicitudes = solicitudes, Pagination = paginators });
        }

        [HttpPost]
        public JsonResult HeaderProcesos(string tipoSolicitud, string typeFilter, string dataFilter)
        {

            List<Models.HeaderProcesos> headersProcesos = new List<Models.HeaderProcesos>();

            string[] headerParamHeaderProcesosC =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOSOLICITUD",
                "@PAGINATION",
                "@TYPEFILTER",
                "@DATAFILTER"
            };

            string[] headerValoresHeaderProcesosC =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                tipoSolicitud,
                "MS01",
                typeFilter,
                dataFilter
            };

            DataSet dataHeaderProcesos = servicioOperaciones.GetObtenerHeaderProcesos(headerParamHeaderProcesosC, headerValoresHeaderProcesosC).Table;

            foreach (DataRow item in dataHeaderProcesos.Tables[0].Rows)
            {
                Models.HeaderProcesos header = new Models.HeaderProcesos();

                header.PostTitulo = item["PostTitulo"].ToString();
                header.HtmlRenderizado = item["HtmlRenderizado"].ToString();
                header.HtmlPagination = item["HtmlPagination"].ToString();
                header.HtmlSearchElement = item["HtmlSearchElement"].ToString();
                header.TipoSolicitud = item["TipoSolicitud"].ToString();
                header.ResultSearch = item["ResultSearch"].ToString();

                headersProcesos.Add(header);
            }

            return Json(new { Code = "200", Message = "OK", HeaderProcesos = headersProcesos });
        }

        [HttpPost]
        public ActionResult ViewPartialProcesos(string TipoProceso, string codificarCod, string borderColor, string glyphiconColor, string glyphicon, string prioridad, string nombre,
                                                string fechaCreacion, string ejecutivoSeguimiento, string totalSolicitudes, string colorComentarios, string comentarios,
                                                string codigoTransaccion, string tipoEvento, string optDescargarDatosCargados, string optAsignarProceso, 
                                                string optDescargarSolicitudContrato, string optHistorialProceso, string optRevertirAnulacion, string optDescargarErrorDatosCargados, 
                                                string optAnularProceso, string optTerminarProceso, string optRevertirTermino)
        {
            ViewBag.TipoProceso = TipoProceso;
            ViewBag.CodificarCod = codificarCod;
            ViewBag.BorderColor = borderColor;
            ViewBag.GlyphiconColor = glyphiconColor;
            ViewBag.Glyphicon = glyphicon;
            ViewBag.Prioridad = prioridad;
            ViewBag.Nombre = nombre;
            ViewBag.FechaCreacion = fechaCreacion;
            ViewBag.EjecutivoSeguimiento = ejecutivoSeguimiento;
            ViewBag.TotalSolicitudes = totalSolicitudes;
            ViewBag.ColorComentarios = colorComentarios;
            ViewBag.Comentarios = comentarios;
            ViewBag.CodigoTransaccion = codigoTransaccion;
            ViewBag.TipoEvento = tipoEvento;
            ViewBag.OptDescargarDatosCargados = optDescargarDatosCargados;
            ViewBag.OptAsignarProceso = optAsignarProceso;
            ViewBag.OptDescargarSolicitudContrato = optDescargarSolicitudContrato;
            ViewBag.OptHistorialProceso = optHistorialProceso;
            ViewBag.OptRevertirAnulacion = optRevertirAnulacion;
            ViewBag.OptDescargarErrorDatosCargados = optDescargarErrorDatosCargados;
            ViewBag.OptAnularProceso = optAnularProceso;
            ViewBag.OptTerminarProceso = optTerminarProceso;
            ViewBag.OptRevertirTermino = optRevertirTermino;

            return PartialView("_ProcesosHttpPost");
        }
        
        [HttpPost]
        public ActionResult ViewPartialSolicitudes(string nombreSolicitud, string nombreProceso, string tipoEvento, string creado, string fechasCompromiso, string comentarios, 
                                                    string codificarCod, string codigoSolicitud,
                                                   string prioridad, string glyphicon, string glyphiconColor, string borderColor, string colorFont, string renderizadoOpciones,
                                                   string optDescargarDatosCargados,
                                                   string optAsignarSolicitud, string optDescargarSolicitudContratoIndividual, string optHistorialSolicitud, string optRevertirAnulacion,
                                                   string optDescargarErrorDatosCargados, string optAnularSolicitud, string optRevertirTermino)
        {
            ViewBag.TipoEvento = tipoEvento;
            ViewBag.NombreSolicitud = nombreSolicitud;
            ViewBag.NombreProceso = nombreProceso;
            ViewBag.FechaCreacion = creado;
            ViewBag.FechasCompromiso = fechasCompromiso;
            ViewBag.Comentarios = comentarios;
            ViewBag.CodigoSolicitud = codigoSolicitud;
            ViewBag.CodificarCod = codificarCod;
            ViewBag.Prioridad = prioridad;
            ViewBag.Glyphicon = glyphicon;
            ViewBag.GlyphiconColor = glyphiconColor;
            ViewBag.BorderColor = borderColor;
            ViewBag.ColorFont = colorFont;
            ViewBag.RenderizadoOpciones = renderizadoOpciones;
            ViewBag.OptDescargarDatosCargados = optDescargarDatosCargados;
            ViewBag.OptAsignarSolicitud = optAsignarSolicitud;
            ViewBag.OptDescargarSolicitudContratoIndividual = optDescargarSolicitudContratoIndividual;
            ViewBag.OptHistorialSolicitud = optHistorialSolicitud;
            ViewBag.OptRevertirAnulacion = optRevertirAnulacion;
            ViewBag.OptDescargarErrorDatosCargados = optDescargarErrorDatosCargados;
            ViewBag.OptAnularSolicitud = optAnularSolicitud;
            ViewBag.OptRevertirTermino = optRevertirTermino;

            return PartialView("_SolicitudesHttpPost");
        }


        [HttpPost]
        public ActionResult ViewPartialPagination(string firstItem, string previousItem, string codificar, string numeroPagina, string nextItem,
                                                  string lastItem, string totalItems, string active, string tipoSolicitud, string htmlRenderizado, string htmlPagination,
                                                  string htmlSearchElement, string htmlEventAction)
        {
            ViewBag.FirstItem = firstItem;
            ViewBag.PreviousItem = previousItem;
            ViewBag.Codificar = codificar;
            ViewBag.NumeroPagina = numeroPagina;
            ViewBag.NextItem = nextItem;
            ViewBag.LastItem = lastItem;
            ViewBag.TotalItems = totalItems;
            ViewBag.Active = active;
            ViewBag.TipoSolicitud = tipoSolicitud;
            ViewBag.HtmlRenderizado = htmlRenderizado;
            ViewBag.HtmlPagination = htmlPagination;
            ViewBag.HtmlSearchElement = htmlSearchElement;
            ViewBag.HtmlEventAction = htmlEventAction;

            return PartialView("_PaginationHttpPost");
        }

        [HttpPost]
        public ActionResult ViewPartialHeaderProcesos(string htmlSearchElement, string postTitulo, string tipoSolicitud, string htmlRenderizado, string htmlPagination, string resultSearch)
        {
            ViewBag.HtmlSearchElement = htmlSearchElement;
            ViewBag.PostTitulo = postTitulo;
            ViewBag.TipoSolicitud = tipoSolicitud;
            ViewBag.HtmlRenderizado = htmlRenderizado;
            ViewBag.HtmlPagination = htmlPagination;
            ViewBag.ResultSearch = resultSearch;

            return PartialView("_HeaderProcesosHttpPost");
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
        public ActionResult ViewPartialLoaderComplete(string message, string proceso)
        {
            ViewBag.HTMLLoaderError = message;
            ViewBag.Proceso = proceso;

            return PartialView("_CompletoGeneric");
        }

        [HttpPost]
        public ActionResult ViewPartialLoaderTransaccion()
        {
            return PartialView("_LoaderTransaccion");
        }

        [HttpPost]
        public JsonResult InformacionCuenta(string idCliente)
        {

            string code = string.Empty;
            string message = string.Empty;
            List<Models.Cliente> clientes = new List<Models.Cliente>();

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
                if (!string.IsNullOrEmpty(idCliente))
                {
                    string[] parametroCliente =
                    {
                        "@AUTHENTICATE",
                        "@AGENTAPP",
                        "@TRABAJADOR",
                        "@IDKAM"
                    };

                    string[] valorCliente =
                    {
                        tokenAuth,
                        agenteAplication,
                        "",
                        idCliente
                    };

                    DataSet informacionCliente = servicioOperaciones.GetInformacionCliente(parametroCliente, valorCliente).Table;

                    foreach (DataRow rows in informacionCliente.Tables[0].Rows)
                    {
                        Models.Cliente cliente = new Models.Cliente();

                        if (rows["Code"].ToString() == "200")
                        {
                            code = rows["Code"].ToString();
                            cliente.Code = rows["Code"].ToString();
                            cliente.Message = rows["Message"].ToString();
                            cliente.Codigo = rows["Codigo"].ToString();
                            cliente.Nombre = rows["Nombre"].ToString();
                            cliente.Apellido = rows["Apellido"].ToString();
                            cliente.Id = rows["Id"].ToString();
                            cliente.ClienteId = rows["ClienteId"].ToString();
                            cliente.NombreCliente = rows["NombreCliente"].ToString();
                            cliente.Rut = rows["Rut"].ToString();
                            cliente.Empresa = rows["Empresa"].ToString();

                            clientes.Add(cliente);
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
                                "HttPost | JsonResult | InformacionCuenta",
                                "Procedimiento Almacenado",
                                "",
                                rows["ErrorLine"].ToString(),
                                rows["ErrorMessage"].ToString(),
                                rows["ErrorNumber"].ToString(),
                                rows["ErrorProcedure"].ToString(),
                                rows["ErrorSeverity"].ToString(),
                                rows["ErrorState"].ToString()
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
                    "HttPost | JsonResult | InformacionCuenta",
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

            return Json(new { Code = code, Clientes = clientes });
        }
        
        [HttpPost]
        public JsonResult RenderizaAnexos(string dataFilter)
        {
            
            List<Models.Anexos> anexos = new List<Models.Anexos>();

            string[] paramRenderAnexos =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@FILTER"
            };

            string[] valRenderAnexos =
            {
                tokenAuth,
                agenteAplication,
                dataFilter
            };

            DataSet dataRenderAnexos = servicioOperaciones.GetObtenerRenderizadoDocAnexos(paramRenderAnexos, valRenderAnexos).Table;

            foreach (DataRow rows in dataRenderAnexos.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Models.Anexos anexo = new Models.Anexos();

                    anexo.Code = rows["Code"].ToString();
                    anexo.Message = rows["Message"].ToString();
                    anexo.RenderizadoTituloOne = rows["RenderizadoTituloOne"].ToString();
                    anexo.RenderizadoTituloTwo = rows["RenderizadoTituloTwo"].ToString();
                    anexo.RenderizadoDescripcion = rows["RenderizadoDescripcion"].ToString();
                    anexo.Glyphicon = rows["Glyphicon"].ToString();
                    anexo.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    anexo.BorderColor = rows["BorderColor"].ToString();
                    anexo.ColorFont = rows["ColorFont"].ToString();
                    anexo.ButtonClass = rows["ButtonClass"].ToString();
                    anexo.RenderizadoButtonOne = rows["RenderizadoButtonOne"].ToString();
                    anexo.RenderizadoButtonTwo = rows["RenderizadoButtonTwo"].ToString();
                    anexo.Path = rows["Path"].ToString();

                    anexos.Add(anexo);
                }
            }

            return Json(new { Code = "200", Message = "OK", Anexos = anexos });
        }

        [HttpPost]
        public ActionResult ViewPartialAnexos(string renderizadoTituloOne, string renderizadoTituloTwo, string renderizadoDescripcion, string glyphicon,
                                              string glyphiconColor,string borderColor, string colorFont,string buttonClass, string renderizadoButtonOne, 
                                              string renderizadoButtonTwo, string path)
        {
            ViewBag.RenderizadoTituloOne = renderizadoTituloOne;
            ViewBag.RenderizadoTituloTwo = renderizadoTituloTwo;
            ViewBag.RenderizadoDescripcion = renderizadoDescripcion;
            ViewBag.Glyphicon = glyphicon;
            ViewBag.GlyphiconColor = glyphiconColor;
            ViewBag.BorderColor = borderColor;
            ViewBag.ColorFont = colorFont;
            ViewBag.ButtonClass = buttonClass;
            ViewBag.RenderizadoButtonOne = renderizadoButtonOne;
            ViewBag.RenderizadoButtonTwo = renderizadoButtonTwo;
            ViewBag.Path = path;

            return PartialView("_AnexoHttpPost");
        }

        [HttpPost]
        public JsonResult RenderizaHeaderProcesos(string tipoSolicitud)
        {
            
            string result = string.Empty;

            string[] headerParamHeaderProcesos =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOSOLICITUD",
                "@PAGINATION",
                "@TYPEFILTER",
                "@DATAFILTER"
            };

            string[] headerValoresHeaderProcesos =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                tipoSolicitud,
                "MS01",
                "",
                ""
            };

            DataSet dataHeaderProcesos = servicioOperaciones.GetObtenerHeaderProcesos(headerParamHeaderProcesos, headerValoresHeaderProcesos).Table;

            foreach (DataRow item in dataHeaderProcesos.Tables[0].Rows)
            {
                result = item["ResultSearch"].ToString();
            }

            return Json(new { Code = "200", Result = result });
        }

        [HttpPost]
        public JsonResult RenderizaDashboardTotal(string tipoDashboard, string day, string month, string year)
        {
            List<Models.EstadisticaTotal> estadisticas = new List<Models.EstadisticaTotal>();
            List<Models.EstadisticaKam> estadisticasKam = new List<Models.EstadisticaKam>();
            List<Models.EstadisticaKamEmpresa> estadisticasKamEmpresa = new List<Models.EstadisticaKamEmpresa>();

            string dashboard = string.Empty;

            string[] paramIndiceEstadistico =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOINDICE",
                "@TIPODASHBOARD",
                "@DAYDASHBOARD",
                "@MONTHDASHBOARD",
                "@YEARDASHBOARD"
            };

            string[] valIndiceEstadistico =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                "VG90YWxlcw==",
                tipoDashboard,
                day,
                month,
                year
            };

            DataSet dataIndice = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadistico, valIndiceEstadistico).Table;

            foreach (DataRow rows in dataIndice.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    switch (rows["Dashboard"].ToString())
                    {
                        case "Kam":
                            switch (rows["TipoDashboard"].ToString())
                            {
                                case "Total":
                                    Models.EstadisticaKam estadisticaKam = new Models.EstadisticaKam();

                                    dashboard = rows["Dashboard"].ToString();

                                    estadisticaKam.Code = rows["Code"].ToString();
                                    estadisticaKam.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                    estadisticaKam.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                    estadisticaKam.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                    estadisticaKam.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                    estadisticaKam.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                    estadisticaKam.TotalProcesos = rows["TotalProcesos"].ToString();
                                    estadisticaKam.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                    estadisticaKam.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                    estadisticaKam.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                    estadisticaKam.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                    estadisticasKam.Add(estadisticaKam);
                                    break;
                                case "Empresa":
                                    Models.EstadisticaKamEmpresa estadisticaKamEmpresa = new Models.EstadisticaKamEmpresa();

                                    dashboard = rows["Dashboard"].ToString();
                                    estadisticaKamEmpresa.Code = rows["Code"].ToString();
                                    estadisticaKamEmpresa.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                    estadisticaKamEmpresa.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                    estadisticaKamEmpresa.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                    estadisticaKamEmpresa.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.TotalProcesos = rows["TotalProcesos"].ToString();
                                    estadisticaKamEmpresa.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                    estadisticaKamEmpresa.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                    estadisticaKamEmpresa.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                    estadisticaKamEmpresa.Concepto = rows["Concepto"].ToString();
                                    estadisticaKamEmpresa.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                    /** TERMINADAS */
                                    estadisticaKamEmpresa.EmpresaPCSSolContratos = rows["EmpresaPCSSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolRenovaciones = rows["EmpresaPCSSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolContratosPercentage = rows["EmpresaPCSSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolRenovacionesPercentage = rows["EmpresaPCSSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphiconColor = rows["EmpresaPCSSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphicon = rows["EmpresaPCSSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolBarColor = rows["EmpresaPCSSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolConcepto = rows["EmpresaPCSSolConcepto"].ToString();
                                    /** CRITICAS */
                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratos = rows["EmpresaCRITICOSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovaciones = rows["EmpresaCRITICOSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratosPercentage = rows["EmpresaCRITICOSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovacionesPercentage = rows["EmpresaCRITICOSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphiconColor = rows["EmpresaCRITICOSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphicon = rows["EmpresaCRITICOSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolBarColor = rows["EmpresaCRITICOSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolConcepto = rows["EmpresaCRITICOSolConcepto"].ToString();
                                    /** URGENTES */
                                    estadisticaKamEmpresa.EmpresaURGENTESolContratos = rows["EmpresaURGENTESolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovaciones = rows["EmpresaURGENTESolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolContratosPercentage = rows["EmpresaURGENTESolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovacionesPercentage = rows["EmpresaURGENTESolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphiconColor = rows["EmpresaURGENTESolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphicon = rows["EmpresaURGENTESolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolBarColor = rows["EmpresaURGENTESolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolConcepto = rows["EmpresaURGENTESolConcepto"].ToString();
                                    /** NORMALES */
                                    estadisticaKamEmpresa.EmpresaNORMALSolContratos = rows["EmpresaNORMALSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovaciones = rows["EmpresaNORMALSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolContratosPercentage = rows["EmpresaNORMALSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovacionesPercentage = rows["EmpresaNORMALSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphiconColor = rows["EmpresaNORMALSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphicon = rows["EmpresaNORMALSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolBarColor = rows["EmpresaNORMALSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolConcepto = rows["EmpresaNORMALSolConcepto"].ToString();
                                    /** SIN PRIORIDAD */
                                    estadisticaKamEmpresa.EmpresaSPSolContratos = rows["EmpresaSPSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolRenovaciones = rows["EmpresaSPSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolContratosPercentage = rows["EmpresaSPSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolRenovacionesPercentage = rows["EmpresaSPSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolGlyphiconColor = rows["EmpresaSPSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolGlyphicon = rows["EmpresaSPSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolBarColor = rows["EmpresaSPSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolConcepto = rows["EmpresaSPSolConcepto"].ToString();
                                    /** ANULADAS */
                                    estadisticaKamEmpresa.EmpresaANULADASolContratos = rows["EmpresaANULADASolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolRenovaciones = rows["EmpresaANULADASolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolContratosPercentage = rows["EmpresaANULADASolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolRenovacionesPercentage = rows["EmpresaANULADASolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphiconColor = rows["EmpresaANULADASolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphicon = rows["EmpresaANULADASolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolBarColor = rows["EmpresaANULADASolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolConcepto = rows["EmpresaANULADASolConcepto"].ToString();
                                    /** ERROR */
                                    estadisticaKamEmpresa.EmpresaERRORSolContratos = rows["EmpresaERRORSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolRenovaciones = rows["EmpresaERRORSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolContratosPercentage = rows["EmpresaERRORSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolRenovacionesPercentage = rows["EmpresaERRORSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphiconColor = rows["EmpresaERRORSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphicon = rows["EmpresaERRORSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolBarColor = rows["EmpresaERRORSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolConcepto = rows["EmpresaERRORSolConcepto"].ToString();

                                    estadisticasKamEmpresa.Add(estadisticaKamEmpresa);
                                    break;
                            }
                            break;
                        case "Contratos":
                            Models.EstadisticaTotal estadistica = new Models.EstadisticaTotal();

                            dashboard = rows["Dashboard"].ToString();

                            estadistica.Code = rows["Code"].ToString();
                            estadistica.Message = rows["Message"].ToString();
                            estadistica.Concepto = rows["Concepto"].ToString();
                            estadistica.TipoProceso = rows["TipoProceso"].ToString();
                            estadistica.Total = rows["Total"].ToString();
                            estadistica.Twest = rows["Twest"].ToString();
                            estadistica.Twrrhh = rows["Twrrhh"].ToString();
                            estadistica.Twc = rows["Twc"].ToString();

                            estadisticas.Add(estadistica);
                            break;
                    }
                }
            }

            return Json(new { Code = "200", Dashborad = dashboard, Estadisticas = estadisticas, EstadisticaKam = estadisticasKam, EstadisticasKamEmpresa = estadisticasKamEmpresa });
        }

        [HttpPost]
        public JsonResult RenderizaDashboardAnalistaContratos(string tipoDashboard, string day, string month, string year)
        {

            List<Models.EstadisticaXContratos> estadisticasXContrato = new List<Models.EstadisticaXContratos>();
            string code = string.Empty;

            string[] paramIndiceEstadisticoXContrato =
            {
                "@AUTHENTICATE",
                "@AGENTAPP",
                "@TRABAJADOR",
                "@TIPOINDICE",
                "@TIPODASHBOARD",
                "@DAYDASHBOARD",
                "@MONTHDASHBOARD",
                "@YEARDASHBOARD"
            };

            string[] valIndiceEstadisticoXContrato =
            {
                tokenAuth,
                agenteAplication,
                Session["NombreUsuario"].ToString(),
                "WEFuYWxpc3RhQ29udHJhdG9z",
                tipoDashboard,
                day,
                month,
                year
            };

            DataSet dataIndiceXContrato = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadisticoXContrato, valIndiceEstadisticoXContrato).Table;

            foreach (DataRow rows in dataIndiceXContrato.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    code = rows["Code"].ToString();

                    Models.EstadisticaXContratos estadistica = new Models.EstadisticaXContratos();

                    estadistica.Code = rows["Code"].ToString();
                    estadistica.Message = rows["Message"].ToString();
                    estadistica.Analista = rows["Analista"].ToString();
                    estadistica.ConceptoC = rows["ConceptoC"].ToString();
                    estadistica.CSolicitado = rows["CSolicitado"].ToString();
                    estadistica.CTerminado = rows["CTerminado"].ToString();
                    estadistica.CTotal = rows["CTotal"].ToString();
                    estadistica.CTwest = rows["CTwest"].ToString();
                    estadistica.CTwrrhh = rows["CTwrrhh"].ToString();
                    estadistica.CTwc = rows["CTwc"].ToString();
                    estadistica.ConceptoR = rows["ConceptoR"].ToString();
                    estadistica.RSolicitado = rows["RSolicitado"].ToString();
                    estadistica.RTerminado = rows["RTerminado"].ToString();
                    estadistica.RTotal = rows["RTotal"].ToString();
                    estadistica.RTwest = rows["RTwest"].ToString();
                    estadistica.RTwrrhh = rows["RTwrrhh"].ToString();
                    estadistica.RTwc = rows["RTwc"].ToString();

                    estadisticasXContrato.Add(estadistica);
                }
                else
                {
                    code = rows["Code"].ToString();
                }
            }

            return Json(new { Code = code, EstadisticasXContrato = estadisticasXContrato });
        }

        [HttpPost]
        public ActionResult ViewPartialDashboardSolicitudes(string total, string concepto, string tipoProceso, string twest, string twrrhh, string twc)
        {
            ViewBag.Total = total;
            ViewBag.Concepto = concepto;
            ViewBag.TipoProceso = tipoProceso;
            ViewBag.Twest = twest;
            ViewBag.Twrrhh = twrrhh;
            ViewBag.Twc = twc;

            return PartialView("_DashboardSolicitudesHttpPost");
        }

        public ActionResult ViewPartialDashboardTotal(string tipoDashboard, string day = "", string month = "", string year = "")
        {
            string dashboard = string.Empty;
            string dashboardTipo = string.Empty;

            List<Models.EstadisticaTotal> estadisticas = new List<Models.EstadisticaTotal>();
            List<Models.EstadisticaXContratos> estadisticasXContrato = new List<Models.EstadisticaXContratos>();
            List<Models.EstadisticaKam> estadisticasKam = new List<Models.EstadisticaKam>();
            List<Models.EstadisticaKamEmpresa> estadisticasKamEmpresa = new List<Models.EstadisticaKamEmpresa>();

            string[] paramIndiceEstadistico =
            {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR",
                    "@TIPOINDICE",
                    "@TIPODASHBOARD",
                    "@DAYDASHBOARD",
                    "@MONTHDASHBOARD",
                    "@YEARDASHBOARD"
                };

            string[] valIndiceEstadistico =
            {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),
                    "VG90YWxlcw==",
                    tipoDashboard,
                    day,
                    month,
                    year
                };

            DataSet dataIndice = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadistico, valIndiceEstadistico).Table;

            foreach (DataRow rows in dataIndice.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    switch (rows["Dashboard"].ToString())
                    {
                        case "Kam":
                            switch (rows["TipoDashboard"].ToString())
                            {
                                case "Total":
                                    Models.EstadisticaKam estadisticaKam = new Models.EstadisticaKam();

                                    dashboard = rows["Dashboard"].ToString();

                                    estadisticaKam.Code = rows["Code"].ToString();
                                    estadisticaKam.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                    estadisticaKam.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                    estadisticaKam.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                    estadisticaKam.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                    estadisticaKam.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                    estadisticaKam.TotalProcesos = rows["TotalProcesos"].ToString();
                                    estadisticaKam.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                    estadisticaKam.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                    estadisticaKam.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                    estadisticaKam.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                    estadisticasKam.Add(estadisticaKam);
                                    break;
                                case "Empresa":
                                    Models.EstadisticaKamEmpresa estadisticaKamEmpresa = new Models.EstadisticaKamEmpresa();

                                    dashboard = rows["Dashboard"].ToString();
                                    estadisticaKamEmpresa.Code = rows["Code"].ToString();
                                    estadisticaKamEmpresa.TotalSolicitudes = rows["TotalSolicitudes"].ToString();
                                    estadisticaKamEmpresa.TotalSolContratos = rows["TotalSolContratos"].ToString();
                                    estadisticaKamEmpresa.PercentageSolContratos = rows["PercentageSolContratos"].ToString();
                                    estadisticaKamEmpresa.TotalSolRenovaciones = rows["TotalSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.PercentageSolRenovaciones = rows["PercentageSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.TotalProcesos = rows["TotalProcesos"].ToString();
                                    estadisticaKamEmpresa.TotalProcContratos = rows["TotalProcContratos"].ToString();
                                    estadisticaKamEmpresa.PercentageProcContratos = rows["PercentageProcContratos"].ToString();
                                    estadisticaKamEmpresa.TotalProcRenovaciones = rows["TotalProcRenovaciones"].ToString();
                                    estadisticaKamEmpresa.Concepto = rows["Concepto"].ToString();
                                    estadisticaKamEmpresa.PercentageProcRenovaciones = rows["PercentageProcRenovaciones"].ToString();

                                    /** TERMINADAS */
                                    estadisticaKamEmpresa.EmpresaPCSSolContratos = rows["EmpresaPCSSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolRenovaciones = rows["EmpresaPCSSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolContratosPercentage = rows["EmpresaPCSSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolRenovacionesPercentage = rows["EmpresaPCSSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphiconColor = rows["EmpresaPCSSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolGlyphicon = rows["EmpresaPCSSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolBarColor = rows["EmpresaPCSSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaPCSSolConcepto = rows["EmpresaPCSSolConcepto"].ToString();
                                    /** CRITICAS */
                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratos = rows["EmpresaCRITICOSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovaciones = rows["EmpresaCRITICOSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolContratosPercentage = rows["EmpresaCRITICOSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolRenovacionesPercentage = rows["EmpresaCRITICOSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphiconColor = rows["EmpresaCRITICOSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolGlyphicon = rows["EmpresaCRITICOSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolBarColor = rows["EmpresaCRITICOSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaCRITICOSolConcepto = rows["EmpresaCRITICOSolConcepto"].ToString();
                                    /** URGENTES */
                                    estadisticaKamEmpresa.EmpresaURGENTESolContratos = rows["EmpresaURGENTESolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovaciones = rows["EmpresaURGENTESolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolContratosPercentage = rows["EmpresaURGENTESolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolRenovacionesPercentage = rows["EmpresaURGENTESolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphiconColor = rows["EmpresaURGENTESolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolGlyphicon = rows["EmpresaURGENTESolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolBarColor = rows["EmpresaURGENTESolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaURGENTESolConcepto = rows["EmpresaURGENTESolConcepto"].ToString();
                                    /** NORMALES */
                                    estadisticaKamEmpresa.EmpresaNORMALSolContratos = rows["EmpresaNORMALSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovaciones = rows["EmpresaNORMALSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolContratosPercentage = rows["EmpresaNORMALSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolRenovacionesPercentage = rows["EmpresaNORMALSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphiconColor = rows["EmpresaNORMALSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolGlyphicon = rows["EmpresaNORMALSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolBarColor = rows["EmpresaNORMALSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaNORMALSolConcepto = rows["EmpresaNORMALSolConcepto"].ToString();
                                    /** SIN PRIORIDAD */
                                    estadisticaKamEmpresa.EmpresaSPSolContratos = rows["EmpresaSPSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolRenovaciones = rows["EmpresaSPSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolContratosPercentage = rows["EmpresaSPSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolRenovacionesPercentage = rows["EmpresaSPSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolGlyphiconColor = rows["EmpresaSPSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolGlyphicon = rows["EmpresaSPSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolBarColor = rows["EmpresaSPSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaSPSolConcepto = rows["EmpresaSPSolConcepto"].ToString();
                                    /** ANULADAS */
                                    estadisticaKamEmpresa.EmpresaANULADASolContratos = rows["EmpresaANULADASolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolRenovaciones = rows["EmpresaANULADASolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolContratosPercentage = rows["EmpresaANULADASolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolRenovacionesPercentage = rows["EmpresaANULADASolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphiconColor = rows["EmpresaANULADASolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolGlyphicon = rows["EmpresaANULADASolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolBarColor = rows["EmpresaANULADASolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaANULADASolConcepto = rows["EmpresaANULADASolConcepto"].ToString();
                                    /** ERROR */
                                    estadisticaKamEmpresa.EmpresaERRORSolContratos = rows["EmpresaERRORSolContratos"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolRenovaciones = rows["EmpresaERRORSolRenovaciones"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolContratosPercentage = rows["EmpresaERRORSolContratosPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolRenovacionesPercentage = rows["EmpresaERRORSolRenovacionesPercentage"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphiconColor = rows["EmpresaERRORSolGlyphiconColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolGlyphicon = rows["EmpresaERRORSolGlyphicon"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolBarColor = rows["EmpresaERRORSolBarColor"].ToString();
                                    estadisticaKamEmpresa.EmpresaERRORSolConcepto = rows["EmpresaERRORSolConcepto"].ToString();

                                    estadisticasKamEmpresa.Add(estadisticaKamEmpresa);
                                    break;
                            }
                            break;
                        case "Contratos":
                            Models.EstadisticaTotal estadistica = new Models.EstadisticaTotal();

                            dashboard = rows["Dashboard"].ToString();

                            estadistica.Code = rows["Code"].ToString();
                            estadistica.Message = rows["Message"].ToString();
                            estadistica.Concepto = rows["Concepto"].ToString();
                            estadistica.TipoProceso = rows["TipoProceso"].ToString();
                            estadistica.Total = rows["Total"].ToString();
                            estadistica.Twest = rows["Twest"].ToString();
                            estadistica.Twrrhh = rows["Twrrhh"].ToString();
                            estadistica.Twc = rows["Twc"].ToString();
                            estadistica.Analista = rows["Analista"].ToString();

                            estadisticas.Add(estadistica);
                            break;
                    }
                }
            }

            switch (dashboard)
            {
                case "Kam":
                    ViewBag.RenderizadoIndiceEstadisticoTotal = estadisticasKam;
                    ViewBag.RenderizadoIndiceEstadisticoTotalEmpresa = estadisticasKamEmpresa;
                    ViewBag.RenderizaTipoDashboard = dashboard;
                    ViewBag.RenderizaOptionFiltrosDashboard = "403";
                    break;
                case "Contratos":
                    ViewBag.RenderizadoIndiceEstadisticoTotal = estadisticas;
                    ViewBag.RenderizaTipoDashboard = dashboard;
                    ViewBag.RenderizaOptionFiltrosDashboard = "200";
                    break;
            }

            string[] paramIndiceEstadisticoXContrato =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@TRABAJADOR",
                    "@TIPOINDICE",
                    "@TIPODASHBOARD",
                    "@DAYDASHBOARD",
                    "@MONTHDASHBOARD",
                    "@YEARDASHBOARD"
                };

            string[] valIndiceEstadisticoXContrato =
            {
                    tokenAuth,
                    agenteAplication,
                    Session["NombreUsuario"].ToString(),
                    "WEFuYWxpc3RhQ29udHJhdG9z",
                    tipoDashboard,
                    day,
                    month,
                    year
                };

            DataSet dataIndiceXContrato = servicioOperaciones.GetIndiceEstadisticoSolicitudes(paramIndiceEstadisticoXContrato, valIndiceEstadisticoXContrato).Table;

            foreach (DataRow rows in dataIndiceXContrato.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    ViewBag.RenderizaOptionEstadisticaXContrato = rows["Code"].ToString();

                    Models.EstadisticaXContratos estadistica = new Models.EstadisticaXContratos();

                    estadistica.Code = rows["Code"].ToString();
                    estadistica.Message = rows["Message"].ToString();
                    estadistica.Analista = rows["Analista"].ToString();
                    estadistica.ConceptoC = rows["ConceptoC"].ToString();
                    estadistica.CSolicitado = rows["CSolicitado"].ToString();
                    estadistica.CTerminado = rows["CTerminado"].ToString();
                    estadistica.CTotal = rows["CTotal"].ToString();
                    estadistica.CTwest = rows["CTwest"].ToString();
                    estadistica.CTwrrhh = rows["CTwrrhh"].ToString();
                    estadistica.CTwc = rows["CTwc"].ToString();
                    estadistica.ConceptoR = rows["ConceptoR"].ToString();
                    estadistica.RSolicitado = rows["RSolicitado"].ToString();
                    estadistica.RTerminado = rows["RTerminado"].ToString();
                    estadistica.RTotal = rows["RTotal"].ToString();
                    estadistica.RTwest = rows["RTwest"].ToString();
                    estadistica.RTwrrhh = rows["RTwrrhh"].ToString();
                    estadistica.RTwc = rows["RTwc"].ToString();

                    estadisticasXContrato.Add(estadistica);
                }
                else
                {
                    ViewBag.RenderizaOptionEstadisticaXContrato = rows["Code"].ToString();
                }
            }

            ViewBag.RenderizadoIndiceEstadisticoXContrato = estadisticasXContrato;

            ViewBag.CodeCorrect = true;

            return PartialView("_DashboardKam");
        }

        [HttpPost]
        public ActionResult ViewPartialDashboardAnalistaContartos(string analista, string totalC, string conceptoC, string cSolicitado, string cTerminado, string Ctwest, string Ctwrrhh, 
                                                                  string Ctwc, string totalR, string conceptoR, string rSolicitado, string rTerminado, string Rtwest, string Rtwrrhh,
                                                                  string Rtwc)
        {
            ViewBag.Analista = analista;
            ViewBag.TotalC = totalC;
            ViewBag.ConceptoC = conceptoC;
            ViewBag.CSolicitado = cSolicitado;
            ViewBag.CTerminado = cTerminado;
            ViewBag.CTwest = Ctwest;
            ViewBag.CTwrrhh = Ctwrrhh;
            ViewBag.CTwc = Ctwc;
            ViewBag.TotalR = totalR;
            ViewBag.ConceptoR = conceptoR;
            ViewBag.RSolicitado = rSolicitado;
            ViewBag.RTerminado = rTerminado;
            ViewBag.RTwest = Rtwest;
            ViewBag.RTwrrhh = Rtwrrhh;
            ViewBag.RTwc = Rtwc;

            return PartialView("_DashboardAnalistaContratosHttpPost");
        }

        [HttpPost]
        public ActionResult ViewPartialNoEncontrado(string glyphicon, string message)
        {
            ViewBag.Glyphicon = glyphicon;
            ViewBag.Message = message;

            return PartialView("_NoResultadosEncontrados");
        }

        #endregion

        #region "Nuevos Metodos"

        [HttpPost]
        public ActionResult SolicitudContratos(string pagination = "MS01", string typeFilter = "", string dataFilter = "") 
        {
            ViewBag.HTMLLoaderError = "";

            /** PROCESOS SOLICITUD CONTRATOS */

            ViewBag.RenderizadoSolicitudContratos = ModuleSolicitud("", "U29saWNpdHVkQ29udHJhdG8=", pagination, typeFilter, dataFilter);

            /** PAGINATOR */

            ViewBag.RenderizadoPaginationsSolicitudContratos = ModulePagination("SolicitudContratos", pagination, "N", typeFilter, dataFilter);

            return PartialView("Operaciones/_SolicitudesContratos");

        }

        [HttpPost]
        public ActionResult SolicitudRenovaciones(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            ViewBag.HTMLLoaderError = "";

            /** PROCESOS SOLICITUD CONTRATOS */
            
            ViewBag.RenderizadoSolicitudRenovaciones = ModuleSolicitud("", "U29saWNpdHVkUmVub3ZhY2lvbg==", pagination, typeFilter, dataFilter);

            /** PAGINATOR */

            ViewBag.RenderizadoPaginationsSolicitudRenovaciones = ModulePagination("SolicitudRenovaciones", pagination, "N", typeFilter, dataFilter);

            return PartialView("Operaciones/_SolicitudesRenovaciones");

        }

        [HttpPost]
        public ActionResult ProcesosContratos(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            ViewBag.HTMLLoaderError = "";

            /** PROCESOS */

            ViewBag.RenderizadoProcesosContratos = ModuleProcesos("", "U29saWNpdHVkQ29udHJhdG8=", pagination, typeFilter, dataFilter);

            /** PAGINATOR */

            ViewBag.RenderizadoPaginationsProcesosContratos = ModulePagination("ProcesosContratos", pagination, "", typeFilter, dataFilter);

            return PartialView("Operaciones/_ProcesosContratos");

        }

        [HttpPost]
        public ActionResult ProcesosRenovaciones(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            ViewBag.HTMLLoaderError = "";

            ViewBag.RenderizadoProcesosRenovaciones = ModuleProcesos("", "U29saWNpdHVkUmVub3ZhY2lvbg==", pagination, typeFilter, dataFilter);
            /** PAGINATOR */

            ViewBag.RenderizadoPaginationsProcesosRenovaciones = ModulePagination("ProcesosRenovaciones", pagination, "N", typeFilter, dataFilter);
            ViewBag.RenderizaRenovaciones = true;

            return PartialView("Operaciones/_ProcesosRenovaciones");

        }

        #endregion

        #region "Metodos Cargo Mod"

        public ActionResult SiguienteWizards(string wizards, string wizardsActual, string codigoCargoMod, string tipoEvento)
        {

            ViewBag.OptionMoveWizards = "OK";
            int wizardsMax = 0;

            if (tipoEvento == "Simulación")
            {
                wizardsMax = 7;
            }

            if (tipoEvento == "Estructura")
            {
                wizardsMax = 8;
            }

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaWizards(codigoCargoMod, wizards, Session["base64Username"].ToString(), objects[0].Token.ToString()));

            wizards = (Convert.ToInt32(wizards) != wizardsMax) ? "Wizards/" + wizards : "Detail"; 
            
            ViewBag.HttpRedirectWizards = ModuleControlRetornoPath() + "/Operaciones/CargoMod/" + codigoCargoMod + "/" + wizards;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

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
                        ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/RQ==/Create";
                        ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/Uw==/Create";
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
            string wizards = "";
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaCliente(Session["base64Username"].ToString(), cliente, codigoSolicitud, objects[0].Token.ToString()));

            //if (tipoEvento == "Uw==")
            //{
            //    wizards = "1";
            //}

            //if (tipoEvento == "RQ==")
            //{
            //    wizards = "1";
            //}

            wizards = "1";

            ViewBag.Redireccionamiento = ModuleControlRetornoPath() + "/Operaciones/CargoMod/" + codigoSolicitud + "/Wizards/" + wizards;
            ViewBag.TipoEvento = tipoEvento;

            return PartialView("Finanzas/_DDLSeleccioneCliente");
        }

        public ActionResult AutocompleteClientes(string empresa, string dataFilter = "")
        {
            ViewBag.RenderizadoClientes = ModuleClientes("", "", "Codigo", dataFilter, Session["base64Username"].ToString());

            return PartialView("Teamwork/_ClientesElementAutocomplete");
        }

        public ActionResult ActualizaNombreCargo(string nombreCargo, string codigoSolicitud, string type, string wizardsActual)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaNombreCargo(Session["base64Username"].ToString(), nombreCargo, codigoSolicitud, type, objects[0].Token.ToString()));

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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_3");
        }

        public ActionResult ActualizaSueldo(string sueldo, string codigoSolicitud, string typeSueldo, string wizardsActual)
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_2");
        }

        public ActionResult ActualizaGratificacion(string gratificacion, string typeGratif, string gratifPactada, string codigoSolicitud, string wizardsActual, string observGratiConv = "")
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_4");
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

        public ActionResult AgregarBono(string codigoBono, string codigoCargoMod, string monto, string wizardsActual, string condiciones = "")
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_5");
        }

        public ActionResult EliminarBono(string codigoBono, string codigoCargoMod, string wizardsActual)
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_5");
        }

        public ActionResult ActualizarBono(string codigoBono, string codigoCargoMod, string monto, string wizardsActual, string condiciones = "")
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_5");
        }

        public ActionResult AgregarANI(string codigoANI, string codigoCargoMod, string monto, string wizardsActual, string deft = null, string observaciones = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(ANIs.__AgregarANI(Session["base64Username"].ToString(), codigoANI, codigoCargoMod, monto,deft != null ? deft : "N", observaciones, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_6");
        }

        public ActionResult EliminarANI(string codigoANI, string codigoCargoMod, string wizardsActual)
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_6");
        }

        public ActionResult ActualizarANI(string codigoANI, string codigoCargoMod, string monto, string wizardsActual, string deft = null, string observaciones = "")
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
            
            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;
            
            return PartialView("Operaciones/CargoMod/Wizards/_6");
        }

        public ActionResult CambiarInputSueldo(string sueldo, string codigoCargoMod, string wizardsActual)
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
            
            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_2");
        }

        public ActionResult CambiarCalculoTypeSueldo(string sueldo, string codigoCargoMod, string wizardsActual)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarCalculoTypeSueldo(codigoCargoMod, sueldo, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.HtmlMessageResult = (objectsCM[i].Message.ToString() != "") ? objectsCM[i].Message.ToString() : "";
            }

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_1");
        }

        public ActionResult CambiarTypeJornada(string typeJornada, string codigoCargoMod, string wizardsActual)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambiarTypeJornada(codigoCargoMod, typeJornada, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.HtmlMessageResult = (objectsCM[i].Message.ToString() != "") ? objectsCM[i].Message.ToString() : "";
            }

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
            
            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult CambioEstadoSolicitud(string estado, string codigoCargoMod, string origen, string observaciones = "", string tagsSelected = "CargoMod")
        {
            string view = string.Empty;

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__CambioEstadoSolicitud(Session["base64Username"].ToString(), codigoCargoMod, estado, observaciones, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
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
                        ViewBag.EnlaceCreacionSolicitudCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/RQ==/Create";
                        ViewBag.EnlaceCreacionSimulacionCargoMod = ModuleControlRetornoPath() + "/Finanzas/CargoMod/Uw==/Create";
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.HtmlMessageResult = objectsCM[i].MessageSimple.ToString();
                        view = "Finanzas/CargoMod/_ItemSolicitudes";
                        break;
                }
            }
            
            ViewBag.TagSelected = tagsSelected;

            return PartialView(view);
        }

        public ActionResult ActualizarJornadaPTSueldo(string typeJornadaCalculo = "", string valorDiario = "", string horasSemanales = "", string codigoCargoMod = "", string wizardsActual = "")
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCMTJ = JsonConvert.DeserializeObject(Cargo.__CambiarTypeJornada(codigoCargoMod, typeJornadaCalculo, objects[0].Token.ToString()));
            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaValorDiaPT(Session["base64Username"].ToString(), codigoCargoMod, valorDiario, horasSemanales, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ViewBag.HtmlMessageResult = (objectsCM[i].Message.ToString() != "") ? objectsCM[i].Message.ToString() : "";
            }

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult ActualizaObservaciones(string observaciones, string codigoCargoMod, string wizardsActual, string type)
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;
            
            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult EliminarObservaciones(string observaciones, string codigoCargoMod, string wizardsActual)
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

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult ActualizaHorario(string horarios, string codigoCargoMod, string wizardsActual)
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
            
            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;
            
            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult ActualizaDiasSemanales(string dia, string codigoCargoMod, string wizardsActual)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__ActualizaDiasSemanales(Session["base64Username"].ToString(), codigoCargoMod, dia, objects[0].Token.ToString()));

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }

        public ActionResult ActualizaJornadaFullTime(string horarios, string horasSemanales, string otherHS, string horasSemanalesOther, string codigoCargoMod, string wizardsActual)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(
                                    Cargo.__ActualizaJornadaFullTime(
                                        Session["base64Username"].ToString(), 
                                        codigoCargoMod, 
                                        "", 
                                        (horasSemanales != "other")
                                            ? horasSemanales
                                            : horasSemanalesOther
                                        , 
                                        objects[0].Token.ToString()
                                    )
                                );

            ViewBag.OptionCargoMod = "Edit";
            ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(codigoCargoMod);
            ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(codigoCargoMod);
            ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(codigoCargoMod);
            ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(codigoCargoMod);
            ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "Cliente", Session["ApplicationCliente"].ToString(), Session["ApplicationCliente"].ToString());
            ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
            ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());

            ViewBag.WizardsNext = Convert.ToInt32(wizardsActual) + 1;
            ViewBag.WizardsPrev = Convert.ToInt32(wizardsActual) - 1;
            ViewBag.WizardsActual = wizardsActual;

            return PartialView("Operaciones/CargoMod/Wizards/_" + wizardsActual);
        }
        
        #endregion

        #region "Modularización API"

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

                    constante.Codigo = objectsConst[x].Code.ToString();
                    constante.Message = objectsConst[x].Message.ToString();

                    provision.Constante = constante;
                }

                dynamic objectsOptConst = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, "CONST", empresa, objects[0].Token.ToString()));

                for (var z = 0; z < objectsOptConst.Count; z++)
                {
                    provision.OptActualizarAsignConstante = objectsOptConst[z].OptActualizar.ToString();
                    provision.OptCrearAsignConstante = objectsOptConst[z].OptCrear.ToString();
                    provision.OptEliminarAsignConstante = objectsOptConst[z].OptEliminar.ToString();
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

                estructura.NumeroHorasSemanales = objectsEstructura[i].HorasSemanales.ToString();
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

                estructura.SueldoBaseCifra = objectsEstructura[i].SueldoBaseCifra.ToString();
                estructura.SueldoLiquidoCifra = objectsEstructura[i].SueldoLiquidoCifra.ToString();
                estructura.GratificacionCifra = objectsEstructura[i].GratificacionCifra.ToString();
                estructura.BaseImponibleCifra = objectsEstructura[i].BaseImponibleCifra.ToString();
                estructura.BaseImponibleAFCCifra = objectsEstructura[i].BaseImponibleAFCCifra.ToString();
                estructura.TotalImponibleCifra = objectsEstructura[i].TotalImponibleCifra.ToString();
                estructura.TotalTributableCifra = objectsEstructura[i].TotalTributableCifra.ToString();
                estructura.TotalHaberesCifra = objectsEstructura[i].TotalHaberesCifra.ToString();

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
                ANI.ValorCifra = objectsANI[i].ValorCifra.ToString();
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
                bono.ValorCifra = objectsBonos[i].ValorCifra.ToString();
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

                dashboard.Creaciones = objectsDashboard[i].Creaciones.ToString();
                dashboard.Simulaciones = objectsDashboard[i].Simulaciones.ToString();
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

            for (var i = 0; i < objectsPgt.Count; i++)
            {
                Models.Teamwork.Pagination paginationIndex = new Models.Teamwork.Pagination();

                paginationIndex.NumeroPagina = objectsPgt[i].NumeroPagina.ToString();
                paginationIndex.Rango = objectsPgt[i].Rango.ToString();
                paginationIndex.Class = objectsPgt[i].Class.ToString();
                paginationIndex.Properties = objectsPgt[i].Properties.ToString();
                paginationIndex.TypeFilter = objectsPgt[i].TypeFilter.ToString();
                paginationIndex.Filter = objectsPgt[i].Filter.ToString();

                if (objectsPgt[i].NumeroPagina.ToString() == "Siguiente &rsaquo;" && objectsPgt[i].Properties.ToString() == "")
                {
                    paginations.Add(paginationIndex);
                }

            }

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

                solicitudCargos.EnlaceDetalleSolicitud = ModuleControlRetornoPath() + "/Operaciones/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Detail";
                solicitudCargos.EnlaceEditar = ModuleControlRetornoPath() + "/Operaciones/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Wizards/" + objectsCM[i].Wizards.ToString();
                solicitudCargos.EnlaceVolver = ModuleControlRetornoPath() + "/Operaciones/CargoMod";
                solicitudCargos.EnlaceEstructura = ModuleControlRetornoPath() + "/Operaciones/Pdf?pdf=EstructuraRenta&data=" + objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.OptDesechar = objectsCM[i].OptDesechar.ToString();
                solicitudCargos.OptPdf = objectsCM[i].OptPdf.ToString();
                solicitudCargos.OptEditar = objectsCM[i].OptEditar.ToString();
                solicitudCargos.OptSolicitar = objectsCM[i].OptSolicitar.ToString();
                solicitudCargos.OptRechazar = objectsCM[i].OptRechazar.ToString();
                solicitudCargos.OptPublicar = objectsCM[i].OptPublicar.ToString();
                solicitudCargos.OptHistorial = objectsCM[i].OptHistorial.ToString();
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

        #region "Modularizacion de codigo fuente"

        private List<DashboardFiniquitos> ModuleDashbordFiniquitos()
        {
            List<DashboardFiniquitos> dashboards = new List<DashboardFiniquitos>();
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

            valMantFin[0] = "DASHBOARDFINIQUITOS";
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
                DashboardFiniquitos dashboard = new DashboardFiniquitos();

                dashboard.Calculados = rows["Calculados"].ToString();
                dashboard.EnTransito = rows["EnTransito"].ToString();
                dashboard.Confirmados = rows["Confirmados"].ToString();
                dashboard.EmisionPago = rows["EmisionPago"].ToString();
                dashboard.TEFPendientesPago = rows["TEFPendientesPago"].ToString();
                dashboard.PendientePagoT90 = rows["PendientePagoT90"].ToString();
                dashboard.PendientePagoL90 = rows["PendientePagoL90"].ToString();
                dashboard.Notariado = rows["Notariado"].ToString();

                dashboard.BajasConfirmadas = rows["BajasConfirmadas"].ToString();
                dashboard.Simulaciones = rows["Simulaciones"].ToString();
                dashboard.CompCreados = rows["CompCreados"].ToString();
                dashboard.CompPendAuth = rows["CompPendAuth"].ToString();
                dashboard.CompPendEmiPago = rows["CompPendEmiPago"].ToString();
                dashboard.CompPendPagoTEF = rows["CompPendPagoTEF"].ToString();
                dashboard.CompPendPagoL90 = rows["CompPendPagoL90"].ToString();
                dashboard.CompPendPagoT90 = rows["CompPendPagoT90"].ToString();
                dashboard.CompNotariar = rows["CompNotariar"].ToString();

                dashboards.Add(dashboard);
            }

            return dashboards;
        }

        private List<Solicitud> ModuleSolicitud(string codigoTransaccion, string tipoSolicitud, string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<Solicitud> solicitudes = new List<Solicitud>();
            string[] param = new string[8];
            string[] val = new string[8];

            param[0] = "@AUTHENTICATE";
            param[1] = "@AGENTAPP";
            param[2] = "@TRABAJADOR";
            param[3] = "@CODIGOTRANSACCION";
            param[4] = "@TIPOSOLICITUD";
            param[5] = "@PAGINATION";
            param[6] = "@TYPEFILTER";
            param[7] = "@DATAFILTER";

            val[0] = tokenAuth;
            val[1] = agenteAplication;
            val[2] = Session["NombreUsuario"].ToString();
            val[3] = codigoTransaccion;
            val[4] = tipoSolicitud; //U29saWNpdHVkQ29udHJhdG8=
            val[5] = pagination;
            val[6] = typeFilter;
            val[7] = dataFilter;
            
            DataSet dataSolicitudesC = servicioOperaciones.GetObtenerSolicitudes(param, val).Table;

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
                    solicitud.CreadoPor = rows["CreadoPor"].ToString();
                    solicitud.AsignadoA = rows["AsignadoA"].ToString();
                    solicitud.FechasCompromiso = rows["FechasCompromiso"].ToString();
                    solicitud.Comentarios = rows["Comentarios"].ToString();
                    solicitud.Estado = rows["Estado"].ToString();

                    solicitud.Prioridad = rows["Prioridad"].ToString();
                    solicitud.Glyphicon = rows["Glyphicon"].ToString();
                    solicitud.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    solicitud.BorderColor = rows["BorderColor"].ToString();
                    solicitud.ColorFont = rows["ColorFont"].ToString();
                    
                    solicitud.OptDescargarDatosCargados = "N";
                    solicitud.OptAsignarSolicitud = "N";
                    solicitud.OptDescargarSolicitudContratoIndividual = "N";
                    solicitud.OptHistorialSolicitud = "N";
                    solicitud.OptRevertirAnulacion = "N";
                    solicitud.OptDescargarErrorDatosCargados = "N";
                    solicitud.OptAnularSolicitud = "N";

                    solicitud.OptRevertirTermino = "N";

                    //solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                    //solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                    //solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                    //solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                    //solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                    //solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                    //solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                    //solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                    //solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                    solicitudes.Add(solicitud);
                }
            }

            return solicitudes;
        }

        private List<Proceso> ModuleProcesos(string codigoTransaccion, string tipoSolicitud, string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<Proceso> procesos = new List<Proceso>();
            string[] param = new string[8];
            string[] val = new string[8];

            param[0] = "@AUTHENTICATE";
            param[1] = "@AGENTAPP";
            param[2] = "@TRABAJADOR";
            param[3] = "@CODIGOTRANSACCION";
            param[4] = "@TIPOSOLICITUD";
            param[5] = "@PAGINATION";
            param[6] = "@TYPEFILTER";
            param[7] = "@DATAFILTER";

            val[0] = tokenAuth;
            val[1] = agenteAplication;
            val[2] = Session["NombreUsuario"].ToString();
            val[3] = codigoTransaccion;
            val[4] = tipoSolicitud; //U29saWNpdHVkQ29udHJhdG8=
            val[5] = pagination;
            val[6] = typeFilter;
            val[7] = dataFilter;

            DataSet dataSolicitudesC = servicioOperaciones.GetObtenerProcesosSolicitudes(param, val).Table;

            foreach (DataRow rows in dataSolicitudesC.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    Proceso proceso = new Proceso();

                    proceso.Code = rows["Code"].ToString();
                    proceso.Message = rows["Message"].ToString();
                    proceso.NombreProceso = rows["NombreProceso"].ToString();
                    
                    proceso.Creado = rows["Creado"].ToString();
                    proceso.CreadoPor = rows["CreadoPor"].ToString();
                    proceso.AsignadoA = rows["AsignadoA"].ToString();
                    proceso.TotalesSolicitud = rows["TotalSolicitudes"].ToString();
                    proceso.Comentarios = rows["Comentarios"].ToString();
                    proceso.Estado = rows["Estado"].ToString();

                    proceso.Prioridad = rows["Prioridad"].ToString();
                    proceso.Glyphicon = rows["Glyphicon"].ToString();
                    proceso.GlyphiconColor = rows["GlyphiconColor"].ToString();
                    proceso.BorderColor = rows["BorderColor"].ToString();
                    proceso.ColorFont = rows["ColorFont"].ToString();

                    proceso.PathExcelSolicitud = ModuleControlRetornoPath() + "/Operaciones/GenerateExcel?excel=Q29udHJhdG8=&codigotransaccion=" + rows["CodigoProceso"].ToString();

                    //solicitud.RenderizadoOpciones = rows["RenderizadoOpciones"].ToString();

                    //solicitud.OptDescargarDatosCargados = rows["OptDescargarDatosCargados"].ToString();
                    //solicitud.OptAsignarSolicitud = rows["OptAsignarSolicitud"].ToString();
                    //solicitud.OptDescargarSolicitudContratoIndividual = rows["OptDescargarSolicitudContratoIndividual"].ToString();
                    //solicitud.OptHistorialSolicitud = rows["OptHistorialSolicitud"].ToString();
                    //solicitud.OptRevertirAnulacion = rows["OptRevertirAnulacion"].ToString();
                    //solicitud.OptDescargarErrorDatosCargados = rows["OptDescargarErrorDatosCargados"].ToString();
                    //solicitud.OptAnularSolicitud = rows["OptAnularSolicitud"].ToString();

                    //solicitud.OptRevertirTermino = rows["OptRevertirTermino"].ToString();

                    procesos.Add(proceso);
                }
            }

            return procesos;
        }

        private List<Pagination> ModulePagination(string typePagination, string paginationIndex, string hasBaja, string typeFilter = "", string dataFilter = "")
        {
            string[] paramPaginator = new string[8];
            string[] valPaginator = new string[8];
            DataSet dataPaginator;
            List<Pagination> paginations = new List<Pagination>();

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
                Pagination pagination = new Pagination();

                pagination.NumeroPagina = rows["NumeroPagina"].ToString();
                pagination.Rango = rows["Rango"].ToString();
                pagination.Class = rows["Class"].ToString();
                pagination.Properties = rows["Properties"].ToString();
                pagination.TypeFilter = rows["TypeFilter"].ToString();
                pagination.Filter = rows["Filter"].ToString();

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
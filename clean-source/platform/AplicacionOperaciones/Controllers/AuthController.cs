using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.DirectoryServices;
using System.Text;
using Teamwork.Infraestructura.Collections.Seleccion;
using Newtonsoft.Json;
using Teamwork.Infraestructura.Collections.Auth;

namespace AplicacionOperaciones.Controllers
{
    public class AuthController : Controller
    {
        ServicioAuth.ServicioAuthClient servicioAuth = new ServicioAuth.ServicioAuthClient();
        ServicioOperaciones.ServicioOperacionesClient servicioOperaciones = new ServicioOperaciones.ServicioOperacionesClient();

        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";
        string fromDebugQA = "AplicacionOperaciones";
        
        public ActionResult Index()
        {
            ViewBag.Title = "Iniciar Sesión";

            ViewBag.cookieUsername = "";
            ViewBag.cookiePassword = "";

            if (Response.Cookies["cookieSaveU"] != null &&
                Response.Cookies["cookieSaveP"] != null)
            {
                ViewBag.cookieUsername = Response.Cookies["cookieSaveU"].Value;
                ViewBag.cookiePassword = Response.Cookies["cookieSaveP"].Value;
            }

            return View();
        }

        public ActionResult Invitation()
        {
            string response = string.Empty;

            if (Session["base64Username"] != null)
            {
                response = ModuleControlRetornoPath() + "/" + Request.QueryString["controller"] + "/" + Request.QueryString["oauth"] + "/" + Request.QueryString["ref"] + "/Detail";
            }
            else
            {
                response = ModuleControlRetornoPath() + "/Auth/?auth=pending&controller=" + Request.QueryString["controller"] + "&oauth=" + Request.QueryString["oauth"] + "&ref=" + Request.QueryString["ref"];
            }

            return Redirect(response);
        }

        public ActionResult SignOut()
        {
            Session.Clear();
            string prefix = "";
            string urlServer = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                urlServer = "http://181.190.6.196/AplicacionOperaciones";
            }
            else
            {
                urlServer = "http://localhost:46903";
            }

            Response.Redirect(urlServer + "/Auth");

            return View();
        }

        public ActionResult TokenAuth()
        {
            if (Session["CodificarAuth"] != null && Session["NombreUsuario"].ToString() != null && Session["RenderizadoPermisos"] != null)
            {
                string code = string.Empty;
                string message = string.Empty;

                DataSet dataBase;

                string[] paramBase64 = new string[4];
                string[] valBase64 = new string[4];

                string[] paramCambioEstado = new string[5];
                string[] valCambioEstado = new string[5];

                paramBase64[0] = "@AUTHENTICATE";
                paramBase64[1] = "@AGENTAPP";
                paramBase64[2] = "@TYPEBASE64";
                paramBase64[3] = "@DATA";

                paramCambioEstado[0] = "@AUTHENTICATE";
                paramCambioEstado[1] = "@AGENTAPP";
                paramCambioEstado[2] = "@TRABAJADOR";
                paramCambioEstado[3] = "@ESTADO";
                paramCambioEstado[4] = "@DATA";

                switch (Request.QueryString["events"].ToString())
                {
                    case "simulate":
                        valBase64[0] = tokenAuth;
                        valBase64[1] = agenteAplication;
                        valBase64[2] = "DECODE";
                        valBase64[3] = Request.QueryString["ref"].ToString();

                        dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                        foreach (DataRow rows in dataBase.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                Response.Redirect(ModuleControlRetornoFiniquitos() + (rows["Data"].ToString() + "&token=simulation").Replace("http://192.168.0.10/TW_FINIQUITOS/", ""));
                            }
                        }
                        break;
                    case "viewprocess":

                        valBase64[0] = tokenAuth;
                        valBase64[1] = agenteAplication;
                        valBase64[2] = "DECODE";
                        valBase64[3] = Request.QueryString["ref"].ToString();

                        dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                        foreach (DataRow rows in dataBase.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                Response.Redirect(rows["Data"].ToString());
                            }
                        }

                        break;
                    case "integratesettlement":
                        
                        valCambioEstado[0] = tokenAuth;
                        valCambioEstado[1] = agenteAplication;
                        valCambioEstado[2] = Session["NombreUsuario"].ToString();
                        valCambioEstado[3] = "EPC";
                        valCambioEstado[4] = Request.QueryString["authorizate"].ToString();

                        DataSet dataCambioEstado = servicioOperaciones.SetCambiarEstadoSolicitud(paramCambioEstado, valCambioEstado).Table;

                        foreach (DataRow rows in dataCambioEstado.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                code = rows["Code"].ToString();
                                message = rows["Code"].ToString();
                            }
                        }

                        if (code == "200") {
                            valBase64[0] = tokenAuth;
                            valBase64[1] = agenteAplication;
                            valBase64[2] = "DECODE";
                            valBase64[3] = Request.QueryString["ref"].ToString();

                            dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                            foreach (DataRow rows in dataBase.Tables[0].Rows)
                            {
                                if (rows["Code"].ToString() == "200")
                                {
                                    Response.Redirect(ModuleControlRetornoFiniquitos() + rows["Data"].ToString().Replace("http://192.168.0.10/TW_FINIQUITOS/", ""));
                                }
                            }
                        }
                        else
                        {
                            ViewBag.RenderizaMensajeError = message;
                        }

                        break;
                    case "integratecalculation":
                        valBase64[0] = tokenAuth;
                        valBase64[1] = agenteAplication;
                        valBase64[2] = "DECODE";
                        valBase64[3] = Request.QueryString["ref"].ToString();

                        dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                        foreach (DataRow rows in dataBase.Tables[0].Rows)
                        {
                            if (rows["Code"].ToString() == "200")
                            {
                                Response.Redirect(ModuleControlRetornoFiniquitos() + (rows["Data"].ToString() + "&token=simulation").Replace("http://192.168.0.10/TW_FINIQUITOS/", ""));
                            }
                        }
                        break;
                    case "teamsjob":
                        
                        dynamic tokens = CollectionSeleccion.__CodigoOAuthCrear(Encoding.UTF8.GetString(Convert.FromBase64String(Session["base64Username"].ToString())), "ActiveDirectory");

                        for (dynamic i = 0; i < tokens.Count; i++)
                        {
                            Response.Redirect(ModuleControlRetornoTeamsjob() + "?" + tokens[i].Tokens);
                        }

                        break;
                    case "planner":
                        dynamic tokensPlanner = CollectionAuth.__CrearTokenConfianza(Session["Username"].ToString());

                        for (dynamic i = 0; i < tokensPlanner.Count; i++)
                        {
                            Response.Redirect(ModuleControlRetornoPlanner(Session["Username"].ToString(), tokensPlanner[i].Token));
                        }
                        break;
                    case "apppostulacion":
                        dynamic tokensApiPostulacion = CollectionAuth.__CrearTokenConfianza(Session["Username"].ToString());

                        for (dynamic i = 0; i < tokensApiPostulacion.Count; i++)
                        {
                            Response.Redirect(ModuleControlRetornoApiPostulacion(Session["Username"].ToString(), tokensApiPostulacion[i].Token));
                        }
                        break;
                    case "teamclass":
                        dynamic tokensTeamclasss = CollectionAuth.__CrearTokenConfianza(Session["Username"].ToString());

                        for (dynamic i = 0; i < tokensTeamclasss.Count; i++)
                        {
                            Response.Redirect(ModuleControlRetornoTeamclass(Session["Username"].ToString(), tokensTeamclasss[i].Token));
                        }
                        break;
                    case "coins":
                        dynamic tokensCoins = CollectionAuth.__CrearTokenConfianza(Session["Username"].ToString());

                        for (dynamic i = 0; i < tokensCoins.Count; i++)
                        {
                            Response.Redirect(ModuleControlRetornoCoins(Session["Username"].ToString(), tokensCoins[i].Token));
                        }
                        break;

                }
                    
            }
            
            return View();
        }

        [HttpPost]
        public JsonResult SignIn(string username, string password, string cookie, string token, string method)
        {
            /** BLOQUE DE CODIGO PARA INICIO DE SESION */
            string pathRedirect = "";
            string message = "";
            string code = "";
            string sistema = "";
            string urlServer = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                sistema = "AplicacionOperaciones";
            }
            else
            {
                sistema = fromDebugQA;
            }

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {

                //urlServer = "http://181.190.6.196/AplicacionOperaciones";
                urlServer = "http://181.190.6.196/AplicacionOperaciones";
            }
            else
            {
                urlServer = "http://localhost:46903";
            }

            /** APLICACION DE HEADERS */

            /** ACTIVE DIRECTORY LOGIN */
            try
            {

                if (method == "ActiveDirectory")
                {
                    DirectoryEntry directory = new DirectoryEntry("LDAP://team-work.cl", @"TEAM-WORK\" +
                                                                    Encoding.UTF8.GetString(Convert.FromBase64String(username)),
                                                                    Encoding.UTF8.GetString(Convert.FromBase64String(password)),
                                                                    AuthenticationTypes.Secure);

                    DirectorySearcher searcher = new DirectorySearcher(directory);

                    searcher.FindOne();

                }

                string[] parametrosHeaders =
                {
                    "@AUTHENTICATE",
                    "@AGENTAPP",
                    "@USERNAME",
                    "@PASSWORD",
                    "@SISTEMA",
                    "@AUTH"
                };

                string[] valoresHeaders =
                {
                    tokenAuth,
                    agenteAplication,
                    username,
                    password,
                    sistema,
                    method
                };

                /** END APLICACION DE HEADERS */

                DataSet data = servicioAuth.GetSignIn(parametrosHeaders, valoresHeaders).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        string prefix = "";

                        if (token == "")
                        {
                            pathRedirect = urlServer + "/Operaciones";
                        }
                        else
                        {
                            pathRedirect = token;
                        }

                        code = rows["Code"].ToString();
                        message = rows["Message"].ToString();

                        Session["NombreUsuario"] = rows["NombreUsuario"].ToString();
                        Session["Cargo"] = rows["Cargo"].ToString();
                        Session["Area"] = rows["Area"].ToString();
                        Session["ApplicationActive"] = "S";
                        Session["Profile"] = rows["Profile"].ToString();

                        Session["base64Username"] = username;
                        Session["base64Password"] = password;
                        Session["CodificarAuth"] = rows["CodificarAuth"].ToString();
                        Session["PreController"] = rows["PreController"].ToString();
                        Session["Username"] = rows["Username"].ToString();

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
                            "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo",
                            "AGENT_WEBSERVICE_APP",
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
                            acceso.PreController = rowsPermisos["PreController"].ToString();
                            acceso.Controller = rowsPermisos["Controllers"].ToString();
                            acceso.Glyphicon = rowsPermisos["Glyphicon"].ToString();
                            acceso.Titulo = rowsPermisos["TituloRenderizado"].ToString();

                            accesos.Add(acceso);
                        }

                        Session["RenderizadoPermisos"] = accesos;

                        if (cookie == "S")
                        {
                            HttpCookie cookieSaveU = new HttpCookie("cookieSaveU", username);
                            HttpCookie cookieSaveP = new HttpCookie("cookieSaveP", password);

                            Response.SetCookie(cookieSaveU);
                            Response.SetCookie(cookieSaveP);
                        }
                    }
                    else
                    {
                        code = rows["Code"].ToString();
                        message = rows["Message"].ToString();
                    }

                }

            }
            catch (Exception ex)
            {
                code = "404";
                message = ex.Message;
            }
            /** END ACTIVE DIRECTORY LOGIN */
            
            return Json(new { Code = code, Message = message, PathRedirect = pathRedirect, Correo = username });
        }

        [HttpPost]
        public ActionResult ViewPartialErrorSignIn(string message)
        {
            ViewBag.Message = message;

            return PartialView("_ErrorSignIn");
        }

        [HttpPost]
        public ActionResult ViewPartialLoadingSignIn()
        {
            return PartialView("_LoadingLogin");
        }

        [HttpPost]
        public ActionResult ViewPartialFormSignIn()
        {
            return PartialView("_FormLogin");
        }


        #region "Modularización de codigo fuente"

        private string ModuleControlRetornoApiPostulacion(string username, string token)
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                //if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                //{
                //    domainReal = Request.Url.AbsoluteUri.Split('/')[2];
                //}
                //else
                //{
                //    domainReal = "181.190.6.196:83";
                //}

                domain = "http://192.168.0.10:9090";
                prefixDomain = "/admintuwork/auth?user=" + username + "&token=" + token;
            }
            else
            {
                domain = "http://localhost:3000";
                prefixDomain = "/admintuwork/auth?user=" + username + "&token=" + token;
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoTeamclass(string username, string token)
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                domain = "http://teamclass.team-work.cl:82";
                prefixDomain = "/signin?username=" + username + "&token=" + token;
            }
            else
            {
                domain = "http://localhost:3000";
                prefixDomain = "/signin?username=" + username + "&token=" + token;
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoPlanner(string username, string token)
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                domain = "http://planner.team-work.cl";
                prefixDomain = "/authenticate?username=" + username + "&token=" + token;
            }
            else
            {
                domain = "http://localhost:3000";
                prefixDomain = "/authenticate?username=" + username + "&token=" + token;
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoCoins(string username, string token)
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                domain = "http://coins.team-work.cl";
                prefixDomain = "/signin?username=" + username + "&token=" + token;
            }
            else
            {
                domain = "http://localhost:3000";
                prefixDomain = "/signin?username=" + username + "&token=" + token;
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoTeamsjob()
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
                prefixDomain = "/Teamsjob/App/Auth";
            }
            else
            {
                domain = "http://localhost:9685/App/Auth";
                //domain = "http://mario/Teamsjob/App/Auth";
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoFiniquitos()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                domain = "http://finiquitos.team-work.cl:82/";
                prefixDomain = "";
            }
            else
            {
                domain = "http://localhost:8080/";
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }

        private string ModuleControlRetornoPath()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                domain = "http://181.190.6.196/AplicacionOperaciones/";
            }
            else
            {
                domain = "http://" + Request.Url.AbsoluteUri.Split('/')[2];
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }
        #endregion

    }
}
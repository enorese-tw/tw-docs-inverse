using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionOperaciones.Controllers
{
    public class RecepcionController : Controller
    {
        ServicioAuth.ServicioAuthClient svcAuth = new ServicioAuth.ServicioAuthClient();
        ServicioCorreo.ServicioCorreoClient svcCorreo = new ServicioCorreo.ServicioCorreoClient();
        ServicioOperaciones.ServicioOperacionesClient svcOperaciones = new ServicioOperaciones.ServicioOperacionesClient();

        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";

        public ActionResult Index()
        {
            if (AplicationActive())
            {
                ViewBag.EnlaceRecepcion = ModuleControlRetornoPath() + "/Recepcion/Recepcionar";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View();
        }

        public ActionResult Recepcionar()
        {
            if (AplicationActive())
            {
                ViewBag.ReferenciaMessageRecepcion = "";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Pagar()
        {
            if (AplicationActive())
            {
                ViewBag.ReferenciaMessagePagar = "";
                ViewBag.ReferenciaChequeIngresado = "";
                ViewBag.ReferenciaFiniquitoIngresado = "";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Notariar()
        {
            if (AplicationActive())
            {
                ViewBag.ReferenciaMessageNotariar = "";
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        #region "HTTPPOST"

        [HttpPost]
        public ActionResult AperturarBox(string codigoDespacho)
        {
            string view = string.Empty;

            try
            {
                ViewBag.ReferenciaDespachado = ModuleDespachoAperturado(codigoDespacho);

                switch (ViewBag.ReferenciaCodigoEvento)
                {
                    case "200":
                        view = "Recepcion/_BoxApertura";
                        break;
                    case "404":
                        view = "Recepcion/_BoxRecepcionar";
                        break;
                    case "300":
                        view = "Recepcion/_BoxEnd";
                        break;
                }

                
            }
            catch (Exception)
            {

            }

            return PartialView(view);
        }

        [HttpPost]
        public ActionResult Match(string codigo, string codigoCheque = "", string codigoFiniquito = "")
        {
            if (codigo.ToUpper().Contains("FNQ-"))
            {
                ViewBag.ReferenciaChequeIngresado = codigoCheque;
                ViewBag.ReferenciaFiniquitoIngresado = codigo;
                ViewBag.ReferenciaMessagePagar = "";
            }
            else if (codigo.ToUpper().Contains("CHQ-"))
            {
                ViewBag.ReferenciaChequeIngresado = codigo;
                ViewBag.ReferenciaFiniquitoIngresado = codigoFiniquito;
                ViewBag.ReferenciaMessagePagar = "";
            }
            else
            {
                ViewBag.ReferenciaChequeIngresado = codigoCheque;
                ViewBag.ReferenciaFiniquitoIngresado = codigoFiniquito;
                ViewBag.ReferenciaMessagePagar = "El codigo que esta leyendo es incosistente con el que permite el sistema, debe ser un finiquito o el cheque, favor leer un codigo de barra correcto";
            }

            return PartialView("Recepcion/_BoxPagar");
        }

        [HttpPost]
        public ActionResult PagarCheque(string codigoCheque, string codigoFiniquito)
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
                valMantFin[16] = codigoCheque;
                valMantFin[17] = "";
                valMantFin[18] = "";
                valMantFin[19] = "";
                valMantFin[20] = "";

                dataMantencion = svcOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in dataMantencion.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Recepcion/_BoxPagado";
                            ViewBag.ReferenciaChequeIngresado = "";
                            ViewBag.ReferenciaFiniquitoIngresado = "";
                            ViewBag.ReferenciaMessagePagado = rows["Message"].ToString();
                            break;
                        case "300":
                            view = "Recepcion/_BoxPagar";
                            ViewBag.ReferenciaChequeIngresado = "";
                            ViewBag.ReferenciaFiniquitoIngresado = "";
                            ViewBag.ReferenciaMessagePagar = rows["Message"].ToString();
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
        public ActionResult NotariarFiniquito(string codigoFiniquito)
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

                valMantFin[0] = "NOTFIN";
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

                data = svcOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

                foreach (DataRow rows in data.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            view = "Recepcion/_BoxNotariado";
                            ViewBag.ReferenciaMessageNotariar = rows["Message"].ToString();
                            break;
                        case "300":
                            view = "Recepcion/_BoxNotariar";
                            ViewBag.ReferenciaMessageNotariar = rows["Message"].ToString();
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

        #region "Modularización de Codigo Fuente"

        private List<Models.Recepcion.Recepcionar> ModuleDespachoAperturado(string codigoDespacho)
        {
            List<Models.Recepcion.Recepcionar> recepciones = new List<Models.Recepcion.Recepcionar>();
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

            valMantFin[0] = "RECEPBOX";
            valMantFin[1] = Session["base64Username"].ToString();
            valMantFin[2] = codigoDespacho;
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

            dataMantencion = svcOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            foreach (DataRow rows in dataMantencion.Tables[0].Rows)
            {
                Models.Recepcion.Recepcionar recepcion = new Models.Recepcion.Recepcionar();

                ViewBag.ReferenciaCodigoEvento = rows["Code"].ToString();

                switch (rows["Code"].ToString())
                {
                    case "200":
                        recepcion.Code = rows["Code"].ToString();
                        recepcion.CodigoDespacho = rows["CodigoDespacho"].ToString();
                        recepcion.Despachados = rows["Despachados"].ToString();
                        recepcion.Recepcionados = rows["Recepcionados"].ToString();
                        recepcion.Total = rows["Total"].ToString();

                        recepciones.Add(recepcion);
                        break;
                    case "404":
                        recepcion.Code = rows["Code"].ToString();
                        recepcion.Message = rows["Message"].ToString();
                        ViewBag.ReferenciaMessageRecepcion = rows["Message"].ToString();

                        recepciones.Add(recepcion);
                        break;
                    case "300":
                        recepcion.Code = rows["Code"].ToString();
                        recepcion.Message = rows["Message"].ToString();
                        recepcion.Total = rows["Total"].ToString();

                        recepciones.Add(recepcion);
                        break;
                }
                
            }

            return recepciones;
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
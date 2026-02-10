using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamwork.Infraestructura.Collections.Procesos;

namespace AplicacionOperaciones.Controllers
{
    public class ProcesoController : Controller
    {
        public ActionResult Index()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Contratos()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
        
            return View();
        }
        
        #region "Metodos API"
        
        public string ProcesoListarContratos(string codigoTransaction = "", string filter = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionProceso.__ProcesoListarContratos(codigoTransaction, Session["NombreUsuario"].ToString(), filter, dataFilter, pagination, ""), Formatting.Indented);
        }

        public string ProcesoModificarNombre(string codigoTransaction = "", string nombreProceso = "", string type = "")
        {
            return JsonConvert.SerializeObject(CollectionProceso.__ProcesoModificarNombre(codigoTransaction, nombreProceso, Session["NombreUsuario"].ToString(), type, ""), Formatting.Indented);
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
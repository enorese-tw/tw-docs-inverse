using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Teamwork.Extensions.Excel;
using Teamwork.Infraestructura.Collections.Paginations;
using Teamwork.Infraestructura.Collections.Procesos;
using Teamwork.Infraestructura.Collections.Solicitudes;
using Teamwork.WebApi.Auth;
using Teamwork.WebApi.Operaciones;

namespace AplicacionOperaciones.Controllers
{
    public class SolicitudController : Controller
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

        public ActionResult Renovaciones()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Consulta()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult GenerateExcel()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            switch (Request.QueryString["excel"])
            {
                case "ReporteRenovaciones":
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Renovaciones.xlsx"));
                    Response.ContentType = "application/excel";
                    Response.BinaryWrite(XLSX_ReporteRenovaciones.__xlsx(Request.QueryString["cliente"].ToString().Replace(' ', '+'), Request.QueryString["fechainicio"].ToString(), Request.QueryString["fechatermino"].ToString(), Request.QueryString["empresa"].ToString()));
                    Response.Flush();
                    Response.End();
                    break;
                case "ReporteContratos":
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "Reporte Contratos.xlsx"));
                    Response.ContentType = "application/excel";
                    Response.BinaryWrite(XLSX_ReporteContratos.__xlsx(Request.QueryString["cliente"].ToString().Replace(' ', '+'), Request.QueryString["fechainicio"].ToString(), Request.QueryString["fechatermino"].ToString(), Request.QueryString["empresa"].ToString()));
                    Response.Flush();
                    Response.End();
                    break;
            }

            return View();
        }

        public ActionResult ReporteRenovaciones()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult ReporteContratos()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        #region "Metodos API"

        public string SolicitudListarContratos(string codigoTransaction = "", string filter = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__SolicitudListarContratos(codigoTransaction, Session["NombreUsuario"].ToString(), filter, dataFilter, pagination, ""), Formatting.Indented);
        }

        public string SolicitudListarRenovaciones(string codigoTransaction = "", string filter = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__SolicitudListarRenovaciones(codigoTransaction, Session["NombreUsuario"].ToString(), filter, dataFilter, pagination, ""), Formatting.Indented);
        }

        public string PaginationSolicitudContratos(string codigoTransaction = "", string filter = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionPagination.__PaginationSolicitudContratos(codigoTransaction, Session["NombreUsuario"].ToString(), filter, dataFilter, pagination, ""), Formatting.Indented);
        }

        public string PaginationSolicitudRenovaciones(string codigoTransaction = "", string filter = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionPagination.__PaginationSolicitudRenovaciones(codigoTransaction, Session["NombreUsuario"].ToString(), filter, dataFilter, pagination, ""), Formatting.Indented);
        }
        
        public string SolicitudAnularSolicitud(string codigoSolicitud = "", string type = "", string observacion = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__SolicitudAnularSolicitud(Session["NombreUsuario"].ToString(), codigoSolicitud, type, observacion, ""), Formatting.Indented);
        }

        public string ConsultaFichaPersonal(string ficha = "", string empresa = "", string rut = "", string filter = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__ConsultaFichaPersonal(ficha, empresa, rut, filter), Formatting.Indented);
        }

        public string ConsultaContrato(string ficha = "", string empresa = "",string rut = "", string filter = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__ConsultaContrato(ficha, empresa, rut, filter), Formatting.Indented);
        }

        public string ConsultaRenovacion(string ficha = "", string empresa = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__ConsultaRenovacion(ficha, empresa), Formatting.Indented);
        }

        public string ConsultaBaja(string ficha = "", string empresa = "")
        {
            return JsonConvert.SerializeObject(CollectionSolicitud.__ConsultaBaja(ficha, empresa), Formatting.Indented);
        }

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
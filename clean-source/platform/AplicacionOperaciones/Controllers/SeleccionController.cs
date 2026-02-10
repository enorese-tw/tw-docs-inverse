using AplicacionOperaciones.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamwork.Infraestructura.Collections.Seleccion;
using Teamwork.Model.Seleccion;
using Teamwork.WebApi.Auth;

namespace AplicacionOperaciones.Controllers
{
    public class SeleccionController : Controller
    {
        public ActionResult Index()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Postulante()
        {
            List<Request> requests = new List<Request>();

            if (AplicationActive())
            {
                try
                {
                    if (Session["TokenOAuth"] == null || Request.QueryString["oauth"] != null)
                    {

                        Session.RemoveAll();

                        dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

                        if (Request.QueryString["oauth"] != null)
                        {
                            requests = CollectionsPostulante.__PostulanteValidateTokenInvitacion(Request.QueryString["oauth"], objects[0].Token.ToString(), "");

                            foreach (Request item in requests)
                            {
                                if (item.Code == "200")
                                {
                                    Session["TokenOAuth"] = Request.QueryString["oauth"];
                                    Response.Redirect("Postulante");
                                }
                            }

                        }
                        else
                        {
                            Request request = new Request();
                            request.Code = "100";
                            request.Message = "No se ha podido identificar la invitación.";

                            requests.Add(request);
                        }
                    }
                    else
                    {
                        Request request = new Request();
                        request.Code = "200";

                        requests.Add(request);
                    }
                }
                catch (Exception ex)
                {
                    Request request = new Request();
                    request.Code = "500";
                    request.Message = "No se ha podido identificar la invitación.";

                    requests.Add(request);
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            
            return View(requests);
        }

        public ActionResult Postulacion()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult Tags()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult OfertasLaborales()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }
        
        #region "Metodos API"

        public string PostulanteCreaOActualizaFichaPostulante(string nombres, string apellidoPaterno, string apellidoMaterno, string telefono, string fechaNacimiento,
                        string direccion, string comuna, string correo, string rut)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.SerializeObject(CollectionsPostulante.__PostulanteCreaOActualizaFichaPostulante(
                "",
                nombres,
                apellidoPaterno,
                apellidoMaterno,
                telefono,
                fechaNacimiento,
                direccion,
                comuna,
                correo,
                Session["TokenOAuth"].ToString(),
                rut,
                objects[0].Token.ToString()
            ), Formatting.Indented);

        }

        public string PostulanteValidaFichaPersonal(string DNI, string tipoDNI)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.SerializeObject(CollectionsPostulante.__PostulanteValidaFichaPersonal(DNI, tipoDNI, objects[0].Token.ToString()), Formatting.Indented);
        }

        public string PostulanteConsultaFichaPersonal(string DNI)
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.SerializeObject(CollectionsPostulante.__PostulanteConsultaFichaPersonal(DNI, objects[0].Token.ToString()), Formatting.Indented);
        }

        public string PostulanteConsultaFieldEncuesta()
        {
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.SerializeObject(CollectionsPostulante.__PostulanteConsultaFieldEncuesta(Session["TokenOAuth"].ToString(), objects[0].Token.ToString()), Formatting.Indented);
        }

        #endregion


        public string ListarTag(string filterType = "", string dataFilter = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ListarTag(filterType, dataFilter, pagination, ""), Formatting.Indented);
        }

        public string CrearTag(string descripcion, string fechaDesde = "", string fechaHasta = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__CrearTag(descripcion, Session["NombreUsuario"].ToString(), fechaDesde, fechaHasta, ""), Formatting.Indented);
        }

        public string ActualizarTag(string tag, string action, string descripcion = "", string status = "", string fechaDesde = "", string fechaHasta = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ActualizarTag(tag, action, Session["NombreUsuario"].ToString(), descripcion, status, fechaDesde, fechaHasta, ""), Formatting.Indented);
        }

        public string EliminarTag(string tag)
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__EliminarTag(tag, ""), Formatting.Indented);
        }

        public string ListarOfertasLaborales(string filterType = "", string dataFilter = "", string target = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ListarOfertasLaborales(filterType, dataFilter, target, Session["NombreUsuario"].ToString(),  pagination, "", ""), Formatting.Indented);
        }

        public string ListarTarget()
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ListarTarget(""), Formatting.Indented);
        }

        public string ActualizarOfertaLaboral(string idOfertaLaboral, string descripcionCorta, string descripcionLarga = "" , string fechaInicio = "", string fechaTermino = "", string target = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ActualizarOfertaLaboral(idOfertaLaboral, descripcionLarga, descripcionCorta, fechaInicio, fechaTermino, target, ""), Formatting.Indented);
        }

        public string ListarTagOfertaLaboral(string idOfertaLaboral, string filterType = "", string dataFilter = "", string accion = "", string pagination = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__ListarTagOfertaLaboral(idOfertaLaboral, filterType, dataFilter, accion, pagination, ""), Formatting.Indented);
        }

        public string CrearTagOfertaLaboral(string idOfertaLaboral = "", string descripcionTag = "", string categoria = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__CrearTagOfertaLaboral(idOfertaLaboral, descripcionTag, categoria, ""), Formatting.Indented);
        }

        public string EliminarTagOfertaLaboral(string idOfertaLaboral = "", string tag = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__EliminarTagOfertaLaboral(idOfertaLaboral, tag, ""), Formatting.Indented);
        }

        public string Pagination(string pagination, string typePagination, string filterType = "", string dataFilter = "", string target = "", string idOfertaLaboral = "")
        {
            return JsonConvert.SerializeObject(CollectionSeleccion.__Pagination(pagination, typePagination, filterType, dataFilter, target, idOfertaLaboral, Session["NombreUsuario"].ToString(),  ""), Formatting.Indented);
        }

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
using FiniquitosV2.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;

namespace FiniquitosV2
{
    public partial class solicitudBajaOUT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Usuario"] != null)
            //{
            //    if (Session["Tipo"].ToString() == "2")
            //    {
            //        linkCalculoBaja.Visible = false;
            //        linkInicio.Visible = false;
            //        linkBasePagos.Visible = false;
            //    }
            //    else if (Session["Tipo"].ToString() == "1")
            //    {
                    
            //    }
            //    else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
            //    {
            //        linkBasePagos.Visible = false;
            //        linkRenunciasRecibidas.Visible = false;
            //        linkSolicitudBaja.Visible = false;
            //        linkBasePagos.Visible = false;
            //        Response.Redirect("xmlsolb4j.aspx");
            //    }
            //    else if (Session["Tipo"].ToString() == "7")
            //    {
            //        linkRenunciasRecibidas.Visible = false;
            //        linkSolicitudBaja.Visible = false;
            //        linkCalculoBaja.Visible = false;
            //        Response.Redirect("Inicio.aspx");
            //    }
            //}
            //else
            //{
            //    Response.Redirect("Login.aspx");
            //}   
            
        }
        
        /** WEBMETHOD INIT */

        [WebMethod()]
        public static string GetValidarPermitidoSolBj4(string rut)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                rut
            };

            return JsonConvert.SerializeObject(svcSoftland.GetValidarPermitidoSolBj4(parametros).Table, Formatting.Indented);

        }

        [WebMethod()]
        public static string GetRutTrabajador(string rut)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                rut
            };

            return JsonConvert.SerializeObject(svcSoftland.GetRutTrabajador(parametros).Table, Formatting.Indented);

        }

        [WebMethod()]
        public static string GetListarContratosFiniquitados(string rut)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                rut
            };

            return JsonConvert.SerializeObject(svcSoftland.GetListarContratosFiniquitados(parametros).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetContratoActivoBaja(string rut, string ficha)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                rut,
                ficha
            };

            return JsonConvert.SerializeObject(svcSoftland.GetContratoActivoBaja(parametros).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerAreaNegocio(string ficha)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                ficha
            };

            return JsonConvert.SerializeObject(svcSoftland.GetObtenerAreaNegocio(parametros).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerCargo(string ficha)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                ficha
            };

            return JsonConvert.SerializeObject(svcSoftland.GetObtenerCargo(parametros).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerCausalesDespido()
        {
            ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

            return JsonConvert.SerializeObject(svcFiniquitos.GetCausales().Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetCentroCosto(string ficha)
        {
            ServicioSoftland.ServicioSoftlandClient svcSoftland = new ServicioSoftland.ServicioSoftlandClient();

            string[] parametros = {
                "SOFTLAND_OUT",
                ficha
            };

            return JsonConvert.SerializeObject(svcSoftland.GetCentroCosto(parametros).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static string SetCrearSolicitudB4J(string rut, string fechaDesvinculcacion, string nombreTrabajador, string ficha, string cargo, string cargoMod, 
                                                  string areaNegocio, string cliente, string fechaInicio, string fechaTermino, 
                                                  string estado, string causal, string observacion, string agencia,
                                                  string adcFiles, string adcDates, string adcExtensions, string adccfiles, string adccdates, string adccExtensions)
        {
            ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

            string[] parametros = {
                "@RUT",
                "@FECHADESVINCULACION",
                "@NOMBRETRABAJADOR",
                "@FICHA",
                "@CARGO",
                "@CARGOMOD",
                "@AREANEGOCIO",
                "@CLIENTE",
                "@FECHAINICONTRATO",
                "@FECHATERMCONTRATO",
                "@ESTADO",
                "@CAUSAL",
                "@OBSERVACION",
                "@ADCFILES",
                "@ADCDATES",
                "@ADCEXTENSIONS",
                "@ADCCFILES",
                "@ADCCDATES",
                "@ADCCEXTENSIONS",
                "@EJECUTIVO",
                "@EMPRESA",
                "@CENTROCOSTO"
            };

            string[] valores = {
                rut,
                fechaDesvinculcacion,
                nombreTrabajador,
                ficha,
                cargo,
                cargoMod,
                areaNegocio,
                cliente,
                fechaInicio,
                fechaTermino,
                estado,
                causal,
                observacion,
                adcFiles,
                adcDates,
                adcExtensions,
                adccfiles,
                adccdates,
                adccExtensions,
                /*HttpContext.Current.Session["Usuario"].ToString()*/
                "ssalas@team-work.cl",
                "2",
                agencia
            };

            return JsonConvert.SerializeObject(svcFiniquitos.SetCrearSolicitudB4J(parametros, valores).Table, Formatting.Indented);
        }

        [WebMethod()]
        public static void GetCorreoElectronicoTeamwork(string transaccion)
        {
            ServicioCorreo.ServicioCorreoClient svcCorreo = new ServicioCorreo.ServicioCorreoClient();
            ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

            string xml = string.Empty;

            xml = "<plantillas><correo TRANSACCION='" + transaccion + "' /></plantillas>";

            string[] parametros = {
                "@PLANTILLA",
                "@DATA"
            };

            string[] valores = {
                "C_SOLB4J",
                xml.Replace(@"'", @"""")
            };

            foreach (DataRow rows in svcFiniquitos.GetPlantillasCorreo(parametros, valores).Table.Tables[0].Rows)
            {
                svcCorreo.correoTeamworkEstandar(rows["DE"].ToString(), 
                                                 rows["CLAVEDE"].ToString(), 
                                                 rows["PARA"].ToString(), 
                                                 rows["HTML"].ToString(), 
                                                 rows["ASUNTO"].ToString(),
                                                 rows["CC"].ToString(),
                                                 rows["ATTACHEMENT"].ToString(),
                                                 "High");
            }

            
        }

        /** WEBMETHOD END */
        
    }
}
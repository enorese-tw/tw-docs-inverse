using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiniquitosV2
{
    public partial class TokenAuth : System.Web.UI.Page
    {
        ServicioAuth.ServicioAuthClient servicioAuth = new ServicioAuth.ServicioAuthClient();
        string tokenAuth = "Base U2VydmljaW8uQXV0aEBTZXJ2aWNpby5BdXRo";
        string agenteAplication = "AGENT_WEBSERVICE_APP";

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Clear();

            string pathNew = string.Empty;
            string owner = string.Empty;
            string ownerPassword = string.Empty;

            DataSet dataBase;

            string[] paramBase64 = new string[4];
            string[] valBase64 = new string[4];

            paramBase64[0] = "@AUTHENTICATE";
            paramBase64[1] = "@AGENTAPP";
            paramBase64[2] = "@TYPEBASE64";
            paramBase64[3] = "@DATA";

            /** PARA OBTENER OWNER => USUARIO */

            valBase64[0] = tokenAuth;
            valBase64[1] = agenteAplication;
            valBase64[2] = "DECODE";
            valBase64[3] = Request.QueryString["owner"].ToString();

            dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

            foreach (DataRow rows in dataBase.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    owner = rows["Data"].ToString();
                }
            }

            /** PARA OBTENER OWNERPASSWORD => CLAVE DE USUARIO */

            valBase64[0] = tokenAuth;
            valBase64[1] = agenteAplication;
            valBase64[2] = "DECODE";
            valBase64[3] = Request.QueryString["ownerPassword"].ToString();

            dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

            foreach (DataRow rows in dataBase.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    ownerPassword = rows["Data"].ToString();
                }
            }

            /** AUTENTIFICACION DE USUARIO */

            Clases.Usuario user = new Clases.Usuario();
            user.correo = owner;
            user.clave = ownerPassword;
            if (user.Login(Properties.Settings.Default.connectionString))
            {
                Session["Usuario"] = owner;
                Session["tipo"] = user.idtipo;
                Session["ApplicationIntegrationOnOperaciones"] = "S";
            }


            if (Request.QueryString["authorizate"].ToString() != "ZGlyZWN0bHk=")
            {
                /** OBTENCION DE RUT A CONSULTAR */

                valBase64[0] = tokenAuth;
                valBase64[1] = agenteAplication;
                valBase64[2] = "DECODE";
                valBase64[3] = Request.QueryString["rut"].ToString();

                dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                foreach (DataRow rows in dataBase.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Session["ApplicationIntegrationOnOperacionesRut"] = rows["Data"].ToString();
                    }
                }

                /** OBTENERCION DE FICHA A CONSULTAR */

                valBase64[0] = tokenAuth;
                valBase64[1] = agenteAplication;
                valBase64[2] = "DECODE";
                valBase64[3] = Request.QueryString["ficha"].ToString();

                dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                foreach (DataRow rows in dataBase.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Session["ApplicationIntegrationOnOperacionesFicha"] = rows["Data"].ToString();
                    }
                }

                /** OBTENCION DE CAUSAL */

                valBase64[0] = tokenAuth;
                valBase64[1] = agenteAplication;
                valBase64[2] = "DECODE";
                valBase64[3] = Request.QueryString["causal"].ToString();

                dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;

                foreach (DataRow rows in dataBase.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        Session["ApplicationIntegrationOnOperacionesCausal"] = rows["Data"].ToString();
                    }
                }

                if (Request.QueryString["token"] == null)
                {
                    pathNew = "&new=ok";
                }
                else
                {
                    pathNew = "&simulate=ok";
                }
            }
            else
            {
                pathNew = "&event=simulation";
            }

            if (Request.QueryString["callback"] != null)
            {
                pathNew += "&callback=" + Request.QueryString["callback"].ToString();
            }

            if (Request.QueryString["destiny"] != null)
            {
                pathNew += "&destiny=" + Request.QueryString["destiny"].ToString();
            }

            if (Request.QueryString["glcid"] != null)
            {
                pathNew += "&glcid=" + Request.QueryString["glcid"].ToString();
            }

            valBase64[0] = tokenAuth;
            valBase64[1] = agenteAplication;
            valBase64[2] = "DECODE";
            valBase64[3] = Request.QueryString["enterprise"].ToString();

            dataBase = servicioAuth.GetBASE64(paramBase64, valBase64).Table;
            
            foreach (DataRow rows in dataBase.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    switch (rows["Data"].ToString())
                    {
                        case "EST":
                            Response.Redirect("CalculoBajaEST?ref=" + Request.QueryString["authorizate"].ToString() + pathNew);
                            break;
                        case "OUT":
                            Response.Redirect("CalculoBajaOUT?ref=" + Request.QueryString["authorizate"].ToString() + pathNew);
                            break;
                        case "TWC":
                            Response.Redirect("CalculoBajaCONSULTORA?ref=" + Request.QueryString["authorizate"].ToString() + pathNew);
                            break;
                    }
                }
            }
            
        }
    }
}
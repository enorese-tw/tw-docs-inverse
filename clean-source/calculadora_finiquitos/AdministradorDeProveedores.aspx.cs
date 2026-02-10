using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using FiniquitosV2.Clases;

namespace FiniquitosV2
{
    public partial class AdministradorDeProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                if (Session["Tipo"].ToString() == "2")
                {
                    Response.Redirect("RenunciasRecibidas.aspx");
                }
                else if (Session["Tipo"].ToString() == "1")
                {
                }
                else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
                {
                    Response.Redirect("Inicio.aspx");
                }
                else if (Session["Tipo"].ToString() == "7")
                {
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkCalculoBaja.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "3")
                {
                    Response.Redirect("RecepcionDocumentos.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        [WebMethod()]
        public static string GetObtenerProveedoresProveedorService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            response = JsonConvert.SerializeObject(web.GetObtenerProveedoresProveedorService, Formatting.Indented);
            return response;
        }

    }
}
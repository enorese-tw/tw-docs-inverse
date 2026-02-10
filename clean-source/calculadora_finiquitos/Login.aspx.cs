using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiniquitosV2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["ref"] != null && Request.QueryString["action"] != null)
            {
                if (Request.QueryString["ref"].ToString() == "ZGFtZSBhY2Nlc28gc295IGFkbWluaXN0cmFkb3IgdHJvbGw=" && Request.QueryString["action"].ToString() == "admin")
                {
                    Session.Remove("Usuario");
                    Session["idDeCalculo"] = null;

                    Session["ApplicationForAdminCalculo"] = true;
                }
                else
                {
                    Session.Remove("Usuario");
                    Session["idDeCalculo"] = null;

                    Session["ApplicationForAdminCalculo"] = false;
                }
            }
            else
            {
                Session.Remove("Usuario");
                Session["idDeCalculo"] = null;

                Session["ApplicationForAdminCalculo"] = false;
            }
        }

        protected void aceptar_Click(object sender, EventArgs e)
        {
            Clases.Usuario user = new Clases.Usuario();
            user.correo= email.Text;
            user.clave = password.Text;
            if (user.Login(Properties.Settings.Default.connectionString))
            {
                Session["Usuario"] = email.Text;
                Session["tipo"] = user.idtipo;
                if (Session["tipo"].Equals(3)) 
                {
                    Response.Redirect("RecepcionDocumentos.aspx");
                }
                else if (Session["tipo"].Equals(1) || Session["tipo"].Equals(2) || Session["tipo"].Equals(4) || Session["tipo"].Equals(7) || Session["tipo"].Equals(6) || Session["tipo"].Equals(8))
                {
                    Response.Redirect("Inicio.aspx");
                }
            }
            else
            {
                Labelerror.Text = "Usuario o Contraseña Incorrecta";
                Labelerror.Visible = true;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiniquitosV2
{
    public partial class RenunciasRecibidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                string sessionuser = Session["Usuario"].ToString();
                Clases.RecepcionDocumentos renuncias = new Clases.RecepcionDocumentos();
                gvRenuncias.DataSource = null;
                gvRenuncias.DataSource = Clases.RecepcionDocumentos.recibidoxkam(Properties.Settings.Default.connectionString, sessionuser);
                gvRenuncias.DataBind();
                gvRenuncias.Columns[8].Visible = false;
                //gvRenuncias.Columns[8].Visible = false;
                if (gvRenuncias.DataSource == null)
                {
                    lblMensaje.Visible = true;
                    lblMensaje.Text = "No se han recibido renuncias asociadas a sus unidades de negocio";

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

            Clases.RecepcionDocumentos r = new Clases.RecepcionDocumentos();
            r.KAM = Session["Usuario"].ToString();
            int cantidad = r.cantidadnoregistrado(Properties.Settings.Default.connectionString);
            if (cantidad == 0)
            {
                lblNoti.Visible = false;
            }
            else
            {
                lblNoti.Visible = true;
                lblNoti.Text = cantidad.ToString();
            }
            
        }

        protected void gvRenuncias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string registrado = (DataBinder.Eval(e.Row.DataItem, "REGISTRADA")).ToString();
                if (registrado.Equals("si"))
                {
                    //e.Row.BackColor = System.Drawing.Color.LightGreen;
                    //e.Row.CssClass = "VERDE";
                    HyperLink hlnk = new HyperLink();
                    hlnk.ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/50/Yes_Check_Circle.svg/240px-Yes_Check_Circle.svg.png";
                    hlnk.ImageWidth = 17;
                    e.Row.Cells[7].Controls.Add(hlnk);
                }
                else if (registrado.Equals("no"))
                {
                    //e.Row.BackColor = System.Drawing.Color.OrangeRed;
                    //e.Row.CssClass = "ROJO";
                    HyperLink hlnk = new HyperLink();
                    hlnk.ImageUrl = "https://cdn2.iconfinder.com/data/icons/color-svg-vector-icons-2/512/error_warning_alert_attention-512.png";
                    hlnk.ImageWidth = 17;
                    e.Row.Cells[7].Controls.Add(hlnk);
                }
            }
        }

        protected void gvRenuncias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empresa = gvRenuncias.SelectedRow.Cells[2].Text;
            string valor = gvRenuncias.SelectedRow.Cells[7].Text;
            if (valor.Equals("no"))
            {
                if (empresa.Equals("TWEST"))
                {
                    Session["rut"]= gvRenuncias.SelectedRow.Cells[1].Text;
                    Session["IDRENUNCIA"] = gvRenuncias.SelectedRow.Cells[0].Text;
                    Response.Redirect("solicitudBajaEST.aspx");

                }
                else if (empresa.Equals("TEAMRRHH"))
                {
                    Session["rut"] = gvRenuncias.SelectedRow.Cells[1].Text;
                    Session["IDRENUNCIA"] = gvRenuncias.SelectedRow.Cells[0].Text;
                    Response.Redirect("solicitudBajaOUT.aspx");
                }
                else if (empresa.Equals("TEAMWORK"))
                {
                    Session["rut"] = gvRenuncias.SelectedRow.Cells[1].Text;
                    Session["IDRENUNCIA"] = gvRenuncias.SelectedRow.Cells[0].Text;
                    Response.Redirect("solicitudBajaCONSULTORA.aspx");
                }
            }
            else {
                Response.Write("<script>alert('Ya se emitio una solicitud de baja de esta renuncia');</script>");
            }
        }

        protected void gvRenuncias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRenuncias.PageIndex = e.NewPageIndex;
            gvRenuncias.DataBind();
        }
    }
}
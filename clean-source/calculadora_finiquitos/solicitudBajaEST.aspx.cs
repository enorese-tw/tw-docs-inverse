using FiniquitosV2.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiniquitosV2
{
    public partial class solicitudBajaEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {
                if (Session["Tipo"].ToString() == "2")
                {
                    linkInicio.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "1")
                {

                }
                else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
                {
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "7")
                {
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkCalculoBaja.Visible = false;
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
            CargacCausal();

            Label8.Text = Session["Usuario"].ToString();

            if (Session["rut"] != null) {
                buscardesderenuncias();
                Session.Remove("rut");
                Button4.Enabled = false;
            }
        }

        protected void selected_index(object sender, EventArgs e)
        {
            Clases.Contrato contrato = new Clases.Contrato();
            contrato.rut = TextBox1.Text;
            string empresa = "TWEST";
            contrato.ficha = ddlContratos.SelectedItem.ToString();
            contrato.ContratoActivobaja(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa));
            TextBox7.Text = contrato.fechaIngreso.ToString();
            TextBox8.Text = contrato.FecTermContrato.ToString();
            TextBox9.Text = contrato.estudios;
            Clases.AreaNegocio arneg = new Clases.AreaNegocio();
            arneg.ficha = contrato.ficha;
            arneg.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa));
            TextBox5.Text = arneg.areanegocio;
            Clases.Cargo cargo = new Clases.Cargo();
            cargo.ficha = contrato.ficha;
            cargo.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa));
            TextBox3.Text = cargo.glosaCargo;
            TextBox4.Text = cargo.glosaMOd;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("IDRENUNCIA");
                if (TextBox1.Text != "")
                {
                    string empresa = "TWEST";
                    MethodServiceSoftland softland = new MethodServiceSoftland();
                    softland.RUTTRABAJADOR = TextBox1.Text;
                    foreach(DataRow rows in softland.GetRutTrabajadorSolicitudBajaService.Tables[0].Rows){
                        TextBox2.Text = rows["nombres"].ToString();
                    }

                    List<Clases.Contrato> con1 = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa), TextBox1.Text);
                    ddlContratos.Items.Clear();
                    ddlContratos.Items.Add("Seleccione");
                    foreach (Clases.Contrato contratos in con1)
                    {
                        ddlContratos.Items.Add(contratos.ficha);
                    }
                }
                else
                {
                    Response.Write("<script>alert('Para buscar debe ingresar rut');</script>");
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitudBajaEST.aspx");
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("solicitudBajaOUT.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitudBajaCONSULTORA.aspx");
        }
        private void CargacCausal()
        {
            if (!IsPostBack)
            {
                MethodServiceFiniquitos web = new MethodServiceFiniquitos();

                foreach (DataRow rows in web.GetCausalesService.Tables[0].Rows)
                {
                    CausalDespido.Items.Add(rows["UNIFICADO"].ToString());
                }
            }
        }

        public void buscardesderenuncias()
        {
            try
            {
                MethodServiceSoftland softland = new MethodServiceSoftland();
                softland.RUTTRABAJADOR = Session["rut"].ToString();
                TextBox1.Text = Session["rut"].ToString();
                foreach (DataRow rows in softland.GetRutTrabajadorSolicitudBajaService.Tables[0].Rows)
                {
                    TextBox2.Text = rows["nombres"].ToString();
                }
                string empresa = "TWEST";
                List<Clases.Contrato> con1 = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa), TextBox1.Text);
                ddlContratos.Items.Clear();
                ddlContratos.Items.Add("Seleccione");
                foreach (Clases.Contrato contratos in con1)
                {
                    ddlContratos.Items.Add(contratos.ficha);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (TextBox10.Text != "" && TextBox11.Text != "")
            {
                if (CausalDespido.SelectedIndex != 0)
                {
                    MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                    if (Session["IDRENUNCIA"] != null)
                    {
                        web.IDRENUNCIA = Session["IDRENUNCIA"].ToString();
                    }
                    else {
                        web.IDRENUNCIA = "0";
                    }

                    web.FECHAHORASOLICITUD = DateTime.Now.ToString();
                    string AHORA = DateTime.Now.ToString();
                    web.FECHAAVISODESVINCULACION = TextBox11.Text;
                    web.USUARIO = Label8.Text;
                    web.RUTTRABAJADOR = TextBox1.Text;
                    web.FICHA = ddlContratos.SelectedItem.ToString();
                    web.NOMBRETRABAJADOR = TextBox2.Text;
                    web.ESTADOSOLICITUD = "1";
                    web.FECHAESTADO = AHORA;
                    web.IDCAUSAL = CausalDespido.SelectedItem.ToString();
                    web.IDEMPRESA = "1";
                    web.OBSERVACION = TextBox10.Text;
                    web.CARGOMOD = TextBox4.Text;
                    web.CLIENTE = TextBox5.Text;
                    web.AREANEGOCIO = "";
                    web.ESPARTTIME = "";
                    web.ESZONAEXT = "";
                    web.IDDESVINCULACION = "0";
                    web.SOLICITUD = "S";
                            
                    foreach (DataRow rows in web.SetCrearModificarEncDesvinculacionService.Tables[0].Rows)
                    {
                        if(Convert.ToInt32(rows["VALIDACION"]) == 0){
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'success',title: 'Se ha guardado exitosamente', text: 'Solicitud guardado con " + rows["RESULTADO"].ToString() + "'});", true);
                        }else{
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error',title: 'No se puede guardar', text: '" + rows["RESULTADO"].ToString() + "'});", true);
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error',title: 'No se puede guardar', text: 'Debe seleccionar causal.'});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error',title: 'No se puede guardar', text: 'Debe completar a el campo contar de y/o observaciones'});", true);
            }
        }
    }
}
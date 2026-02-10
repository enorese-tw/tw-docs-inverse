using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FiniquitosV2.Clases;

namespace FiniquitosV2
{
    public partial class RecepcionDocumentos : System.Web.UI.Page
    {
        ServicioCorreo.ServicioCorreoClient svcCorreo = new ServicioCorreo.ServicioCorreoClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //validacion sesion de usuario
            if (Session["Usuario"] != null)
            {
                if (Session["Tipo"].ToString() == "2")
                {
                    Response.Redirect("RenunciasRecibidas");
                }
                else if (Session["Tipo"].ToString() == "3")
                {
                    linkInicio.Visible = false;
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkCalculoBaja.Visible = false;

                    /** RESPONSIVE */
                    linkBasePagosResponsive.Visible = false;
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                    linkInicioResponsive.Visible = false;
                    linkCalculoBajaResponsive.Visible = false;

                    Label8.Text = Session["Usuario"].ToString();
                    Label9.Text = DateTime.Now.ToString();
                }
                else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
                {
                    Response.Redirect("Inicio");
                }
                else if (Session["Tipo"].ToString() == "7")
                {
                    Response.Redirect("Inicio");
                }
            }
            else
            {
                Response.Redirect("Login");
            }

            Label9.Text = DateTime.Now.ToString();
            DateTime fecha = DateTime.Parse(Label9.Text);
            int mes = fecha.Month;
            int anio = fecha.Year;

            Clases.RecepcionDocumentos r = new Clases.RecepcionDocumentos();
            r.MES = mes;
            r.YEAR = anio;
            int cantidadmes = r.cantidadregistrosmes(Properties.Settings.Default.connectionString);
            Label14.Text = mes.ToString();
            if (cantidadmes == 0)
            {
                Label15.Text = mes.ToString() + "001";
            }
            else
            {
                int maximo = r.maxrenunciames(Properties.Settings.Default.connectionString);
                Label15.Text = (maximo + 1).ToString();

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                Clases.Contrato contrato = new Clases.Contrato();
                if (TextBox1.Text != "" && cmbEmpresa.SelectedIndex != 0)
                {
                    contrato.rut = TextBox1.Text;

                    if (contrato.ConsultarRutExistenteRecepcion(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", cmbEmpresa.SelectedItem)) > 0)
                    {
                        contrato.hoy = DateTime.Now.ToString("dd/MM/yyyy");
                        contrato.ContratoActivobaja(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", cmbEmpresa.SelectedItem));
                        TextBox3.Text = contrato.nombres;
                        string ficha = contrato.ficha;
                        string estudios = contrato.estudios;
                        TextBox4.Text = estudios;

                        Clases.AreaNegocio arneg = new Clases.AreaNegocio();
                        arneg.ficha = ficha;
                        arneg.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", cmbEmpresa.SelectedItem));
                        TextBox2.Text = arneg.areanegocio;
                        string negocio = TextBox2.Text;
                        GridView1.DataSource = Clases.RecepcionDocumentos.buscarKAM(Properties.Settings.Default.connectionString, negocio);
                        GridView1.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'warning',title: 'Atención!', text: 'El trabajador consultado no existe o no pertenece a la empresa seleccionada.'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'warning', title: 'Atención!', text: 'Por favor complete Rut Trabajador y Empresa'});", true);
                }
            }
            catch(Exception ex){
                throw ex;
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox2.Checked = false;
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox3.Text != "")
                {
                    if (TextBox5.Text != "" || TextBox6.Text != "")
                    {
                        if (GridView1.Rows.Count > 0)
                        {
                            Clases.RecepcionDocumentos r = new Clases.RecepcionDocumentos();
                            r.ID = int.Parse(Label15.Text);
                            r.RUTTRABAJADOR = TextBox1.Text;
                            r.IDEMPRESA = int.Parse(cmbEmpresa.SelectedValue);
                            string EMPRESA = cmbEmpresa.SelectedItem.ToString();
                            r.NOMBRETRABAJADOR = TextBox3.Text;
                            r.NEGOCIO = TextBox2.Text;
                            r.ESTADO = TextBox4.Text;
                            r.CAUSAL = TextBox7.Text;
                            r.DESDE = TextBox5.Text + " 00:00:00";
                            r.OBSERVACION = TextBox6.Text;
                            r.REGISTRADA = "no";
                            r.VISTONOTIFICACION = "no";
                            r.FECHAHORARECEPCION = DateTime.Parse(Label9.Text).ToString("MM-dd-yyyy HH:mm:ss");
                            r.KAM = GridView1.Rows[0].Cells[0].Text;
                            if (CheckBox1.Checked == true)
                            {
                                r.LEGALIZADA = "Si";
                                string de = "recepcion@team-work.cl";
                                string clave = "Rtw,3321.";
                                string parakam = r.KAM;
                                string paraTI = "ssalas@team-work.cl";
                                string paraOperaciones = "operaciones@team-work.cl";
                                //string paraFinanzas = "ihernandez@team-work.cl";
                                //string paraFini = "ksoto@team-work.cl";
                                string paraFini2 = "lstancovish@team-work.cl";
                                string testing = "N";
                                string mensaje = "Estimados, <br> Junto con saludar, informo que ha llegado renuncia legalizada de la siguiente persona:<br><br> " +
                                        "<table border=1 cellspacing=0 cellpadding=2 bordercolor='#BFC0C2'><tr><td>EMPRESA </td><td>:</td><td>" + EMPRESA + "</td></tr>" +
                                        "<tr><td>TRABAJADOR</td><td>:</td><td>" + r.NOMBRETRABAJADOR + "</td></tr>" +
                                        "<tr><td>RUT</td><td>:</td><td>" + r.RUTTRABAJADOR + "</td></tr>" +
                                        "<tr><td>A CONTAR DE</td><td>:</td><td>" + r.DESDE + "</td></tr>" +
                                        "<tr><td>LUGAR DE TRABAJO</td><td>:</td><td>" + r.NEGOCIO + "</td></tr></table><br><br>" +
                                        "Saludos cordiales,<br>" +
                                        "<img src='https://image.ibb.co/iXe1mS/firma.jpg'/>";
                                string asunto = "Renuncia " + r.NOMBRETRABAJADOR;
                                string copia = string.Empty;
                                if (testing == "N") {
                                    if (r.NEGOCIO == "APOYO PROVEEDORES Y BIBLIOTECARIO" ||
                                        r.NEGOCIO == "CHILECTRA" ||
                                        r.NEGOCIO == "ENERSIS AMERICA" ||
                                        r.NEGOCIO == "ENERSIS (CHILECTRA)" ||
                                        r.NEGOCIO == "ENERSIS (ENDESA)" ||
                                        r.NEGOCIO == "ENERSIS (GAS ATACAMA)" ||
                                        r.NEGOCIO == "ENERSIS" ||
                                        r.NEGOCIO == "ENEL MEDIA RELATIONS" ||
                                        r.NEGOCIO == "PLANIFICACION Y CONTROL ENEL" ||
                                        r.NEGOCIO == "ENEL CHILE S.A" ||
                                        r.NEGOCIO == "ENEL PYC SOUTH AMERICA" ||
                                        r.NEGOCIO == "SERVICIOS GENERALES ENEL" ||
                                        r.NEGOCIO == "SERVICIO DE APOYO SMART METERS" ||
                                        r.NEGOCIO == "ENEL MONITORES" ||
                                        r.NEGOCIO == "ENERSIS (ENERSIS)" ||
                                        r.NEGOCIO == "ENEL GREEN POWER" || 
                                        r.NEGOCIO == "ENEL APOYO PROCUREMENT")
                                    {
                                        copia = paraTI + ";" + paraOperaciones + ";" + paraFini2;
                                    }
                                    else
                                    {
                                        copia = paraTI + ";" + paraOperaciones + ";" + paraFini2;
                                    }
                                    //new Correo().enviar(de, clave, parakam, mensaje, asunto, copia);
                                    svcCorreo.correoStandar(de, clave, parakam, mensaje, asunto, copia);
                                }
                                else
                                {
                                    //new Correo().enviar(de, clave, paraTI, mensaje, asunto, copia);
                                    svcCorreo.correoStandar(de, clave, parakam, mensaje, asunto, copia);
                                }

                            }
                            else if (CheckBox2.Checked == true)
                            {
                                r.LEGALIZADA = "No";
                                string de = "recepcion@team-work.cl";
                                string clave = "Rtw,3321.";
                                string parakam = r.KAM;
                                string paraTI = "ssalas@team-work.cl";
                                //string paraFinanzas = "ihernandez@team-work.cl";
                                string paraOperaciones = "operaciones@team-work.cl";
                               // string paraFini = "ksoto@team-work.cl";
                                string paraFini2 = "lstancovish@team-work.cl";
                                string testing = "N";
                                string mensaje = "Estimados, <br> Junto con saludar, informo que ha llegado renuncia no legalizada de la siguiente persona:<br><br> " +
                                        "<table border=1 cellspacing=0 cellpadding=2 bordercolor='#BFC0C2'><tr><td>EMPRESA </td><td>:</td><td>" + EMPRESA + "</td></tr>" +
                                        "<tr><td>TRABAJADOR</td><td>:</td><td>" + r.NOMBRETRABAJADOR + "</td></tr>" +
                                        "<tr><td>RUT</td><td>:</td><td>" + r.RUTTRABAJADOR + "</td></tr>" +
                                        "<tr><td>A CONTAR DE</td><td>:</td><td>" + r.DESDE + "</td></tr>" +
                                        "<tr><td>LUGAR DE TRABAJO</td><td>:</td><td>" + r.NEGOCIO + "</td></tr></table><br><br>" +
                                        "Saludos cordiales,<br>" +
                                    "<img src='https://image.ibb.co/iXe1mS/firma.jpg'/>";
                                string asunto = "Renuncia " + r.NOMBRETRABAJADOR;
                                string copia = string.Empty;
                                if (testing == "N")
                                {
                                    if (r.NEGOCIO == "APOYO PROVEEDORES Y BIBLIOTECARIO" ||
                                        r.NEGOCIO == "CHILECTRA" ||
                                        r.NEGOCIO == "ENERSIS AMERICA" ||
                                        r.NEGOCIO == "ENERSIS (CHILECTRA)" ||
                                        r.NEGOCIO == "ENERSIS (ENDESA)" ||
                                        r.NEGOCIO == "ENERSIS (GAS ATACAMA)" ||
                                        r.NEGOCIO == "ENERSIS" ||
                                        r.NEGOCIO == "ENEL MEDIA RELATIONS" ||
                                        r.NEGOCIO == "PLANIFICACION Y CONTROL ENEL" ||
                                        r.NEGOCIO == "ENEL CHILE S.A" ||
                                        r.NEGOCIO == "ENEL PYC SOUTH AMERICA" ||
                                        r.NEGOCIO == "SERVICIOS GENERALES ENEL" ||
                                        r.NEGOCIO == "SERVICIO DE APOYO SMART METERS" ||
                                        r.NEGOCIO == "ENEL MONITORES" ||
                                        r.NEGOCIO == "ENERSIS (ENERSIS)" ||
                                        r.NEGOCIO == "ENEL GREEN POWER" ||
                                        r.NEGOCIO == "ENEL APOYO PROCUREMENT")
                                    {
                                        copia = paraTI + ";" + paraOperaciones + ";" + paraFini2;
                                    }
                                    else
                                    {
                                        copia = paraTI + ";" + paraOperaciones + ";" + paraFini2;
                                    }
                                    //new Correo().enviar(de, clave, parakam, mensaje, asunto, copia);

                                    svcCorreo.correoStandar(de, clave, parakam, mensaje, asunto, copia);
                                }
                                else
                                {
                                    //new Correo().enviar(de, clave, paraTI, mensaje, asunto, copia);
                                    svcCorreo.correoStandar(de, clave, parakam, mensaje, asunto, copia);
                                }

                            }
                            r.GrabarRenuncia(Properties.Settings.Default.connectionString);
                            limpiar();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "ok();", true);
                            
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "errorkam();", true);
                        }
                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'warning', title: 'Atención!', text: 'Faltan datos a llenar'});", true);
                    }
                }
                else 
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error', title: 'Atención!', text: 'Debe buscar los datos de la persona antes de registrar una renuncia'});", true);
                }
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public void limpiar() {
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            cmbEmpresa.SelectedIndex = 0;
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    }
}
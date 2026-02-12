using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using FiniquitosV2.Clases;
using System.IO;
using OfficeOpenXml;
using Finiquitos.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using OfficeOpenXml.Style;
using System.Threading;
using FiniquitosV2.api;

namespace FiniquitosV2
{
    public partial class CalculoBajaEst : System.Web.UI.Page
    {
        System.Data.DataTable table;
        System.Data.DataRow row;
        decimal dTotal = 0;
        private static string strSql;
        private static DataSet ds;
        int totalhaberes = 0;
        string Ficha1;
        string ConexionVariable;
        DataTable dt, dtusuario;
        int DiasTotal, DiasFaltante, SueldoParcial, Fin_Calculo, FlagDias = 0, j = 2;
        DataTable dtDescuento, dtOtrosHaberes, dtOtrosHaberes1;
        List<string> periodosRem;

        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnRecalculo125.Visible = false;
                btnRecalculo175.Visible = false;

                #region "Codigo que se carga al entrar a la pagina"
                MethodServiceFiniquitos web = new MethodServiceFiniquitos();

                dtDescuento = new DataTable();
                dtDescuento.Columns.Add("Descuento");
                dtDescuento.Columns.Add("Valor");
                GridView4.DataSource = dtDescuento;
                GridView4.DataBind();
                Session["datos"] = dtDescuento;

                dtOtrosHaberes = new DataTable();
                dtOtrosHaberes.Columns.Add("Descuento");
                dtOtrosHaberes.Columns.Add("Valor");
                GridView5.DataSource = dtOtrosHaberes;
                GridView5.DataBind();
                Session["datos2"] = dtOtrosHaberes;
                
                if (Session["Usuario"] != null)
                {
                    if (Session["Tipo"].ToString() == "2")
                    {
                        Response.Redirect("RenunciasRecibidas");
                        linkInicio.Visible = false;

                        /** RESPONSIVE */
                        linkInicioResponsive.Visible = false;
                    }
                    else if (Session["Tipo"].ToString() == "3")
                    {
                        Response.Redirect("RecepcionDocumentos");
                    }
                    else if (Session["Tipo"].ToString() == "1")
                    {

                    }
                    else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
                    {
                        linkBasePagos.Visible = false;
                        linkRenunciasRecibidas.Visible = false;
                        linkSolicitudBaja.Visible = false;

                        /** RESPONSIVE */
                        linkBasePagosResponsive.Visible = false;
                        linkRenunciasRecibidasResponsive.Visible = false;
                        linkSolicitudBajaResponsive.Visible = false;
                    }
                    else if (Session["Tipo"].ToString() == "7")
                    {
                        linkRenunciasRecibidas.Visible = false;
                        linkSolicitudBaja.Visible = false;
                        linkCalculoBaja.Visible = false;

                        /** RESPONSIVE */
                        linkRenunciasRecibidasResponsive.Visible = false;
                        linkSolicitudBajaResponsive.Visible = false;
                        linkCalculoBajaResponsive.Visible = false;
                    }
                }
                else
                {
                    Response.Redirect("Login");
                }

                if (Session["rut"] != null)
                {
                    if (Session["FolioSolicitudBaja"] == null)
                    {
                        buscarcontratos();
                        Session.Remove("rut");
                    }
                }

                if (Session["error"] != null)
                {
                    Label21.Text = Session["error"].ToString();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "error();", true);
                    Session.Remove("error");
                }


                Label10.Text = Session["Usuario"].ToString();

                CargacCausal();
                CargaDocumentos();
                //cargarUF();
                CargaOtrosHaberes();
                CargaDescuentos();

                Session["datos"] = dtDescuento;
                Session["datos2"] = dtOtrosHaberes;
                Label12.Text = DateTime.Now.ToString();
                TextBox1.Focus();
                Session["isPosibleGuardar"] = false;
                Session["errorGeneradoGuardarCalculo"] = "<label>No ha realizado el cálculo.</label><br/>";

                if (Session["FolioSolicitudBaja"] != null)
                {
                    Label20.Text = "Folio: " + Session["FolioSolicitudBaja"].ToString();
                    TextBox6.Text = Session["rut"].ToString();
                    Clases.Contrato contrato = new Clases.Contrato();
                    contrato.rut = Session["rut"].ToString();
                    List<Clases.Contrato> con1 = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), TextBox6.Text);
                    ddlContratos.Items.Add("Seleccione");
                    foreach (Clases.Contrato contratos in con1)
                    {
                        ddlContratos.Items.Add(contratos.ficha);
                    }

                    List<Clases.Contrato> con = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), TextBox6.Text);
                    GridView1.DataSource = con;
                    GridView1.DataBind();

                    web.IDDESVINCULACION = Session["FolioSolicitudBaja"].ToString();

                    foreach(DataRow rows in web.GetFiniquitosSolicitudBajaService.Tables[0].Rows)
                    {
                        if(Convert.ToInt32(rows["VALIDACION"]) == 0)
                        {
                            TextBox2.Text = rows["NOMBRECOMPLETOTRABAJADOR"].ToString();
                            TextBox3.Text = rows["FECHAAVISODESVINCULACION"].ToString().Split(' ')[0];
                            ddlContratos.SelectedValue = rows["FICHATRABAJADOR"].ToString();
                            ddlContratos.Enabled = false;
                            CausalDespido.SelectedValue = rows["CAUSAL"].ToString();
                            CausalDespido.Enabled = false;
                            Label18.Text = rows["ESTADOSOLICITUD"].ToString();

                            contrato.centrodecosto(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), ddlContratos.SelectedItem.ToString());
                            lblcentrocosto.Text = contrato.centrocosto;

                            Clases.Cargo cargo = new Clases.Cargo();

                            /** CLASE QUE TRAE EL CARGO ASOCIADO */
                            cargo.ficha = ddlContratos.SelectedItem.ToString();
                            cargo.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                            TextBox5.Text = cargo.glosaCargo;
                            TextBox7.Text = cargo.glosaMOd;

                            /** CLASE QUE TRAE EL AREA DE NEGOCIO ASOCIADA */
                            Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                            anegocio.ficha = ddlContratos.SelectedItem.ToString();
                            anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                            TextBox8.Text = anegocio.areanegocio;
                            TextBox9.Text = anegocio.codanrg;
                        } 
                        else 
                        {
                            
                        }
                    }

                    Session.Remove("rut");
                    Session.Remove("FolioSolicitudBaja");

                }

                /** alertas */
                sobregiro.Visible = false;
                clausulaccaf.Visible = false;
                licenciaACHS.Visible = false;
                comentariosOper.Visible = false;
                retencionjudicial.Visible = false;
                lucrocesante.Visible = false;

                if (Descuento0.SelectedItem.ToString() == "Remuneracion Pendiente")
                {
                    remupendAdc.Visible = true;
                    LblRemunPendAdc.Visible = true;
                }
                else
                {
                    remupendAdc.Visible = false;
                    LblRemunPendAdc.Visible = false;
                }

                Session["ApplicationCalculoContrario"] = true;
                Session["ApplicationCalculoContrarioRealFactor"] = "";
                Session["ApplicationRecalculadoFactor"] = false;

                #endregion

                #region "Codigo de integracion de operaciones"

                if (Request.QueryString["new"] != null)
                {
                    Session["ApplicationIsPostBack"] = null;
                }

                if (Request.QueryString["ref"] != null && Request.QueryString["new"] == null)
                {
                    if (Session["ApplicationIsPostBack"] != null)
                    {
                        if (!(bool)Session["ApplicationIsPostBack"])
                        {
                            btnBuscar.Visible = false;
                            menuNavegationIntegrationOperaciones.Visible = false;

                            ddlContratos.SelectedValue = Session["ApplicationIntegrationOnOperacionesFicha"].ToString();
                            CausalDespido.SelectedValue = Session["ApplicationIntegrationOnOperacionesCausal"].ToString();
                            TextBox6.Enabled = false;
                            ddlContratos.Enabled = false;
                            CausalDespido.Enabled = false;
                            btnImprimir.Visible = false;
                            btnGeneraPdf.Visible = false;
                            btnGenerarArchivo.Visible = false;
                            selected_index(sender, e);
                        }
                    }
                    else
                    {
                        menuNavegationIntegrationOperaciones.Visible = false;
                        btnImprimir.Visible = false;
                        btnGeneraPdf.Visible = false;
                        btnGenerarArchivo.Visible = false;
                    }
                }

                if (Session["ApplicationIsPostBack"] == null)
                {
                    Session["ApplicationIsPostBack"] = true;
                }

                if ((bool)Session["ApplicationIsPostBack"])
                {
                    if (Session["ApplicationIntegrationOnOperaciones"] != null)
                    {
                        if (Request.QueryString["ref"] != "ZGlyZWN0bHk=")
                        {
                            TextBox6.Text = Session["ApplicationIntegrationOnOperacionesRut"].ToString();

                            Session["ApplicationIsPostBack"] = false;

                            #region "ejecucion directa de evento de buscar finiquito"

                            Button3_Click1(sender, e);

                            #endregion
                        }
                        else
                        {
                            Session["ApplicationIsPostBack"] = false;
                        }
                    }
                }

                #endregion
            }

        }

        private void CargacCausal()
        {
            if (!IsPostBack)
            {
                DataSet causales = svcFiniquitos.GetCausales().Table;
                foreach (DataRow rows in causales.Tables[0].Rows)
                {
                    CausalDespido.Items.Add(rows["UNIFICADO"].ToString());
                }
            }
        }

        private void CargaDocumentos()
        {
            if (!IsPostBack)
            {
                GridView3.DataSource = null;

                GridView3.DataSource = svcFiniquitos.GetListarDocumentos().Table;

                GridView3.DataBind();
            }
        }

        private void cargarUF(int month, int years)
        {
            //string mes = (DateTime.Now.Month).ToString();
            //string year = (DateTime.Now.Year).ToString();

            string mes = (month).ToString();
            string year = (years).ToString();

            string[] parametros = {
                mes,
                year
            };

            DataSet UFUltima = svcFiniquitos.GetUltimaUF(parametros).Table;

            float uff = 0;
            foreach(DataRow rows in UFUltima.Tables[0].Rows){
                uff = float.Parse(rows["VALORUF"].ToString());
            }

            Label14.Text = uff.ToString("N2");

            float x90 = float.Parse(Label14.Text) * 90;
            Label16.Text = x90.ToString("N1");

            //modalUFnoDisponible

            if (uff == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalUFnoDisponible').modal('show');", true);
            }
            

        }

        private void CargaOtrosHaberes()
        {
            Clases.OtrosHaberes OtrosHaberes = new Clases.OtrosHaberes();
            Descuento0.DataSource = null;
            Descuento0.DataSource = svcFiniquitos.GetOtrosHaberes().Table;
            Descuento0.DataBind();
        }

        private void CargaDescuentos()
        {
            Clases.Descuentos descuentos = new Clases.Descuentos();
            Descuento.DataSource = null;
            Descuento.DataSource = svcFiniquitos.GetListarDescuentos().Table;
            Descuento.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("CalculoBajaEst");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("CalculoBajaOut");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            Response.Redirect("CalculoBajaConsultora");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox6.Text != "")
                {
                    TextBox6.BackColor = Color.White;
                    bool haySolicitudesBaja = false;
                    MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                    web.RUTTRABAJADOR = TextBox6.Text;

                    foreach (DataRow rows in web.GetFiniquitosSolicitudesBajaService.Tables[0].Rows)
                    {
                        if(Convert.ToInt32(rows["VALIDACION"]) == 0){
                            haySolicitudesBaja = true;
                        }
                    }

                    if (haySolicitudesBaja)
                    {
                        lblHaySolicitudesBaja.Text = "Tiene N° " + web.GetFiniquitosSolicitudesBajaService.Tables[0].Rows.Count + " Solicitudes de baja para el Rut " + TextBox6.Text + ", si desea calcular alguna, solo debe seleccionarla.";
                        gvSolicitudBaja.DataSource = web.GetFiniquitosSolicitudesBajaService;
                        gvSolicitudBaja.DataBind();
                    }
                    else 
                    {
                        lblHaySolicitudesBaja.Text = "Tiene N° 0 Solicitudes de baja para el Rut " + TextBox6.Text + ", debe Agregar Nuevo Finiquito.";
                        gvSolicitudBaja.DataSource = null;
                        gvSolicitudBaja.DataBind();
                    }

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal();", true);
                    upModal.Update();

                }
                else
                {
                    TextBox6.BackColor = Color.FromArgb(235, 120, 120);
                }
            }
            catch (Exception ex)
            {
                Session["error"] = "Se provoco el siguiente error: " + ex.Message;
                Response.Redirect("CalculoBajaEst.aspx");
            }

        }

        protected void VerificarStatusSeguimiento_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            GridViewRow row = chkSelect.NamingContainer as GridViewRow;
            CheckBox chk = (CheckBox)GridView1.Rows[row.RowIndex].Cells[0].FindControl("estatus");

            if (chk.Checked)
            {
                if ((string)GridView1.Rows[row.RowIndex].Cells[10].Text.Replace("&nbsp;", "") != "")
                {
                    chk.Checked = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'Atención!', text:'No se puede incluir debido a que ya esta registrado'});", true);
                }
            }
                
        }
        
        protected void RecalcularTo125_Click(object sender, EventArgs e)
        {
            lblFactor.Text = "1.25";
            btnRecalculo125.Visible = false;
            btnRecalculo175.Visible = true;
            Session["ApplicationCalculoContrario"] = true;
            Session["ApplicationRecalculadoFactor"] = true;
            Session["ApplicationCalculoContrarioRealFactor"] = "1.25";
            lblCalculoContrario.InnerText = "0";
            BtnCalculaDobleFactor(sender, e);
            MaintainScrollPositionOnPostBack = true;
        }
        
        protected void RecalcularTo175_Click(object sender, EventArgs e)
        {
            lblFactor.Text = "1.75";
            btnRecalculo125.Visible = true;
            btnRecalculo175.Visible = false;
            Session["ApplicationCalculoContrario"] = true;
            Session["ApplicationRecalculadoFactor"] = true;
            Session["ApplicationCalculoContrarioRealFactor"] = "1.75";
            lblCalculoContrario.InnerText = "0";
            BtnCalculaDobleFactor(sender, e);
            MaintainScrollPositionOnPostBack = true;
        }

        protected void gvSolicitudesBaja_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["FolioSolicitudBaja"] = gvSolicitudBaja.SelectedRow.Cells[0].Text;
            Session["rut"] = TextBox6.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#myModal').modal('hide');", true);
            Response.Redirect("CalculoBajaEST.aspx");
        }    

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Session["rut"] = TextBox6.Text;
            string redir = string.Empty;
            if (Request.QueryString["ref"] != null)
            {
                if (Request.QueryString["callback"] != null)
                {
                    redir = Request.QueryString["ref"].ToString() + "&callback=" + Request.QueryString["callback"].ToString() + "&glcid=" + Request.QueryString["glcid"].ToString();
                }
                else
                {
                    redir = Request.QueryString["ref"].ToString();
                }
            }

            if (redir == "")
            {
                Response.Redirect("CalculoBajaEst.aspx");
            }
            else
            {
                Response.Redirect("CalculoBajaEst.aspx?ref=" + redir);
            }
        }

        protected void ChkEstatus_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('Ha cambiado el chequeado')</script>");
        }

        public void buscarcontratos()
        {
            try
            {
                Clases.Contrato contrato = new Clases.Contrato();
                contrato.rut = Session["rut"].ToString();
                contrato.validarPersonaExistente(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                if (contrato.Existe == "S") {
                    
                    contrato.ContratoActivobaja(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                    TextBox6.Text = contrato.rut;
                    TextBox2.Text = contrato.nombres;
                    TextBox3.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    //TextBox4.Text = contrato.ficha;
                    List<Clases.Contrato> con1 = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), TextBox6.Text);
                    ddlContratos.Items.Add("Seleccione");
                    foreach (Clases.Contrato contratos in con1)
                    {
                        ddlContratos.Items.Add(contratos.ficha);
                    }

                    List<Clases.Contrato> con = Clases.Contrato.ListarFiniquitados(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), TextBox6.Text);
                    GridView1.DataSource = con;
                    GridView1.DataBind();
                    
                    Session["ApplicationCalculoContrario"] = true;
                    Session["ApplicationCalculoContrarioRealFactor"] = "";
                    Session["ApplicationRecalculadoFactor"] = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'No se puede buscar', text:'El rut ingresado no existe o no pertenece a la empresa'});", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void haberselected_index(object sender, EventArgs e)
        {
            if (Descuento0.SelectedItem.ToString() == "Remuneracion Pendiente")
            {
                remupendAdc.Visible = true;
                LblRemunPendAdc.Visible = true;
            }
            else
            {
                remupendAdc.Visible = false;
                LblRemunPendAdc.Visible = false;
            }

            MaintainScrollPositionOnPostBack = true;
        }
        
        protected void selected_index(object sender, EventArgs e)
        {
            bool isPosibleCalcular = true;
            string[] paramValCal = new string[3];
            string[] valValCal = new string[3];
            DataSet dataVal;

            if (ddlContratos.SelectedItem.ToString() != "Seleccione")
            {
                paramValCal[0] = "@FICHA";
                paramValCal[1] = "@RUT";
                paramValCal[2] = "@IDEMPRESA";

                valValCal[0] = ddlContratos.SelectedItem.ToString();
                valValCal[1] = TextBox6.Text;
                valValCal[2] = "1";

                dataVal = svcFiniquitos.GetValidaRepeticionCalculo(paramValCal, valValCal).Table;

                foreach (DataRow rows in dataVal.Tables[0].Rows)
                {
                    switch (rows["Code"].ToString())
                    {
                        case "200":
                            isPosibleCalcular = false;
                            break;
                    }
                }
            }
            else
            {
                isPosibleCalcular = false;
            }
                

            if (isPosibleCalcular)
            {
                Clases.Contrato contrato = new Clases.Contrato();
                contrato.centrodecosto(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), ddlContratos.SelectedItem.ToString());
                lblcentrocosto.Text = contrato.centrocosto;

                MethodServiceSoftland softland = new MethodServiceSoftland();
                ServicioFiniquitos.ServicioFiniquitosClient servicioFiniquitos = new FiniquitosV2.ServicioFiniquitos.ServicioFiniquitosClient();
                softland.FICHA = ddlContratos.SelectedItem.ToString();

                labelJornadaDias.Text = "";
                labelJornadaDiasValor.Text = "";
                labelJornadaHoras.Text = "";
                labelJornadaHorasValor.Text = "";
                Label71.Text = "";
                Label71.Visible = false;

                DropDownList3.SelectedValue = "No";
                int validaPartime = 0;

                Clases.Cargo cargo = new Clases.Cargo();

                /** CLASE QUE TRAE EL CARGO ASOCIADO */
                cargo.ficha = ddlContratos.SelectedItem.ToString();
                cargo.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                TextBox5.Text = cargo.glosaCargo;
                TextBox7.Text = cargo.glosaMOd;

                /** CLASE QUE TRAE EL AREA DE NEGOCIO ASOCIADA */
                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                anegocio.ficha = ddlContratos.SelectedItem.ToString();
                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                TextBox8.Text = anegocio.areanegocio;
                TextBox9.Text = anegocio.codanrg;

                string[] parametros = {
                    "@AREANEG"
                };

                string[] valores = {
                    TextBox9.Text
                };

                DataSet dataExcpAreaNeg = svcFiniquitos.GetObtenerExcepcionPTAreaNeg(parametros, valores).Table;

                foreach (DataRow rowsExcpAreaNeg in dataExcpAreaNeg.Tables[0].Rows)
                {
                    if (rowsExcpAreaNeg["Code"].ToString() != "300")
                    {
                        if (softland.GetJornadasParttimeESTService.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow rowsVal in softland.GetTrabajadorPartTimeESTService.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(rowsVal["VALOR"]) == 1)
                                {
                                    foreach (DataRow rows in softland.GetJornadasParttimeESTService.Tables[0].Rows)
                                    {
                                        if (rows["CODIGOVARIABLE"].ToString() == "DIAS JORNADA")
                                        {
                                            labelJornadaDias.Text = rows["CODIGOVARIABLE"].ToString() + " : ";
                                            if (Convert.ToInt32(rows["VALOR"]) > 1)
                                            {
                                                labelJornadaDiasValor.Text = " " + rows["VALOR"].ToString();
                                                labelJornadaDiasGlosa.Text = " Dias semanal.";
                                            }
                                            else
                                            {
                                                labelJornadaDiasValor.Text = " " + rows["VALOR"].ToString();
                                                labelJornadaDiasGlosa.Text = " Dia semanal.";
                                            }
                                            validaPartime = validaPartime + 1;
                                        }
                                        else if (rows["CODIGOVARIABLE"].ToString() == "HORAS JORNADA")
                                        {
                                            labelJornadaHoras.Text = rows["CODIGOVARIABLE"].ToString() + " : ";
                                            if (float.Parse(rows["VALOR"].ToString()) > 1)
                                            {
                                                labelJornadaHorasValor.Text = " " + rows["VALOR"].ToString() + " Horas semanal.";
                                            }
                                            else
                                            {
                                                labelJornadaHorasValor.Text = " " + rows["VALOR"].ToString() + " Hora semanal.";
                                            }

                                            validaPartime = validaPartime + 1;
                                        }
                                        else if (rows["CODIGOVARIABLE"].ToString() == "SUELDO BASE")
                                        {
                                            labelSueldoBase.Text = rows["CODIGOVARIABLE"].ToString() + " : ";
                                            labelSueldoBaseValor.Text = " " + rows["VALOR"].ToString();

                                            validaPartime = validaPartime + 1;
                                        }

                                        if (validaPartime > 1)
                                        {
                                            DropDownList3.SelectedValue = "Si";
                                            labelJornadaDias.Visible = true;
                                            labelJornadaDiasValor.Visible = true;
                                            labelJornadaHoras.Visible = true;
                                            labelJornadaHorasValor.Visible = true;
                                            Label71.Visible = true;
                                            Label71.Text = "TRABAJADOR PART TIME";
                                        }
                                        else
                                        {
                                            Label71.Visible = false;
                                            Label71.Text = "";
                                            labelJornadaDias.Visible = false;
                                            labelJornadaDiasValor.Visible = false;
                                            labelJornadaHoras.Visible = false;
                                            labelJornadaHorasValor.Visible = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                string[] parametrosFactor =
                {
                    "@AREANEG"
                };

                string[] valoresFactor =
                {
                    anegocio.codanrg
                };

                DataSet dataCalculo = svcFiniquitos.GetFactorCalculo125(parametrosFactor, valoresFactor).Table;

                if (dataCalculo.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow rows in dataCalculo.Tables[0].Rows)
                    {
                        lblFactor.Text = rows["FACTOR"].ToString();
                    }
                }
                else
                {
                    lblFactor.Text = "1.75";
                }

                if (lblFactor.Text == "1.25")
                {
                    btnRecalculo125.Visible = false;
                    btnRecalculo175.Visible = true;
                }
                else
                {
                    btnRecalculo125.Visible = true;
                    btnRecalculo175.Visible = false;
                }

                /** INFORMATIVOS INCLUSION */

                string[] paramAlertas = new string[3];
                string[] valAlertas = new string[3];
                DataSet dataAlertas;

                paramAlertas[0] = "@FICHA";
                paramAlertas[1] = "@EMPRESA";
                paramAlertas[2] = "@CONCEPTO";

                valAlertas[0] = ddlContratos.SelectedValue;
                valAlertas[1] = "EST";
                valAlertas[2] = "CCAF";

                dataAlertas = svcFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        clausulaccaf.Visible = true;
                    }
                    else
                    {
                        clausulaccaf.Visible = false;
                    }
                }

                /** INFORMATIVO => LUCRO CESANTE PARA CAUSAL VENCIMIENTO DEL PLAZO (INGRESO POR OPERACIONES) */

                valAlertas[0] = ddlContratos.SelectedValue;
                valAlertas[1] = "TWEST";
                valAlertas[2] = "LUCRO CESANTE";

                dataAlertas = svcFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        lucrocesante.Visible = true;
                    }
                    else
                    {
                        lucrocesante.Visible = false;
                    }
                }

                MaintainScrollPositionOnPostBack = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'Atención!', text:'No podras realizar el calculo debido a que este finiquito por esta ficha ya esta previamente registrado.'});", true);
            }
        }

        protected void BtnCalculaDobleFactor(object sender, EventArgs e)
        {
            Thread.Sleep(1000);
            string factorContrario = string.Empty;
            Fin_Calculo = 0;
            Limpiarpagina();
            /** GridView2 = liquidaciones */
            GridView2.DataSource = null;
            DateTime fechaultimodia;
            fechaultimodia = DateTime.Parse("01-01-1001");
            if (CausalDespido.SelectedIndex > 0)
            {
                /** CARGA LOS HABERES QUE PREVIAMENTE FUERON CARGADOS, SIEMPRE Y CUANDO HAYAN REGISTROS */
                if(Session["datos2"] != null){
                    DataTable dt2 = Session["datos2"] as DataTable;
                    GridView5.DataSource = dt2;
                    GridView5.DataBind();
                    Session["datos2"] = dt2;

                    foreach (GridViewRow rowItem in GridView5.Rows)
                    {
                        TotalDescuento0.Text = Convert.ToString(Convert.ToInt32(TotalDescuento0.Text) + Convert.ToInt32(rowItem.Cells[2].Text));
                    }
                }

                /** CARGA LOS DESCUENTOS QUE PREVIAMENTE FUERON CARGADOS, SIEMPRE Y CUANDO HAYAN REGISTROS */
                if(Session["datos"] != null){
                    DataTable dt1 = Session["datos"] as DataTable;
                    GridView4.DataSource = dt1;
                    GridView4.DataBind();
                    Session["datos"] = dt1;

                    foreach (GridViewRow rowItem in GridView4.Rows)
                    {
                        TotalDescuento.Text = Convert.ToString(Convert.ToInt32(TotalDescuento.Text) + Convert.ToInt32(rowItem.Cells[2].Text));  
                    }
                }

                /** VALIDACION DE ZONA EXTREMA */
                if (DropDownList2.SelectedItem.ToString() == "Si")
                {
                        /** ZONA EXTREMA FACTOR */
                    lblFactor.Text = ((double)20 / (double)12).ToString("0.00");
                    btnRecalculo125.Visible = false;
                    btnRecalculo175.Visible = false;
                    Session["ApplicationCalculoContrario"] = false;
                }
                else
                {
                    //lblFactor.Text = ((double)15 / (double)12).ToString(); 
                    //lblFactor.Text = "1.75"; 

                    if (!(bool)Session["ApplicationRecalculadoFactor"])
                    {
                        if (lblFactor.Text.Contains("Label"))
                        {
                            string[] parametrosFactor =
                            {
                                "@AREANEG"
                            };

                            string[] valoresFactor =
                            {
                                TextBox9.Text
                            };

                            foreach (DataRow rows in svcFiniquitos.GetFactorCalculo125(parametrosFactor, valoresFactor).Table.Tables[0].Rows)
                            {
                                lblFactor.Text = rows["FACTOR"].ToString();
                            }
                        }
                    }
                    else
                    {
                        lblFactor.Text = Session["ApplicationCalculoContrarioRealFactor"].ToString();
                    }


                    if ((bool)Session["ApplicationCalculoContrario"])
                    {
                        if (lblFactor.Text == "1.25")
                        {
                            btnRecalculo175.Enabled = true;
                            btnRecalculo125.Enabled = false;

                            factorContrario = "1.75";
                            Session["ApplicationCalculoContrarioRealFactor"] = "1.25";
                        }
                        else
                        {
                            btnRecalculo175.Enabled = false;
                            btnRecalculo125.Enabled = true;

                            factorContrario = "1.25";
                            Session["ApplicationCalculoContrarioRealFactor"] = "1.75";
                        }
                    }
                }
                        

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede calcular', html: '<label>Debe seleccionar causal.</label>'});", true);
            }

            if ((bool)Session["ApplicationCalculoContrario"])
            {
                lblFactor.Text = factorContrario;
                lblCalculoContrario.InnerText = "0";
            }

            btnCalcular_Click(sender, e);
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                int DiasParavacaciones = 0, EXTC = 0, totaldiasfiniquitodiastrab = 0;
                double TotalDiasFinq = 0;
                string[] fichas = new string[GridView1.Rows.Count];
                periodosRem = new List<string>();
                ServicioFiniquitos.ServicioFiniquitosClient servicioFiniquitos = new FiniquitosV2.ServicioFiniquitos.ServicioFiniquitosClient();

                #region "ANTIGUO"

                if (TextBox3.Text != "")
                {
                    /** SET DE EMPRESA => TEXTBOX1 */
                    j = 2;
                    GridView2.DataSource = null;
                    dt = new DataTable();
                    int i = 0;
                    DiasTotal = 0;
                    string Finicio = DateTime.Now.ToString(), Ffinal = "01-01-1900";
                    int contador = 0;
                    int round = 0;
                    int sumaMonth = 0;
                    int sumaDias = 0;
                    int constSuma = 0;
                    double cantidadDias = 0;
                    double diasfebrero = 0;
                    double saldoVacaciones = 0;
                    double saldoDiasHabiles = 0;
                    string[] fechaTerminoUF = null;
                    string fichaInformativos = "";

                    foreach (GridViewRow rowItem in GridView1.Rows)
                    {
                        CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));

                        if (chk.Checked)
                        {
                            fichaInformativos += (fichaInformativos != "" ? "," : "") + " '" + rowItem.Cells[1].Text + "'";
                        }
                    }

                    string[] paramAlertas = new string[3];
                    string[] valAlertas = new string[3];
                    DataSet dataAlertas;

                    /** SOBRE GIRO */

                    paramAlertas[0] = "@FICHA";
                    paramAlertas[1] = "@EMPRESA";
                    paramAlertas[2] = "@CONCEPTO";

                    valAlertas[0] = fichaInformativos;
                    valAlertas[1] = "TWEST";
                    valAlertas[2] = "SOBREGIRO";

                    dataAlertas = servicioFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                    foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            sobregiro.Visible = true;
                            ccaf.Checked = true;
                        }
                        else
                        {
                            sobregiro.Visible = false;
                            ccaf.Checked = false;
                        }
                    }

                    paramAlertas[0] = "@FICHA";
                    paramAlertas[1] = "@EMPRESA";
                    paramAlertas[2] = "@CONCEPTO";

                    valAlertas[0] = fichaInformativos;
                    valAlertas[1] = "TWEST";
                    valAlertas[2] = "LICENCIA ACHS";

                    dataAlertas = servicioFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                    foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            licenciaACHS.Visible = true;
                        }
                        else
                        {
                            licenciaACHS.Visible = false;
                        }
                    }

                    valAlertas[0] = fichaInformativos;
                    valAlertas[1] = "TWEST";
                    valAlertas[2] = "COMENTARIO SF KAM";

                    dataAlertas = servicioFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                    foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            comentariosOper.Visible = true;
                        }
                        else
                        {
                            comentariosOper.Visible = false;
                        }
                    }

                    valAlertas[0] = fichaInformativos;
                    valAlertas[1] = "TWEST";
                    valAlertas[2] = "RETENCION JUDICIAL";

                    dataAlertas = servicioFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                    foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                    {
                        if (rows["Code"].ToString() == "200")
                        {
                            retencionjudicial.Visible = true;
                            lblRetencionJudicialinfo.Text = rows["Content"].ToString();
                            chk_restriccion_retencionjudicial.Checked = true;
                            chk_omitir_restriccion_retencionjudicial.Checked = false;
                        }
                        else
                        {
                            retencionjudicial.Visible = false;
                            lblRetencionJudicialinfo.Text = "";
                            chk_restriccion_retencionjudicial.Checked = false;
                            chk_omitir_restriccion_retencionjudicial.Checked = true;
                        }
                    }

                    foreach (GridViewRow rowItem in GridView1.Rows)
                    {
                        CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));

                        if (chk.Checked)
                        {
                            EXTC++;
                            /** MEJORAR */
                            string[] fechaInit = rowItem.Cells[2].Text.Split('-');

                            Finicio = fechaInit[1] + '-' + fechaInit[0] + '-' + fechaInit[2];

                            string[] fechaFin = rowItem.Cells[3].Text.Split('-');

                            Ffinal = fechaFin[1] + '-' + fechaFin[0] + '-' + fechaFin[2];

                            if (fechaTerminoUF == null)
                            {
                                fechaTerminoUF = rowItem.Cells[3].Text.Split('-');

                                cargarUF(Convert.ToInt32(fechaTerminoUF[1]), Convert.ToInt32(fechaTerminoUF[2]));


                            }

                            //TextBox3.Text = Ffinal.ToString();
                            //calculodiasvaca(Finicio, Ffinal);

                            if(contador == 0){
                                lblAContarDe.Text = DateTime.Parse(Ffinal).AddDays(1).ToString();
                                contador = contador + 1;
                            }
                            
                            #region "Calculo antiguo de vacaciones"
                            /*
                             * Obtencion de contratos seleccionados los cuales tendran que ser evaluados en sus fechas 
                             * Rango inicial y rango final => y determinacion de meses entre ambas fechas
                             */

                            //int __numeroDiaInicial = Convert.ToInt32(fechaInit[0]);
                            //int __numeroDiaFinal = Convert.ToInt32(fechaFin[0]);
                            //int __diaFinalFechaFinal = DateTime.Parse(fechaFin[1] + "-01-" + fechaFin[2]).AddMonths(1).AddDays(-1).Day;
                            //int __constDiasInicial = 0;
                            //int __constDiasFinal = 0;
                            //if ((DateTime.Parse(Finicio).Month == DateTime.Parse(Ffinal).Month && DateTime.Parse(Finicio).Year == DateTime.Parse(Ffinal).Year))
                            //{
                            //    TimeSpan span = (DateTime.Parse(Ffinal) - DateTime.Parse(Finicio));
                            //    __constDiasInicial = span.Days + 1 - ((__numeroDiaFinal >= 30) ? (__diaFinalFechaFinal - __numeroDiaFinal) : 0);
                            //    // 
                            //}
                            //else
                            //{
                            //    __constDiasInicial = (30 - __numeroDiaInicial) + 1;
                            //    __constDiasFinal = ((__diaFinalFechaFinal >= 30) ? 30 : 28) - (__diaFinalFechaFinal - __numeroDiaFinal);
                            //}

                            ////((__diaFinalFechaFinal >= 30) ? 30 : 0)30 - (__diaFinalFechaFinal - __numeroDiaFinal));

                            //// => inicio 01-MESSIGUIENTEALINICIO
                            //// => fin    FINMES-MESANTERIORALFINAL

                            //DateTime __dateIntermedioInicio = DateTime.Parse(fechaInit[1] + "-01-" + fechaInit[2]).AddMonths(1);
                            //DateTime __dateIntermedioFinal = DateTime.Parse(fechaFin[1] + "-01-" + fechaFin[2]).AddDays(-1);

                            //int __constMesesIntermedios = 0;
                            //int __nextMonth = 0;

                            //if (__dateIntermedioInicio < __dateIntermedioFinal)
                            //{
                            //    while (__dateIntermedioInicio.AddMonths(__nextMonth) <= __dateIntermedioFinal)
                            //    {
                            //        __constMesesIntermedios = __constMesesIntermedios + 1;
                            //        __nextMonth = __nextMonth + 1;
                            //    }
                            //}
                            #endregion

                            #region "Calculo de vacaciones nuevo"

                            float contadormeses = 0;
                            float contadordias = 0;
                            int roundmeses = 1;
                            int rounddias = 1;
                            float saldovacaciones = 0;

                            //while (DateTime.Parse(Finicio).AddMonths(roundmeses) <= DateTime.Parse(Ffinal))
                            //{
                            //    contadormeses = contadormeses + 1;
                            //    roundmeses = roundmeses + 1;
                            //}

                            //float vacacionesmeses = contadormeses * 30;
                            //float resto = 0;

                            //if (DateTime.Parse(fechaInit[1] + "-" + fechaInit[0] + "-" + fechaInit[2]).Day == 31 && DateTime.Parse(fechaFin[1] + "-" + fechaFin[0] + "-" + fechaFin[2]).Day == 31)
                            //{
                            //    resto = 0;
                            //}
                            //else
                            //{
                            //    if (DateTime.Parse(fechaInit[1] + "-" + fechaInit[0] + "-" + fechaInit[2]).Day == 31)
                            //    {
                            //        resto = -1;
                            //    }
                            //}


                            //if (DateTime.Parse(fechaFin[1] + "-01-" + fechaFin[2]).AddMonths(1).AddDays(-1).Day > 30 && DateTime.Parse(Ffinal).Day >= 30)
                            //{
                            //    if (DateTime.Parse(Ffinal).Day == 30)
                            //    {
                            //        resto = 0;
                            //    }

                            //    while (DateTime.Parse(Finicio).AddMonths(roundmeses - 1).AddDays(rounddias) <= DateTime.Parse(Ffinal))
                            //    {
                            //        contadordias = contadordias + 1;
                            //        rounddias = rounddias + 1;
                            //    }

                            //    contadordias = contadordias + resto;
                            //}
                            //else
                            //{
                            //    while (DateTime.Parse(Finicio).AddMonths(roundmeses - 1).AddDays(rounddias) <= DateTime.Parse(Ffinal))
                            //    {
                            //        contadordias = contadordias + ((DateTime.Parse(Finicio).AddMonths(roundmeses - 1).AddDays(rounddias).Day != 31) ? 1 : 0);
                            //        rounddias = rounddias + 1;
                            //        // si mes=febrero dia=28 +2
                            //    }

                            //}

                            ///** suma de 2 dias o 1  dia */
                            //if (DateTime.Parse(Ffinal).Month == 3)
                            //{
                            //    if (DateTime.Parse(Ffinal).Day != DateTime.Parse(Finicio).Day)
                            //    {
                            //        if (DateTime.Parse(Ffinal).Month == 3 && DateTime.Parse(Ffinal).Day < 29 && DateTime.Parse(DateTime.Parse(Ffinal).Year + "-03-01").AddDays(-1).Day == 28)
                            //        {
                            //            diasfebrero = 2;
                            //        }
                            //        else if (DateTime.Parse(Ffinal).Month == 3 && DateTime.Parse(Ffinal).Day == 29 && DateTime.Parse(DateTime.Parse(Ffinal).Year + "-03-01").AddDays(-1).Day == 29)
                            //        {
                            //            diasfebrero = 1;
                            //        }
                            //    }
                            //}




                            //saldovacaciones = vacacionesmeses + contadordias + diasfebrero;

                            #endregion

                            totaldiasfiniquitodiastrab = totaldiasfiniquitodiastrab + int.Parse(rowItem.Cells[4].Text);

                            //TotalDiasFinq = TotalDiasFinq + saldovacaciones + resto; 
                            //TotalDiasFinq + (__constDiasInicial + __constDiasFinal) + (__constMesesIntermedios * 30);
                            //diasfiniquitotemp.Text = Convert.ToString(TotalDiasFinq);
                            /** End evaluacion de contratos seleccionados */


                            while (DateTime.Parse(Finicio).AddMonths(sumaMonth) <= DateTime.Parse(Ffinal))
                            {
                                sumaMonth = sumaMonth + 1;
                            }

                            while (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias) <= DateTime.Parse(Ffinal))
                            {
                                if (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Day < 31)
                                {
                                    constSuma = 1;

                                    if (DateTime.Parse(Ffinal).Month == 3)
                                    {
                                        if (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 28 &&
                                            DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Month == 2 &&
                                            DateTime.Parse(DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Year + "-03-01").AddDays(-1).Day == 28)
                                        {
                                            diasfebrero = 2;
                                        }
                                        else if (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 29 &&
                                                 DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Month == 2 &&
                                                 DateTime.Parse(DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Year + "-03-01").AddDays(-1).Day == 29)
                                        {
                                            diasfebrero = 1;
                                        }
                                    }

                                }
                                else if (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 31)
                                {
                                    if (DateTime.Parse(Finicio).AddMonths(sumaMonth - 1).AddDays(sumaDias) == DateTime.Parse(Ffinal))
                                    {
                                        constSuma = 1;
                                    }
                                }

                                cantidadDias = cantidadDias + constSuma + diasfebrero;

                                diasfebrero = 0;
                                constSuma = 0;

                                sumaDias = sumaDias + 1;
                            }

                            fichas[i] = rowItem.Cells[1].Text;

                            double saldoMeses = (Convert.ToDouble(sumaMonth - 1) * double.Parse(lblFactor.Text));
                            double saldoDias = cantidadDias > 0 ? ((cantidadDias - 1) * (double.Parse(lblFactor.Text) / 30)) : 0;

                            saldoVacaciones = saldoVacaciones + saldoMeses + saldoDias;

                            UltimaLiquidacion(Properties.Settings.Default.connectionStringTWEST, fichas[i].ToString());

                            saldoDias = 0;
                            saldoMeses = 0;
                            sumaMonth = 0;
                            sumaDias = 0;
                            cantidadDias = 0;
                            sumaDias = 0;

                            int VALORDT = dt.Rows.Count;
                            DiasParavacaciones = int.Parse(rowItem.Cells[4].Text) + DiasParavacaciones;
                            i++;
                            round++;
                        }
                    }
                    if (EXTC == 0)
                    {
                        Response.Write("<script>alert('Debe Seleccionar contrato')</script>");
                        return;
                    }

                    //sumarSueldo();

                    cantidaddias.Text = totaldiasfiniquitodiastrab.ToString();
                    cantidaddias.Visible = true;

                    int cantidaddedias = int.Parse(cantidaddias.Text);
                    bool veriCostoCero = true;

                    if (excpcostocero.Checked)
                    {
                        veriCostoCero = true;
                    }
                    else
                    {
                        if (cantidaddedias > 31) {
                            veriCostoCero = true;
                        }
                        else
                        {
                            veriCostoCero = false;
                        }
                    }
                    
                    if (veriCostoCero)
                    {
                        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();
                        foreach (GridViewRow rowItem in GridView1.Rows)
                        {
                            CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                            if (chk.Checked)
                            {
                                string ficha = rowItem.Cells[1].Text;

                                string[] parametros = {
                                    "@FICHA"
                                };

                                string[] valores = {
                                    ficha
                                };

                                DataSet data = svcFiniquitos.GetValidaContratosEnCalculo(parametros, valores).Table;
                                Session["isPosibleGuardar"] = true;
                                Session["errorGeneradoGuardarCalculo"] = "";
                                foreach (DataRow rows in data.Tables[0].Rows)
                                {
                                    if (Convert.ToInt32(Convert.ToString(rows["VALIDACION"])) > 0)
                                    {
                                        Session["isPosibleGuardar"] = false;
                                        Session["errorGeneradoGuardarCalculo"] = Session["errorGeneradoGuardarCalculo"] + "<label>" + rows["RESULTADO"] + "</label><br/>";
                                    }

                                }
                            }
                        }

                        if ((bool)Session["isPosibleGuardar"])
                        {
                            //Vacaciones(Finicio, Ffinal);
                            if (dt.Rows.Count > 0)
                            {
                                CargaVarible(Properties.Settings.Default.connectionString);
                                InvierteDT();
                                GridView2.DataSource = dt;
                                //invierteGrid();
                                GridView2.DataBind();

                                saldoDiasHabiles = (saldoVacaciones / double.Parse(lblFactor.Text));

                                /** FORMATEO DE 90 DIAS PARA GRILLA DE PERIODOS => SI CUMPLE CONDICIONAL DE MAYOR A 90 DIAS */

                                lblDiasHabiles.Text = saldoDiasHabiles.ToString("N2");  //diasHabiles(TotalDiasFinq).ToString("N2");
                                lblDiasHabilesInt.Text = saldoDiasHabiles.ToString("N2"); //diasHabiles(TotalDiasFinq).ToString("N2");
                                lblFactorCifra.Text = saldoVacaciones.ToString("N2"); //factor(double.Parse(lblDiasHabilesInt.Text), double.Parse(lblFactor.Text)).ToString("N2");
                                lblFactorCifraInt.Text = saldoVacaciones.ToString("N2"); //factor(double.Parse(lblDiasHabilesInt.Text), double.Parse(lblFactor.Text)).ToString("N2");

                                lblFactorCifraIntAnt125.Text = factor(double.Parse(lblDiasHabilesInt.Text), double.Parse(factorAntiguo125.Text)).ToString();

                                Vacaciones(Finicio, Ffinal);
                            }
                        }
                        else 
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede calcular debido a:', html: '" + Session["errorGeneradoGuardarCalculo"] + " <br/> <label>Por favor revisar los contratos seleccionados antes señalados.</label>'});", true);
                        }

                        MaintainScrollPositionOnPostBack = true;
                    }
                    else
                    {
                        Label72.Visible = true;
                        Session["isPosibleGuardar"] = true;
                        Session["errorGeneradoGuardarCalculo"] = "";
                    }
                }

                if (TextBox2.Text.Contains("PT"))
                {
                    Label71.Visible = true;
                    Label71.Text = "TRABAJADOR PART TIME";
                }
                

                CalcularDescuento_Click(sender, e);
                MaintainScrollPositionOnPostBack = true;

                #endregion
            }
            catch(Exception ex)
            {
                Session["error"] = "Se provoco el siguiente error: " + ex.Message;
                Response.Redirect("CalculoBajaEst.aspx");
            }
        }

        private void CargaVarible(string connectionString)
        {
            int j = 1;
            strSql = string.Format("select * from FNVARIABLEREMUNERACION");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {

                            dt.Rows[j][0] = dr["descripcion"].ToString();
                            j++;

                        }
                    }
                }
            }
        }

        private void Vacaciones(string FechaInicio, string FechaFinal)
        {

            //lblAContarDe.Text = FechaFinal.AddDays(1).ToString("dd-MM-yyyy");
            int totalDiasVacaciones = Clases.Utilidades.TotaldiasVacaciones(DateTime.Parse(lblAContarDe.Text), float.Parse(lblFactorCifra.Text), TextBox1.Text);
            int totalDiasVacaciones125 = Clases.Utilidades.TotaldiasVacaciones(DateTime.Parse(lblAContarDe.Text), float.Parse(lblFactorCifraIntAnt125.Text), TextBox1.Text);

            string[] parametrosFactor =
            {
                "@AREANEG"
            };

            string[] valoresFactor =
            {
                TextBox9.Text
            };

            foreach (DataRow rows in svcFiniquitos.GetFactorCalculo125(parametrosFactor, valoresFactor).Table.Tables[0].Rows)
            {
                switch (lblFactor.Text)
                {
                    case "1.25":
                        Label33.Text = totalDiasVacaciones.ToString();
                        break;
                    case "1.75":
                        Label33.Text = "0";
                        break;
                    default:
                        if (rows["FACTOR"].ToString() == "1.25")
                        {
                            Label33.Text = totalDiasVacaciones.ToString();
                        }
                        else
                        {
                            Label33.Text = "0";
                        }
                        break;
                }
                
            }

            
        }

        private double diasHabiles(double cantidadDiasvar) {
            double calculo = 0;

            calculo = cantidadDiasvar / 30.0;
            //int calculo2 = Convert.ToInt32(((cantidadDiasvar) / 30.0)*1000);
            //calculo = calculo2 / 100;

            return calculo;
        }

        private double factor(double diasHabiles, double factorcifra) {
            double calculo = 0;

            calculo = diasHabiles * factorcifra;

            return calculo;
        }

        private void InvierteDT()
        {
            int recorrecolumna = 0;
            string[] valores = new string[dt.Columns.Count];
            int j = 0;

            int totalColumna = 0;

            totalColumna = dt.Columns.Count;
            for (int x = 1; x < totalColumna; x++)
            {
                dt.Columns[1].SetOrdinal(totalColumna - x);
            }
        }

        private void calculodiasvaca(DateTime FechaInicio, DateTime FechaFinal)
        {
            Clases.FechaContable fechas = new Clases.FechaContable();
            fechas.Fechainicio = FechaInicio;
            fechas.FechaFinal = FechaFinal;
            fechas.calculofecha1();
            if (DropDownList3.SelectedItem.Text == "No")
            {
                lblFactorCifra.Text = (float.Parse(lblFactorCifra.Text) + float.Parse(((fechas.meses * float.Parse(lblFactor.Text)) + (fechas.dias * (float.Parse(lblFactor.Text) / 30))).ToString("0.00"))).ToString();

            }
            //else
            //{
            //    int mes2 = int.Parse(Label28.Text) / 30;

            //    //Label32.Text = (float.Parse(Label32.Text) + float.Parse(((fechas.meses * float.Parse(Label40.Text)) + ((fechas.dias + 1) * (float.Parse(Label40.Text) / 30))).ToString("0.00"))).ToString();

            //    lblDiasHabiles.Text = (float.Parse(cantidaddias.Text) / 30).ToString();

            //    Label32.Text = (float.Parse(lblDiasHabiles.Text) * 1.25).ToString();

            //}
            //if (DateTime.Parse(Label38.Text) < FechaFinal)
            //{
            //    Label38.Text = FechaFinal.ToString();
            //}
        }

        private void UltimaLiquidacion(string connectionString, string ficha)
        {
            int Mes;
            FlagDias = 0;

            //strSql = string.Format("select top (5) ficha, mes from TWEST.softland.sw_variablepersona where ficha = '{0}' group by ficha, mes order by mes desc", ficha);
            strSql = string.Format("SELECT  softland.sw_variablepersona.mes, softland.sw_variablepersona.ficha, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                     " FROM softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                    " WHERE     (softland.sw_variablepersona.ficha = '{0}') GROUP BY softland.sw_variablepersona.ficha, softland.sw_variablepersona.mes, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                    " ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC", ficha);

            ds = Clases.Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTWEST, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            if (FlagDias == 0)
                            {
                                Mes = int.Parse(dr["mes"].ToString());
                                creadt(Properties.Settings.Default.connectionStringTWEST, ficha, Mes, j);
                                j++;
                            }
                        }
                    }
                }
            }
        }

        private void creadt(string connectionString, string ficha, int Mes, int j)
        {
            int fila = 1, DiasRestantes = 0, TotalColumna = 0, DIASMESCORTO = 0;
            bool periodoRepetido = false;
            bool periodoNoExiste = true;
            int dias = 0;
            
            Clases.Remuneracion rem = new Clases.Remuneracion();
            List<Clases.VariableRemuneracion> prueba = new List<Clases.VariableRemuneracion>();
            prueba = rem.Listar(Properties.Settings.Default.connectionStringTWEST, ficha, Mes);

            if ((prueba[0].codVariable.ToUpper() == "P002") && (prueba[0].valor != null))
            {
                if (periodosRem.Count > 0)
                {
                    for (var i = 0; i < periodosRem.Count; i++)
                    {
                        if (periodosRem[i] == prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString())
                        {
                            periodoNoExiste = false;
                        }
                    }

                    if (periodoNoExiste)
                    {
                        periodosRem.Add(prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString());
                    }
                }
                else
                {
                    periodosRem.Add(prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString());
                }
                    


                

                    //for (var i = 0; i < dt.Columns.Count; i++)
                    //{
                    //    if (dt.Rows[0][i].ToString() == prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString())
                    //    {
                    //        periodoRepetido = true;
                    //        j = j - 1;
                    //    }
                    //}

                    if (!periodoRepetido)
                    {
                        if (dt.Columns.Count < 1)
                        {
                            dt.Columns.Add("descripcion", Type.GetType("System.String"));
                            dt.Columns.Add("valor", Type.GetType("System.String"));
                            dt.Rows.Add("Haber", prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString());
                            //dt.Columns.Add(string.Format("valor{0}", j), Type.GetType("System.String"));
                            //dt.Rows.Add("");
                            //dt.Rows[0]["valor" + j] = prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString();
                            //fila = 1;
                        }
                        else
                        {
                            dt.Columns.Add(string.Format("valor{0}", j), Type.GetType("System.String"));
                            dt.Rows.Add("");
                            dt.Rows[0]["valor" + j] = prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString();
                            fila = 1;
                        }
                    }
                    

                    foreach (Clases.VariableRemuneracion nue in prueba)
                    {
                        double SueldoParcial = 0;
                        if (nue.valor != null)
                        {
                            SueldoParcial = float.Parse(nue.valor.Split('.')[0]);
                        }

                        if (nue.descripcion == "DIAS TRABAJADOS")
                        {
                            SueldoParcial = int.Parse(nue.valor);
                            DiasTotal = DiasTotal + int.Parse(nue.valor);
                        }
                        if ((FlagDias == 1) && (nue.valor != null) && (nue.descripcion != "DIAS TRABAJADOS"))
                        {
                            if (!periodoRepetido)
                            {
                                SueldoParcial = (int.Parse(nue.valor) / DIASMESCORTO) * DiasRestantes;
                            }
                            else
                            {
                                SueldoParcial = ((int.Parse(nue.valor) + Convert.ToInt32(dt.Rows[fila]["valor" + j])) / DIASMESCORTO) * DiasRestantes;
                            }
                            
                            //FlagDias = 0;
                        }

                        string descri = nue.descripcion;

                        if (descri == null)
                        { descri = " "; }
                        if (SueldoParcial == null)
                        { SueldoParcial = 0; }

                        if (descri != "DIAS TRABAJADOS")
                        {
                            TotalColumna = TotalColumna + int.Parse(SueldoParcial.ToString());
                        }
                        else
                        {
                            Label28.Text = (int.Parse(Label28.Text) + int.Parse(SueldoParcial.ToString())).ToString();

                            if ((int.Parse(Label28.Text) + int.Parse(SueldoParcial.ToString())) < 90)
                            {
                                Label28.Text = (int.Parse(Label28.Text) + int.Parse(SueldoParcial.ToString())).ToString();
                            }
                            else
                            {
                                Label28.Text = "90";
                            }
                            
                        }
                        if (dt.Columns.Count < 3)
                        {
                            if ((descri != null) || (SueldoParcial != null))
                            {
                                dt.Rows.Add(descri, SueldoParcial);
                            }
                        }
                        else
                        {
                            if (!periodoRepetido)
                            {
                                if (descri == "DIAS TRABAJADOS")
                                {
                                    if (SueldoParcial <= 30)
                                    {
                                        dt.Rows[fila]["valor" + j] = SueldoParcial;
                                    }
                                    else
                                    {
                                        dt.Rows[fila]["valor" + j] = 30;
                                    }
                                }
                                else
                                {
                                    dt.Rows[fila]["valor" + j] = SueldoParcial;
                                }
                                
                            }
                            else
                            {

                                if (descri == "DIAS TRABAJADOS")
                                {
                                    if ((SueldoParcial + Convert.ToInt32(dt.Rows[fila]["valor" + j])) <= 30)
                                    {
                                        dt.Rows[fila]["valor" + j] = SueldoParcial + Convert.ToInt32(dt.Rows[fila]["valor" + j]);
                                    }
                                    else
                                    {
                                        dt.Rows[fila]["valor" + j] = 30;
                                    }
                                }
                                else
                                {
                                    dt.Rows[fila]["valor" + j] = SueldoParcial + Convert.ToInt32(dt.Rows[fila]["valor" + j]);
                                }
                                
                            }
                            fila++;
                        }

                    }

                    if (dt.Columns.Count < 3)
                    {
                        dt.Rows.Add("Total Remuneración", TotalColumna.ToString("N0"));
                    }
                    else
                    {
                        dt.Rows[fila]["valor" + j] = TotalColumna.ToString("N0");
                        fila++;
                    }

                    Label36.Text = ("0").ToString();
                    
                
                
            }
                
        }

        private void Limpiarpagina()
        {
            
            Label28.Text = "0";
            lblPromedioDiario.Text = "0";
            Label35.Text = "0";
            lblDiasHabiles.Text = "0";
            lblFactorCifra.Text = "0";
            Label33.Text = "0";
            lblTotalDiasFeriados.Text = "0";
            Label35.Text = "0";
            Label36.Text = "0";
            lblAContarDe.Text = "01/01/1900";
            Label66.Text = "0";
            Label47.Text = "0";
            Label67.Text = "0";
            TotalDescuento.Text = "0";
            TotalDescuento0.Text = "0";
           
            GridView2.DataSource = null;
            GridView2.DataBind();
            GridView4.DataSource = null;
            GridView4.DataBind();
            GridView5.DataSource = null;
            GridView5.DataBind();


        }

        protected void OtroHeber_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Descuento0.SelectedItem.ToString() != "") && (TextBox12.Text != ""))
                {
                    DataTable dt2 = Session["datos2"] as DataTable;

                    int haber = int.Parse(TextBox12.Text);

                    if (!remupendAdc.Visible)
                    {
                        dt2.Rows.Add(Descuento0.SelectedItem.Text, TextBox12.Text);
                    }
                    else
                    {
                        dt2.Rows.Add(Descuento0.SelectedItem.Text + " " + remupendAdc.Value, TextBox12.Text);
                    }
                    
                    TotalDescuento0.Text = (int.Parse(TotalDescuento0.Text) + int.Parse(TextBox12.Text)).ToString();
                    lblCalculoContrario.InnerText = (int.Parse(lblCalculoContrario.InnerText.Replace(",", "")) + int.Parse(TextBox12.Text)).ToString("N0");
                    GridView5.DataSource = dt2;

                    GridView5.DataBind();
                    Session["datos2"] = dt2;

                    CalcularDescuento_Click(sender, e);
                    MaintainScrollPositionOnPostBack = true;
                }
            }
            catch(Exception ex)
            {
                Session["error"] = "Se provoco el siguiente error: " + ex.Message;
                Response.Redirect("CalculoBajaEst.aspx");
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Descuento.SelectedItem.ToString() != "") && (TextBox11.Text != ""))
                {
                    DataTable dt1 = Session["datos"] as DataTable;
                    
                    int descuento = int.Parse(TextBox11.Text);
                    dt1.Rows.Add(Descuento.SelectedItem.Text,descuento.ToString());

                    TotalDescuento.Text = (int.Parse(TotalDescuento.Text) + int.Parse(TextBox11.Text)).ToString();
                    lblCalculoContrario.InnerText = (int.Parse(lblCalculoContrario.InnerText.Replace(",", "")) - int.Parse(TextBox11.Text)).ToString("N0");
                    GridView4.DataSource = dt1;
                    GridView4.DataBind();
                    Session["datos"] = dt1;

                    CalcularDescuento_Click(sender, e);
                    MaintainScrollPositionOnPostBack = true;
                }
            }
            catch (Exception ex) 
            {
                Session["error"] = "Se provoco el siguiente error: " + ex.Message;
                Response.Redirect("CalculoBajaEst.aspx");
            }
        }

        protected void GridView5_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Valor");

            foreach (GridViewRow dr in GridView5.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Text, dr.Cells[2].Text);

            }
            int captura = int.Parse(GridView5.Rows[e.RowIndex].Cells[2].Text);
            int totalactual = int.Parse(TotalDescuento0.Text);
            TotalDescuento0.Text = (totalactual - captura).ToString();
            lblCalculoContrario.InnerText = (int.Parse(lblCalculoContrario.InnerText.Replace(",", "")) - captura).ToString("N0");
            //TotalDescuento0.Text = captura.ToString();
            dt.Rows[e.RowIndex].Delete();
            GridView5.DataSource = null;
            GridView5.DataSource = dt;
            GridView5.DataBind();
            Session["datos2"] = dt;
            
            CalcularDescuento_Click(sender, e);
            MaintainScrollPositionOnPostBack = true;
        }

        protected void GridView4_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Descuento");
            dt.Columns.Add("Valor");

            foreach (GridViewRow dr in GridView4.Rows)
            {
                dt.Rows.Add(dr.Cells[1].Text, dr.Cells[2].Text);

            }
            int captura = int.Parse(GridView4.Rows[e.RowIndex].Cells[2].Text);
            int totalactual = int.Parse(TotalDescuento.Text);
            TotalDescuento.Text = (totalactual - captura).ToString();
            lblCalculoContrario.InnerText = (int.Parse(lblCalculoContrario.InnerText.Replace(",", "")) + captura).ToString("N0");
            //TotalDescuento0.Text = captura.ToString();
            dt.Rows[e.RowIndex].Delete();
            GridView4.DataSource = null;
            GridView4.DataSource = dt;
            GridView4.DataBind();
            Session["datos"] = dt;

            CalcularDescuento_Click(sender, e);
            MaintainScrollPositionOnPostBack = true;

        }

        protected void CalcularDescuento_Click(object sender, EventArgs e)
        {
            try
            {
                Label36.Text = "0";
                int dias = 0;
                bool mayor90 = false;
                int sumatoriaPeriodoModif = 0;
                int positionTotalRem = 0;
                int remuneracionesModif = 0;
                int periodosEliminados = 0;
                int diasrealesModif = 0;
                int sueldorealesModif = 0;
                int positionDiasTrabajados = 0;
                int diasTrabajadosTotal = 0;
                bool veriCostoCero = true;

                if (excpcostocero.Checked)
                {
                    veriCostoCero = true;
                }
                else
                {
                    if (Convert.ToInt32(cantidaddias.Text) > 31)
                    {
                        veriCostoCero = true;
                    }
                    else
                    {
                        veriCostoCero = false;
                    }
                }

                if (veriCostoCero)
                {
                    if (GridView2.Rows.Count > 0)
                    {
                        for (var h = GridView2.Rows[0].Cells.Count - 1; h > 0; h--)
                        {
                            if (!mayor90)
                            {
                                if ((dias + Convert.ToInt32(GridView2.Rows[1].Cells[h].Text)) <= 90)
                                {
                                    dias = dias + Convert.ToInt32(GridView2.Rows[1].Cells[h].Text);

                                }
                                else
                                {
                                    mayor90 = true;
                                    diasrealesModif = Convert.ToInt32(GridView2.Rows[1].Cells[h].Text);
                                    sueldorealesModif = Convert.ToInt32(GridView2.Rows[2].Cells[h].Text);

                                    GridView2.Rows[1].Cells[h].Text = (90 - dias).ToString();

                                    GridView2.Rows[2].Cells[h].Text = ((sueldorealesModif / diasrealesModif) * (90 - dias)).ToString();
                                    dias = dias + (90 - dias);

                                    for (var z = 3; z < GridView2.Rows.Count; z++)
                                    {
                                        if (GridView2.Rows[z].Cells[0].Text != "&nbsp;")
                                        {
                                            if (GridView2.Rows[z].Cells[0].Text != "Total Remuneraci&#243;n")
                                            {
                                                GridView2.Rows[z].Cells[h].Text = ((Convert.ToInt32(GridView2.Rows[z].Cells[h].Text) / diasrealesModif) * Convert.ToInt32(GridView2.Rows[1].Cells[h].Text)).ToString();
                                            }
                                        }
                                    }

                                    for (var z = 1; z < GridView2.Rows.Count; z++)
                                    {
                                        if (GridView2.Rows[z].Cells[0].Text != "&nbsp;")
                                        {
                                            if (GridView2.Rows[z].Cells[0].Text != "Total Remuneraci&#243;n" && GridView2.Rows[z].Cells[0].Text != "DIAS TRABAJADOS")
                                            {
                                                sumatoriaPeriodoModif = sumatoriaPeriodoModif + Convert.ToInt32(GridView2.Rows[z].Cells[h].Text);
                                            }
                                            else
                                            {
                                                if (GridView2.Rows[z].Cells[0].Text == "Total Remuneraci&#243;n")
                                                {
                                                    positionTotalRem = z;
                                                }
                                                else
                                                {
                                                    positionDiasTrabajados = z;
                                                }

                                            }
                                        }
                                    }

                                    GridView2.Rows[positionTotalRem].Cells[h].Text = (sumatoriaPeriodoModif).ToString("N0");

                                }

                            }
                            else
                            {
                                for (var x = 0; x < GridView2.Rows.Count; x++)
                                {
                                    GridView2.Rows[x].Cells[h].Visible = false;
                                }
                                periodosEliminados = periodosEliminados + 1;
                                dias = 90;
                            }

                        }
                    }
                    

                    if(positionTotalRem == 0)
                    {
                        if (GridView2.Rows.Count > 0)
                        {
                            for (var h = GridView2.Rows[0].Cells.Count - 1; h > 0; h--)
                            {
                                for (var z = 1; z < GridView2.Rows.Count; z++)
                                {
                                    if (GridView2.Rows[z].Cells[0].Text != "&nbsp;")
                                    {
                                        if (GridView2.Rows[z].Cells[0].Text != "Total Remuneraci&#243;n" && GridView2.Rows[z].Cells[0].Text != "DIAS TRABAJADOS")
                                        {
                                            sumatoriaPeriodoModif = sumatoriaPeriodoModif + Convert.ToInt32(GridView2.Rows[z].Cells[h].Text);
                                        }
                                        else
                                        {
                                            if (GridView2.Rows[z].Cells[0].Text == "Total Remuneraci&#243;n")
                                            {
                                                positionTotalRem = z;
                                            }
                                            else
                                            {
                                                positionDiasTrabajados = z;
                                            }

                                        }
                                    }
                                }
                            }
                        }
                        
                    }

                    if (positionTotalRem > 0)
                    {
                        for (var y = GridView2.Rows[0].Cells.Count - 1; y > (periodosEliminados); y--)
                        {
                            remuneracionesModif = remuneracionesModif + Convert.ToInt32(GridView2.Rows[positionTotalRem].Cells[y].Text.Replace(",", ""));
                            diasTrabajadosTotal = diasTrabajadosTotal + Convert.ToInt32(GridView2.Rows[positionDiasTrabajados].Cells[y].Text);
                        }
                    }
                    

                    periodosANoGuardar.Text = periodosEliminados.ToString();
                    Label36.Text = remuneracionesModif.ToString();

                    Label28.Text = dias.ToString();
                }
                
                //Label33.Text = "0";
                if (float.Parse(Label36.Text) > 0 && float.Parse(Label28.Text) > 0)
                {
                    lblPromedioDiario.Text = Math.Round((float.Parse(Label36.Text) / float.Parse(Label28.Text))).ToString();
                }
                else
                {
                    lblPromedioDiario.Text = "0";
                }
                lblTotalDiasFeriados.Text = (double.Parse(lblFactorCifraInt.Text) + double.Parse(Label33.Text)).ToString("N2");
                lblTotalDiasFeriadosInt.Text = (double.Parse(lblFactorCifraInt.Text) + double.Parse(Label33.Text)).ToString();
                Label35.Text = (double.Parse(lblPromedioDiario.Text) * double.Parse(lblTotalDiasFeriadosInt.Text)).ToString("N0");
                


                /** CALCULO A 1.25 */
                //totalfiniquitoAnt125.Text = ((Convert.ToInt32(float.Parse(proporcionalVacacionesAnt125.Text)) + int.Parse(TotalDescuento0.Text)) - int.Parse(TotalDescuento.Text)).ToString("N0");

                /** CALCULO A 1.75 */
                Label47.Text = ((Convert.ToInt32(float.Parse(Label35.Text)) + int.Parse(TotalDescuento0.Text)) - int.Parse(TotalDescuento.Text)).ToString("N0");
                //if ((bool)Session["ApplicationCalculoContrario"])
                //{
                    
                //}
                //
                int descuento0 = int.Parse(TotalDescuento0.Text);
                int descuento = int.Parse(TotalDescuento.Text);
                Label66.Text = descuento0.ToString("N0");
                Label67.Text = descuento.ToString("N0");

                if (labelJornadaDiasValor.Text != "" && labelJornadaDias.Text != "" && labelJornadaHoras.Text != "" && labelJornadaHorasValor.Text != "")
                {
                    /** VALIDACION REMUNERACION FIJA O MIXTA */
                    int bonos = 0;
                    List<String> ultimosPeriodos = new List<string>();
                    int contadorUltPer = 1;
                    string valorPeriodo = string.Empty;
                    bool existe = false;
                    List<int> bonosAsociados = new List<int>();
                    int remMixta = 0;
                    int diasTrabajados = 0;

                    /** CAPTURA DE ULTIMOS 3 MESES */

                    if (GridView2.Rows.Count > 0 && GridView2.Rows[0].Cells.Count > 1) {
                        for (var i = GridView2.Rows[0].Cells.Count - 1; i >= 0; i--)
                        {
                            if (i > 0)
                            {
                                if (ultimosPeriodos.Count > 0)
                                {
                                    existe = false;
                                    for (var j = 0; j < ultimosPeriodos.Count; j++)
                                    {
                                        if (contadorUltPer <= 3)
                                        {
                                            if (ultimosPeriodos[j] == GridView2.Rows[0].Cells[i].Text)
                                            {
                                                valorPeriodo = GridView2.Rows[0].Cells[i].Text;
                                                existe = true;
                                            }
                                        }
                                    }

                                    if (contadorUltPer <= 3)
                                    {
                                        if (existe)
                                        {
                                            ultimosPeriodos.Add(GridView2.Rows[0].Cells[i].Text);

                                        }
                                        else
                                        {
                                            ultimosPeriodos.Add(GridView2.Rows[0].Cells[i].Text);
                                            contadorUltPer = contadorUltPer + 1;
                                        }
                                    }

                                }
                                else
                                {
                                    ultimosPeriodos.Add(GridView2.Rows[0].Cells[i].Text);
                                    contadorUltPer = contadorUltPer + 1;
                                }
                            }
                        }
                    }

                    /** OBTENCION DE FILAS CON BONOS */
                    for (var i = 0; i < GridView2.Rows.Count; i++)
                    {
                        if (GridView2.Rows[i].Cells[0].Text.Contains("BONO") || GridView2.Rows[i].Cells[0].Text == "RECARGO 30% DIAS DOMINGO" || GridView2.Rows[i].Cells[0].Text == "SEMANA CORRIDA")
                        {
                            if (TextBox9.Text != "ABC" && TextBox9.Text != "COF" && TextBox9.Text != "DAN" && TextBox9.Text != "DCD" && TextBox9.Text != "DIJ" && TextBox9.Text != "DIN" &&
                                TextBox9.Text != "AST" && TextBox9.Text != "SEC")
                            {
                                if (!GridView2.Rows[i].Cells[0].Text.Contains("BONO INCENTIVO"))
                                {
                                    bonosAsociados.Add(i);
                                }
                            }
                            else 
                            {
                                bonosAsociados.Add(i);
                            }
                        }
                    }

                    /**  */
                    if (ultimosPeriodos.Count > 1)
                    {
                        for (var i = GridView2.Rows[0].Cells.Count - 1; i >= GridView2.Rows[0].Cells.Count - ultimosPeriodos.Count; i--)
                        {
                            for (var j = 0; j < bonosAsociados.Count; j++)
                            {
                                if (Convert.ToInt32(GridView2.Rows[bonosAsociados[j]].Cells[i].Text) > 0)
                                {
                                    remMixta = remMixta + Convert.ToInt32(GridView2.Rows[bonosAsociados[j]].Cells[i].Text);
                                }
                            }
                        }
                    }
                    else if(ultimosPeriodos.Count == 1)
                    {
                        for (var i = GridView2.Rows[0].Cells.Count - 1; i >= ultimosPeriodos.Count; i--)
                        {
                            for (var j = 0; j < bonosAsociados.Count; j++)
                            {
                                if (Convert.ToInt32(GridView2.Rows[bonosAsociados[j]].Cells[i].Text) > 0)
                                {
                                    remMixta = remMixta + Convert.ToInt32(GridView2.Rows[bonosAsociados[j]].Cells[i].Text);
                                }
                            }
                        }
                    }

                    if (remMixta > 0)
                    {
                        for (var i = GridView2.Rows[0].Cells.Count - 1; i >= GridView2.Rows[0].Cells.Count - ultimosPeriodos.Count; i--)
                        {
                            diasTrabajados = diasTrabajados + Convert.ToInt32(GridView2.Rows[1].Cells[i].Text);
                        }

                        if (ultimosPeriodos.Count >= 3)
                        {
                            bonos = remMixta / diasTrabajados;
                        } 
                        else 
                        {
                            bonos = remMixta / diasTrabajados;
                        }
                    }
                    else
                    {
                        bonos = remMixta;
                    }

                    /** FORMULA ESTANDAR PARA CALCULO REMUNERACION FIJA */

                    MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                    web.CARGOMOD = TextBox7.Text;
                    string[] fecha = TextBox3.Text.Split('/');
                    web.FECHATERMINO = fecha[2] + "-" + fecha[1] + "-" + fecha[0];
                    int valorDiario = 0;

                    //foreach (DataRow rows in web.GetCalculoPartimeEspecialService.Tables[0].Rows)
                    //{
                    //    if (rows["VALIDACION"].ToString() == "0")
                    //    {
                    //        valorDiario = (Convert.ToInt32(labelSueldoBaseValor.Text) / Convert.ToInt32(rows["RESULTADO"])) + bonos;
                    //    }
                    //}

                    valorDiario = (Convert.ToInt32(labelSueldoBaseValor.Text) / 30) + bonos;
                    labelValorDiario.Text = "Valor Diario : ";
                    labelValorDiarioValor.Text = (valorDiario).ToString();
                    int valorSemanal = valorDiario * Convert.ToInt32(labelJornadaDiasValor.Text);
                    labelValorSemana.Text = "Valor Semana : ";
                    labelValorSemanaValor.Text = valorSemanal.ToString();
                    float valorSemanaVacaciones = valorSemanal * 3;
                    float valor1DiaVacaciones = valorSemanaVacaciones / 21;
                    float finiquito = valor1DiaVacaciones * float.Parse(lblTotalDiasFeriadosInt.Text);

                    Label47.Text = (finiquito + Convert.ToInt32(TotalDescuento0.Text) - Convert.ToInt32(TotalDescuento.Text)).ToString("N0");
                    Label35.Text = finiquito.ToString("N0");
                    lblPromedioDiario.Text = valor1DiaVacaciones.ToString("N0");
                }

                if ((bool)Session["ApplicationCalculoContrario"])
                {
                    Session["ApplicationCalculoContrario"] = false;
                    lblCalculoContrario.InnerText = Label47.Text;
                    lblFactorContrario.InnerText = lblFactor.Text;
                    lblFactor.Text = "Label";
                    lblCalculoContrario.InnerText = ((Convert.ToInt32(float.Parse(Label35.Text.Replace(",", ""))) + int.Parse(TotalDescuento0.Text)) - int.Parse(TotalDescuento.Text)).ToString("N0");
                    BtnCalculaDobleFactor(sender, e);
                }

                if(lblFactor.Text == "1.67")
                {
                    contentFactorContrario.Visible = false;

                }

                if (lblFactorContrario.InnerText == lblFactor.Text)
                {
                    switch (lblFactor.Text)
                    {
                        case "1.25":
                            btnRecalculo175.Visible = true;
                            btnRecalculo125.Visible = false;
                            break;
                        case "1.75":
                            btnRecalculo175.Visible = false;
                            btnRecalculo125.Visible = true;
                            break;
                    }
                }

                MaintainScrollPositionOnPostBack = true;
            }
            catch(Exception ex)
            {
                Session["error"] = "Se provoco el siguiente error: " + ex.Message;
                Response.Redirect("CalculoBajaEst.aspx");
            }
        }

        protected void BTNFiniquito_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "genPDF();", true);
        }

        protected void Limpiar_Click(object sender, EventArgs e)
        {

        }

        private int validarTrabajadorFiniquito()
        {
            Clases.FN_ENCDESVINCULACION encdesvin = new Clases.FN_ENCDESVINCULACION();
            encdesvin.FICHATRABAJADOR = ddlContratos.SelectedItem.ToString();

            if ((encdesvin.ObtenerENCFiniquito(Properties.Settings.Default.connectionString) == 1) && (Label10.Text != "symunoz@team-work.cl"))
            {
                Label20.Text = "Folio: " + encdesvin.ID.ToString();
                Response.Write("<script>alert('Ya existe Calculo de Finiquito para Ficha Vigente')</script>");
                //Grabar.Enabled = false;
                return 1;
            }
            else
            { return 0; }

        }

        private void GrabaDocumentos(int IDDesvinculacion)
        {
            try
            {
                Clases.ControldeDocumento controldoc = new Clases.ControldeDocumento();
                foreach (GridViewRow rowItem in GridView3.Rows)
                {
                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus0"));
                    if (chk.Checked)
                    {
                        controldoc.ObtenerIdDocumentos(Properties.Settings.Default.connectionString);
                        controldoc.descripcion = rowItem.Cells[0].Text;
                        controldoc.estado = 2;
                        controldoc.IDDesvinculacion = IDDesvinculacion;
                        controldoc.Ficha = "";
                        controldoc.fechaingreso = DateTime.Today.ToString();
                        controldoc.usuario = Label10.Text;
                        controldoc.Grabar(Properties.Settings.Default.connectionString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrabaVariable(int IDDesvinculacion)
        {
            try
            {
                Clases.FNDatosVariable dtvariable = new Clases.FNDatosVariable();

                for (var i = GridView2.Rows[0].Cells.Count - 1; i > Convert.ToInt32(periodosANoGuardar.Text); i--)
                {
                    for (var j = 1; j < GridView2.Rows.Count; j++)
                    {
                        if (GridView2.Rows[j].Cells[0].Text != "&nbsp;")
                        {
                            dtvariable.IDDesvinculacion = IDDesvinculacion;
                            dtvariable.ObtenerId(Properties.Settings.Default.connectionString);
                            dtvariable.ID = dtvariable.ID;
                            dtvariable.Fecha = DateTime.Today.ToString();
                            dtvariable.Mesano = GridView2.Rows[0].Cells[i].Text;
                            dtvariable.Monto = Convert.ToInt32(GridView2.Rows[j].Cells[i].Text.Replace(",", ""));
                            dtvariable.VarDescripcion = GridView2.Rows[j].Cells[0].Text;
                            dtvariable.Grabar(Properties.Settings.Default.connectionString);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void GrabaOtrosHaberes(int IDDesvinculacion)
        {
            try
            {
                if (GridView5.Rows.Count >= 0)
                {
                    foreach (GridViewRow row in GridView5.Rows)
                    {
                        Clases.OtrosHaberesFiniquitos otroshaber = new Clases.OtrosHaberesFiniquitos();
                        otroshaber.ObtenerId(Properties.Settings.Default.connectionString);
                        otroshaber.IDDesvinculacion = IDDesvinculacion;
                        otroshaber.Descripcion = HttpUtility.HtmlDecode(row.Cells[1].Text);
                        otroshaber.Monto = int.Parse(row.Cells[2].Text);
                        otroshaber.Grabar(Properties.Settings.Default.connectionString);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrabaDescuentoFiniquito(int IDDesvinculacion)
        {
            try
            {
                if (GridView4.Rows.Count >= 0)
                {
                    foreach (GridViewRow row in GridView4.Rows)
                    {
                        Clases.FNDescuentoFiniquito fndescuentofiniquito = new Clases.FNDescuentoFiniquito();
                        fndescuentofiniquito.ObtenerId(Properties.Settings.Default.connectionString);
                        fndescuentofiniquito.IDDesvinculacion = IDDesvinculacion;
                        fndescuentofiniquito.Descripcion = row.Cells[1].Text;
                        fndescuentofiniquito.Monto = int.Parse(row.Cells[2].Text);
                        fndescuentofiniquito.Grabar(Properties.Settings.Default.connectionString);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GrabaDiasVacaciones(int IDDesvinculacion)
        {
            try
            {
                Clases.FNDiasVacacion diasvacacion = new Clases.FNDiasVacacion();
                diasvacacion.ObtenerId(Properties.Settings.Default.connectionString);
                diasvacacion.IDDesvinculacion = IDDesvinculacion;
                diasvacacion.TotalDiasFiniquito = int.Parse(Label28.Text);
                diasvacacion.DiasHabiles = lblDiasHabiles.Text;
                diasvacacion.DiasVacTomadas = int.Parse(Label48.Text);
                diasvacacion.Saldodiashabiles = Label49.Text;
                diasvacacion.DiasVacacionesFactor = lblFactorCifra.Text;
                diasvacacion.FechaDesde = lblAContarDe.Text.Split(' ')[0];
                diasvacacion.DiasInhabiles = int.Parse(Label33.Text);
                diasvacacion.TotalDiasFeriado = lblTotalDiasFeriados.Text;
                diasvacacion.factorCalculo = lblFactor.Text;
                diasvacacion.Grabar(Properties.Settings.Default.connectionString);
            }
            catch (Exception ex)
            {

            }
            
        }

        private void GrabaTotalHaberes(int IDDesvinculacion)
        {
            try
            {
                Clases.FNTotalHaber totalhaber = new Clases.FNTotalHaber();
                totalhaber.ObtenerId(Properties.Settings.Default.connectionString);
                totalhaber.IDDesvinculacion = IDDesvinculacion;
                totalhaber.FeriadoProporcional = Convert.ToInt32(double.Parse(Label35.Text));
                totalhaber.OtrosHaberes = Convert.ToInt32(double.Parse(Label66.Text));
                totalhaber.TotalDescuento = Convert.ToInt32(double.Parse(Label67.Text));
                totalhaber.TotalHaberes = Convert.ToInt32(double.Parse(Label47.Text));
                totalhaber.Grabar(Properties.Settings.Default.connectionString);
            }
            catch (Exception ex)
            {

            }
            
        }

        protected void GrabarByRetencionJudicial(object sender, EventArgs e)
        {
            chk_omitir_restriccion_retencionjudicial.Checked = true;
            Grabar(sender, e);
        }

        protected void Grabar(object sender, EventArgs e)
        {
            try
            {
                if (ddlContratos.SelectedIndex != -1)
                {
                    if (ddlContratos.SelectedItem.ToString() != "Seleccione")
                    {
                        string[] paramFactorCont = new string[3];
                        string[] valFactorCont = new string[3];

                        if ((bool)Session["isPosibleGuardar"])
                        {
                            btnGrabar.Enabled = false;

                            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                            //ServicioTeamwork.ServicioAccessESTClient svcTeamwork = new ServicioTeamwork.ServicioAccessESTClient();
                            web.FECHAHORASOLICITUD = DateTime.Now.ToString();
                            string AHORA = DateTime.Now.ToString();
                            string[] fechavisoDesv = TextBox3.Text.Split('/');

                            if (Label20.Text != "Sin Folio")
                            {
                                web.FECHAAVISODESVINCULACION = TextBox3.Text;
                            }
                            else
                            {
                                web.FECHAAVISODESVINCULACION = fechavisoDesv[0] + "-" + fechavisoDesv[1] + "-" + fechavisoDesv[2];
                            }

                            web.IDRENUNCIA = "0";
                            web.USUARIO = Session["Usuario"].ToString();
                            web.RUTTRABAJADOR = TextBox6.Text;
                            web.FICHA = ddlContratos.SelectedItem.ToString();
                            web.NOMBRETRABAJADOR = TextBox2.Text;
                            web.ESTADOSOLICITUD = "2";
                            web.FECHAESTADO = AHORA;
                            web.IDCAUSAL = CausalDespido.SelectedItem.ToString();
                            web.IDEMPRESA = "1";
                            web.OBSERVACION = "";
                            web.CARGOMOD = TextBox7.Text;
                            web.AREANEGOCIO = TextBox9.Text;
                            web.CLIENTE = TextBox8.Text;
                            web.ESPARTTIME = DropDownList3.SelectedItem.ToString();
                            web.ESZONAEXT = DropDownList2.SelectedItem.ToString();

                            if (ccaf.Checked)
                            {
                                web.CCAF = "S";
                            }
                            else
                            {
                                web.CCAF = "N";
                            }

                            if (Label20.Text != "Sin Folio")
                            {
                                web.IDDESVINCULACION = Label20.Text.Replace("Folio: ", "");
                            }
                            else
                            {
                                web.IDDESVINCULACION = "0";
                            }

                            web.SOLICITUD = "N";

                            if (Request.QueryString["ref"] != null)
                            {
                                web.CODIGOSOLICITUD = Request.QueryString["ref"].ToString();
                            }
                            else
                            {
                                web.CODIGOSOLICITUD = "";
                            }
                            web.FECHACARTABAJA = "";
                            web.EXCEPCIONCOSTOCERO = excpcostocero.Checked ? "S" : "N";
                            web.TOTALFINIQUITOS = Label47.Text;
                            web.NIVEL = "-";
                            web.CATEGORIA = "";

                            int encDesvinculacion = 0;
                            string errorDuplicidad = string.Empty;
                            string applicationIntegrateOperacionesComeBack = string.Empty;

                            if (!chk_restriccion_retencionjudicial.Checked && chk_omitir_restriccion_retencionjudicial.Checked)
                            {
                                foreach (DataRow rows in web.SetCrearModificarEncDesvinculacionService.Tables[0].Rows)
                                {
                                    if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                                    {
                                        encDesvinculacion = Convert.ToInt32(rows["RESULTADO"].ToString().Replace("Folio: N° ", ""));
                                        applicationIntegrateOperacionesComeBack = rows["Redirect"].ToString();
                                    }
                                    else
                                    {
                                        encDesvinculacion = -1;
                                        errorDuplicidad = rows["RESULTADO"].ToString();
                                    }
                                }

                                if (encDesvinculacion > 0)
                                {
                                    Clases.FNContrato contrato = new Clases.FNContrato();
                                    foreach (GridViewRow rowItem in GridView1.Rows)
                                    {
                                        CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                        if (chk.Checked)
                                        {
                                            contrato.ObtenerIdFNContrato(Properties.Settings.Default.connectionString);

                                            int iddesvinculacion = encDesvinculacion;
                                            contrato.IDDesvinculacion = iddesvinculacion;
                                            contrato.Ficha = rowItem.Cells[1].Text;

                                            string[] fechaInicio = rowItem.Cells[2].Text.Split('-');

                                            contrato.FechaInicio = fechaInicio[2] + "-" + fechaInicio[0] + "-" + fechaInicio[1];

                                            string[] fechaFinal = rowItem.Cells[3].Text.Split('-');

                                            contrato.FechaFinal = fechaFinal[2] + "-" + fechaFinal[0] + "-" + fechaFinal[1];

                                            contrato.Dias = int.Parse(rowItem.Cells[4].Text);
                                            contrato.Causal = rowItem.Cells[5].Text;
                                            contrato.Estado = 3;
                                            contrato.Grabar(Properties.Settings.Default.connectionString);

                                        }
                                    }
                                    GrabaDocumentos(encDesvinculacion);
                                    if (GridView2.Rows.Count > 0)
                                    {
                                        GrabaVariable(encDesvinculacion);
                                    }
                                    if (GridView5.Rows.Count > 0)
                                    {
                                        GrabaOtrosHaberes(encDesvinculacion);
                                    }
                                    if (GridView4.Rows.Count > 0)
                                    {
                                        GrabaDescuentoFiniquito(encDesvinculacion);
                                    }
                                    GrabaDiasVacaciones(encDesvinculacion);
                                    GrabaTotalHaberes(encDesvinculacion);

                                    if (lblFactor.Text != "1.67")
                                    {
                                        paramFactorCont[0] = "@IDDESVINCULACION";
                                        paramFactorCont[1] = "@FACTOR";
                                        paramFactorCont[2] = "@MONTO";

                                        valFactorCont[0] = encDesvinculacion.ToString();
                                        valFactorCont[1] = lblFactorContrario.InnerText;
                                        valFactorCont[2] = lblCalculoContrario.InnerText.Replace(",", "");

                                        svcFiniquitos.SetAgregarFactorContrario(paramFactorCont, valFactorCont);
                                    }
                                        
                                    Label20.Text = "Folio: " + encDesvinculacion.ToString();

                                    btnGrabar.Enabled = true;

                                    /** Creación de documentos asociados */

                                    if (Request.QueryString["glcid"] != null)
                                    {
                                        ResponseApi response = new ResponseApi();
                                        response.CreatePdfDocument(
                                            encDesvinculacion.ToString(),
                                            "caratula",
                                            Request.QueryString["glcid"].ToString()
                                        );

                                        response.CreatePdfDocument(
                                            encDesvinculacion.ToString(),
                                            "documento",
                                            Request.QueryString["glcid"].ToString()
                                        );
                                    }

                                    if (applicationIntegrateOperacionesComeBack == "")
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'success',title: 'Calculo guardado exitosamente!', html: '<label>Se ha generado el Cálculo de la baja y este registro no podrá ser modificado.</label>'});", true);
                                    }
                                    else
                                    {
                                        Session.RemoveAll();
                                        if (Request.QueryString["callback"] != null)
                                        {
                                            Response.Redirect(Request.QueryString["callback"].ToString() + "?filter=IdFiniquito&description=" + encDesvinculacion);
                                        }
                                        else
                                        {
                                            Response.Redirect(ModuleControlRetornoPlataformaTeamwork() + applicationIntegrateOperacionesComeBack.Replace("http://192.168.0.10/AplicacionOperaciones/", ""));
                                        }
                                    }
                                }
                                else
                                {
                                    btnGrabar.Enabled = true;
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede guardar el calculo debido a:', html: '" + errorDuplicidad + "'});", true);
                                }
                                
                            }
                            else
                            {
                                if (!chk_omitir_restriccion_retencionjudicial.Checked)
                                {
                                    btnGrabar.Enabled = true;
                                    string scripts = "Swal.fire({title: '¿Quieres guardar el finiquito?', text: 'Este finiquito ha sido tipificado como un finiquito con retención judicial!', icon: 'warning', showCancelButton: true, confirmButtonColor: '#3085d6', cancelButtonColor: '#d33', confirmButtonText: 'Guardar'}).then((result) => {if (result.isConfirmed) {document.querySelector('#chk_omitir_restriccion_retencionjudicial').checked = 'checked'; document.querySelector('#chk_restriccion_retencionjudicial').checked = false; document.querySelector('#btnGrabar').click();}});";
                                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", scripts, true);
                                }
                            }
                        }
                        else
                        {
                            btnGrabar.Enabled = true;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede guardar el calculo debido a:', html: '" + Session["errorGeneradoGuardarCalculo"] + " <br/> <label>Por favor revisar lo antes señalado.</label>'});", true);
                        }
                    }
                    else
                    {
                        btnGrabar.Enabled = true;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede guardar!', html: '<label>No existen datos para grabar el cálculo.</label>'});", true);
                    }
                }
                else
                {
                    btnGrabar.Enabled = true;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede guardar!', html: '<label>Debe buscar un trabajador y realizar su cálculo para grabar.</label>'});", true);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            //fncExportarExcelCopia();

            Plantillas plantilla = new Plantillas();
            Convertidor cnv = new Convertidor();
            string fechaInicio = string.Empty;
            string fechaTermino = string.Empty;
            bool chequeado = false;
            int cellPost = 0;
            string haberesTitulo = string.Empty;
            string haberesValor = string.Empty;
            string descuentoTitulo = string.Empty;
            string descuentoValor = string.Empty;
            string variableNegativa = string.Empty;

            int cantidadDescuentos = 0;
            int cantidadTotalHD = 0;
            int cantidadHaberes = 0;

            string[] fechas;

            List<string[]> fechasMultiContratos = new List<string[]>();
            List<string[]> haberesMulti = new List<string[]>();
            List<string[]> descuentosMulti = new List<string[]>();

            string empresa = "EST";

            using (ExcelPackage excel = new ExcelPackage())
            {

                foreach (GridViewRow rowItem in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                    if (chk.Checked)
                    {
                        chequeado = true;
                    }
                }

                if (cantidaddias.Text != "")
                {
                    if (chequeado)
                    {
                        /** CONFIGURACION DE HOJAS DE LIBRO EXCEL */
                        excel.Workbook.Worksheets.Add("Finiquito");
                        var worksheet = excel.Workbook.Worksheets["Finiquito"];
                        int tipoFiniquito;
                        int fontSizeTexto = 12;
                        int fontSizeTitulo = 14;

                        /** VARIABLE QUE DEFINE EL TIPO DE FINIQUITO, MENOR A 30 DIAS ES VARIABLE TIPO FINIQUITO IGUAL A 0, MAYOR A 30 ES VARIABLE TIPO FINIQUITO IGUAL A 1 */
                        if (Convert.ToInt32(cantidaddias.Text) <= 30)
                        {
                            tipoFiniquito = 0;
                        }
                        else
                        {
                            tipoFiniquito = 1;
                        }

                        /** DEFAULT DE COLUMNAS DE LA HOJA DEL LIBRO, 1.7 EQUIVALEN A 12 PIXELES */
                        worksheet.DefaultColWidth = 1.7;

                        switch (tipoFiniquito)
                        {
                            case 0:

                                /** OBTENCION DE FECHAS DE CONTRATOS */
                                fechas = null;
                                int contador = 0;
                                foreach (GridViewRow rowItem in GridView1.Rows)
                                {
                                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                    if (chk.Checked)
                                    {
                                        fechaInicio = rowItem.Cells[2].Text;
                                        fechaTermino = rowItem.Cells[3].Text;

                                        string[] fechasentre = {
                                            fechaInicio,
                                            fechaTermino
                                        };

                                        if (contador == 0)
                                        {
                                            fechas = fechasentre;
                                        }

                                        fechasMultiContratos.Add(fechasentre);

                                        contador = contador + 1;

                                    }
                                }

                                worksheet.Cells["C1:BM4"].Merge = true;
                                worksheet.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C1"].Style.Font.Bold = true;
                                worksheet.Cells["C1"].Style.Font.Size = fontSizeTitulo;
                                worksheet.Cells["C1"].Style.Font.Name = "Arial";
                                worksheet.Cells["C1"].Style.WrapText = true;
                                worksheet.Cells["C1"].Value = plantilla.tituloFiniquito(tipoFiniquito, empresa);

                                worksheet.Cells["C8:BM11"].Merge = true;
                                worksheet.Cells["C8"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C8"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C8"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C8"].Style.Font.Name = "Arial";
                                worksheet.Cells["C8"].Value = plantilla.inicioFiniquito(tipoFiniquito);

                                worksheet.Cells["C13:BM16"].Merge = true;
                                worksheet.Cells["C13"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C13"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C13"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C13"].Style.Font.Name = "Arial";
                                ExcelRichTextCollection rtfCollecionParrafoSeguido1 = worksheet.Cells["C13"].RichText;

                                ExcelRichText richSeguidoFiniquitoF1 = rtfCollecionParrafoSeguido1.Add(plantilla.seguidoFiniquitoF(fechas[1], empresa));
                                richSeguidoFiniquitoF1.Bold = false;

                                ExcelRichText richSeguidoFiniquitoS1 = rtfCollecionParrafoSeguido1.Add(plantilla.seguidoFiniquitoS(TextBox2.Text));
                                richSeguidoFiniquitoS1.Bold = true;

                                ExcelRichText richSeguidoFiniquitoT1 = rtfCollecionParrafoSeguido1.Add(plantilla.seguidoFiniquitoT(TextBox6.Text, empresa));
                                richSeguidoFiniquitoT1.Bold = false;

                                worksheet.Cells["C18:BM24"].Merge = true;
                                worksheet.Cells["C18"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C18"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C18"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C18"].Style.Font.Name = "Arial";

                                foreach (GridViewRow rowItem in GridView1.Rows)
                                {
                                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                    if (chk.Checked)
                                    {
                                        List<Causal> causal = Clases.Causales.cargarCausalDocumento(Properties.Settings.Default.connectionString, rowItem.Cells[5].Text);

                                        foreach (Causal causalDocumento in causal)
                                        {
                                            worksheet.Cells["C18"].Value = plantilla.parrafoPrimeroFiniquito(tipoFiniquito, TextBox5.Text, fechasMultiContratos, CausalDespido.SelectedItem.ToString(), causalDocumento.NUMERO, causalDocumento.DESCRIPCION, empresa);
                                        }
                                    }
                                }

                                worksheet.Cells["C26:BM27"].Merge = true;
                                worksheet.Cells["C26"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C26"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C26"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C26"].Style.Font.Name = "Arial";
                                worksheet.Cells["C26"].Value = plantilla.parrafoSegundoFiniquitoF(tipoFiniquito, TextBox2.Text, empresa) + " " + plantilla.parrafoSegundoFiniquitoS(tipoFiniquito, TextBox2.Text, empresa);

                                worksheet.Cells["C28:BM30"].Merge = true;
                                worksheet.Cells["C28"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C28"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C28"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C28"].Style.Font.Name = "Arial";
                                worksheet.Cells["C28"].Value = plantilla.parrafoTerceroFiniquito(tipoFiniquito);

                                /** GENERACIÓN DINAMICA DE HABERES Y DESCUENTOS PARA DOCUMENTO */

                                worksheet.Cells["C31:S31"].Merge = true;
                                worksheet.Cells["C31"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C31"].Style.Font.Name = "Arial";
                                worksheet.Cells["C31"].Value = "Feriado Proporcional";
                                worksheet.Cells["T31"].Value = lblTotalDiasFeriados.Text.Replace(',', '.');
                                worksheet.Cells["T31"].Style.Font.Size = fontSizeTexto;

                                worksheet.Cells["BD31:BM31"].Merge = true;
                                worksheet.Cells["BD31"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["BD31"].Style.Font.Name = "Arial";
                                worksheet.Cells["BD31"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["BD31"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                worksheet.Cells["BD31"].Value = "$ " + Label35.Text;

                                cellPost = 32;

                                cantidadTotalHD = 0;

                                /** RECORRIDO PARA HABERES */
                                cantidadHaberes = 0;
                                foreach (GridViewRow rowItem in GridView5.Rows)
                                {
                                    haberesTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                    haberesValor = rowItem.Cells[2].Text;

                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Value = haberesTitulo;

                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes) + ":BM" + Convert.ToString(cellPost + cantidadHaberes)].Merge = true;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Name = "Arial";
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes)].Value = "$" + int.Parse(haberesValor).ToString();

                                    cantidadHaberes = cantidadHaberes + 1;
                                }

                                /** RECORRIDO PARA DESCUENTOS */
                                cantidadDescuentos = 0;
                                foreach (GridViewRow rowItem in GridView4.Rows)
                                {
                                    descuentoTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                    descuentoValor = rowItem.Cells[2].Text;

                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Value = descuentoTitulo;

                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos) + ":BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Merge = true;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Name = "Arial";
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    worksheet.Cells["BD" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Value = "$" + int.Parse(descuentoValor).ToString("N0");

                                    cantidadDescuentos = cantidadDescuentos + 1;
                                }

                                /** SUMA TOTAL DE ELEMENTOS DINAMICOS HABERES Y DESCUENTOS PARA POSICIONAMIENTO EN HOJA DEL LIBRO */
                                cantidadTotalHD = cantidadDescuentos + cantidadHaberes;

                                cellPost = cellPost + cantidadTotalHD + 1;

                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Bold = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Value = "TOTAL";

                                worksheet.Cells["BF" + Convert.ToString(cellPost) + ":BM" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Style.Font.Bold = true;
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                worksheet.Cells["BF" + Convert.ToString(cellPost)].Value = "$ " + Label47.Text;

                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Bold = true;

                                if (Label47.Text.Contains("-"))
                                {
                                    variableNegativa = "MENOS ";
                                }

                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Value = "SON: " + variableNegativa + cnv.enletras(Label47.Text.Replace("-", "")) + " PESOS.-";

                                worksheet.Cells["C" + Convert.ToString(cellPost + 3) + ":BM" + Convert.ToString(cellPost + 10)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 3)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 3)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 3)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 3)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 3)].Value = plantilla.parrafoCuartoFiniquito(tipoFiniquito);

                                if (ccaf.Checked)
                                {
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12) + ":BM" + Convert.ToString(cellPost + 18)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Value = plantilla.parrafoSextoFiniquito(tipoFiniquito);

                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20) + ":BM" + Convert.ToString(cellPost + 21)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 20)].Value = plantilla.parrafoCopias();

                                    cellPost = cellPost + 27;
                                }
                                else
                                {

                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12) + ":BM" + Convert.ToString(cellPost + 13)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 12)].Value = plantilla.parrafoCopias();

                                    cellPost = cellPost + 32;
                                }

                                /** DATOS PARA FIRMA */
                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AA" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AA" + Convert.ToString(cellPost)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AA" + Convert.ToString(cellPost)].Style.Border.Top.Color.SetColor(Color.Black);
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Value = TextBox2.Text;

                                if (TextBox2.Text.Length > 29)
                                {
                                    worksheet.Row(cellPost).Height = 35.25;
                                    worksheet.Cells["C" + Convert.ToString(cellPost)].Style.WrapText = true;
                                }

                                worksheet.Cells["C" + Convert.ToString(cellPost + 1) + ":AA" + Convert.ToString(cellPost + 1)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Value = "C.I. " + TextBox6.Text.Replace(".", "");

                                /** DATOS ESTATICOS PARA FIRMA */
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":BM" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":BM" + Convert.ToString(cellPost)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":BM" + Convert.ToString(cellPost)].Style.Border.Top.Color.SetColor(Color.Black);
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Value = "EST SERVICE LTDA.";

                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1) + ":BM" + Convert.ToString(cellPost + 1)].Merge = true;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Value = "RUT 76.760.090-9 ";

                                worksheet.Cells["C" + Convert.ToString(cellPost + 5)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 5)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 5)].Value = TextBox8.Text;

                                worksheet.Cells["C" + Convert.ToString(cellPost + 6)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 6)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 6)].Value = TextBox7.Text;

                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Value = lblcentrocosto.Text;

                                break;
                            case 1:

                                worksheet.Cells["C1:CA1"].Merge = true;
                                worksheet.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C1"].Style.Font.Bold = true;
                                worksheet.Cells["C1"].Style.Font.Size = fontSizeTitulo;
                                worksheet.Cells["C1"].Style.Font.Name = "Arial";
                                worksheet.Cells["C1"].Style.WrapText = true;
                                worksheet.Cells["C1"].Value = plantilla.tituloFiniquito(tipoFiniquito, empresa);

                                /** OBTENCION DE FECHAS DE CONTRATOS */
                                fechas = null;
                                int contador2 = 0;
                                foreach (GridViewRow rowItem in GridView1.Rows)
                                {
                                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                    if (chk.Checked)
                                    {
                                        fechaInicio = rowItem.Cells[2].Text;
                                        fechaTermino = rowItem.Cells[3].Text;

                                        string[] fechasentre = {
                                            fechaInicio,
                                            fechaTermino
                                        };

                                        if (contador2 == 0)
                                        {
                                            fechas = fechasentre;
                                        }

                                        fechasMultiContratos.Add(fechasentre);

                                        contador2 = contador2 + 1;
                                    }
                                }

                                worksheet.Cells["C4:CA8"].Merge = true;
                                worksheet.Cells["C4"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C4"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C4"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C4"].Style.Font.Name = "Arial";

                                ExcelRichTextCollection rtfCollecionParrafoSeguido2 = worksheet.Cells["C4"].RichText;

                                ExcelRichText richSeguidoFiniquitoF2 = rtfCollecionParrafoSeguido2.Add(plantilla.seguidoFiniquitoF(fechas[1], empresa));
                                richSeguidoFiniquitoF2.Bold = false;

                                ExcelRichText richSeguidoFiniquitoS2 = rtfCollecionParrafoSeguido2.Add(plantilla.seguidoFiniquitoS(TextBox2.Text));
                                richSeguidoFiniquitoS2.Bold = true;

                                ExcelRichText richSeguidoFiniquitoT2 = rtfCollecionParrafoSeguido2.Add(plantilla.seguidoFiniquitoT(TextBox6.Text, empresa));
                                richSeguidoFiniquitoT2.Bold = false;

                                worksheet.Cells["C10:CA14"].Merge = true;
                                worksheet.Cells["C10"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C10"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C10"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C10"].Style.Font.Name = "Arial";
                                worksheet.Cells["C10"].Value = plantilla.parrafoPrimeroFiniquito(tipoFiniquito, TextBox5.Text, fechasMultiContratos, CausalDespido.SelectedItem.ToString(), "", "", empresa);

                                worksheet.Cells["C16:CA30"].Merge = true;
                                worksheet.Cells["C16"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C16"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C16"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C16"].Style.Font.Name = "Arial";
                                worksheet.Cells["C16"].Value = plantilla.parrafoSegundoFiniquitoF(tipoFiniquito, TextBox2.Text, empresa) + " " + plantilla.parrafoSegundoFiniquitoS(tipoFiniquito, TextBox2.Text, empresa);

                                worksheet.Cells["C32:CA34"].Merge = true;
                                worksheet.Cells["C32"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C32"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C32"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C32"].Style.Font.Name = "Arial";
                                worksheet.Cells["C32"].Value = plantilla.parrafoTerceroFiniquito(tipoFiniquito);

                                worksheet.Row(30).Height = 130.00;

                                /** DATOS HABERES */
                                worksheet.Cells["C36:R36"].Merge = true;
                                worksheet.Cells["C36"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C36"].Style.Font.Name = "Arial";
                                worksheet.Cells["C36"].Value = "HABERES";

                                worksheet.Cells["C37:R37"].Merge = true;
                                worksheet.Cells["C37"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C37"].Style.Font.Name = "Arial";

                                worksheet.Cells["C37"].Value = "Feriado Proporcional";

                                worksheet.Cells["S37"].Value = lblTotalDiasFeriados.Text.Replace(',', '.');
                                worksheet.Cells["S37"].Style.Font.Size = fontSizeTexto;

                                worksheet.Cells["BM37:CA37"].Merge = true;
                                worksheet.Cells["BM37"].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["BM37"].Style.Font.Name = "Arial";
                                worksheet.Cells["BM37"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["BM37"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                worksheet.Cells["BM37"].Value = "$ " + Label35.Text;

                                cellPost = 38;

                                cantidadTotalHD = 0;

                                /** RECORRIDO PARA HABERES */
                                cantidadHaberes = 0;
                                foreach (GridViewRow rowItem in GridView5.Rows)
                                {
                                    haberesTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                    haberesValor = rowItem.Cells[2].Text;

                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes)].Value = haberesTitulo;

                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes) + ":CA" + Convert.ToString(cellPost + cantidadHaberes)].Merge = true;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes)].Style.Font.Name = "Arial";
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes)].Value = "$ " + int.Parse(haberesValor).ToString("N0");

                                    cantidadHaberes = cantidadHaberes + 1;
                                }

                                /** RECORRIDO PARA DESCUENTOS */
                                cantidadDescuentos = 0;
                                foreach (GridViewRow rowItem in GridView4.Rows)
                                {
                                    descuentoTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                    descuentoValor = rowItem.Cells[2].Text;

                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Value = descuentoTitulo;

                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos) + ":CA" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Merge = true;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.Font.Name = "Arial";
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                    worksheet.Cells["BM" + Convert.ToString(cellPost + cantidadHaberes + cantidadDescuentos)].Value = "$ " + int.Parse(descuentoValor).ToString("N0");

                                    cantidadDescuentos = cantidadDescuentos + 1;
                                }

                                /** SUMA TOTAL DE ELEMENTOS DINAMICOS HABERES Y DESCUENTOS PARA POSICIONAMIENTO EN HOJA DEL LIBRO */
                                cantidadTotalHD = cantidadDescuentos + cantidadHaberes;

                                cellPost = cellPost + cantidadTotalHD + 1;

                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Bold = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Value = "TOTAL";

                                worksheet.Cells["BM" + Convert.ToString(cellPost) + ":CA" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Style.Font.Bold = true;
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                                worksheet.Cells["BM" + Convert.ToString(cellPost)].Value = "$ " + Label47.Text;

                                if (Label47.Text.Contains("-"))
                                {
                                    variableNegativa = "MENOS ";
                                }

                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Bold = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Value = "SON: " + variableNegativa + cnv.enletras(Label47.Text.Replace("-", "")) + " PESOS.-";

                                worksheet.Cells["C" + Convert.ToString(cellPost + 4) + ":CA" + Convert.ToString(cellPost + 9)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 4)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 4)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 4)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 4)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 4)].Value = plantilla.parrafoCuartoFiniquito(tipoFiniquito);

                                worksheet.Cells["C" + Convert.ToString(cellPost + 11) + ":CA" + Convert.ToString(cellPost + 15)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 11)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 11)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 11)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 11)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 11)].IsRichText = true;

                                ExcelRichTextCollection rtfCollection = worksheet.Cells["C" + Convert.ToString(cellPost + 11)].RichText;

                                ExcelRichText richParrafoQuintoFiniquitoF = rtfCollection.Add(plantilla.parrafoQuintoFiniquitoF());
                                richParrafoQuintoFiniquitoF.Bold = false;

                                ExcelRichText richParrafoQuintoFiniquitoS = rtfCollection.Add(plantilla.parrafoQuintoFiniquitoS(fechasMultiContratos));
                                richParrafoQuintoFiniquitoS.Bold = true;

                                ExcelRichText richParrafoQuintoFiniquitoT = rtfCollection.Add(plantilla.parrafoQuintoFiniquitoT());
                                richParrafoQuintoFiniquitoT.Bold = false;

                                if (ccaf.Checked)
                                {
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17) + ":CA" + Convert.ToString(cellPost + 20)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Value = plantilla.parrafoSextoFiniquito(tipoFiniquito);

                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22) + ":CA" + Convert.ToString(cellPost + 23)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Style.Font.Bold = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 22)].Value = plantilla.parrafoCopias();

                                    cellPost = cellPost + 30;
                                }
                                else
                                {

                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17) + ":CA" + Convert.ToString(cellPost + 18)].Merge = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Justify;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.Font.Size = fontSizeTexto;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.Font.Bold = true;
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Style.Font.Name = "Arial";
                                    worksheet.Cells["C" + Convert.ToString(cellPost + 17)].Value = plantilla.parrafoCopias();

                                    cellPost = cellPost + 35;
                                }


                                /** FIRMAS DOCUMENTO */

                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AE" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AE" + Convert.ToString(cellPost)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                worksheet.Cells["C" + Convert.ToString(cellPost) + ":AE" + Convert.ToString(cellPost)].Style.Border.Top.Color.SetColor(Color.Black);
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost)].Value = TextBox2.Text;

                                if (TextBox2.Text.Length > 29)
                                {
                                    worksheet.Row(cellPost).Height = 35.25;
                                    worksheet.Cells["C" + Convert.ToString(cellPost)].Style.WrapText = true;
                                }

                                worksheet.Cells["C" + Convert.ToString(cellPost + 1) + ":AE" + Convert.ToString(cellPost + 1)].Merge = true;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 1)].Value = "C.I. " + TextBox6.Text.Replace(".", "");

                                /** DATOS ESTATICOS PARA FIRMA */
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":CA" + Convert.ToString(cellPost)].Merge = true;
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":CA" + Convert.ToString(cellPost)].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                                worksheet.Cells["AO" + Convert.ToString(cellPost) + ":CA" + Convert.ToString(cellPost)].Style.Border.Top.Color.SetColor(Color.Black);
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.Font.Name = "Arial";
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost)].Value = "EST SERVICE LTDA.";

                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1) + ":CA" + Convert.ToString(cellPost + 1)].Merge = true;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.Font.Name = "Arial";
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                worksheet.Cells["AO" + Convert.ToString(cellPost + 1)].Value = "RUT 76.760.090-9 ";

                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 7)].Value = TextBox8.Text;

                                worksheet.Cells["C" + Convert.ToString(cellPost + 8)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 8)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 8)].Value = TextBox7.Text;

                                worksheet.Cells["C" + Convert.ToString(cellPost + 9)].Style.Font.Size = fontSizeTexto;
                                worksheet.Cells["C" + Convert.ToString(cellPost + 9)].Style.Font.Name = "Arial";
                                worksheet.Cells["C" + Convert.ToString(cellPost + 9)].Value = lblcentrocosto.Text;

                                break;
                        }


                        worksheet.PrinterSettings.LeftMargin = Convert.ToDecimal(1.1);
                        worksheet.PrinterSettings.RightMargin = Convert.ToDecimal(1.2);
                        worksheet.PrinterSettings.TopMargin = Convert.ToDecimal(.2);
                        worksheet.PrinterSettings.BottomMargin = Convert.ToDecimal(.5);
                        worksheet.PrinterSettings.HeaderMargin = Convert.ToDecimal(.8);

                        string nombreArchivo = "FiniquitoContrato_" + DateTime.Now.ToString().Replace("-", "").Replace(' ', '_').Replace(":", "") + ".xlsx";

                        worksheet.Column(2).PageBreak = true;
                        worksheet.PrinterSettings.FitToPage = true;
                        worksheet.PrinterSettings.PaperSize = ePaperSize.Letter;
                        worksheet.PrinterSettings.VerticalCentered = true;
                        excel.Workbook.Properties.Title = "Attempts";
                        Response.ClearContent();
                        Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", nombreArchivo));
                        Response.ContentType = "application/excel";
                        Response.BinaryWrite(excel.GetAsByteArray());
                        Response.Flush();
                        Response.End();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning',title: 'No se puede exportar!', html: '<label>Debe Seleccionar contratos para generar documento</label>'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning',title: 'No se puede exportar!', html: '<label>Debe calcular para generar documento.</label>'});", true);
                }
            }
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {

            //fncExportarPDFCopia();

            Plantillas plantilla = new Plantillas();
            Convertidor cnv = new Convertidor();
            string fechaInicio = string.Empty;
            string fechaTermino = string.Empty;
            bool chequeado = false;
            string haberesTitulo = string.Empty;
            string haberesValor = string.Empty;
            string descuentoTitulo = string.Empty;
            string descuentoValor = string.Empty;
            string variableNegativa = string.Empty;
            int tipoFiniquito;

            PdfPTable tablaTitulo, tablaInicioFiniquito, tablaSeguidoFiniquito, tablaParrafoPrimeroFiniquito, tablaParrafoSegundoFiniquito, tabla, table2,
                tablaValorStringFiniquito, tablaParrafoTerceroFiniquito, tablaParrafoCuartoFiniquito, tabla3, tablaFooter, tablaParrafoQuintoFiniquito,
                tablaParrafoSextoFiniquito, tablaTextoHaberes, tablaParrafoCopias;
            PdfPCell[] cell, cell2, cell3, cell4, cell5;
            PdfPCell cellTitulo, cellInicioFiniquito, cellSeguidoFiniquito, cellParrafoPrimeroFiniquito, cellParrafoSegundoFiniquito,
                cellValorStringoFiniquito, cellParrafoTerceroFiniquito, cellParrafoCuartoFiniquito, firmaTrabajador, firmaEspaciado, firmaEST,
                cellFooter, cellParrafoQuintoFiniquito, cellParrafoSextoFiniquito, cellTextoHaberes, cellParrafoCopias;
            PdfPRow row, row1, row2, row3, row4, row5;

            Paragraph saltoDeLinea = new Paragraph("\n");

            var fuenteTitulo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 8f, 10);
            var fuenteTexto = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 8f, 10);
            var fuenteTextoFinal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.UNDEFINED, 8f, 10);

            var fontSizeTitulo = 11;
            var fontSizeTexto = 8;

            string[] fechas;

            List<string[]> fechasMultiContratos = new List<string[]>();
            List<string[]> haberesMulti = new List<string[]>();
            List<string[]> descuentosMulti = new List<string[]>();

            string empresa = "EST";

            foreach (GridViewRow rowItem in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                if (chk.Checked)
                {
                    chequeado = true;
                }
            }

            if (cantidaddias.Text != "")
            {
                if (chequeado)
                {
                    //FileStream fs = new FileStream("F:\\FINIQUITOSPDF\\FiniquitoContrato_" + DateTime.Now.ToString().Replace("-", "").Replace(' ', '_').Replace(":", "") + ".pdf",
                    //            FileMode.Create, FileAccess.Write, FileShare.None);

                    //FileStream fs = new FileStream("C:\\Users\\Sebastian Salas\\Downloads\\FiniquitoContrato_" + DateTime.Now.ToString().Replace("-", "").Replace(' ', '_').Replace(":", "") + ".pdf",
                    //        FileMode.Create, FileAccess.Write, FileShare.None);

                    Document document = new Document(PageSize.LETTER, 50, 50, 20, 20);
                    MemoryStream stream = new MemoryStream();
                    PdfWriter writerPdf = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    /** VARIABLE QUE DEFINE EL TIPO DE FINIQUITO, MENOR A 30 DIAS ES VARIABLE TIPO FINIQUITO IGUAL A 0, MAYOR A 30 ES VARIABLE TIPO FINIQUITO IGUAL A 1 */
                    if (Convert.ToInt32(cantidaddias.Text) <= 30)
                    {
                        tipoFiniquito = 0;
                    }
                    else
                    {
                        tipoFiniquito = 1;
                    }

                    switch (tipoFiniquito)
                    {
                        case 0:
                            /** OBTENCION DE FECHAS DE CONTRATOS */
                            fechas = null;
                            int contador = 0;
                            var output = new MemoryStream();

                            foreach (GridViewRow rowItem in GridView1.Rows)
                            {
                                CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                if (chk.Checked)
                                {
                                    fechaInicio = rowItem.Cells[2].Text;
                                    fechaTermino = rowItem.Cells[3].Text;

                                    string[] fechasentre = {
                                        fechaInicio,
                                        fechaTermino
                                    };

                                    if (contador == 0)
                                    {
                                        fechas = fechasentre;
                                    }

                                    fechasMultiContratos.Add(fechasentre);

                                    contador = contador + 1;
                                }
                            }

                            tablaTitulo = new PdfPTable(1);
                            cellTitulo = new PdfPCell(new Phrase(plantilla.tituloFiniquito(tipoFiniquito, empresa), FontFactory.GetFont("Arial", fontSizeTitulo, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellTitulo.UseVariableBorders = true;
                            cellTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellTitulo.BorderColor = BaseColor.WHITE;
                            tablaTitulo.WidthPercentage = 100;
                            tablaTitulo.AddCell(cellTitulo);

                            document.Add(tablaTitulo);

                            document.Add(saltoDeLinea);

                            tablaInicioFiniquito = new PdfPTable(1);
                            cellInicioFiniquito = new PdfPCell(new Phrase(plantilla.inicioFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellInicioFiniquito.UseVariableBorders = true;
                            cellInicioFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellInicioFiniquito.BorderColor = BaseColor.WHITE;
                            tablaInicioFiniquito.WidthPercentage = 100;
                            tablaInicioFiniquito.AddCell(cellInicioFiniquito);

                            document.Add(tablaInicioFiniquito);

                            document.Add(saltoDeLinea);

                            tablaSeguidoFiniquito = new PdfPTable(1);

                            var fraseSeguidoFiniquito1 = new Phrase(plantilla.seguidoFiniquitoF(fechas[1], empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                            fraseSeguidoFiniquito1.Add(new Phrase(new Chunk(plantilla.seguidoFiniquitoS(TextBox2.Text), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                            fraseSeguidoFiniquito1.Add(new Phrase(new Chunk(plantilla.seguidoFiniquitoT(TextBox6.Text, empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                            cellSeguidoFiniquito = new PdfPCell(fraseSeguidoFiniquito1);


                            cellSeguidoFiniquito.UseVariableBorders = true;
                            cellSeguidoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellSeguidoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaSeguidoFiniquito.WidthPercentage = 100;
                            tablaSeguidoFiniquito.AddCell(cellSeguidoFiniquito);

                            document.Add(tablaSeguidoFiniquito);

                            document.Add(saltoDeLinea);

                            tablaParrafoPrimeroFiniquito = new PdfPTable(1);
                            string parrafoEspecial = string.Empty;

                            foreach (GridViewRow rowItem in GridView1.Rows)
                            {
                                CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                if (chk.Checked)
                                {
                                    List<Causal> causal = Clases.Causales.cargarCausalDocumento(Properties.Settings.Default.connectionString, rowItem.Cells[5].Text);

                                    foreach (Causal causalDocumento in causal)
                                    {
                                        parrafoEspecial = plantilla.parrafoPrimeroFiniquito(tipoFiniquito, TextBox5.Text, fechasMultiContratos, CausalDespido.SelectedItem.ToString(), causalDocumento.NUMERO, causalDocumento.DESCRIPCION, empresa);
                                    }
                                }
                            }

                            cellParrafoPrimeroFiniquito = new PdfPCell(new Phrase(parrafoEspecial, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoPrimeroFiniquito.UseVariableBorders = true;
                            cellParrafoPrimeroFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoPrimeroFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoPrimeroFiniquito.WidthPercentage = 100;
                            tablaParrafoPrimeroFiniquito.AddCell(cellParrafoPrimeroFiniquito);

                            document.Add(tablaParrafoPrimeroFiniquito);

                            document.Add(saltoDeLinea);

                            tablaParrafoSegundoFiniquito = new PdfPTable(1);
                            cellParrafoSegundoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoSegundoFiniquitoF(tipoFiniquito, TextBox2.Text, empresa) + " " + plantilla.parrafoSegundoFiniquitoS(tipoFiniquito, TextBox2.Text, empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoSegundoFiniquito.UseVariableBorders = true;
                            cellParrafoSegundoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoSegundoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoSegundoFiniquito.WidthPercentage = 100;
                            tablaParrafoSegundoFiniquito.AddCell(cellParrafoSegundoFiniquito);

                            document.Add(tablaParrafoSegundoFiniquito);
                            
                            document.Add(saltoDeLinea);

                            tablaParrafoTerceroFiniquito = new PdfPTable(1);
                            cellParrafoTerceroFiniquito = new PdfPCell(new Phrase(plantilla.parrafoTerceroFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoTerceroFiniquito.UseVariableBorders = true;
                            cellParrafoTerceroFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoTerceroFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoTerceroFiniquito.WidthPercentage = 100;
                            tablaParrafoTerceroFiniquito.AddCell(cellParrafoTerceroFiniquito);

                            document.Add(tablaParrafoTerceroFiniquito);

                            tabla = new PdfPTable(3);

                            cell = new PdfPCell[] {
                                new PdfPCell(new Phrase("Feriado Proporcional", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase(lblTotalDiasFeriados.Text.Replace(',', '.'), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("$ " + Label35.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                            };

                            cell[0].BorderColor = BaseColor.WHITE;
                            cell[1].BorderColor = BaseColor.WHITE;
                            cell[2].BorderColor = BaseColor.WHITE;
                            cell[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                            row = new PdfPRow(cell);
                            tabla.Rows.Add(row);

                            /** RECORRIDO PARA HABERES */
                            foreach (GridViewRow rowItem in GridView5.Rows)
                            {
                                haberesTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                haberesValor = rowItem.Cells[2].Text;

                                cell2 = new PdfPCell[] {
                                    new PdfPCell(new Phrase(haberesTitulo, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("$ " + int.Parse(haberesValor).ToString("N0"), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                                };

                                cell2[0].BorderColor = BaseColor.WHITE;
                                cell2[1].BorderColor = BaseColor.WHITE;
                                cell2[2].BorderColor = BaseColor.WHITE;
                                cell2[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                                row2 = new PdfPRow(cell2);
                                tabla.Rows.Add(row2);
                            }

                            /** RECORRIDO PARA DESCUENTOS */
                            foreach (GridViewRow rowItem in GridView4.Rows)
                            {
                                descuentoTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                descuentoValor = rowItem.Cells[2].Text;

                                cell3 = new PdfPCell[] {
                                    new PdfPCell(new Phrase(descuentoTitulo, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("$ -" + int.Parse(descuentoValor).ToString("N0"),FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                                };

                                cell3[0].BorderColor = BaseColor.WHITE;
                                cell3[1].BorderColor = BaseColor.WHITE;
                                cell3[2].BorderColor = BaseColor.WHITE;
                                cell3[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                                row3 = new PdfPRow(cell3);
                                tabla.Rows.Add(row3);
                            }

                            document.Add(saltoDeLinea);

                            PdfPTable tabla2 = new PdfPTable(3);

                            cell4 = new PdfPCell[] {
                                new PdfPCell(new Phrase("TOTAL", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("$ " + Label47.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK)))
                            };

                            cell4[0].BorderColor = BaseColor.WHITE;
                            cell4[1].BorderColor = BaseColor.WHITE;
                            cell4[2].BorderColor = BaseColor.WHITE;
                            cell4[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                            row4 = new PdfPRow(cell4);
                            tabla2.Rows.Add(row4);

                            tabla.WidthPercentage = 100;
                            tabla.DefaultCell.Border = 0;
                            document.Add(tabla);

                            document.Add(saltoDeLinea);

                            if (Label47.Text.Contains("-"))
                            {
                                variableNegativa = "MENOS ";
                            }

                            tabla2.WidthPercentage = 100;
                            tabla2.DefaultCell.BorderColor = BaseColor.WHITE;
                            document.Add(tabla2);

                            tablaValorStringFiniquito = new PdfPTable(1);
                            cellValorStringoFiniquito = new PdfPCell(new Phrase("SON: " + variableNegativa + cnv.enletras(Label47.Text.Replace("-", "")) + " PESOS.-", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellValorStringoFiniquito.UseVariableBorders = true;
                            cellValorStringoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaValorStringFiniquito.WidthPercentage = 100;
                            tablaValorStringFiniquito.AddCell(cellValorStringoFiniquito);

                            document.Add(tablaValorStringFiniquito);

                            document.Add(saltoDeLinea);
                            
                            tablaParrafoCuartoFiniquito = new PdfPTable(1);
                            cellParrafoCuartoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoCuartoFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoCuartoFiniquito.UseVariableBorders = true;
                            cellParrafoCuartoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoCuartoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoCuartoFiniquito.WidthPercentage = 100;
                            tablaParrafoCuartoFiniquito.AddCell(cellParrafoCuartoFiniquito);

                            document.Add(tablaParrafoCuartoFiniquito);

                            document.Add(saltoDeLinea);

                            if (ccaf.Checked){
                                tablaParrafoCuartoFiniquito = new PdfPTable(1);
                                cellParrafoCuartoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoSextoFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoCuartoFiniquito.UseVariableBorders = true;
                                cellParrafoCuartoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoCuartoFiniquito.BorderColor = BaseColor.WHITE;
                                tablaParrafoCuartoFiniquito.WidthPercentage = 100;
                                tablaParrafoCuartoFiniquito.AddCell(cellParrafoCuartoFiniquito);

                                document.Add(tablaParrafoCuartoFiniquito);

                                tablaParrafoCopias = new PdfPTable(1);
                                cellParrafoCopias = new PdfPCell(new Phrase(plantilla.parrafoCopias(), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoCopias.UseVariableBorders = true;
                                cellParrafoCopias.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoCopias.BorderColor = BaseColor.WHITE;
                                tablaParrafoCopias.WidthPercentage = 100;
                                tablaParrafoCopias.AddCell(cellParrafoCopias);

                                document.Add(tablaParrafoCopias);

                            } else {

                                tablaParrafoCopias = new PdfPTable(1);
                                cellParrafoCopias = new PdfPCell(new Phrase(plantilla.parrafoCopias(), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoCopias.UseVariableBorders = true;
                                cellParrafoCopias.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoCopias.BorderColor = BaseColor.WHITE;
                                tablaParrafoCopias.WidthPercentage = 100;
                                tablaParrafoCopias.AddCell(cellParrafoCopias);

                                document.Add(tablaParrafoCopias);

                                document.Add(saltoDeLinea);
                            }

                            document.Add(saltoDeLinea);
                            document.Add(saltoDeLinea);
                            document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);
                            //document.Add(saltoDeLinea);

                            tabla3 = new PdfPTable(3);

                            firmaTrabajador = new PdfPCell(new Phrase(TextBox2.Text + "\n" + "C.I. " + TextBox6.Text.Replace(".", ""), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));

                            firmaTrabajador.UseVariableBorders = true;
                            firmaTrabajador.BorderColorTop = BaseColor.BLACK;
                            firmaTrabajador.BorderColorLeft = BaseColor.WHITE;
                            firmaTrabajador.BorderColorRight = BaseColor.WHITE;
                            firmaTrabajador.BorderColorBottom = BaseColor.WHITE;
                            firmaTrabajador.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            firmaTrabajador.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                            firmaEspaciado = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            firmaEspaciado.BorderColor = BaseColor.WHITE;

                            firmaEST = new PdfPCell(new Phrase("EST SERVICE LTDA." + "\n" + "RUT 76.760.090-9 ", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            firmaEST.UseVariableBorders = true;
                            firmaEST.BorderColorTop = BaseColor.BLACK;
                            firmaEST.BorderColorLeft = BaseColor.WHITE;
                            firmaEST.BorderColorRight = BaseColor.WHITE;
                            firmaEST.BorderColorBottom = BaseColor.WHITE;
                            firmaEST.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            firmaEST.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                            tabla3.AddCell(firmaTrabajador);
                            tabla3.AddCell(firmaEspaciado);
                            tabla3.AddCell(firmaEST);

                            tabla3.WidthPercentage = 100;
                            document.Add(tabla3);

                            document.Add(saltoDeLinea);

                            tablaFooter = new PdfPTable(1);
                            cellFooter = new PdfPCell(new Phrase(TextBox8.Text + "\n" + TextBox7.Text + "\n" + lblcentrocosto.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellFooter.UseVariableBorders = true;
                            cellFooter.BorderColor = BaseColor.WHITE;
                            tablaFooter.WidthPercentage = 100;
                            tablaFooter.AddCell(cellFooter);

                            document.Add(tablaFooter);

                            break;
                        case 1:

                            /** OBTENCION DE FECHAS DE CONTRATOS */
                            fechas = null;
                            int contadores = 0;
                            foreach (GridViewRow rowItem in GridView1.Rows)
                            {
                                CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                if (chk.Checked)
                                {
                                    fechaInicio = rowItem.Cells[2].Text;
                                    fechaTermino = rowItem.Cells[3].Text;

                                    string[] fechasentre = {
                                        fechaInicio,
                                        fechaTermino
                                    };

                                    if (contadores == 0)
                                    {
                                        fechas = fechasentre;
                                    }

                                    fechasMultiContratos.Add(fechasentre);

                                    contadores = contadores + 1;
                                }
                            }


                            tablaTitulo = new PdfPTable(1);
                            cellTitulo = new PdfPCell(new Phrase(plantilla.tituloFiniquito(tipoFiniquito, empresa), FontFactory.GetFont("Arial", fontSizeTitulo, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellTitulo.UseVariableBorders = true;
                            cellTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
                            cellTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                            cellTitulo.BorderColor = BaseColor.WHITE;
                            tablaTitulo.WidthPercentage = 100;
                            tablaTitulo.AddCell(cellTitulo);

                            document.Add(tablaTitulo);

                            document.Add(saltoDeLinea);

                            tablaInicioFiniquito = new PdfPTable(1);
                            cellInicioFiniquito = new PdfPCell(new Phrase(plantilla.inicioFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellInicioFiniquito.UseVariableBorders = true;
                            cellInicioFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellInicioFiniquito.BorderColor = BaseColor.WHITE;
                            tablaInicioFiniquito.WidthPercentage = 100;
                            tablaInicioFiniquito.AddCell(cellInicioFiniquito);

                            document.Add(tablaInicioFiniquito);

                            document.Add(saltoDeLinea);

                            tablaSeguidoFiniquito = new PdfPTable(1);

                            var fraseSeguidoFiniquito2 = new Phrase(plantilla.seguidoFiniquitoF(fechas[1], empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                            fraseSeguidoFiniquito2.Add(new Phrase(new Chunk(plantilla.seguidoFiniquitoS(TextBox2.Text), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                            fraseSeguidoFiniquito2.Add(new Phrase(new Chunk(plantilla.seguidoFiniquitoT(TextBox6.Text, empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                            cellSeguidoFiniquito = new PdfPCell(fraseSeguidoFiniquito2);
                            cellSeguidoFiniquito.UseVariableBorders = true;
                            cellSeguidoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellSeguidoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaSeguidoFiniquito.WidthPercentage = 100;
                            tablaSeguidoFiniquito.AddCell(cellSeguidoFiniquito);

                            document.Add(tablaSeguidoFiniquito);

                            document.Add(saltoDeLinea);

                            tablaParrafoPrimeroFiniquito = new PdfPTable(1);
                            cellParrafoPrimeroFiniquito = new PdfPCell(new Phrase(plantilla.parrafoPrimeroFiniquito(tipoFiniquito, TextBox5.Text, fechasMultiContratos, CausalDespido.SelectedItem.ToString(), "", "", empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoPrimeroFiniquito.UseVariableBorders = true;
                            cellParrafoPrimeroFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoPrimeroFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoPrimeroFiniquito.WidthPercentage = 100;
                            tablaParrafoPrimeroFiniquito.AddCell(cellParrafoPrimeroFiniquito);

                            document.Add(tablaParrafoPrimeroFiniquito);

                            document.Add(saltoDeLinea);

                            tablaParrafoSegundoFiniquito = new PdfPTable(1);
                            cellParrafoSegundoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoSegundoFiniquitoF(tipoFiniquito, TextBox2.Text, empresa) + plantilla.parrafoSegundoFiniquitoS(tipoFiniquito, TextBox2.Text, empresa), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoSegundoFiniquito.UseVariableBorders = true;
                            cellParrafoSegundoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoSegundoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoSegundoFiniquito.WidthPercentage = 100;
                            tablaParrafoSegundoFiniquito.AddCell(cellParrafoSegundoFiniquito);

                            document.Add(tablaParrafoSegundoFiniquito);

                            document.Add(saltoDeLinea);

                            tablaParrafoTerceroFiniquito = new PdfPTable(1);
                            cellParrafoTerceroFiniquito = new PdfPCell(new Phrase(plantilla.parrafoTerceroFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoTerceroFiniquito.UseVariableBorders = true;
                            cellParrafoTerceroFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoTerceroFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoTerceroFiniquito.WidthPercentage = 100;
                            tablaParrafoTerceroFiniquito.AddCell(cellParrafoTerceroFiniquito);

                            document.Add(tablaParrafoTerceroFiniquito);

                            document.Add(saltoDeLinea);

                            tablaTextoHaberes = new PdfPTable(1);
                            cellTextoHaberes = new PdfPCell(new Phrase("HABERES", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellTextoHaberes.BorderColor = BaseColor.WHITE;
                            tablaTextoHaberes.WidthPercentage = 100;

                            tablaTextoHaberes.AddCell(cellTextoHaberes);

                            document.Add(tablaTextoHaberes);

                            tabla = new PdfPTable(3);

                            cell = new PdfPCell[] {
                                new PdfPCell(new Phrase("Feriado Proporcional", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase(lblTotalDiasFeriados.Text.Replace(',', '.'), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("$ " + Label35.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                            };

                            cell[0].BorderColor = BaseColor.WHITE;
                            cell[1].BorderColor = BaseColor.WHITE;
                            cell[2].BorderColor = BaseColor.WHITE;
                            cell[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                            row = new PdfPRow(cell);
                            tabla.Rows.Add(row);

                            /** RECORRIDO PARA HABERES */
                            foreach (GridViewRow rowItem in GridView5.Rows)
                            {
                                haberesTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                haberesValor = rowItem.Cells[2].Text;

                                cell2 = new PdfPCell[] {
                                    new PdfPCell(new Phrase(haberesTitulo, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("$ " + int.Parse(haberesValor).ToString("N0"), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                                };

                                cell2[0].BorderColor = BaseColor.WHITE;
                                cell2[1].BorderColor = BaseColor.WHITE;
                                cell2[2].BorderColor = BaseColor.WHITE;
                                cell2[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                                row2 = new PdfPRow(cell2);
                                tabla.Rows.Add(row2);
                            }

                            /** RECORRIDO PARA DESCUENTOS */
                            foreach (GridViewRow rowItem in GridView4.Rows)
                            {
                                descuentoTitulo = replaceCharacter(rowItem.Cells[1].Text);
                                descuentoValor = rowItem.Cells[2].Text;

                                cell3 = new PdfPCell[] {
                                    new PdfPCell(new Phrase(descuentoTitulo, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                    new PdfPCell(new Phrase("$ " + int.Parse(descuentoValor).ToString("N0"),FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)))
                                };

                                cell3[0].BorderColor = BaseColor.WHITE;
                                cell3[1].BorderColor = BaseColor.WHITE;
                                cell3[2].BorderColor = BaseColor.WHITE;
                                cell3[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                                row3 = new PdfPRow(cell3);
                                tabla.Rows.Add(row3);
                            }


                            tabla2 = new PdfPTable(3);

                            cell4 = new PdfPCell[] {
                                new PdfPCell(new Phrase("TOTAL", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))),
                                new PdfPCell(new Phrase("$ " + Label47.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK)))
                            };

                            cell4[0].BorderColor = BaseColor.WHITE;
                            cell4[1].BorderColor = BaseColor.WHITE;
                            cell4[2].BorderColor = BaseColor.WHITE;
                            cell4[2].HorizontalAlignment = Element.ALIGN_RIGHT;

                            row4 = new PdfPRow(cell4);
                            tabla2.Rows.Add(row4);

                            tabla.WidthPercentage = 100;
                            tabla.DefaultCell.Border = 0;
                            document.Add(tabla);

                            document.Add(saltoDeLinea);

                            if (Label47.Text.Contains("-"))
                            {
                                variableNegativa = "MENOS ";
                            }

                            tabla2.WidthPercentage = 100;
                            tabla2.DefaultCell.BorderColor = BaseColor.WHITE;
                            document.Add(tabla2);

                            tablaValorStringFiniquito = new PdfPTable(1);
                            cellValorStringoFiniquito = new PdfPCell(new Phrase("SON: " + variableNegativa + cnv.enletras(Label47.Text.Replace("-", "")) + " PESOS.-", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));
                            cellValorStringoFiniquito.UseVariableBorders = true;
                            cellValorStringoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaValorStringFiniquito.WidthPercentage = 100;
                            tablaValorStringFiniquito.AddCell(cellValorStringoFiniquito);

                            document.Add(tablaValorStringFiniquito);

                            /** 1 */
                            document.Add(saltoDeLinea);

                            tablaParrafoCuartoFiniquito = new PdfPTable(1);
                            cellParrafoCuartoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoCuartoFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellParrafoCuartoFiniquito.UseVariableBorders = true;
                            cellParrafoCuartoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoCuartoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoCuartoFiniquito.WidthPercentage = 100;
                            tablaParrafoCuartoFiniquito.AddCell(cellParrafoCuartoFiniquito);

                            document.Add(tablaParrafoCuartoFiniquito);

                            /** 2 */
                            document.Add(saltoDeLinea);

                            tablaParrafoQuintoFiniquito = new PdfPTable(1);
                            //cellParrafoQuintoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoQuintoFiniquitoF() + plantilla.parrafoQuintoFiniquitoS(fechasMultiContratos) + plantilla.parrafoQuintoFiniquitoT().Replace("\n", ""), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK)));


                            var fraseQuintoParrafo = new Phrase(plantilla.parrafoQuintoFiniquitoF(), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK));
                            fraseQuintoParrafo.Add(new Phrase(new Chunk(plantilla.parrafoQuintoFiniquitoS(fechasMultiContratos).Replace("\n", ""), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                            fraseQuintoParrafo.Add(new Phrase(new Chunk(plantilla.parrafoQuintoFiniquitoT().Replace("\n", ""), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                            cellParrafoQuintoFiniquito = new PdfPCell(fraseQuintoParrafo);
                            cellParrafoQuintoFiniquito.UseVariableBorders = true;
                            cellParrafoQuintoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cellParrafoQuintoFiniquito.BorderColor = BaseColor.WHITE;
                            tablaParrafoQuintoFiniquito.WidthPercentage = 100;
                            tablaParrafoQuintoFiniquito.AddCell(cellParrafoQuintoFiniquito);

                            document.Add(tablaParrafoQuintoFiniquito);
                            /** 3 */

                            document.Add(saltoDeLinea);

                            if (ccaf.Checked)
                            {
                                tablaParrafoSextoFiniquito = new PdfPTable(1);
                                cellParrafoSextoFiniquito = new PdfPCell(new Phrase(plantilla.parrafoSextoFiniquito(tipoFiniquito), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoSextoFiniquito.UseVariableBorders = true;
                                cellParrafoSextoFiniquito.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoSextoFiniquito.BorderColor = BaseColor.WHITE;
                                tablaParrafoSextoFiniquito.WidthPercentage = 100;
                                tablaParrafoSextoFiniquito.AddCell(cellParrafoSextoFiniquito);

                                document.Add(tablaParrafoSextoFiniquito);

                                tablaParrafoCopias = new PdfPTable(1);
                                cellParrafoCopias = new PdfPCell(new Phrase(plantilla.parrafoCopias(), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoCopias.UseVariableBorders = true;
                                cellParrafoCopias.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoCopias.BorderColor = BaseColor.WHITE;
                                tablaParrafoCopias.WidthPercentage = 100;
                                tablaParrafoCopias.AddCell(cellParrafoCopias);

                                document.Add(tablaParrafoCopias);
                            }
                            else {
                                tablaParrafoCopias = new PdfPTable(1);
                                cellParrafoCopias = new PdfPCell(new Phrase(plantilla.parrafoCopias(), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                                cellParrafoCopias.UseVariableBorders = true;
                                cellParrafoCopias.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cellParrafoCopias.BorderColor = BaseColor.WHITE;
                                tablaParrafoCopias.WidthPercentage = 100;
                                tablaParrafoCopias.AddCell(cellParrafoCopias);

                                document.Add(tablaParrafoCopias);

                                document.Add(saltoDeLinea);
                                document.Add(saltoDeLinea);
                                document.Add(saltoDeLinea);
                            }

                            document.Add(saltoDeLinea);
                            document.Add(saltoDeLinea);
                            document.Add(saltoDeLinea);

                            tabla3 = new PdfPTable(3);

                            firmaTrabajador = new PdfPCell(new Phrase(TextBox2.Text + "\n" + "C.I. " + TextBox6.Text.Replace(".", ""), FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));

                            firmaTrabajador.UseVariableBorders = true;
                            firmaTrabajador.BorderColorTop = BaseColor.BLACK;
                            firmaTrabajador.BorderColorLeft = BaseColor.WHITE;
                            firmaTrabajador.BorderColorRight = BaseColor.WHITE;
                            firmaTrabajador.BorderColorBottom = BaseColor.WHITE;
                            firmaTrabajador.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            firmaTrabajador.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                            firmaEspaciado = new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            firmaEspaciado.BorderColor = BaseColor.WHITE;

                            firmaEST = new PdfPCell(new Phrase("EST SERVICE LTDA." + "\n" + "RUT 76.760.090-9 ", FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            firmaEST.UseVariableBorders = true;
                            firmaEST.BorderColorTop = BaseColor.BLACK;
                            firmaEST.BorderColorLeft = BaseColor.WHITE;
                            firmaEST.BorderColorRight = BaseColor.WHITE;
                            firmaEST.BorderColorBottom = BaseColor.WHITE;
                            firmaEST.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER;
                            firmaEST.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                            tabla3.AddCell(firmaTrabajador);
                            tabla3.AddCell(firmaEspaciado);
                            tabla3.AddCell(firmaEST);

                            tabla3.WidthPercentage = 100;
                            document.Add(tabla3);

                            document.Add(saltoDeLinea);
                            document.Add(saltoDeLinea);

                            tablaFooter = new PdfPTable(1);
                            cellFooter = new PdfPCell(new Phrase(TextBox8.Text + "\n" + TextBox7.Text + "\n" + lblcentrocosto.Text, FontFactory.GetFont("Arial", fontSizeTexto, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)));
                            cellFooter.UseVariableBorders = true;
                            cellFooter.BorderColor = BaseColor.WHITE;
                            tablaFooter.WidthPercentage = 100;
                            tablaFooter.AddCell(cellFooter);

                            document.Add(tablaFooter);

                            break;
                    }

                    document.Close();

                    Response.ContentType = "application/pdf";
                    string file = "FiniquitoContrato_" + DateTime.Now.ToString().Replace("-", "").Replace(' ', '_').Replace(":", "") + ".pdf";
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file);
                    Response.BinaryWrite(stream.ToArray());
                    Response.End();
                    Response.Flush();
                    Response.Clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning',title: 'No se puede exportar!', html: '<label>Debe Seleccionar contratos para generar documento</label>'});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning',title: 'No se puede exportar!', html: '<label>Debe calcular para generar documento.</label>'});", true);
            }
        }
        
        public string replaceCharacter(string texto){
            return texto.Replace("&#225;", "á").Replace("&#193;", "Á").Replace("&#233;", "é").Replace("&#201;", "É").Replace("&#237;", "í").Replace("&#205;", "Í").Replace("&#243;", "ó").Replace("&#211;", "Ó").Replace("&#250", "ú").Replace("&#218;", "Ú");
        }

        #region "Modularización de codigo fuente"

        private string ModuleControlRetornoPlataformaTeamwork()
        {
            string domainReal = string.Empty;
            string domain = string.Empty;
            string prefixDomain = string.Empty;

            #region "CONTROL DE RETORNO"

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("181.190.6.196"))
                {
                    domainReal = "181.190.6.196";
                }
                else
                {
                    domainReal = "181.190.6.196";
                }

                domain = "http://" + domainReal;
                prefixDomain = "/AplicacionOperaciones/";
            }
            else
            {
                domain = "http://localhost:46903/";
                prefixDomain = "";
            }

            #endregion

            return domain + prefixDomain;

        }

        #endregion
    }
}
using FiniquitosV2.api;
using FiniquitosV2.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FiniquitosV2
{
    public partial class calculoBajaConsultora : System.Web.UI.Page
    {
        string ConexionVariable, Ficha1, MESULIQ;
        DataTable dt;
        int DiasTotal, DiasFaltante, SueldoParcial, Fin_Calculo, FlagDias = 0;
        DateTime MESPARTIDO;
        DataTable datatable;
        private static string strSql;
        private static DataSet ds;
        DataTable dtDescuento, dtOtrosHaberes, dtOtrosHaberes1;

        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region "Codigo que se carga al entrar a la pagina"
                
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

                    Label10.Text = Session["Usuario"].ToString();
                    Label12.Text = DateTime.Now.ToString();
                    //cargarUF();
                    CargacCausal();
                    CargaOtrosHaberes();
                    CargaDescuentos();
                    CargaDocumentos();
                }
                else
                {
                    Response.Redirect("Login");
                }

                if (Session["rut"] != null)
                {
                    buscarcontratos();
                    Session.Remove("rut");
                }

                sobregiro.Visible = false;
                segurcomplemt.Visible = false;
                afcinfo.Visible = false;
                clausulaccaf.Visible = false;
                licenciaACHS.Visible = false;
                comentariosOper.Visible = false;
                retencionjudicial.Visible = false;
                lucrocesante.Visible = false;

                if (descuento0.SelectedItem.ToString() == "Remuneracion Pendiente")
                {
                    remupendAdc.Visible = true;
                    LblRemunPendAdc.Visible = true;
                }
                else
                {
                    remupendAdc.Visible = false;
                    LblRemunPendAdc.Visible = false;
                }

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
                            selected_format_carta(sender, e);
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
                            Session["ApplicationIsPostBack"] = null;
                        }
                    }
                }

                #endregion

            }
        }

        protected void VerificarStatusSeguimiento_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = sender as CheckBox;
            GridViewRow row = chkSelect.NamingContainer as GridViewRow;
            CheckBox chk = (CheckBox)GridView1.Rows[row.RowIndex].Cells[0].FindControl("estatus");

            if (chk.Checked)
            {
                if ((string)GridView1.Rows[row.RowIndex].Cells[8].Text.Replace("&nbsp;", "") != "")
                {
                    chk.Checked = false;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'Atención!', text:'No se puede incluir debido a que ya esta registrado'});", true);
                }
            }

        }

        public void buscarcontratos()
        {
            try
            {
                Clases.Contrato contrato = new Clases.Contrato();
                contrato.rut = Session["rut"].ToString();
                contrato.validarPersonaExistente(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                if (contrato.Existe == "S")
                {
                    contrato.ContratoActivobaja(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                    TextBox6.Text = contrato.rut;
                    TextBox2.Text = contrato.nombres;

                    List<Clases.Contrato> con = Clases.Contrato.ListarFiniquitadosCONSULTORA(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), TextBox6.Text);
                    GridView1.DataSource = con;
                    GridView1.DataBind();
                    if (GridView1.DataSource != null)
                    {
                        string[] fechaContrato = GridView1.Rows[0].Cells[3].Text.Split('-');

                        if (DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]) <= DateTime.Now)
                        {
                            TextBox10.Text = GridView1.Rows[0].Cells[3].Text;
                        }
                    }

                    ddlContratos.Items.Add("Seleccione");
                    foreach (Clases.Contrato contratos in con)
                    {
                        ddlContratos.Items.Add(contratos.ficha);
                    }
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

        private void CargaOtrosHaberes()
        {
            Clases.OtrosHaberes OtrosHaberes = new Clases.OtrosHaberes();
            descuento0.DataSource = null;
            descuento0.DataSource = svcFiniquitos.GetOtrosHaberes().Table;
            descuento0.DataBind();

        }

        private void CargaDescuentos()
        {
            Descuento.DataSource = null;
            Descuento.DataSource = svcFiniquitos.GetListarDescuentos().Table;
            Descuento.DataBind();
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

        private void cargarUF(int month, int years)
        {
            float x90 = 0;
            float uff = 0;

            string mes = (month).ToString();
            string year = (years).ToString();

            string[] parametros = {
                mes,
                year
            };

            DataSet UFUltima = svcFiniquitos.GetUltimaUF(parametros).Table;
            
            foreach (DataRow rows in UFUltima.Tables[0].Rows)
            {
                uff = float.Parse(rows["VALORUF"].ToString());
            }

            if (Request.QueryString["event"].ToString() == "simulation" && uff == 0)
            {
                string[] parametrosMaxFecha = {
                    mes,
                    year,
                    "-"
                };

                DataSet UFMaxUltima = svcFiniquitos.GetUltimaUF(parametrosMaxFecha).Table;

                foreach (DataRow rows in UFMaxUltima.Tables[0].Rows)
                {
                    uff = float.Parse(rows["VALORUF"].ToString());
                }
            }
            
            Label14.Text = uff.ToString("N2");

            x90 = float.Parse(Label14.Text) * 90;
            Label16.Text = x90.ToString("N1");
            
            if (uff == 0)
            {
                if (Request.QueryString["event"].ToString() != "simulation")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$('#modalUFnoDisponible').modal('show');", true);
                }
            }

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

        private void CargaDocumentos()
        {
            if (!IsPostBack)
            {
                GridView3.DataSource = null;
                GridView3.DataSource = Clases.FNDocumentos.Listar(Properties.Settings.Default.connectionString);
                GridView3.DataBind();
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextBox6.Text != "")
                {
                    TextBox6.BackColor = Color.White;
                    //gvFiniquitos.DataSource = null;
                    //gvFiniquitos.DataSource = Clases.Finiquitos.ObtenerFiniquito(Properties.Settings.Default.connectionString, TextBox6.Text);
                    //gvFiniquitos.DataBind();

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

        private void Vacaciones(DateTime Fecha1, DateTime Fecha2)//, DateTime UltimoDia
        {

            float TotalDiasFinq = 0;
            Clases.FechaContable fechas = new Clases.FechaContable();
            double diasfebrero = 0;
            double cantidadDias = 0;

            if (fechaIngresoIAS.Text == fechaInicioContratoV.Text)
            {
                fechas.Fechainicio = Fecha1;
            }
            else
            {
                fechas.Fechainicio = Fecha1;
            }

            fechas.FechaFinal = Fecha2;
            fechas.calculofecha1();
            años.Text = fechas.años.ToString();
            meses.Text = fechas.meses.ToString();

            int sumaMonth = 0;
            int sumaDias = 0;
            int constSuma = 0;

            while (fechas.Fechainicio.AddMonths(sumaMonth) <= fechas.FechaFinal)
            {
                sumaMonth = sumaMonth + 1;
            }

            while (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias) <= fechas.FechaFinal)
            {
                if (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Day < 31)
                {
                    constSuma = 1;

                    if (fechas.FechaFinal.Month == 3)
                    {
                        if (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 28 &&
                            fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Month == 2 &&
                            DateTime.Parse(fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Year + "-03-01").AddDays(-1).Day == 28)
                        {
                            diasfebrero = 2;
                        }
                        else if (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 29 &&
                                 fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Month == 2 &&
                                 DateTime.Parse(fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Year + "-03-01").AddDays(-1).Day == 29)
                        {
                            diasfebrero = 1;
                        }
                    }

                }
                else if (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias).Day == 31)
                {
                    if (fechas.Fechainicio.AddMonths(sumaMonth - 1).AddDays(sumaDias) == fechas.FechaFinal)
                    {
                        constSuma = 1;
                    }
                }

                cantidadDias = cantidadDias + constSuma + diasfebrero;

                diasfebrero = 0;
                constSuma = 0;

                sumaDias = sumaDias + 1;
            }

            dias.Text = (cantidadDias - 1).ToString();

            if (yearIASFI.Text == "" && mesIASFI.Text == "" && diaIASFI.Text == "")
            {
                yearIASFI.Text = años.Text;
                mesIASFI.Text = meses.Text;
                diaIASFI.Text = dias.Text;
            }

            lbltrabajador.Text = (((fechas.años * 12) * 30) + (fechas.meses * 30) + fechas.dias).ToString();

            if (DropDownList3.SelectedItem.Value == "Si")
            {
                lblañostrabajados.Text = ((int.Parse(años.Text) * 20)).ToString();
                lblmesestrabajados.Text = (int.Parse(meses.Text) * 1.667).ToString();
                lbldiastrabajados.Text = (Math.Round((double.Parse(dias.Text) / 30) * 1.667, 2)).ToString();
            }
            else if (DropDownList3.SelectedItem.Value == "No")
            {
                lblañostrabajados.Text = ((int.Parse(años.Text) * 15)).ToString();
                lblmesestrabajados.Text = (Math.Round(double.Parse(meses.Text) * ((15 + float.Parse(Label30.Text)) / 12), 3, MidpointRounding.ToEven)).ToString();

                lbldiastrabajados.Text = (Math.Round((float.Parse(dias.Text) / 30) * ((15 + float.Parse(Label30.Text)) / 12), 3, MidpointRounding.ToEven)).ToString();
            }

            saldo.Text = (Math.Round((double.Parse(lblañostrabajados.Text) + double.Parse(lblmesestrabajados.Text) + double.Parse(lbldiastrabajados.Text)) + float.Parse(Label29.Text), 2)).ToString();

            Clases.Vacaciones vacaciones = new Clases.Vacaciones();
            vacaciones.ficha = Ficha1;
            vacaciones.fechaDesvinculacion = TextBox10.Text;
            vacaciones.ObtenerVacaciones(Properties.Settings.Default.connectionStringTEAMWORK);
            vacacionestomadas.Text = vacaciones.DiasVacaciones.ToString();
            habiles.Text = (float.Parse(saldo.Text) - float.Parse(vacacionestomadas.Text)).ToString("n2");
            fechamasuno.Text = Fecha2.AddDays(1).ToString();
            int totalDiasVacaciones = Clases.Utilidades.TotaldiasVacaciones(DateTime.Parse(fechamasuno.Text), double.Parse(habiles.Text), TextBox1.Text);
            lblhabiles.Text = lblsaldo.Text;
            lblinhabiles.Text = totalDiasVacaciones.ToString();
            inhabiles.Text = lblinhabiles.Text;
            lblcorridos.Text = (double.Parse(lblhabiles.Text) + double.Parse(lblinhabiles.Text)).ToString();
            corridos.Text = (float.Parse(inhabiles.Text) + float.Parse(habiles.Text)).ToString("n2");

        }

        private int Causal161()
        {
            if (CausalDespido.SelectedItem.ToString() == "Art.161Inciso1   |   Necesidades de la empresa")
            {
                if (int.Parse(años.Text) > 0)
                {
                    if (AFC.Text != "")
                    {

                        return 1;
                    }
                    else
                    {
                        Response.Write("<script>alert('Debe Ingresar Descuento de AFC')</script>");
                        return -1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }

        }
        
        public void validames(DateTime fechainicio)
        {
            int mes, Anio, dias;
            dias = fechainicio.Day;
            mes = fechainicio.Month;
            Anio = fechainicio.Year;
            if (dias > 2)
            {
                MESPARTIDO = DateTime.Parse("01/" + mes.ToString() + "/" + Anio.ToString());
            }
            else
            {
                MESPARTIDO = DateTime.Parse("01-01-0001");
            }
        }

        private void UltimaLiquidacion(string connectionString, string ficha)
        {
            int Mes = 0, j, MES = 0, Dia, diasenmes, AÑO;
            string MES1;
            //DateTime fecha1, fecha2;
            //fecha1

            j = 2;

            string[] fechaContrato = TextBox10.Text.Split('-');

            MES = int.Parse(DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]).Month.ToString());
            Dia = int.Parse(DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]).Day.ToString());
            AÑO = int.Parse(DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]).Year.ToString());
            diasenmes = DateTime.DaysInMonth(AÑO, MES);
            ////if ( diasenmes < Dia)
            ////{
            ////    MES++;
            ////}

            if ((Dia == 28) && (MES == 2))
            {
                Mes++;
            }

            if (Dia == 30)
            {
                switch (MES)
                {
                    case 4:
                        MES++;
                        break;
                    case 6:
                        MES++;
                        break;
                    case 9:
                        MES++;
                        break;
                    case 11:
                        MES++;
                        break;

                }

            }
            if (Dia == 31)
            {
                MES++;
                if (MES > 12)
                {
                    MES = 1;
                    AÑO++;

                }
            }
            if (MES < 10)
            {
                MES1 = "0" + MES.ToString();
            }
            else { MES1 = MES.ToString(); }
            //string fecha = MES1.ToString() + "/01/" + AÑO.ToString();
            string fecha = string.Empty;
            string fechaInicioPeriodos = string.Empty;

            /** VALIDACION DE MESES COMPLETOS - 30 DIAS. */
            string[] fechaInit = { };
            string[] fechaTerm = { };
            List<string> fechasCompletas = new List<string>();
            List<string> fechasDiasTotales = new List<string>();
            foreach (GridViewRow rowItem in GridView1.Rows)
            {
                int contadorSeleccionados = 0;
                CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                if (chk.Checked)
                {
                    if (contadorSeleccionados == 0)
                    {
                        fechaInit = rowItem.Cells[2].Text.Split('-');
                        fechaTerm = rowItem.Cells[3].Text.Split('-');

                        contadorSeleccionados = contadorSeleccionados + 1;
                    }
                }
            }

            if (fechaTerm[0] + "-" + fechaTerm[1] + "-" + fechaTerm[2] == "01-01-0001")
            {
                fechaTerm = TextBox10.Text.Split('-');
                fecha = fechaTerm[2] + "-" + fechaTerm[1] + "-" + fechaTerm[0];
                fechaInicioPeriodos = fechaInit[2] + "-" + fechaInit[1] + "-" + fechaInit[0];
            }
            else
            {
                fecha = fechaTerm[2] + "-" + fechaTerm[1] + "-" + fechaTerm[0];
                fechaInicioPeriodos = fechaInit[2] + "-" + fechaInit[1] + "-" + fechaInit[0];
            }

            DateTime dateInit = new DateTime(Convert.ToInt32(fechaInit[2]), Convert.ToInt32(fechaInit[1]), Convert.ToInt32(fechaInit[0]));
            DateTime dateTerm = new DateTime(Convert.ToInt32(fechaTerm[2]), Convert.ToInt32(fechaTerm[1]), Convert.ToInt32(fechaTerm[0]));

            int mesesDiff = Math.Abs(((dateInit.Month - dateTerm.Month) + 12 * (dateInit.Year - dateTerm.Year)));

            if (mesesDiff > 0)
            {
                if (dateTerm.Month != 2)
                {
                    if (dateTerm.Day >= DateTime.DaysInMonth(dateTerm.Year, dateTerm.Month))
                    {
                        mesesDiff = mesesDiff + 1;
                    }
                }
                else
                {
                    if (dateTerm.Day >= 28)
                    {
                        mesesDiff = mesesDiff + 1;
                    }
                }
            }
            else
            {
                if (dateInit.Day == 1 && dateTerm.Day >= DateTime.DaysInMonth(dateTerm.Year, dateTerm.Month))
                {
                    mesesDiff = mesesDiff + 1;
                }
            }



            int mesSum = 0;
            int yearSum = 0;
            string mesArray = "";
            string yearArray = "";

            for (var x = 0; x < mesesDiff; x++)
            {
                if (x == 0)
                {
                    if (Convert.ToInt32(fechaInit[1]) <= 12)
                    {
                        mesSum = Convert.ToInt32(fechaInit[1]);
                        yearSum = Convert.ToInt32(fechaInit[2]);
                    }
                    else if (Convert.ToInt32(fechaInit[1]) > 12)
                    {
                        mesSum = 1;
                        yearSum = Convert.ToInt32(fechaInit[2]) + 1;
                    }

                    yearArray = yearSum.ToString();
                }
                else
                {
                    if (mesSum < 12)
                    {
                        mesSum = mesSum + 1;
                    }
                    else
                    {
                        mesSum = 1;
                        yearArray = (Convert.ToInt32(yearArray) + 1).ToString();
                    }
                }

                mesArray = (mesSum).ToString();


                /** METODO ANTIGUO EN VALIDACION */

                if (mesesDiff == 0)
                {
                    TimeSpan ts = dateInit - dateTerm;

                    if (mesSum != 2)
                    {
                        if (ts.Days + 1 >= 30)
                        {
                            fechasCompletas.Add(mesArray + '-' + yearArray);
                        }
                    }
                    else if (mesSum == 2)
                    {
                        if (ts.Days + 1 >= 28 && ts.Days + 1 <= 29)
                        {
                            fechasCompletas.Add(mesArray + '-' + yearArray);
                        }
                    }

                }
                else
                {
                    if (mesesDiff == 1)
                    {
                        TimeSpan tsIni = Convert.ToDateTime(dateInit.Month.ToString() + "-" + DateTime.DaysInMonth(dateInit.Year, dateInit.Month).ToString() +
                                         "-" + dateInit.Year.ToString()) -
                                         Convert.ToDateTime(dateInit.Month.ToString() + "-" + dateInit.Day.ToString() + "-" + dateInit.Year.ToString());
                        TimeSpan tsFin = Convert.ToDateTime(dateTerm.Month.ToString() + "-" + dateTerm.Day.ToString() + "-" + dateTerm.Year.ToString()) -
                                         Convert.ToDateTime(dateTerm.Month.ToString() + "-01-" + dateTerm.Year.ToString());

                        if (mesSum != 2)
                        {
                            if (tsIni.Days + 1 >= 30)
                            {
                                fechasCompletas.Add(dateInit.Month.ToString() + '-' + dateInit.Year.ToString());
                            }

                            if (tsFin.Days + 1 >= 30)
                            {
                                fechasCompletas.Add(dateTerm.Month.ToString() + '-' + dateTerm.Year.ToString());
                            }
                        }
                        else if (mesSum == 2)
                        {


                            if (tsIni.Days + 1 >= 28 && tsIni.Days + 1 <= 29)
                            {
                                fechasCompletas.Add(dateInit.Month.ToString() + '-' + dateInit.Year.ToString());
                            }

                            if (tsFin.Days + 1 >= 28 && tsFin.Days + 1 <= 29)
                            {
                                fechasCompletas.Add(dateTerm.Month.ToString() + '-' + dateTerm.Year.ToString());
                            }
                        }

                    }
                    else if (mesesDiff > 1)
                    {
                        TimeSpan tsIni = Convert.ToDateTime(dateInit.Month.ToString() + "-" + DateTime.DaysInMonth(dateInit.Year, dateInit.Month).ToString() +
                                         "-" + dateInit.Year.ToString()) -
                                         Convert.ToDateTime(dateInit.Month.ToString() + "-" + dateInit.Day.ToString() + "-" + dateInit.Year.ToString());
                        TimeSpan tsFin = Convert.ToDateTime(dateTerm.Month.ToString() + "-" + dateTerm.Day.ToString() + "-" + dateTerm.Year.ToString()) -
                                         Convert.ToDateTime(dateTerm.Month.ToString() + "-01-" + dateTerm.Year.ToString());

                        if (x == 0)
                        {
                            if (mesSum != 2)
                            {
                                if (tsIni.Days + 1 >= 30)
                                {
                                    fechasCompletas.Add(dateInit.Month.ToString() + '-' + dateInit.Year.ToString());
                                }
                            }
                            else if (mesSum == 2)
                            {
                                if (tsIni.Days + 1 >= 28 && tsIni.Days + 1 <= 29)
                                {
                                    fechasCompletas.Add(dateInit.Month.ToString() + '-' + dateInit.Year.ToString());
                                }
                            }

                        }
                        else if ((x + 1) == mesesDiff + 1)
                        {
                            if (mesSum != 2)
                            {
                                if (tsFin.Days + 1 >= 30)
                                {
                                    fechasCompletas.Add(dateTerm.Month.ToString() + '-' + dateTerm.Year.ToString());
                                }
                            }
                            else if (mesSum == 2)
                            {
                                if (tsFin.Days + 1 >= 28 && tsFin.Days + 1 <= 29)
                                {
                                    fechasCompletas.Add(dateTerm.Month.ToString() + '-' + dateTerm.Year.ToString());
                                }
                            }

                        }
                        else
                        {
                            fechasCompletas.Add(mesArray + '-' + yearArray);
                        }
                    }
                }

            }


            strSql = string.Format("SELECT     softland.sw_variablepersona.mes, softland.sw_variablepersona.ficha, CAST(DATEPART(DAY, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes) AS VARCHAR(50)) + '-' + CAST(DATEPART(MONTH, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes) AS VARCHAR(50)) + '-' + CAST(DATEPART(YEAR, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes) AS VARCHAR(50)) + ' 12:00:00.000'" +
                     " 'FechaMes' FROM softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                    " WHERE     (softland.sw_variablepersona.ficha = '{0}') AND " +
                    " (CAST(softland.sw_vsnpRetornaFechaMesExistentes.FechaMes AS DATE) >= CAST('{2}' AS date)) AND " +
                    " (CAST(softland.sw_vsnpRetornaFechaMesExistentes.FechaMes AS DATE) <= CASE WHEN CAST('{1}' AS date) = CASE WHEN DATEPART(DAY, EOMONTH(CAST('{1}' AS DATE))) > 30 THEN DATEADD(DAY, -1, EOMONTH(CAST('{1}' AS DATE))) ELSE EOMONTH(CAST('{1}' AS DATE)) END THEN  CAST('{1}' AS DATE) ELSE DATEADD(MONTH, -1, CAST('{1}' AS DATE))  END) " +
                    " GROUP BY softland.sw_variablepersona.ficha, softland.sw_variablepersona.mes, softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                    " ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC", ficha, fecha, fechaInicioPeriodos);

            ds = Clases.Interface.ExecuteDataSet(Properties.Settings.Default.connectionStringTEAMWORK, strSql);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            /** END VALIDACION DE MESES COMPLETOS */

                            #region "CREACION DE VARIABLES REMUNERACIONES"

                            string mesSeteado = dr["FechaMes"].ToString().Split('-')[1];
                            bool completoMes = false;

                            for (var l = 0; l < fechasCompletas.Count; l++)
                            {
                                if (fechasCompletas[l] == mesSeteado + "-" + (Convert.ToDateTime(dr["FechaMes"].ToString()).Year).ToString())
                                {
                                    completoMes = true;
                                }
                            }

                            if (completoMes)
                            {
                                Mes = int.Parse(dr["mes"].ToString());
                                Clases.Licencia licencia = new Clases.Licencia();
                                Clases.Suspension suspension = new Clases.Suspension();

                                string[] fechaConsulta = dr["FechaMes"].ToString().Split(' ')[0].Split('-');

                                licencia.Fechaconsulta = fechaConsulta[2] + "-" + fechaConsulta[1] + "-" + fechaConsulta[0]; ;
                                licencia.ficha = ficha;
                                if (!licencia.ObtenerLicenia(Properties.Settings.Default.connectionStringTEAMWORK) && (j < 5))
                                {
                                    suspension.fecha = fechaConsulta[2] + "-" + fechaConsulta[1] + "-" + fechaConsulta[0];
                                    suspension.ficha = ficha;
                                    if (!suspension.ObtenerSuspension(Properties.Settings.Default.connectionStringTEAMRRHH, "TEAMWORK"))
                                    {
                                        creadt(Properties.Settings.Default.connectionStringTEAMWORK, ficha, Mes, j);
                                        if (MESULIQ == "")
                                        {
                                            MESULIQ = Mes.ToString();
                                        }
                                        j++;
                                    }
                                }
                            }
                            #endregion
                        }

                    }
                }
            }

        }

        private void creadt(string connectionString, string ficha, int Mes, int j)
        {
            int fila = 1, DiasRestantes = 0, TotalColumna = 0, DIASMESCORTO = 0;
            Clases.Remuneracion rem = new Clases.Remuneracion();
            List<Clases.VariableRemuneracion> prueba = new List<Clases.VariableRemuneracion>();
            prueba = rem.ListarCONSULTORA(Properties.Settings.Default.connectionString, ficha, Mes);
            if (dt.Columns.Count < 1)
            {
                dt.Columns.Add("descripcion", Type.GetType("System.String"));
                dt.Columns.Add("valor", Type.GetType("System.String"));

                dt.Rows.Add("Haber", prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString());
                //GridView2.Columns[0].HeaderText = "Haber";
                //GridView2.Columns[1].HeaderText = prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString();
            }
            else
            {
                dt.Columns.Add(string.Format("valor{0}", j), Type.GetType("System.String"));
                dt.Rows.Add("");
                dt.Rows[0]["valor" + j] = prueba[0].fechames.Month.ToString() + "-" + prueba[0].fechames.Year.ToString();
                fila = 1;
            }
            foreach (Clases.VariableRemuneracion nue in prueba)
            {
                double SueldoParcial = 0;
                if (nue.valor != null)
                {
                    SueldoParcial = double.Parse(nue.valor);
                }
                if (nue.descripcion == "DIAS TRABAJADOS")
                {
                    if ((DiasTotal + int.Parse(nue.valor)) < 90)
                    {
                        SueldoParcial = int.Parse(nue.valor);
                        DiasTotal = DiasTotal + int.Parse(nue.valor);
                    }
                    else
                    {
                        if (((DiasTotal + int.Parse(nue.valor)) >= 90) && (FlagDias == 0))
                        {
                            DIASMESCORTO = int.Parse(nue.valor);
                            DiasRestantes = 90 - DiasTotal;
                            SueldoParcial = DiasRestantes;
                            FlagDias = 1;
                            DiasTotal = DiasTotal + DIASMESCORTO;
                        }
                    }
                }
                if ((FlagDias == 1) && (nue.valor != null) && (nue.descripcion != "DIAS TRABAJADOS"))
                {
                    SueldoParcial = (int.Parse(nue.valor) / DIASMESCORTO) * DiasRestantes;
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
                if (dt.Columns.Count < 3)
                {
                    if ((descri != null) || (SueldoParcial != null))
                    {
                        dt.Rows.Add(descri, SueldoParcial);
                    }
                }
                else
                {
                    dt.Rows[fila]["valor" + j] = SueldoParcial;
                    fila++;
                }
            }
            if (dt.Columns.Count < 3)
            {
                dt.Rows.Add("Total Remuneración", TotalColumna);
            }
            else
            {
                dt.Rows[fila]["valor" + j] = TotalColumna;
                fila++;
            }
            lblbono2.Text = (int.Parse(lblbono2.Text) + TotalColumna).ToString();
        }

        protected void Calcular_Click(object sender, EventArgs e)
        {
            reiniciarValores();
            int cantidadFichas = 0;
            ServicioFiniquitos.ServicioFiniquitosClient servicioFiniquitos = new FiniquitosV2.ServicioFiniquitos.ServicioFiniquitosClient();

            string[] fechaTerminoUF = TextBox10.Text.Split('-');
            
            cargarUF(Convert.ToInt32(fechaTerminoUF[1]), Convert.ToInt32(fechaTerminoUF[2]));

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

            paramAlertas[0] = "@FICHA";
            paramAlertas[1] = "@EMPRESA";
            paramAlertas[2] = "@CONCEPTO";

            valAlertas[0] = fichaInformativos;
            valAlertas[1] = "TEAMWORK";
            valAlertas[2] = "SOBREGIRO";

            dataAlertas = svcFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

            foreach (DataRow rows in dataAlertas.Tables[0].Rows)
            {
                if (rows["Code"].ToString() == "200")
                {
                    sobregiro.Visible = true;
                }
                else
                {
                    sobregiro.Visible = false;
                }
            }

            paramAlertas[0] = "@FICHA";
            paramAlertas[1] = "@EMPRESA";
            paramAlertas[2] = "@CONCEPTO";

            valAlertas[0] = fichaInformativos;
            valAlertas[1] = "TEAMWORK";
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
            valAlertas[1] = "TEAMWORK";
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
            valAlertas[1] = "TEAMWORK";
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
                    cantidadFichas = cantidadFichas + 1;

                }
            }

            string[] fichas = new string[cantidadFichas];
            int DiasParavacaciones = 0, TotalDiasFinq = 0, EXTC = 0;
            //Fin_Calculo = 0;
            MESULIQ = "";
            lblsueldobase.Text = "0";
            lblbonovariable.Text = "0";
            DateTime fechainicio = DateTime.Parse("01/01/2069");
            GridView2.DataSource = null;
            bool finiquitoCostoCero = true;
            //DateTime fechaultimodia;
            //fechaultimodia = DateTime.Parse("01-01-1001");
            if (CausalDespido.SelectedItem.Text != "Seleccione   |   Causal")
            {
                if (TextBox10.Text != "")
                {
                    DataSet dataExcpIas;
                    DataSet dataIngreso;

                    string[] paramExcpIas = new string[1];
                    string[] valExcpIas = new string[1];
                    string[] paramIngreso = new string[3];
                    string[] valIngreso = new string[3];

                    paramExcpIas[0] = "@CODIGO";
                    valExcpIas[0] = TextBox5.Text;

                    dataExcpIas = svcFiniquitos.GetObtenerExcepcionCalculoIAS(paramExcpIas, valExcpIas).Table;

                    /** EXCEPCION DE FECHA DE INGRESO PARA CALCULO IAS */
                    string fechaRound = string.Empty;


                    foreach (GridViewRow rowItem in GridView1.Rows)
                    {
                        CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                        if (chk.Checked)
                        {
                            fechaRound = rowItem.Cells[2].Text;

                            //TimeSpan dias = DateTime.Parse(rowItem.Cells[3].Text != "01-01-1001" ? rowItem.Cells[3].Text : TextBox10.Text) - DateTime.Parse(rowItem.Cells[2].Text);

                            //diasFiniquitoReales = diasFiniquitoReales + dias.Days;
                        }
                    }

                    if (fechaIngresoIAS.Text == "" && fechaInicioContratoV.Text == "")
                    {
                        foreach (DataRow rows in dataExcpIas.Tables[0].Rows)
                        {
                            switch (rows["Code"].ToString())
                            {
                                case "200":
                                    paramIngreso[0] = "@RUT";
                                    paramIngreso[1] = "@FICHA";
                                    paramIngreso[2] = "@DATABASE";

                                    valIngreso[0] = TextBox6.Text;
                                    valIngreso[1] = ddlContratos.SelectedValue;
                                    valIngreso[2] = "TEAMWORK";

                                    dataIngreso = svcFiniquitos.GetObtenerFechaIngreso(paramIngreso, valIngreso).Table;

                                    foreach (DataRow rowsIngreso in dataIngreso.Tables[0].Rows)
                                    {
                                        fechaInicioContratoV.Text = rowsIngreso["DiaFCV"].ToString() + "-" + rowsIngreso["MesFCV"].ToString() + "-" + rowsIngreso["YearFCV"].ToString();
                                        fechaIngresoIAS.Text = rowsIngreso["DiaFI"].ToString() + "-" + rowsIngreso["MesFI"].ToString() + "-" + rowsIngreso["YearFI"].ToString();

                                    }

                                    break;
                                default:
                                    fechaIngresoIAS.Text = fechaRound;
                                    fechaInicioContratoV.Text = fechaRound;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        fechaIngresoIAS.Text = fechaRound;
                        fechaInicioContratoV.Text = fechaRound;
                    }

                    string[] fechInicio;

                    if (fechaIngresoIAS.Text != fechaInicioContratoV.Text)
                    {
                        fechInicio = fechaIngresoIAS.Text.Split('-');
                    }
                    else
                    {
                        fechInicio = fechaInicioContratoV.Text.Split('-');
                    }

                    string[] fechaTermino = TextBox10.Text.Split('-');

                    /** CALCULO PARA DIAS PROGRESIVOS */
                    string[] paramProgresivos = new string[3];
                    string[] valProgresivos = new string[3];

                    paramProgresivos[0] = "@FICHA";
                    paramProgresivos[1] = "@RUT";
                    paramProgresivos[2] = "@DATABASE";

                    valProgresivos[0] = ddlContratos.SelectedItem.ToString();
                    valProgresivos[1] = TextBox6.Text;
                    valProgresivos[2] = "TEAMWORK";

                    DataSet dataProgresivo = svcFiniquitos.GetObtenerProgresivos(paramProgresivos, valProgresivos).Table;

                    foreach (DataRow rows in dataProgresivo.Tables[0].Rows)
                    {
                        Label29.Text = rows["Progresivo"].ToString();
                        Label30.Text = rows["DiaGanado"].ToString();
                    }

                    Vacaciones(DateTime.Parse(fechInicio[1] + "/" + fechInicio[0] + "/" + fechInicio[2]), DateTime.Parse(fechaTermino[1] + "/" + fechaTermino[0] + "/" + fechaTermino[2]));

                    if (Causal161() == 1)
                    {
                        GridView2.DataSource = null;
                        dt = new DataTable();
                        int i = 0;
                        AFC.BackColor = Color.White;
                        //DiasTotal = 0;
                        foreach (GridViewRow rowItem in GridView1.Rows)
                        {
                            CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                            if (chk.Checked)
                            {
                                EXTC++;
                                //TotalDiasFinq = TotalDiasFinq + int.Parse(rowItem.Cells[3].Text);
                                fichas[i] = rowItem.Cells[1].Text;
                                Ficha1 = rowItem.Cells[1].Text;

                                /** CARGA LOS HABERES QUE PREVIAMENTE FUERON CARGADOS, SIEMPRE Y CUANDO HAYAN REGISTROS */
                                if (Session["datos2"] != null)
                                {
                                    DataTable dt2 = Session["datos2"] as DataTable;
                                    GridView5.DataSource = dt2;
                                    GridView5.DataBind();
                                    Session["datos2"] = dt2;

                                    foreach (GridViewRow rowItem2 in GridView5.Rows)
                                    {
                                        TotalDescuento0.Text = Convert.ToString(Convert.ToInt32(TotalDescuento0.Text) + Convert.ToInt32(rowItem2.Cells[2].Text));
                                    }
                                }

                                /** CARGA LOS DESCUENTOS QUE PREVIAMENTE FUERON CARGADOS, SIEMPRE Y CUANDO HAYAN REGISTROS */
                                if (Session["datos"] != null)
                                {
                                    DataTable dt1 = Session["datos"] as DataTable;
                                    GridView4.DataSource = dt1;
                                    GridView4.DataBind();
                                    Session["datos"] = dt1;

                                    foreach (GridViewRow rowItem3 in GridView4.Rows)
                                    {
                                        TotalDescuento.Text = Convert.ToString(Convert.ToInt32(TotalDescuento.Text) + Convert.ToInt32(rowItem3.Cells[2].Text));
                                    }
                                }
                                string[] fechaContrato;
                                if (fechaInicioContratoV.Text == fechaIngresoIAS.Text)
                                {
                                    fechaContrato = fechaInicioContratoV.Text.Split('-');
                                }
                                else
                                {
                                    fechaContrato = fechaIngresoIAS.Text.Split('-');
                                }


                                if (DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]) < fechainicio)
                                {
                                    fechainicio = DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]);
                                }
                                //TextBox12.Text = fechainicio.ToString();
                                validames(DateTime.Parse(fechaContrato[1] + "/" + fechaContrato[0] + "/" + fechaContrato[2]));
                                UltimaLiquidacion(string.Format("Data Source=SSTW//SQL2014;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), fichas[i].ToString());
                                if (txtBonorezagado.Text != "")
                                {
                                    lblbonovariable.Text = ((int.Parse(lblbono2.Text) + int.Parse(txtBonorezagado.Text)) / (dt.Columns.Count - 1)).ToString();
                                }
                                else
                                {
                                    txtBonorezagado.Text = "0";
                                    lblbonovariable.Text = ((int.Parse(lblbono2.Text) + int.Parse(txtBonorezagado.Text)) / (dt.Columns.Count - 1)).ToString();
                                }
                                creadtsueldo(string.Format(Properties.Settings.Default.connectionStringTEAMWORK), fichas[i].ToString());
                                // DiasParavacaciones = int.Parse(rowItem.Cells[3].Text) + DiasParavacaciones;
                                i++;
                            }
                        }
                        if (EXTC == 0)
                        {
                            Response.Write("<script>alert('Debe seleccionar un contrato')</script>");
                            return;
                        }
                        //sumarSueldo();

                        //Label10.Text = TotalDiasFinq.ToString();
                        if (fechainicio < DateTime.Parse(fechaTermino[1] + "-" + fechaTermino[0] + "-" + fechaTermino[2]))
                        {
                            Vacaciones(fechainicio, DateTime.Parse(fechaTermino[1] + "-" + fechaTermino[0] + "-" + fechaTermino[2]));// fechaultimodia
                        }
                        if (dt.Rows.Count > 1)
                        {
                            CargaVarible(Properties.Settings.Default.connectionString);
                            InvierteDT();
                            GridView2.DataSource = dt;
                        }

                        DtToGRD();
                        //GridView3.DataSource = datatable;
                        //invierteGrid();
                        GridView2.DataBind();

                        if (int.Parse(años.Text) > 0)
                        {
                            finiquitoCostoCero = false;
                        }
                        else
                        {
                            switch (int.Parse(meses.Text))
                            {
                                case 0:
                                    finiquitoCostoCero = true;
                                    break;
                                case 1:
                                    if (int.Parse(dias.Text) > 1)
                                    {
                                        finiquitoCostoCero = false;
                                    }
                                    break;
                                default:
                                    finiquitoCostoCero = false;
                                    break;
                            }
                        }

                        if (excpcostocero.Checked)
                        {
                            finiquitoCostoCero = false;
                        }

                        if (!finiquitoCostoCero)
                        {
                            lblvacafinal.Text = Math.Round(float.Parse(corridos.Text) * (float.Parse(lblbasevacaciones.Text) / 30)).ToString();

                        }

                        //GridView3.DataBind();
                        if ((CausalDespido.SelectedValue.ToString() == "Art.161Inciso1 | Necesidades de la empresa") && (DropDownList4.SelectedItem.Text == "SI"))
                        {
                            lblmesaviso.Text = lblbasecalculo.Text;
                        }
                        if ((CausalDespido.SelectedValue.ToString() == "Art.161Inciso1 | Necesidades de la empresa"))
                        {
                            int iasInt = 0;
                            iasInt = Indeminzacion();
                            if (IASWithFechaIngreso.Text == "")
                            {
                                if (int.Parse(años.Text) > 0)
                                {
                                    ias.Text = (iasInt * int.Parse(lblbasecalculo.Text)).ToString();
                                }
                                else
                                {
                                    ias.Text = "0";
                                }
                                
                                if (fechaIngresoIAS.Text != fechaInicioContratoV.Text)
                                {
                                    IASWithFechaIngreso.Text = (iasInt * int.Parse(lblbasecalculo.Text)).ToString();
                                }
                            }
                            else
                            {
                                ias.Text = IASWithFechaIngreso.Text;
                            }

                            lblYearServicio.Text = iasInt.ToString();

                            if (YearRealIASFI.Text != "")
                            {
                                if (YearRealIASFI.Text == lblYearServicio.Text)
                                {
                                    lblYearServicio.Text = iasInt.ToString();
                                }
                                else
                                {
                                    lblYearServicio.Text = YearRealIASFI.Text;
                                }
                            }

                            if (YearRealIASFI.Text == "")
                            {
                                YearRealIASFI.Text = lblYearServicio.Text;
                            }

                            if (iasInt > 0)
                            {
                                afcinfo.Visible = true;
                            }
                            else
                            {
                                afcinfo.Visible = false;
                            }
                        }
                        if (AFC.Text.Equals(""))
                        {
                            AFC.Text = (0).ToString();
                            lbltotaldescuento.Text = int.Parse(AFC.Text).ToString();// + int.Parse(TotalDescuento.Text)).ToString();
                        }
                        else
                        {
                            lbltotaldescuento.Text = int.Parse(AFC.Text).ToString();
                        }
                        SumasTotales();
                        if (lbltotalpagos.Text != "0.0")
                        {
                            lbltotalfiniquito.Text = (float.Parse(lbltotalpagos.Text) - Convert.ToInt32(lbltotaldescuento.Text)).ToString("N0");
                            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ " + Convert.ToInt32(lbltotalpagos.Text.Replace(",", "")).ToString("N0");
                        }
                        else
                        {
                            lbltotalfiniquito.Text = "0";
                            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ 0";
                        }

                        if (chkIndemVol.Checked)
                        {
                            lblindemnizacionvoluntaria.Text = (Convert.ToInt32(lblindemnizacionvoluntaria.Text) + Convert.ToInt32(montoIndeminizacionVol.Text)).ToString("N0");
                            lbltotalpagos.Text = (Convert.ToInt32(lbltotalpagos.Text.Replace(",", "")) + Convert.ToInt32(lblindemnizacionvoluntaria.Text.Replace(",", ""))).ToString("N0");
                            lbltotalfiniquito.Text = Convert.ToInt32(lbltotalpagos.Text.Replace(",", "")).ToString("N0");
                            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ " + Convert.ToInt32(lbltotalpagos.Text.Replace(",", "")).ToString("N0");
                        }

                        if (lbltotalfiniquito.Text.Contains("-") && Convert.ToInt32(lblotroshaberes.Text) == 0)
                        {
                            lbltotalfiniquito.Text = "0";
                            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ 0";
                        }

                        MethodServiceSoftland softland = new MethodServiceSoftland();
                        softland.FICHA = ddlContratos.SelectedItem.ToString();
                        foreach (DataRow rows in softland.GetRetencionJudicialOUTService.Tables[0].Rows)
                        {
                            if (softland.GetRetencionJudicialOUTService.Tables[0].Rows.Count > 0)
                            {
                                lblRetencionJudicial.Text = rows["RETENCION"].ToString() + " $ " + Convert.ToInt32(rows["VALOR"]).ToString("N0");
                            }
                        }
                        
                        if (fechaInicioContratoV.Text != fechaIngresoIAS.Text)
                        {
                            fechaIngresoIAS.Text = fechaInicioContratoV.Text;
                            Calcular_Click(sender, e);
                            MaintainScrollPositionOnPostBack = true;
                        }
                    }
                    else
                    {
                        AFC.Focus();
                        AFC.BackColor = Color.FromArgb(235, 120, 120);
                    }
                }
            }
            else
            {
                Response.Write("<script>alert('Debe Seleccionar Causal')</script>");
            }
        }

        public void reiniciarValores()
        {
            Label21.Text = "0";
            lbltrabajador.Text = "0";
            años.Text = "0";
            lblañostrabajados.Text = "0";
            meses.Text = "0";
            lblmesestrabajados.Text = "0";
            dias.Text = "0";
            lbldiastrabajados.Text = "0";
            Label29.Text = "0";
            Label30.Text = "0";
            saldo.Text = "0";
            lblsaldo.Text = "0";
            vacacionestomadas.Text = "0";
            Label34.Text = "0";
            habiles.Text = "0";
            lblhabiles.Text = "0";
            inhabiles.Text = "0";
            lblinhabiles.Text = "0";
            corridos.Text = "0";
            lblcorridos.Text = "0";
            lblsueldobase.Text = "0";
            lblbonovariable.Text = "0";
            lblbono2.Text = "0";
            lblGratificacion.Text = "0";
            lblColacion.Text = "0";
            lblmovilizacion.Text = "0";
            lblbasecalculo.Text = "0";
            lblbasevacaciones.Text = "0";
            lblremuneracionpendiente.Text = "0";
            lblbonofinal.Text = "0";
            lblindemnizacionvoluntaria.Text = "0";
            lblotroshaberes.Text = "0";
            lblvacafinal.Text = "0";
            lblmesaviso.Text = "0";
            ias.Text = "0";
            lbltotalpagos.Text = "0";
            lbltotaldescuento.Text = "0";
            lbltotalfiniquito.Text = "0";
        }

        private void SumasTotales()
        {
            lbltotalpagos.Text = (int.Parse(lblremuneracionpendiente.Text.Replace(",", "")) + int.Parse(lblbonofinal.Text.Replace(",", "")) + int.Parse(lblindemnizacionvoluntaria.Text.Replace(",", "")) + int.Parse(lblotroshaberes.Text.Replace(",", "")) + int.Parse(lblvacafinal.Text.Replace(",", "")) + int.Parse(lblmesaviso.Text.Replace(",", "")) + int.Parse(ias.Text.Replace(",", ""))).ToString("N0");
            lbltotalfiniquito.Text = (int.Parse(double.Parse(lbltotalpagos.Text.Replace(",", "")).ToString()) - int.Parse(double.Parse(lbltotaldescuento.Text.Replace(",", "")).ToString())).ToString("N0");
            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ " + (int.Parse(double.Parse(lbltotalpagos.Text.Replace(",", "")).ToString()) - int.Parse(double.Parse(lbltotaldescuento.Text.Replace(",", "")).ToString())).ToString("N0");
        }

        private int Indeminzacion()
        {
            int anio, mesanio, diasanio;
            anio = int.Parse(años.Text);
            mesanio = int.Parse(meses.Text);
            diasanio = int.Parse(dias.Text);
            if (mesanio > 6)
            {
                anio++;
            }
            else
            {
                if ((mesanio == 6) && (diasanio >= 1))
                {
                    anio++;
                }
            }
            if (anio >= 11)
            {
                anio = 11;
            }
            return anio;
        }

        private void DtToGRD()
        {
            if (datatable != null)
            {
                foreach (DataRow dr in datatable.Rows)
                {
                    switch (dr[0].ToString())
                    {
                        case "SUELDO BASE":
                            lblsueldobase.Text = dr[1].ToString();
                            break;
                        case "COLACION MES":
                            lblColacion.Text = dr[1].ToString();
                            break;
                        case "MOVILIZACION MES":
                            lblmovilizacion.Text = dr[1].ToString();
                            break;
                    }
                    lblbasecalculo.Text = (int.Parse(lblsueldobase.Text) + int.Parse(lblGratificacion.Text) + int.Parse(lblbonovariable.Text) + int.Parse(lblColacion.Text) + int.Parse(lblmovilizacion.Text)).ToString();

                    if (Convert.ToInt32((Label16.Text.Replace(",", "")).Replace(".", ",").Split(',')[0]) < Convert.ToInt32(lblbasecalculo.Text))
                    {
                        lblbasecalculo.Text = (Label16.Text.Replace(",", "")).Replace(".", ",").Split(',')[0];
                    }

                    lblbasevacaciones.Text = (int.Parse(lblsueldobase.Text) + int.Parse(lblbonovariable.Text)).ToString();
                }
            }
            else
            {
                lblsueldobase.Text = "0";
                lblColacion.Text = "0";
                lblmovilizacion.Text = "0";
            }
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

        private void CargaVarible(string connectionString)
        {
            int j = 1;
            strSql = string.Format("select * from FNVARIABLEREMUNERACIONCONSULTORA  WHERE tipo ='v' ");
            ds = Clases.Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
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

        private void creadtsueldo(string connectionString, string ficha)
        {
            try
            {
                string codigoAN, sqlStr;
                int DIASTRABAJADO = 0, Valor1 = 0, mes = 0;

                string[] fechaSplit = TextBox10.Text.Split('-');

                sqlStr = string.Format("SELECT   TOP(1)  softland.sw_variablepersona.mes, softland.sw_variablepersona.ficha, " +
                    "softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                    " FROM softland.sw_variablepersona INNER JOIN softland.sw_vsnpRetornaFechaMesExistentes ON softland.sw_variablepersona.mes = " +
                    "softland.sw_vsnpRetornaFechaMesExistentes.IndiceMes" +
                    " WHERE     (softland.sw_variablepersona.ficha = '{0}') AND softland.sw_vsnpRetornaFechaMesExistentes.FechaMes <= CAST('" + fechaSplit[2] + "-" +
                    fechaSplit[1] + "-" + fechaSplit[0] + "' AS DATE) " +
                    "GROUP BY softland.sw_variablepersona.ficha, softland.sw_variablepersona.mes, " +
                    "softland.sw_vsnpRetornaFechaMesExistentes.FechaMes" +
                    " ORDER BY softland.sw_vsnpRetornaFechaMesExistentes.FechaMes DESC", ficha);

                DataSet ds1 = Clases.Interface.ExecuteDataSet(connectionString, sqlStr);

                if (ds1 != null)
                {
                    if (ds1.Tables.Count > 0)
                    {
                        if (ds1.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds1.Tables[0].Rows)
                            {
                                datatable = new DataTable();
                                mes = int.Parse(dr["mes"].ToString());
                                //mes = int.Parse(MESULIQ);
                                Clases.Remuneracion remuneracion = new Clases.Remuneracion();
                                List<Clases.VariableRemuneracion> VariableRemu = new List<Clases.VariableRemuneracion>();
                                VariableRemu = remuneracion.ListaCONSULTORAFijo(connectionString, ficha, mes);
                                int Mes1 = mes;
                                if (VariableRemu != null)
                                {
                                    while (VariableRemu[1].valor == null)
                                    {
                                        VariableRemu = null;
                                        if (Mes1 == 1)
                                        {
                                            Mes1 = 24;
                                        }
                                        else
                                        {
                                            Mes1--;
                                        }
                                        VariableRemu = remuneracion.ListaCONSULTORAFijo(connectionString, ficha, Mes1);
                                    }
                                }
                                    
                                datatable.Columns.Add("Haber");
                                datatable.Columns.Add("Mensual");
                                foreach (Clases.VariableRemuneracion dr1 in VariableRemu)
                                {
                                    if (dr1.descripcion == "DIAS TRABAJADOS")
                                    {
                                        DIASTRABAJADO = int.Parse(dr1.valor);
                                    }
                                    else
                                    {
                                        if (dr1.descripcion == "SUELDO BASE")
                                        {
                                            datatable.Rows.Add(dr1.descripcion, dr1.valor);
                                        }
                                        else
                                        {
                                            if ((dr1.descripcion == "COLACION MES") || (dr1.descripcion == "MOVILIZACION MES"))
                                            {
                                                datatable.Rows.Add(dr1.descripcion, dr1.valor);
                                            }
                                            else
                                            {
                                                //if (dr1.descripcion != null)
                                                //{
                                                //    datatable.Rows.Add(dr1.descripcion, Gratificacion(connectionString, mes).ToString());
                                                //}
                                            }
                                            if (dr1.descripcion == null)
                                            {
                                                //datatable.Rows.Add(dr1.descripcion, Gratificacion(connectionString, mes).ToString());
                                                //datatable.Rows.Add("", "0");
                                            }
                                            else
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Clases.Gratificacion gratificacion = new Clases.Gratificacion();
                gratificacion.ficha = ficha;
                gratificacion.variable = "P007";
                gratificacion.Obtener(Properties.Settings.Default.connectionStringTEAMWORK);
                int Estado = 0;
                if (TextBox6.Text == "CCD")
                { Estado = 0; }
                else { Estado = 1; }
                
                string[] fechaDesvinculacion = TextBox10.Text.Split('-');

                string fechaAValidar = fechaDesvinculacion[0] + "-" + fechaDesvinculacion[1] + "-" + fechaDesvinculacion[2];
                double gratificacionCalculada = 0;

                if (TextBox8.Text.Equals("MOVISTAR") || TextBox8.Text.Equals("MOVISTAR TRASPASO TWSI"))
                {
                    gratificacionCalculada = -1;
                }
                else
                {
                    if ((gratificacion.valorvariable == "1") && (Estado == 1))
                    {

                        gratificacionCalculada = Math.Truncate(((int.Parse(datatable.Rows[0][1].ToString()) + int.Parse(lblbonovariable.Text)) * 0.25));
                    }
                    else
                    {
                        gratificacionCalculada = Gratificacion(connectionString, mes);
                    }
                }

                DataSet dataset = svcFiniquitos.GetGratificacion(
                    new string[] { "@FECHACONSULTA", "@GRATIFICACIONCALCULADA", "@FICHA", "@EMPRESA" },
                    new string[] { fechaAValidar, gratificacionCalculada.ToString(), ficha, "TWC" }).Table;

                foreach (DataRow rows in dataset.Tables[0].Rows)
                {
                    lblGratificacion.Text = rows["Gratificacion"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private int Gratificacion(string connectionString, int mes)
        {
            string strSql = string.Format("select valor from softland.sw_constvalor where mes = {0} and codConst = 'c013'", mes);
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count != null)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        return int.Parse(dr["valor"].ToString());
                    }
                }
            }
            return 0;
        }

        protected void OtroHeber_Click(object sender, EventArgs e)
        {
            if ((descuento0.SelectedItem.ToString() != "") && (TextBox12.Text != ""))
            {
                DataTable dt2 = Session["datos2"] as DataTable;

                if (!remupendAdc.Visible)
                {
                    dt2.Rows.Add(descuento0.SelectedItem.Text, TextBox12.Text);
                }
                else
                {
                    dt2.Rows.Add(descuento0.SelectedItem.Text + " " + remupendAdc.Value, TextBox12.Text);
                }

                TotalDescuento0.Text = (int.Parse(TotalDescuento0.Text) + int.Parse(TextBox12.Text)).ToString();
                GridView5.DataSource = dt2;

                GridView5.DataBind();
                Session["datos2"] = dt2;
                lblotroshaberes.Text = (int.Parse(lblotroshaberes.Text) + int.Parse(TotalDescuento0.Text)).ToString();
                SumasTotales();
            }
        }

        protected void Agregar_Click(object sender, EventArgs e)
        {
            if ((Descuento.SelectedItem.ToString() != "") && (TextBox11.Text != ""))
            {
                DataTable dt1 = Session["datos"] as DataTable;

                dt1.Rows.Add(Descuento.SelectedItem.Text, TextBox11.Text);

                TotalDescuento.Text = (int.Parse(TotalDescuento.Text) + int.Parse(TextBox11.Text)).ToString();
                GridView4.DataSource = dt1;

                GridView4.DataBind();
                Session["datos"] = dt1;

                lbltotaldescuento.Text = (int.Parse(float.Parse(lbltotaldescuento.Text).ToString()) + int.Parse(TextBox11.Text)).ToString("N0");
                SumasTotales();
            }
        }

        protected void Grabar(object sender, EventArgs e)
        {

            /** CREACION DE NUMERO FOLIO FINIQUITO */

            MethodServiceFiniquitos web = new MethodServiceFiniquitos();

            if (ddlContratos.SelectedItem.ToString() != "Seleccione" && ddlContratos.SelectedItem.ToString() != "")
            {
                if (CausalDespido.SelectedItem.ToString() != "Seleccione | Causal")
                {
                    web.FECHAHORASOLICITUD = DateTime.Now.ToString();
                    string AHORA = DateTime.Now.ToString();
                    string[] fechavisoDesv = TextBox10.Text.Split('-');
                    bool posibleCrear = true;

                    if (Label20.Text != "Sin Folio")
                    {
                        web.FECHAAVISODESVINCULACION = TextBox10.Text;
                    }
                    else
                    {
                        web.FECHAAVISODESVINCULACION = fechavisoDesv[0] + "-" + fechavisoDesv[1] + "-" + fechavisoDesv[2];
                    }

                    switch (Request.QueryString["event"])
                    {
                        case "calculo":
                            web.IDRENUNCIA = "CAL";
                            break;
                        case "simulation":
                            web.IDRENUNCIA = "SIM";
                            break;
                        default:
                            web.IDRENUNCIA = "0";
                            break;
                    }

                    web.USUARIO = Session["Usuario"].ToString();
                    web.RUTTRABAJADOR = TextBox6.Text;
                    web.FICHA = ddlContratos.SelectedItem.ToString();
                    web.NOMBRETRABAJADOR = TextBox2.Text;
                    web.ESTADOSOLICITUD = "2";
                    web.FECHAESTADO = AHORA;
                    web.IDCAUSAL = CausalDespido.SelectedItem.ToString();
                    web.IDEMPRESA = "3";
                    web.OBSERVACION = "";
                    web.CARGOMOD = TextBox7.Text;
                    web.AREANEGOCIO = TextBox5.Text;
                    web.CLIENTE = TextBox8.Text;
                    web.ESPARTTIME = "";
                    web.ESZONAEXT = DropDownList3.SelectedItem.ToString();

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

                    if (Request.QueryString["event"] == "simulation")
                    {
                        if (DropDownList4.SelectedItem.ToString() == "NO")
                        {
                            if (txtFechaCartaBaja.Value == "")
                            {
                                posibleCrear = false;
                            }
                            else
                            {
                                web.FECHACARTABAJA = txtFechaCartaBaja.Value;
                            }
                        }
                        else
                        {
                            web.FECHACARTABAJA = "";
                        }
                    }
                    else
                    {
                        web.FECHACARTABAJA = "";
                    }
                    
                    web.EXCEPCIONCOSTOCERO = excpcostocero.Checked ? "S" : "N";
                    web.TOTALFINIQUITOS = lbltotalfiniquito.Text;

                    if (Request.QueryString["event"] == "simulation")
                    {
                        if (CausalDespido.SelectedItem.Value == "Art.161Inciso1 | Necesidades de la empresa")
                        {
                            if (tec_prof.Checked)
                            {
                                web.NIVEL = "Tecnico-Profesional";
                            }

                            if (operario.Checked)
                            {
                                web.NIVEL = "Operario";
                            }
                        }
                        else
                        {
                            web.NIVEL = "-";
                        }
                    }
                    else
                    {
                        web.NIVEL = "-";
                    }

                    if (Request.QueryString["destiny"] != null)
                    {
                        web.CATEGORIA = Request.QueryString["destiny"].ToString();
                    }

                    if (posibleCrear)
                    {
                        string encDesvinculacion = "0";
                        string errorDuplicidad = string.Empty;
                        string applicationIntegrateOperacionesComeBack = string.Empty;

                        if (!chk_restriccion_retencionjudicial.Checked && chk_omitir_restriccion_retencionjudicial.Checked)
                        {
                            foreach (DataRow rows in web.SetCrearModificarEncDesvinculacionService.Tables[0].Rows)
                            {
                                if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                                {
                                    encDesvinculacion = rows["RESULTADO"].ToString().Replace("Folio: N° ", "");
                                    applicationIntegrateOperacionesComeBack = rows["Redirect"].ToString();
                                }
                                else
                                {
                                    encDesvinculacion = "N";
                                    errorDuplicidad = rows["RESULTADO"].ToString();
                                }
                            }

                            if (encDesvinculacion != "N")
                            {
                                web.IDDESVINCULACION = encDesvinculacion;

                                foreach (GridViewRow rowItem in GridView1.Rows)
                                {
                                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus"));
                                    if (chk.Checked)
                                    {
                                        string[] fechaInicio = rowItem.Cells[2].Text.Split('-');
                                        string[] fechaTermino = rowItem.Cells[3].Text.Split('-');

                                        web.FICHA = rowItem.Cells[1].Text;
                                        web.FECHAINICIO = fechaInicio[1] + "/" + fechaInicio[0] + "/" + fechaInicio[2];

                                        if (fechaTermino[1] != "01" && fechaTermino[0] != "01" && fechaTermino[2] != "0001")
                                        {
                                            web.FECHATERMINO = fechaTermino[1] + "/" + fechaTermino[0] + "/" + fechaTermino[2];
                                        }
                                        else
                                        {
                                            web.FECHATERMINO = TextBox10.Text.Split('-')[1] + "/" + TextBox10.Text.Split('-')[0] + "/" + TextBox10.Text.Split('-')[2];
                                        }


                                        DataSet dataContratos = web.SetContratosOUTService;
                                    }
                                }

                                web.MEAVISO = DropDownList4.SelectedValue.ToString();
                                web.AFC = AFC.Text;

                                DataSet dataConcep = web.SetConceptosAdicionalesOUTService;

                                if (GridView2.Rows.Count > 0 && GridView2.Rows[0].Cells.Count > 1)
                                {
                                    if (GridView2.Rows[0].Cells.Count > 0 && GridView2.Rows.Count > 0)
                                    {
                                        for (var i = 1; i < GridView2.Rows[0].Cells.Count; i++)
                                        {
                                            for (var j = 0; j < GridView2.Rows.Count; j++)
                                            {
                                                if (j > 0)
                                                {
                                                    if (GridView2.Rows[0].Cells[i].Text.ToString() != "&nbsp;" && GridView2.Rows[j].Cells[i].Text.ToString() != "&nbsp;" &&
                                                        GridView2.Rows[j].Cells[0].Text.ToString() != "&nbsp;")
                                                    {
                                                        web.DESCRIPCION = GridView2.Rows[j].Cells[0].Text.ToString();
                                                        web.MONTO = GridView2.Rows[j].Cells[i].Text.ToString();
                                                        web.MESANO = GridView2.Rows[0].Cells[i].Text.ToString();

                                                        DataSet dataDV = web.SetDatosVariablesOUTService;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                foreach (GridViewRow rowItem in GridView3.Rows)
                                {
                                    CheckBox chk = (CheckBox)(rowItem.Cells[0].FindControl("estatus0"));
                                    if (chk.Checked)
                                    {
                                        web.DESCRIPCION = rowItem.Cells[0].Text;
                                        web.USUARIO = Session["Usuario"].ToString();
                                        DataSet dataCD = web.SetControlDocumentosOUTService;
                                    }
                                }

                                if (ckbono.Checked)
                                {
                                    web.BONONOMBRE = Label22.Text;
                                    web.MONTO = txtBonorezagado.Text;
                                    web.PERIODO = DropDownList5.SelectedItem.ToString();

                                    DataSet dataBA = web.SetBonosAdicionalesOUTService;
                                }

                                foreach (GridViewRow rows in GridView5.Rows)
                                {
                                    web.TIPO = "H";
                                    web.DESCRIPCION = rows.Cells[1].Text;
                                    web.MONTO = rows.Cells[2].Text;

                                    DataSet dataHaber = web.SetHaberesYDescuentosOUTService;
                                }

                                foreach (GridViewRow rows in GridView4.Rows)
                                {
                                    web.TIPO = "D";
                                    web.DESCRIPCION = rows.Cells[1].Text;
                                    web.MONTO = rows.Cells[2].Text;

                                    DataSet dataDescuento = web.SetHaberesYDescuentosOUTService;
                                }

                                web.DIASTOTALESTRABAJADOS = Label21.Text;
                                web.DIASTOTALESTRABAJADOS2 = lbltrabajador.Text;
                                web.YEAR = años.Text;
                                web.YEAR2 = lblañostrabajados.Text;
                                web.MESES = meses.Text;
                                web.MESES2 = lblmesestrabajados.Text;
                                web.DIAS = dias.Text;
                                web.DIAS2 = lbldiastrabajados.Text;
                                web.VACACIONESPROGRESIVAS = Label29.Text;
                                web.VACACIONESPROGRESIVAS2 = Label30.Text;
                                web.SALDO = saldo.Text;
                                web.SALDO2 = lblsaldo.Text;
                                web.VACACIONESTOMADAS = vacacionestomadas.Text;
                                web.VACACIONESTOMADAS2 = Label34.Text;
                                web.SALDOHABILES = habiles.Text;
                                web.SALDOHABILES2 = lblhabiles.Text;
                                web.SALDOINHABILES = inhabiles.Text;
                                web.SALDOINHABILES2 = lblinhabiles.Text;
                                web.DIASCORRIDOS = corridos.Text;
                                web.DIASCORRIDOS2 = lblcorridos.Text;
                                web.FECHADIASCORRIDOS = fechamasuno.Text;
                                web.ZONAEXT = DropDownList3.SelectedItem.ToString();

                                switch (DropDownList3.SelectedItem.ToString())
                                {
                                    case "SI":
                                        web.FACTORCALCULO = "1.66";
                                        break;
                                    default:
                                        web.FACTORCALCULO = "1.25";
                                        break;
                                }

                                DataSet dataDVa = web.SetDiasVacacionesOUTService;

                                web.SUELDOBASE = lblsueldobase.Text;
                                web.BONOSVARIABLES = lblbonovariable.Text;
                                web.GRATIFICACION = lblGratificacion.Text;
                                web.COLACION = lblColacion.Text;
                                web.MOVILIZACION = lblmovilizacion.Text;
                                web.BASECALCULO = lblbasecalculo.Text;
                                web.BASEVACACIONES = lblbasevacaciones.Text;

                                DataSet dataHM = web.SetHaberesMensualesOUTService;

                                web.REMUNERACIONPENDIENTE = lblremuneracionpendiente.Text;
                                web.BONO = lblbonofinal.Text;
                                web.INDEMNIZACIONVOLUNTARIA = lblindemnizacionvoluntaria.Text;
                                web.OTROSHABERES = lblotroshaberes.Text;
                                web.VACACIONES = lblvacafinal.Text;
                                web.MESAVISO = lblmesaviso.Text;
                                web.IAS = ias.Text;
                                web.TOTALPAGOS = lbltotalpagos.Text;
                                web.TOTALDESCUENTOS = lbltotaldescuento.Text;
                                web.TOTALFINIQUITO = lbltotalfiniquito.Text;

                                DataSet dataTP = web.SetTotalPagosOUTService;

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

                                    if (Convert.ToInt32(encDesvinculacion) < 0)
                                    {
                                        response.CreatePdfDocument(
                                            encDesvinculacion.ToString(),
                                            "propuesta",
                                            Request.QueryString["glcid"].ToString()
                                        );
                                    }
                                }

                                Label20.Text = "Folio : N° " + encDesvinculacion;
                                Label18.Text = "Calculo Generado";

                                foreach (DataRow rows in dataTP.Tables[0].Rows)
                                {
                                    if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                                    {
                                        if (applicationIntegrateOperacionesComeBack == "")
                                        {
                                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'success',title: 'Calculo guardado exitosamente!', text: 'Se ha generado el Cálculo de la baja y este registro no podrá ser modificado.'});", true);
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
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error', title: 'A ocurrido un error inesperado', text: 'No se puede guardar el calculo, Solicite la eliminación a TI del mismo.'});", true);
                                    }
                                }

                            }
                            else
                            {
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'error',title: 'No se puede guardar el calculo debido a:', html: 'Debe indicar una fecha de emisión de carta de baja'});", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'Atención!', text: 'Debe seleccionar la causal de desvinculación.'});", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({type: 'warning', title: 'Atención!', text: 'Seleccione Ficha activa para poder grabar el finiquito.'});", true);
            }

        }

        protected void haberselected_index(object sender, EventArgs e)
        {
            if (descuento0.SelectedItem.ToString() == "Remuneracion Pendiente")
            {
                remupendAdc.Visible = true;
                LblRemunPendAdc.Visible = true;
            }
            else
            {
                remupendAdc.Visible = false;
                LblRemunPendAdc.Visible = false;
            }
        }

        protected void selected_fechaCartaBaja(object sender, EventArgs e)
        {
            if (Request.QueryString["event"] == "simulation")
            {
                if (DropDownList4.SelectedItem.ToString() == "NO")
                {
                    txtFechaCartaBaja.Visible = true;
                }
                else
                {
                    txtFechaCartaBaja.Visible = false;
                }
            }
        }

        protected void selected_index(object sender, EventArgs e)
        {
            bool isPosibleCalcular = true;
            string[] paramValCal = new string[3];
            string[] valValCal = new string[3];
            DataSet dataVal;
            string mensajeError = string.Empty;
            
            if (ddlContratos.SelectedItem.ToString() != "Seleccione")
            {
                if (Request.QueryString["event"] != "simulation")
                {
                    paramValCal[0] = "@FICHA";
                    paramValCal[1] = "@RUT";
                    paramValCal[2] = "@IDEMPRESA";

                    valValCal[0] = ddlContratos.SelectedItem.ToString();
                    valValCal[1] = TextBox6.Text;
                    valValCal[2] = "3";

                    dataVal = svcFiniquitos.GetValidaRepeticionCalculo(paramValCal, valValCal).Table;

                    foreach (DataRow rows in dataVal.Tables[0].Rows)
                    {
                        switch (rows["Code"].ToString())
                        {
                            case "200":
                                isPosibleCalcular = false;
                                mensajeError = "No podras realizar el calculo debido a que este finiquito por esta ficha ya esta previamente registrado.";
                                break;
                        }
                    }
                }
            }
            else
            {
                isPosibleCalcular = false;
                mensajeError = "No podras realizar el calculo debido a que debe seleccionar un contrato activo.";
            }
              
            if (isPosibleCalcular)
            {
                Clases.Contrato contrato = new Clases.Contrato();
                contrato.centrodecosto(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text), ddlContratos.SelectedItem.ToString());
                lblcentrocosto.Text = contrato.centrocosto;

                Clases.Cargo cargo = new Clases.Cargo();

                /** CLASE QUE TRAE EL CARGO ASOCIADO */
                cargo.ficha = ddlContratos.SelectedItem.ToString();
                cargo.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                TextBox4.Text = cargo.glosaCargo;
                TextBox7.Text = cargo.glosaMOd;

                /** CLASE QUE TRAE EL AREA DE NEGOCIO ASOCIADA */
                Clases.AreaNegocio anegocio = new Clases.AreaNegocio();
                anegocio.ficha = ddlContratos.SelectedItem.ToString();
                anegocio.Obtener(string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text));
                TextBox8.Text = anegocio.areanegocio;
                TextBox5.Text = anegocio.codanrg;

                string[] paramAlertas = new string[3];
                string[] valAlertas = new string[3];
                DataSet dataAlertas;

                paramAlertas[0] = "@FICHA";
                paramAlertas[1] = "@EMPRESA";
                paramAlertas[2] = "@CONCEPTO";

                /** SEGURO COMPLEMENTARIO */

                valAlertas[0] = ddlContratos.SelectedValue;
                valAlertas[1] = "TWC";
                valAlertas[2] = "SEGURO COMPLEMENTARIO";

                dataAlertas = svcFiniquitos.GetAlertasInformativas(paramAlertas, valAlertas).Table;

                foreach (DataRow rows in dataAlertas.Tables[0].Rows)
                {
                    if (rows["Code"].ToString() == "200")
                    {
                        segurcomplemt.Visible = true;
                    }
                    else
                    {
                        segurcomplemt.Visible = false;
                    }
                }

                /** INFORMATIVO => LUCRO CESANTE PARA CAUSAL VENCIMIENTO DEL PLAZO (INGRESO POR OPERACIONES) */

                valAlertas[0] = ddlContratos.SelectedValue;
                valAlertas[1] = "TWC";
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

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "Swal.fire({icon: 'warning', title: 'Atención!', text:'" + mensajeError + "'});", true);
            }
            

        }

        protected void selected_format_carta(object sender, EventArgs e)
        {
            if (Request.QueryString["event"] == "simulation")
            {
                if (CausalDespido.SelectedItem.Value == "Art.161Inciso1 | Necesidades de la empresa")
                {
                    lbl_title_carta_161inciso1.Visible = true;
                    lbl_tec_prof.Visible = true;
                    lbl_operario.Visible = true;
                    operario.Visible = true;
                    tec_prof.Visible = true;
                }
                else
                {
                    lbl_title_carta_161inciso1.Visible = false;
                    lbl_tec_prof.Visible = false;
                    lbl_operario.Visible = false;
                    operario.Visible = false;
                    tec_prof.Visible = false;
                    tec_prof.Checked = false;
                    operario.Checked = false;
                }
            }
        }

        protected void cktecprof_CheckedChanged(object sender, EventArgs e)
        {
            operario.Checked = false;
        }

        protected void ckoperario_CheckedChanged(object sender, EventArgs e)
        {
            tec_prof.Checked = false;
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
            lblotroshaberes.Text = (Convert.ToInt32(lblotroshaberes.Text.Replace(",", "")) - captura).ToString("N0");
            lbltotalpagos.Text = (Convert.ToInt32(lbltotalpagos.Text.Replace(",", "")) - captura).ToString("N0");
            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ " + (Convert.ToInt32(lbltotalfiniquito.Text.Replace(",", "")) - captura).ToString("N0");
            lbltotalfiniquito.Text = (Convert.ToInt32(lbltotalfiniquito.Text.Replace(",", "")) - captura).ToString("N0");
            //TotalDescuento0.Text = captura.ToString();
            dt.Rows[e.RowIndex].Delete();
            GridView5.DataSource = null;
            GridView5.DataSource = dt;
            GridView5.DataBind();
            Session["datos2"] = dt;

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
            lbltotaldescuento.Text = (Convert.ToInt32(lbltotaldescuento.Text.Replace(",", "")) - captura).ToString("N0");
            montoVisibleFiniquito.Text = "Monto Finiquito <br /> $ " + (Convert.ToInt32(lbltotalfiniquito.Text.Replace(",", "")) + captura).ToString("N0");
            lbltotalfiniquito.Text = (Convert.ToInt32(lbltotalfiniquito.Text.Replace(",", "")) + captura).ToString("N0");

            //TotalDescuento0.Text = captura.ToString();
            dt.Rows[e.RowIndex].Delete();
            GridView4.DataSource = null;
            GridView4.DataSource = dt;
            GridView4.DataBind();
            Session["datos"] = dt;

        }

        protected void Generar_Archivo(object sender, EventArgs e)
        {

        }

        protected void Button3_Click1(object sender, EventArgs e)
        {
            Session["rut"] = TextBox6.Text;
            string redir = string.Empty;

            if (Request.QueryString["ref"] != null)
            {
                if (Request.QueryString["callback"] != null)
                {
                    redir = Request.QueryString["ref"].ToString() + "&callback=" + Request.QueryString["callback"].ToString() + "&destiny=" + Request.QueryString["destiny"].ToString() + "&glcid=" + Request.QueryString["glcid"].ToString();
                }
                else
                {
                    redir = Request.QueryString["ref"].ToString();
                }

                if (Request.QueryString["ref"].ToString() != "ZGlyZWN0bHk=")
                {
                    if (Request.QueryString["simulate"] == null)
                    {
                        redir = redir + "&event=calculo";
                    }
                    else
                    {
                        redir = redir + "&event=simulation";
                    }
                }
                else
                {
                    redir = redir + "&event=simulation";
                }

            }

            if (redir == "")
            {
                Response.Redirect("CalculoBajaConsultora.aspx");
            }
            else
            {
                Response.Redirect("CalculoBajaConsultora.aspx?ref=" + redir);
            }
            
        }

        protected void ckbono_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckbono.Checked)
            {
                DropDownList5.Enabled = false;
                txtBonorezagado.Enabled = false;
                txtBonorezagado.Text = "0";
                DropDownList5.SelectedIndex = 0;
            }
            else if (ckbono.Checked)
            {

                DropDownList5.Enabled = true;
                txtBonorezagado.Enabled = true;
            }
        }
        
        protected void ckindemVol_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkIndemVol.Checked)
            {
                montoIndeminizacionVol.Enabled = false;
            }
            else if (chkIndemVol.Checked)
            {

                montoIndeminizacionVol.Enabled = true;
            }
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
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
    public partial class PagosFiniquitos : System.Web.UI.Page
    {
        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            /** SESSION QUE PERMITE GUARDAR EL REGISTRO DEL PAGO, INICIALIZADA EN FALSO */
            Session["applicationIsPosibleSavePago"] = false;

            MethodServiceFiniquitos method = new MethodServiceFiniquitos();

            if (Session["nombretrabajadorFiniquito"] == null || Session["iddesvinculacionFiniquito"] == null || Session["empresaFiniquito"] == null)
            {
                Response.Redirect("Inicio.aspx");
            }

            txtTrabajador.Text = replaceCharacter(Session["nombretrabajadorFiniquito"].ToString());

            method.IDDESVINCULACION = Session["iddesvinculacionFiniquito"].ToString();

            gvContratosAsociados.DataSource = method.GetContratosCalculoPagosService;
            gvContratosAsociados.DataBind();
            gvContratosAsociados.Width = 950;
            gvContratosAsociados.Columns[0].ItemStyle.Width = 200;
            gvContratosAsociados.Columns[1].ItemStyle.Width = 200;
            gvContratosAsociados.Columns[2].ItemStyle.Width = 200;
            gvContratosAsociados.Columns[3].ItemStyle.Width = 50;
            gvContratosAsociados.Columns[4].ItemStyle.Width = 50;

            int contadorInit = 0;
            foreach(GridViewRow row in gvContratosAsociados.Rows){

                if(contadorInit == 0){

                    string[] parametrosCliente = {
                        row.Cells[0].Text
                    };

                    foreach (DataRow rows in svcFiniquitos.GetAreaNegocio(parametrosCliente).Table.Tables[0].Rows)
                    {
                        txtCliente.Text = rows["DesArn"].ToString();
                        txtSigla.Text = rows["codArn"].ToString();
                    }

                }

                contadorInit = contadorInit + 1;
            }

            /** LLENADO DE DROP DOWN LIST */
            if (ddlTipoPago.Items.Count == 0)
            {
                ddlTipoPago.Items.Add("SELECCIONE TIPO DE PAGO");
                foreach (DataRow rows in method.GetTipoPagoService.Tables[0].Rows)
                {
                    if (rows["VALIDACION"].ToString() == "0")
                    {
                        ddlTipoPago.Items.Add(rows["NOMBRE"].ToString());
                    }
                }
            }

            if (ddlBancos.Items.Count == 0)
            {
                ddlBancos.Items.Add("SELECCIONE BANCO");
                foreach (DataRow rows in method.GetBancosPagosService.Tables[0].Rows)
                {
                    if (rows["VALIDACION"].ToString() == "0")
                    {
                        ddlBancos.Items.Add(rows["NOMBRE"].ToString());
                    }
                }
            }

            if (ddlEmpresas.Items.Count == 0)
            {
                ddlEmpresas.Items.Add("SELECCIONE EMPRESA");
                foreach (DataRow rows in method.GetEmpresasPagosService.Tables[0].Rows)
                {
                    if (rows["VALIDACION"].ToString() == "0")
                    {
                        ddlEmpresas.Items.Add(rows["NOMBRE"].ToString());
                    }
                }
            }

            /** DESPLIEGUE DE TOTALES GENERADOS EN EL CALCULO */

            switch (Session["empresaFiniquito"].ToString())
            {
                case "TWEST":

                    lblDeshaucio.Visible = false;
                    lblDeshaucioEnc.Visible = false;
                    lblIAS.Visible = false;
                    lblIASEnc.Visible = false;
                    lblIndeminizacion.Visible = false;
                    lblIndeminizacionEnc.Visible = false;

                    foreach (DataRow rows in method.GetTotalesPagosESTService.Tables[0].Rows)
                    {
                        lblVacaciones.Text = "$ " + Convert.ToInt32(rows["FERIADOPROPORCIONAL"]).ToString("N0");
                        lblTotalFiniquito.Text = "$ " + Convert.ToInt32(rows["FERIADOPROPORCIONAL"]).ToString("N0");
                    }

                    break;
            }
        }

        protected void selected_tipo_pago(object sender, EventArgs e) { 
            switch(ddlTipoPago.SelectedItem.ToString()){
                case "CHEQUE":
                    ddlBancos.Visible = true;
                    ddlEmpresas.Visible = true;
                    numeroCheque.Visible = true;
                    break;
                case "TRANSFERENCIA":
                    ddlBancos.Visible = true;
                    ddlEmpresas.Visible = true;
                    numeroCheque.Visible = false;
                    break;
                case "VALE VISTA":
                    ddlBancos.Visible = true;
                    ddlEmpresas.Visible = true;
                    numeroCheque.Visible = false;
                    break;
                case "SELECCIONE TIPO DE PAGO":
                    ddlBancos.Visible = false;
                    ddlEmpresas.Visible = false;
                    numeroCheque.Visible = false;
                    break;
            }
        }

        protected void selected_banco_correlativo(object sender, EventArgs e)
        {
            string[] parametrosCorltDis = {
                "@BANCO"
            };

            string[] valoresCorltDis = {
                ddlBancos.SelectedItem.ToString()
            };

            foreach(DataRow rows in svcFiniquitos.GetObtenerUltimoCorrelativoDisponiblePagos(parametrosCorltDis, valoresCorltDis).Table.Tables[0].Rows){
                if(Convert.ToInt32(Convert.ToString(rows["VALIDACION"])) == 0){
                    numeroCheque.Value = numeroSerieCheque(rows["ID"].ToString());
                    idCorrelativo.Text = rows["ID"].ToString();
                }
            }
        }

        protected void registrarPago_Click(object sender, EventArgs e)
        { 
            string[] paramValidaRegPago = {
                "@IDDESVINCULACION"
            };

            string[] valoresValidaPago = {
                Session["iddesvinculacionFiniquito"].ToString()
            };

            foreach(DataRow rows in svcFiniquitos.GetValidarRegistroPago(paramValidaRegPago, valoresValidaPago).Table.Tables[0].Rows){
                if(Convert.ToInt32(rows["VALIDACION"].ToString()) == 0){
                    string[] paramRegistrarPago = {
                        "@TIPOPAGO",
                        "@NUMERODOCUMENTO",
                        "@NSERIEDOCUMENTO",
                        "@BANCO",
                        "@EMPRESA",
                        "@MONTO_VACACIONES",
                        "@MONTO_IAS",
                        "@MONTO_DESHAUCIO",
                        "@MONTO_INDEMNVOLUNTARIA",
                        "@MONTO_FINIQUITO",
                        "@USUARIO",
                        "@IDDESVINCULACION",
                        "@NOMBRETRABAJADOR",
                        "@CLIENTENOMBRE",
                        "@CLIENTESIGLA"
                    };

                    string[] valoresRegistrarPago = {
                        ddlTipoPago.SelectedItem.ToString(),
                        idCorrelativo.Text,
                        numeroCheque.Value,
                        ddlBancos.SelectedItem.ToString(),
                        ddlEmpresas.SelectedItem.ToString(),
                        lblVacaciones.Text,
                        lblIAS.Text,
                        lblDeshaucio.Text,
                        lblIndeminizacion.Text,
                        lblTotalFiniquito.Text,
                        Session["Usuario"].ToString(),
                        Session["iddesvinculacionFiniquito"].ToString(),
                        txtTrabajador.Text,
                        txtCliente.Text,
                        txtSigla.Text
                    };

                    foreach(DataRow rowsPago in svcFiniquitos.SetInsertarRegistroPagoPagos(paramRegistrarPago, valoresRegistrarPago).Table.Tables[0].Rows){
                        if (Convert.ToInt32(rowsPago["VALIDACION"].ToString()) == 0)
                        {
                            if (ddlTipoPago.SelectedItem.ToString() == "CHEQUE")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'success',title: 'Pago registrado exitosamente!', html: '<label>El finiquito con folio " + Session["iddesvinculacionFiniquito"].ToString() + " ha pasado a estado Disponible para impresión.</label>'});", true);
                            }
                            else if(ddlTipoPago.SelectedItem.ToString() == "TRANSFERENCIA")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'success',title: 'Pago registrado exitosamente!', html: '<label>El finiquito con folio " + Session["iddesvinculacionFiniquito"].ToString() + " ha pasado a estado pagado.</label>'});", true);
                            }
                            else if (ddlTipoPago.SelectedItem.ToString() == "VALE VISTA")
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'success',title: 'Pago registrado exitosamente!', html: '<label>El finiquito con folio " + Session["iddesvinculacionFiniquito"].ToString() + " ha pasado a estado pagado.</label>'});", true);
                            }
                        }
                        else 
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error',title: 'Ha ocurrido algo!', html: '<label>" + rowsPago["RESULTADO"] + "</label>'});", true);
                        }
                    }
                } else {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "swal({type: 'error',title: 'Atención!', html: '<label>" + rows["RESULTADO"].ToString() + "</label>'});", true);
                }
            }
        }

        [WebMethod()]
        public static string GetValidaDocumentoPagos(string numeroDocumento) 
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();

            web.NUMERODOCUMENTO = numeroDocumento;

            return JsonConvert.SerializeObject(web.GetValidaPagosService, Formatting.Indented);
        }

        [WebMethod()]
        public static string validacionGuardarRegistroPago(string resultValidaDocumento)
        {
            if (resultValidaDocumento == "true")
            {
                HttpContext.Current.Session["applicationIsPosibleSavePago"] = true;
            }
            else
            {
                HttpContext.Current.Session["applicationIsPosibleSavePago"] = false;
            }

            return "1";
        }

        /** FUNCIONES ESPECIALES ACA */

        private string numeroSerieCheque(string idcorrelativo)
        {
            string numeroSerie = string.Empty;

            switch(idcorrelativo.Length){
                case 1:
                    numeroSerie = "000000" + idcorrelativo;
                    break;
                case 2:
                    numeroSerie = "00000" + idcorrelativo;
                    break;
                case 3:
                    numeroSerie = "0000" + idcorrelativo;
                    break;
                case 4:
                    numeroSerie = "000" + idcorrelativo;
                    break;
                case 5:
                    numeroSerie = "00" + idcorrelativo;
                    break;
                case 6:
                    numeroSerie = "0" + idcorrelativo;
                    break;
                case 7:
                    numeroSerie = idcorrelativo;
                    break;
            }

            return numeroSerie;
        }

        public string replaceCharacter(string texto)
        {
            return texto.Replace("&#225;", "á").Replace("&#193;", "Á").Replace("&#233;", "é").Replace("&#201;", "É").Replace("&#237;", "í").Replace("&#205;", "Í").Replace("&#243;", "ó").Replace("&#211;", "Ó").Replace("&#250", "ú").Replace("&#218;", "Ú").Replace("&#209;", "Ñ");
        }
    }
}
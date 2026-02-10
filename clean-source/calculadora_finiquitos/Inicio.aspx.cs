using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json;
using FiniquitosV2.Clases;
using OfficeOpenXml;
using Finiquitos.Clases;
using System.IO;
using System.Drawing;

namespace FiniquitosV2
{
    public partial class Inicio : System.Web.UI.Page
    {

        MethodServiceFiniquitos web = new MethodServiceFiniquitos();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.IsPostBack)
            {

            }

            if (Session["Usuario"] != null)
            {
                if (Session["Tipo"].ToString() == "2") {
                    Response.Redirect("RenunciasRecibidas");
                    linkInicio.Visible = false;

                    /** RESPONSIVE */
                    linkInicioResponsive.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "3")
                {
                    Response.Redirect("RecepcionDocumentos");
                }
                else if (Session["Tipo"].ToString() == "1") {

                }
                else if (Session["Tipo"].ToString() == "4" || Session["Tipo"].ToString() == "6")
                {
                    finanzas.Visible = false;
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;

                    /** RESPONSIVE */
                    linkBasePagosResponsive.Visible = false;
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "7")
                {
                    bajaFiniquito.Visible = false;
                    linkRenunciasRecibidas.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkCalculoBaja.Visible = false;

                    /** RESPONSIVE */
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                    linkCalculoBajaResponsive.Visible = false;
                }
                else if (Session["Tipo"].ToString() == "8")
                {
                    finanzas.Visible = false;
                    linkBasePagos.Visible = false;
                    linkRenunciasRecibidas.Visible = false;

                    /** RESPONSIVE */
                    linkBasePagosResponsive.Visible = false;
                    linkRenunciasRecibidasResponsive.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                    linkSolicitudBaja.Visible = false;
                    linkSolicitudBajaResponsive.Visible = false;
                    linkCalculoBaja.Visible = false;
                    linkCalculoBajaResponsive.Visible = false;
                }
            }
            else
            {
                Response.Redirect("Login");
            }

            if (Session["denegado"] != null) {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "denegado();", true);
                Session.Remove("denegado");
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
            if (Session["ok"] != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), " ", "ok();", true);
                Session.Remove("ok");
            }

            //MethodServiceFiniquitos web = new MethodServiceFiniquitos();

            //web.MEDIOPAGO = "TRANSFERENCIA";
            //foreach(DataRow rows in web.GetFiniquitosConfirmadosService.Tables[0].Rows){
            //    if (Convert.ToInt32(rows["VALIDACION"]) == 0)
            //    {
            //        gvConfirmadosPagoTransf.DataSource = web.GetFiniquitosConfirmadosService;
            //        gvConfirmadosPagoTransf.DataBind();
            //    }
            //    else if(Convert.ToInt32(rows["VALIDACION"]) > 0)
            //    {
            //        gvConfirmadosPagoTransf.DataSource = null;
            //        gvConfirmadosPagoTransf.DataBind();
            //        btnCuadraturaPdfTransf.Visible = false;
            //        btnCuadraturaExcelTransf.Visible = false;
            //    }
            //}

        }

        protected void GetReporteriaFiniquitosUsuarios(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Reporte");
                var worksheet = excel.Workbook.Worksheets["Reporte"];

                worksheet.Cells["A1"].Value = "Usuario";
                worksheet.Cells["B1"].Value = "Finiquitos";
                
                int vueltas = 2;

                web.FILTRO = "S";
                web.FILTRARPOR = "USUARIO";

                foreach (DataRow rows in web.GetReporteriaFiniquitosService.Tables[0].Rows)
                {

                    worksheet.Cells["A" + vueltas.ToString()].Value = rows["USUARIO"].ToString();
                    worksheet.Cells["B" + vueltas.ToString()].Value = rows["FINIQUITOS"].ToString();

                    vueltas = vueltas + 1;
                }
                
                worksheet.Cells["A:B"].AutoFitColumns();

                string nombreArchivo = "Reporte Finiquitos Calculados.xlsx";
                
                excel.Workbook.Properties.Title = "Attempts";
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", nombreArchivo));
                Response.ContentType = "application/excel";
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.Flush();
                Response.End();
            }
        }

        protected void GetReporteriaFiniquitosUsuAreaNeg(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Reporte");
                var worksheet = excel.Workbook.Worksheets["Reporte"];

                worksheet.Cells["A1"].Value = "Usuario";
                worksheet.Cells["B1"].Value = "Area Negocio";
                worksheet.Cells["C1"].Value = "Finiquitos";

                int vueltas = 2;

                web.FILTRO = "S";
                web.FILTRARPOR = "USUARIOYAREANEG";

                foreach (DataRow rows in web.GetReporteriaFiniquitosService.Tables[0].Rows)
                {

                    worksheet.Cells["A" + vueltas.ToString()].Value = rows["USUARIO"].ToString();
                    worksheet.Cells["B" + vueltas.ToString()].Value = rows["AREA_NEGOCIO"].ToString();
                    worksheet.Cells["C" + vueltas.ToString()].Value = rows["FINIQUITOS"].ToString();

                    vueltas = vueltas + 1;
                }

                worksheet.Cells["A:C"].AutoFitColumns();

                string nombreArchivo = "Reporte Finiquitos Calculados.xlsx";

                excel.Workbook.Properties.Title = "Attempts";
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", nombreArchivo));
                Response.ContentType = "application/excel";
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.Flush();
                Response.End();
            }
        }

        protected void GetReporteriaFiniquitosUsuAreaNegFecha(object sender, EventArgs e)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Reporte");
                var worksheet = excel.Workbook.Worksheets["Reporte"];

                worksheet.Cells["A1"].Value = "Usuario";
                worksheet.Cells["B1"].Value = "Area Negocio";
                worksheet.Cells["C1"].Value = "Fecha";
                worksheet.Cells["D1"].Value = "Finiquitos";

                int vueltas = 2;

                web.FILTRO = "S";
                web.FILTRARPOR = "USUARIOAREANEGYFECHA";

                foreach (DataRow rows in web.GetReporteriaFiniquitosService.Tables[0].Rows)
                {

                    worksheet.Cells["A" + vueltas.ToString()].Value = rows["USUARIO"].ToString();
                    worksheet.Cells["B" + vueltas.ToString()].Value = rows["AREA_NEGOCIO"].ToString();
                    worksheet.Cells["C" + vueltas.ToString()].Value = rows["FECHA_CALCULO"].ToString();
                    worksheet.Cells["D" + vueltas.ToString()].Value = rows["FINIQUITOS"].ToString();

                    vueltas = vueltas + 1;
                }

                worksheet.Cells["A:D"].AutoFitColumns();

                string nombreArchivo = "Reporte Finiquitos Calculados.xlsx";

                excel.Workbook.Properties.Title = "Attempts";
                Response.ClearContent();
                Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", nombreArchivo));
                Response.ContentType = "application/excel";
                Response.BinaryWrite(excel.GetAsByteArray());
                Response.Flush();
                Response.End();
            }
        }

        [WebMethod]
        public static string GetFiniquitosRecientesService(string filtrarPor, string filtro)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FILTRARPOR = filtrarPor;
            web.FILTRO = filtro;
            web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            return JsonConvert.SerializeObject(web.GetFiniquitosRecientesService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetCargoService(string fichaVigente)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.FICHA = fichaVigente;
            return JsonConvert.SerializeObject(web.GetCargoService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetObtenerMontoChequePagosService(string idDesvinculacion)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.GetObtenerMontoChequePagosService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetFiniquitosConfirmadosService(string medioPago)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.MEDIOPAGO = medioPago;
            return JsonConvert.SerializeObject(web.GetFiniquitosConfirmadosService, Formatting.Indented);
        }

        [WebMethod]
        public static string SetTMPCargaChequeFiniquitosService(string idDesvinculacion, string nominativo, string empresa)
        {
            string response = string.Empty;

            if (HttpContext.Current.Session["Usuario"] != null)
            {
                MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                Convertidor cnv = new Convertidor();

                web.IDDESVINCULACION = idDesvinculacion;
                foreach (DataRow rows in web.GetDataChequePagosService.Tables[0].Rows)
                {
                    if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                    {
                        web.TIPOPAGO = "CHEQUE";
                        web.IDEMPRESA = empresa;
                        foreach (DataRow rowsCorrelativo in web.GetObtenerCorrelativoDisponibleProveedoresService.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(rowsCorrelativo["VALIDACION"]) == 0)
                            {
                                web.NUMERODOCUMENTO = rowsCorrelativo["ID"].ToString();
                                web.NSERIECHEQUE = numeroSerieCheque(rowsCorrelativo["ID_CORRELATIVO"].ToString());
                            }
                        }

                        web.BANCO = "BANCO CHILE / EDWARDS CITY";
                        web.EMPRESA = rows["EMPRESA"].ToString();
                        web.MONTOVACACIONES = rows["FERIADOPROPORCIONAL"].ToString();
                        web.MONTOIAS = rows["MONTOIAS"].ToString();
                        web.MONTODESHAUCIO = rows["MONTODESHAUCIO"].ToString();
                        web.MONTOINDEMNIZACION = rows["MONTOINDEMNIZACION"].ToString();
                        web.MONTO = rows["MONTO_FINIQUITO"].ToString();
                        web.MONTOFINIQUITO = rows["MONTO_FINIQUITO"].ToString();
                        web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
                        web.IDDESVINCULACION = idDesvinculacion;
                        web.NOMBRETRABAJADOR = rows["NOMBRE_TRABAJADOR"].ToString();
                        web.CLIENTE = rows["CLIENTE"].ToString();
                        web.CLIENTESIGLA = rows["SIGLACLIENTE"].ToString();

                        foreach (DataRow rowsRegPago in web.SetInsertarRegistroPagoPagosService.Tables[0].Rows)
                        {
                            if (Convert.ToInt32(rowsRegPago["VALIDACION"]) == 0)
                            {
                                web.DIA = rows["DIA"].ToString();
                                web.MES = rows["MES"].ToString();
                                web.YEAR = rows["YEAR"].ToString();
                                web.CIFRA = cnv.enletras(rows["MONTO_FINIQUITO"].ToString());
                                web.CIUDAD = "Stgo";
                                web.NOMINATIVO = nominativo;
                                response = JsonConvert.SerializeObject(web.SetTMPCargaChequeFiniquitosService, Formatting.Indented);
                            }
                        }
                    }
                }
            } else {
                HttpContext.Current.Response.Redirect("Login.aspx");
            }

            return response;
        }

        [WebMethod]
        public static string SetTMPInitProcessChequeFiniquitosService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.SetTMPInitProcessChequeFiniquitosService, Formatting.Indented);
        }

        [WebMethod]
        public static string SetAnularFiniquitoService(string idDesvinculacion)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.SetAnularFolioFiniquitoService, Formatting.Indented);
        }

        [WebMethod]
        public static string SetAnularPagosService(string idDesvinculacion, string motivoAnulacion) {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            web.MOTIVOANULACION = motivoAnulacion;
            web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            return JsonConvert.SerializeObject(web.SetAnularPagoPagosService, Formatting.Indented);
        }
        
        [WebMethod]
        public static string SetFiniquitoPagadoService(string idDesvinculacion)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            return JsonConvert.SerializeObject(web.SetFiniquitoPagadoService, Formatting.Indented);
        }

        [WebMethod]
        public static string SetRevertirConfirmacionPagosService(string idDesvinculacion)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            return JsonConvert.SerializeObject(web.SetRevertirConfirmacionPagosService, Formatting.Indented);
        }

        [WebMethod]
        public static string SetNotariadoPagosService(string idDesvinculacion)
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            return JsonConvert.SerializeObject(web.SetNotariadoPagosService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetTMPValidateProcessInitFiniquitosService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetTMPValidateProcessInitFiniquitosService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetTMPChequesInProcessFiniquitoService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetTMPChequesInProcessFiniquitosService, Formatting.Indented);
        }

        #region "NUEVO METODO DE IMPRESION DE CHEQUE"

        [WebMethod]
        public static string SetTMPCloseProcessChequeFiniquitoService()
        {

            string response = string.Empty;
            string nombreArchivo = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();

            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("Cheque");
                    var worksheet = excel.Workbook.Worksheets["Cheque"];

                    /** CONFIGURACION DE HOJA DE LIBRO DE EXCEL PARA LLENADO DE DATOS ESTANDAR EN CHEQUE - BANCO CHILE / EDWARDS CITY - SERIE ABC */
                    ExcelColumn col = worksheet.Column(28);
                    col.ColumnMax = 16384;
                    col.Hidden = true;

                    #region "CONFIGURACION COLUMNAS"

                    worksheet.Column(7).Width = 186;
                    
                    #endregion

                    int contador = 1;
                    int rowExcel = 71;
                    int constante = 0;
                    int comprobante = 8;

                    foreach (DataRow rows in web.SetTMPCloseProcessChequeFiniquitosService.Tables[0].Rows)
                    {
                        if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                        {
                            constante = (92 * contador) - 92;
                            worksheet.Row(92 * contador).PageBreak = true;
                            comprobante = 8 + constante;

                            int monto = Convert.ToInt32(rows["MONTO"]);
                            string ciudad = rows["CIUDAD"].ToString();
                            string dia = rows["DIA"].ToString();
                            string mes = rows["MES"].ToString();
                            string year = rows["YEAR"].ToString();
                            string quien = rows["NOMBRETRABAJADOR"].ToString();
                            string nominativo = rows["NOMINATIVO"].ToString();
                            string cifra = rows["CIFRA"].ToString();
                            string empresa = rows["EMPRESA"].ToString();
                            string nseriecheque = rows["NSERIECHEQUE"].ToString();
                            string fechaGeneracion = rows["FECHA_GENERACION"].ToString();

                            #region "PRIMER COMPROBANTE EN CHEQUE"

                            //worksheet.Cells["E" + comprobante.ToString() + ":U" + (comprobante + 16).ToString()].Merge = true;
                            //worksheet.Cells["E" + comprobante.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            //worksheet.Cells["E" + comprobante.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["I" + comprobante.ToString()].Style.Font.Size = 26;
                            worksheet.Cells["I" + (comprobante + 2).ToString()].Style.Font.Size = 26;
                            worksheet.Cells["I" + (comprobante + 3).ToString()].Style.Font.Size = 26;
                            worksheet.Cells["I" + (comprobante + 4).ToString()].Style.Font.Size = 26;
                            worksheet.Cells["I" + (comprobante + 5).ToString()].Style.Font.Size = 26;
                            //worksheet.Cells["E" + comprobante.ToString()].Style.WrapText = true;
                            //worksheet.Cells["I" + comprobante.ToString()].Value = "GENERACIÓN DE CHEQUE PARA PAGO FINIQUITO " + rows["EMPRESA"].ToString() + 
                            //" \n \n PARA: " 
                            //+ quien + " \n \n POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0") + " \n \n EL DÍA: " + fechaGeneracion;
                            worksheet.Cells["I" + comprobante.ToString()].Value = "GENERACIÓN DE CHEQUE PARA PAGO FINIQUITO " + rows["EMPRESA"].ToString();
                            worksheet.Cells["I" + (comprobante + 2).ToString()].Value = "PARA: " + quien;
                            worksheet.Cells["I" + (comprobante + 3).ToString()].Value = "POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0");
                            worksheet.Cells["I" + (comprobante + 4).ToString()].Value = "EL DÍA: " + fechaGeneracion;
                            worksheet.Cells["I" + (comprobante + 5).ToString()].Value = "";

                            #endregion

                            #region "CONFIGURACION FILAS CHEQUE A CHEQUE"

                            worksheet.Row(comprobante).Height = 34.50;
                            worksheet.Row((comprobante + 1)).Height = 34.50;
                            worksheet.Row((comprobante + 2)).Height = 34.50;
                            worksheet.Row((comprobante + 3)).Height = 34.50;
                            worksheet.Row((comprobante + 4)).Height = 34.50;
                            worksheet.Row((comprobante + 5)).Height = 34.50;
                            worksheet.Row((comprobante + 61)).Height = 28.50;
                            worksheet.Row((comprobante + 62)).Height = 72.00; // row 70
                            worksheet.Row((comprobante + 63)).Height = 42.00;
                            worksheet.Row((comprobante + 64)).Height = 25.50;
                            worksheet.Row((comprobante + 65)).Height = 39.00;
                            worksheet.Row((comprobante + 66)).Height = 4.50;
                            worksheet.Row((comprobante + 67)).Height = 2.25;
                            worksheet.Row((comprobante + 68)).Height = 21.75;
                            worksheet.Row((comprobante + 69)).Height = 34.50;
                            worksheet.Row((comprobante + 70)).Height = 10.50;
                            worksheet.Row((comprobante + 71)).Height = 27.00;
                            worksheet.Row((comprobante + 72)).Height = 6.00; // row 80
                            worksheet.Row((comprobante + 73)).Height = 18.00;
                            worksheet.Row((comprobante + 74)).Height = 15.00;
                            worksheet.Row((comprobante + 75)).Height = 15.00;
                            worksheet.Row((comprobante + 76)).Height = 15.00;
                            worksheet.Row((comprobante + 77)).Height = 15.00;
                            worksheet.Row((comprobante + 78)).Height = 6.00;
                            worksheet.Row((comprobante + 79)).Height = 4.50;
                            worksheet.Row((comprobante + 80)).Height = 6.00;
                            worksheet.Row((comprobante + 81)).Height = 21.75;
                            worksheet.Row((comprobante + 82)).Height = 28.50; // row 90
                            worksheet.Row((comprobante + 83)).Height = 34.50;

                            #endregion

                            /** LLENADO DE DATOS DINAMICOS PARA CREACION DE DOCUMENTO */

                            #region "MONTO DEL CHEQUE"

                            string montoSet = "";

                            worksheet.Cells["T" + (rowExcel + constante).ToString() + ":Z" + (rowExcel + constante).ToString()].Merge = true;

                            switch (monto.ToString().Length)
                            {
                                case 1:
                                    montoSet = monto + " =";
                                    break;
                                case 2:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) + 
                                               " =";
                                    break;
                                case 3:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) +
                                               "  " +
                                               monto.ToString().Substring(2, 1) +
                                               " =";
                                    break;
                                case 4:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               " .  " +
                                               monto.ToString().Substring(1, 1) +
                                               "  " +
                                               monto.ToString().Substring(2, 1) +
                                               "  " +
                                               monto.ToString().Substring(3, 1) +
                                               " =";
                                    break;
                                case 5:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) +
                                               " .  " +
                                               monto.ToString().Substring(2, 1) +
                                               "  " +
                                               monto.ToString().Substring(3, 1) +
                                               "  " +
                                               monto.ToString().Substring(4, 1) +
                                               " =";
                                    break;
                                case 6:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) +
                                               "  " +
                                               monto.ToString().Substring(2, 1) +
                                               " .  " +
                                               monto.ToString().Substring(3, 1) +
                                               "  " +
                                               monto.ToString().Substring(4, 1) +
                                               "  " +
                                               monto.ToString().Substring(5, 1) +
                                               " =";
                                    break;
                                case 7:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               " .  " +
                                               monto.ToString().Substring(1, 1) +
                                               "  " +
                                               monto.ToString().Substring(2, 1) +
                                               "  " +
                                               monto.ToString().Substring(3, 1) +
                                               " .  " +
                                               monto.ToString().Substring(4, 1) +
                                               "  " +
                                               monto.ToString().Substring(5, 1) +
                                               "  " +
                                               monto.ToString().Substring(6, 1) +
                                               " =";
                                    break;
                                case 8:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) +
                                               " .  " +
                                               monto.ToString().Substring(2, 1) +
                                               "  " +
                                               monto.ToString().Substring(3, 1) +
                                               "  " +
                                               monto.ToString().Substring(4, 1) +
                                               " .  " +
                                               monto.ToString().Substring(5, 1) +
                                               "  " +
                                               monto.ToString().Substring(6, 1) +
                                               "  " +
                                               monto.ToString().Substring(7, 1) +
                                               " =";
                                    break;
                                case 9:
                                    montoSet = monto.ToString().Substring(0, 1) +
                                               "  " +
                                               monto.ToString().Substring(1, 1) +
                                               "  " +
                                               monto.ToString().Substring(2, 1) +
                                               " .  " +
                                               monto.ToString().Substring(3, 1) +
                                               "  " +
                                               monto.ToString().Substring(4, 1) +
                                               "  " +
                                               monto.ToString().Substring(5, 1) +
                                               " .  " +
                                               monto.ToString().Substring(6, 1) +
                                               "  " +
                                               monto.ToString().Substring(7, 1) +
                                               "  " +
                                               monto.ToString().Substring(7, 1) +
                                               " =";
                                    break;
                            }

                            worksheet.Cells["T" + (rowExcel + constante).ToString()].Value = montoSet;
                            worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.Font.Size = 30;

                            #endregion

                            #region "CIUDAD Y FECHA DEL CHEQUE"

                            worksheet.Cells["R" + ((rowExcel + 2) + constante).ToString() + ":T" + ((rowExcel + 2) + constante).ToString()].Merge = true;
                            worksheet.Cells["R" + ((rowExcel + 2) + constante).ToString()].Value = ciudad;
                            worksheet.Cells["R" + ((rowExcel + 2) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["R" + ((rowExcel + 2) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["R" + ((rowExcel + 2) + constante).ToString()].Style.Font.Size = 30;

                            string fechaSet = "";

                            worksheet.Cells["U" + ((rowExcel + 2) + constante).ToString() + ":Z" + ((rowExcel + 2) + constante).ToString()].Merge = true;
                            worksheet.Cells["U" + ((rowExcel + 2) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["U" + ((rowExcel + 2) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["U" + ((rowExcel + 2) + constante).ToString()].Style.Font.Size = 30;

                            if (dia.Length == 2)
                            {
                                fechaSet = "   " + dia.Substring(0, 1) + "  " + dia.Substring(1, 1);
                            }
                            else
                            {
                                fechaSet = "   0  " + dia;
                            }

                            if (mes.Length == 2)
                            {
                                fechaSet = fechaSet + "     " + mes.Substring(0, 1) + "  " + mes.Substring(1, 1);
                            }
                            else
                            {
                                fechaSet = fechaSet + "     0  " + mes;
                            }

                            fechaSet = fechaSet + "     " + year.Substring(0, 1) + "   " + year.Substring(1, 1) + "  " + year.Substring(2, 1) + "  " + year.Substring(3, 1);

                            worksheet.Cells["U" + ((rowExcel + 2) + constante).ToString()].Value = fechaSet;

                            #endregion

                            #region "MONINATIVO CHEQUE"

                            if (nominativo == "S")
                            {
                                var shape = worksheet.Drawings.AddShape("myshape" + contador, eShapeStyle.Rect);
                                shape.SetPosition(76 + constante, 30, 7, 12);
                                shape.SetSize(103, 9);
                                shape.Fill.Color = Color.Black;
                                shape.Border.Fill.Color = Color.Black;
                            }

                            #endregion

                            #region "PERSONA PAGO"

                            worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString() + ":Z" + ((rowExcel + 6) + constante).ToString()].Merge = true;
                            worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                            if (quien.Length > 55)
                            {
                                worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Value = "                   " + quien;
                                worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Style.Font.Size = 20;
                            }
                            else
                            {
                                worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Value = "                 " + quien;
                                worksheet.Cells["H" + ((rowExcel + 6) + constante).ToString()].Style.Font.Size = 26;
                            }

                            #endregion

                            #region "MONTO CIFRA DE CHEQUE"

                            worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString() + ":Z" + ((rowExcel + 9) + constante).ToString()].Merge = true;
                            worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Style.Font.Size = 26;

                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString() + ":Z" + ((rowExcel + 11) + constante).ToString()].Merge = true;
                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Style.Font.Size = 26;

                            if (cifra.Length <= 56)
                            {
                                worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Value = "                 " + cifra + " =====";
                            }
                            else
                            {
                                /** ALGORITMO DE DIVISIÓN DE CIFRA */
                                string cifraWithSeparatorInit = cifra.Substring(0, 55);
                                string cifraWithSeparatorEnd = cifra.Substring(56, (cifra.Length - 56));

                                string comprobateEmpty = cifra.Substring(55, 1);
                                string cifraWithSeparator = string.Empty;

                                if (comprobateEmpty == " ")
                                {
                                    cifraWithSeparator = cifraWithSeparatorInit + "; " + cifraWithSeparatorEnd;
                                }
                                else
                                {
                                    cifraWithSeparator = cifraWithSeparatorInit + ";" + cifraWithSeparatorEnd;
                                }

                                string[] arrayCifra = cifraWithSeparator.Split(' ');
                                int contadorCifra = 0;
                                int encontrado = 0;
                                foreach (string cifraUnica in arrayCifra)
                                {
                                    if (cifraUnica.Contains(";"))
                                    {
                                        encontrado = contadorCifra;
                                    }

                                    contadorCifra = contadorCifra + 1;
                                }
                                
                                for (int i = 0; i < encontrado; i++)
                                {
                                    if (i == 0)
                                    {
                                        worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Value = "                 " + arrayCifra[i] + " ";
                                    }

                                    if (i > 0)
                                    {
                                        worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Value = worksheet.Cells["H" + ((rowExcel + 8) + constante).ToString()].Value + " " + arrayCifra[i] + " ";
                                    }
                                }

                                for (int i = encontrado; i < arrayCifra.Length; i++)
                                {
                                    if ((arrayCifra.Length - encontrado) == 1)
                                    {
                                        worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value = " " + worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value + arrayCifra[i].Replace(";", comprobateEmpty) + " =====";
                                    }
                                    else
                                    {
                                        if (i == encontrado)
                                        {
                                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value = " " + worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value + arrayCifra[i].Replace(";", comprobateEmpty) + " ";
                                        }

                                        if (i > encontrado && (i + 1) < arrayCifra.Length)
                                        {
                                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value = worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value + arrayCifra[i] + " ";
                                        }

                                        if ((i + 1) == arrayCifra.Length)
                                        {
                                            worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value = worksheet.Cells["H" + ((rowExcel + 10) + constante).ToString()].Value + arrayCifra[i] + " =====";
                                        }
                                    }
                                }

                            }

                            #endregion

                            #region "PROTECCION DE CHEQUE"

                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString() + ":Z" + ((rowExcel + 21) + constante).ToString()].Merge = true;
                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString()].Style.Font.Size = 40;
                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString()].Style.Font.Bold = true;

                            string montoProtegido = "";

                            switch (monto.ToString().Length)
                            {
                                case 1:
                                    montoProtegido = monto + " =";
                                    break;
                                case 2:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " =";
                                    break;
                                case 3:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " =";
                                    break;
                                case 4:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " =";
                                    break;
                                case 5:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " " +
                                                     monto.ToString().Substring(4, 1) +
                                                     " =";
                                    break;
                                case 6:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " " +
                                                     monto.ToString().Substring(4, 1) +
                                                     " " +
                                                     monto.ToString().Substring(5, 1) +
                                                     " =";
                                    break;
                                case 7:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(4, 1) +
                                                     " " +
                                                     monto.ToString().Substring(5, 1) +
                                                     " " +
                                                     monto.ToString().Substring(6, 1) +
                                                     " =";
                                    break;
                                case 8:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " " +
                                                     monto.ToString().Substring(4, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(5, 1) +
                                                     " " +
                                                     monto.ToString().Substring(6, 1) +
                                                     " " +
                                                     monto.ToString().Substring(7, 1) +
                                                     " =";
                                    break;
                                case 9:
                                    montoProtegido = monto.ToString().Substring(0, 1) +
                                                     " " +
                                                     monto.ToString().Substring(1, 1) +
                                                     " " +
                                                     monto.ToString().Substring(2, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(3, 1) +
                                                     " " +
                                                     monto.ToString().Substring(4, 1) +
                                                     " " +
                                                     monto.ToString().Substring(5, 1) +
                                                     " . " +
                                                     monto.ToString().Substring(6, 1) +
                                                     " " +
                                                     monto.ToString().Substring(7, 1) +
                                                     " " +
                                                     monto.ToString().Substring(7, 1) +
                                                     " =";
                                    break;
                            }

                            worksheet.Cells["K" + ((rowExcel + 20) + constante).ToString()].Value = "$ " + montoProtegido;

                            #endregion

                            #region "SEGUNDO COMPROBANTE CHEQUE, COSTADO INFERIOR IZQUIERO"

                            worksheet.Cells["A" + (comprobante + 66).ToString() + ":E" + (comprobante + 82).ToString()].Merge = true;
                            worksheet.Cells["A" + (comprobante + 66).ToString()].Style.Font.Size = 20;
                            worksheet.Cells["A" + (comprobante + 66).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                            worksheet.Cells["A" + (comprobante + 66).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["A" + (comprobante + 66).ToString()].Style.WrapText = true;

                            worksheet.Cells["A" + (comprobante + 66).ToString()].Value = quien + "\n" +
                                                                                         "MONTO $ " + Convert.ToInt32(monto).ToString("N0") + "\n" +
                                                                                         "EMISIÓN " + fechaGeneracion + "\n" +
                                                                                         "PAGO FINIQUITO";

                            #endregion
                        }


                        contador = contador + 1;
                    }

                    nombreArchivo = "TIPO_CHEQUE_" + DateTime.Now.ToString().Replace("-", "").Replace("/", "").Split(' ')[0] + "_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Split(' ')[1] + ".xlsx";

                    /** END LLENADO DE DATOS */

                    /** CONFIGURACION DE AREA DE IMPRESION DE CHEQUE SOBRE IMPRESORA OKI DATA CORP ML320/1TURBO */
                    worksheet.PrinterSettings.LeftMargin = 0M / 2.54M;
                    worksheet.PrinterSettings.RightMargin = 0M / 2.54M;
                    worksheet.PrinterSettings.TopMargin = 0.8M / 2.54M;
                    worksheet.PrinterSettings.BottomMargin = 0.5M / 2.54M;
                    worksheet.PrinterSettings.HeaderMargin = 1.6M / 2.54M;
                    worksheet.PrinterSettings.FooterMargin = 0M / 2.54M;

                    worksheet.PrinterSettings.FitToPage = true;
                    worksheet.PrinterSettings.FitToWidth = 1;
                    worksheet.PrinterSettings.FitToHeight = 0;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.Letter;
                    worksheet.PrinterSettings.VerticalCentered = true;
                    worksheet.Protection.IsProtected = true;
                    worksheet.Protection.SetPassword("Satw.2018");

                    /** DESCARGA DE ARCHIVO .XLSX HACIA EQUIPO DEL USUARIO **/
                    byte[] data = excel.GetAsByteArray();

                    /** PARA CAIDA DE ARCHIVOS CHEQUE */
                    string ruta = @"\\sistema\FileServer\Sistemas\Ficha_Alta\Cheques\Finiquitos\";

                    string diaHoy = DateTime.Now.Day.ToString();
                    string mesHoy = DateTime.Now.Month.ToString();
                    string yearHoy = DateTime.Now.Year.ToString();

                    string nombreCarpeta = diaHoy + " de " + convertMonthToString(mesHoy) + " de " + yearHoy + @"\";
                    string rutaCarpeta = ruta + nombreCarpeta;
                    if (!Directory.Exists(rutaCarpeta))
                    {
                        Directory.CreateDirectory(ruta + nombreCarpeta);
                    }
                    string path = ruta + nombreCarpeta + nombreArchivo;
                    //string path = ruta + nombreArchivo;
                    File.WriteAllBytes(path, data);

                    response = nombreArchivo;
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;

        }

        #endregion

        /** METODO ACTUAL DE PRODUCCION */
        #region "METODO ACTUAL PRODUCCION"
        //[WebMethod]
        //public static string SetTMPCloseProcessChequeFiniquitoService()
        //{
        //    string response = string.Empty;
        //    string nombreArchivo = string.Empty;
        //    MethodServiceFiniquitos web = new MethodServiceFiniquitos();

        //    try
        //    {
        //        using (ExcelPackage excel = new ExcelPackage())
        //        {
        //            excel.Workbook.Worksheets.Add("Cheque");
        //            var worksheet = excel.Workbook.Worksheets["Cheque"];

        //            /** CONFIGURACION DE HOJA DE LIBRO DE EXCEL PARA LLENADO DE DATOS ESTANDAR EN CHEQUE - BANCO CHILE / EDWARDS CITY - SERIE ABC */
        //            ExcelColumn col = worksheet.Column(25);
        //            col.ColumnMax = 16384;
        //            col.Hidden = true;

        //            worksheet.Column(1).Width = 11.58;
        //            worksheet.Column(2).Width = 11.58;
        //            worksheet.Column(3).Width = 11.58;
        //            worksheet.Column(4).Width = 11.58;
        //            worksheet.Column(5).Width = 13.43;
        //            worksheet.Column(6).Width = 11.72;
        //            worksheet.Column(7).Width = 9.72;
        //            worksheet.Column(8).Width = 11.58;
        //            worksheet.Column(9).Width = 11.58;
        //            worksheet.Column(10).Width = 11.58;
        //            worksheet.Column(11).Width = 4.15;
        //            worksheet.Column(12).Width = 5.15;
        //            worksheet.Column(13).Width = 5.15;
        //            worksheet.Column(14).Width = 5.15;
        //            worksheet.Column(15).Width = 5.15;
        //            worksheet.Column(16).Width = 5.15;
        //            worksheet.Column(17).Width = 5.15;
        //            worksheet.Column(18).Width = 5.15;
        //            worksheet.Column(19).Width = 5.15;
        //            worksheet.Column(20).Width = 5.15;
        //            worksheet.Column(21).Width = 5.15;
        //            worksheet.Column(22).Width = 5.15;
        //            worksheet.Column(23).Width = 5.15;
        //            worksheet.Column(24).Width = 3.28;

        //            int contador = 1;
        //            int rowExcel = 73;
        //            int constante = 0;
        //            int comprobante = 7;

        //            foreach (DataRow rows in web.SetTMPCloseProcessChequeFiniquitosService.Tables[0].Rows)
        //            {
        //                if (Convert.ToInt32(rows["VALIDACION"]) == 0)
        //                {
        //                    constante = (90 * contador) - 90;
        //                    worksheet.Row(90 * contador).PageBreak = true;
        //                    comprobante = 7 + constante;

        //                    int monto = Convert.ToInt32(rows["MONTO"]);
        //                    string ciudad = rows["CIUDAD"].ToString();
        //                    string dia = rows["DIA"].ToString();
        //                    string mes = rows["MES"].ToString();
        //                    string year = rows["YEAR"].ToString();
        //                    string quien = rows["NOMBRETRABAJADOR"].ToString();
        //                    string nominativo = rows["NOMINATIVO"].ToString();
        //                    string cifra = rows["CIFRA"].ToString();
        //                    string empresa = rows["EMPRESA"].ToString();
        //                    string nseriecheque = rows["NSERIECHEQUE"].ToString();
        //                    string fechaGeneracion = rows["FECHA_GENERACION"].ToString();

        //                    worksheet.Cells["E" + comprobante.ToString() + ":U" + (comprobante + 16).ToString()].Merge = true;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.Font.Size = 22;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.WrapText = true;
        //                    worksheet.Cells["E" + comprobante.ToString()].Value = "GENERACIÓN DE CHEQUE PARA PAGO FINIQUITO " + rows["EMPRESA"].ToString() + " \n \n PARA: " + quien + " \n \n POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0") + " \n \n EL DÍA: " + fechaGeneracion;

        //                    worksheet.Cells["M" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["M" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["M" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["N" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["N" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["N" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["O" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["O" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["O" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["P" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["P" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["P" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["Q" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["Q" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["Q" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["R" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["R" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["R" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["S" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["S" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["S" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["T" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["U" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["U" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["U" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["V" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["V" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["V" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["W" + (rowExcel + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["W" + (rowExcel + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["W" + (rowExcel + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;

        //                    worksheet.Cells["L" + ((rowExcel + 3) + constante).ToString() + ":M" + ((rowExcel + 3) + constante).ToString()].Merge = true; // + 3
        //                    worksheet.Cells["L" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 22;

        //                    worksheet.Cells["L" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["L" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        //                    worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString() + ":P" + ((rowExcel + 3) + constante).ToString()].Merge = true;
        //                    worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;

        //                    worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        //                    worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString() + ":S" + ((rowExcel + 3) + constante).ToString()].Merge = true;
        //                    worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;

        //                    worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Style.Font.Size = 26;
        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString() + ":W" + ((rowExcel + 5) + constante).ToString()].Merge = true; // + 5
        //                    worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Style.Font.Size = 22;
        //                    worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString() + ":W" + ((rowExcel + 7) + constante).ToString()].Merge = true; //+ 7
        //                    worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Style.Font.Size = 22;
        //                    worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                    worksheet.Row((rowExcel - 2) + constante).Height = 15.00;
        //                    worksheet.Row((rowExcel - 1) + constante).Height = 15.00;
        //                    worksheet.Row(rowExcel + constante).Height = 33.75;
        //                    worksheet.Row((rowExcel + 1) + constante).Height = 3.5;
        //                    worksheet.Row((rowExcel + 2) + constante).Height = 17.00;
        //                    worksheet.Row((rowExcel + 3) + constante).Height = 33.75;
        //                    worksheet.Row((rowExcel + 4) + constante).Height = 15.00;
        //                    worksheet.Row((rowExcel + 5) + constante).Height = 28.50;
        //                    worksheet.Row((rowExcel + 6) + constante).Height = 15.00;
        //                    worksheet.Row((rowExcel + 7) + constante).Height = 28.50;

        //                    /** LLENADO DE DATOS DINAMICOS PARA CREACION DE DOCUMENTO */

        //                    if (monto.ToString().Length > 0)
        //                    {
        //                        worksheet.Cells["M" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(0, 1);
        //                    }

        //                    if (monto.ToString().Length > 1)
        //                    {
        //                        worksheet.Cells["N" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(1, 1);
        //                    }

        //                    if (monto.ToString().Length > 2)
        //                    {
        //                        worksheet.Cells["O" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(2, 1);
        //                    }

        //                    if (monto.ToString().Length > 3)
        //                    {
        //                        worksheet.Cells["P" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(3, 1);
        //                    }

        //                    if (monto.ToString().Length > 4)
        //                    {
        //                        worksheet.Cells["Q" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(4, 1);
        //                    }

        //                    if (monto.ToString().Length > 5)
        //                    {
        //                        worksheet.Cells["R" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(5, 1);
        //                    }

        //                    if (monto.ToString().Length > 6)
        //                    {
        //                        worksheet.Cells["S" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(6, 1);
        //                    }

        //                    if (monto.ToString().Length > 7)
        //                    {
        //                        worksheet.Cells["T" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(7, 1);
        //                    }

        //                    if (monto.ToString().Length > 8)
        //                    {
        //                        worksheet.Cells["U" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(8, 1);
        //                    }

        //                    if (monto.ToString().Length > 9)
        //                    {
        //                        worksheet.Cells["V" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(9, 1);
        //                    }

        //                    if (monto.ToString().Length > 10)
        //                    {
        //                        worksheet.Cells["W" + (rowExcel + constante).ToString()].Value = monto.ToString().Substring(10, 1);
        //                    }

        //                    if (monto.ToString().Length > 3 && monto.ToString().Length <= 6)
        //                    {
        //                        switch (monto.ToString().Length)
        //                        {
        //                            case 4:
        //                                worksheet.Cells["M" + (rowExcel + constante).ToString()].Value = worksheet.Cells["M" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                            case 5:
        //                                worksheet.Cells["N" + (rowExcel + constante).ToString()].Value = worksheet.Cells["N" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                            case 6:
        //                                worksheet.Cells["O" + (rowExcel + constante).ToString()].Value = worksheet.Cells["O" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                        }
        //                    }

        //                    if (monto.ToString().Length >= 7 && monto.ToString().Length <= 10)
        //                    {
        //                        switch (monto.ToString().Length)
        //                        {
        //                            case 7:
        //                                worksheet.Cells["M" + (rowExcel + constante).ToString()].Value = worksheet.Cells["M" + (rowExcel + constante).ToString()].Value + ".";
        //                                worksheet.Cells["P" + (rowExcel + constante).ToString()].Value = worksheet.Cells["P" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                            case 8:
        //                                worksheet.Cells["N" + (rowExcel + constante).ToString()].Value = worksheet.Cells["N" + (rowExcel + constante).ToString()].Value + ".";
        //                                worksheet.Cells["Q" + (rowExcel + constante).ToString()].Value = worksheet.Cells["Q" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                            case 9:
        //                                worksheet.Cells["O" + (rowExcel + constante).ToString()].Value = worksheet.Cells["O" + (rowExcel + constante).ToString()].Value + ".";
        //                                worksheet.Cells["R" + (rowExcel + constante).ToString()].Value = worksheet.Cells["R" + (rowExcel + constante).ToString()].Value + ".";
        //                                break;
        //                        }
        //                    }

        //                    switch (monto.ToString().Length)
        //                    {
        //                        case 1:
        //                            worksheet.Cells["N" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 2:
        //                            worksheet.Cells["O" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 3:
        //                            worksheet.Cells["P" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 4:
        //                            worksheet.Cells["Q" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 5:
        //                            worksheet.Cells["R" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 6:
        //                            worksheet.Cells["S" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 7:
        //                            worksheet.Cells["T" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 8:
        //                            worksheet.Cells["U" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                        case 9:
        //                            worksheet.Cells["V" + (rowExcel + constante).ToString()].Value = "=";
        //                            break;
        //                    }

        //                    worksheet.Cells["L" + ((rowExcel + 3) + constante).ToString()].Value = ciudad;

        //                    if (dia.Length == 2)
        //                    { // + 3
        //                        worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString()].Value = dia.Substring(0, 1) + "   " + dia.Substring(1, 1);
        //                    }
        //                    else
        //                    {
        //                        worksheet.Cells["O" + ((rowExcel + 3) + constante).ToString()].Value = "0   " + dia.Substring(0, 1);
        //                    }

        //                    if (mes.Length == 2)
        //                    {
        //                        worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString()].Value = mes.Substring(0, 1) + "   " + mes.Substring(1, 1);
        //                    }
        //                    else
        //                    {
        //                        worksheet.Cells["Q" + ((rowExcel + 3) + constante).ToString()].Value = "0   " + mes.Substring(0, 1);
        //                    }

        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Value = year.Substring(0, 1);
        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["T" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Value = year.Substring(1, 1);
        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["U" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Value = year.Substring(2, 1);
        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["V" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Value = year.Substring(3, 1);
        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["W" + ((rowExcel + 3) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

        //                    if (nominativo == "S")
        //                    {
        //                        var shape = worksheet.Drawings.AddShape("myshape" + contador, eShapeStyle.Rect);
        //                        shape.SetPosition(77 + constante, 30, 3, 0);
        //                        shape.SetSize(80, 5);
        //                        shape.Fill.Color = Color.Black;
        //                        shape.Border.Fill.Color = Color.Black;
        //                    }

        //                    if (quien.Length > 55)
        //                    {
        //                        worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Style.Font.Size = 20;
        //                        worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Value = "                                             " + quien;
        //                    }
        //                    else
        //                    {
        //                        worksheet.Cells["B" + ((rowExcel + 5) + constante).ToString()].Value = "                                             " + quien;
        //                    }

        //                    if (cifra.Length <= 56)
        //                    {// + 7
        //                        worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value = "                                        " + cifra + " =====";

        //                    }
        //                    else
        //                    {
        //                        /** ALGORITMO DE DIVISIÓN DE CIFRA */
        //                        string cifraWithSeparatorInit = cifra.Substring(0, 55);
        //                        string cifraWithSeparatorEnd = cifra.Substring(56, (cifra.Length - 56));

        //                        string comprobateEmpty = cifra.Substring(55, 1);
        //                        string cifraWithSeparator = string.Empty;

        //                        if (comprobateEmpty == " ")
        //                        {
        //                            cifraWithSeparator = cifraWithSeparatorInit + "; " + cifraWithSeparatorEnd;
        //                        }
        //                        else
        //                        {
        //                            cifraWithSeparator = cifraWithSeparatorInit + ";" + cifraWithSeparatorEnd;
        //                        }

        //                        string[] arrayCifra = cifraWithSeparator.Split(' ');
        //                        int contadorCifra = 0;
        //                        int encontrado = 0;
        //                        foreach (string cifraUnica in arrayCifra)
        //                        {
        //                            if (cifraUnica.Contains(";"))
        //                            {
        //                                encontrado = contadorCifra;
        //                            }

        //                            contadorCifra = contadorCifra + 1;
        //                        }

        //                        worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString() + ":W" + ((rowExcel + 9) + constante).ToString()].Merge = true;
        //                        worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                        worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

        //                        worksheet.Cells["B" + ((rowExcel + 8) + constante).ToString()].Style.Font.Size = 23;
        //                        worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Style.Font.Size = 23;
        //                        worksheet.Cells["B" + ((rowExcel + 8) + constante).ToString()].Style.Font.Size = 23;

        //                        for (int i = 0; i < encontrado; i++)
        //                        {
        //                            if (i == 0)
        //                            {
        //                                worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value = "                                        " + arrayCifra[i] + " ";
        //                            }

        //                            if (i > 0)
        //                            {
        //                                worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value = worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value + " " + arrayCifra[i] + " ";
        //                            }
        //                        }

        //                        for (int i = encontrado; i < arrayCifra.Length; i++)
        //                        {
        //                            if ((arrayCifra.Length - encontrado) == 1)
        //                            {
        //                                worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value = worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value + arrayCifra[i].Replace(";", comprobateEmpty) + " =====";
        //                            }
        //                            else
        //                            {
        //                                if (i == encontrado)
        //                                {
        //                                    worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value = worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value + arrayCifra[i].Replace(";", comprobateEmpty) + " ";
        //                                }

        //                                if (i > encontrado && (i + 1) < arrayCifra.Length)
        //                                {
        //                                    worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value = worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value + arrayCifra[i] + " ";
        //                                }

        //                                if ((i + 1) == arrayCifra.Length)
        //                                {
        //                                    worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value = worksheet.Cells["D" + ((rowExcel + 8) + constante).ToString()].Value + arrayCifra[i] + " =====";
        //                                }
        //                            }
        //                        }

        //                    }
        //                }


        //                contador = contador + 1;
        //            }

        //            nombreArchivo = "TIPO_CHEQUE_" + DateTime.Now.ToString().Replace("-", "").Replace("/", "").Split(' ')[0] + "_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Split(' ')[1] + ".xlsx";

        //            /** END LLENADO DE DATOS */

        //            /** CONFIGURACION DE AREA DE IMPRESION DE CHEQUE SOBRE IMPRESORA OKI DATA CORP ML320/1TURBO */
        //            worksheet.PrinterSettings.LeftMargin = 3.0M / 2.54M;
        //            worksheet.PrinterSettings.RightMargin = 1M / 2.54M;
        //            worksheet.PrinterSettings.TopMargin = 2.2M / 2.54M;
        //            worksheet.PrinterSettings.BottomMargin = 1.5M / 2.54M;
        //            worksheet.PrinterSettings.HeaderMargin = 0.8M / 2.54M;
        //            worksheet.PrinterSettings.FooterMargin = 0.8M / 2.54M;

        //            worksheet.PrinterSettings.FitToPage = true;
        //            worksheet.PrinterSettings.FitToWidth = 1;
        //            worksheet.PrinterSettings.FitToHeight = 0;
        //            worksheet.PrinterSettings.PaperSize = ePaperSize.Letter;
        //            worksheet.PrinterSettings.HorizontalCentered = true;
        //            worksheet.Protection.IsProtected = true;
        //            worksheet.Protection.SetPassword("Satw.2018");

        //            /** DESCARGA DE ARCHIVO .XLSX HACIA EQUIPO DEL USUARIO **/
        //            byte[] data = excel.GetAsByteArray();

        //            /** PARA CAIDA DE ARCHIVOS CHEQUE */
        //            string ruta = @"\\SATW\CHEQUES\Finiquitos\";

        //            string diaHoy = DateTime.Now.Day.ToString();
        //            string mesHoy = DateTime.Now.Month.ToString();
        //            string yearHoy = DateTime.Now.Year.ToString();

        //            string nombreCarpeta = diaHoy + " de " + convertMonthToString(mesHoy) + " de " + yearHoy + @"\";
        //            string rutaCarpeta = ruta + nombreCarpeta;
        //            if (!Directory.Exists(rutaCarpeta))
        //            {
        //                Directory.CreateDirectory(ruta + nombreCarpeta);
        //            }
        //            string path = ruta + nombreCarpeta + nombreArchivo;
        //            //string path = ruta + nombreArchivo;
        //            File.WriteAllBytes(path, data);

        //            response = nombreArchivo;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response = ex.Message;
        //    }

        //    return response;

        //}

        #endregion

        /** END METODO ACTUAL DE PRODUCCION */

        private static string convertMonthToString(string mes)
        {
            string monthString = string.Empty;

            switch (mes)
            {
                case "1":
                    monthString = "Enero";
                    break;
                case "2":
                    monthString = "Febrero";
                    break;
                case "3":
                    monthString = "Marzo";
                    break;
                case "4":
                    monthString = "Abril";
                    break;
                case "5":
                    monthString = "Mayo";
                    break;
                case "6":
                    monthString = "Junio";
                    break;
                case "7":
                    monthString = "Julio";
                    break;
                case "8":
                    monthString = "Agosto";
                    break;
                case "9":
                    monthString = "Septiembre";
                    break;
                case "10":
                    monthString = "octubre";
                    break;
                case "11":
                    monthString = "Noviembre";
                    break;
                case "12":
                    monthString = "Diciembre";
                    break;
            }

            return monthString;
        }
        
        /** -- METODO ANTIGUO -- */

        [WebMethod()]
        public static string GenerarFormatoCheque(string idDesvinculacion) {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDDESVINCULACION = idDesvinculacion;
            //web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            Convertidor cnv = new Convertidor();
            string nombreArchivo = string.Empty;

            try
            {
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Worksheets.Add("Cheque");
                    var worksheet = excel.Workbook.Worksheets["Cheque"];

                    /** CONFIGURACION DE HOJA DE LIBRO DE EXCEL PARA LLENADO DE DATOS ESTANDAR EN CHEQUE - BANCO CHILE / EDWARDS CITY - SERIE ABC */
                    ExcelColumn col = worksheet.Column(25);
                    col.ColumnMax = 16384;
                    col.Hidden = true;

                    //for (int i = 90; i < 1048577; i++)
                    //{
                    //    worksheet.Row(i).Hidden = true;
                    //}

                    worksheet.Column(1).Width = 11.58;
                    worksheet.Column(2).Width = 11.58;
                    worksheet.Column(3).Width = 11.58;
                    worksheet.Column(4).Width = 11.58;
                    worksheet.Column(5).Width = 13.43;
                    worksheet.Column(6).Width = 11.72;
                    worksheet.Column(7).Width = 9.72;
                    worksheet.Column(8).Width = 11.58;
                    worksheet.Column(9).Width = 11.58;
                    worksheet.Column(10).Width = 11.58;
                    worksheet.Column(11).Width = 4.15;
                    worksheet.Column(12).Width = 5.15;
                    worksheet.Column(13).Width = 5.15;
                    worksheet.Column(14).Width = 5.15;
                    worksheet.Column(15).Width = 5.15;
                    worksheet.Column(16).Width = 5.15;
                    worksheet.Column(17).Width = 5.15;
                    worksheet.Column(18).Width = 5.15;
                    worksheet.Column(19).Width = 5.15;
                    worksheet.Column(20).Width = 5.15;
                    worksheet.Column(21).Width = 5.15;
                    worksheet.Column(22).Width = 5.15;
                    worksheet.Column(23).Width = 5.15;
                    worksheet.Column(24).Width = 3.28;

                    worksheet.Row(71).Height = 4.50;
                    worksheet.Row(72).Height = 16.50;
                    worksheet.Row(73).Height = 33.00;
                    worksheet.Row(74).Height = 10.50;
                    worksheet.Row(75).Height = 13.50;
                    worksheet.Row(76).Height = 30.75;
                    worksheet.Row(77).Height = 22.50;
                    worksheet.Row(78).Height = 27.00;
                    worksheet.Row(79).Height = 12.00;
                    worksheet.Row(80).Height = 27.75;

                    worksheet.Cells["M73"].Style.Font.Size = 26;
                    worksheet.Cells["M73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["M73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["N73"].Style.Font.Size = 26;
                    worksheet.Cells["N73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["N73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["O73"].Style.Font.Size = 26;
                    worksheet.Cells["O73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["O73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["P73"].Style.Font.Size = 26;
                    worksheet.Cells["P73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["P73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["Q73"].Style.Font.Size = 26;
                    worksheet.Cells["Q73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["Q73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["R73"].Style.Font.Size = 26;
                    worksheet.Cells["R73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["R73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["S73"].Style.Font.Size = 26;
                    worksheet.Cells["S73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["S73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["T73"].Style.Font.Size = 26;
                    worksheet.Cells["T73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["T73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["U73"].Style.Font.Size = 26;
                    worksheet.Cells["U73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["U73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["V73"].Style.Font.Size = 26;
                    worksheet.Cells["V73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["V73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["W73"].Style.Font.Size = 26;
                    worksheet.Cells["W73"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["W73"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["L76:M76"].Merge = true;
                    worksheet.Cells["L76"].Style.Font.Size = 22;

                    worksheet.Cells["L76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["L76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells["O76:P76"].Merge = true;
                    worksheet.Cells["O76"].Style.Font.Size = 26;

                    worksheet.Cells["O76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["O76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells["Q76:S76"].Merge = true;
                    worksheet.Cells["Q76"].Style.Font.Size = 26;

                    worksheet.Cells["Q76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["Q76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                    worksheet.Cells["T76"].Style.Font.Size = 26;
                    worksheet.Cells["T76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["T76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["U76"].Style.Font.Size = 26;
                    worksheet.Cells["U76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["U76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["V76"].Style.Font.Size = 26;
                    worksheet.Cells["V76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["V76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["W76"].Style.Font.Size = 26;
                    worksheet.Cells["W76"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["W76"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["B78:W78"].Merge = true;
                    worksheet.Cells["B78"].Style.Font.Size = 22;
                    worksheet.Cells["B78"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["B78"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                    worksheet.Cells["B80:W80"].Merge = true;
                    worksheet.Cells["B80"].Style.Font.Size = 22;
                    worksheet.Cells["B80"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    worksheet.Cells["B80"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;


                    string siglaBanco = string.Empty;
                    string nseriecheque = string.Empty;
                    /** LLENADO DE DATOS DINAMICOS PARA CREACION DE DOCUMENTO */

                    foreach (DataRow rows in web.GetDataChequePagosService.Tables[0].Rows)
                    {

                        foreach(DataRow rowsCorrelativo in web.GetObtenerCorrelativoDisponibleProveedoresService.Tables[0].Rows){
                            if(Convert.ToInt32(rowsCorrelativo["VALIDACION"]) == 0){
                                web.NUMERODOCUMENTO = rowsCorrelativo["ID_CORRELATIVO"].ToString();
                                web.NSERIECHEQUE = numeroSerieCheque(rowsCorrelativo["ID_CORRELATIVO"].ToString());
                                nseriecheque = numeroSerieCheque(rowsCorrelativo["ID_CORRELATIVO"].ToString());
                            }
                        }

                        web.TIPOPAGO = "CHEQUE";
                        web.BANCO = "BANCO DE CHILE / EDWARDS CITY";
                        web.EMPRESA = "TWEST";
                        web.MONTOVACACIONES = rows["FERIADOPROPORCIONAL"].ToString();
                        web.MONTOIAS = rows["MONTOIAS"].ToString();
                        web.MONTODESHAUCIO = rows["MONTODESHAUCIO"].ToString();
                        web.MONTOINDEMNIZACION = rows["MONTOINDEMNIZACION"].ToString();
                        web.MONTOFINIQUITO = rows["TOTALHABER"].ToString();
                        web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
                        web.NOMBRETRABAJADOR = rows["NOMBRE_TRABAJADOR"].ToString();
                        web.CLIENTE = rows["CLIENTE"].ToString();
                        web.CLIENTESIGLA = rows["CLIENTESIGLA"].ToString();
                        web.IDDESVINCULACION = idDesvinculacion;

                        foreach (DataRow rowsRegistro in web.SetInsertarRegistroPagoPagosService.Tables[0].Rows)
                        {
                            if(Convert.ToInt32(rowsRegistro["VALIDACION"]) == 0){
                                if (rows["MONTO_FINIQUITO"].ToString().Length > 0)
                                {
                                    worksheet.Cells["M73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(0, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 1)
                                {
                                    worksheet.Cells["N73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(1, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 2)
                                {
                                    worksheet.Cells["O73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(2, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 3)
                                {
                                    worksheet.Cells["P73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(3, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 4)
                                {
                                    worksheet.Cells["Q73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(4, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 5)
                                {
                                    worksheet.Cells["R73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(5, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 6)
                                {
                                    worksheet.Cells["S73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(6, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 7)
                                {
                                    worksheet.Cells["T73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(7, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 8)
                                {
                                    worksheet.Cells["U73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(8, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 9)
                                {
                                    worksheet.Cells["V73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(9, 1);
                                }

                                if (rows["MONTO_FINIQUITO"].ToString().Length > 10)
                                {
                                    worksheet.Cells["W73"].Value = rows["MONTO_FINIQUITO"].ToString().Substring(10, 1);
                                }


                                worksheet.Cells["L76"].Value = "Stgo";

                                string datetime = rows["FECHA_CHEQUE"].ToString().Replace("-", "/").Split(' ')[0];
                                string time = rows["FECHA_CHEQUE"].ToString().Replace("-", "/").Split(' ')[1].Replace(":", "").Replace(".", "");
                                string[] arrayFecha = datetime.Split('/');

                                string dia = arrayFecha[1];
                                string mes = arrayFecha[0];
                                string year = arrayFecha[2];

                                if (dia.Length == 2)
                                {
                                    worksheet.Cells["O76"].Value = dia.Substring(0, 1) + "   " + dia.Substring(1, 1);
                                }
                                else
                                {
                                    worksheet.Cells["O76"].Value = "0   " + dia.Substring(0, 1);
                                }

                                if (mes.Length == 2)
                                {
                                    worksheet.Cells["Q76"].Value = mes.Substring(0, 1) + "   " + mes.Substring(1, 1);
                                }
                                else
                                {
                                    worksheet.Cells["Q76"].Value = "0   " + mes.Substring(0, 1);
                                }

                                worksheet.Cells["T76"].Value = year.Substring(0, 1);
                                worksheet.Cells["U76"].Value = year.Substring(1, 1);
                                worksheet.Cells["V76"].Value = year.Substring(2, 1);
                                worksheet.Cells["W76"].Value = year.Substring(3, 1);

                                worksheet.Cells["B78"].Value = "                                        " + rows["NOMBRE_TRABAJADOR"].ToString();

                                worksheet.Cells["B80"].Value = "                                        " + cnv.enletras(rows["MONTO_FINIQUITO"].ToString()) + ".-";

                                worksheet.Cells["B78"].Value = "                                        " + rows["NOMBRE_TRABAJADOR"].ToString();

                                if (cnv.enletras(rows["MONTO_FINIQUITO"].ToString()).Length <= 56)
                                {
                                    worksheet.Cells["B80"].Value = "                                        " + cnv.enletras(rows["MONTO_FINIQUITO"].ToString()) + " =====";

                                }
                                else
                                {
                                    /** ALGORITMO DE DIVISIÓN DE CIFRA */
                                    string cifraWithSeparatorInit = cnv.enletras(rows["MONTO_FINIQUITO"].ToString()).Substring(0, 55);
                                    string cifraWithSeparatorEnd = cnv.enletras(rows["MONTO_FINIQUITO"].ToString()).Substring(56, (cnv.enletras(rows["MONTO_FINIQUITO"].ToString()).Length - 56));

                                    string comprobateEmpty = cnv.enletras(rows["MONTO_FINIQUITO"].ToString()).Substring(55, 1);
                                    string cifraWithSeparator = string.Empty;

                                    if (comprobateEmpty == " ")
                                    {
                                        cifraWithSeparator = cifraWithSeparatorInit + "; " + cifraWithSeparatorEnd;
                                    }
                                    else
                                    {
                                        cifraWithSeparator = cifraWithSeparatorInit + ";" + cifraWithSeparatorEnd;
                                    }

                                    string[] arrayCifra = cifraWithSeparator.Split(' ');
                                    int contador = 0;
                                    int encontrado = 0;
                                    foreach (string cifraUnica in arrayCifra)
                                    {
                                        if (cifraUnica.Contains(";"))
                                        {
                                            encontrado = contador;
                                        }

                                        contador = contador + 1;
                                    }

                                    worksheet.Cells["D81:W82"].Merge = true;
                                    worksheet.Cells["D81"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                                    worksheet.Cells["D81"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                                    worksheet.Cells["B80"].Style.Font.Size = 23;
                                    worksheet.Cells["D81"].Style.Font.Size = 23;
                                    worksheet.Cells["B78"].Style.Font.Size = 23;

                                    for (int i = 0; i < encontrado; i++)
                                    {
                                        if (i == 0)
                                        {
                                            worksheet.Cells["B80"].Value = "                                        " + arrayCifra[i] + " ";
                                        }

                                        if (i > 0)
                                        {
                                            worksheet.Cells["B80"].Value = worksheet.Cells["B80"].Value + " " + arrayCifra[i] + " ";
                                        }
                                    }

                                    for (int i = encontrado; i < arrayCifra.Length; i++)
                                    {
                                        if (i == encontrado)
                                        {
                                            worksheet.Cells["D81"].Value = worksheet.Cells["D81"].Value + arrayCifra[i].Replace(";", comprobateEmpty) + " ";
                                        }

                                        if (i > encontrado && (i + 1) < arrayCifra.Length)
                                        {
                                            worksheet.Cells["D81"].Value = worksheet.Cells["D81"].Value + arrayCifra[i] + " ";
                                        }

                                        if ((i + 1) == arrayCifra.Length)
                                        {
                                            worksheet.Cells["D81"].Value = worksheet.Cells["D81"].Value + arrayCifra[i] + " =====";
                                        }
                                    }

                                }


                                nombreArchivo = "TIPO_CHEQUE_" + DateTime.Now.ToString().Replace("-", "").Replace("/", "").Split(' ')[0] + "_" + DateTime.Now.ToString().Replace(".", "").Replace(":", "").Split(' ')[1] + "_TWEST_" + nseriecheque + "_" + siglaBanco + ".xlsx";
                            }
                        }

                    }

                    /** END LLENADO DE DATOS */

                    /** CONFIGURACION DE AREA DE IMPRESION DE CHEQUE SOBRE IMPRESORA OKI DATA CORP ML320/1TURBO */
                    worksheet.Row(90).PageBreak = true;

                    worksheet.PrinterSettings.LeftMargin = 0M / 2.54M;
                    worksheet.PrinterSettings.RightMargin = 0M / 2.54M;
                    worksheet.PrinterSettings.TopMargin = 1.9M / 2.54M;
                    worksheet.PrinterSettings.BottomMargin = 1.3M / 2.54M;
                    worksheet.PrinterSettings.HeaderMargin = 0.8M / 2.54M;
                    worksheet.PrinterSettings.FooterMargin = 0.8M / 2.54M;

                    worksheet.PrinterSettings.FitToPage = true;
                    worksheet.PrinterSettings.PaperSize = ePaperSize.Letter;
                    worksheet.PrinterSettings.VerticalCentered = true;
                    worksheet.Protection.IsProtected = true;
                    worksheet.Protection.SetPassword("Satw.2018");

                    /** DESCARGA DE ARCHIVO .XLSX HACIA EQUIPO DEL USUARIO **/
                    byte[] data = excel.GetAsByteArray();

                    //string path = @"C:\Users\Sebastian Salas\Desktop\CaidadeimpresionesCheques\" + nombreArchivo + ".xlsx";
                    /** PARA CAIDA DE ARCHIVOS CHEQUE */

                    string ruta = @"\\FILESERVER\Ficha Alta\CHEQUES\Finiquitos\";
                    string diaHoy = DateTime.Now.Day.ToString();
                    string mesHoy = DateTime.Now.Month.ToString();
                    string yearHoy = DateTime.Now.Year.ToString();

                    string nombreCarpeta = diaHoy + " de " + convertMonthToString(mesHoy) + " de " + yearHoy + @"\";
                    string rutaCarpeta = ruta + nombreCarpeta;
                    if (!Directory.Exists(rutaCarpeta))
                    {
                        Directory.CreateDirectory(ruta + nombreCarpeta);
                    }
                    string path = ruta + nombreCarpeta + nombreArchivo;
                    //string path = ruta + nombreArchivo;
                    File.WriteAllBytes(path, data);

                    response = nombreArchivo;
                }

            }
            catch (Exception ex)
            {
                response = ex.Message;
            }

            return response;
        }

        private static string numeroSerieCheque(string idcorrelativo)
        {
            string numeroSerie = string.Empty;

            switch (idcorrelativo.Length)
            {
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

        [System.Web.Services.WebMethod]
        public static string llenadocmb() {
            Clases.Usuarios u = new Clases.Usuarios();
            DataTable d = u.Listarkam();
            return JsonConvert.SerializeObject(d,Formatting.Indented);
        }

        [System.Web.Services.WebMethod]
        public static string busquedaxkam(string correo)
        {
            Clases.FN_ENCDESVINCULACION fn = new Clases.FN_ENCDESVINCULACION();
            DataTable d = fn.buscarkam(correo);
            return JsonConvert.SerializeObject(d, Formatting.Indented);
        }

        [System.Web.Services.WebMethod]
        public static string busquedaxrut(string rut)
        {
            Clases.FN_ENCDESVINCULACION fn = new Clases.FN_ENCDESVINCULACION();
            DataTable d = fn.buscarxrut(rut);
            return JsonConvert.SerializeObject(d, Formatting.Indented);
        }

        [System.Web.Services.WebMethod]
        public static string busquedaxfecha(int mes, int year)
        {
            Clases.FN_ENCDESVINCULACION fn = new Clases.FN_ENCDESVINCULACION();
            DataTable d = fn.buscarxfecha(mes, year);
            return JsonConvert.SerializeObject(d, Formatting.Indented);
        }

        [System.Web.Services.WebMethod]
        public static string sessionIdCalculos(string id)
        {
            string result = string.Empty;

            HttpContext.Current.Session["idDeCalculo"] = id;

            result = "1";

            return result;
        }
      
    }
}
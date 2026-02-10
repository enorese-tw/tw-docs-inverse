using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using FiniquitosV2.Clases;
using OfficeOpenXml;
using System.Data;
using Finiquitos.Clases;
using System.IO;

namespace FiniquitosV2
{
    public partial class BasePagosFiniquitos : System.Web.UI.Page
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

        protected void BtnImportarExcel(Object sender, EventArgs e)
        { 
            string applicationGenerateDocumento = Session["applicationGenerateDocument"].ToString();

            if (applicationGenerateDocumento == "ALL")
            {
                /** IMPORTACION DE TODOS LOS REGISTROS */
                using (ExcelPackage excel = new ExcelPackage())
                {
                    MethodServiceFiniquitos web = new MethodServiceFiniquitos();
                    web.TIPOPAGO = "FINIQUITOS";
                    web.FILTRARPOR = Session["applicationFiltrarPor"].ToString();
                    web.FILTRO = Session["applicationFiltro"].ToString();
                    web.FILTRO2 = Session["applicationFiltro2"].ToString();
                    web.FILTRO3 = Session["applicationFiltro3"].ToString();
                    foreach (DataRow rows in web.GetObtenerPagosService.Tables[0].Rows)
                    {
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "BASE FINIQUITOS " + rows["EMPRESA_TW"].ToString()))
                        {
                            excel.Workbook.Worksheets.Add("BASE FINIQUITOS " + rows["EMPRESA_TW"].ToString());
                            var worksheet = excel.Workbook.Worksheets["BASE FINIQUITOS " + rows["EMPRESA_TW"].ToString()];

                            worksheet.Column(1).Width = 43.14;
                            worksheet.Column(2).Width = 23.29;
                            worksheet.Column(3).Width = 23.29;
                            worksheet.Column(4).Width = 27.57;
                            worksheet.Column(5).Width = 16.43;
                            worksheet.Column(6).Width = 16.43;
                            worksheet.Column(7).Width = 27.57;
                            worksheet.Column(8).Width = 19.71;
                            worksheet.Column(9).Width = 43.14;
                            worksheet.Column(10).Width = 21.86;
                            worksheet.Column(11).Width = 23.86;
                            worksheet.Column(12).Width = 23.86;
                            worksheet.Column(13).Width = 43.14;
                            worksheet.Column(14).Width = 19.43;
                            worksheet.Column(15).Width = 23.86;
                            worksheet.Column(16).Width = 43.14;
                            worksheet.Column(17).Width = 23.86;

                            worksheet.Cells["A1:S1"].AutoFilter = true;

                            worksheet.Cells["A1"].Value = "EMPLEADO";
                            worksheet.Cells["A1"].Style.Font.Bold = true;
                            worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["B1"].Value = "FECHA INICIO CONTRATO";
                            worksheet.Cells["B1"].Style.Font.Bold = true;
                            worksheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["C1"].Value = "FECHA TERMINO CONTRATO";
                            worksheet.Cells["C1"].Style.Font.Bold = true;
                            worksheet.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["D1"].Value = "EMPRESA TW";
                            worksheet.Cells["D1"].Style.Font.Bold = true;
                            worksheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["E1"].Value = "ROL PRIVADO";
                            worksheet.Cells["E1"].Style.Font.Bold = true;
                            worksheet.Cells["E1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["E1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["F1"].Value = "MONTO FINIQUITO";
                            worksheet.Cells["F1"].Style.Font.Bold = true;
                            worksheet.Cells["F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["G1"].Value = "IAS";
                            worksheet.Cells["G1"].Style.Font.Bold = true;
                            worksheet.Cells["G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["G1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["H1"].Value = "MES DE AVISO";
                            worksheet.Cells["H1"].Style.Font.Bold = true;
                            worksheet.Cells["H1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["H1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["I1"].Value = "INDEMNIZACION VOLUNTARIA";
                            worksheet.Cells["I1"].Style.Font.Bold = true;
                            worksheet.Cells["I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["I1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["J1"].Value = "VACACIONES";
                            worksheet.Cells["J1"].Style.Font.Bold = true;
                            worksheet.Cells["J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["J1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["K1"].Value = "CLIENTE";
                            worksheet.Cells["K1"].Style.Font.Bold = true;
                            worksheet.Cells["K1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["K1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["L1"].Value = "AREA NEGOCIO";
                            worksheet.Cells["L1"].Style.Font.Bold = true;
                            worksheet.Cells["L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["M1"].Value = "FECHA";
                            worksheet.Cells["M1"].Style.Font.Bold = true;
                            worksheet.Cells["M1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["M1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["N1"].Value = "TIPO DE PAGO";
                            worksheet.Cells["N1"].Style.Font.Bold = true;
                            worksheet.Cells["N1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["N1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["O1"].Value = "BANCO";
                            worksheet.Cells["O1"].Style.Font.Bold = true;
                            worksheet.Cells["O1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["O1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["P1"].Value = "EMPRESA TW";
                            worksheet.Cells["P1"].Style.Font.Bold = true;
                            worksheet.Cells["P1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["P1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["Q1"].Value = "N° DE CHEQUE";
                            worksheet.Cells["Q1"].Style.Font.Bold = true;
                            worksheet.Cells["Q1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["Q1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["R1"].Value = "ESTADO DE PAGO";
                            worksheet.Cells["R1"].Style.Font.Bold = true;
                            worksheet.Cells["R1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["R1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        
                            worksheet.Cells["S1"].Value = "NOMENCLATURA";
                            worksheet.Cells["S1"].Style.Font.Bold = true;
                            worksheet.Cells["S1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["S1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        var worksheetData = excel.Workbook.Worksheets["BASE FINIQUITOS " + rows["EMPRESA_TW"].ToString()];
                        int contador = worksheetData.Dimension.End.Row + 1;

                        /** ----- data dinamica ----- */

                        worksheetData.Cells["A" + Convert.ToString(contador)].Value = rows["NOMBRE_TRABAJADOR"].ToString();
                        worksheetData.Cells["B" + Convert.ToString(contador)].Value = rows["FECHAINICIOC"].ToString();
                        worksheetData.Cells["C" + Convert.ToString(contador)].Value = rows["FECHATERMINOC"].ToString();
                        worksheetData.Cells["D" + Convert.ToString(contador)].Value = rows["EMPRESA_TW"].ToString();
                        worksheetData.Cells["E" + Convert.ToString(contador)].Value = rows["ROL_PRIVADO"].ToString();

                        if (rows["MONTO_FINIQUITO"].ToString() != "")
                        {
                            worksheetData.Cells["F" + Convert.ToString(contador)].Value = rows["MONTO_FINIQUITO"].ToString();
                        }
                        else
                        {
                            worksheetData.Cells["F" + Convert.ToString(contador)].Value = rows["MONTO_FINIQUITO"].ToString();
                        }

                        if (rows["MONTO_IAS"].ToString() != "")
                        {
                            worksheetData.Cells["G" + Convert.ToString(contador)].Value = rows["MONTO_IAS"].ToString();
                        }
                        else
                        {
                            worksheetData.Cells["G" + Convert.ToString(contador)].Value = rows["MONTO_IAS"].ToString();
                        }

                        if (rows["MONTO_DESHAUCIO"].ToString() != "")
                        {
                            worksheetData.Cells["H" + Convert.ToString(contador)].Value = rows["MONTO_DESHAUCIO"].ToString();
                        }
                        else
                        {
                            worksheetData.Cells["H" + Convert.ToString(contador)].Value = rows["MONTO_DESHAUCIO"].ToString();
                        }

                        if (rows["MONTO_INDEMNVOLUNTARIA"].ToString() != "")
                        {
                            worksheetData.Cells["I" + Convert.ToString(contador)].Value = rows["MONTO_INDEMNVOLUNTARIA"].ToString();
                        }
                        else
                        {
                            worksheetData.Cells["I" + Convert.ToString(contador)].Value = rows["MONTO_INDEMNVOLUNTARIA"].ToString();
                        }

                        if (rows["MONTO_VACACIONES"].ToString() != "")
                        {
                            worksheetData.Cells["J" + Convert.ToString(contador)].Value = rows["MONTO_VACACIONES"].ToString();
                        }
                        else
                        {
                            worksheetData.Cells["J" + Convert.ToString(contador)].Value = rows["MONTO_VACACIONES"].ToString();
                        }

                        worksheetData.Cells["K" + Convert.ToString(contador)].Value = rows["CLIENTE"].ToString();
                        worksheetData.Cells["L" + Convert.ToString(contador)].Value = rows["SIGLACLIENTE"].ToString();
                        worksheetData.Cells["M" + Convert.ToString(contador)].Value = rows["FECHA_GENERACION"].ToString();
                        worksheetData.Cells["N" + Convert.ToString(contador)].Value = rows["TIPO_PAGO"].ToString();
                        worksheetData.Cells["O" + Convert.ToString(contador)].Value = rows["BANCO"].ToString();
                        worksheetData.Cells["P" + Convert.ToString(contador)].Value = rows["EMPRESA_TW"].ToString();
                        worksheetData.Cells["Q" + Convert.ToString(contador)].Value = rows["NSERIE_DOCUMENTO"].ToString();
                    
                        switch (rows["ESTADO_ACTUAL"].ToString())
                        {
                            case "DISPONIBLE A GENERAR":
                                worksheetData.Cells["R" + Convert.ToString(contador)].Value = "DISPONIBLE PARA IMPRESIÓN";
                                break;
                            case "GENERADO":
                                worksheetData.Cells["R" + Convert.ToString(contador)].Value = "DISPONIBLE PARA PAGO";
                                break;
                            case "PAGADO":
                                worksheetData.Cells["R" + Convert.ToString(contador)].Value = rows["ESTADO_ACTUAL"].ToString();
                                break;
                        }

                        worksheetData.Cells["S" + Convert.ToString(contador)].Value = rows["NOMENCLATURA_FINANZAS"].ToString();
                    }

                    excel.Workbook.Properties.Title = "Attempts";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", "REPORTE DE PAGOS " + DateTime.Now.ToString().Replace("-", "").Replace(' ', '_').Replace(":", "") + ".xlsx"));
                    Response.ContentType = "application/excel";
                    Response.BinaryWrite(excel.GetAsByteArray());
                    Response.Flush();
                    Response.End();
                }
            }
        }

        [WebMethod()]
        public static string GetObtenerPagosService(string filtrarPor, string filtro, string filtro2, string filtro3)
        {
            HttpContext.Current.Session["applicationGenerateDocument"] = "ALL";
            HttpContext.Current.Session["applicationFiltrarPor"] = filtrarPor;
            HttpContext.Current.Session["applicationFiltro"] = filtro;
            HttpContext.Current.Session["applicationFiltro2"] = filtro2;
            HttpContext.Current.Session["applicationFiltro3"] = filtro3;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.TIPOPAGO = "FINIQUITOS";
            web.FILTRARPOR = filtrarPor;
            web.FILTRO = filtro;
            web.FILTRO2 = filtro2;
            web.FILTRO3 = filtro3;
            return JsonConvert.SerializeObject(web.GetObtenerPagosService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerClientePagosService()
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetObtenerClientePagosService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerTrabajadoresPagosService()
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetObtenerTrabajadoresPagosService, Formatting.Indented);
        }

    }

}
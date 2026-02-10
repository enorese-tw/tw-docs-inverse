using FiniquitosV2.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using OfficeOpenXml;
using System.Data;
using Newtonsoft.Json;

namespace FiniquitosV2
{
    public partial class BasePagoProveedores : System.Web.UI.Page
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
                    web.TIPOPAGO = "PROVEEDORES";
                    web.FILTRARPOR = Session["applicationFiltrarPor"].ToString();
                    web.FILTRO = Session["applicationFiltro"].ToString();
                    web.FILTRO2 = Session["applicationFiltro2"].ToString();
                    web.FILTRO3 = Session["applicationFiltro3"].ToString();
                    foreach (DataRow rows in web.GetObtenerPagosService.Tables[0].Rows)
                    {
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "BASE PROVEEDORES " + rows["EMPRESA_CHEQUE"].ToString()))
                        {
                            excel.Workbook.Worksheets.Add("BASE PROVEEDORES " + rows["EMPRESA_CHEQUE"].ToString());
                            var worksheet = excel.Workbook.Worksheets["BASE PROVEEDORES " + rows["EMPRESA_CHEQUE"].ToString()];

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

                            worksheet.Cells["A1:Q1"].AutoFilter = true;

                            worksheet.Cells["A1"].Value = "PROVEEDOR";
                            worksheet.Cells["A1"].Style.Font.Bold = true;
                            worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["A1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["B1"].Value = "RUT PROVEEDOR";
                            worksheet.Cells["B1"].Style.Font.Bold = true;
                            worksheet.Cells["B1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["B1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["C1"].Value = "TIPO PROVEEDOR";
                            worksheet.Cells["C1"].Style.Font.Bold = true;
                            worksheet.Cells["C1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["C1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["D1"].Value = "OBSERVACION";
                            worksheet.Cells["D1"].Style.Font.Bold = true;
                            worksheet.Cells["D1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["D1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["E1"].Value = "MONTO";
                            worksheet.Cells["E1"].Style.Font.Bold = true;
                            worksheet.Cells["E1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["E1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["F1"].Value = "FECHA";
                            worksheet.Cells["F1"].Style.Font.Bold = true;
                            worksheet.Cells["F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["F1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["G1"].Value = "TIPO DE PAGO";
                            worksheet.Cells["G1"].Style.Font.Bold = true;
                            worksheet.Cells["G1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["G1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["H1"].Value = "BANCO";
                            worksheet.Cells["H1"].Style.Font.Bold = true;
                            worksheet.Cells["H1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["H1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        
                            worksheet.Cells["I1"].Value = "EMPRESA TW";
                            worksheet.Cells["I1"].Style.Font.Bold = true;
                            worksheet.Cells["I1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["I1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["J1"].Value = "N° DE CHEQUE";
                            worksheet.Cells["J1"].Style.Font.Bold = true;
                            worksheet.Cells["J1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["J1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["K1"].Value = "ESTADO DE PAGO";
                            worksheet.Cells["K1"].Style.Font.Bold = true;
                            worksheet.Cells["K1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["K1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                            worksheet.Cells["L1"].Value = "NOMENCLATURA";
                            worksheet.Cells["L1"].Style.Font.Bold = true;
                            worksheet.Cells["L1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            worksheet.Cells["L1"].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        }

                        var worksheetData = excel.Workbook.Worksheets["BASE PROVEEDORES " + rows["EMPRESA_CHEQUE"].ToString()];
                        int contador = worksheetData.Dimension.End.Row + 1;

                        /** ----- data dinamica ----- */

                        worksheetData.Cells["A" + Convert.ToString(contador)].Value = rows["PROVEEDOR"].ToString();
                        worksheetData.Cells["B" + Convert.ToString(contador)].Value = rows["RUT_PROVEEDOR"].ToString();
                        worksheetData.Cells["C" + Convert.ToString(contador)].Value = rows["TIPO_PROVEEDOR"].ToString();

                        worksheetData.Cells["D" + Convert.ToString(contador)].Value = rows["OBSERVACION"].ToString();

                        worksheetData.Cells["E" + Convert.ToString(contador)].Value = rows["MONTO"].ToString();
                        
                        worksheetData.Cells["F" + Convert.ToString(contador)].Value = rows["FECHA_GENERACION"].ToString();
                        worksheetData.Cells["G" + Convert.ToString(contador)].Value = rows["TIPO_PAGO"].ToString();
                        worksheetData.Cells["H" + Convert.ToString(contador)].Value = rows["BANCO"].ToString();
                        worksheetData.Cells["I" + Convert.ToString(contador)].Value = rows["EMPRESA_CHEQUE"].ToString();
                        worksheetData.Cells["J" + Convert.ToString(contador)].Value = rows["NSERIE_DOCUMENTO"].ToString();

                        worksheetData.Cells["K" + Convert.ToString(contador)].Value = "PAGADO";

                        worksheetData.Cells["L" + Convert.ToString(contador)].Value = rows["NOMENCLATURA_FINANZAS"].ToString();
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
            web.TIPOPAGO = "PROVEEDORES";
            web.FILTRARPOR = filtrarPor;
            web.FILTRO = filtro;
            web.FILTRO2 = filtro2;
            web.FILTRO3 = filtro3;
            return JsonConvert.SerializeObject(web.GetObtenerPagosService, Formatting.Indented);
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Newtonsoft.Json;
using FiniquitosV2.Clases;
using Finiquitos.Clases;
using OfficeOpenXml;
using System.IO;
using System.Data;
using iTextSharp.text;
using System.Drawing;

namespace FiniquitosV2
{
    public partial class PagoProveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod()]
        public static string SetInsertarProveedorService(string rut, string nombre, string tipo)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.RUT = rut;
            web.NOMBRE = nombre;
            web.TIPO = tipo;
            return JsonConvert.SerializeObject(web.SetInsertarProveedorService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetVisualizarTiposProveedoresService()
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetVisualizarTiposProveedoresService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetObtenerProveedorService(string rutProveedor)
        {
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.RUTPROVEEDOR = rutProveedor;
            return JsonConvert.SerializeObject(web.GetObtenerProveedorService, Formatting.Indented);
        }

        [WebMethod()]
        public static string GetMontoCifra(string monto)
        {
            string cifra = string.Empty;
            Convertidor cnv = new Convertidor();

            cifra = cnv.enletras(monto);

            return cifra;
        }

        [WebMethod()]
        public static string GetObtenerCorrelativoDisponibleProveedoresService(string empresa) 
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            web.IDEMPRESA = empresa;
            return JsonConvert.SerializeObject(web.GetObtenerCorrelativoDisponibleProveedoresService, Formatting.Indented);
        }

        #region "NUEVO METODO DE CHEQUE PROVEEDORES"

        [WebMethod]
        public static string SetTMPCloseProcessChequeProveedorService()
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

                    worksheet.Column(7).Width = 19.89;

                    #endregion

                    int contador = 1;
                    int rowExcel = 71;
                    int constante = 0;
                    int comprobante = 8;

                    foreach (DataRow rows in web.SetTMPCloseProcessChequeProveedorService.Tables[0].Rows)
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
                            string observacion = rows["OBSERVACION"].ToString();

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
                            worksheet.Cells["I" + comprobante.ToString()].Value = "GENERACIÓN DE CHEQUE PARA PROVEEDORES";
                            worksheet.Cells["I" + (comprobante + 2).ToString()].Value = "PARA: " + quien;
                            worksheet.Cells["I" + (comprobante + 3).ToString()].Value = "POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0");
                            worksheet.Cells["I" + (comprobante + 4).ToString()].Value = "EL DÍA: " + fechaGeneracion;
                            worksheet.Cells["I" + (comprobante + 5).ToString()].Value = observacion;

                            #endregion

                            #region "CONFIGURACION FILAS CHEQUE A CHEQUE"

                            worksheet.Row(comprobante).Height = 34.50;
                            worksheet.Row((comprobante + 1)).Height = 34.50;
                            worksheet.Row((comprobante + 2)).Height = 34.50;
                            worksheet.Row((comprobante + 3)).Height = 34.50;
                            worksheet.Row((comprobante + 4)).Height = 34.50;
                            worksheet.Row((comprobante + 5)).Height = 34.50;
                            worksheet.Row((comprobante + 61)).Height = 28.20;
                            worksheet.Row((comprobante + 62)).Height = 222.6; // row 70
                            worksheet.Row((comprobante + 63)).Height = 42.00;
                            worksheet.Row((comprobante + 64)).Height = 16.8;
                            worksheet.Row((comprobante + 65)).Height = 39.00;
                            worksheet.Row((comprobante + 66)).Height = 4.2;
                            worksheet.Row((comprobante + 67)).Height = 1.8;
                            worksheet.Row((comprobante + 68)).Height = 9;
                            worksheet.Row((comprobante + 69)).Height = 34.20;
                            worksheet.Row((comprobante + 70)).Height = 10.20;
                            worksheet.Row((comprobante + 71)).Height = 27.00;
                            worksheet.Row((comprobante + 72)).Height = 6.00; // row 80
                            worksheet.Row((comprobante + 73)).Height = 18.00;
                            worksheet.Row((comprobante + 74)).Height = 15.00;
                            worksheet.Row((comprobante + 75)).Height = 28.8;
                            worksheet.Row((comprobante + 76)).Height = 9.6;
                            worksheet.Row((comprobante + 77)).Height = 4.2;
                            worksheet.Row((comprobante + 78)).Height = 6.60;
                            worksheet.Row((comprobante + 79)).Height = 34.2;
                            worksheet.Row((comprobante + 80)).Height = 14.40;
                            worksheet.Row((comprobante + 81)).Height = 14.40;
                            worksheet.Row((comprobante + 82)).Height = 14.40; // row 90
                            worksheet.Row((comprobante + 83)).Height = 14.40;

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

                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString() + ":Z" + ((rowExcel + 16) + constante).ToString()].Merge = true;
                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Bottom;
                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString()].Style.Font.Size = 40;
                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString()].Style.Font.Bold = true;

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

                            worksheet.Cells["K" + ((rowExcel + 15) + constante).ToString()].Value = "$ " + montoProtegido;

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
                                                                                         "PAGO PROVEEDOR";

                            #endregion
                        }


                        contador = contador + 1;
                    }

                    worksheet.Row(92 * contador - 1).PageBreak = true;
                    worksheet.Cells["A" + ((92 * contador - 1) + 1).ToString()].Value = "Aqui no debe ir cheque, sino una hoja en blanco como pagina de seguridad";

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
                    string ruta = @"\\cranky\FileServer\Sistemas\Ficha_Alta\Cheques\Proveedores\";

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
        
        [WebMethod]
        public static string SetTMPCargaChequeProveedorProveedoresService(string nombreTrabajador, string monto, string dia, string mes, string year, string empresa, string cifra,
                                                                          string rutProveedor, string correlativo, string nseriecheque, string ciudad, string siglaBanco, string nominativo,
                                                                          string observacion)
        {
            string response = string.Empty;

            MethodServiceFiniquitos web = new MethodServiceFiniquitos();

            web.RUTPROVEEDOR = rutProveedor;
            web.MONTO = monto;
            web.IDCORRELATIVO = correlativo;
            web.NSERIECHEQUE = nseriecheque;
            web.USUARIO = HttpContext.Current.Session["Usuario"].ToString();
            web.IDEMPRESA = empresa;

            web.OBSERVACION = observacion;

            foreach (DataRow rows in web.SetRegistrarPagoProveedoresService.Tables[0].Rows)
            {
                if (Convert.ToInt32(rows["VALIDACION"]) == 0)
                {
                    web.NOMBRETRABAJADOR = nombreTrabajador;
                    web.MONTO = monto;
                    web.DIA = dia;
                    web.MES = mes;
                    web.YEAR = year;
                    web.EMPRESA = empresa;
                    web.CIFRA = cifra;
                    web.CIUDAD = ciudad;
                    web.NOMINATIVO = nominativo;
                    response = JsonConvert.SerializeObject(web.SetTMPCargaChequeProveedorProveedoresService, Formatting.Indented);
                }
            }

            return response;
        }

        
        [WebMethod]
        public static string SetTMPInitProcessChequeProveedorProveedoresService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.SetTMPInitProcessChequeProveedorProveedoresService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetTMPValidateProcessInitProveedoresService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetTMPValidateProcessInitProveedoresService, Formatting.Indented);
        }

        [WebMethod]
        public static string GetTMPChequesInProcessProveedorService()
        {
            string response = string.Empty;
            MethodServiceFiniquitos web = new MethodServiceFiniquitos();
            return JsonConvert.SerializeObject(web.GetTMPChequesInProcessProveedorService, Formatting.Indented);
        }

        #region "METODO DE CHEQUE PRODUCCION PROVEEDORES"

        //[WebMethod]
        //public static string SetTMPCloseProcessChequeProveedorService()
        //{
        //    string response = string.Empty;
        //    string nombreArchivo = string.Empty;
        //    MethodServiceFiniquitos web = new MethodServiceFiniquitos();

        //    try{

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

        //            foreach (DataRow rows in web.SetTMPCloseProcessChequeProveedorService.Tables[0].Rows)
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
        //                    string observacion = rows["OBSERVACION"].ToString();

        //                    worksheet.Cells["E" + comprobante.ToString() + ":U" + (comprobante + 16).ToString()].Merge = true;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.Font.Size = 22;
        //                    worksheet.Cells["E" + comprobante.ToString()].Style.WrapText = true;
        //                    //worksheet.Cells["E" + comprobante.ToString()].Value = "COMPROBANTE DE GENERACIÓN DE CHEQUE PARA PAGO PROVEEDOR \n \n PARA: " + quien + " \n \n POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0") + " \n " + observacion + " \n \n EL DÍA: " + fechaGeneracion;
        //                    worksheet.Cells["E" + comprobante.ToString()].Value = "COMPROBANTE DE GENERACIÓN DE CHEQUE PARA PAGO PROVEEDOR \n \n PARA: " + quien + " \n \n POR EL MONTO: $ " + Convert.ToInt32(monto).ToString("N0") + " \n \n EL DÍA: " + fechaGeneracion;

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

        //                    //string datetime = rows["FECHA_CHEQUE"].ToString().Replace("-", "/").Split(' ')[0];
        //                    //string time = rows["FECHA_CHEQUE"].ToString().Replace("-", "/").Split(' ')[1].Replace(":", "").Replace(".", "");
        //                    //string[] arrayFecha = datetime.Split('/');

        //                    //string dia = arrayFecha[1];
        //                    //string mes = arrayFecha[0];
        //                    //string year = arrayFecha[2];

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
        //                    // + 5
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
        //                        // D81 W82
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
        //                                worksheet.Cells["B" +((rowExcel + 7) + constante).ToString()].Value = "                                        " + arrayCifra[i] + " ";
        //                            }

        //                            if (i > 0)
        //                            {
        //                                worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value = worksheet.Cells["B" + ((rowExcel + 7) + constante).ToString()].Value + " " + arrayCifra[i] + " ";
        //                            }
        //                        }

        //                        for (int i = encontrado; i < arrayCifra.Length; i++)
        //                        {
        //                            if((arrayCifra.Length - encontrado) == 1)
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
        //            string ruta = @"\\SATW\CHEQUES\Proveedores\";

        //            /** RUTA QA */
        //            //string ruta = @"\\SATW\CHEQUES\QA\Proveedores\";

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

        private static string convertMonthToString(string mes) {
            string monthString = string.Empty;

            switch(mes){
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
    }
}
using Teamwork.Model.Teamwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using OfficeOpenXml;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteTransferencias
    {
        public static Files xlsx(DataRowCollection rows)
        {
            List<List<TransferenciasFiles>> tefs = new List<List<TransferenciasFiles>>();
            string[] limitAccounts = { "87274144", "87274039" };
            dynamic changeAccount = new { from = "2504898", to = "87274039" };
            int limitAccount = 10000000;
            Files files = new Files();
            List<byte[]> excels = new List<byte[]>();
            string empresa = "";
            string fecha = "";
            string accountCurrent = "";
            int agrupado = 0;
            int round = 0;
            int currentRound = 0;
            int montoResto = 0;
            int roundPaused = 0;
            bool exit = false;

            /** Estructura de archivos de transferencias */
            for (int i = 0; i < rows.Count; i++)
            {
                agrupado += Convert.ToInt32(rows[i]["MtoTotal"].ToString());
                accountCurrent = rows[i]["CtaOrigen"].ToString();

                if (changeAccount.from == accountCurrent)
                {
                    accountCurrent = changeAccount.to;
                }
            }

            currentRound = (int)Math.Ceiling(Convert.ToDecimal(agrupado) / Convert.ToDecimal(limitAccount));

            agrupado = 0;

            if (limitAccounts.Contains(accountCurrent))
            {
                while (round < currentRound)
                {
                    List<TransferenciasFiles> objectTefs = new List<TransferenciasFiles>();

                    for (int i = roundPaused; i < rows.Count; i++)
                    {
                        int total = Convert.ToInt32(rows[i]["MtoTotal"].ToString());
                        string ctaOrigen = rows[i]["CtaOrigen"].ToString();

                        if (accountCurrent != ctaOrigen)
                        {
                            ctaOrigen = accountCurrent;
                        }

                        if (montoResto == 0 && agrupado + total > limitAccount)
                        {
                            int currentTotal = total;
                            total = limitAccount - agrupado;
                            montoResto = currentTotal - total;
                            roundPaused = i;
                            exit = true;
                        }

                        if (!exit && montoResto > 0)
                        {
                            total = montoResto;
                            montoResto = 0;
                        }

                        TransferenciasFiles objectTef = new TransferenciasFiles();
                        objectTef.CtaOrigen = ctaOrigen;
                        objectTef.MonedaOrigen = rows[i]["MonedaOrigen"].ToString();
                        objectTef.CtaDestino = rows[i]["CtaDestino"].ToString();
                        objectTef.MonedaDestino = rows[i]["MonedaDestino"].ToString();
                        objectTef.CodBanco = rows[i]["CodBanco"].ToString();
                        objectTef.RutBeneficiario = rows[i]["RutBeneficiario"].ToString();
                        objectTef.NombreBeneficiario = rows[i]["NombreBeneficiario"].ToString();
                        objectTef.GlosaTEF = rows[i]["GlosaTEF"].ToString();
                        objectTef.Correo = rows[i]["Correo"].ToString();
                        objectTef.GlosaCorreo = rows[i]["GlosaCorreo"].ToString();
                        objectTef.GlosaCartolaCliente = rows[i]["GlosaCartolaCliente"].ToString();
                        objectTef.GlosaCartolaBeneficiario = rows[i]["GlosaCartolaBeneficiario"].ToString();
                        objectTef.Empresa = rows[i]["Empresa"].ToString();
                        objectTef.FechaTransaccion = rows[i]["FechaTransaccion"].ToString();
                        objectTef.Total = total;
                        objectTefs.Add(objectTef);
                        
                        if (exit)
                        {
                            agrupado = 0;
                            exit = false;
                            break;
                        }
                        
                        agrupado += total;
                    }

                    tefs.Add(objectTefs);

                    round = round + 1;
                }
            }
            else
            {
                List<TransferenciasFiles> objectTefs = new List<TransferenciasFiles>();

                foreach (DataRow row in rows)
                {
                    TransferenciasFiles objectTef = new TransferenciasFiles();
                    objectTef.CtaOrigen = row["CtaOrigen"].ToString();
                    objectTef.MonedaOrigen = row["MonedaOrigen"].ToString();
                    objectTef.CtaDestino = row["CtaDestino"].ToString();
                    objectTef.MonedaDestino = row["MonedaDestino"].ToString();
                    objectTef.CodBanco = row["CodBanco"].ToString();
                    objectTef.RutBeneficiario = row["RutBeneficiario"].ToString();
                    objectTef.NombreBeneficiario = row["NombreBeneficiario"].ToString();
                    objectTef.GlosaTEF = row["GlosaTEF"].ToString();
                    objectTef.Correo = row["Correo"].ToString();
                    objectTef.GlosaCorreo = row["GlosaCorreo"].ToString();
                    objectTef.GlosaCartolaCliente = row["GlosaCartolaCliente"].ToString();
                    objectTef.GlosaCartolaBeneficiario = row["GlosaCartolaBeneficiario"].ToString();
                    objectTef.Empresa = row["Empresa"].ToString();
                    objectTef.FechaTransaccion = row["FechaTransaccion"].ToString();
                    objectTef.Total = Convert.ToInt32(row["MtoTotal"].ToString());
                    objectTefs.Add(objectTef);
                }

                tefs.Add(objectTefs);
            }
            

            /** Archivos individuales de transferencias */
            foreach (List<TransferenciasFiles> tef in tefs)
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    string[] columns = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
                    string[] nameCols = { "Cta_origen", "moneda_origen", "Cta_destino", "moneda_destino", "Cod_banco", "RUT benef.", "nombre benef.", "Mto Total", "Glosa TEF", "Correo", "Glosa correo", "Glosa Cartola Cliente", "Glosa Cartola Beneficiario" };

                    int tope = 7000000;

                    foreach (TransferenciasFiles row in tef)
                    {
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Hoja1"))
                        {
                            excel.Workbook.Worksheets.Add("Hoja1");
                            var worksheetConfig = excel.Workbook.Worksheets["Hoja1"];

                            for (var i = 0; i < columns.Length; i++)
                            {
                                worksheetConfig.Cells[columns[i] + "1"].Value = nameCols[i];
                                worksheetConfig.Cells[columns[i] + "1"].Style.Font.Bold = true;
                            }

                            empresa = row.Empresa;
                            fecha = row.FechaTransaccion;

                            worksheetConfig.Protection.IsProtected = true;
                            worksheetConfig.Protection.SetPassword("domCLt3am.W0rk");
                        }
                        var worksheet = excel.Workbook.Worksheets["Hoja1"];
                        int contador = worksheet.Dimension.End.Row + 1;

                        int rowCustomized = (int)Math.Ceiling(Convert.ToDecimal(row.Total) / Convert.ToDecimal(tope));
                        int resto = Convert.ToInt32(row.Total);
                        int suma = 0;

                        for (int i = 0; i < rowCustomized; i++)
                        {
                            contador = worksheet.Dimension.End.Row + 1;

                            if (resto >= tope)
                            {
                                resto -= tope;
                                suma = tope;
                            }
                            else if (resto < tope)
                            {
                                suma = resto;
                            }

                            worksheet.Cells["A" + contador.ToString()].Value = row.CtaOrigen;
                            worksheet.Cells["B" + contador.ToString()].Value = row.MonedaOrigen;
                            worksheet.Cells["C" + contador.ToString()].Value = row.CtaDestino;
                            worksheet.Cells["D" + contador.ToString()].Value = row.MonedaDestino;
                            worksheet.Cells["E" + contador.ToString()].Value = row.CodBanco;
                            worksheet.Cells["F" + contador.ToString()].Value = row.RutBeneficiario;
                            worksheet.Cells["G" + contador.ToString()].Value = row.NombreBeneficiario;
                            worksheet.Cells["H" + contador.ToString()].Value = suma.ToString();
                            worksheet.Cells["I" + contador.ToString()].Value = row.GlosaTEF;
                            worksheet.Cells["J" + contador.ToString()].Value = row.Correo;
                            worksheet.Cells["K" + contador.ToString()].Value = row.GlosaCorreo;
                            worksheet.Cells["L" + contador.ToString()].Value = row.GlosaCartolaCliente;
                            worksheet.Cells["M" + contador.ToString()].Value = row.GlosaCartolaBeneficiario;
                        }

                    }

                    excels.Add(excel.GetAsByteArray());
                }
            }

            files.ListFile = excels;
            files.Empresa = empresa;
            files.Fecha = fecha;

            return files;
        }
    }

    public class TransferenciasFiles
    {
        public string CtaOrigen { get; set; }
        public string MonedaOrigen { get; set; }
        public string CtaDestino { get; set; }
        public string MonedaDestino { get; set; }
        public string CodBanco { get; set; }
        public string RutBeneficiario { get; set; }
        public string NombreBeneficiario { get; set; }
        public int Total { get; set; }
        public string GlosaTEF { get; set; }
        public string Correo { get; set; }
        public string GlosaCorreo { get; set; }
        public string GlosaCartolaCliente { get; set; }
        public string GlosaCartolaBeneficiario { get; set; }
        public string Empresa { get; set; }
        public string FechaTransaccion { get; set; }
    }
}
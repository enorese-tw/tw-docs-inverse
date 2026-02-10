using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Collections.General;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteSolicitudes
    {
        public static byte[] __xlsx(string codigoTransaction, string plantillaMasiva)
        {
            string prefixSheet = string.Empty;
            string sheetName = string.Empty;
            byte[] bytes = null;
            dynamic report = CollectionExcel.__ReporteSolicitud(codigoTransaction, plantillaMasiva);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                for (var i = 1; i < report.Count; i++)
                {
                    prefixSheet = "Bajas ";
                    sheetName = report[i]["Column13"].ToString();

                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == prefixSheet + sheetName))
                    {
                        excel.Workbook.Worksheets.Add(prefixSheet + sheetName);
                        var worksheetConfig = excel.Workbook.Worksheets[prefixSheet + sheetName];

                        for (var j = 1; j <= ((Newtonsoft.Json.Linq.JObject)report[0]).Count; j++)
                        {
                            worksheetConfig.Cells[1, j].Value = report[0]["Column" + j.ToString()].ToString();
                        }
                    }

                    var worksheet = excel.Workbook.Worksheets[prefixSheet + sheetName];
                    int contador = worksheet.Dimension.End.Row + 1;

                    for (var j = 1; j <= ((Newtonsoft.Json.Linq.JObject)report[i]).Count; j++)
                    {
                        worksheet.Cells[(i + 1), j].Value = report[i]["Column" + j.ToString()].ToString();
                    }
                }

                bytes = excel.GetAsByteArray();
            }

            return bytes;
        }
    }
}
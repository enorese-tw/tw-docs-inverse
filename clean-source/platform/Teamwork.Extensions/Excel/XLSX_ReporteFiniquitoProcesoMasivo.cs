using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Collections.General;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteFiniquitoProcesoMasivo
    {
        public static byte[] __xlsx(string excels, string data)
        {
            byte[] bytes = null;
            dynamic report = CollectionExcel.__ReporteFiniquitos(excels, data);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Reporte");
                var worksheet = excel.Workbook.Worksheets["Reporte"];

                for (var i = 0; i < report.Count; i++)
                {
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
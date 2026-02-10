using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteContratos
    {
        public static byte[] __xlsx(string cliente = "", string fechaInicioFilter = "", string FechaTerminoFilter = "", string empresa = "")
        {
            byte[] bytes = null;
            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__ReporteContratos(
                    cliente,
                    fechaInicioFilter,
                    FechaTerminoFilter,
                    empresa,
                    objectsToken[0].Token.ToString()
                )
            );

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Reporte");
                var worksheet = excel.Workbook.Worksheets["Reporte"];

                for (var i = 0; i < objects.Count; i++)
                {
                    for (var j = 1; j <= ((Newtonsoft.Json.Linq.JObject)objects[i]).Count; j++)
                    {
                        worksheet.Cells[(i + 1), j].Value = objects[i]["Column" + j.ToString()].ToString();
                    }
                }

                bytes = excel.GetAsByteArray();
            }

            return bytes;
        }
    }
}
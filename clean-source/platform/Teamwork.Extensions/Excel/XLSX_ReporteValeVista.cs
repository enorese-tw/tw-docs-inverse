using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteValeVista
    {
        public static byte[] __xlsx(string usuarioCreador, string proceso, dynamic servicioOperaciones = null)
        {
            string[] paramMantFin = new string[34];
            string[] valMantFin = new string[34];
            DataSet dataMantencion;
            byte[] bytes = null;

            paramMantFin[0] = "@ACCION";
            paramMantFin[1] = "@USUARIOCREADOR";
            paramMantFin[2] = "@CODIGOFINIQUITO";
            paramMantFin[3] = "@METODOPAGO";
            paramMantFin[4] = "@CODIGOPAGO";
            paramMantFin[5] = "@CODIGOPROCESO";
            paramMantFin[6] = "@DESTINATARIO";
            paramMantFin[7] = "@BANCO";
            paramMantFin[8] = "@TIPOCUENTA";
            paramMantFin[9] = "@FECHA";
            paramMantFin[10] = "@NUMEROCUENTA";
            paramMantFin[11] = "@RUT";
            paramMantFin[12] = "@OBSERVACIONES";
            paramMantFin[13] = "@MONTO";
            paramMantFin[14] = "@VIGENTE";
            paramMantFin[15] = "@SECUENCIA";
            paramMantFin[16] = "@SERIECHQ";
            paramMantFin[17] = "@EMPRESAORIGEN";
            paramMantFin[18] = "@PAGINATION";
            paramMantFin[19] = "@TYPEFILTER";
            paramMantFin[20] = "@DATAFILTER";
            paramMantFin[21] = "@GASTOADM";
            paramMantFin[22] = "@LOCATION";
            paramMantFin[23] = "@CODIGOCOMPLEMENTO";
            paramMantFin[24] = "@CONCEPTO";
            paramMantFin[25] = "@CODIGOHABER";
            paramMantFin[26] = "@CODIGODESCUENTO";
            paramMantFin[27] = "@ORIGEN";
            paramMantFin[28] = "@ORDERBY";
            paramMantFin[29] = "@TIPODOC";
            paramMantFin[30] = "@FECHAINICIOFILTER";
            paramMantFin[31] = "@FECHATERMINOFILTER";
            paramMantFin[32] = "@CLIENTE";
            paramMantFin[33] = "@ESTADO";

            valMantFin[0] = "REPORTEXCELVALEVISTA";
            valMantFin[1] = usuarioCreador;
            valMantFin[2] = "";
            valMantFin[3] = "";
            valMantFin[4] = "";
            valMantFin[5] = proceso;
            valMantFin[6] = "";
            valMantFin[7] = "";
            valMantFin[8] = "";
            valMantFin[9] = "";
            valMantFin[10] = "";
            valMantFin[11] = "";
            valMantFin[12] = "";
            valMantFin[13] = "";
            valMantFin[14] = "";
            valMantFin[15] = "";
            valMantFin[16] = "";
            valMantFin[17] = "";
            valMantFin[18] = "";
            valMantFin[19] = "";
            valMantFin[20] = "";
            valMantFin[21] = "";
            valMantFin[22] = "";
            valMantFin[23] = "";
            valMantFin[24] = "";
            valMantFin[25] = "";
            valMantFin[26] = "";
            valMantFin[27] = "";
            valMantFin[28] = "";
            valMantFin[29] = "";
            valMantFin[30] = "";
            valMantFin[31] = "";
            valMantFin[32] = "";
            valMantFin[33] = "";

            dataMantencion = servicioOperaciones.CrudMantencionFiniquitos(paramMantFin, valMantFin).Table;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                excel.Workbook.Worksheets.Add("Hoja1");
                var worksheet = excel.Workbook.Worksheets["Hoja1"];

                for (var i = 0; i < dataMantencion.Tables[0].Rows.Count; i++)
                {
                    for (var j = 1; j <= dataMantencion.Tables[0].Rows[i].Table.Columns.Count; j++)
                    {
                        worksheet.Cells[(i + 1), j].Value = dataMantencion.Tables[0].Rows[i]["Column" + j.ToString()].ToString();
                    }
                }

                worksheet.Protection.IsProtected = true;
                worksheet.Protection.SetPassword("domCLt3am.W0rk");
                bytes = excel.GetAsByteArray();
            }

            return bytes;
        }
    }
}

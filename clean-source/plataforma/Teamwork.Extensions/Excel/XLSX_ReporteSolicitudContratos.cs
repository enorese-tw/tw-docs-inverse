using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteSolicitudContratos
    {
        static byte[] file;
        static string[] columns = new string[35];
        static string[] nameColumns = new string[35];
        static string[] valuesColumns = new string[35];

        public static byte[] xslx(DataSet data = null)
        {
            try
            {

                #region "Columnas"

                columns[0] = "A";
                columns[1] = "B";
                columns[2] = "C";
                columns[3] = "D";
                columns[4] = "E";
                columns[5] = "F";
                columns[6] = "G";
                columns[7] = "H";
                columns[8] = "I";
                columns[9] = "J";
                columns[10] = "K";
                columns[11] = "L";
                columns[12] = "M";
                columns[13] = "N";
                columns[14] = "O";
                columns[15] = "P";
                columns[16] = "Q";
                columns[17] = "R";
                columns[18] = "S";
                columns[19] = "T";
                columns[20] = "U";
                columns[21] = "V";
                columns[22] = "W";
                columns[23] = "X";
                columns[24] = "Y";
                columns[25] = "Z";
                columns[26] = "AA";
                columns[27] = "AB";
                columns[28] = "AC";
                columns[29] = "AD";
                columns[30] = "AE";
                columns[31] = "AF";
                columns[32] = "AG";
                columns[33] = "AH";
                columns[34] = "AI";

                #endregion

                #region "Nombres de columnas"

                nameColumns[0] = "Fecha Transacción";
                nameColumns[1] = "Ficha";
                nameColumns[2] = "Area Negocio";
                nameColumns[3] = "Rut Empresa";
                nameColumns[4] = "Rut Trabajador";
                nameColumns[5] = "Nombre Trabajador";
                nameColumns[6] = "Codigo BPO";
                nameColumns[7] = "Cargo Mod";
                nameColumns[8] = "Cargo";
                nameColumns[9] = "Sucursal";
                nameColumns[10] = "Ejecutivo";
                nameColumns[11] = "Fecha de Inicio";
                nameColumns[12] = "Fecha de Termino";
                nameColumns[13] = "Causal";
                nameColumns[14] = "Reemplazo";
                nameColumns[15] = "TipoContrato";
                nameColumns[16] = "CC";
                nameColumns[17] = "Dirección";
                nameColumns[18] = "Comuna";
                nameColumns[19] = "Ciudad";
                nameColumns[20] = "Región";
                nameColumns[21] = "Horario";
                nameColumns[22] = "Horas Trabajadas";
                nameColumns[23] = "Colación";
                nameColumns[24] = "Nacionalidad";
                nameColumns[25] = "Visa";
                nameColumns[26] = "Vencimiento Visa";
                nameColumns[27] = "Cliente Firmante";
                nameColumns[28] = "División (Solo FNC)";
                nameColumns[29] = "Descripcion de funciones";
                nameColumns[30] = "Fecha Inicio Primer Plazo";
                nameColumns[31] = "Fecha Término Primer Plazo";
                nameColumns[32] = "Fecha Inicio Segundo Plazo";
                nameColumns[33] = "Fecha Término Segundo Plazo";
                nameColumns[34] = "Renovacion Automática";

                #endregion

                #region "nombres de columnas para obtener datos dinamicos de array"

                valuesColumns[0] = "FechaTransaccion";
                valuesColumns[1] = "Ficha";
                valuesColumns[2] = "Codigo";
                valuesColumns[3] = "RutEmpresa";
                valuesColumns[4] = "Rut";
                valuesColumns[5] = "Nombre";
                valuesColumns[6] = "CargoBPO";
                valuesColumns[7] = "CargoMod";
                valuesColumns[8] = "Cargo";
                valuesColumns[9] = "Sucursal";
                valuesColumns[10] = "Ejecutivo";
                valuesColumns[11] = "FechaInicio";
                valuesColumns[12] = "FechaTermino";
                valuesColumns[13] = "Causal";
                valuesColumns[14] = "Reemplazo";
                valuesColumns[15] = "TipoContrato";
                valuesColumns[16] = "CC";
                valuesColumns[17] = "Direccion";
                valuesColumns[18] = "Comuna";
                valuesColumns[19] = "Ciudad";
                valuesColumns[20] = "Region";
                valuesColumns[21] = "Horario";
                valuesColumns[22] = "HorasTrab";
                valuesColumns[23] = "Colacion";
                valuesColumns[24] = "Nacionalidad";
                valuesColumns[25] = "Visa";
                valuesColumns[26] = "VencVisa";
                valuesColumns[27] = "Firmante";
                valuesColumns[28] = "Division";
                valuesColumns[29] = "DescripcionFunciones";
                valuesColumns[30] = "FechaInicioPPlazo";
                valuesColumns[31] = "FechaTerminoPPlazo";
                valuesColumns[32] = "FechaInicioSPlazo";
                valuesColumns[33] = "FechaTerminoSPlazo";
                valuesColumns[34] = "RenovacionAutomatica";

                #endregion

                using (ExcelPackage excel = new ExcelPackage())
                {
                    foreach (DataRow rows in data.Tables[0].Rows)
                    {
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == "Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()))
                        {
                            excel.Workbook.Worksheets.Add("Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString());
                            var worksheetConfig = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                            
                            for (var i = 0; i < columns.Length; i++)
                            {
                                worksheetConfig.Cells[columns[i] + "1"].Value = nameColumns[i];
                                worksheetConfig.Cells[columns[i] + "1"].Style.Font.Bold = true;
                            }

                        }

                        var worksheet = excel.Workbook.Worksheets["Contratos " + rows["Codigo"].ToString() + " " + rows["Empresa"].ToString()];
                        int contador = worksheet.Dimension.End.Row + 1;

                        for (var h = 0; h < columns.Length; h++)
                        {
                            worksheet.Cells[columns[h] + contador.ToString()].Value = rows[valuesColumns[h]].ToString();
                        }

                        worksheet.Cells[columns[0] + ":" + columns[columns.Length - 1]].AutoFitColumns();
                    }

                    file = excel.GetAsByteArray();

                }

            }
            catch (Exception)
            {
                file = null;
            }

            return file;
        }
    }
}
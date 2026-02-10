using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Teamwork.Model.Asistencia;
using Teamwork.Model.Operaciones;

namespace Teamwork.Extensions.Excel
{
    public class XLSX_ReporteAsistencia
    {
        #region REPORTE ASISTENCIA
        public static byte[] ReporteAsistencia(List<ReporteAsistencia> reporte = null, List<BajasConfirmadas> bajasConfirmadas = null, List<HorasExtras> horaExtra = null, List<ReporteAsistencia> columnas = null, 
                                               List<Leyenda> leyenda = null, List<JornadaLaboral> jornadaLaboral = null, List<FichaBonos> fichaBonos = null, string periodo = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                #region REPORTE ASISTENCIA
                int contador = 0;

                int diasLibres = 0;
                int diasFalta = 0;
                int diasLicencia = 0;
                int diasVacaciones = 0;
                int diasSinContrato = 0;
                int countJornadas = 0;
                int countBonos = 0;

                int diasMes = 30;
                int diasTrabajados = 0;
                int diasMismoTrabajador = 0;

                string ficha = "";
                string rut = "";
                string hoja = periodo.Split('-')[0] + "-" + periodo.Split('-')[1];

                int colCount = 0;
                string[] columns = new string[columnas.Count];
                string[] nameColumns = new string[columnas.Count];
                string[] dataColumns = new string[columnas.Count];

                char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                int vuelta = 0;
                int j = 0;

                Color colFromHexBackColor = new Color();
                Color colFromHexFontColor = new Color();

                DateTime ultimoDia = DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + periodo.Split('-')[2]).AddMonths(1).AddDays(-1);

                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                {
                    excel.Workbook.Worksheets.Add(hoja);
                }

                ExcelWorksheet worksheetConfig = excel.Workbook.Worksheets[hoja];

                #region NOMBRES DE COLUMNAD
                foreach (ReporteAsistencia col in columnas)
                {
                    nameColumns[colCount] = col.Columna;
                    dataColumns[colCount] = col.DataColumna;
                    colCount++;
                }
                #endregion

                #region ASIGNA HEADER
                for (var i = 0; i < columns.Length; i++)
                {
                    if (i > 25)
                    {
                        columns[i] = alfabeto[vuelta].ToString() + alfabeto[j].ToString();
                        j++;
                        if (j > alfabeto.Length - 1)
                        {
                            j -= alfabeto.Length;
                            vuelta++;
                        }
                    }
                    else
                    {
                        columns[i] = alfabeto[i].ToString();
                    }

                    if (int.TryParse(nameColumns[i], out int dia))
                    {
                        colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");
                        worksheetConfig.Cells[columns[i] + "1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        switch (new DateTime(Convert.ToInt32(periodo.Split('-')[0]), Convert.ToInt32(periodo.Split('-')[1]), Convert.ToInt32(nameColumns[i])).DayOfWeek.ToString())
                        {
                            case "Monday":
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Lu";
                                break;
                            case "Tuesday":
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Ma";
                                break;
                            case "Wednesday":
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Mi";
                                break;
                            case "Thursday":
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Ju";
                                break;
                            case "Friday":
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Vi";
                                break;
                            case "Saturday":
                                colFromHexBackColor = ColorTranslator.FromHtml("#BFFFBF");
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Sa";
                                break;
                            case "Sunday":
                                colFromHexBackColor = ColorTranslator.FromHtml("#BFFFBF");
                                worksheetConfig.Cells[columns[i] + "1"].Value = "Do";
                                break;
                        }
                        worksheetConfig.Cells[columns[i] + "1"].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                    }
                    else
                    {
                        worksheetConfig.Cells[columns[i] + "1"].Value = "";
                    }

                    worksheetConfig.Cells[columns[i] + "2"].Value = nameColumns[i];
                    worksheetConfig.Cells[columns[i] + "2"].Style.Font.Bold = true;
                }
                #endregion

                DateTime fechaBajaConfirmada = new DateTime();
                bool hasBajaConfirmada = false;

                bool markSinContrato = false;
                bool markBaja = false;
                bool markBajaInformadaKam = false;

                ExcelWorksheet worksheet = excel.Workbook.Worksheets[hoja];

                foreach (ReporteAsistencia data in reporte)
                {
                    if(data.Ficha != ficha && data.Rut == rut)
                    {
                        diasMismoTrabajador += diasTrabajados;
                    }

                    if (data.Ficha != ficha)
                    {
                        if (data.Rut != rut)
                        {
                            diasMismoTrabajador = 0;
                        }

                        diasSinContrato = 0;
                        diasLibres = 0;
                        diasFalta = 0;
                        diasLicencia = 0;
                        diasVacaciones = 0;
                        hasBajaConfirmada = false;
                        markSinContrato = false;
                        markBajaInformadaKam = false;

                        fechaBajaConfirmada = new DateTime();
                    }

                    diasSinContrato = Convert.ToInt32(data.SinContrato);

                    contador = data.Ficha == ficha ? contador : worksheet.Dimension.End.Row + 1;

                    #region BAJAS CONFIRMADAS
                    foreach (BajasConfirmadas bc in bajasConfirmadas)
                    {
                        if (bc.Ficha == data.Ficha)
                        {
                            hasBajaConfirmada = true;
                            fechaBajaConfirmada = new DateTime(Convert.ToInt32(bc.FechaTermino.Split('-')[0]), Convert.ToInt32(bc.FechaTermino.Split('-')[1]), Convert.ToInt32(bc.FechaTermino.Split('-')[2]));
                        }
                    }
                    #endregion

                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "AreaNegocio")] + contador.ToString()].Value = data.AreaNegocio;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "Nombres")] + contador.ToString()].Value = data.Nombres;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "Rut")] + contador.ToString()].Value = data.Rut;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "Ficha")] + contador.ToString()].Value = data.Ficha;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "CargoMod")] + contador.ToString()].Value = data.CargoMod;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "NombreCargo")] + contador.ToString()].Value = data.NombreCargo;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealInicio")] + contador.ToString()].Value = data.FechaRealInicio;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealTermino")] + contador.ToString()].Value = data.FechaRealTerminoAux;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "TopeLegal")] + contador.ToString()].Value = data.TopeLegal;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "Causal")] + contador.ToString()].Value = data.Causal;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "Estado")] + contador.ToString()].Value = data.Estado;

                    if(data.Plataforma == "Teamwork")
                    {
                        colFromHexBackColor = ColorTranslator.FromHtml("#ffe1b4");

                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "AreaNegocio")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Nombres")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Rut")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Ficha")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "CargoMod")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "NombreCargo")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealInicio")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealTermino")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "TopeLegal")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Causal")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Estado")] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "AreaNegocio")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Nombres")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Rut")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Ficha")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "CargoMod")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "NombreCargo")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealInicio")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "FechaRealTermino")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "TopeLegal")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Causal")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, "Estado")] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                    }

                    DateTime fechaInicio = Convert.ToDateTime(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + periodo.Split('-')[2]);
                    DateTime fechaUltDia = Convert.ToDateTime(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + periodo.Split('-')[2]).AddMonths(1).AddDays(-1);
                    DateTime fechaAsisIni = new DateTime();
                    DateTime fechaAsisFin = new DateTime();
                    DateTime fechaIniContrato = new DateTime();
                    DateTime fechaTerContrato = new DateTime();

                    if (data.FechaInicio != "")
                        fechaAsisIni = Convert.ToDateTime(data.FechaInicio);

                    if (data.FechaRealInicio != "")
                        fechaIniContrato = Convert.ToDateTime(data.FechaRealInicio);

                    if (data.FechaRealTermino != "")
                        fechaTerContrato = Convert.ToDateTime(data.FechaRealTermino);


                    colFromHexBackColor = ColorTranslator.FromHtml(data.Style);
                    colFromHexFontColor = ColorTranslator.FromHtml(data.Color);

                    switch (data.Tipo)
                    {
                        case "LM"://LICENCIA MEDICA
                            diasLicencia += Convert.ToInt32(data.Dias);
                            break;
                        case "V"://VACACIONES
                            diasVacaciones += Convert.ToInt32(data.Dias);
                            break;
                        case "F"://FALTAS
                            diasFalta++;
                            break;
                        case "L"://LIBRES
                            diasLibres++;
                            break;
                        case "SL"://SUSPENCION LABORAL
                            diasFalta++;
                            break;
                        case "PGS"://PERMISO GOSE SUELDO
                            diasLibres++;
                            break;
                        case "PSG"://PERMISO SIN COSE SUELDO
                            diasFalta++;
                            break;
                        case "PLM"://PROYECION LICENCIA MEDICA
                            diasFalta++;
                            break;
                        case "PF"://PROYECCION FALTA
                            diasFalta++;
                            break;
                        case "CPR"://CRIANZA PROTEGIDA
                            diasFalta++;
                            break;
                        case "R"://RENUNCIA
                            diasFalta++;
                            break;
                    }

                    for (var i = 0; i < Convert.ToInt32(data.UltimoDia); i++)
                    {
                        string cellValue = (string)worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value;
                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                        markSinContrato = false;
                        markBaja = false;

                        if (data.FechaInicio != "")
                        {
                            fechaAsisFin = Convert.ToDateTime(data.FechaInicio).AddDays(Convert.ToDouble(data.Dias));

                            if (fechaAsisFin.Day == DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)).Day)
                            {
                                switch (data.Tipo)
                                {
                                    case "LM":
                                        switch (cellValue)
                                        {
                                            case "V":
                                                diasVacaciones--;
                                                break;
                                            case "F":
                                                diasFalta--;
                                                break;
                                            case "L":
                                                diasLibres--;
                                                break;
                                            case "FN":
                                                diasSinContrato--;
                                                break;
                                        }
                                        break;

                                    case "V":
                                        switch (cellValue)
                                        {
                                            case "F":
                                                diasFalta--;
                                                break;
                                            case "L":
                                                diasLibres--;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }

                        if (cellValue != "LM")
                        {
                            if (data.FechaRealInicio != "")
                            {
                                if (Convert.ToDateTime(data.FechaRealInicio).Month == Convert.ToDateTime(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)).Month &&
                                    Convert.ToDateTime(data.FechaRealInicio).Year == Convert.ToDateTime(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)).Year)
                                {
                                    if (Convert.ToDateTime(data.FechaRealInicio).Day > Convert.ToDateTime(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)).Day)
                                    {
                                        colFromHexBackColor = ColorTranslator.FromHtml("#BCBCBC");
                                        colFromHexFontColor = ColorTranslator.FromHtml("#000000");
                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "SC";
                                        markSinContrato = true;
                                    }
                                }


                                if (!markSinContrato)
                                {
                                    if (hasBajaConfirmada)
                                    {
                                        if (fechaBajaConfirmada.Day < (i + 1))
                                        {
                                            if (fechaBajaConfirmada < fechaTerContrato)
                                            {
                                                if(fechaTerContrato < DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)))
                                                {
                                                    colFromHexBackColor = ColorTranslator.FromHtml("#917a7a");
                                                    colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "PCK";
                                                }
                                                else
                                                {
                                                    colFromHexBackColor = ColorTranslator.FromHtml("#595472");
                                                    colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "BC";
                                                    diasSinContrato++;
                                                }
                                            }
                                            else if(fechaBajaConfirmada == fechaTerContrato)
                                            {
                                                if (fechaTerContrato < DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)))
                                                {
                                                    colFromHexBackColor = ColorTranslator.FromHtml("#595472");
                                                    colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "BC";
                                                }
                                                else
                                                {
                                                    colFromHexBackColor = ColorTranslator.FromHtml("#917a7a");
                                                    colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                    worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "PCK";
                                                }
                                            }
                                            else
                                            {
                                                colFromHexBackColor = ColorTranslator.FromHtml("#917a7a");
                                                colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "PCK";
                                            }
                                            markBaja = true;
                                        }
                                    }

                                    if (!markBaja)
                                    {
                                        if (markBajaInformadaKam)
                                        {
                                            if (fechaTerContrato < DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)))
                                            {
                                                colFromHexBackColor = ColorTranslator.FromHtml("#917a7a");
                                                colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "PCK";
                                            }
                                            else
                                            {
                                                colFromHexBackColor = ColorTranslator.FromHtml("#B703FA");
                                                colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "BK";
                                                diasSinContrato++;
                                            }
                                        }
                                        else
                                        {

                                            if (fechaTerContrato < DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)))
                                            {
                                                colFromHexBackColor = ColorTranslator.FromHtml("#917a7a");
                                                colFromHexFontColor = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "PCK";
                                            }
                                            else
                                            {
                                                if (fechaAsisIni.Day == (i + 1))
                                                {
                                                    if (Convert.ToInt32(string.Concat(fechaAsisIni.Year.ToString(), fechaAsisIni.Month < 10 ? "0" + fechaAsisIni.Month.ToString() : fechaAsisIni.Month.ToString(), fechaAsisIni.Day < 10 ? "0" + fechaAsisIni.Day.ToString() : fechaAsisIni.Day.ToString())) < 
                                                        Convert.ToInt32(string.Concat(fechaAsisFin.Year.ToString(), fechaAsisFin.Month < 10 ? "0" + fechaAsisFin.Month.ToString() : fechaAsisFin.Month.ToString(), fechaAsisFin.Day < 10 ? "0" + fechaAsisFin.Day.ToString() : fechaAsisFin.Day.ToString()))) 
                                                    {
                                                        colFromHexBackColor = ColorTranslator.FromHtml(data.Style);
                                                        colFromHexFontColor = ColorTranslator.FromHtml(data.Color);
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = data.Tipo;
                                                        fechaAsisIni = fechaAsisIni.AddDays(1);

                                                        if (data.Tipo == "BK")
                                                        {
                                                            markBajaInformadaKam = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (cellValue == "" || cellValue == null)
                                                        {
                                                            colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");
                                                            colFromHexFontColor = ColorTranslator.FromHtml("#000000");
                                                            worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                            worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                            worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "P";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (cellValue == "" || cellValue == null)
                                                    {
                                                        colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");
                                                        colFromHexFontColor = ColorTranslator.FromHtml("#000000");
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                                                        worksheet.Cells[columns[Array.IndexOf(dataColumns, (i + 1).ToString())] + contador.ToString()].Value = "P";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    #region HORAS EXTRAS
                    countJornadas = 0;
                    foreach (HorasExtras hhee in horaExtra)
                    {
                        if(data.Ficha == hhee.Ficha)
                        {
                            worksheet.Cells[columns[(Array.IndexOf(dataColumns, "JornadaLaboral") + countJornadas)] + contador.ToString()].Value = hhee.NombreJornada;
                            worksheet.Cells[columns[(Array.IndexOf(dataColumns, "HorasExtras") + countJornadas)] + contador.ToString()].Value = hhee.HoraExtra;
                            worksheet.Cells[columns[(Array.IndexOf(dataColumns, "PorcentajePago") + countJornadas)] + contador.ToString()].Value = hhee.PorcentajePago;
                            countJornadas += 3;
                        }
                    }
                    #endregion

                    #region BONOS
                    countBonos = 0;
                    foreach (FichaBonos fichabono in fichaBonos)
                    {
                        if(data.Ficha == fichabono.Ficha && data.AreaNegocio == fichabono.AreaNegocio)
                        {
                            worksheet.Cells[columns[(Array.IndexOf(dataColumns, "Bono") + countBonos)] + contador.ToString()].Value = fichabono.Bono;
                            worksheet.Cells[columns[(Array.IndexOf(dataColumns, "ValorBono") + countBonos)] + contador.ToString()].Value = fichabono.ValorBono;
                            countBonos += 2;
                        }
                    }
                    #endregion

                    if (diasSinContrato > 0)
                    {
                        switch(Convert.ToInt32(data.UltimoDia))
                        {
                            case 28:
                                diasSinContrato += 2;
                                break;
                            case 29:
                                diasSinContrato++;
                                break;
                            case 31:
                                diasSinContrato--;
                                break;
                        }
                    }

                    if(data.Rut == "18826824-2" && data.Ficha == "C302105084")
                    {

                    }

                    diasTrabajados = (Convert.ToInt32(diasSinContrato)) < 0
                        ? diasMes - (diasFalta + diasLicencia)
                        : diasMes - (Convert.ToInt32(diasSinContrato)) - (diasFalta + diasLicencia);

                    if (diasTrabajados + diasMismoTrabajador > diasMes)
                    {
                        diasTrabajados -= (diasTrabajados + diasMismoTrabajador - diasMes);
                    }

                    if((diasTrabajados + (diasSinContrato + diasFalta + diasLicencia)) < diasMes)
                    {
                        diasSinContrato += (diasMes - (diasTrabajados + diasSinContrato));
                    }

                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasTrabajados")] + contador.ToString()].Value = diasTrabajados < 0 ? 0 : diasTrabajados;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasLibres")] + contador.ToString()].Value = diasLibres;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasFaltas")] + contador.ToString()].Value = diasFalta;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasLicenciaMedica")] + contador.ToString()].Value = diasLicencia;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasVacaciones")] + contador.ToString()].Value = diasVacaciones;
                    worksheet.Cells[columns[Array.IndexOf(dataColumns, "DiasSinContrato")] + contador.ToString()].Value = (Convert.ToInt32(diasSinContrato)) < 0 ? 0 : (Convert.ToInt32(diasSinContrato));


                    ficha = data.Ficha;
                    rut = data.Rut;
                    worksheet.Cells.AutoFitColumns();
                }
                #endregion

                #region REPORTE LEYENDA
                contador = 0;
                hoja = "Tipos Ausencias";

                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                {
                    excel.Workbook.Worksheets.Add(hoja);
                }

                ExcelWorksheet worksheetLegend = excel.Workbook.Worksheets[hoja];

                worksheetLegend.Cells["A1"].Value = "Tipo Inasistensia";
                worksheetLegend.Cells["B1"].Value = "";

                foreach (Leyenda legend in leyenda)
                {
                    contador = worksheetLegend.Dimension.End.Row + 1;

                    colFromHexBackColor = ColorTranslator.FromHtml(legend.Style);
                    colFromHexFontColor = ColorTranslator.FromHtml(legend.Color);

                    worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                    worksheetLegend.Cells["A" + contador.ToString()].Value = legend.Nombre;
                    worksheetLegend.Cells["B" + contador.ToString()].Value = legend.CodigoAsistencia;
                    worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                    worksheetLegend.Cells["B" + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                    worksheetLegend.Cells.AutoFitColumns();
                }

                #endregion

                #region JORNADAS LABORALES
                string areaNegocio = "";
                contador = 0;
                hoja = "Jornadas Laborales";

                if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                {
                    excel.Workbook.Worksheets.Add(hoja);
                }

                ExcelWorksheet worksheetJornadas = excel.Workbook.Worksheets[hoja];

                worksheetJornadas.Cells["A1"].Value = "Area Negocio";
                worksheetJornadas.Cells["B1"].Value = "Codigo";
                worksheetJornadas.Cells["C1"].Value = "Jornada Laboral";
                worksheetJornadas.Cells["D1"].Value = "Descripcion";
                worksheetJornadas.Cells["E1"].Value = "Horas Semanales";
                worksheetJornadas.Cells["F1"].Value = "Porcentaje Pago";

                
                foreach (JornadaLaboral jornada in jornadaLaboral)
                {
                    if (jornada.Code == "200")
                    {
                        contador = worksheetJornadas.Dimension.End.Row + 1;

                        worksheetJornadas.Cells["A" + contador.ToString()].Value = jornada.AreaNegocio;
                        worksheetJornadas.Cells["B" + contador.ToString()].Value = jornada.CodigoJornada;
                        worksheetJornadas.Cells["C" + contador.ToString()].Value = jornada.NombreJornada;
                        worksheetJornadas.Cells["D" + contador.ToString()].Value = jornada.DescripcionJornada;
                        worksheetJornadas.Cells["E" + contador.ToString()].Value = jornada.HorasSemanal;
                        worksheetJornadas.Cells["F" + contador.ToString()].Value = jornada.PorcentajePago;

                        areaNegocio = jornada.AreaNegocio;
                    }

                    worksheetJornadas.Cells.AutoFitColumns();
                }
                #endregion
                excel.Workbook.Properties.Title = "Attempts";
                return excel.GetAsByteArray();
            }
        }

        public static byte[] ReporteAsistenciaV2(List<ReporteAsistencia> reporte = null, List<BajasConfirmadas> bajasConfirmadas = null, List<HorasExtras> horaExtra = null, List<ReporteAsistencia> columnas = null, string periodo = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                var jornadaLaboral = "";
                var ficha = "";
                var cellCount = 0;
                var contador = 0;
                var conteos = 0;

                var diasLibres = 0;
                var diasFalta = 0;
                var diasLicencia = 0;
                var diasVacaciones = 0;
                var diasSinContrato = 0;
                
                int colExtra = 13;

                foreach (ReporteAsistencia data in reporte)
                {
                    int colCount = Convert.ToInt32(data.UltimoDia) + colExtra;
                    string[] columns = new string[colCount];
                    string[] nameColumns = new string[colCount];
                    string[] valuesColumns = new string[colCount];

                    bool markBajaConfirmada = false;

                    conteos++;

                    char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    var vuelta = 0;
                    var j = 0;

                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == data.Hoja))
                    {
                        excel.Workbook.Worksheets.Add(data.Hoja);
                    }

                    var worksheetConfig = excel.Workbook.Worksheets[data.Hoja];

                    nameColumns[0] = "Area Negocio";
                    nameColumns[1] = "Nombres";
                    nameColumns[2] = "Rut";
                    nameColumns[3] = "Ficha";
                    nameColumns[4] = "Cargo";
                    nameColumns[5] = "Fecha Inicio Contrato";
                    nameColumns[6] = "Fecha Termino Contrato";

                    nameColumns[colCount - 6] = "Dias Trabajados";
                    nameColumns[colCount - 5] = "Dias Libres";
                    nameColumns[colCount - 4] = "Dias Faltas";
                    nameColumns[colCount - 3] = "Dias Licencia Medica";
                    nameColumns[colCount - 2] = "Dias Vacaciones";
                    nameColumns[colCount - 1] = "Dias Sin Contrato";

                    DateTime fechaBajaConfirmada = new DateTime();
                    bool hasBajaConfirmada = false;

                    if (data.Ficha != ficha)
                    {
                        diasLibres = 0;
                        diasFalta = 0;
                        diasLicencia = 0;
                        diasVacaciones = 0;
                        hasBajaConfirmada = false;
                    }

                    foreach (BajasConfirmadas bc in bajasConfirmadas)
                    {
                        if (bc.Ficha == data.Ficha)
                        {
                            hasBajaConfirmada = true;
                            fechaBajaConfirmada = new DateTime(Convert.ToInt32(bc.FechaTermino.Split('-')[0]), Convert.ToInt32(bc.FechaTermino.Split('-')[1]), Convert.ToInt32(bc.FechaTermino.Split('-')[2]));
                        }
                    }

                    for (var i = 0; i < columns.Length; i++)
                    {
                        if (i > 6 && i < (Convert.ToInt32(data.UltimoDia) + 7))
                        {
                            nameColumns[i] = Convert.ToString(i - 6);
                        }

                        if (i > 25)
                        {
                            columns[i] = alfabeto[vuelta].ToString() + alfabeto[j].ToString();
                            j++;
                            if (j > alfabeto.Length - 1)
                            {
                                j = j - alfabeto.Length;
                                vuelta++;
                            }
                        }
                        else
                        {
                            columns[i] = alfabeto[i].ToString();
                        }


                        worksheetConfig.Cells[columns[i] + "1"].Value = nameColumns[i];
                        worksheetConfig.Cells[columns[i] + "1"].Style.Font.Bold = true;

                    }

                    var worksheet = excel.Workbook.Worksheets[data.Hoja];

                    contador = data.Ficha == ficha ? contador : worksheet.Dimension.End.Row + 1;


                    worksheet.Cells[columns[0] + contador.ToString()].Value = data.AreaNegocio;
                    worksheet.Cells[columns[1] + contador.ToString()].Value = data.Nombres;
                    worksheet.Cells[columns[2] + contador.ToString()].Value = data.Rut;
                    worksheet.Cells[columns[3] + contador.ToString()].Value = data.Ficha;
                    worksheet.Cells[columns[4] + contador.ToString()].Value = data.CargoMod;
                    worksheet.Cells[columns[5] + contador.ToString()].Value = data.FechaRealInicio;
                    worksheet.Cells[columns[6] + contador.ToString()].Value = data.FechaRealTerminoAux;

                    DateTime fechaInicio = Convert.ToDateTime(data.Hoja + "-01");
                    DateTime fechaUltDia = Convert.ToDateTime(data.Hoja + "-" + data.UltimoDia);
                    DateTime fechaAsisIni = new DateTime();
                    DateTime fechaAsisFin = new DateTime();
                    DateTime fechaIniContrato = new DateTime();
                    DateTime fechaTerContrato = new DateTime();

                    if (data.FechaInicio != "")
                        fechaAsisIni = Convert.ToDateTime(data.FechaInicio);

                    if (data.FechaRealInicio != "")
                        fechaIniContrato = Convert.ToDateTime(data.FechaRealInicio);

                    if (data.FechaRealTermino != "")
                        fechaTerContrato = Convert.ToDateTime(data.FechaRealTermino);
                    

                    Color colFromHex = ColorTranslator.FromHtml(data.Style);

                    if(data.Ficha != ficha)
                    {
                        diasSinContrato = Convert.ToInt32(data.SinContrato);
                    }

                    switch (data.Tipo)
                    {
                        case "LM":
                            diasLicencia += Convert.ToInt32(data.Dias);
                            break;
                        case "V":
                            diasVacaciones += Convert.ToInt32(data.Dias);
                            break;
                        case "F":
                            diasFalta++;
                            break;
                        case "L":
                            diasLibres++;
                            break;
                    }

                    #region
                    for (var i = 0; i < Convert.ToInt32(data.UltimoDia); i++)
                    {
                        var cellValue = (string)worksheet.Cells[columns[i + 7] + contador.ToString()].Value;

                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                        if (data.FechaInicio != "")
                        {
                            fechaAsisFin = Convert.ToDateTime(data.FechaInicio).AddDays(Convert.ToDouble(data.Dias));

                            if (fechaAsisFin.Day > DateTime.Parse(periodo.Split('-')[0] + "-" + periodo.Split('-')[1] + "-" + (i + 1)).Day)
                            {
                                switch (data.Tipo)
                                {
                                    case "LM":
                                        switch (cellValue)
                                        {
                                            case "V":
                                                diasVacaciones--;
                                                break;
                                            case "F":
                                                diasFalta--;
                                                break;
                                            case "L":
                                                diasLibres--;
                                                break;
                                            case "FN":
                                                diasSinContrato--;
                                                break;
                                        }
                                        break;

                                    case "V":
                                        switch (cellValue)
                                        {
                                            case "F":
                                                diasFalta--;
                                                break;
                                            case "L":
                                                diasLibres--;
                                                break;
                                        }
                                        break;
                                }
                            }
                        }

                        if (cellValue != "LM")
                        {
                            if (data.FechaInicio == "")
                            {
                                if (data.FechaRealInicio != "")
                                {
                                    if (fechaIniContrato > fechaInicio)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#BCBCBC");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "SC";
                                    }
                                    else
                                    {
                                        //if (fechaInicio <= DateTime.Today)
                                        //{
                                            if (data.FechaRealTermino == "")
                                            {
                                                colFromHex = ColorTranslator.FromHtml(data.Style);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                            }
                                            else
                                            {
                                                if (fechaTerContrato < fechaInicio)
                                                {
                                                    colFromHex = ColorTranslator.FromHtml("#595472");
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "BC";
                                                }
                                                else
                                                {
                                                    colFromHex = ColorTranslator.FromHtml(data.Style);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                                }
                                            }

                                        //}
                                        //else
                                        //{
                                        //    colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                        //    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        //    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                        //}
                                    }
                                }
                                else if (data.FechaRealTermino != "")
                                {
                                    if (fechaTerContrato < fechaInicio)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#595472");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "BC";
                                    }
                                    else
                                    {
                                        colFromHex = ColorTranslator.FromHtml(data.Style);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                    }
                                }
                                else
                                {

                                    if (fechaInicio <= DateTime.Today)
                                    {
                                        colFromHex = ColorTranslator.FromHtml(data.Style);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                    }
                                    else
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                    }
                                }
                            }
                            else
                            {
                                //DateTime fechaAsisFin = Convert.ToDateTime(data.FechaInicio).AddDays(Convert.ToDouble(data.Dias));

                                if (data.FechaRealInicio != "")
                                {
                                    if (fechaIniContrato > fechaInicio)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#BCBCBC");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "SC";
                                    }
                                    else if (fechaInicio >= fechaAsisIni)
                                    {
                                        if (fechaAsisIni < fechaAsisFin)
                                        {
                                            colFromHex = ColorTranslator.FromHtml(data.Style);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                            fechaAsisIni = fechaAsisIni.AddDays(1);
                                        }
                                        else
                                        {
                                            //if (fechaInicio <= DateTime.Today)
                                            //{
                                                if (data.FechaRealTermino != "")
                                                {
                                                    if (fechaTerContrato < fechaInicio)
                                                    {
                                                        colFromHex = ColorTranslator.FromHtml("#595472");
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "BC";
                                                    }
                                                    else
                                                    {
                                                        if (cellValue == "" || cellValue == null)
                                                        {
                                                            colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (cellValue == "" || cellValue == null)
                                                    {
                                                        colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                                    }
                                                }
                                            //}
                                            //else
                                            //{
                                            //    if (!markBajaConfirmada)
                                            //    {
                                            //        if (hasBajaConfirmada)
                                            //        {
                                            //            if (fechaBajaConfirmada == fechaInicio)
                                            //            {
                                            //                if (fechaBajaConfirmada == fechaTerContrato)
                                            //                {
                                            //                    colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            //                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            //                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                            //                    markBajaConfirmada = true;
                                            //                }
                                            //            }
                                            //            else
                                            //            {
                                            //                colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            //                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            //                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                            //            }
                                            //        }
                                            //        else
                                            //        {
                                            //            colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            //            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            //            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        colFromHex = ColorTranslator.FromHtml("#595472");
                                            //        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            //        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "BC";
                                            //    }
                                            //}
                                        }
                                    }
                                    else
                                    {
                                        if (cellValue == "" || cellValue == null)
                                        {
                                            colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                        }
                                        else if (fechaInicio >= fechaAsisIni)
                                        {
                                            if (fechaAsisIni < fechaAsisFin)
                                            {
                                                colFromHex = ColorTranslator.FromHtml(data.Style);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                                fechaAsisIni = fechaAsisIni.AddDays(1);
                                            }
                                            else
                                            {
                                                if (cellValue == "" || cellValue == null)
                                                {
                                                    colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (data.FechaRealTermino != "")
                                {
                                    if (fechaTerContrato < fechaInicio)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#7F7F7F");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "FN";
                                    }
                                }
                                else
                                {
                                    if (fechaInicio >= fechaAsisIni)
                                    {
                                        if (fechaAsisIni < fechaAsisFin)
                                        {
                                            colFromHex = ColorTranslator.FromHtml(data.Style);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                            fechaAsisIni = fechaAsisIni.AddDays(1);
                                        }
                                        else
                                        {
                                            if (cellValue == "" || cellValue == null)
                                            {
                                                colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (cellValue == "" || cellValue == null)
                                        {
                                            colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                        }
                                    }
                                }
                            }
                        }

                        #region
                        /*
                        if (data.FechaInicio == "")
                        {
                            if (data.FechaRealInicio != "")
                            {
                                if (fechaIniContrato > fechaInicio)
                                {
                                    colFromHex = ColorTranslator.FromHtml("#BCBCBC");
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "SC";
                                }
                                else
                                {
                                    if (fechaInicio <= DateTime.Today)
                                    {
                                        if (data.FechaRealTermino == "")
                                        {
                                            colFromHex = ColorTranslator.FromHtml(data.Style);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                        }
                                        else
                                        {
                                            if (fechaTerContrato < fechaInicio)
                                            {
                                                colFromHex = ColorTranslator.FromHtml("#7F7F7F");
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "FN";
                                            }
                                            else
                                            {
                                                colFromHex = ColorTranslator.FromHtml(data.Style);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#EAECEE");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = " ";
                                    }
                                }
                            }
                            else if (data.FechaRealTermino != "")
                            {
                                if (fechaTerContrato < fechaInicio)
                                {
                                    colFromHex = ColorTranslator.FromHtml("#7F7F7F");
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "FN";
                                }
                                else
                                {
                                    colFromHex = ColorTranslator.FromHtml(data.Style);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                }
                            }
                            else
                            {

                                if (fechaInicio <= DateTime.Today)
                                {
                                    colFromHex = ColorTranslator.FromHtml(data.Style);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                }
                                else
                                {
                                    colFromHex = ColorTranslator.FromHtml("#EAECEE");
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = " ";
                                }
                            }
                        }
                        else
                        {
                            DateTime fechaAsisFin = Convert.ToDateTime(data.FechaInicio).AddDays(Convert.ToDouble(data.Dias));

                            if (data.FechaRealInicio != "")
                            {
                                if (fechaIniContrato > fechaInicio)
                                {
                                    colFromHex = ColorTranslator.FromHtml("#BCBCBC");
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "SC";
                                }
                                else if (fechaInicio >= fechaAsisIni)
                                {
                                    if (fechaAsisIni < fechaAsisFin)
                                    {
                                        colFromHex = ColorTranslator.FromHtml(data.Style);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                        fechaAsisIni = fechaAsisIni.AddDays(1);
                                    }
                                    else
                                    {
                                        if (fechaInicio <= DateTime.Today)
                                        {
                                            if (data.FechaRealTermino != "")
                                            {
                                                if (fechaTerContrato < fechaInicio)
                                                {
                                                    colFromHex = ColorTranslator.FromHtml("#7F7F7F");
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "FN";
                                                }
                                                else
                                                {
                                                    if (cellValue == "" || cellValue == null)
                                                    {
                                                        colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (cellValue == "" || cellValue == null)
                                                {
                                                    colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            colFromHex = ColorTranslator.FromHtml("#EAECEE");
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = " ";
                                        }
                                    }
                                }
                                else
                                {
                                    if (cellValue == "" || cellValue == null)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                    }
                                    else if (fechaInicio >= fechaAsisIni)
                                    {
                                        if (fechaAsisIni < fechaAsisFin)
                                        {
                                            colFromHex = ColorTranslator.FromHtml(data.Style);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                            fechaAsisIni = fechaAsisIni.AddDays(1);
                                        }
                                        else
                                        {
                                            if (cellValue == "" || cellValue == null)
                                            {
                                                colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                                worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                            }
                                        }
                                    }
                                }
                            }
                            else if (data.FechaRealTermino != "")
                            {
                                if (fechaTerContrato < fechaInicio)
                                {
                                    colFromHex = ColorTranslator.FromHtml("#7F7F7F");
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                    worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "FN";
                                }
                            }
                            else
                            {
                                if (fechaInicio >= fechaAsisIni)
                                {
                                    if (fechaAsisIni < fechaAsisFin)
                                    {
                                        colFromHex = ColorTranslator.FromHtml(data.Style);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = data.Tipo;
                                        fechaAsisIni = fechaAsisIni.AddDays(1);
                                    }
                                    else
                                    {
                                        if (cellValue == "" || cellValue == null)
                                        {
                                            colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                            worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                        }
                                    }
                                }
                                else
                                {
                                    if (cellValue == "" || cellValue == null)
                                    {
                                        colFromHex = ColorTranslator.FromHtml("#FFFFFF");
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHex);
                                        worksheet.Cells[columns[i + 7] + contador.ToString()].Value = "P";
                                    }
                                }
                            }
                        }
                        */
                        #endregion

                        fechaInicio = fechaInicio.AddDays(1);
                        ficha = data.Ficha;

                        worksheet.Cells.AutoFitColumns();
                    }
                    #endregion

                    //var diasTrabajados = ((Convert.ToInt32(data.UltimoDia) == 31 ?  30 : Convert.ToInt32(data.UltimoDia)) - (diasFalta + diasLicencia + Convert.ToInt32(data.SinContrato)));
                    int diasDifMes = 0;
                    int diasMes = 30;
                    int diasTrabajados = 0;
                    DateTime ultimoDay = DateTime.Parse(periodo).AddMonths(1).AddDays(-1);

                    //if (ultimoDay.Year == DateTime.Today.Year && ultimoDay.Month == DateTime.Today.Month)
                    //{
                    //    diasDifMes = ((DateTime.Parse(periodo).AddMonths(1)).AddDays(-1) - DateTime.Today).Days;
                    //    if(ultimoDay.Day != DateTime.Today.Day)
                    //    {
                    //        diasMes = Convert.ToInt32(data.UltimoDia);
                    //    }
                    //}

                    diasTrabajados = (Convert.ToInt32(diasSinContrato) - diasDifMes) < 0
                        ? diasMes - (diasDifMes) - (diasFalta + diasLicencia)
                        : diasMes - (diasDifMes) - (Convert.ToInt32(diasSinContrato) - diasDifMes) - (diasFalta + diasLicencia);


                    worksheet.Cells[columns[colCount - 6] + contador.ToString()].Value = diasTrabajados < 0 ? 0 : diasTrabajados;
                    worksheet.Cells[columns[colCount - 5] + contador.ToString()].Value = diasLibres;
                    worksheet.Cells[columns[colCount - 4] + contador.ToString()].Value = diasFalta;
                    worksheet.Cells[columns[colCount - 3] + contador.ToString()].Value = diasLicencia;
                    worksheet.Cells[columns[colCount - 2] + contador.ToString()].Value = diasVacaciones;
                    worksheet.Cells[columns[colCount - 1] + contador.ToString()].Value = (Convert.ToInt32(diasSinContrato) - diasDifMes) < 0 ? 0 : (Convert.ToInt32(diasSinContrato) - diasDifMes);
                }

                excel.Workbook.Properties.Title = "Attempts";
                return excel.GetAsByteArray();
            }
        }
        #endregion

        #region PLANTILLA CARGA MASIVA
        public static byte[] DownloadCargaMasivaRetail(List<ResponseCarga> download = null, List<Personal> personal = null, List<Leyenda> leyenda = null, List<JornadaLaboral> jornadaLaboral = null, 
                                                       List<Bonos> bonos = null, dynamic errores = null, string codigoTransaccion = null, string empresa = null, string fecha = null) 
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                int vuelta = 0;
                int index = 0;
                int count = 0;
                int contador = 0;
                string cellIndex = "";
                string hoja = "";

                Color colFromHexBackColor = new Color();
                Color colFromHexFontColor = new Color();

                foreach (ResponseCarga item in download)
                {
                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == item.NombreHojaCargaMasiva))
                    {
                        excel.Workbook.Worksheets.Add(item.NombreHojaCargaMasiva);
                    }

                    ExcelWorksheet worksheet = excel.Workbook.Worksheets[item.NombreHojaCargaMasiva];

                    colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");

                    foreach (string columna in item.Columnas.Split(':'))
                    {
                        if (index > 25)
                        {
                            cellIndex = alfabeto[vuelta].ToString() + alfabeto[Convert.ToInt32(count)].ToString();
                            count++;

                            if (count > 25)
                            {
                                vuelta++;
                            }
                        }
                        else
                        {
                            cellIndex = alfabeto[Convert.ToInt32(index)].ToString();
                        }

                        index++;
                        worksheet.Cells[cellIndex + "2"].Value = columna.Split('@')[0];

                        if (int.TryParse(columna.Split('@')[0], out int dia))
                        {
                            colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");
                            worksheet.Cells[cellIndex + "1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            switch (new DateTime(Convert.ToInt32(fecha.Split('-')[0]), Convert.ToInt32(fecha.Split('-')[1]), dia).DayOfWeek.ToString())
                            {
                                case "Monday":
                                    worksheet.Cells[cellIndex + "1"].Value = "Lu";
                                    break;
                                case "Tuesday":
                                    worksheet.Cells[cellIndex + "1"].Value = "Ma";
                                    break;
                                case "Wednesday":
                                    worksheet.Cells[cellIndex + "1"].Value = "Mi";
                                    break;
                                case "Thursday":
                                    worksheet.Cells[cellIndex + "1"].Value = "Ju";
                                    break;
                                case "Friday":
                                    worksheet.Cells[cellIndex + "1"].Value = "Vi";
                                    break;
                                case "Saturday":
                                    colFromHexBackColor = ColorTranslator.FromHtml("#BFFFBF");
                                    worksheet.Cells[cellIndex + "1"].Value = "Sa";
                                    break;
                                case "Sunday":
                                    colFromHexBackColor = ColorTranslator.FromHtml("#BFFFBF");
                                    worksheet.Cells[cellIndex + "1"].Value = "Do";
                                    break;
                            }
                            worksheet.Cells[cellIndex + "1"].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                        }
                        else
                        {
                            worksheet.Cells[cellIndex + "1"].Value = "";
                        }

                        if (codigoTransaccion != null)
                        {
                            for (dynamic i = 1; i <= errores.Count; i++)
                            {
                                for (dynamic j = 0; j < item.Columnas.Split(':').Length; j++)
                                {
                                    worksheet.Cells[item.Columnas.Split(':')[j].Split('@')[1] + "" + (i + 1)].Value = (errores[(i - 1)][item.GetValueError.Split('@')[j].Replace(@"""", "")]).ToString();
                                }
                            }

                            worksheet.Cells[1, item.Columnas.Split(':').Length + 1].Value = "Observaciones a Corregir";

                            for (dynamic h = 1; h <= errores.Count; h++)
                            {
                                worksheet.Cells[(h + 1), item.Columnas.Split(':').Length + 1].Value = (errores[(h - 1)]["ObsProcesamiento"]).ToString();
                            }
                        }
                    }

                    #region PERSONAL
                    if (personal.Count > 0)
                    {
                        foreach (Personal data in personal)
                        {
                            contador = worksheet.Dimension.End.Row + 1;

                            worksheet.Cells["A" + contador.ToString()].Value = empresa;
                            worksheet.Cells["B" + contador.ToString()].Value = data.AreaNegocio;
                            worksheet.Cells["C" + contador.ToString()].Value = data.Ficha;
                            worksheet.Cells["D" + contador.ToString()].Value = data.Rut;
                            worksheet.Cells["E" + contador.ToString()].Value = data.Nombres;
                            worksheet.Cells["F" + contador.ToString()].Value = data.FechaInicio;
                            worksheet.Cells["G" + contador.ToString()].Value = data.FechaTermino;

                            if (data.Plataforma == "Teamwork")
                            {
                                colFromHexBackColor = ColorTranslator.FromHtml("#ffe1b4");

                                worksheet.Cells["A" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["B" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["C" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["D" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["E" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["F" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells["G" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                                worksheet.Cells["A" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["B" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["C" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["D" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["E" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["F" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheet.Cells["G" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                            }
                        }
                        worksheet.Cells.AutoFitColumns();
                    }
                    #endregion

                    #region LEYENDA
                    if (leyenda.Count > 0)
                    {
                        contador = 0;
                        hoja = "Tipos Ausencias";
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetLegend = excel.Workbook.Worksheets[hoja];

                        worksheetLegend.Cells["A1"].Value = "Tipo Inasistensia";
                        worksheetLegend.Cells["B1"].Value = "";

                        foreach (Leyenda legend in leyenda)
                        {
                            contador = worksheetLegend.Dimension.End.Row + 1;

                            colFromHexBackColor = ColorTranslator.FromHtml(legend.Style);
                            colFromHexFontColor = ColorTranslator.FromHtml(legend.Color);

                            worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            worksheetLegend.Cells["A" + contador.ToString()].Value = legend.Nombre;
                            worksheetLegend.Cells["B" + contador.ToString()].Value = legend.CodigoAsistencia;
                            worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                            worksheetLegend.Cells["B" + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                            worksheetLegend.Cells.AutoFitColumns();
                        }
                    }
                    #endregion

                    #region JORNADAS LABORALES
                    if (jornadaLaboral.Count > 0)
                    {
                        string areaNegocio = "";
                        contador = 0;
                        hoja = "Jornadas Laborales";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetJornadas = excel.Workbook.Worksheets[hoja];

                        worksheetJornadas.Cells["A1"].Value = "Area Negocio";
                        worksheetJornadas.Cells["B1"].Value = "Codigo";
                        worksheetJornadas.Cells["C1"].Value = "Jornada Laboral";
                        worksheetJornadas.Cells["D1"].Value = "Descripcion";
                        worksheetJornadas.Cells["E1"].Value = "Horas Semanales";
                        worksheetJornadas.Cells["F1"].Value = "Porcentaje Pago";

                        foreach (JornadaLaboral jornada in jornadaLaboral)
                        {
                            if (jornada.Code == "200")
                            {
                                contador = worksheetJornadas.Dimension.End.Row + 1;

                                worksheetJornadas.Cells["A" + contador.ToString()].Value = jornada.AreaNegocio;
                                worksheetJornadas.Cells["B" + contador.ToString()].Value = jornada.CodigoJornada;
                                worksheetJornadas.Cells["C" + contador.ToString()].Value = jornada.NombreJornada;
                                worksheetJornadas.Cells["D" + contador.ToString()].Value = jornada.DescripcionJornada;
                                worksheetJornadas.Cells["E" + contador.ToString()].Value = jornada.HorasSemanal;
                                worksheetJornadas.Cells["F" + contador.ToString()].Value = jornada.PorcentajePago;

                                areaNegocio = jornada.AreaNegocio;
                            }
                            worksheetJornadas.Cells.AutoFitColumns();
                        }
                    }
                    #endregion

                    #region BONOS
                    if (bonos.Count > 0)
                    {
                        contador = 0;
                        hoja = "Bonos";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetBonos = excel.Workbook.Worksheets[hoja];

                        worksheetBonos.Cells["A1"].Value = "Area Negocio";
                        worksheetBonos.Cells["B1"].Value = "Bono";

                        foreach (Bonos bono in bonos)
                        {
                            if (bono.Code == "200")
                            {
                                contador = worksheetBonos.Dimension.End.Row + 1;

                                worksheetBonos.Cells["A" + contador.ToString()].Value = bono.AreaNegocio;
                                worksheetBonos.Cells["B" + contador.ToString()].Value = bono.Bono;
                            }
                            worksheetBonos.Cells.AutoFitColumns();
                        }
                    }
                    #endregion
                }

                excel.Workbook.Properties.Title = "Attempts";
                return excel.GetAsByteArray();
            }
        }

        public static byte[] DownloadCargaMasiva(List<ResponseCarga> download = null, List<Personal> personal = null, List<Leyenda> leyenda = null, List<JornadaLaboral> jornadaLaboral = null, List<Bonos> bonos = null,
            dynamic errores = null, string codigoTransaccion = null, string empresa = null, string fecha = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                int vuelta = 0;
                int vueltaError = 0;
                int index = 0;
                int indexError = 0;
                int count = 0;
                int countError = 0;
                int contador = 0;
                string cellIndex = "";
                string cellIndexError = "";
                string hoja = "";

                Color colFromHexBackColor = new Color();
                Color colFromHexFontColor = new Color();

                foreach (ResponseCarga item in download)
                {
                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == item.NombreHojaCargaMasiva))
                    {
                        excel.Workbook.Worksheets.Add(item.NombreHojaCargaMasiva);
                    }

                    ExcelWorksheet worksheet = excel.Workbook.Worksheets[item.NombreHojaCargaMasiva];

                    colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");

                    foreach (string columna in item.Columnas.Split(':'))
                    {
                        if (index > 25)
                        {
                            cellIndex = alfabeto[vuelta].ToString() + alfabeto[Convert.ToInt32(count)].ToString();
                            count++;

                            if (count > 25)
                                vuelta++;
                        }
                        else
                        {
                            cellIndex = alfabeto[Convert.ToInt32(index)].ToString();
                        }

                        index++;
                        worksheet.Cells[cellIndex + "1"].Value = columna.Split('@')[0];

                        if (codigoTransaccion != null)
                        {
                            for (dynamic i = 1; i <= errores.Count; i++)
                            {
                                for (dynamic j = 0; j < item.Columnas.Split(':').Length; j++)
                                {
                                    if (int.TryParse(item.Columnas.Split(':')[j].Split('@')[1], out int num))
                                    {
                                        indexError = Convert.ToInt32(item.Columnas.Split(':')[j].Split('@')[1]);
                                        if (indexError > 25)
                                        {
                                            cellIndexError = alfabeto[vueltaError].ToString() + alfabeto[Convert.ToInt32(countError)].ToString();
                                            countError++;
                                            if (count > 25)
                                                vueltaError++;
                                        }
                                        else
                                        {
                                            cellIndexError = alfabeto[Convert.ToInt32(indexError - 1)].ToString();
                                        }
                                    }
                                    else
                                    {
                                        cellIndexError = item.Columnas.Split(':')[j].Split('@')[1];
                                    }

                                    worksheet.Cells[cellIndexError + "" + (i + 1)].Value = (errores[(i - 1)][item.GetValueError.Split('@')[j].Replace(@"""", "")]).ToString();
                                }
                            }

                            worksheet.Cells[1, item.Columnas.Split(':').Length + 1].Value = "Observaciones a Corregir";

                            for (dynamic h = 1; h <= errores.Count; h++)
                            {
                                worksheet.Cells[(h + 1), item.Columnas.Split(':').Length + 1].Value = (errores[(h - 1)]["ObsProcesamiento"]).ToString();
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(codigoTransaccion))
                {
                    #region PERSONAL
                    if (personal.Count > 0)
                    {
                        contador = 0;
                        hoja = "Trabajadores";
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetPersonal = excel.Workbook.Worksheets[hoja];

                        worksheetPersonal.Cells["A1"].Value = "Empresa";
                        worksheetPersonal.Cells["B1"].Value = "Area Negocio";
                        worksheetPersonal.Cells["C1"].Value = "Ficha";
                        worksheetPersonal.Cells["D1"].Value = "Rut";
                        worksheetPersonal.Cells["E1"].Value = "Nombres";
                        worksheetPersonal.Cells["F1"].Value = "Fecha Inicio Contrato";
                        worksheetPersonal.Cells["G1"].Value = "Fecha Termino Contrato";

                        foreach (Personal data in personal)
                        {
                            contador = worksheetPersonal.Dimension.End.Row + 1;

                            worksheetPersonal.Cells["A" + contador.ToString()].Value = empresa;
                            worksheetPersonal.Cells["B" + contador.ToString()].Value = data.AreaNegocio;
                            worksheetPersonal.Cells["C" + contador.ToString()].Value = data.Ficha;
                            worksheetPersonal.Cells["D" + contador.ToString()].Value = data.Rut;
                            worksheetPersonal.Cells["E" + contador.ToString()].Value = data.Nombres;
                            worksheetPersonal.Cells["F" + contador.ToString()].Value = data.FechaInicio;
                            worksheetPersonal.Cells["G" + contador.ToString()].Value = data.FechaTermino;

                            if (data.Plataforma == "Teamwork")
                            {
                                colFromHexBackColor = ColorTranslator.FromHtml("#ffe1b4");

                                worksheetPersonal.Cells["A" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["B" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["C" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["D" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["E" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["F" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheetPersonal.Cells["G" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                                worksheetPersonal.Cells["A" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["B" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["C" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["D" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["E" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["F" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                                worksheetPersonal.Cells["G" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                            }
                            worksheetPersonal.Cells.AutoFitColumns();
                        }
                    }
                    #endregion

                    #region LEYENDA
                    if (leyenda.Count > 0)
                    {
                        contador = 0;
                        hoja = "Tipos Ausencias";
                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetLegend = excel.Workbook.Worksheets[hoja];

                        worksheetLegend.Cells["A1"].Value = "Tipo Inasistensia";
                        worksheetLegend.Cells["B1"].Value = "";

                        foreach (Leyenda legend in leyenda)
                        {
                            contador = worksheetLegend.Dimension.End.Row + 1;

                            colFromHexBackColor = ColorTranslator.FromHtml(legend.Style);
                            colFromHexFontColor = ColorTranslator.FromHtml(legend.Color);

                            worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.PatternType = ExcelFillStyle.Solid;

                            worksheetLegend.Cells["A" + contador.ToString()].Value = legend.Nombre;
                            worksheetLegend.Cells["B" + contador.ToString()].Value = legend.CodigoAsistencia;
                            worksheetLegend.Cells["B" + contador.ToString()].Style.Fill.BackgroundColor.SetColor(colFromHexBackColor);
                            worksheetLegend.Cells["B" + contador.ToString()].Style.Font.Color.SetColor(colFromHexFontColor);
                            worksheetLegend.Cells.AutoFitColumns();
                        }
                    }
                    #endregion

                    #region JORNADAS LABORALES
                    if (jornadaLaboral.Count > 0)
                    {
                        string areaNegocio = "";
                        contador = 0;
                        hoja = "Jornadas Laborales";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetJornadas = excel.Workbook.Worksheets[hoja];

                        worksheetJornadas.Cells["A1"].Value = "Area Negocio";
                        worksheetJornadas.Cells["B1"].Value = "Codigo";
                        worksheetJornadas.Cells["C1"].Value = "Jornada Laboral";
                        worksheetJornadas.Cells["D1"].Value = "Descripcion";
                        worksheetJornadas.Cells["E1"].Value = "Horas Semanales";
                        worksheetJornadas.Cells["F1"].Value = "Porcentaje Pago";

                        foreach (JornadaLaboral jornada in jornadaLaboral)
                        {
                            if (jornada.Code == "200")
                            {
                                contador = worksheetJornadas.Dimension.End.Row + 1;

                                worksheetJornadas.Cells["A" + contador.ToString()].Value = jornada.AreaNegocio;
                                worksheetJornadas.Cells["B" + contador.ToString()].Value = jornada.CodigoJornada;
                                worksheetJornadas.Cells["C" + contador.ToString()].Value = jornada.NombreJornada;
                                worksheetJornadas.Cells["D" + contador.ToString()].Value = jornada.DescripcionJornada;
                                worksheetJornadas.Cells["E" + contador.ToString()].Value = jornada.HorasSemanal;
                                worksheetJornadas.Cells["F" + contador.ToString()].Value = jornada.PorcentajePago;

                                areaNegocio = jornada.AreaNegocio;
                            }
                            worksheetJornadas.Cells.AutoFitColumns();
                        }
                    }
                    #endregion

                    #region BONOS
                    if (bonos.Count > 0)
                    {
                        contador = 0;
                        hoja = "Bonos";

                        if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == hoja))
                        {
                            excel.Workbook.Worksheets.Add(hoja);
                        }

                        ExcelWorksheet worksheetBonos = excel.Workbook.Worksheets[hoja];

                        worksheetBonos.Cells["A1"].Value = "Bono";

                        foreach (Bonos bono in bonos)
                        {
                            if (bono.Code == "200")
                            {
                                contador = worksheetBonos.Dimension.End.Row + 1;

                                worksheetBonos.Cells["A" + contador.ToString()].Value = bono.Bono;
                            }
                            worksheetBonos.Cells.AutoFitColumns();
                        }
                    }
                    #endregion
                }

                excel.Workbook.Properties.Title = "Attempts";
                return excel.GetAsByteArray();
            }
        }

        public static byte[] DownloadCargaMasivaRelojControl(List<ResponseCarga> download = null, dynamic errores = null, string codigoTransaccion = null, string plantilla = null)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            using (ExcelPackage excel = new ExcelPackage())
            {
                char[] alfabeto = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                int vuelta = 0;
                int vueltaError = 0;
                int index = 0;
                int indexError = 0;
                int count = 0;
                int countError = 0;
                string cellIndex = "";
                string cellIndexError = "";
                string headerIndex = "";
                string columnasAdicionales = "";

                switch (plantilla)
                {
                    case "NTI=":
                        headerIndex = "2";
                        columnasAdicionales = "Tipo:Entró:Tipo:Atraso:Justificado:Salió:Tipo:Adelanto:Justificado:HEA:HEC:HNT:HT:Cargo";
                        break;

                    case "NTQ=":
                        headerIndex = "6";
                        break;
                }

                Color colFromHexBackColor = new Color();
                Color colFromHexFontColor = new Color();

                foreach (ResponseCarga item in download)
                {
                    if (!excel.Workbook.Worksheets.Any(sheet => sheet.Name == item.NombreHojaCargaMasiva))
                    {
                        excel.Workbook.Worksheets.Add(item.NombreHojaCargaMasiva);
                    }

                    ExcelWorksheet worksheet = excel.Workbook.Worksheets[item.NombreHojaCargaMasiva];

                    colFromHexBackColor = ColorTranslator.FromHtml("#FFFFFF");

                    foreach (string columna in item.Columnas.Split(':'))
                    {
                        if (index > 25)
                        {
                            cellIndex = alfabeto[vuelta].ToString() + alfabeto[Convert.ToInt32(count)].ToString();
                            count++;

                            if (count > 25)
                                vuelta++;
                        }
                        else
                        {
                            cellIndex = alfabeto[Convert.ToInt32(index)].ToString();
                        }

                        index++;
                        worksheet.Cells[cellIndex + headerIndex].Value = columna.Split('@')[0];
                        worksheet.Cells[cellIndex + headerIndex].Style.Font.Bold = true;
                    }

                    foreach (string columna in columnasAdicionales.Split(':'))
                    {
                        if (index > 25)
                        {
                            cellIndex = alfabeto[vuelta].ToString() + alfabeto[Convert.ToInt32(count)].ToString();
                            count++;

                            if (count > 25)
                                vuelta++;
                        }
                        else
                        {
                            cellIndex = alfabeto[Convert.ToInt32(index)].ToString();
                        }

                        index++;
                        worksheet.Cells[cellIndex + headerIndex].Value = columna;
                        worksheet.Cells[cellIndex + headerIndex].Style.Font.Bold = true;
                    }


                    if (codigoTransaccion != null)
                    {
                        for (dynamic i = 1; i <= errores.Count; i++)
                        {
                            for (dynamic j = 0; j < item.Columnas.Split(':').Length; j++)
                            {
                                if (int.TryParse(item.Columnas.Split(':')[j].Split('@')[1], out int num))
                                {
                                    indexError = Convert.ToInt32(item.Columnas.Split(':')[j].Split('@')[1]);
                                    if (indexError > 25)
                                    {
                                        cellIndexError = alfabeto[vueltaError].ToString() + alfabeto[Convert.ToInt32(countError)].ToString();
                                        countError++;
                                        if (count > 25)
                                            vueltaError++;
                                    }
                                    else
                                    {
                                        cellIndexError = alfabeto[Convert.ToInt32(indexError - 1)].ToString();
                                    }
                                }
                                else
                                {
                                    cellIndexError = item.Columnas.Split(':')[j].Split('@')[1];
                                }

                                worksheet.Cells[cellIndexError + "" + (i + Convert.ToInt32(headerIndex))].Value = (errores[(i - 1)][item.GetValueError.Split('@')[j].Replace(@"""", "")]).ToString();
                            }
                        }

                        worksheet.Cells[Convert.ToInt32(headerIndex), item.Columnas.Split(':').Length + columnasAdicionales.Split(':').Length + 1].Value = "Observaciones a Corregir";
                        worksheet.Cells[Convert.ToInt32(headerIndex), item.Columnas.Split(':').Length + columnasAdicionales.Split(':').Length + 1].Style.Font.Bold = true;

                        for (dynamic h = 1; h <= errores.Count; h++)
                        {
                            worksheet.Cells[(h + Convert.ToInt32(headerIndex)), item.Columnas.Split(':').Length + columnasAdicionales.Split(':').Length + 1].Value = (errores[(h - 1)]["ObsProcesamiento"]).ToString();
                        }
                    }
                    worksheet.Cells.AutoFitColumns();
                }

                excel.Workbook.Properties.Title = "Attempts";
                return excel.GetAsByteArray();
            }
        }
        #endregion
    }
}
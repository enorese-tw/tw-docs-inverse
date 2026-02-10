using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class Utilidades
    {
        private static string strCaracteresValidos = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static string strClavesEjemplo = "mAran0iof01GadrAc1RqJera";
        private static string strSql;
        public static void LogError(string connectionString, string proceso, string mensaje)
        {
            if (mensaje.Length > 255)
                mensaje = mensaje.Substring(0, 255);
            strSql = string.Format("INSERT INTO ERROR (ERR_FECHA, ERR_PROCESO, ERR_MENSAJE) VALUES (GETDATE(), '{0}', '{1}')", proceso, mensaje);
            Interface.ExecuteQuery(connectionString, strSql);
        }
        //public static string ValidarClave(string connectionString, string USR_Password, string USR_Nombre, int USR_Codigo)
        //{
        //  string strValida = "";
        //  Usuario usuario = new Usuario();
        //  if (USR_Codigo == -1)
        //  {
        //    usuario.USR_Codigo = USR_Codigo;
        //    usuario.USR_Nombre = USR_Nombre;
        //  }
        //  else
        //  {
        //    usuario.USR_Codigo = USR_Codigo;
        //    usuario.Obtener(connectionString);
        //  }

        //  if (USR_Password.Length < 8)
        //  {
        //    strValida = "La clave debe tener un largo mínimo de 8 caracteres.\n";
        //  }

        //  if (USR_Password.Substring(0, 1).ToLower() != USR_Password.Substring(0, 1))
        //  {
        //    strValida += "La clave debe comenzar con letra mínuscula.\n";
        //  }

        //  int intMayusculas = 0;
        //  int intDigitos = 0;
        //  int intEsDigito;
        //  int intCaracteresEspeciales = 0;

        //  foreach (char chr in USR_Password.ToCharArray())
        //  {
        //    string strCaracter = chr.ToString();
        //    if (chr.ToString().ToUpper() == strCaracter)
        //    {
        //      intMayusculas++;
        //    }

        //    int.TryParse(strCaracter, out intEsDigito);
        //    if (intEsDigito != 0)
        //    {
        //      intDigitos++;
        //    }

        //    if (strCaracteresValidos.IndexOf(chr) == -1)
        //    {
        //      intCaracteresEspeciales++;
        //    }

        //  }

        //  if (intMayusculas == 0)
        //  {
        //    strValida += "La clave debe tener al menos una letra mayúscula.\n";
        //  }

        //  if (intDigitos == 0)
        //  {
        //    strValida += "La clave debe tener al menos un digito.\n";
        //  }


        //  string strPrimeraLetraRepetida = USR_Password.Substring(0, 1) + USR_Password.Substring(0, 1) + USR_Password.Substring(0, 1);
        //  string strPrimerasTresLetras = USR_Password.Substring(0, 3);
        //  if (strPrimeraLetraRepetida == strPrimerasTresLetras)
        //  {
        //    strValida += "La primera letra no se puede repetir 3 veces.\n";
        //  }

        //  string strPrimerasTresUsuario = usuario.USR_Nombre.Substring(0, 3);
        //  if (strPrimerasTresLetras == strPrimerasTresUsuario)
        //  {
        //    strValida += "Las primeras tres letras no pueden ser iguales a las del nombre de usuario.\n";
        //  }

        //  if (intCaracteresEspeciales > 0)
        //  {
        //    strValida += "La clave no puede contener caracteres especiales.\n";
        //  }

        //  if (strClavesEjemplo.IndexOf(USR_Password) > -1)
        //  {
        //    strValida += "La clave no puede ser igual a las de ejemplo.\n";
        //  }

        //  //List<Clave> claves = Claves.Listar(connectionString, idUsuario);
        //  //int intPasswordAnterior = 0;
        //  //foreach (Clave objClave in claves)
        //  //{
        //  //    if (Desencriptar(objClave.Password, "ecivreslio") == clave)
        //  //    {
        //  //        intPasswordAnterior++;
        //  //    }
        //  //}

        //  //if (intPasswordAnterior > 0)
        //  //{
        //  //    strValida += "La clave debe ser distinta de las 8 anteriores.\n";
        //  //}

        //  return strValida;
        //}
        public static float HexaToFloat(string strHexa)
        {
            string hexString = strHexa;
            uint num = uint.Parse(hexString, System.Globalization.NumberStyles.AllowHexSpecifier);

            byte[] floatVals = BitConverter.GetBytes(num);
            float f = BitConverter.ToSingle(floatVals, 0);

            return f;
        }

        public static string Encriptar(string strOriginal, string strLlave)
        {
            try
            {
                Rijndael objCripto = Rijndael.Create();
                byte[] btEncriptado, btOriginal;
                string strClaveEncriptacion = string.Format("Oil{0}Service", strLlave);
                if (strClaveEncriptacion.Length > 32)
                    strClaveEncriptacion.Substring(0, 32);
                else
                {
                    while (strClaveEncriptacion.Length < 32)
                    {
                        strClaveEncriptacion = "9" + strClaveEncriptacion;
                    }
                }
                objCripto.Key = Convert.FromBase64String(strClaveEncriptacion);
                objCripto.IV = new byte[objCripto.BlockSize / 8];

                //btOriginal = Encoding.Unicode.GetBytes(strOriginal);
                btOriginal = ASCIIEncoding.Unicode.GetBytes(strOriginal);
                ICryptoTransform objTransform = objCripto.CreateEncryptor();
                btEncriptado = objTransform.TransformFinalBlock(btOriginal, 0, btOriginal.Length);

                //objCripto.CreateEncryptor();
                return Convert.ToBase64String(btEncriptado);

            }
            catch (Exception ex)
            {
                return string.Format("Error al encriptar el texto: '{0}'", ex.Message);
            }
        }

        public static string Desencriptar(string strEncriptado, string strLlave)
        {
            try
            {
                byte[] btEncriptado, btOriginal;

                Rijndael objCripto = Rijndael.Create();

                string strClaveEncriptacion = string.Format("Oil{0}Service", strLlave);
                if (strClaveEncriptacion.Length > 32)
                    strClaveEncriptacion.Substring(0, 32);
                else
                {
                    while (strClaveEncriptacion.Length < 32)
                    {
                        strClaveEncriptacion = "9" + strClaveEncriptacion;
                    }
                }

                objCripto.Key = Convert.FromBase64String(strClaveEncriptacion);
                objCripto.IV = new byte[objCripto.BlockSize / 8];

                btEncriptado = Convert.FromBase64String(strEncriptado);

                ICryptoTransform objTransform = objCripto.CreateDecryptor();
                btOriginal = objTransform.TransformFinalBlock(btEncriptado, 0, btEncriptado.Length);
                //return Encoding.Unicode.GetString(btOriginal);
                return ASCIIEncoding.UTF32.GetString(btOriginal);
            }
            catch (Exception ex)
            {
                return string.Format("Error al desencriptar el texto: '{0}'", ex.Message);
            }
        }
        public static bool ValidarRut(string rut, string connectionString)
        {
            try
            {
                int operando = 2;
                int suma = 0;
                int digito;
                string digitoOriginal = rut.Substring(rut.Length - 1, 1);
                string digitoCalculado = "";
                for (int i = rut.Length - 2; i > -1; i--)
                {
                    suma = suma + (int.Parse(rut.Substring(i, 1)) * operando);
                    operando++;
                    if (operando == 8)
                        operando = 2;
                }
                digito = 11 - (suma % 11);
                if (digito > 9)
                {
                    if (digito == 10)
                        digitoCalculado = "0";
                    if (digito == 11)
                        digitoCalculado = "K";
                }
                else
                    digitoCalculado = digito.ToString();
                if (digitoCalculado == digitoOriginal)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "TelemedicionLib.Utiles.ValidarRut", ex.Message);
                return false;
            }
        }
        public static void creaArchivo(string datos)
        {
            using (StreamWriter w = File.AppendText("c:/david/log.txt"))
            {
                Log(datos, w);
                // Log("datos", w);
            }

            using (StreamReader r = File.OpenText("c:/david/log.txt"))
            {
                DumpLog(r);
            }
        }
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
        public static int diashabiles(DateTime inicio, double final)
        {
            int TotalDias = 0;
            int feriados = 0;

            DateTime Inicio1 = inicio.AddDays(1);
            strSql = string.Format("Select Fecha from TW_FestivosCH Where Fecha <= '{0}' and Fecha >= '{1}'", Inicio1.AddDays(Math.Truncate(final)), Inicio1);
            //string mensaje = strSql.Substring(0, 255);
            creaArchivo(strSql);
            string strIns = string.Format("INSERT INTO ERROR_FNQ (ID,Error) VALUES ({0},'{1}')", TotalDias, inicio);
            Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strIns);
            DataSet ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);

            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int cuentadias = 0;
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            DateTime FEC = DateTime.Parse(dr["Fecha"].ToString());
                            switch (FEC.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    feriados++;
                                    break;
                                case DayOfWeek.Tuesday:
                                    feriados++;
                                    break;
                                case DayOfWeek.Wednesday:
                                    feriados++;
                                    break;
                                case DayOfWeek.Thursday:
                                    feriados++;
                                    break;
                                case DayOfWeek.Friday:
                                    feriados++;
                                    break;
                            }

                        }
                        cuentadias = feriados;
                    }
                }
            }
            TotalDias += feriados;
            int finesdesemana = 0;
            //TextWriter jjjjjj = "";
            final += feriados;
            DateTime i = Inicio1;
            DateTime k = Inicio1.AddDays(Math.Truncate(final));
            int J = 1;
            //Log(i.ToString("ddd"),File.CreateText("newfile.txt"));
            creaArchivo(i.ToString("ddd"));

            while (J <= int.Parse(Math.Truncate(final).ToString()))
            {
                if ((i.DayOfWeek == DayOfWeek.Saturday) || (i.DayOfWeek == DayOfWeek.Sunday))
                {
                    finesdesemana++;
                    i = i.AddDays(1);
                }
                else { i = i.AddDays(1); J++; }
            }
            TotalDias += finesdesemana;

            return TotalDias;
        }

        public static int TotaldiasVacaciones(DateTime inicio, double final, string empresa)
        {
            int J = 0;
            DateTime fecha = inicio;
            int diasVaca = 0;
            while (J < Convert.ToInt32(Math.Truncate(final)))
            {

                if (DiaFeriado(fecha, empresa) == 0)
                {
                    switch (fecha.DayOfWeek)
                    {
                        case DayOfWeek.Monday:
                            J++;
                            break;
                        case DayOfWeek.Tuesday:
                            J++;
                            break;
                        case DayOfWeek.Wednesday:
                            J++;
                            break;
                        case DayOfWeek.Thursday:
                            J++;
                            break;
                        case DayOfWeek.Friday:
                            J++;
                            break;
                        case DayOfWeek.Saturday:
                            diasVaca++;
                            break;
                        case DayOfWeek.Sunday:
                            diasVaca++;
                            break;
                    }
                    fecha = fecha.AddDays(1);
                }
                else
                {
                    diasVaca++;
                    fecha = fecha.AddDays(1);
                }

            }
            return diasVaca;

        }
        private static int DiaFeriado(DateTime fecha, string empresa)
        {
            string day = fecha.Day.ToString();
            string month = fecha.Month.ToString();
            string year = fecha.Year.ToString();

            strSql = string.Format("Select Festivo from [dbo].[View_Festivos] Where Festivo = CAST('{0}' AS DATE) AND Empresa = '{1}'", year + "-" + month + "-" + day, empresa);
            DataSet ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int cuentadias = 0;
                        DataRow dr = ds.Tables[0].Rows[0];
                        DateTime FEC = DateTime.Parse(dr["Festivo"].ToString());
                        switch (FEC.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                return 1;
                                break;
                            case DayOfWeek.Tuesday:
                                return 1;
                                break;
                            case DayOfWeek.Wednesday:
                                return 1;
                                break;
                            case DayOfWeek.Thursday:
                                return 1;
                                break;
                            case DayOfWeek.Friday:
                                return 1;
                                break;
                            default:
                                return 0;

                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
    }
}

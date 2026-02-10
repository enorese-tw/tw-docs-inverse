using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace FiniquitosV2.Clases
{
    public class Remuneracion//:VariableRemuneracion
    {

        /* public string ficha { get; set; }
         public int mes { get; set; }*/

        private static string strSql;
        private static DataSet ds;

        public List<VariableRemuneracion> Listar(string connectionStringFN, string ficha, int mes)
        {
            try
            {
                Remuneracion remu = new Remuneracion();
                strSql = string.Format("select codVariable from FNVARIABLEREMUNERACION");
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
                List<VariableRemuneracion> result = new List<VariableRemuneracion>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                VariableRemuneracion variableremuneracion = new VariableRemuneracion();
                                variableremuneracion.ficha = ficha;
                                variableremuneracion.mes = mes;
                                variableremuneracion.codVariable = dr["codVariable"].ToString();
                                variableremuneracion.Obtener(connectionStringFN);
                                //finiquito.Obtener(connectionString);
                                result.Add(variableremuneracion);
                                i++;
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError("Hola", "Remuneraciones.Listar", ex.Message);
                return null;
            }
        }
        public List<VariableRemuneracion> ListarOUT(string connectionStringFN, string ficha, int mes, string areaNegocio)
        {
            try
            {
                Remuneracion remu = new Remuneracion();

                if (areaNegocio != "MOV")
                {
                    strSql = string.Format("select codVariable from FNVARIABLEREMUNERACIONOUT WHERE tipo ='v' AND CAST(descripcion AS VARCHAR(MAX)) <> 'BONO EXTRAORDINARIO' ");
                }
                else
                {
                    strSql = string.Format("select codVariable from FNVARIABLEREMUNERACIONOUT  WHERE tipo = 'v' ");
                }
                
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
                List<VariableRemuneracion> result = new List<VariableRemuneracion>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                VariableRemuneracion variableremuneracion = new VariableRemuneracion();
                                variableremuneracion.ficha = ficha;
                                variableremuneracion.mes = mes;
                                variableremuneracion.codVariable = dr["codVariable"].ToString();
                                variableremuneracion.fechames = variableremuneracion.FechaRemuneracion(Properties.Settings.Default.connectionStringTEAMRRHH, ficha, mes);
                                variableremuneracion.Obtener(Properties.Settings.Default.connectionStringTEAMRRHH);
                                //finiquito.Obtener(connectionString);
                                result.Add(variableremuneracion);
                                i++;
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError("Hola", "Remuneraciones.Listar", ex.Message);
                return null;
            }
        }
        public List<VariableRemuneracion> ListarOUTFijo(string connectionStringFN, string ficha, int mes)
        {
            try
            {
                Remuneracion remu = new Remuneracion();
                strSql = string.Format("select codVariable from FNVARIABLEREMUNERACIONOUT where tipo = 'F'");
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
                List<VariableRemuneracion> result = new List<VariableRemuneracion>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                VariableRemuneracion variableremuneracion = new VariableRemuneracion();
                                variableremuneracion.ficha = ficha;
                                variableremuneracion.mes = mes;
                                variableremuneracion.codVariable = dr["codVariable"].ToString();
                                variableremuneracion.Obtener(Properties.Settings.Default.connectionStringTEAMRRHH);
                                //finiquito.Obtener(connectionString);
                                result.Add(variableremuneracion);
                                i++;
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError("Hola", "Remuneraciones.Listar", ex.Message);
                return null;
            }
        }




        public List<VariableRemuneracion> ListarCONSULTORA(string connectionStringFN, string ficha, int mes)
        {
            try
            {
                Remuneracion remu = new Remuneracion();
                strSql = string.Format("select codVariable from FNVARIABLEREMUNERACIONCONSULTORA where tipo = 'V'");
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
                List<VariableRemuneracion> result = new List<VariableRemuneracion>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                VariableRemuneracion variableremuneracion = new VariableRemuneracion();
                                variableremuneracion.ficha = ficha;
                                variableremuneracion.mes = mes;
                                variableremuneracion.codVariable = dr["codVariable"].ToString();
                                variableremuneracion.fechames = variableremuneracion.FechaRemuneracion(Properties.Settings.Default.connectionStringTEAMWORK, ficha, mes);
                                variableremuneracion.Obtener(Properties.Settings.Default.connectionStringTEAMWORK);
                                //finiquito.Obtener(connectionString);
                                result.Add(variableremuneracion);
                                i++;
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError("Hola", "Remuneraciones.Listar", ex.Message);
                return null;
            }
        }
        public List<VariableRemuneracion> ListaCONSULTORAFijo(string connectionStringFN, string ficha, int mes)
        {
            try
            {
                Remuneracion remu = new Remuneracion();
                strSql = string.Format("select codVariable from FNVARIABLEREMUNERACIONCONSULTORA where tipo = 'F'");
                ds = Interface.ExecuteDataSet(Properties.Settings.Default.connectionString, strSql);
                List<VariableRemuneracion> result = new List<VariableRemuneracion>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            int i = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                VariableRemuneracion variableremuneracion = new VariableRemuneracion();
                                variableremuneracion.ficha = ficha;
                                variableremuneracion.mes = mes;
                                variableremuneracion.codVariable = dr["codVariable"].ToString();
                                variableremuneracion.Obtener(Properties.Settings.Default.connectionStringTEAMWORK);
                                //finiquito.Obtener(connectionString);
                                result.Add(variableremuneracion);
                                i++;
                            }
                            return result;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Utilidades.LogError("Hola", "Remuneraciones.Listar", ex.Message);
                return null;
            }
        }

        public static DataTable ConvertListToDataTable(List<Remuneracion[]> list)
        {
            // New table.
            DataTable table = new DataTable();

            // Get max columns.
            int columns = 0;
            foreach (var array in list)
            {
                if (array.Length > columns)
                {
                    columns = array.Length;
                }
            }

            // Add columns.
            for (int i = 0; i < columns; i++)
            {
                table.Columns.Add();
            }

            // Add rows.
            foreach (var array in list)
            {
                table.Rows.Add(array);
            }

            return table;
        }
        //public bool mesquebrado(string connectionString, string ficha, string mes)
        //{
        //    try
        //    {
        //        strSql = string.Format();
        //        ds = Interface.ExecuteDataSet(connectionString, strSql);
        //        if (ds != null)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    DataRow dr = ds.Tables[0].Rows[0];
        //                    valorvariable = dr["valor"].ToString();

        //                }
        //                else
        //                {
        //                    variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
        //                }
        //            }
        //            else
        //            {
        //                variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
        //            }
        //        }
        //        else
        //        {
        //            variable = string.Format("No se encontró el Cargo Mod con Id {0}", variable);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilidades.LogError(connectionString, "TW_Gratificacion.Obtener", ex.Message);
        //        variable = string.Format("Error: {0}", ex.Message);
        //    }
        //}

    }
}
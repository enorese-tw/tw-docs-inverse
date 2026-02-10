using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class RecepcionDocumentos
    {
        public int ID { get; set; }
        public string RUTTRABAJADOR { get; set; }
        public int IDEMPRESA { get; set; }
        public string NOMBRETRABAJADOR { get; set; }
        public string NEGOCIO { get; set; }
        public string ESTADO { get; set; }
        public string CAUSAL { get; set; }
        public string DESDE { get; set; }
        public string LEGALIZADA { get; set; }
        public string OBSERVACION { get; set; }
        public string REGISTRADA { get; set; }
        public string VISTONOTIFICACION { get; set; }
        public string KAM { get; set; }
        public string FECHAHORARECEPCION { get; set; }
        public int MES { get; set; }
        public int YEAR { get; set; }
        public int CONTADOR { get; set; }
        public int MAXIMO { get; set; }
        private static string strSql;
        private string strSqlDelete;
        private static DataSet ds;

        public void GrabarRenuncia(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand comando = new SqlCommand();
            try
            {
                strSql = string.Format("INSERT INTO FN_DOCUMENTOSRECIBIDOS  VALUES (" + ID + ",'" + RUTTRABAJADOR + "'," + IDEMPRESA + ",'" + NOMBRETRABAJADOR + "','" + NEGOCIO + "','" + ESTADO + "','" + CAUSAL + "', CAST('" + DESDE + "' AS DATETIME),'" + LEGALIZADA + "','" + OBSERVACION + "','" + REGISTRADA + "','" + VISTONOTIFICACION + "','" + KAM + "', CAST('" + FECHAHORARECEPCION + "' AS DATETIME));");
                Interface.ExecuteQuery(Properties.Settings.Default.connectionString, strSql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Usuario> buscarKAM(string connectionString, string EMPRESA)
        {
            try
            {
                strSql = string.Format("SELECT Correo FROM [dbo].[View_KamCliente] WHERE NombreEmpresa = '" + EMPRESA + "';");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<Usuario> result = new List<Usuario>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Usuario u = new Usuario();
                                u.correo = dr["Correo"].ToString();
                                result.Add(u);
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
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }

        public int cantidadregistrosmes(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT Count(ID) as conteo FROM FN_DOCUMENTOSRECIBIDOS where  month(FECHAHORARECEPCION) like " + MES + " and year(FECHAHORARECEPCION) like " + YEAR + ";");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr["conteo"] != System.DBNull.Value)
                            {
                                CONTADOR = int.Parse(dr["conteo"].ToString());
                                return CONTADOR;
                            }
                            else
                            { return 0; }
                        }
                    }

                }
                return 0;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
                return -1;
            }
        }
        public int cantidadnoregistrado(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT Count(ID) as conteo FROM FN_DOCUMENTOSRECIBIDOS where REGISTRADA LIKE 'no'and KAM like '"+KAM+"';");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr["conteo"] != System.DBNull.Value)
                            {
                                CONTADOR = int.Parse(dr["conteo"].ToString());
                                return CONTADOR;
                            }
                            else
                            { return 0; }
                        }
                    }

                }
                return 0;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
                return -1;
            }
        }

        public int maxrenunciames(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT MAX(ID) as MAXIMO FROM FN_DOCUMENTOSRECIBIDOS where  month(FECHAHORARECEPCION) like " + MES + " and year(FECHAHORARECEPCION) like " + YEAR + ";");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[0].Rows[0];
                            if (dr["MAXIMO"] != System.DBNull.Value)
                            {
                                MAXIMO = int.Parse(dr["MAXIMO"].ToString());
                                return MAXIMO;
                            }
                            else
                            { return 0; }
                        }
                    }

                }
                return 0;
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Trabajador.ContratoActivo", ex.Message);
                //descripcion = string.Format("Error: {0}", ex.Message);
                return -1;
            }
        }

        public static List<Renuncia> recibidoxkam(string connectionString, string KAM)
        {
            try
            {
                strSql = string.Format("SELECT D.ID AS ID, D.RUTTRABAJADOR AS RUTTRABAJADOR, D.NOMBRETRABAJADOR AS NOMBRETRABAJADOR,E.NOMBRE AS EMPRESA,D.NEGOCIO AS NEGOCIO,D.DESDE AS DESDE,D.FECHAHORARECEPCION AS FECHARECEPCION,D.LEGALIZADA AS LEGALIZADA,D.REGISTRADA AS REGISTRADA FROM FN_DOCUMENTOSRECIBIDOS D INNER JOIN FN_EMPRESAS E ON E.ID LIKE D.IDEMPRESA where D.KAM like '"+KAM+"' order by D.ID DESC;");
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                List<Renuncia> result = new List<Renuncia>();
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                Renuncia d = new Renuncia();
                                d.ID = int.Parse(dr["ID"].ToString());
                                d.NOMBRETRABAJADOR = dr["NOMBRETRABAJADOR"].ToString();
                                d.EMPRESA = dr["EMPRESA"].ToString();
                                d.RUTTRABAJADOR = dr["RUTTRABAJADOR"].ToString();
                                d.NEGOCIO = dr["NEGOCIO"].ToString();
                                d.DESDE = dr["DESDE"].ToString();
                                d.LEGALIZADA = dr["LEGALIZADA"].ToString();
                                d.REGISTRADA = dr["REGISTRADA"].ToString();
                                result.Add(d);
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
                Utilidades.LogError(connectionString, "TelemedicionLib.Usuarios.Listar", ex.Message);
                return null;
            }
        }
    }
}
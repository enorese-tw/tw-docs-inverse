using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FiniquitosV2.Clases
{
    public class FN_ENCDESVINCULACION
    {
        public int ID {get; set;}
        public string FECHAHORASOLICITUD {get; set;}
        public string FECHAAVISODESVINCULACION { get; set; }
        public string USUARIOSOLICITUD {get; set;}
        public string RUTTRABAJADOR {get; set;}
        public string FICHATRABAJADOR {get; set;}
        public string NOMBRECOMPLETOTRABAJADOR {get; set;}
        public int IDESTADO {get; set;}
        public string FECHAESTADO {get; set;}
        public string IDCAUSAL {get; set;}
        public int IDEMPRESA {get; set;}
        public string OBSERVACIONES {get; set;}
        public string ESTADOTEXTO { get; set; }
        public string EMPRESATEXTO { get; set; }
        public string ESTADOACTUAL { get; set; }
        public int IDRENUNCIA {get; set;}
        public string Estado { get; set; }
        private static string strSql;
        public void Grabar(string connectionString)
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "SP_INSERTAR_MODIFICAR";
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add(new SqlParameter("@FECHAHORASOLICITUD", FECHAHORASOLICITUD));
                comando.Parameters.Add(new SqlParameter("@FECHAAVISODESVINCULACION", FECHAAVISODESVINCULACION));
                comando.Parameters.Add(new SqlParameter("@USUARIOSOLICITUD", USUARIOSOLICITUD));
                comando.Parameters.Add(new SqlParameter("@RUTTRABAJADOR", RUTTRABAJADOR));
                comando.Parameters.Add(new SqlParameter("@FICHATRABAJADOR", FICHATRABAJADOR));
                comando.Parameters.Add(new SqlParameter("@NOMBRECOMPLETOTRABAJADOR", NOMBRECOMPLETOTRABAJADOR));
                comando.Parameters.Add(new SqlParameter("@ESTADOSOLICITUD", IDESTADO));
                comando.Parameters.Add(new SqlParameter("@FECHAESTADO", FECHAESTADO));
                comando.Parameters.Add(new SqlParameter("@IDCAUSAL", IDCAUSAL));
                comando.Parameters.Add(new SqlParameter("@IDEMPRESA", IDEMPRESA));
                comando.Parameters.Add(new SqlParameter("@OBSERVACIONES", OBSERVACIONES));
                comando.Parameters.Add(new SqlParameter("@IDRENUNCIA", IDRENUNCIA));
                comando.Connection = connection;
                connection.Open();
                comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DataSet ds;
        public string CadenaConexion()
        {
            return "Data Source=SDTW;Initial Catalog=Finiquitos2018;User ID=ADMINTW;Password=T34m-Work";
        }


        public DataTable buscarkam(string correo)
        {
            try
            {
                SqlConnection con = new SqlConnection(CadenaConexion());
                con.Open();
                string query = "SELECT D.ID AS ID, D.FECHAHORASOLICITUD AS FECHAHORASOLICITUD, D.USUARIOSOLICITUD AS USUARIOSOLICITUD, D.RUTTRABAJADOR AS RUTTRABAJADOR,D.FICHATRABAJADOR AS FICHATRABAJADOR,D.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABAJADOR, ES.DESCRIPCION AS ESTADOTEXTO, D.IDCAUSAL AS IDCAUSAL,EM.NOMBRE AS EMPRESATEXTO FROM FN_ENCDESVINCULACION D INNER JOIN FN_EMPRESAS EM ON EM.ID LIKE D.IDEMPRESA INNER JOIN FN_ESTADOS ES ON ES.ID LIKE D.ESTADOSOLICITUD where d.usuariosolicitud like '"+correo+"'  order by D.ID DESC ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable buscarxrut(string rut)
        {
            try
            {
                SqlConnection con = new SqlConnection(CadenaConexion());
                con.Open();
                string query = "SELECT D.ID AS ID, D.FECHAHORASOLICITUD AS FECHAHORASOLICITUD, D.USUARIOSOLICITUD AS USUARIOSOLICITUD, D.RUTTRABAJADOR AS RUTTRABAJADOR,D.FICHATRABAJADOR AS FICHATRABAJADOR,D.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABAJADOR, ES.DESCRIPCION AS ESTADOTEXTO, D.IDCAUSAL AS IDCAUSAL,EM.NOMBRE AS EMPRESATEXTO FROM FN_ENCDESVINCULACION D INNER JOIN FN_EMPRESAS EM ON EM.ID LIKE D.IDEMPRESA INNER JOIN FN_ESTADOS ES ON ES.ID LIKE D.ESTADOSOLICITUD where d.RUTTRABAJADOR like '" + rut + "'  order by D.ID DESC ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable buscarxfecha(int mes, int año)
        {

            try
            {
                SqlConnection con = new SqlConnection(CadenaConexion());
                con.Open();
                string query = "SELECT D.ID AS ID, D.FECHAHORASOLICITUD AS FECHAHORASOLICITUD, D.USUARIOSOLICITUD AS USUARIOSOLICITUD, D.RUTTRABAJADOR AS RUTTRABAJADOR,D.FICHATRABAJADOR AS FICHATRABAJADOR,D.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABAJADOR, ES.DESCRIPCION AS ESTADOTEXTO, D.IDCAUSAL AS IDCAUSAL,EM.NOMBRE AS EMPRESATEXTO FROM FN_ENCDESVINCULACION D INNER JOIN FN_EMPRESAS EM ON EM.ID LIKE D.IDEMPRESA INNER JOIN FN_ESTADOS ES ON ES.ID LIKE D.ESTADOSOLICITUD where month(D.FECHAHORASOLICITUD) LIKE " + mes + " and YEAR(D.FECHAHORASOLICITUD) like " + año + "  order by ID desc; ";
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.Fill(dt);
                con.Close();
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ObtenerENCFiniquito(string connectionString)
        {
            try
            {
                strSql = string.Format("SELECT F.ID AS ID, F.FECHAHORASOLICITUD AS FECHAHORASOLICITUD,F.FECHAAVISODESVINCULACION AS FECHAAVISODESVINCULACION, F.USUARIOSOLICITUD AS USUARIOSOLICITUD, F.RUTTRABAJADOR AS RUTTRABAJADOR, F.FICHATRABAJADOR AS FICHATRABAJADOR, F.NOMBRECOMPLETOTRABAJADOR AS NOMBRECOMPLETOTRABJADOR, E.DESCRIPCION AS ESTADOSOLICITUD, F.FECHAESTADO AS FECHAESTADO, F.IDCAUSAL  AS IDCAUSAL, M.NOMBRE AS IDEMPRESA  FROM FN_ENCDESVINCULACION F INNER JOIN FN_ESTADOS E ON E.ID LIKE F.ESTADOSOLICITUD INNER JOIN FN_EMPRESAS M ON M.ID LIKE F.IDEMPRESA WHERE FICHATRABAJADOR Like '{0}'", FICHATRABAJADOR);
                ds = Interface.ExecuteDataSet(connectionString, strSql);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            DataRow dr = ds.Tables[0].Rows[0];
                            ID = int.Parse(dr["ID"].ToString());
                            FECHAHORASOLICITUD = dr["FECHAHORASOLICITUD"].ToString();
                            FECHAAVISODESVINCULACION = dr["FECHAAVISODESVINCULACION"].ToString();
                            USUARIOSOLICITUD = dr["USUARIOSOLICITUD"].ToString();
                            RUTTRABAJADOR = dr["RUTTRABAJADOR"].ToString();
                            FICHATRABAJADOR = dr["FICHATRABAJADOR"].ToString();
                            NOMBRECOMPLETOTRABAJADOR = dr["NOMBRECOMPLETOTRABJADOR"].ToString();
                            ESTADOTEXTO = dr["ESTADOSOLICITUD"].ToString();
                            FECHAESTADO =dr["FECHAESTADO"].ToString();
                            IDCAUSAL = dr["IDCAUSAL"].ToString();
                            EMPRESATEXTO = dr["IDEMPRESA"].ToString();
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    { return 0; }

                }
                else { return 0; }
            }
            catch (Exception ex)
            {
                Utilidades.LogError(connectionString, "Cliente.Obtener", ex.Message);
                //TW_RazonSocialCliente = string.Format("Error: {0}", ex.Message);
                return -1;
            }
        }

        public void ObtenerIdDesvinculacion(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FN_ENCDESVINCULACION");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr["ID"] != System.DBNull.Value)
                        {
                            ID = int.Parse(dr["ID"].ToString()) + 1;
                        }
                        else { ID = 1; }

                    }
                }
            }
        }

        public void ObtenerIdmaximo(string connectionString)
        {
            string strSql = string.Format("Select MAX(ID) as ID From FN_ENCDESVINCULACION");
            ds = Clases.Interface.ExecuteDataSet(connectionString, strSql);
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        if (dr["ID"] != System.DBNull.Value)
                        {
                            ID = int.Parse(dr["ID"].ToString());
                        }
                        else { ID = 0; }

                    }
                }
            }
        }
    }
}
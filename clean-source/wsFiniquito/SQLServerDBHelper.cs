using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class SQLServerDBHelper
    {
        string _conexion;
        string _listener;
        string _basededatos;
        string _usuario;
        string _clave;

        /**
         *  PARAMETRO: CLIENT
         *             CASE TW_SA: CUANDO SEA CONEXION A BASE DE DATOS INTERNA DE TW SERVIDOR SATW.
         *             CASE SOFTLAND_EST: CUANDO SEA CONEXION A BASE DE DATOS SOFTLAND PARA EST.
         *             CASE SOFTLAND_OUT: CUANDO SEA CONEXION A BASE DE DATOS SOFTLAND PARA OUT.
         *             CASE SOFTLAND_CONSULTORA: CUANDO SEA CONEXION A BASE DE DATOS SOFTLAND PARA CONSULTORA.
         */
        public SQLServerDBHelper(string CLIENT) {
            DatosXML BasedeDatos = new DatosXML();
            switch (CLIENT)
            {
                case "TW_SA":
                    _conexion = string.Format("Server={0};Database={1};User Id={2};Password={3};Connection Timeout=0", BasedeDatos.ServidorBaseDeDatosSA, BasedeDatos.BaseDeDatos, BasedeDatos.UsuarioBD, BasedeDatos.ClaveBDSA);
                    break;
                case "SOFTLAND_EST":
                    _conexion = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", BasedeDatos.SoftlandServidorDB, BasedeDatos.SoftlandBaseDeDatosEST, BasedeDatos.SoftlandUsuarioDB, BasedeDatos.SoftlandClaveDB);
                    break;
                case "SOFTLAND_OUT":
                    _conexion = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", BasedeDatos.SoftlandServidorDB, BasedeDatos.SoftlandBaseDeDatosOUT, BasedeDatos.SoftlandUsuarioDB, BasedeDatos.SoftlandClaveDB);
                    break;
                case "SOFTLAND_CONSULTORA":
                    _conexion = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", BasedeDatos.SoftlandServidorDB, BasedeDatos.SoftlandBaseDeDatosCONSULTORA, BasedeDatos.SoftlandUsuarioDB, BasedeDatos.SoftlandClaveDB);
                    break;
            }
        }

        public DataTable ExecuteStoreProcedure(string StoreProcedure, List<Parametro> Parameters = null, string TableName = "resultado") 
        {
            SqlConnection con;
            DataTable dt;

            try
            {
                using(con = new SqlConnection(_conexion))
                {
                    SqlDataAdapter da = new SqlDataAdapter(StoreProcedure, con);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if(Parameters != null)
                    {
                        foreach(Parametro x in Parameters)
                        {
                            da.SelectCommand.Parameters.AddWithValue(x.ParameterName, x.ParameterValue);
                        }
                    }

                    dt = new DataTable(TableName);
                    da.Fill(dt);
                }
            }
            finally {
                //con.Close();
            }   

            return dt;
        }

        public DataTable ExecuteQuery(string query, string[] parameters = null, string TableName = "resultado")
        {
            SqlConnection con;
            DataTable dt;
            SqlCommand command;
            string formatString = string.Empty;

            try
            {
                using(con = new SqlConnection(_conexion))
                {
                    if (parameters != null)
                    {
                        formatString += string.Format(query, parameters);
                    }
                    else 
                    {
                        formatString = string.Format(query);
                    }

                    SqlDataAdapter da = new SqlDataAdapter(formatString, con);
                    da.SelectCommand.CommandType = CommandType.Text;
                    dt = new DataTable(TableName);
                    da.Fill(dt);
                }
            }
            finally
            {
                //con.Close();
            }

            return dt;
        }
    }

    public class Parametro
    {
        private string _ParameterName;
        private string _ParameterValue;


        public Parametro(string ParameterName, string ParameterValue)
        {
            _ParameterName = ParameterName;
            _ParameterValue = ParameterValue;
        }

        public string ParameterName
        {
            get { return _ParameterName; }
            set { _ParameterName = value; }
        }

        public string ParameterValue
        {
            get { return _ParameterValue; }
            set { _ParameterValue = value; }
        }

    }
}
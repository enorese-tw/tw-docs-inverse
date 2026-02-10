using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class CSolB4J
    {
        public DataTable SetCrearSolicitudB4J(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_SET_FN_CREAR_SOLB4J", parametros);

            return dt;
        }

        public DataTable GetPlantillasCorreo(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_GET_PLANTILLAS_CORREO", parametros);

            return dt;
        }

        public DataTable GetObtenerSolicitudesB4J(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_OBTENER_SOLB4J", parametros);

            return dt;
        }

        public DataTable SetAdministrarProcesosSolB4J(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_SET_ADMINISTRAR_PROCESO_SOLB4J", parametros);

            return dt;
        }

        public DataTable GetToken(List<Parametro> parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("TW_SA");
            DataTable dt = new DataTable();
            dt = helper.ExecuteStoreProcedure("SP_FN_GET_ENCRIPTAR_DATOS", parametros);

            return dt;
        }
    }
}
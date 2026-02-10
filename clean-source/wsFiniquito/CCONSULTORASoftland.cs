using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ServicioFiniquitos
{
    public class CCONSULTORASoftland
    {
        public DataTable GetRutTrabajadorSolicitudBajaCONSULTORA(string[] parametros)
        {
            SQLServerDBHelper helper = new SQLServerDBHelper("SOFTLAND_CONSULTORA");
            DataTable dt = new DataTable();
            string query = "SELECT DISTINCT p.rut, p.nombres as nombres FROM softland.sw_personal p inner join  softland.sw_estudiosup e on e.codEstudios like p.codEstudios WHERE p.rut like '{0}'";
            dt = helper.ExecuteQuery(query, parametros);

            return dt;
        }
    }
}
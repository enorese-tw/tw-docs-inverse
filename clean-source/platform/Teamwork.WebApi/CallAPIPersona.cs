using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIPersona
    {
        public static string __PersonaConsultarCliente(string filter, string datafilter, string token)
        {
            return HelperCallAPI.__CallAPIPersona(
                "ConsultarCliente",
                "{ Filter: '" + filter + "', DataFilter: '" + datafilter + "' }",
                token
            );
        }

        public static string __PersonaCrearCliente(string cliente, string token)
        {
            return HelperCallAPI.__CallAPIPersona(
                "CrearCliente",
                "{ Cliente: '" + cliente + "' }",
                token
            );
        }

        public static string __PersonaEliminarCliente(string cliente, string token)
        {
            return HelperCallAPI.__CallAPIPersona(
                "EliminarCliente",
                "{ Cliente: '" + cliente + "' }",
                token
            );
        }
    }
}
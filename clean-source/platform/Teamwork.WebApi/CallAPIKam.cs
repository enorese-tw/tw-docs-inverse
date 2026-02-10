using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIKam
    {
        public static string __ConsultaFichaPersonal(string ficha, string empresa, string rut, string filter, string token)
        {
            return HelperCallAPI.__CallAPIKamConsulta(
                "FichaPersonal",
                "{ Ficha: '" + ficha + "', Empresa: '" + empresa + "', Rut: '" + rut + "', Filter: '" + filter + "' }",
                token
            );
        }

        public static string __ConsultaContrato(string ficha, string empresa, string rut, string filter, string token)
        {
            return HelperCallAPI.__CallAPIKamConsulta(
                "Contrato",
                "{ Ficha: '" + ficha + "', Empresa: '" + empresa + "', Rut: '" + rut + "', Filter: '" + filter + "'  }",
                token
            );
        }

        public static string __ConsultaRenovacion(string ficha, string empresa, string token)
        {
            return HelperCallAPI.__CallAPIKamConsulta(
                "Renovacion",
                "{ Ficha: '" + ficha + "', Empresa: '" + empresa + "' }",
                token
            );
        }

        public static string __ConsultaBaja(string ficha, string empresa, string token)
        {
            return HelperCallAPI.__CallAPIKamConsulta(
                "Baja",
                "{ Ficha: '" + ficha + "', Empresa: '" + empresa + "' }",
                token
            );
        }
    }
}
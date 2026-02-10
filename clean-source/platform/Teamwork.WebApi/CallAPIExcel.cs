using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIExcel
    {
        public static string __ReporteCargoMod(string excel, string data, string token)
        {
            return HelperCallAPI.__CallAPIExcelReporte(
                "CargoMod",
                "{ Excel: '" + excel + "', Data: '" + data + "', UsuarioCreador: '' }",
                token
            );
        }

        public static string __ReporteFiniquitos(string excel, string data, string token)
        {
            return HelperCallAPI.__CallAPIExcelReporte(
                "Finiquitos",
                "{ Excel: '" + excel + "', Data: '" + data + "', UsuarioCreador: '' }",
                token
            );
        }

    }
}
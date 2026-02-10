using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Remuneraciones
{
    public class CollectionsReporte
    {
        public static dynamic __ReporteCargoMod(string excel, string codigoCargoMod)
        {
            dynamic objectsAuth = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.DeserializeObject(
                CallAPIExcel.__ReporteCargoMod(
                    excel,
                    codigoCargoMod,
                    objectsAuth[0].Token.ToString()
                )
            );
        }
    }
}
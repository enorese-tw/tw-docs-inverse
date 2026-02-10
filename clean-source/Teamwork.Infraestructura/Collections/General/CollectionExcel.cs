using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.General
{
    public class CollectionExcel
    {
        public static dynamic __ReporteFiniquitos(string excel, string data)
        {
            dynamic objectsAuth = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            return JsonConvert.DeserializeObject(
                CallAPIExcel.__ReporteFiniquitos(
                    excel,
                    data,
                    objectsAuth[0].Token.ToString()
                )
            );
        }

        public static dynamic __ReporteSolicitud(string codigoTransaction, string plantillaMasiva)
        {
            dynamic objectsAuth = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            
            return JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__PlantillaEmitirSolicitud(
                    codigoTransaction, 
                    plantillaMasiva,
                    objectsAuth[0].Token.ToString()
                )
            );
        }

    }
}
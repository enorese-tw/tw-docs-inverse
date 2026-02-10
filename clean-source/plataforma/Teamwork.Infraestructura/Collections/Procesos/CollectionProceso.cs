using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.General;
using Teamwork.Infraestructura.Instances.Procesos;
using Teamwork.Model.General;
using Teamwork.Model.Operaciones.Procesos;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Procesos
{
    public class CollectionProceso
    {
        public static List<Proceso> __ProcesoListarContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string resource)
        {
            List<Proceso> collections = new List<Proceso>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__ProcesoListarContratos(
                    codigoTransaction,
                    usuarioCreador,
                    filter,
                    dataFilter,
                    pagination,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceProceso.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Request> __ProcesoModificarNombre(string codigoTransaction, string nombreProceso, string usuarioCreador, string type, string resource)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__ProcesoModificarNombre(
                    codigoTransaction,
                    nombreProceso,
                    usuarioCreador,
                    type,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }
    }
}
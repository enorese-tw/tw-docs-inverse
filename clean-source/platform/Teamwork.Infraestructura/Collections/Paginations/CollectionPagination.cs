using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.Paginations;
using Teamwork.Model.General;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Paginations
{
    public class CollectionPagination
    {
        public static List<Pagination> __PaginationSolicitudContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string resource)
        {
            List<Pagination> collections = new List<Pagination>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__PaginationSolicitudContratos(
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
                    InstancePagination.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Pagination> __PaginationSolicitudRenovaciones(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string resource)
        {
            List<Pagination> collections = new List<Pagination>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__PaginationSolicitudRenovaciones(
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
                    InstancePagination.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }
    }
}
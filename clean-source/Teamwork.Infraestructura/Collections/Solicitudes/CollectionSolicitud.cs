using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.General;
using Teamwork.Infraestructura.Instances.Soliciudes;
using Teamwork.Model.General;
using Teamwork.Model.Kam;
using Teamwork.Model.Operaciones.Solicitudes;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Solicitudes
{
    public class CollectionSolicitud
    {
        public static List<Solicitud> __SolicitudListarContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string resource)
        {
            List<Solicitud> collections = new List<Solicitud>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__SolicitudListarContratos(
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
                    InstanceSolicitud.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Solicitud> __SolicitudListarRenovaciones(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string resource)
        {
            List<Solicitud> collections = new List<Solicitud>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__SolicitudListarRenovaciones(
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
                    InstanceSolicitud.__CreateObjectInstance(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Request> __SolicitudAnularSolicitud(string usuarioCreador, string codigoSolicitud, string type, string observacion, string resource)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPICargaMasiva.__SolicitudAnularSolicitud(
                    usuarioCreador,
                    codigoSolicitud,
                    type,
                    observacion,
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

        public static List<FichaPersonal> __ConsultaFichaPersonal(string ficha, string empresa, string rut, string filter)
        {
            List<FichaPersonal> collections = new List<FichaPersonal>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIKam.__ConsultaFichaPersonal(
                    ficha,
                    empresa,
                    rut,
                    filter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceFichaPersonal.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Contrato> __ConsultaContrato(string ficha, string empresa, string rut, string filter)
        {
            List<Contrato> collections = new List<Contrato>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIKam.__ConsultaContrato(
                    ficha,
                    empresa,
                    rut,
                    filter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceContrato.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Renovacion> __ConsultaRenovacion(string ficha, string empresa)
        {
            List<Renovacion> collections = new List<Renovacion>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIKam.__ConsultaRenovacion(
                    ficha,
                    empresa,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRenovacion.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Baja> __ConsultaBaja(string ficha, string empresa)
        {
            List<Baja> collections = new List<Baja>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIKam.__ConsultaBaja(
                    ficha,
                    empresa,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceBaja.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }
    }
}
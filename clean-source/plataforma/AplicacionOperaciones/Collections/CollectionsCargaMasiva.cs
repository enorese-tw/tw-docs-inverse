using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones;
using Teamwork.WebApi;

namespace AplicacionOperaciones.Collections
{
    public class CollectionsCargaMasiva
    {
        public static List<ProcesoCargaMasiva> __PlantillaListar(string request, string plantillaMasiva, string token, string resource)
        {
            List<ProcesoCargaMasiva> procesoCargaMasivas = new List<ProcesoCargaMasiva>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaListar(request, plantillaMasiva, token));

            for (var i = 0; i < objects.Count; i++)
            {
                procesoCargaMasivas.Add(
                    InstanceCargaMasiva.__CreateObjectInstance(objects[i], resource)
                );
            }

            return procesoCargaMasivas;
        }

        public static List<RequestCarga> __PlantillaSubir(string data, string codigoTransaction, string usuarioCreador, string templateColumn, string plantillaMasiva, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaSubir(data, codigoTransaction, usuarioCreador, templateColumn, plantillaMasiva, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<ResponseCarga> __PlantillaDownload(string plantillaMasiva, string token)
        {
            List<ResponseCarga> response = new List<ResponseCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaDownload(plantillaMasiva, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                response.Add(
                    InstanceResponseCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return response;
        }

        public static string __PlantillaReportError(string codigoTransaction, string plantillaMasiva, string token)
        {
            List<ResponseCarga> response = new List<ResponseCarga>();

            return CallAPICargaMasiva.__PlantillaReportError(codigoTransaction, plantillaMasiva, token);
        }

        public static List<RequestCarga> __PlantillaValidarCargaContrato(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaContrato(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaRenovacion(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaRenovacion(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaBajas(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaBajas(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaActualizaCreaFicha(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaActualizaCreaFicha(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearContrato(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearContrato(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearRenovacion(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearRenovacion(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearBaja(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearBaja(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<ReporteCargaMasivaContrato> __PlantillaEmitirSolicitudContrato(string codigoTransaction, string plantillaMasiva, string token)
        {
            List<ReporteCargaMasivaContrato> reporteCargaMasivaContrato = new List<ReporteCargaMasivaContrato>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaEmitirSolicitud(codigoTransaction, plantillaMasiva, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                reporteCargaMasivaContrato.Add(
                    InstanceReporte.__CreateObjectInstanceContrato(objects[i], "")
                );
            }

            return reporteCargaMasivaContrato;
        }

        public static List<ReporteCargaMasivaRenovacion> __PlantillaEmitirSolicitudRenovacion(string codigoTransaction, string plantillaMasiva, string token)
        {
            List<ReporteCargaMasivaRenovacion> reporteCargaMasivaRenovacion = new List<ReporteCargaMasivaRenovacion>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaEmitirSolicitud(codigoTransaction, plantillaMasiva, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                reporteCargaMasivaRenovacion.Add(
                    InstanceReporte.__CreateObjectInstanceRenovacion(objects[i], "")   
                );
            }
            
            return reporteCargaMasivaRenovacion;
        }

        public static List<RequestCarga> __PlantillaValidarCargaAsistencia(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaAsistencia(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearAsistencia(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearAsistencia(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaHorasExtras(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaHorasExtras(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearHorasExtras(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearHorasExtras(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<ResponseCarga> __PlantillaDownloadDinamica(string plantillaMasiva, string cliente, string fecha, string empresa, string token)
        {
            List<ResponseCarga> response = new List<ResponseCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaDownloadDinamica(plantillaMasiva, cliente, fecha, empresa, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                response.Add(
                    InstanceResponseCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return response;
        }

        public static List<RequestCarga> __PlantillaSubirDinamica(string data, string codigoTransaction, string usuarioCreador, string templateColumn, string plantillaMasiva, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaSubirDinamica(data, codigoTransaction, usuarioCreador, templateColumn, plantillaMasiva, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaAsistenciaRetail(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaAsistenciaRetail(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearAsistenciaRetail(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearAsistenciaRetail(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaAsistenciaRelojControlGeoVictoria(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaAsistenciaRelojControlGeoVictoria(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearAsistenciaRelojControlGeoVictoria(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearAsistenciaRelojControlGeoVictoria(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaAsistenciaRelojControl(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaAsistenciaRelojControl(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearAsistenciaRelojControl(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearAsistenciaRelojControl(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaValidarCargaBono(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaValidarCargaBono(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }

        public static List<RequestCarga> __PlantillaCrearBono(string codigoTransaction, string token)
        {
            List<RequestCarga> requests = new List<RequestCarga>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPICargaMasiva.__PlantillaCrearBono(codigoTransaction, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequestCarga.__CreateObjectInstance(objects[i], "")
                );
            }

            return requests;
        }
    }
}
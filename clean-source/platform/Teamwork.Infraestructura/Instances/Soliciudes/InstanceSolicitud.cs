using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones.Solicitudes;

namespace Teamwork.Infraestructura.Instances.Soliciudes
{
    public class InstanceSolicitud
    {
        public static Solicitud __CreateObjectInstance(dynamic objects, string resource = "")
        {
            Solicitud instance = new Solicitud();

            instance.NombreSolicitud = objects.NombreSolicitud.ToString();
            instance.FechaRealInicio = objects.FechaRealInicio.ToString();
            instance.FechaRealTermino = objects.FechaRealTermino.ToString();
            instance.Creado = objects.Creado.ToString();
            instance.Prioridad = objects.Prioridad.ToString();
            instance.Color = objects.Color.ToString();
            instance.BorderColor = objects.BorderColor.ToString();
            instance.Comentarios = objects.Comentarios.ToString();
            instance.CodigoSolicitud = objects.CodigoSolicitud.ToString();
            instance.NombreProceso = objects.NombreProceso.ToString();

            return instance;
        }
    }
}
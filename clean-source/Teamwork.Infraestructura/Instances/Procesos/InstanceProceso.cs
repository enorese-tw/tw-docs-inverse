using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones.Procesos;

namespace Teamwork.Infraestructura.Instances.Procesos
{
    public class InstanceProceso
    {
        public static Proceso __CreateObjectInstance(dynamic objects, string resource = "")
        {
            Proceso instance = new Proceso();

            instance.NombreProceso = objects.NombreProceso.ToString();
            instance.CreadoPor = objects.CreadoPor.ToString();
            instance.AsignadoTo = objects.AsignadoTo.ToString();
            instance.EjecutivoName = objects.EjecutivoName.ToString();
            instance.Creado = objects.Creado.ToString();
            instance.Color = objects.Color.ToString();
            instance.BorderColor = objects.BorderColor.ToString();
            instance.Prioridad = objects.Prioridad.ToString();
            instance.Comentarios = objects.Comentarios.ToString();
            instance.CodigoTransaccion = objects.CodigoTransaccion.ToString();

            return instance;
        }
    }
}
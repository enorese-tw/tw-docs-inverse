using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Kam;

namespace Teamwork.Infraestructura.Instances.Soliciudes
{
    public class InstanceRenovacion
    {
        public static Renovacion __CreateObjectInstance(dynamic objects)
        {
            Renovacion instance = new Renovacion();

            instance.FechaInicio = objects.FechaInicio.ToString();
            instance.FechaTermino = objects.FechaTermino.ToString();
            instance.Numero = objects.Numero.ToString();

            return instance;
        }
    }
}
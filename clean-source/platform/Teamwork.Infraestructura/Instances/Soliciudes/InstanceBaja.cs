using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Kam;

namespace Teamwork.Infraestructura.Instances.Soliciudes
{
    public class InstanceBaja
    {
        public static Baja __CreateObjectInstance(dynamic objects)
        {
            Baja instance = new Baja();

            instance.Estado = objects.Estado.ToString();
            instance.PoderDe = objects.PoderDe.ToString();

            return instance;
        }
    }
}
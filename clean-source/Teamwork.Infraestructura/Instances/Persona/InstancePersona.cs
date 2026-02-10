using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Persona;

namespace Teamwork.Infraestructura.Instances.Persona
{
    public class InstancePersona
    {
        public static Cliente __CreateObjectInstance(dynamic objects)
        {
            Cliente instance = new Cliente();

            instance.Nombre = objects.Cliente.ToString();

            return instance;
        }
    }
}
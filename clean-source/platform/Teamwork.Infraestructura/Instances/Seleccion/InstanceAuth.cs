using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;

namespace Teamwork.Infraestructura.Instances.Seleccion
{
    public class InstanceAuth
    {
        public static Token __CreateObjectInstance(dynamic objects)
        {
            Token instance = new Token();

            instance.Tokens = objects.Token?.ToString();

            return instance;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceBanco
    {
        public static Banco __CreateObjectInstance(dynamic objects)
        {
            Banco instance = new Banco();

            instance.Nombre = objects.Banco.ToString();

            return instance;
        }
    }
}
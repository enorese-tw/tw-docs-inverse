using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceButtonFiniquitos
    {
        public static ButtonFiniquitos __CreateObjectInstance(dynamic objects)
        {
            ButtonFiniquitos instance = new ButtonFiniquitos();

            instance.CodigoOpcion = objects.CodigoOpcion?.ToString();
            instance.Valor = objects.Valor?.ToString();

            return instance;
        }
    }
}
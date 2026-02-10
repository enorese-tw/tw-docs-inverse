using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceLiquidaciones
    {
        public static Liquidacion __CreateObjectInstance(dynamic objects)
        {
            Liquidacion instance = new Liquidacion();

            instance.FechaMes = objects.FechaMes?.ToString();
            instance.IdArchivo = objects.IdArchivo?.ToString();
            instance.Year = objects.Years?.ToString();

            return instance;
        }
    }
}
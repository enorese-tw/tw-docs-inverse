using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceDescuentoComplemento
    {
        public static DescuentoComplemento __CreateObjectInstance(dynamic objects)
        {
            DescuentoComplemento instance = new DescuentoComplemento();

            instance.CodigoDescuento = objects.CodigoDescuento.ToString();
            instance.CodigoComplemento = objects.CodigoComplemento.ToString();
            instance.Nombre = objects.Nombre.ToString();
            instance.Valor = objects.Valor.ToString();

            return instance;
        }
    }
}
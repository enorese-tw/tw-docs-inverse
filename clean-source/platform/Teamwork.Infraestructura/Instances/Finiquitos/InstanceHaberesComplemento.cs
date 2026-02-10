using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceHaberesComplemento
    {
        public static HaberesComplemento __CreateObjectInstance(dynamic objects)
        {
            HaberesComplemento instance = new HaberesComplemento();

            instance.CodigoHaber = objects.CodigoHaber.ToString();
            instance.CodigoComplemento = objects.CodigoComplemento.ToString();
            instance.Nombre = objects.Nombre.ToString();
            instance.Valor = objects.Valor.ToString();

            return instance;
        }
    }
}
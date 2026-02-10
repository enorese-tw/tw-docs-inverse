using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.General;

namespace Teamwork.Infraestructura.Instances.Paginations
{
    public class InstancePagination
    {
        public static Pagination __CreateObjectInstance(dynamic objects, string resource = "")
        {
            Pagination instance = new Pagination();

            instance.NumeroPagina = objects.NumeroPagina.ToString();
            instance.Rango = objects.Rango.ToString();
            instance.Class = objects.Class.ToString();
            instance.Properties = objects.Properties.ToString();
            instance.Filter = objects.Filter.ToString();
            instance.DataFilter = objects.DataFilter.ToString();

            return instance;
        }
    }
}
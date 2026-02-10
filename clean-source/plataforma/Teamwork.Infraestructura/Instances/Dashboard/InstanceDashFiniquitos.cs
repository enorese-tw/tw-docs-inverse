using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Dashboard;

namespace Teamwork.Infraestructura.Instances.Dashboard
{
    public class InstanceDashFiniquitos
    {
        public static DashFiniquitos __CreateObjectInstance(dynamic objects)
        {
            DashFiniquitos instance = new DashFiniquitos();

            instance.Categoria = objects.Categoria.ToString();
            instance.Valor = objects.Valor.ToString();
            instance.Descripcion = objects.Descripcion.ToString();
            instance.Background = objects.Background.ToString();
            instance.Filter = objects.Filter.ToString();
            instance.DataFilter = objects.DataFilter.ToString();
            instance.Explicacion = objects.Explicacion.ToString();
            instance.OrderBy = objects.OrderBy.ToString();

            return instance;
        }
    }
}
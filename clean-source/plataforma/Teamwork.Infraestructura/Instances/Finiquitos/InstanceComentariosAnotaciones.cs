using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceComentariosAnotaciones
    {
        public static ComentariosAnotaciones __CreateObjectInstance(dynamic objects)
        {
            ComentariosAnotaciones instance = new ComentariosAnotaciones();

            instance.Html = objects.Html?.ToString();

            return instance;
        }
    }
}
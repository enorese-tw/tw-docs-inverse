using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceHistorial
    {
        public static Historial __CreateObjectInstance(dynamic objects)
        {
            Historial instance = new Historial();

            instance.Fecha = objects.Fecha.ToString();
            instance.Usuario = objects.Creador.ToString();
            instance.Comentarios = objects.Comentarios.ToString();

            return instance;
        }
    }
}
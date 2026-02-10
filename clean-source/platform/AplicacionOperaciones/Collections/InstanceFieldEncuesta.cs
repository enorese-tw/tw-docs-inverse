using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;

namespace AplicacionOperaciones.Collections
{
    public class InstanceFieldEncuesta
    {
        public static FieldEncuesta __CreateObjectInstance(dynamic objects, dynamic request)
        {
            FieldEncuesta instance = new FieldEncuesta();
            
            instance.Type = objects.Type.ToString();
            instance.Placeholder = objects.Placeholder.ToString();
            instance.Name = objects.Name.ToString();
            instance.Required = objects.Required.ToString();
            instance.HiddenUser = objects.HiddenUser.ToString();

            return instance;
        }
    }
}
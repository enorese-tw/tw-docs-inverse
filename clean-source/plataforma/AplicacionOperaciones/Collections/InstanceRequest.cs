using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;

namespace AplicacionOperaciones.Collections
{
    public class InstanceRequest
    {
        public static Request __CreateObjectInstance(dynamic objects, dynamic request)
        {
            Request instance = new Request();

            instance.Code = objects.Code.ToString();
            instance.Message = objects.Message.ToString();

            return instance;
        }
    }
}
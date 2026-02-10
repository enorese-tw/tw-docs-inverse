using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.General;

namespace Teamwork.Infraestructura.Instances.General
{
    public class InstanceRequest
    {
        public static Request __CreateObjectInstance(dynamic objects, dynamic request)
        {
            Request instance = new Request();

            instance.Code = objects.Code.ToString();
            instance.Message = objects.Message.ToString();
            instance.Codigo = objects.Codigo?.ToString();

            return instance;
        }
    }
}
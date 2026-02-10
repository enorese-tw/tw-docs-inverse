using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Autentificacion;

namespace Teamwork.Infraestructura.Instances.Auth
{
    public class InstanceAuth
    {
        public static TokenConfianza __CreateObjectInstance(dynamic objects)
        {
            TokenConfianza instance = new TokenConfianza();
            
            instance.Token = objects.token.ToString();

            return instance;
        }
    }
}
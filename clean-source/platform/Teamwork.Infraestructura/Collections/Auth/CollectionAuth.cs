using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.Auth;
using Teamwork.Model.Autentificacion;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Auth
{
    public class CollectionAuth
    {
        public static List<TokenConfianza> __CrearTokenConfianza(string usuarioCreador)
        {
            List<TokenConfianza> collections = new List<TokenConfianza>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIToken.__CrearTokenConfianza(
                    usuarioCreador,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceAuth.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }
    }
}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.General;
using Teamwork.Infraestructura.Instances.Persona;
using Teamwork.Model.General;
using Teamwork.Model.Persona;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Persona
{
    public class CollectionPersona
    {
        public static List<Cliente> __PersonaConsultarCliente(string filter, string dataFilter)
        {
            List<Cliente> collections = new List<Cliente>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIPersona.__PersonaConsultarCliente(
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstancePersona.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __PersonaCrearCliente(string cliente)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIPersona.__PersonaCrearCliente(
                    cliente,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __PersonaEliminarCliente(string cliente)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIPersona.__PersonaEliminarCliente(
                    cliente,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }
    }
}
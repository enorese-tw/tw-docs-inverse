using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.Dashboard;
using Teamwork.Model.Dashboard;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Dasboard
{
    public class CollectionDashFiniquitos
    {
        public static List<DashFiniquitos> __FiniquitosDashboardFiniquitos(string usuarioCreador)
        {
            List<DashFiniquitos> collections = new List<DashFiniquitos>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosDashboardFiniquitos(
                    usuarioCreador,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceDashFiniquitos.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }
    }
}
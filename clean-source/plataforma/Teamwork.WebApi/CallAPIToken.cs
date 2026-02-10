using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIToken
    {
        public static string __CrearTokenConfianza(string username, string token)
        {
            return HelperCallAPI.__CallAPIToken(
                "CrearTokenConfianza",
                "{ Username: '" + username + "' }",
                token
            );
        }

    }
}
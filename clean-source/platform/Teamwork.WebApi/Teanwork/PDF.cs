using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Teamwork.WebApi.Teanwork
{
    public class PDF
    {
        static string APIRest = WebConfigurationManager.AppSettings["UrlApiRest"];

        public static string __RemuneracionesEstructuraCargoMod(string dataIn, string usuarioCreador, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Data: '" + dataIn + "', UsuarioCreador: '" + usuarioCreador + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "pdf/Remuneraciones/EstructuraCargoMod") as HttpWebRequest;
            request.Method = "POST";
            request.Accept = "application/json; charset=utf-8";
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Base " + token);

            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                StreamReader reader = new StreamReader(response.GetResponseStream());
                responseJson = reader.ReadToEnd();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse err = ex.Response as HttpWebResponse;
                    if (err != null)
                    {
                        string htmlResponse = new StreamReader(err.GetResponseStream()).ReadToEnd();
                        responseJson = htmlResponse;
                    }
                }
            }

            return responseJson;
        }
    }
}
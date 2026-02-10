using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Teamwork.WebApi.Operaciones
{
    public class ValorDiario
    {
        static string APIRest = WebConfigurationManager.AppSettings["UrlApiRest"];

        public static string __ValorDiarioCrear(string empresa, string cliente, string cargoMod, string valorDiario, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', Cliente: '" + cliente + "', CargoMod: '" + cargoMod + "', Monto: '" + valorDiario + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "finanzas/ValorDiario/Crear") as HttpWebRequest;
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

        public static string __ValorDiarioListar(string pagination, string typeFilter, string dataFilter, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "finanzas/ValorDiario/Listar") as HttpWebRequest;
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

        public static string __ValorDiarioActualizar(string empresa, string cliente, string cargoMod, string valorDiario, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', Cliente: '" + cliente + "', CargoMod: '" + cargoMod + "', Monto: '" + valorDiario + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "finanzas/ValorDiario/Actualizar") as HttpWebRequest;
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

        public static string __ValorDiarioEliminar(string empresa, string cargoMod, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', CargoMod: '" + cargoMod + "'}";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "finanzas/ValorDiario/Eliminar") as HttpWebRequest;
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
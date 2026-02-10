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
    public class Bonos
    {

        static string APIRest = WebConfigurationManager.AppSettings["UrlApiRest"];

        public static string __Bonos(string usuarioCreador, string pagination, string typeFilter, string dataFilter, string cliente, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "', Cliente: '" + cliente + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request; 
            request = WebRequest.Create(APIRest + "operaciones/Bonos/Bonos") as HttpWebRequest;
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

        public static string __ValidateAsignBonos(string codigoBono, string cliente, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoBono: '" + codigoBono + "', Cliente: '" + cliente + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/ValidateAsignBonos") as HttpWebRequest;
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

        public static string __AgregarBono(string usuarioCreador, string codigoBono, string codigoCargoMod, string monto, string condiciones, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoBono: '" + codigoBono + "', CodigoCargoMod: '" + codigoCargoMod + "', Monto: '" + monto + "', Condiciones: '" + condiciones + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/AgregarBono") as HttpWebRequest;
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

        public static string __BonosCargoMod(string codigoCargoMod, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoCargoMod: '" + codigoCargoMod + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/BonosCargoMod") as HttpWebRequest;
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
        
        public static string __AgregarNuevoBono(string usuarioCreador, string codigoCargoMod, string glosaNewBono, string monto, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoCargoMod: '" + codigoCargoMod + "', GlosaNewBono: '" + glosaNewBono + "', Monto: '" + monto + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/AgregarNuevoBono") as HttpWebRequest;
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

        public static string __EliminarBono(string usuarioCreador, string codigoBono, string codigoCargoMod, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoBono: '" + codigoBono + "', CodigoCargoMod: '" + codigoCargoMod + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/EliminarBono") as HttpWebRequest;
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

        public static string __ActualizarBono(string usuarioCreador, string codigoBono, string codigoCargoMod, string monto, string condiciones, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoBono: '" + codigoBono + "', CodigoCargoMod: '" + codigoCargoMod + "', Monto: '" + monto + "', Condiciones: '" + condiciones + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/Bonos/ActualizarBono") as HttpWebRequest;
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
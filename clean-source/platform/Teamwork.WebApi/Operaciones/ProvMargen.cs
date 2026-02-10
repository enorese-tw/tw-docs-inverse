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
    public class ProvMargen
    {
        static string APIRest = WebConfigurationManager.AppSettings["UrlApiRest"];

        public static string __ProvMargenCrear(string usuarioCreador, string glosaNewConcepto, string type, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', GlosaNewConcepto: '" + glosaNewConcepto + "', Type: '" + type + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Crear") as HttpWebRequest;
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
        
        public static string __ProvMargenLista(string pagination, string typeFilter, string dataFilter, string type, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "', Type: '" + type + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Lista") as HttpWebRequest;
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
        
        public static string __ProvMargenEliminar(string usuarioCreador, string codigoConcepto, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Eliminar") as HttpWebRequest;
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

        public static string __ProvMargenDesactivar(string usuarioCreador, string codigoConcepto, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Desactivar") as HttpWebRequest;
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
        
        public static string __ProvMargenActivar(string usuarioCreador, string codigoConcepto, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Activar") as HttpWebRequest;
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
    
        public static string __ProvMargenActualizar(string usuarioCreador, string codigoConcepto, string glosaNewConcepto, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "', GlosaNewConcepto: '" + glosaNewConcepto + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/Actualizar") as HttpWebRequest;
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

        public static string __ProvMargenValidateAsignacion(string codigoConcepto, string cliente, string type, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoConcepto: '" + codigoConcepto + "', Cliente: '" + cliente + "', Type: '" + type + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ValidateAsignacion") as HttpWebRequest;
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

        public static string __ProvMargenListaConstCalcProv(string token)
        {
            string responseJson = string.Empty;
            string json = "{  }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ListaConstCalcProv") as HttpWebRequest;
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

        public static string __ProvMargenListaConstTopeProv(string token)
        {
            string responseJson = string.Empty;
            string json = "{  }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ListaConstTopeProv") as HttpWebRequest;
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

        public static string __ProvMargenCrearAsig(string usuarioCreador, string codigoConcepto, string cliente, string type, string monto, string typeInput, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Type: '" + type + "', Monto: '" + monto + "', TypeInput: '" + typeInput + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/CrearAsig") as HttpWebRequest;
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

        public static string __ProvMargenEliminarAsig(string usuarioCreador, string codigoConcepto, string cliente, string type, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Type: '" + type + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/EliminarAsig") as HttpWebRequest;
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

        public static string __ProvMargenActualizarAsig(string usuarioCreador, string codigoConcepto, string cliente, string type, string monto, string typeInput, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Type: '" + type + "', Monto: '" + monto + "', TypeInput: '" + typeInput + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ActualizarAsig") as HttpWebRequest;
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

        public static string __ProvMargenActualizarTypeCalculo(string usuarioCreador, string codigoConcepto, string cliente, string type, string typeInput, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Type: '" + type + "', TypeInput: '" + typeInput + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ActualizarTypeCalculo") as HttpWebRequest;
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

        public static string __ProvMargenOpcionesEnabledAsig(string codigoConcepto, string cliente, string type, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Type: '" + type + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/OpcionesEnabledAsig") as HttpWebRequest;
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

        public static string __ProvMargenCrearAsignConstCalculo(string usuarioCreador, string constante, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Constante: '" + constante + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/CrearAsignConstCalculo") as HttpWebRequest;
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
        
        public static string __ProvMargenEliminarAsignConstCalculo(string usuarioCreador, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/EliminarAsignConstCalculo") as HttpWebRequest;
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

        public static string __ProvMargenActualizarAsignConstCalculo(string usuarioCreador, string constante, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Constante: '" + constante + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ActualizarAsignConstCalculo") as HttpWebRequest;
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


        public static string __ProvMargenCrearAsignConstTope(string usuarioCreador, string constante, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Constante: '" + constante + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/CrearAsignConstTope") as HttpWebRequest;
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

        public static string __ProvMargenEliminarAsignConstTope(string usuarioCreador, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/EliminarAsignConstTope") as HttpWebRequest;
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

        public static string __ProvMargenActualizarAsignConstTope(string usuarioCreador, string constante, string codigoConcepto, string cliente, string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Constante: '" + constante + "', CodigoConcepto: '" + codigoConcepto + "',  Cliente: '" + cliente + "', Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/ProvMargen/ActualizarAsignConstTope") as HttpWebRequest;
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
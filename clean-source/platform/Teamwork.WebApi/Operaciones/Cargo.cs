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
    public class Cargo
    {
        static string APIRest = WebConfigurationManager.AppSettings["UrlApiRest"];

        public static string __CrearSolicitud(string empresa, string usuarioCreador, string evento, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', UsuarioCreador: '" + usuarioCreador + "', Type: '" + evento + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CrearSolicitud") as HttpWebRequest;
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

        public static string __DeshacerSolicitud(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/DeshacerSolicitud") as HttpWebRequest;
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

        public static string __Solicitudes(string usuarioCreador, string pagination, string typeFilter, string dataFilter, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Solicitudes") as HttpWebRequest;
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

        public static string __ValidaSeleccionCliente(string usuarioCreador, string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ValidaSeleccionCliente") as HttpWebRequest;
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

        public static string __Clientes(string empresa, string pagination, string typeFilter, string dataFilter, string usuarioCreador, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "', UsuarioCreador: '" + usuarioCreador + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Clientes") as HttpWebRequest;
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

        public static string __PaginationClientes(string empresa, string pagination, string typeFilter, string dataFilter, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "', Pagination: '" + pagination + "', TypeFilter: '" + typeFilter + "', DataFilter: '" + dataFilter + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/PaginationClientes") as HttpWebRequest;
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

        public static string __ActualizaCliente(string usuarioCreador, string cliente, string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Cliente: '" + cliente + "', CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaCliente") as HttpWebRequest;
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

        public static string __ActualizaNombreCargo(string usuarioCreador, string nombreCargo, string codigoSolicitud, string type, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', NombreCargo: '" + nombreCargo + "', CodigoSolicitud: '" + codigoSolicitud + "', Type: '" + type + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaNombreCargo") as HttpWebRequest;
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
        
        public static string __ActualizaSueldo(string usuarioCreador, string sueldo, string codigoSolicitud, string typeSueldo, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Sueldo: '" + sueldo + "', CodigoSolicitud: '" + codigoSolicitud + "', TypeSueldo: '" + typeSueldo + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaSueldo") as HttpWebRequest;
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

        public static string __ActualizaGratificacion(string usuarioCreador, string gratificacion, string typeGratif, string observGratiConv, string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', Gratificacion: '" + gratificacion + "', TypeGratif: '" + typeGratif + "', ObservGratiConv: '" + observGratiConv + "', CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaGratificacion") as HttpWebRequest;
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

        public static string __Dashboard(string usuarioCreador, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Dashboard") as HttpWebRequest;
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
        
        public static string __EstructuraHeader(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Estructura/Header") as HttpWebRequest;
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

        public static string __EstructuraHaberes(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Estructura/Haberes") as HttpWebRequest;
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
        
        public static string __EstructuraDescuentos(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Estructura/Descuentos") as HttpWebRequest;
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

        public static string __EstructuraMargenProvision(string usuarioCreador, string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/Estructura/MargenProvision") as HttpWebRequest;
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

        public static string __AFP(string empresa, string token)
        {
            string responseJson = string.Empty;
            string json = "{ Empresa: '" + empresa + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/AFP") as HttpWebRequest;
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

        public static string __ActualizarAFP(string codigoSolicitud, string afp, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "', AFP: '" + afp + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizarAFP") as HttpWebRequest;
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

        public static string __CambiarInputSueldo(string codigoSolicitud, string sueldo, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "', Sueldo: '" + sueldo + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CambiarInputSueldo") as HttpWebRequest;
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

        public static string __CambiarCalculoTypeSueldo(string codigoSolicitud, string sueldo, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "', Sueldo: '" + sueldo + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CambiarCalculoTypeSueldo") as HttpWebRequest;
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
        
        public static string __CambiarProvMargGastoExcInc(string usuarioCreador, string codigoSolicitud, string excInc, string codigoVar, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', ExcInc: '" + excInc + "', CodigoVar: '" + codigoVar + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CambiarProvMargGastoExcInc") as HttpWebRequest;
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

        public static string __CambiarTypeJornada(string codigoSolicitud, string typeJornada, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "', Sueldo: '" + typeJornada + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CambiarTypeJornada") as HttpWebRequest;
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
        
        public static string __CambioEstadoSolicitud(string usuarioCreador, string codigoSolicitud, string estado, string observaciones, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Estado: '" + estado + "', Observaciones: '" + observaciones + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/CambioEstadoSolicitud") as HttpWebRequest;
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

        public static string __ValidaStageActual(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ValidaStageActual") as HttpWebRequest;
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

        public static string __ActualizaValorDiaPT(string usuarioCreador, string codigoSolicitud, string valorDiario, string horasSemanales, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', ValorDiario: '" + valorDiario + "', HorasSemanales: '" + horasSemanales + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaValorDiaPT") as HttpWebRequest;
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

        public static string __ActualizaObservaciones(string usuarioCreador, string codigoSolicitud, string Observaciones, string type, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Observaciones: '" + Observaciones + "', Type: '" + type + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaObservaciones") as HttpWebRequest;
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

        public static string __EliminarObservaciones(string usuarioCreador, string codigoSolicitud, string Observaciones, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Observaciones: '" + Observaciones + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/EliminarObservaciones") as HttpWebRequest;
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

        public static string __ValidaCadenaObservaciones(string usuarioCreador, string codigoSolicitud, string Observaciones, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Observaciones: '" + Observaciones + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ValidaCadenaObservaciones") as HttpWebRequest;
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

        public static string __ActualizaHorario(string usuarioCreador, string codigoSolicitud, string horarios, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Horarios: '" + horarios + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaHorario") as HttpWebRequest;
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

        public static string __ActualizaJornadaFullTime(string usuarioCreador, string codigoSolicitud, string horarios, string horasSemanales, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Horarios: '" + horarios + "', HorasSemanales: '" + horasSemanales + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaJornadaFullTime") as HttpWebRequest;
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

        public static string __ActualizaWizards(string codigoSolicitud, string estado, string usuarioCreador, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "', Estado: '" + estado + "', UsuarioCreador: '" + usuarioCreador + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaWizards") as HttpWebRequest;
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

        public static string __ListObservaciones(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ListObservaciones") as HttpWebRequest;
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

        public static string __ActualizaDiasSemanales(string usuarioCreador, string codigoSolicitud, string dia, string token)
        {
            string responseJson = string.Empty;
            string json = "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Dia: '" + dia + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/ActualizaDiasSemanales") as HttpWebRequest;
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

        public static string __HistorialCargoMod(string codigoSolicitud, string token)
        {
            string responseJson = string.Empty;
            string json = "{ CodigoSolicitud: '" + codigoSolicitud + "' }";

            byte[] data = UTF8Encoding.UTF8.GetBytes(json);

            HttpWebRequest request;
            request = WebRequest.Create(APIRest + "operaciones/CargoMod/HistorialCargoMod") as HttpWebRequest;
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
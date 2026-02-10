using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace ServicioFiniquitos
{
    public class DatosXML
    {
        private string _nombrearchivo = string.Empty;

        private string _usuarioBD = string.Empty;
        private string _claveBDSA = string.Empty;
        private string _servidorBDSA = string.Empty;
        private string _basededatos = string.Empty;

        private string _softlandUsuarioDB = string.Empty;
        private string _softlandClaveDB = string.Empty;
        private string _softlandServidorDB = string.Empty;
        private string _softlandBasededatosEst = string.Empty;
        private string _softlandBasededatosOut = string.Empty;
        private string _softlandBasededatosConsultora = string.Empty;

        private const string RaizXML = "tw";
        string NombreArchivoXML = RutaArchivoWeb() + "\\config.xml";

        public DatosXML()
        {
            _usuarioBD = UsuarioBDSQLServer();
            _claveBDSA = ClaveUsuarioBDSQLServerSA();
            _servidorBDSA = ServidorSQLServerSA();
            _basededatos = BaseDeDatosSQLServer();
            _softlandServidorDB = SoftlandServidor();
            _softlandUsuarioDB = SoftlandUsuario();
            _softlandBasededatosEst = SoftlandBasededatosEst();
            _softlandBasededatosOut = SoftlandBasededatosOut();
            _softlandBasededatosConsultora = SoftlandBasededatosConsultora();
            _softlandClaveDB = SoftlandClave();
        }

        public string BaseDeDatos
        {
            get
            {
                return _basededatos;
            }
        }

        public string ServidorBaseDeDatosSA
        {
            get
            {
                return _servidorBDSA;
            }
        }

        public string UsuarioBD
        {
            get
            {
                return _usuarioBD;
            }
        }

        public string ClaveBDSA
        {
            get
            {
                return _claveBDSA;
            }
        }

        /** METODOS ACCESADORES PARA SOFTLAND */

        public string SoftlandServidorDB
        {
            get 
            {
                return _softlandServidorDB;
            }
        }

        public string SoftlandUsuarioDB
        {
            get
            {
                return _softlandUsuarioDB;
            }
        }

        public string SoftlandBaseDeDatosEST
        {
            get
            {
                return _softlandBasededatosEst;
            }
        }

        public string SoftlandBaseDeDatosOUT
        {
            get
            {
                return _softlandBasededatosOut;
            }
        }

        public string SoftlandBaseDeDatosCONSULTORA
        {
            get
            {
                return _softlandBasededatosConsultora;
            }
        }

        public string SoftlandClaveDB
        {
            get
            {
                return _softlandClaveDB;
            }
        }

        /** METODOS DE ACCESO A DATOS ARCHIVO CONFIG.XML */
        private static string RutaArchivoWeb()
        {
            try
            {
                string sAppPath;
                sAppPath = AppDomain.CurrentDomain.BaseDirectory;
                return (sAppPath + "bin");
            }
            catch (Exception ex)
            {
                return "";
                throw new Exception(ex.Message);
            }
        }

        public XmlDocument CargaXML(string NombreArchivo)
        {
            XmlDocument Documento = new XmlDocument();
            Documento.Load(NombreArchivo);
            return Documento;
        }

        public XmlNodeList BuscaNodos(XmlDocument DocXML, string Nombre)
        {
            if (!(DocXML == null))
            {
                return DocXML.SelectNodes(Nombre);
            }
            else
            {
                return null;
            }
        }

        /** METODOS DE EXTRACCION DE DATOS ARCHIVO CONFIG.XML CON CONEXIONES A BASES DE DATOS */

        private string UsuarioBDSQLServer()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);
            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/sqlserver/nombre");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.Attributes.GetNamedItem("usuario").InnerText;
            }

            return aux;
        }

        private string ClaveUsuarioBDSQLServerSA()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);
            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/sqlserver/nombre/passwordsa");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        private string ServidorSQLServerSA()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);
            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/sqlserver/nombre/servidorsa");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        private string BaseDeDatosSQLServer()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/sqlserver/nombre/basedatos");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        /** METODOS DE EXTRACCION DE DATOS PARA SOFTLAND */

        private string SoftlandServidor()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre/servidor");
            foreach(XmlNode Nodo in Nodos)
            {
                if(Nodo.ChildNodes[0].Value.Contains("\\\\")){
                    aux = Nodo.ChildNodes[0].Value.Replace("\\\\", "\\");
                } else {
                    aux = Nodo.ChildNodes[0].Value;
                }
            }

            return aux;
        }

        private string SoftlandBasededatosOut()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre/basededatosout");
            foreach(XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        private string SoftlandBasededatosEst()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre/basededatosest");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        private string SoftlandBasededatosConsultora()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre/basededatosconsultora");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

        private string SoftlandUsuario()
        {
            string aux = "";

            XmlDocument Documento = CargaXML(NombreArchivoXML);
            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre");

            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.Attributes.GetNamedItem("usuario").InnerText;
            }

            return aux;
        }

        private string SoftlandClave()
        {
            string aux = "";
            XmlDocument Documento = CargaXML(NombreArchivoXML);

            XmlNodeList Nodos = BuscaNodos(Documento, RaizXML + "/softland/nombre/password");
            foreach (XmlNode Nodo in Nodos)
            {
                aux = Nodo.ChildNodes[0].Value;
            }

            return aux;
        }

    }
}
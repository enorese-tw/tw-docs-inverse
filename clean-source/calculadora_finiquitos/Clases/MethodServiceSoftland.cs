using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class MethodServiceSoftland
    {

        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

        public string RUTTRABAJADOR { get; set; }
        public string FICHA { get; set; }
        
        public MethodServiceSoftland() { }

        public DataSet GetRutTrabajadorSolicitudBajaService
        {
            get
            {
                return GetRutTrabajadorSolicitudBaja(RUTTRABAJADOR);
            }
        }

        public DataSet GetRutTrabajadorSolicitudBajaOUTService
        {
            get
            {
                return GetRutTrabajadorSolicitudBajaOUT(RUTTRABAJADOR);
            }
        }

        public DataSet GetRutTrabajadorSolicitudBajaCONSULTORAService
        {
            get
            {
                return GetRutTrabajadorSolicitudBajaCONSULTORA(RUTTRABAJADOR);
            }
        }

        public DataSet GetJornadasParttimeESTService
        {
            get
            {
                return GetJornadasParttimeEST(FICHA);
            }
        }

        public DataSet GetTrabajadorPartTimeESTService
        {
            get
            {
                return GetTrabajadorPartTimeEST(FICHA);
            }
        }

        public DataSet GetCreditoCCAFESTService
        {
            get
            {
                return GetCreditoCCAFEST(FICHA);
            }
        }

        public DataSet GetRetencionJudicialOUTService
        {
            get
            {
                return GetRetencionJudicialOUT(FICHA);
            }
        }

        public DataSet GetJornadasParttimeOUTService
        {
            get
            {
                return GetJornadasParttimeOUT(FICHA);
            }
        }

        /** ------------------------ */

        private DataSet GetRutTrabajadorSolicitudBaja(string rutTrabajador)
        {

            string[] parametros = {
                rutTrabajador
            };

            return svcFiniquitos.GetRutTrabajadorSolicitudBaja(parametros).Table;
        }

        private DataSet GetRutTrabajadorSolicitudBajaOUT(string rutTrabajador)
        {

            string[] parametros = {
                rutTrabajador
            };

            return svcFiniquitos.GetRutTrabajadorSolicitudBajaOUT(parametros).Table;
        }

        private DataSet GetRutTrabajadorSolicitudBajaCONSULTORA(string rutTrabajador)
        {

            string[] parametros = {
                rutTrabajador
            };

            return svcFiniquitos.GetRutTrabajadorSolicitudBajaCONSULTORA(parametros).Table;
        }

        private DataSet GetJornadasParttimeEST(string ficha)
        {

            string[] parametros = {
                ficha
            };

            return svcFiniquitos.GetJornadasParttimeEST(parametros).Table;
        }

        private DataSet GetTrabajadorPartTimeEST(string ficha)
        {

            string[] parametros = {
                ficha
            };

            return svcFiniquitos.GetTrabajadorPartTimeEST(parametros).Table;
        }

        private DataSet GetCreditoCCAFEST(string ficha)
        {

            string[] parametros = {
                ficha
            };

            return svcFiniquitos.GetCreditoCCAFEST(parametros).Table;
        }

        private DataSet GetRetencionJudicialOUT(string ficha)
        {

            string[] parametros = {
                ficha
            };

            return svcFiniquitos.GetRetencionJudicialOUT(parametros).Table;
        }

        private DataSet GetJornadasParttimeOUT(string ficha)
        {

            string[] parametros = {
                ficha
            };

            return svcFiniquitos.GetJornadasParttimeOUT(parametros).Table;
        }

    }
}
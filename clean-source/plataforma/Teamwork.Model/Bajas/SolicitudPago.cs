using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Bajas
{
    public class SolicitudPago
    {
        public string Code { get; set;}
        public string Message { get; set; }
        public string Beneficiario { get; set; }
        public string Rut { get; set; }
        public string Banco { get; set; }
        public string Cuenta { get; set; }
        public string Location { get; set; }
        public string MontoFiniquito { get; set; }
        public string MontoAdm { get; set; }
        public string Total { get; set; }
        public string Tipo { get; set; }
    }
}
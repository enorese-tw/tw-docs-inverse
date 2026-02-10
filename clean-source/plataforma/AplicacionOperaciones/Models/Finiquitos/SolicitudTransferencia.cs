using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finiquitos
{
    public class SolicitudTransferencia
    {
        public string Nombres { get; set; }
        public string Banco { get; set; }
        public string Fecha { get; set; }
        public string TipoDeposito { get; set; }
        public string Cuenta { get; set; }
        public string Rut { get; set; }
        public string TotalFiniquito { get; set; }
        public string TotalPago { get; set; }
        public string Observacion { get; set; }
        public string CodigoPago { get; set; }
        public string GastoAdministrativo { get; set; }
        public string MontoTotal { get; set; }
        public string Observaciones { get; set; }
        public string Location { get; set; }
        public string Documento { get; set; }
        public string Origen { get; set; }
    }
}
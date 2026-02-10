using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class DashboardFiniquitos
    {
        public string Calculados { get; set; }
        public string EnTransito { get; set; }
        public string Confirmados { get; set; }
        public string EmisionPago { get; set; }
        public string PendientePagoT90 { get; set; }
        public string PendientePagoL90 { get; set; }
        public string Notariado { get; set; }
        public string TEFPendientesPago { get; set; }

        public string BajasConfirmadas { get; set; }
        public string Simulaciones { get; set; }
        public string CompCreados { get; set; }
        public string CompPendAuth { get; set; }
        public string CompPendEmiPago { get; set; }
        public string CompPendPagoTEF { get; set; }
        public string CompPendPagoL90 { get; set; }
        public string CompPendPagoT90 { get; set; }
        public string CompNotariar { get; set; }



    }
}
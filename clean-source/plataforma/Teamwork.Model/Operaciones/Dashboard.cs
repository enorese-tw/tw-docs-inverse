using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class Dashboard
    {
        public string Simulaciones { get; set; }
        public string ValidacionCotizaciones { get; set; }
        public string CotizacionesAprobadas { get; set; }
        public string CotizacionesRechazadas { get; set; }
        public string Creaciones { get; set; }
        public string RechazadosKam { get; set; }
        public string RechazadosRem { get; set; }
        public string PendienteFinanzas { get; set; }
        public string PendienteRemuneraciones { get; set; }
        public string PendientesFirmaDigital { get; set; }
        public string Terminados { get; set; }
        public string Profile { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class EstadisticaKam
    {
        public string Code { get; set; }
        public string Dashboard { get; set; }
        public string TotalSolicitudes { get; set; }
        public string TotalSolContratos { get; set; }
        public string PercentageSolContratos { get; set; }
        public string TotalSolRenovaciones { get; set; }
        public string PercentageSolRenovaciones { get; set; }
        public string TotalProcesos { get; set; }
        public string TotalProcContratos { get; set; }
        public string PercentageProcContratos { get; set; }
        public string TotalProcRenovaciones { get; set; }
        public string PercentageProcRenovaciones { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Finanzas
{
    public class ProcesosPago
    {
        public string CodigoProceso { get; set; }
        public string NombreProceso { get; set; }
        public string Creado { get; set; }
        public string CreadoPor { get; set; }
        public string FechaCierreDate { get; set; }
        public string FechaAperturaDate { get; set; }
        public string TotalProceso { get; set; }
        public string CantidadPagos { get; set; }
        public string ViewDetalleProceso { get; set; }
        public string DownloadReportePago { get; set; }
        public string EnlaceContinuarProcesoMasivo { get; set; }
        public string TipoPago { get; set; }
        public string Border { get; set; }
        public string GlyphiconColor { get; set; }
        public string OptReportePagos { get; set; }
        public string OptIniciarProcesoMasivoPago { get; set; }
        public string OptContinarProcesoMasivoPago { get; set; }
        public string OptPagarMasivo { get; set; }
    }
}
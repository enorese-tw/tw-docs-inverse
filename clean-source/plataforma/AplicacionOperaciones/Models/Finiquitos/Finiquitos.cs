using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finiquitos
{
    public class Finiquitos
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Folio { get; set; }
        public string NombreSolicitud { get; set; }
        public string CodigoFiniquito { get; set; }
        public string CreadoPor { get; set; }
        public string Creado { get; set; }
        public string CargoMod { get; set; }
        public string Cargo { get; set; }
        public string Causal { get; set; }
        public string TotalFiniquito { get; set; }
        
        public string DetalleFiniquito { get; set; }

        public string ViewCaratula { get; set; }
        public string ViewDocumento { get; set; }
        public string DownloadCaratula { get; set; }
        public string DownloadDocumento { get; set; }
        public string ViewCarta { get; set; }

        public string Border { get; set; }
        public string GlyphiconColor { get; set; }
        public string Glyphicon { get; set; }
        public string Estado { get; set; }

        public string OptAnular { get; set; }
        public string OptViewCaratula { get; set; }
        public string OptDownloadCaratula { get; set; }
        public string OptViewDocumento { get; set; }
        public string OptDownloadDocumento { get; set; }
        public string ConfirmarPago { get; set; }
        public string OptAnularPago { get; set; }
        public string OptRevertirConfirmacion { get; set; }
        public string OptPagar { get; set; }
        public string OptNotariado { get; set; }
        public string OptSugTransf { get; set; }
        public string OptRevSugTef { get; set; }
        public string OptVerificarPago { get; set; }
        public string OptRevVerificarPago { get; set; }
        public string OptEnviarProcesoPago { get; set; }
        public string OptRevEnvioProcesoPago { get; set; }
        public string OptLiquidacionSueldo { get; set; }

        public string OptMoverFiniquito { get; set; }
        public string OptViewCarta { get; set; }

        public string Comentarios { get; set; }
        public string CodigoPago { get; set; }
        public string TipoPago { get; set; }

        public string OnlyFiniquito { get; set; }
    }
}
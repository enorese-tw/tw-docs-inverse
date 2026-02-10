using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Bajas
{
    public class Finiquito
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
        public string Comentarios { get; set; }
        public string Border { get; set; }
        public string GlyphiconColor { get; set; }
        public string Glyphicon { get; set; }
        public string Estado { get; set; }

        public string OptCaratulaFiniquito { get; set; }
        public string OptDocFiniquito { get; set; }
        public string OptHistorialFiniquito { get; set; }
        public string OptValidarFiniquito { get; set; }
        public string OptAnularFiniquito { get; set; }
        public string OptGestionEnvioFiniquito { get; set; }
        public string OptRecepcionFiniquito { get; set; }

        public string OptRecepcionFiniquitoRegiones { get; set; }
        public string OptRecepcionFiniquitoNotaria { get; set; }
        public string OptRecepcionFiniquitoStgoFirma { get; set; }
        
        public string OptSolValeVista { get; set; }
        public string OptSolTef { get; set; }
        public string OptSolCheque { get; set; }

        public string OptEnvioLegalizacion { get; set; }
        public string OptRecepcionLegalizacion { get; set; }

        public string OptConfirmarFiniquito { get; set; }
        public string OptPagarFiniquito { get; set; }

        public string OptLiquidacionesSueldo { get; set; }
        public string OptCrearComplemento { get; set; }

        public string OptActualizarMontoAdministrativo { get; set; }
        public string OptRevertirValidacion { get; set; }
        public string OptRevertirGestionEnvio { get; set; }
        public string OptRevertirLegalizacion { get; set; }
        public string OptRevertirSolicitudPago { get; set; }
        public string OptRevertirConfirmacion { get; set; }
        public string OptRevertirEmisionPago { get; set; }
        public string OptComentarios { get; set; }
        public string OptTerminarFiniquito { get; set; }
        public string OptFirmarFiniquito { get; set; }
        public string OptReprocesarDocumentosFiniquito { get; set; }
        public string OptValidarFinanzas { get; set; }
        public string OptRevertirValidacionFinanzas { get; set; }

    }
}
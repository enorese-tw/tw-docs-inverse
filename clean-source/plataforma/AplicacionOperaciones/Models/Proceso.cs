using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class Proceso
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string TipoEvento { get; set; }
        public string CodigoTransaccion { get; set; }
        public string NombreProceso { get; set; }
        public string Creado { get; set; }
        public string EjecutivoCreador { get; set; }
        public string TotalSolicitudes { get; set; }
        public string Comentarios { get; set; }
        public string CodificarCod { get; set; }
        public string Prioridad { get; set; }
        public string Glyphicon { get; set; }
        public string GlyphiconColor { get; set; }
        public string BorderColor { get; set; }
        public string OptDescargarDatosCargados { get; set; }
        public string OptAsignarProceso { get; set; }
        public string OptDescargarSolicitudContrato { get; set; }
        public string OptHistorialProceso { get; set; }
        public string OptRevertirAnulacion { get; set; }
        public string OptDescargarErrorDatosCargados { get; set; }
        public string OptAnularProceso { get; set; }
        public string OptTerminarProceso { get; set; }
        public string OptRevertirTermino { get; set; }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class Solicitud
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string TipoEvento { get; set; }
        public string CodigoSolicitud { get; set; }
        public string NombreSolicitud { get; set; }
        public string NombreProceso { get; set; }
        public string Creado { get; set; }
        public string CreadoPor { get; set; }
        public string AsignadoA { get; set; }
        public string PathSolicitud { get; set; }
        public string PathProceso { get; set; }
        public string FechasCompromiso { get; set; }
        public string Comentarios { get; set; }
        public string Prioridad { get; set; }
        public string Glyphicon { get; set; }
        public string GlyphiconColor { get; set; }
        public string BorderColor { get; set; }
        public string ColorFont { get; set; }
        public string Estado { get; set; }

        public string OptDescargarDatosCargados { get; set; }
        public string OptAsignarSolicitud { get; set; }
        public string OptDescargarSolicitudContratoIndividual { get; set; }
        public string OptHistorialSolicitud { get; set; }
        public string OptRevertirAnulacion { get; set; }
        public string OptDescargarErrorDatosCargados { get; set; }
        public string OptAnularSolicitud { get; set; }
        public string OptRevertirTermino { get; set; }
    }
}
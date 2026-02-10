using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class Proceso
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string NombreProceso { get; set; }
        public string CodigoProceso { get; set; }
        public string Creado { get; set; }
        public string CreadoPor { get; set; }
        public string AsignadoA { get; set; }
        public string PathProceso { get; set; }
        public string TotalesSolicitud { get; set; }
        public string Comentarios { get; set; }
        public string Prioridad { get; set; }
        public string Glyphicon { get; set; }
        public string GlyphiconColor { get; set; }
        public string BorderColor { get; set; }
        public string ColorFont { get; set; }
        public string Estado { get; set; }

        public string PathExcelSolicitud { get; set; }

    }
}
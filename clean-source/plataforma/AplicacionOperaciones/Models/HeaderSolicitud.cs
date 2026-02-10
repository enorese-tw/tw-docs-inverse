using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class HeaderSolicitud
    {
        public string PostTitulo { get; set; }
        public string HtmlRenderizado { get; set; }
        public string HtmlPagination { get; set; }
        public string HtmlSearchElement { get; set; }
        public string TipoSolicitud { get; set; }
        public string NombreProceso { get; set; }
        public string ResultSearch { get; set; }
        public string Creado { get; set; }
        public string Estado { get; set; }
        public string Glyphicon { get; set; }
        public string GlyphiconColor { get; set; }
        public string EjecutivoKam { get; set; }
        public string EjecutivoCreador { get; set; }
        public string Cliente { get; set; }
    }
}
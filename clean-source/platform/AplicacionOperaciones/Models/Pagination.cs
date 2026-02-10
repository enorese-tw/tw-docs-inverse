using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class Pagination
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Codificar { get; set; }
        public string NumeroPagina { get; set; }
        public string FirstItem { get; set; }
        public string PreviousItem { get; set; }
        public string NextItem { get; set; }
        public string LastItem { get; set; }
        public string TotalItems { get; set; }
        public string Active { get; set; }

        public string TipoSolicitud { get; set; }
        public string HtmlRenderizado { get; set; }
        public string HtmlPagination { get; set; }

        public string HtmlSearchElement { get; set; }

        public string HtmlEventAction { get; set; }
    }
}
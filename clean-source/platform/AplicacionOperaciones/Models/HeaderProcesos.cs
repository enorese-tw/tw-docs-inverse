using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class HeaderProcesos
    {
        public string PostTitulo { get; set; }
        public string HtmlRenderizado { get; set; }
        public string HtmlPagination { get; set; }
        public string HtmlSearchElement { get; set; }
        public string TipoSolicitud { get; set; }
        public string ResultSearch { get; set; }
    }
}
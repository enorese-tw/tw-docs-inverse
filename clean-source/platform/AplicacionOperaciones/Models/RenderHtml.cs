using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class RenderHtml
    {
        public string EtiquetaApertura { get; set; }
        public string EtiquetaCierre { get; set; }
        public string TextoGenerado { get; set; }
        public string HasReferencia { get; set; }
        public string Referencia { get; set; }
        public PlantillasCargaMasiva PlantillaCargaMasiva { get; set; }
    }
}
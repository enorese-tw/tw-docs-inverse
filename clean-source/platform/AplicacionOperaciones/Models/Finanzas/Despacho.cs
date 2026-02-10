using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class Despacho
    {
        public string CodigoDespacho { get; set; }
        public string CodigoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string Contenido { get; set; }
        public string Beneficiario { get; set; }
        public string Monto { get; set; }
        public string Folio { get; set; }
    }
}
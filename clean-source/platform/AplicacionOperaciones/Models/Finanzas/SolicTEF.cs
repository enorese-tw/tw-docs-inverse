using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class SolicTEF
    {
        public string NombreSolicitud { get; set; }
        public string Creado { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string CuentaDestino { get; set; }
        public string CuentaOrigen { get; set; }
        public string MontoTotal { get; set; }
        public string Finiquito { get; set; }
        public string Color { get; set; }
        public string CodigoTEF { get; set; }
        public string BorderColor { get; set; }
        public string OptPagar { get; set; }
        public string OptNotariar { get; set; }
        public string OptIncluir { get; set; }
        public string OptExcluir { get; set; }
    }
}
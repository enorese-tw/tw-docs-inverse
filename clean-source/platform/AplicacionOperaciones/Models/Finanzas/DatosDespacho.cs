using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class DatosDespacho
    {
        public string CodigoDespacho { get; set; }
        public string FechaApertura { get; set; }
        public string HoraApertura { get; set; }
        public string FechaCierre { get; set; }
        public string HoraCierre { get; set; }
        public string Cantidad { get; set; }
        public string Base64Barcode { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
    }
}
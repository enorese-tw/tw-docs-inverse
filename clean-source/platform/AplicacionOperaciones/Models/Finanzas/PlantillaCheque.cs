using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class PlantillaCheque
    {
        public string Empresa { get; set; }
        public string Comprobante { get; set; }
        public string Base64Barcode { get; set; }
        public string Monto { get; set; }
        public string Ciudad { get; set; }
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Year { get; set; }
        public string Beneficiario { get; set; }
        public string CifraFirst { get; set; }
        public string CifraSecond { get; set; }
        public string Protegido { get; set; }
    }
}
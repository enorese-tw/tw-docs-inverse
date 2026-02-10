using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Finanzas
{
    public class Provision
    {
        public string Concepto { get; set; }
        public string Percentage { get; set; }
        public string MontoCLP { get; set; }
        public string MontoCLPCifra { get; set; }
        public string CodigoVariable { get; set; }
        public string OptWithExcluir { get; set; }
        public string CodigoCargoMod { get; set; }
    }
}
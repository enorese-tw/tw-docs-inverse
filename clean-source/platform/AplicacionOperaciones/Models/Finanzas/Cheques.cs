using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class Cheques
    {
        public string Folio { get; set; }
        public string Rut { get; set; }
        public string Nombres { get; set; }
        public string Empresa { get; set; }
        public string Monto { get; set; }
        public string CodigoPago { get; set; }
        public string Proceso { get; set; }
        public string Documento { get; set; }
        public string Origen { get; set; }
    }
}
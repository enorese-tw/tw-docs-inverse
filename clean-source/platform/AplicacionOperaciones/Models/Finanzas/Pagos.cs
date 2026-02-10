using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class Pagos
    {
        public string Destintario { get; set; }
        public string Monto { get; set; }
        public string FechaTransaccion { get; set; }
        public string Empresa { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string Cifra { get; set; }
        public string CodigoPago { get; set; }
        public string Rut { get; set; }

    }
}
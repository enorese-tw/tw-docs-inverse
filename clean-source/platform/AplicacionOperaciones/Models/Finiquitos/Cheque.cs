using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finiquitos
{
    public class Cheque
    {
        public string Nombres { get; set; }
        public string Rut { get; set; }
        public string MontoTotal { get; set; }
        public string CodigoPago { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Recepcion
{
    public class Recepcionar
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string CodigoDespacho { get; set; }
        public string Recepcionados { get; set; }
        public string Despachados { get; set; }
        public string Total { get; set; }
    }
}
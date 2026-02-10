using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finiquitos
{
    public class Dashboard
    {
        public string PendientesVerificacion { get; set; }
        public string PendientesConfirmacion { get; set; }
        public string PendientesConfirmacionCmp { get; set; }

        public string Estado { get; set; }
        public string Descripcion { get; set; }
        public string Cantidad { get; set; }
        public string Color { get; set; }
    }
}
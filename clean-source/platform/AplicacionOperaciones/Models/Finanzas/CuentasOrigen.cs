using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class CuentasOrigen
    {
        public string Banco { get; set; }
        public string NumeroCuenta { get; set; }
        public string GlosaCuenta { get; set; }
        public string RutCuenta { get; set; }
        public string FechaCreacion { get; set; }
        public string Vigente { get; set; }
    }
}
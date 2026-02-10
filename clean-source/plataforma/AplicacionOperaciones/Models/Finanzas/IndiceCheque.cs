using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class IndiceCheque
    {
        public string EmpresaOrigen { get; set; }
        public string Disponibles { get; set; }
        public string Emitidos { get; set; }
        public string Totales { get; set; }
    }
}
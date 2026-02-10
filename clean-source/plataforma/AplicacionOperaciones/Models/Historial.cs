using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class Historial
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Estado { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
    }
}
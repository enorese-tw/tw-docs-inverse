using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class Vacacion
    {
        public string Ficha { get; set; }
        public string FechaEmision { get; set; }
        public string FaDesdeReal { get; set; }
        public string FaHastaReal { get; set; }
        public string FaDesde { get; set; }
        public string NDiasApSof { get; set; }
        public string NDiasAp { get; set; }
        public string DiaVac { get; set; }
        public string Class { get; set; }
        public string CodigoAsistencia { get; set; }
    }
}
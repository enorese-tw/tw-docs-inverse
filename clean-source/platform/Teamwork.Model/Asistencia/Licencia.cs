using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class Licencia
    {
        public string Ficha { get; set; }
        public string FechaEmision { get; set; }
        public string FechaInitRepReal { get; set; }
        public string FechaTerminoReal { get; set; }
        public string FechaIniRep { get; set; }
        public string NroDiasLicSof { get; set; }
        public string NroDiasLic { get; set; }
        public string FechaLic { get; set; }
        public string DiaLic { get; set; }
        public string Class { get; set; }
        public string CodigoAsistencia { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class Periodo
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
        public string Empresa { get; set; }
        public string PeriodoVigente { get; set; }
        public string FechaApertura { get; set; }
        public string FechaCierre { get; set; }
        public string Vigente { get; set; }
        public string EmpresaCodificado { get; set; }
    }
}
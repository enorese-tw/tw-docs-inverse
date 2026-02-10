using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class CierrePeriodo
    {
        public string IdCierre { get; set; }
        public string Empresa { get; set; }
        public string AreaNegocio { get; set; }
        public string FechaCierre { get; set; }
        public string UsuarioCierre { get; set; }
        public string FechaCreacion { get; set; }
        public string Cerrado { get; set; }
        public string Excepcion { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
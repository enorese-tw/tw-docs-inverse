using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class Asistencia
    {
        public string Empresa { get; set; }
        public string Ficha { get; set; }
        public string Fecha { get; set; }
        public string CodigoAsistencia { get; set; }
        public string Observacion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCarga { get; set; }
        public string FechaUltModificacion { get; set; }
        public string UsuarioUltModificacion { get; set; }
        public string Vigente { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Class { get; set; }
        public string Color { get; set; }
        public string Style { get; set; }
    }
}
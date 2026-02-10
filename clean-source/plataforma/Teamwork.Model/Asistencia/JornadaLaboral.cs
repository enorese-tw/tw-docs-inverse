using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class JornadaLaboral
    {
        public string CodigoJornada { get; set; }
        public string Empresa { get; set; }
        public string AreaNegocio { get; set; }
        public string NombreJornada { get; set; }
        public string DescripcionJornada { get; set; }
        public string HorasSemanal { get; set; }
        public string Vigente { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModificacion { get; set; }
        public string UsuarioUltModificacion { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string PorcentajePago { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class FichaJornadaLaboral
    {
        public string CodigoFichaJornada { get; set; }
        public string CodigoJornada { get; set; }
        public string Ficha { get; set; }
        public string JornadaActiva { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltModificacion { get; set; }
        public string UsuarioUltModificacion { get; set; }
        public string NombreJornada { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
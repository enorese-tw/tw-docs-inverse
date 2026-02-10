using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class RutaFicheros
    {
        public string CodigoRuta { get; set; }
        public string RutaFichero { get; set; }
        public string NombreRuta { get; set; }
        public string Estado { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreador { get; set; }
        public string UsuarioUltActualizacion { get; set; }
        public string FechaUltActualizacion { get; set; }
        public string UltimoComentario { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
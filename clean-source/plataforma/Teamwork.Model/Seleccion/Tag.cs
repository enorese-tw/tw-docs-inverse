using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Seleccion
{
    public class Tag
    {
        public string CodigoTag { get; set; }
        public string Descripcion { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string FechaUltimaActualizacion { get; set; }
        public string UsuarioUltimaActualizacion { get; set; }
        public string Estado { get; set; }
        public string FechaVigenciaDesde { get; set; }
        public string FechaVigenciaHasta { get; set; }
        public string OptEditar { get; set; }
        public string OptActivar { get; set; }
        public string OptDesactivar { get; set; }
        public string OptEliminar { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class SolicitudRenovacion
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string NombreSolicitud { get; set; }
        public string Comentarios { get; set; }
        public string Glyphicon { get; set; }
        public string GlyphiconColor { get; set; }
        public string ColorFont { get; set; }
        public string NombreProceso { get; set; }
        public string Creado { get; set; }
        public string EjecutivoCarga { get; set; }
        public string Ficha { get; set; }
        public string Rut { get; set; }
        public string NombreCompleto { get; set; }
        public string CargoMod { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Causal { get; set; }
        public string FechaInicioRenov { get; set; }
        public string FechaTerminoRenov { get; set; }
        public string Empresa { get; set; }
    }
}
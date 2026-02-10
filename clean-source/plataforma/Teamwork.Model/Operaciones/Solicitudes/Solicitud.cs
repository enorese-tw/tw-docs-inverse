using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones.Solicitudes
{
    public class Solicitud
    {
        public string NombreSolicitud { get; set; }
        public string FechaRealInicio { get; set; }
        public string FechaRealTermino { get; set; }
        public string Creado { get; set; }
        public string Prioridad { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string Comentarios { get; set; }
        public string CodigoSolicitud { get; set; }
        public string NombreProceso { get; set; }
    }
}
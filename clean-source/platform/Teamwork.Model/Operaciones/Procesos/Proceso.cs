using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones.Procesos
{
    public class Proceso
    {
        public string NombreProceso { get; set; }
        public string Creado { get; set; }
        public string CreadoPor { get; set; }
        public string EjecutivoName { get; set; }
        public string AsignadoTo { get; set; }
        public string Comentarios { get; set; }
        public string CodigoTransaccion { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string Prioridad { get; set; }
    }
}
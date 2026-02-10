using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Asistencia
{
    public class BajasConfirmadas
    {
        public string Id { get; set; }
        public string Ficha { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string Empresa { get; set; }
        public string Estado { get; set; }
        public string Code { get; set; }
        public string Message { get; set; }
        public string Class { get; set; }
        public string Color { get; set; }
        public string DiaBaja { get; set; }
    }
}
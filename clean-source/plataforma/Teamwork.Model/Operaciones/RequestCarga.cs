using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class RequestCarga
    {
        public string Code { get; set; }
        public string Titulo { get; set; }
        public string Message { get; set; }
        public string Procesados { get; set; }
        public string Errores { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Operaciones
{
    public class ResponseCarga
    {
        public string NombrePlantilla { get; set; }
        public string NombreHojaCargaMasiva { get; set; }
        public string Columnas { get; set; }
        public string GetValueError { get; set; }
    }
}
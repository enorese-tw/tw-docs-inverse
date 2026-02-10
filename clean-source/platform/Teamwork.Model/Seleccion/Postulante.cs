using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Seleccion
{
    public class Postulante
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Comuna { get; set; }
        public string Correo { get; set; }
    }
}
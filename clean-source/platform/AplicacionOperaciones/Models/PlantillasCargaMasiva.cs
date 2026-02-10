using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class PlantillasCargaMasiva
    {
        public string Id { get; set; }
        public string NombrePlantilla { get; set; }
        public string NombreHojaCargaMasiva { get; set; }
        public string RenderizadoOneTexto { get; set; }
        public string RenderizadoSecTexto { get; set; }
        public string RenderizadoMensajeImpt { get; set; }
        public string RenderizadoGlyphicon { get; set; }
        public string RenderizadoColor { get; set; }
        public string Tipo { get; set; }
        public string NodoPadre { get; set; }
        public string NodoHijo { get; set; }
        public string Columnas { get; set; }
    }
}
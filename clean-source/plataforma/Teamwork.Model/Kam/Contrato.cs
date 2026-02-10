using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Kam
{
    public class Contrato
    {
        public string Ficha { get; set; }
        public string Empresa { get; set; }
        public string Cliente { get; set; }
        public string CargoMod { get; set; }
        public string CodigoSucursal { get; set; }
        public string NombreSucursal { get; set; }
        public string DireccionSucursal { get; set; }
        public string ComunaSucursal { get; set; }
        public string CiudadSucursal { get; set; }
        public string RegionSucursal { get; set; }
        public string Causal { get; set; }
        public string TopeLegal { get; set; }
        public string FechaInicioContrato { get; set; }
        public string FechaTerminoContrato { get; set; }
        public string HorasSemanales { get; set; }
        public string Horarios { get; set; }
        public string Colacion { get; set; }
        public string TipoReemplazo { get; set; }
        public string Reemplazo { get; set; }
        public string TipoMovimiento { get; set; }
        public string NombreCargo { get; set; }
        public string Tag { get; set; }
    }
}
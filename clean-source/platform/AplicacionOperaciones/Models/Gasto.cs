using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class Gasto
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string IdGasto { get; set; }
        public string CantidadGastos { get; set; }
        public string Codigo { get; set; }
        public string Token { get; set; }
        public string EstadoGasto { get; set; }
        public string NombreUsuario { get; set; }
        public string Cliente { get; set; }
        public string Empresa { get; set; }
        public string IdEmpresa { get; set; }
        public string ConceptoGasto { get; set; }
        public string IdTipoDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Aprobador { get; set; }
        public string MontoNeto { get; set; }
        public string MontoNetoIVA { get; set; }
        public string MontoAprobado { get; set; }
        public string MontoAprobadoIVA { get; set; }
        public string TipoReembolso { get; set; }
        public string FechaRendicion { get; set; }
        public string FechaPublicacion { get; set; }
        public string FechaAprobacion { get; set; }
        public string FechaUltimoCambio { get; set; }
        public string Descripcion { get; set; }
        public string Borrado { get; set; }
        public string NombreBanco { get; set; }
        public string NombreCuenta { get; set; }
        public string NumeroCuenta { get; set; }
        public string Periodo { get; set; }
        public string Proveedor { get; set; }
        public string IdProveedor { get; set; }
        public string OptBorrar { get; set; }
        public string OptModificar { get; set; }

        public string RutProveedor { get; set; }

    }
}
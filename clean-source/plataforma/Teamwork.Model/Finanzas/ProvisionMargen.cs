using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Finanzas
{
    public class ProvisionMargen
    {
        public string CodigoVariable { get; set; }
        public string Descripcion { get; set; }
        public string OptCrear { get; set; }
        public string OptEliminar { get; set; }
        public string OptDesactivar { get; set; }
        public string OptEditar { get; set; }
        public string OptActivar { get; set; }
        public string AtributosHTML { get; set; }
        public string Monto { get; set; }
        public string Message { get; set; }
        public ConstCalculoProvMargen Constante { get; set; }
        public ConstCalculoProvMargen ConstanteTope { get; set; }
        public string TypeInput { get; set; }
        public string OptCrearAsignConstante { get; set; }
        public string OptEliminarAsignConstante { get; set; }
        public string OptActualizarAsignConstante { get; set; }
        public string OptCrearAsignConstanteTope { get; set; }
        public string OptEliminarAsignConstanteTope { get; set; }
        public string OptActualizarAsignConstanteTope { get; set; }
    }
}
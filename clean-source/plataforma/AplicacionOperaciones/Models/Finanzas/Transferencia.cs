using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models.Finanzas
{
    public class Transferencia
    {
        public string Folio { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public string TotalFiniquito { get; set; }
        public string GastoAdministrativo { get; set; }
        public string TotalPago { get; set; }
        public string CodigoTEF { get; set; }
        public string Proceso { get; set; }
        public string ColorNotariado { get; set; }
        public string Notariado { get; set; }
        public string OptRevertirIncorporacionTefPago { get; set; }
        public string OptNoNotariado { get; set; }
        public string OptNotariado { get; set; }
        public string Origen { get; set; }
        public string Documento { get; set; }
        public string Cuenta { get; set; }
        public string Banco { get; set; }
    }
}
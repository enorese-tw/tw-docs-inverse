using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class Renuncia
    {
        public int ID { get; set; }
        public string RUTTRABAJADOR { get; set; }
        public string EMPRESA { get; set; }
        public string NOMBRETRABAJADOR { get; set; }
        public string NEGOCIO { get; set; }
        public string ESTADO { get; set; }
        public string CAUSAL { get; set; }
        public string DESDE { get; set; }
        public string LEGALIZADA { get; set; }
        public string OBSERVACION { get; set; }
        public string REGISTRADA { get; set; }
        public string VISTONOTIFICACION { get; set; }
        public string KAM { get; set; }
        public string FECHAHORARECEPCION { get; set; }
    }
}
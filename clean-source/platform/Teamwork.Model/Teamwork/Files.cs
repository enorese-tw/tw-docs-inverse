using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Teamwork
{
    public class Files
    {
        public List<byte[]> ListFile { get; set; }
        public string Empresa { get; set; }
        public string Fecha { get; set; }
    }
}
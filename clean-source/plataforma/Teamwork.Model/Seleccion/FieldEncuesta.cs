using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Seleccion
{
    public class FieldEncuesta
    {
        public string Type { get; set; }
        public string Placeholder { get; set; }
        public string Name { get; set; }
        public string Required { get; set; }
        public string HiddenUser { get; set; }
    }
}
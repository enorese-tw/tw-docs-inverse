using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Bajas
{
    public class LiquidacionSueldo
    {
        public string Years { get; set; }
        public List<LiquidacionSueldoFile> LiquidacionesSueldo { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.Model.Finanzas
{
    public class EstructuraCargo
    {
        public string NombreCargo { get; set; }
        public string NombreCargoMod { get; set; }
        public string Cliente { get; set; }
        public string TípoContrato { get; set; }
        public string TopeUFImponible { get; set; }
        public string TopeCLPImponible { get; set; }
        public string TopeUFImponibleAFC { get; set; }
        public string TopeCLPImponibleAFC { get; set; }
        public string SueldoBase { get; set; }
        public string SueldoLiquido { get; set; }
        public string Gratificacion { get; set; }
        public string BaseImponible { get; set; }
        public string BaseImponibleAFC { get; set; }
        public string TotalImponible { get; set; }
        public string TotalTributable { get; set; }
        public string TotalHaberes { get; set; }
        
        public string SueldoBaseCifra { get; set; }
        public string SueldoLiquidoCifra { get; set; }
        public string GratificacionCifra { get; set; }
        public string BaseImponibleCifra { get; set; }
        public string BaseImponibleAFCCifra { get; set; }
        public string TotalImponibleCifra { get; set; }
        public string TotalTributableCifra { get; set; }
        public string TotalHaberesCifra { get; set; }

        public string PorcAFP { get; set; }
        public string AFP { get; set; }
        public string CLPAFP { get; set; }
        public string PorcFondoPensiones { get; set; }
        public string PorcSegInvalidez { get; set; }
        public string PorcSalud { get; set; }
        public string CLPSalud { get; set; }
        public string CLPImpuestoUnico { get; set; }
        public string PorcSeguroDesempleo { get; set; }
        public string CLPSeguroDesempleo { get; set; }
        public string TotalDescuentos { get; set; }

        public string TypeSueldoInputActual { get; set; }
        public string TypeSueldoInputChange { get; set; }
        public string TypeCalculoSueldo { get; set; }
        public string TypeJornada { get; set; }
        public string EnlaceEstructura { get; set; }
        public string CodigoCargoMod { get; set; }
        
        public string GratificacionPactada { get; set; }
        public string GratifCC { get; set; }
        public string MessageSueldoBase { get; set; }
        public string MessageGratif { get; set; }
        public string MessageCargoMod { get; set; }
        public string MessageTypeJornada { get; set; }
        public string MessageTypeCalculo { get; set; }
        public string MaxLengthCargoMod { get; set; }
        public string RestLengthCargoMod { get; set; }
        public string GratifSugerida { get; set; }

        public string NumeroHorasSemanalesPT { get; set; }
        public string NumeroHorasSemanales { get; set; }
        public string TypeCalculoPT { get; set; }
        public string ValorSueldoPT { get; set; }
        public string Horarios { get; set; }
        public string Observaciones { get; set; }
        public string Observaciones2 { get; set; }
        public string Origen { get; set; }
        public string NumeroDias { get; set; }

        public string MessageSugGratif { get; set; }
        public string RazonSocial { get; set; }

        public List<Operaciones.ANIsCargoMod> AsignacionesNOImponibles { get; set; }
        public List<Operaciones.BonosCargoMod> Bonos { get; set; }
        
    }
}
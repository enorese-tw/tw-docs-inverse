using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionOperaciones.Models
{
    public class EstadisticaKamEmpresa
    {
        public string Code { get; set; }
        public string Dashboard { get; set; }
        public string TipoDashboard { get; set; }
        public string Concepto { get; set; }
        public string TotalSolicitudes { get; set; }
        public string TotalSolContratos { get; set; }
        public string PercentageSolContratos { get; set; }
        public string TotalSolRenovaciones { get; set; }
        public string PercentageSolRenovaciones { get; set; }
        public string TotalProcesos { get; set; }
        public string TotalProcContratos { get; set; }
        public string PercentageProcContratos { get; set; }
        public string TotalProcRenovaciones { get; set; }
        public string PercentageProcRenovaciones { get; set; }

        /** para estados de solicitudes */

        public string EmpresaPCSSolContratos { get; set; }
        public string EmpresaPCSSolRenovaciones { get; set; }
        public string EmpresaPCSSolContratosPercentage { get; set; }
        public string EmpresaPCSSolRenovacionesPercentage { get; set; }
        public string EmpresaPCSSolGlyphicon { get; set; }
        public string EmpresaPCSSolGlyphiconColor { get; set; }
        public string EmpresaPCSSolBarColor { get; set; }
        public string EmpresaPCSSolConcepto { get; set; }

        public string EmpresaCRITICOSolContratos { get; set; }
        public string EmpresaCRITICOSolRenovaciones { get; set; }
        public string EmpresaCRITICOSolContratosPercentage { get; set; }
        public string EmpresaCRITICOSolRenovacionesPercentage { get; set; }
        public string EmpresaCRITICOSolGlyphicon { get; set; }
        public string EmpresaCRITICOSolGlyphiconColor { get; set; }
        public string EmpresaCRITICOSolBarColor { get; set; }
        public string EmpresaCRITICOSolConcepto { get; set; }

        public string EmpresaURGENTESolContratos { get; set; }
        public string EmpresaURGENTESolRenovaciones { get; set; }
        public string EmpresaURGENTESolContratosPercentage { get; set; }
        public string EmpresaURGENTESolRenovacionesPercentage { get; set; }
        public string EmpresaURGENTESolGlyphicon { get; set; }
        public string EmpresaURGENTESolGlyphiconColor { get; set; }
        public string EmpresaURGENTESolBarColor { get; set; }
        public string EmpresaURGENTESolConcepto { get; set; }

        public string EmpresaNORMALSolContratos { get; set; }
        public string EmpresaNORMALSolRenovaciones { get; set; }
        public string EmpresaNORMALSolContratosPercentage { get; set; }
        public string EmpresaNORMALSolRenovacionesPercentage { get; set; }
        public string EmpresaNORMALSolGlyphicon { get; set; }
        public string EmpresaNORMALSolGlyphiconColor { get; set; }
        public string EmpresaNORMALSolBarColor { get; set; }
        public string EmpresaNORMALSolConcepto { get; set; }

        public string EmpresaSPSolContratos { get; set; }
        public string EmpresaSPSolRenovaciones { get; set; }
        public string EmpresaSPSolContratosPercentage { get; set; }
        public string EmpresaSPSolRenovacionesPercentage { get; set; }
        public string EmpresaSPSolGlyphicon { get; set; }
        public string EmpresaSPSolGlyphiconColor { get; set; }
        public string EmpresaSPSolBarColor { get; set; }
        public string EmpresaSPSolConcepto { get; set; }

        public string EmpresaANULADASolContratos { get; set; }
        public string EmpresaANULADASolRenovaciones { get; set; }
        public string EmpresaANULADASolContratosPercentage { get; set; }
        public string EmpresaANULADASolRenovacionesPercentage { get; set; }
        public string EmpresaANULADASolGlyphicon { get; set; }
        public string EmpresaANULADASolGlyphiconColor { get; set; }
        public string EmpresaANULADASolBarColor { get; set; }
        public string EmpresaANULADASolConcepto { get; set; }

        public string EmpresaERRORSolContratos { get; set; }
        public string EmpresaERRORSolRenovaciones { get; set; }
        public string EmpresaERRORSolContratosPercentage { get; set; }
        public string EmpresaERRORSolRenovacionesPercentage { get; set; }
        public string EmpresaERRORSolGlyphicon { get; set; }
        public string EmpresaERRORSolGlyphiconColor { get; set; }
        public string EmpresaERRORSolBarColor { get; set; }
        public string EmpresaERRORSolConcepto { get; set; }

    }
}
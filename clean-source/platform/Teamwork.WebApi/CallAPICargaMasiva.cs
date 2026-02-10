using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPICargaMasiva
    {
        public static string __PlantillaListar(string cargaMasiva, string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "Listar",
                "{ CargaMasiva: '" + cargaMasiva + "', PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __PlantillaSubir(string cargaMasiva, string codigoTransaction, string usuarioCreador, string templateColumn, string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "Subir",
                "{ CargaMasiva: '" + cargaMasiva.Replace("'", @"""") + "', CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', TemplateColumn: '" + templateColumn + "', PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __PlantillaDownload(string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "Download",
                "{ PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __PlantillaReportError(string codigoTransaction, string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ReportError",
                "{ CodigoTransaction: '" + codigoTransaction + "', PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaContrato(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaContrato",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaRenovacion(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaRenovacion",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaBajas(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaBajas",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaActualizaCreaFicha(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ActualizaCreaFicha",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearContrato(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearContrato",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearRenovacion(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearRenovacion",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearBaja(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearBaja",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaEmitirSolicitud(string codigoTransaction, string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "EmitirSolicitud",
                "{ CodigoTransaction: '" + codigoTransaction + "', PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __ProcesoListarContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaProceso(
                "ListarContratos",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __ProcesoListarRenovaciones(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaSolicitud(
                "ListarRenovaciones",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __SolicitudListarContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaSolicitud(
                "ListarContratos",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __SolicitudListarRenovaciones(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaSolicitud(
                "ListarRenovaciones",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }


        public static string __PaginationSolicitudContratos(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPagination(
                "SolicitudContratos",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __PaginationSolicitudRenovaciones(string codigoTransaction, string usuarioCreador, string filter, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPagination(
                "SolicitudRenovaciones",
                "{ CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __SolicitudAnularSolicitud(string usuarioCreador, string codigoSolicitud, string type, string observaciones, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaSolicitud(
                "AnularSolicitud",
                "{ UsuarioCreador: '" + usuarioCreador + "', CodigoSolicitud: '" + codigoSolicitud + "', Type: '" + type + "', Observacion: '" + observaciones + "' }",
                token
            );
        }
        
        public static string __PlantillaValidarCargaAsistencia(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaAsistencia",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __ProcesoModificarNombre(string codigoTransaction, string nombreProceso, string usuarioCreador, string type, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaProceso(
                "ModificarNombre",
                "{ CodigoTransaction: '" + codigoTransaction + "', NombreProceso: '" + nombreProceso + "', UsuarioCreador: '" + usuarioCreador + "', Type: '" + type + "' }",
                token
            );
        }
        
        public static string __PlantillaCrearAsistencia(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearAsistencia",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaHorasExtras(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaHorasExtras",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearHorasExtras(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearHorasExtras",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaDownloadDinamica(string plantillaMasiva, string cliente, string fecha, string empresa, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "DownloadDinamica",
                "{ PlantillaMasiva: '" + plantillaMasiva + "', Cliente: '" + cliente + "', Fecha: '" + fecha + "', Empresa: '" + empresa + "' }",
                token
            );
        }

        public static string __PlantillaSubirDinamica(string cargaMasiva, string codigoTransaction, string usuarioCreador, string templateColumn, string plantillaMasiva, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "SubirDinamica",
                "{ CargaMasiva: '" + cargaMasiva.Replace("'", @"""") + "', CodigoTransaction: '" + codigoTransaction + "', UsuarioCreador: '" + usuarioCreador + "', TemplateColumn: '" + templateColumn + "', PlantillaMasiva: '" + plantillaMasiva + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaAsistenciaRetail(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaAsistenciaRetail",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearAsistenciaRetail(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearAsistenciaRetail",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaAsistenciaRelojControlGeoVictoria(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaAsistenciaRelojControlGeoVictoria",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearAsistenciaRelojControlGeoVictoria(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearAsistenciaRelojControlGeoVictoria",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaAsistenciaRelojControl(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarCargaAsistenciaRelojControl",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearAsistenciaRelojControl(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearAsistenciaRelojControl",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaValidarCargaBono(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "ValidarBono",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __PlantillaCrearBono(string codigoTransaction, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaPlantilla(
                "CrearBono",
                "{ CodigoTransaction: '" + codigoTransaction + "' }",
                token
            );
        }

        public static string __ReporteRenovaciones(string cliente, string fechaInicioFilter, string FechaTerminoFilter, string empresa, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaReporte(
                "Renovaciones",
                "{ Cliente: '" + cliente + "', FechaInicioFilter: '" + fechaInicioFilter + "', FechaTerminoFilter: '" + FechaTerminoFilter + "', Empresa: '" + empresa + "' }",
                token
            );
        }

        public static string __ReporteContratos(string cliente, string fechaInicioFilter, string FechaTerminoFilter, string empresa, string token)
        {
            return HelperCallAPI.__CallAPICargaMasivaReporte(
                "Contratos",
                "{ Cliente: '" + cliente + "', FechaInicioFilter: '" + fechaInicioFilter + "', FechaTerminoFilter: '" + FechaTerminoFilter + "', Empresa: '" + empresa + "' }",
                token
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIFiniquitos
    {
        public static string __FiniquitosConsultaFiniquitos(string usuarioCreador, string pagination, string filter, string datafilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaFiniquitos",
                "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', Filter: '" + filter + "', DataFilter: '" + datafilter + "' }",
                token
            );
        }

        public static string __FiniquitosConsultaComplementos(string usuarioCreador, string pagination, string filter, string datafilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaComplementos",
                "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', Filter: '" + filter + "', DataFilter: '" + datafilter + "' }",
                token
            );
        }

        public static string __FiniquitosConsultaSimulaciones(string usuarioCreador, string pagination, string filter, string datafilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaSimulaciones",
                "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', Filter: '" + filter + "', DataFilter: '" + datafilter + "' }",
                token
            );
        }

        public static string __FiniquitosPaginationFiniquitos(string usuarioCreador, string pagination, string filter, string datafilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "PaginationFiniquitos",
                "{ UsuarioCreador: '" + usuarioCreador + "', Pagination: '" + pagination + "', Filter: '" + filter + "', DataFilter: '" + datafilter + "' }",
                token
            );
        }

        public static string __FiniquitosHistorialFiniquitos(string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "HistorialFiniquitos",
                "{ Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosAnularFiniquito(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "AnularFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosValidarFiniquito(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ValidarFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosGestionEnvioRegiones(string usuarioCreador, string codigo, string observacion, string envio, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionEnvioRegiones",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "', Envio: '" + envio + "' }",
                token
            );
        }

        public static string __FiniquitosGestionEnvioSantiagoNotaria(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionEnvioSantiagoNotaria",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosGestionEnvioSantiagoParaFirma(string usuarioCreador, string codigo, string observacion, string rolCoordinador, string coordinador, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionEnvioSantiagoParaFirma",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "', RolCoordinador: '" + rolCoordinador + "', Coordinador: '" + coordinador + "' }",
                token
            );
        }

        public static string __FiniquitosGestionEnvioLegalizacion(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionEnvioLegalizacion",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosGestionRecepcionRegiones(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionRecepcionRegiones",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosGestionRecepcionSantiagoNotaria(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionRecepcionSantiagoNotaria",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosGestionRecepcionSantiagoParaFirma(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionRecepcionSantiagoParaFirma",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosGestionRecepcionLegalizacion(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "GestionRecepcionLegalizacion",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosConsultaDatosTEF(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaDatosTEF",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosSolicitudTEF(string usuarioCreador, string codigo, string observacion, string banco, string numeroCta, string gastoAdm, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "SolicitudTEF",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion +"', Banco: '" + banco + "', NumeroCta: '" + numeroCta + "', GastoAdm: '" + gastoAdm + "' }",
                token
            );
        }

        public static string __FiniquitosSolicitudValeVista(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "SolicitudValeVista",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosSolicitudCheque(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "SolicitudCheque",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosConsultaSolicitudPago(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaSolicitudPago",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosConfirmarProcesoPago(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConfirmarProcesoPago",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosBancos(string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "Bancos",
                "{ }",
                token
            );
        }

        public static string __FiniquitosPagarFiniquito(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "PagarFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosDashboardFiniquitos(string usuarioCreador, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "DashboardFiniquitos",
                "{ UsuarioCreador: '" + usuarioCreador + "' }",
                token
            );
        }

        public static string __FiniquitosListarLiquidacionesSueldoYear(string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ListarLiquidacionesSueldoYear",
                "{ Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosListarLiquidacionesSueldoMes(string codigo, string filter, string dataFilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ListarLiquidacionesSueldoMes",
                "{ Codigo: '" + codigo + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "' }",
                token
            );
        }

        public static string __FiniquitosLiquidacionSueldoBase64(string codigo, string filter, string dataFilter, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "LiquidacionSueldoBase64",
                "{ Codigo: '" + codigo + "', Filter: '" + filter + "', DataFilter: '" + dataFilter + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoCrear(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoCrear",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoListarHaberes(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoListarHaberes",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoListarDescuento(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoListarDescuento",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoAgregarHaber(string usuarioCreador, string codigo, string monto, string descripcion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoAgregarHaber",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "', Monto: '" + monto + "', Descripcion: '" + descripcion + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoAgregarDescuento(string usuarioCreador, string codigo, string monto, string descripcion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoAgregarDescuento",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "', Monto: '" + monto + "', Descripcion: '" + descripcion + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoEliminarHaber(string usuarioCreador, string codigo, string variable, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoEliminarHaber",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "', Variable: '" + variable + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoEliminarDescuento(string usuarioCreador, string codigo, string variable, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoEliminarDescuento",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "', Variable: '" + variable + "' }",
                token
            );
        }

        public static string __FiniquitosComplementoDejarActivoCreado(string usuarioCreador, string codigo, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ComplementoDejarActivoCreado",
                "{ UsuarioCreador: '" + usuarioCreador + "',  Codigo: '" + codigo + "', Fecha: '" + fecha + "' }",
                token
            );
        }

        public static string __FiniquitosActualizarMontoAdministrativo(string usuarioCreador, string codigo, string observacion, string gastoAdm, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ActualizarMontoAdministrativo",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "', GastoAdm: '" + gastoAdm + "' }",
                token
            );
        }
        
        public static string __FiniquitosRevertirValidacion(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirValidacion",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirGestionEnvio(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirGestionEnvio",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirLegalizacion(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirLegalizacion",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirSolicitudPago(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirSolicitudPago",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirConfirmacion(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirConfirmacion",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirEmisionPago(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirEmisionPago",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

        public static string __FiniquitosConfigOpcionesMovimientoMasivos(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConfigOpcionesMovimientoMasivos",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosActualizarComentariosAnotaciones(string codigo, string html, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ActualizarComentariosAnotaciones",
                "{ Codigo: '" + codigo + "', Html: '" + html + "' }",
                token
            );
        }

        public static string __FiniquitosConsultaComentariosAnotaciones(string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ConsultaComentariosAnotaciones",
                "{ Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosTerminarFiniquito(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "TerminarFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosFirmarFiniquito(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "FirmarFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosReprocesarDocumentosFiniquito(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ReprocesarDocumentosFiniquito",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosValidarFinanzas(string usuarioCreador, string codigo, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "ValidarFinanzas",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "' }",
                token
            );
        }

        public static string __FiniquitosRevertirValidacionFinanzas(string usuarioCreador, string codigo, string observacion, string token)
        {
            return HelperCallAPI.__CallAPIFiniquitos(
                "RevertirValidacionFinanzas",
                "{ UsuarioCreador: '" + usuarioCreador + "', Codigo: '" + codigo + "', Observacion: '" + observacion + "' }",
                token
            );
        }

    }
}
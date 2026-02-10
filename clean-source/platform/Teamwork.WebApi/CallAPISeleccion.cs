using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPISeleccion
    {
        public static string __PostulanteValidateTokenInvitacion(string tokenInvitacion, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ValidateTokenInvitacion",
                "{ Token: '" + tokenInvitacion + "' }",
                token
            );
        }

        public static string __PostulanteCreaOActualizaFichaPostulante(string usuarioCreador, string nombres, string apellidoPaterno, string apellidoMaterno, string telefono, string fechaNacimiento,
                        string direccion, string comuna, string correo, string tokenInvitacion, string rut, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "CreaOActualizaFichaPostulante",
                "{ UsuarioCreador: '" + usuarioCreador + "', Nombres: '" + nombres + "', ApellidoPaterno: '" + apellidoPaterno + "', ApellidoMaterno: '" + apellidoMaterno + "', Telefono: '" + telefono + "', FechaNacimiento: '" + fechaNacimiento + "', " +
                " Direccion: '" + direccion + "', Comuna: '" + comuna + "', Correo: '" + correo + "', Token: '" + tokenInvitacion + "', DNI: '" + rut + "' }",
                token
            );
        }

        public static string __PostulanteValidaFichaPersonal(string DNI, string tipoDNI, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ValidaFichaPersonal",
                "{ DNI: '" + DNI + "', tipoDNI: '" + tipoDNI + "' }",
                token
            );
        }

        public static string __PostulanteConsultaFichaPersonal(string DNI, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ConsultaFichaPersonal",
                "{ DNI: '" + DNI + "' }",
                token
            );
        }

        public static string __PostulanteConsultaFieldEncuesta(string tokenInvitacion, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ConsultaFieldEncuesta",
                "{ Token: '" + tokenInvitacion + "' }",
                token
            );
        }

        public static string __PostulanteCrearTag(string descripcion, string usuarioCreador, string fechaDesde, string fechaHasta, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "CrearTag",
                "{ Descripcion: '" + descripcion + "', UsuarioCreador: '" + usuarioCreador + "', FechaVigenciaDesde: '" + fechaDesde + "', FechaVigenciaHasta: '" + fechaHasta + "' }",
                token
            );
        }

        public static string __PostulanteListarTag(string filterType, string dataFilter, string pagination, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ListarTag",
                "{ Filter: '" + filterType + "', DataFilter: '" + dataFilter + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __PostulanteActualizarTag(string tag, string action, string usuario, string descripcion, string status, string fechaDesde, string fechaHasta, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ActualizarTag",
                "{ Key: '" + tag + "', Accion: '" + action + "', UsuarioCreador: '" + usuario + "', Descripcion: '" + descripcion + "', Estado: '" + status + "', FechaVigenciaDesde: '" + fechaDesde + "', FechaVigenciaHasta: '" + fechaHasta + "' }",
                token
            );
        }

        public static string __PostulanteEliminarTag(string tag, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "EliminarTag",
                "{ Key: '" + tag + "' }",
                token
            );
        }

        public static string __ListarOfertasLaborales(string filterType, string dataFilter, string target, string usuario, string pagination, string type, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ListarPostulaciones",
                "{ Filter: '" + filterType + "', DataFilter: '" + dataFilter + "', Target: '" + target + "', UsuarioCreador: '" + usuario + "', Pagination: '" + pagination + "', Type: '" + type + "' }",
                token
            );
        }

        public static string __ListarTarget(string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ListarTarget",
                "{ }",
                token
            );
        }

        public static string __PostulanteActualizarOfertaLaboral(string solicitud, string descripcionLarga, string descripcionCorta, string fechaInicio, string fechaTermino, string target, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ActualizarOfertaLaboral",
                "{ ProcesoSeleccion: '" + solicitud + "' , DescripcionLarga:  '" + descripcionLarga + "' , DescripcionCorta:  '" + descripcionCorta + "' , FechaVigenciaDesde:  '" + fechaInicio + "' , FechaVigenciaHasta:  '" + fechaTermino + "' , Target: '" + target + "' }",
                token
            );
        }

        public static string __ListarTagOfertaLaboral(string idOfertaLaboral, string filterType, string dataFilter, string accion, string pagination, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "ListarTagOfertaLaboral",
                "{ ProcesoSeleccion: '" + idOfertaLaboral + "', Filter: '" + filterType + "', DataFilter: '" + dataFilter + "', Accion: '" + accion + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __CrearTagOfertaLaboral(string idOfertaLaboral, string descripcionTag, string categoria, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "CrearTagOfertaLaboral",
                "{ ProcesoSeleccion: '" + idOfertaLaboral + "', Descripcion: '" + descripcionTag + "', Categoria: '" + categoria + "' }",
                token
            );
        }

        public static string __EliminarTagOfertaLaboral(string idOfertaLaboral, string codigoTag, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "EliminarTagOfertaLaboral",
                "{ ProcesoSeleccion: '" + idOfertaLaboral + "', Key: '" + codigoTag + "' }",
                token
            );
        }

        public static string __Pagination(string pagination, string typePagination, string filterType, string dataFilter, string target, string idOfertaLaboral, string usuario, string token)
        {
            return HelperCallAPI.__CallAPISeleccionPostulante(
                "Pagination",
                "{ Pagination: '" + pagination + "', TypePagination: '" + typePagination + "', Filter: '" + filterType + "', DataFilter: '" + dataFilter + "', Target: '" + target + "', ProcesoSeleccion: '" + idOfertaLaboral + "', UsuarioCreador: '" + usuario + "' }",
                token
            );
        }

        public static string __CodigoOAuthCrear(string userId, string tokens, string token)
        {
            return HelperCallAPI.__CallAPISeleccionAuth(
                "CodigoOAuthCrear",
                "{ UserId: '" + userId + "', Token: '" + tokens + "' }",
                token
            );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Teamwork.WebApi
{
    public class CallAPIAsistencia
    {
        public static string __AsistenciaObtener(string empresa, string ficha, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "'  }",
                token
            );
        }

        public static string __AsistenciaInsertar(string empresa, string ficha, string asistencia, string observacion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Crear",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Asistencia: '" + asistencia + "', Observacion: '" + observacion + "', UsuarioCarga: '" + usuario + "'  }",
                token
            );
        }

        public static string __AsistenciaActualizar(string empresa, string ficha, string fecha, string tipo, string observacion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Actualizar",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', CodigoAsistencia: '" + tipo + "', Observacion: '" + observacion + "', UsuarioUltModificacion: '" + usuario + "'  }",
                token
            );
        }

        public static string __AsistenciaEliminar(string empresa, string ficha, string fecha, string tipo, string observacion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Eliminar",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', CodigoAsistencia: '" + tipo + "', Observacion: '" + observacion + "', UsuarioUltModificacion: '" + usuario + "'  }",
                token
            );
        }

        public static string __AsistenciaLicencia(string empresa, string ficha, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Licencia/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "'  }",
                token
            );
        }

        public static string __AsistenciaVacaciones(string empresa, string ficha, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Vacaciones/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "' }",
                token
            );
        }

        public static string __ObtenerBajaConfirmada(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "BajasConfirmadas/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "' }",
                token
            );
        }

        public static string __PersonalDataObtener(string empresa, string ficha, string fecha, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "PersonalData/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }


        public static string __TipoAsistenciaObtener(string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "TipoAsistencia/Obtener",
                "",
                token
            );
        }

        public static string __LeyendaObtener(string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Leyenda/Obtener",
                "",
                token
            );
        }

        public static string __AsistenciaReporte(string empresa, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Reporte",
                "{ Empresa: '" + empresa + "', Fecha: '" + fecha + "'  }",
                token
            );
        }


        //V2
        public static string __AreaNegocioObtener(string empresa, string usuario, string tipo, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "AreaNegocio/Obtener",
                "{ Empresa: '" + empresa + "', UsuarioCarga: '" + usuario + "', CodigoAsistencia: '" + tipo + "'  }",
                token
            );
        }

        public static string __PersonalDataObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2PersonalData/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "', Pagination: '" + pagination + "' }",
                token
            );
        }


        public static string __AsistenciaObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __AsistenciaLicenciaV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2Licencia/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __AsistenciaVacacionesV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2Vacaciones/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "', Pagination: '" + pagination + "' }",
                token
            );
        }

        public static string __AsistenciaInsertarV2(string empresa, string ficha, string asistencia, string observacion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2Crear",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Asistencia: '" + asistencia + "', Observacion: '" + observacion + "', UsuarioCarga: '" + usuario + "'  }",
                token
            );
        }

        public static string __AsistenciaReporteV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "V2Reporte",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaPagination(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string typePagination, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Pagination/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "', Pagination: '" + pagination + "', TypePagination: '" + typePagination + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerCierrePeriodo(string empresa, string fecha, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "CierrePeriodo/Obtener",
                "{ Empresa: '" + empresa + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "' }",
                token
            );
        }

        public static string __AsistenciaInsertarCierrePeriodo(string empresa, string fecha, string areaNegocio, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "CierrePeriodo/Crear",
                "{ Empresa: '" + empresa + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerPeriodos(string fechaDesde, string fechaHasta, string empresa, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                   "CierrePeriodo/ObtenerPeriodos",
                   "{ Fecha: '" + fechaDesde + "', FechaCreacion: '" + fechaHasta + "', Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "' }",
                   token
               );

        }

        public static string __AsistenciaInsertarExcepcionPeriodo(string empresa, string fecha, string areaNegocio, string excepion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ExcepcionPeriodo/Crear",
                "{ Empresa: '" + empresa + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', Excepion: '" + excepion + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }



        public static string __AsistenciaObtenerArchivosAsistencia(string empresa, string ficha, string areaNegocio, string fecha, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ArchivosAsistencia/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', AreaNegocio: '" + areaNegocio + "', Fecha: '" + fecha + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerNombreArchivo(string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ArchivosAsistencia/NombreArchivo/Obtener",
                "",
                token
            );
        }

        public static string __AsistenciaObtenerRutaFichero(string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ArchivosAsistencia/RutaFichero/Obtener",
                "",
                token
            );
        }

        public static string __AsistenciaInsertarArchivosAsistencia(string empresa, string ficha, string areaNegocio, string fecha, string usuario, string codigoRuta, string nombreArchivo, string nombreRealArchivo, string extension, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ArchivosAsistencia/Crear",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', AreaNegocio: '" + areaNegocio + "', Fecha: '" + fecha + "', UsuarioCarga: '" + usuario + "', CodigoRuta: '" + codigoRuta + "', NombreArchivo: '" + nombreArchivo + "', NombreRealArchivo: '" + nombreRealArchivo + "', Extension: '" + extension + "' }",
                token
            );
        }

        public static string __AsistenciaEliminarArchivoAsistencia(string codigoRuta, string codigoArchivo, string nombreReal, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ArchivosAsistencia/Eliminar",
                "{ CodigoRuta: '" + codigoRuta + "', CodigoArchivo: '" + codigoArchivo + "', NombreRealArchivo: '" + nombreReal + "' }",
                token
            );
        }


        public static string __AsistenciaObtenerJornadaLaboral(string empresa, string areaNegocio, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "JornadaLaboral/Obtener",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaActualizarJornadaLaboral(string codigoJornada, string vigente, string nombreJornada, string descripcionJornada, string horasSemanales, string porcentaje, string action, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "JornadaLaboral/Actualizar",
                "{ CodigoJornada: '" + codigoJornada + "', Vigente: '" + vigente + "', NombreJornada: '" + nombreJornada + "', NombreJornada: '" + nombreJornada + "' , DescripcionJornada: '" + descripcionJornada + "' , HorasSemanal: '" + horasSemanales + "', PorcentajePago: '" + porcentaje + "', UsuarioUltModificacion: '" + usuario + "', CodigoAsistencia: '" + action + "'  }",
                token
            );
        }

        public static string __AsistenciaIngresarJornadaLaboral(string empresa, string areaNegocio, string nombreJornada, string descripcionJornada, string horasSemanales, string porcentaje, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "JornadaLaboral/Ingresar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', NombreJornada: '" + nombreJornada + "' , DescripcionJornada: '" + descripcionJornada + "' , HorasSemanal: '" + horasSemanales + "', PorcentajePago: '" + porcentaje + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerFichaJornadaLaboral(string empresa, string areaNegocio, string ficha, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaJornada/Obtener",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaIngresarFichaJornadaLaboral(string empresa, string areaNegocio, string ficha, string codigoJornada, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaJornada/Ingresar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', UsuarioCarga: '" + usuario + "', CodigoJornada: '" + codigoJornada + "' }",
                token
            );
        }

        public static string __AsistenciaIngresarHoraExtra(string empresa, string ficha, string fecha, string areaNegocio, string codigoJornada, string horaExtra, string action, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "HorasExtra/Ingresar",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', CodigoJornada: '" + codigoJornada + "', HoraExtra: '" + horaExtra + "', UsuarioCarga: '" + usuario + "', CodigoAsistencia: '" + action + "' }",
                token
            );
        }

        public static string __HorasExtrasObtener(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "HorasExtra/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "' }",
                token
            );
        }

        public static string __AsistenciaReporteHorasExtras(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "HorasExtraReporte/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }


        public static string __AsistenciaReporteHorasColumnas(string empresa, string ficha, string fecha, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "ColumnaReporte/Obtener",
                "{ Empresa: '" + empresa + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', AreaNegocio: '" + areaNegocio + "' }",
                token
            );
        }


        public static string __AsistenciaObtenerBonos(string empresa, string areaNegocio, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Bonos/Obtener",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaIngresarBonos(string empresa, string areaNegocio, string bono, string descripcion, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Bonos/Ingresar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Bono: '" + bono + "' , Descripcion: '" + descripcion + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaActualizarBonos(string codigo, string vigente, string bono, string descripcion, string action, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Bonos/Actualizar",
                "{ Codigo: '" + codigo + "', Vigente: '" + vigente + "', Bono: '" + bono + "', Descripcion: '" + descripcion + "', UsuarioUltModificacion: '" + usuario + "', CodigoAsistencia: '" + action + "'  }",
                token
            );
        }

        public static string __AsistenciaEliminarBonos(string codigo, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "Bonos/Eliminar",
                "{ Codigo: '" + codigo + "', UsuarioUltModificacion: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerBonoCliente(string empresa, string areaNegocio, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "BonoCliente/Obtener",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "' }",
                token
            );
        }

        public static string __AsistenciaIngresarBonoCliente(string empresa, string areaNegocio, string codigo, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "BonoCliente/Ingresar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Codigo: '" + codigo + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaActualizarBonoCliente(string empresa, string areaNegocio, string codigo, string vigente, string action, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "BonoCliente/Actualizar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Codigo: '" + codigo + "', Vigente: '" + vigente + "', CodigoAsistencia: '" + action + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaEliminarBonoCliente(string empresa, string areaNegocio, string codigo, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "BonoCliente/Eliminar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Codigo: '" + codigo + "', UsuarioUltModificacion: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaObtenerFichaBonos(string empresa, string areaNegocio, string ficha, string fecha, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaBonos/Obtener",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaIngresarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaBonos/Ingresar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', Codigo: '" + codigo + "', Valor: '" + valor + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaActualizarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaBonos/Actualizar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', Codigo: '" + codigo + "', Valor: '" + valor + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }

        public static string __AsistenciaEliminarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string usuario, string token)
        {
            return HelperCallAPI.__CallAPIAsistencia(
                "FichaBonos/Eliminar",
                "{ Empresa: '" + empresa + "', AreaNegocio: '" + areaNegocio + "', Ficha: '" + ficha + "', Fecha: '" + fecha + "', Codigo: '" + codigo + "', UsuarioCarga: '" + usuario + "' }",
                token
            );
        }
    }
}
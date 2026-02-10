using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Asistencia;
using Teamwork.WebApi;

namespace AplicacionOperaciones.Collections
{
    public class CollectionsAsistencia
    {
        public static List<Asistencia> __AsistenciaObtener(string empresa, string ficha, string fecha, string token, string resource)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtener(empresa, ficha, fecha, token));

            for (var i = 0; i < objects.Count; i++)
            {
                asistencias.Add(
                    InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[i], resource)
                );
            }

            return asistencias;
        }

        public static List<Asistencia> __AsistenciaInsertar(string empresa, string ficha, string asistencia, string observacion, string usuario, string token, string resource)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaInsertar(empresa, ficha, asistencia, observacion, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                asistencias.Add(
                    InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[i], resource)
                );
            }

            return asistencias;

            //return asistencias = InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[0], resource); ;
        }

        public static Asistencia __AsistenciaActualizar(string empresa, string ficha, string fecha, string tipo, string observacion, string usuario, string token, string resource)
        {
            Asistencia asistencias = new Asistencia();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaActualizar(empresa, ficha, fecha, tipo, observacion, usuario, token));

            return asistencias = InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[0], resource); ;
        }


        public static Asistencia __AsistenciaEliminar(string empresa, string ficha, string fecha, string tipo, string observacion, string usuario, string token, string resource)
        {
            Asistencia asistencias = new Asistencia();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaEliminar(empresa, ficha, fecha, tipo, observacion, usuario, token));

            return asistencias = InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[0], resource);
        }

        public static Personal __PersonalDataObtener(string empresa, string ficha, string fecha, string usuario, string token, string resource)
        {
            Personal personal = new Personal();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__PersonalDataObtener(empresa, ficha, fecha, usuario, token));

            return personal = InstanceAsistencia.__CreateObjectInstancePersonal(objects[0], resource);
        }

        public static List<Licencia> __LicenciaObtener(string empresa, string ficha, string fecha, string token, string resource)
        {
            List<Licencia> licencia = new List<Licencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaLicencia(empresa, ficha, fecha, token));
            for (var i = 0; i < objects.Count; i++)
            {
                licencia.Add(InstanceAsistencia.__CreateObjectInstanceLicencia(objects[i], resource));
            }

            return licencia;
        }

        public static List<Vacacion> __VacacionObtener(string empresa, string ficha, string fecha, string token, string resource)
        {
            List<Vacacion> vacacion = new List<Vacacion>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaVacaciones(empresa, ficha, fecha, token));
            for (var i = 0; i < objects.Count; i++)
            {
                vacacion.Add(InstanceAsistencia.__CreateObjectInstanceVacacion(objects[i], resource));
            }

            return vacacion;
        }

        public static List<BajasConfirmadas> __ObtenerBajaConfirmada(string empresa, string ficha, string fecha, string areaNegocio, string token, string resource)
        {
            List<BajasConfirmadas> baja = new List<BajasConfirmadas>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__ObtenerBajaConfirmada(empresa, ficha, fecha, areaNegocio, token));
            for (var i = 0; i < objects.Count; i++)
            {
                baja.Add(InstanceAsistencia.__CreateObjectInstanceBajas(objects[i], resource));
            }

            return baja;
        }

        public static List<TipoAsistencia> __TipoAsistenciaObtener(string token, string resource)
        {
            List<TipoAsistencia> tipoAsistencia = new List<TipoAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__TipoAsistenciaObtener(token));
            for (var i = 0; i < objects.Count; i++)
            {
                tipoAsistencia.Add(InstanceAsistencia.__CreateObjectInstanceTipoAsistencia(objects[i], resource));
            }

            return tipoAsistencia;
        }

        public static List<Leyenda> __LeyendaObtener(string token, string resource)
        {
            List<Leyenda> leyenda = new List<Leyenda>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__LeyendaObtener(token));
            for (var i = 0; i < objects.Count; i++)
            {
                leyenda.Add(InstanceAsistencia.__CreateObjectInstanceLeyenda(objects[i], resource));
            }

            return leyenda;
        }

        public static List<ReporteAsistencia> __AsistenciaReporte(string empresa, string fecha, string token, string resource)
        {
            List<ReporteAsistencia> reporte = new List<ReporteAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaReporte(empresa, fecha, token));

            for (var i = 0; i < objects.Count; i++)
            {
                reporte.Add(InstanceAsistencia.__CreateObjectInstanceReporteAsistencia(objects[i], resource));
            }

            return reporte;
        }


        //V2
        public static List<AreaNegocio> __AreaNegocioObtener(string empresa, string usuario, string tipo, string token, string resource)
        {
            List<AreaNegocio> areaNegocio = new List<AreaNegocio>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AreaNegocioObtener(empresa, usuario, tipo, token));

            for (var i = 0; i < objects.Count; i++)
            {
                areaNegocio.Add(
                    InstanceAsistencia.__CreateObjectInstanceAreaNegocio(objects[i], resource)
                );
            }

            return areaNegocio;
        }

        public static List<Personal> __PersonalDataObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token, string resource)
        {
            List<Personal> personal = new List<Personal>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__PersonalDataObtenerV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token));

            for (var i = 0; i < objects.Count; i++)
            {
                personal.Add(InstanceAsistencia.__CreateObjectInstancePersonal(objects[i], resource));
            }

            return personal;
        }

        public static List<Asistencia> __AsistenciaObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token, string resource)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token));

            for (var i = 0; i < objects.Count; i++)
            {
                asistencias.Add(
                    InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[i], resource)
                );
            }

            return asistencias;
        }

        public static List<Licencia> __LicenciaObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token, string resource)
        {
            List<Licencia> licencia = new List<Licencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaLicenciaV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token));

            for (var i = 0; i < objects.Count; i++)
            {
                licencia.Add(InstanceAsistencia.__CreateObjectInstanceLicencia(objects[i], resource));
            }

            return licencia;
        }

        public static List<Vacacion> __VacacionObtenerV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string token, string resource)
        {
            List<Vacacion> vacacion = new List<Vacacion>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaVacacionesV2(empresa, ficha, fecha, areaNegocio, usuario, pagination, token));

            for (var i = 0; i < objects.Count; i++)
            {
                vacacion.Add(InstanceAsistencia.__CreateObjectInstanceVacacion(objects[i], resource));
            }

            return vacacion;
        }

        public static List<Asistencia> __AsistenciaInsertarV2(string empresa, string ficha, string asistencia, string observacion, string usuario, string token, string resource)
        {
            List<Asistencia> asistencias = new List<Asistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaInsertarV2(empresa, ficha, asistencia, observacion, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                asistencias.Add(
                    InstanceAsistencia.__CreateObjectInstanceAsistencia(objects[i], resource)
                );
            }

            return asistencias;
        }

        public static List<ReporteAsistencia> __AsistenciaReporteV2(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string token, string resource)
        {
            List<ReporteAsistencia> reporte = new List<ReporteAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaReporteV2(empresa, ficha, fecha, areaNegocio, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                reporte.Add(InstanceAsistencia.__CreateObjectInstanceReporteAsistencia(objects[i], resource));
            }

            return reporte;
        }

        public static List<Pagination> __AsistenciaPagination(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string pagination, string typePagination, string token, string resource)
        {
            List<Pagination> asistencias = new List<Pagination>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaPagination(empresa, ficha, fecha, areaNegocio, usuario, pagination, typePagination, token));

            for (var i = 0; i < objects.Count; i++)
            {
                asistencias.Add(
                    InstanceAsistencia.__CreateObjectInstancePagination(objects[i], resource)
                );
            }

            return asistencias;
        }

        public static List<CierrePeriodo> __AsistenciaObtenerCierrePeriodo(string empresa, string fecha, string areaNegocio, string token, string resource)
        {
            List<CierrePeriodo> cierrePeriodo = new List<CierrePeriodo>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerCierrePeriodo(empresa, fecha, areaNegocio, token));

            for (var i = 0; i < objects.Count; i++)
            {
                cierrePeriodo.Add(
                    InstanceAsistencia.__CreateObjectInstanceCierrePeriodo(objects[i], resource)
                );
            }

            return cierrePeriodo;
        }

        public static List<CierrePeriodo> __AsistenciaInsertarCierrePeriodo(string empresa, string fecha, string areaNegocio, string usuario, string token, string resource)
        {
            List<CierrePeriodo> cierrePeriodo = new List<CierrePeriodo>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaInsertarCierrePeriodo(empresa, fecha, areaNegocio, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                cierrePeriodo.Add(
                    InstanceAsistencia.__CreateObjectInstanceCierrePeriodo(objects[i], resource)
                );
            }

            return cierrePeriodo;
        }

        public static List<CierrePeriodo> __AsistenciaObtenerPeriodos(string fechaDesde, string fechaHasta, string empresa, string areaNegocio, string token, string resource)
        {
            List<CierrePeriodo> cierrePeriodo = new List<CierrePeriodo>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerPeriodos(fechaDesde, fechaHasta, empresa, areaNegocio, token));

            for (var i = 0; i < objects.Count; i++)
            {
                cierrePeriodo.Add(
                    InstanceAsistencia.__CreateObjectInstanceCierrePeriodo(objects[i], resource)
                );
            }

            return cierrePeriodo;
        }

        public static List<CierrePeriodo> __AsistenciaInsertarExcepcionPeriodo(string empresa, string fecha, string areaNegocio, string excepcion, string usuario, string token, string resource)
        {
            List<CierrePeriodo> cierrePeriodo = new List<CierrePeriodo>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaInsertarExcepcionPeriodo(empresa, fecha, areaNegocio, excepcion, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                cierrePeriodo.Add(
                    InstanceAsistencia.__CreateObjectInstanceCierrePeriodo(objects[i], resource)
                );
            }

            return cierrePeriodo;
        }


        public static List<ArchivoAsistencia> __AsistenciaObtenerArchivosAsistencia(string empresa, string ficha, string areaNegocio, string fecha, string token, string resource)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerArchivosAsistencia(empresa, ficha, areaNegocio, fecha, token));

            for (var i = 0; i < objects.Count; i++)
            {
                archivoAsistencia.Add(
                    InstanceAsistencia.__CreateObjectInstanceArchivoAsistencia(objects[i], resource)
                );
            }

            return archivoAsistencia;
        }

        public static List<ArchivoAsistencia> __AsistenciaObtenerNombreArchivo(string token, string resource)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerNombreArchivo(token));
            for (var i = 0; i < objects.Count; i++)
            {
                archivoAsistencia.Add(
                    InstanceAsistencia.__CreateObjectInstanceArchivoAsistencia(objects[i], resource)
                );
            }

            return archivoAsistencia;
        }

        public static List<RutaFicheros> __AsistenciaObtenerRutaFichero(string token, string resource)
        {
            List<RutaFicheros> rutaFichero = new List<RutaFicheros>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerRutaFichero(token));
            for (var i = 0; i < objects.Count; i++)
            {
                rutaFichero.Add(
                    InstanceAsistencia.__CreateObjectInstanceRutaFichero(objects[i], resource)
                );
            }

            return rutaFichero;
        }

        public static List<ArchivoAsistencia> __AsistenciaInsertarArchivosAsistencia(string empresa, string ficha, string areaNegocio, string fecha, string usuario, string codigoRuta, string nombreArchivo, string nombreRealArchivo, string extension, string token, string resource)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaInsertarArchivosAsistencia(empresa, ficha, areaNegocio, fecha, usuario, codigoRuta, nombreArchivo, nombreRealArchivo, extension, token));

            for (var i = 0; i < objects.Count; i++)
            {
                archivoAsistencia.Add(
                    InstanceAsistencia.__CreateObjectInstanceArchivoAsistencia(objects[i], resource)
                );
            }

            return archivoAsistencia;
        }

        public static List<ArchivoAsistencia> __AsistenciaEliminarArchivoAsistencia(string codigoRuta, string codigoArchivo, string nombreReal, string token, string resource)
        {
            List<ArchivoAsistencia> archivoAsistencia = new List<ArchivoAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaEliminarArchivoAsistencia(codigoRuta, codigoArchivo, nombreReal, token));

            for (var i = 0; i < objects.Count; i++)
            {
                archivoAsistencia.Add(
                    InstanceAsistencia.__CreateObjectInstanceArchivoAsistencia(objects[i], resource)
                );
            }

            return archivoAsistencia;
        }

        public static List<JornadaLaboral> __AsistenciaObtenerJornadaLaboral(string empresa, string areaNegocio, string usuario, string token, string resource)
        {
            List<JornadaLaboral> jornadaLaboral = new List<JornadaLaboral>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerJornadaLaboral(empresa, areaNegocio, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                jornadaLaboral.Add(
                    InstanceAsistencia.__CreateObjectInstanceJornadaLaboral(objects[i], resource)
                );
            }

            return jornadaLaboral;
        }

        public static List<JornadaLaboral> __AsistenciaActualizarJornadaLaboral(string codigoJornada, string vigente, string nombreJornada, string descripcionJornada, string horasSemanales, string porcentaje, string action, string usuario, string token, string resource)
        {
            List<JornadaLaboral> jornadaLaboral = new List<JornadaLaboral>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaActualizarJornadaLaboral(codigoJornada, vigente, nombreJornada, descripcionJornada, horasSemanales, porcentaje, action, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                jornadaLaboral.Add(
                    InstanceAsistencia.__CreateObjectInstanceJornadaLaboral(objects[i], resource)
                );
            }

            return jornadaLaboral;
        }

        public static List<JornadaLaboral> __AsistenciaIngresarJornadaLaboral(string empresa, string areaNegocio, string nombreJornada, string descripcion, string horasSemanales, string porcentaje, string usuario, string token, string resource)
        {
            List<JornadaLaboral> jornadaLaboral = new List<JornadaLaboral>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarJornadaLaboral(empresa, areaNegocio, nombreJornada, descripcion, horasSemanales, porcentaje, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                jornadaLaboral.Add(
                    InstanceAsistencia.__CreateObjectInstanceJornadaLaboral(objects[i], resource)
                );
            }

            return jornadaLaboral;
        }

        public static List<FichaJornadaLaboral> __AsistenciaObtenerFichaJornadaLaboral(string empresa, string areaNegocio, string ficha, string usuario, string token, string resource)
        {
            List<FichaJornadaLaboral> jornadaLaboral = new List<FichaJornadaLaboral>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerFichaJornadaLaboral(empresa, areaNegocio, ficha, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                jornadaLaboral.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaJornadaLaboral(objects[i], resource)
                );
            }

            return jornadaLaboral;
        }

        public static List<FichaJornadaLaboral> __AsistenciaIngresarFichaJornadaLaboral(string empresa, string areaNegocio, string ficha, string codigoJornada, string usuario, string token, string resource)
        {
            List<FichaJornadaLaboral> jornadaLaboral = new List<FichaJornadaLaboral>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarFichaJornadaLaboral(empresa, areaNegocio, ficha, codigoJornada, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                jornadaLaboral.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaJornadaLaboral(objects[i], resource)
                );
            }

            return jornadaLaboral;
        }

        public static List<HorasExtras> __AsistenciaIngresarHoraExtra(string empresa, string ficha, string fecha, string areaNegocio, string codigoJornada, string horaExtra, string action, string usuario, string token, string resource)
        {
            List<HorasExtras> horasExtras = new List<HorasExtras>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarHoraExtra(empresa, ficha, fecha, areaNegocio, codigoJornada, horaExtra, action, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                horasExtras.Add(
                    InstanceAsistencia.__CreateObjectInstanceHorasExtras(objects[i], resource)
                );
            }

            return horasExtras;
        }

        public static List<HorasExtras> __HorasExtrasObtener(string empresa, string ficha, string fecha, string areaNegocio, string token, string resource)
        {
            List<HorasExtras> horasExtras = new List<HorasExtras>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__HorasExtrasObtener(empresa, ficha, fecha, areaNegocio, token));
            for (var i = 0; i < objects.Count; i++)
            {
                horasExtras.Add(InstanceAsistencia.__CreateObjectInstanceHorasExtras(objects[i], resource));
            }

            return horasExtras;
        }

        public static List<HorasExtras> __AsistenciaReporteHorasExtras(string empresa, string ficha, string fecha, string areaNegocio, string usuario, string token, string resource)
        {
            List<HorasExtras> horasExtras = new List<HorasExtras>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaReporteHorasExtras(empresa, ficha, fecha, areaNegocio, usuario, token));
            for (var i = 0; i < objects.Count; i++)
            {
                horasExtras.Add(InstanceAsistencia.__CreateObjectInstanceHorasExtras(objects[i], resource));
            }

            return horasExtras;
        }

        public static List<ReporteAsistencia> __AsistenciaReporteHorasColumnas(string empresa, string ficha, string fecha, string areaNegocio, string token, string resource)
        {
            List<ReporteAsistencia> columnas = new List<ReporteAsistencia>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaReporteHorasColumnas(empresa, ficha, fecha, areaNegocio, token));

            for (var i = 0; i < objects.Count; i++)
            {
                columnas.Add(
                    InstanceAsistencia.__CreateObjectInstanceReporteAsistencia(objects[i], resource)
                );
            }

            return columnas;
        }


        public static List<Bonos> __AsistenciaObtenerBonos(string empresa, string areaNegocio, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerBonos(empresa, areaNegocio, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaIngresarBonos(string empresa, string areaNegocio, string bono, string descripcion, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarBonos(empresa, areaNegocio, bono, descripcion, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaActualizarBonos(string codigo, string vigente, string bono, string descripcion, string action, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaActualizarBonos(codigo, vigente, bono, descripcion, action, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaEliminarBonos(string codigo, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaEliminarBonos(codigo, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaObtenerBonoCliente(string empresa, string areaNegocio, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerBonoCliente(empresa, areaNegocio, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaIngresarBonoCliente(string empresa, string areaNegocio, string codigo, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarBonoCliente(empresa, areaNegocio, codigo, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaActualizarBonoCliente(string empresa, string areaNegocio, string codigo, string vigente, string action, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaActualizarBonoCliente(empresa, areaNegocio, codigo, vigente, action, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<Bonos> __AsistenciaEliminarBonoCliente(string empresa, string areaNegocio, string codigo, string usuario, string token, string resource)
        {
            List<Bonos> bonos = new List<Bonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaEliminarBonoCliente(empresa, areaNegocio, codigo, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                bonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceBonos(objects[i], resource)
                );
            }

            return bonos;
        }

        public static List<FichaBonos> __AsistenciaObtenerFichaBonos(string empresa, string areaNegocio, string ficha, string fecha, string usuario, string token, string resource)
        {
            List<FichaBonos> fichaBonos = new List<FichaBonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaObtenerFichaBonos(empresa, areaNegocio, ficha, fecha, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                fichaBonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaBonos(objects[i], resource)
                );
            }

            return fichaBonos;
        }

        public static List<FichaBonos> __AsistenciaIngresarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor, string usuario, string token, string resource)
        {
            List<FichaBonos> fichaBonos = new List<FichaBonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaIngresarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, valor, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                fichaBonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaBonos(objects[i], resource)
                );
            }

            return fichaBonos;
        }

        public static List<FichaBonos> __AsistenciaActualizarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string valor, string usuario, string token, string resource)
        {
            List<FichaBonos> fichaBonos = new List<FichaBonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaActualizarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, valor, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                fichaBonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaBonos(objects[i], resource)
                );
            }

            return fichaBonos;
        }

        public static List<FichaBonos> __AsistenciaEliminarFichaBono(string empresa, string areaNegocio, string ficha, string fecha, string codigo, string usuario, string token, string resource)
        {
            List<FichaBonos> fichaBonos = new List<FichaBonos>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPIAsistencia.__AsistenciaEliminarFichaBono(empresa, areaNegocio, ficha, fecha, codigo, usuario, token));

            for (var i = 0; i < objects.Count; i++)
            {
                fichaBonos.Add(
                    InstanceAsistencia.__CreateObjectInstanceFichaBonos(objects[i], resource)
                );
            }

            return fichaBonos;
        }
    }
}

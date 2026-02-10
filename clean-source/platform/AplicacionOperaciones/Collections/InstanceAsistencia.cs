using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Asistencia;

namespace AplicacionOperaciones.Collections
{
    public class InstanceAsistencia
    {
        public static Asistencia __CreateObjectInstanceAsistencia(dynamic objets, dynamic request)
        {
            Asistencia instance = new Asistencia();

            instance.Empresa = objets.Empresa?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.Fecha = objets.Fecha?.ToString();
            instance.CodigoAsistencia = objets.CodigoAsistencia?.ToString();
            instance.Observacion = objets.Observacion?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCarga = objets.UsuarioCarga?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.Vigente = objets.Vigente?.ToString();

            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.Color = objets.Color?.ToString();
            instance.Style = objets.Style?.ToString();

            return instance;
        }

        public static Licencia __CreateObjectInstanceLicencia(dynamic objets, dynamic request)
        {
            Licencia instance = new Licencia();

            instance.Ficha = objets.Ficha?.ToString();
            instance.FechaEmision = objets.FechaEmision?.ToString();
            instance.FechaInitRepReal = objets.FechaInitRepReal?.ToString();
            instance.FechaTerminoReal = objets.FechaTerminoReal?.ToString();
            instance.FechaIniRep = objets.FechaIniRep?.ToString();
            instance.NroDiasLicSof = objets.NroDiasLicSof?.ToString();
            instance.NroDiasLic = objets.NroDiasLic?.ToString();
            instance.DiaLic = objets.DiaLic?.ToString();
            instance.Class = objets.Class?.ToString(); 
            instance.CodigoAsistencia = objets.CodigoAsistencia?.ToString();

            return instance;
        }

        public static Vacacion __CreateObjectInstanceVacacion(dynamic objets, dynamic request)
        {
            Vacacion instance = new Vacacion();

            instance.Ficha = objets.Ficha?.ToString();
            instance.FechaEmision = objets.FechaEmision?.ToString();
            instance.FaDesdeReal = objets.FaDesdeReal?.ToString();
            instance.FaHastaReal = objets.FaHastaReal?.ToString();
            instance.FaDesde = objets.FaDesde?.ToString();
            instance.NDiasApSof = objets.NDiasApSof?.ToString();
            instance.NDiasAp = objets.NDiasAp?.ToString();
            instance.DiaVac = objets.DiaVac?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.CodigoAsistencia = objets.CodigoAsistencia?.ToString();

            return instance;
        }

        public static BajasConfirmadas __CreateObjectInstanceBajas(dynamic objets, dynamic request)
        {
            BajasConfirmadas instance = new BajasConfirmadas();

            instance.Id = objets.Id?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.FechaInicio = objets.FechaInicio?.ToString();
            instance.FechaTermino = objets.FechaTermino?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.Estado = objets.Estado?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.Color = objets.Color?.ToString();
            instance.DiaBaja = objets.DiaBaja?.ToString();

            return instance;
        }

        public static TipoAsistencia __CreateObjectInstanceTipoAsistencia(dynamic objets, dynamic request)
        {
            TipoAsistencia instance = new TipoAsistencia();

            instance.CodigoAsistencia = objets.CodigoAsistencia?.ToString();
            instance.Nombre = objets.Nombre?.ToString();
            instance.Vigente = objets.Vigente?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.Color = objets.Color?.ToString();

            return instance;
        }

        public static Personal __CreateObjectInstancePersonal(dynamic objets, dynamic request)
        {
            Personal instance = new Personal();

            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Nombres = objets.Nombres?.ToString();
            instance.Rut = objets.Rut?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.CargoMod = objets.CargoMod?.ToString();
            instance.FechaInicio = objets.FechaInicio?.ToString();
            instance.FechaTermino = objets.FechaTermino?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.Plataforma = objets.Plataforma?.ToString();

            return instance;
        }

        public static Leyenda __CreateObjectInstanceLeyenda(dynamic objets, dynamic request)
        {
            Leyenda instance = new Leyenda();

            instance.Nombre = objets.Nombre?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.CodigoAsistencia = objets.CodigoAsistencia?.ToString();
            instance.Color = objets.Color?.ToString();
            instance.Style = objets.Style?.ToString();

            return instance;
        }


        public static ReporteAsistencia __CreateObjectInstanceReporteAsistencia(dynamic objets, dynamic request)
        {
            ReporteAsistencia instance = new ReporteAsistencia();

            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Nombres = objets.Nombres?.ToString();
            instance.Rut = objets.Rut?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.CargoMod = objets.CargoMod?.ToString();
            instance.FechaRealInicio = objets.FechaRealInicio?.ToString();
            instance.FechaRealTermino = objets.FechaRealTermino?.ToString();
            instance.FechaRealTerminoAux = objets.FechaRealTerminoAux?.ToString();
            instance.FechaInicio = objets.FechaInicio?.ToString();
            instance.Dias = objets.Dias?.ToString();
            instance.Style = objets.Style?.ToString();
            instance.Color = objets.Color?.ToString();
            instance.Tipo = objets.Tipo?.ToString();
            instance.Hoja = objets.Hoja?.ToString();
            instance.UltimoDia = objets.UltimoDia?.ToString();
            instance.SinContrato = objets.SinContrato?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.Columna = objets.Columna?.ToString();
            instance.DataColumna = objets.DataColumna?.ToString();
            instance.NombreCargo = objets.NombreCargo?.ToString();
            instance.TopeLegal = objets.TopeLegal?.ToString();
            instance.Causal = objets.Causal?.ToString();
            instance.Estado = objets.Estado?.ToString();
            instance.Plataforma = objets.Plataforma?.ToString();

            return instance;
        }


        //V2
        public static AreaNegocio __CreateObjectInstanceAreaNegocio(dynamic objets, dynamic request)
        {
            AreaNegocio instance = new AreaNegocio();

            instance.Codigo = objets.Codigo?.ToString();
            instance.Nombre = objets.Nombre?.ToString();

            return instance;
        }

        public static Pagination __CreateObjectInstancePagination(dynamic objets, dynamic request)
        {
            Pagination instance = new Pagination();

            instance.NumeroPagina = objets.NumeroPagina?.ToString();
            instance.Rango = objets.Rango?.ToString();
            instance.Class = objets.Class?.ToString();
            instance.Properties = objets.Properties?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.Fecha = objets.Fecha?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Usuario = objets.Usuario?.ToString();

            return instance;
        }


        public static CierrePeriodo __CreateObjectInstanceCierrePeriodo(dynamic objets, dynamic request)
        {
            CierrePeriodo instance = new CierrePeriodo();

            instance.IdCierre = objets.IdCierre?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.FechaCierre = objets.FechaCierre?.ToString();
            instance.UsuarioCierre = objets.UsuarioCierre?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.Cerrado = objets.Cerrado?.ToString();
            instance.Excepcion = objets.Excepcion?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }


        public static ArchivoAsistencia __CreateObjectInstanceArchivoAsistencia(dynamic objets, dynamic request)
        {
            ArchivoAsistencia instance = new ArchivoAsistencia();

            instance.CodigoArchivo = objets.CodigoArchivo?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.Cliente = objets.Cliente?.ToString();
            instance.CodigoRuta = objets.CodigoRuta?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.NombreArchivo = objets.NombreArchivo?.ToString();
            instance.NombreRealArchivo = objets.NombreRealArchivo?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreador = objets.UsuarioCreador?.ToString();
            instance.UsuarioUltActualizacion = objets.UsuarioUltActualizacion?.ToString();
            instance.FechaUltActualizacion = objets.ExcFechaUltActualizacionepcion?.ToString();
            instance.UltimoComentario = objets.UltimoComentario?.ToString();
            instance.RutaFichero = objets.RutaFichero?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }

        public static RutaFicheros __CreateObjectInstanceRutaFichero(dynamic objets, dynamic request)
        {
            RutaFicheros instance = new RutaFicheros();

            instance.CodigoRuta = objets.CodigoRuta?.ToString();
            instance.RutaFichero = objets.RutaFichero?.ToString();
            instance.NombreRuta = objets.NombreRuta?.ToString();
            instance.Estado = objets.Estado?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreador = objets.UsuarioCreador?.ToString();
            instance.UsuarioUltActualizacion = objets.UsuarioUltActualizacion?.ToString();
            instance.FechaUltActualizacion = objets.FechaUltActualizacion?.ToString();
            instance.UltimoComentario = objets.UltimoComentario?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }

        public static JornadaLaboral __CreateObjectInstanceJornadaLaboral(dynamic objets, dynamic request)
        {
            JornadaLaboral instance = new JornadaLaboral();

            instance.CodigoJornada = objets.CodigoJornada?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.NombreJornada = objets.NombreJornada?.ToString();
            instance.DescripcionJornada = objets.DescripcionJornada?.ToString();
            instance.HorasSemanal = objets.HorasSemanal?.ToString();
            instance.Vigente = objets.Vigente?.ToString();
            instance.UsuarioCreacion = objets.UsuarioCreacion?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.PorcentajePago = objets.PorcentajePago?.ToString();

            return instance;
        }

        public static FichaJornadaLaboral __CreateObjectInstanceFichaJornadaLaboral(dynamic objets, dynamic request)
        {
            FichaJornadaLaboral instance = new FichaJornadaLaboral();

            instance.CodigoFichaJornada = objets.CodigoFichaJornada?.ToString();
            instance.CodigoJornada = objets.CodigoJornada?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.JornadaActiva = objets.JornadaActiva?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreacion = objets.UsuarioCreacion?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.NombreJornada = objets.NombreJornada?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }

        public static HorasExtras __CreateObjectInstanceHorasExtras(dynamic objets, dynamic request)
        {
            HorasExtras instance = new HorasExtras();

            instance.CodigoHoraExtra = objets.CodigoHoraExtra?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.CodigoJornada = objets.CodigoJornada?.ToString();
            instance.HoraExtra = objets.HoraExtra?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreacion = objets.UsuarioCreacion?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.DiasHHEE = objets.DiasHHEE?.ToString();
            instance.NombreJornada = objets.NombreJornada?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();
            instance.PorcentajePago = objets.PorcentajePago?.ToString();

            return instance;
        }



        public static Bonos __CreateObjectInstanceBonos(dynamic objets, dynamic request)
        {
            Bonos instance = new Bonos();

            instance.CodigoBono = objets.CodigoBono?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Bono = objets.Bono?.ToString();
            instance.Descripcion = objets.Descripcion?.ToString();
            instance.Vigente = objets.Vigente?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreacion = objets.UsuarioCreacion?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.OptEliminar = objets.OptEliminar?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }

        public static FichaBonos __CreateObjectInstanceFichaBonos(dynamic objets, dynamic request)
        {
            FichaBonos instance = new FichaBonos();

            instance.CodigoFichaBonos = objets.CodigoFichaBonos?.ToString();
            instance.Empresa = objets.Empresa?.ToString();
            instance.AreaNegocio = objets.AreaNegocio?.ToString();
            instance.Ficha = objets.Ficha?.ToString();
            instance.FechaBono = objets.FechaBono?.ToString();
            instance.CodigoBono = objets.CodigoBono?.ToString();
            instance.ValorBono = objets.ValorBono?.ToString();
            instance.Vigente = objets.Vigente?.ToString();
            instance.FechaCreacion = objets.FechaCreacion?.ToString();
            instance.UsuarioCreacion = objets.UsuarioCreacion?.ToString();
            instance.FechaUltModificacion = objets.FechaUltModificacion?.ToString();
            instance.UsuarioUltModificacion = objets.UsuarioUltModificacion?.ToString();
            instance.Bono = objets.Bono?.ToString();
            instance.Code = objets.Code?.ToString();
            instance.Message = objets.Message?.ToString();

            return instance;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;

namespace Teamwork.Infraestructura.Instances.Seleccion
{
    public class InstanceSeleccion
    {
        public static Tag __CreateObjectInstanceTag(dynamic objects, string resource = "")
        {
            Tag instance = new Tag();

            instance.CodigoTag = objects.CodigoTag?.ToString();
            instance.Descripcion = objects.Descripcion?.ToString();
            instance.FechaCreacion = objects.FechaRealTermino?.ToString();
            instance.UsuarioCreacion = objects.UsuarioCreacion?.ToString();
            instance.FechaUltimaActualizacion = objects.FechaUltimaActualizacion?.ToString();
            instance.UsuarioUltimaActualizacion = objects.UsuarioUltimaActualizacion?.ToString();
            instance.Estado = objects.Estado?.ToString();
            instance.FechaVigenciaDesde = objects.FechaVigenciaDesde?.ToString();
            instance.FechaVigenciaHasta = objects.FechaVigenciaHasta?.ToString();
            instance.OptEditar = objects.OptEditar?.ToString();
            instance.OptActivar = objects.OptActivar?.ToString();
            instance.OptDesactivar = objects.OptDesactivar?.ToString();
            instance.OptEliminar = objects.OptEliminar?.ToString();
            instance.Code = objects.Code?.ToString();
            instance.Message = objects.Message?.ToString();

            return instance;
        }

        public static OfertaLaboral __CreateObjectInstanceOfertaLaboral(dynamic objects, string resource = "")
        {
            OfertaLaboral instance = new OfertaLaboral();

            instance.Id = objects.Id?.ToString();
            instance.Fecha = objects.Fecha?.ToString();
            instance.Empresa = objects.Empresa?.ToString();
            instance.Nombre = objects.Nombre?.ToString();
            instance.Cliente = objects.Cliente?.ToString();
            instance.Lugar = objects.Lugar?.ToString();
            instance.Cupos = objects.Cupos?.ToString();
            instance.FechaInicio = objects.FechaInicio?.ToString();
            instance.Horario = objects.Horario?.ToString();
            instance.Observacion = objects.Observacion?.ToString();
            instance.Estado = objects.Estado?.ToString();
            instance.Usuario = objects.Usuario?.ToString();
            instance.Duracion = objects.Duracion?.ToString();
            instance.AreaNeg = objects.AreaNeg?.ToString();
            instance.CuposRest = objects.CuposRest?.ToString();
            instance.Region = objects.Region?.ToString();
            instance.Target = objects.Target?.ToString();
            instance.DescripcionLarga = objects.DescripcionLarga?.ToString();
            instance.DescripcionCorta = objects.DescripcionCorta?.ToString();
            instance.FechaInicioPublic = objects.FechaInicioPublic?.ToString();
            instance.FechaTerminoPublic = objects.FechaTerminoPublic?.ToString();
            instance.CodigoSolicitud = objects.CodigoSolicitud?.ToString();
            instance.Enlace = objects.Enlace?.ToString();
            instance.Publicada = objects.Publicada?.ToString();
            instance.Border = objects.Border?.ToString();
            instance.Color = objects.Color?.ToString();
            instance.Code = objects.Code?.ToString();
            instance.Message = objects.Message?.ToString();

            return instance;
        }

        public static Targets __CreateObjectInstanceTarget(dynamic objects, string resource = "")
        {
            Targets instance = new Targets();

            instance.Target = objects.Target?.ToString();
            instance.FechaVigenciaDesde = objects.FechaVigenciaDesde?.ToString();
            instance.FechaVigenciaHasta = objects.FechaVigenciaHasta?.ToString();
            instance.FechaCreacion = objects.FechaCreacion?.ToString();

            return instance;
        }

        public static TagOfertaLaboral __CreateObjectInstanceTagOfertaLaboral(dynamic objects, string resource = "")
        {
            TagOfertaLaboral instance = new TagOfertaLaboral();

            instance.IdOfertaLaboral = objects.IdOfertaLaboral?.ToString();
            instance.CodigoTag = objects.CodigoTag?.ToString();
            instance.FechaCreacion = objects.FechaCreacion?.ToString();
            instance.Estado = objects.Estado?.ToString();
            instance.Categoria = objects.Categoria?.ToString();
            instance.Descripcion = objects.Descripcion?.ToString();
            instance.CodigoSolicitud = objects.CodigoSolicitud?.ToString();
            instance.Code = objects.Code?.ToString();
            instance.Message = objects.Message?.ToString();

            return instance;
        }

        public static Pagination __CreateObjectInstancePagination(dynamic objects, string resource = "")
        {
            Pagination instance = new Pagination();

            instance.NumeroPagina = objects.NumeroPagina?.ToString();
            instance.Class = objects.Class?.ToString();
            instance.Rango = objects.Rango?.ToString();
            instance.Properties = objects.Properties?.ToString();
            instance.Filter = objects.Filter?.ToString();
            instance.DataFilter = objects.DataFilter?.ToString();

            return instance;
        }
    }
}
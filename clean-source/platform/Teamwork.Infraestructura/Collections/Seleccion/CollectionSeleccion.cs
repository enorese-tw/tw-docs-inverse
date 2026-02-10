using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.Seleccion;
using Teamwork.Model.Seleccion;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Seleccion
{
    public class CollectionSeleccion
    {
        public static List<Tag> __CrearTag(string descripcion, string usuarioCreador, string fechaDesde, string fechaHasta, string resource)
        {
            List<Tag> collections = new List<Tag>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__PostulanteCrearTag(
                    descripcion,
                    usuarioCreador,
                    fechaDesde,
                    fechaHasta,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTag(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Tag> __ListarTag(string filterType, string dataFilter, string pagination, string resource)
        {
            List<Tag> collections = new List<Tag>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__PostulanteListarTag(
                    filterType,
                    dataFilter,
                    pagination,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTag(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Tag> __ActualizarTag(string tag, string action, string usuario, string descripcion, string status, string fechaDesde, string fechaHasta, string resource)
        {
            List<Tag> collections = new List<Tag>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__PostulanteActualizarTag(
                    tag,
                    action,
                    usuario,
                    descripcion,
                    status,
                    fechaDesde,
                    fechaHasta,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTag(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Tag> __EliminarTag(string tag, string resource)
        {
            List<Tag> collections = new List<Tag>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__PostulanteEliminarTag(
                    tag,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTag(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<OfertaLaboral> __ListarOfertasLaborales(string filterType, string dataFilter, string target, string usuario, string pagination, string type, string resource)
        {
            List<OfertaLaboral> collections = new List<OfertaLaboral>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__ListarOfertasLaborales(
                    filterType,
                    dataFilter,
                    target,
                    usuario,
                    pagination,
                    type,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceOfertaLaboral(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Targets> __ListarTarget(string resource)
        {
            List<Targets> collections = new List<Targets>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__ListarTarget(
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTarget(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<OfertaLaboral> __ActualizarOfertaLaboral(string idOfertaLaboral, string descripcionLarga, string descripcionCorta, string fechaInicio, string fechaTermino, string target, string resource)
        {
            List<OfertaLaboral> collections = new List<OfertaLaboral>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__PostulanteActualizarOfertaLaboral(
                    idOfertaLaboral,
                    descripcionLarga,
                    descripcionCorta,
                    fechaInicio,
                    fechaTermino,
                    target,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceOfertaLaboral(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<TagOfertaLaboral> __ListarTagOfertaLaboral(string idOfertaLaboral, string filterType, string dataFilter, string accion, string pagination, string resource)
        {
            List<TagOfertaLaboral> collections = new List<TagOfertaLaboral>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__ListarTagOfertaLaboral(
                    idOfertaLaboral,
                    filterType,
                    dataFilter,
                    accion,
                    pagination,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTagOfertaLaboral(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<TagOfertaLaboral> __CrearTagOfertaLaboral(string idOfertaLaboral, string descripcionTag, string categoria, string resource)
        {
            List<TagOfertaLaboral> collections = new List<TagOfertaLaboral>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__CrearTagOfertaLaboral(
                    idOfertaLaboral,
                    descripcionTag,
                    categoria,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTagOfertaLaboral(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<TagOfertaLaboral> __EliminarTagOfertaLaboral(string idOfertaLaboral, string codigoTag, string resource)
        {
            List<TagOfertaLaboral> collections = new List<TagOfertaLaboral>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__EliminarTagOfertaLaboral(
                    idOfertaLaboral,
                    codigoTag,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstanceTagOfertaLaboral(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Pagination> __Pagination(string pagination, string typePagination, string filterType, string dataFilter, string target, string idOfertaLaboral, string usuario, string resource)
        {
            List<Pagination> collections = new List<Pagination>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__Pagination(
                    pagination,
                    typePagination,
                    filterType,
                    dataFilter,
                    target,
                    idOfertaLaboral,
                    usuario,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSeleccion.__CreateObjectInstancePagination(objects[i], resource)
                );
            }

            return collections;
        }

        public static List<Token> __CodigoOAuthCrear(string userId, string token)
        {
            List<Token> collections = new List<Token>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPISeleccion.__CodigoOAuthCrear(
                    userId,
                    token,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceAuth.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

    }
}
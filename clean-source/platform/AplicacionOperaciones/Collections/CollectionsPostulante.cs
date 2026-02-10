using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;
using Teamwork.WebApi;

namespace AplicacionOperaciones.Collections
{
    public class CollectionsPostulante
    {
        public static List<Request> __PostulanteValidateTokenInvitacion(string tokenInvitacion, string token, string resource)
        {
            List<Request> requests = new List<Request>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPISeleccion.__PostulanteValidateTokenInvitacion(tokenInvitacion, token));

            for (var i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], resource)
                );
            }

            return requests;
        }

        public static List<Request> __PostulanteCreaOActualizaFichaPostulante(string usuarioCreador, string nombres, string apellidoPaterno, string apellidoMaterno, string telefono, string fechaNacimiento,
                             string direccion, string comuna, string correo, string tokenInvitacion, string rut, string token)
        {
            List<Request> requests = new List<Request>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPISeleccion.__PostulanteCreaOActualizaFichaPostulante(
                                                                usuarioCreador,
                                                                nombres,
                                                                apellidoPaterno,
                                                                apellidoMaterno,
                                                                telefono,
                                                                fechaNacimiento,
                                                                direccion,
                                                                comuna,
                                                                correo,
                                                                tokenInvitacion,
                                                                rut,
                                                                token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }
            
            return requests;
        }

        public static List<Request> __PostulanteValidaFichaPersonal(string DNI, string tipoDNI, string token)
        {
            List<Request> requests = new List<Request>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPISeleccion.__PostulanteValidaFichaPersonal(DNI, tipoDNI, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                requests.Add(
                        InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }
        
            return requests;
        }

        public static List<Postulante> __PostulanteConsultaFichaPersonal(string DNI, string token)
        {
            List<Postulante> postulantes = new List<Postulante>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPISeleccion.__PostulanteConsultaFichaPersonal(DNI, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                postulantes.Add(
                    InstancePostulante.__CreateObjectInstance(objects[i], "")
                );
            }

            return postulantes;
        }

        public static List<FieldEncuesta> __PostulanteConsultaFieldEncuesta(string tokenInvitacion, string token)
        {
            List<FieldEncuesta> fields = new List<FieldEncuesta>();

            dynamic objects = JsonConvert.DeserializeObject(CallAPISeleccion.__PostulanteConsultaFieldEncuesta(tokenInvitacion, token));

            for (dynamic i = 0; i < objects.Count; i++)
            {
                fields.Add(
                    InstanceFieldEncuesta.__CreateObjectInstance(objects[i], "")
                );
            }

            return fields;
        }

    }
}
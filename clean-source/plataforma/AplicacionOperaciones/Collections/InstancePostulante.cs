using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Seleccion;

namespace AplicacionOperaciones.Collections
{
    public class InstancePostulante
    {
        public static Postulante __CreateObjectInstance(dynamic objects, dynamic request)
        {
            Postulante instance = new Postulante();
            
            instance.DNI = objects.DNI.ToString();
            instance.Nombre = objects.Nombres.ToString();
            instance.ApellidoPaterno = objects.ApellidoPaterno.ToString();
            instance.ApellidoMaterno = objects.ApellidoMaterno.ToString();
            instance.Telefono = objects.Telefono.ToString();
            instance.FechaNacimiento = objects.FechaNacimiento.ToString();
            instance.Direccion = objects.Direccion.ToString();
            instance.Comuna = objects.Comuna.ToString();
            instance.Correo = objects.Correo.ToString();

            return instance;
        }
    }
}
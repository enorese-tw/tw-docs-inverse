using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones;

namespace AplicacionOperaciones.Collections
{
    public class InstanceResponseCarga
    {
        public static ResponseCarga __CreateObjectInstance(dynamic objets, dynamic request)
        {
            ResponseCarga instance = new ResponseCarga();

            instance.NombrePlantilla = objets.NombrePlantilla.ToString();
            instance.NombreHojaCargaMasiva = objets.NombreHojaCargaMasiva.ToString();
            instance.Columnas = objets.Columnas.ToString();
            instance.GetValueError = objets.GetValueError.ToString();

            return instance;
        }
    }
}
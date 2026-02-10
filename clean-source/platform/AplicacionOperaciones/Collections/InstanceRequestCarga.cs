using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Operaciones;

namespace AplicacionOperaciones.Collections
{
    public class InstanceRequestCarga
    {
        public static RequestCarga __CreateObjectInstance(dynamic objets, dynamic request)
        {
            RequestCarga instance = new RequestCarga();

            instance.Code = objets.Code.ToString();
            instance.Message = objets.Message.ToString();
            //instance.Titulo = objets.Titulo.ToString();
            instance.Procesados = objets.Procesados.ToString();
            instance.Errores = (objets.Errores != null) ? objets.Errores.ToString() : "";

            return instance; 
        }
    }
}
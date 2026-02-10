using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceComplementos
    {
        public static Complementos __CreateObjectInstance(dynamic objects)
        {
            Complementos instance = new Complementos();

            instance.Border = objects.Border.ToString();
            instance.GlyphiconColor = objects.GlyphiconColor.ToString();
            instance.Creado = objects.Creado.ToString();
            instance.CreadoPor = objects.CreadoPor.ToString();
            instance.TotalComplemento = objects.TotalComplemento.ToString();
            instance.Comentarios = objects.Comentarios.ToString();
            instance.NombreSolicitud = objects.NombreSolicitud.ToString();
            instance.Solicitud = objects.Solicitud.ToString();
            instance.NombreSolicitudCorto = objects.NombreSolicitudCorto.ToString();
            instance.Folio = objects.Folio.ToString();
            instance.CodigoComplemento = objects.CodigoComplemento.ToString();

            return instance;
        }

       
    }
}
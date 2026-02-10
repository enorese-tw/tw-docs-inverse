using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Model.Bajas;

namespace Teamwork.Infraestructura.Instances.Finiquitos
{
    public class InstanceFiniquitos
    {
        public static Finiquito __CreateObjectInstance(dynamic objects)
        {
            Finiquito instance = new Finiquito();

            instance.Code = objects.Code.ToString();
            instance.Message = objects.Message.ToString();
            instance.NombreSolicitud = objects.NombreSolicitud.ToString();
            instance.CodigoFiniquito = objects.CodigoFiniquito.ToString();
            instance.Folio = objects.Folio.ToString();
            instance.Creado = objects.Creado.ToString();
            instance.CreadoPor = objects.CreadoPor.ToString();
            instance.CargoMod = objects.CargoMod.ToString();
            instance.Cargo = objects.Cargo.ToString();
            instance.Causal = objects.Causal.ToString();
            instance.TotalFiniquito = objects.TotalFiniquito.ToString();
            instance.Comentarios = objects.Comentarios.ToString();

            instance.Border = objects.Border.ToString();
            instance.GlyphiconColor = objects.GlyphiconColor.ToString();
            instance.Glyphicon = objects.Glyphicon.ToString();
            instance.Estado = objects.Estado.ToString();

            instance.OptCaratulaFiniquito = objects.OptCaratulaFiniquito?.ToString();
            instance.OptDocFiniquito = objects.OptDocFiniquito?.ToString();
            instance.OptHistorialFiniquito = objects.OptHistorialFiniquito?.ToString();
            instance.OptValidarFiniquito = objects.OptValidarFiniquito?.ToString();
            instance.OptAnularFiniquito = objects.OptAnularFiniquito?.ToString();
            instance.OptGestionEnvioFiniquito = objects.OptGestionEnvioFiniquito?.ToString();
            instance.OptRecepcionFiniquito = objects.OptRecepcionFiniquito?.ToString();
            instance.OptRecepcionFiniquitoRegiones = objects.OptRecepcionFiniquitoRegiones?.ToString();
            instance.OptRecepcionFiniquitoNotaria = objects.OptRecepcionFiniquitoNotaria?.ToString();
            instance.OptRecepcionFiniquitoStgoFirma = objects.OptRecepcionFiniquitoStgoFirma?.ToString();

            instance.OptSolValeVista = objects.OptSolValeVista?.ToString();
            instance.OptSolTef = objects.OptSolTef?.ToString();
            instance.OptSolCheque = objects.OptSolCheque?.ToString();

            instance.OptEnvioLegalizacion = objects.OptEnvioLegalizacion?.ToString();
            instance.OptRecepcionLegalizacion = objects.OptRecepcionLegalizacion?.ToString();

            instance.OptConfirmarFiniquito = objects.OptConfirmarFiniquito?.ToString();

            instance.OptPagarFiniquito = objects.OptPagarFiniquito?.ToString();

            instance.OptLiquidacionesSueldo = objects.OptLiquidacionesSueldo?.ToString();
            instance.OptCrearComplemento = objects.OptCrearComplemento?.ToString();

            instance.OptActualizarMontoAdministrativo = objects.OptActualizarMontoAdministrativo?.ToString();
            instance.OptRevertirValidacion = objects.OptRevertirValidacion?.ToString();
            instance.OptRevertirGestionEnvio = objects.OptRevertirGestionEnvio?.ToString();
            instance.OptRevertirLegalizacion = objects.OptRevertirLegalizacion?.ToString();
            instance.OptRevertirSolicitudPago = objects.OptRevertirSolicitudPago?.ToString();
            instance.OptRevertirConfirmacion = objects.OptRevertirConfirmacion?.ToString();
            instance.OptRevertirEmisionPago = objects.OptRevertirEmisionPago?.ToString();
            instance.OptComentarios = objects.OptComentarios?.ToString();
            instance.OptTerminarFiniquito = objects.OptTerminarFiniquito?.ToString();
            instance.OptFirmarFiniquito = objects.OptFirmarFiniquito?.ToString();
            instance.OptReprocesarDocumentosFiniquito = objects.OptReprocesarDocumentosFiniquito?.ToString();
            instance.OptValidarFinanzas = objects.OptValidarFinanzas?.ToString();
            instance.OptRevertirValidacionFinanzas = objects.OptRevertirValidacionFinanzas?.ToString();


            return instance;
        }
    }
}
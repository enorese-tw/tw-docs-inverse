// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.IServicioOperaciones
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.ServiceModel;

#nullable disable
namespace ServicioOperaciones;

[ServiceContract]
public interface IServicioOperaciones
{
  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetRenderLoaderCreacionSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudRenovaciones(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudContrato(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetRenderLoaderEnvioSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPlantillasCorreos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerProcesosSolicitudes(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudes(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerHojasCargaMasiva(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerCuentasSinAsignar(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerDatosSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerKam(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerCliente(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerJefeKam(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientesKam(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerMotivosAnulacion(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPaginatorProcesos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPaginatorSolicitudes(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetKamJefe(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetInformacionCliente(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudContratoAnulada(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudRenovacionAnulada(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerHeaderProcesos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerHeaderSolicitudes(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerRenderizadoDocAnexos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerRenderizadoCargaMasiva(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetIndiceEstadisticoSolicitudes(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientesNombre(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerDatosProceso(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetReporte(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetProcesoMasivo(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetValidacionProcesoMasivo(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCreaOActualizaFichaPersonal(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCreaContratoDeTrabajo(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCreaRenovacion(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCreaAnexo(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetAnularProceso(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetTerminarProceso(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetAnularSolicitud(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetLiberaTerminoSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPosiblesBajasMes(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetCC(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetArchivoBajasConfirmadas(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetBajasPorKam(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetKamPosiblesBajas();

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetBajasObtenerDatosSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetBajasObtenerBajasConfirmadas(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCrearBaja(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCambiarEstadoSolicitud(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerFiniquitos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones CrudMantencionFiniquitos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetGasto(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetGastoFinanzas(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetCerrarPeriodo(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetMantencionProveedores(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones SetMantencionPeriodo(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerConceptosGastos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerEstadoGasto(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerTiposDocumentos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerConceptoGastos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerBancos(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerTipoReembolso(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerGastos(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerTiposCuentas(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientes(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerProveedores(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientesAutompletar(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerPeriodoVigente(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerProveedoresAutocomplete(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerProveedoresRut(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClienteByNombre(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerPeriodo(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientesDistintos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerClientesDistintosNombre(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetObtenerNDocumento(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetExisteDocumento(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones CrudMantencionGastos(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPdf(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPrioridadesEstado(
    string[] Parametros,
    string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetHistorial(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioOperaciones.ServicioOperaciones GetPaginations(string[] Parametros, string[] valores);
}

// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.IServicioSoftland
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.ServiceModel;

#nullable disable
namespace ServicioSoftland;

[ServiceContract]
public interface IServicioSoftland
{
  [OperationContract]
  ServicioSoftland.ServicioSoftland GetValidarTrabajador(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetValidarPermitidoSolBj4(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetRutTrabajador(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetListarContratosFiniquitados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetContratoActivoBaja(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetObtenerAreaNegocio(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetObtenerCargo(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetCentroCosto(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetDatosBancariosTrabajador(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetClientes(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetBatchValidacionFichaCargada(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNObtenerUsuario(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetValidafechasqls(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNAdicionalEnrolamiento(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNEsJefatura(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNFicha(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNContratoActivo(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasPostTemp(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasPendientes(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasAdministrativos(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasAdicionales(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasUsadosfinal(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasProgresivosUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasAntiguedadUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTieneLegalesUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTieneAntiguedadUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTieneProgresivosUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNDiasLegalesUsados(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetEncargadoRRHH(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetExisteLasolicitud(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNtodosDatos(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTieneAdministrativos(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTienePostTemp(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetFichaconrut(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland GetVNTienePendientes(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetRegistrarSolicitud(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetAprobarSolicitud(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdateLegales(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdateProgresivos(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdateAntiguedad(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdateTemporada(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdatePendientes(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetUpdateAdministrativos(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetRegistrarSolicitudMarina(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetAprobarSolicitudMarina(string[] Parametros);

  [OperationContract]
  ServicioSoftland.ServicioSoftland SetRegistrarSolicitudMarinaAprobada(string[] Parametros);
}

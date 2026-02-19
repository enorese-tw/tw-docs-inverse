// Decompiled with JetBrains decompiler
// Type: ServicioAuth.IServicioAuth
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

using System.ServiceModel;

#nullable disable
namespace ServicioAuth;

[ServiceContract]
public interface IServicioAuth
{
  [OperationContract]
  ServicioAuth.ServicioAuth GetSignIn(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetBASE64(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetPemisionSectionsAccess(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetPemisionAccess(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetSectionRenderHtml(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetPlantillaCargaMasivaRender(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetDownloadPlantillaCargaMasiva(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetObtenerEmpresas(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetObtenerSitios(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth GetBarcodes(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth SetEnrolarColaborador(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth SetControlErroresSistemas(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth SetEnviaCorreoTeamworkInforma(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth SetBatchChangeContrato(string[] Parametros, string[] valores);

  [OperationContract]
  ServicioAuth.ServicioAuth CrudMantenedorNoticiasTeamwork(string[] Parametros, string[] valores);
}

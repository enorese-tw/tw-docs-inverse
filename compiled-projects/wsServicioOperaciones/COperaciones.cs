// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.COperaciones
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioOperaciones;

public class COperaciones
{
  public DataTable SetProcesoMasivo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_PROCESOMASIVO", Parameters);
  }

  public DataTable SetValidacionProcesoMasivo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_VALIDACIONPROCESOMASIVO", Parameters);
  }

  public DataTable SetCreaOActualizaFichaPersonal(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_CREAOACTUALIZAFICHAPERSONAL", Parameters);
  }

  public DataTable SetCreaContratoDeTrabajo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_CREACONTRATODETRABAJO", Parameters);
  }

  public DataTable SetCreaRenovacion(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_CREARENOVACION", Parameters);
  }

  public DataTable SetCreaAnexo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_CREARANEXO", Parameters);
  }

  public DataTable GetRenderLoaderCreacionSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_RENDERLOADERCREACIONSOLICITUD", Parameters);
  }

  public DataTable GetObtenerSolicitudRenovaciones(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDRENOVACIONES", Parameters);
  }

  public DataTable GetObtenerSolicitudContrato(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDCONTRATO", Parameters);
  }

  public DataTable GetRenderLoaderEnvioSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_RENDERLOADERENVIOSOLICITUD", Parameters);
  }

  public DataTable GetPlantillasCorreos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_PLANTILLASCORREOS", Parameters);
  }

  public DataTable GetObtenerProcesosSolicitudes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERPROCESOSSOLICITUDES", Parameters);
  }

  public DataTable GetObtenerSolicitudes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDES", Parameters);
  }

  public DataTable GetObtenerHojasCargaMasiva(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENCIONHOJASCARGAMASIVA", Parameters);
  }

  public DataTable GetObtenerCuentasSinAsignar(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENCIONCUENTASSINASIGNAR", Parameters);
  }

  public DataTable GetObtenerDatosSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERDATOSSOLICITUD", Parameters);
  }

  public DataTable GetObtenerKam(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERKAM", Parameters);
  }

  public DataTable GetObtenerCliente(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTE", Parameters);
  }

  public DataTable GetObtenerJefeKam(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERJEFEKAM", Parameters);
  }

  public DataTable GetObtenerClientesKam(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTESKAM", Parameters);
  }

  public DataTable GetObtenerMotivosAnulacion(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERMOTIVOSANULACION", Parameters);
  }

  public DataTable GetPaginatorProcesos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_PAGINATORPROCESOS", Parameters);
  }

  public DataTable GetPaginatorSolicitudes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_PAGINATORSOLICITUDES", Parameters);
  }

  public DataTable SetAnularProceso(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_ANULARPROCESO", Parameters);
  }

  public DataTable SetTerminarProceso(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_TERMINARPROCESO", Parameters);
  }

  public DataTable SetAnularSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_ANULARSOLICITUD", Parameters);
  }

  public DataTable SetLiberaTerminoSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_SET_REVERTIRTERMINOSOLICITUD", Parameters);
  }

  public DataTable GetKamJefe(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERKAMJEFE", Parameters);
  }

  public DataTable GetInformacionCliente(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERINFORMACIONCLIENTE", Parameters);
  }

  public DataTable GetObtenerSolicitudContratoAnulada(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDCONTRATOANULADA", Parameters);
  }

  public DataTable GetObtenerSolicitudRenovacionAnulada(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDRENOVACIONESANULADA", Parameters);
  }

  public DataTable GetObtenerHeaderProcesos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERHEADERPROCESOS", Parameters);
  }

  public DataTable GetObtenerHeaderSolicitudes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERHEADERSOLICITUDES", Parameters);
  }

  public DataTable GetObtenerRenderizadoDocAnexos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERRENDERIZADODOCANEXOS", Parameters);
  }

  public DataTable GetObtenerRenderizadoCargaMasiva(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_RENDERIZACARGAMASIVA", Parameters);
  }

  public DataTable GetIndiceEstadisticoSolicitudes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_INDICEESTADISTICOSOLICITUDES", Parameters);
  }

  public DataTable GetObtenerClientesNombre(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTESNOMBRES", Parameters);
  }

  public DataTable GetObtenerDatosProceso(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERDATOSPROCESO", Parameters);
  }

  public DataTable GetReporte(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_REPORTE", Parameters);
  }
}

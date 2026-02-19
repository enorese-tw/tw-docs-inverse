// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.CBajas
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioOperaciones;

public class CBajas
{
  public DataTable GetPosiblesBajasMes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERPOSIBLESBAJAS", Parameters);
  }

  public DataTable GetCC(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERCC", Parameters);
  }

  public DataTable GetArchivoBajasConfirmadas(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERSOLICITUDBAJAS", Parameters);
  }

  public DataTable GetKamPosiblesBajas()
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERKAMPOSIBLESBAJAS");
  }

  public DataTable GetBajasPorKam(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERPOSIBLESBAJASPORKAM", Parameters);
  }

  public DataTable SetCrearBaja(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_SET_CREARBAJA", Parameters);
  }

  public DataTable SetCambiarEstadoSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_SET_CAMBIARESTADOSOLICITUD", Parameters);
  }

  public DataTable GetBajasObtenerDatosSolicitud(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERDATOSSOLICITUD", Parameters);
  }

  public DataTable GetBajasObtenerBajasConfirmadas(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_BJ_GET_OBTENERBAJASCONFIRMADAS", Parameters);
  }
}

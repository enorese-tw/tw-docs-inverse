// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.CFinanzas
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioOperaciones;

public class CFinanzas
{
  public DataTable GetObtenerConceptosGastos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERCONCEPTOSGASTOS", Parameters);
  }

  public DataTable GetObtenerEstadoGasto(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERESTADOGASTO", Parameters);
  }

  public DataTable GetObtenerTiposDocumentos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERTIPOSDOCUMENTOS", Parameters);
  }

  public DataTable GetObtenerConceptoGastos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERCONCEPTOSGASTOS", Parameters);
  }

  public DataTable GetObtenerBancos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERBANCOS", Parameters);
  }

  public DataTable GetObtenerTipoReembolso(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERTIPOREEMBOLSO", Parameters);
  }

  public DataTable GetObtenerGastos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERGASTOS", Parameters);
  }

  public DataTable GetObtenerTiposCuentas(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERTIPOSCUENTAS", Parameters);
  }

  public DataTable GetObtenerClientes(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTES", Parameters);
  }

  public DataTable SetGasto(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_SET_CREARGASTO", Parameters);
  }

  public DataTable SetGastoFinanzas(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_SET_CREARGASTOFINANZAS", Parameters);
  }

  public DataTable GetObtenerProveedores(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERPROVEEDORES", Parameters);
  }

  public DataTable GetObtenerClientesAutompletar(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTESAUTOCOMPLETE", Parameters);
  }

  public DataTable GetObtenerPeriodoVigente(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERPERIODOVIGENTE", Parameters);
  }

  public DataTable GetObtenerProveedoresAutocomplete(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERPROVEEDORESAUTOCOMPLETE", Parameters);
  }

  public DataTable GetObtenerProveedoresRut(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERPROVEEDORESRUT", Parameters);
  }

  public DataTable GetObtenerClienteByNombre(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTENOMBREBYNOMBRE", Parameters);
  }

  public DataTable SetCerrarPeriodo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_SET_CERRARPERIODO", Parameters);
  }

  public DataTable GetObtenerPeriodo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERPERIODO", Parameters);
  }

  public DataTable GetObtenerClientesDistintos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTESDISTINTOS", Parameters);
  }

  public DataTable GetObtenerClientesDistintosNombre(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_KM_GET_OBTENERCLIENTESDISTINTOSNOMBRE", Parameters);
  }

  public DataTable GetObtenerNDocumento(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_OBTENERNDOCUMENTO", Parameters);
  }

  public DataTable GetExisteDocumento(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_GET_EXISTEDOCUMENTO", Parameters);
  }

  public DataTable SetMantencionProveedores(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_CRUD_MANTENCIONPROVEEDORES", Parameters);
  }

  public DataTable SetMantencionPeriodo(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_CRUD_MANTENEDOR_PERIODO", Parameters);
  }

  public DataTable CrudMantencionGastos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FZ_CRUD_MANTENEDOR_GASTOS", Parameters);
  }
}

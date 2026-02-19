// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.CFiniquitos
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioOperaciones;

public class CFiniquitos
{
  public DataTable GetObtenerFiniquitos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FN_GET_OBTENERFINIQUITOS", Parameters);
  }

  public DataTable CrudMantencionFiniquitos(List<Parametro> parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("TW_SA");
    DataTable dataTable = new DataTable();
    List<Parametro> Parameters = parametros;
    return sqlServerDbHelper.ExecuteStoreProcedure("SP_FN_CRUD_MANTENCIONFINIQUITOS", Parameters);
  }
}

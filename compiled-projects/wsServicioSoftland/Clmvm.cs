// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.CImvm
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Data;

#nullable disable
namespace ServicioSoftland;

public class CImvm
{
  public DataTable SetRegistrarSolicitudMarina(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("SOFTLAND_IMVM");
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("INSERT INTO softland.sw_vacsolic(ficha,FsDesde,FsHasta,Estado,nDias,Observ,Proceso)values('{0}','{1}','{2}','{3}',{4},'{5}','{6}')", parameters);
  }

  public DataTable SetAprobarSolicitudMarina(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("SOFTLAND_IMVM");
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("update softland.sw_vacsolic set estado = '{0}', FaDesde = '{1}', FaHasta = '{2}', NDiasAp = {3} where ficha = '{4}' and FsDesde = '{5}' and FsHasta = '{6}'", parameters);
  }

  public DataTable SetRegistrarSolicitudMarinaAprobada(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper("SOFTLAND_IMVM");
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("INSERT INTO softland.sw_vacsolic(ficha,FsDesde,FsHasta,Estado,nDias,Observ,Proceso, NDiasAp, FaDesde, FaHasta)values('{0}','{1}','{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}')", parameters);
  }
}

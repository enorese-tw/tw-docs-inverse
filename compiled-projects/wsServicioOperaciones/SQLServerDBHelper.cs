// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.SQLServerDBHelper
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace ServicioOperaciones;

public class SQLServerDBHelper
{
  private string _conexion;

  public SQLServerDBHelper(string CLIENT)
  {
    DatosXML datosXml = new DatosXML();
    switch (CLIENT)
    {
      case "TW_SA":
        this._conexion = $"Server={datosXml.ServidorBaseDeDatosSA};Database={datosXml.BaseDeDatos};User Id={datosXml.UsuarioBDSA};Password={datosXml.ClaveBDSA};Connection Timeout=0";
        break;
    }
  }

  public DataTable ExecuteStoreProcedure(
    string StoreProcedure,
    List<Parametro> Parameters = null,
    string TableName = "resultado")
  {
    SqlConnection selectConnection;
    DataTable dataTable;
    using (selectConnection = new SqlConnection(this._conexion))
    {
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(StoreProcedure, selectConnection);
      sqlDataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
      sqlDataAdapter.SelectCommand.CommandTimeout = 3600;
      if (Parameters != null)
      {
        foreach (Parametro parameter in Parameters)
          sqlDataAdapter.SelectCommand.Parameters.AddWithValue(parameter.ParameterName, (object) parameter.ParameterValue);
      }
      dataTable = new DataTable(TableName);
      sqlDataAdapter.Fill(dataTable);
    }
    return dataTable;
  }

  public DataTable ExecuteQuery(string query, string[] parameters = null, string TableName = "resultado")
  {
    string empty = string.Empty;
    SqlConnection selectConnection;
    DataTable dataTable;
    using (selectConnection = new SqlConnection(this._conexion))
    {
      SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(parameters == null ? string.Format(query) : empty + string.Format(query, (object[]) parameters), selectConnection);
      sqlDataAdapter.SelectCommand.CommandType = CommandType.Text;
      dataTable = new DataTable(TableName);
      sqlDataAdapter.Fill(dataTable);
    }
    return dataTable;
  }
}

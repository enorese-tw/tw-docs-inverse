// Decompiled with JetBrains decompiler
// Type: ServicioAuth.SQLServerDBHelper
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace ServicioAuth;

public class SQLServerDBHelper
{
  private string _conexion;

  public SQLServerDBHelper(string CLIENT)
  {
    DatosXML datosXml = new DatosXML();
    if (!(CLIENT == "TW_SA"))
      return;
    this._conexion = $"Server={datosXml.ServidorBaseDeDatosSA};Database={datosXml.BaseDeDatos};User Id={datosXml.UsuarioBDSA};Password={datosXml.ClaveBDSA};Connection Timeout=0";
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

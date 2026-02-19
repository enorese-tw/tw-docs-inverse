// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.SQLServerDBHelper
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#nullable disable
namespace ServicioSoftland;

public class SQLServerDBHelper
{
  private string _conexion;
  private string _listener;
  private string _basededatos;
  private string _usuario;
  private string _clave;

  public SQLServerDBHelper(string CLIENT)
  {
    DatosXML datosXml = new DatosXML();
    if (CLIENT == null)
      return;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(CLIENT))
    {
      case 415113716:
        if (!(CLIENT == "SOFTLAND_VNEVADO"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADO};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 750512131:
        if (!(CLIENT == "SOFTLAND_OUT"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosOUT};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 1139011785:
        if (!(CLIENT == "SOFTLAND_TWC"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosCONSULTORA};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 1529943807:
        if (!(CLIENT == "SOFTLAND_VNEVADOINMOB"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADOINMOB};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 2866945346:
        if (!(CLIENT == "SOFTLAND_VNEVADOSOCINMOB"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADOSOCINMOB};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 2966720762:
        if (!(CLIENT == "SOFTLAND_IMVM"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosIMVM};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 3286227124:
        if (!(CLIENT == "SOFTLAND_VNEVADOESCUELA"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADOESCUELA};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 3839049435:
        if (!(CLIENT == "SOFTLAND_VNEVADOSERVINMOB"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADOSERVINMOB};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 4116098334:
        if (!(CLIENT == "SOFTLAND_VNEVADOLICANCABU"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosVNEVADOLICANCABU};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
        break;
      case 4201529779:
        if (!(CLIENT == "SOFTLAND_EST"))
          break;
        this._conexion = $"Data Source={datosXml.SoftlandServidorDB};Initial Catalog={datosXml.SoftlandBaseDeDatosEST};Persist Security Info=True;User ID={datosXml.SoftlandUsuarioDB};Password={datosXml.SoftlandClaveDB}";
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

// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.CSoftland
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Data;

#nullable disable
namespace ServicioSoftland;

public class CSoftland
{
  public DataTable GetValidarTrabajador(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT CASE WHEN COUNT(*) > 0 THEN 'S' ELSE 'N' END 'Existe' FROM softland.sw_Personal WHERE rut LIKE '{1}'", parameters);
  }

  public DataTable GetValidarPermitidoSolBj4(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT CASE WHEN COUNT(*) > 0 THEN 'N' ELSE 'S' END 'ESBAJA' FROM softland.sw_Personal p INNER JOIN softland.sw_estudiosup e ON e.codEstudios like p.codEstudios WHERE e.descripcion NOT LIKE '%BAJA%' and rut LIKE '{1}' AND ficha LIKE '{2}'", parameters);
  }

  public DataTable GetRutTrabajador(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT DISTINCT p.rut, p.nombres as nombres FROM softland.sw_personal p INNER JOIN softland.sw_estudiosup e ON e.codEstudios LIKE p.codEstudios WHERE p.rut LIKE '{1}'", parameters);
  }

  public DataTable GetListarContratosFiniquitados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT p.ficha, p.rut FROM softland.sw_Personal p INNER JOIN softland.sw_estudiosup e ON e.codEstudios like p.codEstudios WHERE e.descripcion NOT LIKE '%BAJA%' and rut LIKE '{1}' ORDER BY fechaingreso DESC", parameters);
  }

  public DataTable GetContratoActivoBaja(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string empty = string.Empty;
    string query = !(parametros[2] != "") ? "SELECT p.ficha as ficha, p.rut, p.nombres as nombres, e.descripcion as codEstudios , CASE WHEN p.fechaIngreso IS NOT NULL THEN CASE WHEN DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' +  CASE WHEN DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' + CAST(DATEPART(YEAR, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE '' END 'fechaIngreso',  CASE WHEN p.FecTermContrato IS NOT NULL THEN CASE WHEN DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) < 10 THEN     '0' + CAST(DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) END + '-' + CASE WHEN DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' + CAST(DATEPART(YEAR, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) ELSE '' END 'FecTermContrato' FROM softland.sw_personal p INNER JOIN softland.sw_estudiosup e ON e.codEstudios LIKE p.codEstudios WHERE p.rut LIKE '{1}' ORDER BY fectermcontrato DESC" : "SELECT p.ficha as ficha, p.rut, p.nombres as nombres, e.descripcion as codEstudios , CASE WHEN p.fechaIngreso IS NOT NULL THEN CASE WHEN DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(DAY, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' +  CASE WHEN DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' + CAST(DATEPART(YEAR, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE '' END 'fechaIngreso',  CASE WHEN p.FecTermContrato IS NOT NULL THEN CASE WHEN DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) < 10 THEN     '0' + CAST(DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(DAY, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) END + '-' + CASE WHEN DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) < 10 THEN     '0' + CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) ELSE     CAST(DATEPART(MONTH, CAST(p.fechaIngreso AS DATE)) AS VARCHAR(50)) END + '-' + CAST(DATEPART(YEAR, CAST(p.FecTermContrato AS DATE)) AS VARCHAR(50)) ELSE '' END 'FecTermContrato' FROM softland.sw_personal p INNER JOIN softland.sw_estudiosup e ON e.codEstudios LIKE p.codEstudios WHERE p.rut LIKE '{1}' AND p.ficha LIKE '{2}' ORDER BY fectermcontrato DESC";
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(query, parameters);
  }

  public DataTable GetObtenerAreaNegocio(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string empty = string.Empty;
    string query = !(parametros[2] != "") ? "SELECT softland.sw_areanegper.ficha, softland.sw_areanegper.codArn, softland.cwtaren.DesArn FROM softland.cwtaren INNER JOIN softland.sw_areanegper ON softland.cwtaren.CodArn = softland.sw_areanegper.codArn INNER JOIN softland.sw_personal ON softland.sw_personal.ficha = softland.sw_areanegper.ficha WHERE softland.sw_personal.rut LIKE '{1}' ORDER BY softland.sw_areanegper.vigHasta DESC" : "SELECT softland.sw_areanegper.ficha, softland.sw_areanegper.codArn, softland.cwtaren.DesArn FROM softland.cwtaren INNER JOIN softland.sw_areanegper ON softland.cwtaren.CodArn = softland.sw_areanegper.codArn INNER JOIN softland.sw_personal ON softland.sw_personal.ficha = softland.sw_areanegper.ficha WHERE softland.sw_personal.rut LIKE '{1}' AND softland.sw_personal.ficha LIKE '{2}' ORDER BY softland.sw_areanegper.vigHasta DESC";
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(query, parameters);
  }

  public DataTable GetObtenerCargo(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT TOP(1) softland.sw_codineper.ficha, softland.sw_codineper.codigo, softland.sw_codine.glosa, softland.sw_cargoper.carCod, softland.cwtcarg.CarNom  FROM softland.sw_codineper INNER JOIN softland.sw_codine ON softland.sw_codineper.codigo = softland.sw_codine.CodIne INNER JOIN softland.sw_cargoper ON softland.sw_codineper.ficha = softland.sw_cargoper.ficha INNER JOIN  softland.cwtcarg ON softland.sw_cargoper.carCod = softland.cwtcarg.CarCod WHERE (softland.sw_codineper.ficha = '{1}')", parameters);
  }

  public DataTable GetCentroCosto(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT p.ficha as ficha, a.codarn 'codarn', c.desarn 'desarn', cs.DescCC 'DescCC' FROM softland.sw_personal p INNER JOIN softland.sw_areanegper a ON p.ficha = a.ficha INNER JOIN softland.cwtaren c ON a.codArn = c.codArn INNER JOIN softland.sw_ccostoper ct ON ct.ficha = p.ficha INNER JOIN softland.cwtccos cs on cs.CodiCC = ct.CodiCC WHERE p.ficha = '{1}'", parameters);
  }

  public DataTable GetDatosBancariosTrabajador(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT descripcion, swtd.TipoDepo, swp.numCtaCte FROM softland.sw_personal swp INNER JOIN softland.sw_banco_suc swbs ON swbs.codBancoSuc = swp.codBancoSuc INNER JOIN softland.sw_tipodeposito swtd ON swtd.CodTipDep = swp.TipoDeposito WHERE swp.rut LIKE '{1}' AND swp.ficha like '{2}'", parameters);
  }

  public DataTable GetClientes(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    return sqlServerDbHelper.ExecuteQuery("select DesArn from softland.cwtaren");
  }

  public DataTable GetBatchValidacionFichaCargada(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("select CASE WHEN COUNT(*) > 0 THEN 'S' ELSE 'N' END 'ExisteFicha' from softland.sw_personal WHERE FICHA LIKE '{1}'", parameters);
  }
}

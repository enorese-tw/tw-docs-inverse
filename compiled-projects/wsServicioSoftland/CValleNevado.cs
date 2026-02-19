// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.CValleNevado
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Data;

#nullable disable
namespace ServicioSoftland;

public class CValleNevado
{
  public DataTable GetVNObtenerUsuario(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT CASE WHEN COUNT(*)>0 THEN 'S' ELSE 'N' END 'EXISTE' FROM softland.sw_personal WHERE rut like '{1}'", parameters);
  }

  public DataTable GetVNAdicionalEnrolamiento(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT nombres, email FROM softland.sw_personal WHERE rut like '{1}'", parameters);
  }

  public DataTable GetVNtodosDatos(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT ficha,nombres, CAST(DATEPART(day ,fechaIngreso) as varchar(max)) +'-'+ CAST(DATEPART(MONTH ,fechaIngreso) AS VARCHAR(100))+'-'+CAST(DATEPART(YEAR ,fechaIngreso) AS VARCHAR(100)) 'fechaIngreso', anoOtraEm,CAST(DATEPART(day ,FecCertVacPro) as varchar(max)) +'-'+ CAST(DATEPART(MONTH ,FecCertVacPro) AS VARCHAR(100))+'-'+CAST(DATEPART(YEAR ,FecCertVacPro) AS VARCHAR(100)) 'FecCertVacPro' FROM softland.SW_PERSONAL where fechaFiniquito ='31-12-9999'", parameters);
  }

  public DataTable GetVNEsJefatura(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(JefeDirecto) as Jefe FROM softland.sw_personal WHERE JefeDirecto like '{1}'", parameters);
  }

  public DataTable GetVNFicha(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT ficha as Ficha FROM softland.sw_personal WHERE rut like '{1}' AND FecTermContrato IS  NULL UNION SELECT FICHA as ficha FROM softland.sw_personal WHERE rut like '{1}' AND FecTermContrato >= getdate()", parameters);
  }

  public DataTable GetVNDiasUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT SUM(NDiasAp) as DiasVacaciones FROM[softland].[sw_vacsolic] where Ficha like '{1}'", parameters);
  }

  public DataTable GetVNDiasUsadosfinal(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT SUM(NDiasAp) as DiasVacaciones FROM [softland].[sw_vacsolic] where Ficha like (SELECT top(1) FICHA FROM [softland].[sw_personal] WHERE RUT LIKE '{1}' and codEstudios like 'IND' )", parameters);
  }

  public DataTable GetVNContratoActivo(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT ficha ,rut , nombres, CAST(fechaIngreso as datetime) 'fechaIngreso', FecTermContrato,jefeDirecto, AnoOtraEm,Email,FecCertVacPro, (SELECT CASE WHEN valor = '1' then 'CORDILLERA' ELSE 'VITACURA' END 'sucursal' FROM softland.sw_variablepersona where ficha like '{1}'  and codVariable LIKE 'P355' and mes = (select IndiceMes from softland.sw_vsnpRetornaFechaMesExistentes  where fechames = (select max(FechaMes)from softland.sw_vsnpRetornaFechaMesExistentes))) 'sucursal' FROM softland.sw_personal WHERE FICHA LIKE '{1}' ", parameters);
  }

  public DataTable GetVNDiasPostTemp(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor FROM [VNEVADO].[softland].[sw_variablepersona] WHERE ficha LIKE '{1}' AND codVariable LIKE'P356' AND mes IN (SELECT IndiceMes FROM [VNEVADO].[softland].[sw_vsnpRetornaFechaMesExistentes]  WHERE CONCAT(YEAR(FechaMes), MONTH(FechaMes)) = CONCAT(YEAR(GETDATE()),MONTH(GETDATE())))", parameters);
  }

  public DataTable GetVNDiasLegalesUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P360'", parameters);
  }

  public DataTable GetVNDiasProgresivosUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P361'", parameters);
  }

  public DataTable GetVNDiasAntiguedadUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P362'", parameters);
  }

  public DataTable GetVNTieneLegalesUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P360'", parameters);
  }

  public DataTable GetVNTieneProgresivosUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P361'", parameters);
  }

  public DataTable GetVNTieneAntiguedadUsados(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P362'", parameters);
  }

  public DataTable GetVNTienePostTemp(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P356'", parameters);
  }

  public DataTable GetVNDiasPendientes(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P357'", parameters);
  }

  public DataTable GetVNTienePendientes(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P357'", parameters);
  }

  public DataTable GetVNDiasAdministrativos(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT valor from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P358'", parameters);
  }

  public DataTable GetVNTieneAdministrativos(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT count(*) as contador from softland.sw_variablepersona where ficha like '{1}' and codVariable like 'P358'", parameters);
  }

  public DataTable GetVNDiasAdicionales(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT ndias as DiasVacacionesAdicionales FROM [softland].[sw_vacadic] where Ficha like {1}", parameters);
  }

  public DataTable GetEncargadoRRHH(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("Select Email From softland.SW_ENCARGREMUNE", parameters);
  }

  public DataTable GetExisteLasolicitud(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("select COUNT(*) as conteo from softland.sw_vacsolic WHERE ficha like '{1}' and FsDesde like cast('{2}' as datetime) and FsHasta like cast('{3}' as datetime)", parameters);
  }

  public DataTable GetValidafechasqls(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("SELECT CASE WHEN(SELECT COUNT(*) FROM softland.sw_vacsolic WHERE CAST('{2}' AS DATE) BETWEEN CAST(FsDesde AS DATETIME) AND CAST(FsHasta AS DATETIME) AND Ficha LIKE '{1}') = 0 THEN CASE WHEN(SELECT COUNT(*) FROM softland.sw_vacsolic WHERE CAST('{3}' AS DATE) BETWEEN CAST(FsDesde AS DATETIME) AND CAST(FsHasta AS DATETIME) AND Ficha LIKE '{1}') = 0 THEN CASE WHEN(SELECT COUNT(*) FROM softland.sw_vacsolic WHERE CAST(FsDesde AS DATE) BETWEEN CAST('{2}' AS DATE) AND CAST('{3}' AS DATE) AND Ficha LIKE '{1}') = 0 THEN CASE WHEN(SELECT COUNT(*) FROM softland.sw_vacsolic WHERE CAST(FsHasta AS DATE) BETWEEN CAST('{2}' AS DATE) AND CAST('{3}' AS DATE) AND Ficha LIKE '{1}') = 0 THEN 'S' ELSE 'N' END ELSE 'N' END ELSE 'N' END ELSE 'N' END 'Disponible'", parameters);
  }

  public DataTable GetTraerTodasVacaciones(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("select f from softland.sw_vacsolic WHERE ficha like '{1}')", parameters);
  }

  public DataTable GetFichaconrut(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("select ficha from softland.sw_personal where rut like '{1}'", parameters);
  }

  public DataTable SetRegistrarSolicitud(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("INSERT INTO softland.sw_vacsolic(ficha,FsDesde,FsHasta,Estado,nDias,Observ,Proceso)values('{1}','{2}','{3}','{4}',{5},'{6}','{7}')", parameters);
  }

  public DataTable SetAprobarSolicitud(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery("update softland.sw_vacsolic set estado = '{1}', FaDesde = '{2}', FaHasta = '{3}', NDiasAp = {4} where ficha = '{5}' and FsDesde = '{6}' and FsHasta = '{7}'", parameters);
  }

  public DataTable SetUpdateLegales(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P360' ", parameters);
  }

  public DataTable SetUpdateProgresivos(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P361' ", parameters);
  }

  public DataTable SetUpdateAntiguedad(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P362' ", parameters);
  }

  public DataTable SetUpdateTemporada(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P356' ", parameters);
  }

  public DataTable SetUpdatePendientes(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P357' ", parameters);
  }

  public DataTable SetUpdateAdministrativos(string[] parametros)
  {
    SQLServerDBHelper sqlServerDbHelper = new SQLServerDBHelper(parametros[0]);
    DataTable dataTable = new DataTable();
    string[] parameters = parametros;
    return sqlServerDbHelper.ExecuteQuery(" update softland.sw_variablepersona set valor = '{1}' where ficha like '{1}' and codvariable like 'P358' ", parameters);
  }
}

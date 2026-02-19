// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.wsServicioOperaciones
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioOperaciones;

public class wsServicioOperaciones : IServicioOperaciones
{
  private List<Parametro> ArrayToListParametros(string[] Parametros, string[] valores)
  {
    List<Parametro> listParametros = new List<Parametro>();
    for (int index = 0; index < Parametros.Length; ++index)
      listParametros.Add(new Parametro(Parametros[index], valores[index]));
    return listParametros;
  }

  public ServicioOperaciones.ServicioOperaciones GetRenderLoaderCreacionSolicitud(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetRenderLoaderCreacionSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudRenovaciones(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerSolicitudRenovaciones(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudContrato(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerSolicitudContrato(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetRenderLoaderEnvioSolicitud(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetRenderLoaderEnvioSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPlantillasCorreos(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetPlantillasCorreos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerProcesosSolicitudes(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerProcesosSolicitudes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudes(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerSolicitudes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerHojasCargaMasiva(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerHojasCargaMasiva(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerCuentasSinAsignar(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerCuentasSinAsignar(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerDatosSolicitud(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerDatosSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerJefeKam(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerJefeKam(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerKam(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerKam(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerCliente(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerCliente(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientesKam(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerClientesKam(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerMotivosAnulacion(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerMotivosAnulacion(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPaginatorProcesos(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetPaginatorProcesos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPaginatorSolicitudes(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetPaginatorSolicitudes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetKamJefe(string[] Parametros, string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetKamJefe(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetInformacionCliente(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetInformacionCliente(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudContratoAnulada(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerSolicitudContratoAnulada(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerSolicitudRenovacionAnulada(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerSolicitudRenovacionAnulada(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerHeaderProcesos(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerHeaderProcesos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerHeaderSolicitudes(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerHeaderSolicitudes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerRenderizadoDocAnexos(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerRenderizadoDocAnexos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerRenderizadoCargaMasiva(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerRenderizadoCargaMasiva(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetIndiceEstadisticoSolicitudes(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetIndiceEstadisticoSolicitudes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientesNombre(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerClientesNombre(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerDatosProceso(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetObtenerDatosProceso(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetReporte(string[] Parametros, string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.GetReporte(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetProcesoMasivo(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetProcesoMasivo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetValidacionProcesoMasivo(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetValidacionProcesoMasivo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCreaOActualizaFichaPersonal(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetCreaOActualizaFichaPersonal(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCreaContratoDeTrabajo(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetCreaContratoDeTrabajo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCreaRenovacion(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetCreaRenovacion(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCreaAnexo(string[] Parametros, string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetCreaAnexo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetAnularProceso(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetAnularProceso(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetTerminarProceso(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetTerminarProceso(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetAnularSolicitud(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetAnularSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetLiberaTerminoSolicitud(
    string[] Parametros,
    string[] valores)
  {
    COperaciones coperaciones = new COperaciones();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          coperaciones.SetLiberaTerminoSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPosiblesBajasMes(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetPosiblesBajasMes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetCC(string[] Parametros, string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetCC(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetArchivoBajasConfirmadas(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetArchivoBajasConfirmadas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetKamPosiblesBajas()
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetKamPosiblesBajas()
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetBajasPorKam(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetBajasPorKam(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetBajasObtenerDatosSolicitud(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetBajasObtenerDatosSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetBajasObtenerBajasConfirmadas(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.GetBajasObtenerBajasConfirmadas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCrearBaja(string[] Parametros, string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.SetCrearBaja(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCambiarEstadoSolicitud(
    string[] Parametros,
    string[] valores)
  {
    CBajas cbajas = new CBajas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cbajas.SetCambiarEstadoSolicitud(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerFiniquitos(
    string[] Parametros,
    string[] valores)
  {
    CFiniquitos cfiniquitos = new CFiniquitos();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfiniquitos.GetObtenerFiniquitos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones CrudMantencionFiniquitos(
    string[] Parametros,
    string[] valores)
  {
    CFiniquitos cfiniquitos = new CFiniquitos();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfiniquitos.CrudMantencionFiniquitos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerConceptosGastos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerConceptosGastos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerEstadoGasto(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerEstadoGasto(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerTiposDocumentos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerTiposDocumentos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerConceptoGastos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerConceptoGastos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerBancos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerBancos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerTipoReembolso(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerTipoReembolso(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerGastos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerGastos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerTiposCuentas(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerTiposCuentas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientes(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerClientes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerProveedores(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerProveedores(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientesAutompletar(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerClientesAutompletar(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerPeriodoVigente(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerPeriodoVigente(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerProveedoresAutocomplete(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerProveedoresAutocomplete(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerProveedoresRut(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerProveedoresRut(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClienteByNombre(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerClienteByNombre(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerNDocumento(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerNDocumento(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetGasto(string[] Parametros, string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.SetGasto(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetGastoFinanzas(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.SetGastoFinanzas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetCerrarPeriodo(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.SetCerrarPeriodo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerPeriodo(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerPeriodo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientesDistintos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerClientesDistintos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetObtenerClientesDistintosNombre(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetObtenerClientesDistintosNombre(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetExisteDocumento(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.GetExisteDocumento(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetMantencionProveedores(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.SetMantencionProveedores(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones SetMantencionPeriodo(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.SetMantencionPeriodo(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones CrudMantencionGastos(
    string[] Parametros,
    string[] valores)
  {
    CFinanzas cfinanzas = new CFinanzas();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cfinanzas.CrudMantencionGastos(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPdf(string[] Parametros, string[] valores)
  {
    CTeamwork cteamwork = new CTeamwork();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cteamwork.GetPdf(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPrioridadesEstado(
    string[] Parametros,
    string[] valores)
  {
    CTeamwork cteamwork = new CTeamwork();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cteamwork.GetPrioridadesEstado(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetHistorial(string[] Parametros, string[] valores)
  {
    CTeamwork cteamwork = new CTeamwork();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cteamwork.GetHistorial(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioOperaciones.ServicioOperaciones GetPaginations(
    string[] Parametros,
    string[] valores)
  {
    CTeamwork cteamwork = new CTeamwork();
    return new ServicioOperaciones.ServicioOperaciones()
    {
      Table = new DataSet()
      {
        Tables = {
          cteamwork.GetPaginations(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }
}

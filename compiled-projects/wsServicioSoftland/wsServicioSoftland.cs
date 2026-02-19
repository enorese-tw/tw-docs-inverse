// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.wsServicioSoftland
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioSoftland;

public class wsServicioSoftland : IServicioSoftland
{
  private List<Parametro> ArrayToListParametros(string[] Parametros, string[] valores)
  {
    List<Parametro> listParametros = new List<Parametro>();
    for (int index = 0; index < Parametros.Length; ++index)
      listParametros.Add(new Parametro(Parametros[index], valores[index]));
    return listParametros;
  }

  public ServicioSoftland.ServicioSoftland GetValidarTrabajador(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetValidarTrabajador(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetValidarPermitidoSolBj4(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetValidarPermitidoSolBj4(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetRutTrabajador(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetRutTrabajador(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetListarContratosFiniquitados(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetListarContratosFiniquitados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetContratoActivoBaja(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetContratoActivoBaja(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetObtenerAreaNegocio(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetObtenerAreaNegocio(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetObtenerCargo(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetObtenerCargo(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetCentroCosto(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetCentroCosto(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetDatosBancariosTrabajador(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetDatosBancariosTrabajador(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetClientes(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetClientes(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetBatchValidacionFichaCargada(string[] Parametros)
  {
    CSoftland csoftland = new CSoftland();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          csoftland.GetBatchValidacionFichaCargada(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNObtenerUsuario(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNObtenerUsuario(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetValidafechasqls(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetValidafechasqls(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNAdicionalEnrolamiento(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNAdicionalEnrolamiento(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNEsJefatura(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNEsJefatura(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasUsadosfinal(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasUsadosfinal(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNFicha(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNFicha(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNContratoActivo(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNContratoActivo(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasPostTemp(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasPostTemp(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasLegalesUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasLegalesUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasProgresivosUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasProgresivosUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetTraerTodasVacaciones(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetTraerTodasVacaciones(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasAntiguedadUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasAntiguedadUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTieneLegalesUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTieneLegalesUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTieneProgresivosUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTieneProgresivosUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTieneAntiguedadUsados(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTieneAntiguedadUsados(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasPendientes(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasPendientes(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasAdministrativos(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasAdministrativos(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNDiasAdicionales(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNDiasAdicionales(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetEncargadoRRHH(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetEncargadoRRHH(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetExisteLasolicitud(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetExisteLasolicitud(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNtodosDatos(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNtodosDatos(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTienePendientes(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTienePendientes(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTienePostTemp(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTienePostTemp(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetVNTieneAdministrativos(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetVNTieneAdministrativos(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland GetFichaconrut(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.GetFichaconrut(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetRegistrarSolicitudMarina(string[] Parametros)
  {
    CImvm cimvm = new CImvm();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cimvm.SetRegistrarSolicitudMarina(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetAprobarSolicitudMarina(string[] Parametros)
  {
    CImvm cimvm = new CImvm();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cimvm.SetAprobarSolicitudMarina(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetRegistrarSolicitudMarinaAprobada(string[] Parametros)
  {
    CImvm cimvm = new CImvm();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cimvm.SetRegistrarSolicitudMarinaAprobada(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetRegistrarSolicitud(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetRegistrarSolicitud(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetAprobarSolicitud(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetAprobarSolicitud(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdateLegales(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdateLegales(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdateProgresivos(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdateProgresivos(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdateAntiguedad(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdateAntiguedad(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdateTemporada(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdateTemporada(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdatePendientes(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdatePendientes(Parametros)
        }
      }
    };
  }

  public ServicioSoftland.ServicioSoftland SetUpdateAdministrativos(string[] Parametros)
  {
    CValleNevado cvalleNevado = new CValleNevado();
    return new ServicioSoftland.ServicioSoftland()
    {
      Table = new DataSet()
      {
        Tables = {
          cvalleNevado.SetUpdateAdministrativos(Parametros)
        }
      }
    };
  }
}

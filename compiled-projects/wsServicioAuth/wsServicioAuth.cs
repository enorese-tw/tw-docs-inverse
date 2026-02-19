// Decompiled with JetBrains decompiler
// Type: ServicioAuth.wsServicioAuth
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

using System.Collections.Generic;
using System.Data;

#nullable disable
namespace ServicioAuth;

public class wsServicioAuth : IServicioAuth
{
  private List<Parametro> ArrayToListParametros(string[] Parametros, string[] valores)
  {
    List<Parametro> listParametros = new List<Parametro>();
    for (int index = 0; index < Parametros.Length; ++index)
      listParametros.Add(new Parametro(Parametros[index], valores[index]));
    return listParametros;
  }

  public ServicioAuth.ServicioAuth GetSignIn(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetSignIn(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetBASE64(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetBASE64(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetPemisionSectionsAccess(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetPemisionSectionsAccess(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetPemisionAccess(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetPemisionAccess(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetSectionRenderHtml(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetSectionRenderHtml(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetPlantillaCargaMasivaRender(
    string[] Parametros,
    string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetPlantillaCargaMasivaRender(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetDownloadPlantillaCargaMasiva(
    string[] Parametros,
    string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetDownloadPlantillaCargaMasiva(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetObtenerEmpresas(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetObtenerEmpresas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetObtenerSitios(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetObtenerSitios(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth GetBarcodes(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.GetBarcodes(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth SetEnrolarColaborador(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.SetEnrolarColaborador(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth SetControlErroresSistemas(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.SetControlErroresSistemas(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth SetEnviaCorreoTeamworkInforma(
    string[] Parametros,
    string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.SetEnviaCorreoTeamworkInforma(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth SetBatchChangeContrato(string[] Parametros, string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.SetBatchChangeContrato(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }

  public ServicioAuth.ServicioAuth CrudMantenedorNoticiasTeamwork(
    string[] Parametros,
    string[] valores)
  {
    CAuth cauth = new CAuth();
    return new ServicioAuth.ServicioAuth()
    {
      Table = new DataSet()
      {
        Tables = {
          cauth.CrudMantenedorNoticiasTeamwork(this.ArrayToListParametros(Parametros, valores))
        }
      }
    };
  }
}

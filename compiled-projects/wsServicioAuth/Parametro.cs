// Decompiled with JetBrains decompiler
// Type: ServicioAuth.Parametro
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

#nullable disable
namespace ServicioAuth;

public class Parametro
{
  private string _ParameterName;
  private string _ParameterValue;

  public Parametro(string ParameterName, string ParameterValue)
  {
    this._ParameterName = ParameterName;
    this._ParameterValue = ParameterValue;
  }

  public string ParameterName
  {
    get => this._ParameterName;
    set => this._ParameterName = value;
  }

  public string ParameterValue
  {
    get => this._ParameterValue;
    set => this._ParameterValue = value;
  }
}

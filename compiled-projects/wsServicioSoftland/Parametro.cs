// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.Parametro
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

#nullable disable
namespace ServicioSoftland;

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

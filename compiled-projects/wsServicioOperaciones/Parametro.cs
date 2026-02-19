// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.Parametro
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

#nullable disable
namespace ServicioOperaciones;

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

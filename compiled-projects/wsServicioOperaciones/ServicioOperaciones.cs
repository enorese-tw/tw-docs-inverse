// Decompiled with JetBrains decompiler
// Type: ServicioOperaciones.ServicioOperaciones
// Assembly: ServicioOperaciones, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 78C989FD-2BAC-4562-AE67-A61EFBC8D368
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioOperaciones\bin\ServicioOperaciones.dll

using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace ServicioOperaciones;

[DataContract]
public class ServicioOperaciones
{
  private DataSet m_Table;

  public ServicioOperaciones() => this.Table = new DataSet("Data");

  [DataMember]
  public DataSet Table
  {
    get => this.m_Table;
    set => this.m_Table = value;
  }
}

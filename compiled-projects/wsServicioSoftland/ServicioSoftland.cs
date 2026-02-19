// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.ServicioSoftland
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace ServicioSoftland;

[DataContract]
public class ServicioSoftland
{
  private DataSet m_Table;

  public ServicioSoftland() => this.Table = new DataSet("Data");

  [DataMember]
  public DataSet Table
  {
    get => this.m_Table;
    set => this.m_Table = value;
  }
}

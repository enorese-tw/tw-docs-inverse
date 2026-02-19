// Decompiled with JetBrains decompiler
// Type: ServicioAuth.ServicioAuth
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

using System.Data;
using System.Runtime.Serialization;

#nullable disable
namespace ServicioAuth;

[DataContract]
public class ServicioAuth
{
  private DataSet m_Table;

  public ServicioAuth() => this.Table = new DataSet("Data");

  [DataMember]
  public DataSet Table
  {
    get => this.m_Table;
    set => this.m_Table = value;
  }
}

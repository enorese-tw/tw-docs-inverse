// Decompiled with JetBrains decompiler
// Type: ServicioAuth.DatosXML
// Assembly: ServicioAuth, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 95DC5F43-ED2F-41E5-8139-49B79C5401D5
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioAuth\bin\ServicioAuth.dll

using System;
using System.Xml;

#nullable disable
namespace ServicioAuth;

public class DatosXML
{
  private string _nombrearchivo = string.Empty;
  private string _usuarioBDSA = string.Empty;
  private string _claveBDSA = string.Empty;
  private string _servidorBDSA = string.Empty;
  private string _basededatos = string.Empty;
  private const string RaizXML = "tw";
  private string NombreArchivoXML = DatosXML.RutaArchivoWeb() + "\\config.xml";

  public DatosXML()
  {
    this._usuarioBDSA = this.UsuarioBDSQLServerSA();
    this._claveBDSA = this.ClaveUsuarioBDSQLServerSA();
    this._servidorBDSA = this.ServidorSQLServerSA();
    this._basededatos = this.BaseDeDatosSQLServer();
  }

  public string BaseDeDatos => this._basededatos;

  public string ServidorBaseDeDatosSA => this._servidorBDSA;

  public string UsuarioBDSA => this._usuarioBDSA;

  public string ClaveBDSA => this._claveBDSA;

  private static string RutaArchivoWeb()
  {
    try
    {
      return AppDomain.CurrentDomain.BaseDirectory + "bin";
    }
    catch (Exception ex)
    {
      return "";
    }
  }

  public XmlDocument CargaXML(string NombreArchivo)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(NombreArchivo);
    return xmlDocument;
  }

  public XmlNodeList BuscaNodos(XmlDocument DocXML, string Nombre) => DocXML?.SelectNodes(Nombre);

  private string UsuarioBDSQLServerSA()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/sqlserver/nombre"))
      str = buscaNodo.Attributes.GetNamedItem("usuario").InnerText;
    return str;
  }

  private string ClaveUsuarioBDSQLServerSA()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/sqlserver/nombre/passwordsa"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string ServidorSQLServerSA()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/sqlserver/nombre/servidorsa"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string BaseDeDatosSQLServer()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/sqlserver/nombre/basedatos"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }
}

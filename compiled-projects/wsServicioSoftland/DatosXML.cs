// Decompiled with JetBrains decompiler
// Type: ServicioSoftland.DatosXML
// Assembly: ServicioSoftland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 96F5CFA2-2689-4B45-93B7-5F4CAE96DC60
// Assembly location: C:\Users\enzo_\Downloads\2026-02-18-apps\18-02-2026\wsServicioSoftland\bin\ServicioSoftland.dll

using System;
using System.Xml;

#nullable disable
namespace ServicioSoftland;

public class DatosXML
{
  private string _nombrearchivo = string.Empty;
  private string _softlandUsuarioDB = string.Empty;
  private string _softlandClaveDB = string.Empty;
  private string _softlandServidorDB = string.Empty;
  private string _softlandBasededatosEst = string.Empty;
  private string _softlandBasededatosOut = string.Empty;
  private string _softlandBasededatosConsultora = string.Empty;
  private string _softlandBasededatosIMVM = string.Empty;
  private string _softlandBasededatosVNEVADO = string.Empty;
  private string _softlandBasededatosVNEVADOESCUELA = string.Empty;
  private string _softlandBasededatosVNEVADOINMOB = string.Empty;
  private string _softlandBasededatosVNEVADOSERVINMOB = string.Empty;
  private string _softlandBasededatosVNEVADOSOCINMOB = string.Empty;
  private string _softlandBasededatosVNEVADOLICANCABU = string.Empty;
  private const string RaizXML = "tw";
  private string NombreArchivoXML = DatosXML.RutaArchivoWeb() + "\\config.xml";

  public DatosXML()
  {
    this._softlandServidorDB = this.SoftlandServidor();
    this._softlandUsuarioDB = this.SoftlandUsuario();
    this._softlandBasededatosEst = this.SoftlandBasededatosEst();
    this._softlandBasededatosOut = this.SoftlandBasededatosOut();
    this._softlandBasededatosConsultora = this.SoftlandBasededatosConsultora();
    this._softlandBasededatosIMVM = this.SoftlandBasededatosIMVM();
    this._softlandBasededatosVNEVADO = this.SoftlandBasededatosVNEVADO();
    this._softlandBasededatosVNEVADOESCUELA = this.SoftlandBasededatosVNEVADOESCUELA();
    this._softlandBasededatosVNEVADOINMOB = this.SoftlandBasededatosVNEVADOINMOB();
    this._softlandBasededatosVNEVADOSERVINMOB = this.SoftlandBasededatosVNEVADOSERVINMOB();
    this._softlandBasededatosVNEVADOSOCINMOB = this.SoftlandBasededatosVNEVADOSOCINMOB();
    this._softlandBasededatosVNEVADOLICANCABU = this.SoftlandBasededatosVNEVADOLICANCABU();
    this._softlandClaveDB = this.SoftlandClave();
  }

  public string SoftlandServidorDB => this._softlandServidorDB;

  public string SoftlandUsuarioDB => this._softlandUsuarioDB;

  public string SoftlandBaseDeDatosEST => this._softlandBasededatosEst;

  public string SoftlandBaseDeDatosOUT => this._softlandBasededatosOut;

  public string SoftlandBaseDeDatosCONSULTORA => this._softlandBasededatosConsultora;

  public string SoftlandBaseDeDatosIMVM => this._softlandBasededatosIMVM;

  public string SoftlandBaseDeDatosVNEVADO => this._softlandBasededatosVNEVADO;

  public string SoftlandBaseDeDatosVNEVADOESCUELA => this._softlandBasededatosVNEVADOESCUELA;

  public string SoftlandBaseDeDatosVNEVADOINMOB => this._softlandBasededatosVNEVADOINMOB;

  public string SoftlandBaseDeDatosVNEVADOSERVINMOB => this._softlandBasededatosVNEVADOSERVINMOB;

  public string SoftlandBaseDeDatosVNEVADOSOCINMOB => this._softlandBasededatosVNEVADOSOCINMOB;

  public string SoftlandBaseDeDatosVNEVADOLICANCABU => this._softlandBasededatosVNEVADOLICANCABU;

  public string SoftlandClaveDB => this._softlandClaveDB;

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

  private string SoftlandServidor()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/servidor"))
      str = !buscaNodo.ChildNodes[0].Value.Contains("\\\\") ? buscaNodo.ChildNodes[0].Value : buscaNodo.ChildNodes[0].Value.Replace("\\\\", "\\");
    return str;
  }

  private string SoftlandBasededatosOut()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosout"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosEst()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosest"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosConsultora()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosconsultora"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosIMVM()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosIMVM"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADO()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevado"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADOESCUELA()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevadoescuela"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADOINMOB()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevadoinmob"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADOSERVINMOB()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevadoservinmob"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADOSOCINMOB()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevadosocinmob"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandBasededatosVNEVADOLICANCABU()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/basededatosvnevadolincancabu"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }

  private string SoftlandUsuario()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre"))
      str = buscaNodo.Attributes.GetNamedItem("usuario").InnerText;
    return str;
  }

  private string SoftlandClave()
  {
    string str = "";
    foreach (XmlNode buscaNodo in this.BuscaNodos(this.CargaXML(this.NombreArchivoXML), "tw/softland/nombre/password"))
      str = buscaNodo.ChildNodes[0].Value;
    return str;
  }
}

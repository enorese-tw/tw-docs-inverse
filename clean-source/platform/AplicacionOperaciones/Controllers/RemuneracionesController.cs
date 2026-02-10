using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teamwork.Model.Finanzas;
using Teamwork.Model.Operaciones;
using Teamwork.WebApi.Auth;
using Teamwork.WebApi.Operaciones;
using Teamwork.WebApi;
using OfficeOpenXml;
using Teamwork.Extensions.Excel;

namespace AplicacionOperaciones.Controllers
{
    public class RemuneracionesController : Controller
    {
        public ActionResult Index()
        {
            if (!AplicationActive())
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }

        public ActionResult CargoMod()
        {
            if (AplicationActive())
            {
                ViewBag.OptionCargoMod = Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1];

                switch (Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 1])
                {
                    case "CargoMod":
                        ViewBag.RenderizadoDashboard = ModuleDashboard(Session["base64Username"].ToString());
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString());
                        ViewBag.TagSelected = "CargoMod";
                        break;
                    case "Detail":
                        ViewBag.OptionCargoMod = "Detail";
                        ViewBag.OptionStageCargoMod = "OK";
                        ViewBag.RenderizadoSolicitudesCargoMod = ModuleSolicitudesCargoMod(Session["base64Username"].ToString(), "MS01", "CodigoSolicitud", Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHeaderEstructura = ModuleEstructuraHeader(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaHaberesEstructura = ModuleEstructuraHaberes(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosEstructura = ModuleEstructuraDescuentos(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizaDescuentosProvision = ModuleEstructuraMargenProvision(Request.Url.AbsoluteUri.Split('/')[Request.Url.AbsoluteUri.Split('/').Length - 2]);
                        ViewBag.RenderizadoBonos = ModuleBonos(Session["base64Username"].ToString(), "MS01", "", "", Session["ApplicationCliente"].ToString());
                        ViewBag.RenderizadoANI = ModuleANI(Session["base64Username"].ToString(), "MS01", "", "");
                        ViewBag.RenderizadoAFP = ModuleAFP(Session["ApplicationEmpresa"].ToString());
                        break;
                    default:

                        break;
                }
            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }

            return View();
        }
        
        public ActionResult GenerateExcel()
        {
            if (AplicationActive())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excel = new ExcelPackage())
                {
                    excel.Workbook.Properties.Title = "Attempts";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", string.Format("attachment;  filename={0}", Request.QueryString["namefile"] + ".xlsx"));
                    Response.ContentType = "application/excel";
                    Response.BinaryWrite(XLSX_ReporteFirmaDigital.__xlsx(Request.QueryString["excel"], Request.QueryString["codigoCargoMod"]));
                    Response.Flush();
                    Response.End();
                }

            }
            else
            {
                return Redirect(ModuleControlRetornoPath() + "/Auth");
            }
            

            return View();
        }

        #region "Modularización de API"

        private List<ConstCalculoProvMargen> ModuleConstCalculoProvMargen()
        {
            List<ConstCalculoProvMargen> constantes = new List<ConstCalculoProvMargen>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenListaConstCalcProv(objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                constante.Codigo = objectsCM[i].Referencia.ToString();
                constante.Descripcion = objectsCM[i].Descripcion.ToString();

                constantes.Add(constante);
            }

            return constantes;
        }

        private List<ProvisionMargen> ModuleProvMargenAsignadas(string cliente, string type, string empresa)
        {
            List<ProvisionMargen> provisiones = new List<ProvisionMargen>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenLista("", "", "", type, empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ProvisionMargen provision = new ProvisionMargen();

                provision.CodigoVariable = objectsCM[i].CodigoVariable.ToString();
                provision.Descripcion = objectsCM[i].Descripcion.ToString();

                dynamic objectsVal = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenValidateAsignacion(objectsCM[i].CodigoVariable.ToString(), cliente, type, empresa, objects[0].Token.ToString()));

                for (var j = 0; j < objectsVal.Count; j++)
                {
                    provision.Monto = objectsVal[j].Valor.ToString();
                    provision.Message = objectsVal[j].Message.ToString();
                }

                dynamic objectsOpt = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, type, empresa, objects[0].Token.ToString()));

                for (var h = 0; h < objectsOpt.Count; h++)
                {
                    provision.OptEditar = objectsOpt[h].OptActualizar.ToString();
                    provision.OptCrear = objectsOpt[h].OptCrear.ToString();
                    provision.OptEliminar = objectsOpt[h].OptEliminar.ToString();
                }

                dynamic objectsConst = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenValidateAsignacion(objectsCM[i].CodigoVariable.ToString(), cliente, "CONST", empresa, objects[0].Token.ToString()));

                for (var x = 0; x < objectsConst.Count; x++)
                {
                    ConstCalculoProvMargen constante = new ConstCalculoProvMargen();

                    constante.Codigo = objectsConst[x].Code.ToString();
                    constante.Message = objectsConst[x].Message.ToString();

                    provision.Constante = constante;
                }

                dynamic objectsOptConst = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenOpcionesEnabledAsig(objectsCM[i].CodigoVariable.ToString(), cliente, "CONST", empresa, objects[0].Token.ToString()));

                for (var z = 0; z < objectsOptConst.Count; z++)
                {
                    provision.OptActualizarAsignConstante = objectsOptConst[z].OptActualizar.ToString();
                    provision.OptCrearAsignConstante = objectsOptConst[z].OptCrear.ToString();
                    provision.OptEliminarAsignConstante = objectsOptConst[z].OptEliminar.ToString();
                }

                if (type != "M")
                {
                    if (objectsCM[i].OptActivar.ToString() == "N")
                    {
                        provisiones.Add(provision);
                    }
                }
                else
                {
                    provisiones.Add(provision);
                }
            }

            return provisiones;
        }

        private List<ProvisionMargen> ModuleProvisionMargen(string pagination = "", string typeFilter = "", string dataFiler = "", string type = "", string empresa = "")
        {
            List<ProvisionMargen> provisiones = new List<ProvisionMargen>();
            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objectsCM = JsonConvert.DeserializeObject(ProvMargen.__ProvMargenLista(pagination, typeFilter, dataFiler, type, empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                ProvisionMargen provision = new ProvisionMargen();

                provision.CodigoVariable = objectsCM[i].CodigoVariable.ToString();
                provision.Descripcion = objectsCM[i].Descripcion.ToString();
                provision.OptEliminar = objectsCM[i].OptEliminar.ToString();
                provision.OptDesactivar = objectsCM[i].OptDesactivar.ToString();
                provision.OptEditar = objectsCM[i].OptEditar.ToString();
                provision.OptActivar = objectsCM[i].OptActivar.ToString();


                provisiones.Add(provision);
            }

            return provisiones;
        }

        private List<AFP> ModuleAFP(string empresa)
        {
            List<AFP> afps = new List<AFP>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsAFP = JsonConvert.DeserializeObject(Cargo.__AFP(empresa, objects[0].Token.ToString()));

            for (var i = 0; i < objectsAFP.Count; i++)
            {
                AFP afp = new AFP();

                afp.Nombre = objectsAFP[i].Nombre.ToString();

                afps.Add(afp);
            }

            return afps;
        }

        private List<ANI> ModuleANI(string usuarioCreador, string pagination, string typeFilter = "", string dataFilter = "")
        {
            List<ANI> anis = new List<ANI>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsANI = JsonConvert.DeserializeObject(ANIs.__ANIS(usuarioCreador, pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsANI.Count; i++)
            {
                ANI ani = new ANI();

                ani.Concepto = objectsANI[i].CodigoANI.ToString();
                ani.Valor = objectsANI[i].Nombre.ToString();

                anis.Add(ani);
            }

            return anis;
        }

        private List<Bono> ModuleBonos(string usuarioCreador, string pagination, string typeFilter = "", string dataFilter = "", string cliente = "")
        {
            List<Bono> bonos = new List<Bono>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsBonos = JsonConvert.DeserializeObject(Bonos.__Bonos(usuarioCreador, pagination, typeFilter, dataFilter, cliente, objects[0].Token.ToString()));

            for (var i = 0; i < objectsBonos.Count; i++)
            {
                Bono bono = new Bono();

                bono.Codigo = objectsBonos[i].CodigoBono.ToString();
                bono.Nombre = objectsBonos[i].Nombre.ToString();

                dynamic objectsAsign = JsonConvert.DeserializeObject(Bonos.__ValidateAsignBonos(objectsBonos[i].CodigoBono.ToString(), cliente, objects[0].Token.ToString()));

                for (var j = 0; j < objectsAsign.Count; j++)
                {
                    switch (objectsAsign[j].Code.ToString())
                    {
                        case "200":
                            bono.Asignado = "success color-fx3";
                            break;
                        default:
                            bono.Asignado = "";
                            break;
                    }

                }

                bonos.Add(bono);
            }

            return bonos;
        }

        private List<ValoresDiarios> ModuleValoresDiarios(string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<ValoresDiarios> valoresDiarios = new List<ValoresDiarios>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsValorDario = JsonConvert.DeserializeObject(ValorDiario.__ValorDiarioListar(pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsValorDario.Count; i++)
            {
                ValoresDiarios valor = new ValoresDiarios();

                valor.Cliente = objectsValorDario[i].Cliente.ToString();
                valor.Empresa = objectsValorDario[i].Empresa.ToString();
                valor.CargoMod = objectsValorDario[i].CargoMod.ToString();
                valor.ValorDiario = objectsValorDario[i].Valor.ToString();

                valoresDiarios.Add(valor);
            }

            return valoresDiarios;
        }

        private List<Provision> ModuleEstructuraMargenProvision(string codigoSolicitud = "")
        {
            List<Provision> estructuras = new List<Provision>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraMargenProvision(Session["base64Username"].ToString(), codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                Provision provision = new Provision();

                provision.Concepto = objectsEstructura[i].Concepto.ToString();
                provision.Percentage = objectsEstructura[i].Porcentaje.ToString();
                provision.MontoCLP = objectsEstructura[i].MontoCLP.ToString();
                provision.CodigoVariable = objectsEstructura[i].CodigoVariable.ToString();
                provision.OptWithExcluir = objectsEstructura[i].OptWithExcluir.ToString();
                provision.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructuras.Add(provision);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraHeader(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraHeader(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.Cliente = objectsEstructura[i].Cliente.ToString();
                estructura.NombreCargo = objectsEstructura[i].NombreCargo.ToString();
                estructura.NombreCargoMod = objectsEstructura[i].NombreCargoMod.ToString();
                estructura.TípoContrato = objectsEstructura[i].TipoContrato.ToString();
                estructura.TopeUFImponible = objectsEstructura[i].UFTopeImponible.ToString();
                estructura.TopeCLPImponible = objectsEstructura[i].CLPTopeImponible.ToString();
                estructura.TopeUFImponibleAFC = objectsEstructura[i].UFTopeImponibleAFC.ToString();
                estructura.TopeCLPImponibleAFC = objectsEstructura[i].CLPTopeImponibleAFC.ToString();
                estructura.EnlaceEstructura = ModuleControlRetornoPath() + "/Finanzas/ViewPdf?pdf=EstructuraRentaTemp&data=" + objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.TypeSueldoInputActual = objectsEstructura[i].TypeSueldoInputActual.ToString();
                estructura.TypeSueldoInputChange = objectsEstructura[i].TypeSueldoInputChange.ToString();
                estructura.TypeCalculoSueldo = objectsEstructura[i].TypeCalculoSueldo.ToString();
                estructura.MessageCargoMod = objectsEstructura[i].MessageCargoMod.ToString();

                estructura.MessageTypeJornada = objectsEstructura[i].MessageTypeJornada.ToString();
                estructura.MessageTypeCalculo = objectsEstructura[i].MessageTypeCalculo.ToString();
                estructura.TypeJornada = objectsEstructura[i].TypeJornada.ToString();
                estructura.MaxLengthCargoMod = objectsEstructura[i].MaxLengthCargoMod.ToString();
                estructura.RestLengthCargoMod = objectsEstructura[i].RestLengthCargoMod.ToString();

                estructura.NumeroHorasSemanalesPT = objectsEstructura[i].NumeroHorasSemanalesPT.ToString();
                estructura.TypeCalculoPT = objectsEstructura[i].TypeCalculoPT.ToString();
                estructura.ValorSueldoPT = objectsEstructura[i].ValorSueldoPT.ToString();
                estructura.Horarios = objectsEstructura[i].Horarios.ToString();
                estructura.Observaciones = objectsEstructura[i].Observaciones.ToString();
                estructura.Observaciones2 = objectsEstructura[i].Observaciones2.ToString();
                estructura.Origen = objectsEstructura[i].Origen.ToString();
                estructura.NumeroDias = objectsEstructura[i].NumeroDias.ToString();

                estructura.RazonSocial = objectsEstructura[i].RazonSocial.ToString();

                ViewBag.TypeSueldoInputActual = objectsEstructura[i].TypeSueldoInputActual.ToString();
                ViewBag.TypeJornadaActual = objectsEstructura[i].TypeJornada.ToString();

                Session["ApplicationCliente"] = objectsEstructura[i].Cliente.ToString();
                Session["ApplicationEmpresa"] = objectsEstructura[i].Empresa.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraHaberes(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraHaberes(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.SueldoBase = objectsEstructura[i].SueldoBase.ToString();
                estructura.SueldoLiquido = objectsEstructura[i].SueldoLiquido.ToString();
                estructura.Gratificacion = objectsEstructura[i].Gratificacion.ToString();
                estructura.BaseImponible = objectsEstructura[i].BaseImponible.ToString();
                estructura.BaseImponibleAFC = objectsEstructura[i].BaseImponibleAFC.ToString();
                estructura.TotalImponible = objectsEstructura[i].TotalImponible.ToString();
                estructura.TotalTributable = objectsEstructura[i].TotalTributable.ToString();
                estructura.TotalHaberes = objectsEstructura[i].TotalHaberes.ToString();
                estructura.Bonos = ModuleBonosCargoMod(objectsEstructura[i].CodigoCargoMod.ToString());
                estructura.AsignacionesNOImponibles = ModuleANIsCargoMod(objectsEstructura[i].CodigoCargoMod.ToString());
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();
                estructura.GratificacionPactada = objectsEstructura[i].GratificacionPactada.ToString();
                estructura.GratifCC = objectsEstructura[i].GratifCC.ToString();
                estructura.MessageGratif = objectsEstructura[i].MessageGratif.ToString();
                estructura.MessageSueldoBase = objectsEstructura[i].MessageSueldoBase.ToString();
                estructura.MessageSugGratif = objectsEstructura[i].MessageSugGratif.ToString();
                estructura.GratifSugerida = objectsEstructura[i].GratifSugerida.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<EstructuraCargo> ModuleEstructuraDescuentos(string codigoSolicitud = "")
        {
            List<EstructuraCargo> estructuras = new List<EstructuraCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsEstructura = JsonConvert.DeserializeObject(Cargo.__EstructuraDescuentos(codigoSolicitud, objects[0].Token.ToString()));

            for (var i = 0; i < objectsEstructura.Count; i++)
            {
                EstructuraCargo estructura = new EstructuraCargo();

                estructura.PorcAFP = objectsEstructura[i].PorcAFP.ToString();
                estructura.AFP = objectsEstructura[i].AFP.ToString();
                estructura.CLPAFP = objectsEstructura[i].CLPAFP.ToString();
                estructura.PorcFondoPensiones = objectsEstructura[i].PorcFondoPensiones.ToString();
                estructura.PorcSegInvalidez = objectsEstructura[i].PorcSegInvalidez.ToString();
                estructura.PorcSalud = objectsEstructura[i].PorcSalud.ToString();
                estructura.CLPSalud = objectsEstructura[i].CLPSalud.ToString();
                estructura.CLPImpuestoUnico = objectsEstructura[i].CLPImpuestoUnico.ToString();
                estructura.PorcSeguroDesempleo = objectsEstructura[i].PorcSeguroDesempleo.ToString();
                estructura.CLPSeguroDesempleo = objectsEstructura[i].CLPSeguroDesempleo.ToString();
                estructura.TotalDescuentos = objectsEstructura[i].TotalDescuentos.ToString();
                estructura.CodigoCargoMod = objectsEstructura[i].CodigoCargoMod.ToString();

                estructuras.Add(estructura);
            }

            return estructuras;
        }

        private List<ANIsCargoMod> ModuleANIsCargoMod(string codigoCagoMod)
        {
            List<ANIsCargoMod> ANIList = new List<ANIsCargoMod>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsANI = JsonConvert.DeserializeObject(ANIs.__ANIsCargoMod(codigoCagoMod, objects[0].Token.ToString()));

            for (var i = 0; i < objectsANI.Count; i++)
            {
                ANIsCargoMod ANI = new ANIsCargoMod();

                ANI.Nombre = objectsANI[i].Nombre.ToString();
                ANI.Valor = objectsANI[i].Valor.ToString();
                ANI.Comentarios = objectsANI[i].Comentarios.ToString();
                ANI.CreadoPor = objectsANI[i].CreadoPor.ToString();
                ANI.Creado = objectsANI[i].Creado.ToString();
                ANI.UltimaActualizacion = objectsANI[i].UltimaActualizacion.ToString();
                ANI.Border = objectsANI[i].Border.ToString();
                ANI.GlyphiconColor = objectsANI[i].GlyphiconColor.ToString();
                ANI.Glyphicon = objectsANI[i].Glyphicon.ToString();
                ANI.CodigoCargoMod = objectsANI[i].CodigoCargoMod.ToString();
                ANI.CodigoANI = objectsANI[i].CodigoANI.ToString();
                ANI.Deft = objectsANI[i].Deft.ToString();
                ANI.Observaciones = objectsANI[i].Observaciones.ToString();

                ANIList.Add(ANI);
            }

            return ANIList;
        }

        private List<BonosCargoMod> ModuleBonosCargoMod(string codigoCagoMod)
        {
            List<BonosCargoMod> bonos = new List<BonosCargoMod>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsBonos = JsonConvert.DeserializeObject(Bonos.__BonosCargoMod(codigoCagoMod, objects[0].Token.ToString()));

            for (var i = 0; i < objectsBonos.Count; i++)
            {
                BonosCargoMod bono = new BonosCargoMod();

                bono.Nombre = objectsBonos[i].Nombre.ToString();
                bono.Valor = objectsBonos[i].Valor.ToString();
                bono.Comentarios = objectsBonos[i].Comentarios.ToString();
                bono.CreadoPor = objectsBonos[i].CreadoPor.ToString();
                bono.Creado = objectsBonos[i].Creado.ToString();
                bono.UltimaActualizacion = objectsBonos[i].UltimaActualizacion.ToString();
                bono.Border = objectsBonos[i].Border.ToString();
                bono.GlyphiconColor = objectsBonos[i].GlyphiconColor.ToString();
                bono.Glyphicon = objectsBonos[i].Glyphicon.ToString();
                bono.CodigoCargoMod = objectsBonos[i].CodigoCargoMod.ToString();
                bono.CodigoBono = objectsBonos[i].CodigoBono.ToString();
                bono.MessageAsignado = objectsBonos[i].MessageAsignado.ToString();
                bono.Condiciones = objectsBonos[i].Condiciones.ToString();

                bonos.Add(bono);
            }

            return bonos;
        }

        private List<Dashboard> ModuleDashboard(string usuarioCreador)
        {
            List<Dashboard> dashboards = new List<Dashboard>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsDashboard = JsonConvert.DeserializeObject(Cargo.__Dashboard(usuarioCreador, objects[0].Token.ToString()));

            for (var i = 0; i < objectsDashboard.Count; i++)
            {
                Dashboard dashboard = new Dashboard();

                dashboard.Creaciones = objectsDashboard[i].Creaciones.ToString();
                dashboard.Simulaciones = objectsDashboard[i].Simulaciones.ToString();
                dashboard.RechazadosKam = objectsDashboard[i].RechazadosKam.ToString();
                dashboard.RechazadosRem = objectsDashboard[i].RechazadosRem.ToString();
                dashboard.PendienteFinanzas = objectsDashboard[i].PendientesFinanzas.ToString();
                dashboard.PendienteRemuneraciones = objectsDashboard[i].PendientesRemuneraciones.ToString();
                dashboard.PendientesFirmaDigital = objectsDashboard[i].PendientesFirmaDigital.ToString();
                dashboard.Terminados = objectsDashboard[i].Terminados.ToString();
                dashboard.Profile = objectsDashboard[i].Profile.ToString();

                dashboards.Add(dashboard);
            }

            return dashboards;
        }

        private List<Clientes> ModuleClientes(string empresa, string pagination, string typeFilter, string dataFilter, string usuarioCreador)
        {
            List<Clientes> clientes = new List<Clientes>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsClientes = JsonConvert.DeserializeObject(Cargo.__Clientes(empresa, pagination, typeFilter, dataFilter, usuarioCreador, objects[0].Token.ToString()));

            for (var i = 0; i < objectsClientes.Count; i++)
            {
                Clientes cliente = new Clientes();

                cliente.Codigo = objectsClientes[i].Codigo.ToString();
                cliente.Value = objectsClientes[i].Value.ToString();

                clientes.Add(cliente);
            }

            return clientes;
        }

        private List<Models.Teamwork.Pagination> ModulePaginationClientes(string empresa, string pagination, string typeFilter, string dataFilter)
        {
            ViewBag.Empresa = empresa;

            List<Models.Teamwork.Pagination> paginations = new List<Models.Teamwork.Pagination>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsPgt = JsonConvert.DeserializeObject(Cargo.__PaginationClientes(empresa, pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsPgt.Count; i++)
            {
                Models.Teamwork.Pagination paginationIndex = new Models.Teamwork.Pagination();

                paginationIndex.NumeroPagina = objectsPgt[i].NumeroPagina.ToString();
                paginationIndex.Rango = objectsPgt[i].Rango.ToString();
                paginationIndex.Class = objectsPgt[i].Class.ToString();
                paginationIndex.Properties = objectsPgt[i].Properties.ToString();
                paginationIndex.TypeFilter = objectsPgt[i].TypeFilter.ToString();
                paginationIndex.Filter = objectsPgt[i].Filter.ToString();

                if (objectsPgt[i].NumeroPagina.ToString() == "Siguiente &rsaquo;" && objectsPgt[i].Properties.ToString() == "")
                {
                    paginations.Add(paginationIndex);
                }

            }

            return paginations;
        }

        private List<SolicitudCargo> ModuleSolicitudesCargoMod(string usuarioCreador, string pagination = "MS01", string typeFilter = "", string dataFilter = "")
        {
            List<SolicitudCargo> solicitudesCargos = new List<SolicitudCargo>();

            dynamic objects = JsonConvert.DeserializeObject(Authenticate.__Authenticate());

            dynamic objectsCM = JsonConvert.DeserializeObject(Cargo.__Solicitudes(Session["base64Username"].ToString(), pagination, typeFilter, dataFilter, objects[0].Token.ToString()));

            for (var i = 0; i < objectsCM.Count; i++)
            {
                SolicitudCargo solicitudCargos = new SolicitudCargo();

                solicitudCargos.CodigoCargoMod = objectsCM[i].CodigoCargoMod.ToString();
                solicitudCargos.Empresa = objectsCM[i].Empresa.ToString();
                solicitudCargos.NombreSolicitud = objectsCM[i].NombreSolicitud.ToString();
                solicitudCargos.Creado = objectsCM[i].Creado.ToString();
                solicitudCargos.CreadoPor = objectsCM[i].CreadoPor.ToString();
                solicitudCargos.Cliente = objectsCM[i].Cliente.ToString();
                solicitudCargos.Comentarios = objectsCM[i].Comentarios.ToString();
                solicitudCargos.Border = objectsCM[i].Border.ToString();
                solicitudCargos.Glyphicon = objectsCM[i].Glyphicon.ToString();
                solicitudCargos.GlyphiconColor = objectsCM[i].GlyphiconColor.ToString();
                solicitudCargos.Estado = objectsCM[i].Estado.ToString();
                solicitudCargos.UltimaActualizacion = objectsCM[i].UltimaActualizacion.ToString();
                solicitudCargos.NombreCargo = objectsCM[i].NombreCargo.ToString();
                solicitudCargos.CodigoSolicitud = objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.EnlaceDetalleSolicitud = ModuleControlRetornoPath() + "/Remuneraciones/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Detail";
                solicitudCargos.EnlaceEditar = ModuleControlRetornoPath() + "/Remuneraciones/CargoMod/" + objectsCM[i].CodigoSolicitud.ToString() + "/Edit";
                solicitudCargos.EnlaceVolver = ModuleControlRetornoPath() + "/Remuneraciones/CargoMod";
                solicitudCargos.EnlaceEstructura = ModuleControlRetornoPath() + "/Finanzas/Pdf?pdf=EstructuraRenta&data=" + objectsCM[i].CodigoSolicitud.ToString();

                solicitudCargos.OptDesechar = objectsCM[i].OptDesechar.ToString();
                solicitudCargos.OptPdf = objectsCM[i].OptPdf.ToString();
                solicitudCargos.OptEditar = objectsCM[i].OptEditar.ToString();
                solicitudCargos.OptSolicitar = objectsCM[i].OptSolicitar.ToString();
                solicitudCargos.OptRechazar = objectsCM[i].OptRechazar.ToString();
                solicitudCargos.OptPublicar = objectsCM[i].OptPublicar.ToString();
                solicitudCargos.OptRechazarRem = objectsCM[i].OptRechazarRem.ToString();
                solicitudCargos.OptHistorial = objectsCM[i].OptHistorial.ToString();
                solicitudCargos.SectHaber = objectsCM[i].SectHaber.ToString();
                solicitudCargos.SectDesc = objectsCM[i].SectDesc.ToString();
                solicitudCargos.SectPorvMargen = objectsCM[i].SectPorvMargen.ToString();

                solicitudCargos.SectPorvMargen = objectsCM[i].SectPorvMargen.ToString();
                solicitudCargos.GlosaCodigo = objectsCM[i].GlosaCodigo.ToString();

                solicitudCargos.OptDescargarReporteFirmaDigital = objectsCM[i].OptDescargarReporteFirmaDigital.ToString();
                solicitudCargos.EnlaceDescargaReporteFirmaDigital = ModuleControlRetornoPath() + "/Remuneraciones/GenerateExcel?excel=ReporteFirmaDigital&namefile=Reporte__Firma__Digital&codigoCargoMod=" + objectsCM[i].CodigoSolicitud.ToString();

                solicitudesCargos.Add(solicitudCargos);
            }

            return solicitudesCargos;
        }

        #endregion
        
        #region "Permisos y Redireccionamientos ante cierre y expiración de sesión"

        public bool AplicationActive()
        {
            bool active = false;

            if (Session["NombreUsuario"] != null && Session["Cargo"] != null && Session["Area"] != null && Session["ApplicationActive"] != null && Session["Profile"] != null && Session["base64Username"] != null && Session["base64Password"] != null &&
                Session["CodificarAuth"] != null && Session["PreController"] != null && Session["Username"] != null)
            {
                active = true;
            }

            return active;
        }

        public string ModuleControlRetornoPath()
        {
            string urlServer = "";

            if (!Request.Url.AbsoluteUri.Split('/')[2].Contains("localhost"))
            {
                urlServer = "http://181.190.6.196/AplicacionOperaciones";
            }
            else
            {
                urlServer = "http://localhost:46903";
            }

            return urlServer;
        }

        #endregion
    }
}
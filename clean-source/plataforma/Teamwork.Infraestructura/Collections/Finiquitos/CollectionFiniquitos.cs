using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Teamwork.Infraestructura.Instances.Finiquitos;
using Teamwork.Infraestructura.Instances.General;
using Teamwork.Infraestructura.Instances.Paginations;
using Teamwork.Model.Bajas;
using Teamwork.Model.General;
using Teamwork.WebApi;
using Teamwork.WebApi.Auth;

namespace Teamwork.Infraestructura.Collections.Finiquitos
{
    public class CollectionFiniquitos
    {


        public static List<Finiquito> __FiniquitosConsultaFiniquitos(string usuarioCreador, string pagination, string filter, string dataFilter)
        {
            List<Finiquito> collections = new List<Finiquito>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConsultaFiniquitos(
                    usuarioCreador,
                    pagination,
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceFiniquitos.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Complementos> __FiniquitosConsultaComplementos(string usuarioCreador, string pagination, string filter, string dataFilter)
        {
            List<Complementos> collections = new List<Complementos>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConsultaComplementos(
                    usuarioCreador,
                    pagination,
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceComplementos.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Pagination> __FiniquitosPaginationFiniquitos(string usuarioCreador, string pagination, string filter, string dataFilter)
        {
            List<Pagination> collections = new List<Pagination>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosPaginationFiniquitos(
                    usuarioCreador,
                    pagination,
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstancePagination.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Historial> __FiniquitosHistorialFiniquitos(string codigo)
        {
            List<Historial> collections = new List<Historial>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosHistorialFiniquitos(
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceHistorial.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosValidarFiniquito(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosValidarFiniquito(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosAnularFiniquito(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosAnularFiniquito(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionEnvioRegiones(string usuarioCreador, string codigo, string observacion, string envio)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionEnvioRegiones(
                    usuarioCreador,
                    codigo,
                    observacion,
                    envio,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionEnvioSantiagoNotaria(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionEnvioSantiagoNotaria(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionEnvioSantiagoParaFirma(string usuarioCreador, string codigo, string observacion, string rolCoordinador, string coordinador)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionEnvioSantiagoParaFirma(
                    usuarioCreador,
                    codigo,
                    observacion,
                    rolCoordinador,
                    coordinador,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionRecepcionRegiones(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionRecepcionRegiones(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionRecepcionSantiagoNotaria(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionRecepcionSantiagoNotaria(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionRecepcionSantiagoParaFirma(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionRecepcionSantiagoParaFirma(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }
        
        public static List<Request> __FiniquitosSolicitudValeVista(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosSolicitudValeVista(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosSolicitudCheque(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosSolicitudCheque(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosSolicitudTEF(string usuarioCreador, string codigo, string observacion, string banco, string numeroCta, string gastoAdm)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosSolicitudTEF(
                    usuarioCreador,
                    codigo,
                    observacion,
                    banco,
                    numeroCta,
                    gastoAdm,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<DatosTEF> __FiniquitosConsultaDatosTEF(string usuarioCreador, string codigo)
        {
            List<DatosTEF> collections = new List<DatosTEF>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConsultaDatosTEF(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceTEF.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Banco> __FiniquitosBancos()
        {
            List<Banco> collections = new List<Banco>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosBancos(
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceBanco.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionEnvioLegalizacion(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionEnvioLegalizacion(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosGestionRecepcionLegalizacion(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosGestionRecepcionLegalizacion(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<SolicitudPago> __FiniquitosConsultaSolicitudPago(string usuarioCreador, string codigo)
        {
            List<SolicitudPago> collections = new List<SolicitudPago>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConsultaSolicitudPago(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceSolicitudPago.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosConfirmarProcesoPago(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConfirmarProcesoPago(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosPagarFiniquito(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosPagarFiniquito(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Liquidacion> __FiniquitosListarLiquidacionesSueldoYear(string codigo)
        {
            List<Liquidacion> collections = new List<Liquidacion>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosListarLiquidacionesSueldoYear(
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceLiquidaciones.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Liquidacion> __FiniquitosListarLiquidacionesSueldoMes(string codigo, string filter, string dataFilter)
        {
            List<Liquidacion> collections = new List<Liquidacion>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosListarLiquidacionesSueldoMes(
                    codigo,
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceLiquidaciones.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosLiquidacionSueldoBase64(string codigo, string filter, string dataFilter)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosLiquidacionSueldoBase64(
                    codigo,
                    filter,
                    dataFilter,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceLiquidaciones.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoCrear(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoCrear(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<HaberesComplemento> __FiniquitosComplementoListarHaberes(string usuarioCreador, string codigo)
        {
            List<HaberesComplemento> collections = new List<HaberesComplemento>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoListarHaberes(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceHaberesComplemento.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<DescuentoComplemento> __FiniquitosComplementoListarDescuento(string usuarioCreador, string codigo)
        {
            List<DescuentoComplemento> collections = new List<DescuentoComplemento>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoListarDescuento(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceDescuentoComplemento.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoAgregarHaber(string usuarioCreador, string codigo, string monto, string descripcion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoAgregarHaber(
                    usuarioCreador,
                    codigo,
                    monto,
                    descripcion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoAgregarDescuento(string usuarioCreador, string codigo, string monto, string descripcion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoAgregarDescuento(
                    usuarioCreador,
                    codigo,
                    monto,
                    descripcion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoEliminarHaber(string usuarioCreador, string codigo, string variable)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoEliminarHaber(
                    usuarioCreador,
                    codigo,
                    variable,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoEliminarDescuento(string usuarioCreador, string codigo, string variable)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoEliminarDescuento(
                    usuarioCreador,
                    codigo,
                    variable,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosComplementoDejarActivoCreado(string usuarioCreador, string codigo, string fecha)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosComplementoDejarActivoCreado(
                    usuarioCreador,
                    codigo,
                    fecha,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }
        
        public static List<Request> __FiniquitosActualizarMontoAdministrativo(string usuarioCreador, string codigo, string observacion, string gastoAdm)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosActualizarMontoAdministrativo(
                    usuarioCreador,
                    codigo,
                    observacion,
                    gastoAdm,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirValidacion(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirValidacion(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirGestionEnvio(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirGestionEnvio(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirLegalizacion(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirLegalizacion(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirSolicitudPago(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirSolicitudPago(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirConfirmacion(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirConfirmacion(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirEmisionPago(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirEmisionPago(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<ButtonFiniquitos> __FiniquitosConfigOpcionesMovimientoMasivos(string usuarioCreador, string codigo)
        {
            List<ButtonFiniquitos> collections = new List<ButtonFiniquitos>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConfigOpcionesMovimientoMasivos(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceButtonFiniquitos.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosActualizarComentariosAnotaciones(string codigo, string html)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosActualizarComentariosAnotaciones(
                    codigo,
                    html,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<ComentariosAnotaciones> __FiniquitosConsultaComentariosAnotaciones(string codigo)
        {
            List<ComentariosAnotaciones> collections = new List<ComentariosAnotaciones>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosConsultaComentariosAnotaciones(
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceComentariosAnotaciones.__CreateObjectInstance(objects[i])
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosTerminarFiniquito(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosTerminarFiniquito(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosFirmarFiniquito(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosFirmarFiniquito(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosReprocesarDocumentosFiniquito(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosReprocesarDocumentosFiniquito(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosValidarFinanzas(string usuarioCreador, string codigo)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosValidarFinanzas(
                    usuarioCreador,
                    codigo,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

        public static List<Request> __FiniquitosRevertirValidacionFinanzas(string usuarioCreador, string codigo, string observacion)
        {
            List<Request> collections = new List<Request>();

            dynamic objectsToken = JsonConvert.DeserializeObject(Authenticate.__Authenticate());
            dynamic objects = JsonConvert.DeserializeObject(
                CallAPIFiniquitos.__FiniquitosRevertirValidacionFinanzas(
                    usuarioCreador,
                    codigo,
                    observacion,
                    objectsToken[0].Token.ToString())
            );

            for (var i = 0; i < objects.Count; i++)
            {
                collections.Add(
                    InstanceRequest.__CreateObjectInstance(objects[i], "")
                );
            }

            return collections;
        }

    }
}
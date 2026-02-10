using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FiniquitosV2.Clases
{
    public class MethodServiceFiniquitos
    {
        ServicioFiniquitos.ServicioFiniquitosClient svcFiniquitos = new ServicioFiniquitos.ServicioFiniquitosClient();

        public string IDDESVINCULACION { get; set; }
        public string NUMERODOCUMENTO { get; set; }
        public string IDTRACKING { get; set; }
        public string USUARIO { get; set; }
        public string RUTPROVEEDOR { get; set; }
        public string MONTO { get; set; }
        public string IDCORRELATIVO { get; set; }
        public string NSERIECHEQUE { get; set; }
        public string CLIENTE { get; set; }
        public string DISPONIBLEGENERAR { get; set; }
        public string DISPONIBLEPAGO { get; set; }
        public string PAGADO { get; set; }
        public string TRABAJADOR { get; set; }
        public string FICHA { get; set; }
        public string FECHAHORASOLICITUD { get; set; }
        public string FECHAAVISODESVINCULACION { get; set; }
        public string RUTTRABAJADOR { get; set; }
        public string NOMBRETRABAJADOR { get; set; }
        public string ESTADOSOLICITUD { get; set; }
        public string FECHAESTADO { get; set; }
        public string IDCAUSAL { get; set; }
        public string IDEMPRESA { get; set; }
        public string OBSERVACION { get; set; }
        public string IDRENUNCIA { get; set; }
        public string SOLICITUD { get; set; }
        public string MEDIOPAGO { get; set; }
        public string TIPOPAGO { get; set; }
        public string BANCO { get; set; }
        public string EMPRESA { get; set; }
        public string MONTOVACACIONES { get; set; }
        public string MONTOIAS { get; set; }
        public string MONTODESHAUCIO { get; set; }
        public string MONTOINDEMNIZACION { get; set; }
        public string MONTOFINIQUITO { get; set; }
        public string CLIENTESIGLA { get; set; }
        public string DIA { get; set; }
        public string MES { get; set; }
        public string YEAR { get; set; }
        public string CIFRA { get; set; }
        public string CIUDAD { get; set; }
        public string NOMINATIVO { get; set; }
        public string CARGOMOD { get; set; }
        public string AREANEGOCIO { get; set; }
        public string ESPARTTIME { get; set; }
        public string ESZONAEXT { get; set; }
        public string FILTRARPOR { get; set; }
        public string FILTRO { get; set; }
        public string FECHAINICIO { get; set; }
        public string FECHATERMINO { get; set; }
        public string MEAVISO { get; set; }
        public string AFC { get; set; }
        public string DESCRIPCION { get; set; }
        public string MESANO { get; set; }
        public string BONONOMBRE { get; set; }
        public string PERIODO { get; set; }
        public string TIPO { get; set; }

        public string DIASTOTALESTRABAJADOS { get; set; }
        public string DIASTOTALESTRABAJADOS2 { get; set; }
        public string YEAR2 { get; set; }
        public string MESES { get; set; }
        public string MESES2 { get; set; }
        public string DIAS { get; set; }
        public string DIAS2 { get; set; }
        public string VACACIONESPROGRESIVAS { get; set; }
        public string VACACIONESPROGRESIVAS2 { get; set; }
        public string SALDO { get; set; }
        public string SALDO2 { get; set; }
        public string VACACIONESTOMADAS { get; set; }
        public string VACACIONESTOMADAS2 { get; set; }
        public string SALDOHABILES { get; set; }
        public string SALDOHABILES2 { get; set; }
        public string SALDOINHABILES { get; set; }
        public string SALDOINHABILES2 { get; set; }
        public string DIASCORRIDOS { get; set; }
        public string DIASCORRIDOS2 { get; set; }
        public string FECHADIASCORRIDOS { get; set; }
        public string ZONAEXT { get; set; }

        public string SUELDOBASE { get; set; }
        public string BONOSVARIABLES { get; set; }
        public string GRATIFICACION { get; set; }
        public string COLACION { get; set; }
        public string MOVILIZACION { get; set; }
        public string BASECALCULO { get; set; }
        public string BASEVACACIONES { get; set; }

        public string REMUNERACIONPENDIENTE { get; set; }
        public string BONO { get; set; }
        public string INDEMNIZACIONVOLUNTARIA { get; set; }
        public string OTROSHABERES { get; set; }
        public string VACACIONES { get; set; }
        public string MESAVISO { get; set; }
        public string IAS { get; set; }
        public string TOTALPAGOS { get; set; }
        public string TOTALDESCUENTOS { get; set; }
        public string TOTALFINIQUITO { get; set; }

        public string FILTRO2 { get; set; }
        public string FILTRO3 { get; set; }

        public string MOTIVOANULACION { get; set; }

        public string RUT { get; set; }

        public string NOMBRE { get; set; }

        public string CODIGOSOLICITUD { get; set; }

        public string CCAF { get; set; }
        public string FACTORCALCULO { get; set; }

        public string FECHACARTABAJA { get; set; }
        public string EXCEPCIONCOSTOCERO { get; set; }

        public string TOTALFINIQUITOS { get; set; }

        public string NIVEL { get; set; }
        public string CATEGORIA { get; set; }

        public MethodServiceFiniquitos() { }

        #region "OUT"

        /** METODOS PUBLICOS DE ACCESO A METODOS DE SERVICIO OUT  */
        public DataSet SetContratosOUTService
        {
            get
            {
                return SetContratosOUT(IDDESVINCULACION, FICHA, FECHAINICIO, FECHATERMINO);
            }
        }

        public DataSet SetConceptosAdicionalesOUTService
        {
            get
            {
                return SetConceptosAdicionalesOUT(IDDESVINCULACION, MEAVISO, AFC);
            }
        }

        public DataSet SetDatosVariablesOUTService
        {
            get
            {
                return SetDatosVariablesOUT(IDDESVINCULACION, DESCRIPCION, MONTO, MESANO);
            }
        }

        public DataSet SetControlDocumentosOUTService
        {
            get
            {
                return SetControlDocumentosOUT(IDDESVINCULACION, DESCRIPCION, USUARIO);
            }
        }

        public DataSet SetBonosAdicionalesOUTService
        {
            get
            {
                return SetBonosAdicionalesOUT(IDDESVINCULACION, BONONOMBRE, MONTO, PERIODO);
            }
        }

        public DataSet SetHaberesYDescuentosOUTService
        {
            get
            {
                return SetHaberesYDescuentosOUT(IDDESVINCULACION, TIPO, DESCRIPCION, MONTO);
            }
        }

        public DataSet SetDiasVacacionesOUTService
        {
            get
            {
                return SetDiasVacacionesOUT(IDDESVINCULACION, DIASTOTALESTRABAJADOS, DIASTOTALESTRABAJADOS2, YEAR, YEAR2, MESES, MESES2, DIAS, DIAS2, VACACIONESPROGRESIVAS, 
                                            VACACIONESPROGRESIVAS2, SALDO, SALDO2, VACACIONESTOMADAS, VACACIONESTOMADAS2, SALDOHABILES, SALDOHABILES2, SALDOINHABILES, SALDOINHABILES2,
                                            DIASCORRIDOS, DIASCORRIDOS2, FECHADIASCORRIDOS, FACTORCALCULO);
            }
        }

        public DataSet SetHaberesMensualesOUTService
        {
            get
            {
                return SetHaberesMensualesOUT(IDDESVINCULACION, SUELDOBASE, BONOSVARIABLES, GRATIFICACION, COLACION, MOVILIZACION, BASECALCULO, BASEVACACIONES);
            }
        }

        public DataSet SetTotalPagosOUTService
        {
            get
            {
                return SetTotalPagosOUT(IDDESVINCULACION, REMUNERACIONPENDIENTE, BONO, INDEMNIZACIONVOLUNTARIA, OTROSHABERES, VACACIONES, MESAVISO, IAS, TOTALPAGOS, TOTALDESCUENTOS,
                                        TOTALFINIQUITO);
            }
        }

        public DataSet GetObtenerExcepcionPTAreaNegService
        {
            get
            {
                return GetObtenerExcepcionPTAreaNeg(AREANEGOCIO);
            }
        }


        private DataSet GetObtenerExcepcionPTAreaNeg(string areaNeg)
        {
            string[] parametros = {
                "@AREANEG"
            };

            string[] valores = {
                areaNeg
            };

            return svcFiniquitos.GetObtenerExcepcionPTAreaNeg(parametros, valores).Table;
        }

        /** METODOS PRIVADOS OUT  */
        private DataSet SetContratosOUT(string idDesvinculacion, string ficha, string fechaInicio, string fechaTermino)
        {
            var fechaInicioNew = fechaInicio.Replace('/', '-').Split('-');
            var fechaTerminoNew = fechaTermino.Replace('/', '-').Split('-');

            string[] parametros = {
                "@IDDESVINCULACION",
                "@FICHA",
                "@FECHAINICIO",
                "@FECHATERMINO"
            };

            string[] valores = {
                idDesvinculacion,
                ficha,
                fechaInicioNew[2] + "-" + fechaInicioNew[1] + "-" + fechaInicioNew[0],
                fechaTerminoNew[2] + "-" + fechaTerminoNew[1] + "-" + fechaTerminoNew[0]
            };

            return svcFiniquitos.SetContratosOUT(parametros, valores).Table;
        }

        private DataSet SetConceptosAdicionalesOUT(string idDesvinculacion, string mesaviso, string afc)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@MESAVISO",
                "@AFC"
            };

            string[] valores = {
                idDesvinculacion,
                mesaviso,
                afc
            };

            return svcFiniquitos.SetConceptosAdicionalesOUT(parametros, valores).Table;
        }

        private DataSet SetDatosVariablesOUT(string idDesvinculacion, string descripcion, string monto, string mesano)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@DESCRIPCION",
                "@MONTO",
                "@MESANO"
            };

            string[] valores = {
                idDesvinculacion,
                descripcion,
                monto,
                mesano
            };

            return svcFiniquitos.SetDatosVariablesOUT(parametros, valores).Table;
        }

        private DataSet SetControlDocumentosOUT(string idDesvinculacion, string descripcion, string usuario)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@DESCRIPCION",
                "@USUARIO"
            };

            string[] valores = {
                idDesvinculacion,
                descripcion,
                usuario
            };

            return svcFiniquitos.SetControlDocumentosOUT(parametros, valores).Table;
        }

        private DataSet SetBonosAdicionalesOUT(string idDesvinculacion, string bonoNombre, string montoBono, string periodo)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@BONONOMBRE",
                "@MONTOBONO",
                "@PERIODO"
            };

            string[] valores = {
                idDesvinculacion,
                bonoNombre,
                montoBono,
                periodo
            };

            return svcFiniquitos.SetBonosAdicionalesOUT(parametros, valores).Table;
        }

        private DataSet SetHaberesYDescuentosOUT(string idDesvinculacion, string tipo, string descripcion, string monto)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@TIPO",
                "@DESCIPCION",
                "@MONTO"
            };

            string[] valores = {
                idDesvinculacion,
                tipo,
                descripcion,
                monto
            };

            return svcFiniquitos.SetHaberesYDescuentosOUT(parametros, valores).Table;
        }

        private DataSet SetDiasVacacionesOUT(string idDesvinculacion, string diasTotalesTrabajados, string diasTotalesTrabajados2, string year, string year2,
                                             string meses, string meses2, string dias, string dias2, string vacacionesProgresivas, string vacacionesProgresivas2,
                                             string saldo, string saldo2, string vacacionesTomadas, string vacacionesTomadas2, string saldoHabiles, string saldoHabiles2, 
                                             string saldoInhabiles, string saldoInhabiles2, string diasCorridos, string diasCorridos2, string fechaDiasCorridos, string factorCalculo)
        {
            var fechaAContarDe = fechaDiasCorridos.Replace('/', '-').Split(' ')[0].Split('-');
            var fechaAContarDeNew = fechaAContarDe[2] + "-" + fechaAContarDe[1] + "-" + fechaAContarDe[0];

            string[] parametros = {
                "@IDDESVINCULACION",
	            "@DIASTOTALESTRABAJADOS",
	            "@DIASTOTALESTRABAJADOS2",
	            "@YEAR",
	            "@YEAR2",
	            "@MESES",
	            "@MESES2",
	            "@DIAS",
	            "@DIAS2",
	            "@VACACIONESPROGRESIVAS",
	            "@VACACIONESPROGRESIVAS2",
	            "@SALDO",
	            "@SALDO2",
	            "@VACACIONESTOMADAS",
	            "@VACACIONESTOMADAS2",
	            "@SALDOHABILES",
	            "@SALDOHABILES2",
	            "@SALDOINHABILES",
	            "@SALDOINHABILES2",
	            "@DIASCORRIDOS",
	            "@DIASCORRIDOS2",
	            "@FECHADIASCORRIDOS",
	            "@ZONAEXT"
            };

            string[] valores = {
                idDesvinculacion,
                diasTotalesTrabajados,
                diasTotalesTrabajados2,
                year,
                year2,
                meses,
                meses2,
                dias,
                dias2,
                vacacionesProgresivas,
                vacacionesProgresivas2,
                saldo,
                saldo2,
                vacacionesTomadas,
                vacacionesTomadas2,
                saldoHabiles,
                saldoHabiles2,
                saldoInhabiles,
                saldoInhabiles2,
                diasCorridos,
                diasCorridos2,
                fechaAContarDeNew,
                factorCalculo
            };

            return svcFiniquitos.SetDiasVacacionesOUT(parametros, valores).Table;
        }

        private DataSet SetHaberesMensualesOUT(string idDesvinculacion, string sueldoBase, string bonosVariables, string gratificacion, string colacion, string movilizacion,
                                               string baseCalculo, string baseVacaciones)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
	            "@SUELDOBASE",
	            "@BONOSVARIABLES",
	            "@GRATIFICACION",
	            "@COLACION",
	            "@MOVILIZACION",
	            "@BASECALCULO",
	            "@BASEVACACIONES"
            };

            string[] valores = {
                idDesvinculacion,
                sueldoBase,
                bonosVariables,
                gratificacion,
                colacion,
                movilizacion,
                baseCalculo,
                baseVacaciones
            };

            return svcFiniquitos.SetHaberesMensualesOUT(parametros, valores).Table;
        }

        private DataSet SetTotalPagosOUT(string idDesvinculacion, string remuneracionPendiente, string bono, string indemnizacionVoluntaria, string otrosHaberes,
                                         string vacaciones, string mesAviso, string ias, string totalPagos, string totalDescuentos, string totalFiniquito)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
	            "@REMUNERACIONPENDIENTE",
	            "@BONO",
	            "@INDEMNIZACIONVOLUNTARIA",
	            "@OTROSHABERES",
	            "@VACACIONES",
	            "@MESAVISO",
	            "@IAS",
	            "@TOTALPAGOS",
	            "@TOTALDESCUENTOS",
	            "@TOTALFINIQUITO"
            };

            string[] valores = {
                idDesvinculacion,
                remuneracionPendiente,
                bono,
                indemnizacionVoluntaria,
                otrosHaberes,
                vacaciones,
                mesAviso,
                ias,
                totalPagos,
                totalDescuentos,
                totalFiniquito
            };

            return svcFiniquitos.SetTotalPagosOUT(parametros, valores).Table;
        }
        

        #endregion

        #region "EST"

        public DataSet GetCausalesService
        {
            get
            {
                return GetCausales();
            }
        }

        public DataSet GetFiniquitosConfirmadosService
        {
            get
            {
                return GetFiniquitosConfirmados(MEDIOPAGO);
            }
        }

        public DataSet GetFiniquitosRecientesService
        {
            get
            {
                return GetFiniquitosRecientes(FILTRARPOR, FILTRO, USUARIO);
            }
        }

        public DataSet SetAnularFolioFiniquitoService
        {
            get
            {
                return SetAnularFolioFiniquito(IDDESVINCULACION);
            }
        }

        public DataSet SetFiniquitoPagadoService
        {
            get
            {
                return SetFiniquitoPagado(IDDESVINCULACION, USUARIO);
            }
        }

        public DataSet GetFiniquitosSolicitudesBajaService
        {
            get
            {
                return GetFiniquitosSolicitudesBaja(RUTTRABAJADOR);
            }
        }

        public DataSet GetFiniquitosSolicitudBajaService
        {
            get
            {
                return GetFiniquitosSolicitudBaja(IDDESVINCULACION);
            }
        }

        public DataSet GetCalculoPartimeEspecialService
        {
            get
            {
                return GetCalculoPartimeEspecial(CARGOMOD, FECHATERMINO);
            }
        }


        /** METODOS PRIVADOS DE CONSUMO DE SERVICIO */

        private DataSet GetCausales()
        {
            return svcFiniquitos.GetCausales().Table;
        }

        private DataSet GetFiniquitosConfirmados(string medioPago)
        {
            string[] parametros = {
                "@MEDIOPAGO"
            };

            string[] valores = {
                medioPago
            };

            return svcFiniquitos.GetFiniquitosConfirmados(parametros, valores).Table;
        }

        private DataSet GetFiniquitosRecientes(string filtrarPor, string filtro, string usuario)
        {
            string[] parametros = {
                "@FILTRARPOR",
                "@FILTRO",
                "@USUARIO"
            };

            string[] valores = {
                filtrarPor,
                filtro,
                usuario
            };

            return svcFiniquitos.GetFiniquitosRecientes(parametros, valores).Table;
        }

        private DataSet SetAnularFolioFiniquito(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.SetAnularFolioFiniquito(parametros, valores).Table;
        }

        private DataSet SetFiniquitoPagado(string idDesvinculacion, string usuario)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@USUARIO"
            };

            string[] valores = {
                idDesvinculacion,
                usuario
            };

            return svcFiniquitos.SetFiniquitoPagado(parametros, valores).Table;
        }

        private DataSet GetFiniquitosSolicitudesBaja(string rutTrabajador)
        {

            string[] parametros = {
                "@RUTTRABAJADOR"
            };

            string[] valores = {
                rutTrabajador
            };

            return svcFiniquitos.GetFiniquitosSolicitudesBaja(parametros, valores).Table;
        }

        private DataSet GetFiniquitosSolicitudBaja(string idDesvinculacion)
        {

            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetFiniquitosSolicitudBaja(parametros, valores).Table;
        }

        private DataSet GetCalculoPartimeEspecial(string cargoMod, string fechaTermino)
        {

            string[] parametros = {
                "@CARGOMOD",
                "@FECHATERMINO"
            };

            string[] valores = {
                cargoMod,
                fechaTermino
            };

            return svcFiniquitos.GetCalculoPartimeEspecial(parametros, valores).Table;
        }

        /** METODOS PUBLICOS DE ACCESO A METODOS DE SERVICIO SOLICITUD DE BAJA, CALCULO */

        public DataSet SetCrearModificarEncDesvinculacionService
        {
            get
            {
                return SetCrearModificarEncDesvinculacion(IDDESVINCULACION, FECHAHORASOLICITUD, FECHAAVISODESVINCULACION, USUARIO, RUTTRABAJADOR, FICHA, NOMBRETRABAJADOR,
                                                          ESTADOSOLICITUD, FECHAESTADO, IDCAUSAL, IDEMPRESA, OBSERVACION, IDRENUNCIA, SOLICITUD, CARGOMOD, CLIENTE, AREANEGOCIO,
                                                          ESPARTTIME, ESZONAEXT, CODIGOSOLICITUD, CCAF, FECHACARTABAJA, EXCEPCIONCOSTOCERO, TOTALFINIQUITOS, NIVEL , CATEGORIA);
            }
        }

        /** METODOS PRIVADOS DE CONSUMO DE SERVICIO SOLICITUD DE BAJA, CALCULO */


        private DataSet SetCrearModificarEncDesvinculacion(string idDesvinculacion, string fechaHoraSolicitud, string fechaAvisoDesvinculacion, string usuarioSolicitud,
                                                           string rutTrabajador, string fichaTrabajador, string nombreTrabajador, string estadoSolicitud,
                                                           string fechaEstado, string idCausal, string idEmpresa, string observacion, string idRenuncia, string solicitud,
                                                           string cargoMod, string cliente, string areaNegocio, string esPartTime, string esZonaExt, string codigoSolicitud,
                                                           string ccaf, string fechaCartaBaja, string excepcionCostoCero, string totalFiniquitos, string nivel, string categoria)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@FECHAHORASOLICITUD",
                "@FECHAAVISODESVINCULACION",
                "@USUARIOSOLICITUD",
                "@RUTTRABAJADOR",
                "@FICHATRABAJADOR",
                "@NOMBRECOMPLETOTRABAJADOR",
                "@ESTADOSOLICITUD",
                "@FECHAESTADO",
                "@IDCAUSAL",
                "@IDEMPRESA", 
                "@OBSERVACIONES",
                "@CARGOMOD",
                "@AREANEGOCIO",
                "@CLIENTE",
                "@ESPARTIME",
                "@ESZONAEXT",
                "@IDRENUNCIA",
                "@SOLICITUD",
                "@CODIGOSOLICITUD",
                "@CCAF",
                "@FECHACARTABAJA",
                "@EXCEPCIONCOSTOCERO",
                "@TOTALFINIQUITO",
                "@NIVEL",
                "@CATEGORIA"
            };

            string[] valores = {
                idDesvinculacion,
                fechaHoraSolicitud,
                fechaAvisoDesvinculacion,
                usuarioSolicitud,
                rutTrabajador,
                fichaTrabajador,
                nombreTrabajador,
                estadoSolicitud,
                fechaEstado,
                idCausal,
                idEmpresa,
                observacion,
                cargoMod,
                areaNegocio,
                cliente,
                esPartTime,
                esZonaExt,
                idRenuncia,
                solicitud,
                codigoSolicitud,
                ccaf,
                fechaCartaBaja,
                excepcionCostoCero,
                totalFiniquitos,
                nivel,
                categoria
            };

            return svcFiniquitos.SetCrearModificarEncDesvinculacion(parametros, valores).Table;
        }
        
        /** METODOS PUBLICOS DE ACCESO A METODOS DE SERVICIO PARA PAGOS */

        public DataSet GetContratosCalculoPagosService
        {
            get
            {
                return GetContratosCalculoPagos(IDDESVINCULACION);
            }
        }

        public DataSet SetChequeDisponiblePagoPagosService
        {
            get
            {
                return SetChequeDisponiblePagoPagos(IDTRACKING, USUARIO);
            }
        }

        public DataSet GetEmpresasPagosService
        {
            get
            {
                return GetEmpresasPago();
            }
        }

        public DataSet GetBancosPagosService
        {
            get
            {
                return GetBancosPago();
            }
        }

        public DataSet GetTipoPagoService
        {
            get
            {
                return GetTipoPago();
            }
        }

        public DataSet GetTotalesPagosESTService
        {
            get
            {
                return GetTotalesPagosEST(IDDESVINCULACION);
            }
        }

        public DataSet GetValidaPagosService
        {
            get
            {
                return GetValidaDocumentoPagos(NUMERODOCUMENTO);
            }
        }

        public DataSet GetObtenerPagosService
        {
            get
            {
                return GetObtenerPagoPagos(TIPOPAGO, FILTRARPOR, FILTRO, FILTRO2, FILTRO3);
            }
        }

        public DataSet GetDataChequePagosService
        {
            get
            {
                return GetDataChequePagos(IDDESVINCULACION);
            }
        }

        public DataSet GetObtenerProveedorService
        {
            get
            {
                return GetObtenerProveedor(RUTPROVEEDOR);
            }

        }

        public DataSet SetRegistrarPagoProveedoresService
        {
            get
            {
                return SetRegistrarPagoProveedores(RUTPROVEEDOR, MONTO, IDCORRELATIVO, NSERIECHEQUE, USUARIO, IDEMPRESA, OBSERVACION);
            }
        }

        public DataSet GetObtenerCorrelativoDisponibleProveedoresService
        {
            get
            {
                return GetObtenerCorrelativoDisponibleProveedores(IDEMPRESA);
            }
        }

        public DataSet GetObtenerClientePagosService
        {
            get
            {
                return GetObtenerClientePagos();
            }
        }

        public DataSet GetObtenerTrabajadoresPagosService
        {
            get
            {
                return GetObtenerTrabajadoresPagos();
            }
        }

        public DataSet GetObtenerMontoChequePagosService
        {
            get
            {
                return GetObtenerMontoChequePagos(IDDESVINCULACION);
            }
        }

        public DataSet SetInsertarRegistroPagoPagosService
        {
            get
            {
                return SetInsertarRegistroPagoPagos(TIPOPAGO, NUMERODOCUMENTO, NSERIECHEQUE, BANCO, EMPRESA, MONTOVACACIONES, MONTOIAS, MONTODESHAUCIO, MONTOINDEMNIZACION,
                                                 MONTOFINIQUITO, USUARIO, IDDESVINCULACION, NOMBRETRABAJADOR, CLIENTE, CLIENTESIGLA);
            }
        }

        /** PAGOS */

        private DataSet SetInsertarRegistroPagoPagos(string tipoPago, string numeroDocumento, string nSerieCheque, string banco, string empresa, string montoVacaciones,
                                                     string montoIAS, string montoDeshaucio, string montoIndeminizacion, string montoFiniquito, string usuario,
                                                     string idDesvinculacion, string nombreTrabajador, string clienteNombre, string clienteSigla)
        {
            string[] parametros = {
                "@TIPOPAGO",
                "@NUMERODOCUMENTO",
                "@NSERIEDOCUMENTO",
                "@BANCO",
                "@EMPRESA",
                "@MONTO_VACACIONES",
                "@MONTO_IAS",
                "@MONTO_DESHAUCIO",
                "@MONTO_INDEMNVOLUNTARIA",
                "@MONTO_FINIQUITO",
                "@USUARIO",
                "@IDDESVINCULACION",
                "@NOMBRETRABAJADOR",
                "@CLIENTENOMBRE",
                "@CLIENTESIGLA"
            };

            string[] valores = {
                tipoPago,
                numeroDocumento,
                nSerieCheque,
                banco,
                empresa,
                montoVacaciones,
                montoIAS,
                montoDeshaucio,
                montoIndeminizacion,
                montoFiniquito,
                usuario,
                idDesvinculacion,
                nombreTrabajador,
                clienteNombre,
                clienteSigla
            };

            return svcFiniquitos.SetInsertarRegistroPagoPagos(parametros, valores).Table;
        }

        private DataSet GetContratosCalculoPagos(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetContratosCalculoPagos(parametros, valores).Table;
        }

        private DataSet SetChequeDisponiblePagoPagos(string idTracking, string usuario)
        {
            string[] parametros = {
                "@IDTRACKING",
                "@USUARIO"
            };

            string[] valores = {
                idTracking,
                usuario
            };

            return svcFiniquitos.SetChequeDisponiblePagoPagos(parametros, valores).Table;
        }

        private DataSet GetEmpresasPago()
        {
            return svcFiniquitos.GetEmpresasPagos().Table;
        }

        private DataSet GetBancosPago()
        {
            return svcFiniquitos.GetBancosPagos().Table;
        }

        private DataSet GetTipoPago() 
        {
            return svcFiniquitos.GetTipoPagoPagos().Table;
        }

        private DataSet GetTotalesPagosEST(string idDesvinculacion)
        { 
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetTotalesPagosEST(parametros, valores).Table;
        }

        private DataSet GetValidaDocumentoPagos(string numeroDocumento)
        {
            string[] parametros = {
                "@NUMERO_CHEQUE"
            };

            string[] valores = {
                numeroDocumento
            };

            return svcFiniquitos.GetValidaDocumentoPagos(parametros, valores).Table;
        }

        private DataSet GetObtenerPagoPagos(string tipoPago, string filtrarPor, string filtro, string filtro2, string filtro3)
        {
            string[] parametros = {
                "@TIPOPAGO",
                "@FILTRARPOR",
                "@FILTRO",
                "@FILTRO2",
                "@FILTRO3"
            };

            string[] valores = {
                tipoPago,
                filtrarPor,
                filtro,
                filtro2,
                filtro3
            };

            return svcFiniquitos.GetObtenerPagoPagos(parametros, valores).Table;
        }

        private DataSet GetDataChequePagos(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                IDDESVINCULACION
            };

            return svcFiniquitos.GetDataChequePagos(parametros, valores).Table;
        }

        private DataSet GetObtenerProveedor(string rutProveedor)
        {
            string[] parametros = {
                "@RUTPROVEEDOR"
            };

            string[] valores = {
                rutProveedor
            };

            return svcFiniquitos.GetObtenerProveedores(parametros, valores).Table;
        }

        private DataSet SetRegistrarPagoProveedores(string rutProveedor, string monto, string idCorrelativo, string nSerieCheque, string usuario, string empresa, string observacion)
        {
            string[] parametros = {
                "@RUTPROVEEDOR",
                "@MONTO",
                "@IDCORRELATIVO",
                "@NSERIECHEQUE",
                "@USUARIO",
                "@EMPRESA",
                "@OBSERVACION"
            };

            string[] valores = {
                rutProveedor,
                monto,
                idCorrelativo,
                nSerieCheque,
                usuario,
                empresa,
                observacion
            };

            return svcFiniquitos.SetRegistrarPagoProveedores(parametros, valores).Table;
        }

        private DataSet GetObtenerCorrelativoDisponibleProveedores(string empresa) 
        {

            string[] parametros = {
                "@EMPRESA"
            };

            string[] valores = {
                empresa
            };

            return svcFiniquitos.GetObtenerCorrelativoDisponibleProveedores(parametros, valores).Table;
        }

        private DataSet GetObtenerClientePagos()
        {
            return svcFiniquitos.GetObtenerClientesPagos().Table;
        }

        private DataSet GetObtenerTrabajadoresPagos()
        {
            return svcFiniquitos.GetObtenerTrabajadoresPagos().Table;
        }

        private DataSet GetObtenerMontoChequePagos(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetObtenerMontoChequePagos(parametros, valores).Table;
        }

        /** SECCION DE VISUALIZACION DE CALCULO DE FINIQUITO */

        public DataSet GetVisualizarDatosTrabajadorCalculoService
        {
            get
            {
                return GetVisualizarDatosTrabajadorCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetValidarConfirmadoFiniquitoCalculoService
        {
            get
            {
                return GetValidarConfirmadoFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarContratosCalculoService
        {
            get
            {
                return GetVisualizarContratosCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarDocumentosCalculoService
        {
            get
            {
                return GetVisualizarDocumentosCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarPeriodosCalculoService
        {
            get
            {
                return GetVisualizarPeriodosCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarTotalDiasCalculoCalculoService
        {
            get
            {
                return GetVisualizarTotalDiasCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarOtrosHaberesFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarOtrosHaberesFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarDescuentosFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarDescuentosFiniquitoCalculo(IDDESVINCULACION);
            }
        }


        public DataSet GetVisualizarDiasVacacionesCalculoService
        {
            get
            {
                return GetVisualizarDiasVacacionesCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarTotalesFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarTotalesFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarHabaeresMensualFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarHabaeresMensualFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarConceptosAdicionalesFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarConceptosAdicionalesFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarBonosAdicionalesFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarBonosAdicionalesFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet GetVisualizarValorUfFiniquitoCalculoService
        {
            get
            {
                return GetVisualizarValorUfFiniquitoCalculo(IDDESVINCULACION);
            }
        }

        public DataSet SetConfirmarRetiroCalculoService
        {
            get
            {
                return SetConfirmarRetiroCalculo(IDDESVINCULACION, MEDIOPAGO, MONTO, OBSERVACION);
            }
        }

        public DataSet GetCentroCostosService
        {
            get
            {
                return GetCentroCostos(FICHA);
            }
        }

        public DataSet GetCentroCostosOUTService
        {
            get
            {
                return GetCentroCostosOUT(FICHA);
            }
        }

        public DataSet GetCargoService
        {
            get
            {
                return GetCargo(FICHA);
            }
        }

        public DataSet GetCargoOUTService
        {
            get
            {
                return GetCargoOUT(FICHA);
            }
        }

        private DataSet GetVisualizarDatosTrabajadorCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarDatosTrabajadorCalculo(parametros, valores).Table;
        }

        private DataSet GetValidarConfirmadoFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetValidarConfirmadoFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarContratosCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarContratosCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarDocumentosCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarDocumentosCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarPeriodosCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarPeriodosCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarTotalDiasCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarTotalDiasCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarOtrosHaberesFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarOtrosHaberesFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarDescuentosFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarDescuentosFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarDiasVacacionesCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarDiasVacacionesCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarTotalesFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarTotalesFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarHabaeresMensualFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarHabaeresMensualFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarConceptosAdicionalesFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarConceptosAdicionalesFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarBonosAdicionalesFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarBonosAdicionalesFiniquitoCalculo(parametros, valores).Table;
        }

        private DataSet GetVisualizarValorUfFiniquitoCalculo(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.GetVisualizarValorUfFiniquitoCalculo(parametros, valores).Table;
        }

        /** */

        private DataSet SetConfirmarRetiroCalculo(string idDesvinculacion, string medioPago, string monto, string observacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@MEDIOPAGO",
                "@MONTO",
                "@OBSERVACION"
            };

            string[] valores = {
                idDesvinculacion,
                medioPago,
                monto,
                observacion
            };

            return svcFiniquitos.SetConfirmarRetiroCalculo(parametros, valores).Table;
        }

        private DataSet GetCentroCostos(string ficha)
        {
            string[] valores = {
                ficha
            };

            return svcFiniquitos.GetCentroCostos(valores).Table;
        }

        private DataSet GetCentroCostosOUT(string ficha)
        {
            string[] valores = {
                ficha
            };

            return svcFiniquitos.GetCentroCostosOUT(valores).Table;
        }

        private DataSet GetCargo(string ficha)
        {
            string[] valores = {
                ficha
            };

            return svcFiniquitos.GetCargo(valores).Table;
        }

        private DataSet GetCargoOUT(string ficha)
        {
            string[] valores = {
                ficha
            };

            return svcFiniquitos.GetCargoOUT(valores).Table;
        }

        /** PROVEEDORES */

        public DataSet SetTMPCargaChequeProveedorProveedoresService
        {
            get
            {
                return SetTMPCargaChequeProveedorProveedores(NOMBRETRABAJADOR, MONTO, DIA, MES, YEAR, EMPRESA, CIUDAD, CIFRA, NSERIECHEQUE, NOMINATIVO, OBSERVACION);
            }
        }

        public DataSet SetTMPInitProcessChequeProveedorProveedoresService
        {
            get
            {
                return SetTMPInitProcessChequeProveedorProveedores();
            }
        }

        public DataSet GetTMPValidateProcessInitProveedoresService
        {
            get
            {
                return GetTMPValidateProcessInitProveedores();
            }
        }

        public DataSet SetTMPCloseProcessChequeProveedorService
        {
            get
            {
                return SetTMPCloseProcessChequeProveedor();
            }
        }

        public DataSet GetTMPChequesInProcessProveedorService
        {
            get
            {
                return GetTMPChequesInProcessProveedor();
            }
        }

        private DataSet SetTMPCargaChequeProveedorProveedores(string nombreTrabajador, string monto, string dia, string mes, string year, string empresa, string ciudad, 
                                                              string cifra, string serie, string nominativo, string observacion)
        {

            string[] parametros = {
                "@NOMBRETRABAJADOR",
                "@MONTO",
                "@DIA",
                "@MES",
                "@YEAR",
                "@EMPRESA",
                "@CIUDAD",
                "@SERIE",
                "@CIFRA",
                "@NOMINATIVO",
                "@OBSERVACION"
            };

            string[] valores = {
                nombreTrabajador,
                monto,
                dia, 
                mes,
                year,
                empresa,
                ciudad,
                serie,
                cifra,
                nominativo,
                observacion
            };

            return svcFiniquitos.SetTMPCargaChequeProveedorProveedores(parametros, valores).Table;
        }

        private DataSet SetTMPInitProcessChequeProveedorProveedores()
        {
            return svcFiniquitos.SetTMPInitProcessChequeProveedorProveedores().Table;
        }

        private DataSet GetTMPValidateProcessInitProveedores()
        {
            return svcFiniquitos.GetTMPValidateProcessInitProveedores().Table;
        }

        private DataSet SetTMPCloseProcessChequeProveedor()
        {
            return svcFiniquitos.SetTMPCloseProcessChequeProveedor().Table;
        }

        private DataSet GetTMPChequesInProcessProveedor()
        {
            return svcFiniquitos.GetTMPChequesInProcessProveedor().Table;
        }

        /** PROVEEDORES MANTENCION */

        public DataSet GetObtenerProveedoresProveedorService
        {
            get
            {
                return GetObtenerProveedoresProveedor();
            }
        }

        private DataSet GetObtenerProveedoresProveedor()
        {
            return svcFiniquitos.GetObtenerProveedoresProveedor().Table;
        }

        /** END PROVEEDORES MANTENCIONM */

        /** END PROVEEDORES */

        /** FINIQUITOS CONFIRMADOS */

        public DataSet SetTMPCargaChequeFiniquitosService
        {
            get
            {
                return SetTMPCargaChequeFiniquitos(NOMBRETRABAJADOR, MONTO, DIA, MES, YEAR, EMPRESA, CIUDAD, CIFRA, NSERIECHEQUE, NOMINATIVO);
            }
        }

        public DataSet SetTMPInitProcessChequeFiniquitosService
        {
            get
            {
                return SetTMPInitProcessChequeFiniquitos();
            }
        }

        public DataSet GetTMPValidateProcessInitFiniquitosService
        {
            get
            {
                return GetTMPValidateProcessInitFiniquitos();
            }
        }

        public DataSet SetTMPCloseProcessChequeFiniquitosService
        {
            get
            {
                return SetTMPCloseProcessChequeFiniquitos();
            }
        }

        public DataSet GetTMPChequesInProcessFiniquitosService
        {
            get
            {
                return GetTMPChequesInProcessFiniquitos();
            }
        }

        private DataSet SetTMPCargaChequeFiniquitos(string nombreTrabajador, string monto, string dia, string mes, string year, string empresa, string ciudad, string cifra, string serie, string nominativo)
        {

            string[] parametros = {
                "@NOMBRETRABAJADOR",
                "@MONTO",
                "@DIA",
                "@MES",
                "@YEAR",
                "@EMPRESA",
                "@CIUDAD",
                "@SERIE",
                "@CIFRA",
                "@NOMINATIVO"
            };

            string[] valores = {
                nombreTrabajador,
                monto,
                dia,
                mes,
                year,
                empresa,
                ciudad,
                serie,
                cifra,
                nominativo
            };

            return svcFiniquitos.SetTMPCargaChequeFiniquitos(parametros, valores).Table;
        }

        private DataSet SetTMPInitProcessChequeFiniquitos()
        {
            return svcFiniquitos.SetTMPInitProcessChequeFiniquitos().Table;
        }

        private DataSet GetTMPValidateProcessInitFiniquitos()
        {
            return svcFiniquitos.GetTMPValidateProcessInitFiniquitos().Table;
        }

        private DataSet SetTMPCloseProcessChequeFiniquitos()
        {
            return svcFiniquitos.SetTMPCloseProcessChequeFiniquitos().Table;
        }

        private DataSet GetTMPChequesInProcessFiniquitos()
        {
            return svcFiniquitos.GetTMPChequesInProcessFiniquitos().Table;
        }

        /** END FINIQUITOS CONFIRMADOS */

        #endregion

        #region "GENERAL"

        public DataSet GetReporteriaFiniquitosService
        {
            get
            {
                return GetReporteriaFiniquitos(FILTRO, FILTRARPOR);
            }
        }

        public DataSet SetAnularPagoPagosService
        {
            get
            {
                return SetAnularPagoPagos(IDDESVINCULACION, MOTIVOANULACION, USUARIO);
            }
        }

        public DataSet SetNotariadoPagosService
        {
            get
            {
                return SetNotariadoPagos(IDDESVINCULACION, USUARIO);
            }
        }

        public DataSet SetRevertirConfirmacionPagosService
        {
            get
            {
                return SetRevertirConfirmacionPagos(IDDESVINCULACION);
            }
        }

        public DataSet SetInsertarProveedorService
        {
            get
            {
                return SetInsertarProveedor(RUT, NOMBRE, TIPO);
            }
        }

        public DataSet GetVisualizarTiposProveedoresService
        {
            get
            {
                return GetVisualizarTiposProveedores();
            }
        }

        private DataSet GetReporteriaFiniquitos(string filtro, string filtrarPor)
        {
            string[] parametros = {
                "@FILTRO",
                "@FILTRARPOR"
            };

            string[] valores = {
                filtro,
                filtrarPor
            };

            return svcFiniquitos.GetReporteriaFiniquitos(parametros, valores).Table;
        }

        private DataSet SetAnularPagoPagos(string idDesvinculacion, string motivoAnulacion, string usuario)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@MOTIVOANULACION",
                "@USUARIO"
            };

            string[] valores = {
                idDesvinculacion,
                motivoAnulacion,
                usuario
            };

            return svcFiniquitos.SetAnularPagoPagos(parametros, valores).Table;
        }

        private DataSet SetRevertirConfirmacionPagos(string idDesvinculacion)
        {
            string[] parametros = {
                "@IDDESVINCULACION"
            };

            string[] valores = {
                idDesvinculacion
            };

            return svcFiniquitos.SetRevertirConfirmacionPagos(parametros, valores).Table;
        }

        private DataSet SetNotariadoPagos(string idDesvinculacion, string usuario)
        {
            string[] parametros = {
                "@IDDESVINCULACION",
                "@USUARIO"
            };

            string[] valores = {
                idDesvinculacion,
                usuario
            };

            return svcFiniquitos.SetNotariadoPagos(parametros, valores).Table;
        }

        private DataSet SetInsertarProveedor(string rut, string nombre, string tipo)
        {
            string[] parametros = {
                "@RUT",
                "@NOMBRE",
                "@TIPO"
            };

            string[] valores = {
                rut,
                nombre,
                tipo
            };

            return svcFiniquitos.SetInsertarProveedor(parametros, valores).Table;
        }

        private DataSet GetVisualizarTiposProveedores()
        {
            return svcFiniquitos.GetVisualizarTiposProveedores().Table;
        }
        
        #endregion

    }
}
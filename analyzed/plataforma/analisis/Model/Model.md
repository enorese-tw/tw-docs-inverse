# Documentación de Teamwork.Model

Este documento proporciona un análisis detallado de la capa `Teamwork.Model` de la aplicación PlataformaTeamwork. Esta capa contiene principalmente Objetos de Transferencia de Datos (DTOs) y entidades que representan la estructura de la información utilizada en el sistema.

## Estructura General

La capa `Teamwork.Model` está organizada en los siguientes módulos funcionales:

- **Asistencia**: Modelos relacionados con el control de asistencia, jornadas laborales, horas extras y reportes.
- **Autentificacion**: Modelos para la gestión de tokens y redirección de autenticación.
- **Bajas**: Modelos para la gestión de finiquitos, renuncias y desvinculaciones.
- **Dashboard**: Modelos para la visualización de datos en los tableros de control.
- **Finanzas**: Modelos para cálculos financieros, provisiones, pagos y estructuras de cargos.
- **General**: Modelos de utilidad general como paginación y respuestas estándar.
- **Kam**: Modelos específicos para la gestión de cuentas (Key Account Management), contratos y fichas de personal.
- **Operaciones**: Modelos para la gestión operativa, procesos masivos, solicitudes y flujos de trabajo.
- **Persona**: Modelos básicos de clientes.
- **Seleccion**: Modelos para el proceso de reclutamiento y selección de personal.
- **Teamwork**: Modelos específicos de la organización Teamwork.

---

## Módulo: Asistencia

Este módulo define las estructuras de datos para gestionar la asistencia de los colaboradores.

### Clases Principales

#### `ArchivoAsistencia`
Representa un archivo de asistencia cargado al sistema.
- **Propiedades**: `CodigoArchivo`, `Empresa`, `Ficha`, `Cliente`, `RutaFichero`, metadatos de creación/modificación.
- **Uso**: Almacenar información sobre archivos de asistencia importados.

#### `Asistencia`
Representa el registro de asistencia de un colaborador en una fecha específica.
- **Propiedades**: `Empresa`, `Ficha`, `Fecha`, `CodigoAsistencia` (tipo de marca), `Observacion`.
- **Estilo**: Incluye propiedades para renderizado UI (`Class`, `Color`, `Style`).

#### `JornadaLaboral` / `FichaJornadaLaboral`
Definen los tipos de jornadas laborales y la asignación de estas a las fichas de los colaboradores.
- **Propiedades**: `CodigoJornada`, `NombreJornada`, `HorasSemanal`, `DiasHHEE`, `PorcentajePago`.

#### `HorasExtras`
Estructura para el registro y cálculo de horas extras.
- **Datos**: `HoraExtra` (cantidad), `DiasHHEE`, `PorcentajePago`.

#### `ReporteAsistencia`
DTO complejo utilizado para generar reportes de asistencia.
- **Lógica**: Contiene propiedades calculadas o agregadas como `Dias` (asistidos), `TopeLegal`, `Causal` de inasistencia.
- **Uso**: Alimentar vistas o exportaciones de reportes de asistencia.

---

## Módulo: Autentificacion

#### `AuthRedirect` (Clase: `TokenConfianza`)
Estructura simple para manejar tokens de confianza.
- **Datos**: `Username`, `Token`.

---

## Módulo: Bajas (Finiquitos)

Este módulo es crítico para el proceso de desvinculación y cálculo de finiquitos.

#### `Finiquito`
DTO central para la gestión de un finiquito.
- **Campos Clave**: `CodigoFiniquito`, `Folio`, `Causal`, `TotalFiniquito`, `Estado`.
- **Lógica de UI**: Contiene numerosas propiedades `Opt...` que actúan como banderas (flags) para habilitar/deshabilitar opciones en la interfaz de usuario (ej: `OptPagarFiniquito`, `OptValidarFinanzas`).

#### `SolicitudPago`
Estructura para procesar pagos de finiquitos.
- **Datos Financieros**: `MontoFiniquito`, `MontoAdm` (administrativo), `Total`.
- **Datos Bancarios**: `Banco`, `Cuenta`, `Beneficiario`.

#### `Complementos`, `HaberesComplemento`, `DescuentoComplemento`
Manejan items adicionales (haberes o descuentos) que se agregan al finiquito base.
- **Uso**: Permite flexibilidad en el cálculo final del finiquito agregando bonos o deducciones específicas.

---

## Módulo: Finanzas

Contiene estructuras para cálculos de remuneraciones y provisiones.

#### `EstructuraCargo`
DTO extenso y fundamental que define la estructura de costos y remuneraciones de un cargo.
- **Datos Salariales**: `SueldoBase`, `SueldoLiquido`, `Gratificacion`, `BaseImponible`.
- **Cargas Sociales**: `PorcAFP`, `PorcSalud`, `PorcSeguroDesempleo`.
- **Totales**: `TotalImponible`, `TotalTributable`, `TotalHaberes`, `TotalDescuentos`.
- **Listas Anidadas**: Contiene listas de `Bonos` (`BonosCargoMod`) y `AsignacionesNOImponibles` (`ANIsCargoMod`).
- **Lógica Implícita**: Agrupa todos los factores necesarios para simular o calcular una liquidación de sueldo.

#### `Provision` / `ProvisionMargen`
Modelan las provisiones financieras asociadas a un colaborador o centro de costo.
- **Datos**: `Concepto`, `Percentage`, `MontoCLP`.
- **Lógica**: `ProvisionMargen` incluye opciones de gestión (`OptCrear`, `OptEditar`) y referencias a constantes de cálculo (`ConstCalculoProvMargen`).

#### `ProcesosPago`
Gestiona lotes o grupos de pagos.
- **Acciones**: `OptPagarMasivo`, `OptIniciarProcesoMasivoPago`.

---

## Módulo: Operaciones

Este módulo maneja el flujo operativo principal del sistema.

#### `Solicitud`
DTO que orquesta las solicitudes de servicios o cambios.
- **Identificadores**: `CodigoSolicitud`, `TipoEvento`, `NombreProceso`.
- **Estado y Flujo**: `Estado`, `Prioridad`, `AsignadoA`.
- **Acciones**: Banderas como `OptAsignarSolicitud`, `OptAnularSolicitud` controlan las acciones disponibles según el estado del flujo.

#### `SolicitudCargo`
Vincula una solicitud con un cargo específico (`CargoMod`).
- **Datos**: `CodigoCargoMod`, `NombreCargo`.
- **Acciones**: `OptEditar`, `OptRechazar`, `OptPublicar`.

#### `ProcesoCargaMasiva` y Reportes Asociados
Clases diseñadas para manejar la carga masiva de datos (ej: Contratos, Renovaciones) desde archivos Excel.
- **Configuración de Carga**: `UploadNombrePlantilla`, `UploadColumnas`, `UploadNodoPadre`.
- **Reportes**: `ReporteCargaMasivaContrato` y `ReporteCargaMasivaRenovacion` mapean las filas de los archivos Excel importados para su procesamiento y validación.

#### `Dashboard` / `DashboardFiniquitos`
Agrupan contadores y estadísticas para los paneles de control.
- **Métricas**: `Simulaciones`, `CotizacionesAprobadas`, `PendienteFinanzas`, `Calculados`, `Confirmados`.

---

## Módulo: Kam (Key Account Management)

#### `Contrato`
Detalla las condiciones contractuales de un colaborador.
- **Datos**: `Ficha`, `Vigencia` (`FechaInicioContrato`, `FechaTerminoContrato`), `HorasSemanales`, `SueldoBase` (referenciado implícitamente o por cargo).
- **Ubicación**: Datos de la sucursal (`NombreSucursal`, `DireccionSucursal`).

#### `FichaPersonal`
Información personal y demográfica completa del colaborador.
- **Datos**: `Rut`, `Nombres`, `Direccion`, `Prevision` (`Afp`, `Salud`), `DatosBancarios`.

---

## Módulo: Seleccion

Maneja el reclutamiento.

#### `OfertaLaboral`
Define una vacante disponible.
- **Datos**: `Renta`, `Lugar`, `Cupos`, `Horario`, `Estado`, `Psicologa` asignada.

#### `Postulante`
Datos de un candidato.
- **Datos**: Contacto e identificación básica.

---
**Nota General**: Esta capa no contiene implementación de reglas de negocio complejas (métodos), sino que define la "forma" de los datos que fluyen entre la Base de Datos, la API y la Interfaz de Usuario. La lógica de negocio reside principalmente en los Servicios o Controladores que manipulan estas estructuras.

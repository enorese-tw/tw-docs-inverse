# Análisis Profundo - Capa Frontend JavaScript

## Directorio Analizado
`FiniquitosV2/Scripts/Finiquitos/ajax/`

## Índice
1. [Resumen Ejecutivo](#resumen-ejecutivo)
2. [Arquitectura General del Frontend](#arquitectura-general)
3. [Dependencias y Librerías](#dependencias)
4. [Patrón de Comunicación con el Backend](#patron-comunicacion)
5. [Análisis Detallado por Archivo](#analisis-detallado)
6. [Catálogo Completo de Endpoints](#catalogo-endpoints)
7. [Cálculos Identificados en el Frontend](#calculos)
8. [Código Comentado y No Utilizado](#codigo-comentado)
9. [Riesgos de Seguridad](#riesgos)
10. [Observaciones Generales](#observaciones)

---

## 1. Resumen Ejecutivo <a name="resumen-ejecutivo"></a>

Se analizaron **11 archivos JavaScript** que componen la totalidad de la lógica frontend del sistema de finiquitos. Estos archivos suman aproximadamente **5.500 líneas de código** y contienen:

- **+60 llamadas AJAX** a endpoints del backend (WebMethods en páginas `.aspx`).
- **Cálculos financieros ejecutados en el cliente** (promedios de remuneraciones), lo cual es un riesgo crítico.
- **Código comentado** en `VisualizacionCalculoFiniquito.js` (líneas 998-1017) e `Inicio.js` (líneas 1083, 1101) que revelan funcionalidad deshabilitada.
- **Dependencias externas no controladas** (imagen de spinner desde `angelsysdoc.com`).
- **Construcción masiva de HTML por concatenación de strings**, dificultando el mantenimiento.

| Archivo | Líneas | Complejidad | Rol Principal |
|---|---|---|---|
| `VisualizacionCalculoFiniquito.js` | 1483 | **Muy Alta** | Detalle del cálculo de finiquito |
| `Inicio.js` | 1267 | **Muy Alta** | Dashboard principal / gestión de estados |
| `SolicitudBaja.js` | 809 | **Alta** | Formulario de solicitud de baja |
| `PagoProveedores.js` | 778 | **Alta** | Generación de cheques proveedores |
| `BasePagosFiniquitos.js` | 646 | **Media** | Listado pagos finiquitos |
| `XmlSolB4J.js` | 638 | **Alta** | Bandeja aprobación solicitudes |
| `Utils.js` | 434 | **Media** | Funciones utilitarias |
| `BasePagosProveedores.js` | 350 | **Media** | Listado pagos proveedores |
| `AdministradorDeProveedores.js` | 78 | **Baja** | ABM proveedores |
| `PagosFiniquitos.js` | 60 | **Baja** | Validación documentos pago |
| `Loader.js` | 40 | **Baja** | Spinner de carga (UI) |

---

## 2. Arquitectura General del Frontend <a name="arquitectura-general"></a>

El frontend sigue un patrón **monolítico basado en jQuery** donde cada página `.aspx` tiene un archivo `.js` asociado que:

1. Se ejecuta dentro de `$(document).ready()`.
2. Bindea eventos a elementos del DOM (clicks, keyup, change, blur).
3. Invoca funciones que realizan llamadas AJAX al backend.
4. Construye HTML dinámicamente mediante concatenación de strings.
5. Muestra resultados directamente en el DOM usando `.html()`.

```
┌──────────────┐     AJAX POST      ┌──────────────────┐
│  Browser JS  │ ──────────────────► │  Página.aspx     │
│  (jQuery)    │                     │  [WebMethod]     │
│              │ ◄────────────────── │                  │
│  Renderiza   │   JSON Response    │  Llama Servicios │
│  HTML        │   {resultado:[]}   │  / BD / Softland │
└──────────────┘                     └──────────────────┘
```

### Flujo de Datos Estándar

Todas las llamadas AJAX siguen exactamente el mismo patrón:

```javascript
$.ajax({
    type: 'POST',
    url: page + '/NombreDelWebMethod',
    data: '{ param1: "valor1", param2: "valor2" }',
    dataType: 'json',
    contentType: 'application/json',
    async: true, // o false en VisualizacionCalculoFiniquito.js
    success: function (response) {
        var data = JSON.parse(response.d);
        if (data.resultado.length > 0) {
            for (var i = 0; i < data.resultado.length; i++) {
                if (data.resultado[i].VALIDACION === "0") {
                    // Caso exitoso
                } else {
                    // Caso error lógico
                }
            }
        }
    },
    error: function (xhr) {
        console.log(xhr.responseText);
    },
    complete: function () {
        // Acciones post-llamada
    }
});
```

**Observación importante**: El campo `VALIDACION` se compara a veces con `"0"` (string) y otras con `0` (número). Esta inconsistencia puede provocar bugs sutiles.

---

## 3. Dependencias y Librerías <a name="dependencias"></a>

| Librería | Versión | Uso |
|---|---|---|
| **jQuery** | No determinada | Core: AJAX, DOM, eventos |
| **Bootstrap** | 3.x (inferido por clases `col-xs-*`) | Grids, modales, tabs |
| **SweetAlert** (`swal`) | 1.x o 2.x | Alertas y confirmaciones |
| **jQuery UI Datepicker** | No determinada | Selector de fechas (en `BasePagosProveedores.js`) |
| **html2canvas** | No determinada | Captura de pantalla para PDF |
| **jsPDF** | No determinada | Generación de PDF en cliente |
| **Plugin custom `$.fn.loader`** | N/A | Definido en `Loader.js` |
| **Plugin externo `pagination()`** | N/A | Paginación de tablas |

### Dependencia externa riesgosa

En `SolicitudBaja.js` e `Inicio.js` se referencia:
```
http://angelsysdoc.com/images/esperar.png
```
Esta es una URL externa no controlada que se usa como imagen de un spinner en SweetAlert. Si el dominio deja de existir o es comprometido, la UI se verá afectada o podría inyectar contenido malicioso.

---

## 4. Patrón de Comunicación con el Backend <a name="patron-comunicacion"></a>

### Convención de nombres de endpoints

Los endpoints siguen una convención clara:

- **`Get*Service`**: Lectura de datos (SELECT).
- **`Set*Service`**: Escritura de datos (INSERT/UPDATE/DELETE).
- **`SetTMP*Service`**: Operaciones sobre tablas temporales de sesión (generación de cheques).

### Respuesta estándar del backend

Todos los endpoints devuelven un JSON con la estructura:
```json
{
  "d": "{\"resultado\": [{\"VALIDACION\": \"0\", \"RESULTADO\": \"mensaje\", ...campos...}]}"
}
```

- `VALIDACION = "0"`: Operación exitosa.
- `VALIDACION != "0"`: Error lógico o dato no encontrado.
- **El JSON viene como string dentro de `response.d`**, por lo que siempre se hace `JSON.parse(response.d)`.

---

## 5. Análisis Detallado por Archivo <a name="analisis-detallado"></a>

### 5.1. VisualizacionCalculoFiniquito.js (1483 líneas)

**Página asociada**: `VisualizacionCalculoFiniquito.aspx`
**Propósito**: Pantalla de detalle y visualización completa del cálculo de un finiquito específico. Es la pantalla con mayor densidad de información del sistema.

#### Inicialización

Al cargar la página, recupera el `idDesvinculacion` desde `sessionStorage` y dispara **14 llamadas AJAX** en secuencia dentro de un `setTimeout` de 500ms:

```javascript
var idDesvinculacion = sessionStorage.getItem("applicationIdDesvinculacionCalculo");
setTimeout(function () {
    ajaxVisualizarDatosTrabajador(...);
    ajaxVisualizarDocumentos(...);
    ajaxVisualizaContratos(...);
    ajaxVisualizarTotalDias(...);
    ajaxVisualizarPeriodosRemuneraciones(...);
    ajaxVisualizarOtrosHaberesFinquito(...);  // Nota: typo "Finquito"
    ajaxVisualizarDescuentosFiniquito(...);
    ajaxVisualizarDiasVacaciones(...);
    ajaxVisualizarTotalesFiniquito(...);
    ajaxFiniquitoConfirmado(...);
    ajaxConceptosAdicionales(...);
    ajaxBonosAdicionales(...);
    ajaxHaberesMensual(...);
    ajaxValorUF(...);
}, 500);
```

**Observación crítica**: Todas estas llamadas usan `async: false`, lo que convierte la carga en **síncrona y bloqueante**. El navegador se congela hasta que todas las llamadas terminan. Esto es una práctica obsoleta y degradada en navegadores modernos.

#### Funciones y Endpoints

| Función JS | Endpoint Backend | Tipo | Descripción |
|---|---|---|---|
| `ajaxVisualizarDatosTrabajador` | `GetObtenerPagosPorFiltroPagosService` | GET | Datos del trabajador: RUT, nombre, empresa, ficha, causal, zona extrema, part time |
| `ajaxVisualizarDocumentos` | `GetVisualizarDocumentosCalculoService` | GET | Lista de documentos asociados al finiquito (nombre+seleccionado) |
| `ajaxVisualizaContratos` | `GetVisualizarContratosCalculoService` | GET | Tabla de contratos: ficha, fechas inicio/fin, días, causal, estado pago |
| `ajaxVisualizarTotalDias` | `GetVisualizarTotalDiasCalculoCalculoService` | GET | Total de días para el cálculo (solo TWEST; TEAMRRHH lo oculta) |
| `ajaxVisualizarPeriodosRemuneraciones` | `GetVisualizarPeriodosCalculoService` | GET | Matriz de remuneraciones de últimos 1-6 meses |
| `ajaxVisualizarOtrosHaberesFinquito` | `GetVisualizarOtrosHaberesFiniquitoCalculoService` | GET | Otros haberes del finiquito (nombre + monto) |
| `ajaxVisualizarDescuentosFiniquito` | `GetVisualizarDescuentosFiniquitoCalculoService` | GET | Descuentos aplicados (nombre + monto) |
| `ajaxVisualizarDiasVacaciones` | `GetVisualizarDiasVacacionesCalculoService` | GET | Días de vacaciones (estructura diferente según empresa) |
| `ajaxVisualizarTotalesFiniquito` | `GetVisualizarTotalesFiniquitoCalculoService` | GET | Tabla de totales (concepto + valor) |
| `ajaxFiniquitoConfirmado` | `GetValidarConfirmadoFiniquitoCalculoService` | GET | Verifica si el finiquito ya fue confirmado para pago |
| `ajaxConceptosAdicionales` | `GetVisualizarConceptosAdicionalesFiniquitoCalculoService` | GET | Conceptos adicionales (solo TEAMRRHH) |
| `ajaxBonosAdicionales` | `GetVisualizarBonosAdicionalesFiniquitoCalculoService` | GET | Bonos adicionales con período (solo TEAMRRHH) |
| `ajaxHaberesMensual` | `GetVisualizarHabaeresMensualFiniquitoCalculoService` | GET | Haberes mensuales (solo TEAMRRHH). Nota: typo "Habaeres" |
| `ajaxValorUF` | `GetVisualizarValorUfFiniquitoCalculoService` | GET | Valor de la UF y tope de UF para el cálculo |
| `ajaxCargo` | `GetCargoService` / `GetCargoOUTService` | GET | Cargo del trabajador (endpoint diferente según empresa) |
| `ajaxCentroCosto` | `GetCentroCostosService` / `GetCentroCostosOUTService` | GET | Centro de costo, área de negocio, cliente |
| `ajaxConfirmarRetiroFiniquito` | `SetConfirmarRetiroCalculoService` | SET | Confirma el finiquito para pago con medio de pago y monto opcional |

#### Lógica de Negocio Diferenciada por Empresa

El sistema maneja **dos empresas con lógica diferente**:

- **TWEST**: Muestra total de días, promedios diarios, y oculta conceptos/bonos adicionales.
- **TEAMRRHH**: Oculta total de días, muestra haberes mensuales, conceptos adicionales y bonos adicionales. Las tablas de vacaciones tienen 3 columnas en lugar de 2.

```javascript
// Ejemplo de bifurcación por empresa
switch(empresa){
    case "TWEST":
        method = "GetCargoService";
        break;
    case "TEAMRRHH":
        method = "GetCargoOUTService";
        break;
    case "TEAMWORK":
        method = "";  // No hace nada
        break;
}
```

#### Cálculo en el Cliente (CRÍTICO)

La función `ajaxVisualizarPromedioCalculo` (línea 699) realiza un **cálculo financiero directamente en JavaScript**:

```javascript
function ajaxVisualizarPromedioCalculo(remuneraciones, dias, empresa) {
    // Solo para TWEST
    var sumaRemuneraciones = 0;
    for (var i = 0; i < remuneraciones.length; i++) {
        sumaRemuneraciones = sumaRemuneraciones + parseInt(remuneraciones[i]);
    }
    // Promedio diario = Suma remuneraciones / Total días
    Math.round(sumaRemuneraciones / parseInt(dias))
}
```

**Riesgo**: Este cálculo puede ser manipulado por el usuario mediante las herramientas de desarrollo del navegador. El promedio diario es un dato clave para calcular indemnizaciones por años de servicio.

#### Confirmación de Pago con Edición de Monto

Los botones `#btnPagoCheque`, `#btnPagoTransf` y `#btnPagoNomina` permiten al usuario **modificar el monto total del finiquito** antes de confirmar. El flujo es:

1. SweetAlert pregunta: "¿Desea cambiar el monto total del finiquito?"
2. Si acepta → Input para nuevo monto → Textarea para observación → Llama `ajaxConfirmarRetiroFiniquito` con el nuevo monto.
3. Si cancela → Llama `ajaxConfirmarRetiroFiniquito` con monto vacío (usa el calculado).

**Observación**: En `#btnPagoTransf` (línea 122), el medio de pago se envía como `"CHEQUE"` en lugar de `"TRANSFERENCIA"`. Esto parece ser un **bug**:
```javascript
// Línea 122 - Posible bug
ajaxConfirmarRetiroFiniquito("VisualizacionCalculoFiniquito.aspx", 
    idDesvinculacion, "CHEQUE", ...); // Debería ser "TRANSFERENCIA"
```

#### Generación de PDF en Cliente

```javascript
function genPDF() {
    $(".table-scroll").addClass("table-scroll2"); // Quita scroll para captura
    html2canvas(document.getElementById("container"), {
        onrendered: function (canvas) {
            var img = canvas.toDataURL('image/jpg', 1.0);
            var doc = new jsPDF({ unit: 'px', format: 'A4' });
            doc.addImage(img, 'png', 10, 16, 432, 590);
            doc.save('Documento-CALCULO.pdf');
        }
    });
}
```

Genera un PDF a partir de una captura de pantalla del contenedor HTML. El tamaño está hardcodeado (`432x590 px`), lo cual puede cortar información si el contenido es más largo.

#### Código Comentado (líneas 998-1017)

Se encontró un bloque extenso de código comentado en `ajaxVisualizarTotalesFiniquito`:

```javascript
//content = content + '<tr>';
//content = content + '<td>A Pagar por Feriado Proporcional</td>';
//content = content + '<td>' + formatNumber.new(data.resultado[i].FERIADOPROPORCIONAL, "$") + '</td>';
//content = content + '</tr>';
//content = content + '<tr>';
//content = content + '<td>Otros Haberes </td>';
//content = content + '<td>' + formatNumber.new(data.resultado[i].OTROSHABERES, "$") + '</td>';
//content = content + '</tr>';
//content = content + '<td>Total Descuentos</td>';
//... (continúa hasta línea 1017)
```

Este código sugiere que originalmente se mostraban filas hardcodeadas para "Feriado Proporcional", "Otros Haberes", "Total Descuentos" y "Total Finiquito". Fue reemplazado por una estructura dinámica basada en `CONCEPTO` y `VALOR`.

#### Funciones Utilitarias Locales

El archivo define sus propias copias de `formatNumber` y `formatStringDate`, que son **idénticas** a las definidas en `Inicio.js`. Esto indica duplicación de código.

---

### 5.2. Inicio.js (1267 líneas)

**Página asociada**: `Inicio.aspx`
**Propósito**: Dashboard principal del sistema. Centraliza la vista de todos los finiquitos y las acciones principales sobre ellos.

#### Inicialización

Al cargar, ejecuta 5 llamadas para popular el dashboard:
```javascript
ajaxValidateInitProcess("Inicio.aspx");      // Estado del proceso de cheques
ajaxKnowChequesInProcess("Inicio.aspx");     // Cheques en cola
ajaxConfirmados("Inicio.aspx", "CHEQUE",...); // Tabla cheques confirmados
ajaxConfirmados("Inicio.aspx", "TRANSFERENCIA",...);
ajaxConfirmados("Inicio.aspx", "NOMINA",...);
ajaxRecientes("Inicio.aspx", "", "");        // Lista principal (con delay 500ms)
```

#### Máquina de Estados de Finiquitos

El dashboard visualiza los finiquitos con **colores según su estado**:

| Estado | Color Borde | Color Botón | Clase CSS |
|---|---|---|---|
| `FINIQUITO NO HA SIDO PAGADO` | Celeste `rgb(91,192,222)` | `btn-info` | — |
| `FINIQUITO CONFIRMADO PARA PAGO` | Naranja `rgb(240,173,78)` | `btn-warning` | — |
| `FINIQUITO DISPONIBLE PARA PAGO` | Azul `#428bca` | `btn-primary` | — |
| `FINIQUITO HA SIDO PAGADO` | Verde `rgb(92,184,92)` | `btn-success` | — |
| `FINIQUITO HA SIDO NOTARIADO` | Verde `rgb(92,184,92)` | `btn-success` | Icono protegido |
| `ESPERANDO IMPRESIÓN DEL CHEQUE.` | Rojo `rgb(217,83,79)` | `btn-danger` | — |

#### Botones de Acción Condicionales

Los botones de acción se muestran según flags del backend:

| Flag del Backend | Botón | Acción |
|---|---|---|
| `OPTION_PAGAR_FOLIO === 'S'` | Pagar Finiquito | `ajaxPagarFiniquito` → `SetFiniquitoPagadoService` |
| `OPTION_CONF_FOLIO === 'S'` | Confirmar Pago | Navega a `VisualizacionCalculoFiniquito.aspx` |
| (siempre visible) | Visualizar Cálculo | Navega a `VisualizacionCalculoFiniquito.aspx` |
| `OPTION_ANULAR_FOLIO === 'S'` | Anular Folio | `ajaxAnularFolio` → `SetAnularFiniquitoService` |
| `OPTION_ANULAR_PAGO === 'S'` | Anular Pago | `ajaxAnularPago` → `SetAnularPagosService` (requiere motivo) |
| `OPTION_NOTARIADO === 'S'` | Notariado | `ajaxNotariadoFiniquito` → `SetNotariadoPagosService` |
| `OPTION_REVERTIRCONF === 'S'` | Revertir Confirmación | `ajaxRevertirConfirmacionPago` → `SetRevertirConfirmacionPagosService` |

#### Endpoints

| Función JS | Endpoint | Descripción |
|---|---|---|
| `ajaxRecientes` | `GetFiniquitosRecientesService` | Lista principal con filtros opcionales |
| `ajaxConfirmados` | `GetFiniquitosConfirmadosService` | Finiquitos confirmados por medio de pago |
| `ajaxObtenerMontoCheque` | `GetObtenerMontoChequePagosService` | Monto para generar cheque |
| `ajaxObtenerMontoPago` | `GetObtenerMontoPagoPagosService` | Monto para pago transferencia/nómina |
| `ajaxCargaChequeFiniquitos` | `SetTMPCargaChequeFiniquitosService` | Carga cheque en tabla temporal |
| `ajaxCargaPagoFiniquitos` | (no definida en código visible) | Carga pago transferencia |
| `ajaxInitProcess` | `SetTMPInitProcessChequeFiniquitosService` | Inicia proceso de generación |
| `ajaxValidateInitProcess` | `GetTMPValidateProcessInitFiniquitosService` | Valida si hay proceso activo |
| `ajaxKnowChequesInProcess` | `GetTMPChequesInProcessFiniquitoService` | Lista cheques en cola |
| `ajaxCloseProcess` | `SetTMPCloseProcessChequeFiniquitoService` | Cierra proceso y genera archivo `.xlsx` |
| `ajaxAnularFolio` | `SetAnularFiniquitoService` | Elimina folio y datos asociados |
| `ajaxAnularPago` | `SetAnularPagosService` | Anula pago (requiere motivo) |
| `ajaxRevertirConfirmacionPago` | `SetRevertirConfirmacionPagosService` | Revierte estado a "calculado" |
| `ajaxNotariadoFiniquito` | `SetNotariadoPagosService` | Marca como notariado |
| `ajaxPagarFiniquito` | `SetFiniquitoPagadoService` | Marca como pagado (cambia estado contratos) |

#### Filtrado en la Tabla Principal

Los filtros disponibles son: `SIN FILTRO`, `NOMBRE`, `FOLIO`, `RUT`, `EMPRESA`, `PARTTIME`, `FULLTIME`. Los filtros `PARTTIME` y `FULLTIME` son especiales: no muestran input de búsqueda sino que filtran directamente por tipo de contrato.

#### Filtrado en Confirmados (Cheques)

Se aplica **filtrado client-side** dentro del callback `success` de `ajaxConfirmados`. Los datos ya vienen del servidor y se filtran en JS comparando campo por campo:
```javascript
case "FOLIO":
    if (filtrarPor == data.resultado[i].ID) { /* muestra fila */ }
case "RUT":
    if (filtrarPor === data.resultado[i].RUTTRABAJADOR) { /* muestra fila */ }
```

**Observación**: Esto significa que **TODOS los datos se envían desde el servidor** y el filtrado se hace en el navegador. Para grandes volúmenes de datos, esto es ineficiente.

#### Datos mostrados por finiquito

Cada tarjeta de finiquito reciente muestra: Folio, Usuario de cálculo, Empresa, Empleado (nombre), RUT, Ficha, Cargo Mod, Causal, Fecha de generación, Monto del finiquito, y botones de acción.

#### Código Comentado

- **Línea 1083**: Botón de cálculo comentado: `//content = content + '<a href="#" class="btn btn-info"...`
- **Línea 1101**: Botón de anotación comentado: `//content = content + '<a href="#" class="btn btn-danger btn-anotacion...`

Ambos sugieren funcionalidades planificadas pero no implementadas o desactivadas.

---

### 5.3. SolicitudBaja.js (809 líneas)

**Página asociada**: `SolicitudBaja.aspx`
**Propósito**: Formulario para crear una nueva Solicitud de Baja (inicio del proceso de finiquito).

#### Variables Globales

```javascript
var dateNotConcur = { fechas: [] };  // Array de fechas de no concurrencia
var comSimple = "'";                  // Comilla simple para construir strings
var path = window.location.href.split('/')[...]; // Nombre de la página actual
```

#### Flujo Principal

1. Usuario ingresa RUT del trabajador.
2. Click en "Buscar Trabajador" → `ajaxValidarPermitidoSolBj4` (verifica que no tenga baja activa) + `ajaxListarContratosFiniquitados` (carga contratos disponibles).
3. Usuario selecciona contrato → `ajaxObtenerContratoActivoBaja` → Carga datos del contrato (tipo, cargo, fechas).
4. Usuario completa: fecha de desvinculación, causal de despido, observaciones.
5. Si causal es "Art.160 N3 (No concurrencia)" → Se habilita sección adicional para fechas de inasistencia y archivos adjuntos.
6. Click "Generar Solicitud" → Validaciones → `ajaxCrearSolicitudB4J` → Upload de archivos.

#### Validación de Fecha de Desvinculación

La función `diffDays` calcula la diferencia en días entre la fecha ingresada y la fecha actual:

```javascript
function diffDays(dateStr) {
    // Parsea fecha DD-MM-AAAA y calcula diferencia con Date.now()
    var diff = Math.floor((date1 - date2) / 86400000); // 86400000 = ms en un día
    return diff;
}
```

Según el resultado muestra alertas visuales:
- **1 día**: "La fecha de desvinculación es posterior a la fecha actual (1 día después)."
- **>1 día**: "...días después."
- **Negativo** (con valor absoluto): "La fecha de desvinculación es anterior a la fecha actual."

#### Causal Especial: Art.160 N3

Cuando se selecciona la causal `"12"` (que corresponde a "Art.160N3 | No concurrencia a sus labores sin causa justificada"):

- Se muestra `#boxAdicInformation` con campos adicionales.
- Se requiere ingresar fechas específicas de inasistencia.
- Se requiere adjuntar archivos como evidencia.
- Las fechas se almacenan en `dateNotConcur.fechas[]` y se serializan con separador `;` para enviar al backend.

#### Carga de Archivos

Los archivos se suben en dos pasos:
1. Se validan extensiones en el cliente (usando `getExtension()` de `Utils.js`).
2. Se crea la solicitud primero (`SetCrearSolicitudB4J`).
3. Se suben archivos en una llamada separada a `UploadFiles.ashx` usando `FormData`.

```javascript
function ajaxUploadFile(data) {
    $.ajax({
        type: 'POST',
        url: '../UploadFiles.ashx',
        data: data,
        contentType: false,
        processData: false,
        success: function (data) { /* ... */ }
    });
}
```

#### Endpoints

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxValidarPermitidoSolBj4` | `GetValidarPermitidoSolBj4` | Verifica si el trabajador ya tiene baja en curso |
| `ajaxListarContratosFiniquitados` | `GetListarContratosFiniquitados` | Lista contratos del RUT |
| `ajaxObtenerContratoActivoBaja` | `GetContratoActivoBaja` | Detalle del contrato seleccionado |
| `ajaxObtenerCausalesDespido` | `GetObtenerCausalesDespido` | Carga combo de causales |
| `ajaxCrearSolicitudB4J` | `SetCrearSolicitudB4J` | Crea la solicitud (endpoint transaccional) |
| `ajaxUploadFile` | `UploadFiles.ashx` | Sube archivos adjuntos |

---

### 5.4. PagoProveedores.js (778 líneas)

**Página asociada**: `PagoProveedores.aspx`
**Propósito**: Generación de cheques para proveedores. Maneja un ciclo de vida de "sesión de trabajo" para la generación masiva.

#### Flujo de Generación de Cheques

```
┌─────────────┐     ┌─────────────┐     ┌──────────────┐     ┌─────────────┐
│ Init Process│ ──► │ Agregar     │ ──► │ Close Process│ ──► │ Archivo     │
│ (Crear TMP) │     │ Cheques     │     │ (Generar)    │     │ .xlsx       │
└─────────────┘     └─────────────┘     └──────────────┘     └─────────────┘
```

#### Función `ajaxConsultarProveedor` (líneas 214-432)

Esta es la función más compleja del archivo. Realiza **3 llamadas AJAX encadenadas** dentro de la misma función:

1. **Primera llamada** → `GetObtenerProveedorService`: Obtiene datos del proveedor (RUT, nombre, banco).
2. **Segunda llamada** → `GetObtenerCorrelativoDisponibleProveedoresService`: Obtiene el siguiente número de cheque disponible para la empresa seleccionada.
3. **Tercera llamada** → `GetTMPCorrelativoChequeDisponibleProveedoresService`: Valida que el correlativo no esté ya en uso en la tabla temporal.

#### Conversión de Monto a Texto

```javascript
function ajaxCifraString(page, monto) {
    // Llama a GetMontoCifra para convertir "100000" → "CIEN MIL PESOS"
    // El resultado se muestra en #montoChequeEnLetras
}
```

Esta conversión se delega al servidor, lo cual es correcto ya que la lógica de conversión de números a texto en español es compleja.

#### Endpoints

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxAgregarProveedor` | `SetInsertarProveedorService` | Alta de proveedor |
| `ajaxVisualizarTiposProveedores` | `GetObtenerTipoProveedorService` | Combo de tipos |
| `ajaxCifraString` | `GetMontoCifra` | Conversión número → texto |
| `ajaxConsultarProveedor` | `GetObtenerProveedorService` | Datos del proveedor |
| `ajaxConsultarProveedor` | `GetObtenerCorrelativoDisponibleProveedoresService` | Correlativo cheque |
| `ajaxConsultarProveedor` | `GetTMPCorrelativoChequeDisponibleProveedoresService` | Validación TMP |
| `ajaxCargaChequeProveedores` | `SetTMPCargaChequeProveedoresService` | Carga cheque en TMP |
| `ajaxValidateInitProcess` | `GetTMPValidateProcessInitProveedoresService` | Valida proceso activo |
| `ajaxInitProcess` | `SetTMPInitProcessChequeProveedoresService` | Inicia sesión TMP |
| `ajaxKnowChequesInProcess` | `GetTMPChequesInProcessProveedoresService` | Lista cheques en cola |
| `ajaxCloseProcess` | `SetTMPCloseProcessChequeProveedoresService` | Genera archivo y cierra |

#### Código Comentado

Línea 103: Se encuentra comentada la llamada a `ajaxPrintChequeProveedor`, una función que aparentemente generaba la impresión directa del cheque:
```javascript
//ajaxPrintChequeProveedor("PagoProveedores.aspx", rutProveedor, correlativo, 
//    nserieCheque, monto, ciudad, dia, mes, year, quien, cifra, siglaBanco, 
//    nominativo, empresa);
```

---

### 5.5. XmlSolB4J.js (638 líneas)

**Página asociada**: `xmlsolb4j.aspx`
**Propósito**: Bandeja de entrada para la gestión (aprobación/rechazo/anulación) de solicitudes de baja.

#### Función Polimórfica Principal

`ajaxObtenerSolicitudesB4J(page, etapa, section, idDesvinculacion)` es una función que sirve para **múltiples propósitos** según los parámetros:

- **Listar solicitudes**: `section = ""`, muestra tabla con todas las solicitudes.
- **Ver detalle**: `section = "DETALLE"`, carga información detallada de una solicitud.
- **Ver archivos**: `section = "ARCHIVOS"`, lista archivos adjuntos.
- **Ver fechas**: `section = "FECHAS"`, muestra fechas de no concurrencia.

#### Flujo de Aprobación

La función `ajaxAdministrarProcesosSolB4J` recibe un `proceso` que puede ser:
- `APROVAR` (nota: con typo, sin "B")
- `RECHAZAR`
- `ANULAR`
- `REVERTIR`

Los botones de acción se muestran/ocultan según flags del backend:
- `BTN_APROV_SOLB4J` → Mostrar botón Aprobar
- `BTN_RECH_SOLB4J` → Mostrar botón Rechazar
- `BTN_ANUL_SOLB4J` → Mostrar botón Anular
- `BTN_RVT_SOLB4J` → Mostrar botón Revertir

#### Descarga de Archivos y PDFs

```javascript
function ajaxDownloadFile(ruta) {
    var form = $('<form method="POST" action="../DownloadFiles.ashx">');
    form.append($('<input type="hidden" name="ruta" value="' + ruta + '">'));
    $('body').append(form);
    form.submit();
    form.remove();
}

function ajaxTemporaryPDF(ruta) {
    // Similar pero con PDFTemporal.ashx
}
```

#### Endpoints

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxObtenerSolicitudesB4J` | `GetObtenerSolicitudesB4J` | Polimórfico según params |
| `ajaxAdministrarProcesosSolB4J` | `SetAdministrarProcesosSolB4J` | Cambio de estado |
| `ajaxDownloadFile` | `DownloadFiles.ashx` | Descarga archivo |
| `ajaxTemporaryPDF` | `PDFTemporal.ashx` | Vista previa PDF temporal |

---

### 5.6. BasePagosFiniquitos.js (646 líneas)

**Página asociada**: `BasePagosFiniquitos.aspx`
**Propósito**: Listado y filtrado de pagos realizados a trabajadores por concepto de finiquito.

#### Filtros Disponibles

| Filtro | Comportamiento |
|---|---|
| `SIN FILTRO` | Carga todos los pagos |
| `TRABAJADOR` | Muestra combo con trabajadores |
| `EMPRESA` | Carga combo dinámico con `ajaxObtenerClientePagos` |
| `FECHAS` | Muestra datepickers desde/hasta |

#### Desglose de Haberes en Tabla

Cada fila de pago muestra un desglose completo:
- **Finiquito**: Monto principal
- **I.A.S.** (Indemnización Años de Servicio)
- **Desahucio** (Mes de Aviso)
- **Indemnización Voluntaria**
- **Vacaciones**

Si alguno de estos valores viene vacío del backend, se muestra "No aplica."

#### Endpoints

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxObtenerClientePagos` | `GetObtenerClientePagosService` | Combo de clientes/empresas |
| `ajaxObtenerTrabajadoresPagos` | `GetObtenerTrabajadoresPagosService` | Combo de trabajadores |
| `ajaxObtenerPagos` | `GetObtenerPagosService` | Lista de pagos con filtros |
| `ajaxPrintingCheque` | `PrintingCheque` (inferido) | Genera archivo para impresión |

---

### 5.7. BasePagosProveedores.js (350 líneas)

**Página asociada**: `BasePagosProveedores.aspx`
**Propósito**: Listado histórico de pagos a proveedores con filtros.

#### Inicialización con Datepicker

```javascript
$(".datepicker").datepicker({ dateFormat: 'dd-mm-yy' });
```

Es el único archivo que utiliza jQuery UI Datepicker directamente.

#### Filtrado Client-Side

Implementa filtrado enteramente en el navegador. La función `ajaxObtenerPagos` recibe un `filterSection` y luego dentro del `success` aplica lógica condicional:

```javascript
if (filterSection === "FILTRO") {
    switch(filterOptionSelected) {
        case "FOLIO":
            if (filterValue == data.resultado[i].FOLIO) { /* muestra */ }
        case "PROVEEDOR":
            if (filterValue == data.resultado[i].PROVEEDOR) { /* muestra */ }
        // ... más casos
    }
}
```

#### Endpoints

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxObtenerPagos` | `GetObtenerPagosPorFiltroPagosService` | Lista pagos con filtros |

---

### 5.8. Utils.js (434 líneas)

**Propósito**: Biblioteca compartida de funciones utilitarias usadas por múltiples scripts.

#### Funciones Definidas

| Función | Líneas | Descripción |
|---|---|---|
| `formatDate(date)` | ~10 | Convierte fecha a formato `DD-MM-AAAA` |
| `formatRuit(rut)` | ~25 | Formatea RUT chileno con puntos y guión (ej: `12.345.678-9`). **Nota**: El nombre `Ruit` es un typo, debería ser `Rut` |
| `getExtension(filename)` | ~5 | Extrae extensión del nombre de archivo |
| `formatDateFull(dateStr)` | ~30 | Convierte fecha ISO a texto legible en español |
| `currencyFormat(num)` | ~15 | Formatea número como moneda chilena (`$1.234.567`) |
| `diffDays(date1, date2)` | ~10 | Calcula diferencia en días entre dos fechas |
| `diffMonths(date1, date2)` | ~10 | Calcula diferencia en meses |
| `validateRut(rut)` | ~30 | Valida RUT chileno con algoritmo módulo 11 |
| `cleanRut(rut)` | ~5 | Limpia RUT de puntos y guión |
| `formatNumber(num)` | ~15 | Formatea número con separador de miles |

#### Validación de RUT

```javascript
function validateRut(rut) {
    // Aplica algoritmo módulo 11 estándar chileno
    // Calcula dígito verificador y lo compara con el proporcionado
    // Retorna true/false
}
```

#### Duplicación Detectada

Las funciones `formatNumber`, `formatStringDate` y `diffDays` están **duplicadas** en al menos `Inicio.js` y `VisualizacionCalculoFiniquito.js`. Esto genera riesgo de inconsistencia si se modifica una copia y no la otra.

---

### 5.9. AdministradorDeProveedores.js (78 líneas)

**Página asociada**: `AdministradorDeProveedores.aspx`
**Propósito**: Listado de proveedores registrados en el sistema.

#### Función Única

```javascript
function ajaxObtenerProveedores(page) {
    $.ajax({
        url: page + '/GetObtenerProveedoresProveedorService',
        // ...
        success: function (response) {
            // Construye tabla HTML con: RUT, Nombre, Tipo, Banco, N° Cuenta
            // Solo muestra proveedores con VALIDACION === "0"
        }
    });
}
```

**Observación**: Este archivo solo tiene funcionalidad de **lectura**. No hay formularios de edición o eliminación de proveedores visibles. La creación se hace desde `PagoProveedores.js`.

#### Endpoint

| Función | Endpoint | Descripción |
|---|---|---|
| `ajaxObtenerProveedores` | `GetObtenerProveedoresProveedorService` | Lista todos los proveedores |

---

### 5.10. PagosFiniquitos.js (60 líneas)

**Página asociada**: Utilizado como módulo auxiliar en el flujo de pagos.
**Propósito**: Valida documentos de pago (números de cheque o comprobante).

#### Funciones

| Función | Endpoint | Descripción |
|---|---|---|
| `validaDocumento` | `GetValidaDocumentoPagos` | Valida que un número de documento exista en el sistema |
| `validaDocumentoSession` | `SetValidaDocumentoPagosSession` | Persiste el resultado de validación en la sesión del servidor |

#### Flujo

1. Se ingresa un número de documento.
2. `validaDocumento` consulta al backend si existe.
3. Si es válido (`VALIDACION === "0"`), muestra el resultado en `#contentValidaDocumento` y llama a `validaDocumentoSession` para guardar en sesión que el documento fue validado.

---

### 5.11. Loader.js (40 líneas)

**Propósito**: Plugin jQuery para mostrar/ocultar un spinner de carga.

```javascript
$.fn.loader = function (options) {
    // action: "init" → Crea HTML del spinner con logo TeamWork Chile
    // action: "stop" → Oculta y limpia el spinner
    return funciones[configurations.action]();
};
```

**No contiene lógica de negocio**. Es puramente un componente de UI reutilizable. Muestra el logo de la empresa (`Images/logo-teamwork-chile.png`) y el texto "Estamos cargando la información..."

---

## 6. Catálogo Completo de Endpoints <a name="catalogo-endpoints"></a>

### 6.1 Endpoints GET (Lectura)

| # | Endpoint | Página .aspx | Descripción |
|---|---|---|---|
| 1 | `GetFiniquitosRecientesService` | Inicio | Lista principal de finiquitos |
| 2 | `GetFiniquitosConfirmadosService` | Inicio | Finiquitos confirmados por medio de pago |
| 3 | `GetObtenerMontoChequePagosService` | Inicio | Monto para cheque |
| 4 | `GetObtenerMontoPagoPagosService` | Inicio | Monto para transferencia/nómina |
| 5 | `GetTMPValidateProcessInitFiniquitosService` | Inicio | Validar proceso de cheques activo |
| 6 | `GetTMPChequesInProcessFiniquitoService` | Inicio | Cheques en cola |
| 7 | `GetObtenerPagosPorFiltroPagosService` | VisualizacionCalculo / BasePagosProveedores | Datos trabajador / pagos con filtro |
| 8 | `GetVisualizarDocumentosCalculoService` | VisualizacionCalculo | Documentos del finiquito |
| 9 | `GetVisualizarContratosCalculoService` | VisualizacionCalculo | Contratos asociados |
| 10 | `GetVisualizarTotalDiasCalculoCalculoService` | VisualizacionCalculo | Total días |
| 11 | `GetVisualizarPeriodosCalculoService` | VisualizacionCalculo | Períodos de remuneración |
| 12 | `GetVisualizarOtrosHaberesFiniquitoCalculoService` | VisualizacionCalculo | Otros haberes |
| 13 | `GetVisualizarDescuentosFiniquitoCalculoService` | VisualizacionCalculo | Descuentos |
| 14 | `GetVisualizarDiasVacacionesCalculoService` | VisualizacionCalculo | Días vacaciones |
| 15 | `GetVisualizarTotalesFiniquitoCalculoService` | VisualizacionCalculo | Totales del finiquito |
| 16 | `GetValidarConfirmadoFiniquitoCalculoService` | VisualizacionCalculo | Estado de confirmación |
| 17 | `GetVisualizarConceptosAdicionalesFiniquitoCalculoService` | VisualizacionCalculo | Conceptos adicionales |
| 18 | `GetVisualizarBonosAdicionalesFiniquitoCalculoService` | VisualizacionCalculo | Bonos adicionales |
| 19 | `GetVisualizarHabaeresMensualFiniquitoCalculoService` | VisualizacionCalculo | Haberes mensuales |
| 20 | `GetVisualizarValorUfFiniquitoCalculoService` | VisualizacionCalculo | Valor UF y tope |
| 21 | `GetCargoService` | VisualizacionCalculo | Cargo trabajador (TWEST) |
| 22 | `GetCargoOUTService` | VisualizacionCalculo | Cargo trabajador (TEAMRRHH) |
| 23 | `GetCentroCostosService` | VisualizacionCalculo | Centro de costo (TWEST) |
| 24 | `GetCentroCostosOUTService` | VisualizacionCalculo | Centro de costo (TEAMRRHH) |
| 25 | `GetValidarPermitidoSolBj4` | SolicitudBaja | Verificar baja existente |
| 26 | `GetListarContratosFiniquitados` | SolicitudBaja | Contratos del trabajador |
| 27 | `GetContratoActivoBaja` | SolicitudBaja | Detalle contrato |
| 28 | `GetObtenerCausalesDespido` | SolicitudBaja | Causales de despido |
| 29 | `GetObtenerSolicitudesB4J` | xmlsolb4j | Solicitudes de baja (polimórfico) |
| 30 | `GetObtenerProveedoresProveedorService` | AdministradorDeProveedores | Lista proveedores |
| 31 | `GetObtenerProveedorService` | PagoProveedores | Datos un proveedor |
| 32 | `GetObtenerTipoProveedorService` | PagoProveedores | Tipos de proveedor |
| 33 | `GetMontoCifra` | PagoProveedores | Número a texto |
| 34 | `GetObtenerCorrelativoDisponibleProveedoresService` | PagoProveedores | Correlativo cheque |
| 35 | `GetTMPCorrelativoChequeDisponibleProveedoresService` | PagoProveedores | Validar correlativo TMP |
| 36 | `GetTMPValidateProcessInitProveedoresService` | PagoProveedores | Proceso activo proveedores |
| 37 | `GetTMPChequesInProcessProveedoresService` | PagoProveedores | Cheques en cola proveedores |
| 38 | `GetObtenerPagosService` | BasePagosFiniquitos | Pagos con filtros |
| 39 | `GetObtenerClientePagosService` | BasePagosFiniquitos | Clientes/empresas |
| 40 | `GetObtenerTrabajadoresPagosService` | BasePagosFiniquitos | Trabajadores |
| 41 | `GetValidaDocumentoPagos` | PagosFiniquitos | Validar documento |

### 6.2 Endpoints SET (Escritura)

| # | Endpoint | Página .aspx | Descripción |
|---|---|---|---|
| 1 | `SetAnularFiniquitoService` | Inicio | Anular folio |
| 2 | `SetAnularPagosService` | Inicio | Anular pago (con motivo) |
| 3 | `SetRevertirConfirmacionPagosService` | Inicio | Revertir confirmación |
| 4 | `SetNotariadoPagosService` | Inicio | Marcar notariado |
| 5 | `SetFiniquitoPagadoService` | Inicio | Marcar pagado |
| 6 | `SetTMPCargaChequeFiniquitosService` | Inicio | Cargar cheque en TMP |
| 7 | `SetTMPInitProcessChequeFiniquitosService` | Inicio | Iniciar proceso cheques |
| 8 | `SetTMPCloseProcessChequeFiniquitoService` | Inicio | Cerrar proceso cheques |
| 9 | `SetConfirmarRetiroCalculoService` | VisualizacionCalculo | Confirmar finiquito |
| 10 | `SetCrearSolicitudB4J` | SolicitudBaja | Crear solicitud de baja |
| 11 | `SetAdministrarProcesosSolB4J` | xmlsolb4j | Administrar solicitudes |
| 12 | `SetInsertarProveedorService` | PagoProveedores | Alta proveedor |
| 13 | `SetTMPCargaChequeProveedoresService` | PagoProveedores | Cargar cheque proveedor en TMP |
| 14 | `SetTMPInitProcessChequeProveedoresService` | PagoProveedores | Iniciar proceso proveedores |
| 15 | `SetTMPCloseProcessChequeProveedoresService` | PagoProveedores | Cerrar proceso proveedores |
| 16 | `SetValidaDocumentoPagosSession` | PagosFiniquitos | Guardar validación en sesión |

### 6.3 Handlers (.ashx)

| # | Handler | Descripción |
|---|---|---|
| 1 | `UploadFiles.ashx` | Subida de archivos (FormData) |
| 2 | `DownloadFiles.ashx` | Descarga de archivos |
| 3 | `PDFTemporal.ashx` | Vista previa PDF temporal |

---

## 7. Cálculos Identificados en el Frontend <a name="calculos"></a>

| Cálculo | Archivo | Función | Riesgo |
|---|---|---|---|
| **Promedio diario de remuneraciones** | `VisualizacionCalculoFiniquito.js` | `ajaxVisualizarPromedioCalculo` | **CRÍTICO** — cálculo financiero manipulable |
| Diferencia de días entre fechas | `SolicitudBaja.js` / `Utils.js` | `diffDays` | Bajo — solo validación visual |
| Diferencia de meses | `Utils.js` | `diffMonths` | Bajo — solo UI |
| Validación de RUT (módulo 11) | `Utils.js` | `validateRut` | Bajo — validación de formato |
| Formateo de moneda | `Utils.js` / `Inicio.js` / `VisualizacionCalculo.js` | `formatNumber` / `currencyFormat` | Bajo — solo presentación |

---

## 8. Código Comentado y No Utilizado <a name="codigo-comentado"></a>

| Archivo | Líneas | Descripción |
|---|---|---|
| `VisualizacionCalculoFiniquito.js` | 998-1017 | Filas hardcodeadas de totales (Feriado Proporcional, Otros Haberes, Total Descuentos, Total Finiquito). Reemplazado por versión dinámica. |
| `Inicio.js` | 1083 | Botón de cálculo genérico (`btn-info`) — nunca activado |
| `Inicio.js` | 1101 | Botón de anotación (`btn-danger btn-anotacion`) — funcionalidad no implementada |
| `PagoProveedores.js` | 103 | Llamada a `ajaxPrintChequeProveedor` — función de impresión directa desactivada |

---

## 9. Riesgos de Seguridad <a name="riesgos"></a>

### 9.1. Cálculo Financiero en el Cliente (Severidad: ALTA)

**Archivo**: `VisualizacionCalculoFiniquito.js`, función `ajaxVisualizarPromedioCalculo`

El promedio diario de remuneraciones se calcula en JavaScript. Un usuario técnico podría:
1. Modificar los valores del array `remuneraciones` en la consola.
2. Alterar el resultado antes de la confirmación del finiquito.
3. Afectar el cálculo de indemnizaciones basadas en este promedio.

**Recomendación**: Mover este cálculo al backend y solo enviar el resultado ya calculado.

### 9.2. Modificación de Monto del Finiquito (Severidad: MEDIA)

**Archivo**: `VisualizacionCalculoFiniquito.js`, botones de confirmación

El sistema permite al usuario cambiar el monto total del finiquito desde el frontend antes de confirmar. Si bien probablemente se valida en el backend, la posibilidad de enviar montos arbitrarios desde el cliente es un riesgo.

### 9.3. Construcción de JSON por Concatenación de Strings (Severidad: MEDIA)

En todas las llamadas AJAX, los parámetros se construyen como:
```javascript
data: '{ idDesvinculacion: "' + idDesvinculacion + '" }'
```

Si `idDesvinculacion` contiene caracteres especiales como `"`, `\`, o `}`, la cadena JSON se rompe. Esto puede ser explotado para inyección JSON si el usuario manipula valores. Debería usarse `JSON.stringify()`.

### 9.4. URL Externa No Controlada (Severidad: BAJA)

La imagen `http://angelsysdoc.com/images/esperar.png` puede dejar de estar disponible o ser comprometida. Debería reemplazarse con una imagen local.

### 9.5. Errores Solo en Console.log (Severidad: BAJA)

Los errores AJAX solo se muestran en `console.log(xhr.responseText)`, sin feedback al usuario. Una falla silenciosa puede causar confusión cuando una acción no se ejecuta sin explicación visible.

---

## 10. Observaciones Generales <a name="observaciones"></a>

### 10.1. Typos en Nombres de Funciones/Endpoints

| Typo | Corrección | Ubicación |
|---|---|---|
| `formatRuit` | `formatRut` | `Utils.js` |
| `ajaxVisualizarOtrosHaberesFinquito` | `...Finiquito` | `VisualizacionCalculoFiniquito.js` |
| `GetVisualizarHabaeresMensualFiniquito...` | `Haberes` | `VisualizacionCalculoFiniquito.js` |
| `APROVAR` | `APROBAR` | `XmlSolB4J.js` |
| `GetVisualizarTotalDiasCalculoCalculoService` | `Calculo` repetido | `VisualizacionCalculoFiniquito.js` |
| `feberero` | `febrero` | `formatStringDate` (Inicio.js y VisualizacionCalculo.js) |

### 10.2. Duplicación de Código

Las funciones `formatNumber`, `formatStringDate` y `diffDays` están copiadas en al menos 3 archivos. Se recomienda centralizar en `Utils.js` y eliminar las copias locales.

### 10.3. Uso de `async: false` (Deprecado)

En `VisualizacionCalculoFiniquito.js`, las 14 llamadas AJAX usan `async: false`. Esto está deprecado en jQuery 1.8+ y bloquea el hilo principal del navegador, causando una experiencia de usuario degradada. Se recomienda migrar a Promises o callbacks encadenados.

### 10.4. Generación de HTML por Concatenación

Todos los archivos construyen HTML mediante concatenación de strings (`content = content + '<tr>'...`). Esto es:
- Propenso a errores (tags sin cerrar, atributos mal escapados).
- Difícil de mantener y depurar.
- Vulnerable a XSS si los datos del backend contienen HTML/JavaScript.

Se recomienda considerar templates (Handlebars, Mustache) o al menos `document.createElement` para contenido dinámico.

### 10.5. Manejo de Permisos

El sistema implementa un buen patrón de **permisos controlados por el servidor**: Los flags como `OPTION_PAGAR_FOLIO`, `BTN_APROV_SOLB4J`, etc., vienen del backend y determinan qué botones se muestran. Esto es correcto ya que la decisión de permisos no se toma en el cliente.

### 10.6. SessionStorage para Navegación

La comunicación entre `Inicio.js` y `VisualizacionCalculoFiniquito.js` se hace a través de `sessionStorage`:
```javascript
// En Inicio.js
sessionStorage.setItem("applicationIdDesvinculacionCalculo", idDesvinculacion);
window.location = 'VisualizacionCalculoFiniquito.aspx';

// En VisualizacionCalculoFiniquito.js
var idDesvinculacion = sessionStorage.getItem("applicationIdDesvinculacionCalculo");
```

Esto funciona pero no permite URLs compartibles (bookmarkable). Una alternativa sería usar query parameters.

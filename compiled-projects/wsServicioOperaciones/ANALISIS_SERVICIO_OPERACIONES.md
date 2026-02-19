# Analisis del proyecto `wsServicioOperaciones`

Este documento resume la logica del servicio WCF decompilado desde `ServicioOperaciones.dll`. El proyecto contiene solo clases de acceso a datos y un servicio que enruta llamadas a procedimientos almacenados en SQL Server. **No hay logica de negocio en C#**, mas alla del mapeo de parametros y la invocacion de stored procedures.

## Resumen ejecutivo
- Tipo: Servicio WCF (contracto `IServicioOperaciones`) que retorna un `DataSet` con una tabla resultante.
- Dependencia principal: SQL Server. Todas las operaciones son `ExecuteStoreProcedure`.
- Configuracion de conexion: archivo `config.xml` ubicado en `AppDomain.CurrentDomain.BaseDirectory + "bin"`.
- No se observan llamadas HTTP, colas, archivos externos (aparte de `config.xml`) ni librerias de terceros.

## Estructura general
- `wsServicioOperaciones` implementa `IServicioOperaciones`.
- Cada metodo del servicio:
  1. Recibe `string[] Parametros` y `string[] valores`.
  2. Convierte ambos arrays en `List<Parametro>` (nombre/valor). No valida longitudes ni nulls.
  3. Llama a la clase de dominio correspondiente (`COperaciones`, `CFinanzas`, `CBajas`, `CFiniquitos`, `CTeamwork`).
  4. Envuelve la `DataTable` resultante en un `DataSet` dentro de `ServicioOperaciones.ServicioOperaciones`.

## Contrato de servicio WCF
Archivo: `IServicioOperaciones.cs`

El contrato define ~70 operaciones. Todas siguen el mismo patron: reciben arrays de parametros y retornan `ServicioOperaciones` (un `DataSet`). Esto sugiere que el cliente es generico y pasa pares nombre/valor que el servidor reinyecta a SQL Server.

### Posibles riesgos / comportamiento implicito
- **Sin validacion de entrada**: si `Parametros.Length != valores.Length`, el mapeo quedara desfasado y puede provocar errores SQL o datos incorrectos.
- **Sin tipado fuerte**: todos los valores son `string` y se pasan como `AddWithValue`, lo que puede forzar conversiones de tipo en SQL Server.
- **Timeout**: `CommandTimeout = 3600` (1 hora) para todos los stored procedures.

## Acceso a datos
Archivos: `SQLServerDBHelper.cs`, `DatosXML.cs`, `Parametro.cs`

### SQLServerDBHelper
- Construye string de conexion solo para el cliente `TW_SA`.
- Conexion:
  - `Server = {ServidorBaseDeDatosSA}`
  - `Database = {BaseDeDatos}`
  - `User Id = {UsuarioBDSA}`
  - `Password = {ClaveBDSA}`
  - `Connection Timeout = 0` (sin limite de tiempo de conexion)
- Ejecuta `Stored Procedures` con `SqlDataAdapter` y devuelve `DataTable`.
- Tiene `ExecuteQuery` para SQL de texto, **pero no se usa en este proyecto**.

### DatosXML
Obtiene datos desde `config.xml`:
- Ruta esperada: `AppDomain.CurrentDomain.BaseDirectory + "bin\\config.xml"`.
- Estructura esperada en XML:
  - `tw/sqlserver/nombre` con atributo `usuario`
  - `tw/sqlserver/nombre/passwordsa` (valor de texto)
  - `tw/sqlserver/nombre/servidorsa` (valor de texto)
  - `tw/sqlserver/nombre/basedatos` (valor de texto)

No se encuentra `config.xml` en este repositorio. Sin ese archivo, el servicio no puede resolver credenciales ni servidor de BD.

## Dominios funcionales y stored procedures
A continuacion, los modulos del servicio y los procedimientos almacenados que ejecutan. **La logica real vive en SQL Server**.

### 1) Operaciones (clase `COperaciones`)
Enfocado a procesos de solicitud, contratos, renovaciones, anexos, carga masiva y reportes.

**Creacion / actualizacion**
- `SP_KM_SET_PROCESOMASIVO`
- `SP_KM_SET_VALIDACIONPROCESOMASIVO`
- `SP_KM_SET_CREAOACTUALIZAFICHAPERSONAL`
- `SP_KM_SET_CREACONTRATODETRABAJO`
- `SP_KM_SET_CREARENOVACION`
- `SP_KM_SET_CREARANEXO`

**Obtencion / consulta**
- `SP_KM_GET_RENDERLOADERCREACIONSOLICITUD`
- `SP_KM_GET_OBTENERSOLICITUDRENOVACIONES`
- `SP_KM_GET_OBTENERSOLICITUDCONTRATO`
- `SP_KM_GET_RENDERLOADERENVIOSOLICITUD`
- `SP_KM_GET_PLANTILLASCORREOS`
- `SP_KM_GET_OBTENERPROCESOSSOLICITUDES`
- `SP_KM_GET_OBTENERSOLICITUDES`
- `SP_KM_GET_OBTENCIONHOJASCARGAMASIVA`
- `SP_KM_GET_OBTENCIONCUENTASSINASIGNAR`
- `SP_KM_GET_OBTENERDATOSSOLICITUD`
- `SP_KM_GET_OBTENERKAM`
- `SP_KM_GET_OBTENERCLIENTE`
- `SP_KM_GET_OBTENERJEFEKAM`
- `SP_KM_GET_OBTENERCLIENTESKAM`
- `SP_KM_GET_OBTENERMOTIVOSANULACION`
- `SP_KM_GET_PAGINATORPROCESOS`
- `SP_KM_GET_PAGINATORSOLICITUDES`
- `SP_KM_GET_OBTENERKAMJEFE`
- `SP_KM_GET_OBTENERINFORMACIONCLIENTE`
- `SP_KM_GET_OBTENERSOLICITUDCONTRATOANULADA`
- `SP_KM_GET_OBTENERSOLICITUDRENOVACIONESANULADA`
- `SP_KM_GET_OBTENERHEADERPROCESOS`
- `SP_KM_GET_OBTENERHEADERSOLICITUDES`
- `SP_KM_GET_OBTENERRENDERIZADODOCANEXOS`
- `SP_KM_GET_RENDERIZACARGAMASIVA`
- `SP_KM_GET_INDICEESTADISTICOSOLICITUDES`
- `SP_KM_GET_OBTENERCLIENTESNOMBRES`
- `SP_KM_GET_OBTENERDATOSPROCESO`
- `SP_KM_GET_REPORTE`

**Anulacion / cierre**
- `SP_KM_SET_ANULARPROCESO`
- `SP_KM_SET_TERMINARPROCESO`
- `SP_KM_SET_ANULARSOLICITUD`
- `SP_KM_SET_REVERTIRTERMINOSOLICITUD`

**Interpretacion funcional (por nombres)**
- “KM” sugiere modulo de **KAM / Key Account Management**.
- “solicitud” parece ser el flujo principal (creacion, envio, anulacion, etc.).
- “renderloader” y “renderizado” indican que SQL entrega fragmentos o datos para renderizar documentos.

### 2) Bajas (clase `CBajas`)
Gestion de solicitudes de baja y confirmaciones.

- `SP_BJ_GET_OBTENERPOSIBLESBAJAS`
- `SP_BJ_GET_OBTENERCC`
- `SP_KM_GET_OBTENERSOLICITUDBAJAS`
- `SP_BJ_GET_OBTENERKAMPOSIBLESBAJAS`
- `SP_BJ_GET_OBTENERPOSIBLESBAJASPORKAM`
- `SP_BJ_SET_CREARBAJA`
- `SP_BJ_SET_CAMBIARESTADOSOLICITUD`
- `SP_BJ_GET_OBTENERDATOSSOLICITUD`
- `SP_BJ_GET_OBTENERBAJASCONFIRMADAS`

**Interpretacion funcional (por nombres)**
- “bajas” probablemente representa bajas de personal o contratos.
- “solicitud de baja” tiene estados y confirmaciones.
- “CC” podria ser centro de costo.

### 3) Finanzas (clase `CFinanzas`)
Relacionada con gastos, documentos, proveedores, periodos y bancos.

- `SP_FZ_GET_OBTENERCONCEPTOSGASTOS`
- `SP_FZ_GET_OBTENERESTADOGASTO`
- `SP_FZ_GET_OBTENERTIPOSDOCUMENTOS`
- `SP_KM_GET_OBTENERBANCOS`
- `SP_FZ_GET_OBTENERTIPOREEMBOLSO`
- `SP_FZ_GET_OBTENERGASTOS`
- `SP_KM_GET_OBTENERTIPOSCUENTAS`
- `SP_KM_GET_OBTENERCLIENTES`
- `SP_FZ_SET_CREARGASTO`
- `SP_FZ_SET_CREARGASTOFINANZAS`
- `SP_FZ_GET_OBTENERPROVEEDORES`
- `SP_KM_GET_OBTENERCLIENTESAUTOCOMPLETE`
- `SP_FZ_GET_OBTENERPERIODOVIGENTE`
- `SP_KM_GET_OBTENERPROVEEDORESAUTOCOMPLETE`
- `SP_FZ_GET_OBTENERPROVEEDORESRUT`
- `SP_KM_GET_OBTENERCLIENTENOMBREBYNOMBRE`
- `SP_FZ_SET_CERRARPERIODO`
- `SP_FZ_GET_OBTENERPERIODO`
- `SP_KM_GET_OBTENERCLIENTESDISTINTOS`
- `SP_KM_GET_OBTENERCLIENTESDISTINTOSNOMBRE`
- `SP_FZ_GET_OBTENERNDOCUMENTO`
- `SP_FZ_GET_EXISTEDOCUMENTO`
- `SP_FZ_CRUD_MANTENCIONPROVEEDORES`
- `SP_FZ_CRUD_MANTENEDOR_PERIODO`
- `SP_FZ_CRUD_MANTENEDOR_GASTOS`

**Interpretacion funcional (por nombres)**
- Hay mantencion (CRUD) para proveedores, periodos y gastos.
- Existe control de documentos (numero, existencia).
- Manejo de reembolsos, tipos de documento y bancos.

### 4) Finiquitos (clase `CFiniquitos`)
- `SP_FN_GET_OBTENERFINIQUITOS`
- `SP_FN_CRUD_MANTENCIONFINIQUITOS`

**Interpretacion funcional (por nombres)**
- Mantencion y consulta de finiquitos (liquidaciones / terminos laborales).

### 5) Teamwork (clase `CTeamwork`)
- `SP_TW_GET_PDF`
- `SP_TW_GET_PRIORIDADESESTADOS`
- `SP_TW_GET_HISTORIAL`
- `SP_TW_GET_PAGINATIONS`

**Interpretacion funcional (por nombres)**
- Posible integracion con modulo “Teamwork” para historial y paginacion.
- `SP_TW_GET_PDF` sugiere generacion/obtencion de PDF desde BD.

## Logica de negocio (real)
En este codigo **no existe logica de negocio** mas alla de:
- Mapeo de parametros nombre/valor a `SqlParameter`.
- Seleccion de stored procedure segun el metodo llamado.

Toda la logica (validaciones, flujos, reglas) esta contenida en los stored procedures de SQL Server. Para documentarla en profundidad se requiere acceso a la BD o al repositorio donde viven esos procedimientos.

## Servicios externos
No hay referencias a HTTP, SOAP externos, colas o APIs. La unica dependencia externa identificada es SQL Server.

## Consultas / procedimientos almacenados
Este servicio **solo ejecuta stored procedures**, no hay SQL inline. La lista completa esta arriba. No se tiene el contenido de los procedimientos, por lo tanto:
- No es posible ver consultas SQL reales.
- No se conocen tablas concretas.
- No se pueden inferir reglas de negocio mas alla de los nombres.

## Recomendaciones para profundizar
Para completar la documentacion interna se requiere:
1. Acceso a la BD `TW_SA` y export de los procedimientos listados.
2. Acceso al `config.xml` real para conocer el servidor/BD.
3. Verificacion de los clientes que consumen este WCF para conocer el significado de cada parametro.

## Inventario de archivos relevantes
- `wsServicioOperaciones.cs`: implementacion WCF.
- `IServicioOperaciones.cs`: contrato con todas las operaciones.
- `COperaciones.cs`, `CBajas.cs`, `CFinanzas.cs`, `CFiniquitos.cs`, `CTeamwork.cs`: wrappers de stored procedures.
- `SQLServerDBHelper.cs`: helper de BD.
- `DatosXML.cs`: lectura de credenciales/configuracion XML.
- `Parametro.cs`: modelo simple de parametro.
- `ServicioOperaciones.cs`: envoltorio `DataSet` para respuesta.

## Limitaciones de este analisis
- No existe `config.xml` en el proyecto.
- No se dispone del contenido de los stored procedures.
- El servicio decompilado no contiene validaciones ni reglas; la logica real esta en SQL.

Si quieres que documente cada stored procedure en detalle, necesito el script de BD o acceso a los procedimientos.

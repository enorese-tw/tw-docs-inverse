# Analisis tecnico decompilado - wsServicioAuth

## 1) Alcance del analisis
Este documento se construye a partir del decompilado del ensamblado `ServicioAuth.dll` y del codigo recuperado en este proyecto.

Archivos analizados:
- `CAuth.cs`
- `DatosXML.cs`
- `IServicioAuth.cs`
- `Parametro.cs`
- `ServicioAuth.cs`
- `SQLServerDBHelper.cs`
- `wsServicioAuth.cs`

No existen en este repositorio:
- Archivo de configuracion real `config.xml`.
- Scripts SQL de procedimientos almacenados.
- Configuracion de hosting WCF (`web.config`/`app.config`) ni bindings/endpoints.

## 2) Resumen ejecutivo
El sistema es un servicio WCF (`IServicioAuth`) que expone 15 operaciones. Todas siguen el mismo patron:
1. Reciben dos arreglos de strings (`Parametros` y `valores`).
2. Convierten ambos arreglos a una lista de pares nombre/valor (`List<Parametro>`).
3. Llaman a una capa de acceso a datos (`CAuth`), que solo enruta a un procedimiento almacenado especifico en SQL Server.
4. Empaquetan el resultado (`DataTable`) dentro de un `DataSet` retornado en un DataContract (`ServicioAuth`).

La logica de negocio visible en C# es minima: casi toda la logica real vive en SQL Server (SPs).

## 3) Arquitectura reconstruida

### 3.1 Capas y responsabilidades
- `IServicioAuth.cs`: contrato WCF de operaciones publicas.
- `wsServicioAuth.cs`: implementacion del contrato, adaptador entrada/salida.
- `CAuth.cs`: fachada de acceso a datos por caso de uso (cada metodo llama un SP).
- `SQLServerDBHelper.cs`: helper ADO.NET para ejecutar SP o SQL texto.
- `DatosXML.cs`: proveedor de configuracion para conexion SQL (lee `config.xml`).
- `ServicioAuth.cs`: DTO de respuesta serializable (`DataSet`).
- `Parametro.cs`: DTO interno para parametros SQL (`ParameterName`, `ParameterValue`).

### 3.2 Flujo de ejecucion end-to-end
1. Cliente invoca operacion WCF definida en `IServicioAuth.cs`.
2. `wsServicioAuth` recibe `string[] Parametros` + `string[] valores`.
3. `ArrayToListParametros` crea `List<Parametro>` indexado 1:1 por posicion.
4. `wsServicioAuth` llama al metodo equivalente en `CAuth`.
5. `CAuth` instancia `SQLServerDBHelper("TW_SA")` y llama `ExecuteStoreProcedure(...)`.
6. `SQLServerDBHelper` construye cadena de conexion desde `DatosXML`.
7. `ExecuteStoreProcedure` usa `SqlDataAdapter` + `CommandType.StoredProcedure`, agrega parametros con `AddWithValue`, ejecuta `Fill(DataTable)`.
8. `wsServicioAuth` agrega el `DataTable` al `DataSet` de respuesta y lo retorna.

## 4) Contrato del servicio (operaciones expuestas)
Fuente: `IServicioAuth.cs:12-59`.

Operaciones WCF:
1. `GetSignIn`
2. `GetBASE64`
3. `GetPemisionSectionsAccess`
4. `GetPemisionAccess`
5. `GetSectionRenderHtml`
6. `GetPlantillaCargaMasivaRender`
7. `GetDownloadPlantillaCargaMasiva`
8. `GetObtenerEmpresas`
9. `GetObtenerSitios`
10. `GetBarcodes`
11. `SetEnrolarColaborador`
12. `SetControlErroresSistemas`
13. `SetEnviaCorreoTeamworkInforma`
14. `SetBatchChangeContrato`
15. `CrudMantenedorNoticiasTeamwork`

Todas exponen exactamente la misma firma:
- Entrada: `string[] Parametros, string[] valores`
- Salida: `ServicioAuth.ServicioAuth` (DataContract con `DataSet Table`)

## 5) Mapeo operacion -> procedimiento almacenado
Fuente principal: `CAuth.cs:15-133`.

| Operacion WCF (`wsServicioAuth`) | Metodo `CAuth` | Procedimiento almacenado |
|---|---|---|
| `GetSignIn` | `GetSignIn` | `SP_GET_TW_SIGNIN` |
| `GetBASE64` | `GetBASE64` | `SP_GET_TW_BASE64` |
| `GetPemisionSectionsAccess` | `GetPemisionSectionsAccess` | `SP_GET_TW_PERMISIONSECTIONSACCESS` |
| `GetPemisionAccess` | `GetPemisionAccess` | `SP_TW_GET_PERMISSIONACCESS` |
| `GetSectionRenderHtml` | `GetSectionRenderHtml` | `SP_GET_TW_SECTIONSRENDERHTML` |
| `GetPlantillaCargaMasivaRender` | `GetPlantillaCargaMasivaRender` | `SP_GET_TW_PLANTILLACARGAMASIVARENDER` |
| `GetDownloadPlantillaCargaMasiva` | `GetDownloadPlantillaCargaMasiva` | `SP_GET_TW_DOWNLOADPLANTILLACARGAMASIVA` |
| `GetObtenerEmpresas` | `GetObtenerEmpresas` | `SP_TW_GET_OBTENEREMPRESA` |
| `GetBarcodes` | `GetBarcodes` | `SP_TW_GET_BARCODES` |
| `GetObtenerSitios` | `GetObtenerSitios` | `SP_TW_CRUD_SITIOSWEB` |
| `SetEnrolarColaborador` | `SetEnrolarColaborador` | `SP_SET_ENROLAR_COLABORADOR` |
| `SetControlErroresSistemas` | `SetControlErroresSistemas` | `SP_SET_TW_CONTROLERRORESSISTEMAS` |
| `SetEnviaCorreoTeamworkInforma` | `SetEnviaCorreoTeamworkInforma` | `SP_SET_TW_ENVIACORREOTEAMWORKINFORMA` |
| `SetBatchChangeContrato` | `SetBatchChangeContrato` | `SP_BAT_TW_CHANGECONTRATO` |
| `CrudMantenedorNoticiasTeamwork` | `CrudMantenedorNoticiasTeamwork` | `SP_CRUD_TW_MANTENEDORNOTICIASTEAMWORK` |

## 6) Conectividad a base de datos

### 6.1 Tipo de base y stack
- Motor: SQL Server.
- Libreria: `System.Data.SqlClient`.
- Mecanismo: `SqlDataAdapter` + `DataTable`.
- Conexion: credenciales SQL explicitas (no integrada).

Referencia: `SQLServerDBHelper.cs:18-24`, `SQLServerDBHelper.cs:33-44`.

### 6.2 Cadena de conexion construida
Patron exacto:
`Server={Servidor};Database={BaseDeDatos};User Id={Usuario};Password={Clave};Connection Timeout=0`

Implicancias:
- `Connection Timeout=0`: espera indefinida para abrir conexion.
- Uso de usuario/clave en texto plano provenientes de XML.

Referencia: `SQLServerDBHelper.cs:23`.

### 6.3 Origen de configuracion
`DatosXML` carga archivo:
- Ruta: `AppDomain.CurrentDomain.BaseDirectory + "bin" + "\\config.xml"`
- Resultado tipico esperado: `<base>/bin/config.xml`

Referencia: `DatosXML.cs:21`, `DatosXML.cs:39-44`.

### 6.4 Nodos XML esperados
Consultas XPath usadas:
- `tw/sqlserver/nombre` -> atributo `usuario`
- `tw/sqlserver/nombre/passwordsa` -> valor nodo
- `tw/sqlserver/nombre/servidorsa` -> valor nodo
- `tw/sqlserver/nombre/basedatos` -> valor nodo

Referencias: `DatosXML.cs:60-89`.

Ejemplo de estructura compatible (inferida):
```xml
<tw>
  <sqlserver>
    <nombre usuario="usuario_sql">
      <passwordsa>clave_sql</passwordsa>
      <servidorsa>HOST\\INSTANCIA</servidorsa>
      <basedatos>NombreBD</basedatos>
    </nombre>
  </sqlserver>
</tw>
```

## 7) Logica de negocio identificable

### 7.1 Que hace el codigo C#
- Enrutamiento de llamadas por operacion.
- Adaptacion de parametros de arreglos a lista de pares.
- Ejecucion de SP en SQL Server.
- Empaquetado de resultado en `DataSet`.

### 7.2 Donde vive la logica real
La logica de negocio material (autenticacion, permisos, render HTML, carga masiva, cambios de contrato, noticias, etc.) no esta en C#; se delega a SPs SQL.

Por nombres de SP, se infieren dominios funcionales:
- Autenticacion: `SP_GET_TW_SIGNIN`.
- Permisos y acceso: `SP_GET_TW_PERMISIONSECTIONSACCESS`, `SP_TW_GET_PERMISSIONACCESS`.
- Render dinamico UI/plantillas: `SP_GET_TW_SECTIONSRENDERHTML`, `SP_GET_TW_PLANTILLACARGAMASIVARENDER`.
- Descarga de plantilla: `SP_GET_TW_DOWNLOADPLANTILLACARGAMASIVA`.
- Catalogos/maestros: empresas/sitios/barcodes.
- Procesos de escritura: enrolamiento, control de errores, envio correo, batch cambio contrato, CRUD noticias.

## 8) Servicios externos
En el codigo C# analizado no hay llamadas HTTP/SOAP/REST directas ni SDK de terceros.

Unica dependencia externa explicita:
- SQL Server.

Nota: El nombre `SetEnviaCorreoTeamworkInforma` sugiere envio de correo, pero desde C# solo se ejecuta SP; cualquier integracion de correo estaria dentro de SQL (Database Mail, colas, u otro backend invocado por SP).

## 9) Entradas, salidas y contrato de datos

### 9.1 Entrada
- Dos arreglos paralelos de strings (`Parametros`, `valores`).
- Se asume que ambos tienen misma longitud.

Referencia: `wsServicioAuth.cs:15-20`.

### 9.2 Salida
- DataContract `ServicioAuth` con propiedad `[DataMember] DataSet Table`.
- Cada operacion retorna un `DataSet` con una sola tabla cargada desde el SP.

Referencias: `ServicioAuth.cs:13-25`, `wsServicioAuth.cs:26-34` (patron repetido en todas las operaciones).

## 10) Riesgos tecnicos y de seguridad

1. Falta de validacion de longitudes de arreglos.
- `ArrayToListParametros` itera por `Parametros.Length` y accede `valores[index]` sin validacion.
- Riesgo: `IndexOutOfRangeException` si arrays desalineados.
- Referencia: `wsServicioAuth.cs:18-19`.

2. Sin manejo de excepciones en operaciones de servicio.
- Cualquier fallo de XML/SQL propaga excepcion sin traduccion controlada de fault WCF.
- Referencias: `wsServicioAuth.cs` y `CAuth.cs` (sin `try/catch`).

3. Credenciales en texto plano en XML.
- Usuario y password leidos directamente desde `config.xml`.
- Referencias: `DatosXML.cs:68-73`, `SQLServerDBHelper.cs:23`.

4. Timeout de conexion indefinido.
- `Connection Timeout=0` puede provocar esperas bloqueantes ante problemas de red/SQL.
- Referencia: `SQLServerDBHelper.cs:23`.

5. `AddWithValue` para todos los parametros.
- Puede inducir conversiones implicitas y degradacion de planes por tipado inferido.
- Referencia: `SQLServerDBHelper.cs:40`.

6. Potencial SQL injection en metodo `ExecuteQuery`.
- Existe interpolacion por `string.Format(query, parameters)` para SQL texto.
- Aunque no se usa en este decompilado, es un riesgo latente si se invoca en el futuro.
- Referencia: `SQLServerDBHelper.cs:48-56`.

7. Relectura de XML en cada extraccion de campo.
- `DatosXML` carga el XML multiples veces (uno por cada getter interno).
- Impacta rendimiento y disponibilidad bajo carga.
- Referencias: `DatosXML.cs:63`, `DatosXML.cs:71`, `DatosXML.cs:79`, `DatosXML.cs:87`.

8. Dependencia fuerte de ruta de despliegue.
- Espera `config.xml` en `<BaseDirectory>bin\\config.xml`.
- Si la estructura del hosting cambia, falla el acceso a BD.
- Referencias: `DatosXML.cs:21`, `DatosXML.cs:43`.

## 11) Hallazgos funcionales por dominio (inferencia por nomenclatura)

- Inicio de sesion y autenticacion: `GetSignIn`.
- Obtencion de contenido codificado: `GetBASE64`.
- Autorizacion por secciones y permisos: `GetPemisionSectionsAccess`, `GetPemisionAccess`.
- Render dinamico de secciones HTML y plantillas de carga masiva.
- Descarga de plantillas para procesos bulk.
- Catalogos de empresas/sitios/barcodes.
- Operaciones de mantenimiento y procesos batch:
  - Enrolamiento colaborador.
  - Registro/control de errores de sistemas.
  - Disparo de envio de correo Teamwork Informa.
  - Cambio masivo de contrato.
  - CRUD de noticias Teamwork.

Importante: El comportamiento exacto, reglas, validaciones y efectos secundarios de cada dominio dependen del contenido de cada SP.

## 12) Evidencia clave por archivo

- `CAuth.cs`: mapa de metodos hacia SPs (`CAuth.cs:15-133`).
- `wsServicioAuth.cs`: implementacion operativa WCF y mapeo de entrada/salida (`wsServicioAuth.cs:13-240`).
- `IServicioAuth.cs`: superficie publica del servicio (`IServicioAuth.cs:12-59`).
- `SQLServerDBHelper.cs`: modelo de ejecucion SQL y cadena de conexion (`SQLServerDBHelper.cs:18-61`).
- `DatosXML.cs`: ruta y parseo de configuracion de BD (`DatosXML.cs:21-89`).
- `ServicioAuth.cs`: DataContract de respuesta (`ServicioAuth.cs:13-25`).
- `Parametro.cs`: encapsulacion de parametros SQL (`Parametro.cs:10-31`).

## 13) Lo que falta para documentacion completa de negocio
Para cerrar una trazabilidad funcional al 100% (reglas, condiciones, tablas afectadas, seguridad, side effects), se necesita:
1. Script de definicion de los 15 procedimientos almacenados.
2. Esquema de BD (`tables/views/functions`) relacionados.
3. `config.xml` real del ambiente (al menos estructura, sin secretos).
4. Configuracion WCF (bindings, seguridad transporte/mensaje, endpoints).
5. Logs de ejecucion o trazas SQL para ver parametros reales por operacion.

## 14) Recomendaciones de continuidad (prioridad)
1. Extraer y versionar scripts de SP desde SQL Server (`OBJECT_DEFINITION` / SSMS generate scripts).
2. Construir un documento por SP con: entradas, validaciones, tablas leidas/escritas, transacciones, codigos de retorno.
3. Reemplazar credenciales en XML por secretos gestionados (DPAPI, vault o similar) y timeout acotado.
4. Validar y normalizar entrada en servicio (`Parametros`/`valores`) antes de ejecutar SQL.
5. Incorporar manejo de errores WCF con FaultContracts para observabilidad y control.

## 15) Conclusiones
- `wsServicioAuth` es una capa de exposicion WCF delgada, con logica C# casi nula.
- La inteligencia de negocio reside en SQL Server via procedimientos almacenados.
- Desde seguridad/operacion, los principales puntos de riesgo son manejo de secretos, ausencia de validaciones y timeouts indefinidos.
- Este repositorio permite reconstruir la arquitectura de integracion y el inventario funcional, pero no reemplaza la auditoria de SPs para entender completamente la logica del negocio.

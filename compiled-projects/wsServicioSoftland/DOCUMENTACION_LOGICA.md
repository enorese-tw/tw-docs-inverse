# Analisis Tecnico Profundo - wsServicioSoftland (decompilado)

## 1. Alcance y fuentes analizadas
Este analisis se hizo sobre el codigo decompilado disponible en este repositorio:

- `IServicioSoftland.cs`
- `wsServicioSoftland.cs`
- `ServicioSoftland.cs`
- `SQLServerDBHelper.cs`
- `DatosXML.cs`
- `Parametro.cs`
- `CSoftland.cs`
- `CValleNevado.cs`
- `Clmvm.cs` (clase `CImvm`)

No hay proyecto `.csproj`, `web.config`, `app.config` ni `config.xml` en el repo; por lo tanto, este documento se enfoca en logica reconstruida desde el decompilado.

## 2. Arquitectura reconstruida
Arquitectura en capas, simple y directa:

1. Capa de contrato WCF
- `IServicioSoftland`: contrato con todos los `OperationContract`.
- `wsServicioSoftland`: implementacion del servicio.

2. Capa de aplicacion/negocio
- `CSoftland`: consultas generales de personas/contratos/cargos/centro de costo.
- `CValleNevado`: vacaciones, variables por persona, validaciones de fechas, jerarquias.
- `CImvm` (en `Clmvm.cs`): variante Marina/IMVM para registrar/aprobar vacaciones.

3. Capa de acceso a datos
- `SQLServerDBHelper`: construye cadena de conexion y ejecuta SQL.
- Metodo para SP existe (`ExecuteStoreProcedure`), pero no se usa desde las clases de negocio.

4. Configuracion
- `DatosXML`: lee servidor, usuario, password y nombres de base desde `config.xml`.

## 3. Flujo operativo end-to-end
Flujo comun para casi todos los endpoints:

1. Cliente invoca operacion WCF con `string[] Parametros`.
2. `wsServicioSoftland` enruta a `CSoftland`, `CValleNevado` o `CImvm`.
3. Clase de negocio crea `SQLServerDBHelper`.
- En `CSoftland` y `CValleNevado`: usa `parametros[0]` como selector de tenant/base.
- En `CImvm`: cliente fijo `SOFTLAND_IMVM`.
4. Se ejecuta SQL de texto via `ExecuteQuery(...)` con `string.Format`.
5. Resultado vuelve como `DataTable`.
6. Servicio encapsula en `DataSet` y responde en `ServicioSoftland.Table`.

No hay capa de validacion, no hay transacciones explicitas, no hay control de errores en negocio.

## 4. Contrato WCF y enrutamiento real
Todas las operaciones declaradas en `IServicioSoftland` estan implementadas en `wsServicioSoftland`.

### 4.1 Operaciones enrutadas a `CSoftland`
- `GetValidarTrabajador`
- `GetValidarPermitidoSolBj4`
- `GetRutTrabajador`
- `GetListarContratosFiniquitados`
- `GetContratoActivoBaja`
- `GetObtenerAreaNegocio`
- `GetObtenerCargo`
- `GetCentroCosto`
- `GetDatosBancariosTrabajador`
- `GetClientes`
- `GetBatchValidacionFichaCargada`

### 4.2 Operaciones enrutadas a `CValleNevado`
- `GetVNObtenerUsuario`
- `GetValidafechasqls`
- `GetVNAdicionalEnrolamiento`
- `GetVNEsJefatura`
- `GetVNDiasUsados`
- `GetVNDiasUsadosfinal`
- `GetVNFicha`
- `GetVNContratoActivo`
- `GetVNDiasPostTemp`
- `GetVNDiasLegalesUsados`
- `GetVNDiasProgresivosUsados`
- `GetTraerTodasVacaciones`
- `GetVNDiasAntiguedadUsados`
- `GetVNTieneLegalesUsados`
- `GetVNTieneProgresivosUsados`
- `GetVNTieneAntiguedadUsados`
- `GetVNDiasPendientes`
- `GetVNDiasAdministrativos`
- `GetVNDiasAdicionales`
- `GetEncargadoRRHH`
- `GetExisteLasolicitud`
- `GetVNtodosDatos`
- `GetVNTienePendientes`
- `GetVNTienePostTemp`
- `GetVNTieneAdministrativos`
- `GetFichaconrut`
- `SetRegistrarSolicitud`
- `SetAprobarSolicitud`
- `SetUpdateLegales`
- `SetUpdateProgresivos`
- `SetUpdateAntiguedad`
- `SetUpdateTemporada`
- `SetUpdatePendientes`
- `SetUpdateAdministrativos`

### 4.3 Operaciones enrutadas a `CImvm`
- `SetRegistrarSolicitudMarina`
- `SetAprobarSolicitudMarina`
- `SetRegistrarSolicitudMarinaAprobada`

## 5. Conexion a base de datos y multi-tenant
`SQLServerDBHelper` arma la cadena segun identificador `CLIENT`:

- `SOFTLAND_VNEVADO`
- `SOFTLAND_OUT`
- `SOFTLAND_TWC`
- `SOFTLAND_VNEVADOINMOB`
- `SOFTLAND_VNEVADOSOCINMOB`
- `SOFTLAND_IMVM`
- `SOFTLAND_VNEVADOESCUELA`
- `SOFTLAND_VNEVADOSERVINMOB`
- `SOFTLAND_VNEVADOLICANCABU`
- `SOFTLAND_EST`

Formato de cadena:
`Data Source={servidor};Initial Catalog={base};Persist Security Info=True;User ID={usuario};Password={clave}`

Datos vienen de `DatosXML` con XPath sobre `tw/softland/nombre/*`.

## 6. Configuracion esperada (`config.xml`)
Ruta construida por `DatosXML`:

- `AppDomain.CurrentDomain.BaseDirectory + "bin" + "\\config.xml"`

Nodos/atributos esperados:

- `tw/softland/nombre` atributo `usuario`
- `tw/softland/nombre/password`
- `tw/softland/nombre/servidor`
- `tw/softland/nombre/basededatosout`
- `tw/softland/nombre/basededatosest`
- `tw/softland/nombre/basededatosconsultora`
- `tw/softland/nombre/basededatosIMVM`
- `tw/softland/nombre/basededatosvnevado`
- `tw/softland/nombre/basededatosvnevadoescuela`
- `tw/softland/nombre/basededatosvnevadoinmob`
- `tw/softland/nombre/basededatosvnevadoservinmob`
- `tw/softland/nombre/basededatosvnevadosocinmob`
- `tw/softland/nombre/basededatosvnevadolincancabu`

Nota: el nodo esperado para Licancabur tiene texto `lincancabu` (posible typo historico).

## 7. Convencion de parametros por modulo
### 7.1 `CSoftland` y `CValleNevado`
- `parametros[0]`: cliente/tenant (selector de base).
- Placeholders SQL usan `{1}`, `{2}`, etc.
- Se asume que el cliente que consume conoce la posicion exacta para cada endpoint.

### 7.2 `CImvm`
- Tenant fijo: `SOFTLAND_IMVM`.
- Placeholders empiezan en `{0}` (sin indice de cliente).

## 8. Inventario de logica de negocio y consultas

## 8.1 Modulo `CSoftland`
1. `GetValidarTrabajador`
- SQL: `SELECT CASE WHEN COUNT(*) > 0 THEN 'S' ELSE 'N' END Existe FROM softland.sw_personal WHERE rut LIKE '{1}'`
- Objetivo: validar existencia de RUT.

2. `GetValidarPermitidoSolBj4`
- SQL sobre `sw_personal` + `sw_estudiosup`.
- Regla: si existe registro NO BAJA para rut+ficha retorna `ESBAJA='N'`, si no `ESBAJA='S'`.

3. `GetRutTrabajador`
- Obtiene `rut` y `nombres` por rut (join `sw_estudiosup`).

4. `GetListarContratosFiniquitados`
- Lista `ficha,rut` por RUT, excluye estudios con descripcion `%BAJA%`, ordena por `fechaIngreso DESC`.

5. `GetContratoActivoBaja`
- Devuelve ficha/rut/nombres/codEstudios y fechas formateadas `dd-MM-yyyy`.
- Si `parametros[2]` vacio, busca por rut; si no, por rut+ficha.

6. `GetObtenerAreaNegocio`
- Tablas: `sw_areanegper`, `cwtaren`, `sw_personal`.
- Si hay ficha en `parametros[2]`, filtra por rut+ficha.

7. `GetObtenerCargo`
- Tablas: `sw_codineper`, `sw_codine`, `sw_cargoper`, `cwtcarg`.
- Trae `TOP(1)` por ficha.

8. `GetCentroCosto`
- Tablas: `sw_personal`, `sw_areanegper`, `cwtaren`, `sw_ccostoper`, `cwtccos`.

9. `GetDatosBancariosTrabajador`
- Tablas: `sw_personal`, `sw_banco_suc`, `sw_tipodeposito`.
- Retorna descripcion banco, tipo deposito y numero de cuenta.

10. `GetClientes`
- Query directa: `select DesArn from softland.cwtaren`.

11. `GetBatchValidacionFichaCargada`
- Devuelve `ExisteFicha` = `S/N` segun existencia de ficha.

## 8.2 Modulo `CValleNevado`
Enfocado en vacaciones y atributos de personal.

1. Identidad y datos base
- `GetVNObtenerUsuario`: existencia por rut (`EXISTE=S/N`).
- `GetVNAdicionalEnrolamiento`: `nombres,email` por rut.
- `GetVNtodosDatos`: ficha, nombre, fechaIngreso, anoOtraEm, FecCertVacPro para personas con `fechaFiniquito='31-12-9999'`.
- `GetFichaconrut`: fichas por rut.

2. Jerarquia y contrato
- `GetVNEsJefatura`: cuenta personas que tienen `JefeDirecto` = parametro.
- `GetVNFicha`: fichas activas (`FecTermContrato IS NULL` o `>= GETDATE()`).
- `GetVNContratoActivo`: perfil completo por ficha, incluye:
  - `jefeDirecto`, `AnoOtraEm`, `Email`, `FecCertVacPro`.
  - subconsulta `P355` para mapear sucursal:
    - `valor='1'` -> `CORDILLERA`
    - otro valor -> `VITACURA`

3. Vacaciones y dias usados
- `GetVNDiasUsados`: suma `NDiasAp` en `sw_vacsolic` por ficha.
- `GetVNDiasUsadosfinal`: suma para ficha `TOP(1)` de persona por rut con `codEstudios='IND'`.
- `GetVNDiasAdicionales`: lee `ndias` en `sw_vacadic`.
- `GetTraerTodasVacaciones`: intento de consulta de vacaciones (query decompilada invalida; ver riesgos).

4. Variables por persona (`softland.sw_variablepersona`)
- `GetVNDiasPostTemp`: codigo `P356` en mes actual.
- `GetVNDiasLegalesUsados`: `P360`.
- `GetVNDiasProgresivosUsados`: `P361`.
- `GetVNDiasAntiguedadUsados`: `P362`.
- `GetVNDiasPendientes`: `P357`.
- `GetVNDiasAdministrativos`: `P358`.

5. Validadores de existencia de variable (count)
- `GetVNTienePostTemp` -> `P356`
- `GetVNTieneLegalesUsados` -> `P360`
- `GetVNTieneProgresivosUsados` -> `P361`
- `GetVNTieneAntiguedadUsados` -> `P362`
- `GetVNTienePendientes` -> `P357`
- `GetVNTieneAdministrativos` -> `P358`

6. Flujo de solicitudes de vacaciones (`sw_vacsolic`)
- `GetExisteLasolicitud`: cuenta coincidencias exactas de ficha + FsDesde + FsHasta.
- `GetValidafechasqls`: valida superposiciones de rango [desde,hasta]:
  - inicio dentro de rango existente
  - fin dentro de rango existente
  - inicio existente dentro del nuevo rango
  - fin existente dentro del nuevo rango
  - retorna `Disponible='S'` solo si no hay cruces.
- `SetRegistrarSolicitud`: inserta solicitud con estado, dias, observacion, proceso.
- `SetAprobarSolicitud`: actualiza estado y dias aprobados.

7. Actualizacion de variables (aparente intencion de negocio)
- `SetUpdateLegales` (`P360`)
- `SetUpdateProgresivos` (`P361`)
- `SetUpdateAntiguedad` (`P362`)
- `SetUpdateTemporada` (`P356`)
- `SetUpdatePendientes` (`P357`)
- `SetUpdateAdministrativos` (`P358`)

## 8.3 Modulo `CImvm` (Marina)
Todas las operaciones usan conexion fija `SOFTLAND_IMVM`.

1. `SetRegistrarSolicitudMarina`
- Inserta solicitud en `sw_vacsolic`.

2. `SetAprobarSolicitudMarina`
- Actualiza estado y dias aprobados.

3. `SetRegistrarSolicitudMarinaAprobada`
- Inserta solicitud ya aprobada incluyendo `NDiasAp`, `FaDesde`, `FaHasta`.

## 9. Modelo de datos observado (tablas usadas)
Tablas y vistas consultadas/modificadas:

- `softland.sw_personal`
- `softland.sw_estudiosup`
- `softland.sw_areanegper`
- `softland.cwtaren`
- `softland.sw_codineper`
- `softland.sw_codine`
- `softland.sw_cargoper`
- `softland.cwtcarg`
- `softland.sw_ccostoper`
- `softland.cwtccos`
- `softland.sw_banco_suc`
- `softland.sw_tipodeposito`
- `softland.sw_vacsolic`
- `softland.sw_variablepersona`
- `softland.sw_vsnpRetornaFechaMesExistentes`
- `softland.sw_vacadic`
- `softland.sw_encargremune`

Codigos de variables funcionales detectados:

- `P355`: sucursal (1=CORDILLERA, otro=VITACURA)
- `P356`: dias post temporada
- `P357`: dias pendientes
- `P358`: dias administrativos
- `P360`: dias legales usados
- `P361`: dias progresivos usados
- `P362`: dias antiguedad usados

## 10. Procedimientos almacenados
`SQLServerDBHelper` tiene `ExecuteStoreProcedure(...)`, pero en este codigo decompilado no hay ningun metodo que lo invoque.

Conclusiones:
- La logica operativa visible usa SQL de texto plano.
- Si existe uso de SP en otros assemblies, no esta en este alcance.

## 11. Servicios externos e integraciones
Integraciones externas identificadas:

1. SQL Server (unico backend externo visible).
2. Servicio WCF consumido por sistemas clientes (no presentes en este repo).

No se observaron llamadas HTTP/REST/SOAP salientes, colas, archivos planos de intercambio ni SDK de terceros.

## 12. Riesgos, defectos y hallazgos criticos
1. Riesgo alto de SQL injection
- Se usa `string.Format` para inyectar valores en SQL (`LIKE '{1}'`, etc).
- No hay consultas parametrizadas reales (`SqlParameter`) en `ExecuteQuery`.

2. Posible bug severo en `SetUpdate*`
- Ejemplo real: `set valor = '{1}' where ficha like '{1}'`.
- `valor` y `ficha` usan mismo placeholder; probable error de indices.
- Efecto esperado: escribir ficha en campo valor, o no actualizar correctamente.

3. Query aparentemente invalida en `GetTraerTodasVacaciones`
- SQL decompilado: `select f from softland.sw_vacsolic WHERE ficha like '{1}')`.
- Problemas: columna `f` no conocida + parentesis final extra.

4. Query dudosa en `GetVNDiasAdicionales`
- `where Ficha like {1}` sin comillas.
- Puede fallar para fichas string o inducir conversiones implicitas.

5. Posible bug de formato fecha en `GetContratoActivoBaja`
- En parte de `FecTermContrato`, el mes se extrae de `fechaIngreso` en vez de `FecTermContrato`.

6. Posible bug de ruta de config
- `BaseDirectory + "bin"` puede generar duplicacion de carpeta `bin` segun hosting real.

7. Posible mismatch de tag XML para Licancabur
- Codigo busca `basededatosvnevadolincancabu` (con `lin`), podria no coincidir con configuraciones escritas con `lic`.

8. Ausencia de manejo de errores y trazabilidad
- No hay `try/catch`, logging, ni retorno estandarizado de error.

9. Ejecucion de `INSERT/UPDATE` via `SqlDataAdapter.Fill`
- Patron no convencional para DML; no usa `ExecuteNonQuery`.
- Puede depender de comportamiento del proveedor sin feedback claro de filas afectadas.

10. Contrato tipado debil
- Todo entra por `string[]`, sin validacion de largo, tipo ni formato.
- Alto acoplamiento al orden de parametros en cliente.

## 13. Logica de negocio inferida
Procesos principales inferidos:

1. Validacion de identidad laboral
- Existence check por RUT/ficha y determinacion de estado BAJA/no BAJA.

2. Consulta de datos maestros de RRHH
- Cargo, area de negocio, centro de costo, datos bancarios.

3. Gestion de vacaciones
- Solicitar vacaciones (`SetRegistrarSolicitud`).
- Validar solapamiento de fechas (`GetValidafechasqls`).
- Aprobar con dias aprobados (`SetAprobarSolicitud`).
- Consultar acumulados y cupos por categorias (legales/progresivos/antiguedad, etc).

4. Variante IMVM (Marina)
- Flujo paralelo de registro/aprobacion en base fija `SOFTLAND_IMVM`.

## 14. Gaps de informacion por falta de artefactos
No disponible en este repo:

- `config.xml` real (servidor, usuarios, passwords, nombres de base reales).
- consumidores WCF (clientes que envian los arreglos de parametros).
- esquema SQL/DDLs y constraints.
- jobs/procedimientos de soporte fuera de este assembly.

Por eso, valores concretos de conexion, endpoints publicados (URL), binding WCF y autenticacion de transporte no se pueden confirmar desde este material.

## 15. Recomendaciones tecnicas prioritarias
1. Migrar `ExecuteQuery` a SQL parametrizado (`SqlCommand` + `SqlParameter`).
2. Corregir inmediatamente indices en `SetUpdate*`.
3. Corregir queries invalidas (`GetTraerTodasVacaciones`, `GetVNDiasAdicionales`).
4. Cambiar DML a `ExecuteNonQuery` y retornar filas afectadas.
5. Reemplazar `string[]` por contratos tipados por operacion.
6. Agregar validaciones de entrada, manejo de errores y logging.
7. Proteger secretos fuera de XML plano (vault/DPAPI/variables seguras).
8. Crear pruebas de regresion para flujo completo de vacaciones.

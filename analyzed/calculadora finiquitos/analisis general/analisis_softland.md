# Análisis de Conexión a Softland

## Descripción General
La aplicación interactúa con Softland principalmente para obtener datos de trabajadores, contratos y realizar validaciones. Se ha identificado un patrón mixto de acceso a datos:
1.  **Conexión Directa a Base de Datos SQL Server**: Se construyen cadenas de conexión dinámicamente en el código.
2.  **Servicio WCF (ServicioFiniquitos)**: Se utiliza un cliente SOAP/WCF para ciertas operaciones encapsuladas.

## Hallazgos Críticos

### 1. Construcción de Cadenas de Conexión (Hardcoded Credentials)
Se detectaron credenciales de base de datos embebidas directamente en el código fuente (C#). Esto representa un riesgo de seguridad significativo.

**Patrón detectado:**
```csharp
string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", TextBox1.Text)
```
- **Servidor**: `conectorsoftland.team-work.cl\SQL2017`
- **Usuario**: `Sa`
- **Contraseña**: `Softland070`
- **Base de datos**: Dinámica, obtenida de un input de usuario (`TextBox1.Text`).

**Archivos afectados:**
- `CalculoBajaConsultora.aspx.cs`
- `CalculoBajaEst.aspx.cs`
- `CalculoBajaOut.aspx.cs`

### 2. Clase `MethodServiceSoftland`
Esta clase actúa como un wrapper para el cliente del servicio `ServicioFiniquitos`.
- **Ubicación**: `FiniquitosV2/Clases/MethodServiceSoftland.cs`
- **Funcionalidad**: Expone métodos para obtener DataSets con información de solicitudes de baja, jornadas part-time, trabajadores, créditos CCAF, etc.
- **Métodos Clave**:
    - `GetRutTrabajadorSolicitudBaja`
    - `GetJornadasParttimeEST`
    - `GetRetencionJudicialOUT`

### 3. Consultas Directas
Además del servicio, se ejecutan consultas SQL directas (`SELECT`) construidas mediante concatenación de strings o `string.Format`.

**Ejemplo en `CalculoBajaConsultora.aspx.cs`:**
```csharp
strSql = string.Format("SELECT softland.sw_variablepersona.mes, ... FROM softland.sw_variablepersona ... WHERE ...", ...);
```
Se accede a tablas específicas de Softland como:
- `softland.sw_variablepersona`
- `softland.sw_vsnpRetornaFechaMesExistentes`
- `softland.sw_constvalor`

## Recomendaciones Preliminares
1.  **Seguridad**: Eliminar credenciales hardcoded. Mover la cadena de conexión a `Web.config` y usar autenticación integrada o credenciales encriptadas.
2.  **Abstracción**: Centralizar todo el acceso a datos. Evitar construir SQL en el Code-Behind de las páginas ASPX.
3.  **Inyección de SQL**: Validar y sanitizar los inputs (`TextBox1.Text`) usados para construir la cadena de conexión y las consultas SQL para prevenir inyección SQL.

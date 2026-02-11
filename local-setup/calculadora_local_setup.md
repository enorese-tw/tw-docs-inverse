# Configuración y Ejecución Local del Proyecto

Este documento detalla los hallazgos sobre la configuración del proyecto `Calculadora Finiquitos` y los pasos necesarios para ejecutarlo en un entorno local.

## 1. Resumen de Arquitectura
- **Tecnología**: ASP.NET Web Forms (.NET Framework 4.5 / 4.8).
- **Servidor Web**: IIS / IIS Express.
- **Base de Datos**: SQL Server.
- **Servicios Externos**: WCF (Windows Communication Foundation).

## 2. Dependencias de Base de Datos
El proyecto se conecta a múltiples bases de datos y servidores SQL. Existen conexiones definidas en `Web.config` y otras **hardcodeadas** en el código C#.

### Servidores Identificados
1.  **192.168.0.10** (Producción/QA/Legacy)
    - Contiene bases de datos: `Finiquitos2018Prod`, `Finiquitos2018QA`, `TW_OPERACIONES`.
    - Credenciales usadas: `User ID=ADMINTW;Password=Satw.261119`.
2.  **conectorsoftland.team-work.cl\SQL2017** (Softland Connector)
    - Contiene bases de datos: `WICONTROL`, `TWEST`, `TEAMRRHH`, `TEAMWORK` y bases de datos dinámicas por empresa.
    - Credenciales usadas: `User ID=Sa;Password=Softland070`.

### Cadenas de Conexión Hardcodeadas
**ATENCIÓN**: Las siguientes cadenas de conexión están escritas directamente en el código ("hardcoded") y deben ser modificadas o redirigidas para un entorno local aislado.

- **`FiniquitosV2\Clases\Usuarios.cs`**:
    - `Data Source=192.168.0.10;Initial Catalog=Finiquitos2018QA;User ID=ADMINTW;Password=Satw.261119`
- **Archivos `.aspx.cs` (BajaEst, BajaOut, BajaConsultora, RecepcionDocumentos, etc.) y `Clases\Contrato.cs`**:
    - Construyen dinámicamente la conexión:
      ```csharp
      string.Format("Data Source=conectorsoftland.team-work.cl\\SQL2017;Initial Catalog={0};Persist Security Info=True;User ID=Sa;Password=Softland070", empresa)
      ```

## 3. Servicios Externos (WCF)
El proyecto consume servicios WCF alojados en `192.168.0.10`.
Defined in `Web.config` and Service References:
- **ServicioCorreo**: `http://192.168.0.10/wsServicioCorreo/wsServicioCorreo.svc`
- **ServicioSoftland**: `http://192.168.0.10/wsServicioSoftland/wsServicioSoftland.svc`
- **ServicioAuth**: `http://192.168.0.10/wsServicioAuth/wsServicioAuth.svc`
- **ServicioFiniquitos**: `http://192.168.0.10/wsServicioFiniquitos/wsServicioFiniquitos.svc`

## 4. Guía para Ejecución Local

Para ejecutar este proyecto localmente, tienes dos estrategias principales:

### Estrategia A: Conexión via VPN (Recomendada si es posible)
Si tienes acceso VPN a la red donde residen `192.168.0.10` y `conectorsoftland.team-work.cl`:
1.  Asegúrate de tener conectividad con ambos servidores.
2.  El proyecto debería funcionar "tal cual", ya que las IPs y credenciales son válidas en esa red.

### Estrategia B: Entorno Local Aislado (Sin VPN)
Debes simular la infraestructura.

#### 1. Base de Datos Local
Debes tener una instancia de SQL Server local.
- **Hosts File**: Edita `C:\Windows\System32\drivers\etc\hosts` para redirigir los dominios:
  ```
  127.0.0.1 conectorsoftland.team-work.cl
  127.0.0.1 192.168.0.10  <-- (Ojo: Redirigir una IP es posible en algunos contextos o requiere un adaptador de bucle invertido)
  ```
  *Mejor opción*: Buscar y Reemplazar `192.168.0.10` y `conectorsoftland.team-work.cl` por `localhost` o `.\SQLEXPRESS` en todo el proyecto.

- **Configuración SQL Server**:
  - Habilitar autenticación mixta (SQL Server and Windows Authentication mode).
  - Crear usuario `Sa` con password `Softland070` (o cambiar la contraseña en el código/config a la que uses localmente).
  - Crear usuario `ADMINTW` con password `Satw.261119` (o cambiarla en código/config).

- **Esquema de Base de Datos**:
  - Necesitas crear las bases de datos: `Finiquitos2018Prod` (o `QA`), `WICONTROL`, `TWEST`, `TEAMRRHH`, `TEAMWORK`, `TW_OPERACIONES`.

#### 2. Mocking de Servicios WCF
Los servicios en `http://192.168.0.10/wsServicio...` no estarán disponibles.
- Opción 1: Crear proyectos WCF simulados locales que implementen las mismas interfaces (`IServicioCorreo`, etc.) y alojarlos en IIS local.
- Opción 2: Modificar los `EndPoint` en `Web.config` para apuntar a servicios de prueba o mocks si existen.

### 5. Archivos Clave a Modificar
Si decides cambiar las IPs/Credenciales, modifica estos archivos:

1.  **`FiniquitosV2\Web.config`**:
    - Sección `<connectionStrings>`
    - Sección `<system.serviceModel> / <client>` (EndPoints de servicios)
    - Sección `<applicationSettings>`

2.  **`FiniquitosV2\Properties\Settings.settings`**:
    - Actualizar valores por defecto.

3.  **Código Fuente (Hardcoded)**:
    - `FiniquitosV2\Clases\Usuarios.cs` (Línea ~15)
    - `FiniquitosV2\Clases\Contrato.cs` (Múltiples líneas con `string.Format`)
    - `FiniquitosV2\CalculoBajaEst.aspx.cs`
    - `FiniquitosV2\CalculoBajaOut.aspx.cs`
    - `FiniquitosV2\CalculoBajaConsultora.aspx.cs`
    - `FiniquitosV2\RecepcionDocumentos.aspx.cs`
    - `FiniquitosV2\solicitudBajaEST.aspx.cs`
    - `FiniquitosV2\solicitudBajaCONSULTORA.aspx.cs`

## 6. Otras Configuraciones
- **Redirecciones Hardcoded**:
  - Se detectaron redirecciones a `http://192.168.0.10/AplicacionOperaciones/` en los archivos `CalculoBaja*.aspx.cs`. Esto debe ser manejado si se prueba el flujo de redirección.
- **ApiByf**:
  - Configurado como `http://localhost:4001/` en `Settings`. Asegúrate de que este servicio esté corriendo localmente si es necesario.

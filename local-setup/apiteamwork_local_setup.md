# Requisitos para Ejecución Local

Este documento detalla todas las conexiones a bases de datos, servicios externos y configuraciones necesarias para ejecutar el proyecto `ApiTeamwork` en un entorno local.

## 1. Bases de Datos (SQL Server)

El proyecto se conecta a dos servidores de base de datos SQL Server distintos. Las credenciales y nombres de base de datos están encriptados en Base64 en el `Web.config` de la capa Api.

### Servidor Principal (Teamwork)
*   **Host Original**: `192.168.0.10` (Clave: `HostDatabase`)
*   **Usuario**: `ADMINTW` (Clave: `UsernameDatabase`)
*   **Contraseña**: `Satw.261119` (Clave: `PasswordDatabase`)
*   **Bases de Datos Requeridas**:
    *   `TW_GENERAL_TEAMWORK` (Clave: `Database`)
    *   `TW_OPERACIONES` (Clave: `DatabaseOperaciones`)
    *   `TW_SELECCION` (Clave: `DatabaseSeleccion`)
    *   `Coins` (Clave: `DBCoins`)

### Servidor Softland
*   **Host Original**: `SSTW\SQL2014` (Clave: `HostDatabaseSoftland`)
*   **Usuario**: `Sa` (Clave: `UsernameDatabaseSoftland`)
*   **Contraseña**: `Softland070` (Clave: `PasswordDatabaseSoftand`)
*   **Bases de Datos Requeridas**:
    *   `TWEST` (Clave: `DatabaseTWEST`)
    *   `TEAMRRHH` (Clave: `DatabaseTWRRHH`)
    *   `TEAMWORK` (Clave: `DatabaseTWC`)

> **Nota**: Para ejecución local, asegúrese de tener estas bases de datos restauradas en su servidor SQL local y actualice los valores en `Web.config` (recuerde que deben estar en Base64) o modifique el código para usar cadenas de conexión estándar.

## 2. Servicios Externos

### Active Directory (LDAP)
Se detectó una conexión a Active Directory en `SignController.cs`.
*   **Dominio**: `team-work.cl`
*   **URL**: `LDAP://team-work.cl`
*   **Autenticación**: `TEAM-WORK\{username}`
*   **Estado**: **Hardcoded**. La URL y el dominio están escritos directamente en el código.
    *   *Archivo*: `Teamwork.Api/Controllers/SignController.cs`

### Envío de Correos (SMTP)
Se detectó configuración de envío de correos en `MailController.cs`.
*   **Host**: `mail.teamworkchile.cl`
*   **Puerto**: `25`
*   **Usuario**: `no-reply@team-work.cl`
*   **Contraseña**: `SoporTW,2019.`
*   **SSL**: Activado (`EnableSsl = true`)
*   **Estado**: **Hardcoded**. Las credenciales y el servidor están escritos directamente en el código.
    *   *Archivo*: `Teamwork.Api/Controllers/MailController.cs`

## 3. Autenticación (JWT)

El proyecto utiliza JWT para la seguridad de la API. Las claves se encuentran en `Web.config`.

*   **Secret Key**: `clave-secreta-api` (Clave: `JWT_SECRET_KEY`)
*   **Audience**: `http://localhost:31440` (Clave: `JWT_AUDIENCE_TOKEN`)
*   **Issuer**: `http://localhost:31440` (Clave: `JWT_ISSUER_TOKEN`)
*   **Expire Minutes**: `120`

## 4. Resumen de Claves en Web.config (Valores Decodificados)

| Key | Valor Original (Base64) | Valor Decodificado |
| :--- | :--- | :--- |
| `HostDatabase` | `MTkyLjE2OC4wLjEw` | `192.168.0.10` |
| `UsernameDatabase` | `QURNSU5UVw==` | `ADMINTW` |
| `PasswordDatabase` | `U2F0dy4yNjExMTk=` | `Satw.261119` |
| `Database` | `VFdfR0VORVJBTF9URUFNV09SSw==` | `TW_GENERAL_TEAMWORK` |
| `DatabaseOperaciones` | `VFdfT1BFUkFDSU9ORVM=` | `TW_OPERACIONES` |
| `DatabaseSeleccion` | `VFdfU0VMRUNDSU9O` | `TW_SELECCION` |
| `DBCoins` | `Q29pbnM=` | `Coins` |
| `HostDatabaseSoftland` | `U1NUV1xTUUwyMDE0` | `SSTW\SQL2014` |
| `UsernameDatabaseSoftland`| `U2E=` | `Sa` |
| `PasswordDatabaseSoftand` | `U29mdGxhbmQwNzA=` | `Softland070` |
| `DatabaseTWEST` | `VFdFU1Q=` | `TWEST` |
| `DatabaseTWRRHH` | `VEVBTVJSSEg=` | `TEAMRRHH` |
| `DatabaseTWC` | `VEVBTVdPUks=` | `TEAMWORK` |

## 5. Recomendaciones para Levantamiento Local

1.  **Base de Datos**: Instale SQL Server localmente. Cree las bases de datos listadas arriba.
2.  **Configuración**:
    *   Puede modificar el `Web.config` para apuntar a su `localhost` (o `.` in SQL Server).
    *   **Importante**: Si cambia los valores en `Web.config`, debe convertirlos a **Base64** antes de pegarlos, ya que el código los decodifica automáticamente.
3.  **Servicios**:
    *   Si no tiene acceso a la VPN o red de Teamwork, las conexiones LDAP y SMTP fallarán.
    *   Para desarrollo local sin estas dependencias, considere comentar el código en `SignController.cs` y `MailController.cs` o crear interfaces simuladas (Mocking).

## 6. Credenciales local

| Key | Valor Original | Valor codificado (Base64) |
| :--- | :--- | :--- |
| `HostDatabase` | `localhost` | `bG9jYWxob3N0` |
| `UsernameDatabase` | `sa` | `c2E=` |
| `PasswordDatabase` | `TuPassword123!` | `VHVQYXNzd29yZDEyMyE=` |
| `Database` | `TW_GENERAL_TEAMWORK` | `VFdfR0VORVJBTF9URUFNV09SSw==` |
| `DatabaseOperaciones` | `TW_OPERACIONES` | `VFdfT1BFUkFDSU9ORVM=` |
| `DatabaseSeleccion` | `TW_SELECCION` | `VFdfU0VMRUNDSU9O` |
| `DBCoins` | `Coins` | `Q29pbnM=` |
| `HostDatabaseSoftland` | `localhost` | `bG9jYWxob3N0` |
| `UsernameDatabaseSoftland` | `sa` | `c2E=` |
| `PasswordDatabaseSoftand` | `TuPassword123!` | `VHVQYXNzd29yZDEyMyE=` |
| `DatabaseTWEST` | `TWEST` | `VFdFU1Q=` |
| `DatabaseTWRRHH` | `TEAMRRHH` | `VEVBTVJSSEg=` |
| `DatabaseTWC` | `TEAMWORK` | `VEVBTVdPUks=` |

> [!NOTE]
>
> He analizado tu configuración para responder a tus preguntas sobre los puertos y la configuración local.
>
>1. Puertos y Hosts actuales: En tu Web.config, las conexiones están definidas así (decodificadas de Base64 to UTF8):
>
>HostDatabase: Apunta a 192.168.0.10. Al no especificar puerto, usa el por defecto 1433.
>HostDatabaseSoftland: Apunta a SSTW\SQL2014. Es una "Instancia Nombrada". Normalmente usa el puerto UDP 1434 para resolver el puerto TCP dinámico >de la instancia, o uno estático si se configuró así en el servidor.
>2. Cómo configurarlo en Local sin choques: Si tienes ambas bases de datos corriendo en tu local bajo el mismo puerto (ej. el default 1433), no hay >riesgo de choque por el puerto. La diferenciación se hace a través del nombre de la base de datos (Catálogo), no del puerto/host.
>
>El sistema usa nombres de base de datos distintos para cada contexto:
>
>Contexto Teamwork: TW_GENERAL_TEAMWORK, TW_OPERACIONES, TW_SELECCION.
>Contexto Softland: TWEST, TEAMRRHH, TEAMWORK.
>Pasos para tu configuración local: Para que funcione localmente apuntando a tu única instancia:
>
>Asegúrate de tener restauradas las bases de datos con esos nombres exactos en tu SQL Server local.
>Cambia en el Web.config tanto HostDatabase como HostDatabaseSoftland para que apunten a tu local.
>Valor: localhost
>Valor Base64 para poner en config: bG9jYWxob3N0
>Actualiza UsernameDatabase/PasswordDatabase (y las de Softland) con tus credenciales locales (también en Base64).
>De esta forma, ambas conexiones irán al mismo servidor (tu local), pero cada repositorio consultará su base de datos específica sin conflictos.

## Nota de ejecución en local
> [!NOTE]

Cuando ocurre el error:

```js
Description: An error occurred during the parsing of a resource required to service this request. Please review the following specific parse error details and modify your source file appropriately.

Parser Error Message: Could not load type 'Teamwork.Model.WebApiApplication'.

Source Error:


Line 1:  <%@ Application Codebehind="Global.asax.cs" Inherits="Teamwork.Model.WebApiApplication" Language="C#" %>
```

Se debe ejecuta la siguiente solución:

El error indica que se está intentando ejecutar la aplicación usando el archivo Global.asax que está dentro del proyecto Teamwork.Model, el cual tiene definido Inherits="Teamwork.Model.WebApiApplication".

Esto ocurre porque probablemente tienes Teamwork.Model configurado como el "Proyecto de Inicio" (Startup Project) en Visual Studio. Este proyecto, aunque parece ser una librería de modelos, está extrañamente configurado como una Aplicación Web, lo que causa la confusión.

Solución:

En el Explorador de Soluciones (Solution Explorer) de Visual Studio.
Haz clic derecho sobre el proyecto Teamwork.Api.
Selecciona "Establecer como proyecto de inicio" (Set as Startup Project).
Ejecuta nuevamente la aplicación.
El proyecto Teamwork.Api es la verdadera API y su Global.asax apunta correctamente a Teamwork.Api.WebApiApplication.

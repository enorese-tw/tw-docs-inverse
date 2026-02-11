# Análisis de Configuración para Entorno Local

Este documento detalla los puntos de configuración que deben modificarse para ejecutar la aplicación `PlataformaTeamwork` (solución `AplicacionOperaciones`) en un entorno local, apuntando a bases de datos y servicios locales.

## Resumen de Arquitectura

La aplicación **NO se conecta directamente a ninguna base de datos**. Funciona como un cliente que consume servicios distribuidos. Por lo tanto, para "apuntar a una base de datos local", en realidad se deben levantar instancias locales de los servicios que la aplicación consume, o bien, apuntar la aplicación a servicios de desarrollo/QA existentes.

Los servicios consumidos son:
1.  **API REST (Teamwork WebApi)**: Maneja lógica de negocio para Carga Masiva, Selección, Asistencia, Finiquitos, etc.
2.  **Servicios SOAP (WCF)**: Manejan Autenticación, Operaciones Core y Envío de Correos.

## Archivos de Configuración

### 1. `AplicacionOperaciones/Web.config`

Este es el archivo principal de configuración.

#### A. URL de la API REST

Se encuentra en la sección `<appSettings>`.

```xml
<add key="UrlApiRest" value="http://mario/api/"/>
```

*   **Para Local**: Cambiar `value` por la URL de su API local (ej. `http://localhost:1234/api/`).

#### B. Endpoints de Servicios WCF

Se encuentran en la sección `<system.serviceModel> / <client>`.

```xml
<client>
  <endpoint address="http://192.168.0.10/wsServicioOperacionesTesting/wsServicioOperaciones.svc" ... />
  <endpoint address="http://192.168.0.10/wsServicioCorreo/wsServicioCorreo.svc" ... />
  <endpoint address="http://192.168.0.10/wsServicioAuth/wsServicioAuth.svc" ... />
</client>
```

*   **Para Local**: Cambiar el atributo `address` de cada endpoint para que apunte a sus servicios WCF locales (ej. `http://localhost:5678/wsServicioOperaciones.svc`).

### 2. `Teamwork.WebApi/HelperCallAPI.cs`

Este archivo lee la clave `UrlApiRest` del `Web.config`. No requiere cambios de código, pero confirma que la dependencia es inyectada por configuración.

## Configuraciones "Hardcodeadas" (Atención)

Se encontraron direcciones IP y dominios escritos directamente en el código, lo cual puede afectar el funcionamiento local si no se corrigen o se tienen en cuenta.

### `AplicacionOperaciones/Controllers/AuthController.cs`

Este controlador contiene lógica de redirección y autenticación con múltiples URLs fijas.

*   **Línea 65, 269**: `http://181.190.6.196/AplicacionOperaciones` (Redirección si no es localhost).
*   **Línea 117, 170, 192**: Reemplazos de `http://192.168.0.10/TW_FINIQUITOS/`.
*   **Líneas 460, 485, 510, 535, 595**: Dominios externos como `teamclass.team-work.cl`, `planner.team-work.cl`, `coins.team-work.cl`.
*   **Línea 560**: Verificación de IP `181.190.6.196`.

**Recomendación**: Revisar este archivo si experimenta problemas de redirección al iniciar sesión o navegar entre módulos. Es posible que deba modificar estas líneas temporalmente para pruebas locales si sus servicios locales usan puertos diferentes a los esperados (ej. `localhost:3000`, `localhost:9090` que ya están contemplados en el código).

## Credenciales de Servicios Externos

No se encontraron credenciales explícitas (API Keys, usuarios/passwords de DB, SMTP) en los archivos de configuración de este proyecto.

*   **Base de Datos**: La conexión a SQL Server está encapsulada dentro de los servicios WCF (`wsServicioOperaciones`, etc.) y la API REST. Debe configurar las cadenas de conexión **en esos proyectos**, no en este.
*   **Correo (SMTP)**: El envío de correos se realiza a través de `wsServicioCorreo`. La configuración SMTP reside en ese servicio.
*   **Active Directory**: El `AuthController` (línea 284) hace referencia a `LDAP://team-work.cl`. Esto requerirá que su máquina tenga acceso a ese dominio o VPN si desea usar autenticación AD.

## Pasos para Levantar en Local

1.  **Desplegar Servicios**: Asegúrese de tener corriendo localmente (o tener acceso a) la API REST y los servicios WCF (`Auth`, `Operaciones`, `Correo`).
2.  **Configurar Endpoints**: Modifique `AplicacionOperaciones/Web.config` cambiando `UrlApiRest` y los `endpoint address` para apuntar a sus servicios.
3.  **Verificar AuthController**: Si usa puertos locales distintos a los hardcodeados (`46903`, `3000`, `9685`, `8080`), ajuste `AuthController.cs`.

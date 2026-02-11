# Análisis para Ejecución Local del Proyecto

Este documento detalla los requisitos y configuraciones necesarias para ejecutar el servicio `wsServiceCorreo` en un entorno local.

## 1. Resumen del Proyecto
- **Tipo**: Servicio WCF (.NET Framework 4.7.2)
- **Propósito**: Envío de correos electrónicos con plantillas y adjuntos.
- **Base de Datos**: No se encontraron conexiones directas a bases de datos en este proyecto.

## 2. Dependencias Externas

### a. Servicio SMTP (Correo Electrónico)
 El servicio utiliza servidores SMTP para el envío de correos. **IMPORTANTE**: Las credenciales y configuraciones están **hardcodeadas** en `ServicioCorreo/Correo.cs`.

**Configuración Principal (Gmail):**
- **Host**: `smtp.gmail.com`
- **Puerto**: `587`
- **Usuario**: `estserviceteamwork@gmail.com`
- **Contraseña**: `vhsg uxsi rrjc yxlw` (Hardcodeada)
- **SSL**: Habilitado

**Configuración Secundaria (TeamWork / Valle Nevado):**
- **Host**: `mail.teamworkchile.cl` / `www.team-work.cl`
- **Puerto**: `25`
- **Usuario**: Típicamente el mismo que el anterior o heredado.
- **SSL**: Habilitado / TLS

> **Nota**: Para ejecución local, asegúrese de que su red permita conexiones salientes a estos puertos y hosts.

### b. Sistema de Archivos (Rutas Absolutas)
El código hace referencia a rutas absolutas específicas que **deben existir** en la máquina local para que el servicio funcione sin errores al generar los correos (imágenes incrustadas).

**Directorio Requerido:**
`C:\inetpub\wwwroot\wsServicioCorreo\Resources\`

**Archivos Requeridos en ese directorio:**
El servicio busca las siguientes imágenes en la ruta mencionada:
1.  `logo-teamwork-chile.png`
2.  `logo-teamwork-sistemas.png`
3.  `LinkAdminSolB4J.png`
4.  `Logo-Marina.png`
5.  `Logo-Valle-Nevado-Celeste.png`
6.  `colorvn.jpg`

**Acción necesaria:**
Para ejecutar localmente, debe crear la carpeta `C:\inetpub\wwwroot\wsServicioCorreo\Resources\` y copiar los archivos de imagen desde la carpeta `ServicioCorreo/Resources` del proyecto a esa ubicación.

## 3. Configuración del Proyecto
- **Web.config**: Existe pero no contiene cadenas de conexión ni configuraciones de servicio personalizadas.
- **Código Fuente (`Correo.cs`)**: Contiene toda la lógica de configuración (SMTP, Rutas).

## 4. Pasos para Ejecución Local

1.  **Clonar/Descargar** el repositorio.
2.  **Crear Directorios**:
    -   Cree la estructura de carpetas: `C:\inetpub\wwwroot\wsServicioCorreo\Resources\`
3.  **Copiar Recursos**:
    -   Copie las imágenes de la carpeta `ServicioCorreo\Resources` del código fuente a la carpeta creada en el paso anterior.
4.  **Ejecutar en Visual Studio**:
    -   Abra `ServicioCorreo.sln`.
    -   El proyecto está configurado para usar IIS Express.
    -   Ejecute el proyecto.
5.  **Prueba**:
    -   Puede utilizar el `WCF Test Client` (que se abre automáticamente con VS al depurar servicios WCF) para invocar los métodos como `correoTeamworkEstandar`.

## 5. Recomendaciones de Mejora (Deuda Técnica)
-   **Externalizar Configuración**: Mover las credenciales SMTP y rutas de archivos al `Web.config` para no tenerlas hardcodeadas.
-   **Rutas Relativas**: Usar `Server.MapPath` o rutas relativas al directorio de ejecución para evitar depender de `C:\inetpub`.

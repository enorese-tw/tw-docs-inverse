# Guía de Configuración y Ejecución Local: Backend BYF

Esta guía detalla los pasos necesarios para configurar y ejecutar el proyecto **Backend BYF** en un entorno local de desarrollo.

## 1. Requisitos Previos

Antes de comenzar, asegúrate de tener instalado el siguiente software:

*   **Node.js**: Versión 14.x o superior (Recomendado: LTS actual).
*   **npm**: Gestor de paquetes incluido con Node.js.
*   **Git**: Para control de versiones.
*   **Acceso a Bases de Datos**: Credenciales de acceso a las instancias de Microsoft SQL Server requeridas (Teamwork, Softland, Auth).

## 2. Instalación

1.  **Clonar el repositorio**:
    ```bash
    git clone <url-del-repositorio>
    cd backend-byf
    ```

2.  **Instalar dependencias**:
    Ejecuta el siguiente comando en la raíz del proyecto para instalar las librerías necesarias:
    ```bash
    npm install
    ```

## 3. Configuración del Entorno (`.env`)

El proyecto utiliza variables de entorno para gestionar credenciales y configuraciones sensibles. Debes crear un archivo `.env` en la raíz del proyecto.

Copia el siguiente contenido en tu archivo `.env` y completa los valores con tus credenciales locales o de desarrollo:

```properties
# Configuración del Servidor
PORT=4000
PORTAPPLICATIONLOCAL=3000
APPLICATION=Teamwork
CDN=http://localhost:4000 # URL base para recursos públicos
NODE_ENV=development

# Base de Datos: Servidores
# Formato JSON escapado o simple, según la implementación en config.js parece esperar JSON para objetos complejos
SERVER={"teamwork":"HOST_SQL_TEAMWORK", "softland":"HOST_SQL_SOFTLAND"}

# Base de Datos: Credenciales
# Las claves deben coincidir con las definidas en SERVER (teamwork, softland)
USERS={"teamwork":"USER_TEAMWORK", "softland":"USER_SOFTLAND"}
PASSWORD={"teamwork":"PASS_TEAMWORK", "softland":"PASS_SOFTLAND"}

# Base de Datos: Nombres de Bases de Datos
DB={"operaciones":"NOMBRE_DB_TEAMWORK", "softland":"NOMBRE_DB_SOFTLAND"}
DBAUTH=NOMBRE_DB_AUTH

# Base de Datos Softland (Nombres Específicos por Empresa)
SERVERSOFTLAND=HOST_SQL_SOFTLAND
DBSOFTLANDTWEST=NOMBRE_DB_TWEST
DBSOFTLANDTWRRHH=NOMBRE_DB_TWRRHH
DBSOFTLANDTWC=NOMBRE_DB_TWC

# Seguridad y Autenticación
SECRET=tu_secreto_jwt_super_seguro
EXPIRESECRET=4h
SCHEMASECURITY=Bearer

# Integración Active Directory (LDAP)
LDAP=ldap://tu-servidor-ad
LDAPDC=dominio
LDAPDCC=com

# Integración Azure Blob Storage
AZURE_STORAGE_CONNECTION_STRING=TuCadenaDeConexionAzureBlobStorage

# Integración Correo (SMTP)
MAIL={"host":"smtp.office365.com", "port":587, "account":{"user":"tu@email.com", "pass":"tu_password"}}
```

> **Nota**: El archivo `src/api/config.js` utiliza `JSON.parse()` para varias variables (`SERVER`, `USERS`, `PASSWORD`, `DB`, `MAIL`). Asegúrate de que los valores en el `.env` para estas llaves sean cadenas JSON válidas.

## 4. Ejecución del Proyecto

### Modo Desarrollo
Para ejecutar la aplicación con recarga automática (nodemon):

```bash
npm run dev
```
Esto iniciará el servidor utilizando `babel-node`. Deberías ver logs indicando que el servidor está corriendo (por defecto en el puerto 4000).

### Modo Producción (Build)
Para compilar y ejecutar la versión optimizada:

1.  **Compilar**:
    ```bash
    npm run build
    ```
    Esto generará una carpeta `build/` con el código transpilado.

2.  **Iniciar**:
    ```bash
    npm start
    ```

## 5. Verificación

Para verificar que el backend se está ejecutando correctamente:

1.  Abre tu navegador o Postman.
2.  Accede a la documentación de Swagger (si está habilitada en la ruta base o `/api-docs`):
    *   URL Típica: `http://localhost:4000/api-docs` (Verificar ruta exacta en `src/routes`).
3.  Prueba un endpoint de salud o login.

## 6. Solución de Problemas Comunes

*   **Error de Conexión SQL**: Verifica que tu dirección IP esté permitida en el firewall del servidor SQL y que las credenciales en `.env` sean correctas.
*   **Error JSON Parse**: Si obtienes errores al iniciar relacionados con JSON, revisa que las variables de entorno complejas (`SERVER`, `MAIL`, etc.) tengan un formato JSON válido en el archivo `.env`.
*   **Módulos no encontrados**: Ejecuta `npm install` nuevamente para asegurar que todas las dependencias estén descargadas.

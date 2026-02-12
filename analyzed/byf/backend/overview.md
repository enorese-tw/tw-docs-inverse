# Visión General del Proyecto: Backend BYF (Bajas y Finiquitos)

## Introducción
El proyecto **Backend BYF** (Api Teamwork) es una aplicación Node.js construida con Express que sirve como backend para la gestión del proceso de **Bajas y Finiquitos** de la empresa. Su objetivo principal es orquestar el flujo de desvinculación de colaboradores, cálculo de finiquitos, generación de documentos legales y gestión de firmas, integrándose profundamente con el ERP **Softland**.

## Arquitectura Técnica

### Stack Tecnológico
- **Lenguaje**: Node.js (con Babel para soporte ES6+).
- **Framework Web**: Express.js.
- **Base de Datos**: Microsoft SQL Server (MSSQL). Utiliza el driver `mssql` nativo.
- **Almacenamiento**: Azure Blob Storage (para documentos).
- **Notificaciones**: Nodemailer (envío de correos).
- **Documentación API**: Swagger (OpenAPI).

### Patrón de Diseño
El proyecto sigue una arquitectura **MVC (Modelo-Vista-Controlador)** adaptada a una API REST:
- **Rutas (`/routes`)**: Definen los endpoints de la API.
- **Controladores (`/controllers`)**: Contienen la lógica de orquestación y manejo de peticiones.
- **Capa de Datos (`/database`)**: Maneja la conexión y ejecución de scripts SQL. No utiliza un ORM tradicional; se basa en consultas SQL crudas y Stored Procedures para interactuar con bases de datos heredadas.

## Estructura del Proyecto (`src`)

| Directorio | Descripción |
| :--- | :--- |
| `api` | Configuración de la aplicación Express (`app.js`) y carga de variables de entorno (`config.js`). |
| `common` | Utilidades compartidas, lógica de nombres de archivos y definiciones de opciones/estados. |
| `controllers` | Lógica de negocio principal (Auth, Bajas, Finiquitos, etc.). |
| `database` | Configuración de conexiones a MSSQL (Teamwork y Softland). |
| `document` | Configuración de Swagger para documentación de API. |
| `functions` | Funciones auxiliares (formato de fechas, RUT, emails, números). |
| `middlewares` | Middleware de seguridad (validación de tokens JWT, roles). |
| `routes` | Definición de endpoints REST. |
| `secure` | Lógica de seguridad adicional (roles). |
| `server` | Punto de entrada del servidor (`index.js`). |
| `services` | Integraciones externas (Azure Blob Storage, Nodemailer). |
| `templates` | Plantillas para generación de contenido (emails, reportes). |

## Módulos Principales y Funcionalidades

### 1. Autenticación y Seguridad
- **JWT (JSON Web Tokens)**: Manejo de sesiones y seguridad de endpoints.
- **Integración Active Directory (AD)**: Permite autenticación contra el directorio activo corporativo.
- **Control de Roles**: Middleware (`verifyRole_auth`, `getRoleUser`) para restringir acceso según perfil (admin, finanzas, validador, analista, etc.).

### 2. Gestión de Bajas (`bajas.controller.js`)
- **Listado y Filtros**: Permite visualizar colaboradores en proceso de baja, con filtrado por RUT, nombre, etc.
- **Integración Softland**: Consulta en tiempo real a bases de datos de Softland (`TWEST`, `TWRRHH`, `TWC`) para obtener:
    - Estado de **Sobregiros**.
    - Estado de **AFC** y **Seguro de Cesantía**.
    - **Cajas de Compensación (CCAF)**.
    - **Licencias Médicas**.
    - **Lucro Cesante**.
- **Gestión de Documentos**: Generación y recuperación de propuestas de finiquito y cartas de aviso.

### 3. Gestión de Finiquitos (`finiquitos.controller.js`)
- **Cálculo de Finiquitos**: Orquesta obtención de haberes, descuentos y vacaciones.
- **Lógica Multiempresa**: Maneja lógica diferenciada para distintas empresas del grupo (TWEST, TWRRHH, TWC) mediante `ServiceArray` y consultas dinámicas.
- **Máquina de Estados**: Gestiona el ciclo de vida del finiquito (estados, validaciones, flujos de aprobación) a través de `opcionesFiniquitos`.
- **Cálculo de Vacaciones**: Lógica compleja para determinar días hábiles, inhábiles, feriados y factores de vacaciones proporcionales, leyendo directamente desde las tablas de Softland.

### 4. Generación y Gestión de Documentos
- **Stored Procedures**: Gran parte de la generación de documentos (PDFs de cartas y finiquitos) parece delegada a Stored Procedures en la base de datos (e.g., `[finiquitos].[__PdfCarta]`).
- **Azure Blob Storage**: Servicio para subir y generar URLs de descarga segura (SAS Tokens) para documentos adjuntos.

## Integraciones Clave

### Softland ERP
El sistema no es autónomo; depende críticamente de la información en Softland.
- **Conexión Directa**: Utiliza conexiones SQL específicas (`getConnectionSoftland`) para consultar vistas y tablas del ERP.
- **Datos Consultados**:
    - Fichas de personal.
    - Variables de personal (sueldos, días trabajados).
    - Licencias médicas.
    - Historial de vacaciones.

### Azure
- **Storage**: Almacenamiento de evidencias y documentos generados.
- **Auth**: Posible integración con Azure AD para autenticación corporativa (mencionado en dependencias y configuración).

## Base de Datos
- **Multi-Base de Datos**: Se conecta a varias BBDD:
    - `TeamWork` (Operaciones/Finiquitos): Datos propios de la aplicación.
    - `Softland` (TWEST, TWRRHH, TWC): Datos maestros de RRHH y Nómina.
    - `Auth`: Datos de usuarios y roles.
- **Uso de Stored Procedures**: Fuerte dependencia de SPs para lógica de negocio y reportes, lo que delega parte de la inteligencia a la base de datos.

## Conclusión
Backend BYF es una pieza crítica de middleware que expone datos complejos de RRHH (Softland) a través de una API moderna, permitiendo flujos de trabajo de desvinculación controlados y digitales que el ERP por sí solo no provee. Su arquitectura destaca por la integración directa a nivel de base de datos con sistemas legados.

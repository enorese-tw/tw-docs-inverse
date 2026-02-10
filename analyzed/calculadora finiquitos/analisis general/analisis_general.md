# Análisis General del Proyecto FiniquitosV2

## Resumen Ejecutivo
El proyecto es una aplicación Web Forms (ASP.NET) heredada que gestiona el cálculo de finiquitos. Presenta una arquitectura monolítica con fuerte acoplamiento entre la interfaz de usuario y la lógica de negocio/acceso a datos.

## Estructura del Proyecto
- **Tecnología**: ASP.NET Web Forms (.NET Framework 4.x).
- **Lenguaje**: C#.
- **Base de Datos**: SQL Server (Softland y BD propia).
- **Frontend**: ASPX, jQuery, Bootstrap.

## Hallazgos Principales

### 1. Seguridad
- **Credenciales Hardcoded**: Se encontraron credenciales de administrador (`sa`) en el código fuente para conectar a la base de datos de Softland.
- **Inyección SQL**: Construcción de consultas SQL mediante concatenación de cadenas, vulnerable a ataques de inyección SQL.

### 2. Arquitectura
- **Lógica en Code-Behind**: La mayor parte de la lógica de negocio y orquestación de llamadas a datos reside en los archivos `.aspx.cs`.
- **Acceso a Datos Mixto**: Coexisten llamadas a un servicio WCF (`ServicioFiniquitos`) con conexiones directas a la base de datos usando `System.Data.SqlClient` y `System.Data.OleDb`.
- **Clases de Dominio Anémicas**: Las clases en `Clases/` actúan a menudo como contenedores de datos pero también incluyen lógica de acceso a datos (`Active Record` pattern implementado de forma inconsistente).

### 3. Mantenibilidad
- **Código Duplicado**: Lógica similar repetida en múltiples páginas de cálculo.
- **Alta Complejidad Ciclomática**: Métodos muy largos y con múltiples anidamientos (e.g., cálculo de vacaciones).

## Documentación Detallada
Para más detalles, consultar los siguientes documentos:
- [Análisis de Softland](analisis_softland.md)
- [Análisis de Datos y Cálculos](analisis_datos.md)

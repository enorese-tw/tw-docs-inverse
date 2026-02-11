# Configuración para Ejecución Local

Este documento detalla los requisitos y configuraciones necesarias para ejecutar el servicio **ServicePdf** en un entorno local.

## 1. Conexión a Base de Datos

El servicio utiliza una conexión a SQL Server definida en el archivo `Web.config`.

### Configuración Actual
- **Clave**: `connectionString`
- **Valor por defecto**: `Data Source=luigi;Initial Catalog=TW_OPERACIONES;Persist Security Info=True;User ID=ADMINTW;Password=Satw.261119`

### Requisitos Locales
Para ejecutar localmente, asegúrate de tener una instancia de SQL Server accesible y actualiza la cadena de conexión en `Web.config` para apuntar a tu servidor local.

- **Base de Datos**: `TW_OPERACIONES`
- **Autenticación**: Asegúrate de que el usuario especificado (o el que configures) tenga permisos de lectura y ejecución sobre la base de datos, específicamente para el esquema `finiquitos`.
- **Procedimientos Almacenados**: El código hace referencia explícita al procedimiento almacenado `[finiquitos].[__PdfComplemento]`. Este SP debe existir en tu base de datos local.

```xml
<!-- Ejemplo para LocalDB -->
<add key="connectionString" value="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TW_OPERACIONES;Integrated Security=True;" />
```

## 2. Servicios Externos (API)

El servicio consume una API externa para obtener datos y documentos.

### Configuración Actual
- **Clave**: `ApiByf`
- **Valor por defecto**: `http://localhost:4001/`

### Requisitos Locales
Debes tener el servicio de API ejecutándose en el puerto `4001` o actualizar esta clave en `Web.config` si tu servicio local corre en otro puerto/dirección.

### Endpoints Consumidos
El controlador `PdfController` realiza peticiones GET a los siguientes endpoints:

1.  **Obtener Documento PDF (String/HTML)**
    -   `GET {ApiByf}pdf/{type}/{documentType}/{idfiniquito}`
    -   Requiere Header: `Authorization: Bearer {glcid}`
    -   Respuesta esperada: JSON con campos `barcode` y `html`.

2.  **Obtener Liquidación PDF (Base64)**
    -   `GET {ApiByf}{type}/pdf/liquidacion/{idfiniquito}/{idarchivo}`
    -   Requiere Header: `Authorization: Bearer {glcid}`
    -   Respuesta esperada: JSON con campo `archivoBase64`.

## 3. Dependencias (Paquetes NuGet)

El proyecto depende de las siguientes librerías principales, restaurables vía NuGet:

-   `iTextSharp` (Generación/Manipulación de PDFs)
-   `NReco.PdfGenerator` (Conversión de HTML a PDF)
-   `Newtonsoft.Json` (Procesamiento de JSON)
-   `br.com.arcnet.barcodegenerator` (Generación de códigos de barras)

Asegúrate de restaurar los paquetes NuGet antes de compilar:
```bash
nuget restore Teamwork_pdf.sln
```

## 4. Estructura del Proyecto

-   **Tecnología**: ASP.NET MVC (.NET Framework 4.5)
-   **Entrada Principal**: `Global.asax.cs`
-   **Lógica de Negocio**: `Controllers/PdfController.cs`

## Resumen de Cambios para Local

1.  Abrir `Web.config`.
2.  Modificar `connectionString` para apuntar a tu SQL Server local.
3.  Verificar que `ApiByf` apunte a la instancia local de la API (ej. `http://localhost:4001/`).
4.  Compilar y ejecutar la solución.

# Documentación del Procedimiento Almacenado: `[remuneraciones].[__SPRM_CargoMod_CrearSolicitud]`

## Objetivo
Este procedimiento almacenado tiene como función principal **inicializar una nueva solicitud** de "Cargo Modificado". Genera un código temporal para la solicitud, crea el registro principal en la tabla `RM_CargosMod` e inserta registros iniciales de haberes no imponibles (ANI) por defecto.

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EMPRESA` | VARCHAR(MAX) | Identificador de la empresa (ej. 'TWEST'). Determina ciertas reglas de negocio por defecto. |
| `@USUARIOCREADOR` | VARCHAR(MAX) | Identificador del usuario creador (codificado en Base64, puede ser correo o nombre de usuario). |
| `@TYPE` | VARCHAR(MAX) | Tipo de solicitud (codificado en Base64). Determina el prefijo del código ('SLC' o 'CTZ'). |

## Parámetros de Salida

| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@CODE` | VARCHAR(MAX) | Código de estado de la operación (ej. '200'). |
| `@CODINE` | VARCHAR(MAX) | Código temporal generado para la solicitud (codificado en Base64). |
| `@MESSAGE` | VARCHAR(MAX) | Mensaje descriptivo del resultado ('Se ha creado el ambiente...'). |

## Lógica de Procesamiento

1.  **Identificación del Usuario y Perfil**:
    *   Decodifica `@USUARIOCREADOR` usando `[dbo].[FN_BASE64_DECODE]`.
    *   Determina el perfil del usuario (`@PROFILE`) consultando las tablas de autenticación (`TW_Usuarios`, `TW_Auth`, `TW_Profiles`).
2.  **Generación de Código Temporal (`@TEMPORALCODINE`)**:
    *   Decodifica `@TYPE`.
    *   Determina el prefijo: 'SLC' si el tipo es 'E', 'CTZ' en caso contrario.
    *   Obtiene la siguiente secuencia desde la tabla `[remuneraciones].[RM_Codigo]`.
    *   Incrementa la secuencia en `RM_Codigo` para el siguiente uso.
3.  **Creación de Registro en `RM_CargosMod`**:
    *   Inserta un nuevo registro con el código generado (`@TEMPORALCODINE + '-' + @EMPRESA`).
    *   Valores por defecto importantes:
        *   `Estado`: 'CREATE' si es KAM, 'PDAPROB' si es otro perfil.
        *   `DiasTrabajados`: 30.
        *   `AFP`: Por defecto según empresa (`[remuneraciones].[FNAFPDefault]`).
        *   `IsapreFonosa`: 'FONASA'.
        *   `Wizards`: 1 (Fase inicial del asistente).
4.  **Inicialización de ANIs**:
    *   Inserta dos registros por defecto en `[remuneraciones].[RM_ANICargoMod]` ('ANI00001' y 'ANI00002') con valor 0.
5.  **Retorno**:
    *   Establece `@CODE` = '200'.
    *   Codifica el nuevo código temporal en Base64 y lo asigna a `@CODINE`.

## Tablas afectadas, si es que hay y con que operaciones

| Tabla | Operación | Descripción |
| :--- | :--- | :--- |
| `[remuneraciones].[RM_Codigo]` | **UPDATE** | Incrementa la secuencia para el siguiente código disponible. |
| `[remuneraciones].[RM_CargosMod]` | **INSERT** | Crea el registro principal de la solicitud. |
| `[remuneraciones].[RM_ANICargoMod]` | **INSERT** | Crea registros iniciales de haberes no imponibles. |

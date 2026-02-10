# `[finiquitos].[__PdfFiniquitoDocumento]`

## Objetivo

Procedimiento almacenado encargado de **generar (o recuperar de caché) el documento principal del finiquito** en formato HTML/PDF. Implementa un patrón de caché perezoso (lazy caching): verifica si el PDF ya fue generado previamente y, solo si no existe, ejecuta dinámicamente un procedimiento del esquema `[pdf]` para producir el contenido HTML, lo almacena en caché y finalmente lo retorna.

---

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
|---|---|---|
| `@IDFINIQUITO` | `NUMERIC` | Identificador único del finiquito a procesar |

---

## Variables Internas

| Variable | Tipo | Descripción |
|---|---|---|
| `@__DATA` | `VARCHAR(MAX)` | ID del finiquito codificado en Base64. Se obtiene mediante `[dbo].[FNBase64Encode](@IDFINIQUITO)` |
| `@__HTML` | `VARCHAR(MAX)` | Contenido HTML del PDF generado. Se recibe como OUTPUT del SP dinámico |
| `@__TITLE` | `VARCHAR(MAX)` | Título del documento. Se recibe como OUTPUT del SP dinámico, **pero nunca se utiliza después** |
| `@__BARCODE` | `VARCHAR(MAX)` | Código de barras del documento. Se recibe como OUTPUT del SP dinámico, **pero nunca se utiliza después** |
| `@__EXISTEPDF` | `NUMERIC` | Contador que indica si ya existe un PDF en caché (0 = no existe) |
| `@__SQL` | `NVARCHAR(MAX)` | Sentencia SQL dinámica construida para invocar el SP generador de PDF |
| `@__PDFDOCUMENTO` | `VARCHAR(MAX)` | Nombre del template PDF obtenido de la vista `View_PdfDocumentosFiniquito` |

> **Nota:** Las variables `@__TITLE` y `@__BARCODE` se declaran y reciben valores pero no se usan. El barcode retornado se construye directamente como `'FNQ-' + IdFiniquito`.

---

## Llamadas Internas (Funciones y Vistas)

### Funciones

| Función | Esquema | Tipo | Uso |
|---|---|---|---|
| `FNBase64Encode` | `dbo` | Función escalar | Codifica `@IDFINIQUITO` a Base64 para pasarlo al generador de PDF |

### Vistas

| Vista | Esquema | Uso |
|---|---|---|
| `View_Pdf` | `finiquitos` | **Lectura**. Se consulta para: (1) verificar si el PDF ya existe en caché filtrando por `IdFiniquito` y `TipoPdf = 'Finiquito'`; (2) retornar el resultado final |
| `View_PdfDocumentosFiniquito` | `finiquitos` | **Lectura**. Se consulta para obtener el nombre del template PDF asociado al finiquito, filtrando por `IdFiniquito` y `TipoDoc = 'Finiquito'` |

### Procedimientos Dinámicos

| Procedimiento | Esquema | Descripción |
|---|---|---|
| `__[NombreTemplate]` | `pdf` | Se invoca dinámicamente mediante `sys.sp_executesql`. El nombre se construye como `[pdf].[__` + valor de `@__PDFDOCUMENTO` + `]`. Recibe la data en Base64 y retorna `@HTMLOUT`, `@BARCODEOUT` y `@TITLEOUT` como parámetros OUTPUT |

---

## Lógica del Procedimiento

### Paso 1 — Codificación Base64
```sql
SET @__DATA = [dbo].[FNBase64Encode](@IDFINIQUITO)
```
Convierte el ID del finiquito a Base64. Este valor se pasa como argumento al SP de generación de PDF.

### Paso 2 — Verificación de Caché
```sql
SELECT @__EXISTEPDF = COUNT(1)
       FROM [finiquitos].[View_Pdf] VP
       WHERE VP.IdFiniquito = @IDFINIQUITO AND
             VP.TipoPdf = 'Finiquito'
```
Verifica si ya existe un PDF de tipo `'Finiquito'` almacenado. Si `COUNT(1) > 0`, se salta la generación.

### Paso 3 — Obtención del Template (solo si `@__EXISTEPDF = 0`)
```sql
SELECT @__PDFDOCUMENTO = VPDF.Pdf
       FROM [finiquitos].[View_PdfDocumentosFiniquito] VPDF
       WHERE VPDF.IdFiniquito = @IDFINIQUITO AND
             VPDF.TipoDoc = 'Finiquito'
```
Obtiene el nombre del template PDF configurado para este finiquito.

### Paso 4 — Ejecución Dinámica del Generador
```sql
SET @__SQL =
'
EXEC [pdf].[__' + @__PDFDOCUMENTO + '] 
        ''' + @__DATA + ''',
        @HTMLOUT OUTPUT,
        @BARCODEOUT OUTPUT,
        @TITLEOUT OUTPUT
'

EXEC sys.sp_executesql 
     @__SQL,
     N'@HTMLOUT VARCHAR(MAX) OUTPUT, @BARCODEOUT VARCHAR(MAX) OUTPUT, @TITLEOUT VARCHAR(MAX) OUTPUT',
     @HTMLOUT = @__HTML OUTPUT,
     @BARCODEOUT = @__BARCODE OUTPUT,
     @TITLEOUT = @__TITLE OUTPUT
```
Construye y ejecuta una sentencia SQL dinámica que invoca el procedimiento `[pdf].[__NombreTemplate]`, obteniendo el HTML generado como OUTPUT.

### Paso 5 — Almacenamiento en Caché
```sql
INSERT INTO [finiquitos].[FN_Pdf]
       VALUES(@IDFINIQUITO,
              @__HTML,
              'Finiquito')
```
Inserta el HTML generado en la tabla de caché con tipo `'Finiquito'`.

### Paso 6 — Retorno del Resultado
```sql
SELECT VP.Pdf 'html',
       'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX)) 'barcode'
       FROM [finiquitos].[View_Pdf] VP
       WHERE VP.IdFiniquito = @IDFINIQUITO AND
             VP.TipoPdf = 'Finiquito'
```
Siempre retorna el PDF desde la vista (ya sea que existiera previamente o se haya generado en esta ejecución).

---

## Retorno

| Campo | Alias | Tipo | Descripción |
|---|---|---|---|
| `VP.Pdf` | `html` | `VARCHAR(MAX)` | Contenido HTML del documento PDF |
| `'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX))` | `barcode` | `VARCHAR(MAX)` | Código de barras estandarizado, ej: `'FNQ-1234'` |

---

## Tablas Afectadas

| Tabla | Esquema | Operación | Condición |
|---|---|---|---|
| `FN_Pdf` | `finiquitos` | `INSERT` | Solo si el PDF no existía previamente en caché |

---

## Lógica de Cálculo

**No se encontraron cálculos aritméticos o de negocio.** El procedimiento es exclusivamente de orquestación: verificar caché, invocar generador y almacenar resultado.

---

## Manejo de Errores

Utiliza `BEGIN TRY / BEGIN CATCH` con transacción sin nombre. En caso de error, ejecuta `ROLLBACK TRANSACTION` pero **no retorna el mensaje de error** al llamador.

```sql
BEGIN CATCH
    ROLLBACK TRANSACTION
END CATCH
```

---

## Código Comentado

No se encontró código comentado en este archivo.

---

## Observaciones

1. Las variables `@__TITLE` y `@__BARCODE` se reciben como OUTPUT pero nunca se usan — el barcode se reconstruye con formato fijo `'FNQ-' + IdFiniquito`
2. A diferencia de los otros dos SPs del grupo (`__PdfFiniquitoCaratula` y `__PdfCarta`), este procedimiento **no retorna el mensaje de error** en el bloque `CATCH`
3. La transacción no tiene nombre, a diferencia de `__PdfCarta` que usa transacción nombrada `pdf`

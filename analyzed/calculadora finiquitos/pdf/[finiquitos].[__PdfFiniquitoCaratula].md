# `[finiquitos].[__PdfFiniquitoCaratula]`

## Objetivo

Procedimiento almacenado encargado de **generar (o recuperar de caché) la carátula del finiquito** en formato HTML/PDF. Implementa el mismo patrón de caché perezoso que los otros SPs del grupo: verifica si el PDF de tipo `'Caratula'` ya fue generado, y si no existe, lo genera dinámicamente y lo almacena.

---

## Parámetros de Entrada

| Parámetro | Tipo | Descripción |
|---|---|---|
| `@IDFINIQUITO` | `NUMERIC` | Identificador único del finiquito |

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
| `View_Pdf` | `finiquitos` | **Lectura**. Se consulta para: (1) verificar si el PDF ya existe en caché filtrando por `IdFiniquito` y `TipoPdf = 'Caratula'`; (2) retornar el resultado final |
| `View_PdfDocumentosFiniquito` | `finiquitos` | **Lectura**. Se consulta para obtener el nombre del template PDF, filtrando por `IdFiniquito` y `TipoDoc = 'Caratula'` |

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
Convierte el ID del finiquito a Base64.

### Paso 2 — Verificación de Caché
```sql
SELECT @__EXISTEPDF = COUNT(1)
       FROM [finiquitos].[View_Pdf] VP
       WHERE VP.IdFiniquito = @IDFINIQUITO AND
             VP.TipoPdf = 'Caratula'
```
Verifica si ya existe un PDF de tipo `'Caratula'` almacenado.

### Paso 3 — Obtención del Template (solo si `@__EXISTEPDF = 0`)
```sql
SELECT @__PDFDOCUMENTO = VPDF.Pdf
       FROM [finiquitos].[View_PdfDocumentosFiniquito] VPDF
       WHERE VPDF.IdFiniquito = @IDFINIQUITO AND
             VPDF.TipoDoc = 'Caratula'
```
Obtiene el nombre del template PDF de tipo `'Caratula'` configurado para este finiquito.

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
Construye y ejecuta el SQL dinámico para generar el HTML de la carátula.

### Paso 5 — Almacenamiento en Caché
```sql
INSERT INTO [finiquitos].[FN_Pdf]
       VALUES(@IDFINIQUITO,
              @__HTML,
              'Caratula')
```
Inserta el HTML generado en la tabla de caché con tipo `'Caratula'`.

### Paso 6 — Retorno del Resultado
```sql
SELECT VP.Pdf 'html',
       'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX)) 'barcode'
       FROM [finiquitos].[View_Pdf] VP
       WHERE VP.IdFiniquito = @IDFINIQUITO AND
             VP.TipoPdf = 'Caratula'
```
Retorna el PDF de la carátula desde la vista.

---

## Retorno

| Campo | Alias | Tipo | Descripción |
|---|---|---|---|
| `VP.Pdf` | `html` | `VARCHAR(MAX)` | Contenido HTML de la carátula del finiquito |
| `'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX))` | `barcode` | `VARCHAR(MAX)` | Código de barras estandarizado, ej: `'FNQ-1234'` |

---

## Tablas Afectadas

| Tabla | Esquema | Operación | Condición |
|---|---|---|---|
| `FN_Pdf` | `finiquitos` | `INSERT` | Solo si el PDF de carátula no existía previamente en caché |

---

## Lógica de Cálculo

**No se encontraron cálculos aritméticos o de negocio.** El procedimiento es exclusivamente de orquestación.

---

## Manejo de Errores

Utiliza `BEGIN TRY / BEGIN CATCH` con transacción sin nombre. En caso de error, ejecuta `ROLLBACK TRANSACTION` y **sí retorna el mensaje de error** con alias `'ERROR'`:

```sql
BEGIN CATCH
    ROLLBACK TRANSACTION
    SELECT ERROR_MESSAGE() 'ERROR'
END CATCH
```

---

## Código Comentado

No se encontró código comentado en este archivo.

---

## Observaciones

1. Las variables `@__TITLE` y `@__BARCODE` se reciben como OUTPUT pero nunca se usan
2. Este SP **sí retorna el error** a diferencia de `__PdfFiniquitoDocumento` que no lo hace
3. Estructuralmente es casi idéntico a `__PdfFiniquitoDocumento`, la única diferencia funcional es que opera con tipo `'Caratula'` en lugar de `'Finiquito'`

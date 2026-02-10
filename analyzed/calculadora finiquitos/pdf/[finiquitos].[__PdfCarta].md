# `[finiquitos].[__PdfCarta]`

## Objetivo

Procedimiento almacenado encargado de **generar (o recuperar de caché) la carta de baja (Propuesta) del finiquito** en formato HTML/PDF. Igual que los otros SPs del grupo, implementa un patrón de caché perezoso, pero presenta una particularidad: el tipo de documento para buscar el template (`'CartaBaja'`) difiere del tipo usado para el caché (`'Propuesta'`).

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
| `View_Pdf` | `finiquitos` | **Lectura**. Se consulta para: (1) verificar si el PDF ya existe en caché filtrando por `IdFiniquito` y `TipoPdf = 'Propuesta'`; (2) retornar el resultado final |
| `View_PdfDocumentosFiniquito` | `finiquitos` | **Lectura**. Se consulta para obtener el nombre del template PDF, filtrando por `IdFiniquito` y `TipoDoc = 'CartaBaja'` |

> **Importante:** Nótese la diferencia de tipos: el template se busca con `'CartaBaja'` pero el caché se maneja con `'Propuesta'`. Internamente el documento se denomina "CartaBaja" pero se presenta al sistema de PDF como "Propuesta".

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
             VP.TipoPdf = 'Propuesta'
```
Verifica si ya existe un PDF de tipo `'Propuesta'` almacenado.

### Paso 3 — Obtención del Template (solo si `@__EXISTEPDF = 0`)
```sql
SELECT @__PDFDOCUMENTO = VPDF.Pdf
       FROM [finiquitos].[View_PdfDocumentosFiniquito] VPDF
       WHERE VPDF.IdFiniquito = @IDFINIQUITO AND
             VPDF.TipoDoc = 'CartaBaja'
```
Obtiene el nombre del template PDF. **Aquí se usa `'CartaBaja'` como TipoDoc**, no `'Propuesta'`.

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
Construye y ejecuta el SQL dinámico para generar el HTML de la carta de baja.

### Paso 5 — Almacenamiento en Caché
```sql
INSERT INTO [finiquitos].[FN_Pdf]
       VALUES(@IDFINIQUITO,
              @__HTML,
              'Propuesta')
```
Inserta el HTML generado en la tabla de caché con tipo `'Propuesta'`.

### Paso 6 — Retorno del Resultado
```sql
SELECT VP.Pdf 'html',
       'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX)) 'barcode'
       FROM [finiquitos].[View_Pdf] VP
       WHERE VP.IdFiniquito = @IDFINIQUITO AND
             VP.TipoPdf = 'Propuesta'
```
Retorna el PDF de la carta/propuesta desde la vista.

---

## Retorno

| Campo | Alias | Tipo | Descripción |
|---|---|---|---|
| `VP.Pdf` | `html` | `VARCHAR(MAX)` | Contenido HTML de la carta de baja / propuesta |
| `'FNQ-' + CAST(VP.IdFiniquito AS VARCHAR(MAX))` | `barcode` | `VARCHAR(MAX)` | Código de barras estandarizado, ej: `'FNQ-1234'` |

---

## Tablas Afectadas

| Tabla | Esquema | Operación | Condición |
|---|---|---|---|
| `FN_Pdf` | `finiquitos` | `INSERT` | Solo si el PDF de propuesta no existía previamente en caché |

---

## Lógica de Cálculo

**No se encontraron cálculos aritméticos o de negocio.** El procedimiento es exclusivamente de orquestación.

---

## Manejo de Errores

Utiliza `BEGIN TRY / BEGIN CATCH` con **transacción nombrada** `pdf`. En caso de error, ejecuta `ROLLBACK TRANSACTION pdf` y retorna el mensaje de error **sin alias**:

```sql
BEGIN CATCH
    ROLLBACK TRANSACTION pdf
    select ERROR_MESSAGE()
END CATCH
```

---

## Código Comentado

No se encontró código comentado en este archivo.

---

## Observaciones

1. **Diferencia de tipos**: Usa `'CartaBaja'` como `TipoDoc` para buscar el template pero `'Propuesta'` como `TipoPdf` para el caché. Esto puede generar confusión al leer el código
2. **Transacción nombrada**: Este es el único de los 3 SPs que usa transacción nombrada (`pdf`), mientras que los otros dos usan transacciones anónimas
3. Las variables `@__TITLE` y `@__BARCODE` se reciben como OUTPUT pero nunca se usan
4. Retorna `ERROR_MESSAGE()` sin alias, a diferencia de `__PdfFiniquitoCaratula` que lo retorna con alias `'ERROR'`
5. El `select ERROR_MESSAGE()` usa `select` en minúsculas, lo cual es una inconsistencia estilística menor con el resto del código

# Función: [remuneraciones].[FNAFPDefault]

## Objetivo
Esta función escalar tiene como objetivo obtener el nombre de la AFP por defecto asociada a una empresa específica (`TWEST`, `TWRRHH`, `TWC`). El código de la AFP se recupera de una tabla de constantes y luego se busca el nombre en la vista correspondiente de la empresa.

## Parámetros de Entrada
| Parámetro | Tipo | Descripción |
| :--- | :--- | :--- |
| `@EMPRESA` | `VARCHAR(MAX)` | Identificador de la empresa (Ej: 'TWEST', 'TWRRHH', 'TWC'). |

## Variables Internas
- `@AFPDEFAULT` (`VARCHAR(MAX)`): Almacena el nombre de la AFP que será retornado.
- `@CODIGOAFP` (`VARCHAR(10)`): Almacena el código de 2 dígitos de la AFP obtenido de las constantes.

## Lógica de Cálculo

1. **Obtención del Código de AFP**:
   Se consulta la tabla `[remuneraciones].[RM_Constantes]` para obtener el valor de la variable de configuración según la empresa:
   - `TWEST` -> Variable `A002`
   - `TWRRHH` -> Variable `A003`
   - `TWC` -> Variable `A004`
   
   El valor obtenido se formatea para asegurar que tenga 2 caracteres, rellenando con un cero a la izquierda si es necesario.

2. **Búsqueda del Nombre**:
   Dependiendo del valor de `@EMPRESA`, se consulta una de las siguientes vistas utilizando el `@CODIGOAFP`:
   - `[remuneraciones].[View_AfpTWEST]`
   - `[remuneraciones].[View_AfpTWRRHH]`
   - `[remuneraciones].[View_AfpTWC]`

3. **Retorno**:
   Retorna el nombre encontrado (`@AFPDEFAULT`).

## Tablas y Vistas Afectadas
- **Lectura**:
    - `[remuneraciones].[RM_Constantes]`
    - `[remuneraciones].[View_AfpTWEST]`
    - `[remuneraciones].[View_AfpTWRRHH]`
    - `[remuneraciones].[View_AfpTWC]`

## Código Comentado
No se observa código comentado en este archivo.

---

## Ejemplo del formato final del código (Constantes)

A solicitud del usuario, se detalla el formato final que se obtiene al procesar el valor de la constante.

### Lógica de formateo:
```sql
SELECT @CODIGOAFP = RIGHT('00' + LTRIM(RTRIM(CAST(RMC.Valor AS VARCHAR(MAX)))), 2)
```

### Ejemplos de transformación:
| Valor Original (`RMC.Valor`) | Paso 1: Cast & Trim | Paso 2: Concatenar '00' | Paso 3: `RIGHT(..., 2)` (Resultado Final) |
| :--- | :--- | :--- | :--- |
| `1` | `'1'` | `'001'` | `'01'` |
| `5` | `'5'` | `'005'` | `'05'` |
| `12` | `'12'` | `'0012'` | `'12'` |
| ` 7 ` | `'7'` | `'007'` | `'07'` |

Este formato asegura que el código de la AFP siempre sea un string de 2 caracteres, lo cual es crítico para el cruce con las vistas de AFP por empresa.

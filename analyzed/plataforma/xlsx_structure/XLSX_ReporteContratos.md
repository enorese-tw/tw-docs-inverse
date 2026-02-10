# XLSX_ReporteContratos / XLSX_ReporteRenovaciones

Estos reportes son de estructura dinámica simple, volcando directamente el resultado de una API externa.

## Propósito
Obtener un listado plano de los contratos o renovaciones registradas en el sistema para un cliente y periodo específico.

## Llamadas a Servicios / APIs

| Servicio/Clase | Método | Parámetros Entrada | Salida | Propósito |
| :--- | :--- | :--- | :--- | :--- |
| `CallAPICargaMasiva` | `__ReporteContratos` | `cliente`, `fechaInicio`, `fechaTermino`, `empresa`, `token` | `string` (JSON) | Obtiene los datos de contratos. |
| `CallAPICargaMasiva` | `__ReporteRenovaciones` | `cliente`, `fechaInicio`, `fechaTermino`, `empresa`, `token` | `string` (JSON) | Obtiene los datos de renovaciones. |

## Estructura de Salida (Hoja: Reporte)

Debido a que el API devuelve objetos con propiedades `Column1`, `Column2`, etc., la estructura depende de la consulta SQL subyacente en el API. Sin embargo, el código itera sobre todas las propiedades disponibles:

| Celda | Valor |
| :--- | :--- |
| [Row i, Col j] | `objects[i]["Column" + j]` |

### Atributos representados (estimados)
Normalmente estos reportes incluyen:
- Rut Trabajador
- Nombre Trabajador
- Empresa
- Fecha Inicio
- Fecha Término
- Cargo
- Estado Solicitud

## Lógica Especial
- No aplica formatos de celda ni colores complejos.
- Utiliza `LicenseContext.NonCommercial` para EPPlus.
- Realiza una autenticación previa (`Authenticate.__Authenticate()`) para obtener el token necesario para el API.

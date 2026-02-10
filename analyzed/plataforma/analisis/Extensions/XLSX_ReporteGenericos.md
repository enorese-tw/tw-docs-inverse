# Análisis de Reportes: Contratos y Renovaciones

## 1. Propósito
Los componentes `XLSX_ReporteContratos.cs` y `XLSX_ReporteRenovaciones.cs` se encargan de generar listados genéricos de contratos y renovaciones respectivamente, basándose en filtros de búsqueda del usuario.

## 2. Lógica de Interacción (APIs)

### **Llamadas a API Carga Masiva**
Ambos componentes llaman a métodos estáticos de la clase `CallAPICargaMasiva` para obtener los datos en formato JSON:
*   **Contratos**: Invoca `CallAPICargaMasiva.__ReporteContratos`.
*   **Renovaciones**: Invoca `CallAPICargaMasiva.__ReporteRenovaciones`.

### **Parámetros de Entrada**
*   `cliente`: ID o nombre del cliente.
*   `fechaInicioFilter`: Inicio del rango de fechas.
*   `FechaTerminoFilter`: Fin del rango de fechas.
*   `empresa`: Empresa asociada.
*   `token`: Token de autenticación obtenido vía `Authenticate.__Authenticate()`.

---

## 3. Estructura del Excel
El archivo Excel contiene una única hoja denominada **Reporte**.

### **Mapeo de Datos**
Dado que los datos provienen de un JSON dinámico, el sistema itera sobre las propiedades del objeto de respuesta (`Column1`, `Column2`, etc.) e inyecta los valores secuencialmente en las celdas de la hoja.

---

## 4. Origen de Datos
Los datos son obtenidos mediante una petición HTTP interna a la API de carga masiva, la cual a su vez consulta la base de datos para generar el listado dinámico solicitado.

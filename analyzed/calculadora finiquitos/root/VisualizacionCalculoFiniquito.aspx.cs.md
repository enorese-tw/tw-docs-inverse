# VisualizacionCalculoFiniquito.aspx.cs

## Información General

| Campo | Valor |
|---|---|
| **Archivo** | `FiniquitosV2/VisualizacionCalculoFiniquito.aspx.cs` |
| **Namespace** | `FiniquitosV2` |
| **Clase** | `VisualizacionCalculoFiniquito : System.Web.UI.Page` |
| **Líneas de código** | 247 |
| **Dependencias clave** | `System.Web.Services`, `Newtonsoft.Json`, `FiniquitosV2.Clases` |

## Descripción Funcional

Página de **visualización y consulta** de finiquitos calculados. Provee 15 WebMethods estáticos que actúan como **API REST-like** para el frontend. Permite consultar, filtrar y gestionar finiquitos ya calculados.

## Métodos

### `Page_Load(object sender, EventArgs e)`
- Valida sesión y redirige según rol.
- Acceso restringido: solo rol `1` (admin) y `7` (finanzas) y roles no restringidos.

### WebMethods (15 endpoints AJAX)

| Método | Propósito |
|---|---|
| `GetDesvinculacionesService(filtrarPor, filtro)` | Busca finiquitos por filtro configurable |
| `GetDesvinculacionesPendientesService()` | Lista finiquitos pendientes |
| `GetDesvinculacionesRecientesService()` | Lista finiquitos recientes |
| `GetCausalService()` | Obtiene causales de despido |
| `SetEditarDesvinculacion(...)` | Edita datos de una desvinculación |
| `SetCambiarEstadoDesvinculacion(...)` | Cambia estado |
| `SetRegistrarFechaFirmaDesvinculacion(...)` | Registra fecha de firma |
| `GetContratosCalculados(idDesvinculacion)` | Obtiene contratos calculados |
| `GetHaberesDescuentos(idDesvinculacion)` | Obtiene haberes y descuentos del cálculo |
| `GetTotalesCalculo(idDesvinculacion)` | Obtiene totales del cálculo |
| `GetUserRole()` | Retorna rol del usuario actual |
| `SetFinalizarDesvinculacion(...)` | Finaliza proceso de desvinculación |
| `GetListadoEstados()` | Lista estados disponibles |
| `SetCambiarEstadoDesvinculacionModificacion(...)` | Cambia estado con motivo de modificación |
| `GetObtenerEmpresasVisualizacion()` | Lista empresas disponibles |

## Patrón de Código

Todos los WebMethods siguen el mismo patrón:

```csharp
[WebMethod()]
public static string MetodoX(string param1, string param2) {
    MethodServiceFiniquitos web = new MethodServiceFiniquitos();
    web.PARAM1 = param1;
    web.PARAM2 = param2;
    return JsonConvert.SerializeObject(web.PropiedadService, Formatting.Indented);
}
```

## Observaciones

> [!TIP]
> Este archivo es un buen ejemplo de cómo deberían estar estructurados los code-behind: delgados, sin lógica de negocio, actuando solo como proxies JSON hacia los servicios.

> [!NOTE]
> El patrón repetitivo de WebMethods sugiere que podría reemplazarse con un controlador genérico o una API Web estándar.

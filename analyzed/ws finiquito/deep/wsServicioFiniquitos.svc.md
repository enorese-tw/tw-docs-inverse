# Documentación Detallada: `wsServicioFiniquitos.svc.cs`

## Visión General
Esta clase implementa la interfaz `IServicioFiniquitos` y actúa como la capa de fachada (Facade) del servicio WCF. Su responsabilidad principal es recibir las peticiones SOAP, orquestar la llamada a la lógica de negocio adecuada y formatear la respuesta en un `DataSet` estándar.

## Implementación de la Interfaz
La clase `wsServicioFiniquitos` implementa explícitamente todos los métodos definidos en el contrato de servicio.

## Lógica Interna y Patrones

### `ArrayToListParametros(string[] Parametros, string[] valores)`
*   **Función**: Convierte los dos arrays paralelos recibidos en la petición SOAP (`Parametros` y `valores`) en una lista de objetos `Parametro`.
*   **Importancia**: Es el mecanismo de "binding" manual que utiliza el servicio para pasar argumentos a la capa de datos.

### Patrón de Delegación
Casi todos los métodos del servicio siguen un patrón idéntico de delegación:

1.  **Instanciación**: Crea una instancia de la clase de lógica de negocio correspondiente (`CPagos`, `CFiniquitos`, `CSolB4J`, etc.).
2.  **Preparación de Respuesta**: Inicializa `ServicioFiniquito` y un `DataSet`.
3.  **Ejecución**: Llama al método de negocio, pasando los parámetros convertidos si es necesario.
4.  **Empaquetado**: Agrega el `DataTable` retornado al `DataSet` de respuesta (`ds.Tables.Add(...)`).
5.  **Retorno**: Devuelve el objeto `ServicioFiniquito`.

Ejemplo Típico:
```csharp
public ServicioFiniquito GetObtenerProveedoresProveedor()
{
    CPagos pagos = new CPagos(); // 1. Instancia Lógica
    ServicioFiniquito IService = new ServicioFiniquito(); // 2. Prepara Respuesta
    DataSet ds = new DataSet();

    ds.Tables.Add(pagos.GetObtenerProveedoresProveedor()); // 3, 4. Ejecuta y agrega
    IService.Table = ds;

    return IService; // 5. Retorna
}
```

## Mapeo de Responsabilidades

| Método de Servicio | Clase Lógica Delegada |
| :--- | :--- |
| `GetObtenerProveedoresProveedor` | `CPagos` |
| `GetObtenerSolicitudesB4J` | `CSolB4J` |
| `GetCargaVariable` | `CFiniquitos` |
| `GetRutTrabajadorSolicitudBajaCONSULTORA` | `CCONSULTORASoftland` | (Aunque no visible en snippet, se infiere por nombre) |
| `GetCentroCostos` | `CESTSoftland` | (Inferencia por contexto EST/Softland) |

## Observaciones de Arquitectura
*   **Acoplamiento Fuerte**: El servicio instancia directamente las clases de lógica (`new CPagos()`), lo que dificulta las pruebas unitarias (no hay Inyección de Dependencias).
*   **Manejo de Errores**: No se observa un manejo de excepciones explícito (`try-catch`) en estos métodos. Si la lógica de negocio falla, la excepción (probablemente SOAP Fault) se propagará directamente al cliente.

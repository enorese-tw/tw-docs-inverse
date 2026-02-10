# Documentación de Consultas: `CCONSULTORASoftland.cs`

## Contexto
Esta clase interactúa exclusivamente con la instancia **CONSULTORA** de la base de datos Softland.

## Consultas Identificadas

### 1. Obtención de Datos de Trabajador para Baja
*   **Método**: `GetRutTrabajadorSolicitudBajaCONSULTORA`
*   **Tipo**: SQL Directo (Query)
*   **Consulta**:
    ```sql
    SELECT DISTINCT p.rut, p.nombres as nombres 
    FROM softland.sw_personal p 
    INNER JOIN softland.sw_estudiosup e ON e.codEstudios like p.codEstudios 
    WHERE p.rut like '{0}'
    ```
*   **Propósito**: 
    *   Validar la existencia de un trabajador en la base de datos de la Consultora.
    *   Recuperar su RUT y Nombre oficial registrado en el ERP.
*   **Uso de Datos**:
    *   Los datos retornados (`rut`, `nombres`) se utilizan para poblar formularios de solicitud de baja o para validar que el RUT ingresado corresponde a un empleado activo/existente en esta empresa específica.
*   **Observaciones**:
    *   Utiliza `LIKE` para el RUT, lo que permite búsquedas flexibles pero podría tener problemas de rendimiento o precisión.
    *   El `INNER JOIN` con `sw_estudiosup` actúa como un filtro implícito: solo retorna trabajadores que tengan un código de estudios válido cruzado con la tabla de estudios.

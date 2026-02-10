# Módulo Operaciones - Cargo - Teamwork.WebApi

**Archivo**: `Teamwork.WebApi/Operaciones/Cargo.cs`

Gestiona las solicitudes de cambio de cargo, estructuras de sueldos, y lógica de remuneraciones.

## Gestión de Solicitudes (`operaciones/CargoMod/`)

- **`__CrearSolicitud`**: Crea una nueva solicitud de modificación de cargo.
    - **Entrada**: `Empresa`, `UsuarioCreador`, `Type` (Evento).
- **`__DeshacerSolicitud`**: Revierte una solicitud.
- **`__Solicitudes`**: Lista solicitudes existentes con filtros.
- **`__Dashboard`**: Resumen de estado de solicitudes.

## Estructuras de Remuneración

Métodos para obtener la estructura de haberes y descuentos asociada a una solicitud.
- **`__EstructuraHeader`**: Cabecera de la liquidación simulada.
- **`__EstructuraHaberes`**: Lista de haberes.
- **`__EstructuraDescuentos`**: Lista de descuentos.
- **`__EstructuraMargenProvision`**: Cálculo de márgenes.

## Modificaciones de Datos del Cargo

Métodos para actualizar campos específicos dentro de una solicitud de cargo.
- **`__ActualizaNombreCargo`**: Cambia el nombre del cargo.
- **`__ActualizaSueldo`**: Modifica el sueldo base o bruto.
    - `TypeSueldo`: Probablemente 'Bruto' o 'Liquido'.
- **`__ActualizaGratificacion`**: Configura la gratificación (Legal, Convenida, etc.).
- **`__CambiarInputSueldo`**: Cambia el valor de entrada del sueldo.
- **`__CambiarCalculoTypeSueldo`**: Cambia la forma de cálculo (e.g., de líquido a bruto).
- **`__CambiarTypeJornada`**: Cambia el tipo de jornada laboral.

## Clientes y AFP

- **`__Clientes`**, **`__PaginationClientes`**: Gestión de clientes asociados a la solicitud.
- **`__ActualizaCliente`**: Asocia un cliente a la solicitud.
- **`__AFP`**: Obtiene lista de AFPs.
- **`__ActualizarAFP`**: Cambia la AFP en la solicitud.

**Observaciones**:
Este módulo es central para la lógica de "CargoMod", que parece ser un flujo de aprobación y modificación de condiciones contractuales.

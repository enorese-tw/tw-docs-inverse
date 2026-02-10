# Módulo Operaciones - Complementos - Teamwork.WebApi

Documentación de los submódulos de operaciones que complementan la gestión de cargos y remuneraciones.

## 1. ANIs (Aportes No Imponibles?) (`Operaciones/ANIs.cs`)

Gestión de montos adicionales no imponibles asociados a una modificación de cargo.

- **`__ANIsCargoMod`**: Lista ANIs por código de modificación de cargo.
- **`__AgregarANI`**: Agrega un nuevo monto.
    - **Params**: `UsuarioCreador`, `CodigoANI`, `Monto`, `Deft`, `Observaciones`.
- **`__ActualizarANI`**: Modifica un monto existente.
- **`__EliminarANI`**: Elimina un registro.

## 2. Bonos (`Operaciones/Bonos.cs`)

Gestión de Bonos asociados a clientes y cargos.

- **`__Bonos`**: Listado general de bonos.
- **`__BonosCargoMod`**: Bonos asociados a una solicitud de cambio de cargo.
- **`__AgregarBono`**: Asocia un bono existente a una solicitud.
    - **Params**: `CodigoBono`, `CodigoCargoMod`, `Monto`, `Condiciones`.
- **`__AgregarNuevoBono`**: Crea un bono *ad-hoc* para la solicitud.
- **`__ValidateAsignBonos`**: Valida si un bono puede ser asignado.

## 3. Provisiones y Margen (`Operaciones/ProvMargen.cs`)

Gestión de conceptos de provisión y margen (costos empresa).

- **`__ProvMargenCrear`**, **`__ProvMargenActualizar`**, **`__ProvMargenEliminar`**: CRUD de conceptos de provisión.
- **`__ProvMargenLista`**: Listado de conceptos.
- **`__ProvMargenCrearAsig`**: Asigna un concepto a un cliente/empresa.
    - **Params**: `Monto`, `TypeInput` (tipo de cálculo), `CodigoConcepto`.
- **Constantes de Cálculo**:
    - `__ProvMargenListaConstCalcProv`: Lista constantes para el cálculo.
    - `__ProvMargenCrearAsignConstCalculo`: Asigna una constante de cálculo.

## 4. Valor Diario (`Operaciones/ValorDiario.cs`)

Gestión de valores diarios para cálculos de finanzas (probablemente para facturación o costos por día).

- **`__ValorDiarioCrear`**: Crea un valor diario para un cargo/cliente.
    - **Params**: `Empresa`, `Cliente`, `CargoMod`, `Monto`.
- **`__ValorDiarioActualizar`**, **`__ValorDiarioEliminar`**.
- **`__ValorDiarioListar`**.

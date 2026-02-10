# Integraciones Externas y Utilidades - Teamwork.WebApi

Documentación de conectores con servicios externos o utilidades específicas.

## 1. KAM (`CallAPIKam.cs`)

Consulta información gestionada por los KAMs (Key Account Managers?), probablemente relacionada con el estado contractual de los empleados en otras plataformas.

- **`__ConsultaFichaPersonal`**: Consulta ficha por RUT.
- **`__ConsultaContrato`**: Consulta contratos activos.
- **`__ConsultaRenovacion`**, **`__ConsultaBaja`**.

## 2. Softland (`CallAPISoftland.cs`)

Integración con ERP Softland.

- **`__SoftlandCargoModCrear`**: Crea una modificación de cargo en Softland.
    - **Ruta**: `softland/CargoMod/Crear`
    - **Params**: `Codigo`, `Glosa`, `Empresa`.

## 3. Reportes Excel (`CallAPIExcel.cs`)

Generación de reportes en formato Excel.

- **`__ReporteCargoMod`**: Reporte de modificaciones de cargo.
- **`__ReporteFiniquitos`**: Reporte de finiquitos.
    - **Ruta**: `excel/Reporte/Finiquitos`.
    - **Params**: `Excel` (Nombre plantilla?), `Data` (Datos JSON serializados).

## 4. Generación PDF (`Teanwork/PDF.cs`)

Generación de documentos PDF.

- **`__RemuneracionesEstructuraCargoMod`**: Genera un PDF con la estructura de remuneraciones de una modificación de cargo.
    - **Ruta**: `pdf/Remuneraciones/EstructuraCargoMod`.
    - **Params**: `Data` (Datos estructura), `UsuarioCreador`.

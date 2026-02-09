# Business Logic Analysis

This document details the business logic, data flow, and database dependencies for specific modules: **Finanzas**, **Finiquitos**, and **Cargo Mod**.

## 1. Finanzas Module

### Overview
The Finanzas module appears to focus on managing monetary values and configurations, specifically "Valor Diario" (Daily Value), likely for billing or payroll calculations relative to specific clients or job roles.

### Components
*   **Controller**: `Teamwork.Api.Controllers.FinanzasController`
*   **Repository**: `Teamwork.Repository.Repository.FinanzasRepository`
*   **Model**: `Teamwork.Model.Data.DataPackageValorDiario`

### Logic & Data Flow
The controller exposes a CRUD interface for "Valor Diario".

| Endpoint | Action | Stored Procedure | Parameters Key |
| :--- | :--- | :--- | :--- |
| `ValorDiario/Crear` | Create new value | `finanzas.package_FZ_ValorDiario` | `@ACCION='Crear'`, Client, CargoMod, Empresa, Monto |
| `ValorDiario/Listar` | List values | `finanzas.package_FZ_ValorDiario` | `@ACCION='Listar'`, Pagination, Filters |
| `ValorDiario/Actualizar` | Update value | `finanzas.package_FZ_ValorDiario` | `@ACCION='Actualizar'`, Client, CargoMod, Empresa, Monto |
| `ValorDiario/Eliminar` | Delete value | `finanzas.package_FZ_ValorDiario` | `@ACCION='Eliminar'`, CargoMod, Empresa |

### Dependencies
*   **Database**: Connects to `DatabaseOperaciones` (defined in Web.config).

---

## 2. Finiquitos Module

### Overview
This is a massive module handling the end-to-end lifecycle of employee termination settlements ("Finiquitos"). It covers calculation, validation, workflow management (sending to notaries, signing), and payment processing.

### Components
*   **Controller**: `Teamwork.Api.Controllers.FiniquitosController`
*   **Repository**: Uses `CallExecutionAPI` (Generic Repository Wrapper).
*   **Model**: `Teamwork.Model.Data.Finiquitos.DataPackageFiniquitos`

### Core Business Logic
The logic is centralized in a single "God Procedure": `package.__Finiquitos`. The Controller methods map specific actions to this procedure using an "OpCode" or "Action" parameter.

#### Key Workflows
1.  **Calculation & Simulation**:
    *   `ConsultaSimulaciones`, `ConsultaFiniquitos`, `ConsultaComplementos` (Additional payments/discounts).
2.  **Workflow State Management**:
    *   **Validation**: `ValidarFiniquito`, `RevertirValidacion`.
    *   **Logistics (Notary/Region)**: `GestionEnvioRegiones`, `GestionEnvioSantiagoNotaria`, `GestionRecepcion...`.
    *   **Legal**: `GestionEnvioLegalizacion`, `TerminarFiniquito`, `FirmarFiniquito`.
3.  **Financials & Payment**:
    *   **Validation**: `ValidarFinanzas` (Approval from Finance dept).
    *   **Payment Methods**: `SolicitudTEF` (Transfer), `SolicitudValeVista`, `SolicitudCheque`.
    *   **Execution**: `ConfirmarProcesoPago`, `PagarFiniquito`.

### Stored Procedure Mapping
Almost every endpoint calls `package.__Finiquitos`. The differentiation happens via the first parameter value (e.g., `'ConsultaFiniquitos'`, `'AnularFiniquito'`, `'SolicitudTEF'`).

---

## 3. Cargo Mod Module

### Overview
"Cargo Mod" (Modification of Cargo/Position) is a complex system for requesting, approving, and configuring job positions ("Cargos"). It seems to be a bridge between operational requests and the ERP (Softland) or Payroll system. It handles salary definitions, bonuses, and margins.

### Components
This feature is split across two controllers:

#### A. OperacionesController (`operaciones/CargoMod/...`)
Handles the business workflow of requesting and configuring a position.
*   **Repository**: `OperacionesRepository`
*   **Primary SP**: `remuneraciones.package_RM_CargoMod`
*   **Helper SPs**:
    *   `remuneraciones.package_RM_Bonos` (Bonuses)
    *   `remuneraciones.package_RM_ANI` (Additional Non-Imposable items?)
    *   `remuneraciones.package_RM_ProvMargen` (Provisions & Margins configuration)

**Key Capabilities:**
*   **Request Lifecycle**: `CrearSolicitud`, `DeshacerSolicitud`, `CambioEstadoSolicitud`, `ValidaStageActual`.
*   **Configuration**:
    *   **Salary**: `ActualizaSueldo`, `CambiarInputSueldo`, `CambiarCalculoTypeSueldo`.
    *   **Structure**: `ActualizaNombreCargo`, `ActualizaHorario`, `ActualizaJornadaFullTime`.
    *   **Financials**: `ActualizaValorDiaPT` (Price/Day Pass Through?), `ActualizaGratificacion`.
*   **Sub-Modules**:
    *   **Bonos**: endpoints `Bonos/...` manage attaching bonuses to the Cargo Mod request.
    *   **ANI**: endpoints `ANI/...` manage other income items.
    *   **Provisions/Margins**: endpoints `ProvMargen/...` manage cost calculations and margins.

#### B. SoftlandController (`softland/CargoMod/...`)
Handles the direct interaction with the "Softland" ERP system or its mirror tables.
*   **Repository**: `SoftlandRepository`
*   **SP**: `dbo.package_TW_CargoMod`
*   **Actions**:
    *   `Crear`, `Actualizar` (Code/Glossary), `Eliminar`.
    *   Validations: `ValidaExistenciaCodine`, `ValidaExistenciaGlosa`.

### Business Insight
"Cargo Mod" likely acts as a "Change Request" system. Operations staff request a new job role with specific financial parameters (Salary, Bonos, Margin). Once approved/processed through the workflow (Stages/Wizards), it likely gets synchronized to Softland via the `SoftlandController` or a backend process.

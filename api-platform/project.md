# Project Analysis: TeamWork API

## 1. Project Overview
**Name**: ApiTeamwork
**Type**: ASP.NET Web API 2 Application
**Framework**: .NET Framework 4.5
**Solution Structure**:
- `Teamwork.Api`: The main Web API entry point.
- `Teamwork.Model`: Data transfer objects (DTOs) and models.
- `Teamwork.Repository`: Data access logic.

## 2. Architecture
The project follows a standard 3-layer architecture, but with a legacy implementations style specific to "Procedure-Driven" design.

```mermaid
graph TD
    Client[Client App / Frontend] -->|HTTP JSON| API[Teamwork.Api (Controllers)]
    API -->|Calls| Repo[Teamwork.Repository]
    Repo -->|ADO.NET / SQLClient| DB[(SQL Server)]
    
    subgraph "Data Flow"
    API -- "Constructs Params" --> Repo
    Repo -- "Executes SP" --> DB
    DB -- "Returns DataTable" --> Repo
    Repo -- "Returns DataTable" --> API
    API -- "Serializes DataTable" --> Client
    end
```

### Key Characteristics
- **No ORM**: The project does not use Entity Framework or Dapper. It uses raw ADO.NET (`SqlConnection`, `SqlDataAdapter`, `DataTable`).
- **Stored Procedure Centric**: All business logic and data retrieval seem to be encapsulated in Stored Procedures (e.g., `SP_CRUD_TW_MANTENEDORNOTICIASTEAMWORK`).
- **Loose Typing**: Most models (`DataPackage...`) and controller parameters utilize `string` for almost all properties, relying on parsing or passing through to SQL.

## 3. Technology Stack
- **Language**: C#
- **Web Framework**: ASP.NET Web API 2 (`System.Web.Http`)
- **JSON Serialization**: Newtonsoft.Json (v11.0.1)
- **Authentication**: JWT (JSON Web Tokens) with custom handlers (`TokenValidationHandler`).
- **Data Access**: System.Data.SqlClient (ADO.NET)

## 4. Component Analysis

### A. Teamwork.Api (Presentation Layer)
- **Controllers**: Located in `Controllers/`. They inherit from `ApiController`.
  - **Routing**: Uses Attribute Routing (`[RoutePrefix]`, `[Route]`).
  - **Pattern**: Controllers act as thin wrappers. They map HTTP requests to Stored Procedure parameters.
  - **Example (`TeamworkController`)**:
    - Constructs arrays of parameter names (`@ACCION`, etc.) and values.
    - Calls `teamworkRepository.ExecuteStoreProcedure`.
    - Returns `JsonResult<DataTable>`.

- **Configuration**:
  - `Global.asax`: Registers Areas, Config, Filters, Routes, Bundles.
  - `App_Start/WebApiConfig.cs`: Configures custom message handlers (`TokenValidationHandler`, `WebApiCustomMessageHandler`) and attribute routing.

### B. Teamwork.Repository (Data Access Layer)
- **Purpose**: Execute Stored Procedures.
- **Key Class: `TeamworkRepository`**:
  - Manages SQL Connections.
  - Connection strings are read from `Web.config`, decoded from Base64.
  - **Method `ExecuteStoreProcedure`**:
    - Takes an SP name and a list of parameters.
    - Returns a raw `DataTable`.
    - No strong typing or object mapping is performed here; the raw SQL result is passed back.

### C. Teamwork.Model (Data Models)
- **DataPackages**: Classes like `DataPackagePostulante` in `Data/`.
- **Structure**: These are POCOs with extensive lists of `string` properties.
- **Usage**: Likely used to bind incoming JSON from the frontend before converting to SP parameters, or as DTOs (though the Controllers seem to return `DataTable` directly).

## 5. Security & Configuration
- **Authentication**: `TokenValidationHandler` implies a custom JWT or token-based auth mechanism.
- **Connection Strings**: Stored in `Web.config` (Keys: `HostDatabase`, `database`, `UsernameDatabase`, `PasswordDatabase`) and appear to be Base64 encoded for obfuscation.
- **CORS/Headers**: `WebApiCustomMessageHandler` likely handles CORS or adding default headers.

## 6. Observations & Technical Debt
1.  **Framework Age**: .NET 4.5 is quite old (EOL).
2.  **"Stringly Typed"**: Heavy reliance on strings for all data types reduces type safety and compile-time checking.
3.  **God-Object Patterns**: SP names like `SP_CRUD_TW_MANTENEDORNOTICIASTEAMWORK` suggest single massive Stored Procedures handling multiple operations ("VIEW", "INSERT", "UPDATE") based on an `@ACCION` parameter.
4.  **Direct DataTable Serialization**: Returning `DataTable` directly to the client exposes the DB schema structure and is generally considered a bad practice compared to returning typed DTOs.
5.  **Hardcoded Parameters**: Controllers contain manually mapped arrays of strings for SP parameters, which is brittle and hard to maintain.

## 7. Relevant Skills
The skill `legacy-dotnet-business-logic-mapper` is highly relevant here.
- **Phase 1 (Mapeo)**: Applicable to mapping these Controllers -> SP calls.
- **Phase 2 (Logic Extraction)**: Logic is likely in the SPs and the "Manager" SPs behavior.
- **Phase 3 (Inventory)**: Mapping the `Get()` methods to `SP_CRUD_...` is exactly what the skill prescribes.

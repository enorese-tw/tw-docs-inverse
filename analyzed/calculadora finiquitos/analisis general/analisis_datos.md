# Análisis de Datos y Cálculos

## Lógica de Negocio y Cálculos
La lógica de cálculo de finiquitos se encuentra dispersa principalmente en los archivos "Code Behind" (`.aspx.cs`) de las páginas de cálculo de baja.

### Archivos Principales
- `CalculoBajaConsultora.aspx.cs`
- `CalculoBajaEst.aspx.cs`
- `CalculoBajaOut.aspx.cs`

### 1. Cálculo de Vacaciones (`Vacaciones` method)
Se identificó un método `Vacaciones` que realiza el cálculo de días proporcionales, inhábiles y saldo de vacaciones.
- **Inputs**: Fechas de inicio y fin, días trabajados.
- **Lógica**:
    - Calcula diferencias de fechas (años, meses, días).
    - Aplica factores de corrección por días hábiles/inhábiles.
    - Considera lógica específica para años bisiestos o meses de 28/29/30/31 días.
    - Consulta días de vacaciones ya tomados (`Clases.Vacaciones.ObtenerVacaciones`).
    - Calcula el saldo final y el monto a pagar basándose en la remuneración.

### 2. Acceso a Datos (DAL)
El acceso a datos no está centralizado y presenta prácticas legado.

#### `Clases.Contrato`
Se utiliza para gestionar la información del contrato del trabajador.
- **Métodos observados**:
    - `validarPersonaExistente`: Verifica si el trabajador existe en la DB.
    - `ContratoActivobaja`: Verifica estado del contrato.
    - `ListarFiniquitadosCONSULTORA`: Obtiene lista de contratos finiquitados.
- **Problema**: Recibe la cadena de conexión completa (con credenciales) como parámetro en cada llamada.

#### `Clases.MethodServiceSoftland`
Wrapper para el servicio web `ServicioFiniquitos`. Retorna `DataSet` directamente, lo que acopla la capa de presentación a la estructura de la base de datos o del servicio.

### 3. Puntos de Atención
- **Duplicación de Código**: La lógica de cálculo parece repetirse con ligeras variaciones entre `CalculoBajaConsultora`, `CalculoBajaEst`, y `CalculoBajaOut`.
- **Mantenibilidad**: La lógica de negocio compleja mezclada con la lógica de UI y acceso a datos dificulta el mantenimiento y las pruebas unitarias.
- **Hardcoding**: Valores mágicos y factores (e.g., `1.667` para feriado proporcional) están dispersos en el código.

## Estructura de Clases Detectada
El namespace `FiniquitosV2.Clases` contiene las entidades de negocio:
- `Contrato`
- `Cargo`
- `AreaNegocio`
- `Vacaciones`
- `FNDocumentos`
- `OtrosHaberes`
- `Descuentos`

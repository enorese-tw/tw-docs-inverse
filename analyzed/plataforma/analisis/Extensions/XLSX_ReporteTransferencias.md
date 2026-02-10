# Análisis de Reporte: XLSX_ReporteTransferencias.cs

## 1. Propósito
Genera los archivos Excel necesarios para realizar transferencias bancarias masivas (TEF). Este componente es crítico debido a que maneja montos de dinero y debe cumplir con restricciones bancarias específicas de formato y montos máximos.

## 2. Lógica de Negocio y Seguridad

### **Límites de Monto por Archivo**
El sistema implementa una lógica de división de archivos basada en límites de montos totales:
*   **Límite de Cuenta**: Existe un límite de **10,000,000** (diez millones) por archivo. Si la suma de las transferencias supera este monto, el sistema divide automáticamente los datos en múltiples archivos Excel.
*   **Tope por Transacción**: Existe un límite individual de **7,000,000** (siete millones). Si una sola transferencia (un registro) supera este monto, la lógica la divide en múltiples filas dentro del mismo Excel para no exceder el tope permitido por el banco.

### **Manejo de Cuentas Específicas**
El código tiene lógica "hardcoded" para ciertas cuentas de origen:
*   Cuentas monitoreadas: `87274144`, `87274039`.
*   Regla de cambio: Si la cuenta es `2504898`, se cambia automáticamente a `87274039`.

### **Seguridad (Protección de Archivo)**
Todos los archivos generados están protegidos para evitar modificaciones accidentales:
*   **Password**: `domCLt3am.W0rk` (Se aplica protección a la `Hoja1`).

---

## 3. Estructura del Excel
El archivo siempre contiene una única hoja denominada **Hoja1**.

| Columna | Nombre de Columna | Descripción |
| :--- | :--- | :--- |
| A | Cta_origen | Número de cuenta desde donde sale el dinero. |
| B | moneda_origen | Moneda (ej. CLP). |
| C | Cta_destino | Cuenta del beneficiario. |
| D | moneda_destino | Moneda de destino. |
| E | Cod_banco | Código bancario de destino. |
| F | RUT benef. | RUT del trabajador/proveedor. |
| G | nombre benef. | Nombre completo del beneficiario. |
| H | Mto Total | Monto a transferir (ajustado al tope de 7M). |
| I | Glosa TEF | Descripción de la transferencia. |
| J | Correo | Email para notificación de pago. |
| K | Glosa correo | Mensaje personalizado en el email. |
| L | Glosa Cartola Cliente | Texto que aparecerá en la cartola del emisor. |
| M | Glosa Cartola Beneficiario | Texto que aparecerá en la cartola del receptor. |

---

## 4. Origen de Datos (Input)
El método `xlsx` recibe un `DataRowCollection`, lo que indica que los datos son pre-procesados en la capa de negocio antes de enviarlos a la extensión.

---

## 5. Salida (Output)
El método retorna un objeto de clase `Files`, el cual contiene:
*   `List<byte[]> ListFile`: Una lista de arreglos de bytes (uno por cada archivo Excel generado según los límites).
*   `Empresa`: Nombre de la empresa emisora.
*   `Fecha`: Fecha de la transacción.

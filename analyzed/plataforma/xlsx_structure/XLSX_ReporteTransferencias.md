# XLSX_ReporteTransferencias (TEF)

Este componente genera los archivos de transferencia bancaria electrónica. Tiene una lógica de seguridad y de límites de montos por cuenta sumamente estricta.

## Propósito
Generar archivos compatibles con portales bancarios para el pago masivo de haberes o finiquitos, asegurando que no se superen los límites de las cuentas origen.

## Llamadas a Servicios / APIs
No utiliza APIs externas directamente; procesa un `DataRowCollection` que ya contiene la información de las transferencias.

## Estructura de Salida (Hoja: Hoja1)

| Columna | Nombre (Header) | Descripción |
| :--- | :--- | :--- |
| A | Cta_origen | Número de cuenta de donde sale el dinero. |
| B | moneda_origen | Tipo de moneda (ej: CLP). |
| C | Cta_destino | Cuenta bancaria del beneficiario. |
| D | moneda_destino | Moneda destino. |
| E | Cod_banco | Código bancario de la cuenta destino. |
| F | RUT benef. | RUT del destinatario. |
| G | nombre benef. | Nombre completo del destinatario. |
| H | Mto Total | Monto de la transacción individual. |
| I | Glosa TEF | Descripción corta de la transferencia. |
| J | Correo | Email para notificación. |
| K | Glosa correo | Mensaje contenido en el email. |
| L | Glosa Cartola Cliente | Descripción que aparecerá en la cartola del pagador. |
| M | Glosa Cartola Beneficiario | Descripción que aparecerá en la cartola del beneficiario. |

## Lógica Especial y Seguridad
- **Protección**: La hoja de cálculo está protegida con la contraseña: `domCLt3am.W0rk`.
- **Límite por Archivo**: Existe un límite de **10.000.000** por lote de transferencias para ciertas cuentas (`87274144`, `87274039`). Si el total agrupado supera este monto, se generan múltiples archivos Excel.
- **División de Registros (Tope)**: Cada registro individual no puede superar los **7.000.000**. Si una transferencia a una persona es mayor a este tope, el registro se "troza" en múltiples líneas de 7M hasta completar el total.
- **Mapeo de Cuentas**: Tiene lógica hardcoded para redirigir fondos de una cuenta origen específica (`2504898`) hacia otra (`87274039`).

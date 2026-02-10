# Correo.cs

## Descripción General
Clase para el **envío de correos electrónicos** mediante SMTP. Permite enviar mensajes con copia (CC) usando credenciales de Gmail.

## Namespace
`FiniquitosV2.Clases`

## Campos de Instancia

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `m` | `MailMessage` | Objeto de mensaje de correo |
| `smtp` | `SmtpClient` | Cliente SMTP para envío |

## Métodos

### `bool enviar(string de, string clave, string para, string mensaje, string asunto, string cc)`
Envía un correo electrónico.

- **Parámetros**:
  - `de` — Dirección del remitente.
  - `clave` — Contraseña del remitente para autenticación SMTP.
  - `para` — Dirección del destinatario.
  - `mensaje` — Cuerpo del correo en formato HTML.
  - `asunto` — Asunto del correo.
  - `cc` — Dirección de copia (CC).
- **Retorno**: `true` si el envío fue exitoso, `false` si hubo error.

#### Configuración SMTP
| Parámetro | Valor |
|-----------|-------|
| Servidor | `smtp.gmail.com` |
| Puerto | `587` |
| SSL | `true` |
| Formato body | `IsBodyHtml = true` |

## Flujo de Envío
1. Configura remitente (`From`) y destinatario (`To`).
2. Agrega dirección de copia (`CC`).
3. Establece asunto y cuerpo HTML.
4. Configura credenciales SMTP con el email y contraseña proporcionados.
5. Habilita SSL y envía por puerto 587.

## Dependencias
- `System.Net.Mail` — Clases de correo .NET.
- `System.Net` — `NetworkCredential` para autenticación.

## Observaciones
- ⚠️ **Seguridad**: La contraseña del correo se pasa como parámetro en texto plano.
- ⚠️ Usa `smtp.gmail.com` específicamente — requiere que la cuenta Gmail tenga habilitado "acceso de aplicaciones menos seguras" o use una contraseña de aplicación.
- El correo siempre se envía como HTML (`IsBodyHtml = true`).
- No hay logging de errores; solo retorna `false` en caso de excepción.

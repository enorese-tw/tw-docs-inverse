# Utilidades.cs

## Descripción General
Clase utilitaria con funciones transversales al sistema: **criptografía**, **validación de RUT**, **logging**, **cálculo de días hábiles** y **manejo de feriados**. Es una clase estática de soporte utilizada por múltiples otras clases.

## Namespace
`FiniquitosV2.Clases`

---

## Sección 1: Criptografía (Rijndael/AES)

### `static string Encriptar(string _cadenaAencriptar)`
Encripta una cadena usando Rijndael (AES-256).

- **Clave**: Derivada de `"qualaboremsequitur"` usando `PasswordDeriveBytes` (RFC2898).
- **IV**: `{ 0x49, 0x76, 0x61, 0x6e, ... }` ("Ivan Medvedev" en ASCII).
- **Salt**: `{ 0x49, 0x76, 0x61, 0x6e, ... }` (mismo que IV).
- **Retorno**: Cadena encriptada en Base64.

### `static string Desencriptar(string _cadenaAdesencriptar)`
Desencripta una cadena encriptada con `Encriptar()`.

- Usa los mismos parámetros criptográficos.
- **Retorno**: Cadena original desencriptada.

> ⚠️ **Seguridad**: La clave, IV y salt están hardcodeados en el código fuente.

---

## Sección 2: Validación de RUT

### `static bool validarRUT(string rut)`
Valida un RUT chileno completo (con dígito verificador).

- Algoritmo: Módulo 11 estándar.
- Soporta dígito verificador `'K'` y `'0'`.
- **Retorno**: `true` si el RUT es válido.

---

## Sección 3: Logging

### `static void LogError(string proceso, string cadenaConexion, string mensaje)`
Registra un error en la base de datos.

```sql
INSERT INTO FNERROR (PROCESO, MENSAJE, FECHA)
VALUES ('{proceso}', '{mensaje}', GETDATE())
```

### `static void creaArchivo(string path, string content)`
Crea un archivo de texto con el contenido proporcionado.

### `static void Log(string text)` / `static void DumpLog()`
- `Log()`: Acumula mensajes en un `StringBuilder` estático.
- `DumpLog()`: Escribe todo el log acumulado a archivo y limpia el buffer.
- **Path hardcodeado**: `c:/david/log.txt`.

> ⚠️ `Log()` y `DumpLog()` usan path hardcodeado `c:/david/log.txt`.

---

## Sección 4: Cálculo de Días Hábiles

### `static double diashabiles(DateTime inicio, DateTime fin)`
Calcula los días hábiles entre dos fechas considerando feriados chilenos.

**Feriados considerados**:
| Fecha | Descripción |
|-------|-------------|
| 1 Enero | Año Nuevo |
| Viernes Santo | Calculado dinámicamente |
| Sábado Santo | Calculado dinámicamente |
| 1 Mayo | Día del Trabajo |
| 21 Mayo | Glorias Navales |
| 29 Junio | San Pedro y San Pablo |
| 16 Julio | Virgen del Carmen |
| 15 Agosto | Asunción de la Virgen |
| 18 Sept | Fiestas Patrias |
| 19 Sept | Glorias del Ejército |
| 12 Oct | Encuentro de Dos Mundos |
| 31 Oct | Día de las Iglesias Evangélicas |
| 1 Nov | Todos los Santos |
| 8 Dic | Inmaculada Concepción |
| 25 Dic | Navidad |

- Excluye sábados y domingos.
- Calcula Viernes y Sábado Santo dinámicamente vía el algoritmo de la fecha de Pascua.
- Gran cantidad de código comentado con versiones anteriores del algoritmo.

### `static double TotaldiasVacaciones(DateTime inicio, DateTime fin, int vacacionestomadas)`
Calcula el total de días de vacaciones pendientes.

- `diasTotales = diashabiles(inicio, fin)`.
- `diasprogresivos = diasTotales * 1.25 / 30` (proporción de vacaciones progresivas).
- `resultado = diasprogresivos - vacacionestomadas`.

---

## Sección 5: Cálculo de Pascua

### `private static DateTime PascuaAlgoritmo(int anio)`
Calcula la fecha de Domingo de Pascua usando un algoritmo estándar (Anonymous Gregorian algorithm).

- Se usa internamente para derivar los Viernes y Sábado Santo.

---

## Dependencias
- `System.Security.Cryptography` — Rijndael.
- `System.IO` — Archivos de log.
- `Interface` — Ejecución SQL para logging de errores.

## Observaciones Críticas
- ⚠️ **Clave criptográfica hardcodeada** en código fuente.
- ⚠️ **Path de log hardcodeado**: `c:/david/log.txt` — parece ser de la máquina del desarrollador original.
- ⚠️ **SQL Injection** en `LogError()`: parámetros concatenados directamente.
- ⚠️ **Código comentado extenso** en `diashabiles()` — múltiples versiones del algoritmo sin limpiar.
- ⚠️ `Log()`/`DumpLog()` usan un `StringBuilder` estático — condiciones de carrera.
- Es la clase más referenciada del sistema (prácticamente todas las demás dependen de ella para logging).

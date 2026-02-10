# Módulo Selección y Personas - Teamwork.WebApi

**Archivos**: `CallAPISeleccion.cs`, `CallAPIPersona.cs`, `CallAPIToken.cs`

## Selección (`CallAPISeleccion.cs`)

Gestiona el proceso de reclutamiento y selección de postulantes.

### Gestión de Postulantes (`seleccion/Postulante/`)

- **`__PostulanteValidateTokenInvitacion`**: Valida el token de invitación enviado a un candidato.
- **`__PostulanteCreaOActualizaFichaPostulante`**: Crea o actualiza la ficha completa del postulante.
    - **Datos**: `Nombres`, `Apellidos`, `Telefono`, `Direccion`, `DNI` (RUT), `Token`.
- **`__PostulanteConsultaFichaPersonal`**: Obtiene datos del postulante por DNI.
- **`__PostulanteConsultaFieldEncuesta`**: Obtiene campos de encuesta asociados al token.

### Ofertas Laborales y Tags

- **`__ListarOfertasLaborales`**: Lista ofertas disponibles con filtros.
- **`__PostulanteActualizarOfertaLaboral`**: Modifica una oferta (fechas, descripción, target).
- **`__CrearTagOfertaLaboral`**, **`__EliminarTagOfertaLaboral`**: Gestiona etiquetas (tags) para categorizar ofertas o postulantes.

### Autenticación OAuth

- **`__CodigoOAuthCrear`**: Crea un código de autorización OAuth (probablemente para integraciones con portales de empleo externos).

## Gestión de Personas/Clientes (`CallAPIPersona.cs`)

Operaciones sobre entidades "Cliente" o "Persona" en el sistema.

- **`__PersonaConsultarCliente`**: Busca clientes.
- **`__PersonaCrearCliente`**, **`__PersonaEliminarCliente`**.

## Tokens de Confianza (`CallAPIToken.cs`)

- **`__CrearTokenConfianza`**: Genera un token de confianza para un usuario, permitiendo operaciones privilegiadas o integraciones sin re-autenticación completa.

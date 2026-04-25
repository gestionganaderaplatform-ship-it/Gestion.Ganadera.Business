# 05. Validacion y manejo de errores

## Como valida este template

La validacion usa `FluentValidation`.

Eso permite:

- separar reglas del controller
- reutilizar reglas
- probar validadores de forma aislada

## Por que se desactiva la respuesta automatica de ModelState

En `Program.cs` se desactiva el filtro automatico de `[ApiController]`.

Motivo:

- controlar el formato de error
- unificarlo con `ProblemDetails`
- usar `FluentValidation` como estrategia principal

## Que es ProblemDetails

Es un formato estandar para respuestas de error HTTP.

En este template se usa en:

- `Gestion.Ganadera.Business.API/ErrorHandling/ApiProblemDetailsFactory.cs`
- `Gestion.Ganadera.Business.API/Extensions/ProblemDetailsExtensions.cs`
- `Gestion.Ganadera.Business.API/Middleware/ErrorHandlerMiddleware.cs`

## Dos tipos de error

### Error controlado

Ejemplo:

- validacion fallida
- no encontrado
- no autenticado

### Error inesperado

Ejemplo:

- excepcion no controlada
- fallo tecnico no previsto

## Soporte adicional con EF Core metadata

El template incluye:

- `IEntityValidationMetadata`
- `EfEntityValidationMetadata`

Eso ayuda a reutilizar reglas estructurales como longitud maxima y campos requeridos.

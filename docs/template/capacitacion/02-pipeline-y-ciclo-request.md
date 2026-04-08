# 02. Pipeline y ciclo de un request

## Que es el pipeline HTTP

En ASP.NET Core, cada request pasa por una cadena de componentes llamados middleware.

Cada middleware puede:

- leer el request
- modificar el request
- agregar datos al contexto
- cortar el flujo
- dejar seguir al siguiente middleware
- modificar la respuesta

Por eso el orden importa.

## Orden real del pipeline en este template

Puedes verlo en:

- `Gestion.Ganadera.API/Program.cs`
- `Gestion.Ganadera.API/Extensions/ApiMiddlewareExtensions.cs`

Orden general:

1. `UseApiMiddlewares()`
2. `UseRateLimiting()` si `RateLimiting:Global:Enabled` esta en `true`
3. `UseRequestMetrics()`
4. `MapControllers()`

Y dentro de `UseApiMiddlewares()`:

1. Swagger y herramientas de desarrollo
2. HTTPS
3. CORS
4. `CorrelationIdMiddleware`
5. autenticacion
6. autorizacion
7. `SecurityHeadersMiddleware`
8. `ErrorHandlerMiddleware`
9. health checks

## Por que ese orden tiene sentido

- el correlation id debe existir temprano
- auth debe ejecutarse antes de llegar a controllers
- el error handler debe envolver buena parte del flujo
- el rate limiting solo se agrega cuando la configuracion global lo habilita
- las metricas deben medir el request casi completo

## Donde se validan cosas

En este template la validacion se distribuye asi:

- `FluentValidation` valida modelos y filtros
- `BaseController` decide como responder cuando algo es invalido
- `ProblemDetails` unifica errores

## Archivos utiles

- `Gestion.Ganadera.API/Program.cs`
- `Gestion.Ganadera.API/Extensions/ApiMiddlewareExtensions.cs`
- `Gestion.Ganadera.API/Middleware/CorrelationIdMiddleware.cs`
- `Gestion.Ganadera.API/Middleware/ErrorHandlerMiddleware.cs`
- `Gestion.Ganadera.API/Middleware/MetricasMiddleware.cs`

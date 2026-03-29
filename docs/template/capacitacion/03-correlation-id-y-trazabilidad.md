# 03. Correlation id y trazabilidad

## Que es un correlation id

Es un identificador unico que acompana una solicitud de principio a fin.

Sirve para contestar una pregunta muy practica:

> Como encuentro todos los rastros del mismo request en logs, errores y metricas.

## Para que sirve

- relacionar logs de una misma solicitud
- relacionar errores con el request que los produjo
- relacionar metricas con el request
- diagnosticar incidentes mas rapido

## Como funciona en este template

La implementacion esta en:

- `Gestion.Ganadera.API/Middleware/CorrelationIdMiddleware.cs`

Ese middleware:

1. revisa si el cliente ya envio `X-Correlation-Id`
2. si no existe, genera uno nuevo
3. lo guarda en `HttpContext.Items`
4. lo agrega en la respuesta HTTP
5. lo empuja al contexto de logs

## Donde se reutiliza en este proyecto

- `Gestion.Ganadera.API/Extensions/ProblemDetailsExtensions.cs`
- `Gestion.Ganadera.API/ErrorHandling/ApiProblemDetailsFactory.cs`
- `Gestion.Ganadera.API/Middleware/MetricsMiddleware.cs`
- `Gestion.Ganadera.API/Extensions/AuthenticationExtensions.cs`
- `Gestion.Ganadera.API/Extensions/RateLimitingExtensions.cs`

## Error comun

Pensar que el correlation id es opcional cuando ya hay logs. En sistemas reales ayuda muchisimo a soporte.

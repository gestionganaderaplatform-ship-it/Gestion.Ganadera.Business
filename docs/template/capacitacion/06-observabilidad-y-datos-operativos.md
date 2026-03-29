# 06. Observabilidad y datos operativos

## Que piezas incluye este template

- logs de aplicacion
- correlation id
- metricas por request
- health checks
- eventos de seguridad
- auditoria de cambios

## Logs

La configuracion principal de logs esta en:

- `Gestion.Ganadera.API/Extensions/LoggingExtensions.cs`

Usa `Serilog` y escribe en consola y base de datos.

## Metricas por request

El middleware vive en:

- `Gestion.Ganadera.API/Middleware/MetricsMiddleware.cs`

La persistencia de esas metricas se delega a un contrato de `Application`.
Con eso, la capa HTTP no necesita conocer `DbContext` ni modelos tecnicos de infraestructura.

Registra:

- metodo
- ruta
- status code
- tiempo de respuesta
- correlation id
- codigo del API

## Auditoria

La auditoria no es lo mismo que logging.

- logging: que ocurrio tecnicamente
- auditoria: que cambio en los datos y quien lo cambio

Se genera con:

- `Gestion.Ganadera.Infrastructure/Persistence/Interceptors/AuditSaveChangesInterceptor.cs`

La auditoria intenta resolver el actor actual desde el contexto autenticado del request.

Si hay claims compatibles, registra ese identificador como actor textual.
Si ademas existe un identificador numerico valido, tambien se propaga a los campos numericos de entidades auditables.

Si no hay identidad disponible, el template no inventa un usuario.

## Por que existe ApiInfo.Codigo

Cuando varias APIs escriben en un mismo repositorio operativo, necesitas saber cual origino cada registro.

Se usa en:

- auditoria
- metricas
- eventos de seguridad
- logs

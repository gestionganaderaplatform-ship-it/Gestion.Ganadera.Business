# Guias de operacion de la API de negocio

Estas guias ayudan a desplegar y mantener `Gestion.Ganadera.API` sin depender de memoria ni de
aplicar cambios manuales al azar.

## Guias disponibles

1. [Despliegue de bases de datos](C:\Users\fabio\source\repos\fabiobaa\Ganaderia\Gestion.Ganadera.Business\docs\operacion\01-despliegue-bases-de-datos.md)

## Regla base

- `Development` puede usar migraciones automaticas para acelerar pruebas.
- `Test` y `Produccion` deben recibir primero el script SQL idempotente generado desde EF Core.
- La API se publica solo despues de confirmar que la base quedo alineada.

# Despliegue de bases de datos

Esta guia define el proceso recomendado para `Test` y `Produccion` en `Gestion.Ganadera.Business.API`.

## Regla base

- `Development` puede usar migraciones automaticas durante el arranque.
- `Test` y `Produccion` no deben depender de `Database.Migrate()` al iniciar la API.
- Antes de publicar una nueva version se debe aplicar un script SQL idempotente generado desde las migraciones.

## Flujo recomendado

1. Generar la migracion correspondiente al cambio de modelo.
2. Revisar el codigo de la migracion.
3. Generar el script SQL idempotente.
4. Revisar el script antes de ejecutarlo.
5. Aplicarlo en `Test`.
6. Validar que `__EFMigrationsHistory` quedo alineada.
7. Publicar la API.
8. Repetir el mismo proceso en `Produccion` con respaldo previo.

## Comando recomendado

```bash
dotnet ef migrations script --idempotent
```

## Antes de aplicar

- confirmar respaldo de la base
- comparar `__EFMigrationsHistory`
- confirmar que no hay drift manual

## Cuándo usar migracion automatica

Solo en desarrollo local.

# Despliegue de bases de datos - Gestion.Ganadera

Este directorio contiene los scripts SQL idempotentes que se aplican antes de publicar la API en `Test` o `Produccion`.

## Ubicacion

- `Database/Despliegue/Test/`
  Scripts idempotentes para el ambiente de pruebas.
- `Database/Despliegue/Produccion/`
  Scripts idempotentes para el ambiente productivo.

## Script generado actualmente

- `Database/Despliegue/Test/20260413_api_test_idempotent.sql`

## Paso a paso recomendado

1. Generar una nueva migracion en el proyecto `Gestion.Ganadera.Business.Infrastructure`.
2. Revisar el cambio de modelo y el codigo de la migracion.
3. Generar el script idempotente en `Database/Despliegue/Test/` o `Database/Despliegue/Produccion/`.
4. Validar que el script solo contenga cambios esperados.
5. Aplicar el script en la base correspondiente.
6. Confirmar que `__EFMigrationsHistory` quedo alineada.
7. Publicar la API.

## Aplicar en Test

Desde una consola con acceso al servidor SQL de `Test`, ejecuta el archivo generado:

```powershell
sqlcmd -S <servidor_sql> -d <base_datos> -E -b -i ".\Database\Despliegue\Test\20260413_api_test_idempotent.sql"
```

Si tu entorno usa autenticacion SQL en lugar de integrada, reemplaza `-E` por `-U` y `-P`.

## Comando para regenerar

Desde `Gestion.Ganadera.Business.API`:

```powershell
& "C:\Users\fabio\.dotnet\tools\dotnet-ef.exe" migrations script --idempotent --no-build `
  --project "..\Gestion.Ganadera.Business.Infrastructure\Gestion.Ganadera.Business.Infrastructure.csproj" `
  --startup-project ".\Gestion.Ganadera.Business.API.csproj" `
  -o "..\Database\Despliegue\Test\20260413_api_test_idempotent.sql"
```

## Regla operativa

- `Development` puede usar migraciones automaticas.
- `Test` y `Produccion` se actualizan con script previo.
- La API no debe depender de `Database.Migrate()` al arrancar en ambientes compartidos.

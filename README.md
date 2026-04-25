# Gestion.Ganadera

Base reusable para construir APIs en `.NET 9` con una estructura limpia, segura y lista para empezar aplicaciones reales sin improvisar la infraestructura en cada proyecto.

## Por donde empezar

Si es tu primera vez en este repositorio, sigue este orden:

1. Lee este `README.md`
2. Luego ve a [docs/README.md](./docs/README.md) para ubicar la documentacion
3. Despues entra a [docs/template/README.md](./docs/template/README.md) para la guia especifica del template

## Que incluye

- Clean Architecture con separacion por proyectos
- validacion JWT para integracion con un proveedor externo
- permisos por claims del JWT
- `ProblemDetails` para errores consistentes
- validacion con `FluentValidation`
- observabilidad con logs, metricas, correlation id y health checks
- auditoria de cambios
- CRUD reusable para features simples
- patron reusable para Excel listo para entidades de negocio

## Estructura de la solucion

```text
Gestion.Ganadera.Business.API
Gestion.Ganadera.Business.Application
Gestion.Ganadera.Business.Domain
Gestion.Ganadera.Business.Infrastructure
```

## Convenciones de carpetas

Para evitar archivos sueltos y ayudar a que el template crezca con orden:

- `API` agrupa infraestructura HTTP por intencion, por ejemplo `Requests/Helpers`, `ErrorHandling`, `Configuration/Providers` y `Security`
- `Application` concentra piezas transversales del template en `Common`, y deja cada feature con sus carpetas propias
- `Domain` crece por concepto o feature, no por tipo tecnico
- `Infrastructure` agrupa lo de EF dentro de `Persistence`, incluyendo `Configurations`, `Repositories`, `Metadata` y `Extensions`

La regla es simple: si una clase necesita explicacion para saber donde ponerla, todavia no esta en la carpeta correcta.

## Quick start

### 1. Prerrequisitos

- `.NET SDK 9`
- `SQL Server` local o contenedor

### 2. Configurar secretos locales

#### Por que?

`appsettings.json` NO debe contener datos sensibles porque se sube a Git. Los secretos se cargan desde el equipo local.

#### Paso 1: Ejecutar el script de configuracion

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\setup-secrets.ps1
```

El script te pedira:
- **Cadena de conexion a tu SQL Server** (ejemplo: `Server=localhost;Database=AppDb;...`)
- **Clave JWT de validacion** o secreto compartido con tu proveedor externo
- **Licencia AutoMapper** solo si ya tienes una licencia real

#### Paso 2: Si prefieres hacerlo manualmente

Abre una terminal en la carpeta `Gestion.Ganadera.Business.API` y ejecuta:

```powershell
# 1. Inicializar secretos (solo una vez)
dotnet user-secrets init

# 2. Configurar tu conexion a SQL Server
#    Si usas autenticacion Windows (tu usuario de Windows):
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=AppDb;Trusted_Connection=True;TrustServerCertificate=True"

#    Si usas autenticacion SQL (usuario y contrasena de SQL Server):
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=AppDb;User=sa;Password=tu_password;TrustServerCertificate=True"

# 3. Configurar valores JWT para validacion
dotnet user-secrets set "Jwt:Enabled" "true"
dotnet user-secrets set "Jwt:Issuer" "https://tu-auth-service"
dotnet user-secrets set "Jwt:Audience" "gestion-ganadera-api"
dotnet user-secrets set "Jwt:SigningKey" "MiClaveCompartidaParaDesarrollo2024"

# 4. Licencia AutoMapper (opcional y solo si ya tienes una licencia real)
dotnet user-secrets set "AutoMapper:LicenseKey" "tu-licencia-real"
```

#### Verificar que se configuraron

```powershell
dotnet user-secrets list
```

Deberias ver algo como:
```text
ConnectionStrings:DefaultConnection = Server=localhost;Database=AppDb;...
Jwt:Enabled = true
Jwt:Issuer = https://tu-auth-service
Jwt:Audience = gestion-ganadera-api
Jwt:SigningKey = MiClaveCompartidaParaDesarrollo2024
AutoMapper:LicenseKey = tu-licencia-real
```

#### Para produccion

En el servidor o en Azure/App Service, configura variables de entorno:

```powershell
# Windows
setx ConnectionStrings__DefaultConnection "Server=prod-server;Database=AppDb;User=sa;Password=xxx;TrustServerCertificate=True"
setx Jwt__Enabled "true"
setx Jwt__Issuer "https://tu-auth-service"
setx Jwt__Audience "gestion-ganadera-api"
setx Jwt__SigningKey "ClaveCompartidaMuySeguraParaProduccion2024"
setx AutoMapper__LicenseKey "tu-licencia-real"
```

> **Nota:** Las variables de entorno usan `__` (doble guion bajo) para anidar propiedades.

### 4. Restaurar paquetes

```powershell
dotnet restore .\Gestion.Ganadera.Business.API\Gestion.Ganadera.Business.API.sln
```

### 5. Aplicar migraciones

```powershell
dotnet ef database update --project .\Gestion.Ganadera.Business.Infrastructure\Gestion.Ganadera.Business.Infrastructure.csproj --startup-project .\Gestion.Ganadera.Business.API\Gestion.Ganadera.Business.API.csproj --context AppDbContext
```

### 6. Ejecutar la API

```powershell
dotnet run --project .\Gestion.Ganadera.Business.API\Gestion.Ganadera.Business.API.csproj
```

### 7. Verificar que respondio

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\smoke-api.ps1
```

Swagger queda disponible en desarrollo y `/health` sirve como verificacion rapida.

Si tenias una ruta vieja guardada, `/openapi/v1.json` ahora redirige a `/swagger/v1/swagger.json`.

### 7.1 Endpoints operativos

Estos endpoints no forman parte del contrato funcional de negocio y por eso no dependen de Swagger como fuente principal:

- `GET /health`
  Valida que la API responda y que el health check de SQL Server este sano.
- `GET /swagger`
  Abre la UI de Swagger en `Development`.
- `GET /swagger/v1/swagger.json`
  Devuelve la especificacion OpenAPI en `Development`.
- `GET /openapi/v1.json`
  Redirige a `/swagger/v1/swagger.json` en `Development` para mantener compatibilidad con rutas antiguas.

### 7.2 Probar JWT desde Swagger

1. Abre `/swagger`
2. Pulsa `Authorize`
3. Pega el encabezado completo:

```text
Bearer TU_TOKEN
```

4. Consume un endpoint protegido como `seguridad/auditoria/1`

La API no emite tokens. Debes obtenerlos desde tu proveedor externo de autenticacion.

Mientras `Jwt:Enabled` sea `false`, el template no exigira token y podras usarlo sin un AuthService externo.
Cuando lo cambies a `true`, cada endpoint protegido volvera a exigir autenticacion y permisos desde claims del JWT.

### 8. Recorrer una prueba guiada del template

El template ya no incluye una feature demo acoplada al dominio.

Para recorrerlo:

1. usa `Auditoria` para validar autenticacion, autorizacion, filtros y convenciones HTTP
2. crea tu primera feature real heredando las bases documentadas en [docs/template/capacitacion/07-como-crear-una-feature.md](./docs/template/capacitacion/07-como-crear-una-feature.md)
3. aplica el patron Excel solo si esa feature realmente lo necesita

## Documentacion importante

- Hub de documentacion: [docs/README.md](./docs/README.md)
- Guia del template: [docs/template/README.md](./docs/template/README.md)
- Guia detallada de la API: [docs/template/backend-api.md](./docs/template/backend-api.md)
- Capacitacion guiada del template: [docs/template/capacitacion/README.md](./docs/template/capacitacion/README.md)
- Bitacora y vision del template: [docs/template/seguimiento-template.md](./docs/template/seguimiento-template.md)

La carpeta `docs/template/` agrupa el material propio del template para que en una solucion real sea facil quitarlo o reemplazarlo sin mezclarlo con documentacion del proyecto.

## Nota sobre logs

La tabla `Seguridad.Log_Aplicacion` puede existir en la base aunque no aparezca en migraciones de EF Core.
Ese almacenamiento lo administra el sink de `Serilog`, no el `DbContext` del template.

## Renombrar el template

Este repositorio incluye scripts para reemplazar `Gestion.Ganadera` por el nombre de tu solucion.

### Linux/macOS

```bash
bash scripts/rename-template.sh MiEmpresa.MiApp AppDbContext AppDb --detach-origin
```

### Windows

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\rename-template.ps1 -NewName "MiEmpresa.MiApp" -NewDbContext "AppDbContext" -NewDbName "AppDb" -DetachOrigin
```

## Archivos base agregados para estabilizar el template

- [.editorconfig](/C:/Users/fabio/source/repos/fabiobaa/Ganaderia/Gestion.Ganadera.Business/.editorconfig): estilo y formato consistentes
- [Directory.Build.props](/C:/Users/fabio/source/repos/fabiobaa/Ganaderia/Gestion.Ganadera.Business/Directory.Build.props): configuracion comun de proyectos
- [setup-secrets.ps1](/C:/Users/fabio/source/repos/fabiobaa/Ganaderia/Gestion.Ganadera.Business/scripts/setup-secrets.ps1): configuracion de secretos locales
- [smoke-api.ps1](/C:/Users/fabio/source/repos/fabiobaa/Ganaderia/Gestion.Ganadera.Business/scripts/smoke-api.ps1): verificacion minima de la API

## Como evolucionar el template sin sobre ingenieria

Antes de agregar una nueva capacidad, valida:

1. Aplica a casi cualquier API.
2. Reduce friccion real al arrancar proyectos.
3. Evita deuda o inconsistencias.
4. Se puede mantener sin volver complejo el template.

Si no pasa ese filtro, no deberia entrar en la version base.

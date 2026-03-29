# 01. Arquitectura y capas

## Que intenta resolver este template

Muchos proyectos backend empiezan con buena intencion y muy rapido se vuelven dificiles de mantener porque:

- la API termina haciendo logica de negocio
- los servicios dependen directo de EF Core y HTTP
- todo vive en un mismo proyecto
- nadie sabe donde poner una nueva clase

Este template evita ese caos desde el inicio separando responsabilidades.

## Que es Clean Architecture

Clean Architecture es una forma de organizar el codigo para que:

- las reglas mas importantes del sistema no dependan de frameworks
- los detalles tecnicos puedan cambiar sin romper el nucleo
- cada capa tenga una responsabilidad clara

No significa crear muchas capas por moda. Significa poder responder con claridad:

- que parte es negocio
- que parte es transporte HTTP
- que parte es base de datos
- que parte es infraestructura tecnica

## Como se ve en este template

```text
Gestion.Ganadera.API
Gestion.Ganadera.Application
Gestion.Ganadera.Domain
Gestion.Ganadera.Infrastructure
```

## Rol de cada proyecto

### API

Responsabilidad:

- recibir requests HTTP
- aplicar middleware
- autenticar y autorizar
- exponer controllers
- registrar dependencias

No deberia contener:

- reglas de negocio importantes
- acceso directo al `DbContext`

En este proyecto:

- `Gestion.Ganadera.API/Program.cs`
- `Gestion.Ganadera.API/Extensions`
- `Gestion.Ganadera.API/Controllers`
- `Gestion.Ganadera.API/Middleware`

### Application

Responsabilidad:

- definir contratos
- definir view models
- definir filtros
- validar entrada
- expresar los casos de uso del sistema

No deberia contener:

- `IFormFile`
- `IActionResult`
- `DbContext`
- detalles de JWT, Serilog o SQL Server

En este proyecto:

- `Gestion.Ganadera.Application/Features/.../Interfaces`
- `Gestion.Ganadera.Application/Features/.../ViewModels`
- `Gestion.Ganadera.Application/Features/.../Validators`
- `Gestion.Ganadera.Application/Abstractions`
- `Gestion.Ganadera.Application/Common`

### Domain

Responsabilidad:

- guardar conceptos estables del sistema
- representar entidades reales

No deberia contener:

- HTTP
- EF Core
- configuracion
- logging

En este proyecto:

- `Gestion.Ganadera.Domain/Base`
- `Gestion.Ganadera.Domain/Features/Seguridad/Auditoria.cs`

### Infrastructure

Responsabilidad:

- implementar detalles tecnicos
- persistencia
- seguridad tecnica
- observabilidad
- integracion con Excel

En este proyecto:

- `Gestion.Ganadera.Infrastructure/Persistence`
- `Gestion.Ganadera.Infrastructure/Security`
- `Gestion.Ganadera.Infrastructure/Observability`
- `Gestion.Ganadera.Infrastructure/Services`

Dentro de `Persistence` tambien viven piezas de soporte tecnico como:

- `Configurations`
- `Repositories`
- `Metadata`
- `Extensions`

## Nota sobre Shared

En este template no se mantiene un proyecto `Shared`.

La regla es simple:

- lo que pertenece a HTTP o permisos vive en `API`
- lo que pertenece a validacion o contratos vive en `Application`
- lo que pertenece a infraestructura tecnica vive en `Infrastructure`

Esto evita convertir `Shared` en un cajon de clases sin dueÃ±o claro.

## Convencion para evitar archivos sueltos

Cuando una pieza no pertenece a una feature concreta, igual debe quedar en una carpeta que explique su responsabilidad:

- `Common` en `Application` para atributos, constantes y ayudas reutilizables de aplicacion
- `Requests/Helpers` en `API` para parsing y normalizacion de requests HTTP
- `Configuration/Providers` en `API` para adapters que leen opciones del host
- `ErrorHandling` en `API` para la construccion uniforme de errores
- `Persistence/Metadata` en `Infrastructure` para metadata basada en EF Core

La meta no es crear mas carpetas. La meta es que un desarrollador nuevo pueda ubicar una clase sin adivinar.

## Direccion de dependencias

La regla importante es esta:

```text
Domain
   ^
Application
   ^
Infrastructure
   ^
API
```

## Decisiones de capa del template

- `Application` dejo de depender de tipos web
- `Auditoria` se movio a `Domain`
- modelos tecnicos como tokens, eventos y metricas se movieron a `Infrastructure`
- la validacion JWT quedo aislada en `API` para no acoplar `Application` ni `Domain` a autenticacion
- las claves primarias y defaults de persistencia se definen en `Infrastructure`, no en entidades de `Domain`

## Errores comunes

### "Si algo esta en Models, entonces va a Domain"

No. La ubicacion depende de la responsabilidad real, no del nombre de la carpeta.

### "Como es un template, todo puede vivir en Application"

Tampoco. Un template tambien necesita fronteras claras, porque esas fronteras son las que luego facilitan crecer.

## Preguntas frecuentes

### Por que `Domain` puede estar pequeno en un template

Porque un template no siempre nace con un dominio rico. Lo importante es que el espacio exista y que, cuando aparezca logica real, sepas donde ponerla.

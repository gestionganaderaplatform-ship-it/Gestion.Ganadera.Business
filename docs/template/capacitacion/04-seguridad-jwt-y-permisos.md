# 04. Seguridad JWT y permisos

## Dos conceptos que no deben confundirse

### Autenticacion

Responde:

> Quien eres.

### Autorizacion

Responde:

> Que puedes hacer.

## Como funciona la autenticacion en este template

La configuracion principal esta en:

- `Gestion.Ganadera.API/Extensions/AuthenticationExtensions.cs`

Ese archivo:

- configura `JwtBearer`
- valida issuer, audience, firma y expiracion
- responde con `ProblemDetails` en 401 y 403
- no genera tokens ni consulta usuarios internos

La API esta pensada para validar JWT emitidos por un proveedor externo de autenticacion.

Mientras `Jwt:Enabled` este en `false`, el template no exigira autenticacion. Al activarlo, comenzara a validar JWT y permisos por claims.

En otras palabras:

- `Jwt:Enabled = false`: modo template o desarrollo local sin AuthService externo
- `Jwt:Enabled = true`: modo integrado con proveedor externo y endpoints protegidos

## Como se resuelve el actor actual

Ademas de autenticar y autorizar, el template puede resolver el actor actual desde claims del request autenticado.

Esto se usa principalmente para auditoria de cambios.

Claims priorizados:

- `nameidentifier`
- `sub`
- `preferred_username`
- `email`
- `client_id`

Si existe un claim util, ese valor se registra como actor textual en auditoria.

Para campos numericos del dominio, el template solo asigna actor si encuentra un claim convertible a `long`.

Si el request no esta autenticado o no trae un claim compatible, el template no inventa un usuario.

## Como probar el token desde Swagger

En desarrollo, Swagger ajusta su documentacion segun `Jwt:Enabled`.

- si `Jwt:Enabled = true`, publica el esquema `Bearer` y muestra el boton `Authorize`
- si `Jwt:Enabled = false`, no exige autenticacion en la especificacion OpenAPI

Flujo recomendado:

1. Obtiene un token desde tu proveedor externo
2. Pulsa `Authorize` en Swagger
3. Pega el encabezado completo:

```text
Bearer TU_TOKEN
```

4. Llama un endpoint protegido, por ejemplo `GET /api/v1/seguridad/auditoria/1`

## Como se manejan los permisos

Piezas importantes:

- `Gestion.Ganadera.API/Security/Permissions/ControllerPermission.cs`
- `Gestion.Ganadera.API/Security/Permissions/ControllerPermissionsAttribute.cs`
- `Gestion.Ganadera.API/Security/Permissions/RequirePermissionAttribute.cs`
- `Gestion.Ganadera.API/Security/Permissions/PermissionPolicy.cs`
- `Gestion.Ganadera.API/Security/Permissions/PermissionAuthorizationRequirement.cs`
- `Gestion.Ganadera.API/Conventions/PermissionApplicationModelConvention.cs`

`ControllerPermission` usa `[Flags]`. Por eso sus valores son potencias de dos.

Eso permite combinar permisos de forma simple, por ejemplo:

- `ControllerPermission.GetAll | ControllerPermission.GetById | ControllerPermission.Filter`

En runtime, `RequirePermission` se traduce a policies de ASP.NET Core y se resuelve leyendo claims del JWT.

Claims soportados:

- `permission`
- `permissions`

El handler acepta multiples claims repetidos o listas separadas por comas, punto y coma o espacios.

## Regla importante

Los permisos no se consultan en base de datos. Toda la autorizacion se basa en claims del token ya validado.

Lo mismo aplica para la identidad usada por auditoria: se toma del contexto autenticado si existe, sin buscar usuarios en base de datos ni depender de autenticacion interna.

## Error comun

Pensar que JWT ya resuelve toda la autorizacion. JWT autentica y transporta claims; los permisos siguen siendo una decision del sistema o del proveedor que emite el token.

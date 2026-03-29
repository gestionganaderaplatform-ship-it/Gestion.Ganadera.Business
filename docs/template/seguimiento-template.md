# Seguimiento del Template

Documento vivo para registrar decisiones, cambios relevantes y proximos pasos del template sin perder foco ni caer en sobre ingenieria.

## Objetivo actual

Consolidar un template backend reusable en `.NET 9` que permita empezar APIs reales rapido, con buena base tecnica y sin convertir el template en un framework dificil de mantener.

## Criterios de evolucion

Antes de agregar algo nuevo al template, validar:

1. Resuelve un problema que aparece en casi cualquier API.
2. Reduce friccion real al arrancar proyectos.
3. Evita deuda tecnica o inconsistencias.
4. Se puede documentar y mantener sin volver complejo el template.

Si la respuesta es no en la mayoria de los casos, no debe entrar en la version base.

## Bitacora de cambios

### 2026-03-15 a 2026-03-19

- Se limpio la separacion de capas para acercar la solucion a Clean Architecture real.
- `Auditoria` se consolido como entidad de `Domain`.
- Los modelos tecnicos de seguridad y observabilidad se movieron a `Infrastructure`.
- `Application` dejo de depender de tipos web de ASP.NET.
- La API se desacoplo de autenticacion interna y ahora solo valida JWT emitidos por un proveedor externo.
- Se elimino el proyecto `Shared` y sus piezas se redistribuyeron por capa.
- Se incorporo `ApiInfo.Codigo` para identificar el origen de datos operativos.
- Se dejo lista una base reusable para Excel, pero se decidio no aplicarla a `Auditoria`.
- Se agregaron `.editorconfig`, `Directory.Build.props` y smoke test basico.
- Se elimino la feature demo del dominio base para dejar el template mas limpio y enfocado en heredar las clases base.
- Se reforzo la documentacion para crear features reales heredando `BaseController`, `BaseService`, `BaseRepository` y las bases de Excel.
- Swagger quedo configurado y documentado para probar JWT externo desde el boton `Authorize`.
- La documentacion se reorganizo para separar puerta de entrada, detalle tecnico y capacitacion guiada.

## Estado actual

### Ya resuelto

- arquitectura por capas razonablemente limpia
- validacion JWT desacoplada
- permisos por claims del JWT
- logging y observabilidad
- auditoria de cambios
- configuracion base de API
- CRUD reusable
- patron reusable para Excel listo para entidades de negocio
- documentacion de onboarding, operacion y capacitacion

### No aplicar por defecto

- importacion o exportacion sobre tablas operativas como auditoria, logs, metricas o eventos de seguridad
- abstracciones extra que no tengan uso transversal claro
- sobre ingenieria para capacidades que no se usen en casi cualquier API

## Proximos pasos sugeridos

### Prioridad alta

- documentar convenciones de nombres y estructura por feature
- limpiar cualquier ruido restante de archivos locales generados por herramientas

### Prioridad media

- agregar un smoke test mas automatizable en pipeline
- definir una guia corta de adopcion del template para nuevas APIs
- documentar permisos por controller o por modulo

### Prioridad baja

- ejemplo de tests por capa
- empaquetado mas formal del template
- automatizacion de versionado del template

## Decisiones tomadas

### Auditoria no debe importar ni exportar Excel

Motivo:

- es una tabla operativa de trazabilidad
- debe priorizar consulta y control, no carga masiva

### El patron Excel si forma parte del template

Motivo:

- es util para muchas entidades de negocio
- reduce trabajo repetitivo
- debe aplicarse solo donde tenga sentido funcional

### No mantener un proyecto `Shared`

Motivo:

- termina volviendose un cajon de clases sin dueÃ±o claro
- es mas claro ubicar cada pieza en la capa que realmente la usa

### La evolucion del template debe ser conservadora

Motivo:

- el objetivo es empezar aplicaciones, no disenar indefinidamente
- cada agregado debe demostrar valor transversal real

## Como mantener esta bitacora

Cada vez que se haga un cambio relevante en arquitectura, seguridad, observabilidad, patron base o documentacion:

1. registrar el cambio en `Bitacora de cambios`
2. anotar la decision si afecta el enfoque del template
3. mover pendientes entre prioridades segun el estado real

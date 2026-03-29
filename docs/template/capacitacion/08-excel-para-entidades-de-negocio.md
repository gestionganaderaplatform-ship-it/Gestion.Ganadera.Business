# 08. Excel para entidades de negocio

## Para que existe este patron

Muchas APIs terminan necesitando:

- exportar datos para usuarios
- cargar informacion masivamente
- entregar plantillas para estandarizar archivos de importacion

## Donde esta en este proyecto

Contratos:

- `Gestion.Ganadera.Application/Features/Base/Interfaces/IBaseService.cs`

Configuracion:

- `Gestion.Ganadera.Application/Abstractions/Interfaces/IExcelImportSettingsProvider.cs`
- `Gestion.Ganadera.API/Options/ExcelImportOptions.cs`
- `Gestion.Ganadera.API/Configuration/Providers/ExcelImportSettingsProvider.cs`

Implementacion base:

- `Gestion.Ganadera.Infrastructure/Services/Base/BaseService.cs`
- `Gestion.Ganadera.API/Controllers/Base/BaseController.cs`

## Que resuelve esta base

- generar archivo de exportacion desde la variante extendida de `BaseService`
- exponer `exportar-excel` desde la variante extendida de `BaseController`
- soportar importacion y plantilla como capacidades opcionales
- limitar cantidad maxima de filas si una feature habilita importacion

## Cuando si usarlo

- catalogos
- maestros
- parametrizacion
- cargas operativas controladas

## Cuando no usarlo

- importacion o plantilla en tablas operativas donde no tenga sentido funcional
- cargas masivas sobre logs, metricas o eventos de seguridad

## Regla practica

No actives import/export solo porque el template lo soporte. Activalo cuando tenga sentido funcional para la entidad.
Una feature puede usar solo exportacion, o exportacion + importacion, sin abrir toda la superficie obligatoriamente.

## Variantes base

- para solo exportacion:
  `BaseService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel, TRepository, TExportFilter>`
  y
  `BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>`

- para exportacion + importacion + plantilla:
  `BaseService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel, TRepository, TExportFilter, TImportModel>`
  y
  `BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter, TImportModel>`

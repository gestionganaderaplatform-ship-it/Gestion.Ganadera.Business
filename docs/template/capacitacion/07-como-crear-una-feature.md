# 07. Como crear una feature

## Punto de partida

El template ya no trae una feature demo dentro del dominio.

La forma recomendada de arrancar una API real es crear tu primera feature propia heredando las bases reutilizables.

## CRUD simple con herencia de bases

Pasos sugeridos:

1. crear la entidad en `Domain` si representa un concepto real
2. crear `ViewModel`, `CreateViewModel` y `UpdateViewModel` en `Application`
3. crear validadores
4. crear interfaces de repositorio y servicio en `Application`
5. implementar repositorio en `Infrastructure` heredando de `BaseRepository<TEntity>`
6. implementar servicio en `Infrastructure` heredando de `BaseService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel, TRepository>`
7. crear controller en `API` heredando de `BaseController<TViewModel, TCreateViewModel, TUpdateViewModel>`

## Que resuelven las bases

### `BaseRepository<TEntity>`

Resuelve operaciones comunes de persistencia:

- consultar por id
- consultar todos
- insertar
- insertar varios
- actualizar
- actualizar parcial
- eliminar
- filtrar por propiedades
- paginar

### `BaseService<TEntity, ...>`

Resuelve:

- mapeo entre entidad y view models
- coordinacion con repositorio base
- reutilizacion del CRUD sin repetir servicio por servicio

### `BaseController<TViewModel, ...>`

Expone endpoints HTTP listos para CRUD simple:

- `GET {codigo}`
- `GET`
- `POST`
- `POST bulk`
- `PUT`
- `PATCH {codigo}`
- `DELETE {codigo}`
- `POST filtrar`
- `GET paginado`

Ademas ya integra:

- validacion
- `ProblemDetails`
- codigos HTTP consistentes
- parsing seguro para `PATCH`
- filtrado generico por propiedades

## Cuando usar endpoints por foreign key

Los endpoints base relacionados con foreign key solo aplican si la feature define una propiedad marcada con `[ForeignKeyDefault]`.

Eso significa:

- si tu `TViewModel` tiene una FK clara y quieres exponerla como lookup principal, puedes usar esos endpoints
- si tu feature no tiene una FK clara, no debes considerarlos parte del contrato usable

No todas las features deben soportar consultas o eliminaciones por llave foranea.

## Cuando dejar de usar el patron base

Si la feature tiene:

- reglas propias
- flujo especial
- varios pasos
- integraciones o validaciones complejas

ya no conviene forzarla demasiado en el patron CRUD generico.

En ese punto, usa el template como base arquitectonica, no como restriccion.

## Patrones base utiles

- `Gestion.Ganadera.API/Controllers/Base/BaseController.cs`
- `Gestion.Ganadera.Infrastructure/Services/Base/BaseService.cs`
- `Gestion.Ganadera.Infrastructure/Persistence/Repositories/Base/BaseRepository.cs`

## Capacidades opcionales

Si una feature necesita capacidades adicionales como Excel, reportes o flujos especiales, la recomendacion es mantener un solo controller por feature.

La regla del template es:

- `BaseController` tiene una variante CRUD simple y una variante CRUD + Excel
- la exportacion Excel se integra desde `IBaseService` y `BaseService`, no con un servicio paralelo
- importacion y plantilla siguen siendo capacidades opcionales por feature
- la feature solo declara los permisos y dependencias que realmente usa

## Patron Excel

Si una feature necesita Excel:

1. crea el filtro de exportacion
2. crea el validador del filtro de exportacion
3. hereda de `BaseService<TEntity, TViewModel, TCreateViewModel, TUpdateViewModel, TRepository, TExportFilter>`
4. hereda de `BaseController<TViewModel, TCreateViewModel, TUpdateViewModel, TExportFilter>`
5. agrega importacion y plantilla solo si la feature realmente lo necesita

No lo apliques por reflejo. Excel debe entrar cuando la feature lo necesite funcionalmente.

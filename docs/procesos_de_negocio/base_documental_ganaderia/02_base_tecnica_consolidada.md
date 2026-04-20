# Base tecnica consolidada

## Alcance
Este documento consolida lo tecnico que si aparece en la documentacion generada y separa lo que quedo definido, lo que fue propuesto y lo que sigue pendiente de validar.

## Definido en esta base documental

### Primer corte ejecutado
- El primer corte real se bajo para `Registro de existente`.
- El esquema usado es `Ganaderia`.
- Las tablas minimas creadas para este corte son:
  - `Finca`
  - `Potrero`
  - `Categoria_Animal`
  - `Rango_Edad`
  - `Tipo_Identificador`
  - `Animal`
  - `Identificador_Animal`
  - `Evento_Ganadero`
  - `Evento_Ganadero_Animal`
  - `Evento_Detalle_Registro_Existente`
- `Animal` conserva el estado actual minimo para operar: finca actual, potrero actual, categoria actual, sexo y estado activo.
- `Animal` conserva fecha inicial de ingreso como snapshot del animal, no como nombre de evento incrustado en la tabla.
- `Rango_Edad` no queda como snapshot en `Animal`; queda en el detalle del proceso de registro.
- La auditoria heredada se aplico en las tablas creadas de este primer corte con `Fecha_Creado`, `Fecha_Modificado`, `Creado_Por` y `Modificado_Por`.
- El alcance por tenant del modulo se maneja con `Cliente_Codigo` heredado desde la base comun.
- El filtro por tenant debe aplicarse tanto a tablas maestras como a tablas transaccionales del modulo usando `Cliente_Codigo`.
- La siembra automatica por cliente nuevo se cerro para `Tipo_Identificador`, `Categoria_Animal` y `Rango_Edad`.
- La base inicial de `Categoria_Animal` queda en: `Becerra`, `Becerro`, `Novilla`, `Torete`, `Novillo`, `Vaca` y `Toro`.
- La base inicial de `Rango_Edad` queda en: `0 a 6 meses`, `7 a 12 meses`, `13 a 24 meses`, `25 a 36 meses` y `Mas de 36 meses`.
- En este primer corte no se bajaron columnas `Codigo_Publico` por entidad para Ganaderia.
- En este primer corte se mantuvo la regla de nomenclatura: cada campo propio arranca con el nombre de su tabla.
- Se simplificaron palabras innecesarias, pero sin quitar el prefijo de tabla.
- Se quitaron fechas de registro duplicadas en tablas donde la auditoria heredada ya cubre la creacion.

### Modelo funcional-tecnico
- El dominio debe conservar una identidad estable del animal.
- La operacion debe quedar sostenida por un tronco de eventos.
- El estado actual del animal se maneja como snapshot controlado por eventos.
- El historial visible y la trazabilidad funcional son obligatorios.

### Modulos funcionales
- Contexto operativo
- Ganado
- Ficha del animal
- Registrar procesos fase 1
- Registrar procesos fase 2
- Historial
- Correccion y anulacion
- Configuracion operativa
- Reportes

### Validaciones
- El frontend orienta y previene.
- El backend decide y garantiza.
- Ninguna regla critica debe quedar solo en frontend.

### Auditoria y trazabilidad
- Debe existir trazabilidad de negocio.
- Debe existir auditoria de cambios en piezas sensibles.
- Debe existir observabilidad tecnica.
- Corregir o anular no borra la traza original.

### Backlog tecnico
- Primero base de datos y dominio.
- Luego backend de consulta.
- Luego backend de procesos fase 1.
- Luego frontend de consulta.
- Luego frontend de procesos fase 1.
- Luego fase 2.
- Luego fortalecimiento transversal.

## Propuesto en esta base documental

### Estandar aplicado para Ganaderia
- Usar esquema `Ganaderia`.
- Bajar las entidades del dominio ganadero a tablas con naming alineado al patron definido en la documentacion tecnica.
- Mantener EF Core con clases limpias en C# y nombres fisicos alineados al modelo propuesto.
- `Categoria_Animal` y `Rango_Edad` se mantienen como catalogos por cliente, con una base inicial simple y editable por cada tenant.

### Modelo logico
- Entidades maestras y operativas:
  - `Finca`
  - `Participacion_Finca`
  - `Potrero`
  - `Categoria_Animal`
  - `Rango_Edad`
  - `Tipo_Identificador`
  - `Lote`
- Nucleo del dominio:
  - `Animal`
  - `Identificador_Animal`
  - `Relacion_Madre_Cria`
- Tronco transaccional:
  - `Evento_Ganadero`
  - `Evento_Ganadero_Animal`
- Detalles por proceso:
  - tablas `Evento_Detalle_*`
- Parametros:
  - `Parametro_Cuenta_Ganaderia`

### Convenciones EF Core
- Clases en singular y `PascalCase`.
- Propiedades con el nombre final de columna.
- `ToTable("Tabla", "Ganaderia")`.
- PK en `<Entidad>_Codigo`.
- FKs con nombre de entidad referenciada.
- `DeleteBehavior.Restrict`.
- `SYSDATETIME()` donde aplique.
- Indices por entidades criticas como `Animal`, `Identificador_Animal`, `Evento_Ganadero` y `Evento_Ganadero_Animal`.

### Contratos funcionales API
- Operaciones por modulo.
- Requests y responses base sugeridos.
- Filtros base sugeridos.
- Flujos de validacion y confirmacion por proceso.

## Pendiente de validar

### Modelo fisico final
- El esquema `Ganaderia` esta planteado como propuesta de trabajo, no como decision cerrada dentro de esta base documental.
- Las tablas, columnas e indices del modelo logico todavia no equivalen a un fisico validado.

### EF Core real
- Para el primer corte de `Registro de existente` ya existen entidades, configuraciones y migracion inicial.
- Lo pendiente es contrastar si el resto del modulo ganadero futuro mantiene exactamente la misma linea.

### Contratos finales
- Los contratos API estan en nivel funcional.
- El contrato HTTP final, el JSON final y los DTOs finales quedaron abiertos.

### Auditoria exacta
- La base funcional exige trazabilidad total.
- Para el primer corte ya se cerro auditoria heredada en las tablas creadas.
- Sigue pendiente validar si futuras tablas del modulo usaran exactamente el mismo bloque sin variaciones.

### Naming y estructura
- El naming del primer corte ya fue bajado a tablas reales bajo el esquema `Ganaderia`.
- Para fases siguientes, todavia debe validarse si no aparece un ajuste adicional.

## Lectura operativa
- Si se va a contrastar documentacion contra implementacion futura, esta base tecnica debe leerse junto con `03_elementos_pendientes_por_validar.md`.
- Si se va a construir desde documentacion, lo mas firme hoy es el modelo por eventos, el reparto de validaciones, la necesidad de historial y la secuencia del backlog.

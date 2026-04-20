# Backlog de Operación Ganadera (Sincronización Antigravity & AI)

Este documento sirve como puente para mantener alineados los contextos entre los asistentes de IA mientras se trabaja en la API de Gestión Ganadera. El objetivo es poder rotar de contexto sin perder el hilo de qué capas se están afectando y en qué estatus vamos.

> **Estado Actual Previo:** CRUD Base de Maestras completado (Finca, Potrero, CategoriaAnimal, RangoEdad, TipoIdentificador). Alineado al esquema CQRS/Clean Architecture del template.

---

## Tarea 1: Consulta de Ganado (Listado)
**Objetivo:** Traer el listado general simplificado.
- [x] **Data requerida:** Identificador principal, Sexo, Categoría, Finca, Potrero y Estado.
- [x] Construir el ViewModel (`GanadoListViewModel` o afín) proyectando los `Includes` necesarios de las tablas `Animal`, `Identificador_Animal`, `Finca`, `Potrero` y `Categoria_Animal`.
- [x] Exponer Query o Endpoint GET en la capa API validando permisos estándares o específicos.

## Tarea 2: Ficha Básica del Animal (Snapshot)
**Objetivo:** Devolver al front-end el snapshot vital.
- [x] Construir `AnimalSnapshotViewModel`.
- [x] Endpoint `GET /api/v1/ganaderia/animales/{codigo}` (o afín) leyendo el estado directo cargado en la misma tabla de `Animal`.

## Tarea 3: Caso de Uso "Validar Registro Existente"
**Objetivo:** Aislar validaciones tempranas antes de ensamblar dominios complejos.
- [x] Validar regla: Identificador no debe estar repetido a nivel global/tennat (`Cliente_Codigo`).
- [x] Validar integridad lógica frente a catálogos configurables: Potrero válido, Categoría válida, Rango válido, Tipo Identificador válido.
- [x] Crear comando/validador (`ValidarRegistroExistenteCommand`) cuyo resultado sea arrojar `ProblemDetails` limpios si hay violaciones.

## Tarea 4: Caso de Uso "Registrar Existente" (Transacción Completa)
**Objetivo:** Materializar la inyección en la base de datos de manera Atómica.
- [x] Crear el Command central (`RegistrarExistenteCommand`).
- [x] Orquestar en un entorno transaccional atómico el proceso de escritura de 5 bloques:
      1. Crear registro en tabla `Animal`.
      2. Crear registro en tabla `Identificador_Animal`.
      3. Crear registro maestro en tabla `Evento_Ganadero`.
      4. Pivotar relaciones en tabla `Evento_Ganadero_Animal`.
      5. Guardar la fotografía del evento en `Evento_Detalle_Registro_Existente`.
- [x] Consolidar cambios con EF Core asegurando atomicidad o un Rollback completo en caso de fallos.

## Tarea 5: Historial Básico del Animal
**Objetivo:** Lectura estructurada para la trazabilidad de negocio.
- [ ] Construir endpoint de sólo lectura para reconstruir la cronología del animal apuntando a un listado filtrado de la tabla `Evento_Ganadero`.

## Tarea 6: Base para el Segundo Proceso (Compra)
**Objetivo:** Clonar la estructura operativa del Registro Existente para Compra.
- [ ] Reutilizar el bloque de validaciones atómicas de "creación de animal".
- [ ] Reutilizar la transacción núcleo ajustando únicamente la tabla de subdetalle correspondiente (`Evento_Detalle_Compra` si aplicase) para mantener la velocidad de entrega en Fase 1.

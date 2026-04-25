# Reglas rectoras de producto y documentacion

## Proposito
Este documento fija la linea de trabajo que siempre debe seguirse mientras se construye GANADERO SaaS. No sustituye el documento funcional general ni la base consolidada; sirve como regla corta para no desviarse de lo ya definido.

## Regla principal
Antes de tocar backend, frontend, onboarding, menu, procesos, modelos o validaciones, se debe leer y tomar como base la documentacion vigente del producto. Si el cambio modifica comportamiento, la documentacion afectada se actualiza en el mismo bloque de trabajo.

## Lectura obligatoria siempre
1. `Gestion.Ganadera.Business/docs/procesos_de_negocio/documento_funcional_producto_ganadero.md`
2. `Gestion.Ganadera.Business/docs/procesos_de_negocio/base_documental_ganaderia/01_base_funcional_consolidada.md`
3. `Gestion.Ganadera.Business/docs/procesos_de_negocio/base_documental_ganaderia/02_base_tecnica_consolidada.md`
4. `Gestion.Ganadera.Business/docs/procesos_de_negocio/base_documental_ganaderia/03_modelo_de_experiencia_registrar.md`
5. `Gestion.Ganadera.Business/docs/procesos_de_negocio/backlog_operacion_ganadera.md`

## Lectura adicional por proyecto
Cuando el cambio cruce otros proyectos, tambien se debe leer lo siguiente:

### Auth
1. `Gestion.Ganadera.Business.Auth/PLATAFORMA.md`
2. `Gestion.Ganadera.Business.Auth/docs/procesos/backlog_operacion_ganadera.md`

### Web
1. `Gestion.Ganadera.Business.Web/docs/procesos/backlog_operacion_ganadera.md`

## Reglas de producto que no deben olvidarse
1. El sistema esta gobernado por procesos y eventos, no por mantenimiento libre de entidades.
2. `Registrar` es el punto unico formal de ejecucion de procesos.
3. La experiencia debe ser guiada, amigable y orientada por contexto de negocio.
4. `Inicio`, alertas, `Ganado` y `Ficha del animal` pueden conducir a `Registrar`, pero no reemplazan la ejecucion formal.
5. El onboarding inicial debe crear la primera finca antes de conducir al usuario a `Registrar`, empezando por `Registro de existente`.
6. La operacion diaria ocurre dentro de una finca activa.
7. Todo cambio funcional debe respetar historial, trazabilidad, estados derivados y permisos base.

## Regla de implementacion
1. Primero se contrasta el cambio contra la documentacion vigente.
2. Luego se implementa codigo alineado a esa base.
3. Si el codigo obliga a precisar algo, se actualiza la documentacion correspondiente.
4. Si aparece contradiccion entre codigo y documentacion, no se inventa: se deja explicita y se valida.

## Regla para menu y experiencia
1. La navegacion visible debe hablar en lenguaje de negocio, no en lenguaje tecnico.
2. El usuario debe entrar por puntos como `Inicio`, `Registrar`, `Ganado` e `Historial`.
3. Los nombres tecnicos de modulo pueden existir internamente, pero no deben dominar la experiencia visible.

## Regla documental
1. No crear documentos duplicados si ya existe una fuente vigente.
2. Cuando una decision quede aprobada en conversacion y afecte producto, debe bajarse a Markdown.
3. La documentacion funcional manda sobre propuestas tecnicas todavia no validadas.

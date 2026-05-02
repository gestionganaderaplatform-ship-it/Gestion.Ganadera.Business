# Proceso · Nacimiento

## 1. Objetivo
Registrar el ingreso de una cría nacida dentro de la operación, creando su identidad, su relación madre-cría y su ubicación inicial.

## 2. Alcance
Cubre:
- selección de madre
- fecha de nacimiento
- registro de identificador de la cría
- sexo de la cría
- categoría inicial
- potrero inicial
- relación madre-cría
- datos opcionales como peso al nacer, observación y responsable
- creación de la cría como animal activo
- historial de nacimiento

No cubre:
- reproducción avanzada
- seguimiento completo de gestación
- control productivo especializado

## 3. Roles que intervienen
- dueño titular
- administrador de finca
- veterinario
- operario autorizado si la política lo permite

## 4. Disparador del proceso
Se inicia cuando nace una cría dentro de la operación y debe incorporarse formalmente al sistema.

## 5. Precondiciones
- finca activa
- madre existente
- madre activa
- madre elegible según regla reproductiva
- usuario con permiso
- existe al menos una forma de identificación operativa permitida

## 6. Datos de entrada
- madre
- fecha_nacimiento
- identificador_principal_cria_tipo
- identificador_principal_cria_valor
- sexo_cria
- categoria_inicial_cria
- potrero_inicial
- peso_nacer
- observacion
- responsable
- identificadores_adicionales_cria

## 7. Campos exactos
### Datos de la madre
- madre
- datos_contexto_madre_mostrados

### Datos de la cría
- identificador_principal_cria_tipo
- identificador_principal_cria_valor
- identificadores_adicionales_cria
- sexo_cria
- categoria_inicial_cria
- potrero_inicial
- peso_nacer
- observacion
- responsable
- fecha_nacimiento

### Campos de sistema
- usuario_registro
- fecha_hora_registro
- tipo_evento = nacimiento
- origen_ingreso = nacimiento

## 8. Obligatorios y opcionales
### Obligatorios
- madre
- fecha_nacimiento
- identificador_principal_cria_tipo
- identificador_principal_cria_valor
- sexo_cria
- categoria_inicial_cria
- potrero_inicial

### Opcionales
- identificadores_adicionales_cria
- peso_nacer
- observacion
- responsable

## 9. Valores por defecto
- fecha_nacimiento: hoy
- peso_nacer: vacío
- observacion: vacía
- responsable: vacío
- identificadores_adicionales_cria: vacíos

## 10. Reglas de validación
1. La madre debe existir y estar activa.
2. La madre debe ser elegible según la regla reproductiva.
3. La fecha de nacimiento es obligatoria.
4. Debe existir identificador principal de la cría.
5. El identificador de la cría no puede estar repetido.
6. Debe definirse sexo de la cría.
7. Debe definirse categoría inicial real.
8. Debe definirse potrero inicial.
9. El potrero inicial debe pertenecer a la finca activa.
10. Si el potrero no existe, puede crearse dentro del flujo según política general.
11. La confirmación final es obligatoria.
12. Identificadores adicionales son opcionales y solo uno por tipo por animal, siguiendo la regla general.

## 11. Bloqueos
- no hay madre seleccionada
- no hay fecha de nacimiento
- no hay identificador principal de la cría
- no hay sexo de la cría
- no hay categoría inicial
- no hay potrero inicial
- madre inexistente
- madre inactiva
- madre no elegible
- identificador repetido
- usuario sin permiso

## 12. Advertencias
- el nacimiento se registra sin peso al nacer
- la fecha es muy antigua
- la categoría inicial no coincide con la sugerida por catálogo, si luego se parametriza
- faltan identificadores adicionales

## 13. Mensajes funcionales
- Debe seleccionar una madre válida.
- La fecha de nacimiento es obligatoria.
- Debe ingresar un identificador para la cría.
- El identificador de la cría ya existe.
- Debe seleccionar el sexo de la cría.
- Debe seleccionar la categoría inicial.
- Debe seleccionar un potrero inicial.
- No puede registrar nacimiento para una madre inactiva.
- La madre no es elegible para este proceso.
- No tiene permiso para registrar nacimientos.

## 14. Flujo principal paso a paso
1. Entrar a Registrar.
2. Elegir Nacimiento.
3. Seleccionar madre.
4. Validar elegibilidad.
5. Registrar fecha de nacimiento.
6. Registrar identificador principal de la cría.
7. Registrar sexo, categoría inicial y potrero inicial.
8. Registrar datos opcionales.
9. Mostrar resumen final.
10. Confirmar.
11. Crear la cría, crear relación madre-cría y guardar el evento.

## 15. Escenarios alternos
- si la madre no existe o está inactiva, se bloquea
- si el potrero no existe, puede crearse dentro del flujo
- si no se conoce el peso al nacer, el proceso no se bloquea
- si se quieren varios identificadores de la cría, se permite principal obligatorio y adicionales opcionales

## 16. Correcciones y anulaciones
- corrección auditada
- anulación trazable
- si la cría ya tiene eventos posteriores, la anulación del nacimiento debe controlarse con mucho cuidado
- operario no anula
- administrador, dueño y veterinario autorizado corrigen según política

## 17. Resultado esperado
La cría queda creada, activa, ubicada y relacionada con la madre.

## 18. Impacto en historial
- tipo_evento = nacimiento
- fecha_evento
- fecha_registro
- usuario_registro
- madre
- cría
- sexo
- categoría inicial
- potrero inicial
- peso al nacer, si existe

## 19. Impacto en estados derivados del animal
Para la cría:
- condicion_activo = activo
- finca_actual = finca activa
- potrero_actual = potrero inicial
- categoria_actual = categoría inicial
- origen_ingreso = nacimiento

Para la relación operativa:
- crea vínculo madre-cría

## 20. Observaciones de diseño funcional
- nacimiento debe crear el animal dentro del proceso y no como alta paralela
- la elegibilidad de la madre debe centralizarse
- este proceso será clave para modelo de datos por la relación madre-cría

## 21. Implementación backend inicial
- Endpoint del proceso: `POST /api/v1/ganaderia/procesos/nacimiento`.
- Endpoint de prevalidación: `POST /api/v1/ganaderia/procesos/nacimiento/validar`.
- Endpoint de consecutivo: `GET /api/v1/ganaderia/procesos/nacimiento/siguiente-consecutivo?fincaCodigo={id}`.
- La validación final vive en backend y controla finca, madre elegible, potrero, categoría, tipo de identificador, sexo, fecha e identificador duplicado por finca.
- El registro crea la cría como animal activo con origen `NACIMIENTO`.
- El registro crea identificador principal, identificador interno del sistema, evento ganadero, detalle del nacimiento y relación familiar madre-cría.
- El peso al nacer y la observación son opcionales.

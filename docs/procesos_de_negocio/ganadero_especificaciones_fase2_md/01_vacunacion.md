# Proceso · Vacunación

## 1. Objetivo
Registrar la aplicación de una vacuna a uno o varios animales para dejar trazabilidad sanitaria, soportar seguimiento operativo y mantener historial confiable.

## 2. Alcance
Este proceso cubre:
- vacunación individual
- vacunación por grupo
- selección de vacuna
- fecha de aplicación
- registro de aplicador cuando aplique
- registro de dosis, lote del producto y observación como datos complementarios
- creación contextual de vacuna si la política lo permite
- registro histórico del evento sanitario

No cubre:
- tratamiento sanitario no preventivo
- programación compleja de esquemas sanitarios
- inventario de medicamentos
- lógica clínica avanzada

## 3. Roles que intervienen
Pueden ejecutar:
- dueño titular
- administrador de finca
- operario autorizado
- veterinario

Pueden corregir o anular según política:
- administrador de finca
- dueño titular
- veterinario si se le otorga permiso especial

## 4. Datos del biológico
El proceso debe manejar los siguientes datos técnicos de la vacunación:

### Enfermedad objetivo
Las enfermedades cubiertas por el proceso son:
- **Fiebre aftosa**
- **Brucelosis**
- **Rabia** (si aplica según la región y el plan sanitario)

La enfermedad objetivo puede ser:
- Dato obligatorio según el tipo de biológico
- Dato condicional según la configuración de la cuenta

### Ciclo de vacunación
El ciclo indica la fase del esquema sanitario:
- **Primera dosis**
- **Refuerzo**
- **Refuerzo 2** (o posterior según esquema)
- **Revacunación anual**

### Lote del biológico
- Campo obligatorio para trazabilidad sanitaria
- Permite rastrear el lote de la vacuna aplicada

### Vacunador
- Puede ser el veterinario encargado
- Se toma del **delegado activo en sesión** si existe uno asignado
- Si no hay delegado, se permite capturar manualmente

### Soporte o certificado
- Documento de soporte que avala la vacunación aplicada
- Puede ser un certificado oficial, guía de pecuaria, o documento de la entidad sanitaria
- **PENDIENTE - DEUDA TÉCNICA: requiere implementación de almacenamiento**
- Actualmente se guarda solo el nombre del archivo como referencia (Soporte_Certificado_Nombre)
- **Solución propuesta: Azure Blob Storage**
  - Tier recomendado: Cool (=$0.01/GB/mes para archivos poco accedidos)
  - Costo estimado: <$1 USD/mes por cliente
  - Alternativa: Storage local si no se usa Azure
- Sealmacena como referencia adjunta

## 4. Disparador del proceso
El proceso inicia cuando se aplica una vacuna en la operación y debe quedar registro formal del evento.

## 5. Precondiciones
- existe una finca activa
- el animal existe
- el animal está activo
- el usuario tiene permiso para registrar vacunación
- la vacuna existe o puede crearse dentro del flujo según política
- si se registra por grupo, todos los animales deben estar activos y seleccionables
- si se requiere soporte, debe existir un documento válido para adjuntar

## 6. Datos de entrada
- modalidad de registro
- animal o grupo
- vacuna
- enfermedad_objetivo
- ciclo_vacunacion
- fecha de aplicación
- lote_biologico
- Vaccunador
- dosis
- soporte_certificado (DEUDA: solo se guarda nombre, falta blob storage)
- observación

## 7. Campos exactos
### Encabezado del proceso
- modalidad_registro
- finca_activa_mostrada

### Datos del evento
- enfermedad_objetivo
- ciclo_vacunacion
- fecha_aplicacion
- lote_biologico
- Vaccunador
- dosis
- soporte_certificado (DEUDA: solo se guarda nombre, falta blob storage)
- observacion

### Selección de animales
- animal, en individual
- grupo_animales, en grupo

### Campos de sistema
- usuario_registro
- fecha_hora_registro
- tipo_evento =vacunacion

## 8. Obligatorios y opcionales
### Obligatorios
- modalidad_registro
- animal o grupo
- vacuna
- enfermedad_objetivo
- ciclo_vacunacion
- fecha_aplicacion
- lote_biologico
- soporte_certificado

### Opcionales
- Vaccunador
- dosis
- observacion

### Automáticos
- usuario_registro
- fecha_hora_registro
- finca_activa
- Vaccunador: se auto-llena desde delegado en sesión si existe

## 9. Valores por defecto
- modalidad_registro: individual
- fecha_aplicacion: hoy
- Vaccunador: desde delegado en sesión
- dosis: vacío
- lote_producto: vacío
- observacion: vacío

## 10. Reglas de validación
1. No se permite registrar vacunación sin animal o grupo seleccionado.
2. No se permite registrar vacunación sin vacuna.
3. La enfermedad objetivo es obligatoria.
4. El ciclo de vacunación es obligatorio.
5. La fecha de aplicación es obligatoria.
6. El lote del biológico es obligatorio.
7. **El soporte o certificado es obligatorio y debe adjuntarse antes de confirmar** (PENDIENTE: repositorio de archivos)
8. No se permite vacunar animales inactivos.
9. No se permite vacunar animales inexistentes.
10. En grupo, todos los animales deben ser válidos al confirmar.
11. Si la vacuna no existe, solo podrá crearse si la política lo permite.
12. La dosis, si se registra, debe cumplir formato válido.
13. El registro debe cerrarse siempre con confirmación final.
14. La fecha debe respetar la política de fecha anterior permitida por cuenta y rol.
15. El Vaccunador seauto-llena desde el delegado en sesión si existe uno activo.

## 11. Bloqueos
- no hay finca activa
- no hay animal ni grupo seleccionado
- no hay vaccine seleccionada
- no hay enfermedad_objetivo
- no hay ciclo_vacunacion
- no hay fecha de aplicación
- no hay lote_biologico
- no hay soporte_certificado adjunto
- el animal no existe
- el animal está inactivo
- algún animal del grupo está inactivo
- usuario sin permiso
- vaccine inexistente cuando no se permita creación contextual
- no hay delegado en sesión y no se proporcionó Vaccunador manualmente

## 12. Advertencias
- falta dosis
- falta Vaccunador (se Sugiere capturar)
- la fecha es muy antigua según la política
- el animal tiene antecedentes sanitarios recientes del mismo tipo
- el grupo es muy grande y conviene revisar antes de confirmar

## 13. Mensajes funcionales
### Errores
- Debe seleccionar una vaccine.
- La enfermedad objetivo es obligatoria.
- El ciclo de vacunación es obligatorio.
- La fecha de aplicación es obligatoria.
- El lote del biológica es obligatorio.
- **Debe adjuntar el soporte o certificado de la vacunación.**
- Debe seleccionar al menos un animal.
- No puede vacunar un animal inactivo.
- El animal no existe en el sistema.
- No tiene permiso para registrar vacunación.
- No hay veterinario delegado en sesión. Proporcione el nombre del Vaccunador.

### Advertencias
- La vacunas se registrará sin dosis.
- La fecha de aplicación supera la ventana habitual de registro.
- El animal tiene antecedentes sanitarios recientes del mismo tipo.

### Confirmación
- Va a registrar vacunación para {n} animal(es).
- Va a registrar vacunación de {enfermedad} ciclo {ciclo} para {n} animal(es).
- ¿Confirma guardar el evento?

### Éxito
- Vacunación registrada correctamente.
- Se registraron la vacunación para {n} animal(es).

## 14. Flujo principal paso a paso
1. El usuario entra a Registrar.
2. Selecciona el proceso Vacunación.
3. El sistema valida finca activa y permisos.
4. El usuario elige modalidad individual o grupo.
5. Selecciona animal o grupo.
6. Selecciona vaccine.
7. **Selecciona enfermedad objetivo.**
8. **Selecciona ciclo de vacunación.**
9. Registra fecha de aplicación.
10. **Registra lote del biológico.**
11. **Adjunta soporte o certificado de la vacunación.**
12. **El Vaccunador se auto-llena desde delegado en sesión** (o se captura manualmente).
13. Registra datos opcionales (dosis, observación).
14. El sistema muestra resumen final.
15. El usuario confirma.
16. El sistema guarda el evento y actualiza historial.

## 15. Escenarios alternos
1. Si la vaccine no existe, se permite creación contextual si la política lo autoriza.
2. Si un animal del grupo está inactivo, el sistema excluye o bloquea según configuración; por defecto debe bloquear la confirmación hasta corregir.
3. **Si hay un veterinario delegado en sesión, el Vaccunador seauto-llena automáticamente.**
4. **Si no hay delegado, el usuario debe capturar manualmente el nombre del Vaccunador.**
5. **El soporte o certificado debe adjuntarse en formato PDF, imagen o documento válido.**
6. Si se registra desde ficha del animal, el animal llega preseleccionado.

## 16. Correcciones y anulaciones
- la corrección debe dejar trazabilidad del valor anterior y nuevo
- la anulación no borra el evento original
- operario no anula
- administrador, dueño y veterinario autorizado pueden corregir o anular según política
- si el evento ya disparó seguimientos futuros, la corrección debe reflejarse en esos pendientes

## 17. Resultado esperado
Queda un evento sanitario válido asociado al animal o grupo, visible en historial y utilizable para seguimiento.

## 18. Impacto en historial
Debe registrar:
- tipo_evento =vacunacion
- fecha_evento
- fecha_registro
- usuario_registro
- enfermedad_objetivo
- ciclo_vacunacion
- vaccine
- lote_biologico
- Vaccunador
- dosis, si existe
- soporte_certificado (referencia al archivo adjunto)
- observacion, si existe

## 19. Impacto en estados derivados del animal
No cambia estado activo ni ubicación.
Puede impactar:
- ultimo_evento_sanitario
- alertas o seguimiento futuro, si luego se parametriza
- trazabilidad sanitaria consolidada

## 20. Observaciones de diseño funcional
- La enfermedad objetivo y el ciclo son datos clave para trazabilidad sanitaria.
- El soporte o certificado es obligatorio para cumplimiento normativo. (PENDIENTE: repositorio de archivos)
- El Vaccunador se toma del delegado en sesión para evitar errores de captura.
- El lote del biológico permite trazabilidad en caso de recalls.
- El sistema debe manejar las enfermedades estándar: aftosa, brucelosis, rabia.
- El ciclo indica la fase del esquema: primera dosis, refuerzo, refuerzo 2, revacunación.
- Conviene manejar vaccine como catálogo controlado.
- La modalidad grupo debe priorizar rapidez.
- Los campos complementarios no deben bloquear fase 2.

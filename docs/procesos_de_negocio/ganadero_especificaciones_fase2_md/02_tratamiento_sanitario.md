# Proceso · Tratamiento sanitario

## 1. Objetivo
Registrar tratamientos aplicados al animal para conservar trazabilidad clínica básica y soporte operativo sanitario.

## 2. Alcance
Cubre:
- tratamiento individual
- tratamiento por grupo
- tipo de tratamiento o producto
- fecha de aplicación
- aplicador, dosis, duración e indicación como opcionales
- observación clínica
- creación contextual del tratamiento o producto si la política lo permite
- base para seguimiento posterior cuando aplique

No cubre:
- historia clínica avanzada
- órdenes médicas complejas
- inventario farmacéutico completo

## 3. Roles que intervienen
- dueño titular
- administrador de finca
- operario autorizado
- veterinario

## 4. Disparador del proceso
Se inicia cuando se aplica o decide registrar un tratamiento sanitario en uno o varios animales.

## 5. Precondiciones
- finca activa
- animal existente y activo
- usuario con permiso
- producto o tratamiento existente, o creación permitida

## 6. Datos de entrada
- modalidad_registro
- animal o grupo
- tratamiento_o_producto
- fecha_aplicacion
- dosis
- duracion
- indicacion
- observacion_clinica
- aplicador

## 7. Campos exactos
- modalidad_registro
- finca_activa_mostrada
- animal
- grupo_animales
- tratamiento_o_producto
- fecha_aplicacion
- dosis
- duracion
- indicacion
- observacion_clinica
- aplicador
- usuario_registro
- fecha_hora_registro
- tipo_evento = tratamiento_sanitario

## 8. Obligatorios y opcionales
### Obligatorios
- modalidad_registro
- animal o grupo
- tratamiento_o_producto
- fecha_aplicacion

### Opcionales
- dosis
- duracion
- indicacion
- observacion_clinica
- aplicador

## 9. Valores por defecto
- modalidad_registro: individual
- fecha_aplicacion: hoy
- campos complementarios: vacíos

## 10. Reglas de validación
1. El animal debe existir y estar activo.
2. El tratamiento o producto es obligatorio.
3. La fecha de aplicación es obligatoria.
4. No se permite registrar tratamiento a animales inexistentes o inactivos.
5. En grupo, todos los seleccionados deben ser válidos.
6. Dosis y duración, si se registran, deben cumplir formato válido.
7. El flujo debe cerrar con confirmación final.
8. Si el tratamiento no existe, solo podrá crearse si la política lo permite.

## 11. Bloqueos
- no hay finca activa
- no hay animal ni grupo
- no hay tratamiento o producto
- no hay fecha
- el animal no existe
- el animal está inactivo
- usuario sin permiso

## 12. Advertencias
- faltan dosis o duración
- la fecha es muy antigua
- el caso puede requerir seguimiento posterior
- se está aplicando el mismo tratamiento en un período muy corto

## 13. Mensajes funcionales
- Debe seleccionar un tratamiento o producto.
- La fecha de aplicación es obligatoria.
- Debe seleccionar al menos un animal.
- No puede registrar tratamiento para un animal inactivo.
- El animal no existe en el sistema.
- No tiene permiso para registrar tratamientos.
- El tratamiento quedó registrado sin dosis.
- El tratamiento quedó registrado sin duración.

## 14. Flujo principal paso a paso
1. Entrar a Registrar.
2. Elegir Tratamiento sanitario.
3. Validar contexto y permisos.
4. Elegir modalidad.
5. Seleccionar animal o grupo.
6. Seleccionar tratamiento o producto.
7. Registrar fecha.
8. Registrar datos complementarios opcionales.
9. Mostrar resumen final.
10. Confirmar.
11. Guardar y actualizar historial.

## 15. Escenarios alternos
- creación contextual del tratamiento o producto
- registro sin dosis o sin duración
- exclusión de animales inválidos antes de confirmar, si luego se habilita esa política; por defecto bloquear

## 16. Correcciones y anulaciones
- corrección con trazabilidad obligatoria
- anulación sin borrado físico
- operario no anula
- administrador, dueño y veterinario autorizado sí pueden según política

## 17. Resultado esperado
Queda registrado un evento de tratamiento sanitario asociado al animal o grupo.

## 18. Impacto en historial
- tipo_evento = tratamiento_sanitario
- fecha_evento
- fecha_registro
- usuario_registro
- producto o tratamiento
- datos complementarios registrados

## 19. Impacto en estados derivados del animal
No cambia estado activo ni ubicación.
Puede alimentar:
- último tratamiento sanitario
- alertas futuras
- lectura clínica resumida

## 20. Observaciones de diseño funcional
- producto o tratamiento debe ser catálogo controlado
- no conviene bloquear por falta de datos complementarios en fase 2
- el veterinario debe poder figurar como aplicador aunque no sea el usuario que registra

## 21. Maestros requeridos
- **TratamientoTipo**: Catálogo de categorías de tratamientos (Ej: Antibiótico, Analgésico, Desparasitante, Vitamínico, Curación externa).
- **TratamientoProducto**: Catálogo de fármacos o insumos sanitarios, asociados obligatoriamente a un *TratamientoTipo*.
- **Diagnóstico/Enfermedad (Opcional)**: Catálogo de afecciones comunes para estandarizar el motivo del tratamiento.

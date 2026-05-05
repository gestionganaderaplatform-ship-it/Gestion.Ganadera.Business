# GANADERO SaaS · Proceso 06 · Muerte

## 1. Objetivo
Registrar la salida del animal por fallecimiento, dejando trazabilidad de la causa o motivo.

## 2. Alcance
Incluye identificación del animal, fecha de muerte, causa o motivo, observación opcional y cambio de condición de activo.

## 3. Roles que intervienen
- Dueño titular
- Administrador de finca
- Operario autorizado para reportar, si la política lo permite

## 4. Disparador del proceso
Se usa cuando un animal muere y debe quedar trazabilidad formal de su salida.

## 5. Precondiciones
- El animal existe.
- El animal estaba activo.
- El usuario tiene permiso.

## 6. Datos de entrada
- Animal
- Fecha de muerte
- Causa o motivo
- Observación opcional
- Reportado por, si aplica

## 7. Campos exactos
- animal
- fecha_muerte
- causa_o_motivo
- observacion
- reportado_por_opcional
- usuario_registro
- fecha_registro

## 8. Obligatorios y opcionales
### Obligatorios
- animal
- fecha_muerte
- causa_o_motivo

### Opcionales
- observacion
- reportado_por_opcional

## 9. Valores por defecto
- fecha_muerte = hoy
- fecha_registro = automática

## 10. Reglas de validación
- No se permite registrar muerte para animales inactivos.
- No se permite registrar muerte para animales ya vendidos.
- No se permite registrar muerte para animales ya reportados como muertos.
- Si no se conoce la causa exacta, se permite causa genérica o pendiente según política.

## 11. Bloqueos
- No hay animal seleccionado.
- No hay fecha de muerte.
- No hay causa o motivo.
- El animal está inactivo.
- El animal ya fue vendido.
- El animal ya fue reportado como muerto.

## 12. Advertencias
- La causa se registra como genérica o pendiente.
- La fecha del evento es muy antigua.
- Existe un patrón repetitivo de causa que amerita revisión posterior.

## 13. Mensajes funcionales
- Debe registrar una causa o motivo.
- La fecha de muerte es obligatoria.
- No puede registrar muerte para un animal inactivo.
- El animal ya fue reportado como muerto.
- El animal ya presenta una salida incompatible.
- Muerte registrada correctamente.

## 14. Flujo principal paso a paso
1. El usuario entra a Registrar.
2. Selecciona Muerte.
3. Identifica el animal.
4. Registra fecha de muerte.
5. Registra causa o motivo.
6. Registra observación si aplica.
7. El sistema muestra resumen final.
8. El usuario confirma.
9. El sistema registra la muerte.
10. El sistema saca al animal del inventario activo.

## 15. Escenarios alternos
- Si no se conoce la causa exacta, se usa causa genérica o pendiente.
- Si el animal ya no está activo, el sistema bloquea y obliga a revisar historial.
- Si hubo error, se corrige o anula según política.

## 16. Correcciones y anulaciones
- Deben quedar auditadas.
- Si la causa cambia, debe conservarse el valor anterior.
- La anulación debe controlarse si luego existen análisis o indicadores basados en la muerte registrada.

## 17. Resultado esperado
El animal sale del inventario activo y queda trazabilidad de muerte.

## 18. Impacto en historial
Genera evento de muerte con fecha, causa, observación, usuario y fecha de registro.

## 19. Impacto en estados derivados del animal
- condicion_activo = inactivo por muerte
- motivo_salida = muerte
- fecha_salida = fecha_muerte

## 20. Observaciones de diseño funcional
- La causa debe manejarse desde catálogo controlado con opción genérica o pendiente.
- El proceso no debe permitir duplicar una salida definitiva.
- Conviene permitir distinguir reportado por y registrado por cuando negocio lo necesite.

## Estado de implementación

### Endpoints
- `POST /api/v1/ganaderia/procesos/muerte` - Registro individual

### Modelo de request
```json
{
  "Animal_Codigo": 1,
  "Fecha_Muerte": "2026-05-02",
  "Causa": "Enfermedad",
  "Observacion": "Detalles de la muerte"
}
```

### Tablas afectadas
- `Evento_Ganadero` (tronco)
- `Evento_Ganadero_Animal` (relación)
- `Evento_Detalle_Muerte` (historial de muertes)
- `Animal` (actualiza `Animal_Activo = false`, `Animal_Fecha_Ultimo_Evento`)

### Estado
✅ Implementado - Backend completo

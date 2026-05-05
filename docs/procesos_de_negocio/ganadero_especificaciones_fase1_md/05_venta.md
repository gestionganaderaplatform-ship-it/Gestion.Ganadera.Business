# GANADERO SaaS · Proceso 05 · Venta

## 1. Objetivo
Registrar la salida del animal por comercialización, dejando trazabilidad del comprador o destino y sacándolo del inventario activo.

## 2. Alcance
Incluye venta individual o por lote, captura de comprador o destino, valor económico opcional y actualización de condición de activo.

## 3. Roles que intervienen
- Dueño titular
- Administrador de finca

## 4. Disparador del proceso
Se usa cuando uno o varios animales salen de la operación por venta.

## 5. Precondiciones
- Los animales existen.
- Los animales están activos.
- El usuario tiene permiso.

## 6. Datos de entrada
- Modalidad
- Animal o lote
- Fecha de venta
- Comprador o destino
- Valor económico opcional
- Observación opcional

## 7. Campos exactos
- modalidad_venta
- animales_seleccionados
- fecha_venta
- comprador_o_destino
- valor_total_opcional
- valor_por_animal_opcional
- observacion
- usuario_registro
- fecha_registro

## 8. Obligatorios y opcionales
### Obligatorios
- animal o lote
- fecha_venta
- comprador_o_destino

### Opcionales
- valor_total_opcional
- valor_por_animal_opcional
- observacion

## 9. Valores por defecto
- fecha_venta = hoy
- fecha_registro = automática

## 10. Reglas de validación
- No se permite vender animales inactivos.
- No se permite vender animales ya vendidos.
- No se permite vender animales con salida definitiva incompatible.
- En lote, se permiten datos comunes una sola vez.
- En lote, puede registrarse valor total o valor por animal.
- El valor económico es opcional y no bloquea.

## 11. Bloqueos
- No hay animal ni lote seleccionado.
- No hay fecha de venta.
- No hay comprador o destino.
- El animal está inactivo.
- El animal ya fue vendido.
- Existe inconsistencia de estado.

## 12. Advertencias
- El valor económico luce atípico.
- El lote seleccionado presenta diferencias relevantes.
- La fecha de venta es muy antigua según política.

## 13. Mensajes funcionales
- Debe registrar el comprador o destino.
- La fecha de venta es obligatoria.
- No puede vender un animal inactivo.
- El animal ya fue vendido.
- El animal presenta una inconsistencia de estado.
- Venta registrada correctamente.

## 14. Flujo principal paso a paso
1. El usuario entra a Registrar.
2. Selecciona Venta.
3. Elige modalidad individual o lote.
4. Identifica animales.
5. Registra comprador o destino.
6. Registra fecha.
7. Registra valor si aplica.
8. El sistema muestra resumen final.
9. El usuario confirma.
10. El sistema registra la venta.
11. El sistema saca los animales del inventario activo.

## 15. Escenarios alternos
- Si el usuario quiere vender varios juntos, usa modalidad lote.
- Si algún animal del lote ya no está activo, debe excluirse o bloquearse la confirmación.
- Si hubo error, se aplica corrección o anulación según rol.

## 16. Correcciones y anulaciones
- Deben quedar auditadas.
- Si existen procesos posteriores incompatibles, la anulación debe restringirse.
- La corrección debe conservar comprador, fecha y valor previo cuando cambien.

## 17. Resultado esperado
Los animales salen del inventario activo y queda trazabilidad de la venta.

## 18. Impacto en historial
Genera evento de venta con fecha, comprador o destino, valor si aplica, fecha de registro y usuario.

## 19. Impacto en estados derivados del animal
- condicion_activo = inactivo por venta
- motivo_salida = venta
- fecha_salida = fecha_venta
- potrero_actual = sin valor operativo o último conocido histórico

## 20. Observaciones de diseño funcional
- La salida por venta debe ser definitiva en fase 1.
- El sistema debe protegerse de ventas duplicadas.
- La trazabilidad del comprador o destino no debe quedar opcional.

## Estado de implementación

### Endpoints
- `POST /api/v1/ganaderia/procesos/venta` - Registro individual
- `POST /api/v1/ganaderia/procesos/venta/lote` - Registro por lote

### Modelo de request (individual)
```json
{
  "Animal_Codigo": 1,
  "Fecha_Venta": "2026-05-02",
  "Comprador": "Juan Pérez",
  "Valor": 150000.00,
  "Observacion": "Venta de控制"
}
```

### Modelo de request (lote)
```json
{
  "Fecha_Venta": "2026-05-02",
  "Comprador": "Juan Pérez",
  "Valor_Total": 450000.00,
  "Observacion": "Venta lote",
  "Animales": [
    { "Animal_Codigo": 1, "Valor": 150000.00 },
    { "Animal_Codigo": 2, "Valor": 150000.00 },
    { "Animal_Codigo": 3, "Valor": 150000.00 }
  ]
}
```

### Tablas afectadas
- `Evento_Ganadero` (tronco)
- `Evento_Ganadero_Animal` (relación)
- `Evento_Detalle_Venta` (historial de ventas)
- `Animal` (actualiza `Animal_Activo = false`, `Animal_Fecha_Ultimo_Evento`)

### Estado
✅ Implementado - Backend completo

# GANADERO SaaS · Proceso 04 · Pesaje

## 1. Objetivo
Registrar el peso de uno o varios animales para conservar trazabilidad de crecimiento y control operativo.

## 2. Alcance
Incluye pesaje individual y secuencia por grupo, con actualización de última referencia de peso del animal.

## 3. Roles que intervienen
- Dueño titular
- Administrador de finca
- Operario autorizado

## 4. Disparador del proceso
Se usa cuando se pesa uno o varios animales.

## 5. Precondiciones
- El animal existe.
- El animal está activo.
- El usuario tiene permiso.

## 6. Datos de entrada
- Modalidad
- Animal o grupo
- Fecha del pesaje
- Peso
- Observación opcional

## 7. Campos exactos
- modalidad_pesaje
- animal_o_grupo
- fecha_pesaje
- peso
- observacion
- usuario_registro
- fecha_registro

## 8. Obligatorios y opcionales
### Obligatorios
- animal o grupo
- fecha_pesaje
- peso

### Opcionales
- observacion

## 9. Valores por defecto
- fecha_pesaje = hoy
- fecha_registro = automática

## 10. Reglas de validación
- No se permite pesar animales inactivos.
- No se permite pesar animales no existentes.
- El peso debe ser numérico y cumplir la regla mínima aceptada por el sistema.
- En modalidad grupo, la captura se mantiene secuencial animal por animal.
- En modalidad grupo, todos comparten la fecha si el usuario no la cambia.
- Si el peso luce atípico, el sistema advierte sin bloquear salvo reglas futuras.
- **Límite de peso: mínimo 5 kg, máximo 1500 kg.**

## 11. Bloqueos
- No hay animal ni grupo seleccionado.
- No hay fecha.
- No hay peso.
- El peso no es válido.
- El animal no existe.
- El animal está inactivo.

## 12. Advertencias
- El peso luce atípico respecto del historial previo.
- El peso rompe una tendencia esperada.
- La fecha del pesaje es muy antigua.

## 13. Mensajes funcionales
- Debe registrar un peso válido.
- Debe seleccionar un animal.
- No puede pesar un animal inactivo.
- El animal no existe en el sistema.
- La fecha del pesaje es obligatoria.
- Pesaje registrado correctamente.

## 14. Flujo principal paso a paso
1. El usuario entra a Registrar.
2. Selecciona Pesaje.
3. Elige modalidad individual o grupo.
4. Registra fecha.
5. Identifica animal o grupo.
6. Registra peso.
7. El sistema muestra resumen final.
8. El usuario confirma.
9. El sistema guarda el evento.
10. El sistema actualiza la última referencia de peso.

## 15. Escenarios alternos
- Si el animal no aparece, no se registra peso hasta que exista en el sistema.
- Si se pesan varios animales seguidos, se mantiene una secuencia fluida de captura.
- Si el peso parece atípico, el sistema advierte.

## 16. Correcciones y anulaciones
- Se permiten según rol y política.
- Deben quedar trazadas las diferencias entre valor anterior y nuevo.
- La anulación debe controlarse si ya existen cálculos posteriores que dependan de ese dato.

## 17. Resultado esperado
El peso queda registrado y actualizado como última referencia del animal.

## 18. Impacto en historial
Genera evento de pesaje con fecha del evento, fecha de registro, usuario y peso registrado.

## 19. Impacto en estados derivados del animal
- **Animal_Peso** = peso registrado
- **Animal_Fecha_Peso** = fecha_pesaje
- condicion_activo = sin cambio
- potrero_actual = sin cambio

## 20. Observaciones de diseño funcional
- El proceso debe priorizar velocidad operativa.
- Conviene mostrar contexto básico del animal antes de guardar.
- El sistema no debe bloquear por atípicos salvo que negocio defina umbrales duros después.

## 21. Implementación técnica

### Endpoints
- `POST /api/v1/ganaderia/procesos/pesaje` - Registro individual
- `POST /api/v1/ganaderia/procesos/pesaje/lote` - Registro por grupo

### Modelo de request (individual)
```json
{
  "Animal_Codigo": 1,
  "Fecha_Pesaje": "2026-05-02",
  "Peso": 350.00,
  "Observacion": "Pesaje de control"
}
```

### Modelo de request (lote)
```json
{
  "Fecha_Pesaje": "2026-05-02",
  "Observacion": "Pesaje routine",
  "Animales": [
    { "Animal_Codigo": 1, "Peso": 350.00 },
    { "Animal_Codigo": 2, "Peso": 380.00 }
  ]
}
```

### Tablas afectadas
- `Evento_Ganadero` (tronco)
- `Evento_Ganadero_Animal` (relación)
- `Evento_Detalle_Pesaje` (historial de todos los pesajes)
- `Animal` (actualiza `Animal_Peso` = último peso, `Animal_Fecha_Peso` = fecha del último peso)

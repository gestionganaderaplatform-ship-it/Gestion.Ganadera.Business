# GANADERO SaaS · Proceso 07 · Traslado entre fincas

## 1. Objetivo
Registrar el paso de uno o varios animales desde una finca a otra dentro de la misma cuenta, sin romper continuidad de historial.

## 2. Alcance
Incluye traslado individual o por lote, selección de finca destino, potrero destino, creación contextual de potrero y actualización de finca actual y potrero actual.

## 3. Roles que intervienen
- Dueño titular
- Administrador con permiso suficiente

## 4. Disparador del proceso
Se usa cuando el ganado cambia de finca dentro de la misma cuenta.

## 5. Precondiciones
- La cuenta tiene más de una finca.
- Existe al menos una finca destino válida distinta de la finca activa.
- Los animales existen y están activos.
- El usuario tiene permiso.

## 6. Datos de entrada
- Modalidad
- Animal o lote
- Finca destino
- Potrero destino
- Fecha de traslado
- Observación opcional

## 7. Campos exactos
- modalidad_traslado
- animales_seleccionados
- finca_origen_mostrada
- finca_destino
- potrero_destino
- fecha_traslado
- observacion
- usuario_registro
- fecha_registro

## 8. Obligatorios y opcionales
### Obligatorios
- animal o lote
- finca_destino
- potrero_destino
- fecha_traslado

### Opcionales
- observacion

## 9. Valores por defecto
- finca_origen = finca activa
- fecha_traslado = hoy
- fecha_registro = automática

## 10. Reglas de validación
- No se permite trasladar animales inactivos.
- No se permite trasladar animales a la misma finca actual.
- No se permite trasladar animales con inconsistencias de estado.
- El proceso no aplica si la cuenta solo tiene una finca o no existe destino válido.
- En lote, todos comparten finca destino y fecha si el usuario no cambia algo.
- Si el potrero destino no existe, puede crearse dentro del flujo con solo nombre.
- La identidad del animal no se rompe.

## 11. Bloqueos
- No hay animal ni lote seleccionado.
- No hay finca destino.
- No hay potrero destino.
- No hay fecha de traslado.
- La finca destino es igual a la actual.
- El animal está inactivo.
- Existe inconsistencia de estado.
- No existe una finca destino válida.

## 12. Advertencias
- El traslado involucra un lote muy grande.
- La fecha del traslado es muy antigua.
- La finca destino presenta condiciones que ameritan revisión futura.

## 13. Mensajes funcionales
- Debe seleccionar una finca destino.
- Debe seleccionar un potrero destino.
- No puede trasladar un animal inactivo.
- La finca destino no puede ser la misma finca actual.
- No existe una finca destino válida para este traslado.
- Traslado registrado correctamente.

## 14. Flujo principal paso a paso
1. El usuario entra a Registrar.
2. Selecciona Traslado entre fincas.
3. Elige modalidad individual o lote.
4. Identifica animales.
5. Selecciona finca destino.
6. Selecciona potrero destino.
7. Registra fecha.
8. El sistema muestra resumen final.
9. El usuario confirma.
10. El sistema registra el traslado.
11. El sistema actualiza finca actual y potrero actual.

## 15. Escenarios alternos
- Si la finca destino no tiene potreros listos, se permite creación contextual.
- Si el usuario intenta trasladar animales inactivos, el sistema lo impide.
- Si la cuenta solo tiene una finca, el proceso no aplica.

## 16. Correcciones y anulaciones
- Se permiten según rol y política.
- Debe conservarse continuidad histórica del animal.
- Si existen eventos posteriores en la finca destino, la anulación debe controlarse estrictamente.

## 17. Resultado esperado
Los animales conservan su identidad e historial, pero cambian de finca actual y potrero actual.

## 18. Impacto en historial
Genera evento de traslado con finca origen, finca destino, potrero destino, fecha del evento, fecha de registro y usuario.

## 19. Impacto en estados derivados del animal
- finca_actual = finca destino
- potrero_actual = potrero destino
- condicion_activo = sin cambio
- identidad_animal = sin cambio

## 20. Observaciones de diseño funcional
- Este proceso no debe confundirse con salida definitiva.
- La continuidad del historial es obligatoria.
## 21. Estado de Implementación

- **Backend (Business API)**: ✅ Completo. Implementado con soporte para transacciones atómicas y actualización de estado derivado (`Finca_Codigo` y `Potrero_Codigo`).
- **Frontend (Web)**: ⚠️ Parcial. Esqueleto base disponible, pendiente integración con los nuevos endpoints de la API.
- **Última actualización técnica**: 2026-05-04.

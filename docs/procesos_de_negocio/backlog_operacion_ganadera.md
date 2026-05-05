# Backlog de Operacion Ganadera - Business API

Este backlog concentra el trabajo del dominio y backend de Ganaderia.
No intenta repetir Auth ni Web. Se usa junto con los otros dos backlogs para cerrar cada proceso end-to-end.

## Forma de trabajo end-to-end

Cada proceso nuevo se trabaja en este orden:

1. cerrar contratos y validaciones del backend
2. exponer endpoints y permisos requeridos
3. conectar frontend con modelos y servicios
4. construir pantalla o flujo visible
5. probar flujo completo con datos reales
6. ajustar documentacion si algo cambio

La regla es simple:
no abrir el siguiente proceso hasta que el actual tenga backend utilizable y frontend conectado.

## Estado actual del backend

### Base transversal
- [x] Catalogos base por cliente
- [x] Consulta de ganado
- [x] Ficha basica del animal
- [x] Historial basico del animal
- [x] Validacion de registro existente
- [x] Registro existente
- [x] Compra
- [x] Compra
- [x] Movimiento de potrero (individual + lote)

### Ajustes recientes ya cerrados
- [x] Crear identificador interno del sistema en registro existente
- [x] Crear identificador interno del sistema en compra
- [x] Validar compatibilidad categoria y sexo
- [x] Restringir un solo identificador principal activo por animal
- [x] Quitar `Hierro` como tipo visible en registro existente
- [x] Dejar `Generacion automatica` e `Identificador propio` como tipos operativos
- [x] Consultar siguiente consecutivo por finca para identificacion automatica
- [x] Tomar marca ganadera del cliente como base del identificador automatico

### Deuda tecnica inmediata
- [ ] Revisar y unificar la consulta de siguiente consecutivo para identificacion automatica. No debe seguir replicada por proceso si ya existe un servicio o endpoint transversal reutilizable.

## Procesos Fase 1

### Proceso 1. Registro de existente
- [x] Endpoint de validacion
- [x] Endpoint de registro
- [x] Persistencia atomica de animal, identificadores, evento y detalle
- [x] Actualizacion de snapshot del animal
- [x] Impacto en historial
- [x] Documento vivo del proceso en `registro_existente_estado_actual.md`
- [x] Ajuste de tipos visibles de identificacion para este proceso
- [x] Salida de `Hierro` como tipo visible
- [x] Entrada de `Generacion automatica` e `Identificador propio`
- [x] Consulta del siguiente consecutivo por finca
- [x] Uso de marca ganadera del cliente para la generacion automatica
- [x] Entregar desde backend la banda de edad esperada por categoria para no calcularla en frontend
- [ ] Validacion manual completa con payload real
- [ ] Cerrar captura de marcas ganaderas en la experiencia de configuracion/onboarding
- [ ] Cerrar del todo la recuperacion del onboarding cuando la finca exista pero la base operativa quede pendiente
- [ ] Validar si el consecutivo por finca queda con conteo operativo o con secuencia dedicada
- [ ] Definir entrada futura de RFID escaneado

### Proceso 2. Compra
- [x] Endpoint de validacion
- [x] Endpoint de registro
- [x] Persistencia atomica de animal, identificadores, evento y detalle
- [x] Actualizacion de snapshot del animal
- [x] Impacto en historial
- [ ] Validacion manual completa con payload real

### Proceso 3. Nacimiento y Muerte
- [x] Endpoints de registro (Nacimiento/Muerte)
- [x] Persistencia atomica y detalles del evento
- [x] Actualizacion automatica de snapshot (Nacimiento: crea animal / Muerte: inactiva)
- [x] Impacto en historial y trazabilidad

### Proceso 4. Movimiento de potrero y Traslado
- [x] Definir request y response
- [x] Crear validador
- [x] Crear detalle del proceso
- [x] Implementar transaccion
- [x] Actualizar snapshot del animal
- [x] Impactar historial
- [x] Endpoint lote (Movimiento)
- [x] Endpoint Traslado entre fincas

## Procesos Fase 2 (Backend ✅)

### Tratamiento, Palpación y Destete
- [x] Maestros y catálogos operativos
- [x] Endpoints de validación y registro
- [x] Persistencia de detalles técnicos
- [x] Actualización de estados derivados en ficha del animal

### Descarte
- [x] Maestros de motivos de descarte
- [x] Endpoint de registro
- [x] Inactivación de animal y registro de salida

## Siguiente bloque recomendado

1. Probar de punta a punta procesos de Fase 1 con payload real en Web
2. Cerrar captura y edicion de marcas ganaderas del cliente en la experiencia
3. **Cerrar Backend: Cambio de categoría** (Único pendiente de lógica core)
4. Iniciar conexión de servicios de Fase 2 en Frontend (Web)

## Pendiente para etapa final

- [ ] Evaluar modulo de analisis sugeridos para `Inicio`
- [ ] Mantener por ahora solo indicadores y lecturas operativas sustentadas en reglas y datos reales
- [ ] Dejar la capa de IA como fase posterior, cuando ya esten cerrados compra, venta, movimientos y capacidad de potreros
- [ ] Si se retoma, construirla desde backend como hallazgos o sugerencias y no como calculo de negocio en frontend

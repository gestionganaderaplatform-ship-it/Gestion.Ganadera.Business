# Estandar reutilizable para procesos ganaderos

## Objetivo

Dejar documentado un criterio transversal para construir procesos ganaderos sin diseñar cada flujo desde cero.

Este documento no reemplaza la especificacion de cada proceso.
Sirve como base comun para `Registro de existente`, `Compra`, `Venta`, `Movimiento de potrero` y otros procesos donde aplique trabajo por grupos.

## Idea principal

Primero se captura lo comun del proceso.
Despues se captura lo particular de cada animal.

La meta es reducir repeticion, acelerar la operacion y hacer que el flujo se sienta natural para el ganadero.

## Plantilla base del flujo

### 1. Entrada

El usuario entra por `Registrar` y elige el proceso.

### 2. Alcance

El sistema define si trabajara:

- un animal
- varios animales

### 3. Agrupacion

Si son varios, el sistema debe agruparlos segun la logica natural del proceso.

La agrupacion no siempre es la misma.
Depende del tipo de proceso.

### 4. Datos comunes

Se pide una sola vez lo que comparten todos los animales del grupo.

### 5. Lista de animales

Se arma la lista de animales afectados o a registrar.

### 6. Datos particulares

Solo se pide por animal lo que realmente cambia.

### 7. Revision y confirmacion

Se revisa el grupo, se revisan los animales y luego se confirma una sola vez.

## Regla de diseño

Cada proceso debe responder estas dos preguntas antes de construirse:

1. Que datos son comunes
2. Que datos son particulares

Si un dato es comun, no debe repetirse por cada animal.

## Agrupador natural segun proceso

### Registro de existente

El agrupador natural es el `potrero`.

### Compra

El agrupador natural suele ser el `lote de compra`, `proveedor` o `origen comun`.

### Venta

El agrupador natural suele ser el `comprador`, `salida` o `destino comun`.

### Movimiento de potrero

El agrupador natural suele ser el `potrero destino`.

### Traslado entre fincas

El agrupador natural suele ser la `finca destino` y el `potrero destino`.

### Vacunacion

El agrupador natural suele ser la `vacuna`, la `fecha` y el `aplicador`.

### Tratamiento sanitario

El agrupador natural suele ser el `tratamiento`, la `fecha` y el `responsable`, cuando aplica trabajo grupal.

## Tipos de proceso

### Procesos de grupo fuerte

Son procesos donde primero se captura el contexto comun y luego se baja al detalle individual.

Ejemplos:

- Registro de existente
- Compra
- Venta
- Movimiento de potrero
- Traslado entre fincas
- Vacunacion
- Descarte

### Procesos mixtos

Tienen datos comunes, pero tambien bastante dato particular por animal.

Ejemplos:

- Pesaje
- Tratamiento sanitario
- Cambio de categoria

### Procesos principalmente individuales

No conviene forzarlos a una logica de grupo grande.

Ejemplos:

- Nacimiento
- Destete
- Palpacion o revision reproductiva

## Regla tecnica transversal

Cuando aplique este estandar, el proceso debe intentar conservar la misma disciplina:

- validacion previa
- captura de datos comunes
- captura de datos particulares
- confirmacion final
- evento de negocio
- impacto en snapshot
- impacto en historial
- auditoria

## Regla funcional transversal

No diseñar procesos por CRUD ni por entidad aislada.

Diseñar procesos por:

- contexto comun
- detalle individual
- resultado operativo

## Conclusion operativa

El patron reusable no es copiar la misma pantalla para todos los procesos.

El patron reusable es este:

primero contexto comun, despues detalle por animal, luego revision y confirmacion.

Cada proceso debe cambiar su agrupador natural, pero no perder esa logica base.

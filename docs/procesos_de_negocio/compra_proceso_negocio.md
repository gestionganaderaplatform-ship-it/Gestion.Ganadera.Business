# Compra · proceso de negocio

## Proposito

`Compra` permite registrar el ingreso de un animal que entra a la operacion porque fue adquirido a un tercero.

No representa una carga inicial, un nacimiento ni un movimiento interno. Representa un evento nuevo de ingreso con trazabilidad comercial y operativa.

## Resultado de negocio esperado

Al terminar el proceso:

- el animal queda creado en el inventario activo de la finca
- el animal queda asociado a un potrero destino
- el animal queda clasificado por categoria
- el animal queda con identificacion principal valida
- el historial operativo refleja que el origen del ingreso fue `Compra`
- el evento conserva fecha y referencia del origen o vendedor

## Cuando aplica

Este proceso aplica cuando el cliente:

- compra un animal y necesita dejarlo activo en su inventario
- requiere trazar de donde vino el animal y en que fecha ingreso
- necesita separar este ingreso de otros origenes como nacimiento o carga inicial

## Cuando no aplica

No debe usarse para:

- cargar animales que ya estaban en la finca antes de usar la plataforma
- registrar nacimientos
- mover animales entre potreros
- trasladar animales entre fincas
- corregir animales ya existentes si el caso real es una edicion posterior

## Diferencia frente a Registro de existente

`Registro de existente` resuelve una base inicial del inventario.

`Compra` registra un evento nuevo de ingreso que si ocurrio durante la operacion y por eso exige contexto del origen o vendedor y fecha de compra.

## Unidad operativa visible

La unidad operativa visible sigue siendo el `potrero` destino dentro de la finca activa.

La compra no pide sexo como decision visible principal.
La clasificacion visible se concentra en la `categoria`.

## Datos minimos que el proceso debe dejar resueltos

Cada compra debe dejar, como minimo:

- finca
- fecha de compra
- origen o vendedor
- potrero destino
- categoria animal
- identificador principal
- origen del ingreso

## Regla funcional de clasificacion

La categoria sigue siendo el dato visible principal.

Sexo y rango de edad esperado se derivan desde la metadata de la categoria entregada por backend.

Eso significa:

- el usuario no decide sexo como paso principal
- el usuario no decide rango de edad como dato principal
- la categoria concentra la clasificacion operativa visible

## Alcance actual del proceso

La version actual del backend registra una compra por animal.

No existe todavia una operacion por lote para varios animales dentro de una sola compra.

Por eso la aplicacion debe comportarse hoy como compra individual y no simular un lote que la API aun no soporta.

## Identificacion del animal

La compra usa el tipo de identificador visible que el negocio permita para el animal.

La regla de duplicidad del identificador debe mantenerse alineada entre:

- documento vivo
- validator final
- repositorio
- prevalidacion de frontend

## Regla sobre fechas

La fecha clave del proceso es la `fecha de compra`.

Debe validarse por dia calendario y no por desfase horario entre cliente, serializacion y backend.

## Relacion con catalogos operativos

El proceso depende de catalogos operativos vivos, especialmente:

- potrero
- categoria animal
- tipo de identificador

`Potrero` debe poder resolverse dentro del flujo para no obligar al usuario a salir del proceso si detecta que le falta uno.

## Riesgos que el proceso debe evitar

- tratar una compra como si fuera una carga inicial
- pedir decisiones tecnicas que ya pueden derivarse por categoria
- dejar pasar identificadores duplicados
- romper la trazabilidad del origen comercial del ingreso
- prometer compra por lote cuando backend aun no la soporta

## Relacion con otros procesos

`Compra` comparte piezas de captura con otros procesos, pero representa un evento de negocio distinto a:

- registro de existente
- nacimiento
- movimiento de potrero
- traslado entre fincas

La reutilizacion de componentes no debe borrar esa diferencia funcional.

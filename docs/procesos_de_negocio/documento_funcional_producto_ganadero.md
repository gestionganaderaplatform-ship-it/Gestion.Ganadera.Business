# GANADERO SaaS
## Documento Funcional Base de Producto · v3.0
### Versión integral para definición de negocio, diseño funcional y preparación de construcción

---

## 1. Propósito del documento

Este documento define la base funcional del producto GANADERO SaaS con un nivel de detalle suficiente para orientar negocio, experiencia, reglas de operación y posterior diseño técnico.

Su objetivo no es solo describir una idea general del sistema. Su propósito es dejar una versión más madura, cuestionada y accionable que permita:

- entender con precisión qué resuelve el producto
- definir qué se construye y en qué orden
- establecer reglas de negocio base
- documentar procesos paso a paso
- dejar claras las decisiones ya aprobadas
- reducir ambigüedad antes de bajar a modelo de datos y arquitectura

Este documento reemplaza la necesidad de seguir trabajando sobre una versión demasiado conceptual. A partir de aquí, la discusión debe centrarse en reglas, flujos, permisos, estados y comportamiento real del sistema.

---

## 2. Problema de negocio que resuelve el producto

GANADERO SaaS busca resolver un problema operativo y de control: en muchas fincas la información del ganado se maneja de forma dispersa, tardía, incompleta o dependiente de la memoria de las personas.

Eso genera dificultades para responder preguntas básicas como:

- cuántos animales hay realmente
- dónde está cada animal
- qué ha pasado con cada uno
- qué animales entraron, salieron o cambiaron de ubicación
- qué decisiones requieren atención hoy
- quién registró cada evento y cuándo lo hizo

El sistema no se concibe como un inventario estático ni como un software de mantenimiento de entidades. Se concibe como un sistema de registro de eventos de negocio sobre el ganado, donde cada evento actualiza automáticamente la realidad operativa del animal y de la finca.

---

## 3. Visión funcional del producto

La lógica central del producto es simple:

- algo ocurre en la operación real
- el usuario registra el evento
- el sistema actualiza automáticamente la situación del animal
- el historial conserva la trazabilidad completa
- las vistas principales permiten consultar, entender y actuar

El usuario no debería tener que editar manualmente la realidad operativa del animal cuando esa realidad puede derivarse de los procesos registrados.

Por eso, el sistema debe estar gobernado por procesos, no por formularios de mantenimiento.

---

## 4. Principios rectores

### 4.1 El proceso manda
La operación del sistema gira alrededor de procesos de negocio y no alrededor de módulos administrativos o tablas.

### 4.2 Registrar es el punto formal de ejecución
Los procesos se ejecutan desde Registrar. Otras vistas pueden dar contexto, buscar, filtrar o conducir hacia Registrar, pero no deben convertirse en puntos paralelos de operación.

### 4.3 La información mínima útil primero
Cada flujo debe pedir solo la información necesaria para dejar correctamente registrado el evento y producir valor inmediato.

### 4.4 El estado se deriva de eventos
La condición actual del animal, su ubicación, su categoría activa y otras situaciones relevantes deben resultar de los eventos registrados y no de edición manual dispersa.

### 4.5 La trazabilidad no se negocia
Todo evento relevante debe dejar rastro: qué pasó, cuándo pasó, cuándo se registró y quién lo registró. Las correcciones y anulaciones también deben quedar auditadas.

### 4.6 La web sale primero
La web será el primer canal completo del producto. La app vendrá después como herramienta especializada para terreno y procesos transaccionales de alta frecuencia.

### 4.7 La experiencia debe soportar la realidad operativa
El producto debe funcionar bien bajo condiciones reales: jornadas de trabajo, ritmo operativo, usuarios no técnicos, registros posteriores al evento y necesidad de velocidad.

### 4.8 Registrar debe sentirse como un punto único y guiado
Registrar no es solo una pantalla más. Debe comportarse como el punto central de entrada a los procesos, con lenguaje claro, agrupación por tipo de proceso y ayudas visibles para que el usuario sepa qué hacer según su contexto.

Inicio, alertas, Ganado y Ficha pueden conducir hacia Registrar, pero la ejecución formal del proceso sigue ocurriendo allí. La experiencia debe orientar, no obligar al usuario a navegar por módulos técnicos ni a decidir entre varios puntos alternos de operación.

---

## 5. Alcance de esta versión del documento

Este documento sí define:

- modelo funcional base del producto
- alcance de construcción por fases
- estructura principal de navegación
- usuarios y roles base
- reglas funcionales transversales
- vistas principales del sistema
- procesos de negocio documentados paso a paso
- criterios de validación y control para la primera etapa

Este documento no baja todavía a:

- modelo de datos físico
- contratos API
- diseño visual final
- arquitectura técnica detallada
- diseño de sincronización offline
- reglas matemáticas avanzadas de indicadores productivos

---

## 6. Modelo de cuenta, finca y ganado

### 6.1 Cuenta
La cuenta representa al cliente dentro del sistema. Es el nivel general donde vive la administración principal, la configuración base y el gobierno general del producto para ese cliente.

### 6.2 Dueño titular
Cada cuenta tiene un único dueño titular. Este es el responsable principal de la cuenta, de sus decisiones sensibles y de la configuración general autorizada.

### 6.3 Finca
Una cuenta puede tener una o varias fincas. La finca es el contexto operativo real donde sucede la gestión del ganado.

### 6.4 Finca activa
La operación diaria se realiza dentro del contexto de una finca activa. Esto evita pedir finca en todos los flujos.

### 6.5 Ganado
El ganado corresponde a los animales gestionados dentro de la operación. La finca es el contexto; el ganado es el conjunto de animales sobre el cual recaen los procesos.

### 6.6 Participantes por finca
Una finca puede tener varios participantes asociados. Cada participante entra con un rol y con un alcance definido.

### 6.7 Terceros asociados al ganado
Puede haber terceros relacionados con el ganado dentro de la operación, aunque no sean el dueño titular de la cuenta. Más adelante se definirá si esto será solo una referencia operativa o si tendrá implicaciones funcionales adicionales.

---

## 7. Roles base del sistema

### 7.1 Dueño titular
Tiene control general sobre la cuenta, sus fincas, reglas principales, visibilidad global y decisiones sensibles.

### 7.2 Administrador de finca
Opera una finca con capacidades amplias de registro, consulta y control operativo, sin ser el dueño titular de la cuenta.

### 7.3 Operario de campo
Ejecuta procesos operativos definidos y consulta solo lo necesario para registrar correctamente su trabajo.

### 7.4 Veterinario
Ejecuta o consulta procesos sanitarios y reproductivos según el alcance que se le asigne.

### 7.5 Regla base de permisos
La matriz exacta de permisos se detallará después, pero ya quedan definidas estas bases:

- el operario no anula eventos
- el operario no gobierna configuración
- el administrador puede corregir o anular eventos dentro de la política permitida
- el dueño titular puede corregir o anular eventos según la política superior definida para la cuenta

---

## 8. Estrategia de construcción por fases

### 8.1 Criterio general
El producto se desarrollará en dos fases principales. Esto no significa que al cerrar la fase 1 el producto se lleve automáticamente a producción. Significa que primero se construye, prueba y revisa una base funcional controlada antes de ampliar el alcance.

### 8.2 Fase 1 de construcción
La fase 1 debe incluir:

- Inicio
- Ganado
- Ficha del animal
- Registrar
- Historial
- Reportes básicos
- Registro de existente
- Compra
- Pesaje
- Movimiento de potrero
- Venta
- Muerte
- Traslado entre fincas

### 8.3 Fase 2 de construcción
La fase 2 debe incluir:

- Vacunación
- Tratamiento sanitario
- Palpación o revisión reproductiva
- Destete
- Cambio de categoría
- Descarte
- Nacimiento, si no se incorpora antes

### 8.4 Regla de avance
La fase 1 debe cerrarse funcionalmente, probarse y revisarse antes de continuar con la fase 2.

---

## 9. Web y app

### 9.1 Web como canal inicial completo
La web será el primer canal del producto y debe soportar la operación principal, la consulta, la administración, la configuración y la supervisión.

### 9.2 App como herramienta posterior especializada
La app se incorporará después, enfocada en procesos de alta frecuencia en terreno, rapidez de captura y movilidad.

### 9.3 Criterio de paso a app
Un proceso tiene afinidad a app cuando:

- ocurre normalmente en terreno
- se repite mucho
- necesita rapidez
- se beneficia de captura simple
- puede aprovechar QR, RFID, cámara o trabajo móvil

---

## 10. Estructura principal del sistema

### 10.1 Inicio
Pantalla de entrada operativa y de seguimiento.

### 10.2 Ganado
Vista principal de consulta del ganado activo.

### 10.3 Ficha del animal
Vista de contexto, situación actual e historial resumido del animal.

### 10.4 Registrar
Único punto formal de ejecución de procesos.

### 10.5 Historial
Consolida los eventos registrados y su trazabilidad.

### 10.6 Reportes
Responde preguntas frecuentes mediante salidas simples y útiles.

### 10.7 Seguridad
Concentra auditoría, sesiones y control de acceso cuando aplique.

### 10.8 Configuración
Agrupa reglas y catálogos realmente necesarios para operar.

---

## 11. Vistas principales

### 11.1 Inicio

#### Propósito
Responder rápidamente:

- cómo está el ganado hoy
- qué requiere atención
- qué pasó recientemente

#### Debe mostrar

- resumen actual del ganado en la finca activa
- alertas accionables
- actividad reciente
- acceso visible a Registrar
- accesos a vistas principales

#### Regla aprobada
Las alertas del Inicio deben mostrar qué requiere atención y permitir entrar al flujo correcto en Registrar.

---

### 11.2 Ganado

#### Propósito
Ser la vista principal de consulta del ganado activo.

#### Debe permitir

- listar animales activos
- buscar por identificador principal o alterno
- filtrar por finca, categoría, sexo, potrero y otras condiciones relevantes
- entrar a la ficha del animal

#### Regla aprobada
- Ganado no ejecuta procesos. Su función es consultar, ubicar y entender animales.
- La búsqueda debe ser global y ejecutarse en el servidor para garantizar que se encuentre cualquier animal en la finca, sin importar la página actual.
- El ordenamiento de identificadores debe ser "natural" (ej. 72XL-2 antes de 72XL-10) para facilitar la lectura operativa.

---

### 11.3 Ficha del animal

#### Propósito
Dar contexto funcional completo del animal sin convertirse en una pantalla de mantenimiento operativo.

#### Debe mostrar como mínimo

- identificación principal y secundaria
- categoría y sexo
- finca actual
- potrero actual
- condición general actual
- relación madre-cría cuando aplique
- historial resumido

#### Regla aprobada
Desde la ficha se puede continuar hacia Registrar con el animal preseleccionado.

---

### 11.4 Registrar

#### Propósito
Ser el punto único de entrada a los procesos.

#### Regla aprobada
Registrar debe organizarse por tipos de proceso y no por formularios ni por entidades.

#### Agrupaciones iniciales sugeridas

- ingreso de animales
- pesaje
- movimiento
- salida
- sanidad
- reproducción

---

### 11.5 Historial

#### Propósito
Conservar y exponer la trazabilidad del sistema.

#### Debe permitir

- ver historial general
- ver historial por animal
- ver historial por proceso o tipo de evento

#### Regla aprobada
El historial debe mostrar evento, fecha del evento, fecha de registro cuando difiera, usuario que registró y correcciones o anulaciones si existieron.

---

### 11.6 Reportes

#### Reportes base de fase 1

- estado actual del ganado
- entradas y salidas por período
- historial de un animal

---

## 12. Reglas funcionales transversales ya aprobadas

### 12.1 Registrar procesos
Registrar es el único punto formal de ejecución de procesos.

### 12.2 Ficha del animal
La ficha no gestiona procesos directamente.

### 12.3 Ganado
La vista Ganado no es un punto alterno de ejecución.

### 12.4 Individual o lote/grupo
En los procesos donde aplique, una de las primeras decisiones debe ser si el registro es individual o por lote o grupo.

### 12.5 Fecha anterior
Los eventos podrán registrarse con fecha anterior dentro de límites parametrizables por cliente y controlados por rol.

### 12.6 Parámetros por cuenta
Las reglas generales se configuran por cuenta. Las excepciones por finca solo existen en casos puntuales y justificados.

### 12.7 Potrero actual
El potrero actual del animal es consecuencia del último movimiento de potrero y no se edita manualmente por fuera del proceso.

### 12.8 Categoría actual
La categoría actual del animal solo cambia por proceso y no por edición manual.

### 12.9 Condición de activo
La condición de activo del animal se determina automáticamente por sus eventos y no por un estado manual.

### 12.10 Identificación
Un animal puede tener varios identificadores y uno se define como principal para búsqueda y visualización.

### 12.11 Identificador mínimo
Para crear un animal debe existir al menos un identificador operativo.

### 12.12 Creación dentro del flujo
Las entidades de selección necesarias para completar un proceso deben poder crearse dentro del flujo cuando sea necesario, sin obligar al usuario a salir a configuración.

### 12.13 Onboarding base
El onboarding debe dejar configurada la base inicial de la finca, pero eso no elimina la creación contextual dentro de los procesos.

### 12.14 Carga masiva
La carga masiva se permite en onboarding y también después en procesos donde realmente tenga sentido.

### 12.15 Corrección y anulación
La corrección o anulación de eventos está controlada por rol y siempre deja trazabilidad.

### 12.16 Relación madre-cría
Debe existir desde la fase 1 al menos de forma básica.

### 12.17 Grupo y lote
Grupo es una agrupación temporal y operativa para procesos. Lote es una agrupación más estable asociada a origen, compra o criterio de negocio.

---

## 13. Identidad y estructura funcional del animal

### 13.1 Identificadores
Cada animal tendrá:

- identificador interno del sistema
- al menos un identificador operativo
- posibilidad de múltiples identificadores adicionales
- uno marcado como principal

### 13.2 Datos funcionales mínimos iniciales
Como mínimo, el sistema debe poder reconocer:

- quién es el animal
- cómo se identifica
- sexo
- categoría actual
- finca actual
- potrero actual
- condición de activo
- relación con lote cuando aplique
- relación madre-cría cuando aplique

### 13.3 Situaciones derivadas
La realidad operativa del animal se construye por eventos, por ejemplo:

- activo o fuera del inventario activo
- ubicación actual
- categoría actual
- historial cronológico

---

## 14. Onboarding inicial

### 14.1 Objetivo
Dejar lista una base mínima para que el cliente pueda empezar a operar el sistema.

### 14.2 Flujo base

#### Paso 1. Bienvenida
Se explica brevemente qué hará el sistema y se da inicio.

#### Paso 2. Crear primera finca
Se registra la finca inicial con la información básica requerida.

#### Paso 3. Configuración inicial mínima
Se deja definida una base inicial de elementos necesarios, como identificadores operativos habilitados y estructura mínima de la finca.

#### Paso 4. Entrada guiada a Registrar
Después de crear la finca inicial, el onboarding debe conducir al usuario hacia `Registrar`, entrando primero por `Registro de existente`.

#### Paso 5. Carga inicial de ganado
Se ofrece registro manual o carga masiva cuando aplique, ya dentro del flujo de `Registro de existente`.

#### Paso 6. Confirmación
El sistema muestra el resultado y luego lleva al usuario a Inicio o al contexto principal ya con la finca activa y el primer registro realizado o pendiente.

### 14.3 Regla importante
El onboarding no reemplaza la necesidad de creación contextual dentro de los flujos operativos.

---

## 15. Procesos de negocio

A continuación se documentan los procesos con estructura funcional homogénea.

Cada proceso define:

- propósito
- cuándo se usa
- quién lo ejecuta
- precondiciones
- decisiones clave
- flujo paso a paso
- qué hacer en situaciones comunes
- resultado esperado
- impacto funcional

---

## 16. Procesos de fase 1

# 16.1 Registro de existente

### Propósito
Registrar animales que ya estaban en la operación antes de usar el sistema.

### Cuándo se usa
Cuando el cliente inicia operación en la plataforma o cuando necesita cargar animales que ya estaban en la finca y aún no han sido registrados.

### Quién lo ejecuta
Dueño titular o administrador de finca.

### Precondiciones

- existe una finca activa
- existe al menos una forma de identificación operativa permitida

### Reglas aprobadas para este proceso

- el dato mínimo obligatorio es: identificador operativo, sexo, categoría, finca activa y potrero actual
- la edad es obligatoria, pero inicialmente se manejará como rango de edad y no como fecha exacta de nacimiento
- el animal no se puede crear sin potrero actual
- el proceso soporta registro individual, registro por lote secuencial y carga masiva cuando aplique
- en registro por lote, los datos comunes pueden capturarse una sola vez; la finca no se pide porque sale de la sesión activa
- el sistema puede sugerir el sexo según la categoría, pero el usuario debe confirmarlo
- al confirmar el registro, el animal queda activo de inmediato
- el proceso siempre debe cerrar con una confirmación final antes de guardar
- en carga masiva, el sistema debe validar por fila, permitir confirmar los registros válidos y devolver el detalle de los errados para corrección

### Decisiones clave del flujo

- individual o lote
- registro manual o carga masiva cuando aplique

### Flujo paso a paso

#### Paso 1. Elegir modalidad
El usuario define si registrará un animal o varios.

#### Paso 2. Definir forma de carga
Si es individual, continúa con captura guiada. Si es lote, puede optar por carga masiva o registro secuencial.

#### Paso 3. Identificar animal o animales
Se exige al menos un identificador operativo. El sistema generará el identificador interno automáticamente.

#### Paso 4. Definir información base
Se captura la información mínima funcional aprobada para crear el animal:

- identificador operativo
- sexo
- categoría
- rango de edad
- potrero actual

La finca no se solicita porque corresponde a la sesión activa.

#### Paso 5. Confirmar ubicación y datos comunes
En modalidad lote, el sistema permite registrar una sola vez los datos comunes permitidos, como potrero, categoría o rango de edad, y luego completar solo lo individual por animal.

#### Paso 6. Confirmación final
El sistema resume la información y el usuario confirma. Como mínimo debe mostrarse:

- identificador
- sexo
- categoría
- rango de edad
- potrero
- cantidad, si es lote

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- identificador operativo vacío
- identificador duplicado según la regla activa del sistema
- sexo vacío
- categoría vacía
- rango de edad vacío
- potrero vacío
- intento de guardar sin finca activa

#### Advertencias

- categoría y sexo no parecen coherentes
- el rango de edad luce atípico para la categoría
- en registros agrupados, los datos comunes presentan disparidades que ameritan revisión

#### Errores por fila en carga masiva

- fila sin identificador
- fila con identificador repetido en el mismo archivo
- fila con identificador ya existente en el sistema
- fila con categoría inválida
- fila con sexo inválido
- fila con potrero inexistente cuando no se permita crearlo automáticamente
- fila sin un dato obligatorio

#### Mensajes funcionales esperados

- El identificador ya existe
- Debe seleccionar un potrero
- La categoría es obligatoria
- No hay una finca activa para registrar
- El archivo tiene filas con error. Puede continuar con las válidas y corregir las demás

### Qué hacer en situaciones comunes

#### Si falta un potrero
Se crea dentro del flujo sin salir a configuración.

#### Si el usuario quiere registrar varios animales similares
Debe usar modalidad lote o carga masiva cuando el proceso lo permita.

#### Si el identificador ya existe
El sistema debe alertar y detener la confirmación hasta corregir.

#### Si la carga masiva tiene errores parciales
El sistema debe separar registros válidos y errados, mostrar el motivo por fila y permitir corregir y reprocesar solo los fallidos.

#### Si la categoría sugiere un sexo
El sistema lo propone, pero el usuario debe confirmarlo.

### Resultado esperado
Los animales quedan creados, activos y visibles en Ganado.

### Impacto funcional

- crea animales activos
- establece ubicación inicial
- deja historial de ingreso por registro de existente
- deja explícito en historial que el origen del ingreso fue registro de existente

---

# 16.2 Compra

### Propósito
Registrar el ingreso de animales adquiridos a terceros.

### Cuándo se usa
Cuando uno o varios animales entran a la operación como consecuencia de una compra.

### Quién lo ejecuta
Dueño titular o administrador.

### Precondiciones

- finca activa definida
- identificadores habilitados

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: identificador operativo, sexo, categoría, rango de edad, potrero destino, fecha de compra y origen o vendedor
- el valor económico es opcional y no bloquea el proceso
- cuando la compra sea por lote, el sistema crea un lote de compra u origen asociado a esos animales
- el animal no se puede crear en Compra sin potrero destino dentro de la finca activa
- el proceso de Compra siempre debe cerrar con una confirmación final antes de guardar
- en Compra por lote, el sistema permite capturar una sola vez los datos comunes y luego completar solo lo individual por animal
- en Compra por carga masiva, el sistema valida por fila, permite confirmar los registros válidos y devuelve el detalle de los errados para corrección

### Decisiones clave

- individual o lote
- con valor económico o sin valor económico registrado

### Flujo paso a paso

#### Paso 1. Elegir individual o lote
Se define la modalidad de la compra.

#### Paso 2. Registrar origen
Se registra vendedor, finca de origen u origen equivalente definido por negocio.

#### Paso 3. Registrar fecha del evento
Por defecto hoy. Puede ajustarse dentro de la política permitida.

#### Paso 4. Registrar animales
Se captura cada animal individualmente o por carga masiva/lote cuando aplique.

#### Paso 5. Registrar ubicación inicial
Se define el potrero inicial dentro de la finca activa.

#### Paso 6. Registrar valor económico si aplica
Este dato es opcional y no bloquea el flujo.

#### Paso 7. Confirmar
El sistema resume la compra y solicita confirmación final antes de guardar. Como mínimo debe mostrar:

- cantidad
- origen o vendedor
- fecha de compra
- potrero destino
- valor económico, si se registró
- si se creó lote de compra

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- identificador operativo vacío
- identificador duplicado según la regla activa del sistema
- sexo vacío
- categoría vacía
- rango de edad vacío
- potrero destino vacío
- fecha de compra vacía
- origen o vendedor vacío
- intento de guardar sin finca activa

#### Advertencias

- el valor económico luce atípico según reglas futuras o umbrales parametrizables
- categoría y sexo no parecen coherentes
- el rango de edad luce atípico para la categoría
- en compras por lote, los datos agrupados presentan diferencias que ameritan revisión

#### Errores por fila en carga masiva

- fila sin identificador
- fila con identificador repetido en el mismo archivo
- fila con identificador ya existente en el sistema
- fila sin fecha de compra
- fila sin origen o vendedor
- fila con categoría inválida
- fila con sexo inválido
- fila con potrero inválido cuando no se permita creación automática

#### Mensajes funcionales esperados

- Debe registrar el origen o vendedor
- La fecha de compra es obligatoria
- Debe seleccionar un potrero destino
- El identificador ya existe
- El archivo tiene filas con error. Puede continuar con las válidas y corregir las demás

### Qué hacer en situaciones comunes

#### Si todos los animales comparten origen y fecha
Se aprovecha la modalidad lote y se capturan una sola vez los datos comunes permitidos, como origen, fecha, potrero destino, categoría o rango de edad cuando aplique.

#### Si algunos datos individuales cambian
Se permite captura individual dentro del lote.

#### Si la carga masiva tiene errores parciales
El sistema debe separar registros válidos y errados, mostrar el motivo por fila y permitir corregir y reprocesar solo los fallidos.

#### Si falta el potrero destino
Se crea dentro del flujo, pero no se permite confirmar la compra sin dejar definido el potrero destino.

### Resultado esperado
Los animales quedan activos y relacionados con un evento de compra.

### Impacto funcional

- crea ingreso al inventario activo
- asocia origen de compra
- puede crear lote de origen o compra
- deja explícito en historial que el origen del ingreso fue Compra, con fecha y origen o vendedor asociado

---

# 16.3 Pesaje

### Propósito
Registrar el peso del animal para conservar trazabilidad de crecimiento y control operativo.

### Cuándo se usa
Cuando se pesa uno o varios animales.

### Quién lo ejecuta
Dueño, administrador u operario autorizado.

### Precondiciones

- el animal existe y está activo

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal o grupo, fecha del pesaje y peso
- no se permite pesar animales inactivos
- no se permite pesar animales no existentes
- en modalidad individual, el sistema identifica el animal y muestra contexto básico antes de guardar
- en modalidad grupo, la captura se mantiene secuencial animal por animal
- en modalidad grupo, todos comparten la fecha si el usuario no la cambia
- si el peso parece atípico, el sistema puede advertir sin bloquear de entrada, según reglas futuras o umbrales parametrizables
- al confirmar, queda historial del pesaje y se actualiza la última referencia de peso del animal
- el proceso debe cerrar con confirmación final mostrando animal o cantidad, fecha y peso registrado

### Decisiones clave

- individual o grupo

### Flujo paso a paso

#### Paso 1. Elegir modalidad
Individual o grupo.

#### Paso 2. Registrar fecha
Por defecto hoy; puede cambiarse dentro de la política permitida.

#### Paso 3. Identificar animal o grupo
Se busca por identificador. En experiencia futura móvil podrá apoyarse en QR o RFID.

#### Paso 4. Registrar peso
Se ingresa el peso correspondiente.

#### Paso 5. Confirmación final
El sistema confirma el registro y muestra animal o cantidad, fecha y peso registrado antes de guardar.

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal ni grupo seleccionado
- no hay fecha
- no hay peso
- el peso no es numérico o no cumple la regla mínima aceptada por el sistema
- el animal no existe
- el animal está inactivo

#### Advertencias

- el peso luce atípico respecto del historial previo
- el peso rompe una tendencia esperada según reglas futuras
- la fecha es muy antigua según la política permitida

#### Mensajes funcionales esperados

- Debe registrar un peso válido
- Debe seleccionar un animal
- No puede pesar un animal inactivo
- El animal no existe en el sistema
- La fecha del pesaje es obligatoria

### Qué hacer en situaciones comunes

#### Si el animal no aparece
No se registra peso. Primero debe existir en el sistema.

#### Si el usuario pesa varios animales seguidos
Debe mantenerse una secuencia fluida de captura por grupo.

#### Si se registró un peso equivocado
La corrección se hará bajo las reglas de corrección y auditoría.

#### Si el peso parece atípico
El sistema puede advertirlo sin bloquear, según reglas parametrizables.

### Resultado esperado
El peso queda registrado en historial.

### Impacto funcional

- agrega evento de pesaje
- actualiza última referencia de peso
- en el futuro podrá alimentar indicadores

---

# 16.4 Movimiento de potrero

### Propósito
Registrar el cambio de ubicación del animal dentro de la finca.

### Cuándo se usa
Cuando uno o varios animales pasan de un potrero a otro.

### Quién lo ejecuta
Dueño, administrador u operario autorizado.

### Precondiciones

- el animal existe y está activo
- existe finca activa

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal o grupo, potrero destino y fecha del movimiento
- en modalidad individual el potrero origen no se pide manualmente; el sistema lo toma del estado actual del animal
- en modalidad grupo el potrero origen puede usarse como base de selección
- no se permite mover animales inactivos
- no se permite confirmar un movimiento cuyo destino sea el mismo potrero actual
- si el potrero destino no existe, puede crearse dentro del flujo
- al confirmar, cambia el potrero actual y queda historial del movimiento
- si el movimiento es por grupo, todos los animales seleccionados reciben el evento
- el proceso debe cerrar con confirmación final mostrando cantidad, origen, destino y fecha

### Decisiones clave

- individual o grupo

### Flujo paso a paso

#### Paso 1. Elegir modalidad
Se define si el movimiento es individual o por grupo.

#### Paso 2. Identificar origen
En individual se identifica el animal y el sistema toma su potrero actual. En grupo se parte del potrero origen o de la selección controlada de animales.

#### Paso 3. Definir potrero destino
Se elige el nuevo potrero. Si no existe, se permite crearlo dentro del flujo.

#### Paso 4. Registrar fecha
Por defecto hoy, con ajuste permitido dentro de política.

#### Paso 5. Confirmación final
Se muestra cantidad, origen, destino y fecha antes de guardar.

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay finca activa
- no hay animal ni grupo seleccionado
- no hay potrero destino
- no hay fecha
- el destino es igual al potrero actual
- el animal está inactivo
- algún animal del grupo está inactivo
- no existe destino válido

#### Advertencias

- el movimiento involucra un grupo muy grande
- el usuario excluye pocos animales de un grupo casi completo
- la fecha del movimiento es muy antigua según la política de registro permitida

#### Mensajes funcionales esperados

- Debe seleccionar un potrero destino
- No puede mover un animal inactivo
- El potrero destino no puede ser el mismo actual
- Debe seleccionar al menos un animal
- No hay una finca activa para registrar

### Qué hacer en situaciones comunes

#### Si el destino no existe
Se crea dentro del flujo.

#### Si el usuario quiere mover casi todos los animales de un potrero
Debe poder seleccionar por grupo con control de exclusiones si aplica.

#### Si el movimiento fue registrado mal
Se corrige según reglas de corrección y auditoría.

#### Si el destino es igual al origen
El sistema debe impedir la confirmación.

### Resultado esperado
La ubicación actual del animal cambia como consecuencia del evento.

### Impacto funcional

- crea evento de movimiento
- actualiza potrero actual
- mantiene historial de ubicación

---

# 16.5 Venta

### Propósito
Registrar la salida del animal por comercialización.

### Cuándo se usa
Cuando uno o varios animales salen por venta.

### Quién lo ejecuta
Dueño titular o administrador.

### Precondiciones

- los animales están activos

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal o lote, fecha de venta y comprador o destino
- el valor económico es opcional
- la observación es opcional
- no se permite vender animales inactivos
- no se permite vender animales ya vendidos
- no se permite vender animales con inconsistencias de estado
- en modalidad lote, se permite capturar datos comunes una sola vez
- en modalidad lote, si aplica, puede registrarse valor total o valor por animal
- al confirmar, el animal sale del inventario activo
- al confirmar, queda historial de venta y trazabilidad del comprador o destino
- el proceso debe cerrar con confirmación final mostrando cantidad, fecha, comprador o destino y valor si se registró

### Decisiones clave

- individual o lote
- con información económica o solo salida operativa

### Flujo paso a paso

#### Paso 1. Elegir modalidad
Individual o lote.

#### Paso 2. Identificar animales
Se seleccionan los animales a vender.

#### Paso 3. Registrar comprador o destino
Se registra la información del comprador o destino cuando aplique.

#### Paso 4. Registrar fecha
Por defecto hoy, ajustable según política.

#### Paso 5. Registrar valor si aplica
Se captura valor por animal o total, según el caso.

#### Paso 6. Confirmación final
El sistema muestra resumen final de la venta antes de guardar. Como mínimo debe incluir:

- cantidad
- fecha
- comprador o destino
- valor, si se registró

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal ni lote seleccionado
- no hay fecha de venta
- no hay comprador o destino
- el animal está inactivo
- el animal ya fue vendido
- el animal presenta una salida definitiva incompatible
- existe inconsistencia de estado

#### Advertencias

- el valor económico luce atípico según reglas futuras o umbrales parametrizables
- el lote seleccionado presenta diferencias relevantes que ameritan revisión
- la fecha de venta es muy antigua según la política permitida

#### Mensajes funcionales esperados

- Debe registrar el comprador o destino
- La fecha de venta es obligatoria
- No puede vender un animal inactivo
- El animal ya fue vendido
- El animal presenta una inconsistencia de estado

### Qué hacer en situaciones comunes

#### Si un animal ya no está activo
No debe permitirse venderlo.

#### Si el usuario quiere vender varios juntos
Debe usar modalidad lote.

#### Si la venta se registró con error
Se aplica corrección o anulación según rol.

#### Si existe inconsistencia de estado
El sistema debe impedir la confirmación hasta revisar el caso.

### Resultado esperado
Los animales salen del inventario activo.

### Impacto funcional

- crea evento de venta
- cambia condición de activo
- conserva historial de salida

---

# 16.6 Muerte

### Propósito
Registrar la salida del animal por fallecimiento.

### Cuándo se usa
Cuando un animal muere y debe quedar trazabilidad de su salida.

### Quién lo ejecuta
Dueño titular o administrador; operario si así se autoriza para reportar el evento.

### Precondiciones

- el animal existe
- el animal estaba activo

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal, fecha de muerte y causa o motivo
- la observación es opcional
- se puede distinguir quién reporta del usuario que registra, si la política del proceso lo requiere
- no se permite registrar muerte para animales inactivos
- no se permite registrar muerte para animales ya vendidos
- no se permite registrar muerte para animales ya reportados como muertos
- si la causa exacta no se conoce, se permite una causa genérica o pendiente según política
- al confirmar, el animal sale del inventario activo
- al confirmar, queda historial de muerte y la causa queda trazada
- el proceso debe cerrar con confirmación final mostrando animal, fecha, causa y observación si existe

### Flujo paso a paso

#### Paso 1. Identificar animal
Se busca el animal correspondiente.

#### Paso 2. Registrar fecha
Por defecto hoy, ajustable dentro de la política permitida.

#### Paso 3. Registrar causa
Se captura causa o motivo según el catálogo o regla definida.

#### Paso 4. Registrar observación si aplica
Se añade detalle adicional cuando corresponda.

#### Paso 5. Confirmación final
El sistema muestra el resumen del evento antes de guardar. Como mínimo debe incluir:

- animal
- fecha
- causa
- observación, si existe

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay fecha de muerte
- no hay causa o motivo
- el animal está inactivo
- el animal ya fue vendido
- el animal ya fue reportado como muerto

#### Advertencias

- la causa se registra como genérica o pendiente
- la fecha del evento es muy antigua según la política permitida
- existe un patrón repetitivo de causa que amerita revisión posterior

#### Mensajes funcionales esperados

- Debe registrar una causa o motivo
- La fecha de muerte es obligatoria
- No puede registrar muerte para un animal inactivo
- El animal ya fue reportado como muerto
- El animal ya presenta una salida incompatible

### Qué hacer en situaciones comunes

#### Si el usuario no conoce la causa exacta
Debe poder registrar una causa genérica o pendiente según política.

#### Si el animal ya no está activo
No debe permitirse el registro como muerte sin revisar el historial.

#### Si el evento fue registrado con error
La corrección o anulación se gestiona según las reglas de auditoría y permisos.

### Resultado esperado
El animal sale del inventario activo y queda historial de muerte.

### Impacto funcional

- crea evento de muerte
- cambia condición de activo
- puede alimentar alertas futuras

---

# 16.7 Traslado entre fincas

### Propósito
Registrar el paso de uno o varios animales desde una finca a otra dentro de la misma cuenta.

### Cuándo se usa
Cuando el ganado cambia de finca sin romper continuidad de historial.

### Quién lo ejecuta
Dueño titular o administrador con permiso suficiente.

### Precondiciones

- la cuenta tiene más de una finca
- existe al menos una finca destino válida distinta de la finca activa
- los animales están activos

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal o lote, finca destino, potrero destino y fecha de traslado
- no se permite trasladar animales inactivos
- no se permite trasladar animales a la misma finca actual
- no se permite trasladar animales con inconsistencias de estado
- el proceso no aplica cuando la cuenta solo tiene una finca o no existe una finca destino válida
- en modalidad lote se permite seleccionar varios animales
- en modalidad lote todos comparten finca destino y fecha si el usuario no cambia algo
- si el potrero destino no existe, puede crearse dentro del flujo
- al confirmar, cambia la finca actual
- al confirmar, cambia el potrero actual
- al confirmar, queda historial continuo del traslado
- la identidad del animal no se rompe
- el proceso debe cerrar con confirmación final mostrando cantidad, finca origen, finca destino, potrero destino y fecha

### Decisiones clave

- individual o lote

### Flujo paso a paso

#### Paso 1. Elegir modalidad
Individual o lote.

#### Paso 2. Identificar animales
Se seleccionan los animales a trasladar.

#### Paso 3. Seleccionar finca destino
Se elige una finca distinta a la finca activa.

#### Paso 4. Seleccionar potrero destino
Se define el potrero de llegada. Si no existe, se crea dentro del flujo.

#### Paso 5. Registrar fecha
Por defecto hoy; ajustable según política.

#### Paso 6. Confirmación final
Se muestra cantidad, finca origen, finca destino, potrero destino y fecha antes de guardar.

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal ni lote seleccionado
- no hay finca destino
- no hay potrero destino
- no hay fecha de traslado
- la finca destino es igual a la actual
- el animal está inactivo
- el animal presenta inconsistencia de estado
- la cuenta no tiene una finca destino válida

#### Advertencias

- el traslado involucra un lote muy grande
- la fecha del traslado es muy antigua según la política permitida
- la finca destino presenta condiciones que ameritan revisión operativa futura

#### Mensajes funcionales esperados

- Debe seleccionar una finca destino
- Debe seleccionar un potrero destino
- No puede trasladar un animal inactivo
- La finca destino no puede ser la misma finca actual
- No existe una finca destino válida para este traslado

### Qué hacer en situaciones comunes

#### Si la finca destino no tiene potreros listos
Debe permitirse creación contextual dentro del flujo.

#### Si el usuario intenta trasladar animales ya inactivos
El sistema debe impedirlo.

#### Si la cuenta solo tiene una finca o no existe destino válido
El proceso de traslado entre fincas no aplica. Si los animales salen de la operación sin otra finca destino, debe usarse el proceso correspondiente de salida, como venta, muerte o descarte, según el caso.

### Resultado esperado
Los animales conservan continuidad de historial y cambian de finca y potrero actual.

### Impacto funcional

- crea evento de traslado
- actualiza finca actual
- actualiza potrero actual
- conserva trazabilidad continua del animal

---

## 17. Procesos de fase 2

# 17.1 Vacunación

### Propósito
Registrar la aplicación de vacunas al animal y conservar trazabilidad sanitaria.

### Quién lo ejecuta
Dueño, administrador, operario autorizado o veterinario.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal, vacuna y fecha de aplicación
- la dosis es opcional
- el lote del producto es opcional
- la observación es opcional
- se puede registrar quién aplicó, si no es el mismo usuario que registra
- no se permite vacunar animales inactivos
- no se permite vacunar animales no existentes
- si la vacuna no existe, puede crearse dentro del flujo según la política del sistema
- si la vacuna requiere seguimiento, el sistema puede dejar programada una alerta o próximo control
- al confirmar, queda historial de vacunación con la vacuna aplicada y la fecha
- el proceso debe cerrar con confirmación final mostrando animal, vacuna, fecha y quién aplicó si se registró

### Flujo base

1. identificar animal
2. seleccionar vacuna
3. registrar fecha
4. registrar aplicador si corresponde
5. registrar datos opcionales como dosis, lote del producto u observación
6. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay vacuna seleccionada
- no hay fecha de aplicación
- el animal no existe
- el animal está inactivo

#### Advertencias

- la vacuna no tiene datos complementarios como dosis o lote del producto
- la fecha es muy antigua según la política permitida
- el animal presenta antecedentes sanitarios que ameritan revisión posterior

#### Mensajes funcionales esperados

- Debe seleccionar una vacuna
- La fecha de aplicación es obligatoria
- No puede vacunar un animal inactivo
- El animal no existe en el sistema

### Situaciones comunes

- si la vacuna no existe, debe poder crearse dentro del flujo si la política lo permite
- si requiere refuerzo o seguimiento, el sistema debe dejar la base para una alerta futura
- si el animal está inactivo, el sistema debe impedir la confirmación

---

# 17.2 Tratamiento sanitario

### Propósito
Registrar tratamientos y dejar trazabilidad clínica básica.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal, tipo de tratamiento o producto y fecha de aplicación
- la dosis es opcional
- la observación clínica es opcional
- se puede registrar quién aplicó
- la duración o indicación es opcional
- no se permite registrar tratamiento para animales inactivos
- no se permite registrar tratamiento para animales no existentes
- si el tratamiento o producto no existe, puede crearse dentro del flujo según política
- si el tratamiento implica restricción futura, el sistema deja base para alertas o controles posteriores
- al confirmar, queda historial sanitario con tratamiento, fecha y responsable si se registró
- el proceso debe cerrar con confirmación final mostrando animal, tratamiento o producto, fecha y responsable si aplica

### Flujo base

1. identificar animal
2. seleccionar tratamiento o tipo
3. registrar fecha
4. registrar datos opcionales como dosis, observación clínica, duración o indicación
5. registrar responsable si corresponde
6. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay tratamiento o producto seleccionado
- no hay fecha de aplicación
- el animal no existe
- el animal está inactivo

#### Advertencias

- faltan datos complementarios como dosis o duración
- la fecha es muy antigua según la política permitida
- el tratamiento puede requerir seguimiento posterior

#### Mensajes funcionales esperados

- Debe seleccionar un tratamiento o producto
- La fecha de aplicación es obligatoria
- No puede registrar tratamiento para un animal inactivo
- El animal no existe en el sistema

### Situaciones comunes

- si el tratamiento o producto no existe, debe poder resolverse dentro del flujo según política
- si el animal está inactivo, el sistema debe impedir la confirmación
- si el tratamiento exige seguimiento posterior, el sistema debe dejar la base para control o alerta futura

---

# 17.3 Palpación o revisión reproductiva

### Propósito
Registrar el resultado de una revisión reproductiva.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal elegible, fecha y resultado
- la observación es opcional
- se puede registrar el responsable de la revisión
- se puede registrar dato complementario como tiempo estimado o estado reproductivo, si aplica
- no se permite registrar palpación para animales inactivos
- no se permite registrar palpación para animales no existentes
- no se permite registrar palpación para animales no elegibles según sexo o categoría, si la regla aplica
- si el resultado genera seguimiento, el sistema deja base para alerta o control posterior
- al confirmar, queda historial reproductivo con resultado, fecha y responsable si se registró
- el proceso debe cerrar con confirmación final mostrando animal, fecha, resultado y responsable si aplica

### Flujo base

1. identificar animal elegible
2. registrar fecha
3. registrar resultado
4. registrar observación y datos complementarios si aplica
5. registrar responsable si corresponde
6. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay fecha
- no hay resultado
- el animal no existe
- el animal está inactivo
- el animal no es elegible según las reglas del proceso

#### Advertencias

- faltan datos complementarios para análisis posterior
- la fecha es muy antigua según la política permitida
- el resultado amerita seguimiento posterior

#### Mensajes funcionales esperados

- Debe registrar un resultado
- La fecha es obligatoria
- El animal no es elegible para este proceso
- No puede registrar palpación para un animal inactivo

### Situaciones comunes

- si el animal no es elegible, el sistema debe impedir la confirmación
- si el animal está inactivo, el sistema debe impedir la confirmación
- si el resultado exige seguimiento posterior, el sistema debe dejar la base para control o alerta futura

---

# 17.4 Destete

### Propósito
Registrar la separación formal de la cría respecto de la madre.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: cría o relación madre-cría y fecha de destete
- el potrero destino es opcional
- la observación es opcional
- se puede registrar responsable si aplica
- no se permite destetar animales inactivos
- no se permite destetar animales no existentes
- no se permite destetar animales sin relación madre-cría válida, cuando esa relación sea requerida por la regla del sistema
- si el destete cambia ubicación, ese cambio se registra dentro del mismo flujo
- al confirmar, queda historial de destete
- al confirmar, se actualiza la relación operativa con la madre según la regla del sistema
- si aplica, cambia el potrero actual de la cría
- el proceso debe cerrar con confirmación final mostrando cría, madre si aplica, fecha y potrero destino si aplica

### Flujo base

1. identificar cría o relación madre-cría
2. registrar fecha
3. definir potrero destino si cambia
4. registrar observación o responsable si aplica
5. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay cría o relación madre-cría seleccionada
- no hay fecha de destete
- la cría no existe
- la cría está inactiva
- no existe relación madre-cría válida cuando la regla la exige

#### Advertencias

- el destete implica cambio de ubicación y conviene revisar el destino
- la fecha es muy antigua según la política permitida
- el caso presenta condiciones que ameritan seguimiento posterior

#### Mensajes funcionales esperados

- Debe seleccionar una cría válida
- La fecha de destete es obligatoria
- No existe una relación madre-cría válida para este caso
- No puede destetar una cría inactiva

### Situaciones comunes

- si no existe relación madre-cría válida, el sistema debe impedir la confirmación cuando la regla lo requiera
- si la cría está inactiva, el sistema debe impedir la confirmación
- si el destete implica cambio de ubicación, debe resolverse dentro del mismo flujo

---

# 17.5 Cambio de categoría

### Propósito
Registrar el cambio formal de categoría del animal.

### Reglas aprobadas para este proceso

- el cambio de categoría no es una edición manual libre
- el sistema sugiere la nueva categoría según reglas definidas
- el usuario autorizado confirma la sugerencia antes de aplicarla
- el mínimo obligatorio es: animal, nueva categoría sugerida y fecha del cambio
- la observación es opcional
- se puede registrar responsable si aplica
- no se permite cambiar categoría a animales inactivos
- no se permite cambiar categoría a animales no existentes
- no se permite aplicar categorías incompatibles con sexo o regla de negocio
- la categoría actual del animal solo cambia por este proceso o por reglas futuras equivalentes aprobadas
- al confirmar, queda historial del cambio de categoría
- al confirmar, se actualiza la categoría actual del animal
- el proceso debe cerrar con confirmación final mostrando animal, categoría anterior, nueva categoría y fecha

### Flujo base

1. identificar animal
2. mostrar categoría actual y categoría sugerida por el sistema
3. registrar fecha
4. registrar observación o responsable si aplica
5. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay categoría sugerida o confirmada
- no hay fecha de cambio
- el animal no existe
- el animal está inactivo
- la categoría es incompatible con las reglas del animal

#### Advertencias

- la sugerencia se basa en información incompleta o estimada
- la fecha es muy antigua según la política permitida
- el usuario decide no confirmar el cambio sugerido

#### Mensajes funcionales esperados

- Debe confirmar la categoría sugerida
- La fecha del cambio es obligatoria
- La categoría no es compatible con este animal
- No puede cambiar la categoría de un animal inactivo

### Situaciones comunes

- si el animal está inactivo, el sistema debe impedir la confirmación
- si la categoría sugerida no es compatible con las reglas del animal, el sistema debe impedir la confirmación
- si el usuario no confirma, el cambio no se aplica

---

# 17.6 Descarte

### Propósito
Registrar la salida del animal por criterio productivo u operativo distinto de venta o muerte.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: animal, motivo de descarte y fecha
- el destino es opcional
- el valor es opcional
- la observación es opcional
- no se permite descartar animales inactivos
- no se permite descartar animales ya vendidos
- no se permite descartar animales ya muertos
- al confirmar, el animal sale del inventario activo
- al confirmar, queda historial de descarte con el motivo trazado
- el proceso debe cerrar con confirmación final mostrando animal, motivo, fecha y destino o valor si se registró

### Flujo base

1. identificar animal
2. registrar motivo
3. registrar fecha
4. registrar destino, valor u observación si aplica
5. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay animal seleccionado
- no hay motivo de descarte
- no hay fecha
- el animal está inactivo
- el animal ya fue vendido
- el animal ya fue reportado como muerto

#### Advertencias

- el descarte incluye valor económico y conviene revisar consistencia con el motivo
- la fecha es muy antigua según la política permitida
- el caso puede requerir revisión posterior

#### Mensajes funcionales esperados

- Debe registrar un motivo de descarte
- La fecha es obligatoria
- No puede descartar un animal inactivo
- El animal ya presenta una salida incompatible

### Situaciones comunes

- si el animal ya no está activo, el sistema debe impedir la confirmación
- si el animal ya tiene una salida definitiva incompatible, el sistema debe impedir la confirmación
- si hubo error en el registro, la corrección o anulación se gestiona según la política de auditoría

---

# 17.7 Nacimiento

### Propósito
Registrar el ingreso de una cría nacida dentro de la operación.

### Reglas aprobadas para este proceso

- el mínimo obligatorio es: madre, fecha de nacimiento, identificador operativo de la cría, sexo de la cría, categoría inicial de la cría y potrero inicial
- la observación es opcional
- el peso al nacer es opcional
- se puede registrar responsable del registro
- no se permite registrar nacimiento para madres no existentes
- no se permite registrar nacimiento para madres inactivas
- no se permite registrar nacimiento para madres no elegibles según la regla reproductiva
- al confirmar, se crea la cría activa
- al confirmar, se crea la relación madre-cría
- al confirmar, queda historial de nacimiento
- al confirmar, queda ubicación inicial de la cría
- el proceso debe cerrar con confirmación final mostrando madre, cría, fecha, sexo, categoría inicial y potrero inicial

### Flujo base

1. identificar madre
2. registrar fecha de nacimiento
3. registrar datos mínimos de la cría
4. asociar relación madre-cría
5. definir potrero inicial
6. registrar observación, peso al nacer o responsable si aplica
7. confirmar

### Validaciones, bloqueos y advertencias

#### Bloqueos duros

- no hay madre seleccionada
- no hay fecha de nacimiento
- no hay identificador operativo de la cría
- no hay sexo de la cría
- no hay categoría inicial de la cría
- no hay potrero inicial
- la madre no existe
- la madre está inactiva
- la madre no es elegible según la regla reproductiva
- el identificador de la cría ya existe

#### Advertencias

- faltan datos complementarios como peso al nacer
- la fecha es muy antigua según la política permitida
- el caso amerita seguimiento posterior según reglas reproductivas futuras

#### Mensajes funcionales esperados

- Debe seleccionar una madre válida
- La fecha de nacimiento es obligatoria
- El identificador de la cría ya existe
- No puede registrar nacimiento para una madre inactiva

### Situaciones comunes

- si la madre no existe, el sistema debe impedir la confirmación
- si la madre está inactiva, el sistema debe impedir la confirmación
- si la madre no es elegible según la regla reproductiva, el sistema debe impedir la confirmación
- si el potrero inicial no existe, debe poder crearse dentro del flujo según la política general del sistema

---

## 18. Reglas de corrección, anulación y auditoría

### 18.1 Corrección
Los eventos mal registrados podrán corregirse según el rol y la política de la cuenta.

### 18.2 Anulación
La anulación no debe borrar la trazabilidad del evento original.

### 18.3 Auditoría mínima obligatoria
Toda corrección o anulación debe conservar:

- evento afectado
- usuario que corrigió o anuló
- fecha y hora de la acción
- justificación cuando la política lo requiera

---

## 19. Parámetros configurables

### 19.1 Regla general
Las configuraciones se definen principalmente por cuenta.

### 19.2 Excepciones por finca
Solo deben existir en casos puntuales, explícitamente permitidos y justificados.

### 19.3 Ejemplos de parámetros configurables

- ventana máxima para registrar eventos con fecha anterior
- identificadores operativos habilitados
- ciertos catálogos operativos
- reglas visibles según política del cliente

---

## 20. Riesgos que deben controlarse

### 20.1 Volver a un sistema por módulos
Se detecta cuando el equipo diseña CRUDs en lugar de procesos.

### 20.2 Duplicar puntos de operación
Se detecta cuando Ganado, Ficha, Alertas y Registrar empiezan a hacer lo mismo.

### 20.3 Exceso de excepciones por finca
Se detecta cuando la configuración deja de ser gobernable.

### 20.4 Demasiada edición manual
Se detecta cuando la realidad operativa del animal puede cambiar sin pasar por procesos.

### 20.5 Construcción inflada de fase 1
Se detecta cuando se agregan procesos complejos antes de consolidar la base.

---

## 21. Lo que sigue después de este documento

Con esta versión ya no corresponde volver al nivel conceptual general. El siguiente nivel de trabajo debe ser bajar proceso por proceso a especificación detallada funcional, incluyendo:

- campos exactos
- validaciones
- mensajes de error
- reglas de negocio finas
- permisos por rol
- comportamiento ante corrección
- impacto en historial
- impacto en estados derivados

Después de eso sí se debe pasar a modelo de datos y diseño técnico.

---

## 22. Cierre

Esta versión busca convertirse en la base funcional real del producto. Ya no presenta solo una visión. Presenta estructura, reglas, decisiones, fases y procesos suficientemente definidos para comenzar a aterrizar construcción con criterio.

El principio que debe seguir gobernando todo lo que venga después es este:

el sistema se construye para registrar y entender lo que le pasa al ganado, no para llenar tablas ni sostener pantallas desconectadas.

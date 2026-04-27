# Registro de Existente · Estado actual y criterio operativo

## Objetivo de este documento

Dejar documentado el estado funcional real del proceso `Registro de existente` tal como hoy lo estamos construyendo.

No reemplaza el documento funcional general del producto.
Sirve como documento vivo de decisiones ya aterrizadas y de pendientes que todavia faltan por cerrar.

## Criterio funcional que estamos siguiendo

Este proceso debe sentirse rapido, guiado y natural para un ganadero.

La experiencia no debe parecer un formulario tecnico largo.
Debe ayudar a cargar animales existentes de forma ordenada, simple y rapida, especialmente cuando el cliente esta entrando por primera vez una poblacion grande.

## Decisiones ya tomadas

### 1. Registrar sigue siendo el punto unico de ejecucion

`Registro de existente` se ejecuta desde `Registrar`.

### 2. Para varios animales, primero va la cantidad

Si el usuario va a registrar varios animales, primero se le pregunta cuantos son.

Despues de eso se le pregunta si van:

- para el mismo potrero
- para distintos potreros

### 3. El agrupador operativo principal es el potrero

No estamos partiendo primero por sexo.
El agrupador natural del trabajo es el potrero.

### 4. Sexo no se pide ni se muestra

El sexo no debe pedirse como campo principal del flujo.
Se determina por la categoria del animal.

### 5. La clasificacion del grupo se hace por categoria con cantidad

Dentro del grupo, el usuario reparte la cantidad total por categoria.

Ejemplo:

- 4 becerras
- 3 becerros
- 2 novillas

Con eso el sistema entiende mejor como viene el lote sin obligar al usuario a responder si el grupo es mixto, solo machos o solo hembras.

### 6. No se pueden agregar categorias por fuera del total del grupo

Si la suma de categorias ya completo la cantidad del grupo, no se permite agregar otra categoria.
El usuario debe ajustar los valores actuales para reclasificar mejor.

### 7. El rango de edad ya no se pide en pantalla

El rango de edad no debe preguntarse como dato visible del flujo.
Se deriva automaticamente a partir de la categoria del animal.

Para este proceso, la regla funcional queda asi:

- `Becerra` y `Becerro` se interpretan dentro de `0 a 12 meses`
- `Novilla`, `Torete` y `Novillo` se interpretan dentro de `13 a 36 meses`
- `Vaca` y `Toro` se interpretan como `Mas de 36 meses`

Como el catalogo tecnico actual todavia esta mas detallado que la experiencia, el sistema guarda un rango tecnico de referencia dentro de esa banda.
Eso no se le muestra ni se le pregunta al usuario.

La decision funcional queda asi:

- la categoria define la banda esperada de edad
- el backend entrega esa metadata junto con la categoria
- en `uno a uno`, el frontend la muestra y la usa para acotar la fecha de nacimiento opcional
- el frontend no debe volver a inferir rangos por nombre de categoria

## Flujo actual que queda establecido

### Momento 1. Inicio

- `Un animal`
- `Varios animales`

### Momento 2. Preparacion del registro

Si son varios:

- pedir cantidad total
- preguntar si van para el mismo potrero o para distintos potreros
- armar grupos por potrero
- si la opcion es `distintos potreros`, cada grupo debe quedar en un potrero diferente

Para cada grupo:

- definir cantidad del grupo
- definir potrero si aplica
- completar fecha de ingreso
- repartir la cantidad por categoria

En `por grupos`, la fecha de nacimiento no se pide en la carga inicial.
Si luego se necesita mayor precision por animal, eso debe resolverse en un ajuste posterior y no en esta captura masiva.
La fecha de ingreso se trata como fecha de calendario y se valida contra el dia actual, no contra la hora exacta del servidor.

### Momento 3. Captura y cierre

- el sistema convierte cada reparto por categoria en grupos efectivos
- se captura la identificacion de cada animal
- se revisa el resumen
- se guarda el registro

## Identificacion del animal · criterio ya establecido

### Tipos visibles para este proceso

Por ahora, en `Registro de existente` solo se trabajan estos tipos:

- `Generacion automatica`
- `Identificador propio`

### Tipos que salen del flujo

- `Hierro` ya no se usa como tipo de identificacion visible
- `RFID` no se muestra todavia en este proceso

### Generacion automatica

Cuando el usuario elige `Generacion automatica`:

- debe seleccionar una marca ganadera del cliente
- el sistema arma el identificador con `MARCA + consecutivo`
- el consecutivo se toma por finca
- el consecutivo visible parte del siguiente animal que va a registrarse en esa finca
- el valor se genera en pantalla y queda solo lectura para el usuario

Formato actual de trabajo:

`MARCA-1`

### Identificador propio

Cuando el usuario elige `Identificador propio`:

- el usuario escribe manualmente el identificador principal
- no se pide marca ganadera
- sigue aplicando la validacion de duplicados por finca y valor de identificador, sin depender del tipo seleccionado

### Marcas ganaderas del cliente

Las marcas ganaderas no viven en la finca.
Por ahora viven en el cliente y se manejan asi:

- maximo 2 marcas por cliente
- se usan como fuente para la generacion automatica
- si el cliente no tiene marcas configuradas, la generacion automatica no debe ser la opcion operativa por defecto

### Identificador interno del sistema

Ademas del identificador principal, el backend sigue creando un identificador interno del sistema.

Ese identificador:

- no es visible como opcion operativa para el usuario
- se conserva para soporte interno y trazabilidad tecnica

## Categorias base que estamos tomando como referencia

| Categoria | Descripcion | Rango de edad |
| --- | --- | --- |
| Becerra | Hembra menor de 1 ano | 0 a 12 meses |
| Becerro | Macho menor de 1 ano | 0 a 12 meses |
| Novilla | Hembra joven sin haber parido | 13 a 36 meses |
| Torete | Macho joven sin castrar | 13 a 36 meses |
| Novillo | Macho castrado joven | 13 a 36 meses |
| Vaca | Hembra adulta que ya pario | Mas de 36 meses |
| Toro | Macho adulto reproductor | Mas de 36 meses |

## Lo que ya quedo alineado en la experiencia

- para varios animales ya se pide cantidad antes de la distribucion por potrero
- ya no se pide sexo como decision visible del flujo
- ya se reparte por categoria con cantidad
- ya no se pide rango de edad como dato visible
- ya se deriva el rango de edad desde la categoria
- ya la categoria entrega desde backend la banda esperada y el rango tecnico de referencia
- ya se bloquea agregar una nueva categoria cuando el total del grupo esta completo
- ya no se usa `Hierro` como tipo de identificacion visible
- ya existe `Generacion automatica` como tipo operativo
- ya existe `Identificador propio` como tipo operativo
- ya se toma la marca ganadera del cliente para la generacion automatica
- ya se toma el siguiente consecutivo por finca para formar el identificador automatico
- ya se alineo la prevalidacion batch y la validacion final para duplicados por finca
- ya se permite crear un potrero dentro del flujo sin salir del proceso
- ya el potrero creado en flujo queda seleccionado en el registro actual
- ya el proceso guarda un borrador local por finca mientras el usuario lo diligencia
- ya el borrador se restaura automaticamente al volver a abrir el proceso en la misma finca

## Pendientes funcionales todavia abiertos

### 1. Configuracion y captura de marcas en la experiencia

La base tecnica ya quedo preparada, pero todavia falta cerrar totalmente:

- donde se editan las marcas del cliente en la interfaz
- en que momento del onboarding se obligan o se sugieren
- que validaciones exactas de formato deben tener

### 2. Regla final de consecutivo por finca

Ya quedo definida la idea general de consecutivo por finca, pero todavia falta validar si el calculo actual debe quedarse como conteo operativo simple o si luego necesitara una secuencia propia mas robusta.

### 3. RFID

`RFID` no se mostrara todavia en este proceso.
Mas adelante se evaluara cuando exista lectura escaneada real.

### 4. Consolidacion futura del catalogo fisico de rangos

La experiencia ya quedo en bandas amplias por categoria.
Todavia falta decidir si el catalogo fisico de `Rango_Edad` se consolida tambien a esas mismas bandas o si sigue interno como rango tecnico de referencia.

### 5. Flujo ideal para carga inicial fuerte

Aunque el proceso ya mejoro bastante, todavia faltan ajustes para que sea mas rapido cuando el cliente entra por primera vez a cargar muchos animales de una finca.

### 6. Extender autosave a otros procesos guiados

La base ya quedo aplicada en `Registro de existente`.
Falta replicarla en los siguientes procesos guiados para que cambiar de pestaÃ±a, recargar o salir de la ruta
no haga perder trabajo parcial.

## Regla de trabajo mientras seguimos

Cada nuevo ajuste de este proceso debe revisar estas seis cosas:

1. lenguaje simple para el ganadero
2. menos pasos visibles
3. agrupacion natural por potrero
4. categoria antes que sexo
5. identificacion clara y coherente
6. velocidad real para cargas grandes

## Estado actual

El proceso ya salio de una logica de formulario tecnico.
Hoy ya se apoya en cantidad, potrero, categoria y un esquema de identificacion mucho mas claro.

Todavia no esta cerrado.
Sigue en ajuste funcional.

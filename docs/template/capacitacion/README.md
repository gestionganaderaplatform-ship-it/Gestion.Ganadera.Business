# Documentacion de Capacitacion del Template

Esta carpeta no reemplaza los README del repositorio. Su objetivo es ayudar a que un desarrollador entienda el template, los conceptos tecnicos que usa y la razon de cada decision.

Toda esta documentacion vive dentro de `docs/template/` para que en una solucion real sea facil retirarla o reemplazarla si ya no aporta valor.

La idea es que alguien pueda leer estos documentos como una capacitacion guiada:

1. entender el concepto
2. ver para que sirve
3. ubicarlo en este proyecto
4. saber cuando usarlo
5. evitar errores comunes

## Ruta de lectura sugerida

Si alguien es nuevo en el proyecto, este orden funciona bien:

1. [01-arquitectura-y-capas.md](./01-arquitectura-y-capas.md)
2. [02-pipeline-y-ciclo-request.md](./02-pipeline-y-ciclo-request.md)
3. [03-correlation-id-y-trazabilidad.md](./03-correlation-id-y-trazabilidad.md)
4. [04-seguridad-jwt-y-permisos.md](./04-seguridad-jwt-y-permisos.md)
5. [05-validacion-y-manejo-errores.md](./05-validacion-y-manejo-errores.md)
6. [06-observabilidad-y-datos-operativos.md](./06-observabilidad-y-datos-operativos.md)
7. [07-como-crear-una-feature.md](./07-como-crear-una-feature.md)
8. [08-excel-para-entidades-de-negocio.md](./08-excel-para-entidades-de-negocio.md)

Si vas a arrancar una API real desde este template, el documento mas importante despues de arquitectura y pipeline suele ser [07-como-crear-una-feature.md](./07-como-crear-una-feature.md), porque explica como heredar las bases sin depender de una feature demo.

## Como usar esta documentacion

- Si quieres entender el template completo, sigue el orden sugerido.
- Si tienes una duda puntual, ve directo al tema correspondiente.
- Si al leer un concepto te preguntas "donde esta eso en el codigo", cada documento incluye una seccion llamada `En este proyecto`.

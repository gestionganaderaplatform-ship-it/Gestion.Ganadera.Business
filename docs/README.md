# Documentacion del proyecto

Esta carpeta separa la documentacion comun del repositorio, la documentacion funcional del dominio ganadero y la documentacion especifica del template.

## Como esta organizada

- `docs/template/`
  Agrupa la guia detallada, la capacitacion y la bitacora del template.
- `docs/template/capacitacion/`
  Reune la lectura guiada para entender arquitectura, pipeline, seguridad, observabilidad y extension del template.
- `docs/operacion/`
  Reune guias operativas de despliegue y soporte para la API de negocio.
- `docs/procesos_de_negocio/`
  Reune la base funcional y tecnica del dominio ganadero, incluyendo procesos, matrices y consolidado del negocio.

## Ruta recomendada

1. [Base funcional del dominio](./procesos_de_negocio/base_documental_ganaderia/01_base_funcional_consolidada.md)
2. [Base tecnica del dominio](./procesos_de_negocio/base_documental_ganaderia/02_base_tecnica_consolidada.md)
3. [Template overview](./template/README.md)
4. [Capacitacion guiada](./template/capacitacion/README.md)
5. [Guia detallada de la API](./template/backend-api.md)
6. [Seguimiento del template](./template/seguimiento-template.md)
7. [Guias de operacion](./operacion/README.md)

## Referencia cruzada de plataforma

- La documentacion transversal temporal de plataforma vive en `Gestion.Ganadera.Business.Auth/PLATAFORMA.md`.
- Los pendientes de contraste global y el documento fuente para contrastar con codigo viven temporalmente en `Gestion.Ganadera.Business.Auth/docs/plataforma/`.

## Nota para proyectos reales

La carpeta `docs/template/` esta pensada para contener solo material propio del template.
Si una solucion derivada ya no necesita esa guia, se puede eliminar o reemplazar esa carpeta sin mezclarla con la documentacion funcional del proyecto.

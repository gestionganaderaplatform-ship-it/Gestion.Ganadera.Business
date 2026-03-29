# Politica de Retencion - Gestion.Ganadera

Este directorio contiene scripts SQL relacionados con la limpieza y retencion de informacion tecnica.

## Reglas definidas

| Tipo      | Tabla                        | Retencion |
|-----------|------------------------------|-----------|
| Logs      | Seguridad.Log_Aplicacion     | 90 dias   |
| Metricas  | Seguridad.Metrica_Solicitud  | 180 dias  |
| Auditoria | Seguridad.Auditoria          | No borrar |

## Consideraciones

- La limpieza se ejecuta desde SQL Server Agent.
- La API no ejecuta estos procedimientos.
- Los scripts estan versionados para control y auditoria.

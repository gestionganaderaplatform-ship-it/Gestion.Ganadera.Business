/*
    Uso:
        1. Reemplaza el valor de @CorrelationId por el que quieres inspeccionar.
        2. Ejecuta el script sobre la base del servicio.

    Objetivo:
        Revisar en una sola salida las tablas operativas que hoy comparten trazabilidad
        por CorrelationId:
        - Seguridad.Log_Aplicacion
        - Seguridad.Metrica_Solicitud
        - Seguridad.Seguridad_Evento
*/

DECLARE @CorrelationId NVARCHAR(100) = N'REEMPLAZAR_CORRELATION_ID';

IF NULLIF(LTRIM(RTRIM(@CorrelationId)), N'') IS NULL
BEGIN
    THROW 50000, 'Debes indicar un CorrelationId valido en @CorrelationId.', 1;
END;

SELECT
    Fuente = N'Seguridad.Log_Aplicacion',
    Fecha = l.Log_Aplicacion_Fecha,
    ApiCodigo = l.Log_Aplicacion_Api_Codigo,
    ClienteCodigo = l.Cliente_Codigo,
    CorrelationId = l.Log_Aplicacion_CorrelationId,
    Categoria = l.Log_Aplicacion_Nivel,
    Metodo = l.Log_Aplicacion_Metodo,
    Ruta = l.Log_Aplicacion_Ruta,
    EstadoHttp = CAST(NULL AS INT),
    TiempoRespuestaMs = CAST(NULL AS BIGINT),
    Mensaje = l.Log_Aplicacion_Mensaje,
    Detalle = l.Log_Aplicacion_Excepcion
FROM Seguridad.Log_Aplicacion AS l
WHERE l.Log_Aplicacion_CorrelationId = @CorrelationId

UNION ALL

SELECT
    Fuente = N'Seguridad.Metrica_Solicitud',
    Fecha = m.Metrica_Solicitud_Fecha_Creacion,
    ApiCodigo = m.Metrica_Solicitud_Api_Codigo,
    ClienteCodigo = m.Cliente_Codigo,
    CorrelationId = m.Metrica_Solicitud_Correlation_Id,
    Categoria = N'Metrica',
    Metodo = m.Metrica_Solicitud_Metodo_Http,
    Ruta = m.Metrica_Solicitud_Ruta_Request,
    EstadoHttp = m.Metrica_Solicitud_Codigo_Estado,
    TiempoRespuestaMs = m.Metrica_Solicitud_Tiempo_Respuesta_Ms,
    Mensaje = N'Request metric',
    Detalle = CAST(NULL AS NVARCHAR(MAX))
FROM Seguridad.Metrica_Solicitud AS m
WHERE m.Metrica_Solicitud_Correlation_Id = @CorrelationId

UNION ALL

SELECT
    Fuente = N'Seguridad.Seguridad_Evento',
    Fecha = e.Evento_Seguridad_Fecha,
    ApiCodigo = e.Evento_Seguridad_Api_Codigo,
    ClienteCodigo = e.Cliente_Codigo,
    CorrelationId = e.Evento_Seguridad_CorrelationId,
    Categoria = e.Evento_Seguridad_Tipo_Evento,
    Metodo = CAST(NULL AS NVARCHAR(128)),
    Ruta = e.Evento_Seguridad_Endpoint,
    EstadoHttp = CAST(NULL AS INT),
    TiempoRespuestaMs = CAST(NULL AS BIGINT),
    Mensaje = CONCAT(N'IP: ', e.Evento_Seguridad_Ip),
    Detalle = CONCAT(
        N'Origin: ', COALESCE(e.Evento_Seguridad_Origin, N'(null)'),
        N' | UserAgent: ', COALESCE(e.Evento_Seguridad_UserAgent, N'(null)')
    )
FROM Seguridad.Seguridad_Evento AS e
WHERE e.Evento_Seguridad_CorrelationId = @CorrelationId

ORDER BY Fecha ASC, Fuente ASC;

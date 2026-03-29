/*
======================================================
 Stored Procedure: Seguridad.sp_Limpiar_MetricaSolicitud
------------------------------------------------------
 Objetivo:
   Eliminar metricas tecnicas antiguas.

 Politica de Retencion:
   - Metricas: 180 dias

 Ejecucion:
   - Job SQL Server Agent
   - Frecuencia: diaria

======================================================
*/

USE AppDb;
GO

CREATE OR ALTER PROCEDURE Seguridad.sp_Limpiar_MetricaSolicitud
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaLimite DATETIME2 = DATEADD(DAY, -180, SYSDATETIME());

    DELETE FROM Seguridad.Metrica_Solicitud
    WHERE Metrica_Solicitud_Fecha_Creacion < @FechaLimite;
END
GO

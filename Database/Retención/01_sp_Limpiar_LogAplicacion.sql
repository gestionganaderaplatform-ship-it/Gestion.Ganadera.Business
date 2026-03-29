/*
======================================================
 Stored Procedure: Seguridad.sp_Limpiar_LogAplicacion
------------------------------------------------------
 Objetivo:
   Eliminar logs de aplicacion con mas de 90 dias
   de antiguedad (Serilog).

 Politica de Retencion:
   - Logs: 90 dias

 Ejecucion:
   - Job SQL Server Agent
   - Frecuencia: diaria
   - Horario recomendado: madrugada

 Responsable:
   - Infraestructura / DBA

======================================================
*/

USE AppDb;
GO

CREATE OR ALTER PROCEDURE Seguridad.sp_Limpiar_LogAplicacion
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @FechaLimite DATETIME2 = DATEADD(DAY, -90, SYSDATETIME());

    DELETE FROM Seguridad.Log_Aplicacion
    WHERE TimeStamp < @FechaLimite;
END
GO

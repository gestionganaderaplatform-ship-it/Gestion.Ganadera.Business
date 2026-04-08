/*
======================================================
 Script: Reset de datos - Gestion.Ganadera.API
------------------------------------------------------
 Objetivo:
   Limpiar datos operativos del esquema Seguridad para
   pruebas locales o de test del API de negocio.

 Nota:
   Se usa DELETE + DBCC CHECKIDENT para permitir
   ejecucion segura aunque existan referencias o la
   tabla de logs aun no exista.

 Uso:
   - Ejecutar sobre la base del API de negocio
   - No usar en produccion
======================================================
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;
GO

IF OBJECT_ID('Seguridad.Auditoria', 'U') IS NOT NULL
    DELETE FROM Seguridad.Auditoria;

IF OBJECT_ID('Seguridad.Log_Aplicacion', 'U') IS NOT NULL
    DELETE FROM Seguridad.Log_Aplicacion;

IF OBJECT_ID('Seguridad.Metrica_Solicitud', 'U') IS NOT NULL
    DELETE FROM Seguridad.Metrica_Solicitud;

IF OBJECT_ID('Seguridad.Seguridad_Evento', 'U') IS NOT NULL
    DELETE FROM Seguridad.Seguridad_Evento;
GO

IF OBJECT_ID('Seguridad.Auditoria', 'U') IS NOT NULL
    DBCC CHECKIDENT ('Seguridad.Auditoria', RESEED, 0);

IF OBJECT_ID('Seguridad.Log_Aplicacion', 'U') IS NOT NULL
    DBCC CHECKIDENT ('Seguridad.Log_Aplicacion', RESEED, 0);

IF OBJECT_ID('Seguridad.Metrica_Solicitud', 'U') IS NOT NULL
    DBCC CHECKIDENT ('Seguridad.Metrica_Solicitud', RESEED, 0);

IF OBJECT_ID('Seguridad.Seguridad_Evento', 'U') IS NOT NULL
    DBCC CHECKIDENT ('Seguridad.Seguridad_Evento', RESEED, 0);
GO

COMMIT TRANSACTION;
GO

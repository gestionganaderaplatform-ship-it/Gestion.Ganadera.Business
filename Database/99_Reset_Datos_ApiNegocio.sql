/*
======================================================
  Script: Reset de datos - Gestion.Ganadera.Business.API
------------------------------------------------------
 Objetivo:
   Limpiar datos operativos, funcionales y catalogos
   del dominio Ganaderia para pruebas locales o de test.

 Conserva:
   - __EFMigrationsHistory
   - Aplicacion.Menu_Navegacion

 Nota:
   Se usa DELETE + DBCC CHECKIDENT en lugar de TRUNCATE
   porque existen llaves foraneas entre tablas.

 Uso:
   - Ejecutar sobre la base del API de negocio
   - No usar en produccion
======================================================
*/

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRANSACTION;

/* Trazabilidad tecnica */
IF OBJECT_ID('Seguridad.Auditoria', 'U') IS NOT NULL
    DELETE FROM Seguridad.Auditoria;

IF OBJECT_ID('Seguridad.Log_Aplicacion', 'U') IS NOT NULL
    DELETE FROM Seguridad.Log_Aplicacion;

IF OBJECT_ID('Seguridad.Metrica_Solicitud', 'U') IS NOT NULL
    DELETE FROM Seguridad.Metrica_Solicitud;

IF OBJECT_ID('Seguridad.Seguridad_Evento', 'U') IS NOT NULL
    DELETE FROM Seguridad.Seguridad_Evento;

/* Transaccional ganaderia */
IF OBJECT_ID('Ganaderia.Evento_Detalle_Compra', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Evento_Detalle_Compra;

IF OBJECT_ID('Ganaderia.Evento_Detalle_Registro_Existente', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Evento_Detalle_Registro_Existente;

IF OBJECT_ID('Ganaderia.Evento_Ganadero_Animal', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Evento_Ganadero_Animal;

IF OBJECT_ID('Ganaderia.Identificador_Animal', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Identificador_Animal;

IF OBJECT_ID('Ganaderia.Evento_Ganadero', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Evento_Ganadero;

IF OBJECT_ID('Ganaderia.Animal', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Animal;

/* Estructura operativa y catalogos por cliente */
IF OBJECT_ID('Ganaderia.Potrero', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Potrero;

IF OBJECT_ID('Ganaderia.Finca', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Finca;

IF OBJECT_ID('Ganaderia.Categoria_Animal', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Categoria_Animal;

IF OBJECT_ID('Ganaderia.Rango_Edad', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Rango_Edad;

IF OBJECT_ID('Ganaderia.Tipo_Identificador', 'U') IS NOT NULL
    DELETE FROM Ganaderia.Tipo_Identificador;

IF OBJECT_ID('Seguridad.Auditoria', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Seguridad.Auditoria'), 'Auditoria_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Seguridad.Auditoria', RESEED, 0);

IF OBJECT_ID('Seguridad.Log_Aplicacion', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Seguridad.Log_Aplicacion'), 'Log_Aplicacion_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Seguridad.Log_Aplicacion', RESEED, 0);

IF OBJECT_ID('Seguridad.Metrica_Solicitud', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Seguridad.Metrica_Solicitud'), 'Metrica_Solicitud_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Seguridad.Metrica_Solicitud', RESEED, 0);

IF OBJECT_ID('Seguridad.Seguridad_Evento', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Seguridad.Seguridad_Evento'), 'Seguridad_Evento_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Seguridad.Seguridad_Evento', RESEED, 0);

IF OBJECT_ID('Ganaderia.Evento_Detalle_Compra', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Evento_Detalle_Compra'), 'Evento_Ganadero_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Evento_Detalle_Compra', RESEED, 0);

IF OBJECT_ID('Ganaderia.Evento_Detalle_Registro_Existente', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Evento_Detalle_Registro_Existente'), 'Evento_Ganadero_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Evento_Detalle_Registro_Existente', RESEED, 0);

IF OBJECT_ID('Ganaderia.Evento_Ganadero_Animal', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Evento_Ganadero_Animal'), 'Evento_Ganadero_Animal_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Evento_Ganadero_Animal', RESEED, 0);

IF OBJECT_ID('Ganaderia.Identificador_Animal', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Identificador_Animal'), 'Identificador_Animal_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Identificador_Animal', RESEED, 0);

IF OBJECT_ID('Ganaderia.Evento_Ganadero', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Evento_Ganadero'), 'Evento_Ganadero_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Evento_Ganadero', RESEED, 0);

IF OBJECT_ID('Ganaderia.Animal', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Animal'), 'Animal_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Animal', RESEED, 0);

IF OBJECT_ID('Ganaderia.Potrero', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Potrero'), 'Potrero_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Potrero', RESEED, 0);

IF OBJECT_ID('Ganaderia.Finca', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Finca'), 'Finca_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Finca', RESEED, 0);

IF OBJECT_ID('Ganaderia.Categoria_Animal', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Categoria_Animal'), 'Categoria_Animal_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Categoria_Animal', RESEED, 0);

IF OBJECT_ID('Ganaderia.Rango_Edad', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Rango_Edad'), 'Rango_Edad_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Rango_Edad', RESEED, 0);

IF OBJECT_ID('Ganaderia.Tipo_Identificador', 'U') IS NOT NULL
   AND COLUMNPROPERTY(OBJECT_ID('Ganaderia.Tipo_Identificador'), 'Tipo_Identificador_Codigo', 'IsIdentity') = 1
    DBCC CHECKIDENT ('Ganaderia.Tipo_Identificador', RESEED, 0);

COMMIT TRANSACTION;
GO

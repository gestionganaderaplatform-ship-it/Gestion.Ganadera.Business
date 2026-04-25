/*
======================================================
  Script: Reset suave de datos - Gestion.Ganadera.Business.API
------------------------------------------------------
 Objetivo:
   Limpiar solo los datos transaccionales y de trazabilidad
   para repetir pruebas funcionales sin perder la estructura
   base de trabajo.

 Conserva:
   - __EFMigrationsHistory
   - Aplicacion.Menu_Navegacion
   - Ganaderia.Finca
   - Ganaderia.Potrero
   - Ganaderia.Categoria_Animal
   - Ganaderia.Rango_Edad
   - Ganaderia.Tipo_Identificador

 Uso sugerido:
   - Repetir pruebas de registro, compra, consulta e historial
   - No usar cuando se quiera rehacer onboarding desde cero
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

COMMIT TRANSACTION;
GO

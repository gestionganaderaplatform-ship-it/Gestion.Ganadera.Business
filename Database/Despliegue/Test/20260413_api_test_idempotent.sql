IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326203657_InitialCreate'
)
BEGIN
    IF SCHEMA_ID(N'Seguridad') IS NULL EXEC(N'CREATE SCHEMA [Seguridad];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326203657_InitialCreate'
)
BEGIN
    IF OBJECT_ID(N'[Seguridad].[Auditoria]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Seguridad].[Auditoria] (
            [Auditoria_Codigo] bigint NOT NULL IDENTITY,
            [Auditoria_Api_Codigo] nvarchar(100) NOT NULL,
            [Auditoria_Nombre_Tabla] nvarchar(max) NOT NULL,
            [Auditoria_Valor_Clave] nvarchar(max) NOT NULL,
            [Auditoria_Valores_Viejos] nvarchar(max) NOT NULL,
            [Auditoria_Nuevos_Valores] nvarchar(max) NOT NULL,
            [Auditoria_Modificado_Por] nvarchar(max) NOT NULL,
            [Auditoria_Fecha_Modificado] datetime2 NOT NULL,
            CONSTRAINT [PK_Auditoria] PRIMARY KEY ([Auditoria_Codigo])
        );
    END;

    IF OBJECT_ID(N'[Seguridad].[Metrica_Solicitud]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Seguridad].[Metrica_Solicitud] (
            [Metrica_Solicitud_Codigo] bigint NOT NULL IDENTITY,
            [Metrica_Solicitud_Api_Codigo] nvarchar(100) NOT NULL,
            [Metrica_Solicitud_Ruta_Request] nvarchar(500) NOT NULL,
            [Metrica_Solicitud_Metodo_Http] nvarchar(10) NOT NULL,
            [Metrica_Solicitud_Codigo_Estado] int NOT NULL,
            [Metrica_Solicitud_Tiempo_Respuesta_Ms] bigint NOT NULL,
            [Metrica_Solicitud_Correlation_Id] nvarchar(100) NULL,
            [Metrica_Solicitud_Fecha_Creacion] datetime2 NOT NULL DEFAULT (SYSDATETIME()),
            CONSTRAINT [PK_Metrica_Solicitud] PRIMARY KEY ([Metrica_Solicitud_Codigo])
        );
    END;

    IF OBJECT_ID(N'[Seguridad].[Seguridad_Evento]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Seguridad].[Seguridad_Evento] (
            [Evento_Seguridad_Codigo] bigint NOT NULL IDENTITY,
            [Evento_Seguridad_Api_Codigo] nvarchar(100) NOT NULL,
            [Evento_Seguridad_Tipo_Evento] nvarchar(max) NOT NULL,
            [Evento_Seguridad_Ip] nvarchar(max) NOT NULL,
            [Evento_Seguridad_Endpoint] nvarchar(max) NOT NULL,
            [Evento_Seguridad_Origin] nvarchar(max) NULL,
            [Evento_Seguridad_UserAgent] nvarchar(max) NULL,
            [Evento_Seguridad_CorrelationId] nvarchar(max) NULL,
            [Evento_Seguridad_Fecha] datetime2 NOT NULL DEFAULT (SYSDATETIME()),
            CONSTRAINT [PK_Seguridad_Evento] PRIMARY KEY ([Evento_Seguridad_Codigo])
        );
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260326203657_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260326203657_InitialCreate', N'9.0.13');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260401131307_AddClientTracing'
)
BEGIN
    IF COL_LENGTH('Seguridad.Seguridad_Evento', 'Cliente_Codigo') IS NULL
    BEGIN
        ALTER TABLE [Seguridad].[Seguridad_Evento] ADD [Cliente_Codigo] BIGINT NULL;
    END;

    IF COL_LENGTH('Seguridad.Metrica_Solicitud', 'Cliente_Codigo') IS NULL
    BEGIN
        ALTER TABLE [Seguridad].[Metrica_Solicitud] ADD [Cliente_Codigo] BIGINT NULL;
    END;

    IF COL_LENGTH('Seguridad.Auditoria', 'Cliente_Codigo') IS NULL
    BEGIN
        ALTER TABLE [Seguridad].[Auditoria] ADD [Cliente_Codigo] BIGINT NULL;
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = 'IX_Seguridad_Evento_Cliente_Codigo'
          AND object_id = OBJECT_ID('[Seguridad].[Seguridad_Evento]'))
    BEGIN
        CREATE INDEX [IX_Seguridad_Evento_Cliente_Codigo]
            ON [Seguridad].[Seguridad_Evento]([Cliente_Codigo]);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = 'IX_Metrica_Solicitud_Cliente_Codigo'
          AND object_id = OBJECT_ID('[Seguridad].[Metrica_Solicitud]'))
    BEGIN
        CREATE INDEX [IX_Metrica_Solicitud_Cliente_Codigo]
            ON [Seguridad].[Metrica_Solicitud]([Cliente_Codigo]);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = 'IX_Auditoria_Cliente_Codigo'
          AND object_id = OBJECT_ID('[Seguridad].[Auditoria]'))
    BEGIN
        CREATE INDEX [IX_Auditoria_Cliente_Codigo]
            ON [Seguridad].[Auditoria]([Cliente_Codigo]);
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260401131307_AddClientTracing'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260401131307_AddClientTracing', N'9.0.13');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408141845_AddApplicationLogTable'
)
BEGIN
    IF SCHEMA_ID(N'Seguridad') IS NULL EXEC(N'CREATE SCHEMA [Seguridad];');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408141845_AddApplicationLogTable'
)
BEGIN
    IF OBJECT_ID(N'[Seguridad].[Log_Aplicacion]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Seguridad].[Log_Aplicacion]
        (
            [Log_Aplicacion_Codigo] BIGINT IDENTITY(1,1) NOT NULL,
            [Log_Aplicacion_Api_Codigo] NVARCHAR(100) NOT NULL,
            [Log_Aplicacion_Nivel] NVARCHAR(128) NOT NULL,
            [Log_Aplicacion_Mensaje] NVARCHAR(MAX) NOT NULL,
            [Log_Aplicacion_Excepcion] NVARCHAR(MAX) NULL,
            [Log_Aplicacion_Origen] NVARCHAR(128) NOT NULL,
            [Log_Aplicacion_Metodo] NVARCHAR(128) NULL,
            [Log_Aplicacion_Ruta] NVARCHAR(500) NULL,
            [Cliente_Codigo] BIGINT NULL,
            [Log_Aplicacion_Usuario] NVARCHAR(200) NULL,
            [Log_Aplicacion_CorrelationId] NVARCHAR(100) NOT NULL,
            [Log_Aplicacion_Fecha] DATETIME2 NOT NULL,
            CONSTRAINT [PK_Log_Aplicacion] PRIMARY KEY ([Log_Aplicacion_Codigo])
        );
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = N'IX_Log_Aplicacion_Cliente_Codigo'
          AND object_id = OBJECT_ID(N'[Seguridad].[Log_Aplicacion]'))
    BEGIN
        CREATE INDEX [IX_Log_Aplicacion_Cliente_Codigo]
            ON [Seguridad].[Log_Aplicacion]([Cliente_Codigo]);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = N'IX_Log_Aplicacion_Log_Aplicacion_Fecha'
          AND object_id = OBJECT_ID(N'[Seguridad].[Log_Aplicacion]'))
    BEGIN
        CREATE INDEX [IX_Log_Aplicacion_Log_Aplicacion_Fecha]
            ON [Seguridad].[Log_Aplicacion]([Log_Aplicacion_Fecha]);
    END;

    IF NOT EXISTS (
        SELECT 1
        FROM sys.indexes
        WHERE name = N'IX_Log_Aplicacion_Log_Aplicacion_Nivel_Log_Aplicacion_Fecha'
          AND object_id = OBJECT_ID(N'[Seguridad].[Log_Aplicacion]'))
    BEGIN
        CREATE INDEX [IX_Log_Aplicacion_Log_Aplicacion_Nivel_Log_Aplicacion_Fecha]
            ON [Seguridad].[Log_Aplicacion]([Log_Aplicacion_Nivel], [Log_Aplicacion_Fecha]);
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408141845_AddApplicationLogTable'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260408141845_AddApplicationLogTable', N'9.0.13');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408143149_ConstrainOperationalTextLengths'
)
BEGIN
    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_UserAgent] = LEFT([Evento_Seguridad_UserAgent], 1000)
    WHERE [Evento_Seguridad_UserAgent] IS NOT NULL
      AND LEN([Evento_Seguridad_UserAgent]) > 1000;

    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_Tipo_Evento] = LEFT([Evento_Seguridad_Tipo_Evento], 100)
    WHERE LEN([Evento_Seguridad_Tipo_Evento]) > 100;

    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_Origin] = LEFT([Evento_Seguridad_Origin], 200)
    WHERE [Evento_Seguridad_Origin] IS NOT NULL
      AND LEN([Evento_Seguridad_Origin]) > 200;

    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_Ip] = LEFT([Evento_Seguridad_Ip], 45)
    WHERE LEN([Evento_Seguridad_Ip]) > 45;

    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_Endpoint] = LEFT([Evento_Seguridad_Endpoint], 500)
    WHERE LEN([Evento_Seguridad_Endpoint]) > 500;

    UPDATE [Seguridad].[Seguridad_Evento]
    SET [Evento_Seguridad_CorrelationId] = LEFT([Evento_Seguridad_CorrelationId], 100)
    WHERE [Evento_Seguridad_CorrelationId] IS NOT NULL
      AND LEN([Evento_Seguridad_CorrelationId]) > 100;

    UPDATE [Seguridad].[Auditoria]
    SET [Auditoria_Valor_Clave] = LEFT([Auditoria_Valor_Clave], 200)
    WHERE LEN([Auditoria_Valor_Clave]) > 200;

    UPDATE [Seguridad].[Auditoria]
    SET [Auditoria_Nombre_Tabla] = LEFT([Auditoria_Nombre_Tabla], 150)
    WHERE LEN([Auditoria_Nombre_Tabla]) > 150;

    UPDATE [Seguridad].[Auditoria]
    SET [Auditoria_Modificado_Por] = LEFT([Auditoria_Modificado_Por], 200)
    WHERE LEN([Auditoria_Modificado_Por]) > 200;

    -- Alter columns to specific lengths safely
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_UserAgent] NVARCHAR(1000) NULL;
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_Tipo_Evento] NVARCHAR(100) NOT NULL;
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_Origin] NVARCHAR(200) NULL;
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_Ip] NVARCHAR(45) NOT NULL;
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_Endpoint] NVARCHAR(500) NOT NULL;
    ALTER TABLE [Seguridad].[Seguridad_Evento] ALTER COLUMN [Evento_Seguridad_CorrelationId] NVARCHAR(100) NULL;

    ALTER TABLE [Seguridad].[Auditoria] ALTER COLUMN [Auditoria_Valor_Clave] NVARCHAR(200) NOT NULL;
    ALTER TABLE [Seguridad].[Auditoria] ALTER COLUMN [Auditoria_Nombre_Tabla] NVARCHAR(150) NOT NULL;
    ALTER TABLE [Seguridad].[Auditoria] ALTER COLUMN [Auditoria_Modificado_Por] NVARCHAR(200) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408143149_ConstrainOperationalTextLengths'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260408143149_ConstrainOperationalTextLengths', N'9.0.13');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408235500_AddMenuNavigation'
)
BEGIN
    IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'Aplicacion')
    BEGIN
        EXEC('CREATE SCHEMA [Aplicacion]')
    END

    IF OBJECT_ID(N'[Aplicacion].[Menu_Navegacion]', N'U') IS NULL
    BEGIN
        CREATE TABLE [Aplicacion].[Menu_Navegacion] (
            [Menu_Navegacion_Codigo] bigint NOT NULL IDENTITY,
            [Menu_Navegacion_Padre_Codigo] bigint NULL,
            [Menu_Navegacion_Clave] nvarchar(100) NOT NULL,
            [Menu_Navegacion_Titulo] nvarchar(150) NOT NULL,
            [Menu_Navegacion_Icono] nvarchar(100) NOT NULL,
            [Menu_Navegacion_Tipo] nvarchar(30) NOT NULL,
            [Menu_Navegacion_Ruta] nvarchar(250) NULL,
            [Menu_Navegacion_Accion] nvarchar(50) NULL,
            [Menu_Navegacion_Orden] int NOT NULL,
            [Menu_Navegacion_Esta_Activo] bit NOT NULL,
            [Menu_Navegacion_Requiere_Cuenta_Padre] bit NOT NULL,
            [Menu_Navegacion_Permiso_Requerido] nvarchar(150) NULL,
            CONSTRAINT [PK_Menu_Navegacion] PRIMARY KEY ([Menu_Navegacion_Codigo]),
            CONSTRAINT [FK_Menu_Navegacion_Menu_Navegacion_Menu_Navegacion_Padre_Codigo] FOREIGN KEY ([Menu_Navegacion_Padre_Codigo]) REFERENCES [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo])
        );

        CREATE UNIQUE INDEX [IX_Menu_Navegacion_Menu_Navegacion_Clave] ON [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Clave]);
        CREATE INDEX [IX_Menu_Navegacion_Menu_Navegacion_Padre_Codigo_Menu_Navegacion_Orden] ON [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Orden]);

        -- Seed Data
        SET IDENTITY_INSERT [Aplicacion].[Menu_Navegacion] ON;
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (1, NULL, N'inicio', 1, N'pi pi-home', 10, NULL, NULL, 0, N'/inicio', N'route', N'Inicio')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (2, NULL, N'seguridad', 1, N'pi pi-shield', 20, NULL, NULL, 0, N'/seguridad', N'group', N'Seguridad')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (3, NULL, N'configuracion', 1, N'pi pi-cog', 30, NULL, NULL, 0, N'/configuracion', N'group', N'Configuración')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (4, N'logout', N'cerrar-sesion', 1, N'pi pi-sign-out', 40, NULL, NULL, 0, NULL, N'action', N'Cerrar sesión')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (5, NULL, N'seguridad-auditoria', 1, N'pi pi-history', 10, 2, NULL, 0, N'/seguridad/auditoria', N'route', N'Auditoría')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (6, NULL, N'seguridad-sesiones', 1, N'pi pi-desktop', 20, 2, NULL, 0, N'/seguridad/sesiones', N'route', N'Sesiones')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (7, NULL, N'seguridad-accesos', 1, N'pi pi-key', 30, 2, NULL, 0, N'/seguridad/accesos', N'route', N'Accesos')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (8, NULL, N'configuracion-preferencias', 1, N'pi pi-sliders-h', 10, 3, NULL, 0, N'/configuracion/preferencias', N'route', N'Preferencias')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (9, NULL, N'configuracion-cuenta', 1, N'pi pi-user', 20, 3, NULL, 0, N'/configuracion/cuenta', N'route', N'Cuenta')
        INSERT [Aplicacion].[Menu_Navegacion] ([Menu_Navegacion_Codigo], [Menu_Navegacion_Accion], [Menu_Navegacion_Clave], [Menu_Navegacion_Esta_Activo], [Menu_Navegacion_Icono], [Menu_Navegacion_Orden], [Menu_Navegacion_Padre_Codigo], [Menu_Navegacion_Permiso_Requerido], [Menu_Navegacion_Requiere_Cuenta_Padre], [Menu_Navegacion_Ruta], [Menu_Navegacion_Tipo], [Menu_Navegacion_Titulo]) VALUES (10, NULL, N'configuracion-delegados', 1, N'pi pi-users', 30, 3, NULL, 1, N'/configuracion/delegados', N'route', N'Delegados')
        SET IDENTITY_INSERT [Aplicacion].[Menu_Navegacion] OFF;
    END;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260408235500_AddMenuNavigation'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260408235500_AddMenuNavigation', N'9.0.13');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260409040039_FixMenuNavigationTitles'
)
BEGIN
    UPDATE Aplicacion.Menu_Navegacion
    SET Menu_Navegacion_Titulo = N'Configuración'
    WHERE Menu_Navegacion_Clave = N'configuracion'
      AND Menu_Navegacion_Titulo <> N'Configuración';

    UPDATE Aplicacion.Menu_Navegacion
    SET Menu_Navegacion_Titulo = N'Cerrar sesión'
    WHERE Menu_Navegacion_Clave = N'cerrar-sesion'
      AND Menu_Navegacion_Titulo <> N'Cerrar sesión';

    UPDATE Aplicacion.Menu_Navegacion
    SET Menu_Navegacion_Titulo = N'Auditoría'
    WHERE Menu_Navegacion_Clave = N'seguridad-auditoria'
      AND Menu_Navegacion_Titulo <> N'Auditoría';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260409040039_FixMenuNavigationTitles'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260409040039_FixMenuNavigationTitles', N'9.0.13');
END;

COMMIT;
GO


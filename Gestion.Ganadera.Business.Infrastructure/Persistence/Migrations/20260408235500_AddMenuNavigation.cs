using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMenuNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu_Navegacion",
                schema: "Aplicacion");
        }
    }
}

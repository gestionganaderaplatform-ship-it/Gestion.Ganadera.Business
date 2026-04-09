using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.Sql(
                """
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Metrica_Solicitud",
                schema: "Seguridad");

            migrationBuilder.DropTable(
                name: "Seguridad_Evento",
                schema: "Seguridad");
        }
    }
}

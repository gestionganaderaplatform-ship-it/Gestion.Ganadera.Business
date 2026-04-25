using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationLogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Seguridad");

            migrationBuilder.Sql(
                """
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                IF OBJECT_ID(N'[Seguridad].[Log_Aplicacion]', N'U') IS NOT NULL
                BEGIN
                    DROP TABLE [Seguridad].[Log_Aplicacion];
                END;
                """);
        }
    }
}

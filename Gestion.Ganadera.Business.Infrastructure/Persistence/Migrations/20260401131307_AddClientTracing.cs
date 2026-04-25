using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddClientTracing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op: estas columnas pueden ser compartidas con otros contextos del sistema.
        }
    }
}

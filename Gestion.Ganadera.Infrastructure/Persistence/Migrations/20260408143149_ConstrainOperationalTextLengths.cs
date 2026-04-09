using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ConstrainOperationalTextLengths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
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
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_UserAgent",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Tipo_Evento",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Origin",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Ip",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Endpoint",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_CorrelationId",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Valor_Clave",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Nombre_Tabla",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Modificado_Por",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false);
        }
    }
}

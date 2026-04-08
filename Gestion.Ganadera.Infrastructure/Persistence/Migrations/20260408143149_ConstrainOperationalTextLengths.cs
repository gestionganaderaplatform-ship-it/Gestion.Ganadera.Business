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
                """);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_UserAgent",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Tipo_Evento",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Origin",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Ip",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Endpoint",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_CorrelationId",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Valor_Clave",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Nombre_Tabla",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Modificado_Por",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_UserAgent",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Tipo_Evento",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Origin",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Ip",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(45)",
                oldMaxLength: 45);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_Endpoint",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Evento_Seguridad_CorrelationId",
                schema: "Seguridad",
                table: "Seguridad_Evento",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Valor_Clave",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Nombre_Tabla",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Auditoria_Modificado_Por",
                schema: "Seguridad",
                table: "Auditoria",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}

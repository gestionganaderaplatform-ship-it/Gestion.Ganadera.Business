using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPalpacionMaestrosYEstadoAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Animal_Estado_Reproductivo_Actual",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Ultima_Palpacion_Fecha",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Animal_Ultimo_Resultado_Reproductivo",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Palpacion_Resultado",
                schema: "Ganaderia",
                columns: table => new
                {
                    Palpacion_Resultado_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Palpacion_Resultado_Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Palpacion_Resultado_Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palpacion_Resultado", x => x.Palpacion_Resultado_Codigo);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palpacion_Resultado",
                schema: "Ganaderia");

            migrationBuilder.DropColumn(
                name: "Animal_Estado_Reproductivo_Actual",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Ultima_Palpacion_Fecha",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Ultimo_Resultado_Reproductivo",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionCatalogoCausaMuerte : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Evento_Detalle_Muerte_Causa",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte");

            migrationBuilder.AddColumn<long>(
                name: "Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Causa_Muerte",
                schema: "Ganaderia",
                columns: table => new
                {
                    Causa_Muerte_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Causa_Muerte_Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Causa_Muerte_Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Causa_Muerte_Activa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Causa_Muerte_Orden = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Causa_Muerte", x => x.Causa_Muerte_Codigo);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Muerte_Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte",
                column: "Causa_Muerte_Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Evento_Detalle_Muerte_Causa_Muerte_Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte",
                column: "Causa_Muerte_Codigo",
                principalSchema: "Ganaderia",
                principalTable: "Causa_Muerte",
                principalColumn: "Causa_Muerte_Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evento_Detalle_Muerte_Causa_Muerte_Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte");

            migrationBuilder.DropTable(
                name: "Causa_Muerte",
                schema: "Ganaderia");

            migrationBuilder.DropIndex(
                name: "IX_Evento_Detalle_Muerte_Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte");

            migrationBuilder.DropColumn(
                name: "Causa_Muerte_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte");

            migrationBuilder.AddColumn<string>(
                name: "Evento_Detalle_Muerte_Causa",
                schema: "Ganaderia",
                table: "Evento_Detalle_Muerte",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}

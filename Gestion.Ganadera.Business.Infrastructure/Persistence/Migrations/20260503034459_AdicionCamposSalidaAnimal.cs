using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionCamposSalidaAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Fecha_Salida",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Animal_Motivo_Salida",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Muerte",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Muerte_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Muerte_Causa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Evento_Detalle_Muerte_Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Muerte", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Muerte_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Muerte",
                schema: "Ganaderia");

            migrationBuilder.DropColumn(
                name: "Animal_Fecha_Salida",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Motivo_Salida",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDescarteMaestrosYProceso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Descarte_Motivo",
                schema: "Ganaderia",
                columns: table => new
                {
                    Descarte_Motivo_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descarte_Motivo_Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descarte_Motivo_Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descarte_Motivo", x => x.Descarte_Motivo_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Descarte",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Descarte_Motivo_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Descarte_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Descarte_Destino = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Evento_Detalle_Descarte_Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Evento_Detalle_Descarte_Observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Descarte", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Descarte_Descarte_Motivo_Descarte_Motivo_Codigo",
                        column: x => x.Descarte_Motivo_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Descarte_Motivo",
                        principalColumn: "Descarte_Motivo_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Descarte_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Descarte_Descarte_Motivo_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Descarte",
                column: "Descarte_Motivo_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Descarte",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Descarte_Motivo",
                schema: "Ganaderia");
        }
    }
}

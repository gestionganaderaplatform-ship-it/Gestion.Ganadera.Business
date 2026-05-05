using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDesteteProceso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Destete",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Animal_Codigo_Madre = table.Column<long>(type: "bigint", nullable: false),
                    Potrero_Destino_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Evento_Detalle_Destete_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Destete_Responsable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Evento_Detalle_Destete_Observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Destete", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Destete_Animal_Animal_Codigo_Madre",
                        column: x => x.Animal_Codigo_Madre,
                        principalSchema: "Ganaderia",
                        principalTable: "Animal",
                        principalColumn: "Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Destete_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Destete_Potrero_Potrero_Destino_Codigo",
                        column: x => x.Potrero_Destino_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Potrero",
                        principalColumn: "Potrero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Destete_Animal_Codigo_Madre",
                schema: "Ganaderia",
                table: "Evento_Detalle_Destete",
                column: "Animal_Codigo_Madre");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Destete_Potrero_Destino_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Destete",
                column: "Potrero_Destino_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Destete",
                schema: "Ganaderia");
        }
    }
}

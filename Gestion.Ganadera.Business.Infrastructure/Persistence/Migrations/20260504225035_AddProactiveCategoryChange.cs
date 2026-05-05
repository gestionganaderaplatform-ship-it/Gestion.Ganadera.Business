using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProactiveCategoryChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Categoria_Animal_Meses_Sugeridos",
                schema: "Ganaderia",
                table: "Categoria_Animal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cambio_Categoria_Sugerido",
                schema: "Ganaderia",
                columns: table => new
                {
                    Cambio_Categoria_Sugerido_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Categoria_Actual_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Categoria_Sugerida_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Sugerencia_Motivo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Sugerencia_Estado = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Fecha_Sugerencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cambio_Categoria_Sugerido", x => x.Cambio_Categoria_Sugerido_Codigo);
                    table.ForeignKey(
                        name: "FK_Cambio_Categoria_Sugerido_Animal_Animal_Codigo",
                        column: x => x.Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Animal",
                        principalColumn: "Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cambio_Categoria_Sugerido_Categoria_Animal_Categoria_Actual_Codigo",
                        column: x => x.Categoria_Actual_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cambio_Categoria_Sugerido_Categoria_Animal_Categoria_Sugerida_Codigo",
                        column: x => x.Categoria_Sugerida_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Cambio_Categoria",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Categoria_Anterior_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Categoria_Nueva_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Cambio_Categoria_Peso_Al_Cambio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Evento_Detalle_Cambio_Categoria_Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Cambio_Categoria", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Cambio_Categoria_Categoria_Animal_Categoria_Anterior_Codigo",
                        column: x => x.Categoria_Anterior_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Cambio_Categoria_Categoria_Animal_Categoria_Nueva_Codigo",
                        column: x => x.Categoria_Nueva_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Cambio_Categoria_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categoria_Animal_Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal",
                column: "Categoria_Animal_Siguiente_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_Categoria_Sugerido_Animal_Codigo",
                schema: "Ganaderia",
                table: "Cambio_Categoria_Sugerido",
                column: "Animal_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_Categoria_Sugerido_Categoria_Actual_Codigo",
                schema: "Ganaderia",
                table: "Cambio_Categoria_Sugerido",
                column: "Categoria_Actual_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_Categoria_Sugerido_Categoria_Sugerida_Codigo",
                schema: "Ganaderia",
                table: "Cambio_Categoria_Sugerido",
                column: "Categoria_Sugerida_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Cambio_Categoria_Sugerido_Cliente_Codigo_Sugerencia_Estado",
                schema: "Ganaderia",
                table: "Cambio_Categoria_Sugerido",
                columns: new[] { "Cliente_Codigo", "Sugerencia_Estado" });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Cambio_Categoria_Categoria_Anterior_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Cambio_Categoria",
                column: "Categoria_Anterior_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Cambio_Categoria_Categoria_Nueva_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Cambio_Categoria",
                column: "Categoria_Nueva_Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Categoria_Animal_Categoria_Animal_Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal",
                column: "Categoria_Animal_Siguiente_Codigo",
                principalSchema: "Ganaderia",
                principalTable: "Categoria_Animal",
                principalColumn: "Categoria_Animal_Codigo",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categoria_Animal_Categoria_Animal_Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal");

            migrationBuilder.DropTable(
                name: "Cambio_Categoria_Sugerido",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Evento_Detalle_Cambio_Categoria",
                schema: "Ganaderia");

            migrationBuilder.DropIndex(
                name: "IX_Categoria_Animal_Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal");

            migrationBuilder.DropColumn(
                name: "Categoria_Animal_Meses_Sugeridos",
                schema: "Ganaderia",
                table: "Categoria_Animal");

            migrationBuilder.DropColumn(
                name: "Categoria_Animal_Siguiente_Codigo",
                schema: "Ganaderia",
                table: "Categoria_Animal");
        }
    }
}

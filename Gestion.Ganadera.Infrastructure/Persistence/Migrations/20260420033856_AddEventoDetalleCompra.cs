using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEventoDetalleCompra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Compra",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Tipo_Identificador_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Compra_Identificador_Valor = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Categoria_Animal_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Rango_Edad_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Potrero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Compra_Sexo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Evento_Detalle_Compra_Fecha_Compra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Compra_Origen_Vendedor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Evento_Detalle_Compra_Valor_Individual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Evento_Detalle_Compra_Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Compra", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Compra_Categoria_Animal_Categoria_Animal_Codigo",
                        column: x => x.Categoria_Animal_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Categoria_Animal",
                        principalColumn: "Categoria_Animal_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Compra_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Compra_Potrero_Potrero_Codigo",
                        column: x => x.Potrero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Potrero",
                        principalColumn: "Potrero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Compra_Rango_Edad_Rango_Edad_Codigo",
                        column: x => x.Rango_Edad_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Rango_Edad",
                        principalColumn: "Rango_Edad_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Compra_Tipo_Identificador_Tipo_Identificador_Codigo",
                        column: x => x.Tipo_Identificador_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Tipo_Identificador",
                        principalColumn: "Tipo_Identificador_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Compra_Categoria_Animal_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Compra",
                column: "Categoria_Animal_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Compra_Potrero_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Compra",
                column: "Potrero_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Compra_Rango_Edad_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Compra",
                column: "Rango_Edad_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Compra_Tipo_Identificador_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Compra",
                column: "Tipo_Identificador_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Compra",
                schema: "Ganaderia");
        }
    }
}

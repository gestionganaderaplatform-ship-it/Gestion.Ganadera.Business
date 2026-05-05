using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTratamientoSanitarioProceso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Tratamiento_Sanitario",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Tratamiento_Producto_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Tratamiento_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Tratamiento_Dosis = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Evento_Detalle_Tratamiento_Duracion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Evento_Detalle_Tratamiento_Indicacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Evento_Detalle_Tratamiento_Aplicador = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Evento_Detalle_Tratamiento_Observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Tratamiento_Sanitario", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Tratamiento_Sanitario_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Tratamiento_Sanitario_Tratamiento_Producto_Tratamiento_Producto_Codigo",
                        column: x => x.Tratamiento_Producto_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Tratamiento_Producto",
                        principalColumn: "Tratamiento_Producto_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Tratamiento_Sanitario_Tratamiento_Producto_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Tratamiento_Sanitario",
                column: "Tratamiento_Producto_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Tratamiento_Sanitario",
                schema: "Ganaderia");
        }
    }
}

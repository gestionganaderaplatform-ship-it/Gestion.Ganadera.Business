using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AdicionCamposSalidaSaludYVacunacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Ultimo_Evento_Sanitario_Fecha",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Animal_Ultimo_Evento_Sanitario_Producto",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Animal_Ultimo_Evento_Sanitario_Tipo",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Vacuna_Enfermedad",
                schema: "Ganaderia",
                columns: table => new
                {
                    Vacuna_Enfermedad_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vacuna_Enfermedad_Nombre = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Vacuna_Enfermedad_Activa = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacuna_Enfermedad", x => x.Vacuna_Enfermedad_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Vacuna",
                schema: "Ganaderia",
                columns: table => new
                {
                    Vacuna_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vacuna_Nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Vacuna_Enfermedad_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Vacuna_Activa = table.Column<bool>(type: "bit", nullable: false),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacuna", x => x.Vacuna_Codigo);
                    table.ForeignKey(
                        name: "FK_Vacuna_Vacuna_Enfermedad_Vacuna_Enfermedad_Codigo",
                        column: x => x.Vacuna_Enfermedad_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Vacuna_Enfermedad",
                        principalColumn: "Vacuna_Enfermedad_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Vacunacion",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Vacunacion_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Vacunacion_Vacuna_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Vacunacion_Enfermedad_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Evento_Detalle_Vacunacion_Ciclo = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Evento_Detalle_Vacunacion_Lote = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Evento_Detalle_Vacunacion_Vacunador = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    Evento_Detalle_Vacunacion_Dosis = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Evento_Detalle_Vacunacion_Soporte_Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Evento_Detalle_Vacunacion_Observacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Vacunacion", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Vacunacion_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Vacunacion_Vacuna_Enfermedad_Evento_Detalle_Vacunacion_Enfermedad_Codigo",
                        column: x => x.Evento_Detalle_Vacunacion_Enfermedad_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Vacuna_Enfermedad",
                        principalColumn: "Vacuna_Enfermedad_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Vacunacion_Vacuna_Evento_Detalle_Vacunacion_Vacuna_Codigo",
                        column: x => x.Evento_Detalle_Vacunacion_Vacuna_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Vacuna",
                        principalColumn: "Vacuna_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Vacunacion_Evento_Detalle_Vacunacion_Enfermedad_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Vacunacion",
                column: "Evento_Detalle_Vacunacion_Enfermedad_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Vacunacion_Evento_Detalle_Vacunacion_Vacuna_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Vacunacion",
                column: "Evento_Detalle_Vacunacion_Vacuna_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Vacuna_Cliente_Codigo_Vacuna_Nombre",
                schema: "Ganaderia",
                table: "Vacuna",
                columns: new[] { "Cliente_Codigo", "Vacuna_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vacuna_Vacuna_Enfermedad_Codigo",
                schema: "Ganaderia",
                table: "Vacuna",
                column: "Vacuna_Enfermedad_Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Vacuna_Enfermedad_Cliente_Codigo_Vacuna_Enfermedad_Nombre",
                schema: "Ganaderia",
                table: "Vacuna_Enfermedad",
                columns: new[] { "Cliente_Codigo", "Vacuna_Enfermedad_Nombre" },
                unique: true,
                filter: "[Cliente_Codigo] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Vacunacion",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Vacuna",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Vacuna_Enfermedad",
                schema: "Ganaderia");

            migrationBuilder.DropColumn(
                name: "Animal_Ultimo_Evento_Sanitario_Fecha",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Ultimo_Evento_Sanitario_Producto",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Ultimo_Evento_Sanitario_Tipo",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

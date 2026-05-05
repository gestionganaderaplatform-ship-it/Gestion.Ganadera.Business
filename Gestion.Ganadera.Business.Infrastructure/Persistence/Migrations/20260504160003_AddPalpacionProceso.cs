using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPalpacionProceso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evento_Detalle_Palpacion",
                schema: "Ganaderia",
                columns: table => new
                {
                    Evento_Ganadero_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Palpacion_Resultado_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Evento_Detalle_Palpacion_Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Evento_Detalle_Palpacion_Responsable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Evento_Detalle_Palpacion_Dato_Complementario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Evento_Detalle_Palpacion_Observacion = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento_Detalle_Palpacion", x => x.Evento_Ganadero_Codigo);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Palpacion_Evento_Ganadero_Evento_Ganadero_Codigo",
                        column: x => x.Evento_Ganadero_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Evento_Ganadero",
                        principalColumn: "Evento_Ganadero_Codigo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evento_Detalle_Palpacion_Palpacion_Resultado_Palpacion_Resultado_Codigo",
                        column: x => x.Palpacion_Resultado_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Palpacion_Resultado",
                        principalColumn: "Palpacion_Resultado_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evento_Detalle_Palpacion_Palpacion_Resultado_Codigo",
                schema: "Ganaderia",
                table: "Evento_Detalle_Palpacion",
                column: "Palpacion_Resultado_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evento_Detalle_Palpacion",
                schema: "Ganaderia");
        }
    }
}

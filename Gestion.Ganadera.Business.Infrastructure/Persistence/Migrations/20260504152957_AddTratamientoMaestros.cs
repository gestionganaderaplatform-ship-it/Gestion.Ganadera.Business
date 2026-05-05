using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTratamientoMaestros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tratamiento_Tipo",
                schema: "Ganaderia",
                columns: table => new
                {
                    Tratamiento_Tipo_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tratamiento_Tipo_Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tratamiento_Tipo_Activa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamiento_Tipo", x => x.Tratamiento_Tipo_Codigo);
                });

            migrationBuilder.CreateTable(
                name: "Tratamiento_Producto",
                schema: "Ganaderia",
                columns: table => new
                {
                    Tratamiento_Producto_Codigo = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tratamiento_Producto_Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Tratamiento_Tipo_Codigo = table.Column<long>(type: "bigint", nullable: false),
                    Tratamiento_Producto_Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Cliente_Codigo = table.Column<long>(type: "bigint", nullable: true),
                    Fecha_Creado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    Fecha_Modificado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Creado_Por = table.Column<long>(type: "bigint", nullable: false),
                    Modificado_Por = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tratamiento_Producto", x => x.Tratamiento_Producto_Codigo);
                    table.ForeignKey(
                        name: "FK_Tratamiento_Producto_Tratamiento_Tipo_Tratamiento_Tipo_Codigo",
                        column: x => x.Tratamiento_Tipo_Codigo,
                        principalSchema: "Ganaderia",
                        principalTable: "Tratamiento_Tipo",
                        principalColumn: "Tratamiento_Tipo_Codigo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tratamiento_Producto_Tratamiento_Tipo_Codigo",
                schema: "Ganaderia",
                table: "Tratamiento_Producto",
                column: "Tratamiento_Tipo_Codigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tratamiento_Producto",
                schema: "Ganaderia");

            migrationBuilder.DropTable(
                name: "Tratamiento_Tipo",
                schema: "Ganaderia");
        }
    }
}

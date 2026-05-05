using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LimpiezaCamposSalidaYAdicionEspecificos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animal_Motivo_Salida",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "Animal_Fecha_Salida",
                schema: "Ganaderia",
                table: "Animal",
                newName: "Animal_Fecha_Venta");

            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Fecha_Muerte",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animal_Fecha_Muerte",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.RenameColumn(
                name: "Animal_Fecha_Venta",
                schema: "Ganaderia",
                table: "Animal",
                newName: "Animal_Fecha_Salida");

            migrationBuilder.AddColumn<string>(
                name: "Animal_Motivo_Salida",
                schema: "Ganaderia",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SyncGanaderiaPendingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha_Nacimiento",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Fecha_Nacimiento",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fecha_Nacimiento",
                schema: "Ganaderia",
                table: "Evento_Detalle_Registro_Existente");

            migrationBuilder.DropColumn(
                name: "Animal_Fecha_Nacimiento",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

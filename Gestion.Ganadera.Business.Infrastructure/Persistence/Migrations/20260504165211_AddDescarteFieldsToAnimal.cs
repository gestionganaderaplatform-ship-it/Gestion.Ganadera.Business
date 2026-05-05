using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDescarteFieldsToAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Fecha_Descarte",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Descarte_Motivo_Codigo",
                schema: "Ganaderia",
                table: "Animal",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animal_Fecha_Descarte",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Descarte_Motivo_Codigo",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CampoPesoAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Animal_Fecha_Peso",
                schema: "Ganaderia",
                table: "Animal",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Animal_Peso",
                schema: "Ganaderia",
                table: "Animal",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Animal_Fecha_Peso",
                schema: "Ganaderia",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Animal_Peso",
                schema: "Ganaderia",
                table: "Animal");
        }
    }
}

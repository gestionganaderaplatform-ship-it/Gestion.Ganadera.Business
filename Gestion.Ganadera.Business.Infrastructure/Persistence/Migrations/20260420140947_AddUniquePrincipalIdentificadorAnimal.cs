using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUniquePrincipalIdentificadorAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Identificador_Animal_Animal_Codigo",
                schema: "Ganaderia",
                table: "Identificador_Animal",
                column: "Animal_Codigo",
                unique: true,
                filter: "[Identificador_Animal_Es_Principal] = 1 AND [Identificador_Animal_Activo] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Identificador_Animal_Animal_Codigo",
                schema: "Ganaderia",
                table: "Identificador_Animal");
        }
    }
}

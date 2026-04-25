using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegistrarMenuHub : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE Aplicacion.Menu_Navegacion
                SET 
                  Menu_Navegacion_Tipo = N'route',
                  Menu_Navegacion_Ruta = N'/ganaderia/registrar',
                  Menu_Navegacion_Icono = N'pi pi-check-square'
                WHERE Menu_Navegacion_Clave = N'registrar';

                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'registrar-registro-existente';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE Aplicacion.Menu_Navegacion
                SET 
                  Menu_Navegacion_Tipo = N'group',
                  Menu_Navegacion_Ruta = NULL,
                  Menu_Navegacion_Icono = N'pi pi-plus-circle'
                WHERE Menu_Navegacion_Clave = N'registrar';

                DECLARE @RegistrarCodigo bigint = (SELECT Menu_Navegacion_Codigo FROM Aplicacion.Menu_Navegacion WHERE Menu_Navegacion_Clave = N'registrar');

                IF @RegistrarCodigo IS NOT NULL
                BEGIN
                    INSERT INTO Aplicacion.Menu_Navegacion
                    (
                        Menu_Navegacion_Padre_Codigo,
                        Menu_Navegacion_Clave,
                        Menu_Navegacion_Titulo,
                        Menu_Navegacion_Icono,
                        Menu_Navegacion_Tipo,
                        Menu_Navegacion_Ruta,
                        Menu_Navegacion_Orden,
                        Menu_Navegacion_Esta_Activo,
                        Menu_Navegacion_Requiere_Cuenta_Padre
                    )
                    VALUES
                    (
                        @RegistrarCodigo,
                        N'registrar-registro-existente',
                        N'Registro de existente',
                        N'pi pi-plus-circle',
                        N'route',
                        N'/ganaderia/procesos/registro-existente',
                        10,
                        1,
                        0
                    );
                END
                """);
        }
    }
}

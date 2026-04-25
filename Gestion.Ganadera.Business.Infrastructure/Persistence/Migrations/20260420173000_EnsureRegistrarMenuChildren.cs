using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Gestion.Ganadera.Business.Infrastructure.Persistence;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260420173000_EnsureRegistrarMenuChildren")]
    public partial class EnsureRegistrarMenuChildren : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DECLARE @RegistrarCodigo bigint =
                (
                    SELECT TOP (1) Menu_Navegacion_Codigo
                    FROM Aplicacion.Menu_Navegacion
                    WHERE Menu_Navegacion_Clave = N'registrar'
                    ORDER BY Menu_Navegacion_Codigo
                );

                IF @RegistrarCodigo IS NOT NULL
                BEGIN
                    IF EXISTS
                    (
                        SELECT 1
                        FROM Aplicacion.Menu_Navegacion
                        WHERE Menu_Navegacion_Clave = N'registrar-registro-existente'
                    )
                    BEGIN
                        UPDATE Aplicacion.Menu_Navegacion
                        SET
                            Menu_Navegacion_Padre_Codigo = @RegistrarCodigo,
                            Menu_Navegacion_Titulo = N'Registro de existente',
                            Menu_Navegacion_Icono = N'pi pi-plus-circle',
                            Menu_Navegacion_Tipo = N'route',
                            Menu_Navegacion_Ruta = N'/ganaderia/procesos/registro-existente',
                            Menu_Navegacion_Accion = NULL,
                            Menu_Navegacion_Orden = 10,
                            Menu_Navegacion_Esta_Activo = 1,
                            Menu_Navegacion_Requiere_Cuenta_Padre = 0,
                            Menu_Navegacion_Permiso_Requerido = NULL
                        WHERE Menu_Navegacion_Clave = N'registrar-registro-existente';
                    END
                    ELSE
                    BEGIN
                        INSERT INTO Aplicacion.Menu_Navegacion
                        (
                            Menu_Navegacion_Padre_Codigo,
                            Menu_Navegacion_Clave,
                            Menu_Navegacion_Titulo,
                            Menu_Navegacion_Icono,
                            Menu_Navegacion_Tipo,
                            Menu_Navegacion_Ruta,
                            Menu_Navegacion_Accion,
                            Menu_Navegacion_Orden,
                            Menu_Navegacion_Esta_Activo,
                            Menu_Navegacion_Requiere_Cuenta_Padre,
                            Menu_Navegacion_Permiso_Requerido
                        )
                        VALUES
                        (
                            @RegistrarCodigo,
                            N'registrar-registro-existente',
                            N'Registro de existente',
                            N'pi pi-plus-circle',
                            N'route',
                            N'/ganaderia/procesos/registro-existente',
                            NULL,
                            10,
                            1,
                            0,
                            NULL
                        );
                    END;
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'registrar-registro-existente';
                """);
        }
    }
}

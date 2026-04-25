using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Gestion.Ganadera.Business.Infrastructure.Persistence;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260420154000_AddGanaderiaMenuNavigation")]
    public partial class AddGanaderiaMenuNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DECLARE @GanaderiaCodigo bigint;
                DECLARE @GanadoCodigo bigint;
                DECLARE @RegistroExistenteCodigo bigint;

                SELECT TOP (1)
                    @GanaderiaCodigo = Menu_Navegacion_Codigo
                FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia'
                ORDER BY Menu_Navegacion_Codigo;

                IF @GanaderiaCodigo IS NULL
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
                        NULL,
                        N'ganaderia',
                        N'Ganaderia',
                        N'pi pi-warehouse',
                        N'group',
                        N'/ganaderia',
                        NULL,
                        15,
                        1,
                        0,
                        NULL
                    );

                    SET @GanaderiaCodigo = SCOPE_IDENTITY();
                END
                ELSE
                BEGIN
                    UPDATE Aplicacion.Menu_Navegacion
                    SET
                        Menu_Navegacion_Padre_Codigo = NULL,
                        Menu_Navegacion_Clave = N'ganaderia',
                        Menu_Navegacion_Titulo = N'Ganaderia',
                        Menu_Navegacion_Icono = N'pi pi-warehouse',
                        Menu_Navegacion_Tipo = N'group',
                        Menu_Navegacion_Ruta = N'/ganaderia',
                        Menu_Navegacion_Accion = NULL,
                        Menu_Navegacion_Orden = 15,
                        Menu_Navegacion_Esta_Activo = 1,
                        Menu_Navegacion_Requiere_Cuenta_Padre = 0,
                        Menu_Navegacion_Permiso_Requerido = NULL
                    WHERE Menu_Navegacion_Codigo = @GanaderiaCodigo;
                END;

                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia'
                  AND Menu_Navegacion_Codigo <> @GanaderiaCodigo;

                SELECT TOP (1)
                    @GanadoCodigo = Menu_Navegacion_Codigo
                FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia-ganado'
                ORDER BY Menu_Navegacion_Codigo;

                IF @GanadoCodigo IS NULL
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
                        @GanaderiaCodigo,
                        N'ganaderia-ganado',
                        N'Ganado',
                        N'pi pi-list',
                        N'route',
                        N'/ganaderia/ganado',
                        NULL,
                        10,
                        1,
                        0,
                        NULL
                    );

                    SET @GanadoCodigo = SCOPE_IDENTITY();
                END
                ELSE
                BEGIN
                    UPDATE Aplicacion.Menu_Navegacion
                    SET
                        Menu_Navegacion_Padre_Codigo = @GanaderiaCodigo,
                        Menu_Navegacion_Clave = N'ganaderia-ganado',
                        Menu_Navegacion_Titulo = N'Ganado',
                        Menu_Navegacion_Icono = N'pi pi-list',
                        Menu_Navegacion_Tipo = N'route',
                        Menu_Navegacion_Ruta = N'/ganaderia/ganado',
                        Menu_Navegacion_Accion = NULL,
                        Menu_Navegacion_Orden = 10,
                        Menu_Navegacion_Esta_Activo = 1,
                        Menu_Navegacion_Requiere_Cuenta_Padre = 0,
                        Menu_Navegacion_Permiso_Requerido = NULL
                    WHERE Menu_Navegacion_Codigo = @GanadoCodigo;
                END;

                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia-ganado'
                  AND Menu_Navegacion_Codigo <> @GanadoCodigo;

                SELECT TOP (1)
                    @RegistroExistenteCodigo = Menu_Navegacion_Codigo
                FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia-registro-existente'
                ORDER BY Menu_Navegacion_Codigo;

                IF @RegistroExistenteCodigo IS NULL
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
                        @GanaderiaCodigo,
                        N'ganaderia-registro-existente',
                        N'Registro de existente',
                        N'pi pi-plus-circle',
                        N'route',
                        N'/ganaderia/procesos/registro-existente',
                        NULL,
                        20,
                        1,
                        0,
                        NULL
                    );

                    SET @RegistroExistenteCodigo = SCOPE_IDENTITY();
                END
                ELSE
                BEGIN
                    UPDATE Aplicacion.Menu_Navegacion
                    SET
                        Menu_Navegacion_Padre_Codigo = @GanaderiaCodigo,
                        Menu_Navegacion_Clave = N'ganaderia-registro-existente',
                        Menu_Navegacion_Titulo = N'Registro de existente',
                        Menu_Navegacion_Icono = N'pi pi-plus-circle',
                        Menu_Navegacion_Tipo = N'route',
                        Menu_Navegacion_Ruta = N'/ganaderia/procesos/registro-existente',
                        Menu_Navegacion_Accion = NULL,
                        Menu_Navegacion_Orden = 20,
                        Menu_Navegacion_Esta_Activo = 1,
                        Menu_Navegacion_Requiere_Cuenta_Padre = 0,
                        Menu_Navegacion_Permiso_Requerido = NULL
                    WHERE Menu_Navegacion_Codigo = @RegistroExistenteCodigo;
                END;

                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'ganaderia-registro-existente'
                  AND Menu_Navegacion_Codigo <> ISNULL(@RegistroExistenteCodigo, -1);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave IN
                (
                    N'ganaderia-registro-existente',
                    N'ganaderia-ganado',
                    N'ganaderia'
                );
                """);
        }
    }
}

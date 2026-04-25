using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPlanesMenuNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DECLARE @ConfiguracionCodigo bigint;
                DECLARE @MenuPlanesCodigo bigint;

                SELECT @ConfiguracionCodigo = Menu_Navegacion_Codigo
                FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave = N'configuracion';

                SELECT TOP (1)
                    @MenuPlanesCodigo = Menu_Navegacion_Codigo
                FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave IN (N'configuracion-planes', N'planes_suscripciones')
                ORDER BY CASE
                    WHEN Menu_Navegacion_Clave = N'configuracion-planes' THEN 0
                    ELSE 1
                END;

                IF @ConfiguracionCodigo IS NOT NULL
                BEGIN
                    IF @MenuPlanesCodigo IS NULL
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
                            @ConfiguracionCodigo,
                            N'configuracion-planes',
                            N'Planes',
                            N'pi pi-wallet',
                            N'route',
                            N'/configuracion/planes',
                            NULL,
                            40,
                            1,
                            1,
                            NULL
                        );
                    END
                    ELSE
                    BEGIN
                        UPDATE Aplicacion.Menu_Navegacion
                        SET
                            Menu_Navegacion_Padre_Codigo = @ConfiguracionCodigo,
                            Menu_Navegacion_Clave = N'configuracion-planes',
                            Menu_Navegacion_Titulo = N'Planes',
                            Menu_Navegacion_Icono = N'pi pi-wallet',
                            Menu_Navegacion_Tipo = N'route',
                            Menu_Navegacion_Ruta = N'/configuracion/planes',
                            Menu_Navegacion_Accion = NULL,
                            Menu_Navegacion_Orden = 40,
                            Menu_Navegacion_Esta_Activo = 1,
                            Menu_Navegacion_Requiere_Cuenta_Padre = 1,
                            Menu_Navegacion_Permiso_Requerido = NULL
                        WHERE Menu_Navegacion_Codigo = @MenuPlanesCodigo;
                    END;

                    DELETE FROM Aplicacion.Menu_Navegacion
                    WHERE Menu_Navegacion_Clave = N'planes_suscripciones'
                      AND Menu_Navegacion_Codigo <> ISNULL(@MenuPlanesCodigo, -1);
                END;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                DELETE FROM Aplicacion.Menu_Navegacion
                WHERE Menu_Navegacion_Clave IN (N'configuracion-planes', N'planes_suscripciones');
                """);
        }
    }
}

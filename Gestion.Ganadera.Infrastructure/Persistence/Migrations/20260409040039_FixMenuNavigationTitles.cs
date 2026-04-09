using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixMenuNavigationTitles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Configuración'
                WHERE Menu_Navegacion_Clave = N'configuracion'
                  AND Menu_Navegacion_Titulo <> N'Configuración';

                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Cerrar sesión'
                WHERE Menu_Navegacion_Clave = N'cerrar-sesion'
                  AND Menu_Navegacion_Titulo <> N'Cerrar sesión';

                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Auditoría'
                WHERE Menu_Navegacion_Clave = N'seguridad-auditoria'
                  AND Menu_Navegacion_Titulo <> N'Auditoría';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Configuracion'
                WHERE Menu_Navegacion_Clave = N'configuracion'
                  AND Menu_Navegacion_Titulo <> N'Configuracion';

                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Cerrar sesion'
                WHERE Menu_Navegacion_Clave = N'cerrar-sesion'
                  AND Menu_Navegacion_Titulo <> N'Cerrar sesion';

                UPDATE Aplicacion.Menu_Navegacion
                SET Menu_Navegacion_Titulo = N'Auditoria'
                WHERE Menu_Navegacion_Clave = N'seguridad-auditoria'
                  AND Menu_Navegacion_Titulo <> N'Auditoria';
                """);
        }
    }
}

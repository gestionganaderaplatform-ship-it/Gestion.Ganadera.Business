using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(AppDbContext))]
    [Migration("20260421023000_UpdateMenuIconsToMaterialSymbols")]
    public partial class UpdateMenuIconsToMaterialSymbols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'home' WHERE Menu_Navegacion_Clave = N'inicio';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'fact_check' WHERE Menu_Navegacion_Clave = N'registrar';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'edit_note' WHERE Menu_Navegacion_Clave IN (N'registrar-registro-existente', N'ganaderia-registro-existente');
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'format_list_bulleted' WHERE Menu_Navegacion_Clave IN (N'ganado', N'ganaderia-ganado');
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'agriculture' WHERE Menu_Navegacion_Clave = N'ganaderia';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'shield_lock' WHERE Menu_Navegacion_Clave = N'seguridad';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'history_edu' WHERE Menu_Navegacion_Clave = N'seguridad-auditoria';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'devices' WHERE Menu_Navegacion_Clave = N'seguridad-sesiones';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'login' WHERE Menu_Navegacion_Clave = N'seguridad-accesos';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'settings' WHERE Menu_Navegacion_Clave = N'configuracion';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'tune' WHERE Menu_Navegacion_Clave = N'configuracion-preferencias';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'account_circle' WHERE Menu_Navegacion_Clave = N'configuracion-cuenta';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'group' WHERE Menu_Navegacion_Clave = N'configuracion-delegados';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'account_balance_wallet' WHERE Menu_Navegacion_Clave = N'configuracion-planes';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'logout' WHERE Menu_Navegacion_Clave = N'cerrar-sesion';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-home' WHERE Menu_Navegacion_Clave = N'inicio';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-plus-circle' WHERE Menu_Navegacion_Clave = N'registrar';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-plus-circle' WHERE Menu_Navegacion_Clave IN (N'registrar-registro-existente', N'ganaderia-registro-existente');
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-list' WHERE Menu_Navegacion_Clave IN (N'ganado', N'ganaderia-ganado');
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-warehouse' WHERE Menu_Navegacion_Clave = N'ganaderia';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-shield' WHERE Menu_Navegacion_Clave = N'seguridad';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-history' WHERE Menu_Navegacion_Clave = N'seguridad-auditoria';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-desktop' WHERE Menu_Navegacion_Clave = N'seguridad-sesiones';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-key' WHERE Menu_Navegacion_Clave = N'seguridad-accesos';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-cog' WHERE Menu_Navegacion_Clave = N'configuracion';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-sliders-h' WHERE Menu_Navegacion_Clave = N'configuracion-preferencias';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-user' WHERE Menu_Navegacion_Clave = N'configuracion-cuenta';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-users' WHERE Menu_Navegacion_Clave = N'configuracion-delegados';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-wallet' WHERE Menu_Navegacion_Clave = N'configuracion-planes';
                UPDATE Aplicacion.Menu_Navegacion SET Menu_Navegacion_Icono = N'pi pi-sign-out' WHERE Menu_Navegacion_Clave = N'cerrar-sesion';
                """);
        }
    }
}

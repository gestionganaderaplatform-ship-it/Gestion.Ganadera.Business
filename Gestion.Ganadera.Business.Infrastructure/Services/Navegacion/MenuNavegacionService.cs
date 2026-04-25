using System.Security.Claims;
using Gestion.Ganadera.Business.Application.Features.Navegacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Navegacion.ModelosVista;
using Gestion.Ganadera.Business.Domain.Features.Navegacion;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Navegacion
{
    /// <summary>
    /// Construye el arbol de navegacion visible para el shell del Web desde base de datos.
    /// </summary>
    public sealed class MenuNavegacionService(
        AppDbContext dbContext,
        IHttpContextAccessor httpContextAccessor) : IMenuNavegacionService
    {
        private static readonly string[] PermissionClaimTypes = ["permission", "permissions"];
        private readonly AppDbContext _dbContext = dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<IReadOnlyList<NodoNavegacionModeloVista>> ObtenerMenuActualAsync(
            CancellationToken cancellationToken = default)
        {
            var items = await _dbContext.Set<MenuNavegacion>()
                .AsNoTracking()
                .Where(item => item.Menu_Navegacion_Esta_Activo)
                .OrderBy(item => item.Menu_Navegacion_Orden)
                .ThenBy(item => item.Menu_Navegacion_Codigo)
                .ToListAsync(cancellationToken);

            var isParentAccount = ResolveIsParentAccount();
            var grantedPermissions = ResolvePermissions();
            var visibleItems = items
                .Where(item => IsVisibleForCurrentUser(item, isParentAccount, grantedPermissions))
                .ToList();

            return BuildTree(visibleItems, null);
        }

        private IReadOnlyList<NodoNavegacionModeloVista> BuildTree(
            IReadOnlyCollection<MenuNavegacion> visibleItems,
            long? parentCode)
        {
            return visibleItems
                .Where(item => item.Menu_Navegacion_Padre_Codigo == parentCode)
                .OrderBy(item => item.Menu_Navegacion_Orden)
                .ThenBy(item => item.Menu_Navegacion_Codigo)
                .Select(item =>
                {
                    var children = BuildTree(visibleItems, item.Menu_Navegacion_Codigo).ToList();
                    var node = new NodoNavegacionModeloVista
                    {
                        Clave = item.Menu_Navegacion_Clave,
                        Titulo = item.Menu_Navegacion_Titulo,
                        Icono = item.Menu_Navegacion_Icono,
                        Tipo = item.Menu_Navegacion_Tipo,
                        Ruta = item.Menu_Navegacion_Ruta,
                        Accion = item.Menu_Navegacion_Accion,
                        Hijos = children
                    };

                    return node;
                })
                .Where(node => node.Tipo != "group" || !string.IsNullOrWhiteSpace(node.Ruta) || node.Hijos.Count > 0)
                .ToList();
        }

        private bool IsVisibleForCurrentUser(
            MenuNavegacion item,
            bool isParentAccount,
            HashSet<string> grantedPermissions)
        {
            if (item.Menu_Navegacion_Requiere_Cuenta_Padre && !isParentAccount)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(item.Menu_Navegacion_Permiso_Requerido))
            {
                return true;
            }

            return grantedPermissions.Contains(item.Menu_Navegacion_Permiso_Requerido.Trim());
        }

        private bool ResolveIsParentAccount()
        {
            var value = _httpContextAccessor.HttpContext?.User.Claims
                .FirstOrDefault(claim =>
                    string.Equals(claim.Type, "es_cuenta_padre", StringComparison.OrdinalIgnoreCase))
                ?.Value;

            if (bool.TryParse(value, out var parsed))
            {
                return parsed;
            }

            return string.Equals(value, "1", StringComparison.OrdinalIgnoreCase);
        }

        private HashSet<string> ResolvePermissions()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }

            return user.Claims
                .Where(claim => PermissionClaimTypes.Any(type =>
                    string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase)))
                .SelectMany(claim => claim.Value.Split([',', ';', ' '], StringSplitOptions.RemoveEmptyEntries))
                .Select(value => value.Trim())
                .Where(value => value.Length > 0)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);
        }
    }
}

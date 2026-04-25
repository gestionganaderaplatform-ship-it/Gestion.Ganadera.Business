using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gestion.Ganadera.Business.API.Security.Permissions
{
    /// <summary>
    /// Requiere que el JWT incluya el permiso solicitado en sus claims.
    /// </summary>
    public sealed class PermissionAuthorizationRequirement(ControllerPermission permission)
        : IAuthorizationRequirement
    {
        public ControllerPermission Permission { get; } = permission;
    }

    /// <summary>
    /// Soporta claims repetidos o listas separadas por coma, punto y coma o espacios.
    /// </summary>
    public sealed class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                return Task.CompletedTask;
            }

            var grantedPermissions = context.User.Claims
                .Where(claim =>
                    string.Equals(claim.Type, PermissionPolicy.ClaimType, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(claim.Type, PermissionPolicy.AlternateClaimType, StringComparison.OrdinalIgnoreCase))
                .SelectMany(SplitClaimValues)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            // Mientras la API de negocio no maneje permisos finos por modulo o rol,
            // un usuario autenticado sin claims de permission recibe acceso base.
            if (grantedPermissions.Count == 0)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (grantedPermissions.Contains(requirement.Permission.ToString()))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private static IEnumerable<string> SplitClaimValues(Claim claim)
        {
            return claim.Value
                .Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(value => !string.IsNullOrWhiteSpace(value));
        }
    }
}

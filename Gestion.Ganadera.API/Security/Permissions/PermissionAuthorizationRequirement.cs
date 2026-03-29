using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Gestion.Ganadera.API.Security.Permissions
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
    public sealed class PermissionAuthorizationHandler(bool jwtEnabled)
        : AuthorizationHandler<PermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionAuthorizationRequirement requirement)
        {
            if (!jwtEnabled)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

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

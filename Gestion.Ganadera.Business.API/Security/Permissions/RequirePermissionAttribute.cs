using Microsoft.AspNetCore.Authorization;

namespace Gestion.Ganadera.Business.API.Security.Permissions
{
    /// <summary>
    /// Declara el permiso requerido para ejecutar una accion o endpoint del API.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class RequirePermissionAttribute : AuthorizeAttribute
    {
        public RequirePermissionAttribute(ControllerPermission permission)
        {
            Permission = permission;
            Policy = PermissionPolicy.BuildPolicyName(permission);
        }

        public ControllerPermission Permission { get; }
    }
}

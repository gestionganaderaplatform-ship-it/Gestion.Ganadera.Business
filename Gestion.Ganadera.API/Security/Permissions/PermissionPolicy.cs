namespace Gestion.Ganadera.API.Security.Permissions
{
    /// <summary>
    /// Centraliza el claim y los nombres de policy usados por permisos.
    /// </summary>
    public static class PermissionPolicy
    {
        public const string ClaimType = "permission";
        public const string AlternateClaimType = "permissions";
        public const string PolicyPrefix = "Permission:";

        public static string BuildPolicyName(ControllerPermission permission)
            => $"{PolicyPrefix}{permission}";
    }
}

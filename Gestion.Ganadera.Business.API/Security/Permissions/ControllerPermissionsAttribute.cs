namespace Gestion.Ganadera.Business.API.Security.Permissions
{
    /// <summary>
    /// Declara el conjunto base de permisos esperado por un controller completo.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class ControllerPermissionsAttribute(ControllerPermission permissions) : Attribute
    {
        public ControllerPermission Permissions { get; } = permissions;
    }
}

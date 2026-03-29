using Microsoft.AspNetCore.Authorization;
using Gestion.Ganadera.API.Security.Permissions;

namespace Gestion.Ganadera.API.Extensions;

/// <summary>
/// Registra autorizacion basada en permisos para proteger controllers y acciones.
/// </summary>
public static class AuthorizationExtensions
{
    public static WebApplicationBuilder AddAuthorizationServices(this WebApplicationBuilder builder)
    {
        var jwtEnabled = builder.Configuration.GetValue<bool>("Jwt:Enabled");

        builder.Services.AddAuthorization(options =>
        {
            foreach (var permission in Enum.GetValues<ControllerPermission>())
            {
                if (permission is ControllerPermission.None
                    or ControllerPermission.All
                    or ControllerPermission.Standard)
                {
                    continue;
                }

                options.AddPolicy(
                    PermissionPolicy.BuildPolicyName(permission),
                    policy =>
                    {
                        policy.AddRequirements(new PermissionAuthorizationRequirement(permission));

                        if (jwtEnabled)
                        {
                            policy.RequireAuthenticatedUser();
                        }
                    });
            }
        });

        builder.Services.AddSingleton<IAuthorizationHandler>(
            _ => new PermissionAuthorizationHandler(jwtEnabled));
        return builder;
    }
}

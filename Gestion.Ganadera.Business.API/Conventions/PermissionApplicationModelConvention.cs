using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Gestion.Ganadera.Business.API.Security.Permissions;

namespace Gestion.Ganadera.Business.API.Conventions
{
    public class PermissionApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var controllerPermission = controller.Attributes
                    .OfType<ControllerPermissionsAttribute>()
                    .FirstOrDefault();

                if (controllerPermission == null)
                    continue;

                var permissions = controllerPermission.Permissions;

                foreach (var action in controller.Actions.ToList())
                {
                    var requiredPermission = action.Attributes
                        .OfType<RequirePermissionAttribute>()
                        .FirstOrDefault();

                    if (requiredPermission == null)
                        continue;

                    if (!permissions.HasFlag(requiredPermission.Permission))
                    {
                        controller.Actions.Remove(action);
                    }
                }
            }
        }
    }
}

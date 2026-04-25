using Microsoft.AspNetCore.Authorization;

namespace Gestion.Ganadera.Business.API.Security.Planes
{
    public sealed class ManejadorAutorizacionPlan
        : AuthorizationHandler<RequisitoPlanMinimo>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RequisitoPlanMinimo requirement)
        {
            if (context.User.Identity?.IsAuthenticated != true)
            {
                return Task.CompletedTask;
            }

            if (requirement.RequiereCuentaPadre &&
                !EsCuentaPadre(context.User.FindFirst(PoliticaPlan.ClaimEsCuentaPadre)?.Value))
            {
                return Task.CompletedTask;
            }

            var nivelActual = ResolverNivelPlan(
                context.User.FindFirst(PoliticaPlan.ClaimNivelPlan)?.Value,
                context.User.FindFirst(PoliticaPlan.ClaimClavePlan)?.Value);

            if (nivelActual >= requirement.NivelMinimo)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private static bool EsCuentaPadre(string? claimValue)
        {
            return bool.TryParse(claimValue, out var esCuentaPadre) && esCuentaPadre;
        }

        private static NivelPlanAcceso ResolverNivelPlan(string? nivelClaim, string? claveClaim)
        {
            if (int.TryParse(nivelClaim, out var nivelNumerico) &&
                Enum.IsDefined(typeof(NivelPlanAcceso), nivelNumerico))
            {
                return (NivelPlanAcceso)nivelNumerico;
            }

            return claveClaim?.Trim().ToLowerInvariant() switch
            {
                "empresarial" => NivelPlanAcceso.Empresarial,
                "productivo" => NivelPlanAcceso.Productivo,
                _ => NivelPlanAcceso.Esencial
            };
        }
    }
}

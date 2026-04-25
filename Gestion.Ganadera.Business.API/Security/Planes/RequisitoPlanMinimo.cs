using Microsoft.AspNetCore.Authorization;

namespace Gestion.Ganadera.Business.API.Security.Planes
{
    public enum NivelPlanAcceso
    {
        Esencial = 1,
        Productivo = 2,
        Empresarial = 3
    }

    public sealed class RequisitoPlanMinimo(
        NivelPlanAcceso nivelMinimo,
        bool requiereCuentaPadre = false) : IAuthorizationRequirement
    {
        public NivelPlanAcceso NivelMinimo { get; } = nivelMinimo;
        public bool RequiereCuentaPadre { get; } = requiereCuentaPadre;
    }
}

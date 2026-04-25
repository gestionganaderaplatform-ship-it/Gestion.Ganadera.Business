using Gestion.Ganadera.Business.Application.Observability.ViewModels;

namespace Gestion.Ganadera.Business.Application.Observability.Interfaces
{
    /// <summary>
    /// Registra eventos tecnicos de seguridad desde cualquier punto del API.
    /// </summary>
    public interface ISecurityEventService
    {
        Task RegistrarAsync(EventoSeguridadViewModel evento);
    }
}

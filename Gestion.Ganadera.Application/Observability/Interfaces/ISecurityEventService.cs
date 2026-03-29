using Gestion.Ganadera.Application.Observability.ViewModels;

namespace Gestion.Ganadera.Application.Observability.Interfaces
{
    /// <summary>
    /// Registra eventos tecnicos de seguridad desde cualquier punto del API.
    /// </summary>
    public interface ISecurityEventService
    {
        Task RegistrarAsync(EventoSeguridadViewModel evento);
    }
}

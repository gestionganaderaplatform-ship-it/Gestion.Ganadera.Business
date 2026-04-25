using Gestion.Ganadera.Business.Application.Features.Navegacion.ModelosVista;

namespace Gestion.Ganadera.Business.Application.Features.Navegacion.Interfaces
{
    /// <summary>
    /// Resuelve el menu de navegacion visible para el usuario autenticado.
    /// </summary>
    public interface IMenuNavegacionService
    {
        Task<IReadOnlyList<NodoNavegacionModeloVista>> ObtenerMenuActualAsync(
            CancellationToken cancellationToken = default);
    }
}

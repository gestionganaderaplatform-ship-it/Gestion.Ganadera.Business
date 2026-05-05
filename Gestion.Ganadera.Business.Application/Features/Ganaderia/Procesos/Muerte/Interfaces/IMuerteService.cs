using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Interfaces;

public interface IMuerteService
{
    Task<bool> RegistrarAsync(RegistrarMuerteRequest request, CancellationToken cancellationToken = default);
}
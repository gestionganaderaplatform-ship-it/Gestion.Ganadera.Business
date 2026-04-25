namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Interfaces;

public interface IGanaderiaCatalogBootstrapService
{
    Task EnsureCatalogosBaseAsync(CancellationToken cancellationToken = default);
}

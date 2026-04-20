namespace Gestion.Ganadera.Application.Features.Ganaderia.Interfaces;

public interface IGanaderiaCatalogBootstrapService
{
    Task EnsureCatalogosBaseAsync(CancellationToken cancellationToken = default);
}

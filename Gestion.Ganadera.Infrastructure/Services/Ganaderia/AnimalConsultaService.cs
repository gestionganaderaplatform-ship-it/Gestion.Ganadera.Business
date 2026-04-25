using Gestion.Ganadera.Application.Features.Ganaderia.Animales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class AnimalConsultaService(IAnimalConsultaRepository repository) : IAnimalConsultaService
{
    public Task<InicioDashboardViewModel> ObtenerResumenInicioAsync(
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        return repository.ObtenerResumenInicioAsync(fincaCodigo, cancellationToken);
    }

    public Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        var paginaNormalizada = pagina <= 0 ? 1 : pagina;
        var tamanoPaginaNormalizado = tamanoPagina <= 0 ? 25 : Math.Min(tamanoPagina, 100);

        return repository.ObtenerPorPaginado(
            paginaNormalizada,
            tamanoPaginaNormalizado,
            fincaCodigo,
            cancellationToken);
    }

    public Task<AnimalViewModel?> Consultar(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        return repository.Consultar(animalCodigo, fincaCodigo, cancellationToken);
    }

    public Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default)
    {
        return repository.ObtenerHistorialAsync(animalCodigo, fincaCodigo, cancellationToken);
    }
}

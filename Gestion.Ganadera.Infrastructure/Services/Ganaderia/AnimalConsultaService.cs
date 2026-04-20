using Gestion.Ganadera.Application.Features.Ganaderia.Animales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class AnimalConsultaService(IAnimalConsultaRepository repository) : IAnimalConsultaService
{
    public Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina, 
        int tamañoPagina, 
        CancellationToken cancellationToken = default)
    {
        var paginaNormalizada = pagina <= 0 ? 1 : pagina;
        var tamañoPaginaNormalizado = tamañoPagina <= 0 ? 25 : Math.Min(tamañoPagina, 100);

        return repository.ObtenerPorPaginado(paginaNormalizada, tamañoPaginaNormalizado, cancellationToken);
    }

    public Task<AnimalViewModel?> Consultar(
        long animalCodigo, 
        CancellationToken cancellationToken = default)
    {
        return repository.Consultar(animalCodigo, cancellationToken);
    }

    public Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(long animalCodigo, CancellationToken cancellationToken = default)
    {
        return repository.ObtenerHistorialAsync(animalCodigo, cancellationToken);
    }
}

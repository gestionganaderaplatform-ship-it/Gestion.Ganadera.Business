using Gestion.Ganadera.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Animales.Interfaces;

public interface IAnimalConsultaService
{
    Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina, 
        int tamañoPagina, 
        CancellationToken cancellationToken = default);

    Task<AnimalViewModel?> Consultar(long animalCodigo, CancellationToken cancellationToken = default);

    Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(long animalCodigo, CancellationToken cancellationToken = default);
}

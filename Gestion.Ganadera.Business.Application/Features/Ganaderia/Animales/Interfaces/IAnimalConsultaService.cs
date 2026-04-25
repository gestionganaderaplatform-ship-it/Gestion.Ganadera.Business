using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Interfaces;

public interface IAnimalConsultaService
{
    Task<InicioDashboardViewModel> ObtenerResumenInicioAsync(
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<AnimalViewModel?> Consultar(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);
}

using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Interfaces;

public interface IAnimalConsultaRepository
{
    Task<InicioDashboardViewModel> ObtenerResumenInicioAsync(
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> ObtenerPorPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo = null,
        string? busqueda = null,
        string? animalIdentificadorPrincipal = null,
        string? categoriaAnimalNombre = null,
        string? potreroNombre = null,
        DateTime? animalFechaIngresoInicial = null,
        CancellationToken cancellationToken = default);

    Task<AnimalViewModel?> Consultar(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<AnimalHistorialViewModel>> ObtenerHistorialAsync(
        long animalCodigo,
        long? fincaCodigo = null,
        CancellationToken cancellationToken = default);

    Task<(IEnumerable<GanadoViewModel> Items, int TotalRegistros)> FiltrarPaginado(
        int pagina,
        int tamanoPagina,
        long? fincaCodigo,
        AnimalConsultaFilterViewModel filtro,
        CancellationToken cancellationToken = default);
}

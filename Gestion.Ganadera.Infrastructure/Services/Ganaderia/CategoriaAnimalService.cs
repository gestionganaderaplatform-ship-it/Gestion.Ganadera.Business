using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class CategoriaAnimalService(
    ICategoriaAnimalRepository repository,
    IRangoEdadRepository rangoEdadRepository,
    IMapper mapper)
    : BaseService<CategoriaAnimal, CategoriaAnimalViewModel, CategoriaAnimalCreateViewModel, CategoriaAnimalUpdateViewModel, ICategoriaAnimalRepository>(repository, mapper), ICategoriaAnimalService
{
    private readonly IRangoEdadRepository _rangoEdadRepository = rangoEdadRepository;

    public override async Task<CategoriaAnimalViewModel?> Consultar(long codigo)
    {
        var categoria = await base.Consultar(codigo);
        if (categoria is null)
        {
            return null;
        }

        var rangos = await ObtenerRangosActivosAsync();
        EnriquecerCategoria(categoria, rangos);
        return categoria;
    }

    public override async Task<IEnumerable<CategoriaAnimalViewModel>> ObtenerTodos()
    {
        var categorias = (await base.ObtenerTodos()).ToList();
        if (categorias.Count == 0)
        {
            return categorias;
        }

        var rangos = await ObtenerRangosActivosAsync();
        foreach (var categoria in categorias)
        {
            EnriquecerCategoria(categoria, rangos);
        }

        return categorias;
    }

    private async Task<List<RangoEdad>> ObtenerRangosActivosAsync()
    {
        return (await _rangoEdadRepository.ObtenerTodos())
            .Where(item => item.Rango_Edad_Activo)
            .OrderBy(item => item.Rango_Edad_Orden)
            .ThenBy(item => item.Rango_Edad_Codigo)
            .ToList();
    }

    private static void EnriquecerCategoria(
        CategoriaAnimalViewModel categoria,
        IReadOnlyCollection<RangoEdad> rangosActivos)
    {
        var rangoConfigurado = ResolverBandaPorCategoria(categoria.Categoria_Animal_Nombre);
        if (rangoConfigurado is null)
        {
            categoria.Categoria_Animal_Rango_Edad_Codigo_Referencia = null;
            categoria.Categoria_Animal_Rango_Edad_Minima_Meses = null;
            categoria.Categoria_Animal_Rango_Edad_Maxima_Meses = null;
            categoria.Categoria_Animal_Rango_Edad_Descripcion = null;
            return;
        }

        categoria.Categoria_Animal_Rango_Edad_Codigo_Referencia = EncontrarCodigoReferencia(
            rangosActivos,
            rangoConfigurado.Value.MinimaMeses,
            rangoConfigurado.Value.MaximaMeses);
        categoria.Categoria_Animal_Rango_Edad_Minima_Meses = rangoConfigurado.Value.MinimaMeses;
        categoria.Categoria_Animal_Rango_Edad_Maxima_Meses = rangoConfigurado.Value.MaximaMeses;
        categoria.Categoria_Animal_Rango_Edad_Descripcion = rangoConfigurado.Value.Descripcion;
    }

    private static (int MinimaMeses, int? MaximaMeses, string Descripcion)? ResolverBandaPorCategoria(
        string? categoriaNombre)
    {
        var nombreNormalizado = string.Concat((categoriaNombre ?? string.Empty)
            .Trim()
            .ToUpperInvariant()
            .Where(character => !char.IsWhiteSpace(character)));

        return nombreNormalizado switch
        {
            "BECERRA" or "BECERRO" => (0, 12, "0 a 12 meses"),
            "NOVILLA" or "TORETE" or "NOVILLO" => (13, 36, "13 a 36 meses"),
            "VACA" or "TORO" => (37, null, "Mas de 36 meses"),
            _ => null
        };
    }

    private static long? EncontrarCodigoReferencia(
        IEnumerable<RangoEdad> rangos,
        int minimaMeses,
        int? maximaMeses)
    {
        if (maximaMeses is null)
        {
            return rangos.FirstOrDefault(item =>
                (item.Rango_Edad_Edad_Minima_Meses ?? 0) >= minimaMeses &&
                item.Rango_Edad_Edad_Maxima_Meses is null)?.Rango_Edad_Codigo;
        }

        var exacto = rangos.FirstOrDefault(item =>
            (item.Rango_Edad_Edad_Minima_Meses ?? 0) == minimaMeses &&
            item.Rango_Edad_Edad_Maxima_Meses == maximaMeses);

        if (exacto is not null)
        {
            return exacto.Rango_Edad_Codigo;
        }

        return rangos
            .Where(item =>
            {
                var minimaRango = item.Rango_Edad_Edad_Minima_Meses ?? 0;
                var maximaRango = item.Rango_Edad_Edad_Maxima_Meses ?? int.MaxValue;
                return minimaRango >= minimaMeses && maximaRango <= maximaMeses.Value;
            })
            .OrderByDescending(item => item.Rango_Edad_Edad_Maxima_Meses ?? int.MaxValue)
            .Select(item => (long?)item.Rango_Edad_Codigo)
            .FirstOrDefault();
    }
}

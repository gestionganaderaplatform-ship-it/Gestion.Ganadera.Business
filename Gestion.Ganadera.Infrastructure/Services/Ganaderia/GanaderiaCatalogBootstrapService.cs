using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Interfaces;
using Gestion.Ganadera.Infrastructure.Persistence;
using Gestion.Ganadera.Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public sealed class GanaderiaCatalogBootstrapService(
    AppDbContext dbContext,
    ICurrentClientProvider currentClientProvider) : IGanaderiaCatalogBootstrapService
{
    private static readonly string[] LegacyTipoIdentificadorCodes = ["ARETE", "HIERRO", "RFID"];

    private readonly AppDbContext _dbContext = dbContext;
    private readonly ICurrentClientProvider _currentClientProvider = currentClientProvider;

    public async Task EnsureCatalogosBaseAsync(CancellationToken cancellationToken = default)
    {
        if (!_currentClientProvider.ClientNumericId.HasValue)
        {
            return;
        }

        var clienteCodigo = _currentClientProvider.ClientNumericId.Value;
        await EnsureTiposIdentificadorAsync(clienteCodigo, cancellationToken);
        await EnsureCategoriasAnimalAsync(clienteCodigo, cancellationToken);
        await EnsureRangosEdadAsync(clienteCodigo, cancellationToken);
    }

    private async Task EnsureTiposIdentificadorAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var tiposBase = GanaderiaCatalogosBase.ObtenerTiposIdentificador();
        var codigosBase = tiposBase
            .Select(item => item.Tipo_Identificador_Codigo_Interno)
            .Where(item => !string.IsNullOrWhiteSpace(item))
            .Cast<string>()
            .ToArray();

        var existentes = await _dbContext.TiposIdentificador
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                item.Tipo_Identificador_Codigo_Interno != null &&
                (codigosBase.Contains(item.Tipo_Identificador_Codigo_Interno) ||
                 LegacyTipoIdentificadorCodes.Contains(item.Tipo_Identificador_Codigo_Interno)))
            .ToListAsync(cancellationToken);

        var existentesPorCodigo = existentes
            .ToDictionary(item => item.Tipo_Identificador_Codigo_Interno!, StringComparer.OrdinalIgnoreCase);

        var faltantes = tiposBase
            .Where(item => !existentesPorCodigo.ContainsKey(item.Tipo_Identificador_Codigo_Interno!))
            .ToList();

        foreach (var tipoBase in tiposBase)
        {
            if (!existentesPorCodigo.TryGetValue(tipoBase.Tipo_Identificador_Codigo_Interno!, out var existente))
            {
                continue;
            }

            existente.Tipo_Identificador_Nombre = tipoBase.Tipo_Identificador_Nombre;
            existente.Tipo_Identificador_Operativo = tipoBase.Tipo_Identificador_Operativo;
            existente.Tipo_Identificador_Permite_Busqueda = tipoBase.Tipo_Identificador_Permite_Busqueda;
            existente.Tipo_Identificador_Permite_Principal = tipoBase.Tipo_Identificador_Permite_Principal;
            existente.Tipo_Identificador_Activo = true;
        }

        foreach (var legacy in existentes.Where(item =>
                     item.Tipo_Identificador_Codigo_Interno != null &&
                     LegacyTipoIdentificadorCodes.Contains(item.Tipo_Identificador_Codigo_Interno, StringComparer.OrdinalIgnoreCase)))
        {
            legacy.Tipo_Identificador_Operativo = false;
            legacy.Tipo_Identificador_Permite_Principal = false;
            legacy.Tipo_Identificador_Activo = false;
        }

        foreach (var faltante in faltantes)
        {
            faltante.Cliente_Codigo = clienteCodigo;
        }

        if (faltantes.Count > 0)
        {
            _dbContext.TiposIdentificador.AddRange(faltantes);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureCategoriasAnimalAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var categoriasBase = GanaderiaCatalogosBase.ObtenerCategoriasAnimal();
        var nombresBase = categoriasBase
            .Select(item => item.Categoria_Animal_Nombre)
            .ToArray();

        var existentes = await _dbContext.CategoriasAnimal
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                nombresBase.Contains(item.Categoria_Animal_Nombre))
            .Select(item => item.Categoria_Animal_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = categoriasBase
            .Where(item => !existentes.Contains(item.Categoria_Animal_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0)
        {
            return;
        }

        foreach (var faltante in faltantes)
        {
            faltante.Cliente_Codigo = clienteCodigo;
        }

        _dbContext.CategoriasAnimal.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureRangosEdadAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var rangosBase = GanaderiaCatalogosBase.ObtenerRangosEdad();
        var nombresBase = rangosBase
            .Select(item => item.Rango_Edad_Nombre)
            .ToArray();

        var existentes = await _dbContext.RangosEdad
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                nombresBase.Contains(item.Rango_Edad_Nombre))
            .Select(item => item.Rango_Edad_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = rangosBase
            .Where(item => !existentes.Contains(item.Rango_Edad_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0)
        {
            return;
        }

        foreach (var faltante in faltantes)
        {
            faltante.Cliente_Codigo = clienteCodigo;
        }

        _dbContext.RangosEdad.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

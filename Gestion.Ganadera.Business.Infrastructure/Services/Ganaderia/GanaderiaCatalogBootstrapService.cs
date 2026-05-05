using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Gestion.Ganadera.Business.Infrastructure.Persistence.SeedData;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

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
        await EnsureCausasMuerteAsync(clienteCodigo, cancellationToken);
        await EnsureVacunasEnfermedadesAsync(clienteCodigo, cancellationToken);
        await EnsureTratamientoTiposAsync(clienteCodigo, cancellationToken);
        await EnsureTratamientoProductosAsync(clienteCodigo, cancellationToken);
        await EnsurePalpacionResultadosAsync(clienteCodigo, cancellationToken);
        await EnsureDescarteMotivosAsync(clienteCodigo, cancellationToken);
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

    private async Task EnsureCausasMuerteAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var causasBase = GanaderiaCatalogosBase.ObtenerCausasMuerte();
        var nombresBase = causasBase
            .Select(item => item.Causa_Muerte_Nombre)
            .ToArray();

        var existentes = await _dbContext.CausasMuerte
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                nombresBase.Contains(item.Causa_Muerte_Nombre))
            .Select(item => item.Causa_Muerte_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = causasBase
            .Where(item => !existentes.Contains(item.Causa_Muerte_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0)
        {
            return;
        }

        foreach (var faltante in faltantes)
        {
            faltante.Cliente_Codigo = clienteCodigo;
        }

        _dbContext.CausasMuerte.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureVacunasEnfermedadesAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var enfermedadesBase = GanaderiaCatalogosBase.ObtenerVacunasEnfermedades();
        var nombresBase = enfermedadesBase
            .Select(item => item.Vacuna_Enfermedad_Nombre)
            .ToArray();

        var existentes = await _dbContext.VacunasEnfermedades
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                nombresBase.Contains(item.Vacuna_Enfermedad_Nombre))
            .Select(item => item.Vacuna_Enfermedad_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = enfermedadesBase
            .Where(item => !existentes.Contains(item.Vacuna_Enfermedad_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0)
        {
            return;
        }

        foreach (var faltante in faltantes)
        {
            faltante.Cliente_Codigo = clienteCodigo;
        }

        _dbContext.VacunasEnfermedades.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureTratamientoTiposAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var tiposBase = GanaderiaCatalogosBase.ObtenerTratamientoTipos();
        var nombresBase = tiposBase.Select(item => item.Tratamiento_Tipo_Nombre).ToArray();

        var existentes = await _dbContext.TratamientosTipos
            .IgnoreQueryFilters()
            .Where(item => item.Cliente_Codigo == clienteCodigo && nombresBase.Contains(item.Tratamiento_Tipo_Nombre))
            .Select(item => item.Tratamiento_Tipo_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = tiposBase
            .Where(item => !existentes.Contains(item.Tratamiento_Tipo_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0) return;

        foreach (var faltante in faltantes) { faltante.Cliente_Codigo = clienteCodigo; }

        _dbContext.TratamientosTipos.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureTratamientoProductosAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var productosBase = GanaderiaCatalogosBase.ObtenerTratamientoProductos();
        var nombresBase = productosBase.Select(item => item.Tratamiento_Producto_Nombre).ToArray();

        var existentes = await _dbContext.TratamientosProductos
            .IgnoreQueryFilters()
            .Where(item => item.Cliente_Codigo == clienteCodigo && nombresBase.Contains(item.Tratamiento_Producto_Nombre))
            .Select(item => item.Tratamiento_Producto_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = productosBase
            .Where(item => !existentes.Contains(item.Tratamiento_Producto_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0) return;

        // Recuperar tipos para asignar la relación obligatoria
        var tipos = await _dbContext.TratamientosTipos
            .IgnoreQueryFilters()
            .Where(item => item.Cliente_Codigo == clienteCodigo)
            .ToListAsync(cancellationToken);

        var tipoAntibiotico = tipos.FirstOrDefault(t => t.Tratamiento_Tipo_Nombre.Contains("Antibiótico"))?.Tratamiento_Tipo_Codigo;
        var tipoDesparasitante = tipos.FirstOrDefault(t => t.Tratamiento_Tipo_Nombre.Contains("Desparasitante"))?.Tratamiento_Tipo_Codigo;
        var tipoVitamina = tipos.FirstOrDefault(t => t.Tratamiento_Tipo_Nombre.Contains("Vitamina"))?.Tratamiento_Tipo_Codigo;
        var tipoDefault = tipos.FirstOrDefault()?.Tratamiento_Tipo_Codigo ?? 0;

        foreach (var faltante in faltantes) 
        { 
            faltante.Cliente_Codigo = clienteCodigo; 
            
            if (faltante.Tratamiento_Producto_Nombre.Contains("Ivermectina", StringComparison.OrdinalIgnoreCase))
                faltante.Tratamiento_Tipo_Codigo = tipoDesparasitante ?? tipoDefault;
            else if (faltante.Tratamiento_Producto_Nombre.Contains("Oxitetraciclina", StringComparison.OrdinalIgnoreCase))
                faltante.Tratamiento_Tipo_Codigo = tipoAntibiotico ?? tipoDefault;
            else if (faltante.Tratamiento_Producto_Nombre.Contains("Complejo B", StringComparison.OrdinalIgnoreCase))
                faltante.Tratamiento_Tipo_Codigo = tipoVitamina ?? tipoDefault;
            else
                faltante.Tratamiento_Tipo_Codigo = tipoDefault;
        }

        _dbContext.TratamientosProductos.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsurePalpacionResultadosAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var resultadosBase = GanaderiaCatalogosBase.ObtenerPalpacionResultados();
        var nombresBase = resultadosBase.Select(item => item.Palpacion_Resultado_Nombre).ToArray();

        var existentes = await _dbContext.PalpacionesResultados
            .IgnoreQueryFilters()
            .Where(item => item.Cliente_Codigo == clienteCodigo && nombresBase.Contains(item.Palpacion_Resultado_Nombre))
            .Select(item => item.Palpacion_Resultado_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = resultadosBase
            .Where(item => !existentes.Contains(item.Palpacion_Resultado_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0) return;

        foreach (var faltante in faltantes) { faltante.Cliente_Codigo = clienteCodigo; }

        _dbContext.PalpacionesResultados.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task EnsureDescarteMotivosAsync(long clienteCodigo, CancellationToken cancellationToken)
    {
        var motivosBase = GanaderiaCatalogosBase.ObtenerDescarteMotivos();
        var nombresBase = motivosBase.Select(item => item.Descarte_Motivo_Nombre).ToArray();

        var existentes = await _dbContext.DescartesMotivos
            .IgnoreQueryFilters()
            .Where(item => item.Cliente_Codigo == clienteCodigo && nombresBase.Contains(item.Descarte_Motivo_Nombre))
            .Select(item => item.Descarte_Motivo_Nombre)
            .ToListAsync(cancellationToken);

        var faltantes = motivosBase
            .Where(item => !existentes.Contains(item.Descarte_Motivo_Nombre, StringComparer.OrdinalIgnoreCase))
            .ToList();

        if (faltantes.Count == 0) return;

        foreach (var faltante in faltantes) { faltante.Cliente_Codigo = clienteCodigo; }

        _dbContext.DescartesMotivos.AddRange(faltantes);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}

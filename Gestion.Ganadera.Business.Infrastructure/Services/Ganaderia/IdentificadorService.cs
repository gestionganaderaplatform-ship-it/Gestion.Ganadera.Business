using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia;

public class IdentificadorService(AppDbContext context) : IIdentificadorService
{
    private const string TipoIdentificadorInternoSistema = "INTERNO_SISTEMA";

    public async Task<long> ObtenerTipoIdentificadorInternoCodigoAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        var clienteCodigo = await context.Fincas
            .Where(f => f.Finca_Codigo == fincaCodigo)
            .Select(f => f.Cliente_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        var tipoIdentificadorInternoCodigo = await context.TiposIdentificador
            .IgnoreQueryFilters()
            .Where(item =>
                item.Cliente_Codigo == clienteCodigo &&
                item.Tipo_Identificador_Codigo_Interno == TipoIdentificadorInternoSistema &&
                item.Tipo_Identificador_Activo)
            .Select(item => (long?)item.Tipo_Identificador_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        if (!tipoIdentificadorInternoCodigo.HasValue)
        {
            throw new ValidationException(
            [
                new ValidationFailure(
                    nameof(IdentificadorAnimal.Tipo_Identificador_Codigo),
                    IdentificadorMessages.TipoIdentificadorInternoNoDisponible)
            ]);
        }

        return tipoIdentificadorInternoCodigo.Value;
    }

    public string ConstruirIdentificadorInterno(long animalCodigo)
        => $"INT-{animalCodigo:D10}";

    public async Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        var totalAnimales = await context.Animales
            .AsNoTracking()
            .CountAsync(item => item.Finca_Codigo == fincaCodigo, cancellationToken);

        return totalAnimales + 1;
    }

    public async Task<IReadOnlyList<ExistenciaIdentificador>> VerificarExistenciaIdentificadoresAsync(
        long fincaCodigo,
        IEnumerable<string> identificadores,
        CancellationToken cancellationToken = default)
    {
        var listaIdentificadores = identificadores.ToList();
        if (listaIdentificadores.Count == 0)
        {
            return [];
        }

        var existentes = await context.IdentificadoresAnimal
            .Join(context.Animales,
                i => i.Animal_Codigo,
                a => a.Animal_Codigo,
                (i, a) => new { i, a })
            .Where(x =>
                x.a.Finca_Codigo == fincaCodigo &&
                x.i.Identificador_Animal_Activo &&
                listaIdentificadores.Contains(x.i.Identificador_Animal_Valor))
            .Select(x => x.i.Identificador_Animal_Valor)
            .ToListAsync(cancellationToken);

        var existentesSet = existentes.ToHashSet(StringComparer.OrdinalIgnoreCase);

        return listaIdentificadores
            .Select(id => new ExistenciaIdentificador(id, existentesSet.Contains(id)))
            .ToList();
    }
}


using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class RegistroExistenteRepository(AppDbContext context) : IRegistroExistenteRepository
{
    private const string TipoIdentificadorInternoSistema = "INTERNO_SISTEMA";

    public async Task<bool> RegistrarAtomicoAsync(
        Animal animal,
        IdentificadorAnimal identificador,
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleRegistroExistente fotoRegistro,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.Animales.AddAsync(animal, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                var tipoIdentificadorInternoCodigo = await ObtenerTipoIdentificadorInternoCodigoAsync(
                    animal.Finca_Codigo,
                    cancellationToken);

                var identificadorInterno = new IdentificadorAnimal
                {
                    Animal_Codigo = animal.Animal_Codigo,
                    Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                    Identificador_Animal_Valor = ConstruirIdentificadorInterno(animal.Animal_Codigo),
                    Identificador_Animal_Es_Principal = false,
                    Identificador_Animal_Activo = true
                };

                identificador.Animal_Codigo = animal.Animal_Codigo;
                await context.IdentificadoresAnimal.AddRangeAsync(
                    [identificador, identificadorInterno],
                    cancellationToken);

                await context.EventosGanaderos.AddAsync(evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                eventoAnimal.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
                eventoAnimal.Animal_Codigo = animal.Animal_Codigo;
                await context.EventosGanaderosAnimal.AddAsync(eventoAnimal, cancellationToken);

                fotoRegistro.Evento_Ganadero_Codigo = evento.Evento_Ganadero_Codigo;
                await context.EventosDetalleRegistroExistente.AddAsync(fotoRegistro, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(Animal Animal, IdentificadorAnimal Identificador, EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleRegistroExistente Foto)> lote,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var tipoIdentificadorInternoCache = new Dictionary<long, long>();

                foreach (var item in lote)
                {
                    await context.Animales.AddAsync(item.Animal, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    if (!tipoIdentificadorInternoCache.TryGetValue(item.Animal.Finca_Codigo, out var tipoIdentificadorInternoCodigo))
                    {
                        tipoIdentificadorInternoCodigo = await ObtenerTipoIdentificadorInternoCodigoAsync(
                            item.Animal.Finca_Codigo,
                            cancellationToken);
                        tipoIdentificadorInternoCache[item.Animal.Finca_Codigo] = tipoIdentificadorInternoCodigo;
                    }

                    var identificadorInterno = new IdentificadorAnimal
                    {
                        Animal_Codigo = item.Animal.Animal_Codigo,
                        Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                        Identificador_Animal_Valor = ConstruirIdentificadorInterno(item.Animal.Animal_Codigo),
                        Identificador_Animal_Es_Principal = false,
                        Identificador_Animal_Activo = true
                    };

                    item.Identificador.Animal_Codigo = item.Animal.Animal_Codigo;
                    await context.IdentificadoresAnimal.AddRangeAsync(
                        [item.Identificador, identificadorInterno],
                        cancellationToken);

                    await context.EventosGanaderos.AddAsync(item.Evento, cancellationToken);
                    await context.SaveChangesAsync(cancellationToken);

                    item.EventoAnimal.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                    item.EventoAnimal.Animal_Codigo = item.Animal.Animal_Codigo;
                    await context.EventosGanaderosAnimal.AddAsync(item.EventoAnimal, cancellationToken);

                    item.Foto.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                    await context.EventosDetalleRegistroExistente.AddAsync(item.Foto, cancellationToken);
                }

                await context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<bool> ExisteIdentificadorAsync(long fincaCodigo, string identificador, CancellationToken cancellationToken = default)
    {
        return await context.IdentificadoresAnimal
            .Join(context.Animales,
                i => i.Animal_Codigo,
                a => a.Animal_Codigo,
                (i, a) => new { i, a })
            .AnyAsync(
                x => x.a.Finca_Codigo == fincaCodigo &&
                     x.i.Identificador_Animal_Valor == identificador &&
                     x.i.Identificador_Animal_Activo,
                cancellationToken);
    }

    public async Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default)
    {
        var totalAnimales = await context.Animales
            .AsNoTracking()
            .CountAsync(item => item.Finca_Codigo == fincaCodigo, cancellationToken);

        return totalAnimales + 1;
    }

    private async Task<long> ObtenerTipoIdentificadorInternoCodigoAsync(
        long fincaCodigo,
        CancellationToken cancellationToken)
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
                    ValidarRegistroExistenteMessages.TipoIdentificadorInternoNoDisponible)
            ]);
        }

        return tipoIdentificadorInternoCodigo.Value;
    }

    public async Task<bool> ExisteIdentificadorActivoEnClienteAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        long tipoIdentificadorCodigo,
        CancellationToken cancellationToken = default)
    {
        var clienteCodigo = await context.Fincas
            .AsNoTracking()
            .Where(f => f.Finca_Codigo == fincaCodigo)
            .Select(f => f.Cliente_Codigo)
            .FirstOrDefaultAsync(cancellationToken);

        return await context.IdentificadoresAnimal
            .Join(
                context.Animales,
                identificador => identificador.Animal_Codigo,
                animal => animal.Animal_Codigo,
                (identificador, animal) => new { identificador, animal })
            .AnyAsync(
                item => item.identificador.Identificador_Animal_Valor == identificadorPrincipal &&
                        item.identificador.Tipo_Identificador_Codigo == tipoIdentificadorCodigo &&
                        item.identificador.Identificador_Animal_Activo &&
                        item.animal.Cliente_Codigo == clienteCodigo,
                cancellationToken);
    }

    public async Task<bool> FincaPoseePotreroAsync(long fincaCodigo, long potreroCodigo, CancellationToken cancellationToken = default)
    {
        return await context.Potreros
            .AnyAsync(
                p => p.Finca_Codigo == fincaCodigo &&
                     p.Potrero_Codigo == potreroCodigo &&
                     p.Potrero_Activo,
                cancellationToken);
    }

    private static string ConstruirIdentificadorInterno(long animalCodigo)
        => $"INT-{animalCodigo:D10}";
}

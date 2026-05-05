using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Identificadores.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class RegistroExistenteRepository(AppDbContext context, IIdentificadorService identificadorService) : IRegistroExistenteRepository
{
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

                var tipoIdentificadorInternoCodigo = await identificadorService.ObtenerTipoIdentificadorInternoCodigoAsync(
                    animal.Finca_Codigo,
                    cancellationToken);

                var identificadorInterno = new IdentificadorAnimal
                {
                    Animal_Codigo = animal.Animal_Codigo,
                    Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                    Identificador_Animal_Valor = identificadorService.ConstruirIdentificadorInterno(animal.Animal_Codigo),
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
                        tipoIdentificadorInternoCodigo = await identificadorService.ObtenerTipoIdentificadorInternoCodigoAsync(
                            item.Animal.Finca_Codigo,
                            cancellationToken);
                        tipoIdentificadorInternoCache[item.Animal.Finca_Codigo] = tipoIdentificadorInternoCodigo;
                    }

                    var identificadorInterno = new IdentificadorAnimal
                    {
                        Animal_Codigo = item.Animal.Animal_Codigo,
                        Tipo_Identificador_Codigo = tipoIdentificadorInternoCodigo,
                        Identificador_Animal_Valor = identificadorService.ConstruirIdentificadorInterno(item.Animal.Animal_Codigo),
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

    public async Task<int> ObtenerSiguienteConsecutivoAsync(long fincaCodigo, CancellationToken cancellationToken = default)
        => await identificadorService.ObtenerSiguienteConsecutivoAsync(fincaCodigo, cancellationToken);

    public async Task<IReadOnlyList<ExistenciaIdentificador>> ExistentesIdentificadoresAsync(
        long fincaCodigo,
        IEnumerable<string> identificadores,
        CancellationToken cancellationToken = default)
    {
        var resultado = await identificadorService.VerificarExistenciaIdentificadoresAsync(fincaCodigo, identificadores, cancellationToken);

        return resultado
            .Select(x => new ExistenciaIdentificador(x.Identificador, x.Existe))
            .ToList();
    }

    public async Task<bool> ExisteIdentificadorActivoEnFincaAsync(
        long fincaCodigo,
        string identificadorPrincipal,
        CancellationToken cancellationToken = default)
    {
        return await context.IdentificadoresAnimal
            .AsNoTracking()
            .Join(
                context.Animales.AsNoTracking(),
                identificador => identificador.Animal_Codigo,
                animal => animal.Animal_Codigo,
                (identificador, animal) => new { identificador, animal })
            .AnyAsync(
                item => item.identificador.Identificador_Animal_Valor == identificadorPrincipal &&
                        item.identificador.Identificador_Animal_Activo &&
                        item.animal.Finca_Codigo == fincaCodigo,
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
}


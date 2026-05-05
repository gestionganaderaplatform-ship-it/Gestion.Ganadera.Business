using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class DescarteRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IDescarteRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleDescarte detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default)
    {
        return await RegistrarLoteAtomicoAsync(
            [(evento, eventoAnimal, detalle, animalActualizado)],
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleDescarte Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        var loteList = lote.ToList();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigos = loteList.Select(x => x.AnimalActualizado.Animal_Codigo).ToList();
            var animalesMap = await context.Animales
                .Where(a => animalCodigos.Contains(a.Animal_Codigo))
                .Select(a => new { a.Animal_Codigo, a.Animal_Activo, a.Finca_Codigo, a.Cliente_Codigo })
                .ToDictionaryAsync(a => a.Animal_Codigo, cancellationToken);

            var actorId = currentActorProvider.ActorNumericId;
            var ahora = DateTime.Now;

            foreach (var item in loteList)
            {
                var animalCodigo = item.AnimalActualizado.Animal_Codigo;

                if (!animalesMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException([new ValidationFailure(nameof(Animal.Animal_Codigo), DescarteMessages.AnimalNoEncontrado)]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException([new ValidationFailure(nameof(Animal.Animal_Codigo), DescarteMessages.AnimalInactivo)]);
                }

                var yaDescartado = await context.EventosGanaderos
                    .Join(context.EventosGanaderosAnimal,
                        e => e.Evento_Ganadero_Codigo,
                        ea => ea.Evento_Ganadero_Codigo,
                        (e, ea) => new { e, ea })
                    .AnyAsync(x => x.ea.Animal_Codigo == animalCodigo
                        && x.e.Evento_Ganadero_Tipo == EventoGanaderoTipo.Descarte
                        && x.e.Evento_Ganadero_Estado == EventoGanaderoEstado.Completado,
                        cancellationToken);

                if (yaDescartado)
                {
                    throw new ValidationException([new ValidationFailure(nameof(Animal.Animal_Codigo), DescarteMessages.AnimalYaDescartado)]);
                }

                // Propagamos datos de contexto para integridad multi-inquilino
                if (item.Evento.Finca_Codigo == 0)
                    item.Evento.Finca_Codigo = animal.Finca_Codigo;
                
                item.Evento.Cliente_Codigo = animal.Cliente_Codigo;
                item.EventoAnimal.Cliente_Codigo = animal.Cliente_Codigo;
                item.Detalle.Cliente_Codigo = animal.Cliente_Codigo;

                await context.EventosGanaderos.AddAsync(item.Evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                item.EventoAnimal.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                item.Detalle.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;

                await context.EventosGanaderosAnimal.AddAsync(item.EventoAnimal, cancellationToken);
                await context.EventosDetalleDescarte.AddAsync(item.Detalle, cancellationToken);

                // Actualización del animal con auditoría manual (necesario en ExecuteUpdate)
                await context.Animales
                    .Where(a => a.Animal_Codigo == animalCodigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Activo, false)
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, item.Detalle.Evento_Detalle_Descarte_Fecha)
                        .SetProperty(a => a.Animal_Fecha_Descarte, item.Detalle.Evento_Detalle_Descarte_Fecha)
                        .SetProperty(a => a.Descarte_Motivo_Codigo, item.Detalle.Descarte_Motivo_Codigo)
                        .SetProperty(a => a.Fecha_Modificado, ahora)
                        .SetProperty(a => a.Modificado_Por, actorId),
                        cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }
}

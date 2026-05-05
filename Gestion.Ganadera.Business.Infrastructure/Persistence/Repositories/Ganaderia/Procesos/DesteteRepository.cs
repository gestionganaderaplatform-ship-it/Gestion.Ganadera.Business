using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class DesteteRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IDesteteRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetalleDestete> detalles,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        var eventosList = eventos.ToList();
        var eventosAnimalList = eventosAnimal.ToList();
        var detallesList = detalles.ToList();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigos = eventosAnimalList.Select(ea => ea.Animal_Codigo).ToList();
            var animalesMap = await context.Animales
                .Where(a => animalCodigos.Contains(a.Animal_Codigo))
                .ToDictionaryAsync(a => a.Animal_Codigo, cancellationToken);

            var actorId = currentActorProvider.ActorNumericId;
            var ahora = DateTime.Now;

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;

                if (!animalesMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), DesteteMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), DesteteMessages.AnimalInactivo)
                    ]);
                }

                // Propagamos datos de contexto
                eventosList[i].Finca_Codigo = animal.Finca_Codigo;
                eventosList[i].Cliente_Codigo = animal.Cliente_Codigo;
                eventosAnimalList[i].Cliente_Codigo = animal.Cliente_Codigo;
                detallesList[i].Cliente_Codigo = animal.Cliente_Codigo;
            }

            await context.EventosGanaderos.AddRangeAsync(eventosList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                eventosAnimalList[i].Evento_Ganadero_Codigo = eventosList[i].Evento_Ganadero_Codigo;
                detallesList[i].Evento_Ganadero_Codigo = eventosList[i].Evento_Ganadero_Codigo;
            }

            await context.EventosGanaderosAnimal.AddRangeAsync(eventosAnimalList, cancellationToken);
            await context.EventosDetalleDestete.AddRangeAsync(detallesList, cancellationToken);

            // 1. Finalizar las relaciones familiares activas y actualizar animales
            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;
                var detalle = detallesList[i];

                // Finalizar relación
                await context.AnimalesRelacionesFamiliares
                    .Where(r => r.Animal_Codigo_Cria == animalCodigo && 
                                r.Animal_Codigo_Madre == detalle.Animal_Codigo_Madre && 
                                r.Animal_Relacion_Familiar_Activa)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(r => r.Animal_Relacion_Familiar_Activa, false)
                        .SetProperty(r => r.Fecha_Modificado, ahora)
                        .SetProperty(r => r.Modificado_Por, actorId),
                        cancellationToken);

                // Actualizar animal (Potrero y fecha último evento)
                if (detalle.Potrero_Destino_Codigo.HasValue)
                {
                    await context.Animales
                        .Where(a => a.Animal_Codigo == animalCodigo)
                        .ExecuteUpdateAsync(s => s
                            .SetProperty(a => a.Potrero_Codigo, detalle.Potrero_Destino_Codigo.Value)
                            .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, ahora)
                            .SetProperty(a => a.Fecha_Modificado, ahora)
                            .SetProperty(a => a.Modificado_Por, actorId),
                            cancellationToken);
                }
                else
                {
                    await context.Animales
                        .Where(a => a.Animal_Codigo == animalCodigo)
                        .ExecuteUpdateAsync(s => s
                            .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, ahora)
                            .SetProperty(a => a.Fecha_Modificado, ahora)
                            .SetProperty(a => a.Modificado_Por, actorId),
                            cancellationToken);
                }
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }
}

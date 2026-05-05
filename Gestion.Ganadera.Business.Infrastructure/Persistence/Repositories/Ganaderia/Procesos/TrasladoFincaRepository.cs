using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class TrasladoFincaRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : ITrasladoFincaRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<Animal> animalesActualizados,
        IEnumerable<EventoDetalleTrasladoFinca> detalles,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();
        var eventosList = eventos.ToList();
        var eventosAnimalList = eventosAnimal.ToList();
        var animalesList = animalesActualizados.ToList();
        var detallesList = detalles.ToList();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigos = animalesList.Select(a => a.Animal_Codigo).ToList();
            var animalesMap = await context.Animales
                .Where(a => animalCodigos.Contains(a.Animal_Codigo))
                .ToDictionaryAsync(a => a.Animal_Codigo, cancellationToken);

            var actorId = currentActorProvider.ActorNumericId;
            var ahora = DateTime.Now;

            for (int i = 0; i < animalesList.Count; i++)
            {
                var animalCodigo = animalesList[i].Animal_Codigo;
                var fincaDestino = animalesList[i].Finca_Codigo;
                var potreroDestino = animalesList[i].Potrero_Codigo;

                if (!animalesMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), TrasladoFincaMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), TrasladoFincaMessages.AnimalInactivo)
                    ]);
                }

                if (animal.Finca_Codigo == fincaDestino)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Finca_Codigo), TrasladoFincaMessages.FincaDestinoIgualOrigen)
                    ]);
                }

                // Propagamos datos de contexto para integridad multi-inquilino
                eventosList[i].Finca_Codigo = fincaDestino; // El evento queda registrado en la finca destino
                eventosList[i].Cliente_Codigo = animal.Cliente_Codigo;
                eventosAnimalList[i].Cliente_Codigo = animal.Cliente_Codigo;

                detallesList[i].Finca_Codigo_Origen = animal.Finca_Codigo;
                detallesList[i].Finca_Codigo_Destino = fincaDestino;
                detallesList[i].Potrero_Codigo_Destino = potreroDestino;
                detallesList[i].Cliente_Codigo = animal.Cliente_Codigo;
            }

            // Guardamos eventos primero para obtener los IDs
            await context.EventosGanaderos.AddRangeAsync(eventosList, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                eventosAnimalList[i].Evento_Ganadero_Codigo = eventosList[i].Evento_Ganadero_Codigo;
                detallesList[i].Evento_Ganadero_Codigo = eventosList[i].Evento_Ganadero_Codigo;
            }

            await context.EventosGanaderosAnimal.AddRangeAsync(eventosAnimalList, cancellationToken);
            await context.EventosDetalleTrasladoFinca.AddRangeAsync(detallesList, cancellationToken);

            // Actualizamos los animales masivamente
            foreach (var animalActualizado in animalesList)
            {
                await context.Animales
                    .Where(a => a.Animal_Codigo == animalActualizado.Animal_Codigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Finca_Codigo, animalActualizado.Finca_Codigo)
                        .SetProperty(a => a.Potrero_Codigo, animalActualizado.Potrero_Codigo)
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, ahora)
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

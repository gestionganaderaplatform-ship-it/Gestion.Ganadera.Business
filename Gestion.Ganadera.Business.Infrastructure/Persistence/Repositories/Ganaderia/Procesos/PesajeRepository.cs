using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class PesajeRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IPesajeRepository
{
    public async Task<bool> CrearRegistroAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetallePesaje detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default)
    {
        return await RegistrarLoteAtomicoAsync(
            [(evento, eventoAnimal, detalle, animalActualizado)],
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetallePesaje Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigos = lote.Select(x => x.AnimalActualizado.Animal_Codigo).ToList();
            var animalesContextMap = await context.Animales
                .Where(a => animalCodigos.Contains(a.Animal_Codigo) && a.Animal_Activo)
                .Select(a => new { a.Animal_Codigo, a.Finca_Codigo, a.Cliente_Codigo })
                .ToDictionaryAsync(a => a.Animal_Codigo, cancellationToken);

            var ahora = DateTime.Now;
            var actorId = currentActorProvider.ActorNumericId;

            foreach (var item in lote)
            {
                if (!animalesContextMap.TryGetValue(item.AnimalActualizado.Animal_Codigo, out var animalData))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(
                            nameof(Animal.Animal_Codigo),
                            PesajeMessages.AnimalNoEncontrado)
                    ]);
                }

                item.Evento.Finca_Codigo = animalData.Finca_Codigo;
                item.Evento.Cliente_Codigo = animalData.Cliente_Codigo;
                item.EventoAnimal.Cliente_Codigo = animalData.Cliente_Codigo;
                item.Detalle.Cliente_Codigo = animalData.Cliente_Codigo;

                await context.EventosGanaderos.AddAsync(item.Evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                item.EventoAnimal.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                await context.EventosGanaderosAnimal.AddAsync(item.EventoAnimal, cancellationToken);

                item.Detalle.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                await context.EventosDetallePesaje.AddAsync(item.Detalle, cancellationToken);

                await context.Animales
                    .Where(a => a.Animal_Codigo == item.AnimalActualizado.Animal_Codigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Peso, item.AnimalActualizado.Animal_Peso)
                        .SetProperty(a => a.Animal_Fecha_Peso, item.AnimalActualizado.Animal_Fecha_Peso)
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, item.AnimalActualizado.Animal_Fecha_Ultimo_Evento)
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
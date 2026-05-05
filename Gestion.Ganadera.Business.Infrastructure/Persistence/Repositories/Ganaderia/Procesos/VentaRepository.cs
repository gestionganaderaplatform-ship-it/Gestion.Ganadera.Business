using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class VentaRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IVentaRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        EventoGanadero evento,
        EventoGanaderoAnimal eventoAnimal,
        EventoDetalleVenta detalle,
        Animal animalActualizado,
        CancellationToken cancellationToken = default)
    {
        return await RegistrarLoteAtomicoAsync(
            [(evento, eventoAnimal, detalle, animalActualizado)],
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAtomicoAsync(
        IEnumerable<(EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleVenta Detalle, Animal AnimalActualizado)> lote,
        CancellationToken cancellationToken = default)
    {
        var strategy = context.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            var animalCodigos = lote.Select(x => x.AnimalActualizado.Animal_Codigo).ToList();
            var animalesContextMap = await context.Animales
                .Where(a => animalCodigos.Contains(a.Animal_Codigo))
                .Select(a => new { a.Animal_Codigo, a.Animal_Activo, a.Finca_Codigo, a.Cliente_Codigo })
                .ToDictionaryAsync(a => a.Animal_Codigo, cancellationToken);

            var ahora = DateTime.Now;
            var actorId = currentActorProvider.ActorNumericId;

            foreach (var item in lote)
            {
                var animalCodigo = item.AnimalActualizado.Animal_Codigo;

                if (!animalesContextMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), VentaMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), VentaMessages.AnimalInactivo)
                    ]);
                }

                var yaVendido = await context.EventosGanaderos
                    .Join(context.EventosGanaderosAnimal,
                        e => e.Evento_Ganadero_Codigo,
                        ea => ea.Evento_Ganadero_Codigo,
                        (e, ea) => new { e, ea })
                    .AnyAsync(x => x.ea.Animal_Codigo == animalCodigo
                        && x.e.Evento_Ganadero_Tipo == EventoGanaderoTipo.Venta
                        && x.e.Evento_Ganadero_Estado == EventoGanaderoEstado.Completado,
                        cancellationToken);

                if (yaVendido)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), VentaMessages.AnimalYaVendido)
                    ]);
                }

                // Propagamos datos de contexto para integridad
                if (item.Evento.Finca_Codigo == 0)
                    item.Evento.Finca_Codigo = animal.Finca_Codigo;
                item.Evento.Cliente_Codigo = animal.Cliente_Codigo;
                item.EventoAnimal.Cliente_Codigo = animal.Cliente_Codigo;
                item.Detalle.Cliente_Codigo = animal.Cliente_Codigo;

                await context.EventosGanaderos.AddAsync(item.Evento, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                item.EventoAnimal.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                await context.EventosGanaderosAnimal.AddAsync(item.EventoAnimal, cancellationToken);

                item.Detalle.Evento_Ganadero_Codigo = item.Evento.Evento_Ganadero_Codigo;
                await context.EventosDetalleVenta.AddAsync(item.Detalle, cancellationToken);

                // Actualización masiva del animal con auditoría manual (necesario en ExecuteUpdate)
                await context.Animales
                    .Where(a => a.Animal_Codigo == animalCodigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Activo, false)
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, item.Detalle.Evento_Detalle_Venta_Fecha)
                        .SetProperty(a => a.Animal_Fecha_Venta, item.Detalle.Evento_Detalle_Venta_Fecha)
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
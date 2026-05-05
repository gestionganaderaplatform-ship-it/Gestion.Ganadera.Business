using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class TratamientoSanitarioRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : ITratamientoSanitarioRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetalleTratamientoSanitario> detalles,
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

            var productoCodigos = detallesList.Select(d => d.Tratamiento_Producto_Codigo).Distinct().ToList();
            var productosMap = await context.TratamientosProductos
                .Where(p => productoCodigos.Contains(p.Tratamiento_Producto_Codigo))
                .ToDictionaryAsync(p => p.Tratamiento_Producto_Codigo, p => p.Tratamiento_Producto_Nombre, cancellationToken);

            var actorId = currentActorProvider.ActorNumericId;
            var ahora = DateTime.Now;

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;

                if (!animalesMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), TratamientoSanitarioMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), TratamientoSanitarioMessages.AnimalInactivo)
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
            await context.EventosDetalleTratamientoSanitario.AddRangeAsync(detallesList, cancellationToken);

            // Actualizamos el snapshot del animal
            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;
                var detalle = detallesList[i];
                productosMap.TryGetValue(detalle.Tratamiento_Producto_Codigo, out var productoNombre);

                await context.Animales
                    .Where(a => a.Animal_Codigo == animalCodigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, ahora)
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Fecha, detalle.Evento_Detalle_Tratamiento_Fecha)
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Tipo, "Tratamiento")
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Producto, productoNombre)
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

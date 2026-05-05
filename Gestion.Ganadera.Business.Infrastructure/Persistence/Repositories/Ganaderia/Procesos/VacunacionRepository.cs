using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class VacunacionRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IVacunacionRepository, IValidarVacunacionRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetalleVacunacion> detalles,
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
                        new ValidationFailure(nameof(Animal.Animal_Codigo), VacunacionMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), VacunacionMessages.AnimalInactivo)
                    ]);
                }

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
            await context.EventosDetalleVacunacion.AddRangeAsync(detallesList, cancellationToken);

            // Actualizamos el snapshot del animal con el último evento sanitario
            var vacunaCodigos = detallesList.Select(d => d.Evento_Detalle_Vacunacion_Vacuna_Codigo).Distinct().ToList();
            var vacunasMap = await context.Vacunas
                .Where(v => vacunaCodigos.Contains(v.Vacuna_Codigo))
                .ToDictionaryAsync(v => v.Vacuna_Codigo, v => v.Vacuna_Nombre, cancellationToken);

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;
                var detalle = detallesList[i];
                vacunasMap.TryGetValue(detalle.Evento_Detalle_Vacunacion_Vacuna_Codigo, out var vacunaNombre);

                await context.Animales
                    .Where(a => a.Animal_Codigo == animalCodigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, detalle.Evento_Detalle_Vacunacion_Fecha)
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Fecha, detalle.Evento_Detalle_Vacunacion_Fecha)
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Tipo, "Vacunación")
                        .SetProperty(a => a.Animal_Ultimo_Evento_Sanitario_Producto, vacunaNombre)
                        .SetProperty(a => a.Fecha_Modificado, ahora)
                        .SetProperty(a => a.Modificado_Por, actorId),
                        cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return true;
        });
    }

    public async Task<bool> AnimalesExistenYActivosAsync(
        List<long> animalCodigos,
        CancellationToken cancellationToken = default)
    {
        var count = await context.Animales
            .Where(a => animalCodigos.Contains(a.Animal_Codigo) && a.Animal_Activo)
            .CountAsync(cancellationToken);

        return count == animalCodigos.Count;
    }

    public async Task<bool> FincaValidaAsync(
        long fincaCodigo,
        CancellationToken cancellationToken = default)
    {
        return await context.Fincas
            .AnyAsync(f => f.Finca_Codigo == fincaCodigo && f.Finca_Activa, cancellationToken);
    }

    public async Task<bool> VacunaValidaAsync(
        long vacunaCodigo,
        CancellationToken cancellationToken = default)
    {
        return await context.Vacunas
            .AnyAsync(v => v.Vacuna_Codigo == vacunaCodigo && v.Vacuna_Activa, cancellationToken);
    }

    public async Task<bool> EnfermedadValidaAsync(
        long enfermedadCodigo,
        CancellationToken cancellationToken = default)
    {
        return await context.VacunasEnfermedades
            .AnyAsync(e => e.Vacuna_Enfermedad_Codigo == enfermedadCodigo && e.Vacuna_Enfermedad_Activa, cancellationToken);
    }
}
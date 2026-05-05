using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Messages;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia.Procesos;

public class PalpacionRepository(
    AppDbContext context,
    ICurrentActorProvider currentActorProvider) : IPalpacionRepository
{
    public async Task<bool> RegistrarAtomicoAsync(
        IEnumerable<EventoGanadero> eventos,
        IEnumerable<EventoGanaderoAnimal> eventosAnimal,
        IEnumerable<EventoDetallePalpacion> detalles,
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

            var resultadoCodigos = detallesList.Select(d => d.Palpacion_Resultado_Codigo).Distinct().ToList();
            var resultadosMap = await context.PalpacionesResultados
                .Where(r => resultadoCodigos.Contains(r.Palpacion_Resultado_Codigo))
                .ToDictionaryAsync(r => r.Palpacion_Resultado_Codigo, cancellationToken);

            var actorId = currentActorProvider.ActorNumericId;
            var ahora = DateTime.Now;

            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;

                if (!animalesMap.TryGetValue(animalCodigo, out var animal))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), PalpacionMessages.AnimalNoEncontrado)
                    ]);
                }

                if (!animal.Animal_Activo)
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), PalpacionMessages.AnimalInactivo)
                    ]);
                }

                // Validación de Elegibilidad: Solo Hembras (H)
                if (animal.Animal_Sexo != "H")
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(Animal.Animal_Codigo), PalpacionMessages.AnimalNoElegible)
                    ]);
                }

                if (!resultadosMap.TryGetValue(detallesList[i].Palpacion_Resultado_Codigo, out var resultado))
                {
                    throw new ValidationException(
                    [
                        new ValidationFailure(nameof(PalpacionResultado.Palpacion_Resultado_Codigo), PalpacionMessages.ResultadoNoEncontrado)
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
            await context.EventosDetallePalpacion.AddRangeAsync(detallesList, cancellationToken);

            // Actualizamos el snapshot del animal (Estado Reproductivo)
            for (int i = 0; i < eventosAnimalList.Count; i++)
            {
                var animalCodigo = eventosAnimalList[i].Animal_Codigo;
                var detalle = detallesList[i];
                var resultado = resultadosMap[detalle.Palpacion_Resultado_Codigo];

                var estadoSimplificado = resultado.Palpacion_Resultado_Nombre.Contains("Preñada", StringComparison.OrdinalIgnoreCase) 
                    ? "Preñada" 
                    : "Abierta";

                await context.Animales
                    .Where(a => a.Animal_Codigo == animalCodigo)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(a => a.Animal_Fecha_Ultimo_Evento, ahora)
                        .SetProperty(a => a.Animal_Ultima_Palpacion_Fecha, detalle.Evento_Detalle_Palpacion_Fecha)
                        .SetProperty(a => a.Animal_Ultimo_Resultado_Reproductivo, resultado.Palpacion_Resultado_Nombre)
                        .SetProperty(a => a.Animal_Estado_Reproductivo_Actual, estadoSimplificado)
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

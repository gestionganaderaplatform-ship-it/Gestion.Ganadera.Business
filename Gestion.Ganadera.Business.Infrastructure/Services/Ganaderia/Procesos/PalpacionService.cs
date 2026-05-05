using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class PalpacionService(IPalpacionRepository repository) : IPalpacionService
{
    public async Task<bool> RegistrarAsync(RegistrarPalpacionRequest request, CancellationToken cancellationToken = default)
    {
        var evento = new EventoGanadero
        {
            Evento_Ganadero_Tipo = EventoGanaderoTipo.RevisionReproductiva,
            Evento_Ganadero_Fecha = request.Fecha_Revision,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Animal_Codigo = request.Animal_Codigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        };

        var detalle = new EventoDetallePalpacion
        {
            Palpacion_Resultado_Codigo = request.Palpacion_Resultado_Codigo,
            Evento_Detalle_Palpacion_Fecha = request.Fecha_Revision,
            Evento_Detalle_Palpacion_Responsable = request.Responsable,
            Evento_Detalle_Palpacion_Dato_Complementario = request.Dato_Complementario,
            Evento_Detalle_Palpacion_Observacion = request.Observacion
        };

        return await repository.RegistrarAtomicoAsync([evento], [eventoAnimal], [detalle], cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(RegistrarPalpacionLoteRequest request, CancellationToken cancellationToken = default)
    {
        var eventos = new List<EventoGanadero>();
        var eventosAnimal = new List<EventoGanaderoAnimal>();
        var detalles = new List<EventoDetallePalpacion>();

        foreach (var animalCodigo in request.Animales)
        {
            eventos.Add(new EventoGanadero
            {
                Evento_Ganadero_Tipo = EventoGanaderoTipo.RevisionReproductiva,
                Evento_Ganadero_Fecha = request.Fecha_Revision,
                Evento_Ganadero_Observacion = request.Observacion,
                Evento_Ganadero_Estado = EventoGanaderoEstado.Completado
            });

            eventosAnimal.Add(new EventoGanaderoAnimal
            {
                Animal_Codigo = animalCodigo,
                Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
            });

            detalles.Add(new EventoDetallePalpacion
            {
                Palpacion_Resultado_Codigo = request.Palpacion_Resultado_Codigo,
                Evento_Detalle_Palpacion_Fecha = request.Fecha_Revision,
                Evento_Detalle_Palpacion_Responsable = request.Responsable,
                Evento_Detalle_Palpacion_Dato_Complementario = request.Dato_Complementario,
                Evento_Detalle_Palpacion_Observacion = request.Observacion
            });
        }

        return await repository.RegistrarAtomicoAsync(eventos, eventosAnimal, detalles, cancellationToken);
    }
}

using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class DesteteService(
    IDesteteRepository repository,
    ICurrentActorProvider currentActorProvider) : IDesteteService
{
    public async Task<bool> RegistrarAsync(RegistrarDesteteRequest request, CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var evento = new EventoGanadero
        {
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Destete,
            Evento_Ganadero_Fecha = request.Fecha_Destete,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Animal_Codigo = request.Animal_Codigo_Cria,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        };

        var detalle = new EventoDetalleDestete
        {
            Animal_Codigo_Madre = request.Animal_Codigo_Madre,
            Potrero_Destino_Codigo = request.Potrero_Destino_Codigo,
            Evento_Detalle_Destete_Fecha = request.Fecha_Destete,
            Evento_Detalle_Destete_Responsable = request.Responsable,
            Evento_Detalle_Destete_Observacion = request.Observacion
        };

        return await repository.RegistrarAtomicoAsync([evento], [eventoAnimal], [detalle], cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(RegistrarDesteteLoteRequest request, CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var eventos = new List<EventoGanadero>();
        var eventosAnimal = new List<EventoGanaderoAnimal>();
        var detalles = new List<EventoDetalleDestete>();

        foreach (var item in request.Items)
        {
            eventos.Add(new EventoGanadero
            {
                Evento_Ganadero_Tipo = EventoGanaderoTipo.Destete,
                Evento_Ganadero_Fecha = request.Fecha_Destete,
                Evento_Ganadero_Fecha_Registro = fechaOperacion,
                Evento_Ganadero_Registrado_Por = usuarioLogueado,
                Evento_Ganadero_Observacion = request.Observacion,
                Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
                Evento_Ganadero_Es_Correccion = false,
                Evento_Ganadero_Es_Anulacion = false
            });

            eventosAnimal.Add(new EventoGanaderoAnimal
            {
                Animal_Codigo = item.Animal_Codigo_Cria,
                Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
            });

            detalles.Add(new EventoDetalleDestete
            {
                Animal_Codigo_Madre = item.Animal_Codigo_Madre,
                Potrero_Destino_Codigo = request.Potrero_Destino_Codigo,
                Evento_Detalle_Destete_Fecha = request.Fecha_Destete,
                Evento_Detalle_Destete_Responsable = request.Responsable,
                Evento_Detalle_Destete_Observacion = request.Observacion
            });
        }

        return await repository.RegistrarAtomicoAsync(eventos, eventosAnimal, detalles, cancellationToken);
    }
}

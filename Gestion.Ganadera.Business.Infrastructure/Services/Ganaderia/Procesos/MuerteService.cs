using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class MuerteService(
    IMuerteRepository repository,
    ICurrentActorProvider currentActorProvider) : IMuerteService
{
    public async Task<bool> RegistrarAsync(
        RegistrarMuerteRequest request,
        CancellationToken cancellationToken = default)
    {
        var entidades = CrearEntidades(
            request.Finca_Codigo,
            request.Animal_Codigo,
            request.Fecha_Muerte,
            request.Causa_Muerte_Codigo,
            request.Observacion);

        return await repository.RegistrarAtomicoAsync(
            entidades.Evento,
            entidades.EventoAnimal,
            entidades.Detalle,
            entidades.AnimalActualizado,
            cancellationToken);
    }

    private (EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleMuerte Detalle, Animal AnimalActualizado) CrearEntidades(
        long fincaCodigo,
        long animalCodigo,
        DateTime fechaMuerte,
        long causaMuerteCodigo,
        string? observacion)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var evento = new EventoGanadero
        {
            Finca_Codigo = fincaCodigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Muerte,
            Evento_Ganadero_Fecha = fechaMuerte,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Animal_Codigo = animalCodigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        };

        var detalle = new EventoDetalleMuerte
        {
            Evento_Detalle_Muerte_Fecha = fechaMuerte,
            Causa_Muerte_Codigo = causaMuerteCodigo,
            Evento_Detalle_Muerte_Observacion = observacion
        };

        var animalActualizado = new Animal
        {
            Animal_Codigo = animalCodigo
        };

        return (evento, eventoAnimal, detalle, animalActualizado);
    }
}
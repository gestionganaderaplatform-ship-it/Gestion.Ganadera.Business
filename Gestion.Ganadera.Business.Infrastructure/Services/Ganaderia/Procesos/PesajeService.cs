using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class PesajeService(
    IPesajeRepository repository,
    ICurrentActorProvider currentActorProvider) : IPesajeService
{
    public async Task<bool> CrearRegistroAsync(
        RegistrarPesajeRequest request,
        CancellationToken cancellationToken = default)
    {
        var entidades = CrearEntidades(
            request.Finca_Codigo,
            request.Animal_Codigo,
            request.Fecha_Pesaje,
            request.Peso,
            request.Observacion);

        return await repository.CrearRegistroAtomicoAsync(
            entidades.Evento,
            entidades.EventoAnimal,
            entidades.Detalle,
            entidades.AnimalActualizado,
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(
        RegistrarPesajeLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var lote = request.Animales
            .Select(animal => CrearEntidades(
                request.Finca_Codigo,
                animal.Animal_Codigo,
                request.Fecha_Pesaje,
                animal.Peso,
                request.Observacion))
            .ToList();

        return await repository.RegistrarLoteAtomicoAsync(lote, cancellationToken);
    }

    private (EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetallePesaje Detalle, Animal AnimalActualizado) CrearEntidades(
        long fincaCodigo,
        long animalCodigo,
        DateTime fechaPesaje,
        decimal peso,
        string? observacion)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var evento = new EventoGanadero
        {
            Finca_Codigo = fincaCodigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Pesaje,
            Evento_Ganadero_Fecha = fechaPesaje,
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

        var detalle = new EventoDetallePesaje
        {
            Evento_Detalle_Pesaje_Fecha = fechaPesaje,
            Evento_Detalle_Peso = peso,
            Evento_Detalle_Pesaje_Observacion = observacion
        };

        var animalActualizado = new Animal
        {
            Animal_Codigo = animalCodigo,
            Animal_Peso = peso,
            Animal_Fecha_Peso = fechaPesaje,
            Animal_Fecha_Ultimo_Evento = fechaPesaje
        };

        return (evento, eventoAnimal, detalle, animalActualizado);
    }
}
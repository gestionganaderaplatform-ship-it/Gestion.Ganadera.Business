using FluentValidation;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class DescarteService(
    IDescarteRepository repository,
    ICurrentActorProvider currentActorProvider,
    IValidator<RegistrarDescarteRequest> validator,
    IValidator<RegistrarDescarteLoteRequest> loteValidator) : IDescarteService
{
    public async Task<bool> RegistrarAsync(RegistrarDescarteRequest request, CancellationToken cancellationToken = default)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var entidades = CrearEntidades(
            request.Finca_Codigo,
            request.Animal_Codigo,
            request.Descarte_Motivo_Codigo,
            request.Evento_Detalle_Descarte_Fecha,
            request.Evento_Detalle_Descarte_Destino,
            request.Evento_Detalle_Descarte_Valor,
            request.Evento_Detalle_Descarte_Observacion);

        return await repository.RegistrarAtomicoAsync(
            entidades.Evento, 
            entidades.EventoAnimal, 
            entidades.Detalle, 
            entidades.AnimalActualizado, 
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(RegistrarDescarteLoteRequest request, CancellationToken cancellationToken = default)
    {
        await loteValidator.ValidateAndThrowAsync(request, cancellationToken);

        var lote = request.Animales_Codigos
            .Select(animalCodigo => CrearEntidades(
                request.Finca_Codigo,
                animalCodigo,
                request.Descarte_Motivo_Codigo,
                request.Evento_Detalle_Descarte_Fecha,
                request.Evento_Detalle_Descarte_Destino,
                request.Evento_Detalle_Descarte_Valor,
                request.Evento_Detalle_Descarte_Observacion))
            .ToList();

        return await repository.RegistrarLoteAtomicoAsync(lote, cancellationToken);
    }

    private (EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleDescarte Detalle, Animal AnimalActualizado) CrearEntidades(
        long fincaCodigo,
        long animalCodigo,
        long motivoCodigo,
        DateTime fechaDescarte,
        string? destino,
        decimal? valor,
        string? observacion)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var evento = new EventoGanadero
        {
            Finca_Codigo = fincaCodigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Descarte,
            Evento_Ganadero_Fecha = fechaDescarte,
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
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado,
            Evento_Ganadero_Animal_Observacion = observacion
        };

        var detalle = new EventoDetalleDescarte
        {
            Descarte_Motivo_Codigo = motivoCodigo,
            Evento_Detalle_Descarte_Fecha = fechaDescarte,
            Evento_Detalle_Descarte_Destino = destino,
            Evento_Detalle_Descarte_Valor = valor,
            Evento_Detalle_Descarte_Observacion = observacion
        };

        var animalActualizado = new Animal
        {
            Animal_Codigo = animalCodigo
        };

        return (evento, eventoAnimal, detalle, animalActualizado);
    }
}

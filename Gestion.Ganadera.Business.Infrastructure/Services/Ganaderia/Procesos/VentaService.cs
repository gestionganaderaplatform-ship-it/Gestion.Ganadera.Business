using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class VentaService(
    IVentaRepository repository,
    ICurrentActorProvider currentActorProvider) : IVentaService
{
    public async Task<bool> RegistrarAsync(
        RegistrarVentaRequest request,
        CancellationToken cancellationToken = default)
    {
        var entidades = CrearEntidades(
            request.Finca_Codigo,
            request.Animal_Codigo,
            request.Fecha_Venta,
            request.Comprador,
            request.Valor,
            request.Observacion);

        return await repository.RegistrarAtomicoAsync(
            entidades.Evento,
            entidades.EventoAnimal,
            entidades.Detalle,
            entidades.AnimalActualizado,
            cancellationToken);
    }

    public async Task<bool> RegistrarLoteAsync(
        RegistrarVentaLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var lote = request.Animales
            .Select(animal => CrearEntidades(
                animal.Finca_Codigo,
                animal.Animal_Codigo,
                request.Fecha_Venta,
                request.Comprador,
                animal.Valor ?? request.Valor_Total,
                request.Observacion))
            .ToList();

        return await repository.RegistrarLoteAtomicoAsync(lote, cancellationToken);
    }

    private (EventoGanadero Evento, EventoGanaderoAnimal EventoAnimal, EventoDetalleVenta Detalle, Animal AnimalActualizado) CrearEntidades(
        long fincaCodigo,
        long animalCodigo,
        DateTime fechaVenta,
        string comprador,
        decimal? valor,
        string? observacion)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var evento = new EventoGanadero
        {
            Finca_Codigo = fincaCodigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.Venta,
            Evento_Ganadero_Fecha = fechaVenta,
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

        var detalle = new EventoDetalleVenta
        {
            Evento_Detalle_Venta_Fecha = fechaVenta,
            Evento_Detalle_Venta_Comprador = comprador,
            Evento_Detalle_Venta_Valor = valor,
            Evento_Detalle_Venta_Observacion = observacion
        };

        var animalActualizado = new Animal
        {
            Animal_Codigo = animalCodigo
        };

        return (evento, eventoAnimal, detalle, animalActualizado);
    }
}
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class MovimientoPotreroService(
    IMovimientoPotreroRepository repository,
    ICurrentActorProvider currentActorProvider) : IMovimientoPotreroService
{
    public async Task<bool> RegistrarAsync(
        RegistrarMovimientoPotreroRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var eventos = request.Animales_Codigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.MovimientoPotrero,
            Evento_Ganadero_Fecha = request.Fecha_Movimiento,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        }).ToList();

        var eventosAnimal = request.Animales_Codigos.Select(animalCodigo => new EventoGanaderoAnimal
        {
            Animal_Codigo = animalCodigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        }).ToList();

        var animalesActualizados = request.Animales_Codigos.Select(animalCodigo => new Animal
        {
            Animal_Codigo = animalCodigo,
            Potrero_Codigo = request.Potrero_Destino_Codigo
        }).ToList();

        var detalles = request.Animales_Codigos.Select(animalCodigo => new EventoDetalleMovimientoPotrero
        {
            Evento_Detalle_Movimiento_Potrero_Fecha = request.Fecha_Movimiento,
            Potrero_Codigo_Destino = request.Potrero_Destino_Codigo
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            animalesActualizados,
            detalles,
            cancellationToken);
    }

    public async Task<bool> ValidarAsync(
        ValidarMovimientoPotreroRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> RegistrarLoteAsync(
        RegistrarMovimientoPotreroLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var animalCodigos = request.Animales.Select(a => a.Animal_Codigo).ToList();

        var eventos = animalCodigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.MovimientoPotrero,
            Evento_Ganadero_Fecha = request.Fecha_Movimiento,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = EventoGanaderoEstado.Completado,
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        }).ToList();

        var eventosAnimal = animalCodigos.Select(animalCodigo => new EventoGanaderoAnimal
        {
            Animal_Codigo = animalCodigo,
            Evento_Ganadero_Animal_Estado_Afectacion = EventoGanaderoAnimalEstadoAfectacion.Procesado
        }).ToList();

        var animalesActualizados = animalCodigos.Select(animalCodigo => new Animal
        {
            Animal_Codigo = animalCodigo,
            Potrero_Codigo = request.Potrero_Destino_Codigo
        }).ToList();

        var detalles = animalCodigos.Select(animalCodigo => new EventoDetalleMovimientoPotrero
        {
            Evento_Detalle_Movimiento_Potrero_Fecha = request.Fecha_Movimiento,
            Potrero_Codigo_Destino = request.Potrero_Destino_Codigo
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            animalesActualizados,
            detalles,
            cancellationToken);
    }
}
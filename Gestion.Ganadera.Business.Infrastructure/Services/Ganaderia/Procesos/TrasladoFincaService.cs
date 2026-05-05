using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Models;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Services.Ganaderia.Procesos;

public class TrasladoFincaService(
    ITrasladoFincaRepository repository,
    ICurrentActorProvider currentActorProvider) : ITrasladoFincaService
{
    public async Task<bool> RegistrarAsync(
        RegistrarTrasladoFincaRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var eventos = request.Animales_Codigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Destino_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.TrasladoFinca,
            Evento_Ganadero_Fecha = request.Fecha_Traslado,
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
            Finca_Codigo = request.Finca_Destino_Codigo,
            Potrero_Codigo = request.Potrero_Destino_Codigo
        }).ToList();

        var detalles = request.Animales_Codigos.Select(animalCodigo => new EventoDetalleTrasladoFinca
        {
            Evento_Detalle_Traslado_Finca_Fecha = request.Fecha_Traslado,
            Finca_Codigo_Origen = request.Finca_Origen_Codigo,
            Finca_Codigo_Destino = request.Finca_Destino_Codigo,
            Potrero_Codigo_Destino = request.Potrero_Destino_Codigo,
            Evento_Detalle_Traslado_Finca_Observacion = request.Observacion
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            animalesActualizados,
            detalles,
            cancellationToken);
    }

    public async Task<bool> ValidarAsync(
        ValidarTrasladoFincaRequest request,
        CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(true);
    }

    public async Task<bool> RegistrarLoteAsync(
        RegistrarTrasladoFincaLoteRequest request,
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var animalCodigos = request.Animales.Select(a => a.Animal_Codigo).ToList();

        var eventos = animalCodigos.Select(animalCodigo => new EventoGanadero
        {
            Finca_Codigo = request.Finca_Destino_Codigo,
            Evento_Ganadero_Tipo = EventoGanaderoTipo.TrasladoFinca,
            Evento_Ganadero_Fecha = request.Fecha_Traslado,
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
            Finca_Codigo = request.Finca_Destino_Codigo,
            Potrero_Codigo = request.Potrero_Destino_Codigo
        }).ToList();

        var detalles = animalCodigos.Select(animalCodigo => new EventoDetalleTrasladoFinca
        {
            Evento_Detalle_Traslado_Finca_Fecha = request.Fecha_Traslado,
            Finca_Codigo_Origen = request.Finca_Origen_Codigo,
            Finca_Codigo_Destino = request.Finca_Destino_Codigo,
            Potrero_Codigo_Destino = request.Potrero_Destino_Codigo,
            Evento_Detalle_Traslado_Finca_Observacion = request.Observacion
        }).ToList();

        return await repository.RegistrarAtomicoAsync(
            eventos,
            eventosAnimal,
            animalesActualizados,
            detalles,
            cancellationToken);
    }
}

using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Gestion.Ganadera.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia.Procesos;

public class RegistroExistenteService(
    IRegistroExistenteRepository repository,
    ICurrentActorProvider currentActorProvider) : IRegistroExistenteService
{
    public Task<bool> CrearRegistroAsync(
        RegistrarExistenteRequest request, 
        CancellationToken cancellationToken = default)
    {
        var usuarioLogueado = currentActorProvider.ActorEmail ?? currentActorProvider.ActorId ?? "SISTEMA";
        var fechaOperacion = DateTime.Now;

        var animal = new Animal
        {
            Finca_Codigo = request.Finca_Codigo,
            Potrero_Codigo = request.Potrero_Codigo,
            Categoria_Animal_Codigo = request.Categoria_Animal_Codigo,
            Animal_Sexo = request.Animal_Sexo,
            Animal_Origen_Ingreso = "REGISTRO_EXISTENTE",
            Animal_Activo = true,
            Animal_Fecha_Ingreso_Inicial = request.Fecha_Informada,
            Animal_Fecha_Registro_Ingreso = fechaOperacion,
            Animal_Fecha_Ultimo_Evento = request.Fecha_Informada
        };

        var identificador = new IdentificadorAnimal
        {
            Tipo_Identificador_Codigo = request.Tipo_Identificador_Codigo,
            Identificador_Animal_Valor = request.Identificador_Principal.Trim(),
            Identificador_Animal_Es_Principal = true,
            Identificador_Animal_Activo = true
        };

        var evento = new EventoGanadero
        {
            Finca_Codigo = request.Finca_Codigo,
            Evento_Ganadero_Tipo = "REGISTRO_EXISTENTE",
            Evento_Ganadero_Fecha = request.Fecha_Informada,
            Evento_Ganadero_Fecha_Registro = fechaOperacion,
            Evento_Ganadero_Registrado_Por = usuarioLogueado,
            Evento_Ganadero_Estado = "COMPLETADO",
            Evento_Ganadero_Observacion = request.Observacion,
            Evento_Ganadero_Es_Correccion = false,
            Evento_Ganadero_Es_Anulacion = false
        };

        var eventoAnimal = new EventoGanaderoAnimal
        {
            Evento_Ganadero_Animal_Estado_Afectacion = "PROCESADO"
        };
        
        var fotoRegistro = new EventoDetalleRegistroExistente
        {
            Potrero_Codigo = request.Potrero_Codigo,
            Categoria_Animal_Codigo = request.Categoria_Animal_Codigo,
            Rango_Edad_Codigo = request.Rango_Edad_Codigo,
            Tipo_Identificador_Codigo = request.Tipo_Identificador_Codigo,
            Evento_Detalle_Registro_Existente_Identificador_Valor = request.Identificador_Principal.Trim(),
            Evento_Detalle_Registro_Existente_Sexo = request.Animal_Sexo,
            Evento_Detalle_Registro_Existente_Fecha_Informada = request.Fecha_Informada
        };

        return repository.CrearRegistroAtómicoAsync(animal, identificador, evento, eventoAnimal, fotoRegistro, cancellationToken);
    }
}

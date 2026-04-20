using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Models;
using Gestion.Ganadera.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia.Procesos;

public class CompraService(
    ICompraRepository repository,
    ICurrentActorProvider currentActorProvider) : ICompraService
{
    public Task<bool> CrearRegistroAsync(
        RegistrarCompraRequest request,
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
            Animal_Origen_Ingreso = "COMPRA",
            Animal_Activo = true,
            Animal_Fecha_Ingreso_Inicial = request.Fecha_Compra,
            Animal_Fecha_Registro_Ingreso = fechaOperacion,
            Animal_Fecha_Ultimo_Evento = request.Fecha_Compra
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
            Evento_Ganadero_Tipo = "COMPRA",
            Evento_Ganadero_Fecha = request.Fecha_Compra,
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

        var fotoRegistro = new EventoDetalleCompra
        {
            Potrero_Codigo = request.Potrero_Codigo,
            Categoria_Animal_Codigo = request.Categoria_Animal_Codigo,
            Rango_Edad_Codigo = request.Rango_Edad_Codigo,
            Tipo_Identificador_Codigo = request.Tipo_Identificador_Codigo,
            Evento_Detalle_Compra_Identificador_Valor = request.Identificador_Principal.Trim(),
            Evento_Detalle_Compra_Sexo = request.Animal_Sexo,
            Evento_Detalle_Compra_Fecha_Compra = request.Fecha_Compra,
            Evento_Detalle_Compra_Origen_Vendedor = request.Origen_Vendedor.Trim(),
            Evento_Detalle_Compra_Valor_Individual = request.Valor_Individual,
            Evento_Detalle_Compra_Observacion = request.Observacion
        };

        return repository.CrearRegistroAtómicoAsync(animal, identificador, evento, eventoAnimal, fotoRegistro, cancellationToken);
    }
}

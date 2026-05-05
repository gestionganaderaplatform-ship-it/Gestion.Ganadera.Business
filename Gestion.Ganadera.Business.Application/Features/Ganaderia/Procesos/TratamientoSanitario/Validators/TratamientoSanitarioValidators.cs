using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TratamientoSanitario.Validators;

public class RegistrarTratamientoValidator : AbstractValidator<RegistrarTratamientoRequest>
{
    public RegistrarTratamientoValidator(ITratamientoProductoRepository productoRepository)
    {
        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.AnimalNoEncontrado);

        RuleFor(x => x.Tratamiento_Producto_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado)
            .MustAsync(async (codigo, cancellation) => await productoRepository.Existe(codigo))
            .WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.FechaObligatoria);
    }
}

public class ValidarTratamientoValidator : AbstractValidator<ValidarTratamientoRequest>
{
    public ValidarTratamientoValidator()
    {
        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.AnimalNoEncontrado);

        RuleFor(x => x.Tratamiento_Producto_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.FechaObligatoria);
    }
}

public class RegistrarTratamientoLoteValidator : AbstractValidator<RegistrarTratamientoLoteRequest>
{
    public RegistrarTratamientoLoteValidator(ITratamientoProductoRepository productoRepository)
    {
        RuleFor(x => x.Animales)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.AnimalNoEncontrado);

        RuleFor(x => x.Tratamiento_Producto_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado)
            .MustAsync(async (codigo, cancellation) => await productoRepository.Existe(codigo))
            .WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.FechaObligatoria);
    }
}

public class ValidarTratamientoLoteValidator : AbstractValidator<ValidarTratamientoLoteRequest>
{
    public ValidarTratamientoLoteValidator()
    {
        RuleFor(x => x.Animales)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.AnimalNoEncontrado);

        RuleFor(x => x.Tratamiento_Producto_Codigo)
            .GreaterThan(0).WithMessage(TratamientoSanitarioMessages.ProductoNoEncontrado);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty().WithMessage(TratamientoSanitarioMessages.FechaObligatoria);
    }
}

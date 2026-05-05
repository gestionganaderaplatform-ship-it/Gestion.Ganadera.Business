using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoProductos.Validators;

public class TratamientoProductoCreateValidator : AbstractValidator<TratamientoProductoCreateViewModel>
{
    public TratamientoProductoCreateValidator(ITratamientoProductoRepository repository)
    {
        RuleFor(x => x.Tratamiento_Producto_Nombre)
            .NotEmpty().WithMessage(TratamientoProductoMessages.NombreObligatorio)
            .MustAsync(async (nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, null, cancellation))
            .WithMessage(TratamientoProductoMessages.NombreDuplicado);

        RuleFor(x => x.Tratamiento_Tipo_Codigo)
            .GreaterThan(0).WithMessage(TratamientoProductoMessages.TipoObligatorio);
    }
}

public class TratamientoProductoUpdateValidator : AbstractValidator<TratamientoProductoUpdateViewModel>
{
    public TratamientoProductoUpdateValidator(ITratamientoProductoRepository repository)
    {
        RuleFor(x => x.Tratamiento_Producto_Codigo)
            .NotEmpty().WithMessage(TratamientoProductoMessages.NoEncontrado);

        RuleFor(x => x.Tratamiento_Producto_Nombre)
            .NotEmpty().WithMessage(TratamientoProductoMessages.NombreObligatorio)
            .MustAsync(async (model, nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, model.Tratamiento_Producto_Codigo, cancellation))
            .WithMessage(TratamientoProductoMessages.NombreDuplicado);

        RuleFor(x => x.Tratamiento_Tipo_Codigo)
            .GreaterThan(0).WithMessage(TratamientoProductoMessages.TipoObligatorio);
    }
}

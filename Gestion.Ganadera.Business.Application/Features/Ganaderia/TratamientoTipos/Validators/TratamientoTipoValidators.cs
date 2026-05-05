using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.TratamientoTipos.Validators;

public class TratamientoTipoCreateValidator : AbstractValidator<TratamientoTipoCreateViewModel>
{
    public TratamientoTipoCreateValidator(ITratamientoTipoRepository repository)
    {
        RuleFor(x => x.Tratamiento_Tipo_Nombre)
            .NotEmpty().WithMessage(TratamientoTipoMessages.NombreObligatorio)
            .MustAsync(async (nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, null, cancellation))
            .WithMessage(TratamientoTipoMessages.NombreDuplicado);
    }
}

public class TratamientoTipoUpdateValidator : AbstractValidator<TratamientoTipoUpdateViewModel>
{
    public TratamientoTipoUpdateValidator(ITratamientoTipoRepository repository)
    {
        RuleFor(x => x.Tratamiento_Tipo_Codigo)
            .NotEmpty().WithMessage(TratamientoTipoMessages.NoEncontrado);

        RuleFor(x => x.Tratamiento_Tipo_Nombre)
            .NotEmpty().WithMessage(TratamientoTipoMessages.NombreObligatorio)
            .MustAsync(async (model, nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, model.Tratamiento_Tipo_Codigo, cancellation))
            .WithMessage(TratamientoTipoMessages.NombreDuplicado);
    }
}

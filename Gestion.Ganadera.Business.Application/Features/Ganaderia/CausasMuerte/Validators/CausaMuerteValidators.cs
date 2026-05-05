using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Validators;

public class CausaMuerteCreateValidator : AbstractValidator<CausaMuerteCreateViewModel>
{
    public CausaMuerteCreateValidator(ICausaMuerteRepository repository)
    {
        RuleFor(x => x.Causa_Muerte_Nombre)
            .NotEmpty().WithMessage(CausaMuerteMessages.NombreRequerido)
            .MaximumLength(100).WithMessage(CausaMuerteMessages.NombreExcedeLongitud)
            .MustAsync(async (nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, null, cancellation))
            .WithMessage(CausaMuerteMessages.NombreDuplicado);

        RuleFor(x => x.Causa_Muerte_Descripcion)
            .MaximumLength(500).WithMessage(CausaMuerteMessages.DescripcionExcedeLongitud);
    }
}

public class CausaMuerteUpdateValidator : AbstractValidator<CausaMuerteUpdateViewModel>
{
    public CausaMuerteUpdateValidator(ICausaMuerteRepository repository)
    {
        RuleFor(x => x.Causa_Muerte_Codigo)
            .GreaterThan(0).WithMessage(CausaMuerteMessages.CausaNoEncontrada);

        RuleFor(x => x.Causa_Muerte_Nombre)
            .NotEmpty().WithMessage(CausaMuerteMessages.NombreRequerido)
            .MaximumLength(100).WithMessage(CausaMuerteMessages.NombreExcedeLongitud)
            .MustAsync(async (model, nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, model.Causa_Muerte_Codigo, cancellation))
            .WithMessage(CausaMuerteMessages.NombreDuplicado);

        RuleFor(x => x.Causa_Muerte_Descripcion)
            .MaximumLength(500).WithMessage(CausaMuerteMessages.DescripcionExcedeLongitud);
    }
}

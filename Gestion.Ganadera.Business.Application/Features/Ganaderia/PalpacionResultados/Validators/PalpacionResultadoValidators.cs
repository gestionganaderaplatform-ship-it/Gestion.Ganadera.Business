using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Validators;

public class PalpacionResultadoCreateValidator : AbstractValidator<PalpacionResultadoCreateViewModel>
{
    public PalpacionResultadoCreateValidator(IPalpacionResultadoRepository repository)
    {
        RuleFor(x => x.Palpacion_Resultado_Nombre)
            .NotEmpty().WithMessage(PalpacionResultadoMessages.NombreObligatorio)
            .MustAsync(async (nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, null, cancellation))
            .WithMessage(PalpacionResultadoMessages.NombreDuplicado);
    }
}

public class PalpacionResultadoUpdateValidator : AbstractValidator<PalpacionResultadoUpdateViewModel>
{
    public PalpacionResultadoUpdateValidator(IPalpacionResultadoRepository repository)
    {
        RuleFor(x => x.Palpacion_Resultado_Codigo)
            .NotEmpty().WithMessage(PalpacionResultadoMessages.NoEncontrado);

        RuleFor(x => x.Palpacion_Resultado_Nombre)
            .NotEmpty().WithMessage(PalpacionResultadoMessages.NombreObligatorio)
            .MustAsync(async (model, nombre, cancellation) => !await repository.ExisteNombreAsync(nombre, model.Palpacion_Resultado_Codigo, cancellation))
            .WithMessage(PalpacionResultadoMessages.NombreDuplicado);
    }
}

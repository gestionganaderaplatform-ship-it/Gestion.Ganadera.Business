using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.DescarteMotivos.Validators;

public class CreateDescarteMotivoValidator : AbstractValidator<CreateDescarteMotivoViewModel>
{
    public CreateDescarteMotivoValidator(IDescarteMotivoRepository repository)
    {
        RuleFor(x => x.Descarte_Motivo_Nombre)
            .NotEmpty().WithMessage(DescarteMotivoMessages.NombreObligatorio)
            .MaximumLength(100)
            .MustAsync(async (name, ct) => !await repository.ExisteNombreAsync(name, null, ct))
            .WithMessage(DescarteMotivoMessages.NombreDuplicado);
    }
}

public class UpdateDescarteMotivoValidator : AbstractValidator<UpdateDescarteMotivoViewModel>
{
    public UpdateDescarteMotivoValidator(IDescarteMotivoRepository repository)
    {
        RuleFor(x => x.Descarte_Motivo_Codigo)
            .NotEmpty().WithMessage(DescarteMotivoMessages.NoEncontrado);

        RuleFor(x => x.Descarte_Motivo_Nombre)
            .NotEmpty().WithMessage(DescarteMotivoMessages.NombreObligatorio)
            .MaximumLength(100)
            .MustAsync(async (model, name, ct) => !await repository.ExisteNombreAsync(name, model.Descarte_Motivo_Codigo, ct))
            .WithMessage(DescarteMotivoMessages.NombreDuplicado);
    }
}

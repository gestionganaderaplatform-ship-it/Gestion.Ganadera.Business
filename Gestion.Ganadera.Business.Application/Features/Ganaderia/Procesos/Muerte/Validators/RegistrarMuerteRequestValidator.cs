using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Muerte.Validators;

public class ValidarMuerteRequestValidator : AbstractValidator<ValidarMuerteRequest>
{
    public ValidarMuerteRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.FincaRequerida);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.AnimalObligatorio);

        RuleFor(x => x.Fecha_Muerte)
            .NotEmpty()
            .WithMessage(MuerteMessages.FechaMuerteRequerida);

        RuleFor(x => x.Causa_Muerte_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.CausaRequerida);
    }
}

public class ValidarMuerteLoteValidator : AbstractValidator<ValidarMuerteLoteRequest>
{
    public ValidarMuerteLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.FincaRequerida);

        RuleFor(x => x.Animales_Codigos)
            .NotEmpty()
            .WithMessage(MuerteMessages.AnimalObligatorio);

        RuleFor(x => x.Fecha_Muerte)
            .NotEmpty()
            .WithMessage(MuerteMessages.FechaMuerteRequerida);

        RuleFor(x => x.Causa_Muerte_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.CausaRequerida);
    }
}

public class RegistrarMuerteRequestValidator : AbstractValidator<RegistrarMuerteRequest>
{
    public RegistrarMuerteRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.FincaRequerida);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.AnimalObligatorio);

        RuleFor(x => x.Fecha_Muerte)
            .NotEmpty()
            .WithMessage(MuerteMessages.FechaMuerteRequerida)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(MuerteMessages.FechaFutura)
            .GreaterThanOrEqualTo(DateTime.Today.AddDays(-7))
            .WithMessage(MuerteMessages.FechaMuyAntigua);

        RuleFor(x => x.Causa_Muerte_Codigo)
            .GreaterThan(0)
            .WithMessage(MuerteMessages.CausaRequerida);
    }
}
using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Destete.Validators;

public class RegistrarDesteteValidator : AbstractValidator<RegistrarDesteteRequest>
{
    public RegistrarDesteteValidator()
    {
        RuleFor(x => x.Animal_Codigo_Cria)
            .GreaterThan(0).WithMessage(DesteteMessages.AnimalNoEncontrado);

        RuleFor(x => x.Animal_Codigo_Madre)
            .GreaterThan(0).WithMessage(DesteteMessages.MadreNoEncontrada);

        RuleFor(x => x.Fecha_Destete)
            .NotEmpty().WithMessage(DesteteMessages.FechaObligatoria);
    }
}

public class ValidarDesteteValidator : AbstractValidator<ValidarDesteteRequest>
{
    public ValidarDesteteValidator()
    {
        RuleFor(x => x.Animal_Codigo_Cria)
            .GreaterThan(0).WithMessage(DesteteMessages.AnimalNoEncontrado);

        RuleFor(x => x.Animal_Codigo_Madre)
            .GreaterThan(0).WithMessage(DesteteMessages.MadreNoEncontrada);

        RuleFor(x => x.Fecha_Destete)
            .NotEmpty().WithMessage(DesteteMessages.FechaObligatoria);
    }
}

public class RegistrarDesteteLoteValidator : AbstractValidator<RegistrarDesteteLoteRequest>
{
    public RegistrarDesteteLoteValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage(DesteteMessages.AnimalNoEncontrado);

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.Animal_Codigo_Cria)
                .GreaterThan(0).WithMessage(DesteteMessages.AnimalNoEncontrado);
            item.RuleFor(i => i.Animal_Codigo_Madre)
                .GreaterThan(0).WithMessage(DesteteMessages.MadreNoEncontrada);
        });

        RuleFor(x => x.Fecha_Destete)
            .NotEmpty().WithMessage(DesteteMessages.FechaObligatoria);
    }
}

public class ValidarDesteteLoteValidator : AbstractValidator<ValidarDesteteLoteRequest>
{
    public ValidarDesteteLoteValidator()
    {
        RuleFor(x => x.Items)
            .NotEmpty().WithMessage(DesteteMessages.AnimalNoEncontrado);

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(i => i.Animal_Codigo_Cria)
                .GreaterThan(0).WithMessage(DesteteMessages.AnimalNoEncontrado);
            item.RuleFor(i => i.Animal_Codigo_Madre)
                .GreaterThan(0).WithMessage(DesteteMessages.MadreNoEncontrada);
        });

        RuleFor(x => x.Fecha_Destete)
            .NotEmpty().WithMessage(DesteteMessages.FechaObligatoria);
    }
}

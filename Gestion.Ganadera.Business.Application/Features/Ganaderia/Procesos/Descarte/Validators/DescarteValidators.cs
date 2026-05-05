using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Descarte.Validators;

public class DescarteValidator : AbstractValidator<RegistrarDescarteRequest>
{
    public DescarteValidator()
    {
        RuleFor(x => x.Finca_Codigo).NotEmpty().WithMessage(DescarteMessages.FincaRequerida);
        RuleFor(x => x.Animal_Codigo).NotEmpty().WithMessage(DescarteMessages.AnimalRequerido);
        RuleFor(x => x.Descarte_Motivo_Codigo).NotEmpty().WithMessage(DescarteMessages.MotivoRequerido);
        RuleFor(x => x.Evento_Detalle_Descarte_Fecha)
            .NotEmpty().WithMessage(DescarteMessages.FechaRequerida)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(DescarteMessages.FechaInvalida);
    }
}

public class ValidarDescarteValidator : AbstractValidator<ValidarDescarteRequest>
{
    public ValidarDescarteValidator()
    {
        RuleFor(x => x.Finca_Codigo).NotEmpty().WithMessage(DescarteMessages.FincaRequerida);
        RuleFor(x => x.Animal_Codigo).NotEmpty().WithMessage(DescarteMessages.AnimalRequerido);
        RuleFor(x => x.Descarte_Motivo_Codigo).NotEmpty().WithMessage(DescarteMessages.MotivoRequerido);
        RuleFor(x => x.Evento_Detalle_Descarte_Fecha)
            .NotEmpty().WithMessage(DescarteMessages.FechaRequerida);
    }
}

public class DescarteLoteValidator : AbstractValidator<RegistrarDescarteLoteRequest>
{
    public DescarteLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo).NotEmpty().WithMessage(DescarteMessages.FincaRequerida);
        RuleFor(x => x.Animales_Codigos).NotEmpty().WithMessage(DescarteMessages.AnimalesRequeridos);
        RuleFor(x => x.Descarte_Motivo_Codigo).NotEmpty().WithMessage(DescarteMessages.MotivoRequerido);
        RuleFor(x => x.Evento_Detalle_Descarte_Fecha)
            .NotEmpty().WithMessage(DescarteMessages.FechaRequerida)
            .LessThanOrEqualTo(DateTime.Now).WithMessage(DescarteMessages.FechaInvalida);
    }
}

public class ValidarDescarteLoteValidator : AbstractValidator<ValidarDescarteLoteRequest>
{
    public ValidarDescarteLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo).NotEmpty().WithMessage(DescarteMessages.FincaRequerida);
        RuleFor(x => x.Animales_Codigos).NotEmpty().WithMessage(DescarteMessages.AnimalesRequeridos);
        RuleFor(x => x.Descarte_Motivo_Codigo).NotEmpty().WithMessage(DescarteMessages.MotivoRequerido);
        RuleFor(x => x.Evento_Detalle_Descarte_Fecha)
            .NotEmpty().WithMessage(DescarteMessages.FechaRequerida);
    }
}

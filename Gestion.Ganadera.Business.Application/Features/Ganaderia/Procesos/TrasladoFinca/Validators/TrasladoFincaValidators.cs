using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.TrasladoFinca.Validators;

public class ValidarTrasladoFincaRequestValidator : AbstractValidator<ValidarTrasladoFincaRequest>
{
    public ValidarTrasladoFincaRequestValidator()
    {
        RuleFor(x => x.Finca_Origen_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaOrigenObligatoria);

        RuleFor(x => x.Finca_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaDestinoObligatoria)
            .NotEqual(x => x.Finca_Origen_Codigo)
            .WithMessage(TrasladoFincaMessages.FincaDestinoIgualOrigen);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Animales_Codigos)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.AnimalesObligatorios);
    }
}

public class ValidarTrasladoFincaLoteValidator : AbstractValidator<ValidarTrasladoFincaLoteRequest>
{
    public ValidarTrasladoFincaLoteValidator()
    {
        RuleFor(x => x.Finca_Origen_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaOrigenObligatoria);

        RuleFor(x => x.Finca_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaDestinoObligatoria)
            .NotEqual(x => x.Finca_Origen_Codigo)
            .WithMessage(TrasladoFincaMessages.FincaDestinoIgualOrigen);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Fecha_Traslado)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.FechaObligatoria);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.AnimalesObligatorios);
    }
}

public class RegistrarTrasladoFincaRequestValidator : AbstractValidator<RegistrarTrasladoFincaRequest>
{
    public RegistrarTrasladoFincaRequestValidator()
    {
        Include(new ValidarTrasladoFincaRequestValidator());

        RuleFor(x => x.Fecha_Traslado)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(TrasladoFincaMessages.FechaFutura);
    }
}

public class RegistrarTrasladoFincaLoteRequestValidator : AbstractValidator<RegistrarTrasladoFincaLoteRequest>
{
    public RegistrarTrasladoFincaLoteRequestValidator()
    {
        RuleFor(x => x.Finca_Origen_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaOrigenObligatoria);

        RuleFor(x => x.Finca_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.FincaDestinoObligatoria)
            .NotEqual(x => x.Finca_Origen_Codigo)
            .WithMessage(TrasladoFincaMessages.FincaDestinoIgualOrigen);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(TrasladoFincaMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Fecha_Traslado)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(TrasladoFincaMessages.FechaFutura);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(TrasladoFincaMessages.AnimalesObligatorios);
    }
}

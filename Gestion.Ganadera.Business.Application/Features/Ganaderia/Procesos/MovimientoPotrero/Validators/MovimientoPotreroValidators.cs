using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.MovimientoPotrero.Validators;

public class ValidarMovimientoPotreroRequestValidator : AbstractValidator<ValidarMovimientoPotreroRequest>
{
    public ValidarMovimientoPotreroRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.FincaObligatoria);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Animales_Codigos)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.AnimalesObligatorios);
    }
}

public class ValidarMovimientoPotreroLoteValidator : AbstractValidator<ValidarMovimientoPotreroLoteRequest>
{
    public ValidarMovimientoPotreroLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.FincaObligatoria);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Fecha_Movimiento)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.FechaObligatoria);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.AnimalesObligatorios);
    }
}

public class RegistrarMovimientoPotreroRequestValidator : AbstractValidator<RegistrarMovimientoPotreroRequest>
{
    public RegistrarMovimientoPotreroRequestValidator()
    {
        Include(new ValidarMovimientoPotreroRequestValidator());

        RuleFor(x => x.Fecha_Movimiento)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(MovimientoPotreroMessages.FechaFutura);
    }
}

public class RegistrarMovimientoPotreroLoteRequestValidator : AbstractValidator<RegistrarMovimientoPotreroLoteRequest>
{
    public RegistrarMovimientoPotreroLoteRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.FincaObligatoria);

        RuleFor(x => x.Potrero_Destino_Codigo)
            .GreaterThan(0)
            .WithMessage(MovimientoPotreroMessages.PotreroDestinoObligatorio);

        RuleFor(x => x.Fecha_Movimiento)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(MovimientoPotreroMessages.FechaFutura);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(MovimientoPotreroMessages.AnimalesObligatorios);
    }
}
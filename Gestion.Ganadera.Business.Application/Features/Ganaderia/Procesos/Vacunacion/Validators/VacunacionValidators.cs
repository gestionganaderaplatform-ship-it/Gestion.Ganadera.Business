using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Vacunacion.Validators;

public class ValidarVacunacionRequestValidator : AbstractValidator<ValidarVacunacionRequest>
{
    public ValidarVacunacionRequestValidator(IValidarVacunacionRepository repository)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.FincaObligatoria)
            .MustAsync(async (codigo, _) => await repository.FincaValidaAsync(codigo))
            .WithMessage(VacunacionMessages.FincaInvalida);

        RuleFor(x => x.Animales_Codigos)
            .NotEmpty()
            .WithMessage(VacunacionMessages.AnimalesObligatorios)
            .MustAsync(async (codigos, _) => await repository.AnimalesExistenYActivosAsync(codigos))
            .WithMessage(VacunacionMessages.AnimalesInvalidos);

        RuleFor(x => x.Vacuna_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.VacunaObligatoria)
            .MustAsync(async (codigo, _) => await repository.VacunaValidaAsync(codigo))
            .WithMessage(VacunacionMessages.VacunaInvalida);

        RuleFor(x => x.Ciclo_Vacunacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.CicloObligatorio);

        RuleFor(x => x.Vacuna_Enfermedad_Codigo)
            .NotEmpty()
            .WithMessage(VacunacionMessages.EnfermedadObligatoria)
            .MustAsync(async (codigo, _) => await repository.EnfermedadValidaAsync(codigo!.Value))
            .WithMessage(VacunacionMessages.EnfermedadInvalida);

        RuleFor(x => x.Lote_Biologico)
            .NotEmpty()
            .WithMessage(VacunacionMessages.LoteObligatorio)
            .MaximumLength(50)
            .WithMessage(VacunacionMessages.LoteLongitudMaxima);
    }
}

public class ValidarVacunacionLoteValidator : AbstractValidator<ValidarVacunacionLoteRequest>
{
    public ValidarVacunacionLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.FincaObligatoria);

        RuleFor(x => x.Vacuna_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.VacunaObligatoria);

        RuleFor(x => x.Ciclo_Vacunacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.CicloObligatorio);

        RuleFor(x => x.Vacuna_Enfermedad_Codigo)
            .NotEmpty()
            .WithMessage(VacunacionMessages.EnfermedadObligatoria);

        RuleFor(x => x.Lote_Biologico)
            .NotEmpty()
            .WithMessage(VacunacionMessages.LoteObligatorio)
            .MaximumLength(50)
            .WithMessage(VacunacionMessages.LoteLongitudMaxima);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.FechaObligatoria);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(VacunacionMessages.AnimalesObligatorios);
    }
}

public class RegistrarVacunacionRequestValidator : AbstractValidator<RegistrarVacunacionRequest>
{
    public RegistrarVacunacionRequestValidator(IValidarVacunacionRepository repository)
    {
        Include(new ValidarVacunacionRequestValidator(repository));

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(VacunacionMessages.FechaFutura);

        RuleFor(x => x.Soporte_Certificado_Nombre)
            .NotEmpty()
            .WithMessage(VacunacionMessages.SoporteObligatorio);
    }
}

public class RegistrarVacunacionLoteRequestValidator : AbstractValidator<RegistrarVacunacionLoteRequest>
{
    public RegistrarVacunacionLoteRequestValidator(IValidarVacunacionRepository repository)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.FincaObligatoria)
            .MustAsync(async (codigo, _) => await repository.FincaValidaAsync(codigo))
            .WithMessage(VacunacionMessages.FincaInvalida);

        RuleFor(x => x.Vacuna_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.VacunaObligatoria)
            .MustAsync(async (codigo, _) => await repository.VacunaValidaAsync(codigo))
            .WithMessage(VacunacionMessages.VacunaInvalida);

        RuleFor(x => x.Ciclo_Vacunacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.CicloObligatorio);

        RuleFor(x => x.Vacuna_Enfermedad_Codigo)
            .NotEmpty()
            .WithMessage(VacunacionMessages.EnfermedadObligatoria)
            .MustAsync(async (codigo, _) => await repository.EnfermedadValidaAsync(codigo!.Value))
            .WithMessage(VacunacionMessages.EnfermedadInvalida);

        RuleFor(x => x.Lote_Biologico)
            .NotEmpty()
            .WithMessage(VacunacionMessages.LoteObligatorio)
            .MaximumLength(50)
            .WithMessage(VacunacionMessages.LoteLongitudMaxima);

        RuleFor(x => x.Fecha_Aplicacion)
            .NotEmpty()
            .WithMessage(VacunacionMessages.FechaObligatoria)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(VacunacionMessages.FechaFutura);

        RuleFor(x => x.Soporte_Certificado_Nombre)
            .NotEmpty()
            .WithMessage(VacunacionMessages.SoporteObligatorio);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(VacunacionMessages.AnimalesObligatorios)
            .MustAsync(async (animales, _) => await repository.AnimalesExistenYActivosAsync(animales.Select(a => a.Animal_Codigo).ToList()))
            .WithMessage(VacunacionMessages.AnimalesInvalidos);
    }
}

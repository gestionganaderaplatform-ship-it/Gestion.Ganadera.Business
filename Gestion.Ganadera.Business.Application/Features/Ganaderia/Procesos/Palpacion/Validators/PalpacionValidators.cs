using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.PalpacionResultados.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Palpacion.Validators;

public class RegistrarPalpacionValidator : AbstractValidator<RegistrarPalpacionRequest>
{
    public RegistrarPalpacionValidator(IPalpacionResultadoRepository resultadoRepository)
    {
        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.AnimalNoEncontrado);

        RuleFor(x => x.Palpacion_Resultado_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.ResultadoNoEncontrado)
            .MustAsync(async (codigo, cancellation) => await resultadoRepository.Existe(codigo))
            .WithMessage(PalpacionMessages.ResultadoNoEncontrado);

        RuleFor(x => x.Fecha_Revision)
            .NotEmpty().WithMessage(PalpacionMessages.FechaObligatoria);
    }
}

public class ValidarPalpacionValidator : AbstractValidator<ValidarPalpacionRequest>
{
    public ValidarPalpacionValidator(IPalpacionResultadoRepository resultadoRepository)
    {
        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.AnimalNoEncontrado);

        RuleFor(x => x.Palpacion_Resultado_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.ResultadoNoEncontrado);

        RuleFor(x => x.Fecha_Revision)
            .NotEmpty().WithMessage(PalpacionMessages.FechaObligatoria);
    }
}

public class RegistrarPalpacionLoteValidator : AbstractValidator<RegistrarPalpacionLoteRequest>
{
    public RegistrarPalpacionLoteValidator(IPalpacionResultadoRepository resultadoRepository)
    {
        RuleFor(x => x.Animales)
            .NotEmpty().WithMessage(PalpacionMessages.AnimalNoEncontrado);

        RuleFor(x => x.Palpacion_Resultado_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.ResultadoNoEncontrado)
            .MustAsync(async (codigo, cancellation) => await resultadoRepository.Existe(codigo))
            .WithMessage(PalpacionResultadoMessages.NoEncontrado);

        RuleFor(x => x.Fecha_Revision)
            .NotEmpty().WithMessage(PalpacionMessages.FechaObligatoria);
    }
}

public class ValidarPalpacionLoteValidator : AbstractValidator<ValidarPalpacionLoteRequest>
{
    public ValidarPalpacionLoteValidator()
    {
        RuleFor(x => x.Animales)
            .NotEmpty().WithMessage(PalpacionMessages.AnimalNoEncontrado);

        RuleFor(x => x.Palpacion_Resultado_Codigo)
            .GreaterThan(0).WithMessage(PalpacionMessages.ResultadoNoEncontrado);

        RuleFor(x => x.Fecha_Revision)
            .NotEmpty().WithMessage(PalpacionMessages.FechaObligatoria);
    }
}

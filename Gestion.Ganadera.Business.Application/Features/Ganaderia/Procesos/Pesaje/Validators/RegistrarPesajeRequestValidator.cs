using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Pesaje.Validators;

public class ValidarPesajeRequestValidator : AbstractValidator<ValidarPesajeRequest>
{
    public ValidarPesajeRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.FincaObligatoria);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.AnimalObligatorio);

        RuleFor(x => x.Fecha_Pesaje)
            .NotEmpty()
            .WithMessage(PesajeMessages.FechaObligatoria);

        RuleFor(x => x.Peso)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.PesoMinimoMsg)
            .LessThanOrEqualTo(PesajeMessages.PesoMaximo)
            .WithMessage(PesajeMessages.PesoMaximoMsg);
    }
}

public class ValidarPesajeLoteValidator : AbstractValidator<ValidarPesajeLoteRequest>
{
    public ValidarPesajeLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.FincaObligatoria);

        RuleFor(x => x.Fecha_Pesaje)
            .NotEmpty()
            .WithMessage(PesajeMessages.FechaObligatoria);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage("Debe incluir al menos un animal.");
    }
}

public class RegistrarPesajeRequestValidator : AbstractValidator<RegistrarPesajeRequest>
{
    public RegistrarPesajeRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.FincaObligatoria);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.AnimalObligatorio);

        RuleFor(x => x.Fecha_Pesaje)
            .NotEmpty()
            .WithMessage(PesajeMessages.FechaObligatoria);

        RuleFor(x => x.Peso)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.PesoMinimoMsg)
            .LessThanOrEqualTo(PesajeMessages.PesoMaximo)
            .WithMessage(PesajeMessages.PesoMaximoMsg);
    }
}

public class RegistrarPesajeLoteRequestValidator : AbstractValidator<RegistrarPesajeLoteRequest>
{
    public RegistrarPesajeLoteRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PesajeMessages.FincaObligatoria);

        RuleFor(x => x.Fecha_Pesaje)
            .NotEmpty()
            .WithMessage(PesajeMessages.FechaObligatoria);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage("Debe incluir al menos un animal.");

        RuleForEach(x => x.Animales)
            .ChildRules(animal =>
            {
                animal.RuleFor(a => a.Animal_Codigo)
                    .GreaterThan(0)
                    .WithMessage(PesajeMessages.AnimalObligatorio);

                animal.RuleFor(a => a.Peso)
                    .GreaterThan(0)
                    .WithMessage(PesajeMessages.PesoMinimoMsg)
                    .LessThanOrEqualTo(PesajeMessages.PesoMaximo)
                    .WithMessage(PesajeMessages.PesoMaximoMsg);
            });
    }
}
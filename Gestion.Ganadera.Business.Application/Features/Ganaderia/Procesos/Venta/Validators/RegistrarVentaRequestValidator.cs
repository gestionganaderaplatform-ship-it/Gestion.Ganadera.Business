using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Venta.Validators;

public class ValidarVentaRequestValidator : AbstractValidator<ValidarVentaRequest>
{
    public ValidarVentaRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VentaMessages.FincaRequerida);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(VentaMessages.AnimalRequerido);

        RuleFor(x => x.Fecha_Venta)
            .NotEmpty()
            .WithMessage(VentaMessages.FechaVentaRequerida);
    }
}

public class ValidarVentaLoteValidator : AbstractValidator<ValidarVentaLoteRequest>
{
    public ValidarVentaLoteValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VentaMessages.FincaRequerida);

        RuleFor(x => x.Fecha_Venta)
            .NotEmpty()
            .WithMessage(VentaMessages.FechaVentaRequerida);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(VentaMessages.AlMenosUnAnimal);
    }
}

public class RegistrarVentaRequestValidator : AbstractValidator<RegistrarVentaRequest>
{
    public RegistrarVentaRequestValidator()
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(VentaMessages.FincaRequerida);

        RuleFor(x => x.Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(VentaMessages.AnimalRequerido);

        RuleFor(x => x.Fecha_Venta)
            .NotEmpty()
            .WithMessage(VentaMessages.FechaVentaRequerida);

        RuleFor(x => x.Comprador)
            .NotEmpty()
            .WithMessage(VentaMessages.CompradorRequerido)
            .MaximumLength(200);

        RuleFor(x => x.Valor)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(VentaMessages.ValorRequerido);

        RuleFor(x => x.Fecha_Venta)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(VentaMessages.FechaFutura);
    }
}

public class RegistrarVentaLoteRequestValidator : AbstractValidator<RegistrarVentaLoteRequest>
{
    public RegistrarVentaLoteRequestValidator()
    {
        RuleFor(x => x.Fecha_Venta)
            .NotEmpty()
            .WithMessage(VentaMessages.FechaVentaRequerida);

        RuleFor(x => x.Comprador)
            .NotEmpty()
            .WithMessage(VentaMessages.CompradorRequerido)
            .MaximumLength(200);

        RuleFor(x => x.Animales)
            .NotEmpty()
            .WithMessage(VentaMessages.AlMenosUnAnimal);

        RuleForEach(x => x.Animales)
            .ChildRules(animal =>
            {
                animal.RuleFor(a => a.Finca_Codigo)
                    .GreaterThan(0)
                    .WithMessage(VentaMessages.FincaRequerida);

                animal.RuleFor(a => a.Animal_Codigo)
                    .GreaterThan(0)
                    .WithMessage(VentaMessages.AnimalRequerido);

                animal.RuleFor(a => a.Valor)
                    .NotNull()
                    .GreaterThan(0)
                    .WithMessage(VentaMessages.ValorRequerido);
            });

        RuleFor(x => x.Fecha_Venta)
            .LessThanOrEqualTo(DateTime.Today)
            .WithMessage(VentaMessages.FechaFutura);
    }
}
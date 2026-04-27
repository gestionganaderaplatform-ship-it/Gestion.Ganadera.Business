using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Animales.Validators;

public class AnimalConsultaFilterValidator : AbstractValidator<AnimalConsultaFilterViewModel>
{
    public AnimalConsultaFilterValidator()
    {
        RuleFor(x => x.Animal_Fecha_Ingreso_Inicial)
            .Must(fecha => !fecha.HasValue || fecha.Value.Date <= DateTime.Today)
            .WithMessage(AnimalConsultaMessages.FechaIngresoFutura);
            
        RuleFor(x => x.Animal_Identificador_Principal)
            .MaximumLength(50).WithMessage(AnimalConsultaMessages.IdentificadorInvalido);
    }
}

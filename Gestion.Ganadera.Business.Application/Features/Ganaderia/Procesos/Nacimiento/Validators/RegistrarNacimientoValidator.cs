using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Validators;

public class RegistrarNacimientoValidator : AbstractValidator<RegistrarNacimientoRequest>
{
    public RegistrarNacimientoValidator(IValidator<ValidarNacimientoRequest> validadorBase)
    {
        Include(validadorBase);

        RuleFor(x => x.Peso_Nacer)
            .Must(valor => !valor.HasValue || valor.Value > 0)
            .WithMessage(NacimientoMessages.PesoNacerInvalido);
    }
}

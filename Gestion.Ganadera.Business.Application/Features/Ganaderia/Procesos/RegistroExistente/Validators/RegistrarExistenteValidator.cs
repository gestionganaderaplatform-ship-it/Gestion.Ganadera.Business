using FluentValidation;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Validators;

public class RegistrarExistenteValidator : AbstractValidator<RegistrarExistenteRequest>
{
    public RegistrarExistenteValidator(IValidator<ValidarRegistroExistenteRequest> validadorBase)
    {
        Include(validadorBase);

        RuleFor(x => x.Fecha_Informada)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.FechaInformadaRequerida)
            .Must(fecha => fecha <= DateTime.Now).WithMessage(ValidarRegistroExistenteMessages.FechaInformadaFutura);
    }
}

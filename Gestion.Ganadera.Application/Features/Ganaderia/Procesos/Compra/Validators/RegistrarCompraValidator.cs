using FluentValidation;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Models;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.Compra.Validators;

public class RegistrarCompraValidator : AbstractValidator<RegistrarCompraRequest>
{
    public RegistrarCompraValidator(IValidator<ValidarCompraRequest> validadorBase)
    {
        Include(validadorBase);

        RuleFor(x => x.Fecha_Compra)
            .NotEmpty().WithMessage(CompraMessages.FechaCompraRequerida)
            .Must(fecha => fecha <= DateTime.Now).WithMessage(CompraMessages.FechaCompraFutura);

        RuleFor(x => x.Origen_Vendedor)
            .NotEmpty().WithMessage(CompraMessages.OrigenVendedorRequerido);
    }
}

using FluentValidation;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Common.Messages;
using Gestion.Ganadera.Business.Application.Features.Base.Models;

namespace Gestion.Ganadera.Business.Application.Features.Base.Validators
{
    /// <summary>
    /// Valida codigos numericos simples recibidos por endpoints base del template.
    /// </summary>
    public class CodigoRequestValidator : AbstractValidator<CodigoRequest>
    {
        public CodigoRequestValidator()
        {
            RuleFor(x => x.Codigo)
             .NotEmpty().WithMessage(ValidationMessages.CodeRequired)
             .Must(c => !string.IsNullOrWhiteSpace(c) && !c.Contains(" "))
             .WithMessage(ValidationMessages.CodeNoSpaces)
             .Matches(RegexPatterns.SoloNumeros).WithMessage(ValidationMessages.CodeMustBeNumeric)
             .Must(c => long.TryParse(c, out _)).WithMessage(ValidationMessages.CodeMustBeInLongRange);
        }
    }
}
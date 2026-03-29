using FluentValidation;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Messages;
using Gestion.Ganadera.Application.Features.Base.Models;

namespace Gestion.Ganadera.Application.Features.Base.Validators
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
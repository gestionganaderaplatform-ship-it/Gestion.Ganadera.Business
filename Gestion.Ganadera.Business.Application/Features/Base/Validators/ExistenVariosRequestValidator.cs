using FluentValidation;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Common.Messages;
using Gestion.Ganadera.Business.Application.Features.Base.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Base.Validators
{
    /// <summary>
    /// Valida requests que consultan existencia masiva de llaves primarias.
    /// </summary>
    public class ExistenVariosRequestValidator : AbstractValidator<ExistenVariosRequest<string>>
    {
        public ExistenVariosRequestValidator()
        {
            RuleFor(x => x.Codigos)
                .NotEmpty().WithMessage(ValidationMessages.AtLeastOneCodeRequired);

            RuleForEach(x => x.Codigos)
                .NotNull().WithMessage(ValidationMessages.CodesCannotBeNull)
                .Matches(RegexPatterns.SoloNumeros).WithMessage(ValidationMessages.CodeDigitsOnly);

            RuleFor(x => x.PropiedadClave)
                .NotEmpty().WithMessage(ValidationMessages.KeyPropertyRequired)
                .Matches(RegexPatterns.SoloLetrasConGuionBajo)
                .WithMessage(ValidationMessages.KeyPropertyInvalidFormat);
        }
    }
}
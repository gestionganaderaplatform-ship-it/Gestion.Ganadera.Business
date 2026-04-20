using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Validators;

public class RangoEdadCreateValidator : StandardEntityValidator<RangoEdadCreateViewModel>
{
    public RangoEdadCreateValidator(
        IEntityValidationMetadata metadata,
        IRangoEdadRepository repository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Rango_Edad_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(RangoEdadValidationMessages.RangoEdadNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(RangoEdadValidationMessages.RangoEdadNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Rango_Edad_Nombre) && currentClientProvider.ClientNumericId.HasValue, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Rango_Edad_Nombre.Trim(), null, cancellationToken))
                .WithMessage(RangoEdadValidationMessages.RangoEdadNombreDuplicado)
                .WithName(nameof(RangoEdadCreateViewModel.Rango_Edad_Nombre));
        });
    }
}

public class RangoEdadUpdateValidator : StandardEntityValidator<RangoEdadUpdateViewModel>
{
    public RangoEdadUpdateValidator(
        IEntityValidationMetadata metadata,
        IRangoEdadRepository repository)
        : base(metadata)
    {
        RuleFor(x => x.Rango_Edad_Codigo)
            .GreaterThan(0)
            .WithMessage(RangoEdadValidationMessages.RangoEdadCodigoInvalido)
            .MustAsync(async (codigo, _) => await repository.Existe(codigo))
            .WithMessage(RangoEdadValidationMessages.RangoEdadNoExiste);

        RuleFor(x => x.Rango_Edad_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(RangoEdadValidationMessages.RangoEdadNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(RangoEdadValidationMessages.RangoEdadNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Rango_Edad_Nombre), () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Rango_Edad_Nombre.Trim(), model.Rango_Edad_Codigo, cancellationToken))
                .WithMessage(RangoEdadValidationMessages.RangoEdadNombreDuplicado)
                .WithName(nameof(RangoEdadUpdateViewModel.Rango_Edad_Nombre));
        });
    }
}

public class RangoEdadViewValidator : StandardEntityValidator<RangoEdadViewModel>
{
    public RangoEdadViewValidator(
        IEntityValidationMetadata metadata,
        IRangoEdadService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Rango_Edad_Codigo > 0, () =>
        {
            RuleFor(x => x.Rango_Edad_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(RangoEdadValidationMessages.RangoEdadNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Rango_Edad_Nombre), () =>
        {
            RuleFor(x => x.Rango_Edad_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(RangoEdadValidationMessages.RangoEdadNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(RangoEdadValidationMessages.RangoEdadNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}

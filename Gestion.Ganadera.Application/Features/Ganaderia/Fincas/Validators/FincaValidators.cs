using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Validators;

public class FincaCreateValidator : StandardEntityValidator<FincaCreateViewModel>
{
    public FincaCreateValidator(
        IEntityValidationMetadata metadata,
        IFincaRepository repository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Finca_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(FincaValidationMessages.FincaNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(FincaValidationMessages.FincaNombreNoDebeEmpezarOTerminarConEspacios);

        When(
            x => !string.IsNullOrWhiteSpace(x.Finca_Nombre) &&
                 currentClientProvider.ClientNumericId.HasValue,
            () =>
            {
                RuleFor(x => x.Finca_Nombre)
                    .MustAsync(async (nombre, cancellationToken) =>
                        !await repository.ExisteNombreAsync(nombre.Trim(), null, cancellationToken))
                    .WithMessage(FincaValidationMessages.FincaNombreDuplicado);
            });
    }
}

public class FincaUpdateValidator : StandardEntityValidator<FincaUpdateViewModel>
{
    public FincaUpdateValidator(
        IEntityValidationMetadata metadata,
        IFincaRepository repository)
        : base(metadata)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(FincaValidationMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) =>
                await repository.Existe(codigo))
            .WithMessage(FincaValidationMessages.FincaNoExiste);

        RuleFor(x => x.Finca_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(FincaValidationMessages.FincaNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(FincaValidationMessages.FincaNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Finca_Nombre), () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Finca_Nombre.Trim(), model.Finca_Codigo, cancellationToken))
                .WithMessage(FincaValidationMessages.FincaNombreDuplicado);
        });
    }
}

public class FincaViewValidator : StandardEntityValidator<FincaViewModel>
{
    public FincaViewValidator(
        IEntityValidationMetadata metadata,
        IFincaService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Finca_Codigo > 0, () =>
        {
            RuleFor(x => x.Finca_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(FincaValidationMessages.FincaNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Finca_Nombre), () =>
        {
            RuleFor(x => x.Finca_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(FincaValidationMessages.FincaNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(FincaValidationMessages.FincaNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}

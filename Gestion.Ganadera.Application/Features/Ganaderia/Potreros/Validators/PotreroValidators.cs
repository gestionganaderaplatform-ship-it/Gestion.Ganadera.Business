using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Validators;

public class PotreroCreateValidator : StandardEntityValidator<PotreroCreateViewModel>
{
    public PotreroCreateValidator(
        IEntityValidationMetadata metadata,
        IPotreroRepository repository,
        IFincaRepository fincaRepository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PotreroValidationMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, _) => await fincaRepository.Existe(codigo))
            .WithMessage(PotreroValidationMessages.FincaNoExiste);

        RuleFor(x => x.Potrero_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(PotreroValidationMessages.PotreroNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(PotreroValidationMessages.PotreroNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Potrero_Nombre) && x.Finca_Codigo > 0 && currentClientProvider.ClientNumericId.HasValue, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Finca_Codigo, model.Potrero_Nombre.Trim(), null, cancellationToken))
                .WithMessage(PotreroValidationMessages.PotreroNombreDuplicado)
                .WithName(nameof(PotreroCreateViewModel.Potrero_Nombre));
        });
    }
}

public class PotreroUpdateValidator : StandardEntityValidator<PotreroUpdateViewModel>
{
    public PotreroUpdateValidator(
        IEntityValidationMetadata metadata,
        IPotreroRepository repository,
        IFincaRepository fincaRepository)
        : base(metadata)
    {
        RuleFor(x => x.Potrero_Codigo)
            .GreaterThan(0)
            .WithMessage(PotreroValidationMessages.PotreroCodigoInvalido)
            .MustAsync(async (codigo, _) => await repository.Existe(codigo))
            .WithMessage(PotreroValidationMessages.PotreroNoExiste);

        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0)
            .WithMessage(PotreroValidationMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, _) => await fincaRepository.Existe(codigo))
            .WithMessage(PotreroValidationMessages.FincaNoExiste);

        RuleFor(x => x.Potrero_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(PotreroValidationMessages.PotreroNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(PotreroValidationMessages.PotreroNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Potrero_Nombre) && x.Finca_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Finca_Codigo, model.Potrero_Nombre.Trim(), model.Potrero_Codigo, cancellationToken))
                .WithMessage(PotreroValidationMessages.PotreroNombreDuplicado)
                .WithName(nameof(PotreroUpdateViewModel.Potrero_Nombre));
        });
    }
}

public class PotreroViewValidator : StandardEntityValidator<PotreroViewModel>
{
    public PotreroViewValidator(
        IEntityValidationMetadata metadata,
        IPotreroService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Potrero_Codigo > 0, () =>
        {
            RuleFor(x => x.Potrero_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(PotreroValidationMessages.PotreroNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Potrero_Nombre), () =>
        {
            RuleFor(x => x.Potrero_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(PotreroValidationMessages.PotreroNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(PotreroValidationMessages.PotreroNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}

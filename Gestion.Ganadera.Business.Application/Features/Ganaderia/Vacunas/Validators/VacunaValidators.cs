using FluentValidation;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Common.Extensions;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Vacunas.Validators;

public class VacunaCreateValidator : StandardEntityValidator<VacunaCreateViewModel>
{
    public VacunaCreateValidator(
        IEntityValidationMetadata metadata,
        IVacunaRepository repository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Vacuna_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(VacunacionMessages.VacunaNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(VacunacionMessages.VacunaNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Vacuna_Nombre) && currentClientProvider.ClientNumericId.HasValue, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Vacuna_Nombre.Trim(), null, cancellationToken))
                .WithMessage(VacunacionMessages.VacunaNombreDuplicado)
                .WithName(nameof(VacunaCreateViewModel.Vacuna_Nombre));
        });
    }
}

public class VacunaUpdateValidator : StandardEntityValidator<VacunaUpdateViewModel>
{
    public VacunaUpdateValidator(
        IEntityValidationMetadata metadata,
        IVacunaRepository repository)
        : base(metadata)
    {
        RuleFor(x => x.Vacuna_Codigo)
            .GreaterThan(0)
            .WithMessage(VacunacionMessages.VacunaCodigoInvalido)
            .MustAsync(async (codigo, _) => await repository.Existe(codigo))
            .WithMessage(VacunacionMessages.VacunaNoExiste);

        RuleFor(x => x.Vacuna_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(VacunacionMessages.VacunaNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(VacunacionMessages.VacunaNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Vacuna_Nombre), () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Vacuna_Nombre.Trim(), model.Vacuna_Codigo, cancellationToken))
                .WithMessage(VacunacionMessages.VacunaNombreDuplicado)
                .WithName(nameof(VacunaUpdateViewModel.Vacuna_Nombre));
        });
    }
}

public class VacunaViewValidator : StandardEntityValidator<VacunaViewModel>
{
    public VacunaViewValidator(
        IEntityValidationMetadata metadata,
        IVacunaService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Vacuna_Codigo > 0, () =>
        {
            RuleFor(x => x.Vacuna_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(VacunacionMessages.VacunaNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Vacuna_Nombre), () =>
        {
            RuleFor(x => x.Vacuna_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(VacunacionMessages.VacunaNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(VacunacionMessages.VacunaNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}
using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Common.Extensions;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Validators;

public class TipoIdentificadorCreateValidator : StandardEntityValidator<TipoIdentificadorCreateViewModel>
{
    public TipoIdentificadorCreateValidator(
        IEntityValidationMetadata metadata,
        ITipoIdentificadorRepository repository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Tipo_Identificador_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Tipo_Identificador_Nombre) && currentClientProvider.ClientNumericId.HasValue, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Tipo_Identificador_Nombre.Trim(), null, cancellationToken))
                .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreDuplicado)
                .WithName(nameof(TipoIdentificadorCreateViewModel.Tipo_Identificador_Nombre));
        });
    }
}

public class TipoIdentificadorUpdateValidator : StandardEntityValidator<TipoIdentificadorUpdateViewModel>
{
    public TipoIdentificadorUpdateValidator(
        IEntityValidationMetadata metadata,
        ITipoIdentificadorRepository repository)
        : base(metadata)
    {
        RuleFor(x => x.Tipo_Identificador_Codigo)
            .GreaterThan(0)
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorCodigoInvalido)
            .MustAsync(async (codigo, _) => await repository.Existe(codigo))
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNoExiste);

        RuleFor(x => x.Tipo_Identificador_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Tipo_Identificador_Nombre), () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Tipo_Identificador_Nombre.Trim(), model.Tipo_Identificador_Codigo, cancellationToken))
                .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreDuplicado)
                .WithName(nameof(TipoIdentificadorUpdateViewModel.Tipo_Identificador_Nombre));
        });
    }
}

public class TipoIdentificadorViewValidator : StandardEntityValidator<TipoIdentificadorViewModel>
{
    public TipoIdentificadorViewValidator(
        IEntityValidationMetadata metadata,
        ITipoIdentificadorService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Tipo_Identificador_Codigo > 0, () =>
        {
            RuleFor(x => x.Tipo_Identificador_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Tipo_Identificador_Nombre), () =>
        {
            RuleFor(x => x.Tipo_Identificador_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(TipoIdentificadorValidationMessages.TipoIdentificadorNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}

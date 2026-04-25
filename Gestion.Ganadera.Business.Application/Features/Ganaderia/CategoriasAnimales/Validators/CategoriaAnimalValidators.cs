using FluentValidation;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Common.Extensions;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Validators;

public class CategoriaAnimalCreateValidator : StandardEntityValidator<CategoriaAnimalCreateViewModel>
{
    public CategoriaAnimalCreateValidator(
        IEntityValidationMetadata metadata,
        ICategoriaAnimalRepository repository,
        ICurrentClientProvider currentClientProvider)
        : base(metadata)
    {
        RuleFor(x => x.Categoria_Animal_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Categoria_Animal_Nombre) && currentClientProvider.ClientNumericId.HasValue, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Categoria_Animal_Nombre.Trim(), null, cancellationToken))
                .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreDuplicado)
                .WithName(nameof(CategoriaAnimalCreateViewModel.Categoria_Animal_Nombre));
        });
    }
}

public class CategoriaAnimalUpdateValidator : StandardEntityValidator<CategoriaAnimalUpdateViewModel>
{
    public CategoriaAnimalUpdateValidator(
        IEntityValidationMetadata metadata,
        ICategoriaAnimalRepository repository)
        : base(metadata)
    {
        RuleFor(x => x.Categoria_Animal_Codigo)
            .GreaterThan(0)
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalCodigoInvalido)
            .MustAsync(async (codigo, _) => await repository.Existe(codigo))
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNoExiste);

        RuleFor(x => x.Categoria_Animal_Nombre)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreFormatoInvalido)
            .Must(nombre => nombre.Trim() == nombre)
            .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreNoDebeEmpezarOTerminarConEspacios);

        When(x => !string.IsNullOrWhiteSpace(x.Categoria_Animal_Nombre), () =>
        {
            RuleFor(x => x)
                .MustAsync(async (model, cancellationToken) =>
                    !await repository.ExisteNombreAsync(model.Categoria_Animal_Nombre.Trim(), model.Categoria_Animal_Codigo, cancellationToken))
                .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreDuplicado)
                .WithName(nameof(CategoriaAnimalUpdateViewModel.Categoria_Animal_Nombre));
        });
    }
}

public class CategoriaAnimalViewValidator : StandardEntityValidator<CategoriaAnimalViewModel>
{
    public CategoriaAnimalViewValidator(
        IEntityValidationMetadata metadata,
        ICategoriaAnimalService service)
        : base(metadata, vm => vm.ToFilterDictionary().Keys.ToHashSet())
    {
        When(x => x.Categoria_Animal_Codigo > 0, () =>
        {
            RuleFor(x => x.Categoria_Animal_Codigo)
                .MustAsync(async (codigo, _) => await service.Existe(codigo))
                .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNoExiste);
        });

        When(x => !string.IsNullOrWhiteSpace(x.Categoria_Animal_Nombre), () =>
        {
            RuleFor(x => x.Categoria_Animal_Nombre)
                .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion)
                .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreFormatoInvalido)
                .Must(nombre => nombre.Trim() == nombre)
                .WithMessage(CategoriaAnimalValidationMessages.CategoriaAnimalNombreNoDebeEmpezarOTerminarConEspacios);
        });
    }
}

using FluentValidation;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Validators;

public class RegistrarExistenteLoteValidator : AbstractValidator<RegistrarExistenteLoteRequest>
{
    public RegistrarExistenteLoteValidator(
        IValidarRegistroExistenteRepository registroExistenteRepository,
        IFincaRepository fincaRepository,
        IPotreroRepository potreroRepository,
        ICategoriaAnimalRepository categoriaRepository,
        IRangoEdadRepository rangoRepository,
        ITipoIdentificadorRepository tipoIdentificadorRepository)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) => await fincaRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.FincaNoExiste);

        RuleFor(x => x.Potrero_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.PotreroCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) => await potreroRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.PotreroNoExiste);

        When(x => x.Finca_Codigo > 0 && x.Potrero_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                    await registroExistenteRepository.FincaPoseePotreroAsync(
                        request.Finca_Codigo,
                        request.Potrero_Codigo,
                        cancellationToken))
                .WithMessage(ValidarRegistroExistenteMessages.PotreroNoPerteneceAFinca)
                .WithName(nameof(RegistrarExistenteLoteRequest.Potrero_Codigo));
        });

        RuleFor(x => x.Categoria_Animal_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.CategoriaCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) => await categoriaRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.CategoriaNoExiste);

        When(
            x => x.Categoria_Animal_Codigo > 0 &&
                 !string.IsNullOrWhiteSpace(x.Animal_Sexo),
            () =>
            {
                RuleFor(x => x)
                    .MustAsync(async (request, cancellationToken) =>
                        await categoriaRepository.EsCompatibleConSexoAsync(
                            request.Categoria_Animal_Codigo,
                            request.Animal_Sexo,
                            cancellationToken))
                    .WithMessage(ValidarRegistroExistenteMessages.CategoriaIncompatibleConSexo)
                    .WithName(nameof(RegistrarExistenteLoteRequest.Categoria_Animal_Codigo));
            });

        RuleFor(x => x.Rango_Edad_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.RangoEdadCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) => await rangoRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.RangoNoExiste);

        RuleFor(x => x.Animal_Sexo)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.SexoRequerido)
            .Must(EsSexoValido).WithMessage(ValidarRegistroExistenteMessages.SexoInvalido);

        RuleFor(x => x.Fecha_Informada)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.FechaInformadaRequerida)
            .Must(fecha => fecha.Date <= DateTime.Today).WithMessage(ValidarRegistroExistenteMessages.FechaInformadaFutura);

        RuleFor(x => x.Fecha_Nacimiento_Comun)
            .Must(fecha => !fecha.HasValue || fecha.Value.Date <= DateTime.Today)
            .WithMessage(ValidarRegistroExistenteMessages.FechaNacimientoFutura);

        RuleFor(x => x.Animales)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.LoteSinAnimales)
            .Must(animales => animales is null || animales.Count <= 100).WithMessage(ValidarRegistroExistenteMessages.LoteSuperaMaximo)
            .Must(NoHayIdentificadoresRepetidos).WithMessage(ValidarRegistroExistenteMessages.IdentificadoresRepetidosEnLote);

        When(x => x.Finca_Codigo > 0 && x.Animales is { Count: > 0 }, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                    await NoHayIdentificadoresActivosEnFincaAsync(
                        request,
                        registroExistenteRepository,
                        cancellationToken))
                .WithMessage(ValidarRegistroExistenteMessages.IdentificadorDuplicado)
                .WithName(nameof(RegistrarExistenteLoteRequest.Animales));
        });

        RuleForEach(x => x.Animales)
            .SetValidator(new IdentificadorIndividualValidator(tipoIdentificadorRepository, registroExistenteRepository));
    }

    private static bool NoHayIdentificadoresRepetidos(IEnumerable<IdentificadorIndividualRequest>? animales)
    {
        if (animales is null)
        {
            return true;
        }

        var identificadores = animales
            .Select(animal => animal?.Identificador_Principal?.Trim() ?? string.Empty)
            .Where(identificador => !string.IsNullOrWhiteSpace(identificador))
            .ToList();

        return identificadores.Count == identificadores.Distinct(StringComparer.OrdinalIgnoreCase).Count();
    }

    private static async Task<bool> NoHayIdentificadoresActivosEnFincaAsync(
        RegistrarExistenteLoteRequest request,
        IValidarRegistroExistenteRepository registroExistenteRepository,
        CancellationToken cancellationToken)
    {
        var identificadores = request.Animales
            .Select(animal => animal?.Identificador_Principal?.Trim() ?? string.Empty)
            .Where(identificador => !string.IsNullOrWhiteSpace(identificador))
            .Distinct(StringComparer.OrdinalIgnoreCase);

        foreach (var identificador in identificadores)
        {
            if (await registroExistenteRepository.ExisteIdentificadorActivoEnFincaAsync(
                    request.Finca_Codigo,
                    identificador,
                    cancellationToken))
            {
                return false;
            }
        }

        return true;
    }

    private static bool EsSexoValido(string? sexo)
    {
        return sexo?.Trim().ToUpperInvariant() switch
        {
            "M" => true,
            "H" => true,
            "MACHO" => true,
            "HEMBRA" => true,
            _ => false
        };
    }
}

public class IdentificadorIndividualValidator : AbstractValidator<IdentificadorIndividualRequest>
{
    public IdentificadorIndividualValidator(
        ITipoIdentificadorRepository tipoIdentificadorRepository,
        IValidarRegistroExistenteRepository registroExistenteRepository)
    {
        RuleFor(x => x.Identificador_Principal)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.IdentificadorRequerido)
            .MaximumLength(50).WithMessage(ValidarRegistroExistenteMessages.IdentificadorLongitudMaxima)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion).WithMessage(ValidarRegistroExistenteMessages.IdentificadorFormatoInvalido);

        RuleFor(x => x.Tipo_Identificador_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.TipoIdentificadorCodigoInvalido)
            .MustAsync(async (codigo, cancellationToken) => await tipoIdentificadorRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.TipoIdentificadorNoExiste);

        RuleFor(x => x.Fecha_Nacimiento)
            .Must(fecha => !fecha.HasValue || fecha.Value.Date <= DateTime.Today)
            .WithMessage(ValidarRegistroExistenteMessages.FechaNacimientoFutura);
    }
}

using FluentValidation;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Nacimiento.Validators;

public class ValidarNacimientoValidator : AbstractValidator<ValidarNacimientoRequest>
{
    public ValidarNacimientoValidator(
        INacimientoRepository nacimientoRepository,
        IValidarRegistroExistenteRepository registroExistenteRepository,
        IFincaRepository fincaRepository,
        IPotreroRepository potreroRepository,
        ICategoriaAnimalRepository categoriaRepository,
        ITipoIdentificadorRepository tipoIdentificadorRepository)
    {
        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0).WithMessage(NacimientoMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, _) => await fincaRepository.Existe(codigo))
            .WithMessage(NacimientoMessages.FincaNoExiste);

        RuleFor(x => x.Madre_Animal_Codigo)
            .GreaterThan(0).WithMessage(NacimientoMessages.MadreCodigoInvalido);

        When(x => x.Finca_Codigo > 0 && x.Madre_Animal_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                    await nacimientoRepository.MadreExisteEnFincaAsync(
                        request.Finca_Codigo,
                        request.Madre_Animal_Codigo,
                        cancellationToken))
                .WithMessage(NacimientoMessages.MadreNoExiste)
                .WithName(nameof(ValidarNacimientoRequest.Madre_Animal_Codigo));

            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                    await nacimientoRepository.MadreElegibleEnFincaAsync(
                        request.Finca_Codigo,
                        request.Madre_Animal_Codigo,
                        cancellationToken))
                .WithMessage(NacimientoMessages.MadreNoElegible)
                .WithName(nameof(ValidarNacimientoRequest.Madre_Animal_Codigo));
        });

        RuleFor(x => x.Fecha_Nacimiento)
            .NotEmpty().WithMessage(NacimientoMessages.FechaNacimientoRequerida)
            .Must(fecha => fecha.Date <= DateTime.Today).WithMessage(NacimientoMessages.FechaNacimientoFutura);

        RuleFor(x => x.Identificador_Principal)
            .NotEmpty().WithMessage(NacimientoMessages.IdentificadorRequerido)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion).WithMessage(NacimientoMessages.IdentificadorFormatoInvalido)
            .MustAsync(async (request, identificador, cancellationToken) =>
                !await registroExistenteRepository.ExisteIdentificadorActivoEnFincaAsync(
                    request.Finca_Codigo,
                    identificador.Trim(),
                    cancellationToken))
            .WithMessage(NacimientoMessages.IdentificadorDuplicado)
            .WithName(nameof(ValidarNacimientoRequest.Identificador_Principal));

        RuleFor(x => x.Potrero_Codigo)
            .GreaterThan(0).WithMessage(NacimientoMessages.PotreroCodigoInvalido)
            .MustAsync(async (codigo, _) => await potreroRepository.Existe(codigo))
            .WithMessage(NacimientoMessages.PotreroNoExiste);

        When(x => x.Finca_Codigo > 0 && x.Potrero_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) =>
                    await registroExistenteRepository.FincaPoseePotreroAsync(
                        request.Finca_Codigo,
                        request.Potrero_Codigo,
                        cancellationToken))
                .WithMessage(NacimientoMessages.PotreroNoPerteneceAFinca)
                .WithName(nameof(ValidarNacimientoRequest.Potrero_Codigo));
        });

        RuleFor(x => x.Categoria_Animal_Codigo)
            .GreaterThan(0).WithMessage(NacimientoMessages.CategoriaCodigoInvalido)
            .MustAsync(async (codigo, _) => await categoriaRepository.Existe(codigo))
            .WithMessage(NacimientoMessages.CategoriaNoExiste);

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
                    .WithMessage(NacimientoMessages.CategoriaIncompatibleConSexo)
                    .WithName(nameof(ValidarNacimientoRequest.Categoria_Animal_Codigo));
            });

        RuleFor(x => x.Tipo_Identificador_Codigo)
            .GreaterThan(0).WithMessage(NacimientoMessages.TipoIdentificadorCodigoInvalido)
            .MustAsync(async (codigo, _) => await tipoIdentificadorRepository.Existe(codigo))
            .WithMessage(NacimientoMessages.TipoIdentificadorNoExiste);

        RuleFor(x => x.Animal_Sexo)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(NacimientoMessages.SexoRequerido)
            .Must(EsSexoValido)
            .WithMessage(NacimientoMessages.SexoInvalido);
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

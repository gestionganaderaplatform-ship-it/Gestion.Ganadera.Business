using FluentValidation;
using Gestion.Ganadera.Business.Application.Common.Constants;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Messages;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Models;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.Procesos.Compra.Validators;

public class ValidarCompraValidator : AbstractValidator<ValidarCompraRequest>
{
    public ValidarCompraValidator(
        IValidarRegistroExistenteRepository registroExistenteRepository,
        IFincaRepository fincaRepository,
        IPotreroRepository potreroRepository,
        ICategoriaAnimalRepository categoriaRepository,
        IRangoEdadRepository rangoRepository,
        ITipoIdentificadorRepository tipoIdentificadorRepository)
    {
        RuleFor(x => x.Identificador_Principal)
            .NotEmpty().WithMessage(CompraMessages.IdentificadorRequerido)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion).WithMessage(CompraMessages.IdentificadorFormatoInvalido)
            .MustAsync(async (request, identificador, cancellationToken) => 
                !await registroExistenteRepository.ExisteIdentificadorActivoEnClienteAsync(
                    request.Finca_Codigo,
                    identificador.Trim(),
                    request.Tipo_Identificador_Codigo,
                    cancellationToken))
            .WithMessage(CompraMessages.IdentificadorDuplicado)
            .WithName(nameof(ValidarCompraRequest.Identificador_Principal));

        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0).WithMessage(CompraMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, _) => await fincaRepository.Existe(codigo))
            .WithMessage(CompraMessages.FincaNoExiste);

        RuleFor(x => x.Potrero_Codigo)
            .GreaterThan(0).WithMessage(CompraMessages.PotreroCodigoInvalido)
            .MustAsync(async (codigo, _) => await potreroRepository.Existe(codigo))
            .WithMessage(CompraMessages.PotreroNoExiste);

        When(x => x.Finca_Codigo > 0 && x.Potrero_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) => 
                    await registroExistenteRepository.FincaPoseePotreroAsync(request.Finca_Codigo, request.Potrero_Codigo, cancellationToken))
                .WithMessage(CompraMessages.PotreroNoPerteneceAFinca)
                .WithName(nameof(ValidarCompraRequest.Potrero_Codigo));
        });

        RuleFor(x => x.Categoria_Animal_Codigo)
           .GreaterThan(0).WithMessage(CompraMessages.CategoriaCodigoInvalido)
           .MustAsync(async (codigo, _) => await categoriaRepository.Existe(codigo))
           .WithMessage(CompraMessages.CategoriaNoExiste);

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
                    .WithMessage(CompraMessages.CategoriaIncompatibleConSexo)
                    .WithName(nameof(ValidarCompraRequest.Categoria_Animal_Codigo));
            });

        RuleFor(x => x.Rango_Edad_Codigo)
           .GreaterThan(0).WithMessage(CompraMessages.RangoEdadCodigoInvalido)
           .MustAsync(async (codigo, _) => await rangoRepository.Existe(codigo))
           .WithMessage(CompraMessages.RangoNoExiste);

        RuleFor(x => x.Tipo_Identificador_Codigo)
           .GreaterThan(0).WithMessage(CompraMessages.TipoIdentificadorCodigoInvalido)
           .MustAsync(async (codigo, _) => await tipoIdentificadorRepository.Existe(codigo))
           .WithMessage(CompraMessages.TipoIdentificadorNoExiste);

        RuleFor(x => x.Animal_Sexo)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage(CompraMessages.SexoRequerido)
           .Must(EsSexoValido)
           .WithMessage(CompraMessages.SexoInvalido);
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

using FluentValidation;
using Gestion.Ganadera.Application.Common.Constants;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Fincas.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Potreros.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Messages;
using Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Models;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.TiposIdentificadores.Interfaces;

namespace Gestion.Ganadera.Application.Features.Ganaderia.Procesos.RegistroExistente.Validators;

public class ValidarRegistroExistenteValidator : AbstractValidator<ValidarRegistroExistenteRequest>
{
    public ValidarRegistroExistenteValidator(
        IValidarRegistroExistenteRepository registroExistenteRepository,
        IFincaRepository fincaRepository,
        IPotreroRepository potreroRepository,
        ICategoriaAnimalRepository categoriaRepository,
        IRangoEdadRepository rangoRepository,
        ITipoIdentificadorRepository tipoIdentificadorRepository)
    {
        RuleFor(x => x.Identificador_Principal)
            .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.IdentificadorRequerido)
            .Matches(RegexPatterns.AlfanumericoConAcentosYPuntuacion).WithMessage(ValidarRegistroExistenteMessages.IdentificadorFormatoInvalido)
            .MustAsync(async (request, identificador, cancellationToken) => 
                !await registroExistenteRepository.ExisteIdentificadorActivoEnClienteAsync(identificador.Trim(), request.Tipo_Identificador_Codigo, cancellationToken))
            .WithMessage(ValidarRegistroExistenteMessages.IdentificadorDuplicado)
            .WithName(nameof(ValidarRegistroExistenteRequest.Identificador_Principal));

        RuleFor(x => x.Finca_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.FincaCodigoInvalido)
            .MustAsync(async (codigo, _) => await fincaRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.FincaNoExiste);

        RuleFor(x => x.Potrero_Codigo)
            .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.PotreroCodigoInvalido)
            .MustAsync(async (codigo, _) => await potreroRepository.Existe(codigo))
            .WithMessage(ValidarRegistroExistenteMessages.PotreroNoExiste);

        When(x => x.Finca_Codigo > 0 && x.Potrero_Codigo > 0, () =>
        {
            RuleFor(x => x)
                .MustAsync(async (request, cancellationToken) => 
                    await registroExistenteRepository.FincaPoseePotreroAsync(request.Finca_Codigo, request.Potrero_Codigo, cancellationToken))
                .WithMessage(ValidarRegistroExistenteMessages.PotreroNoPerteneceAFinca)
                .WithName(nameof(ValidarRegistroExistenteRequest.Potrero_Codigo));
        });

        RuleFor(x => x.Categoria_Animal_Codigo)
           .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.CategoriaCodigoInvalido)
           .MustAsync(async (codigo, _) => await categoriaRepository.Existe(codigo))
           .WithMessage(ValidarRegistroExistenteMessages.CategoriaNoExiste);

        RuleFor(x => x.Rango_Edad_Codigo)
           .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.RangoEdadCodigoInvalido)
           .MustAsync(async (codigo, _) => await rangoRepository.Existe(codigo))
           .WithMessage(ValidarRegistroExistenteMessages.RangoNoExiste);

        RuleFor(x => x.Tipo_Identificador_Codigo)
           .GreaterThan(0).WithMessage(ValidarRegistroExistenteMessages.TipoIdentificadorCodigoInvalido)
           .MustAsync(async (codigo, _) => await tipoIdentificadorRepository.Existe(codigo))
           .WithMessage(ValidarRegistroExistenteMessages.TipoIdentificadorNoExiste);

        RuleFor(x => x.Animal_Sexo)
           .Cascade(CascadeMode.Stop)
           .NotEmpty().WithMessage(ValidarRegistroExistenteMessages.SexoRequerido)
           .Must(s => s == "M" || s == "H" || s == "Macho" || s == "Hembra").WithMessage(ValidarRegistroExistenteMessages.SexoInvalido);
    }
}

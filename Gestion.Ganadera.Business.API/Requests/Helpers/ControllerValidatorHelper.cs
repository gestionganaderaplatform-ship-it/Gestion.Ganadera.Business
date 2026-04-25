using FluentValidation;
using FluentValidation.Results;
using Gestion.Ganadera.Business.API.Requests.Messages;
using Gestion.Ganadera.Business.Application.Features.Base.Models;

namespace Gestion.Ganadera.Business.API.Requests.Helpers
{
    /// <summary>
    /// Ejecuta validaciones comunes de controllers y devuelve errores listos para ProblemDetails.
    /// </summary>
    public static class ControllerValidatorHelper
    {
        public static async Task<object?> ValidarCodigo(
            IValidator<CodigoRequest> validator,
            string codigo)
        {
            var request = new CodigoRequest { Codigo = codigo };
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors;
            }

            if (!long.TryParse(codigo, out _))
            {
                return RequestMessages.InvalidCode;
            }

            return null;
        }

        public static async Task<IEnumerable<ValidationFailure>?> ValidarEntidad<T>(
            IValidator<T> validator,
            T entidad)
        {
            var validationResult = await validator.ValidateAsync(entidad);

            return validationResult.IsValid
                ? null
                : validationResult.Errors;
        }

        public static async Task<IEnumerable<ValidationFailure>?> ValidarEntidadParcial<T>(
            IValidator<T> validator,
            T entidad,
            ISet<string> propiedadesEnviadas,
            params string[] propiedadesSiempreValidadas)
        {
            var validationContext = new ValidationContext<T>(entidad);
            validationContext.RootContextData["PropertiesSent"] = propiedadesEnviadas;

            var validationResult = await validator.ValidateAsync(validationContext);
            if (validationResult.IsValid)
            {
                return null;
            }

            var propiedadesPermitidas = new HashSet<string>(
                propiedadesEnviadas.Concat(propiedadesSiempreValidadas),
                StringComparer.OrdinalIgnoreCase);

            var errores = validationResult.Errors
                .Where(x =>
                    string.IsNullOrWhiteSpace(x.PropertyName) ||
                    propiedadesPermitidas.Contains(x.PropertyName))
                .ToList();

            return errores.Count == 0 ? null : errores;
        }
    }
}
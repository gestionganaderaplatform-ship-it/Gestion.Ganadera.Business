using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Gestion.Ganadera.Business.API.Requests.Messages;

namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Registra validadores y servicios auxiliares para la validacion uniforme de requests.
    /// </summary>
    public static class ValidationExtensions
    {
        public static IActionResult ForeignKeyDefaultNotFound(
            this Type viewModelType)
        {
            var failure = new ValidationFailure(
                "PropiedadForanea",
                RequestMessages.ForeignKeyDefaultNotFound(viewModelType.Name))
            {
                ErrorCode = "ForeignKeyNotFound",
                FormattedMessagePlaceholderValues = new Dictionary<string, object>
                {
                    { "ViewModel", viewModelType.FullName ?? viewModelType.Name }
                }
            };

            return new BadRequestObjectResult(new[] { failure });
        }
    }
}
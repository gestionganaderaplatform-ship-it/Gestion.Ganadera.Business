using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Gestion.Ganadera.Business.API.ErrorHandling;

namespace Gestion.Ganadera.Business.API.Filters
{
    /// <summary>
    /// Traduce errores de ModelState a ProblemDetails antes de ejecutar la accion.
    /// </summary>
    public sealed class ValidateModelStateFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value!.Errors.Any())
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors
                            .Select(e =>
                                string.IsNullOrWhiteSpace(e.ErrorMessage)
                                    ? "Valor inv�lido."
                                    : e.ErrorMessage
                            )
                            .ToArray()
                    );

                context.Result = ApiProblemDetailsFactory.BadRequest(
                    context.HttpContext,
                    errors
                );

                return;
            }

            await next();
        }
    }
}


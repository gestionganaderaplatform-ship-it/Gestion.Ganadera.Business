using Microsoft.AspNetCore.Mvc;
using Gestion.Ganadera.Business.API.Constants;
using Gestion.Ganadera.Business.API.ErrorHandling.Messages;
using Gestion.Ganadera.Business.API.Extensions;

namespace Gestion.Ganadera.Business.API.ErrorHandling
{
    /// <summary>
    /// Fabrica respuestas ProblemDetails para mantener un formato de error uniforme en toda la API.
    /// </summary>
    public static class ApiProblemDetailsFactory
    {
        public static IActionResult BadRequest(
            HttpContext context,
            object? errors = null,
            string? detail = null)
        {
            return Create(
                context,
                StatusCodes.Status400BadRequest,
                ApiMessages.ValidationFailedTitle,
                detail ?? ApiMessages.ValidationFailedDetail,
                errors
            );
        }

        public static IActionResult NotFound(
            HttpContext context,
            string detail)
        {
            return Create(
                context,
                StatusCodes.Status404NotFound,
                ApiMessages.NotFoundTitle,
                detail
            );
        }

        public static IActionResult InternalError(
            HttpContext context,
            string? detail = null)
        {
            return Create(
                context,
                StatusCodes.Status500InternalServerError,
                ApiMessages.InternalErrorTitle,
                detail ?? ApiMessages.OperationFailed,
                errors: null
            );
        }

        public static ProblemDetails CreateUnauthorized(
            HttpContext context,
            string detail)
        {
            return context.ToProblemDetails(
                StatusCodes.Status401Unauthorized,
                ApiErrorMessages.UnauthorizedDetailTitle,
                detail);
        }

        public static ProblemDetails CreateForbidden(
            HttpContext context,
            string detail)
        {
            return context.ToProblemDetails(
                StatusCodes.Status403Forbidden,
                ApiErrorMessages.ForbiddenTitle,
                detail);
        }

        private static IActionResult Create(
            HttpContext context,
            int statusCode,
            string title,
            string detail,
            object? errors = null)
        {
            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            problem.Extensions["correlationId"] =
                context.Items.TryGetValue("X-Correlation-Id", out var cid) ? cid : null;

            if (errors is not null)
            {
                problem.Extensions["errors"] = errors;
            }

            return new ObjectResult(problem)
            {
                StatusCode = statusCode,
                ContentTypes = { "application/problem+json" }
            };
        }
    }
}
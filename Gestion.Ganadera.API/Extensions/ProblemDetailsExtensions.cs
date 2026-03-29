using Microsoft.AspNetCore.Mvc;

namespace Gestion.Ganadera.API.Extensions
{
    /// <summary>
    /// Construye ProblemDetails enriquecidos con datos transversales como correlation id.
    /// </summary>
    public static class ProblemDetailsExtensions
    {
        public static ProblemDetails ToProblemDetails(
            this HttpContext context,
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

            return problem;
        }
    }
}


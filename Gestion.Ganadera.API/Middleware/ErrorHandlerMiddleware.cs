using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gestion.Ganadera.API.ErrorHandling.Messages;
using Gestion.Ganadera.API.Extensions;

namespace Gestion.Ganadera.API.Middleware
{
    /// <summary>
    /// Captura excepciones no controladas y las transforma en respuestas ProblemDetails.
    /// </summary>
    public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, IWebHostEnvironment env)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger = logger;
        private readonly IWebHostEnvironment _env = env;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(
                 error,
                 "Unhandled exception | Method: {Method} | Path: {Path} | StatusCode: {StatusCode}",
                 context.Request.Method,
                 context.Request.Path,
                 context.Response?.StatusCode
                );

                var response = context.Response;
                response!.ContentType = "application/json";

                var statusCode = error switch
                {
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    ValidationException => (int)HttpStatusCode.BadRequest,
                    BadHttpRequestException or JsonException => (int)HttpStatusCode.BadRequest,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var title = error switch
                {
                    ValidationException => ApiErrorMessages.ValidationFailedTitle,
                    BadHttpRequestException or JsonException => ApiErrorMessages.InvalidJsonTitle,
                    UnauthorizedAccessException => ApiErrorMessages.UnauthorizedTitle,
                    KeyNotFoundException => ApiErrorMessages.NotFoundTitle,
                    _ => ApiErrorMessages.InternalErrorTitle
                };

                var problem = context.ToProblemDetails(
                       statusCode,
                       title,
                       _env.IsDevelopment() ? error.Message : ApiErrorMessages.UnexpectedErrorDetail
                 );

                context.Response!.StatusCode = statusCode;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(problem);
            }
        }
    }
}
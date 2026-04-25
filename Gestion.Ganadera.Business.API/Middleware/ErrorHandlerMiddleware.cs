using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Gestion.Ganadera.Business.API.ErrorHandling.Messages;
using Gestion.Ganadera.Business.API.Extensions;

namespace Gestion.Ganadera.Business.API.Middleware
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
                if (IsRequestCancellation(error, context.RequestAborted))
                {
                    _logger.LogInformation(
                        error,
                        "Request canceled by client | Method: {Method} | Path: {Path}",
                        context.Request.Method,
                        context.Request.Path);
                    return;
                }

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

                var detail = _env.IsDevelopment()
                    ? error.Message
                    : error switch
                    {
                        ValidationException => ApiErrorMessages.ValidationFailedDetail,
                        BadHttpRequestException or JsonException => ApiErrorMessages.InvalidJsonTitle,
                        UnauthorizedAccessException => ApiErrorMessages.UnauthorizedTitle,
                        KeyNotFoundException => ApiErrorMessages.RequestedRecordNotFound,
                        _ => ApiErrorMessages.UnexpectedErrorDetail
                    };

                var validationErrors = error is ValidationException validationException
                    ? validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage })
                    : null;

                var problem = context.ToProblemDetails(
                    statusCode,
                    title,
                    detail,
                    validationErrors
                );

                context.Response!.StatusCode = statusCode;
                context.Response.ContentType = "application/problem+json";

                await context.Response.WriteAsJsonAsync(problem);
            }
        }

        private static bool IsRequestCancellation(Exception error, CancellationToken requestAborted)
        {
            return error is OperationCanceledException or TaskCanceledException
                && requestAborted.IsCancellationRequested;
        }
    }
}

namespace Gestion.Ganadera.API.Middleware
{
    /// <summary>
    /// Genera o propaga el correlation id para enlazar logs, errores y metricas del mismo request.
    /// </summary>
    public class CorrelationIdMiddleware(RequestDelegate next)
    {
        private const string HeaderName = "X-Correlation-Id";
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            var correlationId =
                context.Request.Headers.TryGetValue(HeaderName, out var value)
                    ? value.ToString()
                    : Guid.NewGuid().ToString();

            context.Items[HeaderName] = correlationId;
            context.Response.Headers[HeaderName] = correlationId;

            using (Serilog.Context.LogContext.PushProperty("CorrelationId", correlationId))
            {
                await _next(context);
            }
        }
    }
}


namespace Gestion.Ganadera.Business.API.Middleware
{
    /// <summary>
    /// Agrega encabezados HTTP defensivos para endurecer la exposicion publica del API.
    /// </summary>
    public class SecurityHeadersMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        private readonly RequestDelegate _next = next;
        private readonly IConfiguration _configuration = configuration;


        public async Task Invoke(HttpContext context)
        {

            if (!_configuration.GetValue<bool>("Security:EnableSecurityHeaders"))
            {
                await _next(context);
                return;
            }

            // Headers defensivos básicos
            context.Response.Headers.XContentTypeOptions = "nosniff";
            context.Response.Headers.XFrameOptions = "DENY";
            context.Response.Headers["Referrer-Policy"] = "no-referrer";
            context.Response.Headers.XXSSProtection = "0";

            // HSTS solo debe enviarse sobre HTTPS.
            if (context.Request.IsHttps)
            {
                var hstsDays = _configuration.GetValue<int>("Security:HstsDays", 365);
                var maxAgeSeconds = hstsDays * 24 * 60 * 60;

                context.Response.Headers.StrictTransportSecurity =
                    $"max-age={maxAgeSeconds}; includeSubDomains";
            }

            // Ocultar headers innecesarios
            context.Response.Headers.Remove("Server");
            context.Response.Headers.Remove("X-Powered-By");

            await _next(context);
        }
    }
}


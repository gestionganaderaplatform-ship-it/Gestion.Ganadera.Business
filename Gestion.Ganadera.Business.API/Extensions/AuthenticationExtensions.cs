using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Gestion.Ganadera.Business.API.ErrorHandling.Messages;

namespace Gestion.Ganadera.Business.API.Extensions
{
    public static class AuthenticationExtensions
    {
        public static WebApplicationBuilder AddJwtAuthentication(
            this WebApplicationBuilder builder)
        {
            var jwtEnabled = builder.Configuration.GetValue<bool>("Jwt:Enabled");

            if (!jwtEnabled)
            {
                builder.Services.AddAuthentication();
                return builder;
            }

            var jwt = builder.Configuration.GetSection("Jwt");
            var signingKey = jwt["SigningKey"]
                ?? throw new InvalidOperationException("Jwt:SigningKey no esta configurado.");

            if (string.IsNullOrWhiteSpace(signingKey))
            {
                throw new InvalidOperationException("Jwt:SigningKey no puede estar vacio.");
            }

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(signingKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();

                            var httpContext = context.HttpContext;
                            var problem = new ProblemDetails
                            {
                                Status = StatusCodes.Status401Unauthorized,
                                Title = ApiErrorMessages.UnauthorizedTitle,
                                Detail = "Token requerido o invalido.",
                                Instance = httpContext.Request.Path
                            };

                            problem.Extensions["correlationId"] =
                                httpContext.Items.TryGetValue("X-Correlation-Id", out var cid)
                                    ? cid
                                    : null;

                            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            httpContext.Response.ContentType = "application/problem+json";

                            await httpContext.Response.WriteAsJsonAsync(problem);
                        },
                        OnForbidden = async context =>
                        {
                            var httpContext = context.HttpContext;
                            var problem = new ProblemDetails
                            {
                                Status = StatusCodes.Status403Forbidden,
                                Title = ApiErrorMessages.ForbiddenTitle,
                                Detail = "No tiene permisos para acceder a este recurso.",
                                Instance = httpContext.Request.Path
                            };

                            problem.Extensions["correlationId"] =
                                httpContext.Items.TryGetValue("X-Correlation-Id", out var cid)
                                    ? cid
                                    : null;

                            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                            httpContext.Response.ContentType = "application/problem+json";

                            await httpContext.Response.WriteAsJsonAsync(problem);
                        }
                    };
                });

            return builder;
        }
    }
}

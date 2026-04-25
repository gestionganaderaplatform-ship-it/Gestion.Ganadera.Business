using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.Interfaces;

namespace Gestion.Ganadera.Business.API.Middleware;

public sealed class GanaderiaCatalogBootstrapMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(
        HttpContext context,
        ICurrentClientProvider currentClientProvider,
        IGanaderiaCatalogBootstrapService ganaderiaCatalogBootstrapService)
    {
        if (context.User.Identity?.IsAuthenticated == true &&
            currentClientProvider.ClientNumericId.HasValue)
        {
            await ganaderiaCatalogBootstrapService.EnsureCatalogosBaseAsync(context.RequestAborted);
        }

        await _next(context);
    }
}

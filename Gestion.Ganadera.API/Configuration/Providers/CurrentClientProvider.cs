using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Configuration.Providers
{
    public sealed class CurrentClientProvider(IHttpContextAccessor httpContextAccessor)
        : ICurrentClientProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public long? ClientNumericId
        {
            get
            {
                var value = GetClaimValue("cliente_codigo");
                return long.TryParse(value, out var numericId)
                    ? numericId
                    : null;
            }
        }

        public string? ClientId => GetClaimValue("cliente_codigo_publico", "cliente_codigo");

        private string? GetClaimValue(params string[] claimTypes)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated != true)
            {
                return null;
            }

            foreach (var claimType in claimTypes)
            {
                var value = user.Claims
                    .FirstOrDefault(claim =>
                        string.Equals(claim.Type, claimType, StringComparison.OrdinalIgnoreCase))
                    ?.Value;

                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value;
                }
            }

            return null;
        }
    }
}

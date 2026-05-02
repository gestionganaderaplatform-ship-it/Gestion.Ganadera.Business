using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.Business.API.Configuration.Providers
{
    public sealed class CurrentClientProvider(IHttpContextAccessor httpContextAccessor)
        : ICurrentClientProvider
    {
        private static readonly string[] PreferredTextualClientClaims =
        [
            "cliente_codigo_publico",
            "cliente_codigo",
            "client_id"
        ];

        private static readonly string[] PreferredNumericClientClaims =
        [
            "cliente_codigo",
            "client_id"
        ];

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public long? ClientNumericId
        {
            get
            {
                var value = GetClaimValue(PreferredNumericClientClaims);
                return long.TryParse(value, out var numericId)
                    ? numericId
                    : null;
            }
        }

        public string? ClientId => GetClaimValue(PreferredTextualClientClaims);

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

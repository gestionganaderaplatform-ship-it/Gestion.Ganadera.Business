using System.Security.Claims;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.API.Configuration.Providers
{
    /// <summary>
    /// Resuelve el actor actual a partir de claims comunes del token o de la identidad del request.
    /// </summary>
    public sealed class CurrentActorProvider(IHttpContextAccessor httpContextAccessor) : ICurrentActorProvider
    {
        private static readonly string[] PreferredActorClaims =
        [
            ClaimTypes.NameIdentifier,
            "sub",
            "preferred_username",
            ClaimTypes.Email,
            "email"
        ];

        private static readonly string[] PreferredNumericActorClaims =
        [
            ClaimTypes.NameIdentifier,
            "sub",
            "uid",
            "user_id"
        ];

        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? ActorId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated != true)
                {
                    return null;
                }

                foreach (var claimType in PreferredActorClaims)
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

                return string.IsNullOrWhiteSpace(user.Identity?.Name)
                    ? null
                    : user.Identity.Name;
            }
        }

        public long? ActorNumericId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                if (user?.Identity?.IsAuthenticated != true)
                {
                    return null;
                }

                foreach (var claimType in PreferredNumericActorClaims)
                {
                    var value = user.Claims
                        .FirstOrDefault(claim =>
                            string.Equals(claim.Type, claimType, StringComparison.OrdinalIgnoreCase))
                        ?.Value;

                    if (long.TryParse(value, out var numericId))
                    {
                        return numericId;
                    }
                }

                return null;
            }
        }
    }
}

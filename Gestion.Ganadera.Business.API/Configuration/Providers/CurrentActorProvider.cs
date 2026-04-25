using System.Security.Claims;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.Business.API.Configuration.Providers
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
                var user = ResolveAuthenticatedUser();
                if (user is null)
                {
                    return null;
                }

                foreach (var claimType in PreferredActorClaims)
                {
                    var value = ResolveClaimValue(user, claimType);

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

        public string? ActorEmail
        {
            get
            {
                var user = ResolveAuthenticatedUser();
                if (user is null)
                {
                    return null;
                }

                return ResolveClaimValue(user, ClaimTypes.Email)
                    ?? ResolveClaimValue(user, "email");
            }
        }

        public long? ActorNumericId
        {
            get
            {
                var user = ResolveAuthenticatedUser();
                if (user is null)
                {
                    return null;
                }

                foreach (var claimType in PreferredNumericActorClaims)
                {
                    var value = ResolveClaimValue(user, claimType);

                    if (long.TryParse(value, out var numericId))
                    {
                        return numericId;
                    }
                }

                return null;
            }
        }

        private ClaimsPrincipal? ResolveAuthenticatedUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity?.IsAuthenticated == true
                ? user
                : null;
        }

        private static string? ResolveClaimValue(ClaimsPrincipal user, string claimType)
        {
            return user.Claims
                .FirstOrDefault(claim =>
                    string.Equals(claim.Type, claimType, StringComparison.OrdinalIgnoreCase))
                ?.Value;
        }
    }
}

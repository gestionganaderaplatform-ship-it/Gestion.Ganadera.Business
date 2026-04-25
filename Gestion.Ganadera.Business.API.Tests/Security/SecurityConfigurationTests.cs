using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Gestion.Ganadera.Business.API.Extensions;
using Gestion.Ganadera.Business.API.Security.Permissions;
using Xunit;

namespace Gestion.Ganadera.Business.API.Tests.Security
{
    public class SecurityConfigurationTests
    {
        [Fact]
        public void AddJwtAuthentication_DoesNotThrow_WhenJwtIsDisabled()
        {
            var builder = CreateBuilder(new Dictionary<string, string?>
            {
                ["Jwt:Enabled"] = "false"
            });

            var exception = Record.Exception(() => builder.AddJwtAuthentication());

            Assert.Null(exception);
        }

        [Fact]
        public void AddJwtAuthentication_Throws_WhenJwtIsEnabledWithoutSigningKey()
        {
            var builder = CreateBuilder(new Dictionary<string, string?>
            {
                ["Jwt:Enabled"] = "true",
                ["Jwt:Issuer"] = "issuer",
                ["Jwt:Audience"] = "audience",
                ["Jwt:SigningKey"] = ""
            });

            var exception = Assert.Throws<InvalidOperationException>(() => builder.AddJwtAuthentication());

            Assert.Equal("Jwt:SigningKey no puede estar vacio.", exception.Message);
        }

        [Fact]
        public async Task PermissionAuthorizationHandler_DoesNotSucceed_WhenPrincipalIsAnonymous()
        {
            var requirement = new PermissionAuthorizationRequirement(ControllerPermission.GetAll);
            var handler = new PermissionAuthorizationHandler();
            var context = new AuthorizationHandlerContext([requirement], new ClaimsPrincipal(), resource: null);

            await handler.HandleAsync(context);

            Assert.False(context.HasSucceeded);
        }

        [Fact]
        public async Task PermissionAuthorizationHandler_Succeeds_WhenPermissionClaimMatches()
        {
            var requirement = new PermissionAuthorizationRequirement(ControllerPermission.GetAll);
            var handler = new PermissionAuthorizationHandler();
            var identity = new ClaimsIdentity(
            [
                new Claim(PermissionPolicy.ClaimType, "GetAll,Create")
            ], authenticationType: "Bearer");

            var context = new AuthorizationHandlerContext(
                [requirement],
                new ClaimsPrincipal(identity),
                resource: null);

            await handler.HandleAsync(context);

            Assert.True(context.HasSucceeded);
        }

        [Fact]
        public async Task PermissionAuthorizationHandler_Succeeds_WhenIdentityIsAuthenticatedWithoutPermissionClaims()
        {
            var requirement = new PermissionAuthorizationRequirement(ControllerPermission.GetPaged);
            var handler = new PermissionAuthorizationHandler();
            var identity = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, "usuario-123")
            ], authenticationType: "Bearer");

            var context = new AuthorizationHandlerContext(
                [requirement],
                new ClaimsPrincipal(identity),
                resource: null);

            await handler.HandleAsync(context);

            Assert.True(context.HasSucceeded);
        }

        [Fact]
        public async Task PermissionAuthorizationHandler_DoesNotSucceed_WhenIdentityIsUnauthenticated()
        {
            var requirement = new PermissionAuthorizationRequirement(ControllerPermission.GetAll);
            var handler = new PermissionAuthorizationHandler();
            var context = new AuthorizationHandlerContext(
                [requirement],
                new ClaimsPrincipal(new ClaimsIdentity()),
                resource: null);

            await handler.HandleAsync(context);

            Assert.False(context.HasSucceeded);
        }

        private static WebApplicationBuilder CreateBuilder(Dictionary<string, string?> values)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Configuration.AddInMemoryCollection(values);
            return builder;
        }
    }
}

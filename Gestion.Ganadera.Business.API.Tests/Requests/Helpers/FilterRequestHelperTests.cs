using System.Text.Json;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Requests.Messages;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.ViewModels;
using Xunit;

namespace Gestion.Ganadera.Business.API.Tests.Requests.Helpers
{
    public class FilterRequestHelperTests
    {
        [Fact]
        public void TryPreparar_ReturnsFalse_WhenBodyDoesNotContainKnownProperties()
        {
            var body = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase)
            {
                ["PropiedadInexistente"] = CreateJsonElement(1)
            };

            var result = FilterRequestHelper.TryPreparar<AuditoriaViewModel>(
                body,
                out var entidad,
                out var propiedadesEnviadas,
                out var filtros,
                out var error);

            Assert.False(result);
            Assert.Null(entidad);
            Assert.Empty(propiedadesEnviadas);
            Assert.Empty(filtros);
            Assert.Equal(RequestMessages.FilterRequiresOneValidProperty, error);
        }

        [Fact]
        public void TryPreparar_AllowsFilteringByCodeProperty()
        {
            var body = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase)
            {
                ["auditoria_Codigo"] = CreateJsonElement(1)
            };

            var result = FilterRequestHelper.TryPreparar<AuditoriaViewModel>(
                body,
                out var entidad,
                out var propiedadesEnviadas,
                out var filtros,
                out var error);

            Assert.True(result);
            Assert.NotNull(entidad);
            Assert.Equal(1, entidad.Auditoria_Codigo);
            Assert.Contains(nameof(AuditoriaViewModel.Auditoria_Codigo), propiedadesEnviadas);
            Assert.Equal(1L, Assert.IsType<long>(filtros[nameof(AuditoriaViewModel.Auditoria_Codigo)]));
            Assert.Null(error);
        }

        private static JsonElement CreateJsonElement<T>(T value)
        {
            return JsonSerializer.SerializeToElement(value);
        }
    }
}

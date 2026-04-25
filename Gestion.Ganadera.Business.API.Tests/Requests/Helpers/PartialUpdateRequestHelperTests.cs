using System.Text.Json;
using Gestion.Ganadera.Business.API.Requests.Helpers;
using Gestion.Ganadera.Business.API.Requests.Messages;
using Xunit;

namespace Gestion.Ganadera.Business.API.Tests.Requests.Helpers
{
    public class PartialUpdateRequestHelperTests
    {
        [Fact]
        public void TryPreparar_ReturnsFalse_WhenBodyIsEmpty()
        {
            var result = PartialUpdateRequestHelper.TryPreparar<TestUpdateViewModel>(
                [],
                25,
                out var entidad,
                out var propiedadesEnviadas,
                out var propiedadCodigo,
                out var error);

            Assert.False(result);
            Assert.Null(entidad);
            Assert.Empty(propiedadesEnviadas);
            Assert.Null(propiedadCodigo);
            Assert.Equal(RequestMessages.InvalidPatchBody, error);
        }

        [Fact]
        public void TryPreparar_ReturnsFalse_WhenBodyIncludesCodeProperty()
        {
            var body = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase)
            {
                ["Codigo"] = CreateJsonElement(99),
                ["Nombre"] = CreateJsonElement("Monitor")
            };

            var result = PartialUpdateRequestHelper.TryPreparar<TestUpdateViewModel>(
                body,
                25,
                out var entidad,
                out var propiedadesEnviadas,
                out var propiedadCodigo,
                out var error);

            Assert.False(result);
            Assert.Null(entidad);
            Assert.Empty(propiedadesEnviadas);
            Assert.Null(propiedadCodigo);
            Assert.Equal(RequestMessages.PatchCodeMustBeSentInRoute, error);
        }

        [Fact]
        public void TryPreparar_AssignsRouteCode_WhenBodyContainsOnlyPatchableProperties()
        {
            var body = new Dictionary<string, JsonElement>(StringComparer.OrdinalIgnoreCase)
            {
                ["Nombre"] = CreateJsonElement("Monitor"),
                ["Activo"] = CreateJsonElement(true)
            };

            var result = PartialUpdateRequestHelper.TryPreparar<TestUpdateViewModel>(
                body,
                25,
                out var entidad,
                out var propiedadesEnviadas,
                out var propiedadCodigo,
                out var error);

            Assert.True(result);
            Assert.NotNull(entidad);
            Assert.Equal(25, entidad.Codigo);
            Assert.Equal("Monitor", entidad.Nombre);
            Assert.True(entidad.Activo);
            Assert.Equal(nameof(TestUpdateViewModel.Codigo), propiedadCodigo);
            Assert.Contains(nameof(TestUpdateViewModel.Nombre), propiedadesEnviadas);
            Assert.Contains(nameof(TestUpdateViewModel.Activo), propiedadesEnviadas);
            Assert.Null(error);
        }

        private sealed class TestUpdateViewModel
        {
            public long Codigo { get; set; }
            public string? Nombre { get; set; }
            public bool Activo { get; set; }
        }

        private static JsonElement CreateJsonElement<T>(T value)
        {
            return JsonSerializer.SerializeToElement(value);
        }
    }
}

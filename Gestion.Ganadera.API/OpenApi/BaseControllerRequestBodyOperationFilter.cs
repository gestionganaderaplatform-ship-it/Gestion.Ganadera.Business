using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Gestion.Ganadera.API.Requests.Helpers;

namespace Gestion.Ganadera.API.OpenApi
{
    /// <summary>
    /// Reemplaza el schema generico de Dictionary para PATCH y filtrado
    /// por el modelo real de la feature en controllers base cerrados.
    /// </summary>
    public sealed class BaseControllerRequestBodyOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody?.Content is null || context.MethodInfo.DeclaringType is null)
            {
                return;
            }

            if (!TryGetBaseControllerGenericArguments(
                context.MethodInfo.DeclaringType,
                out var viewModelType,
                out var updateViewModelType))
            {
                return;
            }

            if (string.Equals(context.MethodInfo.Name, "ActualizarParcial", StringComparison.Ordinal))
            {
                ApplySchema(
                    operation,
                    BuildOptionalObjectSchema(context, updateViewModelType, excludeCodeProperty: true));
            }

            if (string.Equals(context.MethodInfo.Name, "Filtrar", StringComparison.Ordinal) ||
                string.Equals(context.MethodInfo.Name, "FiltrarPaginado", StringComparison.Ordinal))
            {
                ApplySchema(
                    operation,
                    BuildOptionalObjectSchema(context, viewModelType, excludeCodeProperty: false));
            }
        }

        private static void ApplySchema(OpenApiOperation operation, IOpenApiSchema schema)
        {
            var requestBody = operation.RequestBody;
            if (requestBody?.Content is null ||
                !requestBody.Content.TryGetValue("application/json", out var mediaType))
            {
                return;
            }

            mediaType.Schema = schema;
        }

        private static IOpenApiSchema BuildOptionalObjectSchema(
            OperationFilterContext context,
            Type modelType,
            bool excludeCodeProperty)
        {
            var schema = context.SchemaGenerator.GenerateSchema(modelType, context.SchemaRepository);

            foreach (var property in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (excludeCodeProperty &&
                    property.CanRead &&
                    PartialUpdateRequestHelper.EsPropiedadCodigo(property))
                {
                    schema.Properties?.Remove(property.Name);
                    schema.Required?.Remove(property.Name);
                }
            }

            return schema;
        }

        private static bool TryGetBaseControllerGenericArguments(
            Type declaringType,
            out Type viewModelType,
            out Type updateViewModelType)
        {
            viewModelType = null!;
            updateViewModelType = null!;

            var currentType = declaringType;

            while (currentType is not null)
            {
                if (currentType.IsGenericType &&
                    currentType.GetGenericTypeDefinition().Name == "BaseController`3")
                {
                    var args = currentType.GetGenericArguments();
                    viewModelType = args[0];
                    updateViewModelType = args[2];
                    return true;
                }

                currentType = currentType.BaseType;
            }

            return false;
        }
    }
}

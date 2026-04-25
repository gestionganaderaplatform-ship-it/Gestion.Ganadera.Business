using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using Gestion.Ganadera.Business.API.Requests.Messages;

namespace Gestion.Ganadera.Business.API.Requests.Helpers
{
    /// <summary>
    /// Convierte y normaliza requests PATCH sin mezclar parsing JSON dentro del controller.
    /// </summary>
    public static class PartialUpdateRequestHelper
    {
        public static bool TryPreparar<TUpdateViewModel>(
            Dictionary<string, JsonElement>? body,
            long codigo,
            [NotNullWhen(true)] out TUpdateViewModel? entidad,
            out HashSet<string> propiedadesEnviadas,
            [NotNullWhen(true)] out string? propiedadCodigo,
            [NotNullWhen(false)] out string? error)
        {
            entidad = default;
            propiedadesEnviadas = [];
            propiedadCodigo = null;
            error = null;

            if (body is null || body.Count == 0)
            {
                error = RequestMessages.InvalidPatchBody;
                return false;
            }

            if (IncluyePropiedadCodigo<TUpdateViewModel>(body))
            {
                error = RequestMessages.PatchCodeMustBeSentInRoute;
                return false;
            }

            propiedadesEnviadas = ObtenerPropiedadesEnviadas<TUpdateViewModel>(body);
            if (propiedadesEnviadas.Count == 0)
            {
                error = RequestMessages.PatchRequiresOneProperty;
                return false;
            }

            entidad = Deserializar<TUpdateViewModel>(body);
            if (entidad is null)
            {
                error = RequestMessages.PatchBodyCouldNotBeParsed;
                return false;
            }

            propiedadCodigo = AsignarCodigo(entidad, codigo);
            if (propiedadCodigo is null)
            {
                error = RequestMessages.PatchCodePropertyNotFound;
                return false;
            }

            return true;
        }

        public static HashSet<string> ObtenerPropiedadesEnviadas<TUpdateViewModel>(Dictionary<string, JsonElement> body)
        {
            var propiedades = typeof(TUpdateViewModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanWrite && !EsPropiedadCodigo(x))
                .ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            var enviadas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var propiedad in body)
            {
                if (propiedades.TryGetValue(propiedad.Key, out var nombreReal))
                {
                    enviadas.Add(nombreReal.Name);
                }
            }

            return enviadas;
        }

        public static bool IncluyePropiedadCodigo<TUpdateViewModel>(Dictionary<string, JsonElement> body)
        {
            var propiedadesCodigo = typeof(TUpdateViewModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(EsPropiedadCodigo)
                .Select(property => property.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            return body.Keys.Any(propiedad => propiedadesCodigo.Contains(propiedad));
        }

        public static bool EsPropiedadCodigo(PropertyInfo propertyInfo)
        {
            var propertyType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;

            return propertyInfo.CanWrite &&
                   propertyType == typeof(long) &&
                   (string.Equals(propertyInfo.Name, "Codigo", StringComparison.OrdinalIgnoreCase) ||
                    propertyInfo.Name.EndsWith("_Codigo", StringComparison.OrdinalIgnoreCase));
        }

        public static TUpdateViewModel? Deserializar<TUpdateViewModel>(Dictionary<string, JsonElement> body)
        {
            return JsonSerializer.Deserialize<TUpdateViewModel>(
                JsonSerializer.Serialize(body),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static string? AsignarCodigo<TUpdateViewModel>(TUpdateViewModel entidad, long codigo)
        {
            var propiedadCodigo = typeof(TUpdateViewModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(EsPropiedadCodigo);

            if (propiedadCodigo is null)
            {
                return null;
            }

            propiedadCodigo.SetValue(entidad, codigo);
            return propiedadCodigo.Name;
        }
    }
}

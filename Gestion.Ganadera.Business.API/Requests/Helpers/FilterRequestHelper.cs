using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using Gestion.Ganadera.Business.API.Requests.Messages;

namespace Gestion.Ganadera.Business.API.Requests.Helpers
{
    /// <summary>
    /// Normaliza requests de filtrado preservando valores como false y 0.
    /// </summary>
    public static class FilterRequestHelper
    {
        public static bool TryPreparar<TViewModel>(
            Dictionary<string, JsonElement>? body,
            [NotNullWhen(true)] out TViewModel? entidad,
            out HashSet<string> propiedadesEnviadas,
            out Dictionary<string, object> filtros,
            [NotNullWhen(false)] out string? error)
        {
            entidad = default;
            propiedadesEnviadas = [];
            filtros = [];
            error = null;

            if (body is null || body.Count == 0)
            {
                error = RequestMessages.InvalidFilterBody;
                return false;
            }

            propiedadesEnviadas = ObtenerPropiedadesEnviadas<TViewModel>(body);
            if (propiedadesEnviadas.Count == 0)
            {
                error = RequestMessages.FilterRequiresOneValidProperty;
                return false;
            }

            entidad = Deserializar<TViewModel>(body);
            if (entidad is null)
            {
                error = RequestMessages.FilterBodyCouldNotBeParsed;
                return false;
            }

            filtros = ConstruirFiltros(entidad, propiedadesEnviadas);
            if (filtros.Count == 0)
            {
                error = RequestMessages.FilterRequiresOneUsefulValue;
                return false;
            }

            return true;
        }

        public static HashSet<string> ObtenerPropiedadesEnviadas<TViewModel>(Dictionary<string, JsonElement> body)
        {
            var propiedades = typeof(TViewModel)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanWrite)
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

        public static TViewModel? Deserializar<TViewModel>(Dictionary<string, JsonElement> body)
        {
            return JsonSerializer.Deserialize<TViewModel>(
                JsonSerializer.Serialize(body),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public static Dictionary<string, object> ConstruirFiltros<TViewModel>(
            TViewModel entidad,
            ISet<string> propiedadesEnviadas)
        {
            var filtros = new Dictionary<string, object>();

            foreach (var propiedad in typeof(TViewModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (!propiedad.CanRead || !propiedadesEnviadas.Contains(propiedad.Name))
                {
                    continue;
                }

                var valor = propiedad.GetValue(entidad);
                if (valor is null)
                {
                    continue;
                }

                if (valor is string texto && string.IsNullOrWhiteSpace(texto))
                {
                    continue;
                }

                filtros[propiedad.Name] = valor;
            }

            return filtros;
        }
    }
}

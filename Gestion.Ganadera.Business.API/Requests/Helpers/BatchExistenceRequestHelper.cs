using Gestion.Ganadera.Business.API.Requests.Messages;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;

namespace Gestion.Ganadera.Business.API.Requests.Helpers
{
    /// <summary>
    /// Centraliza las reglas estructurales del endpoint existen-varios para llaves primarias long.
    /// </summary>
    public static class BatchExistenceRequestHelper
    {
        public static string? ValidarPropiedadClave<TViewModel>(
            IEntitySchemaMetadata entitySchemaMetadata,
            string propiedadClave)
        {
            if (!entitySchemaMetadata.PropertyExists<TViewModel>(propiedadClave))
            {
                return RequestMessages.PropertyNotFound(propiedadClave);
            }

            if (!entitySchemaMetadata.IsPrimaryKey<TViewModel>(propiedadClave))
            {
                return RequestMessages.PropertyMustBePrimaryKey(propiedadClave);
            }

            if (!entitySchemaMetadata.IsLongProperty<TViewModel>(propiedadClave))
            {
                return RequestMessages.PropertyMustBeLong(propiedadClave);
            }

            return null;
        }

        public static List<long> NormalizarCodigos(IEnumerable<string> codigos)
        {
            return codigos
                .Select(c => c.Trim())
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .Select(c => long.TryParse(c, out var numero) ? numero : (long?)null)
                .Where(c => c.HasValue)
                .Select(c => c!.Value)
                .ToList();
        }
    }
}
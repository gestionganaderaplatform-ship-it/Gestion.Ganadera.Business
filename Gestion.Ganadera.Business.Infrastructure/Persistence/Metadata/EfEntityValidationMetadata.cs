using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Abstractions.Model;
using Gestion.Ganadera.Business.Infrastructure.Persistence;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Metadata
{
    /// <summary>
    /// Proyecta restricciones del modelo EF Core a reglas reutilizables de validacion.
    /// </summary>
    public sealed class EfEntityValidationMetadata(AppDbContext context) : IEntityValidationMetadata
    {
        private readonly AppDbContext _context = context;
        private readonly Dictionary<Type, List<PropertyValidationRule>> _cache = [];

        public IReadOnlyList<PropertyValidationRule> GetRules<T>()
        {
            var modelType = typeof(T);
            var entityType = ResolveEntityType(modelType);

            if (_cache.TryGetValue(modelType, out var cached))
            {
                return cached;
            }

            var efEntity = _context.Model.FindEntityType(entityType);
            if (efEntity == null)
            {
                return [];
            }

            var rules = new List<PropertyValidationRule>();

            foreach (var prop in efEntity.GetProperties())
            {
                var modelProp = modelType.GetProperty(prop.Name);
                if (modelProp == null)
                {
                    continue;
                }

                object? getter(object model) => modelProp.GetValue(model);

                rules.Add(new PropertyValidationRule
                {
                    Getter = getter,
                    PropertyName = modelProp.Name,
                    Required = !prop.IsNullable,
                    MaxLength = prop.GetMaxLength()
                });
            }

            _cache[modelType] = rules;
            return rules;
        }

        private Type ResolveEntityType(Type modelType)
        {
            var mapInterface = modelType
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == typeof(IMapsToEntity<>));

            return mapInterface?.GetGenericArguments()[0] ?? modelType;
        }
    }
}

using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Infrastructure.Persistence;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Metadata
{
    /// <summary>
    /// Consulta metadata estructural desde EF Core para respaldar validaciones genericas del template.
    /// </summary>
    public sealed class EfEntitySchemaMetadata(AppDbContext context) : IEntitySchemaMetadata
    {
        private readonly AppDbContext _context = context;

        public bool PropertyExists<T>(string propertyName)
        {
            var property = ResolveProperty(typeof(T), propertyName);
            return property is not null;
        }

        public bool IsPrimaryKey<T>(string propertyName)
        {
            var property = ResolveProperty(typeof(T), propertyName);
            return property?.IsPrimaryKey() ?? false;
        }

        public bool IsLongProperty<T>(string propertyName)
        {
            var property = ResolveProperty(typeof(T), propertyName);
            if (property is null)
            {
                return false;
            }

            var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;
            return propertyType == typeof(long);
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

        private Microsoft.EntityFrameworkCore.Metadata.IProperty? ResolveProperty(Type modelType, string propertyName)
        {
            var entityType = ResolveEntityType(modelType);
            var efEntity = _context.Model.FindEntityType(entityType);
            return efEntity?.FindProperty(propertyName);
        }
    }
}

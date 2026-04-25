namespace Gestion.Ganadera.Business.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone metadata estructural de entidades para validaciones transversales del template.
    /// </summary>
    public interface IEntitySchemaMetadata
    {
        bool PropertyExists<T>(string propertyName);
        bool IsPrimaryKey<T>(string propertyName);
        bool IsLongProperty<T>(string propertyName);
    }
}

using Gestion.Ganadera.Application.Abstractions.Model;

namespace Gestion.Ganadera.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone reglas estructurales derivadas del modelo persistido para validaciones genericas.
    /// </summary>
    public interface IEntityValidationMetadata
    {
        IReadOnlyList<PropertyValidationRule> GetRules<T>();
    }
}


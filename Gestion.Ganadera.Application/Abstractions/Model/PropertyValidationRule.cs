namespace Gestion.Ganadera.Application.Abstractions.Model
{
    /// <summary>
/// Describe una regla de validacion generica obtenida desde metadata del modelo.
/// </summary>
public sealed class PropertyValidationRule
    {
        public Func<object, object?> Getter { get; init; } = default!;
        public string PropertyName { get; init; } = string.Empty;
        public bool Required { get; init; }
        public int? MaxLength { get; init; }
    }
}

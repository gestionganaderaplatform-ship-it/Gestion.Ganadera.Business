using FluentValidation;
using Gestion.Ganadera.Application.Abstractions.Interfaces;

/// <summary>
/// Base de validacion que combina reglas explicitas con metadata estructural del modelo.
/// </summary>
public abstract class StandardEntityValidator<T> : AbstractValidator<T>
{
    protected StandardEntityValidator(
        IEntityValidationMetadata metadata,
        Func<T, ISet<string>>? propertiesProvider = null)
    {
        var rules = metadata.GetRules<T>();

        RuleFor(x => x).Custom((instance, context) =>
        {
            ISet<string>? propertiesSent = null;

            if (context.RootContextData.TryGetValue("PropertiesSent", out var rawProperties) &&
                rawProperties is ISet<string> rootProperties)
            {
                propertiesSent = rootProperties;
            }
            else
            {
                propertiesSent = propertiesProvider?.Invoke(instance);
            }

            foreach (var rule in rules)
            {
                if (propertiesSent is not null &&
                    !propertiesSent.Contains(rule.PropertyName))
                {
                    continue;
                }

                var value = rule.Getter(instance!);

                if (rule.Required && value is string s && string.IsNullOrWhiteSpace(s))
                {
                    context.AddFailure(rule.PropertyName, "El campo es obligatorio.");
                    continue;
                }

                if (rule.MaxLength.HasValue && value is string str && str.Length > rule.MaxLength.Value)
                {
                    context.AddFailure(
                        rule.PropertyName,
                        $"La longitud máxima permitida es {rule.MaxLength.Value} caracteres.");
                }
            }
        });
    }
}


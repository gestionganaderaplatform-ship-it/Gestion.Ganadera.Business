namespace Gestion.Ganadera.Application.Common.Extensions
{
    /// <summary>
    /// Convierte objetos simples en diccionarios de filtro para busquedas genericas del template.
    /// </summary>
    public static class FilterExtensions
    {
        public static Dictionary<string, object> ToFilterDictionary<T>(this T entity)
        {
            var filters = new Dictionary<string, object>();

            foreach (var property in typeof(T).GetProperties())
            {
                var value = property.GetValue(entity);

                if (value != null && !IsDefaultValue(value))
                {
                    filters.Add(property.Name, value);
                }
            }

            return filters;
        }

        private static bool IsDefaultValue(object value)
        {
            if (value is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            if (value is ValueType)
            {
                return EqualityComparer<object>.Default.Equals(value, Activator.CreateInstance(value.GetType()));
            }

            return false;
        }
    }
}

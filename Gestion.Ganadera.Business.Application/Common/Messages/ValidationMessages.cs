namespace Gestion.Ganadera.Business.Application.Common.Messages
{
    /// <summary>
    /// Centraliza mensajes de validacion reutilizables en varios validators del template.
    /// </summary>
    public static class ValidationMessages
    {
        public const string CodeRequired = "El codigo no puede estar vacio.";
        public const string CodeNoSpaces = "El codigo no debe contener espacios.";
        public const string CodeMustBeNumeric = "El codigo debe ser un numero valido.";
        public const string CodeMustBeInLongRange = "El codigo debe ser un numero dentro del rango valido.";
        public const string AtLeastOneCodeRequired = "Debe proporcionar al menos un codigo.";
        public const string CodesCannotBeNull = "Los codigos no pueden ser nulos.";
        public const string CodeDigitsOnly = "El codigo debe contener solo numeros.";
        public const string KeyPropertyRequired = "La propiedad clave es obligatoria.";
        public const string KeyPropertyInvalidFormat = "La propiedad clave solo puede contener letras y guiones bajos (_).";
        public const string PropertyNameRequired = "El nombre de la propiedad no puede estar vacio.";
        public const string PropertyNameInvalidFormat = "La propiedad solo puede contener letras y guiones bajos (_).";

        public static string PropertyNotFound(string propertyName, string entityName)
            => $"La propiedad '{propertyName}' no existe en la entidad '{entityName}'.";
    }
}

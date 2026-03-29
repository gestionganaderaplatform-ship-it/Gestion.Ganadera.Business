namespace Gestion.Ganadera.API.Requests.Messages
{
    /// <summary>
    /// Reune mensajes asociados a parsing y validacion estructural de requests HTTP.
    /// </summary>
    public static class RequestMessages
    {
        public const string InvalidFilterBody = "El cuerpo del filtro debe ser un objeto JSON valido.";
        public const string FilterRequiresOneValidProperty = "Debes enviar al menos una propiedad valida para filtrar.";
        public const string FilterBodyCouldNotBeParsed = "No fue posible interpretar el cuerpo del filtro.";
        public const string FilterRequiresOneUsefulValue = "Debes enviar al menos un valor util para filtrar.";
        public const string InvalidPatchBody = "El cuerpo del PATCH debe ser un objeto JSON valido.";
        public const string PatchRequiresOneProperty = "Debes enviar al menos una propiedad para actualizar.";
        public const string PatchBodyCouldNotBeParsed = "No fue posible interpretar el cuerpo del PATCH.";
        public const string PatchCodeMustBeSentInRoute = "El codigo no debe enviarse en el cuerpo del PATCH. Debe enviarse solo en la ruta.";
        public const string PatchCodePropertyNotFound = "No se pudo identificar la propiedad de codigo del modelo de actualizacion.";
        public const string InvalidCode = "Codigo invalido.";
        public const string DefaultConnectionMissing = "La cadena de conexion 'DefaultConnection' no esta configurada.";

        public static string ForeignKeyDefaultNotFound(string viewModelName)
            => $"No se encontro ninguna propiedad con el atributo [ForeignKeyDefault] en el ViewModel '{viewModelName}'.";

        public static string PropertyNotFound(string propertyName)
            => $"La propiedad '{propertyName}' no existe en la entidad.";

        public static string PropertyMustBePrimaryKey(string propertyName)
            => $"La propiedad '{propertyName}' debe ser la llave primaria de la entidad.";

        public static string PropertyMustBeLong(string propertyName)
            => $"La propiedad '{propertyName}' debe ser de tipo long.";
    }
}

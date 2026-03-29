namespace Gestion.Ganadera.Application.Common.Constants
{
    /// <summary>
    /// Reune expresiones regulares compartidas por validadores del template.
    /// </summary>
    public static class RegexPatterns
    {
        public const string Alfanumerico = "^[a-zA-Z0-9]+$";
        public const string AlfanumericoConEspacios = "^[a-zA-Z0-9 ]+$";
        public const string AlfanumericoConAcentos = "^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ ]+$";
        public const string AlfanumericoConAcentosYPuntuacion = "^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ.,;:¡!¿?()\"'\\- ]+$";
        public const string SoloLetras = "^[a-zA-Z]+$";
        public const string SoloLetrasConGuionBajo = "^[a-zA-Z_]+$";
        public const string SoloLetrasConEspacios = "^[a-zA-Z ]+$";
        public const string SoloLetrasSinEspacios = "^[a-zA-Z]+$";
        public const string SoloLetrasConAcentos = "^[a-zA-ZáéíóúÁÉÍÓÚñÑ ]+$";
        public const string SoloLetrasConAcentosSinEspacios = "^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$";
        public const string SoloNumeros = "^[0-9]+$";
        public const string NumerosDecimales = @"^\d+(\.\d+)?$";
        public const string NumerosNegativos = @"^-?\d+(\.\d+)?$";
        public const string Email = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string Telefono = @"^\+?[0-9]{7,15}$";
        public const string CodigoPostal = @"^\d{4,10}$";
        public const string ContrasenaSegura = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
        public const string FechaISO = @"^\d{4}-\d{2}-\d{2}$";
        public const string FechaFlexible = @"^(0[1-9]|[12][0-9]|3[01])[-/](0[1-9]|1[0-2])[-/]\d{4}$|^\d{4}[-/](0[1-9]|1[0-2])[-/](0[1-9]|[12][0-9]|3[01])$";
        public const string Url = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";
        public const string IPv4 = @"^(?:\d{1,3}\.){3}\d{1,3}$";
        public const string IPv6 = @"^([0-9a-fA-F]{1,4}:){7,7}[0-9a-fA-F]{1,4}$";
        public const string DocumentoIdentidad = @"^\d{6,15}$";
        public const string PlacaVehiculo = @"^[A-Z]{3}-\d{3,4}$";
        public const string NombreUsuario = @"^[a-zA-Z0-9]{3,20}$";
        public const string IdentificadorActor = @"^[a-zA-Z0-9@._:-]+$";
        public const string SoloEspacios = @"^\s+$";
        public const string SinEspaciosInicioFin = @"^\S.*\S$";
        public const string SinCaracteresEspeciales = @"^[a-zA-Z0-9 ]+$";
    }
}

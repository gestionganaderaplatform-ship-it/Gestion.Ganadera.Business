namespace Gestion.Ganadera.Business.API.Options
{
    /// <summary>
    /// Opciones que identifican tecnicamente al API para trazabilidad y centralizacion operativa.
    /// </summary>
    public sealed class ApiInfoOptions
    {
        public const string SectionName = "ApiInfo";

        public string Codigo { get; set; } = "Gestion.Ganadera.Business.API";
    }
}


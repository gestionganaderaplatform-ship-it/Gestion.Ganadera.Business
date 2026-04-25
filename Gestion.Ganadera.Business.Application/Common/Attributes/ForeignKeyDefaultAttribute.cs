namespace Gestion.Ganadera.Business.Application.Common.Attributes
{
    /// <summary>
    /// Marca la propiedad foranea principal usada por endpoints genericos de consulta y eliminacion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ForeignKeyDefaultAttribute : Attribute
    {
    }
}

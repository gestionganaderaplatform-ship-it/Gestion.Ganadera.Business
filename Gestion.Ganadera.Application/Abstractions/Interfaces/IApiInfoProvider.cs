namespace Gestion.Ganadera.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone la identidad tecnica del API para trazabilidad y centralizacion de datos operativos.
    /// </summary>
    public interface IApiInfoProvider
    {
        string ApiCodigo { get; }
    }
}

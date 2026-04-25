namespace Gestion.Ganadera.Business.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone el cliente actual resuelto desde claims sin acoplar las capas internas a HTTP.
    /// </summary>
    public interface ICurrentClientProvider
    {
        long? ClientNumericId { get; }
        string? ClientId { get; }
    }
}

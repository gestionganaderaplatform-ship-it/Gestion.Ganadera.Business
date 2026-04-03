namespace Gestion.Ganadera.Application.Abstractions.Interfaces
{
    /// <summary>
    /// Expone el actor actual resuelto desde el contexto de ejecucion sin acoplar las capas internas a HTTP.
    /// </summary>
    public interface ICurrentActorProvider
    {
        string? ActorId { get; }
        string? ActorEmail { get; }
        long? ActorNumericId { get; }
    }
}

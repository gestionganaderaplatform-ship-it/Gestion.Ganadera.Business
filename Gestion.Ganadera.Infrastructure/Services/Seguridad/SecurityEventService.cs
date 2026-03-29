using AutoMapper;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Observability.Interfaces;
using Gestion.Ganadera.Application.Observability.ViewModels;
using Gestion.Ganadera.Infrastructure.Persistence;
using Gestion.Ganadera.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Infrastructure.Seguridad
{
    /// <summary>
    /// Registra eventos tecnicos de seguridad sin acoplar el API a detalles de persistencia.
    /// </summary>
    public sealed class SecurityEventService(
        AppDbContext context,
        IMapper mapper,
        IApiInfoProvider apiInfoProvider) : ISecurityEventService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;

        public async Task RegistrarAsync(EventoSeguridadViewModel evento)
        {
            try
            {
                var entidad = _mapper.Map<EventoSeguridad>(evento);
                entidad.Evento_Seguridad_Api_Codigo = _apiInfoProvider.ApiCodigo;

                _context.Seguridad_Eventos.Add(entidad);
                await _context.SaveChangesAsync();
            }
            catch
            {
                // Nunca romper el request por seguridad
            }
        }
    }
}

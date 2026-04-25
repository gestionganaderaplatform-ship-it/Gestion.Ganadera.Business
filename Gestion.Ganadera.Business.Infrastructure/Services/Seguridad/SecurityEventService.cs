using AutoMapper;
using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Business.Application.Observability.Interfaces;
using Gestion.Ganadera.Business.Application.Observability.ViewModels;
using Gestion.Ganadera.Business.Infrastructure.Persistence;
using Gestion.Ganadera.Business.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Seguridad
{
    /// <summary>
    /// Registra eventos tecnicos de seguridad sin acoplar el API a detalles de persistencia.
    /// </summary>
    public sealed class SecurityEventService(
        AppDbContext context,
        IMapper mapper,
        IApiInfoProvider apiInfoProvider,
        ICurrentClientProvider currentClientProvider) : ISecurityEventService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IApiInfoProvider _apiInfoProvider = apiInfoProvider;
        private readonly ICurrentClientProvider _currentClientProvider = currentClientProvider;

        public async Task RegistrarAsync(EventoSeguridadViewModel evento)
        {
            try
            {
                var entidad = _mapper.Map<EventoSeguridad>(evento);
                entidad.Evento_Seguridad_Api_Codigo = _apiInfoProvider.ApiCodigo;
                entidad.Cliente_Codigo ??= _currentClientProvider.ClientNumericId;

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

using AutoMapper;
using Gestion.Ganadera.Application.Abstractions.Interfaces;
using Gestion.Ganadera.Application.Observability.Interfaces;
using Gestion.Ganadera.Application.Observability.ViewModels;
using Gestion.Ganadera.Infrastructure.Persistence;

namespace Gestion.Ganadera.Infrastructure.Services.Observability
{
    /// <summary>
    /// Persiste metricas tecnicas de requests usando la infraestructura configurada del template.
    /// </summary>
    public sealed class RequestMetricasService(
        AppDbContext context,
        IMapper mapper,
        ICurrentClientProvider currentClientProvider) : IRequestMetricasService
    {
        private readonly AppDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentClientProvider _currentClientProvider = currentClientProvider;

        public async Task RegistrarAsync(MetricaSolicitudViewModel metrica)
        {
            var entidad = _mapper.Map<Infrastructure.Observability.Models.MetricaSolicitud>(metrica);
            entidad.Cliente_Codigo ??= _currentClientProvider.ClientNumericId;

            _context.MetricasSolicitud.Add(entidad);
            await _context.SaveChangesAsync();
        }
    }
}

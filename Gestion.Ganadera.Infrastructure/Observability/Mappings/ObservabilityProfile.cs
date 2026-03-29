using AutoMapper;
using Gestion.Ganadera.Application.Observability.ViewModels;
using Gestion.Ganadera.Infrastructure.Observability.Models;

namespace Gestion.Ganadera.Infrastructure.Observability.Mappings
{
    /// <summary>
    /// Mapea contratos de observabilidad de Application hacia modelos tecnicos de Infrastructure.
    /// </summary>
    public class ObservabilityProfile : Profile
    {
        public ObservabilityProfile()
        {
            CreateMap<MetricaSolicitudViewModel, MetricaSolicitud>();
        }
    }
}

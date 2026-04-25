using AutoMapper;
using Gestion.Ganadera.Business.Application.Observability.ViewModels;
using Gestion.Ganadera.Business.Infrastructure.Observability.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Observability.Mappings
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

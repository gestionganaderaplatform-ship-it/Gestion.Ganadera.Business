using AutoMapper;
using Gestion.Ganadera.Business.Application.Observability.ViewModels;
using Gestion.Ganadera.Business.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Business.Infrastructure.Security.Mappings
{
    /// <summary>
    /// Mapea contratos de seguridad de Application hacia modelos tecnicos de Infrastructure.
    /// </summary>
    public class SecurityProfile : Profile
    {
        public SecurityProfile()
        {
            CreateMap<EventoSeguridadViewModel, EventoSeguridad>();
        }
    }
}

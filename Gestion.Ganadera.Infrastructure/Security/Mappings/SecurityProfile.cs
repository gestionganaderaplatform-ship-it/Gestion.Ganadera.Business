using AutoMapper;
using Gestion.Ganadera.Application.Observability.ViewModels;
using Gestion.Ganadera.Infrastructure.Security.Models;

namespace Gestion.Ganadera.Infrastructure.Security.Mappings
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

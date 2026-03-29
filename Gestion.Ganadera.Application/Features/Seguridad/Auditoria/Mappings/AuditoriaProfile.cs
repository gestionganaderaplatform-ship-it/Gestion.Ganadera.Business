using AutoMapper;
using Gestion.Ganadera.Application.Features.Seguridad.Auditoria.ViewModels;
using AuditoriaEntity = Gestion.Ganadera.Domain.Features.Seguridad.Auditoria;

namespace Gestion.Ganadera.Application.Features.Seguridad.Auditoria.Mappings
{
    public class AuditoriaProfile : Profile
    {
        public AuditoriaProfile()
        {
            CreateMap<AuditoriaEntity, AuditoriaViewModel>().ReverseMap();
            CreateMap<AuditoriaViewModel, AuditoriaEntity>().ReverseMap();
        }
    }
}

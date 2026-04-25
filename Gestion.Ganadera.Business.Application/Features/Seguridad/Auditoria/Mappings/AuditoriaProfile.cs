using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.ViewModels;
using AuditoriaEntity = Gestion.Ganadera.Business.Domain.Features.Seguridad.Auditoria;

namespace Gestion.Ganadera.Business.Application.Features.Seguridad.Auditoria.Mappings
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

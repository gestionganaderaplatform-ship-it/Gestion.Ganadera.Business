using AutoMapper;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.ViewModels;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CausasMuerte.Mappings;

public class CausaMuerteMappingProfile : Profile
{
    public CausaMuerteMappingProfile()
    {
        CreateMap<CausaMuerte, CausaMuerteViewModel>().ReverseMap();
        CreateMap<CausaMuerteCreateViewModel, CausaMuerte>();
        CreateMap<CausaMuerteUpdateViewModel, CausaMuerte>();
    }
}

using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.RangosEdades.Interfaces;

public interface IRangoEdadService
    : IBaseService<RangoEdadViewModel, RangoEdadCreateViewModel, RangoEdadUpdateViewModel>
{
}

using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.RangosEdades.Interfaces;

public interface IRangoEdadService
    : IBaseService<RangoEdadViewModel, RangoEdadCreateViewModel, RangoEdadUpdateViewModel>
{
}

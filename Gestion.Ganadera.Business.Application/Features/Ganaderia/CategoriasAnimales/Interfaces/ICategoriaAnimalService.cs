using Gestion.Ganadera.Business.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;

public interface ICategoriaAnimalService
    : IBaseService<CategoriaAnimalViewModel, CategoriaAnimalCreateViewModel, CategoriaAnimalUpdateViewModel>
{
}

using Gestion.Ganadera.Application.Features.Base.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;

namespace Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;

public interface ICategoriaAnimalService
    : IBaseService<CategoriaAnimalViewModel, CategoriaAnimalCreateViewModel, CategoriaAnimalUpdateViewModel>
{
}

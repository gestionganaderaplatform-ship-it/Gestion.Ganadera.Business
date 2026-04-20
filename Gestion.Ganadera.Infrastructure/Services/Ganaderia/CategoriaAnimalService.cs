using AutoMapper;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;
using Gestion.Ganadera.Domain.Features.Ganaderia;
using Gestion.Ganadera.Infrastructure.Services.Base;

namespace Gestion.Ganadera.Infrastructure.Services.Ganaderia;

public class CategoriaAnimalService(
    ICategoriaAnimalRepository repository,
    IMapper mapper)
    : BaseService<CategoriaAnimal, CategoriaAnimalViewModel, CategoriaAnimalCreateViewModel, CategoriaAnimalUpdateViewModel, ICategoriaAnimalRepository>(repository, mapper), ICategoriaAnimalService
{
}

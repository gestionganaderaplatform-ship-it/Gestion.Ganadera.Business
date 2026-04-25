using Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Interfaces;
using Gestion.Ganadera.Business.Domain.Features.Ganaderia;
using Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Repositories.Ganaderia;

public class CategoriaAnimalRepository(AppDbContext context) : BaseRepository<CategoriaAnimal>(context), ICategoriaAnimalRepository
{
    public Task<bool> ExisteNombreAsync(
        string categoriaAnimalNombre,
        long? categoriaAnimalCodigoExcluir = null,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet
            .AsNoTracking()
            .Where(item => item.Categoria_Animal_Nombre == categoriaAnimalNombre);

        if (categoriaAnimalCodigoExcluir.HasValue)
        {
            query = query.Where(item => item.Categoria_Animal_Codigo != categoriaAnimalCodigoExcluir.Value);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task<bool> EsCompatibleConSexoAsync(
        long categoriaAnimalCodigo,
        string animalSexo,
        CancellationToken cancellationToken = default)
    {
        var sexoEsperado = await _dbSet
            .AsNoTracking()
            .Where(item => item.Categoria_Animal_Codigo == categoriaAnimalCodigo)
            .Select(item => item.Categoria_Animal_Sexo_Esperado)
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(sexoEsperado))
        {
            return true;
        }

        return NormalizarSexo(sexoEsperado) == NormalizarSexo(animalSexo);
    }

    private static string NormalizarSexo(string sexo)
    {
        return sexo.Trim().ToUpperInvariant() switch
        {
            "M" => "MACHO",
            "MACHO" => "MACHO",
            "H" => "HEMBRA",
            "HEMBRA" => "HEMBRA",
            _ => string.Empty
        };
    }
}

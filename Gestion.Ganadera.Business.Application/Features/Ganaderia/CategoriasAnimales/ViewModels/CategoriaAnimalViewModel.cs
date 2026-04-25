using Gestion.Ganadera.Business.Application.Abstractions.Interfaces;
using CategoriaAnimalEntity = Gestion.Ganadera.Business.Domain.Features.Ganaderia.CategoriaAnimal;

namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.ViewModels;

public class CategoriaAnimalViewModel : IMapsToEntity<CategoriaAnimalEntity>
{
    public long Categoria_Animal_Codigo { get; set; }
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string? Categoria_Animal_Sexo_Esperado { get; set; }
    public long? Categoria_Animal_Rango_Edad_Codigo_Referencia { get; set; }
    public int? Categoria_Animal_Rango_Edad_Minima_Meses { get; set; }
    public int? Categoria_Animal_Rango_Edad_Maxima_Meses { get; set; }
    public string? Categoria_Animal_Rango_Edad_Descripcion { get; set; }
    public int Categoria_Animal_Orden { get; set; }
    public bool Categoria_Animal_Activa { get; set; } = true;
}

public class CategoriaAnimalCreateViewModel : IMapsToEntity<CategoriaAnimalEntity>
{
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string? Categoria_Animal_Sexo_Esperado { get; set; }
    public int Categoria_Animal_Orden { get; set; }
    public bool Categoria_Animal_Activa { get; set; } = true;
}

public class CategoriaAnimalUpdateViewModel : IMapsToEntity<CategoriaAnimalEntity>
{
    public long Categoria_Animal_Codigo { get; set; }
    public string Categoria_Animal_Nombre { get; set; } = string.Empty;
    public string? Categoria_Animal_Sexo_Esperado { get; set; }
    public int Categoria_Animal_Orden { get; set; }
    public bool Categoria_Animal_Activa { get; set; } = true;
}

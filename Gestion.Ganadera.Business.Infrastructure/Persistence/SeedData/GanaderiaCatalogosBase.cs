using Gestion.Ganadera.Business.Domain.Features.Ganaderia;

namespace Gestion.Ganadera.Business.Infrastructure.Persistence.SeedData;

internal static class GanaderiaCatalogosBase
{
    public static IReadOnlyList<TipoIdentificador> ObtenerTiposIdentificador()
    {
        return
        [
            new TipoIdentificador
            {
                Tipo_Identificador_Nombre = "Identificador interno del sistema",
                Tipo_Identificador_Codigo_Interno = "INTERNO_SISTEMA",
                Tipo_Identificador_Operativo = false,
                Tipo_Identificador_Permite_Busqueda = true,
                Tipo_Identificador_Permite_Principal = false
            },
            new TipoIdentificador
            {
                Tipo_Identificador_Nombre = "Generacion automatica",
                Tipo_Identificador_Codigo_Interno = "GENERACION_AUTOMATICA",
                Tipo_Identificador_Operativo = true,
                Tipo_Identificador_Permite_Busqueda = true,
                Tipo_Identificador_Permite_Principal = true
            },
            new TipoIdentificador
            {
                Tipo_Identificador_Nombre = "Identificador propio",
                Tipo_Identificador_Codigo_Interno = "PROPIO",
                Tipo_Identificador_Operativo = true,
                Tipo_Identificador_Permite_Busqueda = true,
                Tipo_Identificador_Permite_Principal = true
            }
        ];
    }

    public static IReadOnlyList<CategoriaAnimal> ObtenerCategoriasAnimal()
    {
        return
        [
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Becerra",
                Categoria_Animal_Sexo_Esperado = "Hembra",
                Categoria_Animal_Orden = 1
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Becerro",
                Categoria_Animal_Sexo_Esperado = "Macho",
                Categoria_Animal_Orden = 2
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Novilla",
                Categoria_Animal_Sexo_Esperado = "Hembra",
                Categoria_Animal_Orden = 3
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Torete",
                Categoria_Animal_Sexo_Esperado = "Macho",
                Categoria_Animal_Orden = 4
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Novillo",
                Categoria_Animal_Sexo_Esperado = "Macho",
                Categoria_Animal_Orden = 5
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Vaca",
                Categoria_Animal_Sexo_Esperado = "Hembra",
                Categoria_Animal_Orden = 6
            },
            new CategoriaAnimal
            {
                Categoria_Animal_Nombre = "Toro",
                Categoria_Animal_Sexo_Esperado = "Macho",
                Categoria_Animal_Orden = 7
            }
        ];
    }

    public static IReadOnlyList<RangoEdad> ObtenerRangosEdad()
    {
        return
        [
            new RangoEdad
            {
                Rango_Edad_Nombre = "0 a 6 meses",
                Rango_Edad_Edad_Minima_Meses = 0,
                Rango_Edad_Edad_Maxima_Meses = 6,
                Rango_Edad_Orden = 1
            },
            new RangoEdad
            {
                Rango_Edad_Nombre = "7 a 12 meses",
                Rango_Edad_Edad_Minima_Meses = 7,
                Rango_Edad_Edad_Maxima_Meses = 12,
                Rango_Edad_Orden = 2
            },
            new RangoEdad
            {
                Rango_Edad_Nombre = "13 a 24 meses",
                Rango_Edad_Edad_Minima_Meses = 13,
                Rango_Edad_Edad_Maxima_Meses = 24,
                Rango_Edad_Orden = 3
            },
            new RangoEdad
            {
                Rango_Edad_Nombre = "25 a 36 meses",
                Rango_Edad_Edad_Minima_Meses = 25,
                Rango_Edad_Edad_Maxima_Meses = 36,
                Rango_Edad_Orden = 4
            },
            new RangoEdad
            {
                Rango_Edad_Nombre = "Mas de 36 meses",
                Rango_Edad_Edad_Minima_Meses = 37,
                Rango_Edad_Edad_Maxima_Meses = null,
                Rango_Edad_Orden = 5
            }
        ];
    }
}

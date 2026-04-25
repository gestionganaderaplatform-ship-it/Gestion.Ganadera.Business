namespace Gestion.Ganadera.Business.Application.Features.Ganaderia.CategoriasAnimales.Messages;

public static class CategoriaAnimalValidationMessages
{
    public const string CategoriaAnimalNoExiste = "La categoria animal indicada no existe.";
    public const string CategoriaAnimalNombreDuplicado = "Ya existe una categoria animal con ese nombre.";
    public const string CategoriaAnimalNombreFormatoInvalido = "El nombre de la categoria contiene caracteres no permitidos.";
    public const string CategoriaAnimalNombreNoDebeEmpezarOTerminarConEspacios = "El nombre de la categoria no debe empezar ni terminar con espacios.";
    public const string CategoriaAnimalCodigoInvalido = "El codigo de la categoria debe ser mayor que cero.";
}

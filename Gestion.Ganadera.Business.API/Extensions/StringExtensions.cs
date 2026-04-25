namespace Gestion.Ganadera.Business.API.Extensions
{
    /// <summary>
    /// Expone conversiones simples usadas por controllers y helpers HTTP.
    /// </summary>
    public static class StringExtensions
    {
        public static long ToLong(this string input)
            => long.Parse(input);
    }
}

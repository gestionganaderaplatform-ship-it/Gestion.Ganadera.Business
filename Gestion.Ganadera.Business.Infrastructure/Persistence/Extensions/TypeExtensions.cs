namespace Gestion.Ganadera.Business.Infrastructure.Persistence.Extensions
{
    /// <summary>
    /// Expone ayudas de reflexion y clasificacion de tipos reutilizadas por la infraestructura.
    /// </summary>
    public static class TypeExtensions
    {
        public static bool IsNumericType(this Type type)
        {
            var typeCode = Type.GetTypeCode(type);

            return typeCode == TypeCode.Int16 || typeCode == TypeCode.Int32 || typeCode == TypeCode.Int64
                || typeCode == TypeCode.UInt16 || typeCode == TypeCode.UInt32 || typeCode == TypeCode.UInt64
                || typeCode == TypeCode.Decimal || typeCode == TypeCode.Single || typeCode == TypeCode.Double;
        }
    }
}

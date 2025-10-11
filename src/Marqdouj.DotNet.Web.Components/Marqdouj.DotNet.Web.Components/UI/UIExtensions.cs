using System.Numerics;

namespace Marqdouj.DotNet.Web.Components.UI
{
    internal static class UIExtensions
    {
        internal static bool IsNumerical(this Type type) 
            => type.IsNumericType() || Nullable.GetUnderlyingType(type).IsNumericType();

        internal static bool IsNumericType(this Type? type, bool includeByte = false)
        {
            if (type == null) return false;

            if (type == typeof(byte) || type == typeof(sbyte))
                return includeByte;

            //Check using IsAssignableFrom
            if (typeof(INumber<double>).IsAssignableFrom(type))
                return true;

            //Fallback
            return
                type == typeof(double) ||
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(float) ||
                type == typeof(decimal) ||
                type == typeof(short) ||
                type == typeof(ushort) ||
                type == typeof(uint) ||
                type == typeof(ulong);
        }

        internal static string? ToFormatValue(this object? value, string? format = null)
        {
            var hasFormat = !string.IsNullOrWhiteSpace(format);

            return value switch
            {
                null => null,
                string s => s,
                double n => hasFormat ? n.ToString(format) : n.ToString(),
                decimal n => hasFormat ? n.ToString(format) : n.ToString(),
                float n => hasFormat ? n.ToString(format) : n.ToString(),
                int n => hasFormat ? n.ToString(format) : n.ToString(),
                uint n => hasFormat ? n.ToString(format) : n.ToString(),
                nint n => hasFormat ? n.ToString(format) : n.ToString(),
                nuint n => hasFormat ? n.ToString(format) : n.ToString(),
                long n => hasFormat ? n.ToString(format) : n.ToString(),
                ulong n => hasFormat ? n.ToString(format) : n.ToString(),
                short n => hasFormat ? n.ToString(format) : n.ToString(),
                ushort n => hasFormat ? n.ToString(format) : n.ToString(),
                _ => value?.ToString(),
            };
        }
    }
}

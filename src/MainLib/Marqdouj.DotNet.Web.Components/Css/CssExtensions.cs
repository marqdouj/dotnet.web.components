using System.Text.RegularExpressions;

namespace Marqdouj.DotNet.Web.Components.Css
{
    public static class CssExtensions
    {
        /// <summary>  
        /// Creates a valid css identifier from a Guid.
        /// </summary>  
        /// <param name="guid">If <see cref="Guid"/> is Empty then uses Guid.NewGuid().</param>  
        /// <param name="prefix">
        /// A guid might start with a number, so prefix it with a valid css prefix. Validated using 
        /// the pattern ^[A-Za-z_]+$
        /// Default is "g_".
        /// <see href="https://developer.mozilla.org/en-US/docs/Web/CSS/ident"/>
        /// <see href="https://www.w3.org/TR/CSS21/syndata.html#value-def-identifier"/>"/>
        /// </param>  
        /// <param name="format"><see cref="Guid.ToString()"/></param>
        /// <returns>
        /// Guid converted to css identifier.
        /// Exception if <paramref name="prefix"/> is null or whitespace,
        /// or the prefix does not match the expected RegEx pattern.
        /// </returns>  
        public static string ToCssId(this Guid guid, string prefix = "g_", string format = "")
        {
            if (guid == Guid.Empty)
                guid = Guid.NewGuid();

            ArgumentNullException.ThrowIfNullOrWhiteSpace(prefix, nameof(prefix));
            prefix = prefix.Trim();

            var pattern = "^[A-Za-z_]+$";
            if (!Regex.IsMatch(prefix, pattern))
                throw new ArgumentException(nameof(pattern), $"Invalid prefix: {prefix}");

            return $"{prefix}{guid.ToString(format)}";
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Marqdouj.DotNet.Web.Components.UI
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Get Display attribute name if present.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="useNameIfNotFound">Flag to use the Name (ToString()) if Display attribute is not found. Default is <see langword="true"/></param>
        /// <returns></returns>
        public static string? GetDisplayName<TEnum>(this TEnum value, bool useNameIfNotFound = true) where TEnum : Enum
        {
            var name = value.ToString();
            var field = typeof(TEnum).GetField(name);
            var displayAttr = field?.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            return displayAttr?.Name ?? (useNameIfNotFound ? name : null);
        }

        /// <summary>
        /// Get Display attribute name if present.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="useNameIfNotFound">Flag to use the Name if Display attribute is not found. Default is <see langword="true"/></param>
        /// <returns></returns>
        public static string? GetDisplayName(this PropertyInfo prop, bool useNameIfNotFound = true)
        {
            var displayAttr = prop.GetCustomAttributes<DisplayAttribute>().FirstOrDefault();
            return displayAttr?.Name ?? (useNameIfNotFound ? prop.Name : null);
        }
    }
}


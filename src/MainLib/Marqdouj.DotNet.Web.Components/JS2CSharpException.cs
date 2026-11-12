using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.Components
{
    /// <summary>
    /// When a <see cref="JSException"/> is caught by this library, 
    /// it converts it to this exception and throws it.
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="ex"><see cref="JSException"/></param>
    public class JS2CSharpException(JSException ex) : Exception(ParseMessage(ex))
    {
        private static string ParseMessage(JSException ex)
        {
            var message = ex.Message;
            var index = message.IndexOf("Error:", StringComparison.OrdinalIgnoreCase);

            if (index > -1)
                message = message.Substring(0, index).Replace("\n", " ");

            return message;
        }

        /// <summary>
        /// The full message returned in the <see cref="JSException"/>. Normally includes message and stack combined.
        /// </summary>
        public string FullMessage { get; } = ex.Message;

        /// <summary>
        /// Logs the <see cref="FullMessage"/>.
        /// </summary>
        /// <param name="logger"></param>
        public void LogError(ILogger logger) => logger?.LogError(FullMessage);
    }
}

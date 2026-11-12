using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.Components
{
    internal static class ModuleExtensions
    {
        /// <summary>
        /// Wraps calls to moduleTask.InvokeAsync with a try/catch.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleTask"></param>
        /// <param name="identifier"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="JS2CSharpException"></exception>
        public static async ValueTask<T> InvokeAsyncTC<T>(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                return await module.InvokeAsync<T>(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }

        /// <summary>
        /// Wraps calls to moduleTask.InvokeVoidAsync with a try/catch.
        /// </summary>
        /// <param name="moduleTask"></param>
        /// <param name="identifier"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="JS2CSharpException"></exception>
        public static async ValueTask InvokeVoidAsyncTC(this Lazy<Task<IJSObjectReference>> moduleTask, string identifier, params object?[]? args)
        {
            try
            {
                var module = await moduleTask.Value;
                await module.InvokeVoidAsync(identifier, args);
            }
            catch (JSException ex)
            {
                throw new JS2CSharpException(ex);
            }
        }
    }
}

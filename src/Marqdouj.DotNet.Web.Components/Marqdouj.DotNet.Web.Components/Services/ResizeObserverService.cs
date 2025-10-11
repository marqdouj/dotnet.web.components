using Microsoft.JSInterop;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Marqdouj.DotNet.Web.Components.Services
{
    public interface IResizeObserverService
    {
        event Action<OnResizedArgs>? OnResize;

        ValueTask Observe(string id);
        ValueTask Observe(List<string> ids);
        ValueTask UnObserve(string id);
        ValueTask UnObserve(List<string> ids);
    }

    /// <summary>
    /// Scoped service to monitor html element resizing.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/ResizeObserver"/>
    /// </summary>
    public class ResizeObserverService : IResizeObserverService, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private readonly DotNetObjectReference<ResizeObserverService>? dotNetRef;
        private readonly bool debug = Debugger.IsAttached;

        [DynamicDependency(nameof(OnResized))]
        public ResizeObserverService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Marqdouj.DotNet.Web.Components/js/resizeObserver.js").AsTask());
            dotNetRef = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// <see cref="Observe(List{string}, bool)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public async ValueTask Observe(string id)
        {
            await this.Observe([id]);
        }

        /// <summary>
        /// Add elements to ResizeObserver <see href="https://developer.mozilla.org/en-US/docs/Web/API/ResizeObserver"/>
        /// </summary>
        /// <param name="ids">List of element IDs</param>
        /// <param name="debug">flag to write action to browser console; default = false</param>
        /// <returns></returns>
        public async ValueTask Observe(List<string> ids)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("observe", ids, dotNetRef, debug);
        }

        /// <summary>
        /// <see cref="UnObserve(List{string}, bool)"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="debug"></param>
        /// <returns></returns>
        public async ValueTask UnObserve(string id)
        {
            await this.UnObserve([id]);
        }

        /// <summary>
        /// Remove elements from ResizeObserver <see href="https://developer.mozilla.org/en-US/docs/Web/API/ResizeObserver"/>
        /// </summary>
        /// <param name="ids">List of element IDs</param>
        /// <param name="debug">flag to write action to browser console; default = false</param>
        /// <returns></returns>
        public async ValueTask UnObserve(List<string> ids)
        {
            var module = await moduleTask.Value;
            await module.InvokeAsync<string>("unobserve", ids, debug);
            foreach (var id in ids)
                ids.Remove(id);
        }

        /// <summary>
        /// Subscribe to this action to receive notifications when element size changes.
        /// </summary>
        public event Action<OnResizedArgs>? OnResize;

        [JSInvokable]
        public void OnResized(string id, double height, double width)
        {
            OnResize?.Invoke(new OnResizedArgs(id, height, width));
        }

        public async ValueTask DisposeAsync()
        {
            dotNetRef?.Dispose();

            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}

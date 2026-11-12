using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.Components
{
    internal class SlideshowJs(IJSRuntime jsRuntime) : IAsyncDisposable
    {
#pragma warning disable BL0016 // Unguarded JS interop call
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Marqdouj.DotNet.Web.Components/js/slideShow.js").AsTask());
#pragma warning restore BL0016 // Unguarded JS interop call

        public async ValueTask Initialize()
        {
            await moduleTask.InvokeVoidAsyncTC("initialize");
        }

        public async ValueTask PlusSlides(int direction)
        {
            await moduleTask.InvokeVoidAsyncTC("plusSlides", direction);
        }

        public async ValueTask CurrentSlide(int index)
        {
            await moduleTask.InvokeVoidAsyncTC("currentSlide", index);
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}

using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.Components
{
    internal class SlideshowJs(IJSRuntime jsRuntime) : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() =>
            jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Marqdouj.DotNet.Web.Components/js/slideShow.js").AsTask());

        public async ValueTask Initialize()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("initialize");
        }

        public async ValueTask PlusSlides(int direction)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("plusSlides", direction);
        }

        public async ValueTask CurrentSlide(int index)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("currentSlide", index);
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

using Marqdouj.DotNet.Web.Components.Geolocation;
using Microsoft.JSInterop;

namespace Marqdouj.DotNet.Web.Components.Services
{
    /// <summary>
    /// A wrapper around the device's Geolocation API services.
    /// <see href="https://developer.mozilla.org/en-US/docs/Web/API/Geolocation_API"/>.
    /// </summary>
    public interface IGeolocationService
    {
        /// <summary>
        /// A wrapper around the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/getCurrentPosition"/> function,
        /// used to get the current position of the device.
        /// </summary>
        /// <param name="options"><see cref="PositionOptions"/> used to modify the request.</param>
        /// <returns>The result of the geolocation request.</returns>
        ValueTask<GeolocationResult> GetCurrentPosition(PositionOptions? options = null);
        /// <summary>
        /// A wrapper around the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/watchPosition"/> function,
        /// used to listen for position changes. If the service is listening, a <see cref="WatchPositionReceived"/> event is fired
        /// each time the device's position changes.
        /// </summary>
        /// <param name="options"><see cref="PositionOptions"/> used to modify the request.</param>
        /// <returns>A watch ID that refers to the handler. The ID can be used to unregister the handler with <see cref="ClearWatch(long)"/>.</returns>
        ValueTask<long?> WatchPosition(PositionOptions? options = null);
        /// <summary>
        /// Handles the receipt of new positions. Fired whenever a handler is registered and the device's position changes.
        /// Invoked with the sender and the <see cref="GeolocationEventArgs"/>.
        /// </summary>
        event EventHandler<GeolocationEventArgs> WatchPositionReceived;
        /// <summary>
        /// A wrapper around the <see href="https://developer.mozilla.org/en-US/docs/Web/API/Geolocation/clearWatch"/> function,
        /// used to unregister a handler created with <see cref="WatchPosition(PositionOptions)"/>.
        /// </summary>
        /// <param name="watchId">The ID of the registered watch handler.</param>
        /// <returns>A task that represents the async clear operation.</returns>
        ValueTask ClearWatch(long watchId);
    }

    /// <summary>
    /// An implementation of <see cref="IGeolocationService"/> that provides 
    /// an interop layer for the device's Geolocation API.
    /// </summary>
    public class GeolocationService(IJSRuntime jsRuntime) : IGeolocationService, IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask = new(() => 
            jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Marqdouj.DotNet.Web.Components/js/geolocation.js").AsTask());

        public event EventHandler<GeolocationEventArgs>? WatchPositionReceived;

        /// <inheritdoc />
        public async ValueTask<GeolocationResult> GetCurrentPosition(PositionOptions? options = null)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<GeolocationResult>("Geolocation.getCurrentPosition", options);
        }

        /// <inheritdoc />
        public async ValueTask<long?> WatchPosition(PositionOptions? options = null)
        {
            var module = await moduleTask.Value;
            var callbackObj = DotNetObjectReference.Create(this);
            return await module.InvokeAsync<int>("Geolocation.watchPosition",
                callbackObj, nameof(SetWatchPosition), options);
        }

        /// <inheritdoc />
        [JSInvokable]
        public void SetWatchPosition(GeolocationResult watchResult)
        {
            WatchPositionReceived?.Invoke(this, new GeolocationEventArgs
            {
                GeolocationResult = watchResult
            });
        }

        /// <inheritdoc />
        public async ValueTask ClearWatch(long watchId)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("Geolocation.clearWatch", watchId);
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

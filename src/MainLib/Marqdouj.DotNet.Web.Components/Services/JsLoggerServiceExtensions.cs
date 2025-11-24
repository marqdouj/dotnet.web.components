using Marqdouj.DotNet.Web.Components.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Marqdouj.DotNet.Web.Components.Services
{
    public static class JsLoggerServiceExtensions
    {
        public static IServiceCollection AddJSLoggerService(this IServiceCollection services, IJSLoggerConfig? config = null)
        {
            if (config is not null)
            {
                services.AddSingleton(config);
            }

            services.AddScoped<IJSLoggerService, JSLogger>();
            services.AddScoped(typeof(IJSLoggerService<>), typeof(JSLogger<>));

            return services;
        }
    }
}

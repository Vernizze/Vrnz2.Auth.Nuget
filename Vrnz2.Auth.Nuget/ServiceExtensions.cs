using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Vrnz2.Auth.Nuget.Handler;
using Vrnz2.Auth.Nuget.Settings;
using Vrnz2.BaseInfra.Settings;

namespace Vrnz2.Auth.Nuget
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services) 
        {
            services
                .AddSettings<AuthSettings>("AuthSettings");

            SettingsHandler.Init(services.BuildServiceProvider().GetService<IOptions<AuthSettings>>().Value);

            return services;
        }
    }
}

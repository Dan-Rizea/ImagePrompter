using Application.Interfaces;
using Application.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionVersionRepository, SessionVersionRepository>();

            return services;
        }
    }
}

using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using ImagePrompter.Components.Logic;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionVersionRepository, SessionVersionRepository>();
            services.AddTransient<ISessionLogicService, SessionLogicService>();
            services.AddScoped<SessionDataService>();

            return services;
        }
    }
}

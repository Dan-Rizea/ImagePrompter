using Application.Interfaces;
using Application.Repositories;
using Application.Services.LLM;
using Application.Services.Mailing;
using Application.Services.SessionData;
using Application.Services.SessionLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMailingService, MailingService>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionVersionRepository, SessionVersionRepository>();
            services.AddTransient<ISessionLogicService, SessionLogicService>();
            services.AddTransient<ILLMServices, LLMServices>();
            services.AddScoped<ISessionDataService, SessionDataService>();

            return services;
        }
    }
}

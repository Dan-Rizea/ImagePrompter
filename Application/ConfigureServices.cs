using Application.Interfaces;
using Application.Miscellaneous;
using Application.Repositories;
using Application.Services.LLM;
using Application.Services.Mailing;
using Application.Services.ParentRefresh;
using Application.Services.SessionLogic;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            services.AddSingleton<IMailingService, MailingService>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<ISessionVersionRepository, SessionVersionRepository>();
            services.AddTransient<ISessionLogicService, SessionLogicService>();
            services.AddTransient<ILLMServices, LLMServices>();
            services.AddScoped<IParentRefreshService, ParentRefreshService>();

            return services;
        }
    }
}

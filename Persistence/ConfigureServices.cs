using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, Action<PersistenceOptions> options)
        {
            var providedOptions = new PersistenceOptions();
            options.Invoke(providedOptions);

            var migrationsAssembly = typeof(ApplicationDbContext).Assembly.FullName;
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(providedOptions.ConnectionString, builder => builder.MigrationsAssembly(migrationsAssembly)), ServiceLifetime.Transient);

            return services;
        }
    }

    public class PersistenceOptions
    {
        public string ConnectionString { get; set; }
    }
}

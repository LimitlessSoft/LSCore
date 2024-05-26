using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using LSCore.Repository;

namespace Sample.Minimal.Repository;

public static class ServiceExtensions
{
    public static void RegisterDatabase(this IServiceCollection serviceCollection, IConfigurationRoot configurationRoot)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        serviceCollection.AddEntityFrameworkNpgsql()
            .AddDbContext<SampleDbContext>((services, options) =>
            {
                options.ConfigureDbContext(configurationRoot, "Sample.Minimal.DbMigrations")
                    .UseInternalServiceProvider(services);
            });
    }
}
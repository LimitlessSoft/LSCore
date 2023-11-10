using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using LSCore.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;

namespace LSCore.Repository
{
    public static class LSCoreExtensions
    {
        public static EntityTypeBuilder<TEntity> AddMap<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, ILSCoreEntityMap<TEntity> map)
            where TEntity : class
        {
            return map.Map(entityTypeBuilder);
        }

        public static void ConfigureNpgsqlDatabase<TDbContext, TStartup>(this IConfigurationRoot configurationRoot, IServiceCollection services, string dbName) where TDbContext : DbContext
            where TStartup : ILSCoreMigratable
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TDbContext>((services, options) =>
                {
                    options.ConfigureDbContext(configurationRoot, typeof(TStartup).Namespace, dbName).UseInternalServiceProvider(services);
                });
        }

        public static DbContextOptionsBuilder ConfigureDbContext(this DbContextOptionsBuilder dbContextOptionsBuilder, IConfigurationRoot configurationRoot, string migrationAssembly, string dbName)
        {
            var postgresHost = configurationRoot["POSTGRES_HOST"];
            var postgresPort = configurationRoot["POSTGRES_PORT"];
            var postgresPassword = configurationRoot["POSTGRES_PASSWORD"];
            var connection = $"Server={postgresHost};Port={postgresPort};Userid=postgres;Password={postgresPassword};Pooling=false;MinPoolSize=1;MaxPoolSize=20;Timeout=15;Database={dbName};Include Error Detail=true;";

            return dbContextOptionsBuilder.UseNpgsql(connection, x =>
            {
                x.MigrationsHistoryTable("migrations_history");
                x.MigrationsAssembly(migrationAssembly);
            });
        }
    }
}

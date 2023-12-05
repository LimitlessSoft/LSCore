using Lamar;
using LSCore.Contracts.Interfaces;
using LSCore.Framework;
using LSCore.Repository;
using Microsoft.Extensions.DependencyInjection;
using SP.DbMigration.Repository;

namespace SP.DbMigration.App
{
    public class Startup : LSCoreBaseStartup, ILSCoreMigratable
    {
        private new const string ProjectName = "SP.DbMigration";
        public Startup()
            : base(ProjectName)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            ConfigurationRoot.ConfigureNpgsqlDatabase<MigrationDbContext, Startup>(services, "test_database");
        }

        public override void ConfigureContainer(ServiceRegistry services)
        {
            base.ConfigureContainer(services);
        }
    }
}
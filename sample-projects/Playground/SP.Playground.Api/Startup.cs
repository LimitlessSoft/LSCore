using Lamar;
using LSCore.Contracts.Interfaces;
using LSCore.Contracts.SettingsModels;
using LSCore.Framework;
using SP.Playground.Repository;
using SP.Playground.Contracts;
using LSCore.Repository;

namespace SP.Playground.Api
{
    public class Startup : LSCoreBaseApiStartup, ILSCoreMigratable
    {
        public Startup()
            : base(Constants.ProjectName, false, false, false)
        {

        }
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            ConfigurationRoot.ConfigureNpgsqlDatabase<SPPlaygroundDbContext, Startup>(services);
        }

        public override void Configure(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
        {
            base.Configure(applicationBuilder, serviceProvider);
        }

        public override void ConfigureContainer(ServiceRegistry services)
        {
            base.ConfigureContainer(services);
        }

        public override void ConfigureIoC(ServiceRegistry services)
        {
            base.ConfigureIoC(services);
        }
    }
}
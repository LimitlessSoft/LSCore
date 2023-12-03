using Lamar;
using LSCore.Framework;

namespace SP.Simple.Api
{
    public class Startup : LSCoreBaseApiStartup
    {
        private new const string ProjectName = "SP.Simple";
        public Startup()
            : base(ProjectName,
                  addAuthentication: false,
                  useCustomAuthorizationPolicy: false)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
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
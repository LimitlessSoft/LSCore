using JasperFx.Core.Reflection;
using Lamar;
using LSCore.Contracts.SettingsModels;
using LSCore.Framework;
using Newtonsoft.Json;
using SP.Simple.Contracts;
using SP.Simple.Contracts.MockData.Products;

namespace SP.Simple.Api
{
    public class Startup : LSCoreBaseApiStartup
    {
        private new const string ProjectName = "SP.Simple";
        public Startup()
            : base(ProjectName,
                  addAuthentication: true,
                  useCustomAuthorizationPolicy: false,
                  apiKeyAuthentication: true)
        {
            SeedMockData();
        }

        private void SeedMockData()
        {
            ProductsMockData.SeedData();
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

            // Comment this part out if you do not want to start this API with MINIO.
            // If you do so, everything inside ImagesController will fail since DI will not be able to resolve LSCoreMinioSettings.
            services.For<LSCoreMinioSettings>().Use(new LSCoreMinioSettings()
            {
                AccessKey = ConfigurationRoot["MINIO_ACCESS_KEY"]!,
                BucketBase = Constants.Minio.BucketBase,
                Host = ConfigurationRoot["MINIO_HOST"]!,
                Port = ConfigurationRoot["MINIO_PORT"]!,
                SecretKey = ConfigurationRoot["MINIO_SECRET_KEY"]!
            });
            services.For<LSCoreApiKeysSettings>().Use(new LSCoreApiKeysSettings()
            {
                ApiKeys = ConfigurationRoot.GetSection("API_KEY").GetChildren().Select(x => x.Value).ToList()!

            });
        }

        public override void ConfigureIoC(ServiceRegistry services)
        {
            base.ConfigureIoC(services);
        }
    }
}
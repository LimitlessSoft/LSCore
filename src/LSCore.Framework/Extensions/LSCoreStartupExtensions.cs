using Lamar.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Extensions
{
    public static class LSCoreStartupExtensions
    {
        public static void CreateTDBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            Host.CreateDefaultBuilder(args)
            .UseLamar()
            .ConfigureLogging(x =>
            {
                x.ClearProviders();
                x.AddConsole();
                x.AddDebug();
            })
            .ConfigureWebHostDefaults((webBuilder) =>
            {
                webBuilder.UseStartup<TStartup>();
            })
            .Build()
            .Run();
        }
    }
}

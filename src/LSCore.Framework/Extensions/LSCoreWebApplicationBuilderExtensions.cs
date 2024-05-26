using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Extensions;

public static class LSCoreWebApplicationBuilderExtensions
{
    public static ILoggingBuilder LSCoreAddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        
        return builder.Logging;
    }
}
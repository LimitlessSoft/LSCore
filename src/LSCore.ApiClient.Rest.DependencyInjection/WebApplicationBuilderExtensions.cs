using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.ApiClient.Rest.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
    public static void AddLSCoreApiClientRest<TClient>(
        this WebApplicationBuilder builder,
        LSCoreApiClientRestConfiguration<TClient> configuration
    ) where TClient : LSCoreApiClient
    {
        builder.Services.AddSingleton(configuration ?? throw new ArgumentNullException(nameof(configuration)));
        builder.Services.AddTransient<TClient>();
    }
}
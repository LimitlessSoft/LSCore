using LSCore.Auth.Key.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.Key.DependencyInjection;

public static class Extensions
{
	public static void AddLSCoreAuthKey(
		this WebApplicationBuilder builder,
		LSCoreAuthKeyConfiguration configuration
	)
	{
		builder.Services.AddSingleton(configuration);
	}

	public static void UseLSCoreAuthKey(this WebApplication app)
	{
		app.UseMiddleware<LSCoreAuthKeyMiddleware>();
	}
}

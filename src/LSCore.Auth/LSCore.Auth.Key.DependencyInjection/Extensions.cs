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
		builder.Services.AddScoped<ILSCoreAuthKeyProvider, DummyLSCoreAuthKeyProvider>();
		builder.Services.AddSingleton(configuration);
	}

	/// <summary>
	/// Adds the LSCoreAuthKey with the default configuration.
	/// </summary>
	/// <param name="builder"></param>
	/// <typeparam name="TLSCoreAuthKeyProvider"></typeparam>
	public static void AddLSCoreAuthKey<TLSCoreAuthKeyProvider>(this WebApplicationBuilder builder)
		where TLSCoreAuthKeyProvider : class, ILSCoreAuthKeyProvider
	{
		builder.Services.AddScoped<ILSCoreAuthKeyProvider, TLSCoreAuthKeyProvider>();
		builder.Services.AddSingleton(new LSCoreAuthKeyConfiguration());
	}

	/// <summary>
	/// Adds the LSCoreAuthKey with provided configuration.
	/// </summary>
	/// <param name="builder"></param>
	/// <param name="configuration"></param>
	/// <typeparam name="TLSCoreAuthKeyProvider"></typeparam>
	public static void AddLSCoreAuthKey<TLSCoreAuthKeyProvider>(
		this WebApplicationBuilder builder,
		LSCoreAuthKeyConfiguration configuration
	)
		where TLSCoreAuthKeyProvider : class, ILSCoreAuthKeyProvider
	{
		builder.Services.AddScoped<ILSCoreAuthKeyProvider, TLSCoreAuthKeyProvider>();
		builder.Services.AddSingleton(configuration);
	}

	public static void UseLSCoreAuthKey(this WebApplication app)
	{
		app.UseMiddleware<LSCoreAuthKeyMiddleware>();
	}
}

using LSCore.Auth.Permission.Contracts;
using LSCore.Auth.Permission.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.Permission.DependencyInjection;

public static class Extensions
{
	public static void AddLSCoreAuthPermission<
		TAuthEntityIdentifier,
		TPermission,
		TLSCoreAuthPermissionIdentityEntityRepository
	>(this WebApplicationBuilder builder)
		where TPermission : Enum
		where TLSCoreAuthPermissionIdentityEntityRepository : class,
			ILSCoreAuthPermissionIdentityEntityRepository<TAuthEntityIdentifier, TPermission>
	{
		builder.Services.AddScoped<
			ILSCoreAuthPermissionIdentityEntityRepository<TAuthEntityIdentifier, TPermission>,
			TLSCoreAuthPermissionIdentityEntityRepository
		>();
		builder.Services.AddScoped<
			ILSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>,
			LSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>
		>();
	}

	public static void AddLSCoreAuthPermission<
		TAuthEntityIdentifier,
		TPermission,
		TLSCoreAuthPermissionManager,
		TLSCoreAuthPermissionIdentityEntityRepository
	>(this WebApplicationBuilder builder)
		where TPermission : Enum
		where TLSCoreAuthPermissionManager : class,
			ILSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>
		where TLSCoreAuthPermissionIdentityEntityRepository : class,
			ILSCoreAuthPermissionIdentityEntityRepository<TAuthEntityIdentifier, TPermission>
	{
		builder.Services.AddScoped<
			ILSCoreAuthPermissionIdentityEntityRepository<TAuthEntityIdentifier, TPermission>,
			TLSCoreAuthPermissionIdentityEntityRepository
		>();
		builder.Services.AddScoped<
			ILSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermission>,
			TLSCoreAuthPermissionManager
		>();
	}

	public static void UseLSCoreAuthPermission<TAuthEntityIdentifier, TPermission>(
		this WebApplication app
	)
		where TPermission : Enum
	{
		app.UseMiddleware<LSCoreAuthPermissionMiddleware<TAuthEntityIdentifier, TPermission>>();
	}
}

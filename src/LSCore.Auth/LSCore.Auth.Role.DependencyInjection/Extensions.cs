using LSCore.Auth.Role.Contracts;
using LSCore.Auth.Role.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.Role.DependencyInjection;

public static class Extensions
{
	public static void AddLSCoreAuthRole<
		TAuthEntityIdentifier,
		TRole,
		TLSCoreAuthRoleIdentityEntityRepository
	>(this WebApplicationBuilder builder)
		where TRole : Enum
		where TLSCoreAuthRoleIdentityEntityRepository : class,
			ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole>
	{
		builder.Services.AddScoped<
			ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole>,
			TLSCoreAuthRoleIdentityEntityRepository
		>();
		builder.Services.AddScoped<
			ILSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>,
			LSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>
		>();
	}

	public static void AddLSCoreAuthRole<
		TAuthEntityIdentifier,
		TRole,
		TLSCoreAuthRoleManager,
		TLSCoreAuthRoleIdentityEntityRepository
	>(this WebApplicationBuilder builder)
		where TRole : Enum
		where TLSCoreAuthRoleManager : class, ILSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>
		where TLSCoreAuthRoleIdentityEntityRepository : class,
			ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole>
	{
		builder.Services.AddScoped<
			ILSCoreAuthRoleIdentityEntityRepository<TAuthEntityIdentifier, TRole>,
			TLSCoreAuthRoleIdentityEntityRepository
		>();
		builder.Services.AddScoped<
			ILSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>,
			TLSCoreAuthRoleManager
		>();
	}

	public static void UseLSCoreAuthRole<TAuthEntityIdentifier, TRole>(this WebApplication app)
		where TRole : Enum
	{
		app.UseMiddleware<LSCoreAuthRoleMiddleware<TAuthEntityIdentifier, TRole>>();
	}
}

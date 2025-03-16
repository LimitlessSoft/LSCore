using LSCore.Auth.Contracts;
using LSCore.Auth.Role.Contracts;
using LSCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.Role.DependencyInjection;

public class LSCoreAuthRoleMiddleware<TAuthEntityIdentifier, TRole>(RequestDelegate next)
	where TRole : Enum
{
	public async Task Invoke(HttpContext context)
	{
		var authRoleAttribute = context
			.GetEndpoint()
			?.Metadata.GetMetadata<LSCoreAuthRoleAttribute<TRole>>();
		if (authRoleAttribute == null)
		{
			await next(context);
			return;
		}
		var authContextEntity = context.RequestServices.GetRequiredService<
			LSCoreAuthContextEntity<TAuthEntityIdentifier>
		>();

		// Key authenticated users are not subject to role authorization
		if (authContextEntity.Type == LSCoreAuthEntityType.Key)
		{
			await next(context);
			return;
		}

		if (authContextEntity.IsAuthenticated == false)
			throw new LSCoreUnauthenticatedException();

		var roleManager = context.RequestServices.GetRequiredService<
			ILSCoreAuthRoleManager<TAuthEntityIdentifier, TRole>
		>();

		if (!roleManager.IsInRole(authContextEntity.Identifier!, authRoleAttribute.Roles))
			throw new LSCoreForbiddenException();

		await next(context);
	}
}

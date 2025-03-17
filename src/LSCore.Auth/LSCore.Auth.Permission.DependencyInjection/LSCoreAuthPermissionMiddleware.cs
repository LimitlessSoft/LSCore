using LSCore.Auth.Contracts;
using LSCore.Auth.Permission.Contracts;
using LSCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.Permission.DependencyInjection;

public class LSCoreAuthPermissionMiddleware<TAuthEntityIdentifier, TPermisssion>(
	RequestDelegate next
)
	where TPermisssion : Enum
{
	public async Task Invoke(HttpContext context)
	{
		var authPermissionAttribute = context
			.GetEndpoint()
			?.Metadata.GetMetadata<LSCoreAuthPermissionAttribute<TPermisssion>>();
		if (authPermissionAttribute == null)
		{
			await next(context);
			return;
		}

		var authContextEntity = context.RequestServices.GetRequiredService<
			LSCoreAuthContextEntity<TAuthEntityIdentifier>
		>();

		// Key authenticated users are not subject to permission authorization
		if (authContextEntity.Type == LSCoreAuthEntityType.Key)
		{
			await next(context);
			return;
		}

		if (authContextEntity.IsAuthenticated == false)
			throw new LSCoreUnauthenticatedException();

		var permissionManager = context.RequestServices.GetRequiredService<
			ILSCoreAuthPermissionManager<TAuthEntityIdentifier, TPermisssion>
		>();

		if (
			!permissionManager.HasPermission(
				authContextEntity.Identifier!,
				authPermissionAttribute.RequireAll,
				authPermissionAttribute.Permissions
			)
		)
			throw new LSCoreForbiddenException();

		await next(context);
	}
}

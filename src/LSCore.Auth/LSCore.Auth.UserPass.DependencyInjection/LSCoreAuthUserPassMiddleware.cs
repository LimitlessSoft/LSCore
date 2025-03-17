using System.ComponentModel;
using System.Security.Claims;
using LSCore.Auth.Contracts;
using LSCore.Auth.Contracts.Constants;
using LSCore.Auth.UserPass.Contracts;
using LSCore.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LSCore.Auth.UserPass.DependencyInjection;

public class LSCoreAuthUserPassMiddleware<TAuthEntityIdentifier>(
	RequestDelegate next,
	LSCoreAuthUserPassConfiguration configuration
)
{
	public async Task Invoke(HttpContext context)
	{
		var shouldVerify = configuration.AuthAll;
		if (!shouldVerify) // If we don't need to authorize all requests, check if the current endpoint requires authorization
			shouldVerify = context.GetEndpoint()?.Metadata.GetMetadata<LSCoreAuthAttribute>() != null;

		if (
			configuration.BreakOnFailedAuth
			&& shouldVerify
			&& context.User.Identity?.IsAuthenticated == false
		)
			throw new LSCoreUnauthenticatedException();

		#region Parse Jwt and populate LSCoreContextUser for this request
		var authContextEntity = context.RequestServices.GetRequiredService<
			LSCoreAuthContextEntity<TAuthEntityIdentifier>
		>();
		if (
			context.User.Identity is
			{ IsAuthenticated: true, AuthenticationType: "AuthenticationTypes.Federation" }
		)
		{
			var jwtIdentifier = context.User.FindFirstValue(LSCoreAuthClaims.Identifier);
			if (!string.IsNullOrWhiteSpace(jwtIdentifier))
				if (jwtIdentifier != null)
				{
					authContextEntity.Identifier = (TAuthEntityIdentifier)
						TypeDescriptor
							.GetConverter(typeof(TAuthEntityIdentifier))
							.ConvertFrom(jwtIdentifier);
					authContextEntity.Type = LSCoreAuthEntityType.User;
				}
		}
		#endregion

		await next(context);
	}
}

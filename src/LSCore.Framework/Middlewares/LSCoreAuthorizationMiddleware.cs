using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LSCore.Contracts;
using LSCore.Contracts.Configurations;
using LSCore.Contracts.Exceptions;
using LSCore.Framework.Attributes;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationMiddleware(RequestDelegate next, LSCoreAuthorizationConfiguration configuration)
{
    public async Task Invoke(HttpContext context)
    {
        var shouldVerify = configuration.AuthorizeAll;
        if (!shouldVerify) // If we don't need to authorize all requests, check if the current endpoint requires authorization
            shouldVerify = context.GetEndpoint()?.Metadata.GetMetadata<LSCoreAuthorizeAttribute>() != null;
        
        if (configuration.BreakOnFailedAuthorization && shouldVerify && context.User.Identity?.IsAuthenticated == false)
            throw new LSCoreUnauthenticatedException();
        
        #region Parse Jwt and populate LSCoreContextUser for this request
        var currentUser = context.RequestServices.GetRequiredService<LSCoreContextUser>();
        if (context.User.Identity?.IsAuthenticated == true && context.User.Identity.AuthenticationType == "AuthenticationTypes.Federation")
        {
            var jwtIdentifier = context.User.FindFirstValue(LSCoreContractsConstants.ClaimNames.CustomIdentifier);
            if (!string.IsNullOrWhiteSpace(jwtIdentifier))
                currentUser!.Id = int.Parse(jwtIdentifier);
        }
        #endregion

        await next(context);
    }
}
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using LSCore.Contracts;
using LSCore.Contracts.Exceptions;
using LSCore.Framework.Attributes;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationMiddleware(RequestDelegate next, ILogger<LSCoreAuthorizationMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        #region Parse Jwt and populate LSCoreContextUser for this request
        var currentUser = context.RequestServices.GetRequiredService<LSCoreContextUser>();
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var jwtIdentifier = context.User.FindFirstValue(LSCoreContractsConstants.ClaimNames.CustomIdentifier);
            if (!string.IsNullOrWhiteSpace(jwtIdentifier))
                currentUser!.Id = int.Parse(jwtIdentifier);
        }
        #endregion
        
        #region Ensure that user is authenticated if LSAuthorizeAttribute is present
        var endpoint = context.GetEndpoint();
        var authorizeAttribute = endpoint?.Metadata.GetMetadata<LSCoreAuthorizeAttribute>();
        if (authorizeAttribute != null && context.User.Identity?.IsAuthenticated == false)
            throw new LSCoreUnauthenticatedException();
        #endregion

        await next(context);
    }
}
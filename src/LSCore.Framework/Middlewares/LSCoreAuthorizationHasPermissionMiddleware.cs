using LSCore.Contracts;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationHasPermissionMiddleware<T>(RequestDelegate next, ILogger<LSCoreAuthorizationHasPermissionMiddleware<T>> logger)
    where T : Enum
{
    public async Task Invoke(HttpContext context, LSCoreContextUser contextUser, ILSCoreHasPermissionManager<T> hasPermissionManager)
    {
        var endpoint = context.GetEndpoint();
        var authorizeAttribute = endpoint?.Metadata.GetMetadata<LSCoreAuthorizePermissionAttribute<T>>();
        if (authorizeAttribute == null)
        {
            await next(context);
            return;
        }

        if (!hasPermissionManager.HasPermission(contextUser.Id!.Value, authorizeAttribute.Permissions))
            throw new LSCoreForbiddenException();
        
        await next(context);
    }
}
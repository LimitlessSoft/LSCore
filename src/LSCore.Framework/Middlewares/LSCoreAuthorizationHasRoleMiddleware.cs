using LSCore.Contracts;
using LSCore.Contracts.Exceptions;
using LSCore.Contracts.IManagers;
using LSCore.Framework.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationHasRoleMiddleware<T>(RequestDelegate next, ILogger<LSCoreAuthorizationHasRoleMiddleware<T>> logger)
    where T : Enum
{
    public async Task Invoke(HttpContext context, LSCoreContextUser contextUser, ILSCoreHasRoleManager<T> hasRoleManager)
    {
        var endpoint = context.GetEndpoint();
        var authorizeAttribute = endpoint?.Metadata.GetMetadata<LSCoreAuthorizeRoleAttribute<T>>();
        if (authorizeAttribute == null)
        {
            await next(context);
            return;
        }

        if (!hasRoleManager.HasRole(contextUser.Id!.Value, authorizeAttribute.Roles))
            throw new LSCoreForbiddenException();
        
        await next(context);
    }
}
using LSCore.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LSCore.Framework.Middlewares;

public class LSCoreAuthorizationHasRoleMiddleware<T>(RequestDelegate next, ILogger<LSCoreAuthorizationHasRoleMiddleware<T>> logger)
    where T : Enum
{
    // public async Task Invoke(HttpContent context, LSCoreContextUser contextUser, ILSCoreHasRoleManager<T> hasRoleManager)
}
using System.Text;
using LSCore.Contracts;
using LSCore.Contracts.IManagers;
using LSCore.Framework.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LSCore.Framework.Extensions;

public static class LSCoreWebApplicationBuilderExtensions
{
    /// <summary>
    /// Used to add some default logging providers.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static ILoggingBuilder LSCoreAddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        
        return builder.Logging;
    }
    
    /// <summary>
    /// Used if you want authentication & authorization
    /// [Authorize] prevents non-authorized access
    /// Catch LSCoreContextUser object through DI to get current user if authorization token is passed and verified.
    /// Implement ILSCoreAuthorizeManager and use Authorize method to authenticate and authorize users and get Jwt in return.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddLSCoreAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<LSCoreContextUser>();
        builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://localhost:5000",
                    ValidAudience = "http://localhost:5000",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is a security key with min size of 256 bits")),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
        builder.Services.AddAuthorization();
    }
    
    /// <summary>
    /// Used to process authentication and authorization. Use it to handle LSCoreContextUser object populating.
    /// </summary>
    /// <param name="app"></param>
    public static void UseLSCoreAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LSCoreAuthorizationMiddleware>();
    }
    
    /// <summary>
    /// Used if you want to process LSCoreExceptions and alter response status code and message based on exception type.
    /// </summary>
    /// <param name="app"></param>
    public static void UseLSCoreHandleException(this WebApplication app)
    {
        app.UseMiddleware<LSCoreHandleExceptionMiddleware>();
    }

    /// <summary>
    /// Register ILSCoreHasPermissionManager to be used in LSCoreAuthorizationHasPermissionMiddleware.
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TPermissionManager"></typeparam>
    /// <typeparam name="TPermissionEnum"></typeparam>
    public static void AddLSCoreAuthorizationHasPermission<TPermissionManager, TPermissionEnum>(this WebApplicationBuilder builder)
        where TPermissionManager : class, ILSCoreHasPermissionManager<TPermissionEnum> where TPermissionEnum : Enum
    {
        builder.Services.AddScoped<ILSCoreHasPermissionManager<TPermissionEnum>, TPermissionManager>();
    }
    
    /// <summary>
    /// Used if you want to use [LSCoreAuthorizePermission(Permissions.Permission1, Permissions.Permission2...)] on endpoints.
    /// Dependency Injection require for ILSCoreHasPermissionManager to be registered.
    /// Do so by calling AddLSCoreAuthorizationHasPermission in Program.cs.
    /// </summary>
    /// <param name="app"></param>
    /// <typeparam name="TPermissionEnum"></typeparam>
    public static void UseLSCoreAuthorizationHasPermission<TPermissionEnum>(this WebApplication app)
        where TPermissionEnum : Enum
    {
        app.UseMiddleware<LSCoreAuthorizationHasPermissionMiddleware<TPermissionEnum>>();
    }
    
    /// <summary>
    /// Register ILSCoreHasRoleManager to be used in LSCoreAuthorizationHasRoleMiddleware.
    /// </summary>
    /// <param name="builder"></param>
    /// <typeparam name="TRoleManager"></typeparam>
    /// <typeparam name="TRoleEnum"></typeparam>
    public static void AddLSCoreAuthorizationHasRole<TRoleManager, TRoleEnum>(this WebApplicationBuilder builder)
        where TRoleManager : class, ILSCoreHasRoleManager<TRoleEnum> where TRoleEnum : Enum
    {
        builder.Services.AddScoped<ILSCoreHasRoleManager<TRoleEnum>, TRoleManager>();
    }
    
    /// <summary>
    /// Used if you want to use [LSCoreAuthorizeRole(Roles.Role1, Roles.Role2...)] on endpoints.
    /// Dependency Injection require for ILSCoreHasRoleManager to be registered.
    /// Do so by calling AddLSCoreAuthorizationHasRole in Program.cs.
    /// </summary>
    /// <param name="app"></param>
    /// <typeparam name="TRoleEnum"></typeparam>
    public static void UseLSCoreAuthorizationHasRole<TRoleEnum>(this WebApplication app)
        where TRoleEnum : Enum
    {
        app.UseMiddleware<LSCoreAuthorizationHasRoleMiddleware<TRoleEnum>>();
    }
}
using System.Text;
using LSCore.Contracts;
using LSCore.Contracts.Configurations;
using LSCore.Contracts.IManagers;
using LSCore.Contracts.Interfaces.Repositories;
using LSCore.Domain.Managers;
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
    /// <param name="configuration"></param>
    public static void AddLSCoreAuthorization<TAuthHandler, TAuthorizableRepository>(this WebApplicationBuilder builder, LSCoreAuthorizationConfiguration configuration)
        where TAuthHandler : LSCoreAuthorizeManager
        where TAuthorizableRepository : class, ILSCoreAuthorizableEntityRepository
    {
        builder.Services.AddScoped<LSCoreContextUser>();
        builder.Services.AddSingleton(configuration);
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
                    ValidIssuer = configuration.Issuer,
                    ValidAudience = configuration.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecurityKey)),
                    RequireExpirationTime = true,
                    ClockSkew = configuration.TokenSkew,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
        builder.Services.AddAuthorization();
        builder.Services.AddScoped<LSCoreAuthorizeManager, TAuthHandler>();
        builder.Services.AddScoped<ILSCoreAuthorizableEntityRepository, TAuthorizableRepository>();
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
    
    /// <summary>
    /// Add LSCoreApiKeyConfiguration to be used in LSCoreApiKeyAuthorizationMiddleware.
    /// For request to be authorized, it must contain a header with key and value as specified in LSCoreApiKeyConfiguration.
    /// Header used is <see cref="LSCoreContractsConstants.ApiKeyCustomHeader"/>
    /// Pair with <see cref="UseLSCoreApiKeyAuthorization"/> in Program.cs to authorize requests based on LSCoreApiKeyConfiguration.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="keyConfiguration"></param>
    public static void AddLSCoreApiKeyAuthorization(this WebApplicationBuilder builder, LSCoreApiKeyConfiguration keyConfiguration)
    {
        builder.Services.AddSingleton(keyConfiguration);
    }
    
    /// <summary>
    /// Used if you want to process LSCoreApiKeyConfiguration and authorize requests based on it.
    /// For request to be authorized, it must contain a header with key and value as specified in LSCoreApiKeyConfiguration.
    /// Use <see cref="AddLSCoreApiKeyAuthorization"/> in Program.cs to register LSCoreApiKeyConfiguration.
    /// </summary>
    /// <param name="app"></param>
    public static void UseLSCoreApiKeyAuthorization(this WebApplication app)
    {
        app.UseMiddleware<LSCoreApiKeyAuthorizationMiddleware>();
    }
}
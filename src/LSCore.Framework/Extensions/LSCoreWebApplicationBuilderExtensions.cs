using System.Text;
using LSCore.Contracts;
using LSCore.Framework.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LSCore.Framework.Extensions;

public static class LSCoreWebApplicationBuilderExtensions
{
    public static ILoggingBuilder LSCoreAddLogging(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        
        return builder.Logging;
    }
    
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
    
    public static void UseLSCoreAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<LSCoreAuthorizationMiddleware>();
    }
    
    public static void UseLSCoreHandleException(this WebApplication app)
    {
        app.UseMiddleware<LSCoreHandleExceptionMiddleware>();
    }
}
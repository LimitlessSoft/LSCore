using LSCore.Contracts.Configurations;
using LSCore.Contracts.Interfaces.Repositories;
using LSCore.Framework.Extensions;
using Sample.AuthorizationSimple.Api.Managers;
using Sample.AuthorizationSimple.Api.Repositories;

// This sample uses only simple LSCore authorization
// To see how you can use ApiKey authorization, please refer to AuthorizationApiKey sample
// To see how you can use Roles And Permissions in combination with LSCore authorization, please refer to LSCoreAuthorization sample

// To simplify things, this project doesn't use DDD, but it's recommended to use DDD in your projects

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add LSCoreAuthorization
builder.AddLSCoreAuthorization<AuthManager, UserRepository>(LoadConfiguration());

var app = builder.Build();

// Use LSCoreHandleException middleware since authorization throws LSCoreExceptions
app.UseLSCoreHandleException();
// Use LSCoreAuthorization middleware
app.UseLSCoreAuthorization();
app.MapControllers();
app.Run();

return;

LSCoreAuthorizationConfiguration LoadConfiguration()
{
    return new LSCoreAuthorizationConfiguration()
    {
        Audience = "read-JWT-documentation",
        Issuer = "read-JWT-documentation",
        SecurityKey =
            "Some Security Key which is at least 256 bits long. Best thing to do is store it in a secure place",
        AccessTokenExpirationMinutes = 1, // It has default value, however for testing purposes we set it to 1 minute
        TokenSkew = TimeSpan.Zero // needed for testing purposes since expiration is less than 5 minutes
    };
}
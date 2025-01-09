using LSCore.Contracts.Configurations;
using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Standard LSCore
builder.AddLSCoreDependencyInjection("Sample.Authorization");

// Used for Jwt generation
builder.Services.AddSingleton(new LSCoreAuthorizationConfiguration
{
    Audience = "http://localhost:5000",
    Issuer = "http://localhost:5000",
    SecurityKey = "this is a security key with min size of 256 bits"
});

// Used if you want authentication & authorization
// [Authorize] prevents non-authorized access
// Catch LSCoreContextUser object through DI to get current user if authorization token is passed and verified
builder.AddLSCoreAuthorization();

var app = builder.Build();

// Standard LSCore
app.UseLSCoreHandleException();

// Used if you want authentication & authorization
app.UseLSCoreAuthorization();

app.MapControllers();
app.Run();
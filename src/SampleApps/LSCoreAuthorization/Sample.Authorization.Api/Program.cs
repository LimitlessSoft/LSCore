using LSCore.Contracts.Configurations;
using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;
using Sample.Authorization.Contracts.Enums;
using Sample.Authorization.Contracts.Interfaces.IManagers;
using Sample.Authorization.Domain.Managers;

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

builder.AddLSCoreAuthorizationHasPermission<UserManager, Permission>();

var app = builder.Build();

// Standard LSCore
app.UseLSCoreHandleException();

// Used if you want authentication & authorization
app.UseLSCoreAuthorization();

// Used if you want to use [LSCoreAuthorizePermission(Permissions.Permission1, Permissions.Permission2...)]
app.UseLSCoreAuthorizationHasPermission<Permission>();

// Used if you want to use [LSCoreAuthorizeRole(Roles.Role1, Roles.Role2...)]
// app.useLSCoreAuthorizationHasRole();

app.MapControllers();
app.Run();
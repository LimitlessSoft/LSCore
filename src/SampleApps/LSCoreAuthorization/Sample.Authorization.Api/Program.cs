using LSCore.Contracts.Configurations;
using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;
using Sample.Authorization.Contracts.Enums;
using Sample.Authorization.Domain.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Standard LSCore
builder.AddLSCoreDependencyInjection("Sample.Authorization");

builder.AddLSCoreAuthorization(new LSCoreAuthorizationConfiguration
{
    Audience = "http://localhost:5000",
    Issuer = "http://localhost:5000",
    SecurityKey = "this is a security key with min size of 256 bits"
});

builder.AddLSCoreAuthorizationHasPermission<UserManager, Permission>();
builder.AddLSCoreAuthorizationHasRole<UserManager, Role>();

var app = builder.Build();

app.UseLSCoreHandleException();

app.UseLSCoreAuthorization();

app.UseLSCoreAuthorizationHasPermission<Permission>();
app.UseLSCoreAuthorizationHasRole<Role>();

app.MapControllers();
app.Run();
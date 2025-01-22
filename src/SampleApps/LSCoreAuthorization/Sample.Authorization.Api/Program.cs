using LSCore.Contracts.Configurations;
using LSCore.DependencyInjection.Extensions;
using LSCore.Framework.Extensions;
using Sample.Authorization.Contracts.Enums;
using Sample.Authorization.Domain.Managers;
using Sample.Authorization.Repository.Repositories;

// This project uses tree types of authorization:
// 1. Standard LSCore Authorization
// 2. LSCore Authorization Has Permission
// 3. LSCore Authorization Has Role

// To see how you can use LSCore Authorization, please refer to AuthorizationSimple sample

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Standard LSCore
builder.AddLSCoreDependencyInjection("Sample.Authorization");

builder.AddLSCoreAuthorization<UserManager, UserRepository>(new LSCoreAuthorizationConfiguration
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
using LSCore.Contracts.Configurations;
using LSCore.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddLSCoreDependencyInjection("Sample.Authorization");

builder.Services.AddSingleton(new LSCoreAuthorizationConfiguration()
{
    Audience = "http://localhost:5000",
    Issuer = "http://localhost:5000",
    SecurityKey = "this is a security key with min size of 256 bits"
});

var app = builder.Build();

app.MapControllers();

app.Run();
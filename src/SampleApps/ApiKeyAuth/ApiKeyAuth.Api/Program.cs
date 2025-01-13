using LSCore.Contracts.Configurations;
using LSCore.Framework.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddLSCoreApiKeyAuthorization(new LSCoreApiKeyConfiguration()
{
    ApiKeys = ["1234567890"]
});

var app = builder.Build();

app.UseLSCoreHandleException();

app.UseLSCoreApiKeyAuthorization();

app.Map("/", () => "Hello World!");

app.Run();